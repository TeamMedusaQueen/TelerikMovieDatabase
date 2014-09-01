namespace TelerikMovieDatabase.Utils
{
	using Ionic.Zip;
	using System;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Threading;

	public static class ZIPOperations
	{
		public static ZipFile CreateZipFile(string filePath, string zipName, string zipDirectory)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

			ZipFile zip;
			using (zip = new ZipFile())
			{
				// add this map file into the "zipDirectory" in the zip archive
				zip.AddFile(filePath, zipDirectory);
				zip.Save(zipName);
			}

			return zip;
		}

		public static void ExtractFiles(string zipFile, string zipDirectory, string fileName = "")
		{
			using (ZipFile zipForUnpack = ZipFile.Read(zipFile))
			{
				//If fileName is empty, it extracts all files
				if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName))
				{
					zipForUnpack.ExtractAll(zipDirectory, ExtractExistingFileAction.OverwriteSilently);
				}
				else
				{
					var result = zipForUnpack.Any(entry => entry.FileName.EndsWith(fileName));
					if (result)
					{
						foreach (var folder in zipForUnpack)
						{
							ZipEntry e = folder;
							if (e.ToString().EndsWith(fileName))
							{
								e.Extract(zipDirectory, ExtractExistingFileAction.OverwriteSilently);
							}
						}
					}
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
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

			FileInfo fileInfo = new FileInfo(dirPath);
			string dateCteated = fileInfo.CreationTime.ToString("dd-MMM-yyyy");

			using (ZipFile zip = ZipFile.Read(zipFilePath))
			{
				try
				{
					zip.AddFile(dirPath, zipDirectory);
					zip.Save();
				}
				catch (ArgumentException)
				{
					Console.WriteLine("File With the same name already added");
				}
			}
		}
	}
}