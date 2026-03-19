using Gis_Project.Models;

namespace Gis_Project.ViewModels
{
    public class UserVM
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool Block { get; set; }
        public Region Region { get; set; }
        public List<string> UserRoles { get; set; }
    }
}
