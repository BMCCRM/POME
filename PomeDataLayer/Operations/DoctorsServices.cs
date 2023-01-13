using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using PomeDataLayer.ExtraClasses;
using System.Net;
using System.IO;
using System.Data.Entity.Validation;

namespace PomeDataLayer.Operations
{
    public class DoctorsServices
    {
        private PomeDataContext db = new PomeDataContext();
        private JavaScriptSerializer js = new JavaScriptSerializer();
        public CoreClass GetAllDoctors()
        {
            var returnvalue = new CoreClass();
            try
            {
                var data = db.HospitalDoctors.GroupJoin(db.Hospitals,
                                                        d => d.HospitalID,
                                                        h => h.HospitalID,
                                                        (doct, hosp) => new
                                                        {
                                                            doct,
                                                            hosp
                                                        }).SelectMany(z => z.hosp.DefaultIfEmpty(),
                                                                          (doc, hos) => new
                                                                          {
                                                                              DoctorID = doc.doct.DoctorID,
                                                                              Designation = doc.doct.Designation,
                                                                              DateOfBirth = doc.doct.DateOfBirth,
                                                                              ContactNumber = doc.doct.ContactNumber,
                                                                              CNIC = doc.doct.CNIC,
                                                                              City = doc.doct.City,
                                                                              Email = doc.doct.Email,
                                                                              FirstName = doc.doct.FirstName,
                                                                              LastName = doc.doct.LastName,
                                                                              Mobile = doc.doct.Mobile,
                                                                              Speciality = doc.doct.Speciality,
                                                                              IsActive = doc.doct.IsActive,
                                                                              HospitalID = doc.doct.HospitalID,
                                                                              HospitalName = (hos == null) ? "--" : hos.HospitalName
                                                                          }).ToList();

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
        public CoreClass GetDoctorByID(int id)
        {
            var returnvalue = new CoreClass();
            try
            {
                var validateid = db.HospitalDoctors.Find(id);
                if (validateid != null)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Your Request Is Successfull !";
                    returnvalue.Data = js.Serialize(validateid);
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
                returnvalue.Message = string.Format(ex.Message);
                returnvalue.Data = null;
            }
            return returnvalue;
        }
        public CoreClass GetDoctorByHospitalID(int id)
        {
            var returnvalue = new CoreClass();
            try
            {
                var validateid = db.HospitalDoctors.Where(x => x.HospitalID == id).FirstOrDefault();
                if (validateid != null)
                {
                    var data = db.HospitalDoctors.Select(x => new
                    {
                        x.DoctorID,
                        x.Designation,
                        x.DateOfBirth,
                        x.ContactNumber,
                        x.CNIC,
                        x.City,
                        x.Email,
                        x.FirstName,
                        x.HospitalID,
                        x.LastName,
                        x.Mobile,
                        x.Speciality,
                        x.Password
                    }).Where(x => x.HospitalID == id).ToList();

                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Your Request Is Successfull";
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
                returnvalue.Message = string.Format(ex.Message);
                returnvalue.Data = null;
            }
            return returnvalue;
        }
        public CoreClass InsertHospitalDoctors(Stream ResponseStream) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var poststream = new StreamReader(ResponseStream).ReadToEnd();
                var data = js.Deserialize<HospitalDoctor>(poststream);
                if (data != null)
                {
                    data.CreateDate = DateTime.Now;
                    User user = new User();
                    user.UserName = data.FirstName;
                    user.Address = "";
                    user.CNIC = data.CNIC;
                    user.ContactNumber = data.ContactNumber;
                    user.Email = data.Email;
                    user.IsActive = data.IsActive;
                    user.Password = data.Password;
                    user.RoleID = 4;
                    user.System = "";
                    user.CreateDate = data.CreateDate;
                    db.Users.Add(user);
                    db.SaveChanges();
                    data.UserId = user.UserID;
                    db.HospitalDoctors.Add(data);
                    db.SaveChanges();
                    var NewID = data.DoctorID;
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
        public CoreClass UpdateHospitalDoctors(Stream ResponseStream)
        {
            var returnvalue = new CoreClass();
            try
            {
                var poststream = new StreamReader(ResponseStream).ReadToEnd();
                var data = js.Deserialize<HospitalDoctor>(poststream);

                var validateid = db.HospitalDoctors.Find(data.DoctorID);
                if (validateid == null)
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = "No Data Found To Update !!";
                    returnvalue.Data = null;
                }
                else 
                {
                    var hd = db.HospitalDoctors.Where(x => x.DoctorID == data.DoctorID).FirstOrDefault();
                    if (hd != null)
                    {
                        hd.Designation = data.Designation;
                        hd.City = data.City;
                        hd.CNIC = data.CNIC;
                        hd.ContactNumber = data.ContactNumber;
                        hd.DateOfBirth = data.DateOfBirth;
                        hd.Email = data.Email;
                        hd.FirstName = data.FirstName;
                        hd.HospitalID = data.HospitalID;
                        hd.LastName = data.LastName;
                        hd.Mobile = data.Mobile;
                        hd.Speciality = data.Speciality;
                        hd.IsActive = data.IsActive;
                        hd.UpdateDate = DateTime.Now;
                        hd.Password = data.Password;
                        data.UserId = hd.UserId;
                        db.SaveChanges();
                    }

                    var us = db.Users.Where(x => x.UserID == data.UserId).FirstOrDefault();
                    us.UserName = data.FirstName;
                    us.ContactNumber = data.ContactNumber;
                    us.Email = data.Email;
                    us.CNIC = data.CNIC;
                    us.IsActive = data.IsActive;
                    us.UpdateDate = DateTime.Now;
                    us.Password = data.Password;
                    db.SaveChanges();

                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Data Succesfully Updated";
                    returnvalue.NewRecordID = data.DoctorID.ToString();
                    returnvalue.Data = js.Serialize(data);
                    
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = string.Format(ex.Message);
                returnvalue.Data = null;
            }
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
            return returnvalue;
        }
        public CoreClass DeleteHospitalDoctors(int id)
        {
            var returnvalue = new CoreClass();
            try
            {
                var validateid = db.HospitalDoctors.Find(id);
                if (validateid != null)
                {
                    HospitalDoctor hospitalDoctor = db.HospitalDoctors.First(x => x.DoctorID == id);
                    User user = db.Users.FirstOrDefault(x => x.UserID == hospitalDoctor.UserId);
                    if(user != null)
                      db.Users.Remove(user);

                    db.HospitalDoctors.Remove(hospitalDoctor);
                    db.SaveChanges();
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Data Successfully Deleted !";
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
                returnvalue.Message = string.Format(ex.Message);
                returnvalue.Data = null;
            }
            return returnvalue;
        }
    }
}