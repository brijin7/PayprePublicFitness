<%@ Page Title="" Language="C#" MasterPageFile="~/Fitness.Master" AutoEventWireup="true" CodeFile="PlanDetails.aspx.cs" Inherits="Pages_PlanDetails_PlanDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FitnessContent" runat="Server">
    <link href="PlanDetails.css" rel="stylesheet" />
    <%-- this script is used to hide links in Navbar(ex: classes, Testimonials etc.) --%>
    <script defer src='<%=ResolveUrl("../Master/Master.js") %>'></script>
    <script defer src='<%=ResolveUrl("PlanDetails.js") %>'></script>
    <div class="PlanContainer">
        <div class="row planname">
            <asp:Label ID="lblplanname" runat="server" CssClass="form-check-label">
                <asp:ImageButton ID="btnBack" src="../../Images/Master/Arrow.svg" runat="server" OnClick="btnBack_Click" /></asp:Label>
        </div>
        <div class="row DivCat">
            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 DivCatDetails">
                <div class="row">
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                        <div class="divSub">
                            <div>
                                <asp:Label ID="lblPlanMonth" CssClass="lblMonth" runat="server"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lblPlanMonthSub" CssClass="lblMonthSub" runat="server"></asp:Label>
                                <label class="lblPack">Transformation Pack</label>
                            </div>
                        </div>
                        <div>
                            <asp:Label ID="PlanName" runat="server" CssClass="lblPlanName"></asp:Label>
                        </div>
                        <div class="mt-5">
                            <p class="dtlCatBenefitHead">What else you get</p>
                            <asp:DataList ID="dtlCategoryBenefits" runat="server">
                                <ItemTemplate>
                                    <div class="row">
                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 divCategoryBenefits">
                                            <asp:Label ID="lblCategoryBenefits" CssClass="dtlCatBenefit" runat="server"
                                                Text='<%#Bind("description") %>'></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>

                    </div>
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                        <div>
                            <asp:Label runat="server" class="lblJoingDateText">Joining Date</asp:Label>
                            <asp:Label ID="lblJoingDate" runat="server" class="lblJoingDate"></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="lblTotalAmt" class="lblTotalAmt" runat="server"></asp:Label>
                        </div>
                        <div class="mt-5 mb-5">
                            <p class="dtlCatBenefitHead">Highlight</p>
                            <asp:DataList ID="dtlBenefitImg" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
                                <ItemTemplate>
                                    <div class="row">
                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <div>
                                                <asp:Image ID="dtlBenefitImg" CssClass="dtlBenefitImg" runat="server" ImageUrl='<%#Bind("imageUrl") %>'></asp:Image>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <SeparatorTemplate>
                                    <table style="height: 5px; width: 5px; padding-left: 10px; padding-bottom: 40px;">
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </SeparatorTemplate>
                            </asp:DataList>
                        </div>
                    </div>

                </div>

            </div>
            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-5">
                <div class="row divSummary">
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                        <asp:Label runat="server" class="lblSummaryHead">Payment Summary</asp:Label>
                    </div>
                    <hr style="margin-top: 2rem; opacity: 1" />
                    <div class="row DivSummaryFinalAmt">
                        <div class="row">
                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <div class="mb-3">
                                    <asp:Label ID="lblActualAmount" runat="server" class="lblSummaryAmt">Actual Amount</asp:Label>
                                </div>
                            </div>
                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <div class="mb-3">
                                    <asp:Label ID="lblSummaryTotal" runat="server" class="lblSummaryAmt"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <div class="mb-3">
                                    <asp:Label ID="lblPriceAmount" runat="server" class="lblSummaryAmt">Price Amount</asp:Label>
                                </div>
                            </div>
                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <div class="mb-3">
                                    <asp:Label ID="lblPriceAmt" runat="server" class="lblSummaryAmt"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <div class="mb-3">
                                    <asp:Label ID="lblTotalPay" runat="server" class="lblFinalTotalAmt">Total Payable</asp:Label>
                                </div>
                            </div>
                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <div class="mb-3">
                                    <asp:Label ID="lblFinalTotalAmt" runat="server" class="lblFinalTotalAmt">Total Payable</asp:Label>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="DivbtnBuy">
                        <asp:Button ID="btnBuyNow" runat="server" Text="Pay Now" CssClass="btnBuyNow" OnClick="btnBuyNow_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

