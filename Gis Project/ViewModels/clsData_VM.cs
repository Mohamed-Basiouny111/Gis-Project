using Gis_Project.Models;

namespace Gis_Project.ViewModels
{
    public class clsData_VM
    {
        public clsData_VM()
        {
            Regions = new List<Region>();
        }
        public List<Region> Regions { get; set; }

    }
}
