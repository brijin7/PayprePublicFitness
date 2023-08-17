using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Services;
using System.Web;
using System.Web.UI.HtmlControls;
using ASP;

public partial class Pages_Home_HomeNew : System.Web.UI.Page
{
	readonly Helper Helper = new Helper();
	readonly string BaseUri;
	readonly string BaseTokenUri;
	string Token;
	string ClassesURL;
	string SubscriptionURL;
	readonly string TestimonialURL;
	readonly string GetBranchUri;
	readonly string GetBranchLocationUri;
	readonly string GetGymownerIdUri;
	readonly string FooterUri;
	readonly string FooterSocialMediaUri;
	readonly string LogoutUrl;
	readonly string FaqUri;
    //Newly added
    string GalleryURL;
    //userTestimonials/BranchTestimonials? Input.gymOwnerId=100002&Input.branchId= 1

    public Pages_Home_HomeNew()
	{
		BaseUri = $"{ConfigurationManager.AppSettings["BaseUrl"].Trim()}";
		BaseTokenUri = $"{ConfigurationManager.AppSettings["BaseUrlToken"].Trim()}";
		TestimonialURL = $"{BaseUri}userTestimonials/BranchTestimonials?";
		GetGymownerIdUri = $"{BaseUri}getGymownerIdAndBranchId/gymowner";
		GetBranchUri = $"{BaseUri}branch?queryType=GetBranchMstrSA";
		GetBranchLocationUri = $"{BaseUri}branch/GetBranchBasedOnLocation?lattitude=";
		SubscriptionURL = $"{BaseUri}subscriptionPlanMaster/getuserHomeSubscription";
		ClassesURL = $"{BaseUri}categoryMaster/GetCategoryForUser";
		FooterUri = $"{BaseUri}branch";
		FooterSocialMediaUri = $"{BaseUri}footerDetails";
		FaqUri = $"{BaseUri}faqMaster";
        //Newly added
        GalleryURL = $"{BaseUri}branchGallery";
	}

    #region Page Load 
    protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
        {
            Session["LocArray"] = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "GetLocation", "getLocation();", true);
            //Session.Clear();
            GetApiToken();
			Token = $"{Session["APIToken"]}";
			Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            Session["gymOwnerId"] = GetGymownerId();

