using PomeDataLayer.ExtraClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Net;

namespace PomeDataLayer.AppOperations
{
    public class AppServices
    {
        private PomeDataContext db = new PomeDataContext();
        private JavaScriptSerializer js = new JavaScriptSerializer();

        public CoreClass AppLogin(string username ,string password) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var data = db.HospitalPharmicists.Where(s => s.ContactNumber == username && s.Password == password).FirstOrDefault();

                if (data != null)
                {
                    if (data.IsActive == Convert.ToBoolean(1))
                    {
                        var QuestionData = db.Questions.Select(x => new { x.ID,Question = x.Question1, x.QuestionOrder, x.QuestionType, x.IsActive, x.System }).Where(s => s.IsActive == true).ToList();
                        var OptionData = db.Options.Select(x => new { x.ID, x.IsActive, x.IsInput, x.PAnswerID, x.QuestionID, x.Answer }).Where(s => s.IsActive == true).ToList();
                        var QuestionAnswerData = db.QuestionAnswerMasters.Select(x => new { x.ID, x.IsActive, x.PAnswerID, x.QuestionID }).Where(s => s.IsActive == true).ToList();
                        var AgeData = db.AgeFilters.Select(x => new { x.ID, x.Range }).ToList();
                        returnvalue.Message = "Success";
                        returnvalue.Status = HttpStatusCode.OK;
                        returnvalue.Data = js.Serialize(new {data.ID,data.HospitalID,data.Email,data.Name,QuestionData,OptionData,QuestionAnswerData,AgeData});
                     }
                    else
                    {
                        returnvalue.Message = "Not Active";
                        returnvalue.Status = HttpStatusCode.BadRequest;
                        returnvalue.Data = null;
                    }
                   
                }
                else
                {
                    returnvalue.Message = "Invalid";
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Data = null;
                }
            }
            catch (Exception ex)
            {
                returnvalue.Message = "Error";
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Data = null;
            }
            return returnvalue;
        }

        
    }
}