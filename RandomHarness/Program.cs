using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Singular.Core.Authentication;
using Singular.Core.Encryption;

namespace RandomHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            var password = "testTTT";
            var helper = new EncryptionHelper();
            var hash = helper.EncryptToString(password);

            Console.WriteLine("Password in: {0}",password);
            Console.WriteLine("Password out: {0}", hash);
            password = helper.DecryptString(hash);
            Console.WriteLine("Password out: {0}", password);


        }
    }
}
