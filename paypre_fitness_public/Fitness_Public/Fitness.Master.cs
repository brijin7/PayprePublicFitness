using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Runtime.Remoting.Messaging;
using System.Activities.Expressions;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;
using System.Activities.Statements;

public partial class Fitness : System.Web.UI.MasterPage
{
    IFormatProvider obj = new System.Globalization.CultureInfo("en-GB", true);
    Helper helper = new Helper();
	readonly private string BaseUri;
	readonly private string CheckLogin;
	readonly private string CheckReLogin;
	readonly string checkUserHasLoginUri;
    readonly string checkUserbookingHasApprovedUri;
    //Notofication Related
    readonly string getNotificationUrlUri;
    readonly string getunreadedNotificationUrlUri;
    //Notofication Related
    private string Token;
	IFormatProvider objEnglishDate = new System.Globalization.CultureInfo("en-GB", true);

	public Fitness()
	{
		BaseUri = $"{ConfigurationManager.AppSettings["BaseUrl"].Trim()}";
		CheckLogin = $"{BaseUri}login/checkLogin";
		CheckReLogin = $"{BaseUri}login/checkReLogin";
		checkUserHasLoginUri = $"{BaseUri}UserBookingDetails?";
        checkUserbookingHasApprovedUri = $"{BaseUri}GetUserBookingDetailsBasedOnUserId?";
        //Notofication Related
        getNotificationUrlUri = $"{BaseUri}UserNotification/updateUserNotification";
        getunreadedNotificationUrlUri = $"{BaseUri}UserNotification/GetUserNotification?";
        //Notofication Related
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {            

                if (Session["DashboardCheck"] != null && (bool)Session["DashboardCheck"] == true)
				{
					btnHomeFromDashBoard.Visible = true;
					btnHome.Visible = false;
				}
				else
				{
					btnHomeFromDashBoard.Visible = false;
					btnHome.Visible = true;
				}


				if (Session["LoginStatus"] == "L")
                {
                    Session["LogoutUrl"] = System.Configuration.ConfigurationManager.AppSettings["LogoutUrl"].Trim();
                    GetUserDetails();
                }
                else
                {
                    btnlogin.Visible = true;
                    btnWorkout.Visible = false;
                    btnDiet.Visible = false;
                }
                LblBranch.Text = Session["Master_Branch"].ToString().Trim();
                if (Session["checkUserhasbooking"] != null && (bool)Session["checkUserhasbooking"])
                {
                    
                    btnMyPage.Visible = true;
                }
                else
                {
                  
                    btnMyPage.Visible = false;
                }
                Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
                GetGymownerLogo();
                Session["ImageUrl"] = System.Configuration.ConfigurationManager.AppSettings["ImageUrl"].Trim();

            }

            hfNotificationPosturl.Value = getNotificationUrlUri;
            hfNotificationURl.Value = getunreadedNotificationUrlUri;
            if (Session["userId"] != null)
            {
                if(Session["userRole"].ToString().Trim() == "User")
                {
                    lnkNotification.Visible = true;
                    hfNotificationURlData.Value = "readstatus=N&userId=" + Session["userId"].ToString().Trim();
                }
                else
                {
                    lnkNotification.Visible = false;
                }
                
            }
            else
            {
                lnkNotification.Visible = false;
            }
            
            hfTokenURl.Value = Session["APIToken"].ToString();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    /// <summary>
    /// This Property is used to bind Master Page Dropdown in Homepage
    /// </summary>
    /// 
    public Label LblBranch
    {
        get
        {
            return Master_lblBranch;
        }
    }

    public DropDownList DdlBranch
    {
        get
        {
            return ddlBranch_Master_Nav;
        }
    }
    public HtmlGenericControl Master_listMyprofile
    {
        get
        {
            return listMyprofile;
        }
    }
    public HtmlGenericControl Master_btnlogin
    {
        get
        {
            return btnlogin;
        }
    }
    public HtmlAnchor Master_btnUserProfile
    {
        get
        {
            return btnUserProfile;
        }
    }
    public Label Master_lblUserName
    {
        get
        {
            return lblUserName;
        }
    }
    public Image Master_userimg
    {
        get
        {
            return userimg;
        }
    }
    public LinkButton Master_btnHome
    {
        get
        {
            return btnHome;

        }
    }
    public HtmlGenericControl Master_divMyBodyTest
    {
        get
        {
            return divMyBodyTest;

        }
    }
    public HtmlGenericControl Master_MyProfile
    {
        get
        {
            return MyProfile;

        }
    }
    public HtmlGenericControl Master_DivProfile
    {
        get
        {
            return DivProfile;

        }
    }
    public HtmlGenericControl Master_divGrid
    {
        get
        {
            return divGrid;

        }
    }
    public HtmlGenericControl Master_divUserForm
    {
        get
        {
            return divUserForm;

        }
    }
    public LinkButton Master_btnAdd
    {
        get
        {
            return btnAdd;

        }
    }


    public Button Master_btnMyBodyCancel
    {
        get
        {
            return btnMyBodyCancel;

        }
    }
    public LinkButton Master_lnkbtnMyBodytClose
    {
        get
        {
            return lnkbtnMyBodytClose;

        }
    }

    public HtmlAnchor Master_lnkbtnClasses
    {
        get
        {
            return lnkbtnClasses;

        }
    }
    public HtmlAnchor Master_lnkbtnSubscription
    {
        get
        {
            return lnkbtnSubscription;

        }
    }

    public HtmlAnchor Master_lnkbtnTestimonials
    {
        get
        {
            return lnkbtnTestimonials;

        }
    }

    public HtmlImage Master_UserProfileImg
    {
        get
        {
            return UserProfileImg;

        }
    }
    public HtmlGenericControl Master_DivEditProfile
    {
        get
        {
            return DivEditProfile;

        }
    }

    public HtmlAnchor Master_btnDiet
    {
        get
        {
            return btnDiet;

        }
    }
    public HtmlAnchor Master_btnWorkout
    {
        get
        {
            return btnWorkout;

        }
    }
    public HtmlAnchor Master_btnMyPage
    {
        get
        {
            return btnMyPage;

        }
    }


    public HiddenField Master_hfImageUrl
    {
        get
        {
            return hfImageUrl;

        }
    }

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
                string Endpoint = "user?userId=" + Session["userId"] + "";
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
                        Session["UserDetailsName"] = lblUserName.Text;
                        btnlogin.Visible = false;
                        btnUserProfile.Visible = true;
                        listMyprofile.Visible = true;
                        lstLogout.Visible = true;

                        if (dt.Rows[0]["photoLink"] == "")
                        {
                            userimg.ImageUrl = "Images/Login/UserProfile.png";
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
                            Session["photoLink"] = dt.Rows[0]["photoLink"].ToString();
                            txtname.Text = dt.Rows[0]["firstName"].ToString();
                            ddlGender.SelectedValue = dt.Rows[0]["gender"].ToString();
                      
                      
                        if (imgpreview.Src == "")
                        {
                            imgpreview.Src = "Pages/MyProfile/User.png";
                            Session["photoLink"]= "Pages/MyProfile/User.png";
                        }
                        if (UserProfileImg.Src == "")
                        {
                            if (dt.Rows[0]["gender"].ToString() == "M")
                            {
                                UserProfileImg.Src = "Images/Login/user1.png";
                            }
                            else if (dt.Rows[0]["gender"].ToString() == "F")
                            {
                                UserProfileImg.Src = "Images/Login/UserFemale.jpg";
                            }
                            else
                            {
                                UserProfileImg.Src = "Images/Login/user1.png";
                            }
                        }
                        if (txtMobileNo.Text == "")
                        {
                            txtMobileNo.Enabled = true;
                        }
                        GetUserBodyTest();
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
                        txtname.Text = dt.Rows[0]["UserName"].ToString();   
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Convert.ToDecimal(dt.Rows[i]["BMI"].ToString()) < Convert.ToDecimal(18.5))
                            {
                                dt.Rows[i]["Weightrange"] = "Underweight";
                                dt.Rows[i]["Image"] = "Images/UserInBodyTest/lean.png";
                            }
                            else if ((Convert.ToDecimal(dt.Rows[i]["BMI"].ToString()) > Convert.ToDecimal(18.6)) &&
                               (Convert.ToDecimal(dt.Rows[i]["BMI"].ToString()) < Convert.ToDecimal(24.9)))
                            {
                                dt.Rows[i]["Weightrange"] = "Normal";
                                dt.Rows[i]["Image"] = "Images/UserInBodyTest/med.png";
                            }

                            else if (Convert.ToDecimal(dt.Rows[i]["BMI"].ToString()) > Convert.ToDecimal(25.0) &&
                                (Convert.ToDecimal(dt.Rows[i]["BMI"].ToString()) < Convert.ToDecimal(29.9)))
                            {
                                dt.Rows[i]["Weightrange"] = "Overweight";
                                dt.Rows[i]["Image"] = "Images/UserInBodyTest/overweight.png";
                            }
                            else
                            {
                                dt.Rows[i]["Weightrange"] = "Obese";
                                dt.Rows[i]["Image"] = "Images/UserInBodyTest/overweight.png";
                            }

                            dtlUserBodyTest.DataSource = dt;
                            dtlUserBodyTest.DataBind();
                            divGrid.Visible = true;
                            divUserForm.Visible = false;
                            btnMyBodyCancel.Visible = true;
                            btnAdd.Visible = true;

                        }


                    }
                    else
                    {
                        divUserForm.Visible = true;
                        divGrid.Visible = false;
                        btnMyBodyCancel.Visible= false;
                        btnAdd.Visible = false;
                        Session["MyBody"] = "N";
                    }
                }
                else
                {
                    divUserForm.Visible = true;
                    divGrid.Visible = false;
                    btnMyBodyCancel.Visible = false;
                    btnAdd.Visible = false;
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
    #region Btn Logout Click
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["LoginStatus"] = "LO";
        string apitoken = Session["APIToken"].ToString();
        string Branch = Session["Master_Branch"].ToString();
        string ImageUrl = Session["ImageUrl"].ToString();
        string BaseUrl = Session["BaseUrl"].ToString();
        string categoryId = string.Empty;
        if (Session["categoryId"] != null || Session["categoryId"] == "")
        {
             categoryId = Session["categoryId"].ToString();
        }    
        btnlogin.Visible = true;
        btnUserProfile.Visible = false;
        listMyprofile.Visible = false;
        lstLogout.Visible = false;
        Response.Redirect(Session["LogoutUrl"].ToString(), false);
        Session.Clear();
        Session["categoryId"] = categoryId;
        Session["APIToken"] = apitoken;
        Session["Master_Branch"] = Branch;
		Session["DashboardCheck"] = false;
        Session["ImageUrl"] = ImageUrl;
        Session["BaseUrl"] = BaseUrl;
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

            //int StatusCodes;
            //string ImageUrl;
            //if (fuimage.HasFile)
            //{
            //    helper.UploadImage(fuimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out StatusCodes, out ImageUrl);
            //}
            //else
            //{
            //    if (imgpreview.Src == "Pages/MyProfile/User.png")
            //    {
            //        ImageUrl = "";
            //    }
            //    else
            //    {
            //        ImageUrl = imgpreview.Src;
            //    }
            //}

            int StatusCodes;
            string ImageUrl;
            if (hfImageUrls.Value != "")
            {
                ImageUrl = hfImageUrls.Value;
                // string File = Server.MapPath(imgGalleryPhotoPrev.ImageUrl.ToString());
                //helper.UploadImages(File, Session["BaseUrl"].ToString().Trim() + "UploadImage", out StatusCodes, out ImageUrl);
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
                        if (txtweight.Text == "")
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
  

    #region btn My Body Test Click
    protected void btnMyBodyTest_Click(object sender, EventArgs e)
    {
        DivProfile.Visible = false;
        divMyBodyTest.Visible = true;
        GetUserBodyTest();
       //GetUserDetails();

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
        //DivProfile.Visible = true;
        //divMyBodyTest.Visible = false;
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

    #region Get GymownerLogo
    public string GetGymownerLogo()
    {
        string logourl=string.Empty;
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



	//Login Popup
	#region Login Click
	protected void btnSendOTP_Click(object sender, EventArgs e)
	{
		txtMobileNoLogin.Enabled = false;
		SendOtp();
	}
	#endregion
	#region Get UserDetails
	public void GetLoginUserDetails(string LoginOrReLogin = "Login")
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

				if (txtMobileNoLogin.Text != "")
				{
					string sUrl = Session["BaseUrl"].ToString().Trim()
						 + "signIn?mobileNo=" + txtMobileNoLogin.Text + "";

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

						if (!CheckLoginOrReLogin(LoginOrReLogin, dt.Rows[0]["userId"].ToString()))
						{
							ShowSinglLoginPopUp("Show");
							return;
						}

						Session["userId"] = dt.Rows[0]["userId"].ToString();
						Session["roleId"] = dt.Rows[0]["roleId"].ToString();
						Session["userRole"] = dt.Rows[0]["roleName"].ToString();
						Session["userName"] = dt.Rows[0]["UserName"].ToString();
						Session["mailId"] = dt.Rows[0]["mailId"].ToString();
						Session["mobileNo"] = dt.Rows[0]["mobileNo"].ToString();
						Session["branchName"] = dt.Rows[0]["branchName"].ToString();
						if (Session["userRole"].ToString().Trim() == "Sadmin")
						{
							Session["approvalStatus"] = "A";
							Session["branchId"] = 0;
							Session["gymOwnerId"] = 0;
							Session["branchName"] = "";
							//http://localhost:51781/
							//~/fitnessdep/
							Response.Redirect("~/fitnessdep/index.aspx?qUserId=" + Session["userId"].ToString() + "&qUserRole=" + Session["userRole"].ToString() +
								"&qUserName=" + Session["userName"].ToString() + "&qroleId=" + Session["roleId"].ToString() + "&qmailId=" + Session["mailId"].ToString() +
								"&qbranchName=" + Session["branchName"].ToString() + "&qapprovalStatus=" + Session["approvalStatus"].ToString() + "&qbranchId=" + Session["branchId"].ToString() +
								 "&qgymOwnerId=" + Session["gymOwnerId"].ToString() + "&qAPIToken=" + Session["APIToken"].ToString() + "&qmobileNo=" + Session["mobileNo"].ToString() + "", false);
						}
						else if (Session["userRole"].ToString().Trim() == "GymOwner")
						{
							Session["approvalStatus"] = "A";
							Session["gymOwnerId"] = dt.Rows[0]["gymOwnerId"].ToString();
							Session["branchId"] = 0;
							Session["branchName"] = "";
							Response.Redirect("~/fitnessdep/index.aspx?qUserId=" + Session["userId"].ToString() + "&qUserRole=" + Session["userRole"].ToString() +
								"&qUserName=" + Session["userName"].ToString() + "&qroleId=" + Session["roleId"].ToString() + "&qmailId=" + Session["mailId"].ToString() +
								"&qbranchName=" + Session["branchName"].ToString() + "&qapprovalStatus=" + Session["approvalStatus"].ToString() + "&qbranchId=" + Session["branchId"].ToString() +
								 "&qgymOwnerId=" + Session["gymOwnerId"].ToString() + "&qAPIToken=" + Session["APIToken"].ToString() + "&qmobileNo=" + Session["mobileNo"].ToString() + "", false);
						}
						else if (Session["userRole"].ToString().Trim() == "Employee" || Session["userRole"].ToString().Trim() == "Admin")
						{
							Session["approvalStatus"] = "W";
							Session["gymOwnerId"] = dt.Rows[0]["gymOwnerId"].ToString();
							Session["branchId"] = dt.Rows[0]["branchId"].ToString();
							Response.Redirect("~/fitnessdep/index.aspx?qUserId=" + Session["userId"].ToString() + "&qUserRole=" + Session["userRole"].ToString() +
								"&qUserName=" + Session["userName"].ToString() + "&qroleId=" + Session["roleId"].ToString() + "&qmailId=" + Session["mailId"].ToString() +
								"&qbranchName=" + Session["branchName"].ToString() + "&qapprovalStatus=" + Session["approvalStatus"].ToString() + "&qbranchId=" + Session["branchId"].ToString() +
								 "&qgymOwnerId=" + Session["gymOwnerId"].ToString() + "&qAPIToken=" + Session["APIToken"].ToString() + "&qmobileNo=" + Session["mobileNo"].ToString() + "", false);
						}
						else if (Session["userRole"].ToString().Trim() == "User")
						{
                            //CheckUserhasbookingasApproved(Session["userId"].ToString());
                            //CheckUserhasbooking(Session["userId"].ToString());
                            CheckUserhasbookingasApproved(Session["userId"].ToString());
                            if (CheckUserhasbooking(Session["userId"].ToString()))
                            {                               
                                //Response.Redirect("~/Pages/Dashboard/Dashboard.aspx", false);
                                Response.Redirect("~/Pages/User/MyPage.aspx", false);
                              
                            }
                            else
                            {                               
                                Response.Redirect("~/Home.aspx", false);
                            }
                            // if (CheckUserhasbooking(Session["userId"].ToString()))
                            //{
                            // //Response.Redirect("~/Pages/Dashboard/Dashboard.aspx", false);
                            //Response.Redirect("~/Pages/User/MyPage.aspx", false);
                            //}
                            //else
                            //{
                            //	Response.Redirect("~/Pages/User/MyPage.aspx", false);
                            //}
                            //Response.Redirect("~/Pages/User/MyPage.aspx", false);
                            Session["LoginStatus"] = "L";
						}
                        else if (Session["userRole"].ToString().Trim() == "Trainer")
                        {
                            Session["approvalStatus"] = "W";
                            Session["gymOwnerId"] = dt.Rows[0]["gymOwnerId"].ToString();
                            Session["branchId"] = dt.Rows[0]["branchId"].ToString();
                            //Response.Redirect("~/DashBoard.aspx", false);
                            Response.Redirect("~/fitnessdep/index.aspx?qUserId=" + Session["userId"].ToString() + "&qUserRole=" + Session["userRole"].ToString() +
                              "&qUserName=" + Session["userName"].ToString() + "&qroleId=" + Session["roleId"].ToString() + "&qmailId=" + Session["mailId"].ToString() +
                              "&qbranchName=" + Session["branchName"].ToString() + "&qapprovalStatus=" + Session["approvalStatus"].ToString() + "&qbranchId=" + Session["branchId"].ToString() +
                               "&qgymOwnerId=" + Session["gymOwnerId"].ToString() + "&qAPIToken=" + Session["APIToken"].ToString() + "&qmobileNo=" + Session["mobileNo"].ToString() + "", false);
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
					txtMobileNoLogin.Text = string.Empty;
					txtMail.Text = string.Empty;
					txtotp.Text = string.Empty;
					txtMobileNoLogin.Enabled = true;
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
					mobileNo = txtMobileNoLogin.Text

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
						ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
					}
					else
					{
						ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('Please Check the Mobile Number!');", true);
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
					mobileNo = txtMobileNoLogin.Text,
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
						GetLoginUserDetails();
					}
					else
					{
						txtotp.Text = string.Empty;
						btnResend.Text = "Resend OTP";
						ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('Please Check the OTP');", true);
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
				if (txtMobileNoLogin.Text != "")
				{
					email = txtMobileNoLogin.Text;
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
						mobileNo = txtMobileNoLogin.Text,
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
                        GetLoginUserDetails();
                        //GetUserDetails();
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
					else
					{
						txtotp.Visible = false;
						btnCfmOtp.Visible = false;
						btnResend.Visible = false;
						lblOtp.Visible = false;
						txtMobileNoLogin.Text = string.Empty;
						txtMail.Text = string.Empty;
						txtotp.Text = string.Empty;
						txtMobileNoLogin.Enabled = true;
						ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ResponseMsg.ToString().Trim() + "');", true);
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
	#region Resend
	protected void btnResend_Click(object sender, EventArgs e)
	{
		SendOtp();
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
			ScriptManager.RegisterStartupScript(this, GetType(), "alert", "infoalert('Enter OTP !');", true);
		}

	}
	#endregion
	#region txtMail
	protected void txtMail_TextChanged(object sender, EventArgs e)
	{
		txtMobileNoLogin.Text = string.Empty;
		GetUserDetails();
	}
	#endregion
	#region SingleLogin
	/// <summary>
	/// this method is used to close the single login popup
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void LnkbtnSingleLoginClose_Click(object sender, EventArgs e)
	{
		try
		{
			ShowSinglLoginPopUp(ShowOrHide: "Hide");
		}
		catch (Exception Ex)
		{
			//ShowErrorPopup(Ex);
		}
	}
	/// <summary>
	/// this method is used relogin
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void btnSingleLoginPopUpOk_Click(object sender, EventArgs e)
	{
		try
		{
			GetLoginUserDetails(LoginOrReLogin: "ReLogin");
		}
		catch (Exception Ex)
		{
			//ShowErrorPopup(Ex);
		}
	}
	/// <summary>
	/// this method is used to show or hide single login popup
	/// </summary>
	/// <param name="ShowOrHide">pass "Show" or "Hide"</param>
	private void ShowSinglLoginPopUp(string ShowOrHide)
	{
		if (ShowOrHide == "Show")
		{
			SingleLogin_Overlay.Attributes.Add("class", "SingleLogin_Overlay d-block");
			SingleLogin_Popup.Attributes.Add("class", "SingleLogin_Popup d-block");
		}
		else
		{
			SingleLogin_Overlay.Attributes.Add("class", "SingleLogin_Overlay d-none");
			SingleLogin_Popup.Attributes.Add("class", "SingleLogin_Popup d-none");
		}
	}
	/// <summary>
	/// this method is used to check if the user is already logged in
	/// </summary>
	private bool CheckLoginOrReLogin(string LoginOrReLogin, string UserId)
	{
		try
		{
			string SessionId = Session.SessionID.Trim();
			int StatusCode = 0;
			string Response = null;
			if (LoginOrReLogin == "Login")
			{
				UpdateSingleLogin_In Input = new UpdateSingleLogin_In()
				{
					SessionId = SessionId,
					UserId = int.Parse(UserId.Trim())
				};
				helper.APIpost<UpdateSingleLogin_In>(CheckLogin, Session["APIToken"].ToString(), Input, out StatusCode, out Response);
			}
			else
			{
				UpdateSingleReLogin_In Input = new UpdateSingleReLogin_In()
				{
					SessionId = SessionId,
					UserId = int.Parse(UserId.Trim())
				};
				helper.APIpost<UpdateSingleReLogin_In>(CheckReLogin, Session["APIToken"].ToString(), Input, out StatusCode, out Response);
			}

			if (StatusCode == 1)
			{
				if (SessionId == Response.Trim())
				{
					return true;
				}
				return false;
			}
			else
			{
				return false;
			}
		}
		catch (Exception)
		{
			throw;
		}
	}
	#endregion
	#region Alerts
	public void ShowSuccessPopup(string Message)
	{

		ScriptManager.RegisterStartupScript(this, GetType(), "alert", "successalert('`" + Message.Trim() + "`);", true);
	}
	public void ShowInfoPopup(string Message)
	{
		ScriptManager.RegisterStartupScript(this, GetType(), "alert", "infoalert('`" + Message.Trim() + "`);", true);
	}
	#endregion
	#region Login And ReLogin Input Class
	public class UpdateSingleLogin_In
	{
		public int UserId { get; set; }
		public string SessionId { get; set; }
	}
	public class UpdateSingleReLogin_In
	{
		public int UserId { get; set; }
		public string SessionId { get; set; }
	}
    #endregion
    #region checkUserhasbooking
    public bool CheckUserhasbooking(string userId)
    {
        try
        {
            string requestUri = $"{checkUserHasLoginUri}userId={userId}";
            helper.APIGet(requestUri, Session["APIToken"].ToString(), out DataTable dt, out int statusCode, out string response);

            if (statusCode == 1)
            {
                Session["checkUserhasbooking"] = true;
                Session["liveUrlbranchId"] = dt.Rows[0]["branchId"].ToString();
                Session["userBookingtraningMode"] = dt.Rows[0]["traningMode"].ToString();
                Session["dashBoardBranchName"] = dt.Rows[0]["branchName"].ToString();
                Session["bookingId"] = dt.Rows[0]["bookingId"].ToString();
                return true;
            }
            else
            {
                Session["checkUserhasbooking"] = false;
                Session["userBookingBranchId"] = "";
                Session["dashBoardBranchName"] = "";
                return false;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region checkUserhasbookingasApproved
    public bool CheckUserhasbookingasApproved(string userId)
    {
        try
        {
            btnMyPage.Visible = false;
            string requestUri = $"{checkUserbookingHasApprovedUri}userId={userId}";
            helper.APIGet(requestUri, Session["APIToken"].ToString(), out DataTable dt, out int statusCode, out string response);

            if (statusCode == 1)
            {
                Session["checkUserhasbookingasApproved"] = true;
                Session["SlotTime"] = dt.Rows[0]["SlotTime"].ToString();
               Session["TrainerName"] = dt.Rows[0]["TrainerName"].ToString();
               Session["traningMode"] = dt.Rows[0]["traningMode"].ToString();
               Session["TrainermobileNo"] = dt.Rows[0]["TrainermobileNo"].ToString();
               Session["categoryName"] = dt.Rows[0]["categoryName"].ToString();
               Session["categoryId"] = dt.Rows[0]["categoryId"].ToString();
               Session["userName"] = dt.Rows[0]["userName"].ToString();
                Session["trainerId"] = dt.Rows[0]["trainerId"].ToString();
                //Session["dashBoardBranchName"] = dt.Rows[0]["branchName"].ToString();

                btnWorkout.Visible = true;
                btnDiet.Visible = true;
                btnMyPage.Visible = true;
                return true;
            }
            else
            {
                Session["checkUserhasbookingasApproved"] = false;
                Session["SlotTime"] = "";
                Session["TrainerName"] = "";
                Session["traningMode"] = "";
                Session["TrainermobileNo"] = "";
                Session["categoryName"] = "";
                Session["trainerId"] = "";
                btnWorkout.Visible = false;
                btnDiet.Visible = false;
                btnMyPage.Visible = true;
                //Session["userBookingBranchId"] = "";
                //Session["dashBoardBranchName"] = "";
                return false;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion


    //Login Popup
    protected void btnManinLogin_Click(object sender, EventArgs e)
	{
        DivLogin.Visible = true;
      
        ContentPlaceHolder content = (ContentPlaceHolder)FindControl("FitnessContent");
        if (content != null)
        {
            HtmlGenericControl divTrainerDetails = (HtmlGenericControl)content.FindControl("divtrainerdetails");
            if (divTrainerDetails != null)
            {
                divTrainerDetails.Visible = false;
            }
        }
    }

    protected void btnfeedbacksubmit_Click(object sender, EventArgs e)
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
                UserTestimonials Insert = new UserTestimonials()
                {
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    bookingId = Session["bookingId"].ToString(),
                    imageUrl = Session["photoLink"].ToString(),
                    feedbackRating = ddlrating.SelectedValue,
                    feedbackComment = txtfeedbackcomment.Text,
                    dispayStatus = dispalaystatus.SelectedValue,
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("userTestimonials/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    string[] uid;
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Clear();
                        ClearTestimonials();
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

    #region UserInBodyTest

    public class UserTestimonials
    {
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string bookingId { get; set; }
        public string imageUrl { get; set; }
        public string feedbackRating { get; set; }
        public string feedbackComment { get; set; }
        public string dispayStatus { get; set; }
        public string createdBy { get; set; }
    }
    #endregion

    #region Get User Testimonial 
    public void GetUserTestimonial()
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
                string Endpoint = "userTestimonials?bookingId=" + Session["bookingId"].ToString();
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        Divtestimonialcontent.Visible = false;
                        divgridtestimonial.Visible = true;
                        DataListtestimonial.DataSource = dt;
                        DataListtestimonial.DataBind();
                        btnfeedbackcancel.Visible = true;

                    }
                    else
                    {
                        Divtestimonialcontent.Visible = true;
                        divgridtestimonial.Visible = false;
                        btnfeedbackcancel.Visible = false;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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

    #region ClearTestimonials
    public void ClearTestimonials()
    {
        
        txtfeedbackcomment.Text = "";
        ddlrating.ClearSelection();
        dispalaystatus.ClearSelection();
        divgridtestimonial.Visible = false;
        DivMyfeedback.Visible = false;
        divGrid.Visible = true;
        DivProfile.Visible = true;
        divMyBodyTest.Visible = false;
    }
    #endregion

    #region btn My Body Test Click
    protected void btnfeedback_Click(object sender, EventArgs e)
    {
        divgridtestimonial.Visible = true;
        Divtestimonialcontent.Visible = false;
        if (Session["bookingId"] != null)
        {
            GetUserTestimonial();
        }
        else
        {
            Divtestimonialcontent.Visible = true;
            divgridtestimonial.Visible = false;
            btnfeedbackcancel.Visible = false;
        }
        DivProfile.Visible = false;
        divMyBodyTest.Visible = false;
        DivMyfeedback.Visible = true;
        divMyattendance.Visible = false;
        Divslotswapping.Visible = false;

    }
    #endregion

    protected void btnattendance_Click(object sender, EventArgs e)
    {
        divGridView.Visible = true;
        divattendanceentry.Visible = false;
        BindUserAttendance();
        DivProfile.Visible = false;
        divMyBodyTest.Visible = false;
        DivMyfeedback.Visible = false;
        divMyattendance.Visible = true;
        Divslotswapping.Visible = false;

    }

    protected void btnswapping_Click(object sender, EventArgs e)
    {
        divswapping.Visible = true;
        divswappentry.Visible = false;
        BindUserSwapSlots();
        GetUsersPlans();
        BindWorkingSlot();
        DivProfile.Visible = false;
        divMyBodyTest.Visible = false;
        DivMyfeedback.Visible = false;
        divMyattendance.Visible = false;
        Divslotswapping.Visible = true;       
    }

    #region Bind User Attendance

    #region btn Profile Clsoe 
    protected void lnkProfileBtnClose_Click(object sender, EventArgs e)
    {
        MyProfile.Visible = false;
    }
    #endregion

    public void BindUserAttendance()
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
                string Endpoint = "userAttendance?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                    "&branchId=" + Session["branchId"].ToString() + "&trainerId=" + Session["trainerId"].ToString() + "&userId=" + Session["userId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvTrainer.DataSource = dt;
                        gvTrainer.DataBind();
                        divGridView.Visible = true;
                        divattendanceentry.Visible = false;
                    }
                    else
                    {
                        gvTrainer.DataBind();
                        divGridView.Visible = false;
                        divattendanceentry.Visible = true;
                    }
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                }
                else
                {
                    divGridView.Visible = false;
                    divattendanceentry.Visible = true;
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Errorresponse.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion

    //protected void addattendance_Click(object sender, EventArgs e)
    //{
    //    divGridView.Visible = false;
    //    divattendanceentry.Visible = true;

    //}

    protected void btnattendancesubmit_Click(object sender, EventArgs e)
    {
        UpdateTrainer();
    }

    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = sender as ImageButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        Label lbluniqueId = (Label)gvrow.FindControl("lbluniqueId");
        Label lblempId = (Label)gvrow.FindControl("lblempId");
        Label lblOutTime = (Label)gvrow.FindControl("lblOutTime");
        Label lblinTime = (Label)gvrow.FindControl("lblinTime");
        divouttime.Visible = false;
        if (lblOutTime.Text != "-")
        {
            txtShiftEndTime.Text = lblOutTime.Text.Trim();
        }
        if (lblinTime.Text != "-")
        {
            txtShiftStartTime.Text = lblinTime.Text.Trim();
            divouttime.Visible = true;
        }

        ViewState["TrainerId"] = lbluniqueId.Text.Trim();
        divlogdate.Visible = false;
        divintime.Visible = true;
        divGridView.Visible = false;
        divattendanceentry.Visible = true;
    }

    #region Update Function 
    public void UpdateTrainer()
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
                var Insert = new TrainerInsert()
                {
                    UniqueId = ViewState["TrainerId"].ToString(),
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    userId = Session["userId"].ToString(),
                    inTime = txtShiftStartTime.Text,
                    OutTime = txtShiftEndTime.Text,
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("userAttendance/update", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindUserAttendance();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);;
                }
                ClearAttendance();
                BindUserAttendance();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Trainer Insert
    public class TrainerInsert
    {
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string UniqueId { get; set; }
        public string userId { get; set; }
        public string logDate { get; set; }
        public string inTime { get; set; }
        public string OutTime { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion

    #region ClearAttendance
    public void ClearAttendance()
    {

        txtShiftStartTime.Text = "";
        txtShiftEndTime.Text = "";
        divgridtestimonial.Visible = false;
        DivMyfeedback.Visible = false;
        divGrid.Visible = false;
        DivProfile.Visible = false;
        divMyBodyTest.Visible = false;
        divGridView.Visible = true;
        divattendanceentry.Visible = false;
    }
    #endregion

    protected void btnsubmitswapping_Click(object sender, EventArgs e)
    {
        InsertUser();
    }


    protected void btnfeedbackcancel_Click(object sender, EventArgs e)
    {       
        Divtestimonialcontent.Visible = false;
        divgridtestimonial.Visible = true;
    }
    protected void btncancelswapping_Click(object sender, EventArgs e)
    {
        divswapping.Visible = true;
        divswappentry.Visible = false;
        btnswapadd.Visible = true;
    }

    protected void btnattendancecancel_Click(object sender, EventArgs e)
    {
        divGridView.Visible = true;
        divattendanceentry.Visible = false;
    }

    #region btn My Body Test Close
    protected void lnkbtnMyBodytClose_Click(object sender, EventArgs e)
    {
        DivProfile.Visible = true;
        divMyBodyTest.Visible = false;
    }
    #endregion


    #region Bind User SwapSlots
    public void BindUserSwapSlots()
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
                string Endpoint = Session["BaseUrl"].ToString().Trim() + "userSlotSwapping/SlotSwapping?queryType=getUserSlotSwapping&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "&userId=" + Session["userId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        GridViewuserswapp.DataSource = dt;
                        GridViewuserswapp.DataBind();
                        divswapping.Visible = true;
                        divswappentry.Visible = false;
                        btncancelswapping.Visible = true;
                        btnswapadd.Visible = true;
                    }
                    else
                    {
                        divswapping.Visible = false;
                        divswappentry.Visible = true;
                        btncancelswapping.Visible = false;
                        btnswapadd.Visible = false;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    divswapping.Visible = false;
                    divswappentry.Visible = true;
                    btncancelswapping.Visible = false;
                    btnswapadd.Visible = false;
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;

                    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Errorresponse.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion

    #region Bind Working Slot Grid
    public void BindWorkingSlot()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "slotMaster/GetSlotstoActivate?"
                    + "gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            chkSlotList.DataSource = dt;
                            chkSlotList.DataTextField = "slotTime";
                            chkSlotList.DataValueField = "slotId";
                            chkSlotList.DataBind();

                        }
                        else
                        {
                            chkSlotList.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Insert Function 
    public void InsertUser()
    {
        try
        {
            if (chkSlotList.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('Select Any one slot to swap!!!');", true);
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Any one slot to swap!!!');", true);
                BindUserSwapSlots();
                UserClear();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new UserInsert()
                {
                    newslotId = chkSlotList.SelectedValue,
                    oldslotId = ViewState["UserslotId"].ToString(),
                    userId = Session["userId"].ToString(),
                    slotfromDate = ViewState["fromDate"].ToString(),
                    toDate = txtswapdate.Text,
                    bookingId = ViewState["bookingId"].ToString(),
                    slotswapTypeId = ViewState["UserswaptypeId"].ToString(),
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("userSlotSwapping/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindUserSwapSlots();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }

                }
                BindUserSwapSlots();
                UserClear();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex.ToString().Trim() + "');", true);
            // ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion


    #region User Insert
    public class UserInsert
    {
        public string newslotId { get; set; }
        public string oldslotId { get; set; }
        public string userId { get; set; }
        public string slotswapTypeId { get; set; }
        public string slotfromDate { get; set; }
        public string toDate { get; set; }
        public string bookingId { get; set; }
        public string createdBy { get; set; }
    }
    #endregion

    public void UserClear()
    {

        divswapping.Visible = true;
        divswappentry.Visible = false;
        Userplandetails.Visible = false;
        chkSlotList.ClearSelection();
        txtlogDate.Text = string.Empty;
    }

    public void GetUsersPlans()
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
                string Endpoint = Session["BaseUrl"].ToString().Trim() + "userSlotSwapping/SlotSwapping?queryType=getUserPlanDetails&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "&userId=" + Session["userId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {

                            //userswapType.Text="Slot SwapType : " + dt.Rows[0]["configName"].ToString();
                            //userslot.Text="Slot Timing : " + dt.Rows[0]["SlotTime"].ToString();
                            userswapType.Text = dt.Rows[0]["configName"].ToString();
                            userslot.Text = dt.Rows[0]["SlotTime"].ToString();
                            userswapType1.Text = dt.Rows[0]["configName"].ToString();
                            userslot1.Text = dt.Rows[0]["SlotTime"].ToString();
                            ViewState["UserswaptypeId"] = dt.Rows[0]["slotSwapType"].ToString();
                            ViewState["UserslotId"] = dt.Rows[0]["slotId"].ToString();
                            ViewState["fromDate"] = dt.Rows[0]["fromDate"].ToString();
                            ViewState["bookingId"] = dt.Rows[0]["bookingId"].ToString();
                            Userplandetails.Visible = true;
                        }
                        else
                        {
                            userswapType.Text = "Slot SwapType : - ";
                            userslot.Text = "Slot Timing : - ";
                            ViewState["UserswaptypeId"] = "";
                            ViewState["UserslotId"] = "";
                            ViewState["fromDate"] = "";
                            ViewState["bookingId"] = "";
                            Userplandetails.Visible = false;
                        }

                    }
                    else
                    {
                        userswapType.Text = "Slot SwapType : - ";
                        userslot.Text = "Slot Timing : - ";
                        ViewState["UserswaptypeId"] = "";
                        ViewState["UserslotId"] = "";
                        ViewState["fromDate"] = "";
                        ViewState["bookingId"] = "";
                        Userplandetails.Visible = false;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }

                }
                else
                {
                    userswapType.Text = "Slot SwapType : - ";
                    userslot.Text = "Slot Timing : - ";
                    ViewState["UserswaptypeId"] = "";
                    ViewState["UserslotId"] = "";
                    ViewState["fromDate"] = "";
                    ViewState["bookingId"] = "";
                    Userplandetails.Visible = false;
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;

                    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Errorresponse.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }

    protected void btnswapadd_Click(object sender, EventArgs e)
    {
        divswapping.Visible = false;
        divswappentry.Visible = true;
        btnswapadd.Visible = false;
    }

    #region Age Calculation
    protected void txtMBTDOB_TextChanged(object sender, EventArgs e)
    {
        DateTime date = DateTime.Now;
        DateTime Dob = DateTime.Parse(txtMBTDOB.Text, obj);
        var diff = date - Dob;
        var diffs = diff.Days / 365;
        txtage.Text = diffs.ToString();
        if (txtage.Text == "0")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "infoalert('Enter Age');", true);
            txtage.Text = string.Empty;
        }
    }
    #endregion


    protected void btnfeedbackclose_Click(object sender, EventArgs e)
    {
        DivProfile.Visible = true;
        divMyBodyTest.Visible = false;
        DivMyfeedback.Visible = false;
        divMyattendance.Visible = false;
        Divslotswapping.Visible = false;
    }

    protected void btnattendanceclose_Click(object sender, EventArgs e)
    {
        DivProfile.Visible = true;
        divMyBodyTest.Visible = false;
        DivMyfeedback.Visible = false;
        divMyattendance.Visible = false;
        Divslotswapping.Visible = false;
    }

    protected void btncloseswapping_Click(object sender, EventArgs e)
    {
        DivProfile.Visible = true;
        divMyBodyTest.Visible = false;
        DivMyfeedback.Visible = false;
        divMyattendance.Visible = false;
        Divslotswapping.Visible = false;
    }
}
