using Gis_Project.Models;
using Microsoft.AspNetCore.Identity;

namespace Gis_Project.ViewModels
{
    public class clsData_VM
    {
        public clsData_VM()
        {
            Regions = new List<Region>();
            Contracts = new List<Contract>();
            Assets = new List<Asset>();

            Users = new List<UserVM>();
            Roles = new List<IdentityRole>();
        }
        public List<Region> Regions { get; set; }
        public List<Contract> Contracts { get; set; }
        public List<Asset> Assets { get; set; }

        public List<UserVM> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }

    }
}
