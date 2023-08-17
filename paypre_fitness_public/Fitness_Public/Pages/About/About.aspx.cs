using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Pages_HomeNew_HomeNew : System.Web.UI.Page
{
    readonly Helper Helper = new Helper();
    readonly string BaseUri;
    readonly string BaseTokenUri;
    string Token;
    readonly string FooterUri;
    readonly string FooterSocialMediaUri;
    public Pages_HomeNew_HomeNew()
    {
        BaseUri = $"{ConfigurationManager.AppSettings["BaseUrl"].Trim()}";
        BaseTokenUri = $"{ConfigurationManager.AppSettings["BaseUrlToken"].Trim()}";
        FooterUri = $"{BaseUri}branch";
        FooterSocialMediaUri = $"{BaseUri}footerDetails";

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {         
            Token = $"{Session["APIToken"]}";
            FooterBranchDetails();
            FooterLatestNews();
            FooterInsta();
            FooterYouTube();
            FooterFB();

        }
        Token = $"{Session["APIToken"]}";
    }
    #region Btn Back Click 
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../../Home.aspx");
    }
    #endregion

    //Newly added
    #region FooterBranchDetails
    public void FooterBranchDetails()
    {
        try
        {         
            string URI = $"{FooterUri}?queryType=GetBranchMstr&gymOwnerId={Session["gymOwnerId"]}&branchId={Session["BranchId"]}";
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
           
        }
    }
    #endregion

    #region FooterLatestNews
    public void FooterLatestNews()
    {
        try
        {
            string URI = $"{FooterSocialMediaUri}?gymOwnerId={Session["gymOwnerId"]}&branchId={Session["BranchId"]}&displayType=93";
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
            
        }
    }
    #endregion

    #region FooterInsta
    public void FooterInsta()
    {
        try
        {
            string URI = $"{FooterSocialMediaUri}?gymOwnerId={Session["gymOwnerId"]}&branchId={Session["BranchId"]}&displayType=94";
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
           
        }
    }
    #endregion

    #region FooterInsta
    public void FooterYouTube()
    {
        try
        {
            string URI = $"{FooterSocialMediaUri}?gymOwnerId={Session["gymOwnerId"]}&branchId={Session["BranchId"]}&displayType=95";
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
                lnkytbutton.Visible = false;
            }
        }
        catch (Exception ex)
        {
           
        }
    }
    #endregion
    #region FooterInsta
    public void FooterFB()
    {
        try
        {
            string URI = $"{FooterSocialMediaUri}?gymOwnerId={Session["gymOwnerId"]}&branchId={Session["BranchId"]}&displayType=96";
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
            
        }
    }
    #endregion
    protected void lblSubscr_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;
        Label lblId = gvrow.FindControl("lblId") as Label;
        ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('" + lblId.Text + "');", true);
    }

    protected void footeryt_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('" + hfYoutubelink.Value + "');", true);
    }

    protected void footerfb_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('" + hfFblink.Value + "');", true);
    }

}