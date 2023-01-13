$(document).ready(function () {
    FillGrid();
    GetDDlRoles();
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
            txtuname: {
                required: true,
                lettersonly:true
            },
            txtcnum: {
                required: true,
                custom_number:true
            },
            txtemail: {
                required: true,
                email:true
            },
            address: {
                required: true
            },
            txtnic: {
                required: true,
                cnic:true
            },
            txtpass: {
                required: true
            }
        }
    });
});

var GetDDlRoles = function () {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Roles/GetAllUserRoles",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: OnsuccessRoleDDL,
        cache: false
    });
}
var OnsuccessRoleDDL = function (response) {
    var msg = jQuery.parseJSON(response.Data);
    if (response.Status == 200) {
        $('#ddlroles').empty();
        $('#ddlroles').append('<option value="-1">Select Role</option>');
        $.each(msg, function (i, option) {
            $('#ddlroles').append('<option value="' + option.RoleID + '">' + option.RoleName + '</option>');
        });
    }
}

var clearFields = function () {
    $('#txtuname').val("");
    $('#txtnic').val("");
    $('#txtpass').val("");
    $('#txtemail').val(""); 
    $('#txtaddress').val("");
    $('#ddlroles').multiSelect('select', -1);
    $('#ddlroles').val(-1);
    $('#txtcnum').val("");
    $('#txthiddenid').val("");
    $('#chisactive').prop('checked', false);
    $('#btnUpdate').hide();
    $('#btncancel').hide();
    $('#btnInsert').show();
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
        url: "../WCF/Service1.svc/Users/DeleteUsers/" + id,
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

var FillGrid = function () {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Users/GetAllUsers",
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
        $('#tableappend').append('<table class="table table-bordered table-striped" id="datatable"> <thead><tr> <th>User Name</th><th>Role Name </th><th>Email</th><th>CNIC</th><th>Address</th><th>Status</th> <th>Actions</th> </tr></thead><tbody id="bodydata"></tbody></table>');
        if (msg != null) {
            $.each(msg, function (i, option) {
                $('#bodydata').append('<tr><td>' + option.UserName + '</td> <td>' + option.RoleName + '</td>'
                    + '<td>' + option.Email + '</td>'
                    + '<td>' + option.CNIC + ' </td>'
                    + '<td>' + option.Address + '</td>'
                    + '<td>' + option.IsActive + '</td>'

                    + '<td > <a onclick="OnEditClick(' + option.UserID + ')" class="on-default edit-row"><i style="Color:#17b1e3" class="fa fa-pencil"></i></a>  &nbsp;&nbsp;&nbsp;'
                    + '<a onclick="OnDeleteClick(' + option.UserID + ')"  class="on-default remove-row"><i style="Color:red" class="fa fa-trash-o"></i></a></td>'
                    + '</tr>');
            });
        }
    }
    else {
        $('#tableappend').empty();
        $('#tableappend').append('<table class="table table-bordered table-striped" id="datatable"> <thead><tr> <th>User Name</th><th>Role Name </th><th>Email</th><th>CNIC</th><th>Address</th><th>Status</th> <th>Actions</th> </tr></thead><tbody id="bodydata"></tbody></table>');

    }
    $('#datatable').DataTable();
}

var insertdata = function () {
    if (!$('#form1').valid()) {
        sweetAlert('Validation', 'Please Insert Required Fields !', 'error');
        return false;
    }
    var uname = $('#txtuname').val();
    var address = $('#txtaddress').val();
    var cnic = $('#txtnic').val();
    var roles = $('#ddlroles').val();
    var email = $('#txtemail').val();
    var isactive = ($('#chisactive').is(':checked') ? "True" : "False");
    var contact = $('#txtcnum').val();
    var pass = $('#txtpass').val();
    $.ajax({
        type: "POST",
        url: "../WCF/Service1.svc/Users/InsertUsers",
        contentType: "application/json; charset=utf-8",
        data: "{'UserName':'" + uname + "','CNIC':'" + cnic + "','Email':'" + email + "','IsActive':'" + isactive + "','RoleID':'" + roles + "','Address':'" + address + "','ContactNumber':'" + contact + "','Password':'" + pass + "'}",
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

var OnEditClick = function (id) {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Users/GetUserByID/" + id,
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
    $('#txtuname').val(msg.UserName);
    $('#txtnic').val(msg.CNIC);
    $('#txtpass').val(msg.Password);
    $('#txtemail').val(msg.Email);
    $('#txtaddress').val(msg.Address);
    $('#ddlroles').multiSelect('select', msg.RoleID);
    //  $('#ddlroles').val();
    $('#txtcnum').val(msg.ContactNumber);
    $('#txthiddenid').val(msg.UserID);

    $('#btnUpdate').show();
    $('#btncancel').show();
    $('#btnInsert').hide();

}

var UpdateData = function () {
    if (!$('#form1').valid()) {
        sweetAlert('Validation', 'Please Insert Required Fields !', 'error');
        return false;
    }
    var uname = $('#txtuname').val();
    var address = $('#txtaddress').val();
    var cnic = $('#txtnic').val();
    var roles = $('#ddlroles').val();
    var email = $('#txtemail').val();
    var isactive = ($('#chisactive').is(':checked') ? "True" : "False");
    var contact = $('#txtcnum').val();
    var pass = $('#txtpass').val();
    var id = $('#txthiddenid').val();
    $.ajax({
        type: "POST",
        url: "../WCF/Service1.svc/Users/UpdateUsers",
        contentType: "application/json; charset=utf-8",
        data: "{'UserName':'" + uname + "','CNIC':'" + cnic + "','Email':'" + email + "','IsActive':'" + isactive + "','RoleID':'" + roles + "','Address':'" + address + "','ContactNumber':'" + contact + "','Password':'" + pass + "','UserID':'" + id + "'}",
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

