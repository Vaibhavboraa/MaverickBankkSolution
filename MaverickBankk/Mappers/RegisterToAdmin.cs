using MaverickBankk.Models.DTOs;
using MaverickBankk.Models;

namespace MaverickBankk.Mappers
{
    public class RegisterToAdmin
    {
        Admin admin;
        public RegisterToAdmin(RegisterAdminDTO register)
        {
            admin = new Admin();
            admin.Name = register.Name;
            admin.Email = register.Email;
            admin.Phone = register.Phone;
        }
        public Admin GetAdmin()
        {
            return admin;
        }
    }
}
