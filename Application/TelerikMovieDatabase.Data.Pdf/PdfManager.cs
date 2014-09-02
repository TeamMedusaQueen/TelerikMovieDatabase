namespace TelerikMovieDatabase.Data.Pdf
{
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System;
    using System.Data.SqlClient;
    using System.IO;

    internal class PdfManager
    {
        private static void Main(string[] args)
        {
            const string PdfFileName = "MoviesPDFDocument.pdf";
            const string MetadataFileAuthor = "Team Medusa Queen";
            const string MetadataFileSubject = "PDF Export of TMDB data";
            const string MetadataFileTitle = "TMDB Report";
            const int TableNumberOfColumns = 3;

            DateTime reportDate = DateTime.Now;
            Font fontBold = new Font(Font.FontFamily.TIMES_ROMAN, 13, Font.BOLD, BaseColor.BLACK);

            string[] headerLines = new string[]    
            {
                "Telerik Academy\n",
                "Movie Report\n",
                "bul. Al. Malinov\n",
                reportDate.ToString()
            };

            // The exported file is located on the Desktop
            var pathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            FileStream fileStream = new FileStream(pathToDesktop + "/" + PdfFileName, FileMode.Create);
            Document documentPage = new Document(PageSize.A4, 25, 25, 20, 20);
            PdfWriter documentWriter = PdfWriter.GetInstance(documentPage, fileStream);

            // PDF Metadata
            documentPage.Open();
            documentPage.AddAuthor(MetadataFileAuthor);
            documentPage.AddCreator(MetadataFileAuthor);
            documentPage.AddSubject(MetadataFileSubject);
            documentPage.AddTitle(MetadataFileTitle);

            // Header paragraph data
            Paragraph paragraph = new Paragraph();
            paragraph.Alignment = Element.ALIGN_LEFT;

            foreach (var headerLine in headerLines)
            {
                paragraph.Add(new Phrase(headerLine));
            }

            documentPage.Add(paragraph);

            // Table title
            Paragraph tableTitle = new Paragraph();
            tableTitle.Alignment = Element.ALIGN_CENTER;
            tableTitle.Add("Movie Report\n");
            documentPage.Add(tableTitle);

            // Report table parameters
            PdfPTable reportTable = new PdfPTable(TableNumberOfColumns);
            reportTable.SetWidths(new float[] { 80f, 20f, 25f });

            SqlConnection dbCon = new SqlConnection("Server=(localdb)\\MSSqlLocalDB; " + "Database=TMDB; Integrated Security=true");
            dbCon.Open();

            using (dbCon)
            {
                string query = "SELECT top 25 Title as [Movie Title], Metascore, CONVERT(VARCHAR(11), ReleaseDate, 106) AS [Release Date] FROM Movies";

                SqlCommand cmd = new SqlCommand(query, dbCon);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {

                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {

                        PdfPCell cellName = new PdfPCell(new Phrase(dataReader.GetName(i), fontBold));
                        cellName.HorizontalAlignment = 1;
                        reportTable.AddCell(cellName);
                    }

                    while (dataReader.Read())
                    {
                        for (int q = 0; q < dataReader.FieldCount; q++)
                        {
                            reportTable.AddCell(dataReader[q].ToString());
                        }

                    }
                }

                documentPage.Add(reportTable);
            }

            documentPage.Close();
            documentWriter.Close();
            fileStream.Close();
        }
    }
}