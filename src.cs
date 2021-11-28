using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// used libs
using System.Timers;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace VS_BuildVersioningSys
{
    class Program
    {
        static void Main(string[] args)
        {
            // args[0] = project name                               ("example_project")
            // args[1] = project exeuctable                         ("X:\\somedur\\example_project.dll")
            // args[2] = versioning path                            ("D:\VS Build Revisions")

            /*
             1: create directory if it does not exists with the project name inside the versions system(D:\VS Build Revisions)
             2: create directory with the prefix "ProjectName_M-D-Y_H-M-S"
             3: copy executable into that directory
            */

            FileInfo PE = new FileInfo(args[1]);
            SHA1Managed sha1 = new SHA1Managed();
            DateTime now = DateTime.Now;
            string directoryname = args[2] + $"\\{args[0]}_{now.Month}-{now.Day}-{now.Year}_{now.Hour}-{now.Minute}-{now.Second}";
            string projectexeuctable = Path.GetFileName(args[1]);

            if (Directory.Exists(directoryname) && File.Exists($"{directoryname}\\{projectexeuctable}"))
            {
                return;
            }
            else
            {
                Directory.CreateDirectory(directoryname);
            }

            File.Copy(args[1], directoryname + $"\\{projectexeuctable}");
            string FileInfo = $"SHA1: {BitConverter.ToString(sha1.ComputeHash(File.ReadAllBytes(args[1]))).Replace("-", "")}\nFile Size: {PE.Length}({(decimal)PE.Length / 1000} MB)";
            File.WriteAllText($"{directoryname}\\{Path.GetFileNameWithoutExtension(args[1])}.txt", FileInfo);

        }
    }
}
