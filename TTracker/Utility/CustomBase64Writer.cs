using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TTracker.Utility
{
    public class CustomBase64Writer : BinaryWriter
    {

        public void WriteDataAsEncodedBase64(string namePath, List<string> data)
        {
            if (File.Exists(namePath))
            {
                MessageBox.Show("A file with the given name already exists at the location. ");
                return;
            }

            FileStream fileStream = new FileStream(namePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read, 1024);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);

            foreach (var s in data)
            {
                byte[] stringAsByte = ASCIIEncoding.ASCII.GetBytes(s);                
                string value = Convert.ToBase64String(stringAsByte);
                binaryWriter.Write(value);
                DecodeDataFromBase64(value);
            }

            binaryWriter.Close();
            fileStream.Close();
        }

        public void DecodeDataFromBase64(string namePath)
        {    
            byte[] encodedDataAsBytes = Convert.FromBase64String(namePath);
            var value = ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

        }

    }
}
