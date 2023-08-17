<%@ Page Title="WorkOut" Language="C#" MasterPageFile="~/Fitness.Master" EnableEventValidation="true"
    AutoEventWireup="true" CodeFile="WorkOut.aspx.cs" Inherits="Pages_WorkOut_WorkOut" %>

<asp:Content ID="CndWorkout" ContentPlaceHolderID="FitnessContent" runat="Server">
    <%@ MasterType VirtualPath="~/Fitness.Master" %>
    <script src="https://kit.fontawesome.com/6aac34c85b.js" crossorigin="anonymous"></script>
    <link href="WorkOut.css" rel="stylesheet" />
    <div class="container-fluid containerBg">
        <div>
            <div class="divWorkOutHead">
                <label class="lblWorkOutHead">My Workout</label>
            </div>
            <div id="divWorkOut" runat="server" class="divWorkOut">
                <div class="divWorkOutDtl">
                    <label class="lblWortOutDtl">Start your WorkOut Now <i class="fa-solid fa-dumbbell fa-bounce"></i></label>
                </div>
                <asp:DataList ID="dtlCategory" RepeatDirection="Horizontal" RepeatColumns="3" runat="server">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 workoutlist">
                                <asp:Label ID="lblworkoutCatTypeName" runat="server" class="lblWorkOutName" Text='<%#Bind("workoutCatTypeName") %>'></asp:Label>
                                <asp:ImageButton ID="imgCat" CssClass="imgworkout" OnClick="imgCat_Click" runat="server" ImageUrl='<%#Bind("imageUrl") %>' />
                                <asp:Label ID="lblworkoutCatTypeId" runat="server" class="lblWorkOutName" Visible="false" Text='<%#Bind("workoutCatTypeId") %>'></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div id="divWorkOutVideo" runat="server" visible="false">
                <div class="divWorkOutTypeNav">
                    <asp:DataList ID="dtlCategoryList" RepeatDirection="Horizontal" RepeatColumns="3" runat="server">
                        <ItemTemplate>
                            <div class="divCategoryList">
                                <asp:LinkButton ID="lnkCategoryList" runat="server" class="lblWorkOutTypeHead" Text='<%#Bind("workoutCatTypeName") %>'
                                    OnClick="lnkCategoryList_Click"></asp:LinkButton>
                                <asp:Label ID="lblworkoutCatTypeId" runat="server" class="lblWorkOutName" Visible="false" Text='<%#Bind("workoutCatTypeId") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3">

                        <div id="DivBreakFast" class="Mealtype" runat="server">
                            <div class="row">
                                <div class="col-8 col-sm-8 col-md-8 col-lg-8 col-xl-8 mb-3">
                                    <div class="row">
                                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 DivWorkoutVid">
                                            <asp:DataList ID="dtlWorkoutType" RepeatDirection="Horizontal" OnItemDataBound="dtlWorkoutType_ItemDataBound"
                                                RepeatColumns="2" runat="server">
                                                <ItemTemplate>
                                                    <iframe id="ConsumedVideo" class="VideoWork"
                                                        src='<%# Eval("video")+"?autoplay=0&modestbranding=1&mode=opaque&amp;" +
                                                         "rel=0&amp;autohide=0&amp;showinfo=0&amp;wmode=transparent"%>'
                                                        frameborder="0" allow="accelerometer; autoplay; clipboard-write; 
                                                        encrypted-media; gyroscope; picture-in-picture"
                                                        allowfullscreen></iframe>
                                                    <br />
                                                    <asp:Label ID="lblworkoutTypeName" runat="server" class="lblWorkoutType" Text='<%#Bind("workoutTypeName") %>'></asp:Label>
                                                    <asp:Label ID="lblworkoutCatTypeId" runat="server" class="lblWorkoutType" Visible="false" Text='<%#Bind("workoutCatTypeId") %>'></asp:Label>
                                                    <asp:Label ID="lblworkoutTypeId" runat="server" class="lblWorkoutType" Visible="false" Text='<%#Bind("workoutTypeId") %>'></asp:Label>
                                                    <asp:DataList ID="dtlSetps" RepeatDirection="Horizontal" runat="server" Width="100%" OnItemDataBound="dtlSetps_ItemDataBound">
                                                        <ItemTemplate>
                                                            <div class="divSetps">
                                                                <div class="row">
                                                                    <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">
                                                                        <asp:Label ID="lblsetTypeName" runat="server" class="lblSets" Text='<%#Bind("setTypeName") %>'>&nbsp&nbsp </asp:Label>
                                                                        <asp:Label ID="lblcnoOfReps" runat="server" class="lblReps" Text='<%#Eval("cnoOfReps") + "&nbsp&nbsp&nbsp&nbsp" + "x" + "&nbsp&nbsp&nbsp&nbsp" %>'></asp:Label>
                                                                        <asp:Label ID="lblcweight" runat="server" class="lblReps" Text='<%#Bind("cweight") %>'></asp:Label>
                                                                        <div>
                                                                            <asp:Label ID="lblRepsHead" runat="server" class="lblRepsHead">Reps</asp:Label>
                                                                            <asp:Label ID="lblWeight" runat="server" class="lblRepsHead">Weight</asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">                                                                       
                                                                        <asp:CheckBox ID="chkWorkoutDone" runat="server" ClientIDMode="Static"
                                                                            AutoPostBack="True" OnCheckedChanged="chkWorkoutDone_CheckedChanged"
                                                                            Checked='<%#Eval("VideoCompletedStatus").ToString() == "Yes"? true:false %>'
                                                                            Enabled='<%#Eval("VideoCompletedStatus").ToString() == "Yes"? false:true %>' />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:Label ID="lbluserId" runat="server" class="lblSets" Visible="false" Text='<%#Bind("userId") %>'></asp:Label>
                                                            <asp:Label ID="lblbookingId" runat="server" class="lblSets" Visible="false" Text='<%#Bind("bookingId") %>'></asp:Label>
                                                            <asp:Label ID="lblcsetType" runat="server" class="lblSets" Visible="false" Text='<%#Bind("csetType") %>'></asp:Label>
                                                            <asp:Label ID="lblcnoOfRepss" runat="server" class="lblSets" Visible="false" Text='<%#Eval("cnoOfReps") %>'></asp:Label>
                                                            <asp:Label ID="lblworkoutCatTypeId" runat="server" class="lblWorkoutType" Visible="false" Text='<%#Bind("workoutCatTypeId") %>'></asp:Label>
                                                            <asp:Label ID="lblworkoutTypeId" runat="server" class="lblWorkoutType" Visible="false" Text='<%#Bind("workoutTypeId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:DataList>

                                                </ItemTemplate>
                                            </asp:DataList>


                                        </div>
                                    </div>
                                </div>
                                <div class="col-3 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3 DivActivity">
                                    <asp:Label ID="WeeklyProgress" runat="server" CssClass="weekly-header" Text="Weekly Progress">
                                    <span>Weekly Progress</span>
                                    </asp:Label>
                                    <div class="icon-sec">
                                        <img src="icon-weekly-goal.b61c80cd.svg" />
                                    </div>
                                    <asp:Label ID="work" runat="server" CssClass="itemsName">
                                        <asp:Label ID="lblCompletedCount" runat="server"></asp:Label>
                                        /<asp:Label ID="lblTotalWorkoutCount" runat="server"></asp:Label>
                                    </asp:Label>
                                    <br />
                                    <asp:Label ID="lbl2" runat="server" CssClass="item-subname">WorkOuts</asp:Label>
                                    <hr class="hrd" />

                                    <div class="icon-sec">
                                        <img src="icon-cal-fire.9f062770.svg" />
                                    </div>
                                    <asp:Label ID="Label1" runat="server" CssClass="itemsName">0</asp:Label>
                                    <br />
                                    <asp:Label ID="Label2" runat="server" CssClass="item-subname">Calories</asp:Label>
                                    <hr class="hrd" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     
    <script>
        function confirmClick(chkId) {
            // Display a confirmation prompt
            var confirmed = confirm("Please confirm if you wish to proceed with completing this workout.");

            // If the user confirms, trigger the postback
            if (confirmed) {
                __doPostBack(chkId, '');
                console.log('true');
            }

            // Return false to prevent the checkbox from being checked/unchecked directly
            return false;
        }
    </script>
</asp:Content>

