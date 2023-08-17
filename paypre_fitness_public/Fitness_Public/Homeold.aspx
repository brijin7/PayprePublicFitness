<%@ Page Title="" Language="C#" MasterPageFile="~/Fitness.Master" AutoEventWireup="true" CodeFile="Homeold.aspx.cs" Inherits="Pages_HomeNew_HomeNew" %>

<%@ MasterType VirtualPath="~/Fitness.master" %>
<asp:Content ID="CtntHome" ContentPlaceHolderID="FitnessContent" runat="Server" >
    <%-- Home Page Scipts --%>
    <script defer type="module" src='<%=ResolveUrl("Pages/Home/Home_Main.js") %>'></script>

    <%-- Common Heading Styles --%>
    <link href="Pages/Home/Home_CommonHeadings.css" rel="stylesheet" />
    <asp:Button ID="btn_Home_BranchChange" ClientIDMode="Static" CssClass="d-none" OnClick="btnBranchChange_Click" runat="server" />
    <%-- Index --%>
    <link href="Pages/Home/Home_Index.css" rel="stylesheet" />
    <header class="Container-fluid HomeIndex">
        <div class="row">
            <div class="col-12 col-sm-12 col-md-4 col-lg-4 col-xl-4  HomeLeft">
                <div class="row">
                    <div class="col-2 col-sm-2  col-md-1 col-lg-1 col-xl-1 d-block d-sm-block  d-md-none d-lg-none d-xl-none Marquee-Container">
                        <span class="HomeMarquee Marquee_1">PayPre fitness &nbsp;&nbsp;PayPre fitness &nbsp;&nbsp;PayPre fitness &nbsp;&nbsp;</span>
                        <span class="HomeMarquee Marquee_2">PayPre fitness &nbsp;&nbsp;PayPre fitness &nbsp;&nbsp;PayPre fitness &nbsp;&nbsp;</span>
                    </div>
                    <div class="col-10 col-sm-10 col-md-11 col-lg-11 col-xl-11">
                        <div class="ImgGymText-Container">
                            <img src="Images/HomeIndex/text.svg" class="ImgGymText" />
                        </div>
                        <div class="HeaderLeft-Text">
                            <div class="Desc Desc_1"><span>don't find</span></div>
                            <div class="Desc Desc_2"><span>time to workout</span></div>
                            <div class="Desc Desc_3">
                                <span>make
                                <img src="Images/HomeIndex/FlexedBiceps.png" /></span>
                            </div>
                            <div class="Desc Desc_4">
                                <span>time to workout
                                <img src="Images/HomeIndex/clock.png" /></span>
                            </div>
                        </div>
                        <div class="Getourapp-Container">
                            <div>
                                <div class="Desc Desc_1"><span>get our app in </span></div>
                            </div>
                            <div class="ImgGetOurApp-Container">
                                <a class="LnkImgGetOurApp" target="_blank" href="https://play.google.com/store/apps/details?id=com.rocks.fit">
                                    <img src="Images/HomeIndex/Playstore.png" class="ImgGetOurApp" />
                                </a>
                                <a id="btnLnkImgDown" class="LnkImgDown" href="javascript:void(0)" style="display: none">
                                    <img src="Images/HomeIndex/down.svg" class="ImgDown" />
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="col-2 col-sm-2  col-md-1 col-lg-1 col-xl-1  d-none d-sm-none d-md-block d-lg-block d-xl-block Marquee-Container">
                        <span class="HomeMarquee Marquee_1">PayPre fitness &nbsp;&nbsp;PayPre fitness &nbsp;&nbsp;PayPre fitness &nbsp;&nbsp;</span>
                        <span class="HomeMarquee Marquee_2">PayPre fitness &nbsp;&nbsp;PayPre fitness &nbsp;&nbsp;PayPre fitness &nbsp;&nbsp;</span>
                    </div>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8 d-none d-sm-none d-md-block d-lg-block d-xl-block">
                <div class="col-12 HomeRight ">
                    <%--                    <img src="Images/HomeIndex/HomeIndexRight.png" class="ImgHomeIndexRight" />--%>
                    <video autoplay loop muted class="ImgHomeIndexRight">
                        <source src="Images/HomeIndex/PaypreVideo.webm" type="video/mp4">
                    </video>
                </div>
            </div>
        </div>
    </header>

    <%-- Classes --%>

    <link href="Pages/Home/Home_Classes.css" rel="stylesheet" />
    <div id="scrollClasses"></div>
    <section id="Section-Classes" class="row container-fluid HomeClasses">
        <div class="col-12">
            <div class="row">
                <div class="col-12 Heading-Container HdgClasses">
                    <div class="MainHeading"><span>classes</span></div>
                    <div class="SubHeading"><span>Get healthy & fit by selecting any of our popular classes mentioned below</span></div>
                </div>
            </div>

            <div id="Classes-Container" class="row Classes-Container owl-carousel" style="text-align: -webkit-center;">
                <%-- Classes --%>
            </div>
        </div>

    </section>

    <asp:HiddenField ID="Hdn_Home_Classes" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="Hdn_Home_Classes_CategoryId" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="Hdn_Home_Classes_CategoryName" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="Hdn_Home_Classes_DispAmount" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="Hdn_Home_Classes_SaveAmount" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfYoutubelink" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfFblink" ClientIDMode="Static" runat="server" />
    <asp:Button ID="btn_Home_Classes" OnClick="btn_Home_Classes_Click" CssClass="d-none" ClientIDMode="Static" runat="server" />

    <%-- Subscription --%>
    <link href="Pages/Home/Home_Subscription.css" rel="stylesheet" />
    <div id="scrollSubscription"></div>
    <asp:HiddenField ID="Hdn_Home_Subscription" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="Hdn_Home_SubscriptionId" ClientIDMode="Static" runat="server" />
    <section id="Section-Subscription" class="container-fluid Subscription">
        <div class="col-12">
            <div class="row">
                <div class="col-12 Heading-Container HdgSubscription">
                    <div class="MainHeading"><span>subscription</span></div>
                    <div class="SubHeading"><span>monthly & yearly plans</span></div>
                </div>
            </div>

            <div id="HomeSubscription-Carousel" class="row subscription-carousel owl-carousel mt-5" style="text-align: -webkit-center;">
                <%-- subscription --%>
            </div>

        </div>
    </section>
    <asp:Button ID="btn_Home_Subscription" ClientIDMode="Static" runat="server" OnClick="btn_Home_Subscription_Click" CssClass="d-none" />

    <%-- Testimonials --%>
    <link href="Pages/Home/Home_Testimonials.css" rel="stylesheet" />
    <div id="scrollTestimonials"></div>
    <asp:HiddenField ID="Hdn_Home_Testimonials" ClientIDMode="Static" runat="server" />
    <section id="Section-Testimonials" class="container-fluid Testimonials">
        <div class="col-12">
            <div class="row">
                <div class="col-12 Heading-Container HdgClasses">
                    <div class="MainHeading"><span>Testimonials</span></div>
                    <div class="SubHeading">
                        <span>Unbelievable Before & After Fitness Transformation Shows How Long It Took People To Get In Shape</span>
                    </div>
                </div>
                <div id="HomeTestimonials-Carousel" class="testimonials-carousel owl-carousel Carousel-Container">
                </div>
            </div>
        </div>
        <div id="divTestimonialsSelectedImage" class="Testimonials-SelecteImage d-none">
            <img id="btnTestWhiteLeft" src="Images/Testimonials/white-left.svg" class="btnWhiteLeftRight" />
            <img id="SelectedTransFormation" src="Images/Testimonials/1.png" class="SelectedTransFormation" />
            <img id="btnTestWhiteRight" src="Images/Testimonials/white-right.svg" class="btnWhiteLeftRight" />
        </div>
    </section>
    <div id="divTestimonialOverlay" class="Overlay-Testimonials d-none"></div>

    <%-- Just About --%>
    <link href="Pages/Home/Home_JustAbout.css" rel="stylesheet" />
    <div class="container-fluid JustAbout">
        <div class="col-12">
            <div class="row">
                <div class="col-6 col-sm-6 col-md-4 col-lg-4 col-xl-4">
                    <div class="JustAboutLeft-Container">
                        <h2 class="JustAbout-Heading">just about</h2>
                        <div class="JustAbout-Left">
                            <div class="Desc_1">
                                <span>Hey !</span>
                            </div>
                            <div class="Desc_2">
                                <span class="Decs_2_Position">
                                    <img src="Images/JustAbout/Crown.svg" class="JustAbout-Crown" />
                                    i’m Paypre Fitness
                                </span>
                            </div>
                            <div class="Desc_3">
                                <span>PayPre Fitness is a Unique online & on-site solution based on your individual health needs. We are the ONLY HEALTH CLUB who cares for all your health needs that includes personal training, body transformation, contest preparation, women specialized programs, lifestyle change, motivation, diet & Diet which boosts your energy. 
                                </span>
                            </div>
                        </div>
                        <div class="Explore">
                            <a href="Pages/About/About.aspx" class="btn btnExplore">explore
                            <img class="ExploreArrow" src="Images/JustAbout/ExploreArrow.svg" /></a>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-sm-6 col-md-8 col-lg-8 col-xl-8 p-5 p-sm-5 p-md-0 p-lg-0 p-xl-0">
                    <div class="JustAboutBg">
                        <img class="JustAboutBg-BS" src="Images/JustAbout/JustAbout.png" alt="JustAbout" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- Download App --%>
    <link href="Pages/Home/Home_DownloadApp.css" rel="stylesheet" />
    <div class="container DownloadApp d-none">
        <div class="row">
            <div class="col-6 ImgPosition">
                <img src="Images/DownloadApp/AndroidApp.png" class="ImgCommonDownloadApp" />
            </div>
            <div class="col-6 ImgPosition">
                <img src="Images/DownloadApp/Wing.png" class="ImgCommonDownloadApp ImgWing" />

                <div class="DownloadDesc-Container">
                    <span class="Desc Desc_1">Download </span>
                    <span class="Desc Desc_2">our mobile app</span>
                    <span class="Desc Desc_3">easy & fast way</span>

                    <div class="Download-Options">
                        <img src="Images/DownloadApp/QRCode.png" class="ImgQRCode" />
                        <a target="_blank" href="https://play.google.com/store/apps/details?id=com.rocks.fit">
                            <img src="Images/DownloadApp/Playstore.png" class="ImgPlayStore" />
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- Footer --%>
    <link href="Pages/Home/Home_Footer.css" rel="stylesheet" />
    <div class="container-fluid Footer">
        <div class="row">
            <div class="col-12 FooterBg">
                <div class="col-10 mx-auto FooterSpacing">
                    <div class="row">
                        <%-- About --%>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-4 col-xl-4">
                            <%-- About --%>
                            <div class="About-Container">
                                <div class="Main-Heading">
                                    <div class="Desc_1">
                                        <img src="Images/Footer/about.svg" class="ImgIconCommon" alt="About" />
                                        <span>about</span>
                                    </div>
                                </div>
                                <div class="Address">
                                    <div class="Desc_1">
                                        <img src="Images/Footer/location.svg" class="ImgIconCommon" alt="Location" />
                                        <span id="FooterStreet" runat="server">Basthi Road, Indira Gandhi Nagar, Sultanpet, Hosur, Tamil Nadu 635109
                                        </span>
                                    </div>
                                </div>
                                <div class="MobileNo">
                                    <img src="Images/Footer/Contact.svg" class="ImgIconCommon ImgContact" alt="Contanct" />
                                    <span id="FooterMobile" runat="server">96777 85755</span>
                                </div>
                                <div class="EmailAddress">
                                    <img src="Images/Footer/Gmail.svg" class="ImgIconCommon ImgEmail" alt="Email" />
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
                                        <img src="Images/Footer/news.svg" class="ImgIconCommon" alt="news" />
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
                            </div>
                        </div>
                        <%-- Instagram --%>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-4 col-xl-4">
                            <div class="Instagram-Container">
                                <div class="Main-Heading">
                                    <div class="Desc_1">
                                        <img src="Images/Footer/insta.svg" class="ImgIconCommon" alt="insta" />
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
                                <span>© 2023 PayPre Fitness, All Rights Reserved</span>
                            </div>
                            <div class="DesignedBy">
                                <img class="ImgDesignedBy" src="Images/Footer/designby.svg" alt="designby" />
                                <span>Designed by</span>
                                <a href="https://prematix.com/" target="_blank" class="PrematixLink">@Prematix</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--FAQ--%>
    <link href="Pages/Home/Home_FAQ.css" rel="stylesheet" />
    <div id="DivUserAgreement" runat="server" class="DisplyCard" style="display: none">
        <div class="row DisplyCardPostion divresponsive">
            <div class="divLogo text-start">
                  <asp:Image id="LogoTerms" class="logoImg" runat="server" />
                <label class="TSHead">Terms & Conditions</label>
                <linkbutton onclick="CloseClickTS()" class="FAQCloseBtn">X</linkbutton>
            </div>
            <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">
                <asp:DataList ID="dtlTerms" runat="server" RepeatDirection="Vertical" RepeatColumns="1">
                    <ItemTemplate>
                        <div class="UserHead text-start">
                            <asp:Label ID="lblPrivacypolicy" runat="server" Text='<%# Bind("question") %>' ></asp:Label>
                        </div>
                        <p class="UserText text-start">
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("answer") %>'></asp:Label>
                        </p>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                <img class="imgTC" src="Images/FAQ/TCimg.svg" />
            </div>
        </div>
    </div>
    <div id="DivPrivacy" runat="server" class="DisplyCard" style="display: none">
        <div class="row DisplyCardPostion divresponsive">
            <div class="divLogo text-start">
                 <asp:Image id="LogoPrivacy" class="logoImg" runat="server" />
                <label class="TSHead">Privacy & Policy</label>
                <linkbutton onclick="CloseClickP()" class="FAQCloseBtn">X</linkbutton>
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
                <asp:Image id="LogoFAQ" class="logoImg" runat="server" />
                <label class="TSHead">FAQ</label>
                <linkbutton onclick="CloseClickFAQ()" class="FAQCloseBtnFAQ">X</linkbutton>
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

</asp:Content>
