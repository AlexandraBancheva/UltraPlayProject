namespace UltraPlayProject.Domain.DTOs.ExportDTOs
{
    public class ExportActiveMarketsByMatchId
    {
        public ExportActiveMarketsByMatchId()
        {
            this.Odds = new List<ExportOddsDTO>();
        }

        public string Name { get; set; }

        public int ID { get; set; }

        public bool IsLive { get; set; }

        public List<ExportOddsDTO> Odds { get; set; }
    }
}
