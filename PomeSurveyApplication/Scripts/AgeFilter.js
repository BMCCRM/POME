$(document).ready(function () {
    fillgrid();

    var isValidated = $('#form1').validate({
        rules: {
            txtage: {
                required: true
            }
        }
    });

});

var fillgrid = function ()
{
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/AgeFilter/GetAgeFilter",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: onsuccessFillGrid,
        cache: false
    });
}
var onsuccessFillGrid = function (response) {
    var msg = jQuery.parseJSON(response.Data);
    $('#tableappend').empty();
    $('#tableappend').append('<table class="table table-bordered table-striped" id="datatable"> <thead><tr> <th>Age Filters</th><th>Actions</th> </tr></thead><tbody id="bodydata"></tbody></table>');
    if (msg != null) {
        $.each(msg, function (i, option) {
            $('#bodydata').append('<tr><td>' + option.Range + '</td> '
                + '<td >  <a href="#" onclick="OnEditClick(' + option.ID + ')" class="on-default edit-row"><i style="Color:#17b1e3" class="fa fa-pencil"></i></a>  &nbsp;&nbsp;&nbsp;'
                          + '<a href="#" onclick="OnDeleteClick(' + option.ID + ')" class="on-default remove-row"><i style="Color:red" class="fa fa-trash-o"></i></a></td>'
                + '</tr>');
        });
    }
    try {
        if (userrole == "Agency") {
            $('#ModelForm').hide();
            $('#datatable').DataTable({ 'columns': [null, { 'visible': false }] });
        }
    } catch (e) {
        $('#datatable').DataTable();
    }
}

var clearFields = function () {
    $('#txtage').val("");
    $('#txthiddenid').val("");

    $('#chisactive').prop('checked', false);
    $('#btnUpdate').hide();
    $('#btncancel').hide();
    $('#btnInsert').show();

}

var InsertHospitalData = function () {
    if (!$('#form1').valid()) {
        sweetAlert('Validation', 'Please Insert Required Fields !', 'error');
        return false;
    }
    var range = $('#txtage').val();
    $.ajax({
        type: "POST",
        url: "../WCF/Service1.svc/AgeFilter/InsertAgeFilter",
        data: "{'Range':'" + range + "'}",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: onsuccessInsert,
        cache: false
    });

}
var onsuccessInsert = function (response) {
    if (response.Status == 200) {
        swal("Inserted!", "Data Inserted Successfully!", "success");
        fillgrid();
    }
    else {
        swal("Sorry!", "Something Went Wrong ", "error");
    }
    clearFields();
}

var UpdateHospitalData = function () {
    var range = $('#txtage').val();
    var id = $('#txthiddenid').val();
  
    $.ajax({
        type: "POST",
        url: "../WCF/Service1.svc/AgeFilter/UpdateAgeFilter",
        data: "{'Range':'" + range + "','ID':'" + id + "'}",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: onsuccessUpdate,
        cache: false
    });
}
var onsuccessUpdate = function (response) {
    if (response.Status == 200) {
        swal("Updated!", "Data Updated Successfully!", "success");
        fillgrid();
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
        url: "../WCF/Service1.svc/AgeFilter/DeleteFilter/" + id,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function () {
            swal("Deleted!", "Data has been deleted.", "success");
            fillgrid();
        },
        cache: false
    });
});
}

var OnEditClick = function (id) {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/AgeFilter/GetFilterByID/" + id,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: OnsuccessEdit,
        cache: false
    });
}
var OnsuccessEdit = function (response) {
    var msg = $.parseJSON(response.Data);

    $('#txtage').val(msg.Range);
    $('#txthiddenid').val(msg.ID);
  
    $('#btnUpdate').show();
    $('#btncancel').show();
    $('#btnInsert').hide();
}