using Newtonsoft.Json;

namespace BirFatura.Models
{
    public class SatisUrun
    {
        [JsonProperty("UrunID")]
        public int ProductId { get; set; }

        [JsonProperty("UrunAdi")]
        public string ProductName { get; set; }

        [JsonProperty("StokKodu")]
        public string StockCode { get; set; }

        [JsonProperty("SatisAdeti")]
        public int Quantity { get; set; }

        [JsonProperty("KDVOrani")]
        public int TaxRate { get; set; }

        [JsonProperty("KDVDahilBirimFiyati")]
        public decimal PriceWithTax { get; set; }
    }
}
