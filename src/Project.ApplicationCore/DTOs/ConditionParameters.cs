
namespace Project.ApplicationCore.DTOs
{
    public class ConditionParameters:Pagination
    {
        public ConditionParameters()
        {
            OrderBy = "duration"; 
        }
        public uint MaxDistance { get; set; }
        public uint MinDistance { get; set; } 
        //public bool Range=> MaxDistance > MinDistance;
        public string Name { get; set; } 

    }
    public class SingleStationParameters : Pagination
    {
        public string Name { get; set; }
    }
}
