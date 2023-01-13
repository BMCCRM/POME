$(document).ready(function () {
    var Hospitals = [
        {
            HospitalName: "Select Hospital",
            HospitalID: 0,
            PharmacistIDS: []
        }
    ];
    var Pharmacists = [
        {
            PharmacistName: "Select Pharmacist",
            PharmacistID: 0,
            HospitalID: 0
        }
    ];
    GetHospitalforddl();
    GetRepresentatorforddl();
    $("#datepicker").datepicker({
        format: "mm/dd/yyyy"
    });
    $("#datepicker").datepicker("setDate", new Date());
    $("#datepickerforExport").datepicker("setDate", new Date());

    $('input[type=radio][name=group0]').change(function () {
        if (this.value == 'Date') {
            $('#datepicker').val('').datepicker('remove');
            $("#datepicker").datepicker({
                format: "mm/dd/yyyy"
            });
            $("#datepicker").datepicker("setDate", new Date());
            $("#datepickerforExport").datepicker("setDate", new Date());
        }
        else if (this.value == 'Month') {
            $('#datepicker').val('').datepicker('remove');
            $("#datepicker").datepicker({
                format: "mm/yyyy",
                startView: "months",
                minViewMode: "months"
            });
            $("#datepicker").datepicker("setDate", new Date());
            $("#datepickerforExport").datepicker("setDate", new Date());
        }
    });
    FillGrid();
});

function FillGrid() {
    var date = $('#datepicker').val();
    var hospitalid = $('#ddlhospitals').val();
    var representator = $('#ddlrepresentator').val();
    var heartburn = $('#ddlheartburn').val();
    var pregnentpatient = $('#ddlPatient').val();
    var day = 0;
    var month = 0;
    var year = 0;
    if (date != "") {
        var DateMonthly = $("input:radio[name ='group0']:checked").val();
        if (DateMonthly == "Date") {
            month = date.split('/')[0];
            day = date.split('/')[1];
            year = date.split('/')[2];
        }
        else if (DateMonthly == "Month") {
            day = 0;
            month = date.split('/')[0];
            year = date.split('/')[1];
        }
        else {
            var d = new Date();
            day = d.getDate();
            month = d.getMonth() + 1;
            year = d.getFullYear();
        }
    }
    else {
        var d = new Date();
        day = d.getDate();
        month = d.getMonth() + 1;
        year = d.getFullYear();
    }
    $.ajax({
        type: "Post",
        url: "DashboardService.asmx/GetReportData",
        data: "{'day':'" + day + "','month':'" + month + "','year':'" + year + "','HospitalID':'" + hospitalid + "','Representator':'" + representator + "','heartburn':'" + heartburn + "','PregnentPatient':'" + pregnentpatient + "'}",
        contentType: "application/json; charset=utf-8",
        async: true,
        success: OnSuccessFillGrid,
        cache: false
    });
}
function OnSuccessFillGrid(response) {
    if (response.d != "") {

        var msg = jQuery.parseJSON(response.d);
        $('#tableappend').empty();
        $('#tableappend').append('<table class="table table-bordered table-striped" id="datatable"> <thead><tr> <th>Patient Name</th><th>Age</th><th>City</th><th>Contact #</th><th>Number Of Children</th><th>Visit #</th><th>HeartBurn ?</th> <th>Pregnant Patient ?</th><th>Medicine</th><th>Referred By</th><th>Hospital</th><th>Representative</th></tr></thead><tbody id="bodydata"></tbody></table>');
        if (msg != null) {
            $.each(msg, function (i, option) {
                var HospitalObject = Hospitals.filter( item => (item.HospitalID).toString() === option.hospitalID );
                var PatientObject = Pharmacists.filter( item => (item.PharmacistID).toString() === option.representativeID );

                $('#bodydata').append('<tr><td>' + option.PatientName + '</td> <td>' + option.Age + '</td>'
                    //+'<td>' + option.Email + '</td>' 
                    + '<td>' + option.City + '</td>'
                    + '<td>' + option.Contact + ' </td>'
                    + '<td>' + option.NoOfChildrens + '</td>'
                    + '<td>' + option.VisitNumber + '</td>'
                    + '<td>' + option.HeartBurnAnswer + ' </td>'
                    + '<td>' + option.PregnentAnswer + ' </td>'
                    + '<td>' + option.MedicationAnswer + ' </td>'
                    + '<td>' + option.PrescribeAnswer + ' </td>'
                    + '<td>' + HospitalObject[0].HospitalName + ' </td>'
                    + '<td>' + PatientObject[0].PharmacistName + ' </td>'
                    + '</tr>');
            });
        }
    }
    else {
        $('#tableappend').empty();
        $('#tableappend').append('<table class="table table-bordered table-striped" id="datatable"> <thead><tr> <th>Patient Name</th><th>Age</th><th>City</th><th>Contact #</th><th>Number Of Children</th><th>Visit #</th><th>Pregnant Patient ?</th> <th>HeartBurn ?</th><th>Medicine</th><th>Referred By</th><th>Hospital</th><th>Representative</th></tr></thead><tbody id="bodydata"></tbody></table>');
    }
    //$('#datatable').DataTable();
    $('#datatable').DataTable( {
        dom: 'Bfrtip',
        buttons: [
            'excel',
            //'pdf'
            //for large data to be exported on pdf
            {
                extend: 'pdf',
                orientation: 'landscape',
                pageSize: 'LEGAL',
                title: 'ExportSurveyDetails'
            }
        ]
    } );
}

