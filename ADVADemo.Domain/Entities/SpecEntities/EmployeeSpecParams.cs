namespace ADVADemo.Domain.Entities.SpecEntities
{
    public class EmployeeSpecParams
    {
        private const int MaxPageSize = 10;
        private int pageSize = 3;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public int pageNumber { get; set; } = 1;
        public string? sort { get; set; }
        public int? managerId { get; set; }
        public int? departmentId { get; set; }

        private string? search;
        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

    }
}
