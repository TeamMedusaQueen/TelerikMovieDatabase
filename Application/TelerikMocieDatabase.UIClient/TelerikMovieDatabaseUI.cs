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

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnMovies_Click(object sender, EventArgs e)
        {
            var movies = from movie in db.Movies select movie;

            foreach (var movie in movies)
            {
                StringBuilder info = new StringBuilder();

                string movieName = movie.Title;
                var country = from cntry in movie.Countries select cntry.Name;
                var actors = from actr in movie.Cast select actr.Name;
                var genre = from gnr in movie.Genres select gnr.Title;
                var director = movie.Director;
                var writers = from writer in movie.Writers select writer.Name;
                var time = movie.RunningTime;
                var awards = from award in movie.Awards select award.AwardAcademy;
                var grossIncome = movie.Gross;

                listInfo.Items.Add("Title: " + movieName);
                listInfo.Items.Add("\t\t Country: " + string.Join(", ", country));
                listInfo.Items.Add("\t\t Actors: " + string.Join(", ", actors));
                listInfo.Items.Add("\t\t Genres: " + string.Join(", ", genre));
                listInfo.Items.Add("\t\t Director: " + string.Join(", ", director));
                listInfo.Items.Add("\t\t Writers: " + string.Join(", ", writers));
                listInfo.Items.Add("\t\t Running time: " + time + " min.");
                listInfo.Items.Add("\t\t Awards: " + string.Join(", ", awards));
                listInfo.Items.Add("\t\t Gross Income: " + string.Join(", ", grossIncome));
                listInfo.Items.Add(new string('_', 300));
            }
        }
    }
}
