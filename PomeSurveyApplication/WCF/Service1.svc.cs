using PomeDataLayer.ExtraClasses;
using PomeDataLayer.AppOperations;
using PomeDataLayer.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using PomeDataLayer;
using System.IO.Compression;

namespace PomeSurveyApplication.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1 : IService1

    {
        private PomeDataContext db = new PomeDataContext();
        #region HospitalServices
        public CoreClass GetAllHospitalsWithPharmacistsID()
        {
            var operation = new HospitalServices();
            return operation.GetHospitalsWithPharmacistsID();
        }
        public CoreClass GetAllHospitals()
        {
            var operation = new HospitalServices();
            return operation.GetHospitals();
        }
   
        public CoreClass GetHospitalByID(string id)
        {
            var operation = new HospitalServices();
            return operation.GetHospitalByID(Convert.ToInt32(id));
        }
        public CoreClass InsertHospital(Stream ResponseStream)
        {
            var operation = new HospitalServices();
            return operation.InsertHospitals(ResponseStream);
        }
        public CoreClass UpdateHospital(Stream ResponseStream)
        {
            var operation = new HospitalServices();
            return operation.UpdateHospitals(ResponseStream);
        }

        public CoreClass DeleteHospital(string id)
        {
            var operation = new HospitalServices();
            return operation.DeleteHospital(Convert.ToInt32(id));
        }
        #endregion

        #region DoctorsServices
        public CoreClass GetAllDoctors()
        {
            var operation = new DoctorsServices();
            return operation.GetAllDoctors();
        }
        public CoreClass GetDoctorsByID(string id)
        {
            var operation = new DoctorsServices();
            return operation.GetDoctorByID(Convert.ToInt32(id));
        }
        public CoreClass GetDoctorsByHospitalID(string id)
        {
            var operation = new DoctorsServices();
            return operation.GetDoctorByHospitalID(Convert.ToInt32(id));
        }
        public CoreClass InsertDoctor(Stream ResponseStream)
        {
            var operation = new DoctorsServices();
            return operation.InsertHospitalDoctors(ResponseStream);
        }
        public CoreClass UpdateDoctor(Stream ResponseStream)
        {
            var operation = new DoctorsServices();
            return operation.UpdateHospitalDoctors(ResponseStream);
        }
        public CoreClass DeleteDoctors(string id)
        {
            var operation = new DoctorsServices();
            return operation.DeleteHospitalDoctors(Convert.ToInt32(id));
        }
        #endregion

        #region Pharmacist
        public CoreClass GetAllPharmacist()
        {
            var operation = new PharmacistServices();
            return operation.GetAllPharmacist();
        }
        public CoreClass GetPharmacistByID(string id) 
        {
            var operation = new PharmacistServices();
            return operation.GetPharmacistByID(Convert.ToInt32(id));
        }
        public CoreClass GetPharmacistByHospitalID(string id) 
        {
            var operation = new PharmacistServices();
            return operation.GetPharmacistByHospitalID(Convert.ToInt32(id));
        }
        public CoreClass DeletePharmacist(string id) 
        {
            var operation = new PharmacistServices();
            return operation.DeletePharmacist(Convert.ToInt32(id));
        }
        public CoreClass InsertPharmacist(Stream ResponseStream) 
        {
            var operation = new PharmacistServices();
            return operation.InsertPharmacistData(ResponseStream);
        }
        public CoreClass UpdatePharmacist(Stream ResponseStream) 
        {
            var operation = new PharmacistServices();
            return operation.UpdatePharmacist(ResponseStream);
        }
        #endregion

        #region UserRoles
        public CoreClass GetAllUserRoles() 
        {
            var operation = new UserRolesServices();
            return operation.GetAllRoles();
        }
        public CoreClass GetRolesByID(string id)
        {

            var operation = new UserRolesServices();
            return operation.GetRolesByID(Convert.ToInt32(id));
        }
        public CoreClass DeleteRoles(string id)
        {
            var operation = new UserRolesServices();
            return operation.DeleteRole(Convert.ToInt32(id));
        }
        public CoreClass InsertUserRoles(Stream responseStream)
        {
            var operation = new UserRolesServices();
            return operation.InsertRole(responseStream);
        }
        public CoreClass UpdateUserRoles(Stream responseStream)
        {
            var operation = new UserRolesServices();
            return operation.UpdateRole(responseStream);
        }

        #endregion

        #region Users
        public CoreClass GetAllUsers()
        {
            var operation = new UsersServices();
            return operation.GetAllUsers();
        }
        public CoreClass GetUserByID(string id)
        {
            var operation = new UsersServices();
            return operation.GetUserByID(Convert.ToInt32(id));
        }
        public CoreClass DeleteUsers(string id)
        {
            var operation = new UsersServices();
            return operation.DeleteUsers(Convert.ToInt32(id));
        }
        public CoreClass InsertUsers(Stream responseStream)
        {
            var operation = new UsersServices();
            return operation.InsertUser(responseStream);
        }
        public CoreClass UpdateUsers(Stream responseStream)
        {
            var operation = new UsersServices();
            return operation.Updateuser(responseStream);
        }
        #endregion

        #region Questions
        public CoreClass GetALLQuestions() 
        {
            var operation = new QuestionServices();
            return operation.GetALLQuestions();
        }
        public CoreClass GetOptionsbyQuestionID(string id) 
        {
            var operation = new QuestionServices();
            return operation.GetQuestionOptionsByquestionID(Convert.ToInt32(id));
        }
        public CoreClass InsertQuestion(Stream responseStream) 
        {
            var operation = new QuestionServices();
            return operation.InsertQuestion(responseStream);
        }
        public CoreClass InsertQuestionOperation(Stream responseStream)
        {
            var operation = new QuestionServices();
            return operation.InsertQuestionOptions(responseStream);
        }
        public CoreClass InsertLinkedQuestion(Stream responseStream)
        {
            var operation = new QuestionServices();
            return operation.LinkedQuestionInsert(responseStream);
        }
        public CoreClass DeleteQuestion(string id) 
        {
            var operation = new QuestionServices();
            return operation.DeleteQuestion(Convert.ToInt32(id));
        }
        public CoreClass GetQuestionByOrderList() 
        {
            var operation = new QuestionServices();
            return operation.GetQuestionsByOrderList();
        }
        public CoreClass UpdateQuestion(Stream responseStream)
        {
            var operation = new QuestionServices();
            return operation.UpdateQuestion(responseStream);
        }
        public CoreClass UpdateOption(Stream responseStream)
        {
            var operation = new QuestionServices();
            return operation.UpdateOptions(responseStream);
        }
        public CoreClass GetOptionsDetails(string optionid,string questionid)
        {
            var operation = new QuestionServices();
            return operation.GetOptionsByID(Convert.ToInt32(optionid), Convert.ToInt32(questionid));
        }
        public CoreClass GetQuestionDetails(string id)
        {
            var operation = new QuestionServices();
            return operation.GetQuestionByID(Convert.ToInt32(id));
        }
        public CoreClass DeleteOption(string id)
        {
            var operation = new QuestionServices();
            return operation.DeleteOptions(Convert.ToInt32(id));
        }
        #endregion

        #region Survey
        public CoreClass GetSurvey() 
        {
            var operation = new QuestionServices();
            return operation.GetSurveyNames();
        }
        #endregion

        #region AgeFilter
        public CoreClass GetAgeFilter() 
        {
            var operation = new AgeFilterServices();
            return operation.GetFilters();
        }
        public CoreClass GetFilterByID(string id)
        {
            var operation = new AgeFilterServices();
            return operation.GetFiltersWithID(Convert.ToInt32(id));
        }
        public CoreClass DeleteFilter(string id)
        {
            var operation = new AgeFilterServices();
            return operation.DeleteAgeFilter(Convert.ToInt32(id));
        }
        public CoreClass InsertAgeFilter(Stream responseStream)
        {
            var operation = new AgeFilterServices();
            return operation.InsertAgeFilter(responseStream);
        }
        public CoreClass UpdateAgeFilter(Stream responseStream)
        {
            var operation = new AgeFilterServices();
            return operation.UpdateAgeFilter(responseStream);
        }
        #endregion

        #region Appservices
        public CoreClass Login(string username, string password) 
        {
            var operation = new AppServices();
            return operation.AppLogin(username, password);
        }
        #endregion
     
        #region FileUpload
        public void UploadFile(string fileName, Stream stream)
            {
                try
                {
                    fileName = DateTime.Now.ToString("MMddyy_Hmmss") + "_" + fileName;
                    string FilePath = Path.Combine(HostingEnvironment.MapPath("~/Files/Uploads"), fileName);
                    
                    int length = 0;
                    using (FileStream writer = new FileStream(FilePath, FileMode.Create))
                    {
                        int readCount;
                        var buffer = new byte[8192];
                        while ((readCount = stream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            writer.Write(buffer, 0, readCount);
                            length += readCount;
                        }
                    }
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string jsondata = System.IO.File.ReadAllText(FilePath);
                    var data = js.Deserialize<List<Questioner>>(jsondata);
                    var patientsurveydata = js.Deserialize<List<Questioner>>(jsondata);
                    for (int i = 0; i < patientsurveydata.Count; i++)
                    {
                        int patientid = 0;
                        string pname = patientsurveydata[i].PatientName;
                        string pcontact = patientsurveydata[i].PatientContact;
                        string pcity = patientsurveydata[i].City;
                        int pnoofchildren = Convert.ToInt32(patientsurveydata[i].NumberOfChildren);
                        int pAge = Convert.ToInt32(patientsurveydata[i].PatientAge);
                        int pvisitnumber = Convert.ToInt32(patientsurveydata[i].VisitNumber);
                        string system = patientsurveydata[i].SystemVariable;
                        int pharmacist = Convert.ToInt32(patientsurveydata[i].PharmacistId);
                        int surveyResponseId = 0;
                        
                        var patients = (from p in db.Patients
                                        where p.GivenName == pname && 
                                        p.Contact == pcontact && 
                                        p.AgeID == pAge && 
                                        p.NoOfChildrens == pnoofchildren && 
                                        p.City == pcity && 
                                        p.VisitNumber == pvisitnumber
                                        select p).FirstOrDefault();
                        
                        if (patients != null)
                        {
                            var systemdate = Convert.ToDateTime(patientsurveydata[i].SurveyDate);
                            var validatesurvey = (from p in db.SurveyResponses
                                                  where p.PatientID == patients.ID
                                                      select p).ToList();
                            //&& p.System == system 
                            //&& p.PharmacistID == pharmacist
                            //&& p.SurveyDateTime == systemdate
                            if(validatesurvey.Count == 0)
                            {
                                patientid = patients.ID;
                            }
                            else
                            {
                                patientid = 0;
                            }
                        
                        }
                        else
                        {
                            Patient patient = new Patient();
                            patient.GivenName = patientsurveydata[i].PatientName;
                            patient.Contact = patientsurveydata[i].PatientContact;
                            patient.AgeID = Convert.ToInt32((patientsurveydata[i].PatientAge != "") ? patientsurveydata[i].PatientAge : "0");
                            patient.City = patientsurveydata[i].City;
                            patient.NoOfChildrens = Convert.ToInt32((patientsurveydata[i].NumberOfChildren != "") ? patientsurveydata[i].NumberOfChildren : "0");
                            patient.VisitNumber = Convert.ToInt32((patientsurveydata[i].VisitNumber != "") ? patientsurveydata[i].VisitNumber : "0");
                            patient.Occupation = patientsurveydata[i].Occupation;
                            db.Patients.Add(patient);
                            db.SaveChanges();

                            patientid = patient.ID;
                        }


                        if (patientid != 0)
                        {
                            SurveyResponse surveyResponse = new SurveyResponse();
                            surveyResponse.PharmacistID = Convert.ToInt32((patientsurveydata[i].PharmacistId != "") ? patientsurveydata[i].PharmacistId : "0");
                            surveyResponse.PatientID = patientid;
                            surveyResponse.Latitude = patientsurveydata[i].Latitude;
                            surveyResponse.Longitude = patientsurveydata[i].Longitude;
                            surveyResponse.System = patientsurveydata[i].SystemVariable;
                            surveyResponse.CreateDateTime = DateTime.Now;
                            surveyResponse.SurveyDateTime = Convert.ToDateTime(patientsurveydata[i].SurveyDate);
                            db.SurveyResponses.Add(surveyResponse);
                            db.SaveChanges();
                            surveyResponseId = surveyResponse.ID;

                            var surveydata = patientsurveydata[i].Data;
                            for (int j = 0; j < surveydata.Count; j++)
                            {
                                PatientResponse patientResponse = new PatientResponse();
                                patientResponse.SurveyResponseID = surveyResponseId;
                                patientResponse.QuestionID = Convert.ToInt32((surveydata[j].QuestionId != "") ? surveydata[j].QuestionId : "0");
                                patientResponse.POptionID = Convert.ToInt32((surveydata[j].PAnswerId != "") ? surveydata[j].PAnswerId : "0");
                                patientResponse.OptionID = Convert.ToInt32((surveydata[j].AnswerId != "") ? surveydata[j].AnswerId : "0");
                                patientResponse.AnswerID = Convert.ToInt32((surveydata[j].OptionId != "") ? surveydata[j].OptionId : "0");
                                patientResponse.InputText = surveydata[j].Input;
                                patientResponse.CreateDate = DateTime.Now;
                                db.PatientResponses.Add(patientResponse);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            ErrorLog("Duplicate Entry Error : " + js.Serialize(patientsurveydata[i]));
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog("Error : " + ex.Message.ToString() + "StackTrace : " + ex.StackTrace.ToString());
                }
                
            }
            
        // For Email (Note:Dont Angry ,  because old version dont have email column thats why created new methods) Shahrukh ---- 11/13/2020
        public void UploadFiles(string fileName, Stream stream)
            {
                try
                {
                    fileName = DateTime.Now.ToString("MMddyy_Hmmss") + "_" + fileName;
                    string FilePath = Path.Combine(HostingEnvironment.MapPath("~/Files/Uploads"), fileName);

                    int length = 0;
                    using (FileStream writer = new FileStream(FilePath, FileMode.Create))
                    {
                        int readCount;
                        var buffer = new byte[8192];
                        while ((readCount = stream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            writer.Write(buffer, 0, readCount);
                            length += readCount;
                        }
                    }
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string jsondata = System.IO.File.ReadAllText(FilePath);
                    var data = js.Deserialize<List<Questioners>>(jsondata);
                    var patientsurveydata = js.Deserialize<List<Questioners>>(jsondata);
                    for (int i = 0; i < patientsurveydata.Count; i++)
                    {
                        int patientid = 0;
                        string pname = patientsurveydata[i].PatientName;
                        string pcontact = patientsurveydata[i].PatientContact;
                        string pcity = patientsurveydata[i].City;
                        int pnoofchildren = Convert.ToInt32(patientsurveydata[i].NumberOfChildren);
                        int pAge = Convert.ToInt32(patientsurveydata[i].PatientAge);
                        int pvisitnumber = Convert.ToInt32(patientsurveydata[i].VisitNumber);
                        string system = patientsurveydata[i].SystemVariable;
                        string email = patientsurveydata[i].Email;
                        int pharmacist = Convert.ToInt32(patientsurveydata[i].PharmacistId);
                        int surveyResponseId = 0;

                        var patients = (from p in db.Patients
                                        where p.GivenName == pname &&
                                        p.Contact == pcontact &&
                                        p.AgeID == pAge &&
                                        p.NoOfChildrens == pnoofchildren &&
                                        p.City == pcity &&
                                        p.VisitNumber == pvisitnumber &&
                                        p.email == email
                                        select p).FirstOrDefault();

                        if (patients != null)
                        {
                            var systemdate = Convert.ToDateTime(patientsurveydata[i].SurveyDate);
                            var validatesurvey = (from p in db.SurveyResponses
                                                  where p.PatientID == patients.ID
                                                  select p).ToList();
                            //&& p.System == system 
                            //&& p.PharmacistID == pharmacist
                            //&& p.SurveyDateTime == systemdate
                            if (validatesurvey.Count == 0)
                            {
                                patientid = patients.ID;
                            }
                            else
                            {
                                patientid = 0;
                            }

                        }
                        else
                        {
                            Patient patient = new Patient();
                            patient.GivenName = patientsurveydata[i].PatientName;
                            patient.Contact = patientsurveydata[i].PatientContact;
                            patient.AgeID = Convert.ToInt32((patientsurveydata[i].PatientAge != "") ? patientsurveydata[i].PatientAge : "0");
                            patient.City = patientsurveydata[i].City;
                            patient.NoOfChildrens = Convert.ToInt32((patientsurveydata[i].NumberOfChildren != "") ? patientsurveydata[i].NumberOfChildren : "0");
                            patient.VisitNumber = Convert.ToInt32((patientsurveydata[i].VisitNumber != "") ? patientsurveydata[i].VisitNumber : "0");
                            patient.Occupation = patientsurveydata[i].Occupation;
                            patient.email = patientsurveydata[i].Email;
                            db.Patients.Add(patient);
                            db.SaveChanges();

                            patientid = patient.ID;
                        }


                        if (patientid != 0)
                        {
                            SurveyResponse surveyResponse = new SurveyResponse();
                            surveyResponse.PharmacistID = Convert.ToInt32((patientsurveydata[i].PharmacistId != "") ? patientsurveydata[i].PharmacistId : "0");
                            surveyResponse.PatientID = patientid;
                            surveyResponse.Latitude = patientsurveydata[i].Latitude;
                            surveyResponse.Longitude = patientsurveydata[i].Longitude;
                            surveyResponse.System = patientsurveydata[i].SystemVariable;
                            surveyResponse.CreateDateTime = DateTime.Now;
                            surveyResponse.SurveyDateTime = Convert.ToDateTime(patientsurveydata[i].SurveyDate);
                            db.SurveyResponses.Add(surveyResponse);
                            db.SaveChanges();
                            surveyResponseId = surveyResponse.ID;

                            var surveydata = patientsurveydata[i].Data;
                            for (int j = 0; j < surveydata.Count; j++)
                            {
                                PatientResponse patientResponse = new PatientResponse();
                                patientResponse.SurveyResponseID = surveyResponseId;
                                patientResponse.QuestionID = Convert.ToInt32((surveydata[j].QuestionId != "") ? surveydata[j].QuestionId : "0");
                                patientResponse.POptionID = Convert.ToInt32((surveydata[j].PAnswerId != "") ? surveydata[j].PAnswerId : "0");
                                patientResponse.OptionID = Convert.ToInt32((surveydata[j].AnswerId != "") ? surveydata[j].AnswerId : "0");
                                patientResponse.AnswerID = Convert.ToInt32((surveydata[j].OptionId != "") ? surveydata[j].OptionId : "0");
                                patientResponse.InputText = surveydata[j].Input;
                                patientResponse.CreateDate = DateTime.Now;
                                db.PatientResponses.Add(patientResponse);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            ErrorLog("Duplicate Entry Error : " + js.Serialize(patientsurveydata[i]));
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog("Error : " + ex.Message.ToString() + "StackTrace : " + ex.StackTrace.ToString());
                }

            }

            private void ErrorLog(string error)
            {
                try
                {
                    if (!Directory.Exists(@"C:\Pome\Logs"))
                    {
                        Directory.CreateDirectory(@"C:\Pome\Logs");
                    }

                    File.AppendAllText(@"C:\Pome\Logs\" + "PomeLog_" + DateTime.UtcNow.ToString("yyyy_MM_dd") + ".txt",
                        DateTime.Now + " : " + error + Environment.NewLine);
                }
                catch (Exception exception)
                {
                    Console.Out.WriteLine(exception.Message);
                }
            }
        

            [DataContract]
            public class Datum
            {
                [DataMember(Name = "OptionId")]
                public string OptionId { get; set; }
                [DataMember(Name = "Input")]
                public string Input { get; set; }
                [DataMember(Name = "PAnswerId")]
                public string PAnswerId { get; set; }
                [DataMember(Name = "AnswerId")]
                public string AnswerId { get; set; }
                [DataMember(Name = "QuestionId")]
                public string QuestionId { get; set; }
                [DataMember(Name = "Count")]
                public string Count { get; set; }
                [DataMember(Name = "AlertCount")]
                public string AlertCount { get; set; }
            }
            // For Email (Note:Dont Angry ,  because old version dont have email column thats why created new methods) Shahrukh ---- 11/13/2020
            [DataContract]
            public class Questioners
            {
                [DataMember(Name = "PatientContact")]
                public string PatientContact { get; set; }
                [DataMember(Name = "PatientAge")]
                public string PatientAge { get; set; }
                [DataMember(Name = "PatientName")]
                public string PatientName { get; set; }
                [DataMember(Name = "City")]
                public string City { get; set; }
                [DataMember(Name = "PharmacistId")]
                public string PharmacistId { get; set; }
                [DataMember(Name = "Data")]
                public IList<Datum> Data { get; set; }
                [DataMember(Name = "SurveyDate")]
                public string SurveyDate { get; set; }
                [DataMember(Name = "NumberOfChildren")]
                public string NumberOfChildren { get; set; }
                [DataMember(Name = "VisitNumber")]
                public string VisitNumber { get; set; }
                [DataMember(Name = "Occupation")]
                public string Occupation { get; set; }
                [DataMember(Name = "Address")]
                public string Address { get; set; }
                [DataMember(Name = "Latitude")]
                public string Latitude { get; set; }
                [DataMember(Name = "Longitude")]
                public string Longitude { get; set; }
                [DataMember(Name = "SystemVariable")]
                public string SystemVariable { get; set; }

                [DataMember(Name = "Email")]
                public string Email { get; set; }

                [DataMember(Name = "id")]
                public int id { get; set; }
            }
            [DataContract]
            public class Questioner
            {
                [DataMember(Name = "PatientContact")]
                public string PatientContact { get; set; }
                [DataMember(Name = "PatientAge")]
                public string PatientAge { get; set; }
                [DataMember(Name = "PatientName")]
                public string PatientName { get; set; }
                [DataMember(Name = "City")]
                public string City { get; set; }
                [DataMember(Name = "PharmacistId")]
                public string PharmacistId { get; set; }
                [DataMember(Name = "Data")]
                public IList<Datum> Data { get; set; }
                [DataMember(Name = "SurveyDate")]
                public string SurveyDate { get; set; }
                [DataMember(Name = "NumberOfChildren")]
                public string NumberOfChildren { get; set; }
                [DataMember(Name = "VisitNumber")]
                public string VisitNumber { get; set; }
                [DataMember(Name = "Occupation")]
                public string Occupation { get; set; }
                [DataMember(Name = "Address")]
                public string Address { get; set; }
                [DataMember(Name = "Latitude")]
                public string Latitude { get; set; }
                [DataMember(Name = "Longitude")]
                public string Longitude { get; set; }
                [DataMember(Name = "SystemVariable")]
                public string SystemVariable { get; set; }
                [DataMember(Name = "id")]
                public int id { get; set; }
            }

            public void asd(string fileName, Stream stream) 
            {
                //string textdata = "[{'PatientContact':'03435689741','PatientAge':'24','PatientName':'zohaib','City':'karachi','PharmacistId':'5','Data':[{'OptionId':'0','Input':'','PAnswerId':'0','AnswerId':'2','QuestionId':'1','Count':'1','AlertCount':'0'},{'OptionId':'3','Input':'','PAnswerId':'0','AnswerId':'21','QuestionId':'3','Count':'2','AlertCount':'1'},{'OptionId':'3','Input':'','PAnswerId':'0','AnswerId':'27','QuestionId':'4','Count':'2','AlertCount':'2'},{'OptionId':'0','Input':'','PAnswerId':'0','AnswerId':'3','QuestionId':'2','Count':'2','AlertCount':'0'},{'OptionId':'0','Input':'','PAnswerId':'0','AnswerId':'32','QuestionId':'5','Count':'3','AlertCount':'0'},{'OptionId':'0','Input':'','PAnswerId':'0','AnswerId':'41','QuestionId':'6','Count':'4','AlertCount':'0'},{'OptionId':'0','Input':'','PAnswerId':'0','AnswerId':'44','QuestionId':'7','Count':'5','AlertCount':'0'},{'OptionId':'0','Input':'','PAnswerId':'0','AnswerId':'45','QuestionId':'8','Count':'6','AlertCount':'0'},{'OptionId':'0','Input':'','PAnswerId':'0','AnswerId':'47','QuestionId':'9','Count':'7','AlertCount':'0'},{'OptionId':'0','Input':'','PAnswerId':'0','AnswerId':'49','QuestionId':'10','Count':'8','AlertCount':'0'},{'OptionId':'0','Input':'','PAnswerId':'0','AnswerId':'51','QuestionId':'11','Count':'9','AlertCount':'0'},{'OptionId':'0','Input':'','PAnswerId':'0','AnswerId':'53','QuestionId':'12','Count':'10','AlertCount':'0'},{'OptionId':'0','Input':'','PAnswerId':'0','AnswerId':'21','QuestionId':'3','Count':'11','AlertCount':'0'},{'OptionId':'0','Input':'fghjk','PAnswerId':'0','AnswerId':'59','QuestionId':'13','Count':'12','AlertCount':'0'},{'OptionId':'0','Input':'','PAnswerId':'0','AnswerId':'61','QuestionId':'14','Count':'13','AlertCount':'0'},{'OptionId':'0','Input':'','PAnswerId':'0','AnswerId':'68','QuestionId':'16','Count':'14','AlertCount':'0'},{'OptionId':'0','Input':'','PAnswerId':'0','AnswerId':'80','QuestionId':'17','Count':'15','AlertCount':'0'},{'OptionId':'0','Input':'','PAnswerId':'0','AnswerId':'91','QuestionId':'18','Count':'16','AlertCount':'0'}],'SurveyDate':'01/23/2016','NumberOfChildren':'2','VisitNumber':'2','Occupation':'','Address':'','Latitude':'0.0','Longitude':'0.0','SystemVariable':'','$id':1}]";
            
                StreamReader reader = new StreamReader(stream);
                string textdata = reader.ReadToEnd();
                



                JavaScriptSerializer js = new JavaScriptSerializer();
                

           //var data=js.DeserializeObject(textdata)<Questioner>;
                var data=js.Deserialize<List<Questioner>>(textdata);



                //string jsondata = System.IO.File.ReadAllText(FilePath);
           //var  patientsurveydata = js.Deserialize<List<PatientSurveyData>>(textdata);

                var patientsurveydata = js.Deserialize<List<Questioner>>(textdata);



                for (int i = 0; i < patientsurveydata.Count; i++)
                {
                    int patientid = 0;
                    string pname = patientsurveydata[i].PatientName;
                    string pcontact = patientsurveydata[i].PatientContact;

                    int surveyResponseId = 0;
                    var patients=(from p in db.Patients
                                  where p.GivenName == pname && p.Contact == pcontact
                                  select p).FirstOrDefault();


                    if (patients!=null)
                    {
                        patientid = patients.ID;
                    }
                    else
                    {
                        Patient patient = new Patient();
                        patient.GivenName = patientsurveydata[i].PatientName;
                        patient.Contact = patientsurveydata[i].PatientContact;
                        patient.AgeID = Convert.ToInt32((patientsurveydata[i].PatientAge!="")?patientsurveydata[i].PatientAge:"0");
                        patient.City = patientsurveydata[i].City;
                        patient.NoOfChildrens = Convert.ToInt32((patientsurveydata[i].NumberOfChildren != "") ? patientsurveydata[i].NumberOfChildren : "0");
                        patient.VisitNumber = Convert.ToInt32((patientsurveydata[i].VisitNumber != "") ? patientsurveydata[i].VisitNumber : "0");
                        patient.Occupation = patientsurveydata[i].Occupation;
                        db.Patients.Add(patient);
                        db.SaveChanges();

                        patientid = patient.ID;
                    }

                    SurveyResponse surveyResponse = new SurveyResponse();
                    surveyResponse.PharmacistID = Convert.ToInt32((patientsurveydata[i].PharmacistId != "") ? patientsurveydata[i].PharmacistId : "0");
                    surveyResponse.PatientID = patientid;
                    surveyResponse.Latitude = patientsurveydata[i].Latitude;
                    surveyResponse.Longitude = patientsurveydata[i].Longitude;
                    surveyResponse.System = patientsurveydata[i].SystemVariable;
                    surveyResponse.CreateDateTime = DateTime.Now;
                    surveyResponse.SurveyDateTime = Convert.ToDateTime(patientsurveydata[i].SurveyDate);
                    db.SurveyResponses.Add(surveyResponse);
                    db.SaveChanges();
                    surveyResponseId=surveyResponse.ID;

                    var surveydata = patientsurveydata[i].Data;


                    for (int j = 0; j < surveydata.Count; j++)
                    {
                        PatientResponse patientResponse = new PatientResponse();
                        patientResponse.SurveyResponseID = surveyResponseId;
                        patientResponse.QuestionID = Convert.ToInt32((surveydata[j].QuestionId != "") ? surveydata[j].QuestionId : "0");
                        patientResponse.POptionID = Convert.ToInt32((surveydata[j].PAnswerId != "") ? surveydata[j].PAnswerId : "0");
                        patientResponse.OptionID = Convert.ToInt32((surveydata[j].AnswerId != "") ? surveydata[j].AnswerId : "0");
                        patientResponse.AnswerID = Convert.ToInt32((surveydata[j].OptionId != "") ? surveydata[j].OptionId : "0");
                        patientResponse.InputText = surveydata[j].Input;
                        patientResponse.CreateDate = DateTime.Now;
                        db.PatientResponses.Add(patientResponse);
                        db.SaveChanges();

                    }
                }
                
            
            }
        
        #endregion   
    }
}

public class PatientSurveyData 
{
    public string PatientContact { get; set; }
    public string PatientAge { get; set; }
    public string PatientName { get; set; }
    public string City { get; set; }
    public string PharmacistId { get; set; }
    public string Data { get; set; }
    public string SurveyDate { get; set; }
    public string NumberOfChildren { get; set; }
    public string VisitNumber { get; set; }
    public string Occupation { get; set; }
    public string Address { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string SystemVariable { get; set; }

}

public class SurveyData
{
    public string OptionId { get; set; }
    public string Input { get; set; }
    public string PAnswerId { get; set; }
    public string AnswerId { get; set; }
    public string QuestionId { get; set; }
}
