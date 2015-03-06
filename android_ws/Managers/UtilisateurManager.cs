using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using android_ws.Models;
using android_ws.Models.DAO;

namespace android_ws.Managers
{
    public class UtilisateurManager
    {
        public UtilisateurManager() { }

        public Utilisateur getUserById(int idUser)
        {
            Utilisateur UtilisateurRecherche = null;
            if (idUser > 0)
            {
                UtilisateurDAO UtilisateurDao = new UtilisateurDAO();
                return UtilisateurDao.getUserById(idUser);
            }
            return UtilisateurRecherche;
        }

        public int createUser(Utilisateur newUser)
        {
            int idNewUser = 0;
            if (newUser != null)
            {
                UtilisateurDAO UtilisateurDao = new UtilisateurDAO();
                idNewUser = UtilisateurDao.createUser(newUser);
            }
            return idNewUser;
        }


        public bool updateUser(Utilisateur UserToUpdate)
        {
            bool isUpdated = false;
            if (UserToUpdate != null)
            {
                UtilisateurDAO UtilisateurDAO = new UtilisateurDAO();
                isUpdated = UtilisateurDAO.updateUser(UserToUpdate);
            }
            return isUpdated;
        }

        public bool exists(int iduser)
        {
            bool exists = false;
            if (iduser > 0)
            {
                UtilisateurDAO userDAO = new UtilisateurDAO();
                exists = userDAO.exists(iduser);
            }
            return exists;
        }
    }
}