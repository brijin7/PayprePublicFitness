using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

public partial class Pages_DietWorkOut_WorkOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            paraWorkOut.InnerText = Session["WorkOut"].ToString();
            BindWorkOutCategoryList();

        }
    }
    #region BindWorkOutCAtegory
    public void BindWorkOutCategoryList()
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
                string sUrl = Session["BaseUrl"].ToString().Trim() + "UserWorkOutPlan/GetWorkoutTypeBasedonDateDay?userId="
                     + Session["userId"].ToString().Trim() + "&date=" + TodayDate.ToString("yyyy-MM-dd") + "&day=" + day.Trim() + "&workoutCatTypeId=" + Session["WorkOutId"].ToString() + "";

                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        dtlWorkOutCategory.Visible = true;
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            dtlWorkOutCategory.DataSource = dt;
                            dtlWorkOutCategory.DataBind();
                            if (dt.Rows.Count == 1)
                            {
                                HtmlControl div = dtlWorkOutCategory.Items[0].FindControl("divWorkOuttargetItems") as HtmlControl;
                                div.Attributes["class"] = "col-11 col-sm-3 col-md-3 col-lg-3 divWorkOuttargetItems";

                            }
                            if (dt.Rows.Count == 2)
                            {
                                for (int j = 0; j < dtlWorkOutCategory.Items.Count; j++)
                                {
                                    HtmlControl div = dtlWorkOutCategory.Items[j].FindControl("divWorkOuttargetItems") as HtmlControl;
                                    div.Attributes["class"] = "col-11 col-sm-5 col-md-5 col-lg-5 divWorkOuttargetItems";
                                }
                              
                            }
                        }
                        else
                        {
                            dtlWorkOutCategory.DataBind();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }

                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region WorkOutCategory Click
    protected void WorkOutCat_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;
        Image ImgWorKoutType = (Image)gvrow.FindControl("ImgWorKoutType");
        Image ImageSelected = (Image)gvrow.FindControl("ImageSelected");
        Label lblWorkOutTypeCategory = (Label)gvrow.FindControl("lblWorkOutTypeCategory");
        Label lblVideoUrl = (Label)gvrow.FindControl("lblVideoUrl");
        string url = lblVideoUrl.Text + "?autoplay=1&modestbranding=1&mode=opaque&amp;rel=0&amp;autohide=0&amp;showinfo=0&amp;wmode=transparent";
        Label lblgymOwnerId = (Label)gvrow.FindControl("lblgymOwnerId");
        Label lblbranchId = (Label)gvrow.FindControl("lblbranchId");
        Label lblworkoutCatTypeId = (Label)gvrow.FindControl("lblworkoutCatTypeId");
        Label lblworkoutTypeId = (Label)gvrow.FindControl("lblworkoutTypeId");
        Label lblbookingId = (Label)gvrow.FindControl("lblbookingId");
        Label lbluserId = (Label)gvrow.FindControl("lbluserId");
        Label lblOverAllCompletedStatus = (Label)gvrow.FindControl("lblOverAllCompletedStatus");
        for (int i = 0; i < dtlWorkOutCategory.Items.Count; i++)
        {
            Label workoutTypeId = dtlWorkOutCategory.Items[i].FindControl("lblworkoutTypeId") as Label;
            if (workoutTypeId.Text == lblworkoutTypeId.Text)
            {
                HtmlControl divWorkOuttargetItems = dtlWorkOutCategory.Items[i].FindControl("divWorkOuttargetItems") as HtmlControl;
                divWorkOuttargetItems.Attributes["class"] = "col-11 col-sm-12 col-md-9 col-lg-9 divWorkOuttargetItemsSelection";
                if (dtlWorkOutCategory.Items.Count == 1)
                {

                    divWorkOuttargetItems.Attributes["class"] = "col-11 col-sm-3 col-md-3 col-lg-3 divWorkOuttargetItemsSelection";

                }
                if (dtlWorkOutCategory.Items.Count == 2)
                {

                    divWorkOuttargetItems.Attributes["class"] = "col-11 col-sm-6 col-md-63 col-lg-6 divWorkOuttargetItemsSelection";
                }
            }
            else
            {
                HtmlControl divWorkOuttargetItems = dtlWorkOutCategory.Items[i].FindControl("divWorkOuttargetItems") as HtmlControl;
                divWorkOuttargetItems.Attributes["class"] = "col-11 col-sm-12 col-md-9 col-lg-9 divWorkOuttargetItems";
                if (dtlWorkOutCategory.Items.Count == 1)
                {

                    divWorkOuttargetItems.Attributes["class"] = "col-11 col-sm-3 col-md-3 col-lg-3 divWorkOuttargetItems";

                }
                if (dtlWorkOutCategory.Items.Count == 2)
                {

                    divWorkOuttargetItems.Attributes["class"] = "col-11 col-sm-6 col-md-63 col-lg-6 divWorkOuttargetItems";
                }
            }

        }

        DivWorkoutConsumed.Visible = true;
        ImgConsumed.ImageUrl = ImgWorKoutType.ImageUrl;

        lblWorkOuts.Text = Session["WorkOut"].ToString().Trim();
        lblWorkoutConsumed.Text = lblWorkOutTypeCategory.Text;
        BindSets(lbluserId.Text, lblworkoutCatTypeId.Text, lblworkoutTypeId.Text);
        ConsumedVideo.Attributes["src"] = url;
        ViewState["workoutCatTypeId"] = lblworkoutCatTypeId.Text;
        ViewState["workoutTypeId"] = lblworkoutTypeId.Text;
        ViewState["bookingId"] = lblbookingId.Text;
        ViewState["userId"] = lbluserId.Text;

    }
    #endregion
    #region Bindsets
    public void BindSets(string userId, string workoutCatTypeId, string workoutTypeId)
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "UserWorkOutPlan/GetSetTypeBasedonDate?userId="
                     + userId.Trim() + "&date=" + TodayDate + "&day=" + day.Trim() + "" +
                     "&workoutCatTypeId=" + workoutCatTypeId.Trim() + "&workoutTypeId=" + workoutTypeId.Trim() + "";

                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        dtlSets.Visible = true;
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            dtlSets.DataSource = dt;
                            dtlSets.DataBind();
                        }
                        else
                        {
                            DivWorkoutConsumed.Visible = false;
                            dtlSets.DataBind();
                        }
                    }
                    else
                    {
                        DivWorkoutConsumed.Visible = false;
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }

                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Check Finished Click
    protected void chkFinished_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox lnkbtn = sender as CheckBox;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;
        Label lblcsetType = (Label)gvrow.FindControl("lblcsetType");
        Label lblReps = (Label)gvrow.FindControl("lblReps");
        Label lblWeight = (Label)gvrow.FindControl("lblWeight");
        Label lblworkoutCatTypeId = (Label)gvrow.FindControl("lblworkoutCatTypeId");
        Label lblworkoutTypeId = (Label)gvrow.FindControl("lblworkoutTypeId");
        Label lblbookingId = (Label)gvrow.FindControl("lblbookingId");
        Label lbluserId = (Label)gvrow.FindControl("lbluserId");
        Label lblOverAllCompletedStatus = (Label)gvrow.FindControl("lblOverAllCompletedStatus");
        InsertBranch(lblworkoutCatTypeId.Text, lblworkoutTypeId.Text, lblbookingId.Text, lblcsetType.Text, lblReps.Text, lblWeight.Text, lbluserId.Text);
    }
    #endregion
    #region InsertWorkoutTracking
    public void InsertBranch(string workoutCatTypeId, string workoutTypeId, string bookingId, string setType, string noOfReps,
        string weight, string userId)
    {
        try
        {
            DateTime TodayDate = DateTime.Now;
            string s = TodayDate.DayOfWeek.ToString();
            string day = s.Substring(0, 2);
            Session["userId"] = userId;
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
                    createdBy = Session["userId"].ToString().Trim()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("UserWorkOutTracking/insert", Insert).Result;
                var Fitness = response.Content.ReadAsStringAsync().Result;
                int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();
                if (response.IsSuccessStatusCode)
                {

                    if (StatusCode == 1)
                    {
                        BindSets(Session["userId"].ToString(), workoutCatTypeId.Trim(), workoutTypeId.Trim());
                        BindWorkOutCategoryList();
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

    protected void OnClick(object sender, EventArgs e)
    {
        int repeatcolumn = Convert.ToInt32(hfColumnRepeat.Value);
        this.RsetepeatColumns(repeatcolumn);
    }
    private void RsetepeatColumns(int repeatcolumn = 3)
    {
        dtlWorkOutCategory.RepeatColumns = repeatcolumn;
    }
    #region InsertClass
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
    #endregion
}