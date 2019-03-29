using Avanade.Xrm.Batches;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var conventions = new Conventions
            {
                FilterAssembly = fullAssemblyName => fullAssemblyName.IndexOf("BillaConsole.", 0, StringComparison.InvariantCultureIgnoreCase) != -1
            };
            Bootstrapper.Boot(args, conventions); //(ctrl L) si cancella la riga.
        }
    }
}
