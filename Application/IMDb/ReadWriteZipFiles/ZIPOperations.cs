namespace ReadWriteZipFiles
{
    using System;
    using System.Data.OleDb;
    using System.Data;
    using Ionic.Zip;
    using System.IO;

    public static class ZIPOperations
    {
        public static ZipFile CreateZipFile(string filePath, string zipName, string zipDirectory)
        {
            ZipFile zip;
            using (zip = new ZipFile())
            {
                // add this map file into the "zipDirectory" in the zip archive
                zip.AddFile(filePath, zipDirectory);
                zip.Save(zipName);
            }

            return zip;
        }

        public static void ExtractFiles(string zipFile, string zipDirectory)
        {
            using (ZipFile zipForUnpack = ZipFile.Read(zipFile))
            {
                // here, we extract every entry, but we could extract conditionally
                // based on entry name, size, date, checkbox status, etc.  
                foreach (ZipEntry dir in zipForUnpack)
                {
                    dir.Extract(zipDirectory, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }

        public static void AddFolderToZipArchive(string dirPath, string zipDirectory, string zipFilePath)
        {
            using (ZipFile zip = ZipFile.Read(zipFilePath))
            {
                zip.AddDirectory(dirPath, zipDirectory);
                zip.Save();
            }
        }
        public static void AddFileToZipArchive(string dirPath, string zipDirectory, string zipFilePath)
        {
            using (ZipFile zip = ZipFile.Read(zipFilePath))
            {
                zip.AddFile(dirPath, zipDirectory);
                zip.Save();
            }
        }
    }
}
