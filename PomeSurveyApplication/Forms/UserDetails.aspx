<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFiles/Home.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="PomeSurveyApplication.Forms.UserDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/timepicker/bootstrap-datepicker.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../assets/jquery-multi-select/multi-select.css" />
    <link rel="stylesheet" type="text/css" href="../assets/select2/select2.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wraper container-fluid">
        <div class="row">
             <div class="col-md-12">
                <div class="col-md-3">
                    <label>Select Hospital :</label>
                    <select class="select2" id="ddlhospitals" name="ddlhospital">
                        <option value="0">Select Hospital</option>
                    </select>
                </div>
                <div class="col-md-3">
                    <label>Select Pharmacist :</label>
                    <select class="select2" id="ddlrepresentator" name="ddlrepresentator">
                        <option value="0">Select Pharmacist</option>
                    </select>
                </div>
                <div class="col-md-3">
                    <label>Filter By HeartBurn :</label>
                    <select class="select2" id="ddlheartburn" name="ddlheartburn">
                        <option value="0">Filter By HeartBurn</option>
                        <option value="Yes">Yes</option>
                        <option value="No">No</option>
                    </select>
                </div>
                <div class="col-md-3">
                    <label>Filter By Pregnent Patient :</label>
                    <select class="select2" id="ddlPatient" name="ddlheartburn">
                        <option value="0">Filter By Pregnent Patient</option>
                        <option value="1">Yes</option>
                        <option value="2">No</option>
                    </select>
                </div>
                
        </div>
            <div class="col-md-12" style="margin-top:25px">
                    <div class="col-md-3">
                        <label>Filter By Date/Month :</label>

                        <div class="form-check col-md-6">
                                            <input type="radio" class="form-check-input" id="datefilterradio" name="group0" value="Date" checked="checked"/>
                                            <label class="form-check-label" for="exampleCheck1">Date</label>
                                         </div>
                                        <div class="form-check col-md-6">
                                            <input type="radio" class="form-check-input" id="monthfilterradio" name="group0" value="Month" />
                                            <label class="form-check-label" for="exampleCheck1">Month</label>
                                         </div>
                                        
                                    </div>
                        </div>
            <div class="col-md-12">
                <div class="col-md-3">
                    <%--<label>Filter By Month :</label>--%>
                    <div class="input-group">
                        <input class="form-control" placeholder="mm/dd/yyyy" id="datepicker" type="text" />
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    </div>
                </div>
                 <div class="col-md-2">
                             <input type="button" name="btnFetchData" class="btn bg-primary" id="btnFetchData"  onclick="FillGrid()" value="Fetch Data" />
                        </div>
            </div>
            
         </div>
        <br />
        <br />
        
        <div class="panel">

            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="m-b-30">
                            <h3 class="panel-title">Patient Detail List</h3>
                        </div>
                    </div>
                </div>
                <div id="tableappend">
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageEndScripts" runat="server">
    <script src="../assets/jquery-datatables-editable/jquery.dataTables.js"></script>
    <script src="../assets/timepicker/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="../assets/jquery-multi-select/jquery.multi-select.js"></script>
    <script type="text/javascript" src="../assets/jquery-multi-select/jquery.quicksearch.js"></script>
    <script src="../assets/select2/select2.min.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>

    <script src="../Highcharts-4.2.5/js/highcharts.js"></script>
    <script src="../Highcharts-4.2.5/js/highcharts-3d.js"></script>
    <script src="../Highcharts-4.2.5/js/modules/exporting.js"></script>
    <script src="../js/jquery.PrintArea.js"></script>
    <script src="../assets/timepicker/bootstrap-datepicker.js"></script>
    <script src="../Scripts/UserDetails.js"></script>
    <script>
        jQuery(document).ready(function () {

            // Date Picker
            jQuery('#datepicker').datepicker();
            jQuery('#datepicker-inline').datepicker();
            jQuery('#datepicker-multiple').datepicker({
                numberOfMonths: 3,
                showButtonPanel: true
            });

            //multiselect start

            $('#my_multi_select1').multiSelect();
            $('#my_multi_select2').multiSelect({
                selectableOptgroup: true
            });

            $('#my_multi_select3').multiSelect({
                selectableHeader: "<input type='text' class='form-control search-input' autocomplete='off' placeholder='search...'>",
                selectionHeader: "<input type='text' class='form-control search-input' autocomplete='off' placeholder='search...'>",
                afterInit: function (ms) {
                    var that = this,
                        $selectableSearch = that.$selectableUl.prev(),
                        $selectionSearch = that.$selectionUl.prev(),
                        selectableSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selectable:not(.ms-selected)',
                        selectionSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selection.ms-selected';

                    that.qs1 = $selectableSearch.quicksearch(selectableSearchString)
                        .on('keydown', function (e) {
                            if (e.which === 40) {
                                that.$selectableUl.focus();
                                return false;
                            }
                        });

                    that.qs2 = $selectionSearch.quicksearch(selectionSearchString)
                        .on('keydown', function (e) {
                            if (e.which == 40) {
                                that.$selectionUl.focus();
                                return false;
                            }
                        });
                },
                afterSelect: function () {
                    this.qs1.cache();
                    this.qs2.cache();
                },
                afterDeselect: function () {
                    this.qs1.cache();
                    this.qs2.cache();
                }
            });

            // Select2
            jQuery(".select2").select2({
                width: '100%'
            });
        });
        //$("#datepicker").datepicker({
        //    format: "mm-yyyy",
        //    startView: "months",
        //    minViewMode: "months"
        //});
    </script>
</asp:Content>
