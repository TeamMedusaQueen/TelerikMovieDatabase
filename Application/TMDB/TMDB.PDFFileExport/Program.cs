using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data.SqlClient;
using System.IO;

namespace TMDB.PDFFileExport
{
    class Program
    {
        static void Main(string[] args)
        {
            // The exported file is located on the Desktop
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            System.IO.FileStream fs = new FileStream(path + "/MoviesPDFDocument.pdf", FileMode.Create);
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.AddAuthor("Team Medusa Queen");
            document.AddCreator("Sample application using iTextSharp");
            document.AddSubject("PDF Export of TMDB data");
            document.AddTitle("TMDB Report");

            document.Open();
            iTextSharp.text.Font fontBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font fontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            DateTime date = DateTime.Now;

            Phrase ps = new Phrase("Telerik Academy" + "\n", fontBold);
            Phrase ps1 = new Phrase("Movie Report" + "\n", fontBold);
            Phrase p1 = new Phrase(date.TimeOfDay + "\n");
            Phrase p4 = new Phrase("bul. Al. Malinov" + "\n");

            var paragraph = new Paragraph();
            paragraph.Alignment = Element.ALIGN_LEFT;
            paragraph.Add(ps);
            paragraph.Add(ps1);
            paragraph.Add(p1);
            paragraph.Add(p4);
            //paragraph.Add(p2);
            //paragraph.Add(p5);
            //paragraph.Add(p6)s
            document.Add(paragraph);



            Paragraph paragraph4 = new Paragraph();
            paragraph4.Alignment = Element.ALIGN_CENTER;
            paragraph4.Add("Movie Report\n\n");
            paragraph4.Font.IsBold();
            document.Add(paragraph4);

            PdfPTable table = new PdfPTable(3);
            table.TotalWidth = 150f;

            float[] widths = new float[] { 80f, 20f, 25f };
            table.SetWidths(widths);

            PdfPCell cellID = new PdfPCell(new Phrase("Movie Title", fontBold));
            cellID.HorizontalAlignment = 1;
            PdfPCell cellName = new PdfPCell(new Phrase("Metascore", fontBold));
            cellName.HorizontalAlignment = 1;
            PdfPCell cellDesc = new PdfPCell(new Phrase("Release Date", fontBold));
            cellDesc.HorizontalAlignment = 1;
            table.AddCell(cellID);
            table.AddCell(cellName);
            table.AddCell(cellDesc);


            SqlConnection dbCon = new SqlConnection("Server=(localdb)\\MSSqlLocalDB; " + "Database=TMDB; Integrated Security=true");
            dbCon.Open();
            using (dbCon)
            {
                string query = "SELECT top 25 Title, Metascore, CONVERT(VARCHAR(11), ReleaseDate, 106) FROM Movies";

                SqlCommand cmd = new SqlCommand(query, dbCon);


                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        table.AddCell(rdr[0].ToString());
                        table.AddCell(rdr[1].ToString());
                        table.AddCell(rdr[2].ToString());
                    }
                }

                document.Add(table);
            }

            document.Close();
            writer.Close();
            fs.Close();

        }
    }
}
