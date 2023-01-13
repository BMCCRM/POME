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
    public class PharmacistServices
    {
        private PomeDataContext db = new PomeDataContext();
        private JavaScriptSerializer js = new JavaScriptSerializer();

        public CoreClass GetAllPharmacist() 
        {
            var returnvalue = new CoreClass();
            try
            {
              
                var data = db.HospitalPharmicists.GroupJoin(db.Hospitals,
                                                        p => p.HospitalID,
                                                        x => x.HospitalID,
                                                        (phar, hos) => new {phar,hos} ).SelectMany(z => z.hos.DefaultIfEmpty(),
                                                        (x,b) => new 
                                                        {   x.phar.Address,
                                                            x.phar.City,
                                                            x.phar.ContactNumber,
                                                            x.phar.DateOfBirth,
                                                            x.phar.Domain,
                                                            x.phar.Email,
                                                            x.phar.HospitalID,
                                                            x.phar.ID,
                                                            x.phar.Name,
                                                            x.phar.NIC,
                                                            HospitalName = (b == null) ? "--" : b.HospitalName 
                                                        }).ToList();
                if (data.Count > 0)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Your Request Is Successfull !";
                    returnvalue.Data = js.Serialize(data);
                }
                else 
                {
                    returnvalue.Status = HttpStatusCode.OK;
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

        public CoreClass GetPharmacistByID(int id)
        {
            var returnvalue = new CoreClass();
            try
            {
                var validateid = db.HospitalPharmicists.Find(id);
                if (validateid != null)
                {

                    var data = db.HospitalPharmicists.GroupJoin(db.Hospitals,
                                                            p => p.HospitalID,
                                                            x => x.HospitalID,
                                                            (phar, hos) => new { phar, hos }).SelectMany(z => z.hos.DefaultIfEmpty(),
                                                            (x, b) => new
                                                            {
                                                                x.phar.Address,
                                                                x.phar.City,
                                                                x.phar.ContactNumber,
                                                                x.phar.DateOfBirth,
                                                                x.phar.Domain,
                                                                x.phar.Email,
                                                                x.phar.HospitalID,
                                                                x.phar.ID,
                                                                x.phar.Name,
                                                                x.phar.NIC,
                                                                x.phar.Password,
                                                                x.phar.IsActive,
                                                                HospitalName = (b == null) ? "Select Hospital" : b.HospitalName
                                                            }).Where(p => p.ID == id).First();

                    //var data = db.HospitalPharmicists.Select(x => new
                    //{
                    //    x.Address,
                    //    x.City,
                    //    x.ContactNumber,
                    //    x.DateOfBirth,
                    //    x.Domain,
                    //    x.Email,
                    //    x.HospitalID,
                    //    x.ID,
                    //    x.Name,
                    //    x.NIC,
                    //    x.Password,
                    //    x.IsActive
                    //}).Where(p => p.ID == id).First();

                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Your Request Is Successfull !";
                    returnvalue.Data = js.Serialize(data);
                }
                else 
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = "No Data Found !!";
                }
                
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = ex.Message.ToString();
            }
            return returnvalue;
        }

        public CoreClass GetPharmacistByHospitalID(int id) 
        {
            var returnvalue = new CoreClass();
            try
            {
                var validateid = db.HospitalPharmicists.Join(db.Hospitals,
                                                        p => p.HospitalID,
                                                        x => x.HospitalID,
                                                        (phar, hos) => new
                                                        {   phar.Address,
                                                            phar.City,
                                                            phar.ContactNumber,
                                                            phar.DateOfBirth,
                                                            phar.Domain,
                                                            phar.Email,
                                                            phar.HospitalID,
                                                            phar.ID,
                                                            phar.Name,
                                                            phar.NIC,
                                                            hos.HospitalName
                                                        }).Where(x => x.HospitalID == id).ToList();
                if (validateid.Count > 0)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Your Request Is Successfull";
                    returnvalue.Data = js.Serialize(validateid);
                }
                else 
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = "No Data Found !!";
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = ex.Message.ToString();
            }
            return returnvalue;
        }

        public CoreClass DeletePharmacist(int id)
        {
            var returnvalue = new CoreClass();
            try
            {
                var validateid = db.HospitalPharmicists.Find(id);
                if (validateid != null)
                {
                    db.HospitalPharmicists.Remove(db.HospitalPharmicists.First(x => x.ID == id));
                    db.SaveChanges();
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Record Successfully Deleted !";
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
                returnvalue.Message = ex.Message.ToString();
            }
            return returnvalue;
        }

        public CoreClass InsertPharmacistData(Stream ResponseStream)
        {
            var returnvalue = new CoreClass();
            try
            {
                var poststream = new StreamReader(ResponseStream).ReadToEnd();
                var data = js.Deserialize<HospitalPharmicist>(poststream);

                if (data.Name != null)
                {
                    data.CreateDate = DateTime.Now;
                    var hospitalPharmacist = db.HospitalPharmicists.Where(p => p.ContactNumber.Equals(data.ContactNumber)).ToList();
                    if (hospitalPharmacist.Count==0)
                    {
                        db.HospitalPharmicists.Add(data);
                        db.SaveChanges();
                        returnvalue.Status = HttpStatusCode.OK;
                        returnvalue.Message = "Data Inserted Successfully !";
                        returnvalue.NewRecordID = data.ID.ToString();
                        returnvalue.Data = js.Serialize(data);
                    }
                    else
                    {
                        returnvalue.Status = HttpStatusCode.BadRequest;
                        returnvalue.Message = "Contact Number Already Given";
                        returnvalue.Data = js.Serialize(data);
                    }
                    
                }
                else 
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = "No Data Found To Insert !";
                    returnvalue.NewRecordID = null;
                    returnvalue.Data = js.Serialize(data);
                }
            }
            catch (Exception ex)
            {
                returnvalue.Status = HttpStatusCode.BadRequest;
                returnvalue.Message = ex.Message.ToString();
            }
            return returnvalue;
        }
        public CoreClass UpdatePharmacist(Stream ResponseStream)
        {
            var returnvalue = new CoreClass();
            try
            {
                var poststream = new StreamReader(ResponseStream).ReadToEnd();
                var data = js.Deserialize<HospitalPharmicist>(poststream);

                var updateid = db.HospitalPharmicists.Where(x => x.ID == data.ID).FirstOrDefault();
                if (updateid != null)
                {
                    updateid.Name = data.Name;
                    updateid.Address = data.Address;
                    updateid.City = data.City;
                    updateid.ContactNumber = data.ContactNumber;
                    updateid.DateOfBirth = data.DateOfBirth;
                    updateid.Domain = data.Domain;
                    updateid.Email = data.Email;
                    updateid.NIC = data.NIC;
                    updateid.HospitalID = data.HospitalID;
                    updateid.IsActive = data.IsActive;
                    updateid.UpdateDate = DateTime.Now;
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
                returnvalue.Message = ex.Message.ToString();
            }
            return returnvalue;
        }
    }
}