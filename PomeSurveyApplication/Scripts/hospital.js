$(document).ready(function () {
    FillGridData();
    clearFields();
    jQuery.validator.addMethod("custom_number", function (value, element) {
        return this.optional(element) || value === "NA" ||
            value.match(/^[0-9,\+-]+$/);

    }, "Please enter a valid number ");
    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[a-zA-Z\s]+$/.test(value);
    }, "Letters only please");

    var isValidated = $('#form1').validate({
        rules: {
            txthname: {
                required: true
               
            },
            txtContact: {
                required: true,
                custom_number: true
            },
            txtlocation: {
                required: true
            },
            address: {
                required: true
            },
            txtdepartment: {
                required: true
            }
        }
    });
});

function InsertHospitalData()
{
    if (!$('#form1').valid()) {
        sweetAlert('Validation', 'Please Insert Required Fields !', 'error');
        return false;
    }
   // if ($('#btnInsert').html() == "ADD") {
        var hname = $('#txthname').val();
        var contactnum = $('#txtContact').val();
        var city = $('#txtCity').val();
        var address = $('#txtaddress').val();
        var department = $('#txtdep').val();
        var location = $('#txtlocation').val();
        var isactive = ($('#chisactive').is(':checked') ? "True" : "False");

        $.ajax({
            type: "POST",
            url: "../WCF/Service1.svc/Hospitals/InsertHospital",
            data: "{'HospitalName':'" + hname + "','ContactNumber':'" + contactnum + "','City':'" + city + "','Address':'" + address + "','Department':'" + department + "','Location':'" + location + "','IsActive':'" + isactive + "'}",
            contentType: "application/json; charset=utf-8",
            async: false,
            success: onsuccessInsert,
            cache: false
        });
    //}
   
}
function onsuccessInsert(response)
{
    if (response.Status == 200) {
        swal("Inserted!", "Data Inserted Successfully!", "success");
        FillGridData();
    }
    else {
        swal("Sorry!", "Something Went Wrong ", "error");
    }
    clearFields();
}

function UpdateHospitalData() {

        var hname = $('#txthname').val();
        var contactnum = $('#txtContact').val();
        var city = $('#txtCity').val();
        var address = $('#txtaddress').val();
        var department = $('#txtdep').val();
        var location = $('#txtlocation').val();
        var id = $('#txthiddenid').val();
        var isactive = ($('#chisactive').is(':checked') ? "True" : "False");
        $.ajax({
            type: "POST",
            url: "../WCF/Service1.svc/Hospitals/UpdateHospitals",
            data: "{'HospitalName':'" + hname + "','ContactNumber':'" + contactnum + "','City':'" + city + "','Address':'" + address + "','Department':'" + department + "','Location':'" + location + "','IsActive':'" + isactive + "','HospitalID':'" + id + "'}",
            contentType: "application/json; charset=utf-8",
            async: false,
            success: onsuccessUpdate,
            cache: false
        });
    
}
function onsuccessUpdate(response) {
    if (response.Status == 200) {
        swal("Updated!", "Data Updated Successfully!", "success");
        FillGridData();
    }
    else {
        swal("Sorry!", "Something Went Wrong ", "error");
    }
    clearFields();
}

function FillGridData()
{
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Hospitals/GetAllHospitals",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: onsuccessFillGrid,
        cache: false
    });
}
function onsuccessFillGrid(response) {
    var msg = jQuery.parseJSON(response.Data);
    $('#tableappend').empty();
    $('#tableappend').append('<table class="table table-bordered table-striped" id="datatable"> <thead><tr> <th>Hospital Name</th><th>City</th><th>Contact Number</th><th>Address</th><th>Department</th><th>Location</th> <th>Actions</th> </tr></thead><tbody id="bodydata"></tbody></table>');
    if (msg != null) {
        $.each(msg, function (i, option) {
            $('#bodydata').append('<tr><td>' + option.HospitalName + '</td> ' 
                +'<td>' + option.City + '</td>' 
                +'<td>' + option.ContactNum + ' </td>' 
                +'<td>' + option.Address + '</td>' 
                +'<td>' + option.Department + '</td>' 
                +'<td>' + option.Location + ' </td>' 
                + '<td >  <a href="#" onclick="OnEditClick(' + option.ID + ')" class="on-default edit-row"><i style="Color:#17b1e3" class="fa fa-pencil"></i></a>  &nbsp;&nbsp;&nbsp;'
                          + '<a href="#" onclick="OnDeleteClick(' + option.ID + ')" class="on-default remove-row"><i style="Color:red" class="fa fa-trash-o"></i></a></td>'
                +'</tr>');
        });
    }

    try {
        if (userrole == "Agency") {
            $('#ModelForm').hide();
            $('#datatable').DataTable({ 'columns': [null, null, null, null, null, null, { 'visible': false }] });
        }
    } catch (e) {
        $('#datatable').DataTable();
    }
    
    
    
}

var clearFields = function () {
    $('#txthname').val("");
    $('#txtContact').val("");
    $('#txtCity').val("");
    $('#txtaddress').val("");
    $('#txtdep').val("");
    $('#txtlocation').val("");
    $('#txthiddenid').val("");

    $('#chisactive').prop('checked', false);
    $('#btnUpdate').hide();
    $('#btncancel').hide();
    $('#btnInsert').show();

}

var OnEditClick = function (id) {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Hospitals/GetHospitalByID/" + id,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: OnsuccessEdit,
        cache: false
    });
}
var OnsuccessEdit = function (response) {
    var msg = $.parseJSON(response.Data);
    
    $('#txthname').val(msg.HospitalName);
    $('#txtContact').val(msg.ContactNumber);
    $('#txtCity').val(msg.City);
    $('#txtaddress').val(msg.Address);
    $('#txtdep').val(msg.Department);
    $('#txtlocation').val(msg.Location);
    $('#txthiddenid').val(msg.HospitalID);
    if (msg.IsActive == true) {
        $('#chisactive').prop('checked', true);
    } else {
        $('#chisactive').prop('checked', false);
    }
    //$('#controlButton').text('Update');
    $('#btnUpdate').show();
    $('#btncancel').show();
    $('#btnInsert').hide();
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
        url: "../WCF/Service1.svc/Hospitals/DeleteHospitals/" + id,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function () {
            swal("Deleted!", "Data has been deleted.", "success");
            FillGridData();
        },
        cache: false
    });
});
} 
