using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_ws.Models.DAO.DAOInterfaces
{
    interface IUtilisateurDAO
    {
        Utilisateur getUserById(int idUser);
        int createUser(Utilisateur newUser);
        bool updateUser(Utilisateur updatedUser);
        bool exists(int idUser);
    }
}
