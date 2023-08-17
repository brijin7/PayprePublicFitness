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
using System.Reflection.Emit;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Pages_MyProfile : System.Web.UI.Page
{
    Helper helper = new Helper();
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            Session["BaseUrlToken"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrlToken"].Trim();
            GetUserDetails();

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
                        if (imgpreview.Src == "")
                        {
                            imgpreview.Src = "~/Pages/MyProfile/User.png";
                        }
                        Master.Master_lblUserName.Text = dt.Rows[0]["firstName"].ToString();
                        if (dt.Rows[0]["photoLink"] == "")
                        {
                            Master.Master_userimg.ImageUrl = "../../Images/Login/UserProfile.png";
                        }
                        else
                        {
                            Master.Master_userimg.ImageUrl = dt.Rows[0]["photoLink"].ToString();
                        }
                        if(txtMobileNo.Text == "")
                        {
                            txtMobileNo.Enabled = true;
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
                if(imgpreview.Src == "../../Pages/MyProfile/User.png")
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
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        GetUserDetails();
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
    #region Btn Submit 
    protected void btnSubSubmit_Click(object sender, EventArgs e)
    {
        UpdateUser();
    }
    #endregion
    #region Btn Cancel
    protected void btnSubCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../Home.aspx");
    }
    #endregion
    #region Btn Back Click 
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../../Home.aspx");
    }
    #endregion
}