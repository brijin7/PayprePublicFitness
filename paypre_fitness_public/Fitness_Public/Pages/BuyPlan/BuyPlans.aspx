<%@ Page Title="" Language="C#" MasterPageFile="~/Fitness.Master" AutoEventWireup="true" CodeFile="BuyPlans.aspx.cs" EnableEventValidation="false"
    Inherits="Pages_BuyPlan_BuyPlans" %>

<asp:Content ID="BuyPlan" ContentPlaceHolderID="FitnessContent" runat="Server">
    <link href="BuyPlan.css" rel="stylesheet" />
    <%-- this script is used to hide links in Navbar(ex: classes, Testimonials etc.) --%>
    <script defer src='<%=ResolveUrl("../Master/Master.js") %>'></script>
    <script defer src='<%=ResolveUrl("BuyPlan.js") %>'></script>
    <div class="PlanContainer">
        <div>
            <img id="uparrow" onclick="topFunction()" class="downArrow" style="display: none" src="../../Images/About/DownArrow.svg" />
        </div>
        <script>
            window.onscroll = function () { scrollFunction() };

            function scrollFunction() {
                if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
                    uparrow.style.display = "block";
                } else {
                    uparrow.style.display = "none";
                }
            }
            function topFunction() {
                document.body.scrollTop = 0;
                document.documentElement.scrollTop = 0;

            }

        </script>

        <div class="row planname">
            <asp:Label ID="lblplanname" runat="server" CssClass="form-check-label">
                <asp:ImageButton ID="btnBack" src="../../Images/Master/Arrow.svg" runat="server" OnClick="btnBack_Click" />
                &nbsp </asp:Label>
        </div>
        <div>
            <div class="row">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                    <img class="CategoryImgBg" src="../../Images/BuyPlan/Classesimgbg.png" />
                    <asp:Label CssClass="CategoryNameBg" runat="server" ID="lblCategoryName"></asp:Label>
                    <img class="CategoryImg" src="<%=getPathName()%>" />
                </div>
                <%-- </div>
            <div class="row">--%>
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 divCategoryListContainer">
                    <%-- Fake button just for call onClick of button event using jquery function--%>
                    <asp:Button ID="btnfake" runat="server" OnClick="OnClick" Style="display: none" />
                    <%-- Hidden field to set the ratio value--%>
                    <asp:HiddenField ID="hfColumnRepeat" runat="server" Value="3" />
                    <asp:DataList ID="dtlCategoryList" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12" runat="server">
                                    <div class="divCategoryList" runat="server" id="divCategoryList">
                                        <asp:Label ID="lblcategoryName" runat="server" Text='<%#Bind("categoryName") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblplanDurationName" CssClass="lbldtlPlanName" runat="server" Text='<%#Bind("planDurationName") %>'></asp:Label>
                                        <asp:Label ID="lblnetAmount" CssClass="lbldtlAmt" runat="server" Text='<%#"₹"+Eval( "netAmount") + "/-" %>'></asp:Label><br />
                                        <div class="divdtltrainingName">
                                            <asp:LinkButton ID="lbltrainingTypeName" ClientIDMode="Static" CssClass="lbldtlTraningName"
                                                runat="server" Text='<%#Eval("trainingTypeName") + " " + "▼" %>' OnClick="lbltrainingTypeName_Click">
                                           
                                            </asp:LinkButton><br />
                                            <div id="DivCategory" class="dtlCatCard" runat="server" visible="false">
                                                <asp:LinkButton ID="btnDtlClose" runat="server" CssClass="dtlClosebtn" OnClick="btnDtlClose_Click">X</asp:LinkButton>
                                                <asp:Label CssClass="lblSubDtlHead" runat="server">Choose Your Plan</asp:Label><br />
                                                <asp:Label CssClass="lblSubDtlHeadSub" runat="server">Trainers available for all plans</asp:Label><br />
                                                <asp:DataList ID="dtlCategory" runat="server">
                                                    <ItemTemplate>
                                                        <div class="row DivdtlCategory">
                                                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                                <asp:Label ID="lblCtrainingTypeName" CssClass="lblCName" runat="server"
                                                                    Text='<%#Bind("trainingTypeName") %>'></asp:Label>
                                                            </div>
                                                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                                <asp:LinkButton ID="lblCnetAmount" CssClass="lblCAmt"
                                                                    runat="server" Text='<%#"₹"+Eval( "netAmount") + "/-" %>'
                                                                    OnClick="lblCnetAmount_Click"></asp:LinkButton><br />
                                                            </div>
                                                        </div>
                                                        <asp:Label ID="lblplanDuration" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("planDuration") %>'></asp:Label>
                                                        <asp:Label ID="lblplanDurationName" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("planDurationName") %>'></asp:Label>
                                                        <asp:Label ID="lblcategoryId" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("categoryId") %>'></asp:Label>
                                                        <asp:Label ID="lblgymOwnerId" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("gymOwnerId") %>'></asp:Label>
                                                        <asp:Label ID="lblbranchId" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("branchId") %>'></asp:Label>
                                                        <asp:Label ID="lblbranchName" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("branchName") %>'></asp:Label>
                                                        <asp:Label ID="lbltrainingTypeId" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("trainingTypeId") %>'></asp:Label>
                                                        <asp:Label ID="lbltrainingMode" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("trainingMode") %>'></asp:Label>
                                                        <asp:Label ID="lblpriceId" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("priceId") %>'></asp:Label>
                                                        <asp:Label ID="lblprice" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("price") %>'></asp:Label>
                                                        <asp:Label ID="lbltaxId" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("taxId") %>'></asp:Label>
                                                        <asp:Label ID="lbltaxName" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("taxName") %>'></asp:Label>
                                                        <asp:Label ID="lbltaxAmount" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("tax") %>'></asp:Label>
                                                        <asp:Label ID="lblactualAmount" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("actualAmount") %>'></asp:Label>
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
                                        <asp:Label ID="lblcategoryId" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("categoryId") %>'></asp:Label>
                                        <asp:Label ID="lblgymOwnerId" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("gymOwnerId") %>'></asp:Label>
                                        <asp:Label ID="lblbranchId" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("branchId") %>'></asp:Label>
                                        <asp:Label ID="lblbranchName" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("branchName") %>'></asp:Label>
                                        <asp:Label ID="lbltrainingTypeId" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("trainingTypeId") %>'></asp:Label>
                                        <asp:Label ID="lbltrainingMode" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("trainingMode") %>'></asp:Label>
                                        <asp:Label ID="lblpriceId" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("priceId") %>'></asp:Label>
                                        <asp:Label ID="lblprice" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("price") %>'></asp:Label>
                                        <asp:Label ID="lbltaxId" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("taxId") %>'></asp:Label>
                                        <asp:Label ID="lbltaxName" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("taxName") %>'></asp:Label>
                                        <asp:Label ID="lbltaxAmount" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("tax") %>'></asp:Label>
                                        <asp:Label ID="lblactualAmount" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("actualAmount") %>'></asp:Label>
                                        <asp:Label ID="lblCtrainingTypeName" CssClass="lblCName" runat="server" Visible="false" Text='<%#Bind("trainingTypeName") %>'></asp:Label>
                                        <asp:Label ID="lblTotalAmt" CssClass="lblCAmt" runat="server" Visible="false" Text='<%#Bind("netAmount") %>'></asp:Label>

                                        <asp:Button CssClass="dtlbtnBuy" runat="server" ID="btnBuyList" Text="BuyNow" OnClick="btnBuyList_Click" />
                                        <asp:Label Visible="false" ID="lblplanDuration" CssClass="lbldtlPlanName" runat="server" Text='<%#Bind("planDuration") %>'></asp:Label>
                                    </div>
                                    <br />
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



        <footer class="planfooter">
            <div class="row">
                <div class="col-4">
                    <section class="flex-content padding_1x">
                        <h3>Equipments</h3>
                        <a href="#">Treadmills</a>
                        <a href="#">Ellipticals</a>
                        <a href="#">Exercise Cycles</a>
                        <a href="#">Racks</a>
                        <a href="#">Synergy 360 Systems</a>
                        <a href="#">Cable Crossovers </a>
                        <a href="#">Stair Climbers</a>
                        <a href="#">Rowing Machines</a>
                        <a href="#">Free Weights</a>
                        <a href="#">Kettlebells</a>
                        <a href="#">Lateral X Trainers</a>
                        <a href="#">Amt Crosstrainers</a>
                    </section>
                </div>
                <div class="col-4">
                    <section class="flex-content padding_1x">
                        <h3>Facilities</h3>
                        <a href="#">AC</a>
                        <a href="#">Wifi</a>
                        <a href="#">Lockers</a>
                        <a href="#">Changing Room</a>
                        <a href="#">Free Parking</a>
                        <a href="#">Personal Training</a>
                    </section>
                </div>
            </div>
            <div class="flex">
                <section class="flex-content padding_1x">
                    <a href="#" class="Lastfooter">
                        <image src="../../Images/BuyPlan/phone.svg"></image>
                    </a>
                    <a href="#" class="Lastfooter">+91 96777 85755</a> &nbsp;
                    <a href="#" class="Lastfooter">
                        <image src="../../Images/BuyPlan/Facebook.svg"></image>
                    </a>&nbsp;
                     <a href="#" class="Lastfooter">
                         <image src="../../Images/BuyPlan/Whatsapp.svg"></image>
                     </a>&nbsp;
                     <a href="#" class="Lastfooter">
                         <image src="../../Images/BuyPlan/Youtube.svg"></image>
                     </a>
                </section>
                <section class="flex-content padding_1x">
                    <a href="#" class="Lastfooter">User Agreement</a> &nbsp;
                    <a href="#" class="Lastfooter">Privacy Policy</a> &nbsp;
                    <a href="#" class="Lastfooter">FAQ</a> &nbsp;
                    <a class="Lastfooter">©2023 Rocks Fitness Guru,All Rights Reserved</a>
                </section>
            </div>
        </footer>


    </div>

</asp:Content>

