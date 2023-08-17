<%@ Page Title="Meal" Language="C#" MasterPageFile="~/Fitness.Master" AutoEventWireup="true" CodeFile="Meal.aspx.cs" Inherits="Pages_Meals_Meal" %>

<asp:Content ID="cndMeals" ContentPlaceHolderID="FitnessContent" runat="Server">
    <%@ MasterType VirtualPath="~/Fitness.Master" %>
    <script src="https://kit.fontawesome.com/6aac34c85b.js" crossorigin="anonymous"></script>
    <link href="Meal.css" rel="stylesheet" />
    <div class="container-fluid containerBg">
        <div class="mealContainer">
            <div class="divMealPlan">
                <asp:Label runat="server" CssClass="lblMealHead">Meal Plan</asp:Label>
                <div class="DaysdivMain">
                    <asp:DataList ID="DtlDays" RepeatDirection="Horizontal" runat="server" OnItemDataBound="DtlDays_ItemDataBound">
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <div class="divDays" id="divDays" runat="server">
                                        <asp:Label ID="lblDay" runat="server" class="lblDays lblDaysActive" Text='<%#Bind("days") %>'></asp:Label>
                                        <div>
                                            <asp:Label ID="lblDates" runat="server" class="lblDays lblDaysActive" Text='<%#Bind("Dates") %>'></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
        <div id="divMyMeals" class="divMymeals" runat="server">
            <div class="row">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                    <div class="divMealTypes">
                        <label class="lblMyMeals">My Meals</label>
                        <hr />
                        <asp:DataList ID="dtlMyMeals" RepeatDirection="Horizontal" RepeatColumns="3" runat="server">
                            <ItemTemplate>
                                <div class="row">
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                        <div class="DivMealHead">
                                            <asp:ImageButton ID="imgMealType" OnClick="imgMealType_Click" CssClass="Foodimg"
                                                runat="server" ImageUrl='<%#Bind("imageUrl") %>' /><br />
                                            <div class="lblMealHeadtype">
                                                <asp:Label ID="lblmealTypeName" runat="server" class="lblMealSubHead"
                                                    Text='<%#Bind("mealTypeName") %>'></asp:Label><br />
                                                <asp:Label ID="lbldietTypeName" runat="server" class="lblMealDtl"
                                                    Text='<%#Bind("dietTypeName") %>'></asp:Label>
                                                <asp:Label ID="lblmealType" runat="server" class="lblMealDtl" Visible="false"
                                                    Text='<%#Bind("mealType") %>'></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Label ID="lbldietTypeId" runat="server" Visible="false" Text='<%#Bind("dietTypeId") %>'></asp:Label>
                                <asp:Label ID="lblbookingId" runat="server" Visible="false" Text='<%#Bind("bookingId") %>'></asp:Label>
                                <asp:Label ID="lbluserId" runat="server" Visible="false" Text='<%#Bind("userId") %>'></asp:Label>
                                <asp:Label ID="lblfromTime" runat="server" Visible="false" Text='<%#Bind("fromTime") %>'></asp:Label>
                                <asp:Label ID="lbltoTime" runat="server" Visible="false" Text='<%#Bind("toTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>

                </div>
            </div>
        </div>
        <div id="divMealType" runat="server" visible="false">
            <div class="divMealTypeNav">
                <asp:DataList ID="dtlMealTypeList" RepeatDirection="Horizontal" runat="server">
                    <ItemTemplate>
                        <div class="divMealTypeList">
                            <asp:LinkButton ID="lnkMealType" runat="server" OnClick="lnkMealType_Click" Text='<%#Bind("mealTypeName") %>'
                                class="lblMealTypeHead"></asp:LinkButton>
                            <asp:Label ID="lblmealType" runat="server" class="lblMealDtl" Visible="false"
                                Text='<%#Bind("mealType") %>'></asp:Label>
                            <asp:Label ID="lbldietTypeId" runat="server" Visible="false" Text='<%#Bind("dietTypeId") %>'></asp:Label>
                            <asp:Label ID="lblbookingId" runat="server" Visible="false" Text='<%#Bind("bookingId") %>'></asp:Label>
                            <asp:Label ID="lbluserId" runat="server" Visible="false" Text='<%#Bind("userId") %>'></asp:Label>
                            <asp:Label ID="lblfromTime" runat="server" Visible="false" Text='<%#Bind("fromTime") %>'></asp:Label>
                            <asp:Label ID="lbltoTime" runat="server" Visible="false" Text='<%#Bind("toTime") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div class="row">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3">
                    <div id="DivBreakFast" class="Mealtype" runat="server">
                        <div>
                            <asp:Label ID="lblMealTypeHead" runat="server"
                                CssClass="lblMealDays"></asp:Label>
                            <progress id="PrgDay" runat="server" value="50" max="100"></progress>
                            <asp:Label ID="lblMealTime" runat="server" CssClass="lblMealTime"></asp:Label>
                            <div class="row pe-0 px-0">
                                <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                                    <asp:Label ID="lblTarget" CssClass="lblTraget" runat="server">
                                    </asp:Label>
                                </div>
                                <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                                    <asp:Label CssClass="lblConsumed" runat="server" ID="lblConsumed"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <asp:DataList ID="dtlFooditem" RepeatDirection="Horizontal" RepeatColumns="3" runat="server" OnItemCommand="dtlFooditem_ItemCommand">
                            <ItemTemplate>
                                <div class="row">
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 DivBreakFast">
                                        <div class="divMeadEdit">
                                            <asp:LinkButton ID="imgMealSwap" OnClick="imgMealSwap_Click" Visible='<%#Eval("consumingStatus").ToString() == "N" ? true : false%>' runat="server" class="imgswap">
                        <i class="fa-solid fa-shuffle fa-fade"></i>&nbsp Swap
                                            </asp:LinkButton>
                                        </div>
                                        <asp:Image ID="imgFoodConsumed" Visible='<%#Eval("consumingStatus").ToString() == "Y" ? true : false%>' runat="server" CssClass="imgConsumed" ImageUrl="~/Pages/Meals/Image/Done.png" />
                                        <asp:ImageButton ID="imgFoodClick" runat="server" CommandName="ConsumeFood" CssClass="imgMeal" ImageUrl='<%#Bind("imageUrl") %>' OnClientClick='<%#Eval("consumingStatus").ToString() == "Y" ? "return false;" : "return confirm(\"Are you sure you want to consume this food item?\");" %>' />
                                        <br />
                                        <asp:Label ID="lblfoodItemName" runat="server" class="lblMealName"
                                            Text='<%#Bind("foodItemName") %>'></asp:Label>
                                        <div class="divCal">
                                            <label class="lblCal">
                                                Fat:<asp:Label ID="lblfat" runat="server"
                                                    Text='<%#Eval("fat") + "g" %>'></asp:Label></label>&nbsp&nbsp
                                    <label class="lblCal">
                                        Calories:<asp:Label ID="lblcalories" runat="server"
                                            Text='<%#Eval("calories") + "K" %>'></asp:Label></label>&nbsp&nbsp
                                    <label class="lblCal">
                                        Protein:<asp:Label ID="lblprotein" runat="server"
                                            Text='<%#Eval("protein") + "g" %>'></asp:Label></label>&nbsp&nbsp
                                    <label class="lblCal">
                                        Carbs:<asp:Label ID="lblcarbs" runat="server"
                                            Text='<%#Eval("carbs") + "g" %>'></asp:Label></label>
                                        </div>
                                    </div>
                                </div>
                                <asp:Label ID="lbluserId" runat="server" Visible="false" Text='<%#Bind("userId") %>'></asp:Label>
                                <asp:Label ID="lblbookingId" runat="server" Visible="false" Text='<%#Bind("bookingId") %>'></asp:Label>
                                <asp:Label ID="lblfoodItemId" runat="server" Visible="false" Text='<%#Bind("foodItemId") %>'></asp:Label>
                                <asp:Label ID="lbldietTimeId" runat="server" Visible="false" Text='<%#Bind("foodDietTimeId") %>'></asp:Label>
                                <asp:Label ID="lblmealType" runat="server" Visible="false" Text='<%#Bind("mealType") %>'></asp:Label>
                                <asp:Label ID="lbluniqueId" runat="server" Visible="false" Text='<%#Bind("uniqueId") %>'></asp:Label>
                                <asp:Label ID="lblfromTime" runat="server" Visible="false" Text='<%#Bind("fromTime") %>'></asp:Label>
                                <asp:Label ID="lbltoTime" runat="server" Visible="false" Text='<%#Bind("toTime") %>'></asp:Label>
                                <asp:Label ID="lblUserfoodDietTimeId" runat="server" Visible="false" Text='<%#Bind("UserfoodDietTimeId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMealEdit" class="MealEditPopup" runat="server" visible="false">
        <div class="MealCard">
            <div class="MealtypeEdit">
                <div class="row">
                    <div class="divEditHead">
                        <label class="lblEditHead">Select Your Favorite Food</label>
                        <asp:LinkButton ID="btnSwapClose" OnClick="btnSwapClose_Click" CssClass="btnSwapClose" runat="server">X</asp:LinkButton>
                    </div>
                    <asp:DataList ID="dtlFooditemEdit" RepeatDirection="Horizontal" RepeatColumns="3" runat="server">
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 DivMealEdit">
                                    <asp:ImageButton ID="imgSwapFood" runat="server" OnClick="imgSwapFood_Click" CssClass="imgMeal"
                                        ImageUrl='<%#Bind("imageUrl") %>' /><br />
                                    <asp:Label ID="lblfoodItemName" runat="server" class="lblMealName"
                                        Text='<%#Bind("foodItemName") %>'></asp:Label>
                                    <div class="divCal">
                                        <label class="lblCal">
                                            Calories:<asp:Label ID="lblcalories" runat="server"
                                                Text='<%#Eval("calories") + "K" %>'></asp:Label></label>&nbsp&nbsp
                                    <label class="lblCal">
                                        ServingIn:<asp:Label ID="lblprotein" runat="server"
                                            Text='<%#Eval("servingIn") + "g" %>'></asp:Label></label>&nbsp&nbsp
                                    </div>
                                </div>
                            </div>
                            <asp:Label ID="lblfoodItemId" runat="server" Visible="false" Text='<%#Bind("foodItemId") %>'></asp:Label>
                            <asp:Label ID="lbldietTimeId" runat="server" Visible="false" Text='<%#Bind("dietTimeId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

