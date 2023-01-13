$(document).ready(function () {
    FillGrid();
    $('#optionDiv').hide();
    GetLinkingQuestion();
    $('#linkedform').hide();

    var isValidated = $('#form1').validate({
        rules: {
            question: {
                required: true
            },
        }
    });
});
var newrecordid;
var parentans = 0;
var optionid;
// this method will fill grid 
var FillGrid = function () {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Questions/GetALLQuestions",
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
        $('#tableappend').append('<table class="table table-bordered table-striped" id="datatable"> <thead><tr><th></th><th>Question</th><th>Question Type</th><th>Question Order</th><th>IsActive</th><th>Actions</th> </tr></thead><tbody id="bodydata"></tbody></table>');
        if (msg != null) {
            $.each(msg, function (i, option) {
                $('#bodydata').append('<tr><td  class="details-control" abc="' + option.ID + '"></td><td>' + option.Question1 + '</td> <td>' + option.QuestionType + '</td>'
                    + '<td>' + option.QuestionOrder + '</td>'
                    + '<td>' + option.IsActive + ' </td>'
                    + '<td > <a onclick="OnEditClick(' + option.ID + ')" class="on-default edit-row"><i style="Color:#17b1e3" class="fa fa-pencil"></i></a>  &nbsp;&nbsp;&nbsp;'
                    + '<a onclick="OnDeleteClick(' + option.ID + ')"  class="on-default remove-row"><i style="Color:red" class="fa fa-trash-o"></i></a></td> &nbsp;&nbsp;&nbsp;'
                    + '</td>'
                    + '</tr>');
            });
        }
    }
    else {
        $('#tableappend').empty();
        $('#tableappend').append('<table class="table table-bordered table-striped" id="datatable"> <thead><tr> <th>Question</th><th>Order List</th><th>Status</th><th>Control Type</th><th>Survey Name</th><th>Reporting Field</th><th>Actions</th> </tr></thead><tbody id="bodydata"></tbody></table>');
    }
    var table = $('#datatable').DataTable();
    $('#datatable tbody').on('click', 'td.details-control', function () {
                var tr = $(this).closest('tr');
                var row = table.row(tr);

                if (row.child.isShown()) {
                    // This row is already open - close it
                    row.child.hide();
                    tr.removeClass('shown');
                }
                else {
                    // Open this row
                    var id = this.attributes.abc.value;
                    ShowOptions(id, row,tr);
                    //row.child(format(row.data())).show();
                    //tr.addClass('shown');
                }
            });
}
//this method will fill nested options of question in grid
function format(d) {
    // `d` is the original data object for the row

    $('body').append('<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;" id=newtableappend></table>');
    for (var i = 0; i < d.length; i++) {
        $('#newtableappend').append("<tr><td>Options:</td><td>'" + d[i].Answer + "'</td></tr><tr><td>LinkQuestion:</td><td><a onclick='AttachQuestion(" + d[i].ID + ")' class='on-default edit-row'><i style='Color:#17b1e3' class='fa fa-pencil'></i></a></td></tr><tr><td>Edit:</td><td><a onclick='EditOption(" + d[i].ID + "," + d[i].QuestionID + ")' class='on-default edit-row'><i style='Color:#17b1e3' class='fa fa-pencil'></i></a></td></tr><tr><td>Remove Option:</td><td><a onclick='RemoveOptionData(" + d[i].ID + ")'class='on-default remove-row'><i style='Color:red' class='fa fa-trash-o'></i></a></td></tr>");
    }

    var data = $('#newtableappend').prop('innerHTML');
    var newdata = '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' + data + '</table>';
     $('#newtableappend').empty();
     return newdata;
}
var ShowOptions = function (id,row,tr) {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Questions/GetOptionsbyQuestionID/" + id,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {
            if (response.Status == 200) {
                var msg = jQuery.parseJSON(response.Data);
                row.child(format(msg)).show();
               // $('#newtableappend').empty();
                tr.addClass('shown');
            }
        },
        cache: false
    });
}
// this method will show questions to link form
var AttachQuestion = function (id) {
    parentans = id;
    $('#Insertform').fadeOut();
    $('#linkedform').fadeIn();
    $('html,body').animate({
        scrollTop: $("#linkedform").offset().top
    },
            'slow');

}
// this div is gonna clearfield from question div.
var clearFields = function (){
    $('#txtquestion').val("");
    $('#txtol').val("");
    $('#ddlControl').multiSelect('select', -1);
    $('#txthiddenid').val("");
    $('#Chstatus').prop('checked', false);
    $('#btnUpdate').hide();
    $('#btncancel').hide();
    $('#btnInsert').show();
}
// this method will delete questions
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
        url: "../WCF/Service1.svc/Questions/DeleteQuestion/" + id,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {
            if (response.Status == 200) {
                swal("Deleted!", "Data has been deleted.", "success");
                FillGrid();
            }
            else {
                swal("Error!",""+response.Message+"", "error");
              
            }
            
        },
        cache: false
    });
});
}
// this method will delete options of question
var RemoveOptionData = function (id) {
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
        url: "../WCF/Service1.svc/Questions/DeleteOption/" + id,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {
            if (response.Status == 200) {
                swal("Deleted!", "Data has been deleted.", "success");
                FillGrid();
            }
            else {
                swal("Error!", "" + response.Message + "", "error");
                swal("Deleted!", "Data has been deleted.", "success");
            }

        },
        cache: false
    });
});
}
// this method will get details of selected question 
var OnEditClick = function (id) {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Questions/GetQuestionDetails/" + id,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: OnsuccessEdit,
        cache: false
    });
}
var OnsuccessEdit = function (response) {
    var msg = $.parseJSON(response.Data);

    if (msg.IsActive == true) {
        $('#Chstatus').prop('checked', true);
    } else {
        $('#Chstatus').prop('checked', false);
    }
    if (msg.IsReportingField == true) {
        $('#Chisreportingfield').prop('checked', true);
    } else {
        $('#Chisreportingfield').prop('checked', false);
    }
    $('#txtol').val(msg.QuestionOrder);
    $('#txtquestion').val(msg.Question1);
    $('#ddlControl').multiSelect('select', msg.QuestionType);

    $('#txthiddenid').val(msg.ID);
    $('#btnUpdate').show();
    $('#btncancel').show();
    $('#btninsert').hide();

}
// this method will insert question
var InsertQuestions = function ()  {
    if (!$('#form1').valid()) {
        sweetAlert('Validation', 'Please Insert Required Fields !', 'error');
        return false;
    }
    if ($('#ddlControl').val() == '-1') {
        sweetAlert('Validation', 'Please Insert Required Fields !', 'error');
        return false;
    }
    var ddlcontrol = $('#ddlControl').val();
    var orderlist = $('#txtol').val();
    var isactive = ($('#Chstatus').is(':checked') ? "True" : "False");
    var ques = $('#txtquestion').val();
    var reportingfield = ($('#Chisreportingfield').is(':checked') ? "True" : "False");
    $.ajax({
        type: "POST",
        url: "../WCF/Service1.svc/Questions/InsertQuestion",
        contentType: "application/json; charset=utf-8",
        data: "{'Question1':'" + ques + "','QuestionType':'" + ddlcontrol + "','QuestionOrder':'" + orderlist + "','IsActive':'" + isactive + "','IsReportingField':'"+reportingfield+"'}",
        async: false,
        success: OnsuccessInsert,
        cache: false
    });
}
var OnsuccessInsert = function (response) {
    if (response.Status == 200) {
        swal("Inserted!", "Data Inserted Successfully!", "success");
     
        FillGrid();
        newrecordid = response.NewRecordID;
        $('#optionDiv').show();
        $('html,body').animate({
            scrollTop: $("#optionDiv").offset().top
        },
            'slow');
    }
    else {
        swal("Sorry!", "Something Went Wrong", "error");
    }
}
//this method will clear data from option div
var optionclearField = function () {
    var inputCount = document.getElementById('optionsform').getElementsByTagName('input').length;
    if (inputCount != 1) {
        for (var i = inputCount; i > 1 ; i--) {
            $('#a' + i + '').remove();
        }
    }
    $('#txtoption1').val('');
    $('#btnoUpdate').hide();
    $('#btnocancel').hide();
    $('#btninsertOperation').show();
}

