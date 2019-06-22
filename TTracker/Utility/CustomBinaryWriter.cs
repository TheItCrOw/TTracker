using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TTracker.Utility
{
    public class CustomBinaryWriter : BinaryWriter
    {

        public void WriteDataToBinaryFile(string namePath, List<string> data)
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
                //byte[] arr = Encoding.ASCII.GetBytes(s);
                byte[] stringAsByte = ASCIIEncoding.ASCII.GetBytes(s);
                string value = Convert.ToBase64String(stringAsByte);

                binaryWriter.Write(value);
            }

            binaryWriter.Close();
            fileStream.Close();
        }

        public void ReadDataFromBinaryFile(string namePath)
        {
            FileStream fileStream = new FileStream(namePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);

            MessageBox.Show(binaryReader.Read().ToString());

            binaryReader.Close();
            fileStream.Close();
        }

        private byte[] StringToBytesArray(string str)
        {
            str = "HelloThisISAText";
            var bitsToPad = 8 - str.Length % 8;

            if (bitsToPad != 8)
            {
                var neededLength = bitsToPad + str.Length;
                str = str.PadLeft(neededLength, '0');
            }

            int size = str.Length / 8;
            byte[] arr = new byte[size];

            for (int a = 0; a < size; a++)
            {
                arr[a] = Convert.ToByte(str.Substring(a * 8, 8), 2);
            }

            return arr;
        }

    }
}
