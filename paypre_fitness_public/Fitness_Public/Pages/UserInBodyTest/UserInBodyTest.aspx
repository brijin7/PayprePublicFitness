<%@ Page Title="In Body Test" Language="C#" MasterPageFile="~/Fitness.Master" AutoEventWireup="true" CodeFile="UserInBodyTest.aspx.cs" Inherits="Pages_UserInBodyTest" %>
<%@ MasterType VirtualPath="../../Fitness.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FitnessContent" runat="Server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.10.0/css/all.min.css" rel="stylesheet">
    <link href="UserInBodyTest.css" rel="stylesheet" />
 <link href="../MyProfile/DatePicker.css" rel="stylesheet" />
    <script defer src='<%=ResolveUrl("../Master/Master.js") %>'></script>
    <script defer src='<%=ResolveUrl("../DietWorkOut/DietWorkout.js") %>'></script>
    <div class="container">
        <div id="head" style="display: flex; margin-top: 2rem;">

            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 divTitle">
                <asp:Label ID="lblplanname" runat="server" CssClass="form-check-label">
                    <asp:ImageButton ID="btnBack" src="../../Images/Master/Arrow.svg" runat="server" OnClick="btnBack_Click" />
                    &nbsp </asp:Label>
                &nbsp
            <label class="lblHead">In Body Test</label>
            </div>
            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 divAdd">
                <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click" CssClass="AddPlus">
                  <img src="../../Images/UserInBodyTest/images.png" class="btnAdd" /></asp:LinkButton>
            </div>
        </div>
        <div id="divGrid" class="showContent" runat="server">
            <asp:Button ID="btnfake" runat="server" OnClick="OnClick" Style="display: none" />
            <asp:HiddenField ID="hfColumnRepeat" ClientIDMode="Static" runat="server" Value="3" />
            <asp:DataList ID="dtlUserBodyTest" runat="server" Width="100%" RepeatColumns="3">
                <ItemTemplate>

                    <div id="divtargetItems" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 divtargetItems divresponsive">
                        <div class="row">
                            <div class="col-3 col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                <asp:Image ID="Img" runat="server" ImageUrl='<%#Bind("Image") %>' CssClass="ImageStyle" />
                            </div>
                            <div class="col-9 col-sm-9 col-md-9 col-lg-9 col-xl-9" style="text-align: center">
                                <div class="row">
                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                        <asp:Label ID="Label1" runat="server" CssClass="divlbl" Text="Height :"></asp:Label>
                                        <asp:Label ID="lblheight" CssClass="divlabel" runat="server" Text='<%#Bind("height") %>'></asp:Label>

                                    </div>
                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                        <asp:Label ID="Label3" runat="server" CssClass="divlbl" Text="Weight :"></asp:Label>
                                        <asp:Label ID="lblweight" CssClass="divlabel" runat="server" Text='<%#Bind("weight") %>'></asp:Label>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                        <asp:Label ID="Label2" runat="server" CssClass="divlbl" Text="BMI :"></asp:Label>
                                        <asp:Label ID="lblBMI" CssClass="divlabel" runat="server" Text='<%#Bind("BMI") %>'></asp:Label>

                                    </div>
                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                        <asp:Label ID="Label5" runat="server" CssClass="divlbl" Text="BMR :"></asp:Label>
                                        <asp:Label ID="lblBMR" CssClass="divlabel" runat="server" Text='<%#Bind("BMR") %>'></asp:Label>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                        <asp:Label ID="Label7" runat="server" CssClass="divlbl" Text="TDEE :"></asp:Label>
                                        <asp:Label ID="lblTDEE" CssClass="divlabel" runat="server" Text='<%#Bind("TDEE") %>'></asp:Label>

                                    </div>

                                </div>
                                <div class="row divWeightRange">
                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 divlabel divdate">
                                        <asp:Label ID="Label4" CssClass="divlabel" runat="server" Text='<%#Bind("Weightrange") %>'></asp:Label>
                                    </div>
                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6  divdate" style="text-align: end">
                                        <asp:Image ImageUrl="../../Images/UserInBodyTest/DateTime.svg" CssClass="date" runat="server"></asp:Image>
                                        <asp:Label ID="lbldate" runat="server" Text='<%#Bind("date") %>'></asp:Label>
                                    </div>
                                </div>

                            </div>
                        </div>


                    </div>
                    <asp:Label ID="lbluserId" runat="server" Text='<%#Bind("userId") %>' Visible="false"></asp:Label>
                    <asp:Label ID="lblUserName" runat="server" Text='<%#Bind("UserName") %>' Visible="false"></asp:Label>
                    <asp:Label ID="lbldob" runat="server" Text='<%#Bind("dob") %>' Visible="false"></asp:Label>
                    <asp:Label ID="lblgender" runat="server" Text='<%#Bind("gender") %>' Visible="false"></asp:Label>
                    <asp:Label ID="lblfatPercentage" runat="server" Text='<%#Bind("fatPercentage") %>' Visible="false"></asp:Label>
                    <asp:Label ID="lblWorkOutStatus" runat="server" Text='<%#Bind("WorkOutStatus") %>' Visible="false"> </asp:Label>
                    <asp:Label ID="lblWorkOutValue" runat="server" Text='<%#Bind("WorkOutValue") %>' Visible="false"></asp:Label>
                    <asp:Label ID="lblage" runat="server" Text='<%#Bind("age") %>' Visible="false"></asp:Label>




                </ItemTemplate>
            </asp:DataList>
        </div>
        <div class="DivMyProfile" id="divUserForm" runat="server" visible="false">
            <div class="row">

                <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtname" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="1" />
                        <asp:Label CssClass="txtlabel" runat="server">Name <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvName" ValidationGroup="UserEnroll" ControlToValidate="txtname" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Name">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtDOB" AutoComplete="off" CssClass="txtbox ConvertfromDate" runat="server" TabIndex="2" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Date Of Birth <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvDOB" ValidationGroup="UserEnroll" ControlToValidate="txtDOB" runat="server" CssClass="rfvStyle"
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0"
                        ValidationGroup="UserEnroll" ControlToValidate="ddlGender" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Gender">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtage" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="4" MaxLength="3"
                            onkeypress="return isNumber(event);"
                            OnTextChanged="txtage_TextChanged" AutoPostBack="true" />
                        <asp:Label CssClass="txtlabel" runat="server">Age <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="Rfvage" ValidationGroup="UserEnroll" ControlToValidate="txtage" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Age">
                    </asp:RequiredFieldValidator>
                </div>

            </div>


            <div class="row">

                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtweight" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="5"
                            onkeypress="return AllowOnlyAmountAndDot(this.id);" MaxLength="4"
                            OnTextChanged="txtweight_TextChanged" AutoPostBack="true" />
                        <asp:Label CssClass="txtlabel" runat="server">Weight in Kg <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="Rfvweight" ValidationGroup="UserEnroll" ControlToValidate="txtweight" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Weight">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtheight" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="6"
                            onkeypress="return AllowOnlyAmountAndDot(this.id);" MaxLength="4"
                            OnTextChanged="txtheight_TextChanged" AutoPostBack="true" />
                        <asp:Label CssClass="txtlabel" runat="server">Height in cms <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="Rfvheight" ValidationGroup="UserEnroll" ControlToValidate="txtheight" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Height">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtfat" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="7"
                            onkeyup="this.value = minmax(this.value, 0, 100);" onkeypress="return isNumber(event);"
                            MaxLength="3" />
                        <asp:Label CssClass="txtlabel" runat="server">Fat % <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvFat" ValidationGroup="UserEnroll" ControlToValidate="txtfat" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Fat">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5 mb-3">
                    <asp:DropDownList ID="ddlWorkOutDetails" CssClass="form-select" runat="server" TabIndex="8"
                        OnSelectedIndexChanged="ddlWorkOutDetails_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Value="0">WorkOut Details *</asp:ListItem>
                        <asp:ListItem Value="1.2">I Don't Workout</asp:ListItem>
                        <asp:ListItem Value="1.375">1-5 times/week </asp:ListItem>
                        <asp:ListItem Value="1.55">3-7 times/week  </asp:ListItem>
                        <asp:ListItem Value="1.9">7+ times/week </asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" ValidationGroup="UserEnroll"
                        ControlToValidate="ddlWorkOutDetails" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select WorkOutDetails">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtBMI" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="9" Enabled="false" />
                        <asp:Label CssClass="txtlabel" runat="server">BMI <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvBMI" ValidationGroup="UserEnroll" ControlToValidate="txtBMI" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter BMI">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtBMR" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="10" Enabled="false" />
                        <asp:Label CssClass="txtlabel" runat="server">BMR <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvBMR" ValidationGroup="UserEnroll" ControlToValidate="txtBMR" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter BMR">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtTDEE" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="11" Enabled="false" />
                        <asp:Label CssClass="txtlabel" runat="server">TDEE <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvTDEE" ValidationGroup="UserEnroll" ControlToValidate="txtTDEE" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter TDEE">
                    </asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="text-end">
                <asp:Button CssClass="btnSubmit" ID="btnSubmit" TabIndex="12" runat="server" Text="Submit" ValidationGroup="UserEnroll" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" TabIndex="13" CausesValidation="false" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
    </div>

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
</asp:Content>

