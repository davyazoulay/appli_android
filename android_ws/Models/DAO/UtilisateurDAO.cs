using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using android_ws.PropertiesManager;

namespace android_ws.Models.DAO
{
    public class UtilisateurDAO
    {
        private string _connectionString;
        private PropertiesManager.Properties _properties;

        const string CHAINE_CNX = "CHAINE_CNX";
        const string BDD_PROPERTIES = "BDD_PROPERTIES";

        const string SP_GETUSERBYID = "SP_GETUSERBYID";
        const string SP_CREATEUSER = "SP_CREATEUSER";
        const string SP_UPDATEUSER = "SP_UPDATEUSER";
        const string SP_CHECKIFUSEREXISTS = "SP_CHECKIFUSEREXISTS";

        public UtilisateurDAO()
        {
            this._properties = new PropertiesManager.Properties();
            this._connectionString = this._properties.get(CHAINE_CNX);
        }

        public Utilisateur getUserById(int idUser)
        {
            Utilisateur utilisateur = null;
            using (SqlConnection cnx = new SqlConnection(this._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(this._properties.get(SP_GETUSERBYID).ToString()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idUser", idUser));
                    cnx.Open();
                    cmd.Connection = cnx;
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (reader.Read())
                        {
                            utilisateur = new Utilisateur(reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetString(reader.GetOrdinal("login")),
                                reader.GetString(reader.GetOrdinal("mdp")),
                                reader.GetString(reader.GetOrdinal("nom")),
                                reader.GetString(reader.GetOrdinal("prenom")),
                                reader.GetString(reader.GetOrdinal("email")),
                                reader.GetDateTime(reader.GetOrdinal("date_naissance")),
                                reader.GetString(reader.GetOrdinal("pays")),
                                reader.GetString(reader.GetOrdinal("ville")),
                                reader.GetString(reader.GetOrdinal("code_postal")));
                        }
                    }
                }
            }
            return utilisateur;
        }

        public int createUser(Utilisateur newUser)
        {
            int idNewUser;
            using (SqlConnection cnx = new SqlConnection(this._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(this._properties.get(SP_CREATEUSER).ToString()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@login", newUser.Login));
                    cmd.Parameters.Add(new SqlParameter("@mdp", newUser.Mdp));
                    cmd.Parameters.Add(new SqlParameter("@nom", newUser.Nom));
                    cmd.Parameters.Add(new SqlParameter("@prenom", newUser.Prenom));
                    cmd.Parameters.Add(new SqlParameter("@email", newUser.Email));
                    cmd.Parameters.Add(new SqlParameter("@date_naissance", newUser.DateNaissance));
                    cmd.Parameters.Add(new SqlParameter("@pays", newUser.Pays));
                    cmd.Parameters.Add(new SqlParameter("@ville", newUser.Ville));
                    cmd.Parameters.Add(new SqlParameter("@code_postal", newUser.CodePostal));
                    cnx.Open();
                    cmd.Connection = cnx;
                    idNewUser = int.Parse(cmd.ExecuteScalar().ToString());
                    cnx.Close();
                }
            }
            return idNewUser;
        }

        public bool updateUser(Utilisateur updatedUser)
        {
            bool updatedOrNot = false;
            using (SqlConnection cnx = new SqlConnection(this._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(this._properties.get(SP_UPDATEUSER).ToString()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", updatedUser.Id));
                    cmd.Parameters.Add(new SqlParameter("@login", updatedUser.Login));
                    cmd.Parameters.Add(new SqlParameter("@mdp", updatedUser.Mdp));
                    cmd.Parameters.Add(new SqlParameter("@nom", updatedUser.Nom));
                    cmd.Parameters.Add(new SqlParameter("@prenom", updatedUser.Prenom));
                    cmd.Parameters.Add(new SqlParameter("@email", updatedUser.Email));
                    cmd.Parameters.Add(new SqlParameter("@date_naissance", updatedUser.DateNaissance));
                    cmd.Parameters.Add(new SqlParameter("@pays", updatedUser.Pays));
                    cmd.Parameters.Add(new SqlParameter("@ville", updatedUser.Ville));
                    cmd.Parameters.Add(new SqlParameter("@code_postal", updatedUser.CodePostal));
                    cnx.Open();
                    cmd.Connection = cnx;
                    if (cmd.ExecuteNonQuery() > 1)
                    {
                        updatedOrNot = true;
                    }
                    cnx.Close();
                }
            }
            return updatedOrNot;
        }

        public bool exists(int idUser)
        {
            bool userExists = false;
            using (SqlConnection cnx = new SqlConnection(this._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(this._properties.get(SP_CHECKIFUSEREXISTS).ToString()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", idUser));
                    cnx.Open();
                    cmd.Connection = cnx;
                    if ((int)cmd.ExecuteScalar() >= 1)
                    {
                        userExists = true;
                    }
                }
            }
            return userExists;
        }
    }
}