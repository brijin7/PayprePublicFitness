<%@ Page Title="" Language="C#" MasterPageFile="~/Fitness.Master" AutoEventWireup="true" CodeFile="PaymentPage.aspx.cs" Inherits="Pages_PaymentPage_PaymentPage" %>

<%@ MasterType VirtualPath="../../Fitness.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FitnessContent" runat="Server">
    <link href="Paymentpage.css" rel="stylesheet" />

    <div class="PlanContainer">
        <div class="row planname">
            <asp:Label ID="lblplanname" runat="server" CssClass="form-check-label">
                <asp:ImageButton src="../../Images/Master/Arrow.svg" ID="backbutton" OnClick="backbutton_Click" runat="server" />&nbsp Payment Methods 
            </asp:Label>
        </div>
        <div class="row Totalbg">
            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                <div class="row partupi">
                    <div class="col-12">
                        <div class="row upitext">
                            <%--<input type="radio" class="option-input radio" name="example" />--%>
                            <img class="walletimage" src="../../Images/PaymentPage/Upi.png" />
                            UPI ID's
                            <span class="upisub">Google Pay,PhonePe & more</span>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <p class="upi1">
                                    <img src="../../Images/PaymentPage/Phone.png" />
                                    Keep your phone Handly!
                                </p>
                                <div class="form-group">
                                    <asp:TextBox placeholder="Mobile Number / MailId" ID="fname" runat="server" CssClass="textbox"></asp:TextBox>
                                    <%--<input type="text" placeholder="Mobile Number" id="fname" runat="server" class="textbox" name="fname" disabled="disabled">--%>
                                </div>
                                <div class="row">
                                    <asp:Button runat="server" ID="verifyandpay" OnClick="verifyandpay_Click" class="verifypaybutton" Text="Verify & Pay" />
                                    <%--<span class="" runat="server"  onclick="verifyandpay_Click" id="verifyandpay"></span>--%>
                                </div>
                                <div class="row">
                                    <div class="upi-images mt-4">
                                        <img src="../../Images/PaymentPage/Phonepay.png" class="phonepe">
                                        <img src="../../Images/PaymentPage/Upi.png" class="phonepe">
                                        <img src="../../Images/PaymentPage/Gpay.png" class="phonepe">
                                        <img src="../../Images/PaymentPage/whatsapp.png" class="phonepe">
                                        <img src="../../Images/PaymentPage/Paytm.png" class="phonepe">
                                    </div>
                                </div>
                            </div>

                            <div class="col-6">
                                <div class="row">
                                    <div class="col-10">
                                        <img class="qrcode" style="display: none" src="../../Images/PaymentPage/qrcode.png" />
                                        <p class="qrcodetext" style="display: none">Scan & Pay</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- <div class="row partwallet">
                    <div class="col-12">
                        <div class="row wallettext1">
                            <input type="radio" class="option-input radio" name="example" />
                            <img class="walletimage" src="../../Images/PaymentPage/png.png" />
                            PayPre Wallet
                                <div class="walletamountbg">
                                    <div class="row">
                                        <span class="walletamount">₹20000.00 <span class="paybutton">Pay Now →</span></span>

                                    </div>
                                    <div class="row">
                                        <span class="walletamountsub">Current Balance amount in PayPre Wallet</span>
                                    </div>
                                </div>
                        </div>
                    </div>
                </div>
                <div class="row partnetbank">
                    <div class="col-12">
                        <div class="row netbanktext">
                            <input type="radio" class="option-input radio" name="example" />
                            <img class="walletimage" src="../../Images/PaymentPage/Bank.png" />
                            NetBanking
                             <span class="netbanksub">State Bank,Indian Bank etc</span>
                        </div>
                    </div>
                </div>--%>
            </div>
            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                <div class="col-12 Details2">
                    <div class="detailssummary">
                        <div class="ud-brand row">
                            <div class="col-12">
                                <div class="brandAndLogo">
                                    <div class="brand-logo"></div>
                                    <div class="brand-DesContainer row">
                                        <div class="col-12 description_1">rocks fitness</div>
                                        <div class="col-12 description_2">fitness | faith | fulfilment</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <p class="summaryleft">Online Payment</p>
                            </div>
                            <hr style="margin: 2rem; border-top: 1px dashed #f8f9fa; width: 92%; opacity: 1" />
                            <div class="row">
                                <p class="summaryleftsub">
                                    Actual Amount <span class="summaryrightsub2" id="ActualAmount" runat="server"></span>
                                </p>
                                <p id="Amountmain" runat="server" class="summaryleftsub">Amount &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<span id="Amount" runat="server" class="summaryrightsub2"></span></p>
                            </div>
                            <hr style="margin: 2rem; opacity: 1; width: 92%;" />
                            <div class="row">
                                <p class="summaryleftbottom">Total Payable <span id="TotalAmount" runat="server" class="summaryrightbottom"></span></p>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>


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

        img {
            width: 30px;
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
            width: auto;
            font-size: 12px;
            font-weight: bold;
            padding: 7px 8px;
            letter-spacing: 1px;
            text-transform: uppercase;
            transition: transform 80ms ease-in;
            border-radius: 0.5rem;
        }

        .rfvColor {
            color: red;
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
            //document.getElementById('txtMobileNo').value = hfMobileNo.Value;
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
                    buttonConfrm.style.display = 'none';
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
    <link href="popupLogin.css" rel="stylesheet" />
    <%--Content Page Css--%>
    <link href="popupContentPage.css" rel="stylesheet" />
    <div id="DivLogin" runat="server" class="DisplyCard1" visible="false">
        <div class="LoginCard">
            <div class="row Logincontent">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 LoginHeight">

                    <a>
                        <img src="../../Images/Login/FitnessLogo.png" class="LoginLogoImage" /></a>
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


<%--    <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0/dist/js/bootstrap.bundle.min.js"></script>--%>
   

    <script src="https://accounts.google.com/gsi/client"></script>
    <script defer src='<%=ResolveUrl("paymentpage.js") %>'></script>
    <script defer src='<%=ResolveUrl("../Master/Master.js") %>'></script>

</asp:Content>

