using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace android_ws.Managers
{
    public class LibraryManager
    {
        UtilisateurManager _utilisateurManager;

        public LibraryManager() { }

        public UtilisateurManager Utilisateurs
        {
            get
            {
                if (_utilisateurManager == null)
                    return _utilisateurManager = new UtilisateurManager();
                else
                    return _utilisateurManager;
            }
        }
    }
}