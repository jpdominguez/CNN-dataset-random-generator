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
        public static HashSet<string> labelsHashSet;

        public static void grant_access(string fullPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(fullPath);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);
        }

        public static void getLabels(string path)
        {
            labelsList = File.ReadAllLines(path).ToList();
            labelsList.Sort();
            labelsHashSet = new HashSet<string>(labelsList);
        }

        public static string findLabel(string imageName)
        {
            //var ind = labelsList.FindIndex(x => x.Contains(imageName.Split('\\')[imageName.Split('\\').Count() - 1]));
            //var res = labelsList.ElementAt(ind);
            //labelsList.RemoveAt(ind);
            //var res = labelsHashSet.Select((s, i) => new { i, s }).Where(x => x.s.StartsWith(imageName.Split('\\')[imageName.Split('\\').Count() - 1])).Select(t => t.i).ToList();
            string resultado;
            var res = labelsHashSet.Where(x => x.StartsWith(imageName.Split('\\')[imageName.Split('\\').Count() - 1]));
            resultado = res.ElementAt(0);
            labelsHashSet.Remove(res.ElementAt(0));

            return resultado; // ElementAt(0);
        }
    }
}
