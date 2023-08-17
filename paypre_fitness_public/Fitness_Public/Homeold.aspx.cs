using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HomeNew_HomeNew : System.Web.UI.Page
{
    readonly Helper Helper = new Helper();
    readonly string BaseUri;
    readonly string BaseTokenUri;
    string Token;
    string ClassesURL;
    string SubscriptionURL;
    readonly string TestimonialURL;
    readonly string GetBranchUri;
    readonly string GetGymownerIdUri;
    readonly string FooterUri;
    readonly string FooterSocialMediaUri;
    readonly string LogoutUrl;
    readonly string FaqUri;
   
    public Pages_HomeNew_HomeNew()
    {
        BaseUri = $"{ConfigurationManager.AppSettings["BaseUrl"].Trim()}";
        BaseTokenUri = $"{ConfigurationManager.AppSettings["BaseUrlToken"].Trim()}";
        TestimonialURL = $"{BaseUri}userTestimonials/AllUser";
        GetGymownerIdUri = $"{BaseUri}getGymownerIdAndBranchId/gymowner";
        GetBranchUri = $"{BaseUri}getGymownerIdAndBranchId/branch?gymOwnerId=";
        SubscriptionURL = $"{BaseUri}subscriptionPlanMaster/getuserHomeSubscription";
        ClassesURL = $"{BaseUri}categoryMaster/GetCategoryForUser";
        FooterUri = $"{BaseUri}branch";
        FooterSocialMediaUri = $"{BaseUri}footerDetails";
        FaqUri = $"{BaseUri}faqMaster";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Session.Clear();
            GetApiToken();
            Token = $"{Session["APIToken"]}";
            Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            GetTestimonials();

            Session["gymOwnerId"] = GetGymownerId();

            BindDDlBranch();
            Subscriptions();
            Classes();
            FooterBranchDetails();
            FooterLatestNews();
            FooterInsta();
            FooterYouTube();
            FooterFB();
            GetFAQ();
            GetPrivacy();
            GetTerms();
            GetGymownerLogo();
            Session["Master_Branch"] = Master.DdlBranch.SelectedItem.Text.Trim();

        }
        Token = $"{Session["APIToken"]}";
       
    }
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

    #region Tetimonials
    public void GetTestimonials()
    {
        try
        {
            Helper.APIGet(TestimonialURL, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                Hdn_Home_Testimonials.Value = JSON_OUT;
            }
            else
            {
                Hdn_Home_Testimonials.Value = "[]";
            }
        }
        catch (Exception ex)
        {
            ShowErrorPopup(ex);
        }
    }
    #endregion

    #region Subscription
    public void Subscriptions()
    {
        try
        {
            string branchId = this.Master.DdlBranch.SelectedValue.Trim();
            Session["BranchId"] = branchId;
            string URI = $"{SubscriptionURL}?gymOwnerId={Session["gymOwnerId"]}&branchId={branchId}";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                Hdn_Home_Subscription.Value = JSON_OUT;
            }
            else
            {
                Hdn_Home_Subscription.Value = "[]";
            }
        }
        catch (Exception ex)
        {
            ShowErrorPopup(ex);
        }
    }
    #endregion

    #region
    public void Classes()
    {
        try
        {
            string branchId = this.Master.DdlBranch.SelectedValue.Trim();
            string URI = $"{ClassesURL}?gymOwnerId={Session["gymOwnerId"]}&branchId={branchId}";

            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                Hdn_Home_Classes.Value = JSON_OUT;
            }
            else
            {
                Hdn_Home_Classes.Value = "[]";
            }
        }
        catch (Exception ex)
        {
            ShowErrorPopup(ex);
        }
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
    public void ShowErrorPopup(Exception Ex)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert(`" + Ex.Message.Trim() + "`);", true);
    }
    #endregion

    #region Subscription Click
    protected void btn_Home_Subscription_Click(object sender, EventArgs e)
    {
        Session["SubscriptionId"] = Hdn_Home_SubscriptionId.Value;
        Response.Redirect("~/Pages/Subscription/Subscription.aspx");
    }
    #endregion

    #region Classes Click
    protected void btn_Home_Classes_Click(object sender, EventArgs e)
    {
        Session["categoryId"] = Hdn_Home_Classes_CategoryId.Value;
        Session["categoryName"] = Hdn_Home_Classes_CategoryName.Value;
        Session["branchId"] = this.Master.DdlBranch.SelectedValue;
        Session["myplan"] = "0";
        //Response.Redirect("Pages/BuyPlan/BuyPlans.aspx");
        Response.Redirect("Pages/Booking/Booking.aspx");
        
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

    #region Get GetBranch
    void BindDDlBranch()
    {
        try
        {
            this.Master.DdlBranch.Items.Clear();
            string GetBranch = $"{GetBranchUri}{Session["gymOwnerId"]}";
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
        Subscriptions();
        Classes();
        FooterBranchDetails();
        FooterLatestNews();
        FooterInsta();
        FooterYouTube();
        FooterFB();
        GetFAQ();
        GetPrivacy();
        GetTerms();
        GetGymownerLogo();
    }
    #endregion

    //Newly added
    #region FooterBranchDetails
    public void FooterBranchDetails()
    {
        try
        {
            string branchId = this.Master.DdlBranch.SelectedValue.Trim();
            Session["BranchId"] = branchId;
            string URI = $"{FooterUri}?queryType=GetBranchMstr&gymOwnerId={Session["gymOwnerId"]}&branchId={branchId}";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                //Shree Yellama Devi complex, Kodathi Gate, 114 / 2, Sarjapur - Marathahalli Rd, Carmelaram, post, Bengaluru, Karnataka 560035   address2,district,state,city,pincode
                FooterStreet.InnerText = Dt.Rows[0]["address1"].ToString() + ',' + Dt.Rows[0]["address2"].ToString() + ',' + Dt.Rows[0]["city"].ToString() + ',' + Dt.Rows[0]["district"].ToString() 
                                        + ',' + Dt.Rows[0]["state"].ToString() + ',' + Dt.Rows[0]["pincode"].ToString(); 
                //FooterDistrict.InnerText = "";
                FooterMobile.InnerText = Dt.Rows[0]["primaryMobileNumber"].ToString();
                Footermail.InnerText = Dt.Rows[0]["emailId"].ToString();
                //Hdn_Home_Subscription.Value = JSON_OUT;
            }
            else
            {
                //Hdn_Home_Subscription.Value = "[]";
            }
        }
        catch (Exception ex)
        {
            ShowErrorPopup(ex);
        }
    }
    #endregion

    #region FooterLatestNews
    public void FooterLatestNews()
    {
        try
        {
            string branchId = this.Master.DdlBranch.SelectedValue.Trim();
            Session["BranchId"] = branchId;
            string URI = $"{FooterSocialMediaUri}?gymOwnerId={Session["gymOwnerId"]}&branchId={branchId}&displayType=L";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                dtlSub.Visible = true;
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                dtlSub.DataSource = Dt;
                dtlSub.DataBind();
            }
            else
            {
                dtlSub.Visible = false;
                //Hdn_Home_Subscription.Value = "[]";
            }
        }
        catch (Exception ex)
        {
            ShowErrorPopup(ex);
        }
    }
    #endregion

    #region FooterInsta
    public void FooterInsta()
    {
        try
        {
            string branchId = this.Master.DdlBranch.SelectedValue.Trim();
            Session["BranchId"] = branchId;
            string URI = $"{FooterSocialMediaUri}?gymOwnerId={Session["gymOwnerId"]}&branchId={branchId}&displayType=I";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                dtlinsta.Visible = true;
                string JSON_OUT = JsonConvert.SerializeObject(Dt);             
                dtlinsta.DataSource = Dt;
                dtlinsta.DataBind();
            }
            else
            {
                dtlinsta.Visible = false;
                //Hdn_Home_Subscription.Value = "[]";
            }
        }
        catch (Exception ex)
        {
            ShowErrorPopup(ex);
        }
    }
    #endregion

    #region FooterInsta
    public void FooterYouTube()
    {
        try
        {
            string branchId = this.Master.DdlBranch.SelectedValue.Trim();
            Session["BranchId"] = branchId;
            string URI = $"{FooterSocialMediaUri}?gymOwnerId={Session["gymOwnerId"]}&branchId={branchId}&displayType=Y";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                lnkytbutton.Visible = true;
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                footeryt.Src = Dt.Rows[0]["icons"].ToString();
                hfYoutubelink.Value = Dt.Rows[0]["link"].ToString();
            }
            else
            {
                lnkytbutton.Visible= false;
            }
        }
        catch (Exception ex)
        {
            ShowErrorPopup(ex);
        }
    }
    #endregion
    #region FooterInsta
    public void FooterFB()
    {
        try
        {
            string branchId = this.Master.DdlBranch.SelectedValue.Trim();
            Session["BranchId"] = branchId;
            string URI = $"{FooterSocialMediaUri}?gymOwnerId={Session["gymOwnerId"]}&branchId={branchId}&displayType=F";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                lnkfbbutton.Visible = true;
                string JSON_OUT = JsonConvert.SerializeObject(Dt);
                footerfb.Src = Dt.Rows[0]["icons"].ToString();
                hfFblink.Value = Dt.Rows[0]["link"].ToString();
            }
            else
            {
                lnkfbbutton.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ShowErrorPopup(ex);
        }
    }
    #endregion
    protected void lblSubscr_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;
        Label lblId = gvrow.FindControl("lblId") as Label;
        ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('"+ lblId.Text + "');", true);
    }

    protected void footeryt_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('" + hfYoutubelink.Value + "');", true);
    }

    protected void footerfb_Click(object sender, EventArgs e)
    {       
        ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('" + hfFblink.Value + "');", true);
    }

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
            ShowErrorPopup(ex);
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
            ShowErrorPopup(ex);
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
            ShowErrorPopup(ex);
        }
    }

    #endregion
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
                        LogoFAQ.ImageUrl = dt.Rows[0]["logoUrl"].ToString();
                        LogoPrivacy.ImageUrl = dt.Rows[0]["logoUrl"].ToString();
                        LogoTerms.ImageUrl = dt.Rows[0]["logoUrl"].ToString();

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