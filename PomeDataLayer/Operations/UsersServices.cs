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
    public class UsersServices
    {
        private PomeDataContext db = new PomeDataContext();
        JavaScriptSerializer js = new JavaScriptSerializer();
        public CoreClass GetAllUsers() 
        {
            var returnvalue = new CoreClass();
            try
            {
                var data = db.Users.GroupJoin(db.UserRoles,
                                         u => u.RoleID,
                                         r => r.RoleID,
                                         (user, rolee) => new { user, rolee }).SelectMany(z => z.rolee.DefaultIfEmpty(),
                                         (x, b) => new { x.user.Address,
                                                         x.user.CNIC, 
                                                         x.user.ContactNumber, 
                                                         x.user.Email,
                                                         x.user.UserName,
                                                         x.user.UserID,
                                                         x.user.CreateDate, 
                                                         x.user.IsActive, 
                                                         x.user.RoleID,
                                                         x.user.System,
                                                         x.user.Password,
                                                         RoleName = (b == null) ? "--" : b.RoleName}).Where(R => R.RoleID  != 4).Where(R => R.RoleID != 5).ToList();
               
                if (data.Count > 0)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Your Request Is Successfull";
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
                returnvalue.Message = ex.Message;
            }
            return returnvalue;
        }
        public CoreClass GetUserByID(int id)
        {
            var returnvalue = new CoreClass();
            try 
	        {
                var data = db.Users.Select(x => new
                {
                    x.Address,
                    x.CNIC,
                    x.ContactNumber,
                    x.Email,
                    x.UserName,
                    x.UserID,
                    x.CreateDate,
                    x.IsActive,
                    x.RoleID,
                    x.System,
                    x.Password
                }).Where(p => p.UserID == id).FirstOrDefault();
                if (data != null)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Your Request Is Successfull"; ;
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
        public CoreClass DeleteUsers(int id)
        {
            var returnvalue = new CoreClass();
            try
            {
                var data = db.Users.Find(id);
                if (data != null)
                {
                    db.Users.Remove(db.Users.First(x => x.UserID == id));
                    db.SaveChanges();
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Data Deleted Successfully !!";
                }
                else 
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = "No Data Found To Delete At ID = " + id.ToString();
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = ex.Message;
            }
            return returnvalue;
        }
        public CoreClass InsertUser(Stream ResponseStream)
        {
            var returnvalue = new CoreClass();
            try
            {
                var poststream = new StreamReader(ResponseStream).ReadToEnd();
                var data = js.Deserialize<User>(poststream);

                if (data != null)
                {
                    data.CreateDate = DateTime.Now;
                    db.Users.Add(data);
                    db.SaveChanges();
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Data Inserted Successfully !";
                    returnvalue.NewRecordID = data.UserID.ToString();
                    returnvalue.Data = js.Serialize(data);
                }
                else {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = "Please Insert Correct Data !";
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.OK;
                returnvalue.Message = ex.Message;
            }
            return returnvalue;
        }
        public CoreClass Updateuser(Stream responseStream)
        {
            var returnvalue = new CoreClass();
            var poststream = new StreamReader(responseStream).ReadToEnd();
            var data = js.Deserialize<User>(poststream);
            try
            {
                if (data != null)
                {
                    var values = db.Users.Where(x => x.UserID == data.UserID).FirstOrDefault();
                    if (values != null)
                    {
                        values.Address = data.Address;
                        values.CNIC = data.CNIC;
                        values.ContactNumber = data.ContactNumber;
                        values.Email = data.Email;
                        values.IsActive = data.IsActive;
                        values.System = data.System;
                        values.UserName = data.UserName;
                        values.Password = data.Password;
                        values.RoleID = data.RoleID;
                        values.UpdateDate = DateTime.Now;
                        db.SaveChanges();
                        returnvalue.Status = HttpStatusCode.OK;
                        returnvalue.Message = "Data Updated Successfully !!";
                        returnvalue.NewRecordID = data.UserID.ToString();
                       // returnvalue.Data - js.Serialize(data);
                    }
                    else 
                    {
                        returnvalue.Status = HttpStatusCode.BadRequest;
                        returnvalue.Message = "No Data Found To Update !";
                        returnvalue.NewRecordID = data.UserID.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = ex.Message;
                returnvalue.NewRecordID = data.UserID.ToString();
            }
            return returnvalue;
        }
    }
}