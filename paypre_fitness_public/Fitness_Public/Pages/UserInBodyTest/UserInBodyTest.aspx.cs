using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Data;
using System.Net.NetworkInformation;
using System.Web.UI.HtmlControls;

public partial class Pages_UserInBodyTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            GetUserDetails();
            GetUserBodyTest();

        }

    }
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
                        Session["BodyTest"] = "Y";
                        Master.Master_btnHome.Enabled = true;
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
                            if (dt.Rows.Count == 1)
                            {
                                HtmlControl div = dtlUserBodyTest.Items[0].FindControl("divtargetItems") as HtmlControl;
                                div.Attributes["class"] = "col-11 col-sm-4 col-md-4 col-lg-4 col-xl-4 divtargetItems1";

                            }
                            else if(dt.Rows.Count == 2)
                            {
                                for (int j = 0; j < dtlUserBodyTest.Items.Count; j++)
                                {
                                    HtmlControl div = dtlUserBodyTest.Items[j].FindControl("divtargetItems") as HtmlControl;
                                    div.Attributes["class"] = "col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 divtargetItems2";
                                }
                            }
                            else
                            {
                                for (int j = 0; j < dtlUserBodyTest.Items.Count; j++)
                                {
                                    HtmlControl div = dtlUserBodyTest.Items[j].FindControl("divtargetItems") as HtmlControl;
                                    div.Attributes["class"] = "col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 divtargetItems3";
                                }

                        
                               

                            }

                        }

                    }
                    else
                    {
                        divGrid.Visible = false;
                        Master.Master_btnHome.Enabled = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    divGrid.Visible = false;
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
                        txtname.Text = dt.Rows[0]["firstName"].ToString();
                        txtDOB.Text = dt.Rows[0]["dob"].ToString();
                        ddlGender.SelectedValue = dt.Rows[0]["gender"].ToString();
                        
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
    #endregion
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
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
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
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select WorkOut Details');", true);
                    return;
                }

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Gender');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Work Out Details Selected Index Changed
    protected void ddlWorkOutDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtheight.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Enter Height');", true);
            return;
        }
        if (ddlGender.SelectedValue == "0")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Gender');", true);
            return;
        }
        if (txtweight.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Enter Weight');", true);
            return;
        }
        if (txtage.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Enter Age');", true);
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
                    dob = txtDOB.Text,
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
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString() + "');", true);

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
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #region Btn Submit 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertUserEnroll();
    }
    #endregion
    #region Btn Cancel
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        GetUserBodyTest();
    }
    #endregion
    #region Btn Back Click 
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        if(Session["BodyTest"] == "Y")
        {
            if(Session["Status"] == "H")
            {
                Response.Redirect("../../Home.aspx");
            }
            else
            {

                Response.Redirect("../PaymentPage/PaymentPage.aspx");
            }
            
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('Update Body Test Details');", true);
        }
        
    }
    #endregion
    #region Btn Add Click 
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divGrid.Visible = false;
        divUserForm.Visible = true;
        btnAdd.Visible = false;
    }
    #endregion
    #region Clear
    public void Clear()
    {
        btnAdd.Visible = true;
        txtage.Text = "";
        txtBMI.Text = "";
        txtBMR.Text = "";
        txtDOB.Text = "";
        txtfat.Text = "";
        txtheight.Text = "";
        txtname.Text = "";
        txtTDEE.Text = "";
        txtweight.Text = "";
        divUserForm.Visible = false;
        divGrid.Visible = true;
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
}