$(document).ready(function () {
    // GetPatientCount();
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
    if (userrole == 'Doctor') {
        $('#ddlhos').hide();
    }
    GetHospitalforddl();
    FillddlAge();
    GetTotalPatientsByQuestion();
   
    
});



function printDiv() {
    //$('#btnchopja').click();
    //$('#DivIdToPrint').printThis();
    //var divToPrint = document.getElementById('DivIdToPrint');

    //var newWin = window.open('', 'Print-Window');

    //newWin.document.open();

    //newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>'); 

    //newWin.document.close();

    //setTimeout(function () { newWin.close(); }, 10);

    //  $("#print_button2").click(function () {
    var mode = 'iframe'; // popup
    var close = mode == "popup";
    var options = { mode: mode, popClose: close };
    $("#DivIdToPrint").printArea(options);

    //  });
}

function GetPatientCount() {
    var date = $('#datepicker').val();
    var hospitalid = $('#ddlhospitals').val();
    var ageid = $('#ddlage').val();
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
        url: "DashboardService.asmx/GetPatientCount",
        contentType: "application/json; charset=utf-8",
        data: "{'day':'" + day + "','month':'" + month + "','year':'" + year + "','HospitalID':'" + hospitalid + "','AgeID':'" + ageid + "'}",
        async: true,
        success: OnSuccessGetPatientCount,
        cache: false
    });
}

function OnSuccessGetPatientCount(data, status) {
    if (data.d != null) {
        $('#totalnopatients').empty();
        var msg = jQuery.parseJSON(data.d);
        $.each(msg, function (i, option) {
            $('#totalnopatients').append(option.totalpatients);
        });
    }
}

function GetTotalPatientsByQuestion() {
    var date = $('#datepicker').val();
    var hospitalid = $('#ddlhospitals').val();
    var ageid = $('#ddlage').val();
    var day = 0;
    var month = 0;
    var year = 0;
    if (date != "")
    {
        var DateMonthly = $("input:radio[name ='group0']:checked").val();
        if (DateMonthly == "Date")
        {
            month = date.split('/')[0];
            day = date.split('/')[1];
            year = date.split('/')[2];
        }
        else if (DateMonthly == "Month")
        {
            day = 0;
            month = date.split('/')[0];
            year = date.split('/')[1];
        }
        else
        {
            var d = new Date();
            day = d.getDate();
            month = d.getMonth() + 1;
            year = d.getFullYear();
        }
    }
    else
    {
        var d = new Date();
        day = d.getDate();
        month = d.getMonth() + 1;
        year = d.getFullYear();
    }
    $.ajax({
        type: "Post",
        url: "DashboardService.asmx/GetTotalPatientsByQuestion",
        data: "{'day':'" + day + "','month':'" + month + "','year':'" + year + "','HospitalID':'" + hospitalid + "','AgeID':'" + ageid + "'}",
        contentType: "application/json; charset=utf-8",
        async: true,
        success: OnSuccessGetTotalPatientsByQuestion,
        cache: false
    });
    GetPatientCount();
}

