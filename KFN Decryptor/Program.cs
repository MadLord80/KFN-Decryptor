using Microsoft.Win32;
using System;
//using System.Globalization;
using System.Reflection;

namespace KFN_Decryptor
{
    class Program
    {
        static readonly AssemblyName program = Assembly.GetExecutingAssembly().GetName();
        static bool createNew = false;
        static bool recurse = false;

        static void Main(string[] args)
        {
            // check .NET Framework version
            //RegistryKey installed_versions = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP");
            //string[] version_names = installed_versions.GetSubKeyNames();
            ////version names start with 'v', eg, 'v3.5' which needs to be trimmed off before conversion
            //double Framework = Convert.ToDouble(version_names[version_names.Length - 1].Remove(0, 1), CultureInfo.InvariantCulture);
            int releaseKey = 0;
            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
            }
            if (releaseKey < 378389)
            {
                Console.WriteLine("need .NET Framework 4.5 or later");
                Environment.Exit(1);
            }

            if (args.Length == 0)
            {
                string version = program.Version.ToString();
                Console.WriteLine("KFN Decryptor v." + version.Remove(version.Length - 2));
                Console.WriteLine("https://github.com/MadLord80/KFN-Decryptor");
                Console.WriteLine("Try --help for more information");
                Environment.Exit(0);
            }

            if (args.Length < 2 || args.Length > 4)
            {
                Usage();
                Environment.Exit(0);
            }
            
            if (args.Length == 2 && args[0] == "-d")
            {
                ReadDir(args[1]);
            }
            else if (args.Length == 2 && args[0] == "-f")
            {
                ReadFile(args[1]);
            }

            if (args[0] == "-n" || args[0] == "-R" || args[0] == "-d")
            {
                if (args[0] == "-n") { createNew = true; }
                if (args[0] == "-R" || (createNew && args[1] == "-R")) { recurse = true; }
            }
            else if (args[0] == "-n" || args[0] == "-f")
            {
                if (args[0] == "-n") { createNew = true; }
                if (args[0] == "-f" || (createNew && args[1] == "-f"))
                {
                    //string fpath = (args[0] == "-f" && args[1] != null);
                }
            }
            else
            {
                Usage();
            }
            Environment.Exit(0);
        }
        
        static void Usage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("\t" + program.Name + " [-n] [[-R] -d <string> | -f <string>]");
            Console.WriteLine("\nParameters:");
            Console.WriteLine("\t-n\t\tCreate new KFN files instead modify current");
            Console.WriteLine("\t-R\t\tRead all files under each directory, recursively");
            Console.WriteLine("\t-d <string>\tPath to directory with KFN files");
            Console.WriteLine("\t-f <string>\tPath to KFN file");
        }

        static void ReadDir(string dirPath)
        {

        }

        static void ReadFile(string filePath)
        {

        }
    }
}
