namespace TelerikMovieDatabase.UIClient
{
    using System;
    using System.Linq;
    using System.Windows.Forms;

    static class UIEntry
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmTMDB());
        }
    }
}
