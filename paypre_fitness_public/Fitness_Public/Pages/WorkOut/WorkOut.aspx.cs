using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.UI.HtmlControls;

public partial class Pages_WorkOut_WorkOut : System.Web.UI.Page
{

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            Session["BaseUrlToken"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrlToken"].Trim();
            Session["Master_Branch"] = Session["branchId"].ToString();
            GetCategory();
            Master.DdlBranch.Visible = false;
        }

    }
    #endregion

    #region Category List 

    public void GetCategory()
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
                string Endpoint = "UserWorkOutPlan/GetCategoryTypeBasedonDateDayCategory?userId=" + Session["userId"] +"" +
                    "&categoryId="+ Session["categoryId"].ToString() + "&date="+ Date + "&day="+ day + "";
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
                            dt.Columns.Add("imageUrl");
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i]["workoutCatTypeName"].ToString() == "HIIT" ||
                                    dt.Rows[i]["workoutCatTypeName"].ToString() == "Hiit")
                                {
                                    dt.Rows[i]["imageUrl"] = "hiit.jpg";
                                }

                                else if (dt.Rows[i]["workoutCatTypeName"].ToString() == "ZUMBA" ||
                                    dt.Rows[i]["workoutCatTypeName"].ToString() == "Zumba")
                                {
                                    dt.Rows[i]["imageUrl"] = "Zumba.jpg";

                                }
                                else if (dt.Rows[i]["workoutCatTypeName"].ToString() == "YOGA" ||
                                    dt.Rows[i]["workoutCatTypeName"].ToString() == "Yoga")
                                {
                                    dt.Rows[i]["imageUrl"] = "yoga.jpg";
                                }
                                else if (dt.Rows[i]["workoutCatTypeName"].ToString() == "GROUP CLASSES" ||
                                   dt.Rows[i]["workoutCatTypeName"].ToString() == "Group Classes")
                                {
                                    dt.Rows[i]["imageUrl"] = "groupclasses.jpg";
                                }
                                else if (dt.Rows[i]["workoutCatTypeName"].ToString() == "DANCE" ||
                                  dt.Rows[i]["workoutCatTypeName"].ToString() == "Dance")
                                {
                                    dt.Rows[i]["imageUrl"] = "Dance.jpg";
                                }
                                else
                                {
                                    dt.Rows[i]["imageUrl"] = "../../Images/DietWotkOut/ph2.png";
                                }

                            }
                           
                            dtlCategory.DataSource = dt;
                            dtlCategory.DataBind();

                            dtlCategoryList.DataSource = dt;
                            dtlCategoryList.DataBind();
                        }
                        else
                        {
                            dtlCategory.DataBind();
                            dtlCategoryList.DataBind();

                        }
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                }
                else
                {
                    dtlCategory.DataSource = null;
                    dtlCategory.DataBind();

                    dtlCategoryList.DataSource = null;
                    dtlCategoryList.DataBind();
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

    #region Category Click
    protected void imgCat_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnk = sender as ImageButton;
        DataListItem dtl = lnk.NamingContainer as DataListItem;
        Label lblworkoutCatTypeId = dtl.FindControl("lblworkoutCatTypeId") as Label;
        ViewState["workoutCatTypeId"] = lblworkoutCatTypeId.Text;
        GetWorkType();
        divWorkOut.Visible = false;
        divWorkOutVideo.Visible = true;

        for (int i = 0; i < dtlCategoryList.Items.Count; i++)
        {
            Label lblworkoutCatType = dtlCategoryList.Items[i].FindControl("lblworkoutCatTypeId") as Label;
            LinkButton lnkCategoryList = dtlCategoryList.Items[i].FindControl("lnkCategoryList") as LinkButton;
            
            if (lblworkoutCatTypeId.Text == lblworkoutCatType.Text)
            {
                lnkCategoryList.CssClass = "lblWorkOutTypeHead lblWorkOutTypeHeadSelect";
            }
            else
            {
                lnkCategoryList.CssClass = "lblWorkOutTypeHead";

            }

        }
    }


    protected void lnkCategoryList_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        DataListItem dtl = lnk.NamingContainer as DataListItem;
        Label lblworkoutCatTypeId = dtl.FindControl("lblworkoutCatTypeId") as Label;
        ViewState["workoutCatTypeId"] = lblworkoutCatTypeId.Text;
        GetWorkType();
        divWorkOut.Visible = false;
        divWorkOutVideo.Visible = true;

        for (int i = 0; i < dtlCategoryList.Items.Count; i++)
        {
            Label lblworkoutCatType = dtlCategoryList.Items[i].FindControl("lblworkoutCatTypeId") as Label;
            LinkButton lnkCategoryList = dtlCategoryList.Items[i].FindControl("lnkCategoryList") as LinkButton;

            if (lblworkoutCatTypeId.Text == lblworkoutCatType.Text)
            {
                lnkCategoryList.CssClass = "lblWorkOutTypeHead lblWorkOutTypeHeadSelect";
            }
            else
            {
                lnkCategoryList.CssClass = "lblWorkOutTypeHead";

            }

        }

    }

    #endregion

    #region Workout Type 
    public void GetWorkType()
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
                string Endpoint = "UserWorkOutPlan/GetWorkoutTypeBasedonDateDay?userId=" + Session["userId"].ToString() +"&" +
                    "workoutCatTypeId=" + ViewState["workoutCatTypeId"].ToString() + "&date="+ Date + "&day="+ day + "";
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
                            dtlWorkoutType.DataSource = dt;
                            dtlWorkoutType.DataBind();
                            lblTotalWorkoutCount.Text = dt.Rows.Count.ToString();
                            int count = dt.AsEnumerable()
                            .Where(workout => workout["OverallCompletedStatus"].ToString() == "Yes").Count();
                            lblCompletedCount.Text = count.ToString();
                        }
                        else
                        {
                            dtlWorkoutType.DataBind();

                        }
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                }
                else
                {
                    dtlWorkoutType.DataSource = null;
                    dtlWorkoutType.DataBind();
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

    protected void dtlWorkoutType_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Label lblworkoutCatTypeId = e.Item.FindControl("lblworkoutCatTypeId") as Label;
            Label lblworkoutTypeId = e.Item.FindControl("lblworkoutTypeId") as Label;



            try
            {
                string Date = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime TodayDate = DateTime.Now.Date;
                string s = TodayDate.DayOfWeek.ToString();
                string day = s.Substring(0, 2);

                var dtlSetps = e.Item.FindControl("dtlSetps") as DataList;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                    string Endpoint = "UserWorkOutPlan/GetSetTypeBasedonDate?userId="+ Session["userId"].ToString() + "&" +
                        "workoutCatTypeId=" + lblworkoutCatTypeId.Text + "&workoutTypeId=" + lblworkoutTypeId.Text + "&date="+ Date + "&day="+ day + "";
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
                                dtlSetps.DataSource = dt;
                                dtlSetps.DataBind();
                            }
                            else
                            {
                                dtlSetps.DataBind();

                            }
                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                        }
                    }
                    else
                    {
                        dtlSetps.DataSource = null;
                        dtlSetps.DataBind();
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
    }

    #region Workout Done Check Change

    protected void chkWorkoutDone_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox lnkbtn = sender as CheckBox;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;
        Label lblcsetType = (Label)gvrow.FindControl("lblcsetType");
        Label lblcnoOfRepss = (Label)gvrow.FindControl("lblcnoOfRepss");
        Label lblcweight = (Label)gvrow.FindControl("lblcweight");
        Label lblworkoutCatTypeId = (Label)gvrow.FindControl("lblworkoutCatTypeId");
        Label lblworkoutTypeId = (Label)gvrow.FindControl("lblworkoutTypeId");
        Label lblbookingId = (Label)gvrow.FindControl("lblbookingId");
        Label lbluserId = (Label)gvrow.FindControl("lbluserId");
        InsertWorkout(lblworkoutCatTypeId.Text, lblworkoutTypeId.Text, lblbookingId.Text, lblcsetType.Text, lblcnoOfRepss.Text, lblcweight.Text, lbluserId.Text);
    }

    #endregion
    #region InsertWorkoutTracking
    public void InsertWorkout(string workoutCatTypeId, string workoutTypeId, string bookingId, string setType, string noOfReps,
            string weight, string userId)
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
                var Insert = new InsertWorkoutTracking()
                {
                    workoutCatTypeId = workoutCatTypeId.Trim(),
                    workoutTypeId = workoutTypeId.Trim(),
                    bookingId = bookingId.Trim(),
                    date = TodayDate.ToString("yyyy-MM-dd").Trim(),
                    day = day.Trim(),
                    setType = setType.Trim(),
                    noOfReps = noOfReps.Trim(),
                    weight = weight.Trim(),
                    userId = userId.Trim(),
                    createdBy = userId.Trim()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("UserWorkOutTracking/insert", Insert).Result;
                var Fitness = response.Content.ReadAsStringAsync().Result;
                int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();
                if (response.IsSuccessStatusCode)
                {

                    if (StatusCode == 1)
                    {
                        GetWorkType();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }

    #endregion


    public class InsertWorkoutTracking
    {
        public string workoutCatTypeId { get; set; }
        public string workoutTypeId { get; set; }
        public string bookingId { get; set; }
        public string date { get; set; }
        public string day { get; set; }
        public string setType { get; set; }
        public string noOfReps { get; set; }
        public string weight { get; set; }
        public string userId { get; set; }
        public string createdBy { get; set; }
    }



    public class InsertTranUserFoodTracking
    {
        public List<UserFoodTracking> lstTranUserFoodTracking { get; set; }
    }


    public class UserFoodTracking
    {
        public string userId { get; set; }
        public string bookingId { get; set; }
        public string foodMenuId { get; set; }
        public string mealtypeId { get; set; }
        public string consumingStatus { get; set; }
        public string date { get; set; }
        public string createdBy { get; set; }

    }


    protected void dtlSetps_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            CheckBox chkWorkoutDone = (CheckBox)e.Item.FindControl("chkWorkoutDone");

            // Assign the OnClientClick event using the UniqueID of the CheckBox control
            chkWorkoutDone.Attributes["onclick"] = "return confirmClick('" + chkWorkoutDone.UniqueID + "');";
        }
    }
}
