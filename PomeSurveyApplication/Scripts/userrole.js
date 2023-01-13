$(document).ready(function () {
    FillGrid();
    clearFields();

    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[a-zA-Z\s]+$/.test(value);
    }, "Letters only please");
    var isValidated = $('#form1').validate({
        rules: {
            txtrolename: {
                required: true,
                lettersonly: true
            }
        }
    });
});

var FillGrid = function () {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Roles/GetAllUserRoles",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: onsuccessFillGrid,
        cache: false
    });
}

var onsuccessFillGrid = function (response) {
    if (response.Data != "") {

        var msg = jQuery.parseJSON(response.Data);
        $('#tableappend').empty(); 
        $('#tableappend').append('<table class="table table-bordered table-striped" id="datatable"> <thead><tr> <th>User Roles</th><th>IsActive</th><th>Actions</th> </tr></thead><tbody id="bodydata"></tbody></table>');
        if (msg != null) {
            $.each(msg, function (i, option) {
                $('#bodydata').append('<tr><td>' + option.RoleName + '</td> <td>' + option.IsActive + '</td>'
                    + '<td > <a onclick="OnEditClick(' + option.RoleID + ')" class="on-default edit-row"><i style="Color:#17b1e3"  class="fa fa-pencil"></i></a>  &nbsp;&nbsp;&nbsp;'
                    + '<a onclick="OnDeleteClick(' + option.RoleID + ')"  class="on-default remove-row"><i style="Color:red" class="fa fa-trash-o"></i></a></td>'
                    + '</tr>');
            });
        }
    }
    else {
        $('#tableappend').empty();
        $('#tableappend').append('<table class="table table-bordered table-striped" id="datatable"> <thead><tr> <th>User Roles</th><th>IsActive</th><th>Actions</th> </tr></thead><tbody id="bodydata"></tbody></table>');

    }
    $('#datatable').DataTable();
}

var insertdata = function () {
    if (!$('#form1').valid()) {
        sweetAlert('Validation', 'Please Insert Required Fields !', 'error');
        return false;
    }
        var name = $('#txtrolename').val();
        var city = ($('#chisactive').is(':checked')) ? 'True':'False'

       $.ajax({
            type: "POST",
            url: "../WCF/Service1.svc/Roles/InsertUserRoles",
            contentType: "application/json; charset=utf-8",
            data: "{'RoleName':'" + name + "','IsActive':'" + city + "'}",
            async: false,
            success: OnsuccessInsert,
            cache: false
        });
}

var UpdateData = function () {
    if (!$('#form1').valid()) {
        sweetAlert('Validation', 'Please Insert Required Fields !', 'error');
        return false;
    }
    var name = $('#txtrolename').val();
    var city = ($('#chisactive').is(':checked')) ? 'True' : 'False'
    var id = $('#txthiddenid').val();

    $.ajax({
        type: "POST",
        url: "../WCF/Service1.svc/Roles/UpdateUserRoles",
        contentType: "application/json; charset=utf-8",
        data: "{'RoleName':'" + name + "','IsActive':'" + city + "','RoleID':'" + id + "'}",
        async: false,
        success: OnsuccessUpdate,
        cache: false
    });
}

var OnsuccessUpdate = function (response) {
    if (response.Status == 200) {

        swal("Updated!", "Data Updated Successfully!", "success");
        FillGrid()
        $('#controlButton').html('Insert');
    } else {
        swal("Sorry!", "Something Went Wrong ", "error");
    }
    clearFields();
}

var OnsuccessInsert = function(response) {
    if (response.Status == 200) {
        swal("Inserted!", "Data Inserted Successfully!", "success");
        FillGrid()
    }
    else {
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
        url: "../WCF/Service1.svc/Roles/DeleteRoles/" + id,
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

var clearFields = function ()
{
    $('#txtrolename').val("");
    $('#chisactive').prop('checked', false);
    $('#btnUpdate').hide();
    $('#btncancel').hide();
    $('#btnInsert').show();
}

var OnEditClick = function (id) {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Roles/GetRolesByID/" + id,
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
    $('#txtrolename').val(msg.RoleName);
    
    if (msg.IsActive == true) {
        $('#chisactive').prop('checked', true);
    } else {
        $('#chisactive').prop('checked', false);
    }
    $('#txthiddenid').val(msg.RoleID);
    $('#btnUpdate').show();
    $('#btncancel').show();
    $('#btnInsert').hide();
  
}