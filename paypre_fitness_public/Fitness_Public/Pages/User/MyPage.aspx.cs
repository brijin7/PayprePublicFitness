using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Runtime.Remoting.Messaging;
using System.Configuration;

public partial class Pages_User_MyPage : System.Web.UI.Page
{

    Helper helper = new Helper();
    readonly private string BaseUri;
    readonly string checkUserbookingHasApprovedUri;
    readonly string getYoutubeLiveUrlUri;
    readonly string getSubscriptionDetails;
    readonly string checkUserHasLoginUri;
    string token;
    public Pages_User_MyPage()
    {
        BaseUri = $"{ConfigurationManager.AppSettings["BaseUrl"].Trim()}";
        checkUserbookingHasApprovedUri = $"{BaseUri}GetUserBookingDetailsBasedOnUserId?";
        getYoutubeLiveUrlUri = $"{BaseUri}liveConfig/LiveDate?";
        getSubscriptionDetails = $"{BaseUri}UserSubspBookingDetails?";
        checkUserHasLoginUri = $"{BaseUri}UserBookingDetails?";
    }

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Master_btnDiet.Visible = false;
        Master.Master_btnWorkout.Visible = false;
        Master.Master_hfImageUrl.Visible = false;
        token = $"{Session["APIToken"]}";

        if (CheckUserhasbookingasApproved(Session["userId"].ToString()))
        {
            if (Session["SlotTime"].ToString() == "")
            {
                UserPlanDetails.Visible = false;
                Nouserplan.Visible = true;
                workoutlink.Visible = false;
                dietplanlink.Visible = false;
            }
            else
            {
                UserPlanDetails.Visible = true;
                Nouserplan.Visible = false;
                workoutlink.Visible = true;
                dietplanlink.Visible = true;
                Master.Master_btnDiet.Visible = true;
                Master.Master_btnWorkout.Visible = true;
                Master.Master_btnMyPage.Visible = true;
                SlotTime.InnerText = Session["SlotTime"].ToString();
                TrainerName.InnerText = Session["TrainerName"].ToString();
                if (Session["traningMode"].ToString() == "D")
                {
                    traningMode.InnerText = "Direct";
                }
                else
                {
                    traningMode.InnerText = "Online";
                }

                TrainermobileNo.InnerText = Session["TrainermobileNo"].ToString();
                categoryname.InnerText = Session["categoryName"].ToString();
                planduration.InnerText = Session["PlanDuration"].ToString();
            }
        }
        else
        {
            UserPlanDetails.Visible = false;
            Nouserplan.Visible = true;
            workoutlink.Visible = false;
            dietplanlink.Visible = false;
            Master.Master_btnDiet.Visible = false;
            Master.Master_btnWorkout.Visible = false;
            Master.Master_btnMyPage.Visible = false;
        }

        if (!IsPostBack)
        {
            Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            Session["BaseUrlToken"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrlToken"].Trim();
            Session["Master_Branch"] = Session["branchId"].ToString();
            GetCalories();
            GetWorkout();
            GetSubscriptionDetails();
            Master.DdlBranch.Visible= false;
        }

        if(Session["userName"].ToString() != " ")
        {
            lblUserName.Text = Session["userName"].ToString() + "!";
        }
        else if (Session["mobileNo"].ToString() != " ")
        {
            lblUserName.Text = Session["mobileNo"].ToString() + "!";
        }
        else
        {
            lblUserName.Text = Session["mailId"].ToString() + "!";
        }
        GetLiveUrl();
        CheckUserhasbooking(Session["userId"].ToString());
    }
   
