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
        public static List<string> labelsList;
        public static List<string> labelsList_used;

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
            //StringBuilder lines = new StringBuilder();
            string label_res = "NULL";
            string buffer = string.Empty;
            /*
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
            */
            List<string> lines = File.ReadAllLines(path).ToList();

            if (lines.Any(x => x.Contains(imageName.Split('\\')[imageName.Split('\\').Count() - 1])))
            {
                var ind = lines.FindIndex(x => x.Contains(imageName.Split('\\')[imageName.Split('\\').Count() - 1]));

                label_res = lines.ElementAt(ind);
                lines.RemoveAt(ind);
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
            File.AppendAllLines(path, lines);
            //File.WriteAllLines(path, lines.ToArray());
            return label_res;
        }



        public static void getLabels(string path)
        {
            labelsList = File.ReadAllLines(path).ToList();
        }

        public static string findLabel(string imageName)
        {
            var ind = labelsList.FindIndex(x => x.Contains(imageName.Split('\\')[imageName.Split('\\').Count() - 1]));
            //var ind = labelsList.Find(x => !labelsList_used.Contains(x) && x.Contains(imageName.Split('\\')[imageName.Split('\\').Count() - 1]));


            //var ind2 = labelsList.Select((Value, Index) => new { Value, Index }).Single(p => !labelsList_used.Contains(p.Value) && p.Value.Contains(imageName.Split('\\')[imageName.Split('\\').Count() - 1]));
            //labelsList_used.Add(ind);
            //labelsList.RemoveAt(ind2.Index);
            var res = labelsList.ElementAt(ind);
            labelsList.RemoveAt(ind);
            return res;
        }
    }
}
