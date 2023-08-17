<%@ Page Title="Diet&WorkOut" Language="C#" MasterPageFile="~/Fitness.Master" AutoEventWireup="true" CodeFile="DietWorkOutNew.aspx.cs" Inherits="Pages_DietWorkOut_DietWorkOut" %>

<%@ MasterType VirtualPath="../../Fitness.Master" %>
<asp:Content ID="CtDietWorkOut" ContentPlaceHolderID="FitnessContent" runat="Server">
    <link href="PopupPaymentPageDiet.css" rel="stylesheet" />
    <link href="PaymentpageDiet.css" rel="stylesheet" />
    <script defer src='<%=ResolveUrl("../Master/Master.js") %>'></script>
    <link href="DietCss.css" rel="stylesheet" />

    <style>
        html {
            background-color: white;
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
    </style>
    <div class="NavDetails">
        <div>
            <asp:DataList ID="dtlClass" runat="server" RepeatDirection="Horizontal">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkCategory" runat="server" OnClick="lnkCategory_Click" CssClass="lnkNav">
                        <asp:Label CssClass="lblNavList" ID="lblCategory" runat="server" Text='<%# Bind("categoryName") %>'></asp:Label>
                        <asp:Label ID="lblCategoryID" Text='<%# Bind("categoryId") %>' runat="server" Visible="false"></asp:Label>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:DataList>

        </div>
    </div>
    <div style="background-color: white;">
        <div class="container">
            <div class="row">
                <div id="divMainPlan" runat="server" class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">
                    <div class="divHeadDtl">
                        <div class="row">
                            <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5">
                                <div>
                                    <asp:Label ID="DietWorkname" runat="server" CssClass="DietWorkname">          
                                    </asp:Label>
                                    <div>
                                        <span class="Weeks mt-3" id="Weeks" runat="server">3 Weeks  | 21 Sessions</span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 dietworkout">
                                <asp:Button ID="btnDiet" CssClass="btnCategory" runat="server" Text="Diet" OnClick="btnDiet_Click" />
                                <asp:Button ID="btnWorkOut" CssClass="btnCategory" runat="server" Text="Workout" OnClick="btnWorkOut_Click" />

                            </div>
                        </div>
                    </div>
                    <div id="divDiet" runat="server">
                        <%--Progress Bar--%>
                        <div class="divDtlDiet">
                            <div id="divAll" runat="server">
                                <asp:DataList ID="dtlDays" runat="server" RepeatDirection="Horizontal" OnItemDataBound="dtlDays_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="row divDays">
                                            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                                <div class="row">
                                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                                        <asp:Label ID="lblDay" runat="server" Text='<%# Bind("Day") %>'
                                                            CssClass="lblDays"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'
                                                            CssClass="lblDays"></asp:Label>

                                                        <div>
                                                            <progress id="PrgDay" runat="server" max="100"></progress>
                                                            <div class="row pe-0 px-0">
                                                                <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                                    <asp:Label ID="lblTarget" CssClass="lblTraget" runat="server"
                                                                        Text='<%# "Target ("+ Eval("Target")+"KCals)" %>'></asp:Label>
                                                                </div>
                                                                <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                                    <asp:Label CssClass="lblConsumed" runat="server" ID="lblConsumed"
                                                                        Text='<%# "Consumed ("+ Eval("Consumed")+"KCals)" %>'></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <asp:DataList ID="dtlDietPlan" runat="server" RepeatDirection="Vertical"
                                                            RepeatColumns="1" OnItemDataBound="dtlDietPlan_ItemDataBound">
                                                            <ItemTemplate>
                                                                <div class="row">
                                                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                                                        <div class="row divMealTypeContainer">
                                                                            <div class="col-12 col-sm-11 col-md-11 col-lg-11 col-xl-11 divMealType">
                                                                                <asp:Label ID="lblMealTYpeName" CssClass="lblMealtype" runat="server" Text='<%# Bind("mealtypeName") %>'></asp:Label>
                                                                                <progress id="Progress1" class="progressBar50" runat="server" max="100" value="50"></progress>
                                                                                <div class="row">
                                                                                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                                                        <asp:Label ID="lblCal" CssClass="lblKcl" runat="server" Text='<%# "(" +Eval("calories")+")KCals" %>'></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                                                        <div runat="server" class="lblTime">
                                                                                            <img class="imgTime" src="../../Images/DietWotkOut/Time.png" />
                                                                                            <asp:Label ID="lblTime" Text='<%# Bind("fromTime") %>' runat="server"></asp:Label>
                                                                                        </div>

                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <asp:DataList ID="dtlFoodItem" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                                                                    <ItemTemplate>
                                                                        <div class="row">
                                                                            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                                                                <div class="row divFoodContainer">
                                                                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 divFooditem">
                                                                                        <div class="row">
                                                                                            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 Fooditem Foodlock">
                                                                                                <asp:Image ID="ImgtargetItems" runat="server" CssClass="imgFood" ImageUrl='<%#Bind("imageUrl") %>' />
                                                                                                <asp:Image ID="ImgDone" runat="server" Visible="false"
                                                                                                    CssClass="imgDone" ImageUrl="../../Images/DietWotkOut/Done.png" />
                                                                                                <asp:ImageButton ID="ImgLock" runat="server" CssClass="imgLock" Visible="true"
                                                                                                    ImageUrl="../../Images/DietWotkOut/Lock.png" OnClick="ImgLock_Click" Enabled="true" />
                                                                                                <div class="FooditemDetails">
                                                                                                    <asp:Label ID="lblFoodItemName" CssClass="lblFoodName" runat="server"
                                                                                                        Text='<%# Bind("foodItemName") %>'></asp:Label>
                                                                                                    <div class="divFoodDls">
                                                                                                        <asp:Label ID="lblFats" CssClass="lblFoodDtls" runat="server"
                                                                                                            Text='<%# "Fats:"+Eval("fat")+"g" %>'></asp:Label>
                                                                                                        <asp:Label ID="lblCalories" CssClass="lblFoodDtls" runat="server"
                                                                                                            Text='<%# "Calories:"+Eval("calories") %>'></asp:Label>
                                                                                                        <asp:Label ID="lblProtein" CssClass="lblFoodDtls" runat="server"
                                                                                                            Text='<%# "Protein:"+Eval("protein")+"g" %>'></asp:Label>
                                                                                                        <asp:Label ID="lblCarbs" CssClass="lblFoodDtls" runat="server"
                                                                                                            Text='<%# "Carbs:"+Eval("carbs")+"g" %>'></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>

                                                                                        </div>
                                                                                    </div>

                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                            <div id="divUser" runat="server">
                                <asp:DataList ID="dtlDaysUser" runat="server" RepeatDirection="Horizontal" OnItemDataBound="dtlDaysUser_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="row divDays">
                                            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                                <div class="row">
                                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                                        <asp:Label ID="lblDay" runat="server" Text='<%# Bind("Day") %>'
                                                            CssClass="lblDays"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'
                                                            CssClass="lblDays"></asp:Label>
                                                        <div>
                                                            <progress id="PrgDay" runat="server" max="100"></progress>
                                                            <div class="row pe-0 px-0">
                                                                <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                                    <asp:Label ID="lblTarget" CssClass="lblTraget" runat="server"
                                                                        Text='<%# "Target ("+ Eval("Target")+"KCals)" %>'></asp:Label>
                                                                </div>
                                                                <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                                    <asp:Label CssClass="lblConsumed" runat="server" ID="lblConsumed"
                                                                        Text='<%# "Consumed ("+ Eval("Consumed")+"KCals)" %>'></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <asp:DataList ID="dtlDietPlanUser" runat="server" RepeatDirection="Vertical"
                                                            RepeatColumns="1" OnItemDataBound="dtlDietPlanUser_ItemDataBound">
                                                            <ItemTemplate>
                                                                <div class="row">
                                                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                                                        <div class="row divMealTypeContainer">
                                                                            <div class="col-12 col-sm-11 col-md-11 col-lg-11 col-xl-11 divMealType">
                                                                                <asp:Label ID="lblMealTYpeName" CssClass="lblMealtype" runat="server" Text='<%# Bind("mealTypeName") %>'></asp:Label>
                                                                                <progress id="Progress1" class="progressBar" runat="server"
                                                                                    max="100"></progress>

                                                                                <div class="row">
                                                                                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                                                        <asp:Label ID="lblCal" CssClass="lblKcl" runat="server" Text='<%# "(" +Eval("calories")+")KCals" %>'></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                                                        <div runat="server" class="lblTime">
                                                                                            <img class="imgTime" src="../../Images/DietWotkOut/Time.png" />
                                                                                            <asp:Label ID="lblTime" Text='<%# Bind("fromTime") %>' runat="server"></asp:Label>
                                                                                        </div>

                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <asp:DataList ID="dtlFoodItemUser" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" Enabled="false">
                                                                    <ItemTemplate>
                                                                        <div class="row">
                                                                            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                                                                <div class="row divFoodContainer">
                                                                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 divFooditem">
                                                                                        <div class="row">
                                                                                            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 Fooditem Foodlock" id="divtargetItems" runat="server">
                                                                                                <asp:LinkButton ID="LnkDiet" runat="server" CssClass="lnkWorkout" OnClick="LnkDiet_Click">
                                                                                                    <asp:Image ID="ImgtargetItems" runat="server" CssClass="imgFood" ImageUrl='<%#Bind("imageUrl") %>' />
                                                                                                    <asp:Image ID="ImgLock" runat="server" CssClass="imgLock" Visible="true"
                                                                                                        ImageUrl="../../Images/DietWotkOut/Lock.png" />
                                                                                                    <asp:Image ID="ImgDone" runat="server" Visible="false"
                                                                                                        CssClass="imgDone" ImageUrl="../../Images/DietWotkOut/Done.png" />

                                                                                                    <div id="FooditemDetails" runat="server" class="FooditemDetails">
                                                                                                        <asp:Label ID="lblFoodItemName" CssClass="lblFoodName" runat="server"
                                                                                                            Text='<%# Bind("foodItemName") %>'></asp:Label>
                                                                                                        <div class="divFoodDls">
                                                                                                            <asp:Label ID="lblFats" CssClass="lblFoodDtls" runat="server"
                                                                                                                Text='<%# "Fats:"+Eval("fat")+"g" %>'></asp:Label>
                                                                                                            <asp:Label ID="lblCalories" CssClass="lblFoodDtls" runat="server"
                                                                                                                Text='<%# "Calories:"+Eval("calories") %>'></asp:Label>
                                                                                                            <asp:Label ID="lblProtein" CssClass="lblFoodDtls" runat="server"
                                                                                                                Text='<%# "Protein:"+Eval("protein")+"g" %>'></asp:Label>
                                                                                                            <asp:Label ID="lblCarbs" CssClass="lblFoodDtls" runat="server"
                                                                                                                Text='<%# "Carbs:"+Eval("carbs")+"g" %>'></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </asp:LinkButton>
                                                                                            </div>

                                                                                        </div>
                                                                                    </div>

                                                                                </div>

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
                                                                        <asp:Label ID="lblConsumingStatus" runat="server" Text='<%#Bind("consumingStatus") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                    </div>
                    <div id="divWorkOut" runat="server" visible="false">
                        <div id="divAllWork" runat="server">
                            <asp:DataList ID="dtlWorkOut" runat="server" RepeatDirection="Vertical" RepeatColumns="1" OnItemDataBound="dtlWorkOut_ItemDataBound">
                                <ItemTemplate>
                                    <div class="row divDaysWorkOut">
                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-5">
                                            <div class="row divresponsiveDays">
                                                <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                    <asp:Label ID="lblWorkOut" runat="server" CssClass="lblDays" Text='<%# Bind("workoutCatTypeName") %>'></asp:Label>
                                                    <asp:ImageButton ID="ToggleView" runat="server" CssClass="imgShowHide default open"
                                                        ImageUrl="~/Images/DietWotkOut/hideshowarrow.png" OnClick="ToggleView_Click" />

                                                    <%--<img  class="imgShowHide default open" src="../../Images/DietWotkOut/hideshowarrow.png" />--%>
                                                    <progress id="Progress5" class="progressBar" runat="server" max="100" value="70"></progress>
                                                </div>
                                                <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5">
                                                    <asp:Label ID="lblVideoCount" runat="server" CssClass="lblVideosCount"
                                                        Text='<%# "0/" +Eval("VideoCount")+"Videos" %>'></asp:Label>
                                                    <asp:Label ID="lblVideoCounts" runat="server" Visible="false"
                                                        Text='<%# Bind("VideoCount") %>'></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:DataList ID="dtlWorkOutList" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
                                        <ItemTemplate>
                                            <div runat="server" id="divWorkOutView" class="row divWorkOut">
                                                <div class="col-12 col-sm-11 col-md-11 col-lg-11 col-xl-11 mb-5">

                                                    <div class="WrkVideo">

                                                        <asp:ImageButton ID="ImageWorkLock" runat="server" CssClass="LockImgWorkout" Visible="true"
                                                            ImageUrl="../../Images/DietWotkOut/Lock.png" OnClick="ImageWorkLock_Click" />
                                                        <iframe id="ConsumedVideo" class="WorkOurVideo VideoLock" src='<%# Eval("video")+"?autoplay=0&modestbranding=1&mode=opaque&amp;rel=0&amp;autohide=0&amp;showinfo=0&amp;wmode=transparent"%>'
                                                            frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

                                                    </div>
                                                    <%-- <video autoplay loop muted class="WorkOurVideo Foodlock">
                                                    <source runat="server" src="~/Images/DietWotkOut/abs.mp4" type="video/mp4">
                                                </video>--%>
                                                    <asp:Label runat="server" CssClass="lblWorkOutHead" ID="lblworkoutType"
                                                        Text='<%# Bind("workoutType") %>'></asp:Label>
                                                    <asp:Label runat="server" Visible="false" ID="lblworkoutTypeId"
                                                        Text='<%# Bind("workoutTypeId") %>'></asp:Label>
                                                    <br />
                                                    <asp:Label runat="server" CssClass="lblWorkOutDtls"
                                                        Text='<%# "Done by :" +Eval("UserUsed")+"People" %>'></asp:Label><br />
                                                    <%--<asp:Label CssClass="lblTime" runat="server">
                                             <img class="WorkOutimgTime" src="../../Images/DietWotkOut/Time.png" />30 Min</asp:Label>--%>
                                                </div>

                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ItemTemplate>
                            </asp:DataList>

                        </div>
                        <div id="divUserWorkOut" runat="server">
                            <asp:DataList ID="dtlWorkOutUser" runat="server" RepeatDirection="Vertical" RepeatColumns="1" OnItemDataBound="dtlWorkOutUser_ItemDataBound">
                                <ItemTemplate>
                                    <div class="row divDaysWorkOut">
                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                            <div class="row divresponsiveDays">
                                                <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                    <asp:Label ID="lblWorkOut" runat="server" CssClass="lblDays" Text='<%# Bind("workoutCatTypeName") %>'></asp:Label>
                                                    <asp:Label ID="lblworkoutCatTypeId" runat="server" Visible="false" Text='<%# Bind("workoutCatTypeId") %>'></asp:Label>
                                                    <asp:ImageButton ID="ToggleViewUser" runat="server" CssClass="imgShowHide default open"
                                                        ImageUrl="~/Images/DietWotkOut/hideshowarrow.png" OnClick="ToggleViewUser_Click" />
                                                    <progress id="Progress5" class="progressBar" runat="server" max="100" value="70"></progress>
                                                </div>
                                                <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5">
                                                    <asp:Label ID="lblVideoCount" runat="server" CssClass="lblVideosCount"
                                                        Text='<%# "0/" +Eval("VideoCount")+"Videos" %>'></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:DataList ID="dtlWorkOutListUser" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" OnItemDataBound="dtlWorkOutListUser_ItemDataBound">
                                        <ItemTemplate>
                                            <div runat="server" id="divWorkOutView" class="row divWorkOut">
                                                <div class="col-12 col-sm-11 col-md-11 col-lg-11 col-xl-11">
                                                    <asp:Image ID="ImageSelected" runat="server" ImageUrl="../../Images/DietWotkOut/Done.png" CssClass="imgWorkOutDone"
                                                        Visible='<%#Eval("OverAllCompletedStatus").ToString() == "Yes"? true:false %>' />

                                                    <iframe id="ConsumedVideo" class="WorkOurVideo" src='<%# Eval("video")+"?autoplay=0&modestbranding=1&mode=opaque&amp;rel=0&amp;autohide=0&amp;showinfo=0&amp;wmode=transparent"%>'
                                                        frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

                                                    </script>
                                                <asp:Label runat="server" CssClass="lblWorkOutHead" ID="lblworkoutType"
                                                    Text='<%# Bind("workoutTypeName") %>'></asp:Label>
                                                    <asp:Label runat="server" Visible="false" ID="lblworkoutTypeId"
                                                        Text='<%# Bind("workoutTypeId") %>'></asp:Label>
                                                    <br />
                                                    <asp:Label runat="server" CssClass="lblWorkOutDtls"
                                                        Text='<%# "Done by :" +Eval("UserUsed")+"People" %>'></asp:Label><br />

                                                </div>

                                            </div>
                                            <asp:Label ID="lblgymOwnerId" runat="server" Text='<%#Bind("gymOwnerId") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblbranchId" runat="server" Text='<%#Bind("branchId") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblworkoutCatTypeId" runat="server" Text='<%#Bind("workoutCatTypeId") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("workoutTypeId") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblbookingId" runat="server" Text='<%#Bind("bookingId") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbluserId" runat="server" Text='<%#Bind("userId") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblOverAllCompletedStatus" runat="server" Text='<%#Bind("OverAllCompletedStatus") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblVideoUrl" runat="server" Text='<%#Bind("video") %>' Visible="false"></asp:Label>
                                            <asp:DataList ID="dtlSets" CellSpacing="20" runat="server" RepeatDirection="Vertical" RepeatColumns="1" Width="100%">
                                                <ItemTemplate>

                                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12" id="sets">

                                                        <div class="row">
                                                            <div class="col-4 col-sm-4 col-md-4 col-lg-4 workOutSets">
                                                                <asp:Label ID="lblsetTypeName" runat="server" Text='<%#Bind("setTypeName") %>'></asp:Label>

                                                            </div>
                                                            <div class="col-5 col-sm-5 col-md-5 col-lg-5 divSets">
                                                                <asp:Label ID="lblReps" runat="server" Text='<%#Bind("cnoOfReps") %>' CssClass="setsCount">
                                               
                                                                </asp:Label>
                                                                <span class="reps" runat="server">Reps
                                                                </span>

                                                                <span class="setsMarks" runat="server">X
                                                                </span>
                                                                <asp:Label ID="lblWeight" runat="server" Text='<%#Bind("cweight") %>' CssClass="setsCount">
                                                                </asp:Label><span class="reps" runat="server">Kilogram
                                                                </span>
                                                            </div>
                                                            <div class="col-3 col-sm-3 col-md-3 col-lg-3 workOutSetsCheck">
                                                                <asp:CheckBox ID="chkFinished" runat="server"
                                                                    Checked='<%#Eval("VideoCompletedStatus").ToString() == "Yes"? true:false %>'
                                                                    Enabled='<%#Eval("VideoCompletedStatus").ToString() == "Yes"? false:true %>'
                                                                    AutoPostBack="true" OnCheckedChanged="chkFinished_CheckedChanged" />
                                                            </div>
                                                        </div>
                                                        <br />

                                                    </div>
                                                    <asp:Label ID="lblcsetType" runat="server" Text='<%#Bind("csetType") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblgymOwnerId" runat="server" Text='<%#Bind("gymOwnerId") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lbluserId" runat="server" Text='<%#Bind("userId") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblbranchId" runat="server" Text='<%#Bind("branchId") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblworkoutCatTypeId" runat="server" Text='<%#Bind("workoutCatTypeId") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblworkoutTypeId" runat="server" Text='<%#Bind("workoutTypeId") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblbookingId" runat="server" Text='<%#Bind("bookingId") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblVideoCompletedStatus" runat="server" Text='<%#Bind("VideoCompletedStatus") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblOverAllCompletedStatus" runat="server" Text='<%#Bind("OverAllCompletedStatus") %>' Visible="false"></asp:Label>

                                                </ItemTemplate>
                                            </asp:DataList>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>

                    </div>
                    <div id="Plan" runat="server">
                        <div class="row mt-5">
                            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 PlanImgContainer">
                                <img class="CategoryImgBg" src="../../Images/BuyPlan/Classesimgbg.png" />
                                <asp:Label CssClass="CategoryNameBg" runat="server" ID="lblCategoryName"></asp:Label>
                                <img class="CategoryImg" src="<%=getPathName()%>" />
                                <div class="row DivPlanList">
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 
                                  col-xl-12 divCategoryListContainer">
                                        <%-- Fake button just for call onClick of 
                                     button event using jquery function--%>
                                        <asp:Button ID="btnfake" runat="server" OnClick="OnClick"
                                            Style="display: none" />
                                        <%-- Hidden field to set the ratio value--%>
                                        <asp:HiddenField ID="hfColumnRepeat" runat="server" Value="3" />
                                        <asp:DataList ID="dtlCategoryList" runat="server"
                                            RepeatDirection="Horizontal" RepeatColumns="3">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12"
                                                        runat="server">
                                                        <div class="divCategoryList" runat="server"
                                                            id="divCategoryList">
                                                            <asp:Label ID="lblcategoryName"
                                                                runat="server" Text='<%#Bind("categoryName") %>'
                                                                Visible="false"></asp:Label>
                                                            <asp:Label ID="lbltrainingTypeName" CssClass="lbldtlPlanName"
                                                                runat="server" Text='<%#Bind("trainingTypeName") %>'></asp:Label>
                                                            <asp:Label ID="lblplanDurationName"
                                                                CssClass="lbldtlPlanDuration" runat="server"
                                                                Text='<%#Bind("planDurationName") %>'></asp:Label>
                                                            <asp:Label ID="lblactualAmount" CssClass="lbldtlActAmt"
                                                                runat="server" Text='<%#"₹"+Eval( "actualAmount") %>'></asp:Label>
                                                            <asp:Label ID="lblnetAmount" CssClass="lbldtlAmt"
                                                                runat="server" Text='<%#"₹"+Eval( "netAmount") + "/-" %>'></asp:Label>
                                                            <asp:Label ID="lblCSaveAmt" CssClass="lbldtlSavedAmt"
                                                                runat="server" Text='<%#"Save "+"₹"+Eval( "SavedAmount") %>'></asp:Label>
                                                            <div class="DivdtlCategory">
                                                                <asp:DataList ID="dtlCategoryBenifit" runat="server" Visible="false">
                                                                    <ItemTemplate>
                                                                        <div class="row">
                                                                            <div class="col-12 col-sm-12 col-md-12col-lg-12 col-xl-12">
                                                                                <asp:Label ID="lblCtrainingTypeName" CssClass="lblCName"
                                                                                    runat="server"
                                                                                    Text='<%#Bind("description") %>'></asp:Label>
                                                                                <br />
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </div>

                                                            <asp:Label ID="lblcategoryId" CssClass="lblCName"
                                                                runat="server" Visible="false" Text='<%#Bind("categoryId") %>'></asp:Label>
                                                            <asp:Label ID="lblgymOwnerId" CssClass="lblCName"
                                                                runat="server" Visible="false" Text='<%#Bind("gymOwnerId") %>'></asp:Label>
                                                            <asp:Label ID="lblbranchId" CssClass="lblCName"
                                                                runat="server" Visible="false"
                                                                Text='<%#Bind("branchId") %>'></asp:Label>
                                                            <asp:Label ID="lblbranchName" CssClass="lblCName"
                                                                runat="server" Visible="false"
                                                                Text='<%#Bind("branchName") %>'></asp:Label>
                                                            <asp:Label ID="lbltrainingTypeId" CssClass="lblCName"
                                                                runat="server" Visible="false"
                                                                Text='<%#Bind("trainingTypeId") %>'></asp:Label>
                                                            <asp:Label ID="lbltrainingMode" CssClass="lblCName"
                                                                runat="server" Visible="false"
                                                                Text='<%#Bind("trainingMode") %>'></asp:Label>
                                                            <asp:Label ID="lblpriceId" CssClass="lblCName" runat="server"
                                                                Visible="false"
                                                                Text='<%#Bind("priceId") %>'></asp:Label>
                                                            <asp:Label ID="lblprice" CssClass="lblCName" runat="server"
                                                                Visible="false"
                                                                Text='<%#Bind("price") %>'></asp:Label>
                                                            <asp:Label ID="lbltaxId" CssClass="lblCName"
                                                                runat="server" Visible="false"
                                                                Text='<%#Bind("taxId") %>'></asp:Label>
                                                            <asp:Label ID="lbltaxName" CssClass="lblCName"
                                                                runat="server" Visible="false"
                                                                Text='<%#Bind("taxName") %>'></asp:Label>
                                                            <asp:Label ID="lbltaxAmount" CssClass="lblCName"
                                                                runat="server" Visible="false"
                                                                Text='<%#Bind("tax") %>'></asp:Label>
                                                            <asp:Label ID="lblTotalAmt" CssClass="lblCAmt"
                                                                runat="server" Visible="false"
                                                                Text='<%#Bind("netAmount") %>'></asp:Label>
                                                            <asp:Button CssClass="dtlbtnBuy" runat="server"
                                                                ID="btnBuyList" Text="BuyNow" OnClick="btnBuyList_Click" />
                                                            <asp:Label Visible="false" ID="lblplanDuration"
                                                                CssClass="lbldtlPlanName" runat="server"
                                                                Text='<%#Bind("planDuration") %>'></asp:Label>
                                                            <asp:Label Visible="false" ID="lbldisplayAmount"
                                                                CssClass="lbldtlPlanName" runat="server"
                                                                Text='<%#Bind("displayAmount") %>'></asp:Label>
                                                            <asp:Label Visible="false" ID="lblSavedAmount"
                                                                CssClass="lbldtlPlanName" runat="server"
                                                                Text='<%#Bind("SavedAmount") %>'></asp:Label>
                                                            <asp:Label Visible="false" ID="lblCttrainingTypeName"
                                                                CssClass="lbldtlPlanName" runat="server"
                                                                Text='<%#Bind("trainingTypeName") %>'></asp:Label>
                                                            <asp:Label Visible="false" ID="lblCnetAmount"
                                                                CssClass="lbldtlPlanName" runat="server"
                                                                Text='<%#Bind("netAmount") %>'></asp:Label>
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
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4" id="Summary" runat="server">
                    <div class="divSummary">
                        <div class="row">
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <asp:Label ID="lblSummaryPlanHead" CssClass="lblSummaryPlanHead" runat="server"></asp:Label>
                                <div>
                                    <asp:Label CssClass="lblSummaryPlanSubHead" runat="server">Transformation Pack</asp:Label>
                                </div>
                            </div>
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <asp:Label CssClass="lblSummaryDateHead" runat="server">Joining Date</asp:Label><br />
                                <div>
                                    <asp:Label ID="lblSummaryDate" CssClass="lblSummaryDate" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-5">
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <asp:Label ID="lblSummaryDuration" CssClass="lblSummaryDuration" runat="server"></asp:Label>
                            </div>
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <asp:Label ID="lblSummaryDisPlayAmt" CssClass="lblSummaryAmt" runat="server"></asp:Label>
                                <br />
                                <div>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-5">
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <asp:Label CssClass="lblSummaryDuration" runat="server">Actual Amount</asp:Label>
                            </div>
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <asp:Label ID="lblActualAmt" CssClass="lblSummaryAmt" runat="server"></asp:Label>
                                <br />
                                <div>
                                    <asp:Label ID="lblSaveAmt" CssClass="lblSaveAmt" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row mt-5">
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <asp:Label CssClass="lblSummaryFinalHead" runat="server">Amount</asp:Label>
                            </div>
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <asp:Label ID="lblSummaryAmtFinal" CssClass="lblSummaryAmtFinal" runat="server"></asp:Label>
                                <br />
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <asp:Label CssClass="lblSummaryTotalHead" runat="server">Total Payable</asp:Label>
                            </div>
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <asp:Label ID="lblSummaryTotalAmt" CssClass="lblSummaryTotalAmt" runat="server"></asp:Label>
                                <br />
                            </div>
                        </div>
                        <div class="text-center mt-5">
                            <asp:Button ID="btnBuyNow" runat="server" CssClass="btnPayNow" OnClick="btnBuyNow_Click" Text="Buy Now" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>




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
    <%--Login Stylesheet--%>
    <link href="popupLoginDiet.css" rel="stylesheet" />
    <%--Content Page Css--%>
    <link href="popupContentPageDiet.css" rel="stylesheet" />
    <div id="DivLogin" runat="server" class="DisplyCard1" visible="false">
        <div class="LoginCard">
            <div class="row Logincontent">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 LoginHeight">

                    <a>
                        <img src="~/Images/Login/PaypreLogo.png" runat="server" class="LoginLogoImage" /></a>
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
    <script defer src='<%=ResolveUrl("../Subscription/qrcode.min.js") %>'></script>
    <script defer src='<%=ResolveUrl("PopupPaymentPageDiet.js") %>'></script>

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
                                                <div class="payresponsemsg" id="payrrorresponse">
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
                <p>Your Plan is Purchased Successfully !</p>
            </div>

        </div>

    </div>
    <asp:Button ID="btnHiddenCloseVideoPopup" OnClick="btnHiddenCloseVideoPopup_Click" ClientIDMode="Static" runat="server" CssClass="d-none" />

    <asp:HiddenField ID="hfSubscription" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfSubscriptionInsert" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfBaseurl" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfToken" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfBookingId" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfPaymentBaseurl" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfNotificationSMSBaseurl" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfNotificationSMSDatas" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfNotificationSMSUserId" ClientIDMode="Static" runat="server" />


    <style>
        .DisplyCard2 {
            position: fixed;
            text-align: center;
            height: 100%;
            width: 100%;
            top: 0;
            right: 0;
            left: 0;
            z-index: 9997;
            background-color: #00000059;
            padding-top: 2rem;
            padding-bottom: 2rem;
            display: flex;
            justify-content: center;
            align-items: center;
        }


        .LoginCard1 {
            background-color: #ffffff;
            border-radius: 1rem;
            padding: 2rem;
            z-index: 1;
            padding-bottom: 4rem;
        }

        .Logincontent {
            min-height: 54px;
            background: #ffffff;
            margin: 2rem;
            border-radius: 30px;
        }

        .PopUpMain {
            font-family: 'Manrope';
            font-style: normal;
            font-weight: 600;
            font-size: 2rem;
            line-height: 32px;
            text-align: center;
        }

        .PopUpSub {
            font-family: 'Manrope';
            font-style: normal;
            font-weight: 500;
            font-size: 1.4rem;
            line-height: 20px;
        }
    </style>


    <div id="DivDietPopup" runat="server" class="DisplyCard2" visible="false" onclick="CloseClick()">
        <div class="LoginCard1" id="LoginCard">
            <div class="row Logincontent">
                <div class="PopUpMain">
                    To See Exact Fooditems 
                </div>
                <div class="PopUpSub">
                    Buy Now
                </div>

            </div>
        </div>
    </div>

    <%--      <div id="DivWorkOutpOpup" runat="server" class="DisplyCard1" style="visibility:hidden" onclick="CloseClick()">--%>
    <div id="DivWorkOutpOpup" runat="server" class="DisplyCard2" onclick="CloseClick()" visible="false">
        <div class="LoginCard1">
            <div class="row Logincontent">
                <div class="PopUpMain">
                    To See Exact Workout Video
                </div>
                <div class="PopUpSub">
                    Buy Now
                </div>

            </div>
        </div>
    </div>
    <script>
        function CloseClick() {

            document.getElementById('btnHiddenCloseVideoPopup').click();

        }
    </script>

    <script>
       /* window.scrollTo(0, 10);*/
        function positionTabsAfterNav() {
            let navHeight = document.getElementsByTagName('nav')[0].offsetHeight;
            let tabAllClassesHeight = document.querySelector('.NavDetails').offsetHeight;
            document.querySelector('.NavDetails').style.top = `${navHeight}px`;
            document.querySelector('.divHeadDtl').style.top = `${navHeight + tabAllClassesHeight - 1}px`;
        }
        window.onscroll = function () {
            positionTabsAfterNav();
        }

    </script>
</asp:Content>

