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
    public class AgeFilterServices
    {
        private JavaScriptSerializer js = new JavaScriptSerializer();
        private PomeDataContext db = new PomeDataContext();

        public CoreClass GetFilters()
        {
            var returnvalue = new CoreClass();
            try
            {
                var data = db.AgeFilters.Select(x => new { x.ID, x.Range }).ToList();
                if (data.Count > 0)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Your Request Is Successfull";
                    returnvalue.Data = js.Serialize(data);
                }
                else
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "No Data Found !";
                    returnvalue.Data = null;
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = ex.Message;
            }
            return returnvalue;
        }

        public CoreClass GetFiltersWithID(int id) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var data = db.AgeFilters.Select(x => new { x.ID, x.Range }).Where(s => s.ID == id).FirstOrDefault();
                if (data != null)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Your Request is Successfull";
                    returnvalue.Data = js.Serialize(data);
                }
                else
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = "No Data Found !!";
                    returnvalue.Data = null;
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = ex.Message;
                returnvalue.Data = null;
            }
            return returnvalue;
        }

        public CoreClass DeleteAgeFilter(int id) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var validateid = db.AgeFilters.Find(id);
                if (validateid != null)
                {
                    db.AgeFilters.Remove(validateid);
                    db.SaveChanges();
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Data Deleted Successfully";
                }
                else
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = "No Data Found To Delete !";
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = ex.Message;
            }
            return returnvalue;
        }

        public CoreClass InsertAgeFilter(Stream responseStream) 
        {
            var returnvalue  = new CoreClass();
            try
            {
                var poststream = new StreamReader(responseStream).ReadToEnd();
                var data = js.Deserialize<AgeFilter>(poststream);
                if (data != null)
                {
                    db.AgeFilters.Add(data);
                    db.SaveChanges();
                    if (data.ID != null)
                    {
                        returnvalue.Status = HttpStatusCode.OK;
                        returnvalue.Message = "Data Inserted Seccessfully !";
                        returnvalue.NewRecordID = data.ID.ToString();
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
                returnvalue.Message = ex.Message;
                returnvalue.Data = null;
            }
            return returnvalue;
        }

        public CoreClass UpdateAgeFilter(Stream responseStream) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var poststream = new StreamReader(responseStream).ReadToEnd();
                var data = js.Deserialize<AgeFilter>(poststream);
                var values = db.AgeFilters.FirstOrDefault(x => x.ID == data.ID);

                if (values != null)
                {
                    values.Range = data.Range;
                    db.SaveChanges();
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Data Updated Successfully !";
                    returnvalue.NewRecordID = data.ID.ToString();
                    returnvalue.Data = js.Serialize(data);
                }
                else 
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = "No Data Found To Update !";
                    returnvalue.NewRecordID = data.ID.ToString();
                    returnvalue.Data = js.Serialize(data);
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = ex.Message;
                returnvalue.NewRecordID = null;
            }
            return returnvalue;
        }
    }
}