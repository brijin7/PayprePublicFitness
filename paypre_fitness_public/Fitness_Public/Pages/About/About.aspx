<%@ Page Title="" Language="C#" MasterPageFile="~/Fitness.Master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="Pages_HomeNew_HomeNew" %>

<asp:Content ID="CtntHome" ContentPlaceHolderID="FitnessContent" runat="Server">
    <%-- this script is used to hide links in Navbar(ex: classes, Testimonials etc.) --%>
    <script defer src='<%=ResolveUrl("../Master/Master.js") %>'></script>

    <%-- Common Heading Styles --%>
    <link href="About_CommonHeadings.css" rel="stylesheet" />
    <link href="About_Accolades.css" rel="stylesheet" />
    <link href="Certificate.css" rel="stylesheet" />
    <link href="About_Index.css" rel="stylesheet" />
    <%-- CEO --%>
    <link href="About_Ceo.css" rel="stylesheet" />
    <div class="container-fluid">
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

        <div>
            <div class="row">
                <div class="col-1" style="text-align-last: end; margin-top: 3rem;">
                    <asp:ImageButton ID="btnBack" src="../../Images/About/LeftArrow.svg" Style="width: 25%;" runat="server" OnClick="btnBack_Click" />
                </div>
                <div class="col-5">
                    <img class="CeoImg" src="../../Images/About/Ceo.png" />
                </div>
                <div class="col-6">
                    <asp:Label runat="server" CssClass="CeoName">ROCKIE</asp:Label>
                    <asp:Label runat="server" CssClass="CeoFullName">RAJESHKUMAR</asp:Label>
                    <asp:Label runat="server" CssClass="CeoOf">CEO OF ROCKS FITNESS</asp:Label>
                    <asp:Label runat="server" CssClass="CeoEdu">B.E, ACE, INFS, ISSN, ACE Advance Health Coach, </asp:Label>
                    <asp:Label runat="server" CssClass="CeoEdunl">ACE Functional Training Specialist & Diet Expert  </asp:Label>
                    <img class="CeoHr" src="../../Images/About/line.svg" />
                    <img class="CeoSign" src="../../Images/About/ceoSign.svg" />

                </div>

            </div>


            <div class="col-12 mt-3">
                <div class="wrapper">
                    <div class="marquee">
                        <p>
                            ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; 
                        ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp;ROCKFITNESS&nbsp;&nbsp;
                        ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp;
                        ROCKFITNESS&nbsp;&nbsp;ROCKFITNESS&nbsp;&nbsp;  ROCKFITNESS&nbsp;&nbsp;ROCKFITNESS&nbsp;&nbsp;

                       ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; 
                       ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp;
                        </p>
                        <p>
                            ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; 
                        ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp;ROCKFITNESS&nbsp;&nbsp;
                        ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp;
                        ROCKFITNESS&nbsp;&nbsp;ROCKFITNESS&nbsp;&nbsp;  ROCKFITNESS&nbsp;&nbsp;ROCKFITNESS&nbsp;&nbsp;

                       ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; 
                       ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp; ROCKFITNESS&nbsp;&nbsp;
                        </p>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-4 pe-0 ps-0">
                <div class="DivHead">
                    <label class="lblAccHead">Accolades</label>
                </div>
                <div class="DivAccoladesbg">
                    <div class="DivExp">
                        <label class="lblExp1">14</label>
                        <label class="lblExpPlus">+</label>
                        <label class="lblExpYear">Years OF</label>
                        <br />
                        <label class="lblExp2">Experience</label>
                    </div>
                    <div class="divTrans">
                        <label class="lblTrans1">TRANSFORMED</label><br />
                        <label class="lblTrans2">lives</label>
                        <label class="lblTrans3">1000+</label>
                    </div>
                    <div class="DivKA">
                        <img class="AwdImg1" src="../../Images/About/aw1.png" />
                        <label class="lblKA1">MR.KARNATAKA</label>
                        <br />
                        <label class="lblKA2">2016</label>
                    </div>
                    <div class="DivInt">
                        <img class="AwdImg1" src="../../Images/About/aw3.png" />
                        <label class="lblInt1">INTERNATIONAL YASH AWARDS</label><br />
                        <label class="lblInt2">BEST TRANSFORMATION & FITNESS EXPERT</label>
                    </div>
                    <div class="DivTN">
                        <label class="lblTN1">MR.TAMILNADU</label>
                        <label class="lblTN2">2015</label>
                        <img class="AwdImg1" src="../../Images/About/aw1.png" />
                    </div>
                    <div class="DivInt2">
                        <label class="lblInt21">SHARED A JUDGING PANEL</label>
                        <label class="lblInt22">INTERNATIONAL & NATIONAL</label><br />
                        <label class="lblInt23">LEVEL BODY BULIDING SHOWS</label>
                    </div>
                    <div class="DivInt3">
                        <label class="lblInt31">INTERNATIONAL</label>
                        <img class="AwdImg2" src="../../Images/About/aw2.png" />
                        <label class="lblInt32">TITLE BEACH BODY MODEL</label>
                        <label class="lblInt33">@MAURITIUS</label>
                    </div>
                    <img src="../../Images/About/AccoladesBg.gif" class="AccImg" />
                </div>
            </div>

        </div>

        <div class="row">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-4 pe-0 ps-0 mb-5">
                <div class="DivHead">
                    <label class="lblAccHead">Certificate of Excellence</label>
                </div>
                <div class="carouselDiv">
                    <div class="carousel right">
                        <div class="slide"></div>
                        <div class="wrap">
                            <ul>
                                <li>
                                    <label id="CCloseBtn" onclick="CClose()" class="CClose d-none">X</label>
                                    <label class="lblC1Text">
                                        Global Human
                                        <br />
                                        Peace University</label>
                                    <label class="lblC1Text1">Doctor of Sports</label>
                                    <img id="imgC1" onclick="C1()" class="imgC1" src="../../Images/About/C1.svg" /></li>
                                <li>
                                    <label class="lblC1Text">
                                        American Council 
                                        <br />
                                        on Exercise</label>
                                    <label class="lblC1Text1">
                                        Certified Personal
                                        <br />
                                        Trainer</label>
                                    <img id="imgC2" onclick="C2()" class="imgC2" src="../../Images/About/C2.svg" /></li>
                                <li>
                                    <label class="lblC1Text">
                                        Certificate of 
                                        <br />
                                        Completion</label>
                                    <label class="lblC1Text1">
                                        Classic Fitness
                                    </label>
                                    <img id="imgC3" onclick="C3()" class="imgC3" src="../../Images/About/C3.svg" /></li>
                                <li>
                                    <label class="lblC1Text">
                                        American Council 

                                        <br />
                                        on Exercise</label>
                                    <label class="lblC1Text1">
                                        Certified 
                                        <br />
                                        Personal Trainer 
                                    </label>
                                    <img id="imgC4" onclick="C3()" class="imgC4" src="../../Images/About/C4.svg" /></li>
                                <li>
                                    <label class="lblC1Text">
                                        American Council 
                                        <br />
                                        on Exercise</label>
                                    <label class="lblC1Text1">
                                        Functional Training  
                                        <br />
                                        Specialist Program  
                                    </label>
                                    <img id="imgC5" onclick="C5()" class="imgC5" src="../../Images/About/C5.svg" /></li>
                                <li>
                                    <label class="lblC1Text">
                                        Certificate of 
                                        <br />
                                        Completion</label>
                                    <label class="lblC1Text1">
                                        COVID safe Coaches & 
                                        <br />
                                        Officials Certification 
                                    </label>
                                    <img id="imgC6" onclick="C6()" class="imgC6" src="../../Images/About/C6.svg" /></li>
                                <li>
                                    <label class="lblC1Text">
                                        Certificate of
                                        <br />
                                        Achivement</label>
                                    <label class="lblC1Text1">
                                        Transformation  Expert &
                                        <br />
                                        Fitness Expert
                                    </label>
                                    <img id="imgC7" onclick="C7()" class="imgC7" src="../../Images/About/C7.svg" /></li>
                                <li>
                                    <label class="lblC1Text">
                                        Certificate of
                                        <br />
                                        Achivement</label>
                                    <label class="lblC1Text1">
                                        Transformation  Expert &
                                        <br />
                                        Fitness Expert
                                    </label>
                                    <img id="imgC8" onclick="C8()" class="imgC8" src="../../Images/About/C8.svg" /></li>


                            </ul>
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <div class="row">
            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 ps-0 pe-0">
                <div class="divabtFit">
                    <video autoplay loop muted class="AboutVid">
                        <source src="../../Images/About/AboutVideo.mp4" type="video/mp4">
                    </video>
                </div>
            </div>
            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                <div class="divabtDtls">

                    <div class="DivabtdtlsHead">
                        <label class="lblAbtDtlsHead">About Rocks Fitness</label>
                        <div class="divabtcnt">
                            <p class="abttextcnt">• Rocks fitness is an one stop solution for all your fitness & Health needs </p>
                            <p class="abttextcnt">• Hosur 1 st biggest luxury gym with 3 spacious floors </p>
                            <p class="abttextcnt">• CARDIO & MACHINE FLOOR  </p>
                            <p class="abttextcnt">• WOMENS FLOOR   </p>
                            <p class="abttextcnt">• CROSS FIT & GROUP CLASS    </p>
                            <p class="abttextcnt">• Equipment are from cybex & imported from germany  </p>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                <div class="divEquipmnet">
                    <label class="EquipHead">Equipments We have</label>
                    <div class="row">
                        <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2 divEquip">
                            <p class="mb-0">• Threadmills </p>
                            <p class="mb-0">• Cross Trainers </p>
                            <p class="mb-0">• Cycles </p>
                            <p class="mb-0">• Rowing Machins  </p>
                            <p class="mb-0">• Lat Pulldown  </p>
                            <p class="mb-0">• Chest Press  </p>
                            <p class="mb-0">• Hammers  </p>
                        </div>
                        <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2 divEquip">
                            <p class="mb-0">• Pec / Rear Fly </p>
                            <p class="mb-0">• Sholder Press </p>
                            <p class="mb-0">• Lareal Raise </p>
                            <p class="mb-0">• Bicep / Tricep  </p>
                            <p class="mb-0">• Preacher Curls  </p>
                            <p class="mb-0">• Smith Machine  </p>
                            <p class="mb-0">• Functional Trainer  </p>
                            <p class="mb-0">• Barbells & Dumbells  </p>
                        </div>
                        <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2 divEquip">
                            <p class="mb-0">• Assisted Chin / DIP </p>
                            <p class="mb-0">• Incline Bench Press </p>
                            <p class="mb-0">• Decline Bench Press </p>
                            <p class="mb-0">• Flat Bench Press  </p>
                            <p class="mb-0">• Adjustable Benches  </p>
                            <p class="mb-0">• Utlity Benches  </p>
                            <p class="mb-0">• Squat Racks  </p>
                            <p class="mb-0">• Yoga Mats  </p>
                        </div>
                        <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2 divEquip">
                            <p class="mb-0">• Leg Press </p>
                            <p class="mb-0">• Leg Extension </p>
                            <p class="mb-0">• Leg Curls </p>
                            <p class="mb-0">• Hack Squat  </p>
                            <p class="mb-0">• Calves Raise   </p>
                            <p class="mb-0">• Free Weights  </p>
                            <p class="mb-0">• Kettle Bells  </p>
                        </div>
                        <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2 divEquip">
                            <p class="mb-0">• Boxing Bags </p>
                            <p class="mb-0">• Rops </p>
                            <p class="mb-0">• Steppers  </p>
                            <p class="mb-0">• Tyres  </p>
                            <p class="mb-0">• Swiss Balls   </p>
                            <p class="mb-0">• Med Balls  </p>
                            <p class="mb-0">• Bous  Balls </p>
                        </div>
                        <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2 divEquip">
                            <p class="mb-0">• Med Balls </p>
                            <p class="mb-0">• Plyometric Jump Stool </p>
                            <p class="mb-0">• Slam Balls  </p>
                            <p class="mb-0">• Strength Sand Bag  </p>
                            <p class="mb-0">• Sledge Push   </p>
                            <p class="mb-0">• TRX  </p>
                            <p class="mb-0">• Cones, Ladders & Hurdles </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row mt-2">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                <div class="divEquipmnet">
                    <label class="EquipHead">Group Class</label>
                    <div class="row">
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 divEquip">
                            <p class="mb-0">• MMA Training  </p>
                            <p class="mb-0">• HIIT Squad </p>
                            <p class="mb-0">• Warrior Workout </p>
                            <p class="mb-0">• Body Pump  </p>
                            <p class="mb-0">• RFM   </p>
                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 divEquip">
                            <p class="mb-0">• Body toning & Condtioning </p>
                            <p class="mb-0">• Stepper HIIT  </p>
                            <p class="mb-0">• Zumba </p>
                            <p class="mb-0">• Aerobics  </p>
                            <p class="mb-0">• Bollywood beats  </p>
                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 divEquip">
                            <p class="mb-0">• Cross Fit  </p>
                            <p class="mb-0">• Yoga  </p>
                            <p class="mb-0">• Bosy Step Hip Hop </p>
                            <p class="mb-0">• SuperHero  </p>
                            <p class="mb-0">• Spartan Training </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row mt-5">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                <div class="divEquipmnet">
                    <label class="EquipHead">Our Special Trainers</label>
                    <div class="row">
                        <div class="col-12 col-sm-7 col-md-7 col-lg-7 col-xl-7 divEquip">
                            <p class="mb-0">
                                • All our trainers are well trained by master coach Rockie and has 5+ 
                        years of experience with International ACE & INFS certification 
                            </p>
                            <p class="mb-0">• We have 6 Male Trainers & 2 female trainers and 2 zumba instructors </p>
                            <p class="mb-0">• You can experience the high level professionalism over Rocks Fitness which is part of our motto </p>
                        </div>
                        <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5 divEquip">
                            <marquee direction="left">
                                <img class="trainerimg" src="../../Images/About/Trainer1.svg" />
                                <img class="trainerimg" src="../../Images/About/Trainer1.svg" />
                                <img class="trainerimg" src="../../Images/About/Trainer1.svg" />
                                <img class="trainerimg" src="../../Images/About/Trainer1.svg" />
                                <img class="trainerimg" src="../../Images/About/Trainer1.svg" />
                            </marquee>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row mt-5">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                <div class="divEquipmnet">
                    <label class="EquipHead">Why Join Rocks Fitness</label>
                    <p class="mb-0">
                        • Every individuals who join gets the Get started plan with In Body Test
                    </p>
                    <p class="mb-0">• Based on your Goal & Lifestyle our Dieticians help to design a customized Diet plan </p>
                    <p class="mb-0">
                        • people who have Health issues/Injuries / pains goes through the process of Assessment , 
                        later they involved safely into Workouts
                    </p>
                    <p class="mb-0">• Each individuals get the Customized workout plans </p>
                    <p class="mb-0">• 2 months once we re access & update your plans for Constant Results </p>
                    <p class="mb-0">
                        • Because of this ,there is no way of turning back & we give a utmost attention and 
                        care for individuals to transform their lives
                    </p>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mt-5 mb-5">
                <div class="text-center">
                    <label class="finalHead">FITNESS-FAITH-FULFILMENT</label>
                </div>
            </div>

        </div>

        <link href="../Home/Home_Footer.css" rel="stylesheet" />
        <div class="container-fluid Footer">
            <div class="row">
                <div class="col-12 FooterBg">
                    <div class="col-10 mx-auto FooterSpacing">
                        <div class="row">
                            <%-- About --%>
                            <div class="col-12 col-sm-12 col-md-12 col-lg-4 col-xl-4">
                                <%-- About --%>
                                <%--    <div class="About-Container">
                                    <div class="Main-Heading">
                                        <div class="Desc_1">
                                            <img src="../../Images/Footer/about.svg" class="ImgIconCommon" alt="About" />
                                            <span>about</span>
                                        </div>
                                    </div>
                                    <div class="Address">
                                        <div class="Desc_1">
                                            <img src="../../Images/Footer/location.svg" class="ImgIconCommon" alt="Location" />
                                            <span>Basthi Road, Indira Gandhi Nagar, Sultanpet, Hosur, Tamil Nadu 635109
                                            </span>
                                        </div>
                                        <div class="Desc_2">
                                            <img src="../../Images/Footer/location.svg" class="ImgIconCommon" alt="Location" />
                                            <span>Shree Yellama Devi complex, Kodathi Gate, 114/2, Sarjapur - Marathahalli Rd, Carmelaram, post, Bengaluru, Karnataka 560035
                                            </span>
                                        </div>
                                    </div>
                                    <div class="MobileNo">
                                        <img src="../../Images/Footer/Contact.svg" class="ImgIconCommon ImgContact" alt="Contanct" />
                                        <span>96777 85755</span>
                                    </div>
                                    <div class="EmailAddress">
                                        <img src="../../Images/Footer/Gmail.svg" class="ImgIconCommon ImgEmail" alt="Email" />
                                        <span>write@rocksfitnessguru.com</span>
                                    </div>
                                   <%-- <div class="SocialMedia-Icon">
                                        <a href="#">
                                            <img src="../../Images/Footer/Facebook.svg" class="ImgSocialIconCommon ImgFacebook" alt="Facebook" />
                                        </a><a href="#">
                                            <img src="../../Images/Footer/Whatsapp.svg" class="ImgSocialIconCommon ImgWhatsapp" alt="Whatsapp" />
                                        </a><a href="#">
                                            <img src="../../Images/Footer/Youtube.svg" class="ImgSocialIconCommon ImgYoutube" alt="Youtube" />
                                        </a>
                                    </div>--%>
                                <%-- <div class="SocialMedia-Icon">
                                     <asp:LinkButton ID="lnkfbbutton" runat="server"  OnClick="footerfb_Click"  Font-Underline="false" >
                                                <asp:Label ID="lblfb" runat="server" Visible="false" Text='<%# Bind("link") %>' class="ImgCmnInstStory"></asp:Label>
                                                <img src='<%# Bind("icons") %>' id="footerfb" runat="server" class="ImgSocialIconCommon" alt="Facebook" />
                                            </asp:LinkButton>    
                                    <asp:LinkButton ID="lnkytbutton" runat="server"  OnClick="footeryt_Click"  Font-Underline="false" >
                                                <asp:Label ID="lbyt" runat="server" Visible="false" Text='<%# Bind("link") %>' class="ImgCmnInstStory"></asp:Label>
                                                <img src='<%# Bind("icons") %>' id="footeryt" runat="server" class="ImgSocialIconCommon" alt="Facebook" />
                                            </asp:LinkButton>  
                                </div>
                                </div>--%>
                                <div class="About-Container">
                                    <div class="Main-Heading">
                                        <div class="Desc_1">
                                            <img src="../../Images/Footer/about.svg" class="ImgIconCommon" alt="About" />
                                            <span>about</span>
                                        </div>
                                    </div>
                                    <div class="Address">
                                        <div class="Desc_1">
                                            <img src="../../Images/Footer/location.svg" class="ImgIconCommon" alt="Location" />
                                            <span id="FooterStreet" runat="server">Basthi Road, Indira Gandhi Nagar, Sultanpet, Hosur, Tamil Nadu 635109
                                            </span>
                                        </div>
                                    </div>
                                    <div class="MobileNo">
                                        <img src="../../Images/Footer/Contact.svg" class="ImgIconCommon ImgContact" alt="Contanct" />
                                        <span id="FooterMobile" runat="server">96777 85755</span>
                                    </div>
                                    <div class="EmailAddress">
                                        <img src="../../Images/Footer/Gmail.svg" class="ImgIconCommon ImgEmail" alt="Email" />
                                        <span id="Footermail" runat="server">write@rocksfitnessguru.com</span>
                                    </div>
                                    <div class="SocialMedia-Icon">
                                        <asp:LinkButton ID="lnkfbbutton" runat="server" OnClick="footerfb_Click" Font-Underline="false">
                                            <asp:Label ID="lblfb" runat="server" Visible="false" Text='<%# Bind("link") %>' class="ImgCmnInstStory"></asp:Label>
                                            <img src='<%# Bind("icons") %>' id="footerfb" runat="server" class="ImgSocialIconCommon" alt="Facebook" />
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkytbutton" runat="server" OnClick="footeryt_Click" Font-Underline="false">
                                            <asp:Label ID="lbyt" runat="server" Visible="false" Text='<%# Bind("link") %>' class="ImgCmnInstStory"></asp:Label>
                                            <img src='<%# Bind("icons") %>' id="footeryt" runat="server" class="ImgSocialIconCommon" alt="Facebook" />
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <%-- LatestNews --%>
                            <div class="col-12 col-sm-12 col-md-12 col-lg-4 col-xl-4">
                                <div class="LatestNews-Container">
                                    <div class="Main-Heading">
                                        <div class="Desc_1">
                                            <img src="../../Images/Footer/news.svg" class="ImgIconCommon" alt="news" />
                                            <span>latest news</span>
                                        </div>
                                    </div>
                                    <asp:DataList ID="dtlSub" runat="server" RepeatDirection="Vertical">
                                        <ItemTemplate>
                                            <div class="LatestNews">
                                                <img src='<%# Bind("icons") %>' id="latestnewsimage" runat="server" class="ImgCommonNews" alt="news" />
                                                <asp:Label ID="lbllatestdetails" runat="server" Text='<%# Bind("description") %>' class="Desc_1"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>

                                    <%--     <div class="LatestNews">
                                        <img src="../../Images/Footer/Tarun.svg" class="ImgCommonNews" alt="news" />
                                        <span class="Desc_1">Our Client Tarun Reduce <b>2kgs</b> in <b>6 Days</b>
                                        </span>
                                    </div>
                                    <div class="LatestNews">
                                        <img src="../../Images/Footer/Tarun.svg" class="ImgCommonNews" alt="news" />
                                        <span class="Desc_1">Our Client Tarun Reduce <b>2kgs</b> in <b>6 Days</b>
                                        </span>
                                    </div>--%>
                                </div>
                            </div>
                            <%-- Instagram --%>
                            <div class="col-12 col-sm-12 col-md-12 col-lg-4 col-xl-4">
                                <div class="Instagram-Container">
                                    <div class="Main-Heading">
                                        <div class="Desc_1">
                                            <img src="../../Images/Footer/insta.svg" class="ImgIconCommon" alt="insta" />
                                            <span>instagram</span>
                                        </div>
                                    </div>
                                    <div class="Insta-Stories">
                                        <asp:DataList ID="dtlinsta" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblinstalink" runat="server" OnClick="lblSubscr_Click" Font-Underline="false">
                                                    <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Bind("link") %>' class="ImgCmnInstStory"></asp:Label>
                                                    <img src='<%# Bind("icons") %>' id="Instaimages" runat="server" class="ImgCmnInstStory" alt="Insta Story" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <%--  <div class="Insta-Stories">
                                        <a href="https://www.instagram.com/rockie_beast/" target="_blank">
                                            <img src="../../Images/Footer/InstaStory_1.png" class="ImgCmnInstStory" alt="Insta Story" />
                                        </a><a href="https://www.instagram.com/rockie_beast/" target="_blank">
                                            <img src="../../Images/Footer/InstaStory_2.png" class="ImgCmnInstStory" alt="Insta Story" />
                                        </a><a href="https://www.instagram.com/rockie_beast/" target="_blank">
                                            <img src="../../Images/Footer/InstaStory_3.png" class="ImgCmnInstStory" alt="Insta Story" />
                                        </a><a href="https://www.instagram.com/rockie_beast/" target="_blank">
                                            <img src="../../Images/Footer/InstaStory_4.png" class="ImgCmnInstStory" alt="Insta Story" />
                                        </a>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12">
                        <%-- Privacy Policy --%>
                        <div class="PrivacyPolicy-Container">
                            <div class="PrivacyPolicies">
                                <div class="DescCommon Desc_1">
                                    <a href="#" onclick="TSClick()" class="CommonLinkPrivacy">User Agreement</a>
                                </div>
                                <div class="DescCommon Desc_2">
                                    <a href="#" onclick="PClick()" class="CommonLinkPrivacy">Privacy Policy</a>
                                </div>
                                <div class="DescCommon Desc_3">
                                    <a href="#" onclick="FAQClick()" class="CommonLinkPrivacy">FAQ</a>
                                </div>
                            </div>

                            <div class="CopyRightsAndDesign_Bs">
                                <div class="CopyRight">
                                    <span>© 2023 Rocks Fitness Guru, All Rights Reserved</span>
                                </div>
                                <div class="DesignedBy">
                                    <img class="ImgDesignedBy" src="../../Images/Footer/designby.svg" alt="designby" />
                                    <span>Designed by</span>
                                    <a href="https://prematix.com/" traget="_blank" class="PrematixLink">@Prematix</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>

        function C1() {
            document.getElementById('imgC1').classList.add("C1");
            document.getElementById('CCloseBtn').classList.remove('d-none')

        }
        function C2() {
            document.getElementById('imgC2').classList.add("C1");
            document.getElementById('CCloseBtn').classList.remove('d-none')

        }
        function C3() {
            document.getElementById('imgC3').classList.add("C1");
            document.getElementById('CCloseBtn').classList.remove('d-none')

        }
        function C4() {
            document.getElementById('imgC4').classList.add("C1");
            document.getElementById('CCloseBtn').classList.remove('d-none')

        }
        function C5() {
            document.getElementById('imgC5').classList.add("C1");
            document.getElementById('CCloseBtn').classList.remove('d-none')

        }
        function C6() {
            document.getElementById('imgC6').classList.add("C1");
            document.getElementById('CCloseBtn').classList.remove('d-none')

        }
        function C7() {
            document.getElementById('imgC7').classList.add("C1");
            document.getElementById('CCloseBtn').classList.remove('d-none')

        }
        function C8() {
            document.getElementById('imgC8').classList.add("C1");
            document.getElementById('CCloseBtn').classList.remove('d-none')

        }


        function CClose() {
            document.getElementsByClassName('C1')[0].classList.remove("C1");
            document.getElementById('CCloseBtn').classList.add('d-none')


        }
    </script>
