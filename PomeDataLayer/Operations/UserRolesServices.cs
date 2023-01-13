using PomeDataLayer.ExtraClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;
using System.Net;

namespace PomeDataLayer.Operations
{
    public class UserRolesServices
    {
        PomeDataContext db = new PomeDataContext();
        JavaScriptSerializer js = new JavaScriptSerializer();

        public CoreClass GetAllRoles() 
        {
            var returnvalue = new CoreClass();
            try
            {
                var data = db.UserRoles.Select(x => new {x.CreateDate,
                                                        x.IsActive,
                                                        x.RoleID,
                                                        x.RoleName,
                                                        x.System}).Where(p => p.RoleID != 4).Where(p => p.RoleID != 5).ToList();
                if (data.Count > 0)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Your Request Is Successfull";
                    returnvalue.Data = js.Serialize(data);
                }
                else {
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
        public CoreClass GetRolesByID(int id) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var data = db.UserRoles.Select(x => new
                {
                    x.CreateDate,
                    x.IsActive,
                    x.RoleID,
                    x.RoleName,
                    x.System
                }).Where(p => p.RoleID == id).FirstOrDefault();
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
        public CoreClass DeleteRole(int id) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var validateid = db.UserRoles.Find(id);
                if (validateid != null)
                {
                    db.UserRoles.Remove(validateid);
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
        public CoreClass InsertRole(Stream responseStream)
        {
            var returnvalue = new CoreClass();
            try
            {
                var poststream = new StreamReader(responseStream).ReadToEnd();
                var data = js.Deserialize<UserRole>(poststream);
                if (data != null)
                {
                    data.CreateDate = DateTime.Now;
                    db.UserRoles.Add(data);
                    db.SaveChanges();
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Data Inserted Seccessfully !";
                    returnvalue.NewRecordID = data.RoleID.ToString();
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
                returnvalue.Message = ex.Message;
                returnvalue.Data = null;
            }
            return returnvalue;
        }
        public CoreClass UpdateRole(Stream ResponseStream) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var poststream = new StreamReader(ResponseStream).ReadToEnd();
                var data = js.Deserialize<UserRole>(poststream);

                var values = db.UserRoles.FirstOrDefault(x => x.RoleID == data.RoleID);
                if (values != null)
                {
                    values.UpdateDate = DateTime.Now;
                    values.System = data.System;
                    values.RoleName = data.RoleName;
                    values.IsActive = data.IsActive;
                    db.SaveChanges();
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Data Updated Successfully !";
                    returnvalue.NewRecordID = data.RoleID.ToString();
                    returnvalue.Data = js.Serialize(data);
                }
                else 
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = "No Data Found To Update !";
                    returnvalue.NewRecordID = data.RoleID.ToString();
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