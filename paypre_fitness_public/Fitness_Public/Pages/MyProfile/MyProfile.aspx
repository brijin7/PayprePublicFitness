<%@ Page Title="My Profile" Language="C#" MasterPageFile="~/Fitness.Master" AutoEventWireup="true" CodeFile="MyProfile.aspx.cs" Inherits="Pages_MyProfile" %>
<%@ MasterType VirtualPath="../../Fitness.Master" %>
<asp:Content ID="CntMyProfile" ContentPlaceHolderID="FitnessContent" runat="Server">
    <script defer src='<%=ResolveUrl("../Master/Master.js") %>'></script>
    <link href="MyProfile.Css" rel="stylesheet" />
    <link href="DatePicker.css" rel="stylesheet" />
    <div class="container-fluid">
        <asp:Label ID="lblplanname" runat="server" CssClass="form-check-label lblBack">
            <asp:ImageButton ID="btnBack" src="../../Images/Master/Arrow.svg" runat="server" OnClick="btnBack_Click" />
            &nbsp </asp:Label>
        &nbsp
        <label class="lblHead">My Profile</label>
        <div class="DivMyProfile">
            <div class="row">
                <div class="col-12 col-sm-9 col-md-9 col-lg-9 col-xl-9">
                    <div class="row mb-3">
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtFirstName" AutoComplete="off"
                                    CssClass="txtbox" runat="server" placeholder=" " />
                                <asp:Label ID="lblMobile" CssClass="txtlabel" runat="server">First Name <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator
                                ID="rfvtxtPassWord" ValidationGroup="MyProfile" ControlToValidate="txtFirstName" runat="server" CssClass="rfvStyle"
                                Display="Dynamic" ErrorMessage="Enter First Name">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtLastName" AutoComplete="off"
                                    CssClass="txtbox" runat="server" placeholder=" " />
                                <asp:Label ID="lblLastName" CssClass="txtlabel" runat="server">Last Name </asp:Label>
                            </div>
                        </div>
                        
                    </div>
                    <div class="row mb-3">
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                            <asp:Label ID="lblgender" CssClass="lblGender" runat="server">Gender <span class="reqiredstar">*</span></asp:Label>
                            <asp:RadioButtonList ID="rbtnGender" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="M">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" ValidationGroup="MyProfile" ControlToValidate="rbtnGender" runat="server" CssClass="rfvStyle"
                                Display="Dynamic" ErrorMessage="Select  Gender">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                            <asp:Label ID="Label3" CssClass="lblGender" runat="server">Marital Status </asp:Label>
                            <asp:RadioButtonList ID="rbtnMaritalStatus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="S">Single</asp:ListItem>
                                <asp:ListItem Value="M">Married</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtDOB" AutoComplete="off"
                                    CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                                <asp:Label ID="Label4" CssClass="txtlabel" runat="server">DOB <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator2" ValidationGroup="MyProfile" ControlToValidate="txtDOB" runat="server" CssClass="rfvStyle"
                                Display="Dynamic" ErrorMessage="Enter  DOB">
                            </asp:RequiredFieldValidator>
                        </div>
                        
                        
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtMobileNo" AutoComplete="off" onkeypress="return isNumber(event);" MaxLength="10" Enabled="false"
                                    CssClass="txtbox" runat="server" placeholder=" " />
                                <asp:Label ID="Label5" CssClass="txtlabel" runat="server">Mobile No <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator3" ValidationGroup="MyProfile" ControlToValidate="txtMobileNo" runat="server" CssClass="rfvStyle"
                                Display="Dynamic" ErrorMessage="Enter  Mobile No">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator
                                ID="revMobileNo" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Invalid Mobile No."
                                ValidationExpression="[0-9]{10}" CssClass="rfvStyle" Display="Dynamic"
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
                                ID="revEmailId" runat="server" ControlToValidate="txtMailId" ErrorMessage="Invalid Email Id."
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="rfvStyle"
                                Display="Dynamic">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtAddress1" AutoComplete="off"
                                    CssClass="txtbox" runat="server" placeholder=" " />
                                <asp:Label ID="Label1" CssClass="txtlabel" runat="server">Address 1 </asp:Label>
                            </div>
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtAddress2" AutoComplete="off"
                                    CssClass="txtbox" runat="server" placeholder=" " />
                                <asp:Label ID="Label2" CssClass="txtlabel" runat="server">Address 2 </asp:Label>
                            </div>
                        </div>
                        <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5 mb-3">
                            <div class="row">
                                <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                    <div class="txtboxdiv">
                                        <asp:TextBox ID="txtPincode" AutoComplete="off" TabIndex="10" MaxLength="6" onkeypress="return isNumber(event);" CssClass="txtbox"
                                            onchange="myFunction()" runat="server" placeholder=" " />
                                        <asp:Label CssClass="txtlabel" runat="server">Pin Code <span class="reqiredstar">*</span></asp:Label>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RfvPincode" ValidationGroup="MyProfile" ControlToValidate="txtPincode" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Picode">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                    <span runat="server" class="spanstyle">City :</span><asp:Label ID="txtCity" CssClass="lblstyle" runat="server"></asp:Label>
                                    <br />
                                    <span runat="server" class="spanstyle">District :</span><asp:Label ID="txtDistrict" CssClass="lblstyle" runat="server"></asp:Label>
                                    <br />
                                    <span runat="server" class="spanstyle">State :</span><asp:Label ID="txtState" CssClass="lblstyle" runat="server"></asp:Label>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3">
                    <img id="imgpreview" clientidmode="Static" runat="server" class="imgpreview" src="~/Pages/MyProfile/User.png" />
                    <asp:FileUpload ID="fuimage" CssClass="mx-4" TabIndex="3" runat="server" onchange="showpreview(this);" />
                </div>
            </div>

            <div class="DivSubmit">
                <asp:Button CssClass="btnSubmit" ID="btnSubSubmit" runat="server" Text="Update" ValidationGroup="MyProfile" OnClick="btnSubSubmit_Click" />
                <asp:Button ID="btnSubCancel" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnSubCancel_Click" />
            </div>
        </div>
        <div>

        </div>
    </div>
    <asp:HiddenField ID="hfState" runat="server" EnableViewState="true" />
    <asp:HiddenField ID="hfDistrict" runat="server" EnableViewState="true" />
    <asp:HiddenField ID="hfCity" runat="server" EnableViewState="true" />

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

    <%--JQuery--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.3/jquery.js" integrity="sha512-nO7wgHUoWPYGCNriyGzcFwPSF+bPDOR+NvtOYy2wMcWkrnCNPKBcFEkU80XIN14UVja0Gdnff9EmydyLlOL7mQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <%--Flat Picker--%>
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <%--DataPicker--%>

    <script src="https://ajax.aspnetcdn.com/ajax/4.5.1/1/MicrosoftAjax.js" type="text/javascript"></script>
    <script>
        const datepicker = document.getElementsByClassName('datePicker');
        const fromDate = document.getElementsByClassName('fromDate');

        const toDate = document.getElementsByClassName('toDate');
        const ConvertfromDate = document.getElementsByClassName('ConvertfromDate');

        const ConverttoDate = document.getElementsByClassName('ConverttoDate');
        const dateTimepicker = document.getElementsByClassName('dateTimepicker');
        const timePicker = document.getElementsByClassName('timePicker');
        const daterangepicker = document.getElementsByClassName('daterangepicker');

        let date = new Date();
        var Todate;
        let fp = flatpickr(daterangepicker,
            {
                mode: "range",
                minDate: "today",
                dateFormat: "d-m-Y",

            });

        fp = flatpickr(datepicker,
            {
                enableTime: false,
                dateFormat: "d-m-Y",
                altFormat: "d-m-Y",
                altInput: true,
                time_24hr: false,
                minDate: "today",
                onOpen: function () {
                    const numInput = document.querySelectorAll('.numInput');
                    numInput.forEach((input) => input.type = '');
                }

            });
        fp = flatpickr(toDate,
            {
                enableTime: false,
                dateFormat: "Y-m-d",
                altFormat: "d-m-Y",
                altInput: true,
                time_24hr: false
            });

        fp = flatpickr(fromDate,
            {
                enableTime: false,
                dateFormat: "Y-m-d",
                altFormat: "d-m-Y",
                altInput: true,
                time_24hr: false,
                onOpen: function () {
                    const numInput = document.querySelectorAll('.numInput');
                    numInput.forEach((input) => input.type = '');
                },
                onChange: function (selectedDates, dateStr, instance) {
                    toDate[0].value = '';
                    flatpickr(toDate,
                        {
                            enableTime: false,
                            dateFormat: "Y-m-d",
                            altFormat: "d-m-Y",
                            altInput: true,
                            time_24hr: false,
                            minDate: dateStr
                        });
                },
            });

        fp = flatpickr(ConverttoDate,
            {
                enableTime: false,
                dateFormat: "d-m-Y",
                altFormat: "d-m-Y",
                altInput: true,
                time_24hr: false
            });

        fp = flatpickr(ConvertfromDate,
            {
                enableTime: false,
                dateFormat: "d-m-Y",
                altFormat: "d-m-Y",
                altInput: true,
                time_24hr: false,
                onOpen: function () {
                    const numInput = document.querySelectorAll('.numInput');
                    numInput.forEach((input) => input.type = '');
                },
                onChange: function (selectedDates, dateStr, instance) {
                    toDate[0].value = '';
                    flatpickr(ConverttoDate,
                        {
                            enableTime: false,
                            dateFormat: "d-m-Y",
                            altFormat: "d-m-Y",
                            altInput: true,
                            time_24hr: false,
                            minDate: dateStr
                        });
                },
            });

        fp = flatpickr(timePicker,
            {
                enableTime: true,
                noCalendar: true,
                time_24hr: true,
                dateFormat: "h:i K",
                minTime: "today",

            });

        fp = flatpickr(dateTimepicker,
            {
                enableTime: true,
                dateFormat: "d/m/Y h:i:K",
                time_24hr: false,
                minDate: "today",
                onOpen: function () {
                    const numInput = document.querySelectorAll('.numInput');
                    numInput.forEach((input) => input.type = '');
                },
                maxDate: new Date().fp_incr(1) // 14 days from now
            });

    </script>


    <script type="text/javascript">
        function showpreview(input) {
            var fup = document.getElementById("<%=fuimage.ClientID %>");
            var fileName = fup.value;
            var maxfilesize = 1024 * 1024;
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
                    swal("Please, Upload image file less than or equal to 1 MB !!!");
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
</asp:Content>

