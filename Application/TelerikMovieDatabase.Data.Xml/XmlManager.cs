namespace TelerikMovieDatabase.Data.Xml
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.IO;
	using System.Linq;
	using System.Runtime.Serialization;
	using System.Text;
	using System.Xml;

	public class XmlManager
	{
		public static TEntity Deserialize<TEntity>(string fileName)
		{
			TEntity data;

			var filePath = GetFilePath(fileName);
			using (var fileStream = File.OpenRead(filePath))
			{
				var xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(fileStream, XmlDictionaryReaderQuotas.Max);
				var dataContractSerializer = new DataContractSerializer(typeof(TEntity));
				data = (TEntity)dataContractSerializer.ReadObject(xmlDictionaryReader);
				xmlDictionaryReader.Close();
			}

			return data;
		}

		public static string Serialize<TEntity>(TEntity data)
		{
			using (var memoryStream = new MemoryStream())
			{
				XmlWriterSettings xmlSettings = new XmlWriterSettings
				{
					Indent = true,
					IndentChars = "\t",
					Encoding = new UTF8Encoding(false),
					CloseOutput = false,
				};

				var xmlDictionaryWriter = XmlWriter.Create(memoryStream, xmlSettings);
				var dataContractSerializer = new DataContractSerializer(data.GetType());
				dataContractSerializer.WriteObject(xmlDictionaryWriter, data);
				xmlDictionaryWriter.Close();

				memoryStream.Flush();
				memoryStream.Seek(0, SeekOrigin.Begin);
				using (var streamReader = new StreamReader(memoryStream))
				{
					return streamReader.ReadToEnd();
				}
			}
		}

		public static void SaveToFile(string fileName, string contents)
		{
			File.WriteAllText(GetFilePath(fileName), contents);
		}

		public static string GetFilePath(string fileName)
		{
			var filePath = Path.Combine(Settings.Default.FolderPath, fileName);
			return filePath;
		}
	}
}