//Insert Options
var InsertOptions = function () {
    var msg;
    var res;
    var inputCount = document.getElementById('optionsform').getElementsByTagName('input').length;
    for (var i = 1; i < inputCount + 1; i++) {
        var option = $('#txtoption' + i + '').val();
        var isinput = (option == 'InputArea') ? 'True' : 'False';
        $.ajax({
            type: "POST",
            url: "../WCF/Service1.svc/Questions/InsertQuestionOperation",
            contentType: "application/json; charset=utf-8",
            data: "{'QuestionID':'" + newrecordid + "','Answer':'" + option + "','IsInput':'" + isinput + "','IsActive':'true'}",
            async: false,
            success: function(response){
                msg = response.Message;
                res = response.Status
        },
            cache: false
        });
    }
    OnsuccessOptionsInsert(msg, res);
}
var OnsuccessOptionsInsert = function (msg,res) {
    if (res == 200) {
        swal("Inserted!", "Data Inserted Successfully!", "success");
        FillGrid();
        $('#optionDiv').hide();
    }
    else {
        swal("Sorry!", "Something Went Wrong", "error");
    }
    clearFields();
    optionclearField();
}
//This method will remove option textboxes
var btnremoveoptions = function () {
    var inputCount = document.getElementById('optionsform').getElementsByTagName('input').length;
    if (inputCount != 1) {
        $('#a' + inputCount + '').remove();
    }
}
//This method will Add option textboxes
var btnaddoptions = function (){
    var inputCount = document.getElementById('optionsform').getElementsByTagName('input').length;
    var validateoddeven = inputCount % 2;
    if (validateoddeven == 0) {
        $('#odd').append('<div class="form_row" id="a'+(inputCount+1)+'"><div class="input"> <input placeholder="Fill out Option" name="txtoption" id="txtoption' + (inputCount + 1) + '" />  <label class="label" >Option: <span class="blue">*</span></label>  </div></div>');
        colorlabel();
    }
    else {
        $('#even').append('<div class="form_row" id="a' + (inputCount + 1) + '"><div class="input"> <input placeholder="Fill out Option" name="txtoption" id="txtoption' + (inputCount + 1) + '" />  <label class="label" >Option: <span class="blue">*</span></label>  </div></div>');
        colorlabel();
    }
 //   alert(inputCount);
}
//this method will Color the Label when you click on it
var colorlabel = function () {
    $(function () {
        $('input, textarea').each(function () {
            $(this).on('focus', function () {
                $(this).parent('.input').addClass('active');
            });

            $(this).on('blur', function () {
                if ($(this).val().length == 0) {
                    $(this).parent('.input').removeClass('active');
                }
            });
            if ($(this).val() != '') $(this).parent('.input').addClass('active');
        });
    });

}

