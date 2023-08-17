using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

public partial class Pages_BuyPlan_BuyPlans : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            Session["BaseUrlToken"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrlToken"].Trim();
            BindCategoryList();
            this.BindCategoryList();
        }       

    }
    #endregion
    #region Bind Category Images
    public string getPathName()
    {
        if (Session["categoryId"].ToString() == "2")
        {
            return "../../Images/BuyPlan/FatLoss.png";

        }
        else
        {
            return "../../Images/BuyPlan/MuscleBuild.png";
        }

    }
    #endregion
   
    #region Bind Category List
    public void BindCategoryList()
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
                string Endpoint = "fitnessCategoryPrice/GetPriceOnCategory?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                    "&branchId=" + Session["branchId"].ToString() + "&categoryId=" + Session["categoryId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);

                        dtlCategoryList.DataSource = dt;
                        dtlCategoryList.DataBind();
                        lblCategoryName.Text = dt.Rows[0]["categoryName"].ToString();

                        for (int i = 0; i < dtlCategoryList.Items.Count; i++)
                        {
                            Label lblcategoryName = dtlCategoryList.Items[i].FindControl("lblcategoryName") as Label;
                            HtmlControl divCategoryList = dtlCategoryList.Items[i].FindControl("divCategoryList") as HtmlControl;
                            if (lblcategoryName.Text == "Muscle Building")
                            {
                                divCategoryList.Attributes["class"] = "divCategoryList";
                            }
                            else if (lblcategoryName.Text == "Fat Loss")
                            {
                                divCategoryList.Attributes["class"] = "divCategoryListFat";
                            }
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
    #region Bind Category 
    protected void lbltrainingTypeName_Click(object sender, EventArgs e)
    {

        LinkButton lnkbtn = sender as LinkButton;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;

        Label lblplanDuration = (Label)gvrow.FindControl("lblplanDuration");

        var dataList = gvrow.FindControl("dtlCategory") as DataList;
        Label planId = gvrow.FindControl("lblplanDuration") as Label;

        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "fitnessCategoryPrice/GetPriceOnDuration?gymOwnerId=100002&branchId=1&categoryId=1&planDuration=" + planId.Text + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        dataList.DataSource = dt;
                        dataList.DataBind();

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
        for (int i = 0; i < dtlCategoryList.Items.Count; i++)
        {
            HtmlControl DivCategory = dtlCategoryList.Items[i].FindControl("DivCategory") as HtmlControl;
            Label plan = dtlCategoryList.Items[i].FindControl("lblplanDuration") as Label;
            if (plan.Text == planId.Text)
            {
                DivCategory.Visible = true;
            }
            else
            {
                DivCategory.Visible = false;
            }
        }

    }
    #endregion
    #region Category Click
    protected void lblCnetAmount_Click(object sender, EventArgs e)
    {
        LinkButton lblCnetAmount = sender as LinkButton;
        DataListItem gvrow = lblCnetAmount.NamingContainer as DataListItem;
        Label lblplanDuration = gvrow.FindControl("lblplanDuration") as Label;
        Label lblCtrainingTypeName = (Label)gvrow.FindControl("lblCtrainingTypeName");
        Label lblcategoryId = (Label)gvrow.FindControl("lblcategoryId");
        Label lblplanDurationName = (Label)gvrow.FindControl("lblplanDurationName");
        Label lblactualAmount = (Label)gvrow.FindControl("lblactualAmount");
        Label lblgymOwnerId = (Label)gvrow.FindControl("lblgymOwnerId");
        Label lblbranchId = (Label)gvrow.FindControl("lblbranchId");
        Label lblbranchName = (Label)gvrow.FindControl("lblbranchName");
        Label lbltrainingTypeId = (Label)gvrow.FindControl("lbltrainingTypeId");
        Label lbltrainingMode = (Label)gvrow.FindControl("lbltrainingMode");
        Label lblpriceId = (Label)gvrow.FindControl("lblpriceId");
        Label lblprice = (Label)gvrow.FindControl("lblprice");
        Label lbltaxId = (Label)gvrow.FindControl("lbltaxId");
        Label lbltaxName = (Label)gvrow.FindControl("lbltaxName");
        Label lbltaxAmount = (Label)gvrow.FindControl("lbltaxAmount");

        string PlanName = lblplanDurationName.Text;
        string netamont = lblCnetAmount.Text;
        string[] pname;
        string[] pnet;
        string[] NetAmount;
        pname = PlanName.Split(' ');
        pnet = netamont.Split('₹');
        string Net = pnet[1];
        NetAmount = Net.Split('/');
        Session["planDuration"] = pname[0];
        Session["PcategoryId"] = lblcategoryId.Text;
        Session["netamount"] = NetAmount[0];
        Session["trainingTypeName"] = lblCtrainingTypeName.Text;
        Session["actualAmount"] = lblactualAmount.Text;
        Session["gymOwnerId"] = lblgymOwnerId.Text;
        Session["branchId"] = lblbranchId.Text;
        Session["branchName"] = lblbranchName.Text;
        Session["trainingTypeId"] = lbltrainingTypeId.Text;
        Session["planDurationId"] = lblplanDuration.Text;
        Session["trainingMode"] = lbltrainingMode.Text;
        Session["priceId"] = lblpriceId.Text;
        Session["price"] = lblprice.Text;
        Session["taxId"] = lbltaxId.Text;
        Session["taxName"] = lbltaxName.Text;
        Session["taxAmount"] = lbltaxAmount.Text;
        Session["BuyPlan"] = "P";
        for (int i = 0; i < dtlCategoryList.Items.Count; i++)
        {
            Label planDuration = dtlCategoryList.Items[i].FindControl("lblplanDuration") as Label;
            if (lblplanDuration.Text == planDuration.Text)
            {
                LinkButton lbltrainingTypeName = dtlCategoryList.Items[i].FindControl("lbltrainingTypeName") as LinkButton;
                lbltrainingTypeName.Text = lblCtrainingTypeName.Text + " " + "▼";
                Label lblnetAmount = dtlCategoryList.Items[i].FindControl("lblnetAmount") as Label;
                lblnetAmount.Text = lblCnetAmount.Text;
                HtmlControl DivCategory = dtlCategoryList.Items[i].FindControl("DivCategory") as HtmlControl;
                DivCategory.Visible = false;
            }

        }

    }
    #endregion
    #region Dtl Close btn
    protected void btnDtlClose_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dtlCategoryList.Items.Count; i++)
        {
            HtmlControl DivCategory = dtlCategoryList.Items[i].FindControl("DivCategory") as HtmlControl;
            DivCategory.Visible = false;
        }

    }
    #endregion
    #region Btn Buy 
    protected void btnBuyList_Click(object sender, EventArgs e)
    {
        Button lblCnetAmount = sender as Button;
        DataListItem gvrow = lblCnetAmount.NamingContainer as DataListItem;
        Label lblplanDuration = gvrow.FindControl("lblplanDuration") as Label;
        Label lblCtrainingTypeName = (Label)gvrow.FindControl("lblCtrainingTypeName");
        Label lblcategoryId = (Label)gvrow.FindControl("lblcategoryId");
        Label lblplanDurationName = (Label)gvrow.FindControl("lblplanDurationName");
        Label lblactualAmount = (Label)gvrow.FindControl("lblactualAmount");
        Label lblgymOwnerId = (Label)gvrow.FindControl("lblgymOwnerId");
        Label lblbranchId = (Label)gvrow.FindControl("lblbranchId");
        Label lblbranchName = (Label)gvrow.FindControl("lblbranchName");
        Label lbltrainingTypeId = (Label)gvrow.FindControl("lbltrainingTypeId");
        Label lbltrainingMode = (Label)gvrow.FindControl("lbltrainingMode");
        Label lblpriceId = (Label)gvrow.FindControl("lblpriceId");
        Label lblprice = (Label)gvrow.FindControl("lblprice");
        Label lbltaxId = (Label)gvrow.FindControl("lbltaxId");
        Label lbltaxName = (Label)gvrow.FindControl("lbltaxName");
        Label lbltaxAmount = (Label)gvrow.FindControl("lbltaxAmount");
        Label lblTotalAmt = (Label)gvrow.FindControl("lblTotalAmt");
        
        string PlanName = lblplanDurationName.Text;
        string[] pname;       
       
        pname = PlanName.Split(' ');
       
        Session["planDuration"] = pname[0];
        Session["PcategoryId"] = lblcategoryId.Text;
        Session["netamount"] = lblTotalAmt.Text;
        Session["trainingTypeName"] = lblCtrainingTypeName.Text;
        Session["actualAmount"] = lblactualAmount.Text;
        Session["gymOwnerId"] = lblgymOwnerId.Text;
        Session["branchId"] = lblbranchId.Text;
        Session["branchName"] = lblbranchName.Text;
        Session["trainingTypeId"] = lbltrainingTypeId.Text;
        Session["planDurationId"] = lblplanDuration.Text;
        Session["trainingMode"] = lbltrainingMode.Text;
        Session["priceId"] = lblpriceId.Text;
        Session["price"] = lblprice.Text;
        Session["taxId"] = lbltaxId.Text;
        Session["taxName"] = lbltaxName.Text;
        Session["taxAmount"] = lbltaxAmount.Text;
        Session["BuyPlan"] = "P";
      
        Response.Redirect("../PlanDetails/PlanDetails.aspx");
    }
    #endregion
    #region Btn Back Click 
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../../Home.aspx");
    }
    #endregion

    ///Start - Dynamic Repeat column Jquery script
    protected void OnClick(object sender, EventArgs e)
    {
        int repeatcolumn = Convert.ToInt32(hfColumnRepeat.Value);
        this.ResetrepeatColumns(repeatcolumn);
    }      
    private void ResetrepeatColumns(int repeatcolumn = 3)
    {
        dtlCategoryList.RepeatColumns = repeatcolumn;
    }
    ///End - Dynamic Repeat column Jquery script
}