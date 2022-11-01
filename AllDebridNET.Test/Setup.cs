using System;
using System.IO;

namespace RDNET.Test
{
    public static class Setup
    {
        public static String ApiKey => File.ReadAllText(@"C:\Projects\Alldebrid.NET\AllDebridNET.Test\secret.txt");
    }
}
