using PomeDataLayer.ExtraClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace PomeDataLayer.Operations
{
    public class HospitalServices
    {
        private PomeDataContext db = new PomeDataContext();
        private JavaScriptSerializer js = new JavaScriptSerializer();

        public CoreClass GetHospitalsWithPharmacistsID()
        {
            var returnvalue = new CoreClass();
            try
            {
                //var result = db.EmployeeItems
                //    // If you have a filter add the .Where() here ...
                //      .GroupBy(e => e.EmployeeId)
                //      .ToList()
                //                    // Because the ToList(), this select projection is not done in the DB
                //      .Select(eg => new
                //      {
                //          EmployeeId = eg.Key,
                //          EmployeeName = eg.First().EmployeeName,
                //          Items = string.Join(",", eg.Select(i => i.ItemName))
                //      });
                //ProductName = p == null ? "(No products)" : p.ProductName
                var HospitalWithPharmacistIDdata = (from hospital in db.Hospitals
                                            join pharmacist in db.HospitalPharmicists on hospital.HospitalID equals pharmacist.HospitalID into ps
                                            from pharmacist in ps.DefaultIfEmpty()
                                            select new { HospitalName = hospital.HospitalName, ContactNum = hospital.ContactNumber, Location = hospital.Location, City = hospital.City, Department = hospital.Department, Address = hospital.Address, ID = hospital.HospitalID, PharmacistID = pharmacist == null ? 0 : pharmacist.ID }).ToList();


                //(from hospital in db.Hospitals
                // join pharmacist in db.HospitalPharmicists on hospital.HospitalID equals pharmacist.HospitalID into ps
                // from pharmacist in ps.DefaultIfEmpty()
                // group hospital.HospitalID by new { HospitalName = hospital.HospitalName, ContactNum = hospital.ContactNumber, Location = hospital.Location, City = hospital.City, Department = hospital.Department, Address = hospital.Address, ID = hospital.HospitalID, PharmacistID = pharmacist == null ? 0 : pharmacist.ID } into grp
                // select new { HospitalName = grp.Key.HospitalName, ContactNum = grp.Key.ContactNum, Location = grp.Key.Location, City = grp.Key.City, Department = grp.Key.Department, Address = grp.Key.Address, ID = grp.Key.ID, PharmacistID = grp.Key.PharmacistID }).ToList();

                            //var query = from row in table1
                            //group row.EntryYear by new { row.Title, row.EntryMonth } into grp
                            //select new { grp.Key.Title, grp.Key.EntryMonth, Years = string.Join(", ", grp) };


                            //.GroupBy(e => e.ID).Select(data => new
                            //  {

                            //      HospitalName = data.First().HospitalName,
                            //      ContactNum = data.First().ContactNum,
                            //      Location = data.First().Location,
                            //      City = data.First().City,
                            //      Department = data.First().Department,
                            //      Address = data.First().Address,
                            //      ID = data.First().ID,

                            //      //EmployeeId = data,
                            //      //EmployeeName = data.First().EmployeeName,
                            //      PharmacistIDs = string.Join(",", data.Select(i => i.PharmacistID))
                            //  }).ToList();
                if (HospitalWithPharmacistIDdata.Count > 0)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = string.Format("Data Succesfully Retrieved");
                    returnvalue.Data = js.Serialize(HospitalWithPharmacistIDdata);
                }
                else
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = string.Format("No Data Found !!");
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
        public CoreClass GetHospitals()
        {
            var returnvalue = new CoreClass();
            try
            {
                var data = (from p in db.Hospitals
                            select new { HospitalName = p.HospitalName, ContactNum = p.ContactNumber, Location = p.Location, City = p.City, Department = p.Department,Address =p.Address,ID = p.HospitalID }).ToList();
                if (data.Count > 0)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = string.Format("Data Succesfully Retrieved");
                    returnvalue.Data = js.Serialize(data);
                }
                else
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = string.Format("No Data Found !!");
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
        public CoreClass InsertHospitals(Stream ResponseStream)
        {
            var returnvalue = new CoreClass();
            try
            {
                string poststream = new StreamReader(ResponseStream).ReadToEnd();
                var data = js.Deserialize<Hospital>(poststream);
                if (data != null)
                {
                    data.CreateDate = DateTime.Now;
                    db.Hospitals.Add(data);
                    db.SaveChanges();
                    var NewID = data.HospitalID;
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
        public CoreClass UpdateHospitals(Stream ResponseStream)
        {
            var returnvalue = new CoreClass();
            try
            {
                string poststream = new StreamReader(ResponseStream).ReadToEnd();
                var data = js.Deserialize<Hospital>(poststream);
                var validateid = db.Hospitals.Find(data.HospitalID);
                if (validateid != null)
                {
                    var update = db.Hospitals.First(x => x.HospitalID == data.HospitalID);
                    update.HospitalName = data.HospitalName;
                    update.Department = data.Department;
                    update.Location = data.Location;
                    update.Address = data.Address;
                    update.City = data.City;
                    update.ContactNumber = data.ContactNumber;
                    update.IsActive = data.IsActive;
                    update.UpdateDate = DateTime.Now;

                    db.SaveChanges();
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = "Data Succesfully Updated";
                    returnvalue.NewRecordID = data.HospitalID.ToString();
                    returnvalue.Data = js.Serialize(data);
                }
                else
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = "No Data Found To Update";
                    returnvalue.NewRecordID = data.HospitalID.ToString();
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
        public CoreClass DeleteHospital(int id)
        {
            var returnvalue = new CoreClass();
            try
            {
                var validateid = db.Hospitals.Find(id);
                if (validateid != null)
                {
                    var validatehospitalInPharmacist = db.HospitalPharmicists.Where(s => s.HospitalID == id).FirstOrDefault();
                    if (validatehospitalInPharmacist == null)
                    {
                        var validatehospitalInDoctors = db.HospitalDoctors.Where(d => d.HospitalID == id).FirstOrDefault();
                        if (validatehospitalInPharmacist == null)
                        {
                            db.Hospitals.Remove(db.Hospitals.First(x => x.HospitalID == id));
                            db.SaveChanges();
                            returnvalue.Status = HttpStatusCode.OK;
                            returnvalue.Message = "Data Succesfully Deleted";
                            returnvalue.Data = null;
                        }
                        else 
                        {
                            returnvalue.Status = HttpStatusCode.BadRequest;
                            returnvalue.Message = "Please delete Linked Records from Doctors";
                        }
                    }
                    else
                    {
                        returnvalue.Status = HttpStatusCode.BadRequest;
                        returnvalue.Message = "Please delete Linked Records from Gaviscon Representator ! ";
                    }
                }
                else
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = "No Data Found To Delete !!";
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
        public CoreClass GetHospitalByID(int id)
        {
            var returnvalue = new CoreClass();
            try
            {
                var validateid = db.Hospitals.Find(id);
                if (validateid != null)
                {
                    returnvalue.Status = HttpStatusCode.OK;
                    returnvalue.Message = string.Format("Data Succesfully Retrieved");
                    returnvalue.Data = js.Serialize(validateid);
                }
                else
                {
                    returnvalue.Status = HttpStatusCode.BadRequest;
                    returnvalue.Message = string.Format("No Data Found !!");
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