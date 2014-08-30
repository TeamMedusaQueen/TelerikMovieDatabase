namespace ReadWriteZipFiles
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            string fileName = Path.GetFileNameWithoutExtension("..\\..\\BoxOffice.xls");
            
            ZIPOperations.CreateZipFile("..\\..\\BoxOffice.xls", "..\\..\\" + fileName + ".zip", fileName);
            
            string fileName2 = Path.GetFileNameWithoutExtension("..\\..\\BoxOffice2.xls");
            ZIPOperations.AddFileToZipArchive("..\\..\\BoxOffice2.xls", fileName2, "..\\..\\BoxOffice.zip");
            ZIPOperations.AddFolderToZipArchive("..\\..\\Test", "Test", "..\\..\\BoxOffice.zip");
            ZIPOperations.ExtractFiles("..\\..\\BoxOffice.zip", "..\\..\\XLSData");

            var directories = Directory.GetDirectories("..\\..\\XLSData");
            List<string> files = new List<string>();

            foreach (var folder in directories)
            {
                files.AddRange(Directory.GetFiles(folder));
            }

            foreach (var file in files)
            {
                OleDbConnection dbCon = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";");
                dbCon.Open();

                using (dbCon)
                {
                    OleDbCommand command = new OleDbCommand("SELECT * FROM [Sheet1$]", dbCon);
                    OleDbDataReader reader = command.ExecuteReader();

                    using (reader)
                    {
                        while (reader.Read())
                        {
                            string name = (string)reader["Name"];
                            string score = (string)reader["Value"];
                            Console.WriteLine("{0} - score: {1}", name, score);
                        }
                    }
                }
            }
        }
    }
}
