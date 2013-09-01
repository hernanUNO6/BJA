using Bja.Entidades;
using Bja.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Modelo
{
    public class Rbac
    {
        BjaContext context = new BjaContext();

        /// <summary>
        /// Function used to authenticate the specified user
        /// </summary>
        /// <param name="userName">user identifier </param>
        /// <param name="password">user password</param>
        /// <returns>User: authenticated, null: not authenticated</returns>
        public User authenticate(String userName, String password)
        {
            User user = null;
            String hashedPassword = password.GetHashCode().ToString("x");
            //validar password

            user = (from u in context.Users
                        where u.UserName == userName
                        && u.Password == hashedPassword
                        select u).FirstOrDefault();

            return user;
        }

        //public Boolean isAuthorized(String userName, String permissionName)
        //{

        //}

        public void insertUser(String userName, String completeName , String password, long userID, long? sessionId = null )
        {

            var newUser = new User();

            newUser.Id = (int)IdentifierGenerator.NewId();
            newUser.IdSession = (sessionId == null)?SessionManager.getCurrentSession().Id:(long)sessionId;
            newUser.UserName = userName;
            newUser.CompleteName = completeName;
            newUser.Password = password.GetHashCode().ToString("x");
            newUser.IdUserRelation = userID;

            context.Users.Add(newUser);

            context.SaveChanges();
        }

        public User getUser(long idUser)
        {
            User user = null;

            user = (from u in context.Users
                        where u.Id == idUser
                        select u).FirstOrDefault();

            return user;
        }

    }
}
