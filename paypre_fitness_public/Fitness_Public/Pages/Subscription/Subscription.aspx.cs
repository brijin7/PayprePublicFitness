using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.EnterpriseServices;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;
using System.Xml.Linq;



public partial class Pages_Subscription_Subscription : System.Web.UI.Page
{
    readonly Helper Helper = new Helper();
    readonly string BaseUri;
    readonly string BaseTokenUri;
    string Token;
    string SubscriptionURL;
    string SubscriptionBenefitesURL;
    public Pages_Subscription_Subscription()
    {
        BaseUri = $"{ConfigurationManager.AppSettings["BaseUrl"].Trim()}";
        BaseTokenUri = $"{ConfigurationManager.AppSettings["BaseUrlToken"].Trim()}";
        SubscriptionURL = $"{BaseUri}subscriptionPlanMaster/getuserHomeSubscription";
        SubscriptionBenefitesURL = $"{BaseUri}subscriptionBenefits";
    }

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            Session["BaseUrlToken"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrlToken"].Trim();
            Token = $"{Session["APIToken"]}";
            BindSubscriptionPlan();
            BindNavbarSubscriptionPlan();
            BindBenefitesSubscriptionPlan();
        }
        Token = $"{Session["APIToken"]}";
    }
    #endregion

    #region Bind SubscriptionPlan
    public void BindSubscriptionPlan()
    {
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "subscriptionPlanMaster/getuserSubscriptionDetails?subscriptionPlanId=" + Session["SubscriptionId"].ToString();
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        //lblHead.Text = dt.Rows[0]["packageName"].ToString() + " " + "Plan";
                        lblSubsName.Text = dt.Rows[0]["packageName"].ToString();
                        lblSubsHead.Text = dt.Rows[0]["description"].ToString();
                        lblfromTime.Text = dt.Rows[0]["fromTime"].ToString();
                        lblToTime.Text = dt.Rows[0]["toTime"].ToString();
                        lblAmt.Text = "₹" + dt.Rows[0]["netAmount"].ToString() + " " + "/-";

                        string star = dt.Rows[0]["credits"].ToString();
                        s1.Attributes.Remove("star");
                        s2.Attributes.Remove("star");
                        s3.Attributes.Remove("star");
                        s4.Attributes.Remove("star");
                        s5.Attributes.Remove("star");
                        if (star == "5")
                        {
                            s1.Attributes.Add("Class", "fa fa-star star");
                            s2.Attributes.Add("Class", "fa fa-star star");
                            s3.Attributes.Add("Class", "fa fa-star star");
                            s4.Attributes.Add("Class", "fa fa-star star");
                            s5.Attributes.Add("Class", "fa fa-star star");
                        }
                        if (star == "4")
                        {
                            s1.Attributes.Add("Class", "fa fa-star star");
                            s2.Attributes.Add("Class", "fa fa-star star");
                            s3.Attributes.Add("Class", "fa fa-star star");
                            s4.Attributes.Add("Class", "fa fa-star star");
                            s5.Attributes.Add("class", "fa fa-star");
                        }

                        if (star == "3")
                        {
                            s1.Attributes.Add("class", "fa fa-star star");
                            s2.Attributes.Add("class", "fa fa-star star");
                            s3.Attributes.Add("class", "fa fa-star star");
                            s4.Attributes.Add("class", "fa fa-star");
                            s5.Attributes.Add("class", "fa fa-star");

                        }
                        if (star == "2")
                        {
                            s1.Attributes.Add("Class", "fa fa-star star");
                            s2.Attributes.Add("Class", "fa fa-star star");
                            s3.Attributes.Add("class", "fa fa-star");
                            s4.Attributes.Add("class", "fa fa-star");
                            s5.Attributes.Add("class", "fa fa-star");
                        }
                        if (star == "1")
                        {
                            s1.Attributes.Add("Class", "fa fa-star star");
                            s2.Attributes.Add("class", "fa fa-star");
                            s3.Attributes.Add("class", "fa fa-star");
                            s4.Attributes.Add("class", "fa fa-star");
                            s5.Attributes.Add("class", "fa fa-star");
                        }

                        Session["netAmount"] = dt.Rows[0]["netAmount"].ToString();
                        Session["amount"] = dt.Rows[0]["amount"].ToString();
                        Session["tax"] = dt.Rows[0]["tax"].ToString();
                        Session["taxId"] = dt.Rows[0]["taxId"].ToString();
                        Session["taxName"] = dt.Rows[0]["taxName"].ToString();
                        Session["BuyPlan"] = "S";
                        Session["gymOwnerId"] = dt.Rows[0]["gymOwnerId"].ToString();
                        Session["branchId"] = dt.Rows[0]["branchId"].ToString();
                        Session["branchName"] = dt.Rows[0]["branchName"].ToString();
                        Session["subscriptionPlanId"] = dt.Rows[0]["subscriptionPlanId"].ToString();

                        subdescription.InnerText = dt.Rows[0]["packageName"].ToString();
                        subdate.InnerText = DateTime.Now.ToString("dd-MM-yyyy");
                        submonth.InnerText = dt.Rows[0]["description"].ToString();
                        subamount.InnerText = "₹ " + dt.Rows[0]["netAmount"].ToString();
                        ActualAmount.InnerText = "₹ " + dt.Rows[0]["actualAmount"].ToString();
                        Amount.InnerText = "₹ " + dt.Rows[0]["netAmount"].ToString();
                        TotalAmount.InnerText = "₹ " + dt.Rows[0]["netAmount"].ToString();
                        saveamount.InnerText = "Save Amount "+"₹ " + dt.Rows[0]["savedAmount"].ToString();                        
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Errorresponse.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    //#region Buy Button 
    //protected void btnBuyNow_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("../PaymentPage/PaymentPage.aspx");

    //}
    //#endregion
    #region Btn Back Click 
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../../Home.aspx");
    }
    #endregion
    #region Bind Navbar SubscriptionPlan
    public void BindNavbarSubscriptionPlan()
    {
        try
        {
            string URI = $"{SubscriptionURL}?gymOwnerId={Session["gymOwnerId"]}&branchId={Session["BranchId"]}";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                dtlSub.DataSource = Dt;
                dtlSub.DataBind();
                for (int i = 0; i < dtlSub.Items.Count; i++)
                {
                    string SubscriptionId = Session["SubscriptionId"].ToString();
                    Label Id = dtlSub.Items[i].FindControl("lblId") as Label;
                    LinkButton lblSubscr = dtlSub.Items[i].FindControl("lblSubscr") as LinkButton;
                    if (SubscriptionId == Id.Text)
                    {
                        lblSubscr.Attributes.Add("class", "Imgbackactive");
                    }
                    else
                    {
                        lblSubscr.Attributes.Remove("class");

                    }
                }
            }
            else
            {
                hfSubscription.Value = "[]";
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion

    #region Bind Benefites SubscriptionPlan
    public void BindBenefitesSubscriptionPlan()
    {
        try
        {
            string URI = $"{SubscriptionBenefitesURL}?subscriptionPlanId={Session["SubscriptionId"]}";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                dtlBenfSub.DataSource = Dt;
                dtlBenfSub.DataBind();
            }
            else
            {
                hfSubscription.Value = "[]";
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion

    protected void lblSubscr_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;
        Label lblId = gvrow.FindControl("lblId") as Label;
        Session["SubscriptionId"] = lblId.Text;

        for (int i = 0; i < dtlSub.Items.Count; i++)
        {
            LinkButton lblSubscr = dtlSub.Items[i].FindControl("lblSubscr") as LinkButton;
            Label Id = dtlSub.Items[i].FindControl("lblId") as Label;
            if (Id.Text == lblId.Text)
            {
                lblSubscr.Attributes.Add("class", "Imgbackactive");
            }
            else
            {
                lblSubscr.Attributes.Remove("class");

            }
        }
        BindSubscriptionPlan();
        BindBenefitesSubscriptionPlan();
    }

    //Login
    #region Login Click
    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        txtMobileNo.Enabled = false;
        SendOtp();
    }
    #endregion
    #region Get UserDetails
    public void GetUserDetails(string LoginOrReLogin = "Login")
    {
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                HttpResponseMessage response;

                if (txtMobileNo.Text != "")
                {
                    string sUrl = Session["BaseUrl"].ToString().Trim()
                         + "signIn?mobileNo=" + txtMobileNo.Text + "";

                    response = client.GetAsync(sUrl).Result;
                }
                else
                {
                    string sUrl = Session["BaseUrl"].ToString().Trim()
                        + "signIn?mobileNo=" + txtMail.Text + "";

                    response = client.GetAsync(sUrl).Result;
                }

                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows[0]["roleName"].ToString() == "User")
                        {

                            //fname.Text = dt.Rows[0]["mobileNo"].ToString();
                            //if (fname.Text == "")
                            //{
                            //    fname.Text = dt.Rows[0]["mailId"].ToString();
                            //}
                            Session["mobileNo"] = dt.Rows[0]["mobileNo"].ToString();
                            Session["userId"] = dt.Rows[0]["userId"].ToString();
                            Session["userRole"] = dt.Rows[0]["roleName"].ToString();
                            Session["mailId"] = dt.Rows[0]["mailId"].ToString();
                            DivLogin.Visible = false;
                            Master.Master_btnlogin.Visible = false;
                            Master.Master_btnUserProfile.Visible = true;
                            Master.Master_listMyprofile.Visible = true;
                            Master.Master_lblUserName.Text = dt.Rows[0]["mobileNo"].ToString();
                            Session["LoginStatus"] = "L";
                            string Img = dt.Rows[0]["Image"].ToString();
                            if (Master.Master_lblUserName.Text == "")
                            {
                                Master.Master_lblUserName.Text = dt.Rows[0]["mailId"].ToString();
                            }
                            if (Img == "")
                            {
                                Master.Master_userimg.ImageUrl = "Images/Login/UserProfile.png";
                                Master.Master_UserProfileImg.Src = "Images/Login/user1.png";
                            }
                            else
                            {
                                Master.Master_userimg.ImageUrl = dt.Rows[0]["Image"].ToString();

                            }
                            //fname.Enabled = false;
                            //GetUserBodyTest();
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, GetType(), "Popup",
                            //    "infoalert('Invalid User');", true);
                            DivLogin.Visible = true;
                            txtotp.Visible = false;
                            btnCfmOtp.Visible = false;
                            btnResend.Visible = false;
                            lblOtp.Visible = false;
                            txtMobileNo.Text = string.Empty;
                            txtMail.Text = string.Empty;
                            txtotp.Text = string.Empty;
                            txtMobileNo.Enabled = true;
							ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Invalid User');", true);
						}
                    }
                    else
                    {
                        Signup();
                    }
                }
                else
                {
                    txtotp.Visible = false;
                    btnCfmOtp.Visible = false;
                    btnResend.Visible = false;
                    lblOtp.Visible = false;
                    txtMobileNo.Text = string.Empty;
                    txtMail.Text = string.Empty;
                    txtotp.Text = string.Empty;
                    txtMobileNo.Enabled = true;
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Errorresponse.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region txtMail
    protected void txtMail_TextChanged(object sender, EventArgs e)
    {
        txtMobileNo.Text = string.Empty;
        GetUserDetails();
    }
    #endregion
    #region SendOtp
    public void SendOtp()
    {
        try
        {
            using (var client = new HttpClient())
            {
                // hfMobileNo.Value = txtMobileNo.Text;
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new SendOTPClass()
                {
                    queryType = "sendSignInOtp",
                    mobileNo = txtMobileNo.Text

                };
                HttpResponseMessage response = client.PostAsJsonAsync("sendOtp", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "SendOtp", "SendOtp();", true);
                        btnCfmOtp.Visible = true;
                        btnResend.Visible = true;
                        txtotp.Visible = true;
                        lblOtp.Visible = true;
                        string[] Response = ResponseMsg.Split('~');
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Please Check the Mobile Number!');", true);
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Errorresponse.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region CfmOtp
    protected void btnCfmOtp_Click(object sender, EventArgs e)
    {
        if (txtotp.Text != "")
        {
            VerifyOtp();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "SendOtp", "SendOtp();", true);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Enter OTP !');", true);
        }

    }
    #endregion
    #region SendOTP Class
    public class SendOTPClass
    {
        public string queryType { get; set; }
        public string mobileNo { get; set; }
    }
    #endregion
    #region Signup Class
    public class SignupClass
    {
        public string mobileNo { get; set; }
        public string mailId { get; set; }
        public string passWord { get; set; }
    }
    #endregion
    #region Signup
    public void Signup()
    {
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                HttpResponseMessage response;
                string email;
                if (txtMobileNo.Text != "")
                {
                    email = txtMobileNo.Text;
                }
                else
                {
                    email = txtMail.Text;
                }

                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (match.Success)
                {
                    var Insert = new SignupClass()
                    {
                        mailId = txtMail.Text,
                        mobileNo = null
                    };
                    response = client.PostAsJsonAsync("signUp", Insert).Result;

                }
                else
                {
                    var Insert = new SignupClass()
                    {
                        mobileNo = txtMobileNo.Text,
                        mailId = null
                    };
                    response = client.PostAsJsonAsync("signUp", Insert).Result;
                }
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        GetUserDetails();
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    else
                    {
                        txtotp.Visible = false;
                        btnCfmOtp.Visible = false;
                        btnResend.Visible = false;
                        lblOtp.Visible = false;
                        txtMobileNo.Text = string.Empty;
                        txtMail.Text = string.Empty;
                        txtotp.Text = string.Empty;
                        txtMobileNo.Enabled = true;
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Please Check the Mobile Number!');", true);
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Errorresponse.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region VerifyOtp
    public void VerifyOtp()
    {
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new VerifyOTPClass()
                {
                    mobileNo = txtMobileNo.Text,
                    otp = txtotp.Text
                };
                HttpResponseMessage response = client.PostAsJsonAsync("verifyOtp", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        GetUserDetails();
                    }
                    else
                    {
                        txtotp.Text = string.Empty;
                        btnResend.Text = "Resend OTP";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Please Check the OTP');", true);
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        txtotp.Text = string.Empty;
                        btnResend.Text = "Resend OTP";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region VerifyOTP Class
    public class VerifyOTPClass
    {
        public string mobileNo { get; set; }
        public string otp { get; set; }
    }
    #endregion
    #region Resend
    protected void btnResend_Click(object sender, EventArgs e)
    {
        SendOtp();
    }
    #endregion
    #region Alerts
    public void ShowSuccessPopup(string Message)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert(`" + Message.Trim() + "`);", true);
    }
    public void ShowInfoPopup(string Message)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert(`" + Message.Trim() + "`);", true);
    }
    #endregion

    #region Buy Now Btn
    protected void btnBuyNow_Click(object sender, EventArgs e)
    {
        if (Session["mobileNo"] != null)
        {
            Divpaymentdetails.Visible = true;

           fname.Enabled = false;
            if (Session["mobileNo"].ToString().Trim() != "")
            {
                fname.Text = Session["mobileNo"].ToString().Trim();
            }
            else if (Session["mailId"].ToString().Trim() != "")
            {
               fname.Text = Session["mailId"].ToString().Trim();
            }
            else
            {
               fname.Enabled = true;
            }

            string taxname;
            if (Session["taxName"].ToString().Trim() == null || Session["taxName"].ToString().Trim() == "")
            {
                taxname = "CGST-9.00, SGST-9.00";
            }
            else
            {
                taxname = Session["taxName"].ToString();
            }

            var Insertjavasctipt = new BookingsubClass();


            Insertjavasctipt.queryType = "insert";
            Insertjavasctipt.gymOwnerId = Session["gymOwnerId"].ToString();
            Insertjavasctipt.branchId = Session["branchId"].ToString();
            Insertjavasctipt.branchName = Session["branchName"].ToString();
            Insertjavasctipt.subscriptionPlanId = Session["subscriptionPlanId"].ToString();
            Insertjavasctipt.userId = Session["userId"].ToString();
            Insertjavasctipt.booking = "W";
            Insertjavasctipt.loginType = "U";
            Insertjavasctipt.price = Session["amount"].ToString();
            Insertjavasctipt.taxId = Session["taxId"].ToString();
            Insertjavasctipt.taxName = taxname;
            Insertjavasctipt.taxAmount = Session["tax"].ToString();
            Insertjavasctipt.totalAmount = Session["netAmount"].ToString();
            Insertjavasctipt.paidAmount = "0";
            Insertjavasctipt.paymentStatus = "N";
            Insertjavasctipt.paymentType = "69";
            Insertjavasctipt.transactionId = "";
            Insertjavasctipt.bankName = "";
            Insertjavasctipt.bankReferenceNumber = "";
            Insertjavasctipt.offerId = "0";
            Insertjavasctipt.offerAmount = "0";
            Insertjavasctipt.createdBy = Session["userId"].ToString();

            string output = JsonConvert.SerializeObject(Insertjavasctipt);
            hfSubscriptionInsert.Value= output;
            hfBaseurl.Value = Session["BaseUrl"].ToString() + "SubscriptionBooking";
            hfPaymentBaseurl.Value = Session["BaseUrl"].ToString() + "BookingIdBasedSubspBookingDetails";
            hfToken.Value= Session["APIToken"].ToString();
            hfNotificationSMSBaseurl.Value= Session["BaseUrl"].ToString() + "sendBookingNotification";

            //var Notificationjavasctipt = new NotificationClass();
            //Notificationjavasctipt.queryType = "SendSMSforbookingsubscription";
            //Notificationjavasctipt.mobileNo = Session["mobileNo"].ToString();
            //Notificationjavasctipt.mailId = Session["mailId"].ToString();
            //Notificationjavasctipt.userId = Session["userId"].ToString();
            //string Notificationoutput = JsonConvert.SerializeObject(Notificationjavasctipt);
            hfNotificationSMSDatas.Value = Session["mobileNo"].ToString();
            hfNotificationSMSUserId.Value = Session["userId"].ToString();

        }
        else
        {
            DivLogin.Visible = true;
        }


       
    }
    #endregion

    public void InsertsubBooking()
    {
        try
        {
            string taxname;
            if (Session["taxName"].ToString().Trim() == null || Session["taxName"].ToString().Trim() == "")
            {
                taxname = "CGST-9.00, SGST-9.00";
            }
            else
            {
                taxname = Session["taxName"].ToString();
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new BookingsubClass()
                {
                    queryType = "insert",
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    branchName = Session["branchName"].ToString(),
                    subscriptionPlanId = Session["subscriptionPlanId"].ToString(),
                    userId = Session["userId"].ToString(),
                    booking = "W",
                    loginType = "U",
                    price = Session["amount"].ToString(),
                    taxId = Session["taxId"].ToString(),
                    taxName = taxname,
                    taxAmount = Session["tax"].ToString(),
                    totalAmount = Session["netAmount"].ToString(),
                    paidAmount = "0",
                    paymentStatus = "N",
                    paymentType = "69",
                    transactionId = "",
                    bankName = "",
                    bankReferenceNumber = "",
                    offerId = "0",
                    offerAmount = "0",
                    createdBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("SubscriptionBooking", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Divpaymentdetails.Visible = false;
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        Divpaymentdetails.Visible = false;
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }

    public class BookingsubClass
    {
        public string queryType { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string branchName { get; set; }
        public string subscriptionPlanId { get; set; }
        public string booking { get; set; }
        public string userId { get; set; }
        public string loginType { get; set; }
        public string price { get; set; }
        public string taxId { get; set; }
        public string taxName { get; set; }
        public string taxAmount { get; set; }
        public string totalAmount { get; set; }
        public string paidAmount { get; set; }
        public string paymentStatus { get; set; }
        public string paymentType { get; set; }
        public string transactionId { get; set; }
        public string bankName { get; set; }
        public string bankReferenceNumber { get; set; }
        public string offerId { get; set; }
        public string offerAmount { get; set; }
        public string createdBy { get; set; }


    }
    public class NotificationClass
    {
        public string queryType { get; set; }
        public string mobileNo { get; set; }
    }
    #region Pay Button 
    protected void verifyandpay_Click(object sender, EventArgs e)
    {
        //if(hfBookingId.Value == "")
        //{
        //    InsertsubBooking();
        //}
        //else
        //{
        //    string Message = "Success";
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert(`" + Message.Trim() + "`);", true);
        //}
       // InsertsubBooking();
    }
    #endregion

    protected void Closebutton_Click(object sender, ImageClickEventArgs e)
    {
        Divpaymentdetails.Visible = false;
    }

    protected void goback_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../Home.aspx", true);
    }

   

}