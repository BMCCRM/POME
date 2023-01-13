$(document).ready(function () {
    FillGrid();
    GetHospitalforddl();
    clearFields();
    $('#ddlhospitals').multiSelect('select', '1');
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
            txtname: {
                required: true,
                lettersonly: true
            },
            txtnumber: {
                required: true,
                custom_number: true
            },
            txtemail: {
                required: true,
                email: true
            },
            address: {
                required: true
            },
            txtnic: {
                required: true,
                cnic: true
            },
            txtDomain: {
                required: true
            },
            txtpass: {
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

var InsertPharmacistData = function () {
    if (!$('#form1').valid()) {
        sweetAlert('Validation', 'Please Insert Required Fields !', 'error');
        return false;
    }
    var name = $('#txtname').val();
    var city = $('#txtcity').val();
    var email = $('#txtemail').val();
    var number = $('#txtnumber').val();
    var domain = $('#txtDomain').val();
    var nic = $('#txtnic').val();
    var dob = $('#datepicker').val();
    var isactive = ($('#chisactive').is(':checked') ? "True" : "False");
    var hospitalid = $('#ddlhospitals').val();
    var address = $('#txtaddress').val();
    var pass = $('#txtpass').val();

    $.ajax({
        type: "POST",
        url: "../WCF/Service1.svc/Pharmacist/InsertPharmacist",
        contentType: "application/json; charset=utf-8",
        data: "{'Name':'" + name + "','City':'" + city + "','Email':'" + email + "','ContactNumber':'" + number + "','DateOfBirth':'" + dob + "','Domain':'" + domain + "','HospitalID':'" + hospitalid + "','Address':'" + address + "','NIC':'" + nic + "','Password':'" + pass + "','IsActive':'" + isactive + "'}",
        async: false,
        success: OnsuccessInsert,
        cache: false
    });
}
function OnsuccessInsert(response) {
    if (response.Status == 200) {
        swal("Inserted!", "Data Inserted Successfully!", "success");
        FillGrid();
        clearFields();
    }
    else {
        if (response.Message == "Contact Number Already Given") {
            swal("Sorry!", "Contact Number Already Given", "error");
        }
        else {
            swal("Sorry!", "Something Went Wrong ", "error");
        }

    }

}

var UpdatePharmacistData = function () {
    if (!$('#form1').valid()) {
        sweetAlert('Validation', 'Please Insert Required Fields !', 'error');
        return false;
    }
    var name = $('#txtname').val();
    var city = $('#txtcity').val();
    var email = $('#txtemail').val();
    var number = $('#txtnumber').val();
    var domain = $('#txtDomain').val();
    var nic = $('#txtnic').val();
    var dob = $('#datepicker').val();
    var isactive = ($('#chisactive').is(':checked') ? "True" : "False");
    var hospitalid = $('#ddlhospitals').val();
    var address = $('#txtaddress').val();
    var id = $('#txthiddenid').val();
    var pass = $('#txtpass').val();
    $.ajax({
        type: "POST",
        url: "../WCF/Service1.svc/Pharmacist/UpdatePharmacist",
        contentType: "application/json; charset=utf-8",
        data: "{'Name':'" + name + "','City':'" + city + "','Email':'" + email + "','ContactNumber':'" + number + "','DateOfBirth':'" + dob + "','Domain':'" + domain + "','HospitalID':'" + hospitalid + "','Address':'" + address + "','NIC':'" + nic + "','ID':'" + id + "','Password':'" + pass + "','IsActive':'" + isactive + "'}",
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

var clearFields = function () {
    $('#txtname').val("");
    $('#txtcity').val("");
    $('#txtemail').val("");
    $('#txtnumber').val("");
    $('#txtDomain').val("");
    $('#txtnic').val("");
    $('#ddlhospitals').multiSelect('select', -1);
    $('#chisactive').prop('checked', false);
    // $('#ddlhospitals').val(-1);
    $('#txtaddress').val("");
    $('#txthiddenid').val("");
    $('#txtpass').val("");

    $('#btnUpdate').hide();
    $('#btncancel').hide();
    $('#btninsert').show();
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
        url: "../WCF/Service1.svc/Pharmacist/DeletePharmacist/" + id,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function () {
            swal("Deleted!", "Data has been deleted.", "success");
        },
        cache: false
    });
});
}

var FillGrid = function () {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Pharmacist/GetAllPharmacist",
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
        $('#tableappend').append('<table class="table table-bordered table-striped" id="datatable"> <thead><tr> <th>Pharmacist Name</th><th>Email</th><th>City </th><th>Address</th><th>Contact Number</th><th>NIC</th><th>Hospital Name</th> <th>Actions</th> </tr></thead><tbody id="bodydata"></tbody></table>');
        if (msg != null) {
            $.each(msg, function (i, option) {
                $('#bodydata').append('<tr><td>' + option.Name + '</td> <td>' + option.Email + '</td>'
                    //+'<td>' + option.Email + '</td>' 
                    + '<td>' + option.City + '</td>'
                    + '<td>' + option.Address + ' </td>'
                    + '<td>' + option.ContactNumber + '</td>'
                    + '<td>' + option.NIC + '</td>'
                    + '<td>' + option.HospitalName + ' </td>'
                    + '<td > <a onclick="OnEditClick(' + option.ID + ')" class="on-default edit-row"><i style="Color:#17b1e3" class="fa fa-pencil"></i></a>  &nbsp;&nbsp;&nbsp;'
                    + '<a onclick="OnDeleteClick(' + option.ID + ')"  class="on-default remove-row"><i style="Color:red" class="fa fa-trash-o"></i></a></td>'
                    + '</tr>');
            });
        }
    }
    else {
        $('#tableappend').empty();
        $('#tableappend').append('<table class="table table-bordered table-striped" id="datatable"> <thead><tr> <th>Pharmacist Name</th><th>Email</th><th>City </th><th>Address</th><th>Contact Number</th><th>NIC</th><th>Hospital Name</th> <th>Actions</th> </tr></thead><tbody id="bodydata"></tbody></table>');

    }
    try {
        if (userrole == "Agency") {
            $('#ModelForm').hide();
                $('#datatable').DataTable({ 'columns': [null,null,null,null,null,null,null, { 'visible': false }] });
        }
    } catch (e) {
        $('#datatable').DataTable();
    }
}

var OnEditClick = function (id) {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Pharmacist/GetPharmacistByID/" + id,
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
    $('#txtname').val(msg.Name);
    $('#txtcity').val(msg.City);
    $('#txtemail').val(msg.Email);
    $('#txtnumber').val(msg.ContactNumber);
    $('#txtDomain').val(msg.Domain);
    $('#txtnic').val(msg.NIC);

    if (msg.IsActive == true) {
        $('#chisactive').prop('checked', true);
    } else {
        $('#chisactive').prop('checked', false);
    }
    //$('#ddlhospitals').multiSelect('select', '' + msg.HospitalID + '');
    //ndate = new Date(parseInt(msg.DateOfBirth.substr(6)));
    //$("#datepicker").datepicker({
    //    dateFormat: 'dd/mm/yyyy'
    //}).datepicker('setDate', ndate);

    //var a = $('#datepicker').val();

    //var date = a.split("/")[1] + "/" + a.split("/")[0] + "/" + a.split("/")[2];

    // $('#ddlhospitals').val(msg.HospitalID);
    $('#txtaddress').val(msg.Address);
    $('#txthiddenid').val(msg.ID);
    $('#txtpass').val(msg.Password);

    $('#btnUpdate').show();
    $('#btncancel').show();
    $('#btninsert').hide();

}

