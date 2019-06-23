using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TTracker.Utility
{
    public static class CustomBase64Writer
    {

        public static void WriteDataAsEncodedBase64(string namePath, List<string> data)
        {
            if (File.Exists(namePath))
            {
                MessageBox.Show("A file with the given name already exists at the location.");
                return;
            }

            var combinedDataString = string.Empty;
            foreach (var s in data)
            {
                combinedDataString += "~THISISANEXPORTEDFILEOFTTRACKER~" + s;
            }

            var checkedCombinedDataString = ReplaceNoneASCIIChars(combinedDataString);

            byte[] stringAsByte = ASCIIEncoding.ASCII.GetBytes(checkedCombinedDataString);
            string value = Convert.ToBase64String(stringAsByte);
            File.WriteAllText(namePath, value);
            var blub = DecodeDataFromBase64(value);
        }

        public static string DecodeDataFromBase64(string encodedValue)
        {
            byte[] encodedDataAsBytes = Convert.FromBase64String(encodedValue);
            var decodedValue = ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return decodedValue;
        }

        public static string ReplaceNoneASCIIChars(string s)
        {
            var charsToRemove = new string[] { "ß", "ä", "ü", "ö" };

            foreach(var c in charsToRemove)
            {
                switch (c)
                {
                    case "ö":
                        s = s.Replace(c, "oe");
                        break;
                    case "ä":
                        s = s.Replace(c, "ae");
                        break;
                    case "ü":
                        s = s.Replace(c, "ue");
                        break;
                    case "ß":
                        s = s.Replace(c, "ss");
                        break;
                    default:
                        break;
                }
            }
            return s;
        }
    }
}
