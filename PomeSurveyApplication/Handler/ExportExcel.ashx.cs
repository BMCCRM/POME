
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;


namespace PomeSurveyApplication.Handler
{
    /// <summary>
    /// Summary description for ExportExcel
    /// </summary>
    public class ExportExcel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string type = context.Request.QueryString["Type"];

            #region Survey Statistics

            if (type == "SurveyStatistics")
            {
                string ExportDate = context.Request.QueryString["ExportDate"];
                string HospitalPharmacist = context.Request.QueryString["HospitalPharmacist"];
                string MonthlyDaily = context.Request.QueryString["MonthlyDaily"];

                DateTime ExportDateDt = DateTime.Now;
                try
                {
                    ExportDateDt = DateTime.ParseExact(ExportDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {

                }

                #region Download Work For Survey Statistics

                if (HospitalPharmacist != "")
                {
                    NameValueCollection _nv = new NameValueCollection();
                    _nv.Clear();
                    _nv.Add("@day-int", (MonthlyDaily == "Month") ? "0" : ExportDateDt.Day.ToString());
                    _nv.Add("@month-int", ExportDateDt.Month.ToString());
                    _nv.Add("@year-int", ExportDateDt.Year.ToString());
                    DataSet dsData = GetData(((HospitalPharmacist == "Hospitals") ? "sp_getSurveyStatisticsByHospitals" : "sp_getSurveyStatisticsByPharmacists"), _nv);

                    if (dsData != null)
                    {
                        if (dsData.Tables.Count != 0)
                        {
                            MemoryStream ms = DataTableToXlsx(dsData, context);
                            ms.WriteTo(context.Response.OutputStream);
                            context.Response.ContentType = "application/vnd.ms-excel";
                            context.Response.AddHeader("Content-Disposition", "attachment;filename=SurveyStatisticsDataBy" + HospitalPharmacist + ".xlsx");
                            context.Response.StatusCode = 200;
                            context.Response.End();
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream();
                            ExcelPackage pack = new ExcelPackage();
                            ExcelWorksheet ws = pack.Workbook.Worksheets.Add("Statistics Data");
                            ws.Cells[1, 1].Value = "No Data Found!";
                            pack.SaveAs(ms);
                            ms.WriteTo(context.Response.OutputStream);
                            context.Response.ContentType = "application/vnd.ms-excel";
                            context.Response.AddHeader("Content-Disposition", "attachment;filename=SurveyStatisticsDataBy" + HospitalPharmacist + ".xlsx");
                            context.Response.StatusCode = 200;
                            context.Response.End();
                        }
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        ExcelPackage pack = new ExcelPackage();
                        ExcelWorksheet ws = pack.Workbook.Worksheets.Add("Statistics Data");
                        ws.Cells[1, 1].Value = "No Data Found!";
                        pack.SaveAs(ms);
                        ms.WriteTo(context.Response.OutputStream);
                        context.Response.ContentType = "application/vnd.ms-excel";
                        context.Response.AddHeader("Content-Disposition", "attachment;filename=SurveyStatisticsDataBy" + HospitalPharmacist + ".xlsx");
                        context.Response.StatusCode = 200;
                        context.Response.End();
                    }
                }

                #endregion
            }
            #endregion

            #region Survey Detail Report

            if (type == "SurveyDetailReport")
            {
                string ExportDate = context.Request.QueryString["ExportDate"];
                string HospitalPharmacist = context.Request.QueryString["HospitalPharmacist"];
                string MonthlyDaily = context.Request.QueryString["MonthlyDaily"];

                DateTime ExportDateDt = DateTime.Now;
                try
                {
                    ExportDateDt = DateTime.ParseExact(ExportDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {

                }

                if (HospitalPharmacist != "")
                {
                    NameValueCollection _nv = new NameValueCollection();
                    _nv.Clear();
                    //_nv.Add("@day-int", (MonthlyDaily == "Month") ? "0" : ExportDateDt.Day.ToString());
                    _nv.Add("@day-int", ExportDateDt.Day.ToString());
                    _nv.Add("@month-int", ExportDateDt.Month.ToString());
                    _nv.Add("@year-int", ExportDateDt.Year.ToString());
                    //DataSet dsData = GetData(((HospitalPharmacist == "Hospitals") ? "sp_getSurveyStatisticsByHospitals" : "sp_getSurveyStatisticsByPharmacists"), _nv);

                    DataSet dsData = GetData("sp_getSurveyDetailsReportData", _nv);

                    if (dsData != null)
                    {
                        if (dsData.Tables.Count != 0)
                        {
                            MemoryStream ms = DataTableToXlsxForSurvey(dsData, context);
                            ms.WriteTo(context.Response.OutputStream);
                            context.Response.ContentType = "application/vnd.ms-excel";
                            context.Response.AddHeader("Content-Disposition", "attachment;filename=SurveyDetailsReport.xlsx");
                            context.Response.StatusCode = 200;
                            context.Response.End();
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream();
                            ExcelPackage pack = new ExcelPackage();
                            ExcelWorksheet ws = pack.Workbook.Worksheets.Add("Survey Data");
                            ws.Cells[1, 1].Value = "No Data Found!";
                            pack.SaveAs(ms);
                            ms.WriteTo(context.Response.OutputStream);
                            context.Response.ContentType = "application/vnd.ms-excel";
                            context.Response.AddHeader("Content-Disposition", "attachment;filename=SurveyDetailsReport.xlsx");
                            context.Response.StatusCode = 200;
                            context.Response.End();
                        }
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        ExcelPackage pack = new ExcelPackage();
                        ExcelWorksheet ws = pack.Workbook.Worksheets.Add("Survey Data");
                        ws.Cells[1, 1].Value = "No Data Found!";
                        pack.SaveAs(ms);
                        ms.WriteTo(context.Response.OutputStream);
                        context.Response.ContentType = "application/vnd.ms-excel";
                        context.Response.AddHeader("Content-Disposition", "attachment;filename=SurveyDetailsReport.xlsx");
                        context.Response.StatusCode = 200;
                        context.Response.End();
                    }
                }

            }

            #endregion
        }

        public static MemoryStream DataTableToXlsxForSurvey(DataSet dsData, HttpContext context)
        {
            MemoryStream Result = new MemoryStream();
            ExcelPackage pack = new ExcelPackage();

            for (int i = 0; i < dsData.Tables.Count; i++)
            {
                DataTable dt = dsData.Tables[i];
                if (dt != null)
                {
                    if (dt.Rows.Count != 0)
                    {
                        ExcelWorksheet ws = pack.Workbook.Worksheets.Add(dt.Rows[0]["TableName"].ToString());
                        ws.DefaultColWidth = 25;
                        ws.Cells[1, 1, dt.Rows.Count + 2, dt.Columns.Count - 1].Style.WrapText = true;
                        ws.Cells[1, 1, dt.Rows.Count + 2, dt.Columns.Count - 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells[1, 1, dt.Rows.Count + 2, dt.Columns.Count - 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        for (int j = 1; j <= dt.Rows.Count; j++)
                        {
                            if (j == 1)
                            {
                                for (int k = 1; k < dt.Columns.Count; k++)
                                {
                                    if (dt.Columns[k].ToString() != "TableName")
                                    {
                                        ws.Cells[j, 23, j, dt.Columns.Count - 2].Merge = true;
                                        ws.Cells[j, 23, j, dt.Columns.Count - 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                                        ws.Cells[j, 23].Value = "Question";
                                        ws.Cells[j, 23].Style.Font.Bold = true;
                                        ws.Cells[j, 23].Style.Font.UnderLine = true;

                                        if (k <= 22)
                                        {
                                            ws.Cells[j, k, j + 1, k].Merge = true;
                                            ws.Cells[j, k, j + 1, k].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                                            ws.Cells[j, k].Style.Font.Bold = true;
                                            ws.Cells[j, k].Style.Font.UnderLine = true;
                                            ws.Cells[j, k].Value = dt.Columns[k].ToString();
                                        }
                                        else
                                        {
                                            ws.Cells[j + 1, k].Value = dt.Columns[k].ToString();
                                            ws.Cells[j + 1, k].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                                        }
                                    }

                                }
                            }
                            DataRow dr = dt.Rows[j - 1];
                            for (int k = 1; k < dt.Columns.Count; k++)
                            {

                                if (dt.Columns[k].ToString() != "TableName")
                                {
                                    if (dr.ItemArray[k].ToString() == "")
                                    {
                                        ws.Cells[j + 2, k].Value = "N/A";
                                    }
                                    else
                                    {
                                        ws.Cells[j + 2, k].Value = dr.ItemArray[k].ToString();
                                    }

                                    ws.Cells[j + 2, k].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                                }

                            }
                        }
                    }
                }

            }


            //#region Doctor Details Sheet Planning
            //ExcelWorksheet ws = pack.Workbook.Worksheets.Add("");
            //ws.Row(1).Height = 55.75;
            //ws.Column(1).Hidden = true;
            //ws.Column(2).Hidden = true;
            //ws.Cells["A1:AL1"].Merge = true;
            //ws.Cells["A1:AL1"].Value = "Plan Upload Sheet";
            //ws.Cells["A1:AL1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //ws.Cells["A1:AL1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //ws.Cells["A1:AL1"].Style.Font.Name = "Arial";
            //ws.Cells["A1:AL1"].Style.Font.Size = 21;
            //ws.Cells["A1:AL1"].Style.Font.Bold = true;
            //ws.Cells["A1:AL1"].Style.Font.Color.SetColor(Color.FromArgb(164, 82, 61));
            //ws.Cells["A1:AL1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            //ws.Cells["A1:AL1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(238, 236, 225));
            //ws.Cells["A1:AL1"].Style.WrapText = true;

            ////ws.Cells[2, 1].Value = initial.ToString("dd/MM/yyyy");

            ////ws.Cells["A2:D2"].Merge = true;
            ////ws.Cells["A2:D2"].Value = "Plan of Month";
            ////ws.Cells["E2:AL2"].Merge = true;
            ////ws.Cells["E2:AL2"].Value = initial.ToString("dd/MM/yyyy");

            //ws.Cells[3, 1].Value = "Master Plan ID";
            //ws.Cells[3, 2].Value = "Doctor ID";
            //ws.Cells[3, 3].Value = "Doctor Code";
            //ws.Cells[3, 4].Value = "Doctor Name";
            //ws.Cells[3, 5].Value = "Speciality";
            //ws.Cells[3, 6].Value = "Class";
            //ws.Cells[3, 7].Value = "Brick";
            //ws.Cells[3, 8].Value = "1";
            //ws.Cells[3, 9].Value = "2";
            //ws.Cells[3, 10].Value = "3";
            //ws.Cells[3, 11].Value = "4";
            //ws.Cells[3, 12].Value = "5";
            //ws.Cells[3, 13].Value = "6";
            //ws.Cells[3, 14].Value = "7";
            //ws.Cells[3, 15].Value = "8";
            //ws.Cells[3, 16].Value = "9";
            //ws.Cells[3, 17].Value = "10";
            //ws.Cells[3, 18].Value = "11";
            //ws.Cells[3, 19].Value = "12";
            //ws.Cells[3, 20].Value = "13";
            //ws.Cells[3, 21].Value = "14";
            //ws.Cells[3, 22].Value = "15";
            //ws.Cells[3, 23].Value = "16";
            //ws.Cells[3, 24].Value = "17";
            //ws.Cells[3, 25].Value = "18";
            //ws.Cells[3, 26].Value = "19";
            //ws.Cells[3, 27].Value = "20";
            //ws.Cells[3, 28].Value = "21";
            //ws.Cells[3, 29].Value = "22";
            //ws.Cells[3, 30].Value = "23";
            //ws.Cells[3, 31].Value = "24";
            //ws.Cells[3, 32].Value = "25";
            //ws.Cells[3, 33].Value = "26";
            //ws.Cells[3, 34].Value = "27";
            //ws.Cells[3, 35].Value = "28";
            //ws.Cells[3, 36].Value = "29";
            //ws.Cells[3, 37].Value = "30";
            //ws.Cells[3, 38].Value = "31";
            //int col = 2;
            //int row = 4;
            //#endregion

            //#region Call Details Sheet Planning
            //ExcelWorksheet ws1 = pack.Workbook.Worksheets.Add("Call Details");
            //ws1.Row(1).Height = 55.75;
            //ws1.Column(2).Hidden = true;
            //ws1.Cells["A1:G1"].Merge = true;
            //ws1.Cells["A1:G1"].Value = "Plan Upload Sheet";
            //ws1.Cells["A1:G1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //ws1.Cells["A1:G1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //ws1.Cells["A1:G1"].Style.Font.Name = "Arial";
            //ws1.Cells["A1:G1"].Style.Font.Size = 21;
            //ws1.Cells["A1:G1"].Style.Font.Bold = true;
            //ws1.Cells["A1:G1"].Style.Font.Color.SetColor(Color.FromArgb(164, 82, 61));
            //ws1.Cells["A1:G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            //ws1.Cells["A1:G1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(238, 236, 225));
            //ws1.Cells["A1:G1"].Style.WrapText = true;
            //ws1.Cells["A2:G2"].Merge = true;
            //ws1.Cells["A2:G2"].Value = "The note will go here ";
            //ws1.Cells[3, 1].Value = "Doctor Code";
            //ws1.Cells[3, 2].Value = "PlannerID";
            //ws1.Cells[3, 3].Value = "Day";
            //ws1.Cells[3, 4].Value = "Activity";
            //ws1.Cells[3, 5].Value = "Start time";
            //ws1.Cells[3, 6].Value = "End Time";
            ///*ws1.Cells[3, 7].Value = "P1";
            //ws1.Cells[3, 8].Value = "P2";
            //ws1.Cells[3, 9].Value = "P3";
            //ws1.Cells[3, 10].Value = "P4";
            //ws1.Cells[3, 11].Value = "R1";
            //ws1.Cells[3, 12].Value = "R2";
            //ws1.Cells[3, 13].Value = "R3";
            //ws1.Cells[3, 14].Value = "R4";
            //ws1.Cells[3, 15].Value = "G1";
            //ws1.Cells[3, 16].Value = "G2";*/
            //ws1.Cells[3, 17].Value = "Remarks";
            //ws1.Cells[3, 7].Value = "Remarks";
            //int row1 = 4;
            //#endregion

            //#region Setting data on Sheets
            //foreach (DataRow rw in table.Rows)
            //{
            //    foreach (DataColumn cl in table.Columns)
            //    {
            //        if (rw[cl.ColumnName] != DBNull.Value && (
            //            cl.ColumnName.ToString() == "DoctorId" ||
            //            cl.ColumnName.ToString() == "DoctorCode" ||
            //            cl.ColumnName.ToString() == "DoctorName" ||
            //            cl.ColumnName.ToString() == "Speciality" ||
            //            cl.ColumnName.ToString() == "Class" ||
            //            cl.ColumnName.ToString() == "Brick"
            //            ))
            //        {
            //            ws.Cells[row, col].Value = rw[cl.ColumnName].ToString();
            //            if (cl.ColumnName.ToString() == "DoctorId")
            //            {
            //                DataTable lsDT = SchedulerManager.getEventsForPlanning(Convert.ToInt32(HttpContext.Current.Session["CurrentUserId"].ToString()), initial, Convert.ToInt32(rw[cl.ColumnName].ToString()));
            //                if (lsDT.Rows.Count > 0)
            //                {
            //                    #region Master Plan Id
            //                    ws.Cells[row, 1].Value = lsDT.Rows[0][6].ToString();
            //                    #endregion

            //                    for (int i = 0; i < lsDT.Rows.Count; i++)
            //                    {
            //                        string startdate = lsDT.Rows[i][3].ToString().Split(new string[] { " " }, StringSplitOptions.None)[0].ToString();
            //                        string enddate = lsDT.Rows[i][4].ToString().Split(new string[] { " " }, StringSplitOptions.None)[0].ToString();
            //                        string starttime = lsDT.Rows[i][3].ToString().Split(new string[] { " " }, StringSplitOptions.None)[1].ToString();
            //                        string endtime = lsDT.Rows[i][4].ToString().Split(new string[] { " " }, StringSplitOptions.None)[1].ToString();
            //                        int Day = Convert.ToInt32(startdate.Split(new string[] { "/" }, StringSplitOptions.None)[1].ToString());
            //                        ws.Cells[row, 7 + Day].Value = "P";
            //                        string plannerID = lsDT.Rows[i][5].ToString();
            //                        string ActName = lsDT.Rows[i][0].ToString();
            //                        string DocCode = lsDT.Rows[i][7].ToString();
            //                        string Description = lsDT.Rows[i][12].ToString();
            //                        ws1.Cells[row1, 1].Value = DocCode;
            //                        ws1.Cells[row1, 2].Value = plannerID;
            //                        ws1.Cells[row1, 3].Value = Day.ToString();
            //                        ws1.Cells[row1, 5].Value = starttime;
            //                        ws1.Cells[row1, 6].Value = endtime;
            //                        ws1.Cells[row1, 17].Value = Description;
            //                        ws1.Cells[row1, 7].Value = Description;
            //                        switch (ActName)
            //                        {
            //                            case "Calls":
            //                                ws1.Cells[row1, 4].Value = "C";
            //                                break;
            //                            case "Marketing Activity":
            //                                ws1.Cells[row1, 4].Value = "MA";
            //                                break;
            //                            case "Leave":
            //                                ws1.Cells[row1, 4].Value = "L";
            //                                break;
            //                            case "Meeting":
            //                                ws1.Cells[row1, 4].Value = "M";
            //                                break;
            //                            case "Public Holiday":
            //                                ws1.Cells[row1, 4].Value = "P";
            //                                break;
            //                        }
            //                        row1++;
            //                    }
            //                }
            //            }
            //            col++;
            //        }
            //    }
            //    row++;
            //    col = 2;
            //}
            //#endregion

            if (pack.Workbook.Worksheets.Count == 0)
            {
                ExcelWorksheet ws = pack.Workbook.Worksheets.Add("Statistics Data");
                ws.Cells[1, 1].Value = "No Data Found!";
            }
            pack.SaveAs(Result);

            return Result;
        }

        public static MemoryStream DataTableToXlsx(DataSet dsData, HttpContext context)
        {
            MemoryStream Result = new MemoryStream();
            ExcelPackage pack = new ExcelPackage();

            for (int i = 0; i < dsData.Tables.Count; i++)
            {
                DataTable dt = dsData.Tables[i];
                if (dt != null)
                {
                    if (dt.Rows.Count != 0)
                    {
                        ExcelWorksheet ws = pack.Workbook.Worksheets.Add(dt.Rows[0]["TableName"].ToString());

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (j == 0)
                            {
                                for (int k = 0; k < dt.Columns.Count; k++)
                                {
                                    if (dt.Columns[k].ToString() != "TableName")
                                    {
                                        ws.Cells[j + 1, k + 1].Value = dt.Columns[k].ToString();
                                        ws.Cells[j + 1, k + 1].Style.Font.Bold = true;
                                    }
                                }
                            }
                            DataRow dr = dt.Rows[j];
                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                if (dt.Columns[k].ToString() != "TableName")
                                {
                                    ws.Cells[j + 2, k + 1].Value = dr.ItemArray[k].ToString();
                                }
                            }
                        }
                    }
                }

            }


            if (pack.Workbook.Worksheets.Count == 0)
            {
                ExcelWorksheet ws = pack.Workbook.Worksheets.Add("Statistics Data");
                ws.Cells[1, 1].Value = "No Data Found!";
            }
            pack.SaveAs(Result);

            return Result;
        }

        


        public DataSet GetData(String spName, NameValueCollection nv)
        {
            #region Initialization

            var connection = new SqlConnection();
            string dbTyper = "";

            #endregion

            try
            {
                #region Open Connection

                connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                var dataSet = new DataSet();
                connection.Open();




                #endregion

                #region Get Store Procedure and Start Processing

                var command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.CommandText = spName;
                command.CommandTimeout = 20000;


                if (nv != null)
                {
                    #region Retreiving Data

                    for (int i = 0; i < nv.Count; i++)
                    {
                        string[] arraysplit = nv.Keys[i].Split('-');

                        if (arraysplit.Length > 2)
                        {
                            #region Code For Data Type Length

                            dbTyper = "SqlDbType." + arraysplit[1].ToString() + "," + arraysplit[2].ToString();

                            // command.Parameters.AddWithValue(arraysplit[0].ToString(), dbTyper).Value = nv[i].ToString();



                            if (nv[i].ToString() == "NULL")
                            {
                                command.Parameters.AddWithValue(arraysplit[0].ToString(), dbTyper).Value = DBNull.Value;

                            }
                            else
                            {
                                command.Parameters.AddWithValue(arraysplit[0].ToString(), dbTyper).Value = nv[i].ToString();

                            }


                            #endregion
                        }
                        else
                        {
                            #region Code For Int Values
                            dbTyper = "SqlDbType." + arraysplit[1].ToString();
                            // command.Parameters.AddWithValue(arraysplit[0].ToString(), dbTyper).Value = nv[i].ToString();

                            if (nv[i].ToString() == "NULL")
                            {
                                command.Parameters.AddWithValue(arraysplit[0].ToString(), dbTyper).Value = DBNull.Value;

                            }
                            else
                            {
                                command.Parameters.AddWithValue(arraysplit[0].ToString(), dbTyper).Value = nv[i].ToString();

                            }


                            #endregion
                        }
                    }

                    #endregion
                }

                #endregion

                #region Return DataSet

                var dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = command;
                dataAdapter.Fill(dataSet);

                return dataSet;

                #endregion
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine(exception.Message);
                return null;
            }
            finally
            {
                #region Close Connection

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

                #endregion
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}