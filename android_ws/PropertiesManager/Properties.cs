using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace android_ws.PropertiesManager
{
    public class Properties
    {
        /// <summary>
        /// Classe qui permet de récupérer un fichier de propriétés et de lire les propriétés par la suite.
        /// </summary>
        private Dictionary<String, String> list;
        private static String filenameRecOnServeur = @"";
        private static String filenameDevOnServeur = @"";
        private static String filenameDevLocal = @"C:\Users\DAVY\Desktop\EPSI\B3\ASP.NET\android_ws\android_ws\PropertiesManager\PropertiesFiles\Properties.txt";

        /// <summary>
        /// Constructeur de l'objet Properties
        /// </summary>
        /// <param name="pathTofile">STRING - Chemin relatif du fichier à chargé pour édition</param>
        public Properties()
        {
            if (System.IO.File.Exists(filenameDevOnServeur))
            {
                load(filenameDevOnServeur);
            }
            else if (System.IO.File.Exists(filenameRecOnServeur))
            {
                load(filenameRecOnServeur);
            }
            else
            {
                load(filenameDevLocal);
            }
        }

        /// <summary>
        /// Permet de retourner la valeure de la propriété dont l'intitulé a été passé en paramètre.
        /// </summary>
        /// <param name="field"></param>
        /// <returns>STRING - Intitulé de la propriété</returns>
        public String get(String field)
        {
            return (list.ContainsKey(field)) ? (list[field]) : (null);
        }

        /// <summary>
        /// Permet d'enregistrer le fichier après l'avoir éditer.
        /// </summary>
        /// <returns>BOOLEAN - True si le fichier s'est bien enregistré / False si le fichier n'a pas pu s'enregistrer</returns>
        public bool Save(string fileName)
        {
            try
            {
                if (!System.IO.File.Exists(fileName))
                    System.IO.File.Create(fileName);

                System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);

                foreach (String prop in list.Keys.ToArray())
                    if (!String.IsNullOrWhiteSpace(list[prop]))
                        file.WriteLine(prop + "=" + list[prop]);

                file.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Permet de charger un fichier de propriété
        /// </summary>
        /// <returns>True: fichier chargé / False: fichier non chargé</returns>
        public void load(string pathToFile)
        {
            list = new Dictionary<String, String>();
            try
            {
                loadFromFile(pathToFile);
            }
            catch (Exception e)
            {
                throw e;
            }
            /*if (System.IO.File.Exists(pathToFile))
            {
               loadFromFile(pathToFile);
              
            }*/

        }

        /// <summary>
        /// Permet de charger toutes les propriétés d'un fichier spécifique dans un dictionnaire
        /// </summary>
        /// <param name="file">STRING - Chemin relatif du fichier</param>
        private void loadFromFile(String file)
        {
            foreach (String line in System.IO.File.ReadAllLines(file))
            {
                if ((!String.IsNullOrEmpty(line)) &&
                    (!line.StartsWith(";")) &&
                    (!line.StartsWith("#")) &&
                    (!line.StartsWith("'")) &&
                    (line.Contains('=')))
                {
                    int index = line.IndexOf('=');
                    String key = line.Substring(0, index).Trim();
                    String value = line.Substring(index + 1).Trim();

                    if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
                        (value.StartsWith("'") && value.EndsWith("'")))
                    {
                        value = value.Substring(1, value.Length - 2);
                    }

                    try
                    {
                        //ignore dublicates
                        list.Add(key, value);
                    }
                    catch { }
                }
            }
        }
    }
}