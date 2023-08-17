<%@ Page Title="" Language="C#" MasterPageFile="~/Fitness.Master" AutoEventWireup="true" CodeFile="WorkOut.aspx.cs" Inherits="Pages_DietWorkOut_WorkOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FitnessContent" runat="Server">
    <script defer src='<%=ResolveUrl("../Master/Master.js") %>'></script>
    <link href="WorkOut.css" rel="stylesheet" />
    <script defer src='<%=ResolveUrl("Workout.js") %>'></script>

    <div class="container">
        <div class="col-12 col-sm-4 col-md-4 col-lg-4">
            <div class="Workname">
                <a href="DietWorkOut.aspx" style="text-decoration: none">
                    <img src="../../Images/Master/Arrow.svg" alt="Arrow" class="Arrow" /></a>
                <p class="workoutcategory" id="paraWorkOut" runat="server">
                </p>

            </div>

        </div>
        <div class="col-12 WorkOutContent" id="divWorkoutTarget">
            <div class="row divTarget">
                <%--    Target--%>

                <div class="col-12 col-sm-8 col-md-8 col-lg-8">
                    <div class="col-12 divresponsive">
                        <asp:Button ID="btnfake" runat="server" OnClick="OnClick" Style="display: none" />
                        <asp:HiddenField ID="hfColumnRepeat" ClientIDMode="Static" runat="server" Value="3" />
                        <asp:DataList ID="dtlWorkOutCategory" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" Width="100%">
                            <ItemTemplate>
                               

                                    <div class="col-11 col-sm-9 col-md-9 col-lg-9 divWorkOuttargetItems" id="divWorkOuttargetItems" runat="server">
                                        <asp:LinkButton ID="WorkOutCat" runat="server" CssClass="lnkWorkout" OnClick="WorkOutCat_Click">

                                            <asp:Image ID="ImageSelected" runat="server" ImageUrl="~/Images/DietWotkOut/pngwing15.svg" CssClass="selectedImage"
                                                Visible='<%#Eval("OverAllCompletedStatus").ToString() == "Yes"? true:false %>' />
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/DietWotkOut/Group.svg" CssClass="Arrowtransform" />
                                            <asp:Image ID="ImgWorKoutType" runat="server" ImageUrl='<%#Eval("imageUrl").ToString() == ""?"../../Images/DietWotkOut/ph2.png":Eval("imageUrl").ToString() %>'
                                                CssClass="workoutImages" />
                                            
                                            <asp:Label ID="lblWorkOutTypeCategory" runat="server" CssClass="workoutItems" Text='<%#Bind("workoutTypeName") %>'>

                                            </asp:Label>
                                            <div class="workoutDone">
                                                <span>Done by : 
                                                <asp:Label ID="lblDonePeople" runat="server" Text='<%#Bind("UserUsed") %>'>
                                            
                                                </asp:Label>
                                                    People</span>
                                            </div>

                                            <label class="WorkOutTime">
                                                <img src="../../Images/DietWotkOut/three-o-clock-clock.png" class="Clock" />
                                                &nbsp; 55 min
                                            </label>
                                        </asp:LinkButton>
                                        <asp:Label ID="lblgymOwnerId" runat="server" Text='<%#Bind("gymOwnerId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblbranchId" runat="server" Text='<%#Bind("branchId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblworkoutCatTypeId" runat="server" Text='<%#Bind("workoutCatTypeId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblworkoutTypeId" runat="server" Text='<%#Bind("workoutTypeId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblbookingId" runat="server" Text='<%#Bind("bookingId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbluserId" runat="server" Text='<%#Bind("userId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblOverAllCompletedStatus" runat="server" Text='<%#Bind("OverAllCompletedStatus") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblVideoUrl" runat="server" Text='<%#Bind("video") %>' Visible="false"></asp:Label>
                                    </div>

                               
                            </ItemTemplate>
                        </asp:DataList>


                    </div>
                </div>

                <div class="col-12 col-sm-12 col-md-4 col-lg-4 divWorkOutConsumed" runat="server" id="DivWorkoutConsumed" visible="false">
                    <div class="col-12">
                        <div class=" ConsumedContainer">
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4">
                                <asp:Image ID="ImgConsumed" runat="server" CssClass="ConsumedImages" />

                            </div>
                            <div class="col-12 col-sm-8 col-md-8 col-lg-8 ConsumedContainerFlex">

                                <asp:Label ID="lblWorkoutConsumed" runat="server" CssClass="WorkoutConsumed"></asp:Label>
                                <div class="workoutConsumedCategory">
                                    <asp:Label ID="lblWorkOuts" runat="server" CssClass="WorkOutConsumedLabel"></asp:Label>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 col-sm-12 col-md-12 col-lg-12 WorkOutVideo">

                                <iframe id="ConsumedVideo" runat="server" class="videoStyle"
                                    frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                            </div>
                        </div>
                        <div class="row">
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
                        </div>

                        <%--     <div class="row">
                            <label class="Instructions">
                                Step by step Instructions
                            </label>
                        </div>
                        <div class="row">
                            <label class="steps">
                                Step 1
                            </label>
                        </div>
                        <div class="row">
                            <label class="StpesList">
                                Now Exhale and Sowly raise your head, shoulders and upper body off the ground.
                            </label>
                        </div>

                        <div class="col-12 containers">
                            <asp:Button ID="btnFinished" runat="server" Text="Finished" CssClass="finishButton" OnClick="btnFinished_Click" />

                        </div>--%>
                    </div>
                </div>



            </div>
        </div>
    </div>

</asp:Content>

