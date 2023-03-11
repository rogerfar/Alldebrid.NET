using System;

namespace AllDebridNET.Test;

public static class Setup
{
    public static String ApiKey => System.IO.File.ReadAllText(@"C:\Projects\Alldebrid.NET\AllDebridNET.Test\secret.txt");
}