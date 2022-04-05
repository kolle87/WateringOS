using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web;
using WateringOS_4_0.Models;
using WateringOS_4_0.Loggers;

namespace WateringOS_4_0.Services
{
    public class AccountService
    {
        [Inject]
        public ProtectedSessionStorage SessionStorage { get; set; }

        public event Action AccountLogin;
        public event Action AccountLogout;
        //public event Action AccountDelete;

        public List<ApprovedUser> ApprovedUsers = new(); 


        public bool LogIn(string fingerprint, string _instance, string phrase)
        {
            if (phrase=="3507866") 
            {
                ApprovedUsers.Add(new ApprovedUser{ Fingerprint=fingerprint, HashCode=_instance });
                Logger.Post(Logger.USR, LogType.Information, "User " + fingerprint + " logged in", "New user logged in with administrator level");
                if (AccountLogin != null) { AccountLogin?.Invoke(); }
                return true;
            }
            else
            {
                Logger.Post(Logger.USR, LogType.Warning, "User " + fingerprint + " entered a wrong password", "A user entered a wrong password.");
                return false;
            }            
        }

        public void LogOut(string fingerprint)
        {
            var user = ApprovedUsers.FirstOrDefault(x => x.Fingerprint == fingerprint);
            if (user != null)
            {               
                ApprovedUsers.Remove(user);
                Logger.Post(Logger.USR, LogType.Information, "User " + fingerprint + " logged out", "User logged out from administrator level");
                if (AccountLogout != null) { AccountLogout?.Invoke(); }
            }
        }

        public void Remove(string fingerprint)
        {
            var user = ApprovedUsers.FirstOrDefault(x => x.Fingerprint == fingerprint);
            if (user != null)
            {
                ApprovedUsers.Remove(user);                
            }
            Logger.Post(Logger.USR, LogType.Information, "User " + fingerprint + " closed the session", "The user closed the session and was removed from user list");
        }

        public bool IsAuthorized(string fingerprint)
        {
            var user = ApprovedUsers.FirstOrDefault(x => x.Fingerprint == fingerprint);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetFingerprint()
        {
            using RNGCryptoServiceProvider rngCsp = new();
                byte[] data = new byte[12];
                rngCsp.GetBytes(data);
                return BitConverter.ToString(data, 0);
        }
    }
}