function GetHospitalforddl() {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Hospitals/GetAllHospitalsWithPharmacistsID",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: OnsuccessHospitalDDL,
        cache: false
    });
}
function OnsuccessHospitalDDL(response) {
    var msg = jQuery.parseJSON(response.Data);
    if (response.Status == 200) {
        Hospitals = [];
        Hospitals.push({
            HospitalName: "Select Hospital",
            HospitalID: 0,
            PharmacistIDS: []
        });
        $.each(msg, function (i, option) {
            var flag = true;
            var len = Hospitals.length;
            for (var i = 0; i < len; i++) {
                if(Hospitals[i].HospitalName == option.HospitalName)
                {
                    Hospitals[i].PharmacistIDS.push(option.PharmacistID);
                    flag = false;
                }
            }
            if (flag)
            {
                Hospitals.push({
                    HospitalName: option.HospitalName,
                    HospitalID: option.ID,
                    PharmacistIDS: [option.PharmacistID]
                });
            }
        });
        $('#ddlhospitals').empty();
        //$('#ddlhospitals').append('<option value="0">Select Hospital</option>');
        $.each(Hospitals, function (i, option) {
            $('#ddlhospitals').append('<option value="' + option.HospitalID + '">' + option.HospitalName + '</option>');
        });

        $("#ddlhospitals").change(function () {
            var HospitalObject = Hospitals.filter( item => (item.HospitalID).toString() === $("#ddlhospitals").val() );
            if(HospitalObject.length != 0 && $("#ddlhospitals").val() != "0")
            {
                $('#ddlrepresentator').empty();
                $('#ddlrepresentator').append('<option value="0">Select Pharmacist</option>');
                $.each(HospitalObject[0].PharmacistIDS, function (i, option) {
                    var PharmacistObject = Pharmacists.filter( item => item.PharmacistID === option );
                    if(PharmacistObject.length != 0)
                    {
                        $('#ddlrepresentator').append('<option value="' + PharmacistObject[0].PharmacistID + '">' + PharmacistObject[0].PharmacistName + '</option>');
                    }
                });
                $('#ddlrepresentator').select2("val", "0");
            }
            else
            { 
                $('#ddlrepresentator').empty();
                $.each(Pharmacists, function (i, option) {
                    $('#ddlrepresentator').append('<option value="' + option.PharmacistID + '">' + option.PharmacistName + '</option>');
                });
                $('#ddlrepresentator').select2("val", "0");
            }
        });

    }
}

function GetRepresentatorforddl() {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Pharmacist/GetAllPharmacist",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: OnsuccessRepresentatorDDL,
        cache: false
    });
}
function OnsuccessRepresentatorDDL(response) {
    var msg = jQuery.parseJSON(response.Data);
    if (response.Status == 200) {
        Pharmacists = [];
        Pharmacists.push({
                PharmacistName: "Select Pharmacist",
                PharmacistID: 0,
                HospitalID: 0
            });
        $.each(msg, function (i, option) {
            var flag = true;
            var len = Pharmacists.length;
            for (var i = 0; i < len; i++) {
                if (Pharmacists[i].PharmacistName == option.Name) {
                    Pharmacists[i].HospitalID=option.HospitalID;
                    flag = false;
                }
            }
            if (flag) {
                Pharmacists.push({
                    PharmacistName: option.Name,
                    PharmacistID: option.ID,
                    HospitalID: option.HospitalID
                });
            }
        });
        $('#ddlrepresentator').empty();
        //$('#ddlrepresentator').append('<option value="0">Select Pharmacist</option>');
        $.each(Pharmacists, function (i, option) {
            $('#ddlrepresentator').append('<option value="' + option.PharmacistID + '">' + option.PharmacistName + '</option>');
        });

        $("#ddlrepresentator").change(function () {
            var PatientObject = Pharmacists.filter( item => (item.PharmacistID).toString() === $("#ddlrepresentator").val() );
            if(PatientObject.length != 0 && $("#ddlrepresentator").val() != "0")
            {
               // $("#ddlhospitals").val(PatientObject[0].HospitalID);
                $('#ddlhospitals').select2("val", PatientObject[0].HospitalID);
            }
        });

    }
}