function OnSuccessGetTotalPatientsByQuestion(data, status) {
    if (data.d != null) {
        var data1 = [];
        var data2 = [];
        var data3 = [];
        var data4 = [];
        var data5 = [];
        var data6 = [];
        var data7 = [];
        var data8 = [];
        var data9 = [];
        var data10 = [];
        var data11 = [];
        var name11 = [];
        var data12 = [];
        var data13 = [];

        var totalRemedies = 0;
        var totalnopatients1 = 0;
        var totalnopatients6 = 0;
        var totalnopatients7 = 0;
        var totalnopatients8 = 0;
        var totalnopatients9 = 0;
        var totalnopatients10 = 0;
        var totalnopatients11 = 0;
        var totalnopatients12 = 0;
        var totalnopatients14 = 0;
        var msg = jQuery.parseJSON(data.d);
        $.each(msg, function (i, option) {
            if (option.Question == 'Are you pregnant?') {
                data1.push({
                    name: option.Answer,
                    y: parseInt(option.totalpatients)
                });
                if (option.Answer == 'Yes') {
                    totalnopatients1 = option.totalpatients;
                }
            } else if (option.Question == 'Reason for visit?') {
                data2.push([
                    option.Answer, parseInt(option.totalpatients)
                ]);
            } else if (option.Question == 'Do you smoke or consume tobacco?') {
                if (option.Answer == 'Yes') {
                    totalnopatients6 = option.totalpatients;
                } else {
                    totalnopatients6 = 0;
                }
            } else if (option.Question == 'Do you consume alcohol?') {
                if (option.Answer == 'Yes') {
                    totalnopatients7 = option.totalpatients;
                }
            } else if (option.Question == 'Do you consume spicy foods?') {
                if (option.Answer == 'Yes') {
                    totalnopatients8 = option.totalpatients;
                }
            } else if (option.Question == 'Do you consume fried & oily foods?') {
                if (option.Answer == 'Yes') {
                    totalnopatients9 = option.totalpatients;
                }
            } else if (option.Question == 'Do you consume fruits and vegetables?') {
                if (option.Answer == 'Yes') {
                    totalnopatients10 = option.totalpatients;
                }
            } else if (option.Question == 'Do you consume tea/coffee or carbonated drinks?') {
                if (option.Answer == 'Yes') {
                    totalnopatients11 = option.totalpatients;
                }
            } else if (option.Question == 'Do you experience Heartburn?') {
                if (option.Answer == 'Yes') {
                    totalnopatients12 = option.totalpatients;
                }
            } else if (option.Question == 'Have you complained about it to the doctor?') {
                if (option.Answer == 'Yes') {
                    totalnopatients14 = option.totalpatients;
                }
            } else if (option.Question == 'Frequency of complain?') {
                data3.push([
                    option.Answer, parseInt(option.totalpatients)
                ]);
            } else if (option.Question == 'Is there a specific time when it occurs?') {
                data4.push({
                    name: option.Answer,
                    y: parseInt(option.totalpatients)
                });
            } else if (option.Question == 'How was your pregnancy confirmed?') {
                data5.push([
                    option.Answer, parseInt(option.totalpatients)
                ]);
            } else if (option.Question == 'Are you suffering from any other disease?') {
                data6.push({
                    name: option.Answer,
                    y: parseInt(option.totalpatients)
                });
            //}// else if (option.Question == 'How do you know it is Heartburn?') {
            //    data12.push({
            //        name: option.Answer,
            //        y: parseInt(option.totalpatients)
            //    });
            } else if (option.Question == 'Did you suffer from Heartburn even before pregnancy? If yes, has it become better or worse now?') {
                data13.push({
                    name: option.Answer,
                    y: parseInt(option.totalpatients)
                });
            } else if (option.Question == 'Is your Heartburn aggravated by any factor?') {
                data7.push([
                    option.Answer, parseInt(option.totalpatients)
                ]);
            } else if (option.Question == 'What do you do to relieve yourself of Heartburn?') {
                
                if (option.Answer == "Home Remedies") {

                }
                else if (option.Answer == "Chewing Gum" || option.Answer == "Baking Soda" || option.Answer == "Banana" || option.Answer == "Apple" || option.Answer == "Almonds" || option.Answer == "Others") {
                    data8.push({
                        name: option.Answer,
                        y: parseInt(option.totalpatients)
                    });
                    totalRemedies += parseInt(option.totalpatients);
                }
                else {
                    data8.push({
                        name: option.Answer,
                        y: parseInt(option.totalpatients)
                    });
                }
            } else if (option.Question == 'Have you been taking any medication for Heartburn?') {
                data9.push([
                    option.Answer, parseInt(option.totalpatients)
                ]);
            } else if (option.Question == 'Have you been experiencing any other problem due to or during pregnancy?') {
                data10.push({
                    name: option.Answer,
                    y: parseInt(option.totalpatients)
                });
            } else if (option.Question == 'Duration of current pregnancy?') {
                name11.push([option.Answer]);
                data11.push([parseInt(option.totalpatients)]);
            }
        });
        data8.push({
            name: "Home Remedies",
            y: parseInt(totalRemedies)
        });
        //alert(totalRemedies);
        $('#totalnopatients1').empty();
        $('#totalnopatients1').append(totalnopatients1);
        $('#totalnopatients6').empty();
        $('#totalnopatients6').append(totalnopatients6);
        $('#totalnopatients7').empty();
        $('#totalnopatients7').append(totalnopatients7);
        $('#totalnopatients8').empty();
        $('#totalnopatients8').append(totalnopatients8);
        $('#totalnopatients9').empty();
        $('#totalnopatients9').append(totalnopatients9);
        $('#totalnopatients10').empty();
        $('#totalnopatients10').append(totalnopatients10);
        $('#totalnopatients11').empty();
        $('#totalnopatients11').append(totalnopatients11);
        $('#totalnopatients12').empty();
        $('#totalnopatients12').append(totalnopatients12);
        $('#totalnopatients14').empty();
        $('#totalnopatients14').append(totalnopatients14);

        $('#maincontainer2').empty();
        $('#maincontainer3').empty();
        $('#maincontainer4').empty();
        $('#maincontainer5').empty();
        $('#maincontainer6').empty();
        $('#maincontainer7').empty();
        $('#maincontainer8').empty();
        $('#maincontainer9').empty();
        $('#maincontainer10').empty();
        $('#maincontainer11').empty();
        $('#maincontainer12').empty();
        $('#maincontainer13').empty();

        fillpiechartPatients('#maincontainer1', 'Total No of Patients Pregnant', data1);
        fill3ddonutchartCommonVisit('#maincontainer2', 'Most Common Reason for visit', data2);
        fill3ddonutchart('#maincontainer3', 'Frequency of complain', data3);
        fillpiechart('#maincontainer4', 'Specific Time When it Occurs?', data4);
        fillpiechart('#maincontainer5', 'Most Common Pregnancy Test', data5);
        fill3ddonutchart('#maincontainer6', 'Most Common Disease', data6);
        fill3ddonutchart('#maincontainer7', 'Most Common Factor That Aggravates Heartburn', data7);
        fillpiechart('#maincontainer8', 'Most Common Factor That Relieves Heartburn', data8);
        fillpiechart('#maincontainer9', 'Most Common Medication for Heartburn', data9);
        fill3ddonutchart('#maincontainer10', 'Most Common Problem Due to or During Pregnancy', data10);
        fillBarChart('#maincontainer11', 'Trimester Wise Data', name11, data11);
      //  fill3ddonutchart('#maincontainer12', 'How do you know it is Heartburn?', data12);
        fill3ddonutchart('#maincontainer13', 'Did you suffer from Heartburn even before pregnancy? If yes, has it become better or worse now?', data13);
    }
}



