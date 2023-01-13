using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace PomeSurveyApplication.Dashboard
{
    /// <summary>
    /// Summary description for DashboardService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DashboardService : System.Web.Services.WebService
    {

        #region Private Members
        private static string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        SqlConnection _connection;
        string dbTyper = "";
        DataSet ds;
        SqlCommand command;
        NameValueCollection _nvCollection = new NameValueCollection();
        #endregion

        [WebMethod(EnableSession = true)]
        public string GetTotalPatientWithFilter(string day,string month, string year, string validate, string HospitalID,string AgeID) 
        {
            var returnstring = string.Empty;
            try
            {
                string id = Session["UserID"].ToString();
                var dt = new DataTable();
                if ((HospitalID != "-1") || (AgeID != "-1"))
                {
                    _nvCollection.Clear();
                    _nvCollection.Add("@day-int", day);
                    _nvCollection.Add("@month-int", month);
                    _nvCollection.Add("@year-int", year);
                    _nvCollection.Add("@validate-int", validate);
                    _nvCollection.Add("@ageid-int", AgeID);
                    _nvCollection.Add("@hospitalid-int", HospitalID);
                    dt = GetData("sp_countOfPatienthospitalwise", _nvCollection).Tables[0];
                }
                else
                {
                    _nvCollection.Clear();
                    _nvCollection.Add("@day-int", day);
                    _nvCollection.Add("@month-int", month);
                    _nvCollection.Add("@year-int", year);
                    _nvCollection.Add("@validate-int", validate);
                    _nvCollection.Add("@userid-int", id);
                    _nvCollection.Add("@ageid-int", AgeID);
                    dt = GetData("sp_countOfPatient", _nvCollection).Tables[0];
                }
               
                if (dt != null)
                {
                    returnstring = ToJsonString(dt);
                }
                else
                {
                    returnstring = "No";
                }
            }
            catch (Exception ex)
            {
                returnstring = ex.Message;
            }
            return returnstring;
        } //ok day month wise

        [WebMethod(EnableSession = true)]
        public string GetTotalPatientWithTrimesterFilter(string day,string month, string year, string validate,string HospitalID,string AgeID)
        {
            var returnstring = string.Empty;
            try
            {
                string id = Session["UserID"].ToString();
                var dt = new DataTable();
                if ((HospitalID != "-1") || (AgeID != "-1"))
                {
                    _nvCollection.Clear();
                    _nvCollection.Add("@day-int", day);
                    _nvCollection.Add("@month-int", month);
                    _nvCollection.Add("@year-int", year);
                    _nvCollection.Add("@HospitalID-int", HospitalID);
                    _nvCollection.Add("@validate-varchar(50)", validate);
                    _nvCollection.Add("@ageid-int", AgeID);
                    dt = GetData("sp_countOfTrimesterHospitalWise", _nvCollection).Tables[0];
                }
                else
                {
                    _nvCollection.Clear();
                    _nvCollection.Add("@day-int", day);
                    _nvCollection.Add("@month-int", month);
                    _nvCollection.Add("@year-int", year);
                    _nvCollection.Add("@userid-int", id);
                    _nvCollection.Add("@validate-varchar(50)", validate);
                    _nvCollection.Add("@ageid-int", AgeID);
                    dt = GetData("sp_countOfTrimester1to3", _nvCollection).Tables[0]; //it will get the record of every trimester
                }
                
           
                if (dt != null)
                {
                    returnstring = ToJsonString(dt);
                }
                else
                {
                    returnstring = "No";
                }
            }
            catch (Exception ex)
            {
                returnstring = ex.Message;
            }
            return returnstring;
        } //ok day month wise
        
        [WebMethod(EnableSession = true)]
        public string GetDataOnPregnentPatientClick(string day,string month,string year,string validate, string HospitalID, string AgeID) 
        {
            var returnstring = string.Empty;
            try
            {
                string id = Session["UserID"].ToString();
                var dt = new DataTable();
                if ((HospitalID != "-1") || (AgeID != "-1"))
                {
                    _nvCollection.Clear();
                    _nvCollection.Add("@day-int", day);
                    _nvCollection.Add("@month-int", month);
                    _nvCollection.Add("@year-int", year);
                    _nvCollection.Add("@validate-varchar(10)", validate);
                    _nvCollection.Add("@hospitalid-int", HospitalID);
                    _nvCollection.Add("@ageid-int", AgeID);
                    dt = GetData("sp_pregnentpatientdatahospitalwise", _nvCollection).Tables[0];
                   
                }
                else
                {
                    _nvCollection.Clear();
                    _nvCollection.Add("@day-int", day);
                    _nvCollection.Add("@month-int", month);
                    _nvCollection.Add("@year-int", year);
                    _nvCollection.Add("@validate-varchar(10)", validate);
                    _nvCollection.Add("@userid-int", id);
                    _nvCollection.Add("@ageid-int", AgeID);
                    dt = GetData("sp_PregnentPatientsData", _nvCollection).Tables[0];
                    
                }

                if (dt != null)
                {
                    returnstring = ToJsonString(dt);
                }
                else
                {
                    returnstring = "No";
                }
            }
            catch (Exception ex)
            {
                returnstring = ex.Message;
            }
            return returnstring;
        } //ok day month wise

        [WebMethod(EnableSession = true)]
        public string TrimesterwiseData(string day,string month, string year, string validate,string HospitalID,string AgeID)
        {
            var returnstring = string.Empty;
            try
            {
                string id = Session["UserID"].ToString();
                var dt = new DataTable();
                if ((HospitalID != "-1") || (AgeID != "-1"))
                {
                    _nvCollection.Clear();
                    _nvCollection.Add("@day-int", day);
                    _nvCollection.Add("@month-int", month);
                    _nvCollection.Add("@year-int", year);
                    _nvCollection.Add("@validate-varchar(50)", validate);
                    _nvCollection.Add("@hospitalID-int", id);
                    _nvCollection.Add("@ageid-int", AgeID);
                    dt = GetData("sp_TrimisterWiseDataHospitalwise", _nvCollection).Tables[0];
                }
                else
	            {
                    _nvCollection.Clear();
                    _nvCollection.Add("@day-int", day);
                    _nvCollection.Add("@month-int", month);
                    _nvCollection.Add("@year-int", year);
                    _nvCollection.Add("@validate-varchar(50)", validate);
                    _nvCollection.Add("@userid-int", id);
                    _nvCollection.Add("@ageid-int", AgeID);
                    dt = GetData("sp_TrimisterWiseData1to3", _nvCollection).Tables[0]; // it will get the record of every trimister
	            }
                
                if (dt != null)
                {
                    returnstring = ToJsonString(dt);
                }
                else
                {
                    returnstring = "No";
                }
            }
            catch (Exception ex)
            {
                returnstring = ex.Message;
            }
            return returnstring;
        } //ok day month wise

        [WebMethod(EnableSession = true)]
        public string GetPatientCount(string day,string month, string year, string HospitalID, string AgeID)
        {
            string returnstring = string.Empty;
            try
            {
                string id = Session["UserID"].ToString();
                var dt = new DataTable();
                if ((HospitalID != "-1")||  ( AgeID != "-1"))
                {
                    _nvCollection.Clear();
                    _nvCollection.Add("@day-int", day);
                    _nvCollection.Add("@month-int", month);
                    _nvCollection.Add("@year-int", year);
                    _nvCollection.Add("@hospitalid-int", HospitalID);
                    _nvCollection.Add("@ageid-int", AgeID);
                    dt = GetData("sp_getPatientCountOfHospital", _nvCollection).Tables[0];
                }
                else
                {
                    _nvCollection.Clear();
                    _nvCollection.Add("@userid-int", id);
                    _nvCollection.Add("@day-int", day);
                    _nvCollection.Add("@month-int", month);
                    _nvCollection.Add("@year-int", year);
                    dt = GetData("sp_GetPatientCount", _nvCollection).Tables[0];
                }
                if (dt != null)
                {
                    returnstring = ToJsonString(dt);
                }
                else
                {
                    returnstring = "No";
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return returnstring;
        } //ok day month wise

        [WebMethod(EnableSession = true)]
        public string GetTotalPatientsByQuestion(string day,string month,string year, string HospitalID,string AgeID)
        {
            string returnstring = string.Empty;
            try
            {
                string id = Session["UserID"].ToString();
                var dt = new DataTable();
                if (AgeID != "-1") 
                {
                    _nvCollection.Clear();
                    _nvCollection.Add("@day-int", day);
                    _nvCollection.Add("@month-int", month);
                    _nvCollection.Add("@year-int", year);
                    _nvCollection.Add("@userid-int", id); 
                    _nvCollection.Add("@ageid-int", AgeID);
                    _nvCollection.Add("@hospital-int", HospitalID);
                    dt = GetData("sp_GetDashboardDataAgeWise", _nvCollection).Tables[0];
                }
                else if (HospitalID != "-1")
                {
                    _nvCollection.Clear();
                    _nvCollection.Add("@day-int", day);
                    _nvCollection.Add("@month-int", month);
                    _nvCollection.Add("@year-int", year);
                    _nvCollection.Add("@ageid-int", AgeID);
                    _nvCollection.Add("@hospitalid-int", HospitalID);
                    dt = GetData("sp_GetDataHospitalWise", _nvCollection).Tables[0];
                }
                else
                {
                    _nvCollection.Clear();
                    _nvCollection.Add("@day-int", day);
                    _nvCollection.Add("@month-int", month);
                    _nvCollection.Add("@year-int", year);
                    _nvCollection.Add("@userid-int", id);
                    dt = GetData("sp_GetDashboardData", _nvCollection).Tables[0];
                }

                if (dt != null)
                {
                    returnstring = ToJsonString(dt);
                }
                else
                {
                    returnstring = "No";
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return returnstring;
        } //ok day month wise

        [WebMethod(EnableSession = true)]
        public string GetDataOnCommonVisitClick(string day,string month, string year, string validate, string HospitalID,string AgeID) 
        {
            string returnstring = string.Empty;
            try
            {
                string id = Session["UserID"].ToString();
                _nvCollection.Clear();
                var dt = new DataTable();
                if ((HospitalID != "-1") || (AgeID != "-1"))
                {
                    _nvCollection.Add("@day-int", day);
                    _nvCollection.Add("@month-int", month);
                    _nvCollection.Add("@year-int", year);
                    _nvCollection.Add("@validate-int", validate);
                    _nvCollection.Add("@hospitalid-int", HospitalID);
                    _nvCollection.Add("@ageid-int", AgeID);
                    dt = GetData("sp_commonreasonvisitHospitalWise", _nvCollection).Tables[0];
                }
                else
                {
                    _nvCollection.Add("@day-int", day);
                    _nvCollection.Add("@month-int", month);
                    _nvCollection.Add("@year-int", year);
                    _nvCollection.Add("@validate-int", validate);
                    _nvCollection.Add("@hospitalid-int", HospitalID);
                    _nvCollection.Add("@userid-int", id);
                    _nvCollection.Add("@ageid-int", AgeID);
                    dt = GetData("sp_commonreasonvisit", _nvCollection).Tables[0];
                }
                
                if (dt != null)
                {
                    returnstring = ToJsonString(dt);
                }
                else
                {
                    returnstring = "No";
                }
            }
            catch (Exception ex)
            {
                returnstring = ex.Message;
            }
            return returnstring;
        } //ok day month wise
        [WebMethod(EnableSession = true)]
        public string GetReportData(string day, string month, string year, string HospitalID, string Representator, string heartburn, string PregnentPatient) //ok day month wise
        {
            string returnstring = string.Empty;
            try
            {
                _nvCollection.Clear();
                var dt = new DataTable();

                    _nvCollection.Add("@day-int", day);
                    _nvCollection.Add("@month-int", month);
                    _nvCollection.Add("@year-int", year);
                    _nvCollection.Add("@hospitalid-int", HospitalID);
                    _nvCollection.Add("@pharmacistid-int", Representator);
                    _nvCollection.Add("@ispregnent-int", PregnentPatient);
                    _nvCollection.Add("@isheartburn-varchar(40)", heartburn);

                    dt = GetData("sp_PatientReport", _nvCollection).Tables[0];
           

                if (dt != null)
                {
                    returnstring = ToJsonString(dt);
                }
                else
                {
                    returnstring = "No";
                }
            }
            catch (Exception ex)
            {
                returnstring = ex.Message;
            }
            return returnstring;
        } //ok day month wise
        public string ToJsonString(DataTable dt)
        {
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            var rows = new List<Dictionary<string, string>>();
            foreach (DataRow dr in dt.Rows)
            {
                var row = dt.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col].ToString());
                rows.Add(row);
            }

            var builder = new StringBuilder();
            serializer.Serialize(rows, builder);
            return builder.ToString();
        }
        private DataSet GetData(String SpName, NameValueCollection NV)
        {
            var connection = new SqlConnection();
            string dbTyper = "";

            try
            {
                connection.ConnectionString = _connectionString;
                var dataSet = new DataSet();
                connection.Open();

                var command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.CommandText = SpName;
                command.CommandTimeout = 20000;

                if (NV != null)
                {
                    //New code implemented for retrieving data
                    for (int i = 0; i < NV.Count; i++)
                    {
                        string[] arraySplit = NV.Keys[i].Split('-');

                        if (arraySplit.Length > 2)
                        {
                            dbTyper = "SqlDbType." + arraySplit[1].ToString() + "," + arraySplit[2].ToString();

                            if (NV[i].ToString() == "NULL")
                            {
                                command.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = DBNull.Value;
                            }
                            else
                            {
                                command.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = NV[i].ToString();
                            }
                        }
                        else
                        {
                            dbTyper = "SqlDbType." + arraySplit[1].ToString();

                            if (NV[i].ToString() == "NULL")
                            {
                                command.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = DBNull.Value;
                            }
                            else
                            {
                                command.Parameters.AddWithValue(arraySplit[0].ToString(), dbTyper).Value = NV[i].ToString();
                            }
                        }
                    }
                }

                var dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = command;
                dataAdapter.Fill(dataSet);

                return dataSet;
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine(exception.Message);
                //Constants.ErrorLog("Exception Raising From DAL In NewReport.aspx | " + exception.Message + " | Stack Trace : |" + exception.StackTrace + "|| Procedure Name :" + SpName);

                return null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

    }
}

