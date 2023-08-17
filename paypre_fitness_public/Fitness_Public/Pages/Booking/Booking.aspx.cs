using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Text.RegularExpressions;

public partial class Pages_Booking_Booking : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            Session["BaseUrlToken"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrlToken"].Trim();
            List<string> days = new List<string>();
            DataTable DtDays = new DataTable();
            DtDays.Columns.Add("Dates");
            DtDays.Columns.Add("days");
            for (int i = 0; i < 7; i++)
            {
                string Date = string.Empty;
                string Day = string.Empty;
                DtDays.NewRow();
                Date = DateTime.Now.AddDays(i).ToString("dd");
                Day = DateTime.Now.AddDays(i).ToString("ddd");
                DtDays.Rows.Add(Date, Day);

            }
            DtlDays.DataSource = DtDays;
            DtlDays.DataBind();

            BindWorkout();
            ddlTrainer.Items.Insert(0, new ListItem("Trainer List *", "0"));
        }

    }

    #endregion
    #region Payment Page Click 
    protected void BreakFast_Click(object sender, ImageClickEventArgs e)
    {
        BookingPopup.Visible = true;
        BindCategoryList();
        GetSwapType();
    }

    protected void imgBtnClass_Click(object sender, ImageClickEventArgs e)
    {
        BookingPopup.Visible = true;
        BindCategoryList();
        GetSwapType();
    }
    #endregion
    #region Bind Workout List
    public void BindWorkout()
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
                string Endpoint = "categoryMaster/GetCategoryForUser?gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + "";
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
                            DtlWorkout.DataSource = dt;
                            DtlWorkout.DataBind();
                        }
                        else
                        {
                            DtlWorkout.DataBind();
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
    #region Get Slot 
 
    public void GetSlot()
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
                string Endpoint = "GetSlotList?categoryId=" + Session["categoryId"].ToString() + "" +
                   "&trainingTypeId=" + ViewState["trainingTypeId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["SlotList"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            dtlSlot.DataSource = dt;
                            dtlSlot.DataBind();
                        }
                        else
                        {
                            dtlSlot.DataBind();

                        }
                    }
                    else
                    {
                       
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                }
                else
                {
                    dtlSlot.DataSource = null;
                    dtlSlot.DataBind();
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
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
                string Endpoint = "fitnessCategoryPrice/GetPriceOnPublicWeb?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                    "&branchId=" + Session["branchId"].ToString() + "&categoryId=" + Session["categoryId"].ToString() + "";
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
                            DtlCategoryList.DataSource = dt;
                            DtlCategoryList.DataBind();
                        }
                        else
                        {
                            DtlCategoryList.DataBind();
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
    #region Category Click
    protected void lblAll_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        DataListItem dtl = lnk.NamingContainer as DataListItem;
        Label PlanSelected = dtl.FindControl("PlanSelected") as Label;
        Label lblPlanName = dtl.FindControl("lblPlanName") as Label;
        Label lbltrainingTypeName = dtl.FindControl("lbltrainingTypeName") as Label;
        Label lblpriceId = dtl.FindControl("lblpriceId") as Label;
        Label lbltrainingTypeId = dtl.FindControl("lbltrainingTypeId") as Label;
        Label lblplanDurationId = dtl.FindControl("lblplanDurationId") as Label;
        Label lbltrainingMode = dtl.FindControl("lbltrainingMode") as Label;
        Label lblprice = dtl.FindControl("lblprice") as Label;
        Label lbltaxId = dtl.FindControl("lbltaxId") as Label;
        Label lbltaxName = dtl.FindControl("lbltaxName") as Label;
        Label lbltax = dtl.FindControl("lbltax") as Label;
        Label lblnetAmount = dtl.FindControl("lblnetAmount") as Label;
        Label lblSavedAmount = dtl.FindControl("lblSavedAmount") as Label;

        lblTotalAmt.Text = "₹" + " " +  lblnetAmount.Text;
        lblSavedAmt.Text = "₹" + " " + lblSavedAmount.Text;
        lblFinalAmt.Text = "₹" + " " + lblnetAmount.Text;

        ViewState["trainingTypeId"] = lbltrainingTypeId.Text;
        ViewState["planDurationId"] = lblplanDurationId.Text;
        ViewState["trainingMode"] = lbltrainingMode.Text;
        ViewState["priceId"] = lblpriceId.Text;
        ViewState["price"] = lblprice.Text;
        ViewState["taxId"] = lbltaxId.Text;
        ViewState["taxName"] = lbltaxName.Text;
        ViewState["tax"] = lbltax.Text;
        ViewState["netAmount"] = lblnetAmount.Text;

        

        for (int i = 0; i < DtlCategoryList.Items.Count; i++)
        {
            Label lblPlan = DtlCategoryList.Items[i].FindControl("lblPlanName") as Label;
            Label lbltrainingType = DtlCategoryList.Items[i].FindControl("lbltrainingTypeName") as Label;
            Label PlanSelecteds = DtlCategoryList.Items[i].FindControl("PlanSelected") as Label;
            Label lblpriceIds = DtlCategoryList.Items[i].FindControl("lblpriceId") as Label;
            HtmlControl divCategory = DtlCategoryList.Items[i].FindControl("divCategory") as HtmlControl;
            if (lblpriceId.Text == lblpriceIds.Text)
            {
                PlanSelecteds.CssClass = "lblPlanSelect lblPlanSelected";
                divCategory.Attributes["class"] = "divPlanCat divPlanCatSelect";
            }
            else
            {
                PlanSelecteds.CssClass = "lblPlanSelect";
                divCategory.Attributes["class"] = "divPlanCat";
            }
        }

        GetSlot();
    }
    #endregion
    #region Slot Click 
    protected void lblFromTime_Click(object sender, EventArgs e)
    {
        LinkButton lblFromTime_Click = sender as LinkButton;
        DataListItem gvrow = lblFromTime_Click.NamingContainer as DataListItem;
        Label lblslotId = gvrow.FindControl("lblslotId") as Label;
        ViewState["SlotId"] = lblslotId.Text;
        for (int i = 0; i < dtlSlot.Items.Count; i++)
        {
            LinkButton lnk = dtlSlot.Items[i].FindControl("lblFromTime") as LinkButton;
            Label slot = dtlSlot.Items[i].FindControl("lblslotId") as Label;

            if (lblslotId.Text == slot.Text)
            {
                lnk.CssClass = "ddlSlotBtnSelect";

            }
            else
            {
                lnk.CssClass = "ddlSlotBtn";
            }
        }
        GetTrainer();

    }
    #endregion
    #region Get Trainer 
    public void GetTrainer()
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
                string Endpoint = "GetTrainerList?categoryId=" + Session["categoryId"].ToString() + "" +
                   "&trainingTypeId=" + ViewState["trainingTypeId"].ToString() + "&slotId=" + ViewState["SlotId"].ToString() + " ";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["TrainerList"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            ddlTrainer.DataSource = dt;
                            ddlTrainer.DataTextField = "trainerName";
                            ddlTrainer.DataValueField = "trainerId";
                            ddlTrainer.DataBind();
                        }
                        else
                        {
                            ddlTrainer.DataBind();

                        }
                    }
                    else
                    {
                        ddlTrainer.Items.Clear();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    ddlTrainer.Items.Insert(0, new ListItem("Trainer List *", "0"));
                }
                else
                {
                    ddlTrainer.Items.Clear();
                    ddlTrainer.Items.Insert(0, new ListItem("Trainer List *", "0"));
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region Btn BuyNow Click
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
            var Insertjavasctipt = new BookingClass();

            string trainerId = string.Empty;
            string mobileormail = string.Empty;

            if (Session["mobileNo"].ToString().Trim() != "")
            {
                mobileormail = Session["mobileNo"].ToString().Trim();
            }
            else
            {
                mobileormail = Session["mailId"].ToString().Trim();
            }


            if (ddlTrainer.SelectedValue == "")
            {
                trainerId = "0";

            }
            else
            {
                trainerId = ddlTrainer.SelectedValue;

            }



            Insertjavasctipt.queryType = "insert";
            Insertjavasctipt.gymOwnerId = Session["gymOwnerId"].ToString();
            Insertjavasctipt.branchId = Session["branchId"].ToString();
            Insertjavasctipt.branchName = Session["Master_Branch"].ToString();
            //Insertjavasctipt.branchName = Session["branchName"].ToString();
            Insertjavasctipt.categoryId = Session["categoryId"].ToString();
            Insertjavasctipt.trainingTypeId = ViewState["trainingTypeId"].ToString();
            Insertjavasctipt.planDurationId = ViewState["planDurationId"].ToString();
            Insertjavasctipt.traningMode = ViewState["trainingMode"].ToString();
            Insertjavasctipt.phoneNumber = mobileormail;
            Insertjavasctipt.userId = Session["userId"].ToString();
            Insertjavasctipt.booking = "W";
            Insertjavasctipt.loginType = "U";
            Insertjavasctipt.trainerId = trainerId;
            Insertjavasctipt.slotId = ViewState["SlotId"].ToString();
            Insertjavasctipt.wakeUpTime = txtWakeUpTime.Text;
            Insertjavasctipt.slotSwapType = ddlSwapType.SelectedValue;
            Insertjavasctipt.priceId = ViewState["priceId"].ToString();
            Insertjavasctipt.price = ViewState["price"].ToString();
            Insertjavasctipt.taxId = ViewState["taxId"].ToString();
            Insertjavasctipt.taxName = ViewState["taxName"].ToString();
            Insertjavasctipt.taxAmount = ViewState["tax"].ToString();
            Insertjavasctipt.totalAmount = ViewState["netAmount"].ToString();
            Insertjavasctipt.paymentCyclesStatus = "0";
            Insertjavasctipt.paymentCycles = "0";
            Insertjavasctipt.paidAmount = "0";
            Insertjavasctipt.paymentStatus = "N";
            Insertjavasctipt.paymentType = "69";
            Insertjavasctipt.createdBy = Session["userId"].ToString();

            string output = JsonConvert.SerializeObject(Insertjavasctipt);
            hfSubscriptionInsert.Value = output;
            hfBaseurl.Value = Session["BaseUrl"].ToString() + "booking";
            hfToken.Value = Session["APIToken"].ToString();
            hfPaymentBaseurl.Value = Session["BaseUrl"].ToString() + "BookingDetails";
            hfNotificationSMSBaseurl.Value = Session["BaseUrl"].ToString() + "sendBookingNotification";
            hfNotificationSMSDatas.Value = Session["mobileNo"].ToString();
            hfNotificationSMSUserId.Value = Session["userId"].ToString();

            //InsertBooking();
        }
        else
        {
            DivLogin.Visible = true;
        }
    }

    #endregion

    #region Get SwapType 
    public void GetSwapType()
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
                string Endpoint = "configMaster/getDropDownDetails?typeId=30";
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
                            ddlSwapType.DataSource = dt;
                            ddlSwapType.DataTextField = "configName";
                            ddlSwapType.DataValueField = "configId";
                            ddlSwapType.DataBind();
                        }
                        else
                        {
                            ddlSwapType.DataBind();

                        }
                    }
                    else
                    {
                       
                        ddlSwapType.Items.Clear();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    ddlSwapType.Items.Insert(0, new ListItem("Slot swapType *", "0"));
                }
                else
                {
                   
                    ddlSwapType.Items.Clear();
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion

    public void InsertBooking()
    {
        try
        {
            string trainerId = string.Empty;
            string mobileormail = string.Empty;

            if (Session["mobileNo"].ToString().Trim() != "")
            {
                mobileormail = Session["mobileNo"].ToString().Trim();
            }
            else
            {
                mobileormail = Session["mailId"].ToString().Trim();
            }


            if (ddlTrainer.SelectedValue == "")
            {
                trainerId = "0";

            }
            else
            {
                trainerId = ddlTrainer.SelectedValue;

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
                    branchId = Session["Master_Branch"].ToString(),
                    branchName = "Hosur",
                    categoryId = Session["categoryId"].ToString(),
                    trainingTypeId = ViewState["trainingTypeId"].ToString(),
                    planDurationId = ViewState["planDurationId"].ToString(),
                    traningMode = ViewState["trainingMode"].ToString(),
                    phoneNumber = mobileormail,
                    userId = Session["userId"].ToString(),
                    booking = "W",
                    loginType = "U",
                    trainerId = trainerId,
                    slotId = ViewState["SlotId"].ToString(),
                    wakeUpTime = txtWakeUpTime.Text,
                    slotSwapType = ddlSwapType.SelectedValue,
                    priceId = ViewState["priceId"].ToString(),
                    price = ViewState["price"].ToString(),
                    taxId = ViewState["taxId"].ToString(),
                    taxName = ViewState["taxName"].ToString(),
                    taxAmount = ViewState["tax"].ToString(),
                    totalAmount = ViewState["netAmount"].ToString(),
                    paymentCyclesStatus = "0",
                    paymentCycles = "0",
                    paidAmount = ViewState["netAmount"].ToString(),
                    paymentStatus = "P",
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
                BookingPopup.Visible = false;
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
        public string planDurationId { get; set; }
        public string traningMode { get; set; }
        public string phoneNumber { get; set; }
        public string userId { get; set; }
        public string loginType { get; set; }
        public string trainerId { get; set; }
        public string slotId { get; set; }
        public string wakeUpTime { get; set; }
        public string slotSwapType { get; set; }
        public string priceId { get; set; }
        public string price { get; set; }
        public string taxId { get; set; }
        public string taxName { get; set; }
        public string taxAmount { get; set; }
        public string totalAmount { get; set; }
        public string paidAmount { get; set; }
        public string paymentStatus { get; set; }
        public string paymentCycles { get; set; }
        public string paymentType { get; set; }
        public string paymentCyclesStatus { get; set; }
        public string createdBy { get; set; }

    }
    #endregion



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
                            //Session["mobileNo"] = dt.Rows[0]["mobileNo"].ToString();
                            //Session["userId"] = dt.Rows[0]["userId"].ToString();
                            //Session["userRole"] = dt.Rows[0]["roleName"].ToString();
                            //Session["mailId"] = dt.Rows[0]["mailId"].ToString();

                            Session["userId"] = dt.Rows[0]["userId"].ToString();
                            Session["roleId"] = dt.Rows[0]["roleId"].ToString();
                            Session["userRole"] = dt.Rows[0]["roleName"].ToString();
                            Session["userName"] = dt.Rows[0]["UserName"].ToString();
                            Session["mailId"] = dt.Rows[0]["mailId"].ToString();
                            Session["mobileNo"] = dt.Rows[0]["mobileNo"].ToString();
                            Session["branchName"] = dt.Rows[0]["branchName"].ToString();
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
                            DivLogin.Visible = true;
                            txtotp.Visible = false;
                            btnCfmOtp.Visible = false;
                            btnResend.Visible = false;
                            lblOtp.Visible = false;
                            txtMobileNo.Text = string.Empty;
                            txtMail.Text = string.Empty;
                            txtotp.Text = string.Empty;
                            txtMobileNo.Enabled = true;
                            //ScriptManager.RegisterStartupScript(this, GetType(), "Popup",
                            //   "infoalert('Invalid User');", true);
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
        //InsertBooking();
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



    protected void Closebuttondetails_Click1(object sender, EventArgs e)
    {
        BookingPopup.Visible = false;
    }

}