function fillpiechart(id, Question, data) {
    $(function () {
        $(id).highcharts({
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false,
                type: 'pie'
            },
            title: {
                text: Question
            },
            tooltip: {
                // pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                        style: {
                            color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                        }
                    }
                }
            },
            series: [{
                name: 'Total',
                colorByPoint: true,
                data: data
            }]
        });
    });
}

function fillpiechartPatients(id, Question, data)
{
    $(function () {
        $(id).highcharts({
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false,
                type: 'pie'
            },
            title: {
                text: Question
            },
            tooltip: {
                // pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    events: {
                        click: function (e) {
                       
                            var value = e.target.point.name;
                            var date = $('#datepicker').val();
                            var hospitalid = $('#ddlhospitals').val();
                            var ageid = $('#ddlage').val();
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
                                url: "DashboardService.asmx/GetDataOnPregnentPatientClick",
                                data: "{'day':'" + day + "','month':'" + month + "','year':'" + year + "','validate':'" + value + "','HospitalID':'" + hospitalid + "','AgeID':'" + ageid + "'}",
                                contentType: "application/json; charset=utf-8",
                                async: true,
                                success: OnSuccessGetTotalPatientsByQuestionFilter,
                                cache: false
                            });
                            $.ajax({
                                type: "Post",
                                url: "DashboardService.asmx/GetTotalPatientWithFilter",
                                data: "{'day':'" + day + "','month':'" + month + "','year':'" + year + "','validate':'" + value + "','HospitalID':'" + hospitalid + "','AgeID':'" + ageid + "'}",
                                contentType: "application/json; charset=utf-8",
                                async: true,
                                success: OnSuccessGetPatientCount,
                                cache: false
                            });
                        }
                    },
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                        style: {
                            color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                        }
                    }
                }
            },
            series: [{
                name: 'Total',
                colorByPoint: true,
                data: data
            }]
        });
    });
}

