using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementInterface.API.ModelsView
{
    public class LoginModel
    {
       
        public string Username { get; set; }
        public string Password { get; set; }
        
    }
}
