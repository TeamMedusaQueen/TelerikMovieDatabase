﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDB.ExcelOperations
{
    public class ExcelManager
    {
        public static void InsertInExcelFile(int reportId, string title, int profits)
        {
            OleDbConnection excelConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\\..\\..\\..\\Databases\\SQLite\\MovieBudgetReportList.xlsx;Extended Properties=Excel 12.0;");
            excelConnection.Open();

            using (excelConnection)
            {
                OleDbCommand insertCommand = new OleDbCommand("INSERT INTO [Sheet1$] (ReportID, Title, Profits) VALUES (@reportId, @title, @profits)", excelConnection);
                insertCommand.Parameters.AddWithValue("@ReportID", reportId);
                insertCommand.Parameters.AddWithValue("@Title", title);
                insertCommand.Parameters.AddWithValue("@Profits", profits);

                insertCommand.ExecuteNonQuery();
            }
        }
    }
}
