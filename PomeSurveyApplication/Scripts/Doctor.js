$(document).ready(function () {
    FillGrid();
    GetHospitalforddl();
    clearFields();

    jQuery.validator.addMethod("custom_number", function (value, element) {
        return this.optional(element) || value === "NA" ||
            value.match(/^[0-9,\+-]+$/);

    }, "Please enter a valid number ");
    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[a-zA-Z\s]+$/.test(value);
    }, "Letters only please");
    jQuery.validator.addMethod("cnic", function (value, element) {
        return this.optional(element) || /^[0-9]{5}-[0-9]{7}-[0-9]{1}$/.test(value);
    }, "Please enter correct CNIC number !");

    var isValidated = $('#form1').validate({
        rules: {
            txtdname: {
                required: true,
                lettersonly: true
            },
            txtmnum: {
                required: true,
                custom_number: true
             
            },
            txtnic: {
                required: true,
                cnic: true
            },
            txtdesig: {
                required: true
            }
        }
    });

});

function GetHospitalforddl() {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Hospitals/GetAllHospitals",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: OnsuccessHospitalDDL,
        cache: false
    });
}
function OnsuccessHospitalDDL(response) {
    var msg = jQuery.parseJSON(response.Data);
    if (response.Status == 200) {
        $('#ddlhospitals').empty();
        $('#ddlhospitals').append('<option value="-1">Select Hospital</option>');
        $.each(msg, function (i, option) {
            $('#ddlhospitals').append('<option value="' + option.ID + '">' + option.HospitalName + '</option>');
        });
    }
}

var clearFields = function () {
    $('#txtdname').val("");
    $('#txtmnum').val("");
    $('#txtCity').val("");
    $('#txtdesig').val("");
    $('#txtspec').val("");
    $('#txtnic').val("");
    $('#txtpass').val("");
    $('#ddlhospitals').multiSelect('select', -1);
   // $('#ddlhospitals').val(-1);
    $('#txtemail').val("");
    $('#txthiddenid').val("");
    $('#txtcnum').val("");
    $('#chisactive').prop('checked', false);
    $('#btnUpdate').hide();
    $('#btncancel').hide();
    $('#btnInsert').show();
}

var FillGrid = function () {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Doctors/GetAllDoctors",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: OnSuccessFillGrid,
        cache: false
    });
}
var OnSuccessFillGrid = function (response) {
    if (response.Data != "") {

        var msg = jQuery.parseJSON(response.Data);
        $('#tableappend').empty();
        $('#tableappend').append('<table class="table table-bordered table-striped" id="datatable"> <thead><tr> <th>Doctor Name</th><th>Speciality </th><th>Designation</th><th>City </th><th>Email</th><th>Mobile Number</th><th>CNIC</th><th>Hospital Name</th> <th>Actions</th> </tr></thead><tbody id="bodydata"></tbody></table>');
        if (msg != null) {
            $.each(msg, function (i, option) {
                $('#bodydata').append('<tr><td>' + option.FirstName + '</td> <td>' + option.Speciality + '</td>'
                    //+'<td>' + option.Email + '</td>' 
                    + '<td>' + option.Designation + '</td>'
                    + '<td>' + option.City + ' </td>'
                    + '<td>' + option.Email + '</td>'
                    + '<td>' + option.Mobile + '</td>'
                    + '<td>' + option.CNIC + '</td>'
                    + '<td>' + option.HospitalName + ' </td>'
                    + '<td > <a onclick="OnEditClick(' + option.DoctorID + ')" class="on-default edit-row"><i style="Color:#17b1e3" class="fa fa-pencil"></i></a>  &nbsp;&nbsp;&nbsp;'
                    + '<a onclick="OnDeleteClick(' + option.DoctorID + ')"  class="on-default remove-row"><i style="Color:red" class="fa fa-trash-o"></i></a></td>'
                    + '</tr>');
            });
        }
    }
    else {
        $('#tableappend').empty();
        $('#tableappend').append('<table class="table table-bordered table-striped" id="datatable"> <thead><tr> <th>Doctor Name</th><th>Speciality </th><th>Designation</th><th>City </th><th>Email</th><th>Mobile Number</th><th>CNIC</th><th>Hospital Name</th> <th>Actions</th> </tr></thead><tbody id="bodydata"></tbody></table>');

    }
    try {
        if (userrole == "Agency") {
            $('#ModelForm').hide();
                $('#datatable').DataTable({ 'columns': [null, null, null, null, null, null, null, null, { 'visible': false }] });
        }
    } catch (e) {
        $('#datatable').DataTable();
    }
}

