using PomeDataLayer.ExtraClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Hosting;

namespace PomeSurveyApplication.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        #region Hospitals

        [OperationContract]
        [WebGet(UriTemplate = "/Hospitals/GetAllHospitalsWithPharmacistsID",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CoreClass GetAllHospitalsWithPharmacistsID();

        [OperationContract]
        [WebGet(UriTemplate = "/Hospitals/GetAllHospitals",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CoreClass GetAllHospitals();

        [OperationContract]
        [WebGet(UriTemplate = "/Hospitals/GetHospitalByID/{ID}",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CoreClass GetHospitalByID(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Hospitals/InsertHospital",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CoreClass InsertHospital(Stream ResponseStream);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Hospitals/UpdateHospitals",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CoreClass UpdateHospital(Stream ResponseStream);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Hospitals/DeleteHospitals/{ID}",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CoreClass DeleteHospital(string id);

        #endregion

        #region Doctors

        [OperationContract]
        [WebGet(UriTemplate = "/Doctors/GetAllDoctors",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CoreClass GetAllDoctors();

        [OperationContract]
        [WebGet(UriTemplate = "/Doctors/GetDoctorByID/{ID}",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CoreClass GetDoctorsByID(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/Doctors/GetDoctorByHospitalID/{ID}",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CoreClass GetDoctorsByHospitalID(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Doctors/InsertDoctor",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CoreClass InsertDoctor(Stream ResponseStream);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Doctors/UpdateDoctor",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CoreClass UpdateDoctor(Stream ResponseStream);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Doctors/DeleteDoctors/{ID}",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CoreClass DeleteDoctors(string id);
        #endregion

        #region Pharmacist
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Pharmacist/GetAllPharmacist",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass GetAllPharmacist();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Pharmacist/GetPharmacistByID/{id}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass GetPharmacistByID(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Pharmacist/GetPharmacistByHospitalID/{id}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass GetPharmacistByHospitalID(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Pharmacist/DeletePharmacist/{id}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass DeletePharmacist(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Pharmacist/InsertPharmacist",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass InsertPharmacist(Stream ResponseStream);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Pharmacist/UpdatePharmacist",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass UpdatePharmacist(Stream ResponseStream);

        #endregion

        #region UserRoles
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Roles/GetAllUserRoles",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass GetAllUserRoles();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Roles/GetRolesByID/{id}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass GetRolesByID(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Roles/DeleteRoles/{id}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass DeleteRoles(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Roles/InsertUserRoles",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass InsertUserRoles(Stream ResponseStream);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Roles/UpdateUserRoles",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass UpdateUserRoles(Stream ResponseStream);

        #endregion

        #region Users
         [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Users/GetAllUsers",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass GetAllUsers();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Users/GetUserByID/{id}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass GetUserByID(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Users/DeleteUsers/{id}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass DeleteUsers(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Users/InsertUsers",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass InsertUsers(Stream ResponseStream);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Users/UpdateUsers",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass UpdateUsers(Stream ResponseStream);
        #endregion

        #region Question
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Questions/GetALLQuestions",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass GetALLQuestions();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Questions/GetOptionsbyQuestionID/{id}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass GetOptionsbyQuestionID(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Questions/InsertQuestion",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass InsertQuestion(Stream responseStream);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Questions/InsertQuestionOperation",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass InsertQuestionOperation(Stream responseStream);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Questions/InsertLinkedQuestion",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass InsertLinkedQuestion(Stream responseStream);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Questions/UpdateQuestion",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass UpdateQuestion(Stream responseStream);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Questions/UpdateOption",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass UpdateOption(Stream responseStream);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Questions/GetQuestionByOrderList",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass GetQuestionByOrderList();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Questions/DeleteQuestion/{id}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass DeleteQuestion(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Questions/GetOptionsDetails/{optionid}/{questionid}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass GetOptionsDetails(string optionid,string questionid);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Questions/GetQuestionDetails/{questionid}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass GetQuestionDetails(string questionid);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Questions/DeleteOption/{id}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass DeleteOption(string id);

        #endregion

        #region AppServices
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "AppServices/Login/{username}/{password}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass Login(string username,string password);
        #endregion

        #region AppSevices
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UploadFile/{fileName}")]
        void UploadFile(string fileName, Stream stream);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UploadFiles/{fileName}")]
        void UploadFiles(string fileName, Stream stream);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/POST/{fileName}")]
        void asd(string fileName, Stream stream);
        
        #endregion

        #region AgeFilter
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/AgeFilter/GetAgeFilter",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass GetAgeFilter();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/AgeFilter/GetFilterByID/{id}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass GetFilterByID(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/AgeFilter/DeleteFilter/{id}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass DeleteFilter(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AgeFilter/InsertAgeFilter",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass InsertAgeFilter(Stream ResponseStream);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AgeFilter/UpdateAgeFilter",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        CoreClass UpdateAgeFilter(Stream ResponseStream);

        #endregion

    }
}
