namespace TelerikMovieDatabase.Data.Excel
{
	using ClosedXML.Excel;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using TelerikMovieDatabase.Data;
	using TelerikMovieDatabase.Models;

	public class Excel2007Manager<TEntity> : ImportExportManagerBase<TEntity, byte[]>
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

		public override byte[] Serialize(IEnumerable<TEntity> data, string fileName)
		{
			var workBook = new XLWorkbook();
			var workSheet = workBook.Worksheets.Add(fileName);

			int rowIndex = 1;

			var entityProperties = typeof(TEntity).GetProperties();
			// Header
			for (int propertyIndex = 0; propertyIndex < entityProperties.Length; propertyIndex++)
			{
				var property = entityProperties[propertyIndex];
				workSheet.Cell(rowIndex, propertyIndex + 1).Value = property.Name;
			}

			// Rows
			foreach (var item in data)
			{
				rowIndex++;

				for (int propertyIndex = 0; propertyIndex < entityProperties.Length; propertyIndex++)
				{
					var property = entityProperties[propertyIndex];
					var propertyValue = property.GetValue(item);
					string propertyValueToString = null;

					if (propertyValue is IEnumerable && !(propertyValue is String))
					{
						var dataArray = propertyValue as IEnumerable;
						var dataItems = new List<string>();
						foreach (var dataItem in dataArray)
						{
							dataItems.Add(dataItem.ToString());
						}

						propertyValueToString = string.Join(", ", dataItems);
					}
					else
					{
						propertyValueToString = propertyValue.ToString();
					}

					workSheet.Cell(rowIndex, propertyIndex + 1).Value = propertyValueToString;
				}
			}

			var tableData = workSheet.Range(1, 1, rowIndex, entityProperties.Length).CreateTable();
			tableData.Theme = XLTableTheme.TableStyleMedium17;
			workSheet.Columns().AdjustToContents();

			byte[] fileData;

			using (var excelFileStream = new MemoryStream())
			{
				workBook.SaveAs(excelFileStream);

				excelFileStream.Flush();
				excelFileStream.Seek(0, SeekOrigin.Begin);
				fileData = excelFileStream.GetBuffer();
			}

			return fileData;
		}

		public override IEnumerable<TEntity> Deserialize(byte[] output)
		{
			throw new NotImplementedException();
		}

		public override void SaveToFile(string filePath, byte[] contents)
		{
			File.WriteAllBytes(filePath, contents);
		}
	}
}