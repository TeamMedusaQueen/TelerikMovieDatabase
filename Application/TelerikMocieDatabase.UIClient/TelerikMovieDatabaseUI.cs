namespace TelerikMovieDatabase.UIClient
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Data.Entity;

    using TelerikMovieDatabase.Common;
    using TelerikMovieDatabase.Models;

    using TelerikMovieDatabase.Data.MsSql;
    using TelerikMovieDatabase.Data.MySql;
    using TelerikMovieDatabase.Data.SqLite;
    using TelerikMovieDatabase.Data.Excel.Models;
    using TelerikMovieDatabase.Data.Imdb;
    using System.IO;
    using TelerikMovieDatabase.Data.Pdf;

    public partial class frmTMDB : Form
    {
        private TelerikMovieDatabaseMsSqlContext db = new TelerikMovieDatabaseMsSqlContext();
        private const string MoviesInitialXmlFileName = "MoviesInitial";

        public frmTMDB()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            listInfo.Items.Clear();

            if (cmbExport.Text == "MongoDB")
            {
                if (cmbImport.Text == "SQL")
                {

                }
                else
                {
                    listInfo.Items.Add("Invalid command!");
                }
            }
            else if (cmbExport.Text == "MsSQL")
            {
                if (cmbImport.Text == "MySQL")
                {

                }
                else if (cmbImport.Text == "XLS")
                {

                }
                else
                {
                    listInfo.Items.Add("Invalid command!");
                }
            }
            else if (cmbExport.Text == "MySQL")
            {

            }
            else if (cmbExport.Text == "SQLite")
            {
                if (cmbImport.Text == "XLS")
                {

                }
                else
                {
                    listInfo.Items.Add("Invalid command!");
                }
            }
            else if (cmbExport.Text == "XML")
            {
                if (cmbImport.Text == "SQL")
                {

                }
                else
                {
                    listInfo.Items.Add("Invalid command!");
                }
            }
            else if (cmbExport.Text == "XLS")
            {
                if (cmbImport.Text == "SQL")
                {

                }
                else
                {
                    listInfo.Items.Add("Invalid command!");
                }
            }
            else
            {
                listInfo.Items.Add("Invalid command!");
            }
        }
        private static void InitializeMongoDbAndXml()
        {
            //	IEnumerable<Movie> movies;
            //	using (var data = new TelerikMovieDatabaseMsSqlData())
            //	{
            //		data.Movies.DisableProxyCreation();
            //		movies = OpenMovieDatabase.GetTop250()
            //			.Select(movieJson => movieJson.GetMovieModel(data.Context)).ToArray();
            //		data.Movies.EnableProxyCreation();
            //	}
            //
            //	// Import First 150 to MongoDB
            //	new MongoDbInitializer().Init(movies.Take(150), forceReCreate: false);
            //	// Import the rest to the initial xml file
            //	ManagerProvider<Movie>.Xml.Export(movies.Skip(150).ToArray(), MoviesInitialXmlFileName);
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            listInfo.Items.Clear();

            string searchText = txtSearch.Text;
            var movies = db.Movies.Where(x => x.Title.Contains(searchText)).Select(x => x);

            foreach (var movie in movies)
            {
                var info = new Dictionary<string, object>(GetMovieInfo(movie));

                listInfo.Items.Add("Title: " + info["Title"]);

                if (chkAllInfo.Checked)
                {
                    chkActors.Checked = true;
                    chkAwards.Checked = true;
                    chkDirector.Checked = true;
                    chkGenre.Checked = true;
                    chkWriters.Checked = true;
                }

                if (chkActors.Checked)
                {
                    listInfo.Items.Add("\t\t Actors: " + info["Actors"]);
                }

                if (chkGenre.Checked)
                {
                    listInfo.Items.Add("\t\t Genres: " + info["Genres"]);
                }

                if (chkDirector.Checked)
                {
                    listInfo.Items.Add("\t\t Director: " + info["Director"]);
                }

                if (chkWriters.Checked)
                {
                    listInfo.Items.Add("\t\t Writers: " + info["Writers"]);
                }

                if (chkAwards.Checked)
                {
                    listInfo.Items.Add("\t\t Awards: " + info["Awards"]);
                }

                if (chkAllInfo.Checked)
                {
                    listInfo.Items.Add("\t\t Nominations: " + info["Nominations"]);
                    listInfo.Items.Add("\t\t Country: " + info["Country"]);
                    listInfo.Items.Add("\t\t Running time: " + info["Time"] + " min.");
                    listInfo.Items.Add("\t\t Gross Income: " + info["Gross"]);
                }

                listInfo.Items.Add(new string('_', 300));
            }
        }

        private void btnMovies_Click(object sender, EventArgs e)
        {
            listInfo.Items.Clear();

            var movies = from movie in db.Movies select movie;
            Dictionary<string, object> movieInfo;

            foreach (var movie in movies)
            {
                movieInfo = new Dictionary<string, object>(GetMovieInfo(movie));

                listInfo.Items.Add("Title: " + movieInfo["Title"]);
                listInfo.Items.Add("\t\t Country: " + movieInfo["Country"]);
                listInfo.Items.Add("\t\t Actors: " + movieInfo["Actors"]);
                listInfo.Items.Add("\t\t Genres: " + movieInfo["Genres"]);
                listInfo.Items.Add("\t\t Director: " + movieInfo["Director"]);
                listInfo.Items.Add("\t\t Writers: " + movieInfo["Writers"]);
                listInfo.Items.Add("\t\t Running time: " + movieInfo["Time"] + " min.");
                listInfo.Items.Add("\t\t Nominations: " + movieInfo["Nominations"]);
                listInfo.Items.Add("\t\t Awards: " + movieInfo["Awards"]);
                listInfo.Items.Add("\t\t Gross Income: " + movieInfo["Gross"]);
                listInfo.Items.Add(new string('_', 300));
            }
        }

        private Dictionary<string, object> GetMovieInfo(Movie movie)
        {
            Dictionary<string, object> info = new Dictionary<string, object>();

            info["Title"] = movie.Title;
            info["Country"] = string.Join(", ", from country in movie.Countries select country.Name);
            info["Actors"] = string.Join(", ", from actor in movie.Cast select actor.Name);
            info["Genres"] = string.Join(", ", from genre in movie.Genres select genre.Title);
            info["Director"] = movie.Director;
            info["Writers"] = string.Join(", ", from writer in movie.Writers select writer.Name);
            info["Time"] = movie.RunningTime;
            info["Nominations"] = string.Join(", ", from nominee in movie.Nominations select nominee.AwardAcademy);
            info["Awards"] = string.Join(", ", from award in movie.Awards select award.AwardAcademy);
            info["Gross"] = movie.Gross;

            return info;
        }

        private void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbExport.Text == "MongoDB")
            {
                cmbImport.Items.Clear();
                cmbImport.Items.Add("SQL");
                cmbImport.Text = "SQL";
            }

            if (cmbExport.Text == "MsSQL")
            {
                cmbImport.Items.Clear();
                cmbImport.Items.Add("MySQL");
                cmbImport.Items.Add("XLS");
                cmbImport.Text = "MySQL";
            }

            if (cmbExport.Text == "MySQL")
            {
                cmbImport.Items.Clear();
                cmbImport.Items.Add("XLS");
                cmbImport.Text = "XLS";
            }

            if (cmbExport.Text == "SQLite")
            {
                cmbImport.Items.Clear();
                cmbImport.Items.Add("XLS");
                cmbImport.Text = "XLS";
            }

            if (cmbExport.Text == "XML")
            {
                cmbImport.Items.Clear();
                cmbImport.Items.Add("SQL");
                cmbImport.Text = "SQL";
            }

            if (cmbExport.Text == "XLS")
            {
                cmbImport.Items.Clear();
                cmbImport.Items.Add("SQL");
                cmbImport.Text = "SQL";
            }
        }

        private void btnOpenReport_Click(object sender, EventArgs e)
        {
            listInfo.Items.Clear();

            if (cmbExportInfo.Text == "JSON")
            {
                using (var data = new TelerikMovieDatabaseMsSqlData())
                {
                    var jsonManager = ManagerProvider<Movie>.Json;
                    jsonManager.IsMultiple = true;
                    jsonManager.Export(
                        data.Movies,
                        "MovieBoxOfficeReport",
                        movie => new Movie()
                        {
                            ID = movie.ID,
                            Title = movie.Title,
                            Gross = movie.BoxOfficeEntry.GeneratedWeekendIncome,
                        },
                        movie => movie.BoxOfficeEntry != null,
                        movie => movie.BoxOfficeEntry);
                }

                // Read the exported json files to MySql
                var jsonFiles = Directory.GetFiles(Data.Json.Settings.Default.FolderPath)
                    .Select(file => Path.GetFileNameWithoutExtension(file))
                    .ToArray();
                var movesGross = ManagerProvider<Movie>.Json.DeserializeMultiple(jsonFiles)
                    .ToDictionary(movie => movie.Title, movie => movie.Gross);
                MySqlManager.InsertIntoMySqlDatabase(movesGross);
            }
            else if (cmbExportInfo.Text == "PDF")
            {
                PdfManager.ExportPdfReport("MoviesReport");
            }
            else if (cmbExportInfo.Text == "XML")
            {
                using (var data = new TelerikMovieDatabaseMsSqlData())
                {
                    ManagerProvider<Movie>.Xml.Export(
                        data.Movies,
                        "GoodOldMovies",
                        movie => movie.Metascore.HasValue && movie.Metascore.Value > 70
                            && movie.ReleaseDate.HasValue && movie.ReleaseDate.Value.Year < 1970,
                        movie => movie.Director,
                        movie => movie.Writers,
                        movie => movie.Cast);
                }

            }
            else if (cmbExportInfo.Text == "XLS")
            {
                var grossReports = MySqlManager.GetDataFromMySqlDatabase();
                var movieBudgets = SqLiteManager.GetDataFromSqLiteDatabase();

                var moviesRevenue = new List<MovieRevenueReport>();

                foreach (var moveBudget in movieBudgets)
                {
                    var grossReport = grossReports
                        .FirstOrDefault(movie => movie.Title.Equals(moveBudget.Title, StringComparison.OrdinalIgnoreCase));

                    if (grossReport != null)
                    {
                        moviesRevenue.Add(new MovieRevenueReport()
                        {
                            Title = moveBudget.Title,
                            Gross = grossReport.Gross,
                            Revenue = grossReport.Gross - moveBudget.Budget
                        });
                    }
                }

                ManagerProvider<MovieRevenueReport>.Excel2007.Export(moviesRevenue.ToArray(), "MoviesRevenue");
            }
            else
            {
                listInfo.Items.Add("Invalid operation!");
            }
        }
    }
}