            BindDDlBranch();
            string branchId = this.Master.DdlBranch.SelectedValue.Trim();
            Session["branchId"] = branchId;
            Session["Master_Branch"] = Master.DdlBranch.SelectedItem.Text.Trim();
            Subscriptions();
            //Newly added
            //TrainersDescription();
            MealPlanDescription();
            Testimonials();
            TestimonialsContents();
            //FooterLatestNews();
            FooterInsta();
            FooterYouTube();
            FooterFB();
            GetFAQ();
            GetPrivacy();
            GetTerms();
            FooterBranchDetails();
            FooterTwitter();
            FooterLinkedIn();
            BindClasses();
            BindTrainer();

        }
		Token = $"{Session["APIToken"]}";

    

    }
    #endregion
    #region GetApiToken
    public void GetApiToken()
    {
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{BaseTokenUri}token");
            string value = "username=Fitness@gmail.com&password=Fitness@123&grant_type=password";
            request.Content = new StringContent(value, Encoding.UTF8, "application/x-www-form-urlencoded");//CONTENT-TYPE header

            HttpResponseMessage response = client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                var Token = response.Content.ReadAsStringAsync().Result;
                string ResponseMsg = JObject.Parse(Token)["access_token"].ToString();

                Session["APIToken"] = ResponseMsg;
            }
        }

        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    #region Get GymownerId
    string GetGymownerId()
	{
		try
		{
			Helper.APIGet(GetGymownerIdUri, Token, out DataTable Dt, out int StatusCode, out string Response);

			if (StatusCode == 1)
			{
				return Dt.Rows[0]["gymOwnerId"].ToString().Trim();


			}
			else
			{
				return "";
			}
		}
		catch (Exception)
		{
			throw;
		}


	}
    #endregion
    #region OnBranchChange
    protected void btnBranchChange_Click(object sender, EventArgs e)
    {
        Session["Master_Branch"] = Master.DdlBranch.SelectedItem.Text.Trim();
        string branchId = this.Master.DdlBranch.SelectedValue.Trim();
        Session["branchId"] = branchId;
        DataTable arr = (DataTable)Session["Branchdt"];
        if (arr.Rows.Count != 0)
        {
            var gym = arr.AsEnumerable().Where(row => row["branchId"].ToString() == branchId.ToString()).Select(r => r["gymOwnerId"]).ToList();
            string Ownerid = gym[0].ToString();
            Session["gymOwnerId"] = Ownerid;
        }
        Subscriptions();
        //Newly added
        //TrainersDescription();
        MealPlanDescription();
        Testimonials();
        TestimonialsContents();
        //FooterLatestNews();
        FooterInsta();
        FooterYouTube();
        FooterFB();
        GetFAQ();
        GetPrivacy();
        GetTerms();
        FooterBranchDetails();
        FooterTwitter();
        FooterLinkedIn();
        BindClasses();
        BindTrainer();
    }
    #endregion
    #region Btn Location Get Click
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        double latitude = Convert.ToDouble(hdnLatitude.Value);
        double longitude = Convert.ToDouble(hdnLongitude.Value);

        // Use the latitude and longitude as needed
        // For example, you can save them to session variables
        string branchIds = hfBranchId.Value;
        string gymOwnerId = hfgymOwnerId.Value;

        Session["Latitude"] = latitude;
        Session["Longitude"] = longitude;
        BindDDlBranchBacedOnLocation();
        if (branchIds != "")
        {
            Session["gymOwnerId"] = gymOwnerId;
            this.Master.DdlBranch.SelectedValue = branchIds;
        }
        
        string branchId = this.Master.DdlBranch.SelectedValue.Trim();
        Session["branchId"] = branchId;
        Session["Master_Branch"] = Master.DdlBranch.SelectedItem.Text.Trim();
        Subscriptions();
        //Newly added
        //TrainersDescription();
        MealPlanDescription();
        Testimonials();
        TestimonialsContents();
        //FooterLatestNews();
        FooterInsta();
        FooterYouTube();
        FooterFB();
        GetFAQ();
        GetPrivacy();
        GetTerms();
        FooterBranchDetails();
        FooterTwitter();
        FooterLinkedIn();
        BindClasses();
        BindTrainer();
    }

    #endregion
    #region Get GetBranch Location
    public void BindDDlBranchBacedOnLocation()
    {
        try
        {
            this.Master.DdlBranch.Items.Clear();
            string GetBranch = $"{GetBranchLocationUri}{Session["Latitude"]}&longitude={Session["Longitude"]}&radius=20000";
            Helper.APIGet(GetBranch, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                this.Master.DdlBranch.DataSource = Dt;
                this.Master.DdlBranch.DataValueField = "branchId";
                this.Master.DdlBranch.DataTextField = "branchName";
                this.Master.DdlBranch.DataBind();
                this.Master.DdlBranch.CssClass = "ddlBranch d-block";
            }
            else
            {
                this.Master.DdlBranch.CssClass = "ddlBranch d-none";
            }
            Session["Branchdt"] = Dt;
            var arrayloc = Dt.AsEnumerable().ToArray();
            Session["LocArray"] = arrayloc;
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    #region Subscription
    public void Subscriptions()
	{
		try
		{
			string branchId = Session["branchId"].ToString();
			string URI = $"{SubscriptionURL}?gymOwnerId={Session["gymOwnerId"]}&branchId={branchId}";
			Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

			if (StatusCode == 1)
			{
				string JSON_OUT = JsonConvert.SerializeObject(Dt);
				Hdn_Home_Subscription.Value = JSON_OUT;
                divSubs.Visible = true;
                Master.Master_lnkbtnSubscription.Visible = true;
            }
			else
			{
                divSubs.Visible = false;
                Master.Master_lnkbtnSubscription.Visible = false;
                Hdn_Home_Subscription.Value = "[]";
			}
		}
		catch (Exception ex)
		{
			//ShowErrorPopup(ex);
		}
	}
	#endregion
    #region Subscription Click
    protected void btn_Home_Subscription_Click(object sender, EventArgs e)
    {
        Session["SubscriptionId"] = Hdn_Home_SubscriptionId.Value;
        Response.Redirect("~/Pages/Subscription/Subscription.aspx");
    }
    #endregion
    #region Get GetBranch
    void BindDDlBranch()
    {
        try
        {
            this.Master.DdlBranch.Items.Clear();
            string GetBranch = $"{GetBranchUri}";
            Helper.APIGet(GetBranch, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                this.Master.DdlBranch.DataSource = Dt;
                this.Master.DdlBranch.DataValueField = "branchId";
                this.Master.DdlBranch.DataTextField = "branchName";
                this.Master.DdlBranch.DataBind();
                this.Master.DdlBranch.CssClass = "ddlBranch d-block";

                Session["gymOwnerId"] = Dt.Rows[0]["gymOwnerId"].ToString();
            }
            else
            {
                this.Master.DdlBranch.CssClass = "ddlBranch d-none";
            }
            Session["Branchdt"] = Dt;
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    #region Bind Classes
    public void BindClasses()
    {
        try
        {
            string branchId = Session["branchId"].ToString();
            //"categoryMaster/GetCategoryForUser?gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + "";
            string URI = $"{BaseUri}categoryMaster/GetCategoryForUser?gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + "";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                hfclassescontainer.Value = JSON_OUT;
            }
            else
            {
                hfclassescontainer.Value = "[]";
            }
        }
        catch (Exception ex)
        {
            //ShowErrorPopup(ex);
        }
    }
    #endregion
    #region Bind Trainer
    public void BindTrainer()
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
                string Endpoint = "trainer?gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["TrainerDetails"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            string JSON_OUT = JsonConvert.SerializeObject(dt);
                            hftrainersHomeName.Value = JSON_OUT;
                            hftrainerdetailsBaseUrl.Value = Session["BaseUrl"].ToString().Trim() + "trainerDetails?Public='Y'&trainerId=";
                            hfTokenTrainers.Value = Session["APIToken"].ToString().Trim();
                        }
                        else
                        {
                            hftrainersHomeName.Value = "[]";
                        }

                    }
                    else
                    {
                        divTrainer.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    divTrainer.Visible = false;
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
    #region Class Click
    protected void lnkClass_Click(object sender, EventArgs e)
    {
        string Value = Session["Latitude"].ToString();
        LinkButton lnkClass = sender as LinkButton;
        DataListItem gvrow = lnkClass.NamingContainer as DataListItem;
        Label lblcategoryId = gvrow.FindControl("lblcategoryId") as Label;
        Session["categoryId"] = lblcategoryId.Text;
        Response.Redirect("Pages/Booking/Booking.aspx");

    }


    protected void imgBtnClass_Click(object sender, ImageClickEventArgs e)
    {
        string Value = Session["Latitude"].ToString();
        ImageButton lnkClass = sender as ImageButton;
        DataListItem gvrow = lnkClass.NamingContainer as DataListItem;
        Label lblcategoryId = gvrow.FindControl("lblcategoryId") as Label;
        Session["categoryId"] = lblcategoryId.Text;
        Response.Redirect("Pages/Booking/Booking.aspx");
    }

    [System.Web.Services.WebMethod]
    public static string btnBuyNow_Click(string categoryId)
    {
        HttpContext.Current.Session["categoryId"] = categoryId;

        return "Success"; // Return a response if needed
    }
    //protected void btnBuyNow_Click(object sender, EventArgs e)
    //{
    //    Button lnkClass = sender as Button;
    //    DataListItem gvrow = lnkClass.NamingContainer as DataListItem;
    //    Label lblcategoryId = gvrow.FindControl("lblcategoryId") as Label;
    //    Session["categoryId"] = lblcategoryId.Text;
    //    Response.Redirect("Pages/Booking/Booking.aspx");
    //}



    #endregion

    //Newly added

    #region Trainer Click 
    protected void imgBtnClass_Click1(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = sender as ImageButton;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;
        //DataList gvrow = lnkbtn.NamingContainer as DataList;
        Label lbltrainerId = gvrow.FindControl("lbltrainerId") as Label;
        // Label lbltrainerId = (Label)gvrow.FindControl("lbltrainerId");
        ViewState["TrainerId"] = lbltrainerId.Text.Trim();
        BindTrainerDetails();
        divtrainerdetails.Visible = true;
        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Showtrainerdetails();", true);
        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Showtrainerdetails()", true);
    }
    #endregion
    #region Bind Trainer Details
    public void BindTrainerDetails()
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
                //string Endpoint = "trainer?gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["Master_Branch"].ToString() + "";
                string Endpoint = "trainerDetails?trainerId=" + ViewState["TrainerId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["TrainerDetails"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            //trainerdetailsdatalist.DataSource = dt;
                            //trainerdetailsdatalist.DataBind();
                            //string JSON_OUT = JsonConvert.SerializeObject(dt);
                            //hftrainerdetails.Value = JSON_OUT;
                        }
                        else
                        {
                            //hftrainerdetails.Value = "[]";
                           // trainerdetailsdatalist.DataBind();
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
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
    #region Trainer List 
    protected void trainerdetailsdatalist_ItemCreated(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            int index = e.Item.ItemIndex;
            Image myImage = (Image)e.Item.FindControl("imgC1");
            myImage.ID = "imgC1_" + index;
        }
    }
    #endregion
    #region Meal Plan Description
    public void MealPlanDescription()
    {
        try
        {
            string branchId = Session["branchId"].ToString();
            string URI = $"{GalleryURL}?branchId={branchId}&galleryId=113";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                hfMealplanDescription.Value = JSON_OUT;
                divMeals.Visible = true;
            }
            else
            {
                hfMealplanDescription.Value = "[]";
                divMeals.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //ShowErrorPopup(ex);
        }
    }
    #endregion
    //#region TrainersDescription
    //public void TrainersDescription()
    //{
    //    try
    //    {
    //        string branchId = Session["branchId"].ToString();
    //        string URI = $"{GalleryURL}?branchId={branchId}&galleryId=114";
    //        Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

    //        if (StatusCode == 1)
    //        {
    //            string JSON_OUT = JsonConvert.SerializeObject(Dt);
    //            hfTrainerDescription.Value = JSON_OUT;
                
    //        }
    //        else
    //        {
    //            hfTrainerDescription.Value = "[]";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //ShowErrorPopup(ex);
    //    }
    //}
    //#endregion
    #region Testimonials
    public void Testimonials()
    {
        try
        {
            string branchId = Session["branchId"].ToString();
            string URI = $"{GalleryURL}?branchId={branchId}&galleryId=115";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                hfTestimonialsImage.Value = JSON_OUT;

                divReview.Visible = true;
                Master.Master_lnkbtnTestimonials.Visible = true;
            }
            else
            {
                hfTestimonialsImage.Value = "[]";
                divReview.Visible = false;
                Master.Master_lnkbtnTestimonials.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //ShowErrorPopup(ex);
        }
    }
    #endregion
    #region TestimonialsContents
    public void TestimonialsContents()
    {
        try
        {
            string branchId = Session["branchId"].ToString();
            string URI = $"{TestimonialURL}gymOwnerId={Session["gymOwnerId"]}&branchId={branchId}";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                hftestimonialDatas.Value = JSON_OUT;
            }
            else
            {
                hftestimonialDatas.Value = "[]";
            }
        }
        catch (Exception ex)
        {
            //ShowErrorPopup(ex);
        }
    }
    #endregion
    #region FooterBranchDetails
    public void FooterBranchDetails()
    {
        try
        {
            string branchId = Session["branchId"].ToString();
            string URI = $"{FooterUri}?queryType=GetBranchMstr&gymOwnerId={Session["gymOwnerId"]}&branchId={branchId}";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                lblMobileNo.InnerText = Dt.Rows[0]["primaryMobileNumber"].ToString();
                lblMailId.InnerText = Dt.Rows[0]["emailId"].ToString();
            }
            else
            {
                lblMobileNo.InnerText = "";
                lblMailId.InnerText = "";
            }
        }
        catch (Exception ex)
        {
            //ShowErrorPopup(ex);
        }
    }
    #endregion
    #region FooterInsta
    public void FooterInsta()
    {
        try
        {
            string branchId = Session["branchId"].ToString();
            string URI = $"{FooterSocialMediaUri}?gymOwnerId={Session["gymOwnerId"]}&branchId={branchId}&displayType=I";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                lnkItbutton.Visible = true;
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                Session["footerIt"]= Dt.Rows[0]["link"].ToString();
                footerIt.Src = Dt.Rows[0]["icon"].ToString();
            }
            else
            {
                lnkItbutton.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //ShowErrorPopup(ex);
        }
    }
    #endregion
    #region FooterYouTube
    public void FooterYouTube()
    {
        try
        {
            string branchId = Session["branchId"].ToString();
            string URI = $"{FooterSocialMediaUri}?gymOwnerId={Session["gymOwnerId"]}&branchId={branchId}&displayType=Y";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                lnkytbutton.Visible = true;
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                Session["footerYT"] = Dt.Rows[0]["link"].ToString();
                footerYT.Src = Dt.Rows[0]["icon"].ToString();
            }
            else
            {
                lnkytbutton.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //ShowErrorPopup(ex);
        }
    }
    #endregion
    #region Footerfb
    public void FooterFB()
    {
        try
        {
            string branchId = Session["branchId"].ToString();
            string URI = $"{FooterSocialMediaUri}?gymOwnerId={Session["gymOwnerId"]}&branchId={branchId}&displayType=F";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                lnkfbbutton.Visible = true;
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                Session["footerfb"] = Dt.Rows[0]["link"].ToString();
                footerfb.Src = Dt.Rows[0]["icon"].ToString();
            }
            else
            {
               lnkfbbutton.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //ShowErrorPopup(ex);
        }
    }
    #endregion
    #region FooterLinkedIn
    public void FooterLinkedIn()
    {
        try
        {
            string branchId = Session["branchId"].ToString();
            string URI = $"{FooterSocialMediaUri}?gymOwnerId={Session["gymOwnerId"]}&branchId={branchId}&displayType=L";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                lnklinkedIn.Visible = true;
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                Session["footerlinkedIn"] = Dt.Rows[0]["link"].ToString();
                footerlinkedIn.Src = Dt.Rows[0]["icon"].ToString();
            }
            else
            {
                lnklinkedIn.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //ShowErrorPopup(ex);
        }
    }
    #endregion
    #region FooterTwitter
    public void FooterTwitter()
    {
        try
        {
            string branchId = Session["branchId"].ToString();
            string URI = $"{FooterSocialMediaUri}?gymOwnerId={Session["gymOwnerId"]}&branchId={branchId}&displayType=T";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                lnktwitter.Visible = true;
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                Session["footerTwitter"] = Dt.Rows[0]["link"].ToString();
                footertwitter.Src = Dt.Rows[0]["icon"].ToString();
            }
            else
            {
                lnktwitter.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //ShowErrorPopup(ex);
        }
    }
    #endregion
    #region Footer Click
    protected void footerfb_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('" + Session["footerfb"].ToString() + "');", true);
    }
    protected void footerYT_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('" + Session["footerYT"].ToString() + "');", true);
    }
    protected void footerIt_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('" + Session["footerIt"].ToString() + "');", true);
    }
    protected void lnktwitter_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('" + Session["footerTwitter"].ToString() + "');", true);
    }

    protected void lnklinkedIn_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('" + Session["footerlinkedIn"].ToString() + "');", true);
    }
    #endregion
    #region Faq
    public void GetFAQ()
    {
        try
        {
            string Url = $"{FaqUri}?gymOwnerId={Session["gymOwnerId"]}&questionType=F";
            Helper.APIGet(Url, Token, out DataTable dt, out int StatusCode, out string Response);
            if (StatusCode == 1)
            {
                dtlFAQ.DataSource = dt;
                dtlFAQ.DataBind();

            }
        }
        catch (Exception ex)
        {
            //ShowErrorPopup(ex);
        }
    }
    protected void dtlFAQ_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Label Answer = (Label)e.Item.FindControl("lblanswer");
            DataList dtlFA = (DataList)e.Item.FindControl("dtlFA");
            var List = Answer.Text.ToString().Split('•');
            DataTable dt = new DataTable();
            dt.Columns.Add("answer");
            for (int i = 1; i < List.Count(); i++)
            {
                dt.Rows.Add(List[i]);
            }
            dtlFA.DataSource = dt;
            dtlFA.DataBind();
        }


    }
    #endregion
    #region Privacy
    public void GetPrivacy()
    {
        try
        {
            string Url = $"{FaqUri}?gymOwnerId={Session["gymOwnerId"]}&questionType=P";
            Helper.APIGet(Url, Token, out DataTable dt, out int StatusCode, out string Response);
            if (StatusCode == 1)
            {
                dtlPrivacy.DataSource = dt;
                dtlPrivacy.DataBind();

            }
        }
        catch (Exception ex)
        {
            //ShowErrorPopup(ex);
        }
    }

    #endregion
    #region Terms&Conditions
    public void GetTerms()
    {
        try
        {
            string Url = $"{FaqUri}?gymOwnerId={Session["gymOwnerId"]}&questionType=T";
            Helper.APIGet(Url, Token, out DataTable dt, out int StatusCode, out string Response);
            if (StatusCode == 1)
            {
                dtlTerms.DataSource = dt;
                dtlTerms.DataBind();

            }
        }
        catch (Exception ex)
        {
           // ShowErrorPopup(ex);
        }
    }

    #endregion


   
}

