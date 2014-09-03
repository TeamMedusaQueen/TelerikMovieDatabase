namespace TelerikMovieDatabase.Data.Excel
{
	using ClosedXML.Excel;
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.IO;
	using System.Linq;
	using System.Text;
	using TelerikMovieDatabase.Data;
	using TelerikMovieDatabase.Data.Xml;
	using TelerikMovieDatabase.Models;

	public class Excel2007Manager<TEntity> : ImportExportManagerBase<TEntity, MemoryStream>
		where TEntity : class, IKeyHolder
	{
		public override string FolderPath
		{
			get
			{
				return Settings.Default.FolderPath;
			}
		}

		public override string FileExtension
		{
			get
			{
				return ".xlsx";
			}
		}

		public override MemoryStream Serialize(IEnumerable<TEntity> data, string fileName)
		{
			var xmlData = new XmlManager<TEntity>().Serialize(data, fileName);
			var dataSet = new DataSet();

			var workBook = new XLWorkbook();
			var workSheet = workBook.Worksheets.Add(fileName);

			var xmlDataBytes = Encoding.UTF8.GetBytes(xmlData);
			using (var memoryStream = new MemoryStream(xmlDataBytes))
			{
				dataSet.ReadXml(memoryStream);
			}

			var table = dataSet.Tables[0];
			int rowIndex = 0;
			int rowsCount = table.Rows.Count;
			int colIndex = 0;
			int colsCount = table.Columns.Count;
			for (rowIndex = 0; rowIndex <= rowsCount - 1; rowIndex++)
			{
				var tableRow = table.Rows[rowIndex];
				for (colIndex = 0; colIndex <= colsCount - 1; colIndex++)
				{
					var tableCol = tableRow.ItemArray[colIndex];
					workSheet.Cell(rowIndex + 1, colIndex + 1).Value = tableCol.ToString();
				}
			}

			//var tableData = workSheet.Range(0, 0, rowsCount, colsCount).CreateTable();
			//tableData.Theme = XLTableTheme.TableStyleMedium17;
			workSheet.Columns().AdjustToContents();

			var excelFileStream = new MemoryStream();
			workBook.SaveAs(excelFileStream);

			excelFileStream.Flush();
			excelFileStream.Seek(0, SeekOrigin.Begin);

			return excelFileStream;
		}

		public override IEnumerable<TEntity> Deserialize(MemoryStream output)
		{
			throw new NotImplementedException();
		}

		public override void SaveToFile(string filePath, MemoryStream contents)
		{
			File.WriteAllBytes(filePath, contents.GetBuffer());
		}
	}
}