function fill3ddonutchart(id, Question, data) {
    $(function () {
        $(id).highcharts({
            chart: {
                type: 'pie',
                options3d: {
                    enabled: true,
                    alpha: 45
                }
            },
            title: {
                text: Question
            },
            subtitle: {
                //text: '3D donut in Highcharts'
            },
            plotOptions: {
                pie: {
                    innerSize: 100,
                    depth: 45
                }
            },
            series: [{
                name: 'Total',
                data: data //[
                //    ['Bananas', 8],
                //    ['Kiwi', 3],
                //    ['Mixed nuts', 1],
                //    ['Oranges', 6],
                //    ['Apples', 8],
                //    ['Pears', 4],
                //    ['Clementines', 4],
                //    ['Reddish (bag)', 1],
                //    ['Grapes (bunch)', 1]
                //]
            }]
        });
    });
}

function fill3ddonutchartCommonVisit(id, Question, data)
{
    $(function () {
        $(id).highcharts({
            chart: {
                type: 'pie',
                options3d: {
                    enabled: true,
                    alpha: 45
                }
            },
            title: {
                text: Question
            },
            subtitle: {
                //text: '3D donut in Highcharts'
            },
            plotOptions: {
                pie: {
                    innerSize: 100,
                    depth: 45,
                    cursor: 'pointer',
                    events: {
                        click: function (e) {
                            var value = e.point.name;
                            var date = $('#datepicker').val();
                            var hospitalid = $('#ddlhospitals').val();
                            var ageid = $('#ddlage').val();
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
                                url: "DashboardService.asmx/GetDataOnCommonVisitClick",
                                data: "{'day':'" + day + "','month':'" + month + "','year':'" + year + "','validate':'" + value + "','HospitalID':'" + hospitalid + "','AgeID':'" + ageid + "'}",
                                contentType: "application/json; charset=utf-8",
                                async: true,
                                success: OnSuccessGetTotalPatientsByQuestionFilterss,
                                cache: false
                            });
                        }
                    }
                }
            },
            series: [{
                name: 'Total',
                data: data,
                dataLabels: {
            enabled: true,
            allowOverlap: true,
            padding: 0,
            width: 5000
           
        }
            }]
        });
    });
}


