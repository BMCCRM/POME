using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using PomeDataLayer.ExtraClasses;
using System.Net;
using System.IO;

namespace PomeDataLayer.Operations
{
    public class QuestionServices
    {
        private JavaScriptSerializer js = new JavaScriptSerializer();
        private PomeDataContext db = new PomeDataContext();

        public CoreClass GetALLQuestions() 
        {
            var returnvalue = new CoreClass();
            try
            {
                var data = db.Questions.Select(x => new { x.ID, x.IsActive, x.Question1, x.QuestionOrder, x.QuestionType, x.System }).ToList();

                if (data.Count > 0)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Your Request IS Successfull";
                    returnvalue.Data = js.Serialize(data);
                }
                else
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "No Data Found !!";
                    returnvalue.Data = null;
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = string.Format(ex.Message);
                returnvalue.Data = null;
            }
            return returnvalue;
        }
        public CoreClass GetQuestionOptionsByquestionID(int questionid) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var data = db.Options.Where(x => x.QuestionID == questionid).ToList();
                if (data.Count > 0)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Your Request IS Successfull";
                    returnvalue.Data = js.Serialize(data);
                }
                else 
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "No Data Found !!";
                    returnvalue.Data = null;
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = string.Format(ex.Message);
                returnvalue.Data = null;
            }
            return returnvalue;
        }
        public CoreClass InsertQuestion(Stream responseStream) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var poststream = new StreamReader(responseStream).ReadToEnd();
                var data = js.Deserialize<Question>(poststream);

                if (data != null)
                {
                    data.CreateDateTime = DateTime.Now;
                    db.Questions.Add(data);
                    db.SaveChanges();
                    var NewID = data.ID;
                    returnvalue.NewRecordID = NewID.ToString();
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = string.Format("Data Inserted Successfully");
                    returnvalue.Data = js.Serialize(data);
                }
                else
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = "Please Insert Correct Data";
                    returnvalue.NewRecordID = null;
                    returnvalue.Data = js.Serialize(data);
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = string.Format(ex.Message);
                returnvalue.Data = null;
            }
            return returnvalue;
        }
        public CoreClass InsertQuestionOptions(Stream responseStream)
        {
            var returnvalue = new CoreClass();
            try
            {
                var poststream = new StreamReader(responseStream).ReadToEnd();
                var data = js.Deserialize<Option>(poststream);
                if (data != null)
                {
                    db.Options.Add(data);
                    db.SaveChanges();
                    returnvalue.NewRecordID = data.ID.ToString();
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = string.Format("Data Inserted Successfully");
                    returnvalue.Data = js.Serialize(data);
                }
                else 
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = string.Format("Please Insert Correct Data");
                    returnvalue.Data = js.Serialize(data);
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = string.Format(ex.Message);
                returnvalue.Data = null;
            }
            return returnvalue;
        }
        public CoreClass GetSurveyNames() 
        {
            var returnvalue = new CoreClass();
            try
            {
                var data = db.Surveys.Select(x => new { x.SurveyName, x.ID, x.IsActive, x.SurveyDateTime, x.System, x.UpdateDate }).ToList();
                if (data.Count > 0)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Your Request IS Successfull";
                    returnvalue.Data = js.Serialize(data);
                }
                else
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "No Data Found !!";
                    returnvalue.Data = null;
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = string.Format(ex.Message);
                returnvalue.Data = null;
            }
            return returnvalue;
        }
        public CoreClass DeleteQuestion(int id) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var validateid = db.Questions.Find(id);
                if (validateid != null)
                {
                    var validatequestion = db.Options.Where(x => x.QuestionID == id).FirstOrDefault();
                    if (validatequestion == null)
                    {
                        db.Questions.Remove(validateid);
                        db.SaveChanges();
                        returnvalue.Status = HttpStatusCode.OK;
                        returnvalue.Message = "Data Successfully Deleted !";
                    }
                    else
                    {
                        returnvalue.Status = HttpStatusCode.BadRequest;
                        returnvalue.Message = "Please Delete Options First !";
                    }
                }
                else 
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = "No Data Found !";
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = ex.Message.ToString();
            }
            return returnvalue;
        }
        public CoreClass LinkedQuestionInsert(Stream responseStream) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var poststream = new StreamReader(responseStream).ReadToEnd();
                var data = js.Deserialize<QuestionAnswerMaster>(poststream);
                if (data != null)
                {
                    var validatedata = db.QuestionAnswerMasters.Where(s => s.PAnswerID == data.PAnswerID && s.QuestionID == data.QuestionID).FirstOrDefault();
                    if (validatedata == null)
                    {
                        db.QuestionAnswerMasters.Add(data);
                        db.SaveChanges();
                        returnvalue.NewRecordID = data.ID.ToString();
                        returnvalue.Status = HttpStatusCode.OK;
                        returnvalue.Message = string.Format("Data Inserted Successfully");
                        returnvalue.Data = js.Serialize(data);
                    }
                    else
                    {
                        returnvalue.Status = HttpStatusCode.OK;
                        returnvalue.Message = string.Format("Question Already Linked With this Option");
                        returnvalue.Data = js.Serialize(data);
                    }
                }
                else 
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = string.Format("Please Insert Correct Data");
                    returnvalue.Data = js.Serialize(data);
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = string.Format(ex.Message);
                returnvalue.Data = null;
            }
            return returnvalue;
                    
        }
        public CoreClass GetQuestionsByOrderList() 
        {
            var returnvalue = new CoreClass();
            try
            {
                var data = db.Questions.Select(x => new { x.ID, x.Question1, x.QuestionOrder, x.IsActive }).Where(s => s.QuestionOrder == 0 && s.IsActive == true).ToList();
                if (data.Count > 0)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Your Request IS Successfull";
                    returnvalue.Data = js.Serialize(data);
                }
                else
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "No Data Found !!";
                    returnvalue.Data = null;
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = string.Format(ex.Message);
                returnvalue.Data = null;
            }
            return returnvalue;
        }
        public CoreClass UpdateQuestion(Stream responseStream) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var poststream = new StreamReader(responseStream).ReadToEnd();
                var data = js.Deserialize<Question>(poststream);
                if (data != null)
                {
                    var validateid = db.Questions.Where(s => s.ID == data.ID).FirstOrDefault();
                    if (validateid != null)
                    {
                        validateid.Question1 = data.Question1;
                        validateid.QuestionOrder = data.QuestionOrder;
                        validateid.QuestionType = data.QuestionType;
                        validateid.IsActive = data.IsActive;
                        validateid.IsReportingField = data.IsReportingField;
                        db.SaveChanges();
                        returnvalue.Status = HttpStatusCode.OK;
                        returnvalue.Message = string.Format("Data Updated Successfully");
                        returnvalue.Data = js.Serialize(data);
                    }
                    else
                    {
                        returnvalue.Status = HttpStatusCode.BadRequest;
                        returnvalue.Message = string.Format("No Record Found To Update");
                        returnvalue.Data = js.Serialize(data);
                    }
                }
                else 
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = string.Format("Please Insert Correct Data");
                    returnvalue.Data = js.Serialize(data);
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = string.Format(ex.Message);
                returnvalue.Data = null;
            }
            return returnvalue;
        }
        public CoreClass UpdateOptions(Stream responseStream) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var poststream = new StreamReader(responseStream).ReadToEnd();
                var data = js.Deserialize<Option>(poststream);
                if (data != null)
                {
                    var validateid = db.Options.Where(s => s.ID == data.ID).FirstOrDefault();
                    if (validateid != null)
                    {
                        validateid.IsActive = data.IsActive;
                        validateid.IsInput = data.IsInput;
                        validateid.PAnswerID = data.PAnswerID;
                        validateid.Answer = data.Answer;
                        validateid.QuestionID = data.QuestionID;
                        db.SaveChanges();
                        returnvalue.Status = HttpStatusCode.OK;
                        returnvalue.Message = string.Format("Data Updated Successfully");
                        returnvalue.Data = js.Serialize(data);
                    }
                    else
                    {
                        returnvalue.Status = HttpStatusCode.BadRequest;
                        returnvalue.Message = string.Format("No Record Found To Update");
                        returnvalue.Data = js.Serialize(data);
                    }
                }
                else
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = string.Format("Please Insert Correct Data");
                    returnvalue.Data = js.Serialize(data);
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = string.Format(ex.Message);
                returnvalue.Data = null;
            }
            return returnvalue;
        }
        public CoreClass GetOptionsByID(int optionid, int questionid)
        {
            var returnvalue = new CoreClass();
            try
            {
                var data = db.Options.Where(s => s.ID == optionid && s.QuestionID == questionid).FirstOrDefault();
                if (data != null)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Your Request IS Successfull";
                    returnvalue.Data = js.Serialize(data);
                }
                else 
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "No Data Found !!";
                    returnvalue.Data = null;
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = string.Format(ex.Message);
                returnvalue.Data = null;
            }
            return returnvalue;
        }
        public CoreClass GetQuestionByID(int questionid) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var data = db.Questions.Where(s => s.ID == questionid).FirstOrDefault();
                if (data != null)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Your Request IS Successfull";
                    returnvalue.Data = js.Serialize(data);
                }
                else
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "No Data Found !!";
                    returnvalue.Data = null;
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = string.Format(ex.Message);
                returnvalue.Data = null;
            }
            return returnvalue;
        }
        public CoreClass DeleteOptions(int id) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var validateid = db.Options.Where(s => s.ID == id).FirstOrDefault();
                if (validateid != null)
                {
                   
                    db.Options.Remove(validateid);
                    db.SaveChanges();
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Data Successfully Deleted !";
                }
                else
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = "No Data Found !";
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = ex.Message.ToString();
            }
            return returnvalue;
        }
        
    }
}