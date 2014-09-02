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
        private static void ExportFromMovieAwardsAndNominationsToXml()
        {
            //const string movieAward = "movie-awards";
            //const string movieNomination = "movie-nominations";
            //const string industryAwardsNode = "industry-awards";
            //string path = "../../MovieAwards.xml";

            //var connectionString = "Data Source=.SQLEXPRESS;Initial Catalog=...;Integrated Security=True";
            //var xmlFileData = "";
            //DataSet data = new DataSet();
            //var tables = new[] { "Hospital", "Patient" };
            //foreach (var table in tables)
            //{
            //    var query = "SELECT * FROM " + table + " WHERE (Hospital_Code = 'Hosp1')";
            //    SqlConnection connection = new SqlConnection(connectionString);
            //    SqlCommand command = new SqlCommand(query, connection);
            //    connection.Open();
            //    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            //    dataAdapter.Fill(data);
            //    connection.Close();
            //    connection.Dispose();
            //    xmlFileData += data.GetXml();
            //}
            //File.WriteAllText(path, xmlFileData);
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