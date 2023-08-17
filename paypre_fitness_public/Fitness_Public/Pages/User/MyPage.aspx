<%@ Page Title="My Page" Language="C#" MasterPageFile="~/Fitness.Master" AutoEventWireup="true" CodeFile="MyPage.aspx.cs" Inherits="Pages_User_MyPage" %>

<%@ MasterType VirtualPath="~/Fitness.Master" %>
<asp:Content ID="CntMyPage" ContentPlaceHolderID="FitnessContent" runat="Server">
    <link href="myPage.css" rel="stylesheet" />
    <script src="https://kit.fontawesome.com/6aac34c85b.js" crossorigin="anonymous"></script>
    <div class="container-fluid">
        <div class="divWelcome">
            <label class="lblWelcome">Welcome<asp:Label runat="server" ID="lblUserName" class="lblUserName"></asp:Label></label>
        </div>

        <div class="row">
            <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8">   
                <div class="UserDtlContainer">
                    <div class="row">
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4" >
                            <label class="lblPlanDtlHead">Plan Details</label>
                             <div class="divPlandtl" id="UserPlanDetails" runat="server">
                                <label class="lblPlanDtl">
                                    Your Plan :
                                    <label id="categoryname" runat="server"><%--Muscle Building--%></label></label>
                                <label class="lblPlanDtl">
                                    Slot Timing :
                                    <label id="SlotTime" runat="server"><%--8.00AM--%></label></label>
                                <label class="lblPlanDtl">
                                    Trainer :
                                    <label id="TrainerName" runat="server"><%--Ganesh--%></label></label>
                                <label class="lblPlanDtl">
                                    Category  :
                                    <label id="traningMode" runat="server"><%--Online--%></label></label>                            
                                <label class="lblPlanDtl">
                                    Trainer Contact:
                                    <label id="TrainermobileNo" runat="server"><%--8956809509--%></label></label>
                                <label class="lblPlanDtl">
                                    Plan Duations:
                                    <label id="planduration" runat="server"><%--8956809509--%></label></label>
                           
                            </div>
                             <div class="divPlandtl" id="Nouserplan" runat="server">
                                <label class="lblPlanDtl">
                                    Plan Details Not Generated !!!</label>
                           
                            </div>
                            <label id="subheader" runat="server" class="lblPlanDtlHead">Subscription Details</label>
                             <div class="divPlandtl" id="UserSubscriptionDetails" runat="server">
                                <label class="lblPlanDtl">
                                    Your Package Name :
                                    <label id="lblpackageName" runat="server"><%--Muscle Building--%></label></label>
                                <label class="lblPlanDtl">
                                    Branch Name :
                                    <label id="lblbranchName" runat="server"><%--8.00AM--%></label></label>
                                <label class="lblPlanDtl">
                                    GymOwner Name :
                                    <label id="lblgymOwnerName" runat="server"><%--Ganesh--%></label></label>
                                <label class="lblPlanDtl">
                                    Plan Duations:
                                    <label id="lblplanduration" runat="server"><%--8956809509--%></label></label>
                           
                            </div>

                        </div>
                        <%--<div class="col-12 col-sm-1 col-md-1 col-lg-1 col-xl-1 vl"></div>--%>
                        <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">
                            <div class="divlive">
                                <div class="row">
                                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 float-start">
                                        <label id="videoheading" runat="server" class="lblVideoHead">Live Video</label>
                                    </div>
                                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 float-end">
                                        <asp:LinkButton CssClass="btnWorkout" ID="workoutlink" runat="server" PostBackUrl="~/Pages/WorkOut/WorkOut.aspx">
                                             <i class="fa-solid fa-dumbbell fa-beat-fade"></i>&nbsp Workout</asp:LinkButton>
                                        <asp:LinkButton CssClass="btnWorkout" ID="dietplanlink" runat="server" PostBackUrl="~/Pages/Meals/Meal.aspx">
                                           <i class="fa-solid fa-utensils fa-beat-fade"></i>&nbsp Diet Paln</asp:LinkButton>
                                    </div>
                                </div>
                                <iframe id="iliveUrl" runat="server" class="demovid"  title="YouTube video player"
                                    frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
                                    allowfullscreen></iframe>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
            <div class="col-12 col-sm-12 col-md-4 col-lg-4 col-xl-4">
                <div class="divWorkOut">
                    <div class="row">
                        <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5">
                            <div class="divCol">
                                <div class="Colhead">
                                    <div>
                                        <i class="fa-solid fa-fire-flame-simple fa-fade"></i>
                                    </div>
                                    <i class="fas fa-ellipsis-h f_s_11 white_text"></i>
                                </div>
                                <div class="Colbody">
                                    <asp:Label ID="lblTotalCal" runat="server" class="lblCol"></asp:Label><br />
                                    <label class="lbltotalCal">Total Calories</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5">
                            <div class="divCol">
                                <div class="Colhead Coltotal">
                                    <div>
                                        <i class="fa-sharp fa-solid fa-fire-flame-curved fa-flip"></i>
                                    </div>
                                    <i class="fas fa-ellipsis-h f_s_11 white_text"></i>
                                </div>
                                <div class="Colbody">
                                    <asp:Label ID="lblConsumedCal" runat="server" class="lblCol"></asp:Label><br />
                                    <label class="lbltotalCal">Consumed Calories</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5">
                            <div class="divCol">
                                <div class="Colhead water">
                                    <div>
                                        <i class="fa-solid fa-droplet fa-beat"></i>
                                    </div>
                                    <i class="fas fa-ellipsis-h f_s_11 white_text"></i>
                                </div>
                                <div class="Colbody">
                                    <label class="lblCol">3 L</label><br />
                                    <label class="lbltotalCal">Water Consumed</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5">
                            <div class="divCol">
                                <div class="Colhead workout">
                                    <div>
                                        <i class="fa-solid fa-dumbbell fa-bounce"></i>
                                    </div>
                                    <i class="fas fa-ellipsis-h f_s_11 white_text"></i>
                                </div>
                                <div class="Colbody">
                                    <asp:Label runat="server" ID="lblCompletedCount" CssClass="lblCol"></asp:Label>
                                    <asp:Label runat="server" CssClass="lblCol" ID="lblTotalWorkoutCount"></asp:Label><br />
                                    <label class="lbltotalCal">Total Workout</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </div>
</asp:Content>

