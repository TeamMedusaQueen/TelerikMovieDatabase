namespace TelerikMovieDatabase.Utils
{
	using Ionic.Zip;
	using System;
	using System.IO;
	using System.Linq;

	public static class ZipManager
	{
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

		public static void AddFileToZipArchive(string dirPath, string zipFilePath)
		{
			if (!File.Exists(zipFilePath))
			{
				File.Delete(zipFilePath);
			}

			using (var zip = new ZipFile(zipFilePath))
			{
				try
				{
					zip.AddFile(dirPath, string.Empty);
					zip.Save();
				}
				catch (ArgumentException)
				{
					Console.WriteLine("File with the same name already added");
				}
			}
		}

		private static ZipFile CreateZipFile(string filePath, string zipDirectory, string zipName)
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
	}
}