var insertdata = function () {
    if (!$('#form1').valid()) {
        sweetAlert('Validation', 'Please Insert Required Fields !', 'error');
        return false;
    }
        var name = $('#txtdname').val();
        var mobnum = $('#txtmnum').val();
        var city = $('#txtCity').val();
        var designation = $('#txtdesig').val();
        var speciality = $('#txtspec').val();
        var nic = $('#txtnic').val();
        var hospital = $('#ddlhospitals').val();
        var email = $('#txtemail').val();
        var isactive = ($('#chisactive').is(':checked') ? "True" : "False");
        var dob = $('#datepicker').val();
        var contact = $('#txtcnum').val();
        var pass = $('#txtpass').val();
        $.ajax({
            type: "POST",
            url: "../WCF/Service1.svc/Doctors/InsertDoctor",
            contentType: "application/json; charset=utf-8",
            data: "{'FirstName':'" + name + "','Designation':'" + designation + "','Speciality':'" + speciality + "','DateOfBirth':'" + dob + "','CNIC':'" + nic + "','City':'" + city + "','Email':'" + email + "','IsActive':'" + isactive + "','HospitalID':'" + hospital + "','Mobile':'" + mobnum + "','ContactNumber':'" + contact + "','Password':'" + pass + "'}",
            async: false,
            success: OnsuccessInsert,
            cache: false
        });
    
}
var OnsuccessInsert = function (response) {
    if (response.Status == 200) {
        swal("Inserted!", "Data Inserted Successfully!", "success");
        FillGrid()
    }
    else {
        swal("Sorry!", "Something Went Wrong ", "error");
    }
    clearFields();
}

var UpdateData = function () {
    if (!$('#form1').valid()) {
        sweetAlert('Validation', 'Please Insert Required Fields !', 'error');
        return false;
    }
    var name = $('#txtdname').val();
    var mobnum = $('#txtmnum').val();
    var city = $('#txtCity').val();
    var designation = $('#txtdesig').val();
    var speciality = $('#txtspec').val();
    var nic = $('#txtnic').val();
    var hospital = $('#ddlhospitals').val();
    var email = $('#txtemail').val();
    var isactive = ($('#chisactive').is(':checked') ? "True" : "False");
    var dob = $('#datepicker').val();
    var contact = $('#txtcnum').val();
    var id = $('#txthiddenid').val();
    var pass = $('#txtpass').val();
    $.ajax({
        type: "POST",
        url: "../WCF/Service1.svc/Doctors/UpdateDoctor",
        contentType: "application/json; charset=utf-8",
        data: "{'FirstName':'" + name + "','Designation':'" + designation + "','Speciality':'" + speciality + "','DateOfBirth':'" + dob + "','CNIC':'" + nic + "','City':'" + city + "','Email':'" + email + "','IsActive':'" + isactive + "','HospitalID':'" + hospital + "','Mobile':'" + mobnum + "','ContactNumber':'" + contact + "','DoctorID':'" + id + "','Password':'" + pass + "'}",
        async: false,
        success: OnsuccessUpdate,
        cache: false
    });
}
var OnsuccessUpdate = function (response) {
    if (response.Status == 200) {
        swal("Updated!", "Data Updated Successfully!", "success");
        FillGrid()

    } else {
        swal("Sorry!", "Something Went Wrong ", "error");
    }
    clearFields();
}

var OnDeleteClick = function (id) {
    swal({
        title: "Are you sure?",
        text: "You will not be able to recover this data !",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: false
    },
function () {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Doctors/DeleteDoctors/" + id,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function () {
            swal("Deleted!", "Data has been deleted.", "success");
            FillGrid();
        },
        cache: false
    });
});
}

var OnEditClick = function (id) {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Doctors/GetDoctorByID/" + id,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: OnsuccessEdit,
        cache: false
    });
}
var OnsuccessEdit = function (response) {
    var msg = $.parseJSON(response.Data);
    $("#Insertform").slideDown("slow", function () {
        // Animation complete.
    });
    
    if (msg.IsActive == true) {
        $('#chisactive').prop('checked', true);
    } else {
        $('#chisactive').prop('checked', false);
    }
    var date1 = new Date();

    $("#datepicker").datepicker({
        dateFormat: 'dd/mm/yyyy'
    }).datepicker('setDate', date1);

    var a = $('#datepicker').val();

    var date = a.split("/")[1] + "/" + a.split("/")[0] + "/" + a.split("/")[2];

    $('#txtdname').val(msg.FirstName);
    $('#txtmnum').val(msg.Mobile);
    $('#txtCity').val(msg.City);
    $('#txtdesig').val(msg.Designation);
    $('#txtspec').val(msg.Speciality);
    $('#txtnic').val(msg.CNIC);
    $('#ddlhospitals').multiSelect('select', msg.HospitalID);
    $('#txtemail').val(msg.Email);
    $('#txtcnum').val(msg.ContactNumber);

    $('#txthiddenid').val(msg.DoctorID);
    $('#btnUpdate').show();
    $('#btncancel').show();
    $('#btnInsert').hide();

}

