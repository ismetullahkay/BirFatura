using Newtonsoft.Json;

namespace BirFatura.Models
{
    public class Satis
    {
        [JsonProperty("FaturaID")]
        public int OrderId { get; set; }

        [JsonProperty("MusteriAdi")]
        public string CustomerName { get; set; }

        [JsonProperty("MusteriAdresi")]
        public string CustomerAddress { get; set; }

        [JsonProperty("MusteriTel")]
        public string CustomerPhone { get; set; }

        [JsonProperty("MusteriSehir")]
        public string CustomerCity { get; set; }

        [JsonProperty("MusteriTCVKN")]
        public string CustomerTaxNumber { get; set; }

        [JsonProperty("MusteriVergiDairesi")]
        public string? CustomerTaxOffice { get; set; }

        [JsonProperty("SatilanUrunler")]
        public List<SatisUrun> Products { get; set; }

        [JsonProperty("ToplamTutar")]
        public decimal TotalAmount { get; set; }

      
        [JsonProperty("OrderDate")]
        public DateTime? OrderDate { get; set; }
    }
}
