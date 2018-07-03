namespace SecurityWithIOT.API.Dtos
{
    public class CompanyDto
    {
        public int Id { get; set; }

        public string CompanyName {get;set;}

        public string Tel {get;set;}

        public string Fax {get;set;}

        public string Mail {get;set;}

        public DepartmentDto Departments {get;set;}
    }
}