using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using Newtonsoft.Json;
using System.Web.Services;
using System.Data.Services.Client;
using System.Numerics;
using System.Runtime.Remoting.Messaging;
using System.Web.UI.HtmlControls;
using System.Globalization;

public partial class Pages_Dashboard_Dashboard : System.Web.UI.Page
{
    static IFormatProvider objEnglishDate = new System.Globalization.CultureInfo("en-GB", true);
    readonly Helper Helper = new Helper();
    Helper helper = new Helper();
    readonly string BaseUri;
    readonly string getDashBoardDetailsUri;
    readonly string getYoutubeLiveUrlUri;
    readonly string getNotificationUrlUri;
    readonly string getunreadedNotificationUrlUri;
    string token;
    string userId;
    string branchId;
    string branchName;
    string userName;
    string trainingMode;
    public Pages_Dashboard_Dashboard()
    {
        BaseUri = $"{ConfigurationManager.AppSettings["BaseUrl"].Trim()}";
        getDashBoardDetailsUri = $"{BaseUri}dashboardReport/getReport?";
        getYoutubeLiveUrlUri = $"{BaseUri}liveConfig/LiveDate?";
        getNotificationUrlUri = $"{BaseUri}UserNotification/updateUserNotification";
        getunreadedNotificationUrlUri = $"{BaseUri}UserNotification/GetUserNotification?";


    }

