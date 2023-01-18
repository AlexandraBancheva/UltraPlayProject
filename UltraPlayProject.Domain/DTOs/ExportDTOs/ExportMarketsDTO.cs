namespace UltraPlayProject.Domain.DTOs.ExportDTOs
{
    public class ExportMarketsDTO
    {
        public ExportMarketsDTO()
        {
            this.Odds = new List<ExportOddsDTO>();
        }

        public string Name { get; set; }
        public int ID { get; set; }
        public bool IsLive { get; set; }
        public ICollection<ExportOddsDTO> Odds { get; set; }
    }
}
