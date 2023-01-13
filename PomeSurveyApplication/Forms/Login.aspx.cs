using PomeDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRM.Forms
{
    public partial class Login : System.Web.UI.Page
    {
        private PomeDataContext db = new PomeDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void BtnLoginClick(object sender, EventArgs e)
        {
            string username = txtusername.Text;
            string password = txtpass.Text;

            var validatedatafromusers = db.Users.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
            if (validatedatafromusers == null)
            {
                var validatedatafromdoctors = db.HospitalDoctors.Where(h => h.FirstName == username && h.Password == password).FirstOrDefault();
                if (validatedatafromdoctors == null)
                {
                    var validatedatafrompharmacist = db.HospitalPharmicists.Where(p => p.Name == username && p.Password == password).FirstOrDefault();
                    if (validatedatafrompharmacist == null)
                    {
                        //lberror.Text = "UserName Password Is Incorrect !";
                        diverror.Visible = true;
                    }
                    else
                    {
                        if (validatedatafrompharmacist.IsActive != Convert.ToBoolean(1))
                        {
                            //lberror.Text = "User Is InActive";
                            diverror.Visible = true;
                        }
                        else
                        {
                            Session["UserRole"] = "medrep";
                            Session["UserID"] = validatedatafrompharmacist.ID;
                            Session["UserName"] = validatedatafrompharmacist.Name;
                            Response.Redirect("Pharmacist.aspx");
                        }
                    }
                }
                else
                {
                    if (validatedatafromdoctors.IsActive != Convert.ToBoolean(1))
                    {
                        //lberror.Text = "User Is InActive";
                        diverror.Visible = true;
                    }
                    else
                    {
                        Session["UserRole"] = "doctor";
                        Session["UserID"] = validatedatafromdoctors.DoctorID;
                        Session["UserName"] = validatedatafromdoctors.FirstName;
                        Response.Redirect("Dashboard.aspx");
                    }
                }
            }
            else
            {

                if (validatedatafromusers.IsActive != Convert.ToBoolean(1))
                {
                    //lberror.Text = "User Is InActive";
                    diverror.Visible = true;
                }
                else
                {
                    var rolename = db.UserRoles.Where(x => x.RoleID == validatedatafromusers.RoleID).First();
                    Session["UserRole"] = rolename.RoleName;
                    Session["UserID"] = validatedatafromusers.UserID;
                    Session["UserName"] = validatedatafromusers.UserName;
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }
    }
}