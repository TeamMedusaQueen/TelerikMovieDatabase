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

    using TelerikMovieDatabase.Models;

    using TelerikMovieDatabase.Data.MsSql;

    public partial class frmTMDB : Form
    {
        TelerikMovieDatabaseMsSqlContext db = new TelerikMovieDatabaseMsSqlContext();

        public frmTMDB()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
            info["Awards"] = string.Join(", ", from award in movie.Awards select award.AwardAcademy);
            info["Gross"] = movie.Gross;

            return info;
        }
    }
}
