using DatabaseLayer.Context;
using DatabaseLayer.ModelsDB;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseLayer.DatabaseOperations
{
    public class RegisterNewUser
    {
        public void CreateNewUser(RegisterDB register)
        {
            using( var context = new ECommerceDB())
            {
                context.registerDB.Add(register);
                context.SaveChanges();
            }
        }

        public List<RegisterDB> GetRegisteredUsersList()
        {
            using (var context = new ECommerceDB())
            {
                var listOfUsers = context.registerDB.ToList();
                return listOfUsers;
            }
        }
    }
}