<%--    <script src='https://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>--%>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js" 
        integrity="sha512-bLT0Qm9VnAYZDflyKcBaQ2gg0hSYNQrJ8RilYldYQ1FxQYoCLtUjuuRuZo+fjqhx/qtq/1itJ0C2ejDxltZVFg=="
        crossorigin="anonymous"></script>
    <script>
        (function ($) {
            "use strict";

            var bindToClass = 'carousel',
                containerWidth = 0,
                scrollWidth = 0,
                posFromLeft = 0,
                stripePos = 0,
                animated = null,
                $slide, $carousel, el, $el, ratio, scrollPos, nextMore, prevMore, pos, padding;

            function calc(e) {
                $el = $(this).find(' > .wrap');
                el = $el[0];
                $carousel = $el.parent();
                $slide = $el.prev('.slide');

                nextMore = prevMore = false;

                containerWidth = el.clientWidth;
                scrollWidth = el.scrollWidth;
                padding = 0.2 * containerWidth;
                posFromLeft = $el.offset().left;
                stripePos = e.pageX - padding - posFromLeft;
                pos = stripePos / (containerWidth - padding * 2);
                scrollPos = (scrollWidth - containerWidth) * pos;

                if (scrollPos < 0)
                    scrollPos = 0;
                if (scrollPos > (scrollWidth - containerWidth))
                    scrollPos = scrollWidth - containerWidth;

                $el.animate({ scrollLeft: scrollPos }, 200, 'swing');

                if ($slide.length)
                    $slide.css({
                        width: (containerWidth / scrollWidth) * 100 + '%',
                        left: (scrollPos / scrollWidth) * 100 + '%'
                    });

                clearTimeout(animated);
                animated = setTimeout(function () {
                    animated = null;
                }, 200);

                return this;
            }

            function move(e) {
                if (animated) return;

                ratio = scrollWidth / containerWidth;
                stripePos = e.pageX - padding - posFromLeft;

                if (stripePos < 0)
                    stripePos = 0;

                pos = stripePos / (containerWidth - padding * 2);

                scrollPos = (scrollWidth - containerWidth) * pos;

                el.scrollLeft = scrollPos;
                if ($slide[0] && scrollPos < (scrollWidth - containerWidth))
                    $slide[0].style.left = (scrollPos / scrollWidth) * 100 + '%';

                prevMore = el.scrollLeft > 0;
                nextMore = el.scrollLeft < (scrollWidth - containerWidth);

                $carousel.toggleClass('left', prevMore);
                $carousel.toggleClass('right', nextMore);
            }

            $.fn.carousel = function (options) {
                $(document)
                    .on('mouseenter.carousel', '.' + bindToClass, calc)
                    .on('mousemove.carousel', '.' + bindToClass, move);
            };

            $.fn.carousel();

        })(jQuery);
    </script>

    <%--FAQ--%>
    <link href="../Home/Home_FAQ.css" rel="stylesheet" />
    <div id="DivUserAgreement" runat="server" class="DisplyCard" style="display: none">
        <div class="row DisplyCardPostion divresponsive">
            <div class="divLogo text-start">
                <img class="logoImg" src="../../Images/FAQ/faqLogo.svg" />
                <label class="TSHead">Terms & Conditions</label>
                <linkbutton onclick="CloseClickTS()" class="FAQCloseBtn">X</linkbutton>
            </div>
            <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">
                <div class="UserHead text-start">
                    1.TERMS AND CONDITIONS
                </div>
                <p class="UserText text-start">
                    This document is an electronic record in terms of Information Technology Act, 
                    2000 and published in accordance with the provisions of Rule 4 of the Information 
                    Technology (Reasonable security practices and procedures and sensitive personal 
                    data or information) Rules, 2011 that require publishing the Rules and Regulations, 
                    Privacy Policy and Terms and Conditions for access or usage of Platform through 
                    Rocks Fitness Mobile Application.
                </p>
                <div class="UserHead text-start">
                    2.AGREEMENT TO TERMS
                </div>
                <p class="UserText text-start">
                    User Agreement: These Terms and Conditions constitute a legally binding agreement made between you,
                    whether personally or on behalf of an entity ( “user” or “client”) and Rocks Fitness , concerning your 
                    access to and use of the Mobile Application as well as any other media form, media channel, mobile application
                    related, linked, or otherwise connected there to (collectively, the “Mobile Application”).You acknowledge and agree
                    that the Subsidiaries and Affiliates will be entitled to provide the Services to you under the terms of this Agreement. 
                    You agree that by accessing the Mobile Application, you have read, understood, and agree to be bound by all of these Terms and Conditions. 
                    IF YOU DO NOT AGREE WITH ALL OR ANY OF THESE TERMS AND CONDITIONS, THEN YOU ARE EXPRESSLY PROHIBITED FROM USING THE MOBILE APPLICATION 
                    AND YOU MUST DISCONTINUE THEIR USE IMMEDIATELY.
                    Changes to Terms & Conditions: Supplemental terms and conditions or documents that may be posted on the Mobile 
                    Application from time to time are hereby expressly incorporated herein by reference. We reserve the right, in our sole discretion, 
                    to make changes or modifications to these Terms and Conditions at any time and for any reason. You will be subject to, 
                    and will be deemed to have been made aware of and to have accepted, the changes in any revised Terms and Conditions by your 
                    continued use of the Mobile Application or using any of the Services after the date such revised Terms and Conditions become effective. 
                    The information provided on the Mobile Application is not intended for distribution to or use by any person or entity in any jurisdiction or 
                    country where such distribution or use would be contrary to law or regulation or which would subject us to any registration requirement within
                    such jurisdiction or country. Accordingly, those persons who choose to access the Mobile Application from other locations do so on their own 
                    initiative and are solely responsible for compliance with local laws, if and to the extent local laws are applicable.
                </p>
            </div>
            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                <img class="imgTC" src="../../Images/FAQ/TCimg.svg" />
            </div>
        </div>
    </div>
    <div id="DivPrivacy" runat="server" class="DisplyCard" style="display: none">
        <div class="row DisplyCardPostion divresponsive">
            <div class="divLogo text-start">
                <img class="logoImg" src="../../Images/FAQ/faqLogo.svg" />
                <label class="TSHead">Privacy & Policy</label>
                <linkbutton onclick="CloseClickP()" class="FAQCloseBtn">X</linkbutton>
            </div>
            <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">
                <p class="UserText text-start">
                    • We care about data privacy and security. By using the Mobile Application, you agree to be bound by our Privacy Policy, 
                    which is incorporated into these Terms and Conditions.
                    <br />
                    <br />
                    • If we receive actual knowledge that anyone under the age of 18 has provided personal information to us without the requisite
                    and verifiable parental consent, we will delete that information from the Mobile Application as quickly as is reasonably practicable.
                    <br />
                    <br />
                    • When you visit our mobile application, and use our services, you trust us with your personal information. We take your privacy very seriously.
                    <br />
                    <br />
                    • In this privacy policy, we seek to explain to you in the clearest way possible what information we collect, how we use it and what rights you 
                    have in relation to it.
                    <br />
                    <br />
                    • We hope you take some time to read through it carefully, as it is important.
                    <br />
                    <br />
                    • If there are any terms in this privacy policy that you do not agree with, please discontinue use of our Apps and our services and/or you may 
                    disagree to provide any further information to us.
                    <br />
                    <br />
                    • We collect certain personal information necessary to provide you services, when you create an account with us.
                    Some information – such as IP address and/or browser and device characteristics – is collected automatically when you 
                    visit our Services or Apps.We may collect information regarding your geo-location, mobile device, push notifications, when you use our apps.
                    <br />
                    <br />
                    • If you choose to register or log in to our services using a social media account, we may have access to certain information about you.
                    <br />
                    <br />
                    • We keep your information for as long as necessary to fulfill the purposes outlined in this privacy policy unless otherwise required by law.
                    <br />
                    <br />
                    • We aim to protect your personal information through a system of organizational and technical security measures.
                </p>

            </div>
            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                <img class="imgTC" src="../../Images/FAQ/Pimg.svg" />
            </div>
        </div>
    </div>
    <div id="DivFAQ" runat="server" class="DisplyCard" style="display: none">
        <div class="row DisplyCardPostion divresponsive">
            <div class="divLogo text-start">
                <img class="logoImg" src="../../Images/FAQ/faqLogo.svg" />
                <label class="TSHead">FAQ</label>
                <linkbutton onclick="CloseClickFAQ()" class="FAQCloseBtnFAQ">X</linkbutton>
            </div>
            <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">
                <div class="faq-container">
                    <div class="faq">
                        <div class="question text-start">
                            <h5>How to Logging in ?
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                • Registered users can only use our application. so Please, First need to register through our app
                                <br />
                                • Next enter your phone number to verify the phone number, Fitness will send a one-time password to user
                                <br />
                                • Once you verified your phone number Then, you successfully logged in (or) you can directly login with google.
                                <br />
                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>How to book ?
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                • Booking available in both online and offline(Direct visit).   
                                <br />
                                • First you need to choose which plan you want to book.
                                <br />
                                • Next you select the payment method. After completing the payment you will see your bookings on my bookings.
                                <br />
                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>How to buy subscription ?
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                • First you need to choose which subscription plan you want to buy.   
                                <br />
                                • Next you select the payment method.
                                <br />
                                • After completing the payment you will see your bookings on my bookings.
                                <br />
                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>How to update my profile?
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                • you can update your personal details in edit profile option                              
                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>What payment options do I have?
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                • UPI                              
                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>WHY JOIN US ?
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                • You have probably tried various strategies to lose weight, consulted doctors & took weight
                                loss pills and yet you haven't seen any changes or been disappointed.
                                <br />
                                • understand this and provide you with customised excercise and diet routines to push yourself & make you to achieve your healthy goal.
                                  We are committed to make your fitness dreams come true & your life a healthy paradise.
                                <br />
                                •  We are here for you – any age, any group, any healthy issue & specialized for women & body transformation.
                                <br />
                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>Why fitness?
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                • A healthier state of mind   
                                <br />
                                • Clear thought & better mind    
                                <br />
                                • More Energy               
                                <br />
                                • Feel Relaxed   
                                <br />
                                • Sleep Better  
                                <br />
                                • Strong Bones, Muscles & joints  
                                <br />
                                • Reduce risk of BP, Diabetics & heart problems
                                <br />
                                • Fast recovery    
                                <br />
                                • Achieve your life goal                        
                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>The ten major genetic variables that affect fat loss, 
                                <br />
                                muscle growth, strength, and athletic ability
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                • The great news is that fat loss and fitness are not determined by genetics alone.
                                The way your body looks today is the result of genetics, behavior, and environment all put together.  
                                <br />
                                Genetic Variables
                                <br />
                                • Basal metabolic rate    
                                <br />
                                •Number of fat cells               
                                <br />
                                • Number of muscle fibers   
                                <br />
                                • Muscle fiber type  
                                <br />
                                • Muscle insertions  
                                <br />
                                • Limb length
                                <br />
                                • Joint size    
                                <br />
                                • Digestive differences
                                <br />
                                • Food allergies and insensitivities 
                                <br />
                                • Carbohydrate tolerance          
                                <br />
                                <br />
                                Dedication, discipline, and hard work can take you so far, 
                                it can appear as if you’ve shattered your genetic “limits.”
                                The reality is that most people never come close to fulfilling their full potential.
                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>How Do I Get a Flat Stomach?
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                • The two types of exercise can help - strength training and cardiovascular exercise.<br />
                                • The abdominals are just like any other muscle group, for their definition to become visible, 
                                they must grow larger and the fat that lies over them must decrease. What makes the definition 
                                of the abdominals so difficult to see is that they are situated in the area of the body that contains the most fat.<br />
                                • Strength training the abdominals is only half the story.
                                My clients will get a flat stomach only when I combine strength 
                                training with cardiovascular exercise to get rid of the fat.<br />
                                • Most clients do not do nearly enough cardiovascular exercise to decrease their body 
                                fat percentage to a point where they would see their abdominals.<br />
                                • Even when the aerobic exercise stimulus is adequate, the role of diet must not be underestimated. 
                                All people with a flat stomach or six-pack have a very low percentage of body fat.
                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>What Is the Best Way to Lose Fat ?
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                The simple (and complex) answer is that there is no “best way” to lose fat.
                                Each client will respond differently to a training program. However, 
                                there are some I apply when designing my clients’ programs.
                                <br />
                                • Activities that incorporate many muscle groups and are weight bearing, they use more calories 
                                per minute and are therefore better suited for fat loss.<br />
                                • The abdominals are just like any other muscle group, for their definition to become visible, 
                                they must grow larger and the fat that lies over them must decrease. What makes the definition 
                                of the abdominals so difficult to see is that they are situated in the area of the body that contains the most fat.<br />
                                • It is often assumed that low-intensity exercise is best for burning fat.<br />
                                • To decrease body fat percentage, my clients do not necessarily have to use fat during exercise. 
                                Much of the fat from adipose is lost in the hours following exercise. 
                                Because clients can perform a greater intensity of work if the work is broken up with periods of rest, 
                                interval training is a great way to perform high-intensity work and help decrease body fat percentage.<br />
                                • Both strength training and endurance exercise have been shown to decrease body fat percentage.
                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>Women must train differently from men
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                • Most women who say they want to be “toned” mean that they want to get fitter and firmer without getting bigger.
                                Guess what? The best and fastest way to achieve the “fitter and firmer” 
                                look women want is the same way that men achieve the “muscular” look they want: with weight training.
                                And without it, a woman will never get much stronger.   
                                <br />
                                • A muscle either gets stronger and more developed or it doesn’t. 
                                There’s no such thing as “toning” or gender-specific exercise; 
                                these are just the perceptions created by the fitness industry. 
                                I don’t think that’s a bad thing if it gets more women involved.
                                What I’m saying is that if women squat, lunge, row, press, and deadlift, just like men,
                                they’ll be rewarded with many times greater results than if they pursue some kind of 
                                dainty “toning” exercises with three-pound pink dumbbells or follow exercise programs that don’t use resistance at all.
                              
                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>What causes us to OVEREAT?
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                •Mood swing   
                                <br />
                                • Depression
                                <br />
                                • External influence
                                <br />
                                • Too much restriction on food
                                <br />
                                • Due to misjudgment
                                <br />

                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>Should I Do Cardio First or Weight Training First?
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                It depends on the client’s goals. Many personal trainers think that performing strength 
                                training before cardiovascular exercise will augment the amount of fat used during the 
                                cardio workout because the strength training will deplete the muscles’ 
                                store of carbohydrates (glycogen).
                                <br />
                                •However, strength training is not likely to deplete glycogen stores, 
                                because a lot of the workout time is spent resting between sets and exercises.
                                Even if the strength workout were long and intense enough to accomplish this task,
                                exercising in a glycogen-depleted state has many negative consequences, 
                                including an increase in acidic compounds produced in response to low carbohydrate levels,
                                low blood insulin, hypoglycemia, increased amino acid (protein) metabolism, increased blood 
                                and muscle ammonia and a strong perception of fatigue. 
                                <br />
                                • Currently, no research shows that strength training immediately before a cardio workout 
                                increases the amount of fat used during the cardio workout, or vice versa. Most likely, 
                                the intensity of the activity, not the mode of exercise, determines the “fuel”—either fat,
                                carbohydrate or protein—that is used.
                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>When will I see results?
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                •This depends on what kind of results we’re talking about. You may have more energy within a matter of hours or a few days.  
                                <br />
                                • Fat loss or weight loss will occur as quickly a few days to a few weeks depending on your lifestyle choices.
                                <br />
                                • External influence
                                <br />
                                • To see increased strength and actual muscle size it will take 6-8 weeks.
                                <br />
                                • For an overall feeling of well-being and a sense of better strength, it could be a matter of just a few weeks.                        

                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>If you lift weights, you’ll lose flexibility and get muscle-bound.
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                •The surest way to decrease your flexibility is to sit on your butt all day long doing nothing. 
                                <br />
                                • Weight training can actually increase your flexibility if you perform the exercises through the full range of motion. 
                                I’ve seen male bodybuilders weighing 120 kgs of solid muscle do full splits as part of their posing routines.
                                <br />
                                • As for the women, watch a professional fitness show like the Fitness Olympia. You’ll see some of the most
                                flexible athletes in the world,
                                even though they train with weights every bit as hard as the men.
                                <br />
                                • If increasing flexibility is one of your fitness goals, then devote some time for stretching at the end of 
                                your lifting sessions and emphasize the tightest areas.
                                <br />
                                • An easy way to fit stretching into your routine with no extra time commitment is to stretch in between sets. 
                                I add yoga into my clients weekly plan to gain more flexibility. 
                                Just remember that yoga is a good adjunct to weight training, it’s not a substitute for it.
                                <br />

                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>You should lose all the fat first, then start weight training later.
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                •People who are extremely overweight may need to focus on nutrition at first if they’re not very mobile yet.
                                <br />
                                • But if you’re physically able to exercise safely, you’ll get amazing benefits from starting a weight-training 
                                program,even if you still have a lot of fat to lose. Almost anyone can start with walking for
                                cardio and basic lifts for strength, and it’s never too soon to start developing good habits.
                                <br />
                                • Yes, you can lose weight with diet and cardio or even diet alone, 
                                but many people who do that find they’re not as happy with their bodies as they thought they’d be.
                                They fit into smaller clothes, but they still don’t want to be seen out of clothes.
                                They look soft and unathletic. It’s never too late to pick up the weights, 
                                but wouldn’t it be better to start training from day one and finish with a stronger,
                                harder, and more athletic body?

                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>How do you know that your diet is working?
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                After 2 – 3 weeks of sticking to your diet, you should assess how it’s going.
                                Weight loss isn’t the only criterion to consider when deciding if your diet is right or wrong.
                                However, you should judge your progress based on the following criteria:
                                •Your weight - did it go down, up, or stay the same?
                                <br />
                                • Your clothes - do they feel looser, tighter, or the same?
                                <br />
                                • The mirror - do you look thinner, fatter, or the same?
                                Any of the above is up -> time to consult your dietian
                                <br />
                                • Your energy levels - do you feel energized, tired, or somewhere in between?
                                <br />
                                • Your strength - is it going up, down, or staying about the same?
                                <br />
                                • Your sleep - are you exhausted by the end of the night, do you have trouble 

                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>Tips to have a good night sleep
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                •Warm water bath 2 hours prior to sleep
                                <br />
                                • Get more sunlight
                                <br />
                                • Avoid digital screen for 90 mins before sleep
                                <br />
                                • Avoid caffeine before 6 – 8 hours of sleep
                                <br />
                                • Reduce irregular or long day night naps
                                <br />
                                • Get into regular sleep / wake cycle, specially over weekends
                                <br />
                                • Lavender & Magnesium can help with relaxation & sleep quality
                                <br />
                                • Avoid alcohol close to sleep
                                <br />
                                • Avoid large meal before bed, instead take 2 hours before
                                <br />
                                • Exercise regularly
                              

                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>What are the Member Benefits Included
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                Equipment
                                <br />
                                • Treadmills
                                <br />
                                • Exercise Cycles
                                <br />
                                • Stair Climbers    
                                <br />
                                • Rowing Machines
                                <br />
                                • Free Weights
                                <br />
                                • Racks
                                <br />
                                • Crossfit
                                <br />
                                • Cable Crossovers
                                <br />
                                • Kettlebells
                                <br />
                                • Lateral X Trainers
                                <br />
                                • Amt Crosstrainers
                                <br />
                                Lifestyle
                                   • Personal Training
                                <br />
                                • Nutrition Plans
                                <br />
                                • Exercise Cycles
                                <br />
                                • Detox Program
                                <br />
                                • Adaptive Motion Trainers
                                <br />
                                • Spinning Cycles & Classes
                                <br />
                                • Zumba Classes
                                <br />
                                • Cardio Classes
                                <br />
                                • Body Conditioning Classes
                                <br />
                                • Yoga Classes
                                <br />
                                Services
                                 • 24-Hour Online Access
                                <br />
                                • Private Restrooms
                                <br />
                                • Private Showers
                                <br />
                                • Health Plan Discounts
                                <br />
                                • Wellness Programs
                                <br />
                                • Cardio Tvs
                                <br />
                                • Hdtvs
                                <br />
                                • Women Specialized Facilities
                                <br />
                                • Hygiene
                                <br />
                                • Video Consultations
                                <br />
                                • Locker Facility
                                <br />
                                • Music Ambience
                                <br />
                                • Parking Facility (2 & 4 Wheelers)
                                <br />
                            </p>
                        </div>
                        <div class="question text-start">
                            <h5>Cancellation and Reschedule is there for booking
                                <img src="../../Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                        </div>
                        <div class="answer">
                            <p class="UserText text-start">
                                No
                            </p>
                        </div>
                    </div>
                </div>

            </div>
            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                <img class="imgFAQ" src="../../Images/FAQ/FAQimg.svg" />
            </div>
        </div>
    </div>
    <script>
        function TSClick() {
            $('#<%= DivUserAgreement.ClientID %>').css("display", "block");

        }
        function CloseClickTS() {
            $('#<%= DivUserAgreement.ClientID %>').css("display", "none");

        }

        function PClick() {
            $('#<%= DivPrivacy.ClientID %>').css("display", "block");

        }
        function CloseClickP() {
            $('#<%= DivPrivacy.ClientID %>').css("display", "none");

        }

        function FAQClick() {
            $('#<%= DivFAQ.ClientID %>').css("display", "block");

        }
        function CloseClickFAQ() {
            $('#<%= DivFAQ.ClientID %>').css("display", "none");

        }
    </script>
    <script>
        var questions = document.querySelectorAll(".question");

        questions.forEach(function (question) {
            question.addEventListener("click", function () {
                this.classList.toggle("active");
                var answer = this.nextElementSibling;
                if (answer.style.display === "block") {
                    answer.style.display = "none";
                } else {
                    answer.style.display = "block";
                }
            });
        });
    </script>
    <asp:HiddenField ID="hfYoutubelink" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfFblink" ClientIDMode="Static" runat="server" />
</asp:Content>