// this method will link the question with its parent question.
var LinkedQuestionInsert = function () {
    var msg;
    var status;
    var selected = [];
    $('#countquestion input:checked').each(function () {
        selected.push($(this).attr('ques'));
    });
    var qid ;
    for (var i = 0; i < selected.length; i++) {
        qid = selected[i];
        $.ajax({
            type: "POST",
            url: "../WCF/Service1.svc/Questions/InsertLinkedQuestion",
            contentType: "application/json; charset=utf-8",
            data: "{'PAnswerID':'" + parentans + "','QuestionID':'" + qid + "','IsActive':'true'}",
            async: false,
            success: function (response) {
                if (response.Status == 200) {
                    msg = response.Message;
                    status = response.Status;
                } else {
                  //  swal("Sorry!", ""+response.Message+"", "error");
                }
            },
            cache: false
        });
    }
    OnsuccessLinking(msg,status)
   
}
var OnsuccessLinking = function (msg,status) {
    if (status == 200) {
        swal("Success!", "Question Linked Successfully", "success");
        $('#countquestion').find('input[type=checkbox]:checked').removeAttr('checked');
    }
    else {
        swal("Sorry!", "Something Went Wrong", "error");
    }
   
    //$('#Insertform').fadeIn();
    //$('#linkedform').fadeOut();
}

