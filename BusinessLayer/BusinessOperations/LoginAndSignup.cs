using DatabaseLayer.DatabaseOperations;
using DatabaseLayer.ModelsDB;
using SharedLayerDTO.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessOperations
{
    public class LoginAndSignup
    {
        RegisterNewUser registerNewUser = new RegisterNewUser();

        public bool isLogin(LoginDTO loginDTO)
        {
            var listofUsers = registerNewUser.GetRegisteredUsersList();

            if (listofUsers.Any(x => x.Email == loginDTO.Email && x.Password == loginDTO.Password))
            {
                return true;
            }

            return false;
        }

        public bool RegisterUser(RegisterDTO registerDTO)
        {
            var listofUsers = registerNewUser.GetRegisteredUsersList();

            if (listofUsers.Any(x => x.Email == registerDTO.Email))
            {
                return false;
            }
            else
            {
                if(registerDTO.Email != null && registerDTO.FullName != null && registerDTO.Password != null)
                {
                    var register = new RegisterDB()
                    {
                        Email = registerDTO.Email,
                        FullName = registerDTO.FullName,
                        Password = registerDTO.Password
                    };
                    registerNewUser.CreateNewUser(register);

                    return true;
                }

                return false;
            }
        }

        public RegisterDTO UserAccess(string email)
        {
            var listofUsers = registerNewUser.GetRegisteredUsersList();

            RegisterDB user;

            if (listofUsers.Any(x => x.Email == email))
            {
                user = listofUsers.FirstOrDefault(x => x.Email == email);

                RegisterDTO registerDTO = new RegisterDTO()
                {
                    Email = user.Email,
                    Password = user.Password,
                    FullName = user.FullName
                };

                return registerDTO;
            }
            else
            {
                return null;
            }
        }
    }
}
