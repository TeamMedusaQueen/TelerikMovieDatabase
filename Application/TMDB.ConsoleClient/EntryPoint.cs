namespace TMDB.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using TMDB.Data;
    using System.Data.Entity;
    using TMDB.JsonOperations;
    using TMDB.ExcelOperations;
    using System.Data.OleDb;
    using TMDB.Models;

    internal class EntryPoint
    {
        private static void Main()
        {
            Dictionary<string, int> testData = new Dictionary<string, int>();
            testData.Add("testData 1", 1);
            testData.Add("testData2", 2);
            JsonManager manger = new JsonManager();
            string result = manger.ExportToJson(testData);
            Console.WriteLine(result);
            var data = manger.ImportFromJson(result);
            foreach (var entry in data)
            {
                Console.WriteLine(entry.Key + "--->" + entry.Value);
            }

            TmdbContext db = new TmdbContext();                       //reading from excell files and adding to sqlDB in BoxOfficeEntry
            //ExcelManager.InsertInSqlDB(db);

            
                Console.WriteLine(db.BoxOfficeEntries);
            
            ////ImportMovieAwardsAndNominationsFromXML();
            //
            // using (var dbContext = new TMDB.Data.TmdbContext())
            // {
            //     var movie = dbContext.Movies.Select(m => m.ID == 1);
            //     foreach (var m in movie)
            //     {
            //         Console.WriteLine(m);
            //     }
            // 	//var movies = dbContext.Movies.ToArray();
            //     //
            //     //
            // 	//var movieJSONModels = OMDB.GetTop250();
            // 	//foreach (var movieJSONModel in movieJSONModels)
            // 	//{
            // 	//	var movie = movieJSONModel.GetMovieModel(dbContext);
            // 	//	dbContext.Movies.Add(movie);
            // 	//}
            // 
            // 	dbContext.SaveChanges();
            // }
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
    }
}