<%@ Page Title="Home" Language="C#" MasterPageFile="~/Fitness.Master" AutoEventWireup="true" CodeFile="Home.aspx.cs"
    Inherits="Pages_Home_HomeNew" EnableEventValidation="true" %>

<%@ MasterType VirtualPath="~/Fitness.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FitnessContent" runat="Server">
    <%-- Home Page Scipts --%>
    <asp:Button ID="btn_Home_BranchChange" ClientIDMode="Static" CssClass="d-none" OnClick="btnBranchChange_Click" runat="server" />
    <link href="Pages/Home/Certificate.css" rel="stylesheet" />

    <link href="Pages/Home/Home.css" rel="stylesheet" />
    <style>
        /*TrainerDetails*/
        .DivHead {
            margin-left: 10rem;
            margin-bottom: 2rem;
        }

        .lblAccHead {
            font-size: 2rem;
            color: black;
            font-weight: 500;
        }

        .carouselDiv {
            margin-left: 5rem;
        }

        .CClose {
            color: black;
            font-size: 2rem;
            position: fixed;
            z-index: 3;
            top: 10%;
            left: 83%;
            font-weight: 500;
        }

        .lblC1Text {
            font-size: 1.6rem;
            position: absolute;
            margin-top: 7rem;
            color: black;
            font-weight: 500;
            margin-left: -19rem;
        }

        .lblC1Text1 {
            font-size: 1.6rem;
            position: absolute;
            margin-top: 13rem;
            color: black;
            font-weight: 700;
        }

        .DisplyCardPostiontrainerdetails {
            border-width: 0px;
            position: fixed;
            width: 90%;
            height: auto;
            padding: 0px 50px 0px 50px;
            background-color: #ffffff;
            font-size: 40px;
            left: 50%;
            transform: translateX(-50%);
            top: 11%;
            border-radius: 25px;
            padding-top: 2rem;
            padding-bottom: 2rem;
        }

        .C1 {
            position: fixed !important;
            z-index: 2 !important;
            margin-left: 20rem !important;
            opacity: 5;
            top: 15%;
            left: 30%;
            bottom: 0%;
            transform: translateX(-50%);
            background: #000000d6;
            height: 36rem !important;
            transition: all 0.3s ease-in-out !important;
            width: 80% !important;
        }

        .imgC2 {
            margin-left: 15rem !important;
        }

        .carousel {
            display: block;
            /* -webkit-transform: translateZ(0); */
            width: 92%;
            margin: auto;
            font-size: 0;
            border-radius: 6px;
            height: 200px;
            -webkit-overflow-scrolling: touch;
        }

        /*TrainerDetails*/
    </style>

    <%-- Home Page Scipts --%>
    <script defer type="module" src='<%=ResolveUrl("Pages/Home/HomeNew_Main.js") %>'></script>
    <div class="container-fluid containerBg">
        <div class="row DivHome1st">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 px-0">
                <div class="Home1stContainer">
                    <video autoplay loop muted class="HomeVideo">
                        <source src="Images/HomeIndex/PaypreVideo.webm" type="video/mp4">
                    </video>
                    <asp:Label runat="server" class="lblHomed1">
                        <span>don't find time to workout </span><br />
                        make time to workout</asp:Label>
                    <%--<label class="lblHomed2">Get Free Trail Activate Now</asp:label>--%>
                    <%--<button class="btnTryNow">Try Now</button>--%>
                </div>
            </div>
        </div>
    </div>
    <div id="scrollClasses"></div>
    <div id="divClasses" runat="server" class="divClasses reveal fade-bottom">
        <%--<asp:Label runat="server" class="lblSubsSubHead">Classes</asp:Label>--%>
        <%-- <div class="owl-slider">
            <div id="carouselClasses" class="Classes-Container owl-carousel">
                <asp:DataList ID="dtlclassList" RepeatDirection="Horizontal" runat="server">
                    <ItemTemplate>
                        <div class="divClass item">
                            <asp:LinkButton ID="lnkClass" runat="server" OnClick="lnkClass_Click">
                                <asp:ImageButton ID="imgBtnClass" runat="server" class="classimg" OnClick="imgBtnClass_Click" ImageUrl='<%#Bind("imageUrl") %>' />
                                <asp:Label ID="lblcategoryName" runat="server" class="lblClassName" Text='<%#Bind("categoryName") %>'></asp:Label>
                                <asp:Label ID="lblcategoryId" runat="server" class="lblClassName" Visible="false" Text='<%#Bind("categoryId") %>'></asp:Label>
                                <asp:Button ID="btnBuyNow" runat="server" class="btnCBuyNow" OnClick="btnBuyNow_Click" Text="Buy Plan" />
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>--%>
        <label class="lblSubsSubHead">Classes</label>
        <asp:HiddenField ID="hfclassescontainer" ClientIDMode="Static" runat="server" />
        <div class="col-12">
            <div id="Classes-Container" class="row Classes-Container owl-carousel" style="text-align: -webkit-center;">
            </div>
        </div>
    </div>

    <div id="divMeals" runat="server" class="divMeal reveal fade-bottom">
        <asp:Label runat="server" class="lblMealHead">Good & Healthy Meals</asp:Label>
        <asp:HiddenField ID="hfMealplanDescription" ClientIDMode="Static" runat="server" />
        <div class="row">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                <div id="HomeMealplan-Description" class="divMealImg reveal fade-bottom">
                </div>
            </div>
        </div>
    </div>

    <%-- <div class="divTrainers reveal fade-bottom">
        <asp:Label runat="server" class="lbltrainerHead">Our Special Trainers</asp:Label>
        <asp:HiddenField ID="hfTrainerDescription" ClientIDMode="Static" runat="server" />
        <div id="HomeTrainer-Description" class="row">
        </div>
    </div>--%>

    <div id="scrollSubscription"></div>
    <div id="divSubs" runat="server" class="divTrainers reveal fade-bottom">
        <asp:Label runat="server" class="lbltrainerHead">Subscription</asp:Label>
        <link href="Pages/Home/Home_Subscription.css" rel="stylesheet" />
        <asp:HiddenField ID="Hdn_Home_Subscription" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="Hdn_Home_SubscriptionId" ClientIDMode="Static" runat="server" />
        <section id="Section-Subscription" class="container-fluid Subscription text-center">
            <div class="col-12">
                <div id="HomeSubscription-Carousel" class="row subscription-carousel owl-carousel mt-5" style="text-align: -webkit-center;">
                </div>

            </div>
        </section>
        <asp:Button ID="btn_Home_Subscription" ClientIDMode="Static" OnClick="btn_Home_Subscription_Click" runat="server" CssClass="d-none" />
    </div>


    <div id="divTrainer" runat="server" class="divTrainer reveal fade-bottom">
        <asp:Label runat="server" class="lbltrainerHead">Our Trainers List</asp:Label><br />
        <asp:Label runat="server" class="lbltrainerquote">The same voice that says “give up” can also be trained to say “keep going”</asp:Label>
        <%--  <div class="owl-slider">
            <div id="carousel" class="Classes-Container owl-carousel">
                <asp:DataList ID="dtlTrainer" RepeatDirection="Horizontal" runat="server">
                    <ItemTemplate>
                        <div class="divClass item">
                            <asp:Label ID="lbltrainerId" runat="server" class="lblTrainerName d-none" Text='<%#Bind("trainerId") %>'></asp:Label>
                            <asp:Label ID="lblfirstName" runat="server" class="lblTrainerName" Text='<%#Bind("firstName") %>'></asp:Label>
                            <asp:ImageButton ID="imgBtnClass" runat="server" ClientIDMode="Static" class="imgTrainer" OnClick="imgBtnClass_Click1" ImageUrl='<%#Bind("photoLink") %>' />
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>--%>
        <asp:HiddenField ID="hftrainersHomeName" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="hftrainerdetailsBaseUrl" ClientIDMode="Static" runat="server" />
        <div class="col-12">
            <div id="Trainers-Container" class="row Trainers-Container owl-carousel" style="text-align: -webkit-center;">
            </div>
        </div>
    </div>

    <div id="scrollTestimonials"></div>
    <div id="divReview" runat="server" class="divReview reveal fade-bottom">
        <asp:Label runat="server" class="lbltrainerHead">Testimonials</asp:Label>
        <asp:HiddenField ID="hfTestimonialsImage" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="hftestimonialDatas" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="hfTokenTrainers" ClientIDMode="Static" runat="server" />
        <div class="row">
            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                <div class="divReviewImg reveal fade-left">
                    <div id="testimonialimage" clientidmode="Static" runat="server">
                    </div>
                </div>
            </div>
            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                <div id="testimonialcontent" clientidmode="Static" runat="server">
                </div>
            </div>
        </div>
    </div>

        <div class="container-fluid Footer reveal fade-bottom">
        <div class="FooterBg">
            <div class="divFooterLogo">
            </div>
            <div class="row">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 divFooterMap">
                    <div class="mapstyle reveal fade-right" id="MyMapLOC">
                    </div>
                </div>
            </div>
            <div class="row footer1">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                    <div class="divAbout">
                        <img runat="server" src="~/Images/Footer/Contact.svg" class="ImgIconCommon ImgContact" alt="Contanct" />
                        <span id="lblMobileNo" runat="server" class="lblMobileNo"></span>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                    <div class="divAbout">
                        <img src="Images/Footer/Gmail.svg" class="ImgIconCommon ImgEmail" alt="Email" />
                        <span id="lblMailId" runat="server" class="lblMobileNo"></span>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                      <div class="divAbout">
                        <div>
                            <asp:LinkButton ID="lnkfbbutton" runat="server" OnClick="footerfb_Click" Font-Underline="false">
                                <img runat="server" id="footerfb" src="~/Pages/Home/Image/Facebook.svg" class="ImgSocialIconCommon ImgFacebook" alt="Facebook" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkytbutton" runat="server" OnClick="footerYT_Click" Font-Underline="false">
                                <img runat="server" id="footerYT" src="~/Pages/Home/Image/Youtube.svg" class="ImgSocialIconCommon ImgYoutube" alt="Youtube" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkItbutton" runat="server" OnClick="footerIt_Click" Font-Underline="false">
                                <img runat="server" id="footerIt" src="~/Pages/Home/Image/Insta.svg" class="ImgSocialIconCommon ImgYoutube" alt="Youtube" />
                            </asp:LinkButton>
                             <asp:LinkButton ID="lnktwitter" runat="server" OnClick="lnktwitter_Click" Font-Underline="false">
                                <img runat="server" id="footertwitter" src="~/Pages/Home/Image/twitter.svg" class="ImgSocialIconCommon ImgYoutube" alt="Youtube" />
                            </asp:LinkButton>
                             <asp:LinkButton ID="lnklinkedIn" runat="server" OnClick="lnklinkedIn_Click" Font-Underline="false">
                                <img runat="server" id="footerlinkedIn" src="~/Pages/Home/Image/linkedin.svg" class="ImgSocialIconCommon ImgYoutube" alt="Youtube" />
                            </asp:LinkButton>
                        </div>
                    </div>                   
                </div>
            </div>
            <div class="row">
              <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 footer1">                   
                    <div class="CopyRightsAndDesign_Bs">
                        <div class="CopyRight">
                            <span>© 2023 PayPre Fitness, All Rights Reserved</span>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
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
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                   <div class="DesignedBy">
                        <img runat="server" class="ImgDesignedBy" src="~/Images/Footer/designby.svg" alt="designby" />
                        <span>Designed by</span>
                        <a href="https://prematix.com/" traget="_blank" class="PrematixLink">@Prematix Software Solutions Pvt Ltd</a>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <div id="divtrainerdetails" runat="server" class="DisplyCard d-none" clientidmode="Static">
        <asp:HiddenField ID="hftrainerdetails" ClientIDMode="Static" runat="server" />
        <div class="row DisplyCardPostiontrainerdetails divresponsive">
            <div class="divLogo text-start">
                <asp:Label runat="server" class="TSHead">Trainer Details</asp:Label>
                <linkbutton onclick="CloseClickTD()" class="FAQCloseBtn">
                    <i class="fa-solid fa-xmark fa-fade" style="color: #ff0000;"></i></linkbutton>
            </div>
            <div id="trainerdetailscertficates">
                <%-- <div class="row">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-4 pe-0 ps-0 mb-5">
                   
                    <div class="DivHead">
                        <asp:Label runat="server" class="lblAccHead">Certificate of Excellence</asp:Label>
                    </div>
                    <div class="carouselDiv">
                        <div class="carousel right">
                            <div class="slide"></div>
                            

                            </div>

                         
                        </div>
                    </div>

                </div>--%>
            </div>
        </div>
    </div>



    <!-- Owl Carousel CSS -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/OwlCarousel2/2.3.4/assets/owl.carousel.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/OwlCarousel2/2.3.4/assets/owl.theme.default.min.css">

    <!-- jQuery and Owl Carousel JavaScript -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/OwlCarousel2/2.3.4/owl.carousel.min.js"></script>


    <script>
        function reveal() {
            var reveals = document.querySelectorAll(".reveal");

            for (var i = 0; i < reveals.length; i++) {
                var windowHeight = window.innerHeight;
                var elementTop = reveals[i].getBoundingClientRect().top;
                var elementVisible = 150;

                if (elementTop < windowHeight - elementVisible) {
                    reveals[i].classList.add("active");
                } else {
                    reveals[i].classList.remove("active");
                }
            }
        }

        window.addEventListener("scroll", reveal);
    </script>
    <script>

        $('.Classes-Container.owl-carousel').owlCarousel({
            loop: false,
            nav: true,
            mouseDrag: true,
            touchDrag: true,
            dots: true,
            margin: 20,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 2
                },
                900: {
                    items: 3
                },
                1200: {
                    items: 4
                }
            },
            onInitialized: function () {
                document.querySelector(".Classes-Container").querySelector(".owl-prev").style.display = "none";
            },
            center: false,
            onTranslated: function () {
                var currentSlide = this.current();
                var totalSlides = this.items().length;
                var lastpreviousSlides = totalSlides - 2;
                if (currentSlide === 0) {
                    document.querySelector(".Classes-Container").querySelector(".owl-prev").style.display = "none";
                }
                else {
                    document.querySelector(".Classes-Container").querySelector(".owl-prev").style.display = "block";;
                }

                if (currentSlide >= totalSlides - 1) {
                    document.querySelector(".Classes-Container").querySelector(".owl-next").style.display = "none";
                }
                //else if (currentSlide == lastpreviousSlides) {
                //    document.querySelector(".Classes-Container").querySelector(".owl-next").style.display = "none";  
                //}
                else {
                    document.querySelector(".Classes-Container").querySelector(".owl-next").style.display = "block";
                }
            }
        });
        document.querySelector('.owl-prev').innerHTML = `<img src="Images/Testimonials/white-left.svg" />`;
        document.querySelector('.owl-next').innerHTML = `<img src="Images/Testimonials/white-right.svg" class="btnRightLeft" />`;

    </script>



    <%--FAQ--%>
    <link href="Pages/Home/Home_FAQ.css" rel="stylesheet" />
    <div id="DivUserAgreement" runat="server" class="DisplyCard" style="display: none">
        <div class="row DisplyCardPostion divresponsive">
            <div class="divLogo text-start">
                <asp:Image ID="LogoTerms" class="logoImg" runat="server" />
                <asp:Label runat="server" class="TSHead">Terms & Conditions</asp:Label>
                <linkbutton onclick="CloseClickTS()" class="FAQCloseBtn">
                    <i class="fa-solid fa-xmark fa-fade" style="color: #ff0000;"></i></linkbutton>
            </div>
            <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">
                <div class="divTC">
                    <asp:DataList ID="dtlTerms" runat="server" RepeatDirection="Vertical" RepeatColumns="1">
                        <ItemTemplate>
                            <div class="UserHead text-start">
                                <asp:Label ID="lblPrivacypolicy" runat="server" Text='<%# Bind("question") %>'></asp:Label>
                            </div>
                            <p class="UserText text-start">
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("answer") %>'></asp:Label>
                            </p>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                <img class="imgTC" src="Images/FAQ/TCimg.svg" />
            </div>
        </div>
    </div>
    <div id="DivPrivacy" runat="server" class="DisplyCard" style="display: none">
        <div class="row DisplyCardPostion divresponsive">
            <div class="divLogo text-start">
                <asp:Image ID="LogoPrivacy" class="logoImg" runat="server" />
                <asp:Label runat="server" class="TSHead">Privacy & Policy</asp:Label>
                <linkbutton onclick="CloseClickP()" class="FAQCloseBtn">
                    <i class="fa-solid fa-xmark fa-fade" style="color: #ff0000;"></i></linkbutton>
            </div>
            <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">

                <asp:DataList ID="dtlPrivacy" runat="server" RepeatDirection="Vertical" RepeatColumns="1">
                    <ItemTemplate>
                        <p class="UserText text-start">
                            <asp:Label ID="lblPrivacypolicy" runat="server" Text='<%# Bind("answer") %>'></asp:Label>
                        </p>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                <img class="imgTC" src="Images/FAQ/Pimg.svg" />
            </div>
        </div>
    </div>
    <div id="DivFAQ" runat="server" class="DisplyCard" style="display: none">
        <div class="row DisplyCardPostion divresponsive">
            <div class="divLogo text-start">
                <asp:Image ID="LogoFAQ" class="logoImg" runat="server" />
                <asp:Label runat="server" class="TSHead">FAQ</asp:Label>
                <linkbutton onclick="CloseClickFAQ()" class="FAQCloseBtnFAQ">
                    <i class="fa-solid fa-xmark fa-fade" style="color: #ff0000;"></i></linkbutton>
            </div>
            <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">
                <div class="faq-container">
                    <div class="faq">
                        <asp:DataList ID="dtlFAQ" runat="server"
                            OnItemDataBound="dtlFAQ_ItemDataBound">
                            <ItemTemplate>
                                <div class="question text-start">
                                    <h5>
                                        <asp:Label ID="lblQuestion" runat="server" Text='<%# Bind("question") %>'></asp:Label>
                                        <asp:Label ID="lblanswer" runat="server" Text='<%# Bind("answer") %>' Visible="false"></asp:Label>
                                        <img src="Images/FAQ/downarrow.svg" class="FAQdownarrow" /></h5>
                                </div>
                                <div class="answer">
                                    <asp:DataList ID="dtlFA" runat="server">
                                        <ItemTemplate>
                                            <p class="UserText text-start" style="margin-top: 0rem; margin-bottom: 0rem">
                                                •
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("answer") %>'></asp:Label>
                                            </p>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>

            </div>
            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                <img class="imgFAQ" src="Images/FAQ/FAQimg.svg" />
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
        function CloseClickTD() {
            ////////$('#<%= divtrainerdetails.ClientID %>').css("display", "none");
            $('#<%= divtrainerdetails.ClientID %>').addClass('d-none');

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
    <script>
        const ddlBranch = document.getElementById('ddlBranch_Master_Nav');
        const btnOnBranchChange = document.getElementById('btn_Home_BranchChange');

        ddlBranch.onchange = function () {
            btnOnBranchChange.click();
        };
    </script>



    <script async defer src="https://maps.googleapis.com/maps/api/js?libraries=places&key=AIzaSyB56Km4bH3DEKxXLRZBltsTIm3eYgPqt0k&callback=Function.prototype" type="text/javascript"></script>
    <%--    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&libraries=places">  
    </script>--%>
    <asp:HiddenField ID="hfBranchId" runat="server" EnableViewState="true" />
    <asp:HiddenField ID="hfgymOwnerId" runat="server" EnableViewState="true" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css"  />


   <script>

       if (navigator.geolocation) {
           navigator.geolocation.getCurrentPosition(
               function (position) {
                   success(position.coords.latitude, position.coords.longitude)
               },
               function errorCallback(error) {
                   console.log(error)
               }
           );
       } else {

           alert("There is Some Problem on your current browser to get Geo Location!");
       }

       function success(latt, lng) {

           if (document.getElementById('<%=hfBranchId.ClientID %>').value != '') {
                var lat = document.getElementById('<%=hdnLatitude.ClientID %>').value;
                var long = document.getElementById('<%=hdnLongitude.ClientID %>').value;
            }
            else {
                var lat = 12.746499441101124;
                var long = 77.81192796126807;
            }
            $.ajax({
                type: "GET",
                url: '<%= Session["BaseUrl"].ToString() %>' + "branch/GetBranchBasedOnLocation?lattitude=" + lat + "&longitude=" + long + "&radius=20000",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (Response) {
                    var map = new google.maps.Map(document.getElementById('MyMapLOC'), {
                        zoom: 10,
                        center: new google.maps.LatLng(lat, long),
                        mapTypeId: google.maps.MapTypeId.ROADMAP
                    });
                    var customIcon = {
                        path: 'M96 64c0-17.7 14.3-32 32-32h32c17.7 0 32 14.3 32 32V224v64V448c0 17.7-14.3 32-32 32H128c-17.7 0-32-14.3-32-32V384H64c-17.7 0-32-14.3-32-32V288c-17.7 0-32-14.3-32-32s14.3-32 32-32V160c0-17.7 14.3-32 32-32H96V64zm448 0v64h32c17.7 0 32 14.3 32 32v64c17.7 0 32 14.3 32 32s-14.3 32-32 32v64c0 17.7-14.3 32-32 32H544v64c0 17.7-14.3 32-32 32H480c-17.7 0-32-14.3-32-32V288 224 64c0-17.7 14.3-32 32-32h32c17.7 0 32 14.3 32 32zM416 224v64H224V224H416z',
                        fillColor: '#8f1964', // Change the color as needed
                        fillOpacity: 1,
                        strokeWeight: 0,
                        scale: 0.05
                    };

                    var marker = new google.maps.Marker({
                        position: new google.maps.LatLng(lat, long),
                        map: map,
                        title: "Your Location",
                        label: {
                            text: "Your Location",
                            color: "#B50F2B",
                            fontWeight: "bold",
                            fontSize: "16px",

                        },

                        // icon: customIcon
                        icon: {
                            url: "Pages/Home/Image/Location.svg",
                            scaledSize: new google.maps.Size(45, 45),
                            labelOrigin: new google.maps.Point(0, 0)
                        },
                    });

                    var infoWindow = new google.maps.InfoWindow(), marker, i;
                    for (i = 0; i < Response.Response.length; i++) {
                        var position = new google.maps.LatLng(Response.Response[i]["latitude"], Response.Response[i]["longitude"]);


                        marker = new google.maps.Marker({
                            position: position,
                            map: map,
                            title: Response.Response[i]["branchName"],
                            fillColor: '#fff',
                            label: {
                                text: Response.Response[i]["branchName"],
                                color: "#000",
                                fontWeight: "bold",
                                fontSize: "16px",
                            },

                            //icon: customIcon
                            icon: {
                                url: "Pages/Home/Image/Location.svg",
                                scaledSize: new google.maps.Size(45, 45),
                                labelOrigin: new google.maps.Point(0, 0),
                            },
                        });



                        google.maps.event.addListener(marker, 'mouseover', (function (marker, i) {
                            return function () {

                                var mob = Response.Response[i]["primaryMobileNumber"] != "" ? "<h5> Contact No. :" + Response.Response[i]["primaryMobileNumber"] + "</h5>" : "";

                                infoWindow.setContent('<div style="font-size:19px;font-weight:500;color:#901d77">' +
                                    Response.Response[i]["branchName"] + '</div>' +
                                    '<h5>' + Response.Response[i]["address1"] + ',' + Response.Response[i]["city"] + ',' + Response.Response[i]["district"] + '</h5>' +
                                    '<h5> Timing :' + Response.Response[i]["fromtimeAM"] + ' - '
                                    + Response.Response[i]["totimeAM"] + '</h5>' + mob +
                                    '<a href="#" style="text-decoration: none;" runat="server" onclick = "return theFunction();">View More</a>');
                                infoWindow.open(map, marker);
                                var latitude = Response.Response[i]["latitude"];
                                var longitude = Response.Response[i]["longitude"];

                                // Set the values of hidden fields
                                document.getElementById('<%=hfBranchId.ClientID %>').value = Response.Response[i]["branchId"];
                                document.getElementById('<%=hfgymOwnerId.ClientID %>').value = Response.Response[i]["gymOwnerId"];
                                document.getElementById('<%=hdnLatitude.ClientID %>').value = latitude;
                                document.getElementById('<%=hdnLongitude.ClientID %>').value = longitude;


                            }

                        })(marker, i));



                   }
                   var boundsListener = google.maps.event.addListener((map), 'bounds_changed', function (event) {
                       this.setZoom(10);
                       google.maps.event.removeListener(boundsListener);
                   });

               },
               error: function (response) {
                   // Handle the error if needed
               }
           });

        }
        function theFunction() {
            document.getElementById('<%=btnSubmit.ClientID %>').click();
       }
   </script>




    <asp:HiddenField runat="server" ID="hdnLatitude" />
    <asp:HiddenField runat="server" ID="hdnLongitude" />

    <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" Style="display: none;" />
    <script>
        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition);
            } else {
                alert("Geolocation is not supported by this browser.");
            }
        }

        function showPosition(position) {
            var latitude = position.coords.latitude;
            var longitude = position.coords.longitude;

            // Set the values of hidden fields
            document.getElementById('<%=hdnLatitude.ClientID %>').value = latitude;
            document.getElementById('<%=hdnLongitude.ClientID %>').value = longitude;

            // Submit the form

            document.getElementById('<%=btnSubmit.ClientID %>').click();

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

</asp:Content>

