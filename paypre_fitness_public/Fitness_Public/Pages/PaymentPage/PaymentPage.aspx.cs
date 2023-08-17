using Newtonsoft.Json.Linq;
using System;
using System.Activities.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services.Description;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Data;
using System.Text.RegularExpressions;
using System.Configuration;

public partial class Pages_PaymentPage_PaymentPage : System.Web.UI.Page
{
    readonly Helper Helper = new Helper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["mobileNo"] != null)
            {
                fname.Enabled = false;
                if(Session["mobileNo"].ToString().Trim() !="")
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
            }
            else
            {
               DivLogin.Visible = true;
            }
            BuyPlanDetails();
        }
        
    }

    public void BuyPlanDetails()
    {
        if (Session["BuyPlan"].ToString() == "S")
        {
            TotalAmount.InnerText = '₹' + Session["netAmount"].ToString();
            ActualAmount.InnerText = '₹' + Session["amount"].ToString();
            Amount.Visible = false;
            Amountmain.Visible = false;
        }
        else if (Session["BuyPlan"].ToString() == "P")
        {
            TotalAmount.InnerText = '₹' + Session["netAmount"].ToString();
            ActualAmount.InnerText = '₹' + Session["actualAmount"].ToString();
            Amount.InnerText = '₹' + Session["netAmount"].ToString();
            Amount.Visible = true;
            Amountmain.Visible = true;
        }
    }

    #region Pay Button 
    protected void verifyandpay_Click(object sender, EventArgs e)
    {  
        if (Session["BuyPlan"].ToString() == "S")
        {
            InsertsubBooking();
        }
        else
        {
            InsertBooking();
        }
    }
    #endregion

    public void InsertBooking()
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
                var Insert = new BookingClass()
                {

                    queryType = "insert",
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    branchName = Session["branchName"].ToString(),
                    categoryId = Session["PcategoryId"].ToString(),
                    trainingTypeId = Session["trainingTypeId"].ToString(),
                    planDurationId = Session["planDurationId"].ToString(),
                    traningMode = Session["trainingMode"].ToString(),
                    phoneNumber = Session["mobileNo"].ToString(),
                    userId = Session["userId"].ToString(),
                    booking = "W",
                    loginType = "U",
                    priceId = Session["priceId"].ToString(),
                    price = Session["price"].ToString(),
                    taxId = Session["taxId"].ToString(),
                    taxName = taxname,
                    taxAmount = Session["taxAmount"].ToString(),
                    totalAmount = Session["netamount"].ToString(),
                    paymentCyclesStatus = "F",
                    paymentCycles = "0",
                    paidAmount = "0",
                    slotFromTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    slotToTime = "",
                    slotId = "0",
                    paymentStatus = "N",
                    transactionId = "",
                    bankName = "",
                    bankReferenceNumber = "",
                    offerId = "0",
                    offerAmount = "0",
                    paymentType = "69",
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("booking", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        Response.Redirect("../DietWorkOut/DietWorkOut.aspx", true);

                    }
                    else
                    {

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
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {

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

    #region Booking Class 

    public class BookingClass
    {
        public string queryType { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string branchName { get; set; }
        public string categoryId { get; set; }
        public string booking { get; set; }
        public string trainingTypeId { get; set; }
        public string slotId { get; set; }
        public string planDurationId { get; set; }
        public string traningMode { get; set; }
        public string phoneNumber { get; set; }
        public string userId { get; set; }
        public string loginType { get; set; }
        public string priceId { get; set; }
        public string price { get; set; }
        public string taxId { get; set; }
        public string taxName { get; set; }
        public string taxAmount { get; set; }
        public string totalAmount { get; set; }
        public string paidAmount { get; set; }
        public string paymentStatus { get; set; }
        public string slotFromTime { get; set; }
        public string slotToTime { get; set; }
        public string paymentCycles { get; set; }
        public string paymentType { get; set; }
        public string paymentCyclesStatus { get; set; }
        public string transactionId { get; set; }
        public string bankName { get; set; }
        public string bankReferenceNumber { get; set; }
        public string offerId { get; set; }
        public string offerAmount { get; set; }
        public string createdBy { get; set; }

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
    #endregion

    protected void backbutton_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["BuyPlan"].ToString() == "S")
        {
            Response.Redirect("../Subscription/Subscription.aspx");
        }
        else
        {
            Response.Redirect("../PlanDetails/PlanDetails.aspx");
        }
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

                            fname.Text = dt.Rows[0]["mobileNo"].ToString();
                            if (fname.Text == "")
                            {
                                fname.Text = dt.Rows[0]["mailId"].ToString();
                            }
                            Session["mobileNo"] = dt.Rows[0]["mobileNo"].ToString();
                            Session["userId"] = dt.Rows[0]["userId"].ToString();
                            Session["userRole"] = dt.Rows[0]["roleName"].ToString();
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
                            fname.Enabled = false;
                            GetUserBodyTest();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", 
                                "infoalert('Invalid User');", true);
                            DivLogin.Visible = true;
                            txtotp.Visible = false;
                            btnCfmOtp.Visible = false;
                            btnResend.Visible = false;
                            lblOtp.Visible = false;
                            txtMobileNo.Text = string.Empty;
                            txtMail.Text = string.Empty;
                            txtotp.Text = string.Empty;
                            txtMobileNo.Enabled = true;
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
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + Response[1].ToString().Trim() + "');", true);

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

    #region Get User In body Test Details 
    public void GetUserBodyTest()
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
                string Endpoint = "userInBodyTest?Input.userId=" + Session["userId"] + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        if (Session["userRole"].ToString().Trim() == "User")
                        {
                            Session["BodyTest"] = "N";
                            Session["Status"] = "P";
                            Master.Master_listMyprofile.Visible = true;
                            Master.Master_MyProfile.Visible = true;
                            Master.Master_divMyBodyTest.Visible = true;
                            Master.Master_DivProfile.Visible = false;
                            Master.Master_divGrid.Visible = false;
                            Master.Master_divUserForm.Visible = true;
                            Master.Master_btnAdd.Visible = false;
                            Master.Master_btnMyBodyCancel.Visible = false;
                            Master.Master_lnkbtnMyBodytClose.Visible = false;   

                        }

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

}