    protected void Page_Load(object sender, EventArgs e)
    {
		Session["DashboardCheck"] = true;
		token = $"{Session["APIToken"]}";
        userId = $"{Session["userId"]}";
        branchId = $"{Session["liveUrlbranchId"]}";
        branchName = $"{Session["dashBoardBranchName"]}";
        userName = $"{Session["userName"]}";
        trainingMode = $"{Session["userBookingtraningMode"]}";
        hfNotificationPosturl.Value = getNotificationUrlUri;
        hfNotificationURl.Value = getunreadedNotificationUrlUri;
        hfNotificationURlData.Value = "readstatus=N&userId=" + Session["userId"].ToString().Trim();
        hfTokenURl.Value = token;

        if (!IsPostBack)
        {
            lblUserName_Dashboard.Text = userName;
            lblBranch_Dashboard.Text = branchName;
            Session["LogoutUrl"] = System.Configuration.ConfigurationManager.AppSettings["LogoutUrl"].Trim();
            GetLiveUrl();
            if (trainingMode == "O")
            {
                hfTrainingMode.Value = "O";
                player.Attributes.Add("class", "w-100 h-auto youtubeVideo");
            }
            else
            {
                hfTrainingMode.Value = trainingMode;
                player.Attributes.Remove("class");
                player.Attributes.Add("class", "w-100 h-auto youtubeVideo d-none");
            }
            GetUserDetails();
            GetGymownerLogo();
        }
    }
    #region this method is used get the live url
    public void GetLiveUrl()
    {
        try
        {
            string requestUri = $"{getYoutubeLiveUrlUri}branchId={branchId}&date={DateTime.Now.ToString("yyyy-MM-dd")}";
            Helper Helper = new Helper();
            Helper.APIGet(requestUri, token, out DataTable dt, out int statusCode, out string response);
            if (statusCode == 1)
            {
                if (dt.Rows[0]["liveurl"] != null)
                {
                    if (dt.Rows[0]["liveurl"].ToString().Contains('='))
                    {
                        string liveUrl = dt.Rows[0]["liveurl"] == null ? "" : dt.Rows[0]["liveurl"].ToString();
                        hdnLiveUrl.Value = liveUrl.Split('=')[1];
                    }
                    else
                    {
                        hdnLiveUrl.Value = "";
                    }
                }
            }
            else
            {
                hdnLiveUrl.Value = "";
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region Webmethod
    [WebMethod(EnableSession = true)]
    public static string getDashboardDetails(string fromDate, string toDate)
    {
        try
        {
            fromDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            toDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            Pages_Dashboard_Dashboard db = new Pages_Dashboard_Dashboard();
            Helper Helper = new Helper();

            string userId = HttpContext.Current.Session["userId"].ToString();
            string Token = HttpContext.Current.Session["APIToken"].ToString();
            string requertUri = $"{db.getDashBoardDetailsUri}userId={userId}&fromDate={fromDate}&toDate={toDate}";
            Helper.APIGetJson(requertUri, Token, out int statusCode, out string Response);

            if (statusCode == 1)
            {
                return Response;
            }
            else
            {
                return "[]";
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    #region Logout
    protected void btnLogout_Click(object sender, EventArgs e)
    {
		Session["LoginStatus"] = "LO";
		string apitoken = Session["APIToken"].ToString();
		string Branch = Session["Master_Branch"].ToString();
		string categoryId = string.Empty;
		if (Session["categoryId"] != null || Session["categoryId"] == "")
		{
			categoryId = Session["categoryId"].ToString();
		}
		btnUserProfile.Visible = false;
		listMyprofile.Visible = false;
		lstLogout.Visible = false;
		Response.Redirect(Session["LogoutUrl"].ToString(), false);
		Session.Clear();
		Session["categoryId"] = categoryId;
		Session["APIToken"] = apitoken;
		Session["Master_Branch"] = Branch;
		Session["DashboardCheck"] = false;

	}
    #endregion



    #region myplan
    protected void lnkBtnMyPlan_Click(object sender, EventArgs e)
    {
        Session["myplan"] = "1";
        Response.Redirect("~/Pages/DietWorkOutNew/DietWorkOutNew.aspx");
    }
    #endregion


    //My Profile


    #region Get User Details 
    public void GetUserDetails()
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
                string Endpoint = "user?Input.userId=" + Session["userId"] + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);

                        if (dt.Rows[0]["firstName"] == "")
                        {
                            lblUserName.Text = dt.Rows[0]["mobileNo"].ToString();
                        }
                        else
                        {
                            lblUserName.Text = dt.Rows[0]["firstName"].ToString();
                        }

                        if (lblUserName.Text == "")
                        {
                            lblUserName.Text = dt.Rows[0]["mailId"].ToString();
                        }
                        btnUserProfile.Visible = true;
                        listMyprofile.Visible = true;
                        lstLogout.Visible = true;

                        if (dt.Rows[0]["photoLink"] == "")
                        {
                            userimg.ImageUrl = "../../Images/Login/UserProfile.png";
                        }
                        else
                        {
                            userimg.ImageUrl = dt.Rows[0]["photoLink"].ToString();
                        }

                        lblProUserFName.Text = dt.Rows[0]["firstName"].ToString();
                        if (lblProUserFName.Text == "")
                        {
                            DivProfile.Visible = false;
                            DivEditProfile.Visible = true;
                        }
                        lblProUserLName.Text = dt.Rows[0]["lastName"].ToString();
                        lblProGender.Text = dt.Rows[0]["gender"].ToString() == "M" ? "Male" : dt.Rows[0]["gender"].ToString() == "F" ? "Female" : "";


                        txtFirstName.Text = dt.Rows[0]["firstName"].ToString();
                        txtLastName.Text = dt.Rows[0]["lastName"].ToString();
                        rbtnGender.SelectedValue = dt.Rows[0]["gender"].ToString();
                        rbtnMaritalStatus.SelectedValue = dt.Rows[0]["maritalStatus"].ToString();
                        rbtnMaritalStatus.SelectedValue = dt.Rows[0]["maritalStatus"].ToString();
                        txtPincode.Text = dt.Rows[0]["zipcode"].ToString();
                        txtCity.Text = dt.Rows[0]["city"].ToString();
                        txtDistrict.Text = dt.Rows[0]["district"].ToString();
                        txtState.Text = dt.Rows[0]["state"].ToString();
                        hfCity.Value = dt.Rows[0]["city"].ToString();
                        hfDistrict.Value = dt.Rows[0]["district"].ToString();
                        hfState.Value = dt.Rows[0]["state"].ToString();
                        txtDOB.Text = dt.Rows[0]["dob"].ToString();
                        txtMobileNo.Text = dt.Rows[0]["mobileNo"].ToString();
                        txtMailId.Text = dt.Rows[0]["mailId"].ToString();
                        imgpreview.Src = dt.Rows[0]["photoLink"].ToString();
                        UserProfileImg.Src = dt.Rows[0]["photoLink"].ToString();
                        txtname.Text = dt.Rows[0]["firstName"].ToString();
                        txtDOB.Text = dt.Rows[0]["dob"].ToString();
                        ddlGender.SelectedValue = dt.Rows[0]["gender"].ToString();
                        if (imgpreview.Src == "")
                        {
                            imgpreview.Src = "../../Pages/MyProfile/User.png";
                        }
                        if (UserProfileImg.Src == "")
                        {
                            if (dt.Rows[0]["gender"].ToString() == "M")
                            {
                                UserProfileImg.Src = "../../Images/Login/user1.png";
                            }
                            else if (dt.Rows[0]["gender"].ToString() == "F")
                            {
                                UserProfileImg.Src = "../../Images/Login/UserFemale.jpg";
                            }
                            else
                            {
                                UserProfileImg.Src = "../../Images/Login/user1.png";
                            }
                        }
                        if (txtMobileNo.Text == "")
                        {
                            txtMobileNo.Enabled = true;
                        }
                        GetUserBodyTest();
                        MyBooking();
                        MySubscription();
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
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        Session["MyBody"] = "Y";
                        dt.Columns.Add("Weightrange");
                        dt.Columns.Add("Image");
                        lblProAge.Text = dt.Rows[0]["age"].ToString() + " " + "Years" + " " + " | ";
                        lblProWeight.Text = dt.Rows[0]["weight"].ToString() + " " + "kgs" + " | ";
                        lblProHeight.Text = dt.Rows[0]["height"].ToString() + "cm";
                        lblProBMI.Text = dt.Rows[0]["BMI"].ToString() + " " + "BMI" + " | ";
                        lblProBMR.Text = dt.Rows[0]["BMR"].ToString() + " " + "BMR" + " | ";
                        lblProTDEE.Text = dt.Rows[0]["TDEE"].ToString() + " " + "TDEE";
                        txtMBTDOB.Text = dt.Rows[0]["dob"].ToString();
                        txtage.Text = dt.Rows[0]["age"].ToString();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Convert.ToDecimal(dt.Rows[i]["BMI"].ToString()) < Convert.ToDecimal(18.5))
                            {
                                dt.Rows[i]["Weightrange"] = "Underweight";
                                dt.Rows[i]["Image"] = "../../Images/UserInBodyTest/lean.png";
                            }
                            else if ((Convert.ToDecimal(dt.Rows[i]["BMI"].ToString()) > Convert.ToDecimal(18.6)) &&
                               (Convert.ToDecimal(dt.Rows[i]["BMI"].ToString()) < Convert.ToDecimal(24.9)))
                            {
                                dt.Rows[i]["Weightrange"] = "Normal";
                                dt.Rows[i]["Image"] = "../../Images/UserInBodyTest/med.png";
                            }

                            else if (Convert.ToDecimal(dt.Rows[i]["BMI"].ToString()) > Convert.ToDecimal(25.0) &&
                                (Convert.ToDecimal(dt.Rows[i]["BMI"].ToString()) < Convert.ToDecimal(29.9)))
                            {
                                dt.Rows[i]["Weightrange"] = "Overweight";
                                dt.Rows[i]["Image"] = "../../Images/UserInBodyTest/overweight.png";
                            }
                            else
                            {
                                dt.Rows[i]["Weightrange"] = "Obese";
                                dt.Rows[i]["Image"] = "../../Images/UserInBodyTest/overweight.png";
                            }

                            dtlUserBodyTest.DataSource = dt;
                            dtlUserBodyTest.DataBind();
                            divGrid.Visible = true;

                        }


                    }
                    else
                    {
                        Session["MyBody"] = "N";
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
    #region My Profile Click
    protected void lnkMyProfile_Click(object sender, EventArgs e)
    {
        MyProfile.Visible = true;
    }
    #endregion
    #region My Profile Edit 
    protected void btnProEdit_Click(object sender, ImageClickEventArgs e)
    {
        DivProfile.Visible = false;
        DivEditProfile.Visible = true;
    }
    #region Btn Submit 
    protected void btnSubSubmit_Click(object sender, EventArgs e)
    {
        UpdateUser();
    }
    #endregion
    #region Btn Cancel
    protected void btnSubCancel_Click(object sender, EventArgs e)
    {
        DivProfile.Visible = true;
        DivEditProfile.Visible = false;
    }
    #endregion
    #region Update User Details 

    public void UpdateUser()
    {
        try
        {

            int StatusCodes;
            string ImageUrl;
            if (fuimage.HasFile)
            {
                helper.UploadImage(fuimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out StatusCodes, out ImageUrl);
            }
            else
            {
                if (imgpreview.Src == "Pages/MyProfile/User.png")
                {
                    ImageUrl = "";
                }
                else
                {
                    ImageUrl = imgpreview.Src;
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new UserClass()
                {

                    userId = Session["userId"].ToString(),
                    firstName = txtFirstName.Text,
                    lastName = txtLastName.Text,
                    gender = rbtnGender.SelectedValue,
                    addressLine1 = txtAddress2.Text,
                    addressLine2 = txtAddress1.Text,
                    zipcode = txtPincode.Text,
                    city = hfCity.Value,
                    district = hfDistrict.Value,
                    state = hfState.Value,
                    maritalStatus = rbtnMaritalStatus.SelectedValue,
                    dob = txtDOB.Text,
                    mobileNo = txtMobileNo.Text,
                    photoLink = ImageUrl,
                    mailId = txtMailId.Text,
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("user/update", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        GetUserDetails();
                        DivProfile.Visible = true;
                        DivEditProfile.Visible = false;
                        if (Session["MyBody"] == "N")
                        {
                            txtMBTDOB.Text = txtDOB.Text;
                            DivProfile.Visible = false;
                            DivEditProfile.Visible = false;
                            divGrid.Visible = false;
                            divUserForm.Visible = true;
                            btnAdd.Visible = false;
                            divMyBodyTest.Visible = true;
                        }
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region User Class
    public class UserClass
    {
        public string userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string maritalStatus { get; set; }
        public string dob { get; set; }
        public string mobileNo { get; set; }
        public string photoLink { get; set; }
        public string mailId { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion

    #endregion
    #region btn Profile Clsoe 
    protected void lnkProfileBtnClose_Click(object sender, EventArgs e)
    {
        MyProfile.Visible = false;
    }
    #endregion

    #region btn My Body Test Click
    protected void btnMyBodyTest_Click(object sender, EventArgs e)
    {
        DivProfile.Visible = false;
        divMyBodyTest.Visible = true;
    }
    #endregion
    #region My Body Test
    #region Btn Add Click 
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divGrid.Visible = false;
        divUserForm.Visible = true;
        btnAdd.Visible = false;
    }
    #endregion
    protected void OnClick(object sender, EventArgs e)
    {
        int repeatcolumn = Convert.ToInt32(hfColumnRepeat.Value);
        this.RsetepeatColumns(repeatcolumn);
    }
    private void RsetepeatColumns(int repeatcolumn = 5)
    {
        for (int i = 0; i < dtlUserBodyTest.Items.Count; i++)
        {
            dtlUserBodyTest.RepeatColumns = repeatcolumn;
        }
    }
    public void InsertUserEnroll()
    {
        try
        {
            DateTime now = DateTime.Now;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                UserInBodyTest Insert = new UserInBodyTest()
                {
                    userId = Session["userId"].ToString(),
                    firstName = txtname.Text,
                    lastName = "",
                    dob = txtMBTDOB.Text,
                    gender = ddlGender.SelectedValue,
                    WorkOutValue = ddlWorkOutDetails.SelectedValue,
                    WorkOutStatus = ddlWorkOutDetails.SelectedItem.Text,
                    age = txtage.Text,
                    weight = txtweight.Text,
                    height = txtheight.Text,
                    fatPercentage = txtfat.Text,
                    BMR = txtBMR.Text,
                    BMI = txtBMI.Text,
                    TDEE = txtTDEE.Text,
                    date = now.ToString("yyyy-MM-dd"),
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("userInBodyTest/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    string[] uid;
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Clear();
                        GetUserBodyTest();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "successalert('" + ResponseMsg.ToString() + "');", true);

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }

    #region Calculate BMI And BMR and TDEE
    #region Calculate BMI 
    public void CalBMI()
    {
        try
        {
            decimal height = Convert.ToDecimal((txtheight.Text)) / 100;
            decimal BMR = Convert.ToDecimal((txtweight.Text)) / (height * height);
            txtBMI.Text = BMR.ToString("0.00");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Calculate BMR and TDEE
    public void CalBMRandTDEE()
    {
        try
        {
            if (ddlGender.SelectedValue != "0")
            {
                int cal = 0;
                if (ddlGender.SelectedValue == "F")
                {
                    cal = Convert.ToInt32(655 + (Convert.ToDecimal(9.6) * Convert.ToDecimal(txtweight.Text))
                       + (Convert.ToDecimal(1.8) * Convert.ToDecimal(txtheight.Text))
                       - (Convert.ToDecimal(4.7) * Convert.ToDecimal(txtage.Text)));
                }
                else
                {
                    cal = Convert.ToInt32(66 + (Convert.ToDecimal(13.8) * Convert.ToDecimal(txtweight.Text))
                       + (Convert.ToDecimal(5.0) * Convert.ToDecimal(txtheight.Text))
                       - (Convert.ToDecimal(6.8) * Convert.ToDecimal(txtage.Text)));
                }
                if (ddlWorkOutDetails.SelectedValue != "0")
                {
                    decimal TDEE = Convert.ToInt32(Convert.ToDecimal(cal) * Convert.ToDecimal(ddlWorkOutDetails.SelectedValue));
                    txtBMR.Text = cal.ToString();
                    txtTDEE.Text = TDEE.ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "infoalert('Select WorkOut Details');", true);
                    return;
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "infoalert('Select Gender');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Work Out Details Selected Index Changed
    protected void ddlWorkOutDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtheight.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "infoalert('Enter Height');", true);
            return;
        }
        if (ddlGender.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "infoalert('Select Gender');", true);
            return;
        }
        if (txtweight.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "infoalert('Enter Weight');", true);
            return;
        }
        if (txtage.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "infoalert('Enter Age');", true);
            return;
        }
        else
        {
            CalBMI();
            CalBMRandTDEE();
        }
    }
    #endregion
    #region Weight Changed Event
    protected void txtweight_TextChanged(object sender, EventArgs e)
    {
        if (txtage.Text != "" && txtheight.Text != "" && ddlGender.SelectedValue != "0" && txtweight.Text != "" && ddlWorkOutDetails.SelectedValue != "0")
        {
            CalBMI();
            CalBMRandTDEE();
        }

    }
    #endregion
    #region Height Changed Event
    protected void txtheight_TextChanged(object sender, EventArgs e)
    {
        if (txtage.Text != "" && txtheight.Text != "" && ddlGender.SelectedValue != "0" && txtweight.Text != "" && ddlWorkOutDetails.SelectedValue != "0")
        {
            CalBMI();
            CalBMRandTDEE();
        }

    }

    #endregion
    #region Age Changed Event
    protected void txtage_TextChanged(object sender, EventArgs e)
    {
        if (txtage.Text != "" && txtheight.Text != "" && ddlGender.SelectedValue != "0" && txtweight.Text != ""
            && ddlWorkOutDetails.SelectedValue != "0")
        {
            CalBMI();
            CalBMRandTDEE();
        }

    }
    #endregion
    #region Gender Changed Event
    protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtage.Text != "" && txtheight.Text != "" && ddlGender.SelectedValue != "0" && txtweight.Text != ""
          && ddlWorkOutDetails.SelectedValue != "0")
        {
            CalBMI();
            CalBMRandTDEE();
        }
    }
    #endregion
    #endregion
    #region Clear
    public void Clear()
    {
        btnAdd.Visible = true;
        txtage.Text = "";
        txtBMI.Text = "";
        txtBMR.Text = "";
        txtMBTDOB.Text = "";
        txtfat.Text = "";
        txtheight.Text = "";
        txtname.Text = "";
        txtTDEE.Text = "";
        txtweight.Text = "";
        divUserForm.Visible = false;
        divGrid.Visible = true;
        lnkbtnMyBodytClose.Visible = true;
        btnMyBodyCancel.Visible = true;
    }
    #endregion
    #region Btn  Submit 
    protected void btnMyBodySubmit_Click(object sender, EventArgs e)
    {
        InsertUserEnroll();
    }
    #endregion
    #region Btn Cancel
    protected void btnMyBodyCancel_Click(object sender, EventArgs e)
    {
        Clear();
        DivProfile.Visible = true;
        divMyBodyTest.Visible = false;
    }
    #endregion
    #region UserInBodyTest

    public class UserInBodyTest
    {
        public string userId { get; set; }
        public string queryType { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dob { get; set; }
        public string weight { get; set; }
        public string height { get; set; }
        public string mobileNo { get; set; }
        public string fatPercentage { get; set; }
        public string age { get; set; }
        public string BMR { get; set; }
        public string BMI { get; set; }
        public string TDEE { get; set; }
        public string date { get; set; }
        public string gender { get; set; }
        public string WorkOutValue { get; set; }
        public string WorkOutStatus { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion
    #endregion
    #region btn My Body Test Close
    protected void lnkbtnMyBodytClose_Click(object sender, EventArgs e)
    {
        DivProfile.Visible = true;
        divMyBodyTest.Visible = false;
    }
    #endregion
    #region My Plan Click
    protected void lnkMyPlan_Click(object sender, EventArgs e)
    {
        Session["myplan"] = "1";
        Response.Redirect("~/Pages/DietWorkOutNew/DietWorkOutNew.aspx");
    }
    public void BindPlanName()
    {
        try
        {
            DateTime TodayDate = DateTime.Now;
            string s = TodayDate.DayOfWeek.ToString();
            string day = s.Substring(0, 2);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "UserBookingDetails?userId=" + Session["userId"].ToString() + "";


                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {

                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            Session["categoryId"] = dt.Rows[0]["categoryId"].ToString();
                            Session["categoryName"] = dt.Rows[0]["categoryName"].ToString();

                        }
                        else
                        {

                        }
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


    #region Get My Booking
    public void MyBooking()
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
                string Endpoint = "UserBookingDetails?userId=" + Session["userId"] + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        dtlMyBooking.DataSource = dt;
                        dtlMyBooking.DataBind();
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

    protected void btnMyBooking_Click(object sender, EventArgs e)
    {
        DivProfile.Visible = false;
        divMyBodyTest.Visible = false;
        DivMyBooking.Visible = true;
        DivBookingDetails.Visible = false;
    }

    protected void lnk_Click(object sender, EventArgs e)
    {
        DivBookingList.Visible = false;
        DivBookingDetails.Visible = true;
        LinkButton lnk = sender as LinkButton;
        DataListItem dtl = lnk.NamingContainer as DataListItem;
        HtmlControl div = dtl.FindControl("divMyDtlBooking") as HtmlControl;
        Label lbltotalAmount = dtl.FindControl("lbltotalAmount") as Label;
        Label lblUserName = dtl.FindControl("lblUserName") as Label;
        Label lblbookingDate = dtl.FindControl("lblbookingDate") as Label;
        Label lblfromDate = dtl.FindControl("lblfromDate") as Label;
        Label lblPlaneDuration = dtl.FindControl("lblPlaneDuration") as Label;
        Label lblcategoryName = dtl.FindControl("lblcategoryName") as Label;
        Label lblpaidAmount = dtl.FindControl("lblpaidAmount") as Label;
        Label lblprice = dtl.FindControl("lblprice") as Label;
        Label lbltaxAmount = dtl.FindControl("lbltaxAmount") as Label;


        lblMyBookingSummaryUserName.Text = lblUserName.Text;
        lblMyBookingBookingSummaryDate.Text = lblbookingDate.Text;
        lblMyBookingSummaryjoinDate.Text = lblfromDate.Text;
        lblMyBookingSummaryDuration.Text = lblPlaneDuration.Text;
        lblMyBookingSummaryPlan.Text = lblcategoryName.Text;
        lblMyBookingSummaryTotalAmt.Text = lbltotalAmount.Text;
        lblMyBookingSummaryTotal.Text = lbltotalAmount.Text;
        lblMyBookingSummaryPaidAmt.Text = "₹" + lblpaidAmount.Text;
        lblMyBookingSummaryAmt.Text = "₹" + lblprice.Text;
        lblMyBookingSummaryTax.Text = "₹" + lbltaxAmount.Text;
    }

    protected void lnkMyBookingClose_Click(object sender, EventArgs e)
    {
        DivProfile.Visible = true;
        divMyBodyTest.Visible = false;
        DivMyBooking.Visible = false;
    }

    protected void btnMySubs_Click(object sender, EventArgs e)
    {
        DivProfile.Visible = false;
        divMyBodyTest.Visible = false;
        DivMyBooking.Visible = false;
        DivMySubs.Visible = true;
        DivMySubsList.Visible = true;
    }
    #region Get My Subscription
    public void MySubscription()
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
                string Endpoint = "UserSubspBookingDetails?userId=" + Session["userId"] + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        DtlMySubs.DataSource = dt;
                        DtlMySubs.DataBind();
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
    protected void lnkMySubsClose_Click(object sender, EventArgs e)
    {
        DivProfile.Visible = true;
        DivMySubs.Visible = false;
        DivMySubsDetails.Visible = false;
    }
    protected void lnkMySubs_Click(object sender, EventArgs e)
    {
        DivMySubsList.Visible = false;
        DivMySubsDetails.Visible = true;
        LinkButton lnk = sender as LinkButton;
        DataListItem dtl = lnk.NamingContainer as DataListItem;
        HtmlControl div = dtl.FindControl("divMyDtlSubs") as HtmlControl;
        Label lbltotalAmount = dtl.FindControl("lbltotalAmount") as Label;
        Label lblUserName = dtl.FindControl("lblUserName") as Label;
        Label lblbookingDate = dtl.FindControl("lblbookingDate") as Label;
        Label lblfromDate = dtl.FindControl("lblfromDate") as Label;
        Label lblpaidAmount = dtl.FindControl("lblpaidAmount") as Label;
        Label lblprice = dtl.FindControl("lblprice") as Label;
        Label lbltaxAmount = dtl.FindControl("lbltaxAmount") as Label;
        Label lblpackageName = dtl.FindControl("lblpackageName") as Label;
        Label lblPlaneDuration = dtl.FindControl("lblPlaneDuration") as Label;


        lblMySubsUserName.Text = lblUserName.Text;
        lblMySubsBookingDate.Text = lblbookingDate.Text;
        lblMySubsSummaryjoinDate.Text = lblfromDate.Text;
        lblMySubsSummaryTotalAmt.Text = lbltotalAmount.Text;
        lblMySubsSummaryTotal.Text = lbltotalAmount.Text;
        lblMySubsSummaryPaidAmt.Text = "₹" + lblpaidAmount.Text;
        lblMySubsSummaryAmt.Text = "₹" + lblprice.Text;
        lblMySubsSummaryTax.Text = "₹" + lbltaxAmount.Text;
        lblMySubsSummaryTax.Text = "₹" + lbltaxAmount.Text;
        lblMySubspackageName.Text =  lblpackageName.Text;
        lblMySubsPlanDuration.Text =  lblPlaneDuration.Text + " "+ "Days";
    }


    #region Get GymownerLogo
    public string GetGymownerLogo()
    {
        string logourl = string.Empty;
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "ownerMaster/IndividualOwner?gymOwnerId=" + Session["gymOwnerId"] + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        //btnHomeFromDashBoard.ImageUrl = dt.Rows[0]["logoUrl"].ToString();
                        lblGymName.Text = dt.Rows[0]["gymName"].ToString();
                        btnHome.Style.Add("background-image", dt.Rows[0]["logoUrl"].ToString());

                    }
                    else
                    {
                        logourl = "../images/master/logoFitness.svg";
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
        return logourl;
    }
    #endregion
}