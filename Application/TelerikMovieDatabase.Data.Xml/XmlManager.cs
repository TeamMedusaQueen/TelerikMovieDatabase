namespace TelerikMovieDatabase.Data.Xml
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Runtime.Serialization;
	using System.Text;
	using System.Xml;
	using TelerikMovieDatabase.Data;
	using TelerikMovieDatabase.Models;

	public class XmlManager<TEntity> : ImportExportManagerBase<TEntity, string>
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
				return ".xml";
			}
		}

		public override IEnumerable<TEntity> Deserialize(string fileName)
		{
			TEntity[] data;

			var filePath = GetFilePath(fileName);
			using (var fileStream = File.OpenRead(filePath))
			{
				var xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(fileStream, XmlDictionaryReaderQuotas.Max);
				var dataContractSerializer = new DataContractSerializer(typeof(TEntity[]));
				data = (TEntity[])dataContractSerializer.ReadObject(xmlDictionaryReader);
				xmlDictionaryReader.Close();
			}

			return data;
		}

		public override string Serialize(IEnumerable<TEntity> data, string fileName)
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

		public override void SaveToFile(string filePath, string contents)
		{
			File.WriteAllText(filePath, contents);
		}
	}
}