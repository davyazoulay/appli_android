using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace android_ws.Models
{
    public class Utilisateur
    {
        public int Id;
        public string Login;
        public string Mdp;
        public string Nom;
        public string Prenom;
        public string Email;
        public DateTime DateNaissance;
        public string Pays;
        public string Ville;
        public string CodePostal;

        public Utilisateur() { }

        public Utilisateur(int idUser)
        {
            this.Id = idUser;
        }

        public Utilisateur(int id, string login, string mdp, string nom, string prenom, string email, DateTime date_naissance, string pays, string ville, string code_postal)
        {

        }
    }
}