<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Pages_Dashboard_Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
    <%-- View Port Meta data --%>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <%-- bootstrap 5.0.2 css--%>
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <%-- Datepicker --%>
    <link href="../../CustomStyles/DatePicker.css" rel="stylesheet" />
    <%-- Dashboard --%>
    <link href="Dashboard.css" rel="stylesheet" />
    <%-- Master Navbar --%>
    <link href="../../CustomStyles/Master.css" rel="stylesheet" />

    <%-- jQuery 3.6.0  --%>
    <script defer src='<%=ResolveUrl("~/Scripts/jquery-3.6.0.min.js") %>'></script>
    <%-- popper.js 1.16.0 --%>
    <script defer src='<%=ResolveUrl("~/Scripts/umd/popper.min.js") %>'></script>
    <%--  bootstrap 5.0.2 --%>
    <script defer src='<%=ResolveUrl("~/Scripts/bootstrap.min.js") %>'></script>
    <%-- Sweet Alert --%>
    <script defer src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <%-- flatPicker --%>
    <script src='<%=ResolveUrl("../../Scripts/DatePicker/flatPicker.js") %>'></script>

    <%-- Google Chart --%>
    <script defer src='<%=ResolveUrl("../../Scripts/GoogleChart/GoogleChart.js")%>'></script>
    <%-- Dashboard chart --%>
    <script defer type="module" src='<%=ResolveUrl("DashboardCharts.js")%>'></script>

    <%-- Notification --%>
    <script defer src='<%=ResolveUrl("Notification.js")%>'></script>

    <%-- youtube live --%>
    <%--<script src="https://www.youtube.com/iframe_api"></script>--%>

    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <script lang="javascript" type="text/javascript">
        function successalert(sMsg) {
            swal({
                title: 'Fitness',
                text: sMsg,
                icon: "success"
            });
        }
        function infoalert(sMsg) {
            swal({
                title: 'Fitness',
                text: sMsg,
                icon: "info"
            });
        }
        function erroralert(sMsg) {
            swal({
                title: 'Fitness',
                text: sMsg,
                icon: "error"
            });
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="frmDashboard" runat="server">
        <div id="divOverlay_loader" class="overlay_loader d-none">
            <div class="loaderGif"></div>
        </div>
        <script src="loader.js"></script>

        <nav class="navbar ud-navbar navbar-expand-lg navbar-light bg-light">
            <div class="container-fluid">
                <div class="divBrand">
                    <div class="col-12">
                        <div class="ud-brand row">
                            <div class="col-12">
                                <div class="brandAndLogo">
                                    <a id="btnHome" runat="server" class="brand-logo" href="javascript:void(0)"></a>
                                    <%--<asp:LinkButton ID="btnHome" class="brand-logo" runat="server" PostBackUrl="~/Home.aspx"></asp:LinkButton>--%>
                                    <div class="brand-DesContainer row">
                                        <div class="col-12 description_1">
                                            <asp:Label ID="lblGymName" runat="server"></asp:Label></div>
                                        <%--<div class="col-12 description_2">fitness | faith | fulfilment</div>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="col-2">
                            </div>
                        </div>
                    </div>
                </div>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse justify-content-end" id="navbarNav">
                    <ul class="navbar-nav navlist-container">
                        <li class="nav-item">
                            <asp:Label
                                ID="lblBranch_Dashboard"
                                class="nav-link branchName"
                                ClientIDMode="Static"
                                runat="server"></asp:Label>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton
                                ID="lnkBtnMyPlan"
                                runat="server"
                                class="nav-link myPlanLink"
                                OnClick="lnkBtnMyPlan_Click">myplan
                            </asp:LinkButton>
                        </li>

                        <li class="nav-item">
                            <a id="btnUserProfile" runat="server" visible="false">
                                <asp:Image ID="userimg" runat="server" CssClass="UserProfile" ImageUrl="~/Images/Login/UserProfile.png" />
                            </a>
                        </li>
                        <li id="listMyprofile" runat="server" visible="false">
                            <asp:LinkButton ID="lnkMyProfile" CssClass="lblProfileUserName" runat="server" OnClick="lnkMyProfile_Click">
                                <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                <img runat="server" class="imgProDownarrow" src="~/Images/Login/ProfileDownArrow.png" />
                            </asp:LinkButton>
                        </li>
                        <li class="liNotifiction">
                            <asp:LinkButton CssClass="lnkNotification" ID="lnkNotification" ClientIDMode="Static" runat="server">
                                🔔
                                <asp:Label ID="lblNotifCount" ClientIDMode="Static" runat="server" CssClass="lblNotifiCount">0</asp:Label>
                                <div runat="server" class="notificationListDiv d-none" id="divshownotification">
                                    <%-- <asp:Label runat ="server" CssClass="lblNotificationList" ID="lblNotificationList">Live Starts Today 12 PM Today
                                        </asp:Label>--%>
                                </div>
                            </asp:LinkButton>
                        </li>
                        <li id="btnGetApp" class="nav-item" runat="server">
                            <a class="nav-link btnGetApp" runat="server" target="_blank" rel="noopener noreferrer"
                                href="https://play.google.com/store/apps/details?id=com.rocks.fit">GET APP</a>
                        </li>
                        <li id="lstLogout" runat="server" class="nav-item dropdown">
                            <asp:LinkButton ID="btnLogout" runat="server" OnClick="btnLogout_Click"
                                class="nav-link btnlogin">Logout</asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
        <section class="container-fluid px-0 px-sm-5 px-md-5 px-lg-5 px-xl-5">
            <div class="DashboardChart-Container">
                <%-- Header --%>
                <div class="Header-Conatiner">
                    <div class="row">
                        <div class="col-12 col-sm-12 col-md-4 col-lg-4 col-xl-4">
                            <div class="Heading-Container">
                                <span class="hdr_common hdr_1">Hello,
                                    <asp:Label ID="lblUserName_Dashboard" runat="server"></asp:Label>
                                    <img src="../../Images/Dashboard/wave.png" class="ImgWave" />
                                </span>
                                <span class="hdr_common hdr_2">Here is your fitness summary</span>
                            </div>
                        </div>
                        <%-- Date --%>
                        <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8">
                            <div class="row Date-Container">
                                <div class="col-12 col-sm-12 col-md-6 col-lg-3 col-xl-3 divFromDate mb-2">
                                    <h5 class="hdnFromdateTodate_cmn">from date</h5>
                                    <asp:TextBox
                                        ID="txtDashboard_FromDate"
                                        ClientIDMode="Static"
                                        class="txtFromAndToDate fromDatePicker form-control"
                                        runat="server"></asp:TextBox>

                                </div>
                                <div class="col-12 col-sm-12 col-md-6 col-lg-3 col-xl-3 divToDate mb-2">
                                    <h5 class="hdnFromdateTodate_cmn">to date</h5>
                                    <asp:TextBox
                                        ID="txtDashboard_ToDate"
                                        ClientIDMode="Static"
                                        class="txtFromAndToDate toDatePicker form-control"
                                        runat="server"></asp:TextBox>
                                </div>
                                <div class="col-12 col-sm-12 col-md-6 col-lg-3 col-xl-3 divBtnSearch mb-2">
                                    <asp:Button
                                        ID="btnSearch_Dashboard"
                                        ClientIDMode="Static"
                                        runat="server"
                                        CssClass="btn btn-primary btnSearch"
                                        Text="Search" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%-- charts --%>
                <div class="Chart-Container">
                    <div class="row">
                        <div class="col-12 col-sm-12 col-md-12 col-lg-8 col-xl-8">
                            <div class="row ">
                                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3">
                                    <div id="curve_Calories" class="CmnCharts"></div>
                                </div>
                                <div class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6 mb-3">
                                    <div id="chart_Activities" class="CmnCharts"></div>
                                </div>
                                <div class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6 mb-3">
                                    <div id="curve_FoodNutriants" class="CmnCharts"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-4 col-xl-4">
                            <div class="Hearbeat-Container mb-4 d-none">
                                <div class="ImgHeart-Container">
                                    <img src="../../Images/Dashboard/Heart.png" class="ImgHeart" />
                                </div>
                                <div class="Heartbeat-Description">
                                    <span class="DescCmn Desc_1">80 Beats/min</span>
                                    <span class="DescCmn Desc_2">Heart Rate</span>
                                </div>
                                <div class="HeartRate-Container">
                                    <img src="../../Images/Dashboard/HeartRate.png" class="ImghHeartRate" />
                                </div>
                            </div>

                            <div class="WaterConsumption-Container mb-4 d-none">
                                <div class="ImgWater-Container">
                                    <img src="../../Images/Dashboard/waterGlass.png" width="50" height="50" />
                                </div>
                                <div class="Water-Description">
                                    <span class="DescCmn Desc_1">10 Glasses</span>
                                    <span class="DescCmn Desc_2">Water Consumption</span>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="w-100 h-auto youtubeVideo" id="player" runat="server"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <asp:HiddenField ID="hdnLiveUrl" runat="server" />





        <div id="MyProfile" runat="server" class="UsrProfileDiv" visible="false">
            <div class="MyProfile divresponsive">
                <div class="row">
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                        <img runat="server" id="UserProfileImg" class="userimg" />
                    </div>
                    <div id="DivProfile" runat="server" class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8 userDetailsdiv">
                        <asp:LinkButton ID="lnkProfileBtnClose" runat="server" CssClass="LnkProfileClose"
                            OnClick="lnkProfileBtnClose_Click">X</asp:LinkButton>
                        <div class="userDetails">
                            <div class="text-start">
                                <asp:ImageButton ID="btnProEdit" ImageUrl="~/Images/Login/Edit.png"
                                    class="logoedit" runat="server" OnClick="btnProEdit_Click" />
                                <asp:Label ID="lblProUserFName" CssClass="lblUserName" runat="server">
                                </asp:Label>
                                <asp:Label ID="lblProUserLName" CssClass="lblUserName" runat="server"></asp:Label>
                            </div>
                            <div class="text-start">
                                <asp:Label ID="lblProAge" CssClass="lblUser" runat="server"></asp:Label>
                                <asp:Label ID="lblProGender" CssClass="lblUser" runat="server"></asp:Label>
                            </div>
                            <div class="text-start">
                                <asp:Label ID="lblProWeight" CssClass="lblUser" runat="server"></asp:Label>
                                <asp:Label ID="lblProHeight" CssClass="lblUser" runat="server"></asp:Label>
                            </div>
                            <div class="text-start">
                                <asp:Label ID="lblProBMI" CssClass="lblUser" runat="server"></asp:Label>
                                <asp:Label ID="lblProBMR" CssClass="lblUser" runat="server"></asp:Label>
                                <asp:Label ID="lblProTDEE" CssClass="lblUser" runat="server"></asp:Label>
                            </div>

                            <div class="divOptions">
                                <asp:LinkButton ID="lnkMyPlan" runat="server" CssClass="btnOptions" OnClick="lnkMyPlan_Click">
                                     &nbsp&nbsp🏋️‍♂️  My Plan &nbsp&nbsp
                                    <img runat="server" class="Img1" src="~/Images/Login/arrow.png" /></asp:LinkButton>&nbsp&nbsp
                                <asp:LinkButton ID="btnMyBodyTest" runat="server" CssClass="btnOptions"
                                    OnClick="btnMyBodyTest_Click">
                                      &nbsp&nbsp📝  In Body Test &nbsp&nbsp
                                    <img runat="server" class="Img1" src="~/Images/Login/arrow.png" /></asp:LinkButton>&nbsp&nbsp
                                <asp:LinkButton ID="btnMyBooking" runat="server" CssClass="btnOptions"
                                    OnClick="btnMyBooking_Click">
                                    &nbsp&nbsp🎫  My Booking &nbsp&nbsp
                                    <img runat="server" class="Img1" src="~/Images/Login/arrow.png" /></asp:LinkButton>&nbsp&nbsp
                                   <asp:LinkButton ID="btnMySubs" runat="server" CssClass="btnOptions"
                                       OnClick="btnMySubs_Click">
                                    &nbsp&nbsp🔁 My Subscription &nbsp&nbsp
                                    <img runat="server" class="Img1" src="~/Images/Login/arrow.png" /></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div id="DivEditProfile" runat="server" visible="false"
                        class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">
                        <label class="lblHead">My Profile</label>
                        <div class="DivMyProfile">
                            <div class="row">
                                <div class="col-12 col-sm-9 col-md-9 col-lg-9 col-xl-9">
                                    <div class="row mb-3">
                                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                            <div class="txtboxdiv">
                                                <asp:TextBox ID="txtFirstName" AutoComplete="off"
                                                    CssClass="txtbox" runat="server" placeholder=" " />
                                                <asp:Label ID="lblMobile" CssClass="txtlabel" runat="server">
                                                    First Name <span class="reqiredstar">*</span></asp:Label>
                                            </div>
                                            <asp:RequiredFieldValidator
                                                ID="rfvtxtPassWord" ValidationGroup="MyProfile"
                                                ControlToValidate="txtFirstName" runat="server" CssClass="rfvStyle"
                                                Display="Dynamic" ErrorMessage="Enter First Name">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                            <div class="txtboxdiv">
                                                <asp:TextBox ID="txtLastName" AutoComplete="off"
                                                    CssClass="txtbox" runat="server" placeholder=" " />
                                                <asp:Label ID="lblLastName" CssClass="txtlabel"
                                                    runat="server">Last Name </asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3 text-start">
                                            <asp:Label ID="lblgender" CssClass="lblGender"
                                                runat="server">Gender <span class="reqiredstar">*</span></asp:Label>
                                            <asp:RadioButtonList ID="rbtnGender" runat="server"
                                                CssClass="rbnlbl" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="M">Male</asp:ListItem>
                                                <asp:ListItem Value="F">Female</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator1" ValidationGroup="MyProfile"
                                                ControlToValidate="rbtnGender" runat="server" CssClass="rfvStyle"
                                                Display="Dynamic" ErrorMessage="Select  Gender">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3 text-start">
                                            <asp:Label ID="Label3" CssClass="lblGender" runat="server">
                                                Marital Status </asp:Label>
                                            <asp:RadioButtonList ID="rbtnMaritalStatus" CssClass="rbnlbl"
                                                runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="S">Single</asp:ListItem>
                                                <asp:ListItem Value="M">Married</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                            <div class="txtboxdiv">
                                                <asp:TextBox ID="txtDOB" AutoComplete="off"
                                                    CssClass="txtbox fromDatePicker" runat="server" placeholder=" " />
                                                <asp:Label ID="Label4" CssClass="txtlabel" runat="server">DOB 
                                                    <span class="reqiredstar">*</span></asp:Label>
                                            </div>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator2" ValidationGroup="MyProfile"
                                                ControlToValidate="txtDOB" runat="server" CssClass="rfvStyle"
                                                Display="Dynamic" ErrorMessage="Enter  DOB">
                                            </asp:RequiredFieldValidator>
                                        </div>


                                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                            <div class="txtboxdiv">
                                                <asp:TextBox ID="txtMobileNo" AutoComplete="off"
                                                    onkeypress="return isNumber(event);" MaxLength="10" Enabled="false"
                                                    CssClass="txtbox" runat="server" placeholder=" " />
                                                <asp:Label ID="Label5" CssClass="txtlabel" runat="server">
                                                    Mobile No <span class="reqiredstar">*</span></asp:Label>
                                            </div>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator3" ValidationGroup="MyProfile"
                                                ControlToValidate="txtMobileNo" runat="server" CssClass="rfvStyle"
                                                Display="Dynamic" ErrorMessage="Enter  Mobile No">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator
                                                ID="revMobileNo" runat="server" ControlToValidate="txtMobileNo"
                                                ErrorMessage="Invalid Mobile No."
                                                ValidationExpression="[0-9]{10}" CssClass="rfvStyle"
                                                Display="Dynamic"
                                                ValidationGroup="MyProfile">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                            <div class="txtboxdiv">
                                                <asp:TextBox ID="txtMailId" AutoComplete="off"
                                                    CssClass="txtbox" runat="server" placeholder=" " />
                                                <asp:Label ID="Label6" CssClass="txtlabel" runat="server">Mail Id </asp:Label>
                                            </div>
                                            <asp:RegularExpressionValidator
                                                ID="revEmailId" runat="server" ControlToValidate="txtMailId"
                                                ErrorMessage="Invalid Email Id."
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                CssClass="rfvStyle"
                                                Display="Dynamic">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                            <div class="txtboxdiv">
                                                <asp:TextBox ID="txtAddress1" AutoComplete="off"
                                                    CssClass="txtbox" runat="server" placeholder=" " />
                                                <asp:Label ID="Label1" CssClass="txtlabel"
                                                    runat="server">Address 1 </asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                            <div class="txtboxdiv">
                                                <asp:TextBox ID="txtAddress2" AutoComplete="off"
                                                    CssClass="txtbox" runat="server" placeholder=" " />
                                                <asp:Label ID="Label2" CssClass="txtlabel"
                                                    runat="server">Address 2 </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3">
                                            <div class="row">
                                                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                                    <div class="txtboxdiv">
                                                        <asp:TextBox ID="txtPincode" AutoComplete="off"
                                                            TabIndex="10" MaxLength="6" onkeypress="return isNumber(event);"
                                                            CssClass="txtbox"
                                                            onchange="myFunction()" runat="server" placeholder=" " />
                                                        <asp:Label CssClass="txtlabel" runat="server">
                                                            Pin Code <span class="reqiredstar">*</span></asp:Label>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="RfvPincode"
                                                        ValidationGroup="MyProfile" ControlToValidate="txtPincode"
                                                        runat="server" CssClass="rfvStyle"
                                                        ErrorMessage="Enter Picode">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                                    <span runat="server" class="spanstyle">City :
                                                    </span>
                                                    <asp:Label ID="txtCity" CssClass="lblstyle"
                                                        runat="server"></asp:Label>
                                                    <br />
                                                    <span runat="server" class="spanstyle">District :
                                                    </span>
                                                    <asp:Label ID="txtDistrict"
                                                        CssClass="lblstyle" runat="server"></asp:Label>
                                                    <br />
                                                    <span runat="server" class="spanstyle">State :
                                                    </span>
                                                    <asp:Label ID="txtState" CssClass="lblstyle"
                                                        runat="server"></asp:Label>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                    <img id="imgpreview" clientidmode="Static" runat="server"
                                        class="imgpreview" src="~/Pages/MyProfile/User.png" />
                                    <asp:FileUpload ID="fuimage" CssClass="mx-4" TabIndex="3"
                                        runat="server" onchange="showpreview(this);" />
                                </div>
                            </div>

                            <div class="DivSubmit">
                                <asp:Button CssClass="btnSubmit" ID="btnSubSubmit" runat="server" Text="Update"
                                    ValidationGroup="MyProfile" OnClick="btnSubSubmit_Click" />
                                <asp:Button ID="btnSubCancel" CssClass="btnCancel" runat="server"
                                    Text="Cancel" OnClick="btnSubCancel_Click" />
                            </div>
                        </div>

                    </div>
                    <div id="divMyBodyTest" runat="server" visible="false" class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">
                        <asp:LinkButton ID="lnkbtnMyBodytClose" runat="server" CssClass="LnkProfileCloseBody"
                            OnClick="lnkbtnMyBodytClose_Click">X</asp:LinkButton>
                        <div id="head" style="display: flex; margin-top: 2rem;">

                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 divTitle">
                                <label class="lblHead">In Body Test</label>
                            </div>
                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 divAdd">
                                <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click" CssClass="AddPlus">
                  <img runat="server" src="~/Images/UserInBodyTest/images.png" class="btnAdd1" /></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divGrid" class="showContent divresponsive1" runat="server">
                            <asp:Button ID="btnfake" runat="server" OnClick="OnClick" Style="display: none" />
                            <asp:HiddenField ID="hfColumnRepeat" ClientIDMode="Static" runat="server" Value="3" />
                            <asp:DataList ID="dtlUserBodyTest" runat="server" Width="100%" RepeatColumns="2">
                                <ItemTemplate>
                                    <div id="divtargetItems" runat="server"
                                        class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6
                                        divtargetItems2 ">
                                        <div class="row">
                                            <div class="col-3 col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                                <asp:Image ID="Img" runat="server"
                                                    ImageUrl='<%#Bind("Image") %>' CssClass="ImageStyle" />
                                            </div>
                                            <div class="col-9 col-sm-9 col-md-9 col-lg-9 col-xl-9"
                                                style="text-align: center">
                                                <div class="row">
                                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                        <asp:Label ID="Label1" runat="server"
                                                            CssClass="divlbl" Text="Height :"></asp:Label>
                                                        <asp:Label ID="lblheight" CssClass="divlabel"
                                                            runat="server" Text='<%#Bind("height") %>'></asp:Label>

                                                    </div>
                                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                        <asp:Label ID="Label3" runat="server" CssClass="divlbl"
                                                            Text="Weight :"></asp:Label>
                                                        <asp:Label ID="lblweight" CssClass="divlabel"
                                                            runat="server" Text='<%#Bind("weight") %>'></asp:Label>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                        <asp:Label ID="Label2" runat="server" CssClass="divlbl"
                                                            Text="BMI :"></asp:Label>
                                                        <asp:Label ID="lblBMI" CssClass="divlabel" runat="server"
                                                            Text='<%#Bind("BMI") %>'></asp:Label>

                                                    </div>
                                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                        <asp:Label ID="Label5" runat="server" CssClass="divlbl"
                                                            Text="BMR :"></asp:Label>
                                                        <asp:Label ID="lblBMR" CssClass="divlabel" runat="server"
                                                            Text='<%#Bind("BMR") %>'></asp:Label>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                        <asp:Label ID="Label7" runat="server" CssClass="divlbl"
                                                            Text="TDEE :"></asp:Label>
                                                        <asp:Label ID="lblTDEE" CssClass="divlabel" runat="server"
                                                            Text='<%#Bind("TDEE") %>'></asp:Label>

                                                    </div>

                                                </div>
                                                <div class="row divWeightRange">
                                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 
                                                        divlabel divdate">
                                                        <asp:Label ID="Label4" CssClass="divlabel" runat="server"
                                                            Text='<%#Bind("Weightrange") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6  
                                                        divdate"
                                                        style="text-align: end">
                                                        <asp:Image ImageUrl="~/Images/UserInBodyTest/DateTime.svg"
                                                            CssClass="date" runat="server"></asp:Image>
                                                        <asp:Label ID="lbldate" CssClass="lblMyBodyDate" runat="server" Text='<%#Bind("date") %>'>
                                                        </asp:Label>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>


                                    </div>
                                    <asp:Label ID="lbluserId" runat="server" Text='<%#Bind("userId") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Bind("UserName") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lbldob" runat="server" Text='<%#Bind("dob") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblgender" runat="server" Text='<%#Bind("gender") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblfatPercentage" runat="server"
                                        Text='<%#Bind("fatPercentage") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblWorkOutStatus" runat="server"
                                        Text='<%#Bind("WorkOutStatus") %>' Visible="false"> </asp:Label>
                                    <asp:Label ID="lblWorkOutValue" runat="server"
                                        Text='<%#Bind("WorkOutValue") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblage" runat="server" Text='<%#Bind("age") %>'
                                        Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <div class="DivMyProfile" id="divUserForm" runat="server" visible="false">
                            <div class="row">
                                <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5 mb-3">
                                    <div class="txtboxdiv">
                                        <asp:TextBox ID="txtname" AutoComplete="off"
                                            CssClass="txtbox" runat="server" placeholder=" " TabIndex="1" />
                                        <asp:Label CssClass="txtlabel" runat="server">Name 
                                            <span class="reqiredstar">*</span></asp:Label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="rfvName" ValidationGroup="UserEnroll"
                                        ControlToValidate="txtname" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Name">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                    <div class="txtboxdiv">
                                        <asp:TextBox ID="txtMBTDOB" AutoComplete="off"
                                            CssClass="txtbox fromDatePicker" runat="server" TabIndex="2" placeholder=" " />
                                        <asp:Label CssClass="txtlabel" runat="server">Date Of Birth <span class="reqiredstar">*</span></asp:Label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RfvDOB"
                                        ValidationGroup="UserEnroll" ControlToValidate="txtDOB"
                                        runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Date Of Birth">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                    <asp:DropDownList ID="ddlGender" CssClass="form-select" runat="server" TabIndex="3"
                                        OnSelectedIndexChanged="ddlGender_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Gender *</asp:ListItem>
                                        <asp:ListItem Value="F">Female </asp:ListItem>
                                        <asp:ListItem Value="M">Male </asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0"
                                        ValidationGroup="UserEnroll" ControlToValidate="ddlGender"
                                        runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Select Gender">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                    <div class="txtboxdiv">
                                        <asp:TextBox ID="txtage" AutoComplete="off"
                                            CssClass="txtbox" runat="server" placeholder=" "
                                            TabIndex="4" MaxLength="3"
                                            onkeypress="return isNumber(event);"
                                            OnTextChanged="txtage_TextChanged" AutoPostBack="true" />
                                        <asp:Label CssClass="txtlabel" runat="server">Age 
                                            <span class="reqiredstar">*</span></asp:Label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="Rfvage"
                                        ValidationGroup="UserEnroll" ControlToValidate="txtage"
                                        runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Age">
                                    </asp:RequiredFieldValidator>
                                </div>

                            </div>


                            <div class="row">
                                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                    <div class="txtboxdiv">
                                        <asp:TextBox ID="txtweight" AutoComplete="off"
                                            CssClass="txtbox" runat="server" placeholder=" " TabIndex="5"
                                            onkeypress="return AllowOnlyAmountAndDot(this.id);" MaxLength="4"
                                            OnTextChanged="txtweight_TextChanged" AutoPostBack="true" />
                                        <asp:Label CssClass="txtlabel" runat="server">Weight in Kg 
                                            <span class="reqiredstar">*</span></asp:Label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="Rfvweight"
                                        ValidationGroup="UserEnroll" ControlToValidate="txtweight"
                                        runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Weight">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                    <div class="txtboxdiv">
                                        <asp:TextBox ID="txtheight" AutoComplete="off"
                                            CssClass="txtbox" runat="server" placeholder=" " TabIndex="6"
                                            onkeypress="return AllowOnlyAmountAndDot(this.id);" MaxLength="4"
                                            OnTextChanged="txtheight_TextChanged" AutoPostBack="true" />
                                        <asp:Label CssClass="txtlabel" runat="server">Height in cms 
                                            <span class="reqiredstar">*</span></asp:Label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="Rfvheight" ValidationGroup="UserEnroll"
                                        ControlToValidate="txtheight" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Height">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                    <div class="txtboxdiv">
                                        <asp:TextBox ID="txtfat" AutoComplete="off"
                                            CssClass="txtbox" runat="server" placeholder=" " TabIndex="7"
                                            onkeyup="this.value = minmax(this.value, 0, 100);"
                                            onkeypress="return isNumber(event);"
                                            MaxLength="3" />
                                        <asp:Label CssClass="txtlabel" runat="server">Fat % 
                                            <span class="reqiredstar">*</span></asp:Label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RfvFat" ValidationGroup="UserEnroll"
                                        ControlToValidate="txtfat" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Fat">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5 mb-3">
                                    <asp:DropDownList ID="ddlWorkOutDetails" CssClass="form-select"
                                        runat="server" TabIndex="8"
                                        OnSelectedIndexChanged="ddlWorkOutDetails_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="0">WorkOut Details *</asp:ListItem>
                                        <asp:ListItem Value="1.2">I Don't Workout</asp:ListItem>
                                        <asp:ListItem Value="1.375">1-5 times/week </asp:ListItem>
                                        <asp:ListItem Value="1.55">3-7 times/week  </asp:ListItem>
                                        <asp:ListItem Value="1.9">7+ times/week </asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                        InitialValue="0" ValidationGroup="UserEnroll"
                                        ControlToValidate="ddlWorkOutDetails" runat="server"
                                        CssClass="rfvStyle"
                                        ErrorMessage="Select WorkOutDetails">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                    <div class="txtboxdiv">
                                        <asp:TextBox ID="txtBMI" AutoComplete="off" CssClass="txtbox"
                                            runat="server" placeholder=" " TabIndex="9" Enabled="false" />
                                        <asp:Label CssClass="txtlabel" runat="server">BMI 
                                            <span class="reqiredstar">*</span></asp:Label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RfvBMI" ValidationGroup="UserEnroll"
                                        ControlToValidate="txtBMI" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter BMI">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                    <div class="txtboxdiv">
                                        <asp:TextBox ID="txtBMR" AutoComplete="off" CssClass="txtbox"
                                            runat="server" placeholder=" " TabIndex="10" Enabled="false" />
                                        <asp:Label CssClass="txtlabel" runat="server">BMR 
                                            <span class="reqiredstar">*</span></asp:Label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RfvBMR" ValidationGroup="UserEnroll"
                                        ControlToValidate="txtBMR" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter BMR">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                    <div class="txtboxdiv">
                                        <asp:TextBox ID="txtTDEE" AutoComplete="off" CssClass="txtbox"
                                            runat="server" placeholder=" " TabIndex="11" Enabled="false" />
                                        <asp:Label CssClass="txtlabel" runat="server">TDEE 
                                            <span class="reqiredstar">*</span></asp:Label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RfvTDEE" ValidationGroup="UserEnroll"
                                        ControlToValidate="txtTDEE" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter TDEE">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="text-end">
                                <asp:Button CssClass="btnSubmit" ID="btnMyBodySubmit" TabIndex="12"
                                    OnClick="btnMyBodySubmit_Click"
                                    runat="server" Text="Submit" ValidationGroup="UserEnroll" />
                                <asp:Button ID="btnMyBodyCancel" OnClick="btnMyBodyCancel_Click"
                                    CssClass="btnCancel" TabIndex="13"
                                    CausesValidation="false" runat="server" Text="Cancel" />
                            </div>
                        </div>
                    </div>
                    <div id="DivMyBooking" runat="server" visible="false" class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">
                        <asp:LinkButton ID="lnkMyBookingClose" runat="server" CssClass="LnkProfileCloseBody"
                            OnClick="lnkMyBookingClose_Click">X</asp:LinkButton>
                        <div id="MyBookinghead" style="display: flex; margin-top: 2rem;">

                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 divTitle">
                                <label class="lblHead">My Booking</label>
                            </div>
                        </div>
                        <div id="DivBookingList" class="DivMyBooking" runat="server">
                            <asp:DataList ID="dtlMyBooking" runat="server" Width="100%" RepeatColumns="2">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk" CssClass="lnkDivBooking" runat="server" OnClick="lnk_Click">
                                        <div id="divMyDtlBooking" runat="server">
                                            <div class="row">
                                                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12"
                                                    style="text-align: center">
                                                    <div class="row">
                                                        <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 divBookingID">
                                                            <asp:Label ID="Label1" runat="server"
                                                                CssClass="lblBooking" Text="BookingID :"></asp:Label>
                                                            <asp:Label ID="lblBookingID" CssClass="lblBookingID"
                                                                runat="server" Text='<%#Bind("bookingId") %>'></asp:Label>
                                                        </div>
                                                        <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                            <asp:Label ID="lblUserName" runat="server" Text='<%#Bind("UserName") %>'
                                                                CssClass="divBookinglabel"></asp:Label>
                                                            <asp:Label ID="lblcategoryName" CssClass="divBookinglabel"
                                                                runat="server" Text='<%#Bind("categoryName") %>'></asp:Label>
                                                            <asp:Label ID="lbltrainingTypeName" CssClass="divBookinglabel" runat="server"
                                                                Text='<%#Bind("trainingTypeName") %>'></asp:Label>
                                                            <asp:Label ID="lbltotalAmount" CssClass="divBookinglabel" runat="server"
                                                                Text='<%#"₹"+ Eval("totalAmount") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <asp:Label ID="Label9" runat="server"
                                                        CssClass="lblDuration" Text="Duration :">
                                                        <asp:Label ID="lblPlaneDuration" CssClass="lblDuration" runat="server"
                                                            Text='<%#Bind("PlaneDuration") %>'></asp:Label>
                                                    </asp:Label>
                                                </div>
                                            </div>


                                        </div>
                                    </asp:LinkButton>
                                    <asp:Label ID="lbluserId" runat="server" Text='<%#Bind("userId") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="Label10" runat="server" Text='<%#Bind("branchName") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="Label11" runat="server" Text='<%#Bind("phoneNumber") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblbookingDate" runat="server" Text='<%#Bind("bookingDate") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblfromDate" runat="server" Text='<%#Bind("fromDate") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblpaidAmount" runat="server" Text='<%#Bind("paidAmount") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblprice" runat="server" Text='<%#Bind("price") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lbltaxAmount" runat="server" Text='<%#Bind("taxAmount") %>'
                                        Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <div id="DivBookingDetails" class="DivBookingDetails" runat="server" visible="false">
                            <div class="row">
                                <asp:Label ID="lblMyBookingSummaryUserName" CssClass="lblMyBookingUserName" runat="server"></asp:Label>
                                <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 mt-2">
                                    <asp:Label CssClass="lblMyBookingDate" runat="server">Booking Date</asp:Label>
                                    <asp:Label ID="lblMyBookingBookingSummaryDate" CssClass="lblMyBookingDate" runat="server"></asp:Label>
                                </div>
                                <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 mt-2">
                                    <asp:Label CssClass="lblJoiningDate" runat="server">Joining Date</asp:Label>
                                    <asp:Label ID="lblMyBookingSummaryjoinDate" CssClass="lblJoiningDate" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row DivMyBookingSummary">
                                <asp:Label ID="lblMyBookingSummaryDuration" CssClass="lblMyBookingSummaryDuration" runat="server"></asp:Label>
                                <asp:Label ID="lblMyBookingSummaryPlan" CssClass="lblMyBookingSummaryHead" runat="server"></asp:Label>
                                <hr />
                                <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 DivMyBookingSubSummary">
                                    <asp:Label CssClass="lblMyBookingSummaryHead" runat="server">Amount</asp:Label>
                                    <asp:Label CssClass="lblMyBookingSummaryHead" runat="server">Tax </asp:Label>
                                    <asp:Label CssClass="lblMyBookingSummaryHead" runat="server">Total Amount</asp:Label>
                                    <asp:Label CssClass="lblMyBookingSummaryHead" runat="server">Paid Amount</asp:Label>
                                </div>
                                <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 DivMyBookingSubSummaryRight">
                                    <asp:Label ID="lblMyBookingSummaryAmt" CssClass="lblMyBookingSummary" runat="server"></asp:Label>
                                    <asp:Label ID="lblMyBookingSummaryTax" CssClass="lblMyBookingSummary" runat="server"></asp:Label>
                                    <asp:Label ID="lblMyBookingSummaryTotalAmt" CssClass="lblMyBookingSummary" runat="server"> </asp:Label>
                                    <asp:Label ID="lblMyBookingSummaryPaidAmt" CssClass="lblMyBookingSummary" runat="server"> </asp:Label>

                                </div>
                                <hr />
                                <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 DivMyBookingTotalSubSummary">
                                    <asp:Label CssClass="lblMyBookingSummaryTotalHead" runat="server">Total Payable</asp:Label>
                                </div>
                                <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 DivMyBookingTotalSubSummaryRight">
                                    <asp:Label ID="lblMyBookingSummaryTotal" CssClass="lblMyBookingSummaryTotal" runat="server"> </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="DivMySubs" runat="server" visible="false" class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">
                        <asp:LinkButton ID="lnkMySubsClose" runat="server" CssClass="LnkProfileCloseBody"
                            OnClick="lnkMySubsClose_Click">X</asp:LinkButton>
                        <div id="MySubshead" style="display: flex; margin-top: 2rem;">

                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 divTitle">
                                <label class="lblHead">My Subscription</label>
                            </div>
                        </div>
                        <div id="DivMySubsList" class="DivMyBooking" runat="server">
                            <asp:DataList ID="DtlMySubs" runat="server" Width="100%" RepeatColumns="2">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkMySubs" CssClass="lnkDivBooking" runat="server" OnClick="lnkMySubs_Click">
                                        <div id="divMyDtlSubs" runat="server">
                                            <div class="row">
                                                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12"
                                                    style="text-align: center">
                                                    <div class="row">
                                                        <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 divBookingID">
                                                            <asp:Label ID="Label1" runat="server"
                                                                CssClass="lblBookingID" Text="SubscriptionId :"></asp:Label>
                                                            <asp:Label ID="lblsubBookingId" CssClass="lblBookingID"
                                                                runat="server" Text='<%#Bind("subBookingId") %>'></asp:Label>
                                                        </div>
                                                        <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                            <asp:Label ID="lblUserName" runat="server" Text='<%#Bind("UserName") %>'
                                                                CssClass="divBookinglabel"></asp:Label>
                                                            <asp:Label ID="lblpackageName" CssClass="divBookinglabel"
                                                                runat="server" Text='<%#Bind("packageName") %>'></asp:Label>
                                                            <asp:Label ID="lbltotalAmount" CssClass="divBookinglabel" runat="server"
                                                                Text='<%#"₹"+ Eval("totalAmount") %>'></asp:Label>
                                                            <asp:Label ID="lblpaymentStatus" CssClass="divBookinglabel" runat="server"
                                                                Text='<%#Eval("paymentStatus") == "P" ? "Paid" : "NotPaid" %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <asp:Label ID="Label9" runat="server"
                                                        CssClass="lblDuration" Text="No Of Days :">
                                                        <asp:Label ID="lblPlaneDuration" CssClass="lblDuration" runat="server"
                                                            Text='<%#Bind("noOfDays") %>'></asp:Label>
                                                    </asp:Label>
                                                </div>
                                            </div>


                                        </div>
                                    </asp:LinkButton>
                                    <asp:Label ID="lbluserId" runat="server" Text='<%#Bind("userId") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblbookingDate" runat="server" Text='<%#Bind("bookingDate") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblfromDate" runat="server" Text='<%#Bind("fromDate") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblpaidAmount" runat="server" Text='<%#Bind("paidAmount") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblprice" runat="server" Text='<%#Bind("price") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lbltaxAmount" runat="server" Text='<%#Bind("taxAmount") %>'
                                        Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <div id="DivMySubsDetails" class="DivBookingDetails" runat="server" visible="false">
                            <div class="row">
                                <asp:Label ID="lblMySubsUserName" CssClass="lblMyBookingUserName" runat="server"></asp:Label>
                                <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 mt-2">
                                    <asp:Label CssClass="lblMyBookingDate" runat="server">Booking Date</asp:Label>
                                    <asp:Label ID="lblMySubsBookingDate" CssClass="lblMyBookingDate" runat="server"></asp:Label>
                                </div>
                                <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 mt-2">
                                    <asp:Label CssClass="lblJoiningDate" runat="server">Joining Date</asp:Label>
                                    <asp:Label ID="lblMySubsSummaryjoinDate" CssClass="lblJoiningDate" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row DivMyBookingSummary">
                                <asp:Label ID="lblMySubspackageName" CssClass="lblMyBookingSummaryDuration" runat="server"></asp:Label>
                                <asp:Label ID="lblMySubsPlanDuration" CssClass="lblMyBookingSummaryHead" runat="server"></asp:Label>
                                <hr />
                                <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 DivMyBookingSubSummary">
                                    <asp:Label CssClass="lblMyBookingSummaryHead" runat="server">Amount</asp:Label>
                                    <asp:Label CssClass="lblMyBookingSummaryHead" runat="server">Tax </asp:Label>
                                    <asp:Label CssClass="lblMyBookingSummaryHead" runat="server">Total Amount</asp:Label>
                                    <asp:Label CssClass="lblMyBookingSummaryHead" runat="server">Paid Amount</asp:Label>


                                </div>
                                <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 DivMyBookingSubSummaryRight">
                                    <asp:Label ID="lblMySubsSummaryAmt" CssClass="lblMyBookingSummary" runat="server"></asp:Label>
                                    <asp:Label ID="lblMySubsSummaryTax" CssClass="lblMyBookingSummary" runat="server"></asp:Label>
                                    <asp:Label ID="lblMySubsSummaryTotalAmt" CssClass="lblMyBookingSummary" runat="server"> </asp:Label>
                                    <asp:Label ID="lblMySubsSummaryPaidAmt" CssClass="lblMyBookingSummary" runat="server"> </asp:Label>

                                </div>
                                <hr />
                                <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 DivMyBookingTotalSubSummary">
                                    <asp:Label CssClass="lblMyBookingSummaryTotalHead" runat="server">Total Payable</asp:Label>
                                </div>
                                <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 DivMyBookingTotalSubSummaryRight">
                                    <asp:Label ID="lblMySubsSummaryTotal" CssClass="lblMyBookingSummaryTotal" runat="server"> </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <asp:HiddenField ID="hfTrainingMode" runat="server" />
        <asp:HiddenField ID="hfState" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hfDistrict" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hfCity" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hfNotificationPosturl" ClientIDMode="Static" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hfNotificationURl" ClientIDMode="Static" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hfTokenURl" runat="server" ClientIDMode="Static" EnableViewState="true" />
        <asp:HiddenField ID="hfNotificationURlData" ClientIDMode="Static" runat="server" EnableViewState="true" />
    </form>
    <script>
        // document.getElementById("txtPincode").addEventListener("change", myFunction);
        function myFunction() {
            var NewArea = $('[id*=txtPincode]').val();
            event.preventDefault();

            fetch("https://api.postalpincode.in/pincode/" + $('[id*=txtPincode]').val())
                .then(response => response.json())
                .then(
                    function (data) {
                        if (data[0].Status == 'Success') {
                            document.getElementById('<%=txtCity.ClientID%>').textContent = data[0].PostOffice[0].Block;
                            document.getElementById('<%=txtDistrict.ClientID%>').textContent = data[0].PostOffice[0].District;
                            document.getElementById('<%=txtState.ClientID%>').textContent = data[0].PostOffice[0].State;
                            $('#<%=hfCity.ClientID%>').val(data[0].PostOffice[0].Block);
                            $('#<%=hfDistrict.ClientID%>').val(data[0].PostOffice[0].District);
                            $('#<%=hfState.ClientID%>').val(data[0].PostOffice[0].State);
                            document.getElementById('<%=txtCity.ClientID%>').style.color = 'black';
                        }
                        else {
                            if (data[0].PostOffice == null) {
                                document.getElementById('<%=txtCity.ClientID%>').textContent = 'Invalid Pincode';
                                document.getElementById('<%=txtDistrict.ClientID%>').textContent = '';
                                document.getElementById('<%=txtState.ClientID%>').textContent = '';
                                document.getElementById('<%=txtCity.ClientID%>').style.color = 'red';
                            }

                        }
                    }
                )
                .catch()
        }

    </script>
    <script type="text/javascript">
        function showpreview(input) {
            var fup = document.getElementById("<%=fuimage.ClientID %>");
            var fileName = fup.value;
            var maxfilesize = 3840 * 2160;
            filesize = input.files[0].size;
            var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
            if (ext == "gif" || ext == "GIF" || ext == "PNG" || ext == "png" || ext == "jpg" || ext == "JPG" || ext == "bmp" || ext == "BMP" || ext == "jpeg" || ext == "JPEG") {
                if (filesize <= maxfilesize) {
                    if (input.files && input.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $('#<%=imgpreview.ClientID%>').prop('src', e.target.result);

                        };
                        reader.readAsDataURL(input.files[0]);

                    }
                }
                else {
                    swal("Please, Upload image file less than or equal to 10 MB !!!");
                    fup.focus();
                    return false;
                }
            }
            else {
                swal("Please, Upload Gif, Jpg, Jpeg or Bmp Images only !!!");
                fup.focus();
                return false;
            }
        }



        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <script>
        const trainingType = document.getElementById('hfTrainingMode').value;
        if (trainingType == 'O') {
            //// Check if the window is being duplicated
            //if (window.performance.navigation.type === 1) {
            //    // If the window is being duplicated, reload the page to reset the JavaScript code
            //    window.location.reload();
            //}

            var script = document.createElement("script");
            script.type = "text/javascript";
            script.src = "https://www.youtube.com/iframe_api";
            document.head.appendChild(script);

            function onPlayerReady(event) {
                event.target.playVideo();
            }
            function onPlayerStateChange(event) {
                // Handle player state changes
            }
            // This function is called by the YouTube API when it's ready
            function onYouTubeIframeAPIReady() {
                let videoId = document.getElementById('hdnLiveUrl').value;
                var player = new YT.Player('player', {
                    videoId,
                    playerVars: {
                        'autoplay': 1,
                        'controls': 1,
                        'modestbranding': 1,
                        'rel': 0,
                        'showinfo': 0,
                    },
                    events: {
                        'onReady': onPlayerReady,
                        'onStateChange': onPlayerStateChange
                    }
                });
            }
        }
    </script>
</body>
</html>
