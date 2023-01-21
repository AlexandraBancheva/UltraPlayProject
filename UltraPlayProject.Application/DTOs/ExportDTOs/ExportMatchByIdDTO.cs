namespace UltraPlayProject.Domain.DTOs.ExportDTOs
{
    public class ExportMatchByIdDTO
    {
        public ExportMatchByIdDTO()
        {
            this.ActiveMarkets = new List<ExportActiveMarketsByMatchId>();
            this.InactiveMarkets = new List<ExportInactiveMarketsByMatchIdDTO>();
        }

        public string Name { get; set; }

        public DateTime StartDate{ get; set; }

        public ICollection<ExportActiveMarketsByMatchId> ActiveMarkets { get; set; }

        public ICollection<ExportInactiveMarketsByMatchIdDTO> InactiveMarkets { get; set; }
    }
}
