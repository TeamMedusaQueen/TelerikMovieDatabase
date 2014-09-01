namespace TMDB.ZipOperations
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
            ////All this code is in TMDB.ExcellOperations

            //const string ReportsPath = "..\\..\\XLSData";
            //const string directoryPathForArchive = "..\\..\\Reports";
            //const string zipFilePath = "..\\..\\";
            //const string zipFileName = "ReportsArchive.zip";

            //if (!Directory.Exists(ReportsPath))
            //{
            //    Directory.CreateDirectory(ReportsPath);
            //}

            //List<string> files = new List<string>();
            //var directories = Directory.GetDirectories(ReportsPath);

            ////Only testing

            //var filesForArchive = Directory.GetFiles(directoryPathForArchive);

            //foreach (var file in filesForArchive)
            //{
            //    string fileName = Path.GetFileNameWithoutExtension(file);

            //    if (!File.Exists(zipFilePath + zipFileName))
            //    {
            //        ZIPOperations.CreateZipFile(file, zipFilePath + zipFileName, File.GetCreationTime(file).ToString("dd-MMM-yyyy"));
            //    }
            //    else
            //    {
            //        ZIPOperations.AddFileToZipArchive(file, File.GetCreationTime(file).ToString("dd-MMM-yyyy"), zipFilePath + zipFileName);
            //    }
            //}

            ////ZIPOperations.AddFolderToZipArchive("..\\..\\Test", "Test", "..\\..\\BoxOffice.zip");

            ////ZIPOperations.ExtractFiles(zipFilePath + zipFileName, ReportsPath, "BoxOffice.xls");  //If "BoxOffice.xls" missing then extracts all files

            //foreach (var folder in directories)
            //{
            //    files.AddRange(Directory.GetFiles(folder));
            //}

            //foreach (var file in files)
            //{
            //    OleDbConnection dbCon = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES");
            //    dbCon.Open();

            //    using (dbCon)
            //    {
            //        OleDbCommand command = new OleDbCommand("SELECT * FROM [Sheet1$]", dbCon);
            //        OleDbDataReader reader = command.ExecuteReader();

            //        using (reader)
            //        {
            //            while (reader.Read())
            //            {
            //                string name = (string)reader["Name"];
            //                string score = (string)reader["Value"];
            //                Console.WriteLine("{0} - score: {1}", name, score);
            //            }
            //        }
            //    }
            //}
        }
    }
}
