using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DRG.src
{
    public static class Utilities
    {
        public static void grant_access(string fullPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(fullPath);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);
        }

        public static string getLabel(string path, string imageName)
        {
            //StreamReader reader = new StreamReader(path);
            //List<String> lines = new List<string>();
            StringBuilder lines = new StringBuilder();
            string label_res = "NULL";
            string buffer = string.Empty;
            using (StreamReader reader = new StreamReader(path, true))
            {
                while (!reader.EndOfStream)
                {
                    buffer = reader.ReadLine();

                    if (buffer.Contains(imageName.Split('\\')[imageName.Split('\\').Count() - 1]))
                    {
                        label_res = buffer;
                    }
                    else
                    {
                        lines.AppendLine(buffer);
                    }
                }
            }

            //reader.Close();
            //reader.Dispose();
            //File.Delete(path);
            //StreamWriter writer = new StreamWriter(path);
            /*
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.(lines.ToString());
            }
                */

            File.Delete(path);
            File.AppendAllText(path, lines.ToString());
                //File.WriteAllLines(path, lines.ToArray());
                return label_res;
        }
    }
}
