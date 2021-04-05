using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    public class AuthorizedUser : User //--ToDo: Переделать под синглтон??
    {
        public AuthorizedUser(User user)
        {
            Id = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Login = user.Login;
            Password = user.Password;
            Is_admin = user.Is_admin;

        }
    }
}
