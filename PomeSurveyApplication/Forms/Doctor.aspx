<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFiles/Home.Master" AutoEventWireup="true" CodeBehind="Doctor.aspx.cs" Inherits="PomeSurveyApplication.Forms.Doctor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/timepicker/bootstrap-datepicker.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../assets/jquery-multi-select/multi-select.css" />
    <link rel="stylesheet" type="text/css" href="../assets/select2/select2.css" />

    <style>
        @import url(http://fonts.googleapis.com/css?family=Roboto:400,300,700);

        .blue {
            color: #13A7D7;
        }                                                                 

        .reset {
            margin-left: 0 !important;
            clear: both;
        }

        * {
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
            outline: 0;
        }

            *:after, *:before {
                -moz-box-sizing: border-box;
                -webkit-box-sizing: border-box;
                box-sizing: border-box;
            }

        .more {
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            border-radius: 4px;
            -moz-box-shadow: 0 1px 2px rgba(0, 0, 0, 0.2);
            -webkit-box-shadow: 0 1px 2px rgba(0, 0, 0, 0.2);
            box-shadow: 0 1px 2px rgba(0, 0, 0, 0.2);
            background-color: #17b1e3;
            background-image: -moz-linear-gradient(top, #19b7eb, #13A7D7);
            background-image: -ms-linear-gradient(top, #19b7eb, #13A7D7);
            background-image: -webkit-gradient(linear, 0 0, 0 100%, from(#19b7eb), to(#13A7D7));
            background-image: -webkit-linear-gradient(top, #19b7eb, #13A7D7);
            background-image: -o-linear-gradient(top, #19b7eb, #13A7D7);
            background-image: linear-gradient(to bottom, #19b7eb, #13a7d7);
            background-repeat: repeat-x;
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#15b6ea', endColorstr='#13a3d2', GradientType=0);
            border: none;
            border-top: rgba(255, 255, 255, 0.2) 1px solid;
            border-bottom: rgba(0, 0, 0, 0.4) 1px solid;
            color: #fff;
            display: inline-block;
            font-size: 1em;
            font-weight: 700;
            padding: 15px 20px;
            text-align: center;
            text-transform: uppercase;
            text-shadow: 0px 1px 1px rgba(0, 0, 0, 0.5);
        }

            .more:hover {
                background-color: #13a8d9;
                background-image: -moz-linear-gradient(top, #14aee0, #12a0ce);
                background-image: -ms-linear-gradient(top, #14aee0, #12a0ce);
                background-image: -webkit-gradient(linear, 0 0, 0 100%, from(#14aee0), to(#12a0ce));
                background-image: -webkit-linear-gradient(top, #14aee0, #12a0ce);
                background-image: -o-linear-gradient(top, #14aee0, #12a0ce);
                background-image: linear-gradient(to bottom, #14aee0, #12a0ce);
                background-repeat: repeat-x;
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#13abdc', endColorstr='#129cc9', GradientType=0);
                cursor: pointer;
                text-decoration: none;
            }

            .more.wider {
                padding-left: 25px !important;
                padding-right: 25px !important;
            }

        .form {
            /*margin: 0 auto;*/
            /*padding: 0;*/
            /*width: 600px;*/
        }

            .form fieldset {
                border: 0;
                margin: 0;
                padding: 0;
            }

            .form .form_row {
                margin: 0 0 10px 0;
                position: relative;
            }

            .form .input input, form .input textarea, form .input select {
                -moz-border-radius: 14px;
                -webkit-border-radius: 14px;
                border-radius: 14px;
                -moz-transition: all 0.2s linear;
                -o-transition: all 0.2s linear;
                -webkit-transition: all 0.2s linear;
                transition: all 0.2s linear;
                background: white;
                border: 1px solid #ccc;
                color: black;
                display: inline-block;
                height: 45px;
                font-family: "Roboto", Helvetica, Arial, sans-serif;
                font-size: 15px;
                font-weight: 200;
                margin: 0;
                overflow: hidden;
                padding: 10px 20px 10px 100px;
                width: 100%;
            }

            .form .input textarea {
                /*height: 250px;*/
                width: 98%;
                resize: none;
            }

            .form .input .label {
                -moz-transition: all 0.2s linear;
                -o-transition: all 0.2s linear;
                -webkit-transition: all 0.2s linear;
                transition: all 0.2s linear;
                color: #333;
                cursor: text;
                font-size: 0.8em;
                font-weight: 400;
                position: absolute;
                top: 14px;
                left: 15px;
            }

            .form .input.active {
                overflow: hidden;
            }

                .form .input.active input, form .input.active textarea {
                    padding-left: 120px;
                }

                    .form .input.active input:required:invalid, form .input.active textarea:required:invalid {
                        border: 1px solid #e9322d;
                        color: #e9322d;
                    }

                        .form .input.active input:required:invalid + .label, form .input.active textarea:required:invalid + .label {
                            background: #e9322d;
                        }

                        .form .input.active input:required:invalid:focus + .label, form .input.active textarea:required:invalid:focus + .label {
                            background: #e9322d;
                        }

                .form .input.active .label {
                    -moz-border-radius: 14px 0 0 14px;
                    -webkit-border-radius: 14px;
                    border-radius: 14px 0 0 14px;
                    color: #fff;
                    background: #00A4AD;
                    top: 0;
                    left: 0;
                    padding: 14px 10px;
                    text-align: center;
                    width: 100px;
                }

                    .form .input.active .label span {
                        color: #fff !important;
                    }

                .form .input.active textarea + .label {
                    -moz-border-radius: 14px 0;
                    -webkit-border-radius: 14px;
                    border-radius: 14px 0;
                }

            .form :-moz-placeholder {
                color: #777;
            }

            .form :-ms-input-placeholder {
                color: #777;
            }

            .form ::-webkit-input-placeholder {
                color: #777;
            }

        @media only screen and (min-width: 768px) {
            [class*="span"] {
                display: block;
                float: left;
                min-height: 30px;
                margin-left: 2.564102564102564%;
            }

            .span6 {
                width: 48.717948717948715%;
                *width: 48.664757228587014%;
            }

            .span12 {
                width: 100%;
                *width: 99.94680851063829%;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wraper container-fluid">
        <div class="page-title">
            <h1 class="title">Doctors </h1>
        </div>
        <div class="panel">

            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="m-b-30">
                            <h3 class="panel-title">Doctors Detail List</h3>
                        </div>
                    </div>
                </div>
                <div id="tableappend">
                </div>
            </div>
            <!-- end: page -->
        </div>

        <div class="row"  id="ModelForm">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Add Doctors </h3>
                    </div>
                    <div class=" panel-body">
                        <div class="form">
                            <fieldset class="span6 reset">
                                <div class="form_row">
                                    <div class="input">
                                        <input placeholder="Doctor Name" type="text" name="txtdname" id="txtdname" />
                                        <label class="label" for="name">Name: <span class="blue">*</span></label>
                                    </div>
                                </div>

                                <div class="form_row">
                                    <div class="input">
                                        <input placeholder="Fill out your Mobile number" name="txtmnum" id="txtmnum" />
                                        <label class="label" for="email">Mobile #: <span class="blue">*</span></label>
                                    </div>
                                </div>
                                <div class="form_row">
                                    <div class="input">
                                        <%--<div class="input-group">--%>
                                            <input placeholder="Date Of Birth" id="datepicker" name="datepicker" />
                                            <label class="label" for="datepicker">DOB:</label>
                                        <%--</div>--%>
                                    </div>
                                </div>
                                <div class="form_row">
                                    <div class="input">
                                        <input placeholder="Fill out your city" type="text" name="txtdname" id="txtCity" />
                                        <label class="label" for="company">City:</label>
                                    </div>
                                </div>
                                 <div class="form_row">
                                    <div class="input">
                                        <input placeholder="Fill out your Contact Number" type="text" name="txtmnum" id="txtcnum" />
                                        <label class="label" for="company">Contact #:</label>
                                    </div>
                                </div>
                                   <div class="form_row">
                                    <div class="input">
                                        <input placeholder="Fill out your CNIC Number" name="txtnic" id="txtnic" />
                                        <label class="label" for="email">CNIC: <span class="blue">*</span></label>
                                    </div>
                                </div>
                            </fieldset>
                            <asp:Button Text="" ClientIDMode="Static" Style="display: none" ID="txthiddenid" runat="server" />
                            <fieldset class="span6">
                                <div class="form_row">
                                    <div class="input">
                                        <input placeholder="Fill out your Password" type="password" name="txtpass" id="txtpass" />
                                        <label class="label" >Password: <span class="blue">*</span></label>
                                    </div>
                                </div>
                                <div class="form_row">
                                    <div class="input">
                                        <input placeholder="Fill out your Designation" type="text" name="txtdesig" id="txtdesig" />
                                        <label class="label" >Designation: <span class="blue">*</span></label>
                                    </div>
                                </div>

                                <div class="form_row">
                                    <div class="input">
                                        <input placeholder="Fill out your Speciality" name="txtdname" type="text" id="txtspec" />
                                        <label class="label" for="subject">Speciality: <span class="blue">*</span></label>
                                    </div>
                                </div>
                                 <div class="form_row">
                                    <div class="input">
                                        <select class="select2" id="ddlhospitals"  name="ddlhospital" >
                                            <option value="-1">Select Hospital</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form_row">
                                    <div class="input">
                                        <input placeholder="Fill out your email" name="txtemail" id="txtemail" />
                                        <label class="label" for="email">Email: <span class="blue">*</span></label>
                                    </div>
                                </div>
                              <div class="form_row">
                                    <div class="input">
                                        <input  type="checkbox" id="chisactive" />
                                        <label class="label" for="name">IsActive <span class="blue">*</span></label>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset class="span12 reset">
                                <div class="form_row">
                                    <a onclick="insertdata()" id="btnInsert" class="more wider">Insert</a>
                                    <a onclick="UpdateData()" id="btnUpdate" style="display: none" class="more wider">Update</a>
                                    <a onclick="clearFields()" style="display: none" id="btncancel" class="more wider">Cancel</a>
                                </div>
                            </fieldset>
                        </div>

                    </div>
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
    <script src="../Scripts/Doctor.js"></script>

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
    </script>
    <script>
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
    </script>

</asp:Content>
