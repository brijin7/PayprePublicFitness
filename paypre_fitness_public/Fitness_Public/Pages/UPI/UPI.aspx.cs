using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Security.Policy;


public partial class Pages_UPI_UPI : System.Web.UI.Page
{
    readonly Helper Helper = new Helper();
    readonly string BaseUri;
    readonly string BaseTokenUri;
    string Token;
    string SubscriptionBookingURL;
    string ClassesBookingURL;
    string PaymentUPIURL;
    public Pages_UPI_UPI() 
    {
   
        BaseUri = $"{ConfigurationManager.AppSettings["BaseUrl"].Trim()}";
        BaseTokenUri = $"{ConfigurationManager.AppSettings["BaseUrlToken"].Trim()}";
        SubscriptionBookingURL = $"{BaseUri}BookingIdBasedSubspBookingDetails";
        ClassesBookingURL = $"{BaseUri}BookingDetails";
        PaymentUPIURL = $"{BaseUri}PaymentUPI"; 
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["BookingId"] = Request.QueryString["BookingId"];
            Session["Plan"] = Request.QueryString["Plan"];
            GetApiToken();
            Token = $"{Session["APIToken"]}";
            if (Session["Plan"].ToString().Trim() == "S")
            {
                BindSubscriptionPlanDetails();
            }
            else
            {
                BindClassesPlanPlanDetails();
            }
        }        
    }
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
    #region Bind SubscriptionPlan Details
    public void BindSubscriptionPlanDetails()
    {
        try
        {
            string URI = $"{SubscriptionBookingURL}?bookingId={Session["BookingId"]}";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                branchname.Text = Dt.Rows[0]["branchName"].ToString();
                bookingid.Text = Dt.Rows[0]["subBookingId"].ToString();
                packagename.Text = Dt.Rows[0]["packageName"].ToString();
                noofdays.Text = Dt.Rows[0]["noOfDays"].ToString();
                transactionid.Text = Dt.Rows[0]["UserName"].ToString() == "" ? "Buddy" : Dt.Rows[0]["UserName"].ToString();
                amount.Text = Dt.Rows[0]["netAmount"].ToString();
                Session["Master_Branch"] = Dt.Rows[0]["branchName"].ToString();
                Session["BranchId"] = Dt.Rows[0]["branchId"].ToString();
                Session["GymownerId"] = Dt.Rows[0]["gymOwnerId"].ToString();
                Session["bookingid"] = Dt.Rows[0]["subBookingId"].ToString();
                Session["branchName"] = Dt.Rows[0]["branchName"].ToString();
                Session["amount"] = Dt.Rows[0]["netAmount"].ToString();
                hfBookingId.Value = Dt.Rows[0]["subBookingId"].ToString();
                hfAmount.Value = Dt.Rows[0]["netAmount"].ToString();
                BindPaymentUpiDetails();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Response.ToString().Trim() + "');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region Bind  ClassesPlan Details
    public void BindClassesPlanPlanDetails()
    {
        try
        {
            string URI = $"{ClassesBookingURL}?bookingId={Session["BookingId"]}";
            Helper.APIGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                branchname.Text = Dt.Rows[0]["branchName"].ToString();
                bookingid.Text = Dt.Rows[0]["bookingId"].ToString();
                packagename.Text = Dt.Rows[0]["categoryName"].ToString();
                noofdays.Text = Dt.Rows[0]["PlaneDuration"].ToString();
                transactionid.Text = Dt.Rows[0]["UserName"].ToString();
                amount.Text = Dt.Rows[0]["totalAmount"].ToString();
                Session["Master_Branch"] = Dt.Rows[0]["branchName"].ToString();
                Session["BranchId"] = Dt.Rows[0]["branchId"].ToString();
                Session["GymownerId"] = Dt.Rows[0]["gymOwnerId"].ToString();
                Session["bookingid"] = Dt.Rows[0]["bookingId"].ToString();
                Session["branchName"] = Dt.Rows[0]["branchName"].ToString();
                Session["amount"] = Dt.Rows[0]["totalAmount"].ToString();
                hfBookingId.Value = Dt.Rows[0]["bookingId"].ToString();
                hfAmount.Value = Dt.Rows[0]["totalAmount"].ToString();
                BindPaymentUpiDetails();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Response.ToString().Trim() + "');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion

    #region Bind  Payment Details
    public void BindPaymentUpiDetails()
    {
        try
        {
            string URI = $"{PaymentUPIURL}?gymOwnerId={Session["GymownerId"]}&branchId={Session["BranchId"]}";
            Helper.APIPaymentGet(URI, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                hfMerchantCode.Value = Dt.Rows[0]["merchantCode"].ToString();
                hfMerchantId.Value = Dt.Rows[0]["merchantId"].ToString();
                hfMerchantName.Value = Dt.Rows[0]["name"].ToString();
                hfmode.Value = Dt.Rows[0]["mode"].ToString();
                hforgid.Value = Dt.Rows[0]["orgId"].ToString();
                hfsign.Value = Dt.Rows[0]["sign"].ToString();
                hfTranUpiId.Value = Dt.Rows[0]["UPIId"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Response.ToString().Trim() + "');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
  
    //protected void payment_Click(object sender, EventArgs e)
    //{
    //    Token = $"{Session["APIToken"]}";
    //    if (Session["Plan"].ToString().Trim() == "S")
    //    {
    //        BindSubscriptionPlanDetails();
    //    }
    //    else
    //    {
    //        BindClassesPlanPlanDetails();
    //    }
    //    //Mam sented
    //    //       var win = window.open('intent://paypre.in/fitness/ER?url-para=upi://pay?pa=' + TranUpiId + '&pn=' + MerchantName + '&tid=' + MerchantId + '&tr='
    //    //+ TranOrderNo + '&tn=' + TranName + '&am=' + Amt + '&cu=INR&url=&mc=' + MerchantCode + '&type=fitness#Intent;scheme=https;action=android.intent.action.VIEW;package=com.prematix.paypreupilite;end');

    //    //We already Used
    //    //string url = "intent://prematix.solutions/fitness?url-para=`upi://pay?pa=" + hfTranUpiId.Value + "&pn=" + hfMerchantName.Value + "&tr=" + Session["bookingid"].ToString().Trim() +
    //    //    "&tn=" + Session["branchName"].ToString().Trim() + "&am=" + Session["amount"].ToString().Trim() + "&cu=INR&url=&parking-name=" + Session["branchName"].ToString().Trim() +
    //    //    "&merchant-code=" + hfMerchantCode.Value + "&mode=" + hfmode.Value + "&orgid=" + hforgid.Value + "&sign=" + hfsign.Value + "`#Intent;scheme=http;package=com.prematix.paypreupilite;end);";

    //    string url = "intent://paypre.in/fitness/ER?url-para=upi://pay?pa=" + hfTranUpiId.Value + "&pn=" + hfMerchantName.Value + "&tid=" + hfMerchantId.Value + "&tr=" + Session["bookingid"].ToString().Trim() +
    //   "&tn=" + Session["branchName"].ToString().Trim() + "&am=" + Session["amount"].ToString().Trim() + "&cu=INR&url=&mc=" + hfMerchantCode.Value + "&type=fitness#Intent;scheme=https;action=android.intent.action.VIEW;package=com.prematix.paypreupilite;end');";

    //    string data = "window.open('" + url + "')";

    //    ClientScript.RegisterStartupScript(this.GetType(), "script", data, true);
    //}

}