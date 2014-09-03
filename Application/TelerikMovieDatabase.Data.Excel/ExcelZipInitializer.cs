namespace TelerikMovieDatabase.Data.Excel
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using TelerikMovieDatabase.Utils;

	public static class ExcelZipInitializer
	{
		public static void Init(string fileName)
		{
			const string ExcelExtension = ".xls";
			const string ZipExtension = ".zip";

			var folderPath = Settings.Default.FolderPath;
			var excelFilePath = Path.Combine(folderPath, fileName + ExcelExtension);
			var zipFilePath = Path.Combine(folderPath, fileName + ZipExtension);

			ZipManager.AddFileToZipArchive(excelFilePath, zipFilePath);
		}
	}
}
