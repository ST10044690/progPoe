using Agri_Energy_Connect.Models.Entities;

namespace Agri_Energy_Connect.Models
{
    public class FarmerRegistrationViewModel
    {
        public FarmerRegistrationViewModel()
        {
            UserDetails = new User();
        }

        public User UserDetails { get; set; }
        public string FarmName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
    }
}
