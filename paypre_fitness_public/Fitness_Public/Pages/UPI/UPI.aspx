<%@ Page Title="" Language="C#" MasterPageFile="~/Fitness.Master" AutoEventWireup="true" CodeFile="UPI.aspx.cs" Inherits="Pages_UPI_UPI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FitnessContent" runat="Server">
    <link href="upi.css" rel="stylesheet" />
    <script defer src='<%=ResolveUrl("upi.js") %>'></script>

    <div class="col-12 mt-5">
        <div class="containerUPIBg">
            <div id="divForm" runat="server" visible="true">
                <div class="wrapper">
                    <div class="payment">
                        <div class="payment-logo">
                            <p>Pre</p>
                        </div>
                        <h2>Payment Gateway</h2>
                        <div class="form">
                            <div class="card space icon-relative">
                                <label class="label">Branch Name:</label>
                                <asp:TextBox  runat="server" class="input" id="branchname" placeholder="XXXXX Parking" disabled="disabled"> </asp:TextBox>
                                <i class="fa-solid fa-square-parking far"></i>
                            </div>
                            <div class="card space icon-relative">
                                <label class="label">Booking Id:</label>
                                  <asp:TextBox  runat="server" class="input" id="bookingid" placeholder="XXXXX Parking" disabled="disabled"> </asp:TextBox>
                                <i class="fa-solid fa-ticket far"></i>
                            </div>
                            <div class="card space icon-relative" id="Vehicle">
                                <label class="label">Package Name.:</label>
                                  <asp:TextBox  runat="server" class="input" id="packagename" placeholder="XXXXX Parking" disabled="disabled"> </asp:TextBox>
                                <i class="fa-solid fa-car far"></i>
                            </div>
                            <div class="card space icon-relative" id="mobile">
                                <label class="label">Number of Days:</label>
                                  <asp:TextBox  runat="server" class="input" id="noofdays" placeholder="XXXXX Parking" disabled="disabled"> </asp:TextBox>
                                <i class="fa-solid fa-mobile far"></i>
                            </div>
                            <div class="card space icon-relative">
                                <label class="label">User Name:</label>
                                  <asp:TextBox  runat="server" class="input" id="transactionid" placeholder="XXXXX Parking" disabled="disabled"> </asp:TextBox>
                                <i class="fa-solid fa-user far"></i>
                            </div>
                            <div class="card space icon-relative">
                                <label class="label">Amount:</label>
                                  <asp:TextBox  runat="server" class="input" id="amount" placeholder="XXXXX Parking" disabled="disabled"> </asp:TextBox>
                                <i class="fa-solid fa-indian-rupee-sign far"></i>
                            </div>
                            <div class="btn">
                                <%--<btn id="payment"  OnClick="payment_Click" onclick="payment()">Pay</btn>--%>
                                <asp:Button ID="payment" ClientIDMode="Static" CssClass="btn" Text="Pay" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hfBaseurl" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfToken" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfMerchantName" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfTranUpiId" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfMerchantId" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfMerchantCode" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfmode" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hforgid" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfsign" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfBookingId" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfAmount" ClientIDMode="Static" runat="server" />
</asp:Content>

