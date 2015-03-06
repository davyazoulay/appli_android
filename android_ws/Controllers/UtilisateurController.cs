using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http;
using android_ws.Managers;

namespace android_ws.Controllers
{
    public class UtilisateurController : ApiController
    {
        /// <summary>
        /// Contrôller qui permet d'assurer la gestion des Utilisateurs utilisateurs.
        /// --> GetUtilisateurById: permet de retourner le Utilisateur correspondant à l'id entrée en paramètre.
        /// --> CreateUtilisateur: permet de créer un prol.
        /// --> UpdateUtilisateur: permet de mettre un jour un Utilisateur donnée.
        /// </summary>

        LibraryManager Librairie = new LibraryManager();

        [HttpGet]
        [Route("api/Utilisateur/GetUserById/{idUserRecherche}")]
        public IHttpActionResult GetUserById([FromUri]int idUserRecherche)
        {
            if (idUserRecherche > 0)
            {
                try
                {
                    if (Librairie.Utilisateurs.exists(idUserRecherche))
                    {
                        Utilisateur UserRecherche = Librairie.Utilisateurs.getUserById(idUserRecherche);
                        return Ok(UserRecherche);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("api/Utilisateur/CreateUser")]
        public IHttpActionResult CreateUser([FromBody]Utilisateur newUser)
        {
            if (!string.IsNullOrWhiteSpace(newUser.Login)
                && !string.IsNullOrWhiteSpace(newUser.Mdp)
                && !string.IsNullOrWhiteSpace(newUser.Nom)
                && !string.IsNullOrWhiteSpace(newUser.Prenom)
                && !string.IsNullOrWhiteSpace(newUser.Email)
                && !string.IsNullOrWhiteSpace(newUser.Pays)
                && !string.IsNullOrWhiteSpace(newUser.Ville)
                && !string.IsNullOrWhiteSpace(newUser.Code_postal))
            {
                try
                {
                    int idNewUser = Librairie.Utilisateurs.createUser(newUser);
                    Utilisateur UserCreatedIdOnly = new Utilisateur(idNewUser);
                    return Ok(UserCreatedIdOnly);
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/Utilisateur/UpdateUser")]
        public IHttpActionResult UpdateUser([FromBody]Utilisateur userModified)
        {
            if (userModified.Id > 0
                && !string.IsNullOrWhiteSpace(userModified.Login)
                && !string.IsNullOrWhiteSpace(userModified.Mdp)
                && !string.IsNullOrWhiteSpace(userModified.Nom)
                && !string.IsNullOrWhiteSpace(userModified.Prenom)
                && !string.IsNullOrWhiteSpace(userModified.Email)
                && !string.IsNullOrWhiteSpace(userModified.Pays)
                && !string.IsNullOrWhiteSpace(userModified.Ville)
                && !string.IsNullOrWhiteSpace(userModified.Code_postal))
            {
                try
                {
                    if (Librairie.Utilisateurs.exists(userModified.Id))
                    {
                        bool isModified = Librairie.Utilisateurs.updateUser(userModified);
                        return Ok(isModified);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }
            else
            {
                return BadRequest();
            }
        }
	}
}