// this method will get all questions from database
var GetLinkingQuestion = function () {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Questions/GetQuestionByOrderList",
        contentType: "application/json; charset=utf-8",
        //data: "{'PAnswerID':'" + parentans + "','QuestionID':'" + newrecordid + "','IsActive':'true'}",
        async: false,
        success: OnsuccessLinkingQuestion,
        cache: false
    });
}
var OnsuccessLinkingQuestion = function (response) {
    if (response.Status == 200) {
        var msg = $.parseJSON(response.Data);
        $.each(msg, function (i, option) {
            $('#printquestion').append("<tr><td><input type='checkbox' ques='"+option.ID+"' /></td><td>" + option.Question1 + "</td></tr>");
        });
    }
}

// this method will update question details
var UpdateQuestions = function () {
    if (!$('#form1').valid()) {
        sweetAlert('Validation', 'Please Insert Required Fields !', 'error');
        return false;
    }
    if ($('#ddlControl').val() == '-1'){
        sweetAlert('Validation', 'Please Insert Required Fields !', 'error');
        return false;
    }

    var ddlcontrol = $('#ddlControl').val();
    var orderlist = $('#txtol').val();
    var isactive = ($('#Chstatus').is(':checked') ? "True" : "False");
    var ques = $('#txtquestion').val();
    var id = $('#txthiddenid').val();
    var isreportfield = ($('#Chisreportingfield').is(':checked') ? "True" : "False");
    $.ajax({
        type: "POST",
        url: "../WCF/Service1.svc/Questions/UpdateQuestion",
        contentType: "application/json; charset=utf-8",
        data: "{'Question1':'" + ques + "','QuestionType':'" + ddlcontrol + "','QuestionOrder':'" + orderlist + "','IsActive':'" + isactive + "','ID':'"+id+"','IsReportingField':'"+isreportfield+"'}",
        async: false,
        success: OnsuccessUpdateQuestion,
        cache: false
    });
}
var OnsuccessUpdateQuestion = function (response) {
    if (response.Status == 200) {
        swal("Updated!", "Data Updated Successfully!", "success");
        FillGrid();
    }
    else {
        swal("Sorry!", "Something Went Wrong", "error");
    }
    clearFields();
}

//this method will get selected options value
var EditOption = function (optionid, questionid) {

    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Questions/GetOptionsDetails/"+optionid+"/"+questionid,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: OnSuccessEditOption,
        cache: false
    });
}
var OnSuccessEditOption = function (response) {
    if (response.Status == 200) {
        var msg = $.parseJSON(response.Data);
        $('#optionDiv').show();
        $('html,body').animate({
            scrollTop: $("#optionDiv").offset().top
        },
            'slow');
        $('#txtoption1').val(msg.Answer);
        $('#txthiddenid').val(msg.ID);
        newrecordid = msg.QuestionID;
        $('#btnadd').hide();
        $('#btnremove').hide()

        $('#btnoUpdate').show();
        $('#btnocancel').show();
        $('#btninsertOperation').hide();
    }
}
// this method will update options
var UpdateOption = function () {
    var option = $('#txtoption1').val();
    var isinput = (option == 'InputArea') ? 'True' : 'False';
    var id = $('#txthiddenid').val();
        $.ajax({
            type: "POST",
            url: "../WCF/Service1.svc/Questions/UpdateOption",
            contentType: "application/json; charset=utf-8",
            data: "{'QuestionID':'" + newrecordid + "','Answer':'" + option + "','IsInput':'" + isinput + "','IsActive':'true','ID':"+id+"}",
            async: false,
            success: function (response) {
                if (response.Status == 200) {
                    swal("Updated!", "Data Updated Successfully!", "success");
                    $('#btnadd').show();
                    $('#btnremove').show();
                    FillGrid();
                    optionclearField();
                    $('#optionDiv').hide();
                }
                else {
                    swal("Sorry!", "Something Went Wrong", "error");
                }
                
            },
            cache: false
        });

    }