function fillBarChart(id, Question, name, data) {
    $(function () {
        $(id).highcharts({
            chart: {
                type: 'bar'

            },
            title: {
                text: Question
            },
            subtitle: {
                //text: 'Source: <a href="https://en.wikipedia.org/wiki/World_population">Wikipedia.org</a>'
            },
            xAxis: {
                categories: name,
                title: {
                    text: null
                }
            },                                                   
            yAxis: {
                min: 0,
                title: {
                    //text: 'Population (millions)',
                    align: 'high'
                },
                labels: {
                    overflow: 'justify'
                }
            },
            tooltip: {
                //valueSuffix: ' millions'
            },
            plotOptions: {
                bar: {
                    cursor: 'pointer',
                    events: {
                        click: function (e)
                        {
                            var value = e.target.point.category[0];
                            var date = $('#datepicker').val();
                            var hospitalid = $('#ddlhospitals').val();
                            var ageid = $('#ddlage').val();
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
                                url: "DashboardService.asmx/TrimesterwiseData",
                                data: "{'day':'" + day + "','month':'" + month + "','year':'" + year + "','validate':'" + value + "','HospitalID':'" + hospitalid + "','AgeID':'" + ageid + "'}",
                                contentType: "application/json; charset=utf-8",
                                async: true,
                                success: OnSuccessGetTotalPatientsByQuestionFilter,
                                cache: false
                            });

                            $.ajax({
                                type: "Post",
                                url: "DashboardService.asmx/GetTotalPatientWithTrimesterFilter",
                                data: "{'day':'" + day + "','month':'" + month + "','year':'" + year + "','validate':'" + value + "','HospitalID':'" + hospitalid + "','AgeID':'" + ageid + "'}",
                                contentType: "application/json; charset=utf-8",
                                async: true,
                                success: OnSuccessGetPatientCount,
                                cache: false
                            });

                        }
                    },
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -40,
                y: 80,
                floating: true,
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                name: 'Total No of Patients',
                data: data,
                padding: 0
            }]
        });
    });
}

