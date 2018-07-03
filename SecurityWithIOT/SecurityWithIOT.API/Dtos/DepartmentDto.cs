namespace SecurityWithIOT.API.Dtos
{
    public class DepartmentDto
    {
        public int Id { get; set; }

        public string DepartmentName { get; set; }

        public string Tel {get;set;}

        public string Fax {get;set;}

        public string Mail {get;set;}

        public CompanyDto Company {get;set;}
    }
}