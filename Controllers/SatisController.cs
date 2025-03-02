using BirFatura.Models;
using BirFatura.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.IO; 
using PdfDocument = iTextSharp.text.Document;
using Newtonsoft.Json;
using System.Drawing;


namespace BirFatura.Controllers
{
    [Route("satis")]
    public class SatisController : Controller
    {
        private readonly TokenService _tokenService;
        private readonly SatisService _satisService;

        public SatisController(SatisService satisService, TokenService tokenService)
        {
            _satisService = satisService;
            _tokenService = tokenService;
        }


        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var tokenResponse =await _tokenService.GetTokenAsync();
            string token = tokenResponse.AccessToken;


            List<Satis> satislar = await _satisService.GetSatislarAsync(token);
            Console.WriteLine("Satislar: " + JsonConvert.SerializeObject(satislar));
            return View(satislar);

        }
        [HttpPost("faturalandir/{orderId}")]
        public async Task<IActionResult> Faturalandir(int orderId)
        {

           
            var tokenResponse = await _tokenService.GetTokenAsync();
            string token = tokenResponse.AccessToken;

           
            List<Satis> satislar = await _satisService.GetSatislarAsync(token);

            
            Satis selectedSatis = satislar.Find(s => s.OrderId == orderId);
            if (selectedSatis == null)
                return NotFound();

            using (MemoryStream ms = new MemoryStream())
            {

                PdfDocument document = new PdfDocument(PageSize.A4); 
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

              
                document.Add(new Paragraph("Fatura"));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph($"Siparis ID: {selectedSatis.OrderId}"));
                document.Add(new Paragraph($"Müşteri Adı: {selectedSatis.CustomerName}"));
                document.Add(new Paragraph($"Müşteri Adresi: {selectedSatis.CustomerAddress}"));
                document.Add(new Paragraph($"Müşteri Telefon Numarasi: {selectedSatis.CustomerPhone}"));
                document.Add(new Paragraph($"Müşteri Yaşadığı Şehir: {selectedSatis.CustomerCity } "));
                document.Add(new Paragraph($"Müşteri TCVKN: {selectedSatis.CustomerTaxNumber}"));
                document.Add(new Paragraph($"Müşteri Vergi Dairesi: {selectedSatis.CustomerTaxOffice} "));
                document.Add(new Paragraph($"Sipariş Tarihi: {DateTime.Now:dd/MM/yyyy}"));
               

                document.Add(new Paragraph(" "));


                decimal toplamTutar = 0;
               
                if (selectedSatis.Products != null && selectedSatis.Products.Count > 0)
                {
                    document.Add(new Paragraph("Ürünler:"));

                    PdfPTable table = new PdfPTable(6);
                    table.AddCell("Ürün ID");
                    table.AddCell("Ürün Adı");
                    table.AddCell("Stok Kodu");
                    table.AddCell("Satis Adeti");
                    table.AddCell("Kdv Orani");
                    table.AddCell("Kdv Dahil Birim Fiyatı");

                    foreach (var urun in selectedSatis.Products)
                    {
                        table.AddCell(urun.ProductId.ToString());
                        table.AddCell(urun.ProductName.ToString());
                        table.AddCell(urun.StockCode.ToString());         
                        table.AddCell(urun.Quantity.ToString());
                        table.AddCell(urun.TaxRate.ToString());
                        table.AddCell(urun.PriceWithTax.ToString("C"));

                         toplamTutar += urun.PriceWithTax * urun.Quantity;
                    }
                    
                    document.Add(table);
                }
                document.Add(new Paragraph($"Toplam Tutar: {toplamTutar.ToString("C")}"));
                document.Close();
                byte[] pdfBytes = ms.ToArray();
                return File(pdfBytes, "application/pdf", "fatura.pdf");
            }
        }

    }
}