function OnSuccessGetTotalPatientsByQuestionFilter(data, status) {
    if (data.d != null) {
        var data1 = [];
        var data2 = [];
        var data3 = [];
        var data4 = [];
        var data5 = [];
        var data6 = [];
        var data7 = [];
        var data8 = [];
        var data9 = [];
        var data10 = [];
        var data11 = [];
        var data13 = [];
        var name11 = [];
        var totalRemedies = 0;
        var totalnopatients1 = 0;
        var totalnopatients6 = 0;
        var totalnopatients7 = 0;
        var totalnopatients8 = 0;
        var totalnopatients9 = 0;
        var totalnopatients10 = 0;
        var totalnopatients11 = 0;
        var totalnopatients12 = 0;
        var totalnopatients14 = 0;
        var msg = jQuery.parseJSON(data.d);

      //  $("#DivIdToPrint").animate({ scrollTop: 0 }, "slow");
        $("html, body").animate({ scrollTop: 0 }, 10);
        $.each(msg, function (i, option) {
            if (option.Question == 'Are you pregnant?') {
                data1.push({
                    name: option.Answer,
                    y: parseInt(option.totalpatients)
                });
                if (option.Answer == 'Yes') {
                    totalnopatients1 = option.totalpatients;
                }
            } else if (option.Question == 'Reason for visit?') {
                data2.push([
                    option.Answer, parseInt(option.totalpatients)
                ]);
            } else if (option.Question == 'Do you smoke or consume tobacco?') {
                if (option.Answer == 'Yes') {
                    totalnopatients6 = option.totalpatients;
                } else {
                    totalnopatients6 = 0;
                }
            } else if (option.Question == 'Do you consume alcohol?') {
                if (option.Answer == 'Yes') {
                    totalnopatients7 = option.totalpatients;
                }
            } else if (option.Question == 'Do you consume spicy foods?') {
                if (option.Answer == 'Yes') {
                    totalnopatients8 = option.totalpatients;
                }
            } else if (option.Question == 'Do you consume fried & oily foods?') {
                if (option.Answer == 'Yes') {
                    totalnopatients9 = option.totalpatients;
                }
            } else if (option.Question == 'Do you consume fruits and vegetables?') {
                if (option.Answer == 'Yes') {
                    totalnopatients10 = option.totalpatients;
                }
            } else if (option.Question == 'Do you consume tea/coffee or carbonated drinks?') {
                if (option.Answer == 'Yes') {
                    totalnopatients11 = option.totalpatients;
                }
            } else if (option.Question == 'Do you experience Heartburn?') {
                if (option.Answer == 'Yes') {
                    totalnopatients12 = option.totalpatients;
                }
            } else if (option.Question == 'Have you complained about it to the doctor?') {
                if (option.Answer == 'Yes') {
                    totalnopatients14 = option.totalpatients;
                }
            } else if (option.Question == 'Frequency of complain?') {
                data3.push([
                    option.Answer, parseInt(option.totalpatients)
                ]);
            } else if (option.Question == 'Is there a specific time when it occurs?') {
                data4.push({
                    name: option.Answer,
                    y: parseInt(option.totalpatients)
                });
            } else if (option.Question == 'How was your pregnancy confirmed?') {
                data5.push([
                    option.Answer, parseInt(option.totalpatients)
                ]);
            } else if (option.Question == 'Are you suffering from any other disease?') {
                data6.push({
                    name: option.Answer,
                    y: parseInt(option.totalpatients)
                });
            //} else if (option.Question == 'How do you know it is Heartburn?') {
            //    data12.push({
            //        name: option.Answer,
            //        y: parseInt(option.totalpatients)
            //    });
            } else if (option.Question == 'Did you suffer from Heartburn even before pregnancy? If yes, has it become better or worse now?') {
                data13.push({
                    name: option.Answer,
                    y: parseInt(option.totalpatients)
                });
            } else if (option.Question == 'Is your Heartburn aggravated by any factor?') {
                data7.push([
                    option.Answer, parseInt(option.totalpatients)
                ]);
            } else if (option.Question == 'What do you do to relieve yourself of Heartburn?') {
                if (option.Answer == "Home Remedies") {

                }
                else if (option.Answer == "Chewing Gum" || option.Answer == "Baking Soda" || option.Answer == "Banana" || option.Answer == "Apple" || option.Answer == "Almonds" || option.Answer == "Others") {
                    data8.push({
                        name: option.Answer,
                        y: parseInt(option.totalpatients)
                    });
                    totalRemedies += parseInt(option.totalpatients);
                }
                else {
                    data8.push({
                        name: option.Answer,
                        y: parseInt(option.totalpatients)
                    });
                }
            } else if (option.Question == 'Have you been taking any medication for Heartburn?') {
                data9.push([
                    option.Answer, parseInt(option.totalpatients)
                ]);
            } else if (option.Question == 'Have you been experiencing any other problem due to or during pregnancy?') {
                data10.push({
                    name: option.Answer,
                    y: parseInt(option.totalpatients)
                });
            } else if (option.Question == 'Duration of current pregnancy?') {
                name11.push([option.Answer]);
                data11.push([parseInt(option.totalpatients)]);
            }
        });
        data8.push({
            name: "Home Remedies",
            y: parseInt(totalRemedies)
        });
        $('#totalnopatients1').empty();
        $('#totalnopatients1').append(totalnopatients1);
        $('#totalnopatients6').empty();
        $('#totalnopatients6').append(totalnopatients6);
        $('#totalnopatients7').empty();
        $('#totalnopatients7').append(totalnopatients7);
        $('#totalnopatients8').empty();
        $('#totalnopatients8').append(totalnopatients8);
        $('#totalnopatients9').empty();
        $('#totalnopatients9').append(totalnopatients9);
        $('#totalnopatients10').empty();
        $('#totalnopatients10').append(totalnopatients10);
        $('#totalnopatients11').empty();
        $('#totalnopatients11').append(totalnopatients11);
        $('#totalnopatients12').empty();
        $('#totalnopatients12').append(totalnopatients12);
        $('#totalnopatients14').empty();
        $('#totalnopatients14').append(totalnopatients14);
        //    fillpiechartPatients('#maincontainer1', 'Total No of Patients Pregnant', data1);
        $('#maincontainer2').empty();
        $('#maincontainer3').empty();
        $('#maincontainer4').empty();
        $('#maincontainer5').empty();
        $('#maincontainer6').empty();
        $('#maincontainer7').empty();
        $('#maincontainer8').empty();
        $('#maincontainer9').empty();
        $('#maincontainer10').empty();
        $('#maincontainer11').empty();
        $('#maincontainer12').empty();
        $('#maincontainer13').empty();

        fill3ddonutchart('#maincontainer2', 'Most Common Reason for visit', data2);
        fill3ddonutchart('#maincontainer3', 'Frequency of complain', data3);
        fillpiechart('#maincontainer4', 'Specific Time When it Occurs?', data4);
        fillpiechart('#maincontainer5', 'Most Common Pregnancy Test', data5);
        fill3ddonutchart('#maincontainer6', 'Most Common Disease', data6);
        fill3ddonutchart('#maincontainer7', 'Most Common Factor That Aggravates Heartburn', data7);
        fillpiechart('#maincontainer8', 'Most Common Factor That Relieves Heartburn', data8);
        fillpiechart('#maincontainer9', 'Most Common Medication for Heartburn', data9);
        fill3ddonutchart('#maincontainer10', 'Most Common Problem Due to or During Pregnancy', data10);
        fillBarChart('#maincontainer11', 'Trimester Wise Data', name11, data11);
     //   fill3ddonutchart('#maincontainer12', 'How do you know it is Heartburn?', data12);
        fill3ddonutchart('#maincontainer13', 'Did you suffer from Heartburn even before pregnancy? If yes, has it become better or worse now?', data13);
    }
}

