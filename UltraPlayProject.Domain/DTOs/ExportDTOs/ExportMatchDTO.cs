namespace UltraPlayProject.Domain.DTOs.ExportDTOs
{
    public class ExportMatchDTO
    {
        public ExportMatchDTO()
        {
            this.Markets = new List<ExportMarketsDTO>();
        }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public ICollection<ExportMarketsDTO> Markets { get; set; }
    }
}
