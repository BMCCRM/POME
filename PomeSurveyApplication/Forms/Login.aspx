<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HRM.Forms.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Pome Survey Portal</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="Patient Profiling Program   ..." name="description" />
    <meta content="Hasan Ali Rasheed" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
        <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&amp;subset=all" rel="stylesheet" type="text/css" />
        <link href="http://maxcdn.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css" rel="stylesheet"  />
        <%--<link href="../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />--%>
        <link href="../css/bootstrap.min.css" rel="stylesheet"  type="text/css"  />
        <!-- END GLOBAL MANDATORY STYLES -->
        <!-- BEGIN THEME GLOBAL STYLES -->
        <link href="../css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
        <!-- END THEME GLOBAL STYLES -->
        <!-- BEGIN PAGE LEVEL STYLES -->
         <link href="../css/login-5.min.css" rel="stylesheet" />
        <!-- END PAGE LEVEL STYLES -->

        <link rel="shortcut icon" href="favicon.ico" /> 
</head>
<body class="login">
    <form id="form1" runat="server">
        <div class="user-login-5">
            <div class="row bs-reset">
                <div class="col-md-6 bs-reset mt-login-5-bsfix">
                   
                    <div class="login-bg" style="background-image:url(../Files/bg1.jpg)">
                        <%--<img class="login-logo"  src="../Files/Logo.png" />--%>
                         </div>
                </div>
                <div class="col-md-6 login-container bs-reset mt-login-5-bsfix">
                    <div class="login-content">
                        <img src="../Files/reckitt-benckiser-logo.png" style="width:200px;height:100px" />
                        <br />
                        <h1>Patient Profiling Program Login</h1>
                        <%--<p> Lorem ipsum dolor sit amet, coectetuer adipiscing elit sed diam nonummy et nibh euismod aliquam erat volutpat. Lorem ipsum dolor sit amet, coectetuer adipiscing. </p>--%>
                        <div class="login-form" >
                            <div class="alert alert-danger " runat="server" visible="false" id="diverror">
                                <button class="close" data-close="alert"></button>
                                <span>Please Insert Correct UserName Or Password </span>
                            </div>
                            <div class="row">
                                
                                <div class="col-xs-6">
                                    <div class="form-group form-md-line-input">
                                        <asp:TextBox runat="server" class="form-control" id="txtusername" placeholder="Enter your name"  />
                                                <label for="form_control_1"><i class="fa fa-user"></i> &nbsp;UserName</label>
                                            </div>
                                  </div>
                                <div class="col-xs-6">
                                    <div class="form-group form-md-line-input">
                                        <asp:TextBox runat="server" class="form-control" id="txtpass" TextMode="Password" placeholder="Enter your Password"  />
                                                <label for="form_control_1"><i class="fa fa-key"></i> &nbsp;Password</label>
                                            </div>
                                   </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                              <%--      <div class="rem-password">
                                        <label class="rememberme mt-checkbox mt-checkbox-outline">
                                            <input type="checkbox" name="remember" value="1" /> Remember me
                                            <span></span>
                                        </label>
                                    </div>--%>
                                </div>
                                <div class="col-sm-8 text-right">
                                    <%--<input type="button" name="btnsubmit" class="btn green" value="Sign In" />--%>
                                    <asp:Button CssClass="btn green" OnClick="BtnLoginClick" ID="btnLogin" Text="Sign In" runat="server" />
                                    <%--<button class="btn green" type="submit">Sign In</button>--%>
                                </div>
                            </div>
                        </div>
                        <!-- BEGIN FORGOT PASSWORD FORM -->
          
                        <!-- END FORGOT PASSWORD FORM -->
                    </div>
                    <div class="login-footer">
                        <div class="row bs-reset">
                            <div class="col-xs-5 bs-reset">
                             
                            </div>
                            <div class="col-xs-7 bs-reset">
                                <div class="login-copyright text-right">
                                    <p>Copyright &copy; BMC Solution 2016</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

         <!-- END : LOGIN PAGE 5-1 -->
        <!--[if lt IE 9]>
        <script src="../assets/global/plugins/respond.min.js"></script>
        <script src="../assets/global/plugins/excanvas.min.js"></script> 
        <![endif]-->
        <!-- BEGIN CORE PLUGINS -->
        <script src="../js/jquery.js"></script>
        <script src="../js/bootstrap.min.js"></script>
        <%--<script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>--%>
        <%--<script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>--%>

        <!-- END CORE PLUGINS -->
     

    </form>
 
</body>
</html>
