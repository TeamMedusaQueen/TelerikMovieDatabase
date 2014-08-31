namespace TMDB.ConsoleClient
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Xml;
    using System.Data.SQLite;

	internal class Program
	{
		private static void Main()
		{
            var result  = ConnectToSqLiteDatabase();
            foreach (var entry in result)
            {
                Console.WriteLine(entry);
            }
			////ImportMovieAwardsAndNominationsFromXML();
            //
			//using (var dbContext = new TMDB.Data.TmdbContext())
			//{
			//	var movies = dbContext.Movies.ToArray();
            //  
            //
			//	//var movieJSONModels = OMDB.GetTop250();
			//	//foreach (var movieJSONModel in movieJSONModels)
			//	//{
			//	//	var movie = movieJSONModel.GetMovieModel(dbContext);
			//	//	dbContext.Movies.Add(movie);
			//	//}
            //
			//	//dbContext.SaveChanges();
			//}
            //
			////var mongoDbContext = new TMDB.Data.Provider.MongoDatabase.TmdbMongoDbContext();
			////mongoDbContext.InitialCreate();
		}

		private static void ImportMovieAwardsAndNominationsFromXML()
		{
			const string movieAward = "movie-awards";
			const string movieNomination = "movie-nominations";
			const string industryAwardsNode = "industry-awards";

			var result = new StringBuilder();

			var nominations = new List<string>();
			var awards = new List<string>();
			List<string> currentCollection = null;

			// Create an XmlReader
			using (XmlReader reader = XmlReader.Create(new StringReader(File.ReadAllText("Data.xml"))))
			{
				XmlWriterSettings ws = new XmlWriterSettings();
				ws.Indent = true;

				bool isInAwardNode = false;
				bool isInNominationNode = false;

				// Parse the file and display each of the nodes.
				while (reader.Read())
				{
					switch (reader.NodeType)
					{
						case XmlNodeType.Element:
							if (!(isInAwardNode || isInNominationNode))
							{
								isInAwardNode = reader.Name == movieAward;
								isInNominationNode = reader.Name == movieNomination;

								if (isInAwardNode)
								{
									currentCollection = awards;
								}

								if (isInNominationNode)
								{
									currentCollection = nominations;
								}
							}
							else if (reader.Name == industryAwardsNode)
							{
								var awardYear = reader.GetAttribute("year");
								var awardName = reader.ReadElementContentAsString();
								currentCollection.Add("Name: " + awardName + " Year: " + awardYear);
							}
							break;

						case XmlNodeType.EndElement:
							if (reader.Name != industryAwardsNode)
							{
								isInAwardNode = false;
								isInNominationNode = false;
								currentCollection = null;
							}
							break;
					}
				}
			}

			Console.WriteLine("Nominations:");
			foreach (var nomination in nominations)
			{
				Console.WriteLine(nomination);
			}

			Console.WriteLine("Awards:");
			foreach (var award in awards)
			{
				Console.WriteLine(award);
			}
		}

        private static List<Object> ConnectToSqLiteDatabase()
        {
            SQLiteConnection sqliteConnection = new SQLiteConnection("Data Source = ..\\..\\..\\..\\Databases\\SQLite\\MovieBudget.db");
            sqliteConnection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM MovieBudgetReports", sqliteConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            var data = new List<object>();
            using (reader)
            {
                while (reader.Read())
                {
                    int reportId = Convert.ToInt32(reader["ReportID"]);
                    string title = (string)reader["Title"];
                    int budget = Convert.ToInt32(reader["Budget"]);
                    data.Add(reportId);
                    data.Add(title);
                    data.Add(budget);
                    //InsertInExcel("Bugs", carModel, description, importance);
                }
            }
            return data;
        }
	}
}