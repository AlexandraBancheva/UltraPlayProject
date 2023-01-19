namespace UltraPlayProject.Domain.DTOs.ExportDTOs
{
    public class ExportMatchByIdDTO
    {
        public ExportMatchByIdDTO()
        {
            this.ActiveMarkets = new List<ImportActiveMarketsByMatchIdDTO>();
            this.InactiveMarkets = new List<ExportInactiveMarketsByMatchIdDTO>();
        }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public ICollection<ImportActiveMarketsByMatchIdDTO> ActiveMarkets { get; set; }

        public ICollection<ExportInactiveMarketsByMatchIdDTO> InactiveMarkets { get; set; }
    }
}
