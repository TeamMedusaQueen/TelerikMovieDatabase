namespace TelerikMovieDatabase.Data.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    public class XmlManager
    {
        public void ExportFromMovieToXml()
        {
            string path = "../../../Movies.xml";

            var connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=TMDB;Integrated Security=True";
            var xmlFileData = "<?xml version='1.0'?>";
            DataSet data = new DataSet();
            data.DataSetName = "Movies";
            var tables = new[] { "Movies" };
            foreach (var table in tables)
            {
                var query = "SELECT Title, RunningTime, Rating  FROM " + table;
                //" WHERE (RunningTime = '200')";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(data);
                connection.Close();
                connection.Dispose();
                xmlFileData += data.GetXml();
            }
            File.WriteAllText(path, xmlFileData);
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