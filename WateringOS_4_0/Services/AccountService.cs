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

namespace WateringOS_4_0.Services
{
    public class AccountService
    {
        [Inject]
        public ProtectedSessionStorage SessionStorage { get; set; }

        public event Action AccountLogin;
        public event Action AccountLogout;
        public event Action AccountDelete;

        public List<ApprovedUser> ApprovedUsers = new List<ApprovedUser>(); 


        public bool LogIn(string fingerprint, string _instance, string phrase)
        {
            if (phrase=="3507866") 
            {
                ApprovedUsers.Add(new ApprovedUser{ Fingerprint=fingerprint, HashCode=_instance });
                Console.WriteLine("{0} logged in", fingerprint);
                if (AccountLogin != null) { AccountLogin?.Invoke(); }
                return true;
            }
            else
            {
                Console.WriteLine("{0} wrong password", fingerprint);
                return false;
            }            
        }

        public void LogOut(string fingerprint)
        {
            var user = ApprovedUsers.FirstOrDefault(x => x.Fingerprint == fingerprint);
            if (user != null)
            {               
                ApprovedUsers.Remove(user);
                Console.WriteLine("{0} logged out", fingerprint);
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
            Console.WriteLine("{0} closed the session", fingerprint);
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
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[12];
                rngCsp.GetBytes(data);
                return BitConverter.ToString(data, 0);
            }
        }
    }
}
