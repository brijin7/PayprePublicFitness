<%@ Page Title="" Language="C#" MasterPageFile="~/Fitness.Master" AutoEventWireup="true" CodeFile="DietWorkOut.aspx.cs" Inherits="Pages_DietWorkOut_DietWorkOut" %>

<asp:Content ID="CtDietWorkOut" ContentPlaceHolderID="FitnessContent" runat="Server">
    <script defer src='<%=ResolveUrl("../Master/Master.js") %>'></script>
    <link href="DietWorkOut.css" rel="stylesheet" />
    <script defer src='<%=ResolveUrl("DietWorkout.js") %>'></script>

    <div class="container">

        <div class="row">
            <div class="col-12 col-sm-6 col-md-6 col-lg-6">
                <div style="position: relative; display: flex;">
                    <img src="../../Images/DietWotkOut/Group (1).svg" alt="Arrow" class="Vector" />
                    <asp:Label ID="DietWorkname" runat="server" CssClass="DietWorkname">
              
                    </asp:Label>
                    <span class="Weeks" id="Weeks" runat="server"></span>
                    <%-- <img src="../../Images/DietWotkOut/pngwing5.svg" class="Wing" />--%>
                </div>
            </div>
            <div class="col-12 col-sm-6 col-md-6 col-lg-6 dietworkout">
                <asp:Button ID="btnDiet" CssClass="divcontainer" runat="server" Text="Diet" OnClick="btnDiet_Click" />
                <asp:Button ID="btnWorkOut" CssClass="divcontainer" runat="server" Text="Workout" OnClick="btnWorkOut_Click" />

            </div>
        </div>
        <%--    Diet--%>
        <div class="col-12 showContent" id="divDiet" runat="server">
            <div class="row divTarget">
                <%--    Target--%>
                <div class="col-12 col-sm-12 col-md-7 col-lg-7">
                    <div class="row">
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3">
                            <p class="target">
                                Target
                            </p>
                            <p class="targetDiet">
                                Diet
                            </p>
                        </div>
                        <asp:Button ID="btnfake" runat="server" OnClick="OnClick" Style="display: none" />
                        <asp:HiddenField ID="hfColumnRepeat" ClientIDMode="Static" runat="server" Value="3" />
                    </div>
                    <div class="col-12 divresponsive">

                        <asp:DataList ID="dtlDietTarget" runat="server" RepeatDirection="Vertical" Width="100%" OnItemDataBound="dtlDietTarget_ItemDataBound">
                            <ItemTemplate>
                                <div class="row">


                                    <div class="col-8 col-sm-8 col-md-8 col-lg-8 DietTargetContainer">

                                        <asp:Label ID="lblmealTypeName" CssClass="DietName" runat="server" Text='<%#Bind("mealTypeName") %>'></asp:Label>

                                        <asp:Label ID="lblDietCalo" CssClass="DietCalo" runat="server" Text='<%# "("+ Eval("calories").ToString()+"KCals)" %>'></asp:Label>

                                    </div>

                                    <div class="col-4 col-sm-4 col-md-4 col-lg-4">
                                        <p class="DietTime">
                                            <img src="../../Images/DietWotkOut/three-o-clock-clock.png" class="Clock" />
                                            <asp:Label ID="lblfromTime" runat="server" Text='<%#Bind("fromTime") %>'></asp:Label>
                                        </p>
                                    </div>
                                </div>

                                <asp:DataList ID="dtlDietTargetItems"
                                    class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 dt"
                                    runat="server" RepeatDirection="Horizontal" RepeatColumns="3" CellSpacing="5" Width="100%">
                                    <ItemTemplate>

                                        <div class="row">
                                            <div id="divtargetItems" runat="server" class="col-12 col-sm-12 col-md-3 col-lg-3 col-xl-3  divtargetItems">
                                                <asp:LinkButton ID="LnkDiet" runat="server" CssClass="lnkWorkout" OnClick="LnkDiet_Click">


                                                    <asp:Image ID="ImgtargetItems" runat="server" CssClass="targetItemsImg" ImageUrl='<%#Bind("imageUrl") %>' />

                                                    <div class=" targetItemsName">

                                                        <asp:Label ID="lblfoodItemName" CssClass="Targetitems" runat="server" Text='<%#Eval("foodItemName")+"("+Eval("servingIn")+")" %>'></asp:Label>
                                                        <div class="row TargetitemsSub">
                                                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">

                                                                <asp:Label ID="Label2" runat="server">&#8226; Protein:</asp:Label>
                                                                <asp:Label ID="lblProtein" runat="server" Text='<%#Bind("protein") %>'></asp:Label>
                                                            </div>
                                                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                                <asp:Label ID="Label1" runat="server">&#8226; Fats:</asp:Label>
                                                                <asp:Label ID="lblfat" runat="server" Text='<%#Bind("fat") %>'></asp:Label>
                                                            </div>

                                                        </div>
                                                        <div class="row TargetitemsSub">
                                                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">

                                                                <asp:Label ID="Label3" runat="server">&#8226; Carbs:</asp:Label>
                                                                <asp:Label ID="lblcarbs" runat="server" Text='<%#Bind("carbs") %>'></asp:Label>
                                                            </div>
                                                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">

                                                                <asp:Label ID="Label4" runat="server">&#8226; Calories:</asp:Label>
                                                                <asp:Label ID="lblcalories" runat="server" Text='<%#Bind("calories") %>'></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <asp:Label ID="lbluserId" runat="server" Text='<%#Bind("userId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblbookingId" runat="server" Text='<%#Bind("bookingId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblmealType" runat="server" Text='<%#Bind("mealType") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblmealTypeName" runat="server" Text='<%#Bind("mealTypeName") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblfromTime" runat="server" Text='<%#Bind("fromTime") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbldietTypeId" runat="server" Text='<%#Bind("dietTypeId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblfoodItemId" runat="server" Text='<%#Bind("foodItemId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblservingIn" runat="server" Text='<%#Bind("servingIn") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblfoodDietTimeId" runat="server" Text='<%#Bind("foodDietTimeId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbltoTime" runat="server" Text='<%#Bind("toTime") %>' Visible="false"></asp:Label>

                                    </ItemTemplate>
                                </asp:DataList>

                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>

                <%--    Consumed--%>
                <div class="col-12 col-sm-12 col-md-5 col-lg-5 divConsumed">
                    <p class="target">
                        Consumed
                    </p>
                    <p class="targetDiet">
                        Diet
                    </p>
                    <div class="col-12 divresponsive">
                        <asp:DataList ID="dtlConsumed" runat="server" RepeatDirection="Vertical" Width="100%" OnItemDataBound="dtlConsumed_ItemDataBound">
                            <ItemTemplate>
                                <div class="consumedDietName">
                                    <asp:Label ID="lblmealTypeName" runat="server" Text='<%#Bind("mealTypeName") %>'></asp:Label>
                                    <asp:Label ID="lblDietCalo" CssClass="consumedItemsEner" runat="server" Text='<%# "("+ Eval("calories").ToString()+"KCals)" %>'></asp:Label>
                                </div>

                                <asp:DataList ID="dtlConsumedItems" runat="server" RepeatDirection="Vertical" RepeatColumns="1" Width="100%">
                                    <ItemTemplate>
                                        <div class="consumedItems">
                                            <img src="../../Images/DietWotkOut/pngwing15.svg" class="ConsumedselectedImageDiet" />

                                            <div class="row">
                                                <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                                                    <asp:Image ID="Image1" runat="server" CssClass="itemsImg" ImageUrl='<%#Bind("imageUrl") %>' />
                                                </div>
                                                <div class="col-6 col-sm-6 col-md-6 col-lg-6 divItems">
                                                    <asp:Label ID="Label5" CssClass="itemsName" runat="server" Text='<%#Eval("foodItemName")+"("+Eval("servingIn")+")" %>'></asp:Label>
                                                    <div class="row itemsSub">
                                                        <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">

                                                            <asp:Label ID="Label6" runat="server">&#8226; Protein:</asp:Label>
                                                            <asp:Label ID="Label7" runat="server" Text='<%#Bind("protein") %>'></asp:Label>
                                                        </div>
                                                        <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                            <asp:Label ID="Label8" runat="server">&#8226; Fats:</asp:Label>
                                                            <asp:Label ID="Label9" runat="server" Text='<%#Bind("fat") %>'></asp:Label>
                                                        </div>

                                                    </div>
                                                    <div class="row itemsSub">
                                                        <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">

                                                            <asp:Label ID="Label10" runat="server">&#8226; Carbs:</asp:Label>
                                                            <asp:Label ID="Label11" runat="server" Text='<%#Bind("carbs") %>'></asp:Label>
                                                        </div>
                                                        <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">

                                                            <asp:Label ID="Label12" runat="server">&#8226; Calories:</asp:Label>
                                                            <asp:Label ID="Label13" runat="server" Text='<%#Bind("calories") %>'></asp:Label>
                                                        </div>
                                                    </div>



                                                </div>
                                            </div>

                                        </div>

                                        <asp:Label ID="lblmealTypeName" runat="server" Text='<%#Bind("mealTypeName") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblfromTime" runat="server" Text='<%#Bind("fromTIme") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblfoodItemId" runat="server" Text='<%#Bind("foodMenuId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblservingIn" runat="server" Text='<%#Bind("servingIn") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblfoodDietTimeId" runat="server" Text='<%#Bind("uniqueId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbltoTime" runat="server" Text='<%#Bind("toTime") %>' Visible="false"></asp:Label>

                                    </ItemTemplate>
                                </asp:DataList>

                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="col-12 containers">
                        <asp:Button ID="btnFinished" runat="server" CssClass="finishButton" Text="Finished" OnClick="btnFinished_Click" />

                    </div>
                </div>

            </div>
        </div>

        <%--    WorkOut--%>
        <link href="WorkOut.css" rel="stylesheet" />
        <script defer src='<%=ResolveUrl("DietWorkout.js") %>'></script>
        <asp:Button ID="btnWork" runat="server" OnClick="OnClick" Style="display: none" />
        <asp:HiddenField ID="hfWork" ClientIDMode="Static" runat="server" Value="3" />
        <div class="col-12" id="divWorkOut" runat="server">
            <asp:DataList ID="dtlWorkOut" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" Width="100%">
                <ItemTemplate>
                    <div class="row">
                        <div class="col-12 col-sm-2 col-md-2 col-lg-12 col-xl-12  pe-5 divWorkOutContainer" id="divWorkOutContainer" runat="server">
                            <asp:LinkButton ID="lnkWorkOut" runat="server" CssClass="lnkWorkout" OnClick="lnkWorkOut_Click">
                                <asp:Image ID="ImgWorKoutType" runat="server" ImageUrl='<%#Bind("imageUrl") %>' CssClass="WorkOutItemsImg" />
                                <br />
                                <asp:Label ID="lblWorkOutType" runat="server" CssClass="WorkOutType" Text='<%#Bind("workoutCatTypeName") %>'>

                                </asp:Label>
                            </asp:LinkButton>
                            <asp:Label ID="lblworkoutCatTypeId" runat="server" Text='<%#Bind("workoutCatTypeId") %>' Visible="false">

                            </asp:Label>
                            <asp:Label ID="lblbookingId" runat="server" Text='<%#Bind("bookingId") %>' Visible="false">    </asp:Label>
                            <asp:Label ID="lblgymOwnerId" runat="server" Text='<%#Bind("gymOwnerId") %>' Visible="false">    </asp:Label>
                            <asp:Label ID="lblbranchId" runat="server" Text='<%#Bind("branchId") %>' Visible="false">    </asp:Label>
                            <asp:Label ID="lbluserId" runat="server" Text='<%#Bind("userId") %>' Visible="false">    </asp:Label>
                        </div>
                    </div>


                </ItemTemplate>

            </asp:DataList>

        </div>

    </div>
    <asp:HiddenField ID="hfFromdate" runat="server" />
    <asp:HiddenField ID="hfTodate" runat="server" />


</asp:Content>

