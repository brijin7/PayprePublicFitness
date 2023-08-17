<%@ Page Title="Subscription" Language="C#" MasterPageFile="~/Fitness.Master" AutoEventWireup="true" CodeFile="Subscription.aspx.cs" Inherits="Pages_Subscription_Subscription" %>

<%@ MasterType VirtualPath="../../Fitness.Master" %>
<asp:Content ID="CndSubscription" ContentPlaceHolderID="FitnessContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <%--    <link href="PaymentPaget.css" rel="stylesheet" />--%>
    <link href="PopupPaymentPage.css" rel="stylesheet" />
    <link href="../PaymentPage/Paymentpage.css" rel="stylesheet" />
    <script defer src='<%=ResolveUrl("../Master/Master.js") %>'></script>
    <link href="Subscription.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

    <style>
        .star {
            color: orange;
        }
    </style>
    <div class="SubsDiv">
        <div class="row SubsHead w-100 navbar">
            <div id="subsdiv" class="navbartop">
                <asp:DataList ID="dtlSub" runat="server" RepeatDirection="Horizontal">
                    <ItemTemplate>
                        <asp:LinkButton ID="lblSubscr" runat="server" OnClick="lblSubscr_Click" Font-Underline="false">
                            <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Bind("subscriptionPlanId") %>' class="ImgBack"></asp:Label>
                            <asp:Label ID="lblHead" runat="server" Text='<%# Bind("packageName") %>' class="ImgBack"></asp:Label>
                        </asp:LinkButton>

                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div class="row w-100">
            <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8 align-content-center">
                <div class="SubsContainer">
                    <img class="SubsImgBg" src="../../Images/Subscription/Subs.png" />
                    <div class="row OfferDiv">
                        <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                        </div>
                        <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 text-center">
                            <asp:Label ID="s1" runat="server" class="fa fa-star"></asp:Label>
                            <asp:Label ID="s2" runat="server" class="fa fa-star"></asp:Label>
                            <asp:Label ID="s3" runat="server" class="fa fa-star"></asp:Label>
                            <asp:Label ID="s4" runat="server" class="fa fa-star"></asp:Label>
                            <asp:Label ID="s5" runat="server" class="fa fa-star"></asp:Label>
                        </div>
                    </div>
                    <div class="row PlanHeadDiv mt-3">
                        <div class="col-4 col-sm-4 col-md-4 col-lg-4 col-xl-4 SubsSubPlanDiv">
                            <asp:Label runat="server" ID="lblSubsName" class="SubsDays"></asp:Label>
                            <div class="SubsPlanDiv">
                                <label class="SubsSubs">Subscription</label>
                                <div>
                                    <asp:Label ID="lblSubsHead" runat="server" class="SubsPlan"></asp:Label>
                                </div>

                            </div>
                        </div>
                        <div class="col-4 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                            <div class="DivTiming text-center">
                                <label class="SubsFacilitiesHead">Timing 🕐</label>
                                <div>
                                    <asp:Label ID="lblfromTime" runat="server" class="SubsFac"></asp:Label><label class="SubsFac" runat="server">To</label>
                                    <asp:Label ID="lblToTime" class="SubsFac" runat="server"></asp:Label>
                                </div>
                            </div>

                        </div>
                        <div class="col-4 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                            <div class="divSubsAmt text-center">
                                <div>
                                    <label class="SubsAccess">(Access to Gym For Month)</label>
                                </div>
                                <div>
                                    <asp:Label ID="lblAmt" runat="server" class="SubsAmt"></asp:Label>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row mt-3">
                        <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                            <div class="DivEquipment">
                                <label class="SubsEquipmentHead">Equipments 🔩</label>
                                <div class="row">
                                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                        <div style="display: grid">
                                            <label class="SubsEquip">TreadMills </label>
                                            <label class="SubsEquip">Elipticals</label>
                                            <label class="SubsEquip">Exercise Cycles</label>
                                            <label class="SubsEquip">Stair Climbers</label>
                                            <label class="SubsEquip">Rowing Machines </label>
                                            <label class="SubsEquip">Free Weights</label>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                        <div style="display: grid">
                                            <label class="SubsEquip">Cable Crossovers    </label>
                                            <label class="SubsEquip">Kettlebells</label>
                                            <label class="SubsEquip">Lateral X Trainers</label>
                                            <label class="SubsEquip">Amt Crosstrainers</label>
                                            <label class="SubsEquip">Racks</label>
                                            <label class="SubsEquip">Synergy 360 Systems</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                            <div class="DivFacilities">
                                <label class="SubsFacilitiesHead">Facilities 💪</label>
                                <asp:DataList ID="dtlBenfSub" runat="server" RepeatDirection="Vertical">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblSubscr" runat="server" Font-Underline="false">
                                            <asp:Label ID="lblsubdes" runat="server" Text='<%# Bind("description") %>' class="SubsFac1"></asp:Label>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 align-content-center">
                <div class="col-12 Details2">
                    <div class="detailssummary">
                        <div class="ud-brand1 row">
                            <div class="row">
                                <p class="summaryleft" id="subdescription" runat="server">Free</p>
                                <p><span id="Span2" runat="server" class="summaryright">Joining Date</span></p>
                                <p class="summaryleft1">Subscription Plan<span id="subdate" runat="server" class="summaryright1"> 24/02/2023</span></p>
                            </div>
                            <div class="row">
                                <p class="summaryleft2" id="submonth" runat="server">1 Month</p>
                                <p class="summaryright2main"><span id="subamount" runat="server" class="summaryright2">₹ 24999.00 </span></p>
                            </div>
                            <hr style="margin: 2rem; border-top: 1px dashed #f8f9fa; width: 92%; opacity: 1" />
                            <div class="row">
                                <p class="summaryleftsub">
                                    Actual Amount &nbsp&nbsp&nbsp&nbsp&nbsp<span class="summaryrightsub2 amountstrick" id="ActualAmount" runat="server"> ₹ 24999.00</span>
                                </p>
                                <p>
                                    <span class="summaryrightsub2saveamount" id="saveamount" runat="server">₹ 24999.00</span>
                                </p>
                                <p id="Amountmain" runat="server" class="summaryleftsub saveamounttop">Amount &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<span id="Amount" runat="server" class="summaryrightsub2">  ₹ 24999.00 </span></p>
                            </div>
                            <hr style="margin: 2rem; opacity: 1; width: 92%;" />
                            <div class="row">
                                <p class="summaryleftbottom">Total Payable <span id="TotalAmount" runat="server" class="summaryrightbottom">₹ 24999.00 </span></p>
                            </div>

                            <div class="DivbtnBuy">
                                <asp:Button ID="btnBuyNow" runat="server" Text="Buy Now" CssClass="btnBuyNow" OnClick="btnBuyNow_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfSubscription" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfSubscriptionInsert" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfBaseurl" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfToken" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfBookingId" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfPaymentBaseurl" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfNotificationSMSBaseurl" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfNotificationSMSDatas" ClientIDMode="Static" runat="server" />
     <asp:HiddenField ID="hfNotificationSMSUserId" ClientIDMode="Static" runat="server" />

    <%--Login--%>
    <style>
        #google-button {
            margin-top: 0px;
        }

        .container {
            margin: 20px;
            background-color: rgba(66, 133, 244, 0.15);
            padding: 10px;
            border-radius: 10px;
            width: 450px;
            margin-top: 25px;
            display: none;
        }

        .heading-icon img {
            width: 30px;
            /*border-radius: 50%;*/
        }

        img {
            width: 132px;
            /*border-radius: 50%;*/
        }

        .id,
        .email,
        .name {
            display: inline-block;
            font-family: 'Verdana';
        }

        .name {
            font-size: 30px;
            position: relative;
            top: -16px;
            margin-left: 5px;
        }

        lable {
            font-family: 'Arial Black';
        }

        button {
            display: block;
            background-color: #4285F4;
            border: 0px;
            padding: 8px 20px;
            color: white;
            margin-top: 15px;
            cursor: pointer;
            outline: none;
        }

           .btnResnd {
            width: 105px;
            font-size: 12px;
            font-weight: bold;
            padding: 7px 12px;
            letter-spacing: 1px;
            text-transform: uppercase;
            transition: transform 80ms ease-in;
            border-radius: 0.5rem;
        }

        .rfvColor {
            color: red;
        }
    </style>

    <style>
        .QRlogo {
            background-color: #fff;
            border: 0.25rem solid #fff;
            border-radius: 0.25rem;
            box-shadow: 0 0.125rem 0.25rem rgb(0 0 0 / 25%);
            height: 11%;
            left: 73%;
            overflow: hidden;
            position: absolute;
            top: 39%;
            transform: translate(-50%, -50%);
            width: 6%;
        }

        #qrcode {
            display: flex;
            justify-content: center;
            align-items: center;
            width: 163px;
            height: 160px;
            margin-top: -20px;
            margin-left: 6rem;
        }

        .qrtimer {
            margin-bottom: 1rem;
            margin-top: 0rem;
        }

        @media (max-width: 375px) {
            .QRlogo {
                background-color: #fff;
                border: 0.25rem solid #fff;
                border-radius: 0.25rem;
                box-shadow: 0 0.125rem 0.25rem rgb(0 0 0 / 25%);
                height: 23%;
                left: 45%;
                overflow: hidden;
                position: absolute;
                top: 58%;
                transform: translate(-50%, -50%);
                width: 40%;
            }

            #qrcode {
                display: flex;
                justify-content: center;
                align-items: center;
                width: 163px;
                height: 160px;
                margin-top: -20px;
                margin-left: 5rem;
            }
        }
    </style>

    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <script>

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

        function SendOtp() {
            let seconds = 31;
            let button = document.querySelector('#<%=btnResend.ClientID%>');
            let buttonConfrm = document.getElementById('<%=btnCfmOtp.ClientID %>');
            function incrementSeconds() {

                seconds = seconds - 1;
                if (seconds < 10) {
                    button.value = '00:0' + seconds;
                    button.disabled = true;
                }
                else {
                    button.value = '00:' + seconds;
                    button.disabled = true;
                }
                if (seconds == 0) {
                    seconds = 31;
                    //buttonConfrm.style.display = 'none';
                    document.getElementById('<%=hfOtp.ClientID %>').value = "";
                    button.value = "ReSend OTP";
                    clearInterval(cancel);
                    button.disabled = false;
                }
            }
            var cancel = setInterval(incrementSeconds, 1000);
        }
    </script>
    <%--Login Stylesheet--%>
    <link href="../PaymentPage/popupLogin.css" rel="stylesheet" />
    <%--Content Page Css--%>
    <link href="../PaymentPage/popupContentPage.css" rel="stylesheet" />
    <div id="DivLogin" runat="server" class="DisplyCard1" visible="false">
        <div class="LoginCard">
            <div class="row Logincontent">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 LoginHeight">

                    <a>
                        <img src="../../Images/Login/PaypreLogo.png" class="LoginLogoImage" /></a>
                    <div class="LoginDatas">
                        <p class="LoginHeader">Let's Get Started</p>
                        <%--<p class="LoginSubHeader">Let's Get Started</p>--%>

                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtMobileNo" MaxLength="10" onkeydown="return (event.keyCode!=13)" MinLength="10" AutoComplete="off" ClientIDMode="Static"
                                onKeyPress="return isNumber(event)" CssClass="txtbox" runat="server" placeholder=" " />
                            <asp:Label ID="lblMobile" ClientIDMode="Static" CssClass="txtlabel" runat="server">Mobile No. <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvtxtMobileNo" runat="server"
                            ControlToValidate="txtMobileNo" ErrorMessage="Enter MobileNo" CssClass="rfvColor"></asp:RequiredFieldValidator>

                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtotp" ClientIDMode="Static" onkeydown="return (event.keyCode!=13)" AutoComplete="off" CssClass="txtbox" Visible="false" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" ClientIDMode="Static" ID="lblOtp" Visible="false" runat="server">OTP <span class="reqiredstar">*</span></asp:Label>
                        </div>

                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtMail" ClientIDMode="Static" AutoPostBack="true" MaxLength="10" OnTextChanged="txtMail_TextChanged" MinLength="10" AutoComplete="off" CssClass="txtbox d-none" runat="server" />
                            <asp:Label ID="lblMail" ClientIDMode="Static" CssClass="txtlabel d-none" runat="server">Mail Id. <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <div class="LoginButton">
                            <asp:Button ID="btnSendOTP" ClientIDMode="Static" CssClass="btnSubmit d-none" runat="server" Text="Send OTP" OnClick="btnSendOTP_Click" />
                            <asp:Button ID="btnCancel" ClientIDMode="Static" CssClass="btnCancel d-none" runat="server" Text="Cancel" />
                        </div>
                        <div class="LoginResendButton">
                            <asp:Button ID="btnCfmOtp" ClientIDMode="Static" runat="server" CssClass="btnVerify" Visible="false" ValidationGroup="OTP" OnClick="btnCfmOtp_Click"
                                Text="Verify & Login" />
                            <asp:Button ID="btnResend" ClientIDMode="Static" runat="server" class="btnResnd" Visible="false" OnClick="btnResend_Click" />
                        </div>


                        <div class="LoginGmail">
                            <p class="LoginGmailHeader">or Login / SignUp </p>
                            <div class="divSingUpOption">
                                <div class="row socialmedia">
                                    <div id="google-button" class="imgText">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <asp:HiddenField ID="hfOtp" runat="server" />
            <asp:HiddenField ID="hfMobileNo" runat="server" />
        </div>
    </div>


    <script src="https://accounts.google.com/gsi/client"></script>
    <script defer src='<%=ResolveUrl("../PaymentPage/PaymentPage.js") %>'></script>
    <script defer src='<%=ResolveUrl("../Master/Master.js") %>'></script>
    <script defer src='<%=ResolveUrl("qrcode.min.js") %>'></script>
    <script defer src='<%=ResolveUrl("PopupPaymentPage.js") %>'></script>
    <!-- Sweet alert -->
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.js"></script>

    <!--jquery-3.3.1-->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"
        integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo"
        crossorigin="anonymous"></script>

    <div id="Divpaymentdetails" runat="server" clientidmode="static" class="DisplyCard1" visible="false">
        <div class="LoginCard">
            <div class="row planname">
                <asp:Label ID="lblplanname" runat="server" CssClass="form-check-label">Payment Methods  
                    <asp:ImageButton src="../../Images/Subscription/Close.png" CssClass="closebutton" ID="Closebutton" OnClick="Closebutton_Click" runat="server" />
                </asp:Label>
            </div>
            <div class="row Totalbg">
                <div class="row">
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                        <div class="tab-heading">
                            <ul class="nav nav-tabs">
                                <li class="p-heading paymentli">Payment options</li>
                                <li><a href="#a" id="divupi" class="active" data-toggle="tab">
                                    <div class="heading-content d-flex">
                                        <span class="heading-icon">
                                            <img src="../../Images/PaymentPage/Upi.png" width="25px" alt="upi"></span><div class="heading-name"><span class="heading-title">UPI</span><span class="heading-description">Google Pay,PhonePe &amp; More </span></div>
                                    </div>
                                </a></li>
                                <li><a href="#g" data-toggle="tab" class="paymentli" id="QRcode" onclick="verifyandpay_Click">
                                    <div class="heading-content d-flex">
                                        <span class="heading-icon">
                                            <svg stroke="currentColor" fill="currentColor" stroke-width="0" viewBox="0 0 1024 1024" height="1em" width="1em" xmlns="http://www.w3.org/2000/svg">
                                                <path d="M468 128H160c-17.7 0-32 14.3-32 32v308c0 4.4 3.6 8 8 8h332c4.4 0 8-3.6 8-8V136c0-4.4-3.6-8-8-8zm-56 284H192V192h220v220zm-138-74h56c4.4 0 8-3.6 8-8v-56c0-4.4-3.6-8-8-8h-56c-4.4 0-8 3.6-8 8v56c0 4.4 3.6 8 8 8zm194 210H136c-4.4 0-8 3.6-8 8v308c0 17.7 14.3 32 32 32h308c4.4 0 8-3.6 8-8V556c0-4.4-3.6-8-8-8zm-56 284H192V612h220v220zm-138-74h56c4.4 0 8-3.6 8-8v-56c0-4.4-3.6-8-8-8h-56c-4.4 0-8 3.6-8 8v56c0 4.4 3.6 8 8 8zm590-630H556c-4.4 0-8 3.6-8 8v332c0 4.4 3.6 8 8 8h332c4.4 0 8-3.6 8-8V160c0-17.7-14.3-32-32-32zm-32 284H612V192h220v220zm-138-74h56c4.4 0 8-3.6 8-8v-56c0-4.4-3.6-8-8-8h-56c-4.4 0-8 3.6-8 8v56c0 4.4 3.6 8 8 8zm194 210h-48c-4.4 0-8 3.6-8 8v134h-78V556c0-4.4-3.6-8-8-8H556c-4.4 0-8 3.6-8 8v332c0 4.4 3.6 8 8 8h48c4.4 0 8-3.6 8-8V644h78v102c0 4.4 3.6 8 8 8h190c4.4 0 8-3.6 8-8V556c0-4.4-3.6-8-8-8zM746 832h-48c-4.4 0-8 3.6-8 8v48c0 4.4 3.6 8 8 8h48c4.4 0 8-3.6 8-8v-48c0-4.4-3.6-8-8-8zm142 0h-48c-4.4 0-8 3.6-8 8v48c0 4.4 3.6 8 8 8h48c4.4 0 8-3.6 8-8v-48c0-4.4-3.6-8-8-8z"></path></svg>
                                        </span>
                                        <div class="heading-name"><span class="heading-title">UPI QR</span><span class="heading-description">Google Pay,PhonePe &amp; More </span></div>
                                    </div>
                                </a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                        <div class="tab-content w-100">
                            <div class="tab-pane" id="g">
                                <div class="upi-data row">
                                    <div class="upi-container row">
                                        <form id="Qrcode-form" class="">
                                            <div class="upi-info">
                                                <span class="phone-icon">
                                                    <svg stroke="currentColor" fill="currentColor" stroke-width="0" viewBox="0 0 1024 1024" height="1em" width="1em" xmlns="http://www.w3.org/2000/svg">
                                                        <path d="M744 62H280c-35.3 0-64 28.7-64 64v768c0 35.3 28.7 64 64 64h464c35.3 0 64-28.7 64-64V126c0-35.3-28.7-64-64-64zm-8 824H288V134h448v752zM472 784a40 40 0 1 0 80 0 40 40 0 1 0-80 0z"></path></svg></span><span class="phone-info">Keep your phone handy!</span>
                                            </div>
                                            <div class="upi-form">
                                                <div class="qrtimer" id="rqheader">
                                                    Payment Session will expire in <span class="timercount upi-note"><span id="qrtimer">05:00</span></span>
                                                </div>
                                                <div class="mt-2 row">
                                                    <div class="col-md-12">
                                                        <div id="qrcode">
                                                            <img src="../../Images/Subscription/QRcodelogo.png" alt="qrcode" class="QRlogo">
                                                            <%-- <img  id="qrcodes" alt="qrcode" class="QRlogo">--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="qrresponsemsg" id="qrerrorresponse">
                                                </div>
                                                <div class="upi-note">
                                                    <span class="upi-info-icon mr-1">
                                                        <svg stroke="currentColor" fill="currentColor" stroke-width="0" viewBox="0 0 512 512" height="1em" width="1em" xmlns="http://www.w3.org/2000/svg">
                                                            <path d="M256 8C119.043 8 8 119.083 8 256c0 136.997 111.043 248 248 248s248-111.003 248-248C504 119.083 392.957 8 256 8zm0 110c23.196 0 42 18.804 42 42s-18.804 42-42 42-42-18.804-42-42 18.804-42 42-42zm56 254c0 6.627-5.373 12-12 12h-88c-6.627 0-12-5.373-12-12v-24c0-6.627 5.373-12 12-12h12v-64h-12c-6.627 0-12-5.373-12-12v-24c0-6.627 5.373-12 12-12h64c6.627 0 12 5.373 12 12v100h12c6.627 0 12 5.373 12 12v24z"></path></svg></span>Scan this QR Code to Pay the Amount
                                                </div>
                                                <div class="upi-images mt-4">
                                                    <img src="../../Images/PaymentPage/Phonepay.png" class="phonepe">
                                                    <img src="../../Images/PaymentPage/Upi.png" class="phonepe">
                                                    <img src="../../Images/PaymentPage/Gpay.png" class="phonepe">
                                                    <img src="../../Images/PaymentPage/whatsapp.png" class="phonepe">
                                                    <img src="../../Images/PaymentPage/Paytm.png" class="phonepe">
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane active" id="a">
                                <div class="upi-data row">
                                    <div class="upi-container row">
                                        <form id="upi-form" class="">
                                            <div class="upi-info">
                                                <span class="phone-icon">
                                                    <svg stroke="currentColor" fill="currentColor" stroke-width="0" viewBox="0 0 1024 1024" height="1em" width="1em" xmlns="http://www.w3.org/2000/svg">
                                                        <path d="M744 62H280c-35.3 0-64 28.7-64 64v768c0 35.3 28.7 64 64 64h464c35.3 0 64-28.7 64-64V126c0-35.3-28.7-64-64-64zm-8 824H288V134h448v752zM472 784a40 40 0 1 0 80 0 40 40 0 1 0-80 0z"></path></svg></span><span class="phone-info">Keep your phone handy!</span>
                                            </div>
                                            <div class="upi-form">
                                                <div class="paytimer" id="payheader">
                                                    Payment Session will expire in <span class="timercount upi-note"><span id="paytimer">10:00</span></span>
                                                </div>
                                                <div class="mt-2 row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <asp:TextBox placeholder="Mobile Number / MailId" ID="fname" ClientIDMode="Static" runat="server" CssClass="textbox"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="d-flex justify-content-center mt-3">
                                                    <div class="row">
                                                        <asp:Button runat="server" ClientIDMode="Static" ID="verifyandpay" OnClick="verifyandpay_Click" class="verifypaybutton" Text="Verify & Pay" />
                                                    </div>
                                                </div>
                                                <div class="qrresponsemsg" id="payrrorresponse">
                                                </div>
                                                <div class="upi-note">
                                                    <span class="upi-info-icon mr-1">
                                                        <svg stroke="currentColor" fill="currentColor" stroke-width="0" viewBox="0 0 512 512" height="1em" width="1em" xmlns="http://www.w3.org/2000/svg">
                                                            <path d="M256 8C119.043 8 8 119.083 8 256c0 136.997 111.043 248 248 248s248-111.003 248-248C504 119.083 392.957 8 256 8zm0 110c23.196 0 42 18.804 42 42s-18.804 42-42 42-42-18.804-42-42 18.804-42 42-42zm56 254c0 6.627-5.373 12-12 12h-88c-6.627 0-12-5.373-12-12v-24c0-6.627 5.373-12 12-12h12v-64h-12c-6.627 0-12-5.373-12-12v-24c0-6.627 5.373-12 12-12h64c6.627 0 12 5.373 12 12v100h12c6.627 0 12 5.373 12 12v24z"></path></svg></span>Payment link will send to above Mobile Number
                                                </div>
                                                <div class="upi-images mt-4">
                                                    <img src="../../Images/PaymentPage/Phonepay.png" class="phonepe">
                                                    <img src="../../Images/PaymentPage/Upi.png" class="phonepe">
                                                    <img src="../../Images/PaymentPage/Gpay.png" class="phonepe">
                                                    <img src="../../Images/PaymentPage/whatsapp.png" class="phonepe">
                                                    <img src="../../Images/PaymentPage/Paytm.png" class="phonepe">
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>




            </div>
        </div>
    </div>


    <div id="Divsessionover" runat="server" clientidmode="static" class="DisplyCard1 d-none">
        <div class="modal fade show" role="dialog" tabindex="-1" id="SessionPopupBag" style="display: block">
            <div class="modal-dialog alert-model modal-dialog-centered" role="document">
                <div class="modal-content">
                    <p class="text-center payment-alert mt-5 mb-4">Payment Session is timed out !</p>
                    <div class="d-flex justify-content-center mt-3 mb-5">
                        <asp:Button runat="server" ClientIDMode="Static" ID="goback" OnClick="goback_Click" class=" go-back-btn btn btn-success" Text="Go Back" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="Divpaymentsuccess" runat="server" clientidmode="static" class="DisplyCard1 d-none">

        <div class="LoginCard">
            <div class="row planname ">
                <asp:Label ID="Label1" runat="server" CssClass="form-check-label">Payment Status  
                    <asp:ImageButton src="../../Images/Subscription/Close.png" CssClass="closebutton d-none" ID="closepaymentpopup" ClientIDMode="Static" runat="server" />
                </asp:Label>
                <div class="paymentsucessstatus">
                    <svg viewBox="0 0 26 26" xmlns="http://www.w3.org/2000/svg">
                        <g stroke="currentColor" stroke-width="2" fill="none" fill-rule="evenodd" stroke-linecap="round" stroke-linejoin="round">
                            <path class="circle" d="M13 1C6.372583 1 1 6.372583 1 13s5.372583 12 12 12 12-5.372583 12-12S19.627417 1 13 1z" />
                            <path class="tick" d="M6.5 13.5L10 17 l8.808621-8.308621" />
                        </g>
                    </svg>
                  
                </div>
                  <p>Well done! Your Subscription has been successfully purchased!</p>
            </div>

        </div>

    </div>

</asp:Content>