function OnSuccessGetTotalPatientsByQuestionFilterss(data, status) {
    if (data.d != null) {
        var data1 = [];
        var data2 = [];
        var data3 = [];
        var data4 = [];
        var data5 = [];
        var data6 = [];
        var data7 = [];
        var data8 = [];
        var data9 = [];
        var data10 = [];
        var data11 = [];
        var data13 = [];
        var name11 = [];

        var totalnopatients1 = 0;
        var totalnopatients6 = 0;
        var totalnopatients7 = 0;
        var totalnopatients8 = 0;
        var totalnopatients9 = 0;
        var totalnopatients10 = 0;
        var totalnopatients11 = 0;
        var totalnopatients12 = 0;
        var totalnopatients14 = 0;
        var msg = jQuery.parseJSON(data.d);

        //  $("#DivIdToPrint").animate({ scrollTop: 0 }, "slow");
        $("#freqchart").animate({ scrollTop: 0 }, 10);
        $.each(msg, function (i, option) {
             if (option.Question == 'Frequency of complain?') {
                data3.push([
                    option.Answer, parseInt(option.totalpatients)
                ]);
            } else if (option.Question == 'Is there a specific time when it occurs?') {
                data4.push({
                    name: option.Answer,
                    y: parseInt(option.totalpatients)
                });
            } 
        });
        $('#maincontainer3').empty();
        $('#maincontainer4').empty();
        fill3ddonutchart('#maincontainer3', 'Frequency of complain', data3);
        fillpiechart('#maincontainer4', 'Specific Time When it Occurs?', data4);
       
    }
}


var FillddlAge = function (){
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/AgeFilter/GetAgeFilter",
        contentType: "application/json; charset=utf-8",
        async: true,
        success: onsuccessFillddlAge,
        cache: false
    });
}
var onsuccessFillddlAge = function (response) {
    var msg = $.parseJSON(response.Data);
    if (response.Status == 200) {
        $('#ddlage').empty();
        $('#ddlage').append('<option value="-1">Select Age</option>');
        $.each(msg, function (i, option) {
            $('#ddlage').append('<option value="' + option.ID + '">' + option.Range + '</option>');
        });
    }
}


function GetHospitalforddl() {
    $.ajax({
        type: "GET",
        url: "../WCF/Service1.svc/Hospitals/GetAllHospitals",
        contentType: "application/json; charset=utf-8",
        async: true,
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

