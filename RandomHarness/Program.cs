using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RandomHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = "REDO-RAZORGENERATOR";
            var runspace = Runspace.DefaultRunspace;
            var pipeline = runspace.CreateNestedPipeline("Write-Host '" + message + "'", false);
            pipeline.Invoke();
        }
    }
}
