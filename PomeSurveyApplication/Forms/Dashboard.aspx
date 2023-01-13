<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFiles/Home.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="PomeSurveyApplication.Dashboard.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/timepicker/bootstrap-datepicker.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../assets/jquery-multi-select/multi-select.css" />
    <link rel="stylesheet" type="text/css" href="../assets/select2/select2.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Page Content Start -->
            <!-- ================== -->

            <div id="DivIdToPrint" class="wraper container-fluid">
                <div class="page-title"> 
                    <div class=
                        "col-md-10"> 
                        <h3 style="font-size:35px" class="title">Welcome !</h3> 
                    </div>
                    <div class="col-md-2">
                        <img src="../Files/click-here-to-print.png" onclick="printDiv()" style="width:180px;height:54px;margin-bottom:20px;cursor:pointer" title="print Dashboard" />
                        <%--<input id="print_butto" type="button" value="Print Page"/>--%>
                    </div>
                    <br />  
                    <div class="col-md-12">
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
                    <div class="col-md-12" style="margin-bottom:20px">

                        <div class="col-md-3">
                        
                            
                            <div class="input-group">
                                    <input class="form-control" placeholder="mm/dd/yyyy" id="datepicker" type="text" />
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                </div>
                            </div>

                     
                         <div class="col-md-2">
                             <input type="button" name="btnFetchData" class="btn bg-primary" id="btnFetchData" style="margin-top:0px " onclick="GetTotalPatientsByQuestion()" value="Fetch Data" />
                        </div>

                    </div>
                    <div class="col-md-12" id="divhospital">
                     <div class="col-md-3" id="ddlhos">
                            <label>Filter By Hospital :</label>
                                <select class="select2" id="ddlhospitals"  name="ddlhospital"  >
                                            <option value="-1">Select Hospital</option>
                                        </select>
                            </div>
                        <div class="col-md-3">
                            <label>Filter By Age :</label>
                                <select class="select2" id="ddlage"  name="ddlhospital"  >
                                            <option value="-1">Select Age</option>
                                        </select>
                            </div>
                        </div>

                    <div class="col-md-12">
                        <br />
                    </div>

                    <div class="col-md-12">
                        <div class="col-md-8">

                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12" id="ButtonsSet2">
                                <div class="col-sm-12">
                                    <br />
                                    <button type="button" id="exportbtn" onclick="toggleRow('Export')" class="btn btn-primary" style="width: 100%">Export Statistics</button>
                                </div>
                            </div>
                            <div id="customExportRow" style="display:none;margin-top:10px" class="col-md-12">
                                <div class="col-md-12">
                                    <div class="input-group" style="margin-bottom:10px">
                                    <input class="form-control" placeholder="mm/dd/yyyy" id="datepickerforExport" type="text" />
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-check">
                                            <input type="radio" class="form-check-input" id="hospitalradio" name="group1" value="Hospitals" checked="checked"/>
                                            <label class="form-check-label" for="exampleCheck1">Hospitals</label>
                                         </div>
                                        <div class="form-check">
                                            <input type="radio" class="form-check-input" id="pharmacistradio" name="group1" value="Pharmacists"/>
                                            <label class="form-check-label" for="exampleCheck1">Pharmacists</label>
                                         </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-check">
                                            <input type="radio" class="form-check-input" id="monthradio" name="group2" value="Month" checked="checked"/>
                                            <label class="form-check-label" for="exampleCheck1">Month</label>
                                         </div>
                                        <div class="form-check">
                                            <input type="radio" class="form-check-input" id="dailyradio" name="group2" value="Day"/>
                                            <label class="form-check-label" for="exampleCheck1">Day</label>
                                         </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <button type="button" class="btn btn-primary btn-default" onclick="generateExport()" style="width: 100%">Generate</button>
                                    
                                    <%-- Button for Survey Details Excel Report Generation --%>
                                    <button type="button" class="btn btn-info btn-default" onclick="generateSurveyDetailExcelRpt()" style="margin-top:10px;width: 100%">Generate Survey Details Report</button>

                                </div>
                            </div>
                        </div>
                    </div>
                    

                    <div class="col-md-12">
                        <br />
                    </div>
                    
                </div>

                <div class="row text-center">
                    <div class="col-lg-6 col-sm-6">
                        <div class="panel">
                            <div class="panel-body">
                                <img src="../Files/patient.png" style="width:50px; height:50px"  />
                                <div class="h2 text-success m-t-10" id="totalnopatients">0</div>
                                <p class="text-muted m-b-10" style="color:black" >Total No Of Patients</p>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6 col-sm-6">
                        <div class="panel">
                            <div class="panel-body">
                                <img src="../Files/Pregnant%20patients.png" style="width:50px;height:50px" />
                                <div class="h2 text-success m-t-10" id="totalnopatients1">0</div>
                                <p class="text-muted m-b-10" style="color:black">Total No Of Patients Pregnant</p>
                            </div>
                        </div>
                    </div>

                <div class="row text-center">
                    <div class="col-lg-6 col-sm-6">
                        <div class="panel">
                            <div class="panel-body">
                                <img src="../Files/heartburn.png" style="width:50px;height:50px" />
                                <div class="h2 text-success m-t-10" id="totalnopatients12">0</div>
                                <p class="text-muted m-b-10" style="color:black">Total No Of Patients Who Experience Heartburn</p>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6 col-sm-6">
                        <div class="panel">
                            <div class="panel-body">
                                <img src="../Files/complain%20to%20dr%20about%20heartburn.png.png" style="width:50px;height:50px" />
                                <div class="h2 text-success m-t-10" id="totalnopatients14">0</div>
                                <p class="text-muted m-b-10"style="color:black">Total No Of Patients Who Complained About Heartburn to The Doctor</p>
                            </div>
                        </div>
                    </div>
                    
                </div>

                <div class="row text-center">
                    <div class="col-lg-3 col-sm-6">
                        <div class="panel">
                            <div class="panel-body">
                                <img src="../Files/spicy%20foods.png" style="width:50px; height:50px" />
                                <div class="h2 text-success m-t-10" id="totalnopatients8">0</div>
                                <p class="text-muted m-b-10" style="color:black">Total No Of Patients Who Consume Spicy Foods</p>
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-lg-3 col-sm-6">
                        <div class="panel">
                            <div class="panel-body">
                                <img src="../Files/fry%20oily%20foods.png" style="width:50px; height:40px" />
                                <div class="h2 text-success m-t-3" id="totalnopatients9">0</div>
                                <p class="text-muted m-b-10"style="color:black">Total No Of Patients Who Consume Fried & Oily Foods</p>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-3 col-sm-6">
                        <div class="panel">
                            <div class="panel-body">
                                <img src="../Files/Fruit_and_Vegetable_Icon_Set.png" style="width:50px; height:50px" />
                                <div class="h2 text-success m-t-10" id="totalnopatients10">0</div>
                                <p class="text-muted m-b-10"style="color:black">Total No Of Patients Who Consume Fruits and Vegetables</p>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-3 col-sm-6">
                        <div class="panel">
                            <div class="panel-body">
                                <img src="../Files/Cold_drink-512.png" style="height:50px;width:50px" />
                                <div class="h2 text-success m-t-10" id="totalnopatients11">0</div>
                                <p class="text-muted m-b-10"style="color:black">Total No Of Patients Who Consume Tea/Coffee or Carbonated Drinks</p>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">

                    <div class="col-lg-6"><!-- /primary heading -->
                        <div class="portlet">
                            <div id="maincontainer1" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                        </div>  
                    </div> <!-- end col -->
                    <div class="col-lg-6">
                        <div class="portlet">
                            <div id="maincontainer2" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                        </div>
                    </div>

                </div> <!-- end row -->

                <div class="row">
                    <div class="col-lg-12">
                        <div class="portlet">
                        <div id="maincontainer11" style="min-width: 310px; height: 400px; max-width: 1120px; margin: 0 auto"></div>
                        </div> 
                    </div> <!-- end col -->
                    <!-- end col -->
                    </div>

                <div class="row" id="freqchart">
                    <div class="col-lg-6">
                        <div class="portlet">
                        <div id="maincontainer3" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                        </div> 
                    </div> <!-- end col -->
                    <div class="col-lg-6">
                        <div class="portlet"><!-- /primary heading -->
                            <div id="maincontainer4" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                    </div> <!-- end col -->
                </div> <!-- end row -->
               </div>
                
                <div class="row">
                    <div class="col-lg-6">
                        <div class="portlet">
                        <div id="maincontainer5" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                        </div> 
                    </div> <!-- end col -->
                    <div class="col-lg-6">
                        <div class="portlet"><!-- /primary heading -->
                            <div id="maincontainer6" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                    </div> <!-- end col -->
                </div> <!-- end row -->
               </div>

                <div class="row">
                    <div class="col-lg-6">
                        <div class="portlet">
                        <div id="maincontainer7" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                        </div> 
                    </div> <!-- end col -->
                    <div class="col-lg-6">
                        <div class="portlet"><!-- /primary heading -->
                            <div id="maincontainer8" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                    </div> <!-- end col -->
                </div> <!-- end row -->
               </div>

                <div class="row">
                    <div class="col-lg-6">
                        <div class="portlet">
                        <div id="maincontainer9" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                        </div> 
                    </div> <!-- end col -->
                    <div class="col-lg-6">
                        <div class="portlet"><!-- /primary heading -->
                            <div id="maincontainer10" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                    </div> <!-- end col -->
                </div> <!-- end row -->
                </div>
                <div class="row">
                  <%--  <div class="col-lg-6">
                        <div class="portlet">
                            <div id="maincontainer12" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                        </div>
                    </div>--%>
                    <div class="col-lg-12">
                        <div class="portlet">
                            <div id="maincontainer13" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                        </div>
                    </div>
                </div>

                <!-- WEATHER -->
                <%--<div class="row">
                            
                    <div class="col-lg-6">
                        
                        <!-- BEGIN WEATHER WIDGET 1 -->
                        <div class="panel bg-success-alt">
                            <div class="panel-body">
                            
                                <div class="row">
                                    <div class="col-sm-7">
                                        <div class="row">
                                            <div class="col-xs-6 text-center">
                                                <canvas id="partly-cloudy-day" width="115" height="115"></canvas>
                                            </div>
                                            <div class="col-xs-6">
                                                <h2 class="m-t-0 text-white"><b>32°</b></h2>
                                                <p class="text-white">Partly cloudy</p>
                                                <p class="text-white">15km/h - 37%</p>
                                            </div>
                                        </div><!-- End row -->
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="row">
                                            <div class="col-xs-4 text-center">
                                                <h4 class="text-white m-t-0">SAT</h4>
                                                <canvas id="cloudy" width="35" height="35"></canvas>
                                                <h4 class="text-white">30<i class="wi-degrees"></i></h4>
                                            </div>
                                            <div class="col-xs-4 text-center">
                                                <h4 class="text-white m-t-0">SUN</h4>
                                                <canvas id="wind" width="35" height="35"></canvas>
                                                <h4 class="text-white">28<i class="wi-degrees"></i></h4>
                                            </div>
                                            <div class="col-xs-4 text-center">
                                                <h4 class="text-white m-t-0">MON</h4>
                                                <canvas id="clear-day" width="35" height="35"></canvas>
                                                <h4 class="text-white">33<i class="wi-degrees"></i></h4>
                                            </div>
                                        </div><!-- end row -->
                                    </div>
                                </div><!-- end row -->
                            </div><!-- panel-body -->
                        </div><!-- panel-->
                        <!-- END Weather WIDGET 1 -->
                        
                    </div><!-- End col-md-6 -->

                    <div class="col-lg-6">
                        
                        <!-- WEATHER WIDGET 2 -->
                        <div class="panel bg-primary-alt">
                            <div class="panel-body">
                            
                                <div class="row">
                                    <div class="col-sm-7">
                                        <div class="">
                                            <div class="row">
                                                <div class="col-xs-6 text-center">
                                                    <canvas id="snow" width="115" height="115"></canvas>
                                                </div>
                                                <div class="col-xs-6">
                                                    <h2 class="m-t-0 text-white"><b> 23°</b></h2>
                                                    <p class="text-white">Partly cloudy</p>
                                                    <p class="text-white">15km/h - 37%</p>
                                                </div>
                                            </div><!-- end row -->
                                        </div><!-- weather-widget -->
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="row">
                                            <div class="col-xs-4 text-center">
                                                <h4 class="text-white m-t-0">SAT</h4>
                                                <canvas id="sleet" width="35" height="35"></canvas>
                                                <h4 class="text-white">30<i class="wi-degrees"></i></h4>
                                            </div>
                                            <div class="col-xs-4 text-center">
                                                <h4 class="text-white m-t-0">SUN</h4>
                                                <canvas id="fog" width="35" height="35"></canvas>
                                                <h4 class="text-white">28<i class="wi-degrees"></i></h4>
                                            </div>
                                            <div class="col-xs-4 text-center">
                                                <h4 class="text-white m-t-0">MON</h4>
                                                <canvas id="partly-cloudy-night" width="35" height="35"></canvas>
                                                <h4 class="text-white">33<i class="wi-degrees"></i></h4>
                                            </div>
                                        </div><!-- End row -->
                                    </div> <!-- col-->
                                </div><!-- End row -->
                            </div><!-- panel-body -->
                        </div><!-- panel -->
                        <!-- END WEATHER WIDGET 2 -->
                            
                    </div><!-- /.col-md-6 -->
                </div> --%>
                <!-- End row -->  

                <%--<div class="row">
                    
                    <div class="col-lg-12">

                        <div class="portlet"><!-- /primary heading -->
                            <div class="portlet-heading">
                                <h3 class="portlet-title text-dark text-uppercase">
                                    Projects
                                </h3>
                                <div class="portlet-widgets">
                                    <a href="javascript:;" data-toggle="reload"><i class="ion-refresh"></i></a>
                                    <span class="divider"></span>
                                    <a data-toggle="collapse" data-parent="#accordion1" href="#portlet2"><i class="ion-minus-round"></i></a>
                                    <span class="divider"></span>
                                    <a href="#" data-toggle="remove"><i class="ion-close-round"></i></a>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div id="portlet2" class="panel-collapse collapse in">
                                <div class="portlet-body">
                                    <div class="table-responsive">
                                        <table class="table">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Project Name</th>
                                                    <th>Start Date</th>
                                                    <th>Due Date</th>
                                                    <th>Status</th>
                                                    <th>Assign</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>1</td>
                                                    <td>Velonic Admin v1</td>
                                                    <td>01/01/2015</td>
                                                    <td>26/04/2015</td>
                                                    <td><span class="label label-info">Released</span></td>
                                                    <td>Coderthemes</td>
                                                </tr>
                                                <tr>
                                                    <td>2</td>
                                                    <td>Velonic Frontend v1</td>
                                                    <td>01/01/2015</td>
                                                    <td>26/04/2015</td>
                                                    <td><span class="label label-success">Released</span></td>
                                                    <td>Coderthemes</td>
                                                </tr>
                                                <tr>
                                                    <td>3</td>
                                                    <td>Velonic Admin v1.1</td>
                                                    <td>01/05/2015</td>
                                                    <td>10/05/2015</td>
                                                    <td><span class="label label-pink">Pending</span></td>
                                                    <td>Coderthemes</td>
                                                </tr>
                                                <tr>
                                                    <td>4</td>
                                                    <td>Velonic Frontend v1.1</td>
                                                    <td>01/01/2015</td>
                                                    <td>31/05/2015</td>
                                                    <td><span class="label label-purple">Work in Progress</span></td>
                                                    <td>Coderthemes</td>
                                                </tr>
                                                <tr>
                                                    <td>5</td>
                                                    <td>Velonic Admin v1.3</td>
                                                    <td>01/01/2015</td>
                                                    <td>31/05/2015</td>
                                                    <td><span class="label label-warning">Coming soon</span></td>
                                                    <td>Coderthemes</td>
                                                </tr>

                                                <tr>
                                                    <td>6</td>
                                                    <td>Velonic Admin v1.3</td>
                                                    <td>01/01/2015</td>
                                                    <td>31/05/2015</td>
                                                    <td><span class="label label-primary">Coming soon</span></td>
                                                    <td>Coderthemes</td>
                                                </tr>

                                                <tr>
                                                    <td>7</td>
                                                    <td>Velonic Admin v1.3</td>
                                                    <td>01/01/2015</td>
                                                    <td>31/05/2015</td>
                                                    <td><span class="label label-info">Cool</span></td>
                                                    <td>Coderthemes</td>
                                                </tr>

                                                <tr>
                                                    <td>8</td>
                                                    <td>Velonic Admin v1.3</td>
                                                    <td>01/01/2015</td>
                                                    <td>31/05/2015</td>
                                                    <td><span class="label label-warning">Coming soon</span></td>
                                                    <td>Coderthemes</td>
                                                </tr>
                                                
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div> <!-- end col -->
                    
                </div> --%>
                <!-- end row -->

            </div>
            <!-- Page Content Ends -->
            <!-- ================== -->

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
    <script src="../Scripts/Dashboard.js" type="text/javascript"></script>
        <script>
            jQuery(document).ready(function () {

                // Date Picker
                jQuery('#datepicker').datepicker();
                jQuery('#datepicker-inline').datepicker();
                jQuery('#datepicker-multiple').datepicker({
                    numberOfMonths: 3,
                    showButtonPanel: true
                });

                jQuery('#datepickerforExport').datepicker();
                jQuery('#datepickerforExport-inline').datepicker();
                jQuery('#datepickerforExport-multiple').datepicker({
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
            function toggleRow(callingFrom) {
                if (callingFrom == 'Export') {
                    //$("input:checkbox[name=type]:checked").each(function () {
                    //yourArray.push($(this).val());
                    $("input:checkbox[name=type]").each(function () {
                        //$(this).attr('checked', 'checked');
                        $(this).prop('checked', true);
                    });
                    $('#customExportRow').toggle('fast');
                }
            }
            function generateExport() {
                //var SelectedArray = [];
                var ExportDate = $("#datepickerforExport").val();
                var HospitalPharmacist = $("input:radio[name ='group1']:checked").val();
                var MonthlyDaily = $("input:radio[name ='group2']:checked").val();


                //$("input:checkbox[name=type]:checked").each(function () {
                //    SelectedArray.push($(this).val());
                //});
                //if (SelectedArray.length != 0) {
                var type = 'SurveyStatistics';
                //window.open("Handler/ExportExcel.ashx?date=" + initialdate + "&Type=" + type + "&CheckedValue=" + SelectedArray, "Billing Data", "dialogHeight:" + (screen.Height - 60) + "px; dialogWidth: " + (screen.width - 20) + "px; dialogTop: px; dialogLeft: px; edge: Raised; center: Yes; help: No; resizable: Yes; status: No;");
                window.location = "../Handler/ExportExcel.ashx?Type=" + type + "&ExportDate=" + ExportDate + "&HospitalPharmacist=" + HospitalPharmacist + "&MonthlyDaily=" + MonthlyDaily;
                //, "Billing Data", "dialogHeight:" + (screen.Height - 60) + "px; dialogWidth: " + (screen.width - 20) + "px; dialogTop: px; dialogLeft: px; edge: Raised; center: Yes; help: No; resizable: Yes; status: No;");
                //}
            }


            //*********** *********** function for Survey Detail Excel Formatted Report -- Mubashir ***********_______________ //

            function generateSurveyDetailExcelRpt() {

                var ExportDate = $("#datepickerforExport").val();
                var HospitalPharmacist = $("input:radio[name ='group1']:checked").val();
                var MonthlyDaily = $("input:radio[name ='group2']:checked").val();

                var type = 'SurveyDetailReport';
                //window.open("Handler/ExportExcel.ashx?date=" + initialdate + "&Type=" + type + "&CheckedValue=" + SelectedArray, "Billing Data", "dialogHeight:" + (screen.Height - 60) + "px; dialogWidth: " + (screen.width - 20) + "px; dialogTop: px; dialogLeft: px; edge: Raised; center: Yes; help: No; resizable: Yes; status: No;");
                window.location = "../Handler/ExportExcel.ashx?Type=" + type + "&ExportDate=" + ExportDate + "&HospitalPharmacist=" + HospitalPharmacist + "&MonthlyDaily=" + MonthlyDaily;

            }
    </script>
</asp:Content>
