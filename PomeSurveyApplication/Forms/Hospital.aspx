﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFiles/Home.Master" AutoEventWireup="true" CodeBehind="Hospital.aspx.cs" Inherits="PomeSurveyApplication.Forms.Hospital" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                height: 100px;
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
            <h1 class="title">Hospital </h1>
        </div>
        <div class="panel">

            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="m-b-30">
                            <h3 class="panel-title">Hospital Detail </h3>
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
                        <h3 class="panel-title">Add Hospitals </h3>
                    </div>
                    <div class=" panel-body">
                        <div class="form">
                            <fieldset class="span6 reset">
                                <div class="form_row">
                                    <div class="input">
                                        <input placeholder="Enter the Hospital Name" type="text" name="txthname" id="txthname" />
                                        <label class="label" for="name">Name: <span class="blue">*</span></label>
                                    </div>
                                </div>

                                <div class="form_row">
                                    <div class="input">
                                        <input placeholder="Enter Hospital's contact number" name="txtContact" id="txtContact" />
                                        <label class="label" for="email">Number: <span class="blue">*</span></label>
                                    </div>
                                </div>

                                <div class="form_row">
                                    <div class="input">
                                        <input placeholder="Enter the city where Hospital is located" type="text" name="txthname" id="txtCity" />
                                        <label class="label" for="company">City:</label>
                                    </div>
                                </div>
                            </fieldset>
                            <asp:Button Text="" ClientIDMode="Static"  style="display:none" ID="txthiddenid" runat="server" />
                            <fieldset class="span6">
                                <div class="form_row">
                                    <div class="input">
                                        <input placeholder="Enter the name of department" name="txtdepartment" type="text" id="txtdep" />
                                        <label class="label" for="tel">Department: <span class="blue">*</span></label>
                                    </div>
                                </div>

                                <div class="form_row">
                                    <div class="input">
                                        <input placeholder="Enter Hospital location" type="text" name="txtlocation" id="txtlocation" />
                                        <label class="label" for="subject">Location: <span class="blue">*</span></label>
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
                                    <div class="input">
                                        <textarea placeholder="Enter the Hospital Address" id="txtaddress" name="address"></textarea>
                                        <label class="label" for="address">Address: <span class="blue"></span></label>
                                    </div>
                                </div>

                                <div class="form_row">
                                    <a onclick="InsertHospitalData()" id="btnInsert" class="more wider">Add</a>
                                      <a onclick="UpdateHospitalData()" id="btnUpdate" style="display:none" class="more wider">Update</a>
                                    <a onclick="clearFields()" style="display:none"id="btncancel" class="more wider">Cancel</a>
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
    <%--<script src="../js/script.js"></script>--%>
    <%--<script src="../assets/magnific-popup/magnific-popup.js"></script>--%>
    <script src="../assets/jquery-datatables-editable/jquery.dataTables.js"></script>
    <script src="../assets/datatables/dataTables.bootstrap.js"></script>
    <%--<script src="../assets/jquery-datatables-editable/datatables.editable.init.js"></script>--%>
    <script src="../Scripts/hospital.js"></script>

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