    #endregion
    #region Get Calories 
    public void GetCalories()
    {
        try
        {
            string Date = DateTime.Now.ToString("yyyy-MM-dd");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "tranUserFoodTracking/DateBasedFoodCalories?userId=" + Session["userId"].ToString() + "&date=" + Date + "";

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
                            lblTotalCal.Text = dt.Rows[0]["TotalCalories"].ToString();
                            lblConsumedCal.Text = dt.Rows[0]["ConsumedCalories"].ToString();

                        }
                        else
                        {
                            lblTotalCal.Text ="0";
                            lblConsumedCal.Text = "0";

                        }
                    }
                    else
                    {
                        lblTotalCal.Text = "0";
                        lblConsumedCal.Text = "0";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                }
                else
                {

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
    #region Get Workout 
    public void GetWorkout()
    {
        try
        {
            string Date = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime TodayDate = DateTime.Now.Date;
            string s = TodayDate.DayOfWeek.ToString();
            string day = s.Substring(0, 2);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "UserWorkOutTracking/GetTranUserWorkOutTrackingBasedonDateDay?userId=" + Session["userId"].ToString() + "" +
                    "&date=" + Date + "&day=" + day + "";

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
                            lblTotalWorkoutCount.Text = dt.Rows.Count.ToString();
                            int count = dt.AsEnumerable()
                            .Where(workout => workout["OverallCompletedStatus"].ToString() == "Yes").Count();
                            lblCompletedCount.Text = count.ToString() + " " + "/";

                        }
                        else
                        {
                            lblTotalWorkoutCount.Text = "0";
                        }
                    }
                    else
                    {
                        lblTotalWorkoutCount.Text = "0";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                }
                else
                {

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
    #region this method is used get the live url
    public void GetLiveUrl()
    {
        try
        {
            string requestUri = $"{getYoutubeLiveUrlUri}branchId=" + Session["branchId"].ToString() + "&categoryId=" + Session["categoryId"].ToString() + "&trainerId=" + Session["trainerId"].ToString() + "" +
                 "&trainingTypeId=" + Session["trainingTypeId"].ToString() + "&slotId=" + Session["slotId"].ToString() + "&date=" + DateTime.Now.ToString("yyyy-MM-dd") + "";
            Helper Helper = new Helper();
            Helper.APIGet(requestUri, token, out DataTable dt, out int statusCode, out string response);
            if (statusCode == 1)
            {
                if (dt.Rows[0]["liveurl"] != null)
                {
                  
                        string liveUrl = dt.Rows[0]["liveurl"] == null ? "" : dt.Rows[0]["liveurl"].ToString();
                        iliveUrl.Src = liveUrl+ "?rel=0&amp;autoplay=1&mute=1";
                        videoheading.InnerText = "Live Video";

                }
                else
                {
                    iliveUrl.Src = "https://www.youtube.com/embed/i7LJ-39bFgE?rel=0&amp;autoplay=1&mute=1";
                    videoheading.InnerText = "Demo Video";
                }
            }
            else
            {
                iliveUrl.Src = "https://www.youtube.com/embed/i7LJ-39bFgE?rel=0&amp;autoplay=1&mute=1";
                videoheading.InnerText = "Demo Video";
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
                Session["PlanDuration"] = dt.Rows[0]["fromDate"].ToString() + " / " + dt.Rows[0]["toDate"].ToString();
                Session["slotId"] = dt.Rows[0]["slotId"].ToString();
                Session["trainingTypeId"] = dt.Rows[0]["trainingTypeId"].ToString();
                Session["trainerId"] = dt.Rows[0]["trainerId"].ToString();
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
                Session["categoryId"] = "";
                Session["PlanDuration"] = "";
                Session["slotId"] = "";
                Session["trainingTypeId"] = "";
                Session["trainerId"] = "";
                return false;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region this method is used get the Subscription Details
    public void GetSubscriptionDetails()
    {
        try
        {
            string requestUri = $"{getSubscriptionDetails}userId=" + Session["userId"].ToString() + "";
            Helper Helper = new Helper();
            Helper.APIGet(requestUri, token, out DataTable dt, out int statusCode, out string response);
            if (statusCode == 1)

            {
                UserSubscriptionDetails.Visible = true;
                subheader.Visible = true;
                lblpackageName.InnerText= dt.Rows[0]["packageName"].ToString();
                lblbranchName.InnerText = dt.Rows[0]["branchName"].ToString();
                lblgymOwnerName.InnerText = dt.Rows[0]["gymOwnerName"].ToString();
                lblplanduration.InnerText = dt.Rows[0]["fromDate"].ToString() + " / " + dt.Rows[0]["toDate"].ToString();

            }
            else
            {
                UserSubscriptionDetails.Visible = false;
                subheader.Visible = false;
            }
        }
        catch (Exception)
        {
            throw;
        }
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
}