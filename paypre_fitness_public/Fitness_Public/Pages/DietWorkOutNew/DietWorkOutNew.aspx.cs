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
using static System.Net.WebRequestMethods;
using Microsoft.SqlServer.Server;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Activities.Expressions;
using System.Configuration;
using System.Reflection;
using System.Xml.Linq;
using System.Activities.Statements;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data.Services.Client;
using System.Web.Util;

public partial class Pages_DietWorkOut_DietWorkOut : System.Web.UI.Page
{
    IFormatProvider objEnglishDate = new System.Globalization.CultureInfo("en-GB", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["BaseUrl"] = ConfigurationManager.AppSettings["BaseUrl"].Trim();
            btnDiet.Style.Add("border", "2px solid #2C60AF");
            btnDiet.Style.Add("color", "#2C60AF");
            BindCategory();

            if (Session["mobileNo"] != null)
            {
                BindDiet();
            }
            else
            {
                divUser.Visible = false;
                divAll.Visible = true;
                BindDays();
            }
          
            BindCategoryList();
            lblSummaryDate.Text = DateTime.Now.ToString("d-MM-yyyy");

        }
    }

    #region btnDiet Click Event
    protected void btnDiet_Click(object sender, EventArgs e)
    {

        btnDiet.Style.Add("border", "2px solid #2C60AF");
        btnDiet.Style.Add("color", "#2C60AF");
        btnWorkOut.Style.Add("color", "#000");
        btnWorkOut.Style.Add("border", "2px solid #00000");
        //divDiet.Style.Add("border", "2px solid #2C60AF");
        divDiet.Visible = true;
        divWorkOut.Visible = false;
        BindCategory();
    }
    #endregion
    #region btnWorkOut Click Event
    protected void btnWorkOut_Click(object sender, EventArgs e)
    {

        btnDiet.Style.Add("border", "2px solid #00000");
        btnWorkOut.Style.Add("border", "2px solid #af2c2c");
        btnWorkOut.Style.Add("color", "#af2c2c");
        btnDiet.Style.Add("color", "#000");
        //divWorkOut.Style.Add("border", "2px solid #af2c2c");
        divDiet.Visible = false;
        divWorkOut.Visible = true;
        if (Session["mobileNo"] != null)
        {
            BindWorkOutListUser();
        }
        else
        {

            BindWorkout();
        }


    }
    #endregion
    #region Diet

    #region Bind Category Name
    public void BindCategory()
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


                string sUrl = Session["BaseUrl"].ToString().Trim() + "categoryMaster/GetCategoryForUser?gymOwnerId=" + Session["gymOwnerId"].ToString().Trim() + ""
                    + "&branchId=" + Session["branchId"].ToString() + "";

                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            dtlClass.DataSource = dt;
                            dtlClass.DataBind();
                            for (int i = 0; i < dtlClass.Items.Count; i++)
                            {
                                Label CategoryID = dtlClass.Items[i].FindControl("lblCategoryID") as Label;
                                Label lblCategoryName = dtlClass.Items[i].FindControl("lblCategory") as Label;
                                if(Session["myplan"].ToString() == "0")
                                {
                                    if (Session["categoryId"].ToString() == CategoryID.Text)
                                    {
                                        lblCategoryName.CssClass = "lblNavList NavListSelect";
                                    }
                                    else
                                    {
                                        lblCategoryName.CssClass = "lblNavList";

                                    }
                                }
                                else
                                {
                                    if (dt.Rows[0]["categoryId"].ToString() == CategoryID.Text)
                                    {
                                        lblCategoryName.CssClass = "lblNavList NavListSelect";
                                    }
                                    else
                                    {
                                        lblCategoryName.CssClass = "lblNavList";

                                    }
                                    Session["categoryName"] = dt.Rows[0]["categoryName"].ToString();
                                    Session["categoryId"] = dt.Rows[0]["categoryId"].ToString();

                                }
                               

                            }
                           
                            DietWorkname.Text = Session["categoryName"].ToString();
                        }
                        


                    }
                    else
                    {
                        dtlClass.DataSource = null;
                        dtlClass.DataBind();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }

                else
                {
                    divDiet.Visible = false;
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

    #region BindDiet
    public void BindDiet()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "UserBookingDetails?userId=" + Session["userId"].ToString() + "";


                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {

                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            divDiet.Visible = true;
                            DateTime Fromdate = DateTime.Parse(dt.Rows[0]["fromDate"].ToString(), objEnglishDate);
                            DateTime Todate = DateTime.Parse(dt.Rows[0]["toDate"].ToString(), objEnglishDate);
                            var TDiff = Todate.Subtract(Fromdate);
                            String DaysDiff = TDiff.TotalDays.ToString();
                            int Week = NumberOfWeeks(Fromdate, Todate);
                            int Sessions = Convert.ToInt32(DaysDiff);
                            string categoryName = dt.Rows[0]["categoryName"].ToString();
                            DietWorkname.Text = categoryName.Trim();
                            Session["categoryName"] = categoryName.Trim();
                            Session["categoryId"] = dt.Rows[0]["categoryId"].ToString();
                            Weeks.InnerText = Week + " Weeks " + " | " + Sessions + " Sessions ";
                            ViewState["bookingId"] = dt.Rows[0]["bookingId"].ToString();
                            for (int i = 0; i < dtlClass.Items.Count; i++)
                            {
                                Label CategoryID = dtlClass.Items[i].FindControl("lblCategoryID") as Label;
                                Label lblCategoryName = dtlClass.Items[i].FindControl("lblCategory") as Label;
                                if (Session["categoryId"].ToString() == CategoryID.Text)
                                {
                                    lblCategoryName.CssClass = "lblNavList NavListSelect";
                                }
                                else
                                {
                                    lblCategoryName.CssClass = "lblNavList";
                                    lblCategoryName.Visible = false;
                                }
                                Session["myplan"] = "0";

                            }
                            divUser.Visible = true;
                            divAll.Visible = false;
                            BindDaysUser();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        divUser.Visible = false;
                        divAll.Visible = true;
                        BindDays();

                    }
                }

                else
                {
                    divUser.Visible = false;
                    divAll.Visible = true;
                    BindDays();
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }

    public static int NumberOfWeeks(DateTime dateFrom, DateTime dateTo)
	{
		TimeSpan Span = dateTo.Subtract(dateFrom);

		if (Span.Days <= 7)
		{
			if (dateFrom.DayOfWeek > dateTo.DayOfWeek)
			{
				return 2;
			}

			return 1;
		}

		int Days = Span.Days - 7 + (int)dateFrom.DayOfWeek;
		int WeekCount = 1;
		int DayCount = 0;

		for (WeekCount = 1; DayCount < Days; WeekCount++)
		{
			DayCount += 7;
			if (DayCount > Days)
			{
				break;
			}
		}
		//DayCount = Span.Days;
		//WeekCount = Days / 7;
		return WeekCount;
	}
	#endregion

	#region Category Onclick
	protected void lnkCategory_Click(object sender, EventArgs e)
	{
		btnDiet.Style.Add("border", "2px solid #2C60AF");
		btnDiet.Style.Add("color", "#2C60AF");
		btnWorkOut.Style.Add("border", "1px solid #000");
		btnWorkOut.Style.Add("color", "#000");
		divDiet.Visible = true;
		divWorkOut.Visible = false;
		LinkButton lnk = sender as LinkButton;
        DataListItem dtl = lnk.NamingContainer as DataListItem;
        Label lblCategoryID = dtl.FindControl("lblCategoryID") as Label;
        Label lblCategory = dtl.FindControl("lblCategory") as Label;

        for (int i = 0; i < dtlClass.Items.Count; i++)
        {
            Label CategoryID = dtlClass.Items[i].FindControl("lblCategoryID") as Label;
            Label lblCategoryName = dtlClass.Items[i].FindControl("lblCategory") as Label;
            if (lblCategoryID.Text == CategoryID.Text)
            {
                lblCategoryName.CssClass = "lblNavList NavListSelect";
            }
            else
            {
                lblCategoryName.CssClass = "lblNavList";

            }

        }
        Session["categoryId"] = lblCategoryID.Text;
        Session["categoryName"] = lblCategory.Text;
        DietWorkname.Text = Session["CategoryName"].ToString();
        BindCategoryList();
        if (ViewState["bookingId"] != null)
        {
            BindDaysUser();
        }
        else
        {
            divUser.Visible = false;
            divAll.Visible = true;
            BindDays();
        }
        
    }
    #endregion

    #region Bind Days
    public void BindDays()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "CategoryDietPlan/GetWeekDays";


                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {

                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            dt.Columns.Add("ShowStatus");
                            dt.Columns.Add("Target");
                            dt.Columns.Add("Consumed");
                            decimal target = BindTargetCal();

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                decimal Consumed = (target * 75) / 100;
                                decimal percentage = (Consumed / target) * 100;
                                dt.Rows[i]["ShowStatus"] = "N";
                                dt.Rows[i]["Target"] = target;
                                dt.Rows[i]["Consumed"] = Consumed;

                            }
                            dt.Rows[0]["ShowStatus"] = "Y";
                            dtlDays.DataSource = dt;
                            dtlDays.DataBind();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                decimal Consumed = (target * 75) / 100;
                                decimal percentage = (Consumed / target) * 100;
                                HtmlGenericControl Progress1 = dtlDays.Items[i].FindControl("PrgDay") as HtmlGenericControl;
                                int pro = Convert.ToInt16(percentage);
                                if (pro <= 25)
                                {

                                    Progress1.Attributes.Add("Class", "progressBar25");
                                }
                                if (pro > 25 && pro <= 50)
                                {

                                    Progress1.Attributes.Add("Class", "progressBar50");
                                }
                                if (pro > 50 && pro <= 75)
                                {

                                    Progress1.Attributes.Add("Class", "progressBar75");

                                }
                                if (pro > 75 && pro <= 100)
                                {

                                    Progress1.Attributes.Add("Class", "progressBar100");
                                }
                                Progress1.Attributes.Add("Value", percentage.ToString());
                            }
                            divUser.Visible = false;
                            divAll.Visible = true;
                        }
                        else
                        {

                        }

                    }
                    else
                    {
                        divDiet.Visible = false;
                        dtlDays.DataSource = null;
                        dtlDays.DataBind();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }

                else
                {
                    divDiet.Visible = false;
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
    #region Bind Target
    public int BindTargetCal()
    {
        int TotalTarget = 0;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
            string sUrl1 = Session["BaseUrl"].ToString().Trim() + "CategoryDietPlan/GetPublicCategoryDietPlan?gymOwnerId=" + Session["gymOwnerId"].ToString().Trim() + ""
                  + "&branchId=" + Session["branchId"].ToString() + "&categoryId=" + Session["categoryId"].ToString() + "";

            HttpResponseMessage response = client.GetAsync(sUrl1).Result;

            if (response.IsSuccessStatusCode)
            {
                var FinessList = response.Content.ReadAsStringAsync().Result;
                int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                string ResponseMsg = JObject.Parse(FinessList)["CategoryDietPlan"].ToString();

                if (StatusCode == 1)
                {
                    divDiet.Visible = true;

                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                    if (dt.Rows.Count > 0)
                    {
                        List<breakFastDetails> foodItem = JsonConvert.DeserializeObject<List<breakFastDetails>>(ResponseMsg);
                        Session["foodItem"] = foodItem;

                        var dtMealtype = (from row in foodItem.AsEnumerable()
                                          group row by new
                                          {

                                              mealTypeId = row.mealTypeId,
                                              mealTypeName = row.mealtypeName,
                                              fromTime = row.fromTime,
                                              toTime = row.toTime,

                                          } into t1
                                          select new
                                          {

                                              mealTypeId = t1.Key.mealTypeId,
                                              mealTypeName = t1.Key.mealTypeName,
                                              fromTime = t1.Key.fromTime,
                                              toTime = t1.Key.toTime,
                                              calories = t1.Sum(x => Convert.ToInt16(x.calories)),

                                          })
                             .Select(g =>
                             {
                                 var h = dt.NewRow();

                                 h["mealTypeId"] = g.mealTypeId;

                                 h["mealTypeName"] = g.mealTypeName;
                                 h["fromTime"] = g.fromTime;
                                 h["toTime"] = g.toTime;

                                 h["calories"] = g.calories;

                                 return h;
                             }).CopyToDataTable();

                        TotalTarget = foodItem.AsEnumerable().Sum(row => Convert.ToInt16(row.calories));
                        ViewState["TotalTarget"] = TotalTarget;
                        Plan.Visible = true;
                        Summary.Visible = true;
                        divMainPlan.Attributes["class"] = "col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8";
                    }

                }
            }
            else
            {
                divDiet.Visible = false;
                
            }
            return TotalTarget;
        }



    }
    #endregion
    #region Bind MealType
    protected void dtlDays_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            var dtlDietPlan = e.Item.FindControl("dtlDietPlan") as DataList;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                    string sUrl1 = Session["BaseUrl"].ToString().Trim() + "CategoryDietPlan/GetPublicCategoryDietPlan?gymOwnerId=" + Session["gymOwnerId"].ToString().Trim() + ""
                          + "&branchId=" + Session["branchId"].ToString() + "&categoryId=" + Session["categoryId"].ToString() + "";

                    HttpResponseMessage response = client.GetAsync(sUrl1).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var FinessList = response.Content.ReadAsStringAsync().Result;
                        int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                        string ResponseMsg = JObject.Parse(FinessList)["CategoryDietPlan"].ToString();

                        if (StatusCode == 1)
                        {
                            divDiet.Visible = true;

                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                            if (dt.Rows.Count > 0)
                            {
                                List<breakFastDetails> foodItem = JsonConvert.DeserializeObject<List<breakFastDetails>>(ResponseMsg);
                                Session["foodItem"] = foodItem;

                                var dtMealtype = (from row in foodItem.AsEnumerable()
                                                  group row by new
                                                  {

                                                      mealTypeId = row.mealTypeId,
                                                      mealTypeName = row.mealtypeName,
                                                      fromTime = row.fromTime,
                                                      toTime = row.toTime,

                                                  } into t1
                                                  select new
                                                  {

                                                      mealTypeId = t1.Key.mealTypeId,
                                                      mealTypeName = t1.Key.mealTypeName,
                                                      fromTime = t1.Key.fromTime,
                                                      toTime = t1.Key.toTime,
                                                      calories = t1.Sum(x => Convert.ToInt16(x.calories)),

                                                  })
                                     .Select(g =>
                                     {
                                         var h = dt.NewRow();

                                         h["mealTypeId"] = g.mealTypeId;

                                         h["mealTypeName"] = g.mealTypeName;
                                         h["fromTime"] = g.fromTime;
                                         h["toTime"] = g.toTime;

                                         h["calories"] = g.calories;

                                         return h;
                                     }).CopyToDataTable();

                                //decimal TotalTarget = dtMealtype.AsEnumerable().Sum(row => Convert.ToDecimal(row.Field<double>("calories")));

                                dtlDietPlan.DataSource = dtMealtype;
                                dtlDietPlan.DataBind();
                                divDiet.Visible = true;

                            }
                        }
                        else
                        {
                            divDiet.Visible = false;
                            dtlDays.DataSource = null;
                            dtlDays.DataBind();
                        }
                    }
                    else
                    {
                        divDiet.Visible = false;
                        dtlDays.DataSource = null;
                        dtlDays.DataBind();

                    }


                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
            }
        }
    }
    #endregion
    #region Bind MealType
    protected void dtlDietPlan_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            List<breakFastDetails> foodItems = new List<breakFastDetails>();
            foodItems = (List<breakFastDetails>)Session["foodItem"];
            var foodItem = foodItems.ElementAt(0);
            var dataList = e.Item.FindControl("dtlFoodItem") as DataList;
            Label MealTYpeName = e.Item.FindControl("lblMealTYpeName") as Label;
            if (MealTYpeName.Text == "BreakFast")
            {
                var dtdiet = foodItems.Where(x => x.mealtypeName == "BreakFast").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
            if (MealTYpeName.Text == "Snacks1")
            {
                var dtdiet = foodItems.Where(x => x.mealtypeName == "Snacks1").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
            if (MealTYpeName.Text == "Lunch")
            {
                var dtdiet = foodItems.Where(x => x.mealtypeName == "Lunch").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
            if (MealTYpeName.Text == "Snacks2")
            {
                var dtdiet = foodItems.Where(x => x.mealtypeName == "Snacks2").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();

            }
            if (MealTYpeName.Text == "Dinner")
            {
                var dtdiet = foodItems.Where(x => x.mealtypeName == "Dinner").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
            if (MealTYpeName.Text == "Snacks3")
            {
                var dtdiet = foodItems.Where(x => x.mealtypeName == "Snacks3").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
        }
    }
    #endregion

    #region User Plan details
    #region Bind Days
    public void BindDaysUser()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "CategoryDietPlan/GetWeekDays";


                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {

                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            dt.Columns.Add("ShowStatus");
                            dt.Columns.Add("Target");
                            dt.Columns.Add("Consumed");
                            BindTargetCalUser();
                            string target = ViewState["TotalTarget"].ToString();
                            string consumed = ViewState["TotalConsumed"].ToString();


                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                dt.Rows[i]["ShowStatus"] = "N";
                                dt.Rows[i]["Target"] = target;
                                dt.Rows[i]["Consumed"] = "0";
                            }
                            dt.Rows[0]["ShowStatus"] = "Y";
                            dt.Rows[0]["Consumed"] = consumed;

                            dtlDaysUser.DataSource = dt;
                            dtlDaysUser.DataBind();
                            HtmlGenericControl Progress1 = dtlDaysUser.Items[0].FindControl("PrgDay") as HtmlGenericControl;
                            int pro = Convert.ToInt16(ViewState["Progress"].ToString());
                            if (pro <= 25)
                            {

                                Progress1.Attributes.Add("Class", "progressBar25");
                            }
                            if (pro > 25 && pro <= 50)
                            {

                                Progress1.Attributes.Add("Class", "progressBar50");
                            }
                            if (pro > 50 && pro <= 75)
                            {

                                Progress1.Attributes.Add("Class", "progressBar75");

                            }
                            if (pro > 75 && pro <= 100)
                            {

                                Progress1.Attributes.Add("Class", "progressBar100");
                            }
                            Progress1.Attributes.Add("Value", ViewState["Progress"].ToString());
                            divDiet.Visible = true;
                        }
                        else
                        {

                        }

                    }
                    else
                    {
                        divDiet.Visible = false;
                        dtlDaysUser.DataSource = null;
                        dtlDaysUser.DataBind();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }

                else
                {
                    divDiet.Visible = false;
                    dtlDaysUser.DataSource = null;
                    dtlDaysUser.DataBind();
                    divDiet.Visible = false;
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
    #region Bind Target
    public void BindTargetCalUser()
    {

        decimal TotalTarget = 0;
        decimal TotalConsumed = 0;  
        decimal Progress = 0;
        DateTime TodayDate = DateTime.Now;
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());


            string sUrl = Session["BaseUrl"].ToString().Trim() + "userFoodMenu/listPublicUserDietFoodCategory?userId=" + Session["userId"].ToString().Trim() + ""
                + "&bookingId=" + ViewState["bookingId"].ToString() + "&fromDate=" + TodayDate.ToString("yyyy-MM-dd") + "" +
                "&categoryId=" + Session["categoryId"] + "";

            HttpResponseMessage response = client.GetAsync(sUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                var FinessList = response.Content.ReadAsStringAsync().Result;
                int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                string ResponseMsg = JObject.Parse(FinessList)["UserFoodMenu"].ToString();

                if (StatusCode == 1)
                {
                    divDiet.Visible = true;

                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                    if (dt.Rows.Count > 0)
                    {
                        List<Diet> foodItem = JsonConvert.DeserializeObject<List<Diet>>(ResponseMsg);
                        Session["foodItem"] = foodItem;


                        var dtMealtype = (from row in foodItem.AsEnumerable()
                                          group row by new
                                          {
                                              userId = row.userId,
                                              bookingId = row.bookingId,
                                              mealType = row.mealType,
                                              mealTypeName = row.mealTypeName,
                                              fromTime = row.fromTime,
                                              toTime = row.toTime,

                                          } into t1
                                          select new
                                          {
                                              userId = t1.Key.userId,
                                              bookingId = t1.Key.bookingId,
                                              mealType = t1.Key.mealType,
                                              mealTypeName = t1.Key.mealTypeName,
                                              fromTime = t1.Key.fromTime,
                                              toTime = t1.Key.toTime,
                                              calories = t1.Sum(x => Convert.ToInt16(x.calories)),

                                          })
                             .Select(g =>
                             {
                                 var h = dt.NewRow();
                                 h["userId"] = g.userId;
                                 h["bookingId"] = g.bookingId;
                                 h["mealType"] = g.mealType;

                                 h["mealTypeName"] = g.mealTypeName;
                                 h["fromTime"] = g.fromTime;
                                 h["toTime"] = g.toTime;

                                 h["calories"] = g.calories;

                                 return h;
                             }).CopyToDataTable();
                        TotalTarget = foodItem.AsEnumerable().Sum(row => Convert.ToInt16(row.calories));
                        TotalConsumed = foodItem.AsEnumerable().Where(x => x.consumingStatus == "Y")
                        .Sum(row => Convert.ToInt16(row.calories));
                        Progress = (TotalConsumed / TotalTarget) * 100;
                        ViewState["TotalTarget"] = TotalTarget;
                        ViewState["TotalConsumed"] = TotalConsumed;
                        ViewState["Progress"] = Convert.ToInt16(Progress);
                        divUser.Visible = true;
                        divAll.Visible = false;
                        Plan.Visible = false;
                        Summary.Visible = false;
                        divMainPlan.Attributes["class"] = "col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12";
                    }

                }

            }
            else
            {
                divUser.Visible = false;
                divAll.Visible = true;
                BindDays();
            }


        }



    }

    #endregion
    #region Bind MealType
    protected void dtlDaysUser_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Session["FoodTracking"] = "";
            var dtlDietPlanUser = e.Item.FindControl("dtlDietPlanUser") as DataList;

            try
            {
                DateTime TodayDate = DateTime.Now;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());


                    string sUrl = Session["BaseUrl"].ToString().Trim() + "userFoodMenu/listPublicUserDietFoodCategory?userId=" + Session["userId"].ToString().Trim() + ""
                        + "&bookingId=" + ViewState["bookingId"].ToString() + "&fromDate=" + TodayDate.ToString("yyyy-MM-dd") + "" +
                        "&categoryId=" + Session["categoryId"] + "";

                    HttpResponseMessage response = client.GetAsync(sUrl).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var FinessList = response.Content.ReadAsStringAsync().Result;
                        int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                        string ResponseMsg = JObject.Parse(FinessList)["UserFoodMenu"].ToString();

                        if (StatusCode == 1)
                        {
                            divDiet.Visible = true;

                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                            if (dt.Rows.Count > 0)
                            {
                                List<Diet> foodItem = JsonConvert.DeserializeObject<List<Diet>>(ResponseMsg);
                                Session["foodItem"] = foodItem;

                                var dtMealtype = (from row in foodItem.AsEnumerable()
                                                  group row by new
                                                  {
                                                      userId = row.userId,
                                                      bookingId = row.bookingId,
                                                      mealType = row.mealType,
                                                      mealTypeName = row.mealTypeName,
                                                      fromTime = row.fromTime,
                                                      toTime = row.toTime,

                                                  } into t1
                                                  select new
                                                  {
                                                      userId = t1.Key.userId,
                                                      bookingId = t1.Key.bookingId,
                                                      mealType = t1.Key.mealType,
                                                      mealTypeName = t1.Key.mealTypeName,
                                                      fromTime = t1.Key.fromTime,
                                                      toTime = t1.Key.toTime,
                                                      calories = t1.Sum(x => Convert.ToInt16(x.calories)),
                                                      fat = t1.Where(x => x.consumingStatus == "Y").
                                                      Sum(x => Convert.ToInt16(x.calories)),
                                                      carbs = ((t1.Where(x => x.consumingStatus == "Y").
                                                      Sum(x => Convert.ToDecimal(x.calories))) /
                                                      (t1.Sum(x => Convert.ToDecimal(x.calories)))) * 100
                                                  })
                                 .Select(g =>
                                 {
                                     var h = dt.NewRow();
                                     h["userId"] = g.userId;
                                     h["bookingId"] = g.bookingId;
                                     h["mealType"] = g.mealType;

                                     h["mealTypeName"] = g.mealTypeName;
                                     h["fromTime"] = g.fromTime;
                                     h["toTime"] = g.toTime;

                                     h["calories"] = g.calories;
                                     h["fat"] = g.fat;
                                     h["carbs"] = g.carbs;

                                     return h;
                                 }).CopyToDataTable();


                                dtlDietPlanUser.DataSource = dtMealtype;
                                dtlDietPlanUser.DataBind();


                                if (e.Item.ItemIndex == 0)
                                {

                                    for (int i = 0; i < dtlDietPlanUser.Items.Count; i++)
                                    {

                                        HtmlGenericControl Progress1 = dtlDietPlanUser.Items[i].FindControl("Progress1") as HtmlGenericControl;
                                        int pro = Convert.ToInt16(dtMealtype.Rows[i]["carbs"].ToString());
                                        if (pro <= 25)
                                        {

                                            Progress1.Attributes.Add("Class", "progressBar25");
                                        }
                                        if (pro > 25 && pro <= 50)
                                        {

                                            Progress1.Attributes.Add("Class", "progressBar50");
                                        }
                                        if (pro > 50 && pro <= 75)
                                        {

                                            Progress1.Attributes.Add("Class", "progressBar75");

                                        }
                                        if (pro > 75 && pro <= 100)
                                        {

                                            Progress1.Attributes.Add("Class", "progressBar100");
                                        }
                                        Progress1.Attributes.Add("Value", dtMealtype.Rows[i]["carbs"].ToString());
                                        var dataList = dtlDietPlanUser.Items[i].FindControl("dtlFoodItemUser") as DataList;
                                        for (int j = 0; j < dataList.Items.Count; j++)
                                        {
                                            HtmlControl divtargetItems = dataList.Items[j].FindControl("divtargetItems") as HtmlControl;
                                            HtmlControl FooditemDetails = dataList.Items[j].FindControl("FooditemDetails") as HtmlControl;
											Image ImgDone = dataList.Items[j].FindControl("ImgDone") as Image;
											Image ImgLock = dataList.Items[j].FindControl("ImgLock") as Image;
											Image ImgtargetItems = dataList.Items[j].FindControl("ImgtargetItems") as Image;
											Label lblConsumingStatus = dataList.Items[j].FindControl("lblConsumingStatus") as Label;
											if (lblConsumingStatus.Text == "Y")
											{

												ImgDone.Visible = true;

											}
											else
											{

												ImgDone.Visible = false;
											}
											ImgtargetItems.CssClass = "imgFoodLock";
											FooditemDetails.Attributes["class"] = "FooditemDetailslock";
											ImgLock.Visible = false;
                                            divtargetItems.Attributes["class"] = "col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 Fooditem";

                                        }
                                        dataList.Enabled = true;
                                    }
                                }




                            }
                        }
                        else
                        {
                            dtlDietPlanUser.DataSource = null;
                            dtlDietPlanUser.DataBind();


                        }
                    }


                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
            }

        }
    }
    #endregion
    #region Bind MealType
    protected void dtlDietPlanUser_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {

            List<Diet> foodItems = new List<Diet>();
            foodItems = (List<Diet>)Session["foodItem"];
            var foodItem = foodItems.ElementAt(0);
            var dataList = e.Item.FindControl("dtlFoodItemUser") as DataList;
            Label MealTYpeName = e.Item.FindControl("lblMealTYpeName") as Label;
            if (MealTYpeName.Text == "Breakfast")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Breakfast").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
            if (MealTYpeName.Text == "Snacks1")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Snacks1").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
            if (MealTYpeName.Text == "Lunch")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Lunch").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
            if (MealTYpeName.Text == "Snacks2")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Snacks2").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();

            }
            if (MealTYpeName.Text == "Dinner")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Dinner").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
            if (MealTYpeName.Text == "Snacks3")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Snacks3").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }

        }
    }
    #endregion
    #endregion
    #region DietTarget Click
    protected void LnkDiet_Click(object sender, EventArgs e)
    {
        DateTime TodayDate = DateTime.Now;

        LinkButton lnkbtn = sender as LinkButton;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;

        Label lblbookingId = (Label)gvrow.FindControl("lblbookingId");
        Label lblmealType = (Label)gvrow.FindControl("lblmealType");
        Label lbluserId = (Label)gvrow.FindControl("lbluserId");
        Label lblfoodItemId = (Label)gvrow.FindControl("lblfoodItemId");
        Label lblfoodDietTimeId = (Label)gvrow.FindControl("lblfoodDietTimeId");
        HtmlControl divtargetItems = gvrow.FindControl("divtargetItems") as HtmlControl;
        Session["userId"] = lbluserId.Text;


        List<UserFoodTracking> FoodTracking = new List<UserFoodTracking>();
        if (Session["FoodTracking"].ToString() != "")
        {
            FoodTracking = (List<UserFoodTracking>)Session["FoodTracking"];
        }

        if (divtargetItems.Attributes["class"] == "col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 divtargetItemsSelection")
        {
            var itemToRemove = FoodTracking.SingleOrDefault(r => r.foodMenuId == lblfoodItemId.Text
            && r.mealtypeId == lblmealType.Text);
            if (itemToRemove != null)
            {
                FoodTracking.Remove(itemToRemove);
            }
            Session["FoodTracking"] = FoodTracking;
            divtargetItems.Attributes["class"] = "col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 divtargetItems";
        }
        else
        {
            FoodTracking.AddRange(new List<UserFoodTracking>
            {
                new UserFoodTracking {userId=lbluserId.Text,bookingId=lblbookingId.Text,foodMenuId=lblfoodItemId.Text,
                mealtypeId=lblmealType.Text,consumingStatus="Y",date=TodayDate.ToString("yyyy-MM-dd")
                ,createdBy=  Session["userId"].ToString()
                }

            });
            Session["FoodTracking"] = FoodTracking;
            divtargetItems.Attributes["class"] = "col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 divtargetItemsSelection";
        }
        InsertDietTracking();

    }
    #endregion

    #region InsertDietTracking
    public void InsertDietTracking()
    {
        try
        {
            DateTime TodayDate = DateTime.Now;
            string s = TodayDate.DayOfWeek.ToString();
            string day = s.Substring(0, 2);

            List<UserFoodTracking> FoodTracking = new List<UserFoodTracking>();
            if (Session["FoodTracking"].ToString() != "")
            {
                FoodTracking = (List<UserFoodTracking>)Session["FoodTracking"];
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new InsertTranUserFoodTracking()
                {
                    lstTranUserFoodTracking = FoodTracking.ToList(),
                };
                HttpResponseMessage response = client.PostAsJsonAsync("tranUserFoodTracking/insert", Insert).Result;
                var Fitness = response.Content.ReadAsStringAsync().Result;
                int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();
                if (response.IsSuccessStatusCode)
                {

                    if (StatusCode == 1)
                    {
                        Session["FoodTracking"] = "";
                        if (ViewState["bookingId"] != null)
                        {
                            BindDaysUser();
                        }
                        else
                        {
                            divUser.Visible = false;
                            divAll.Visible = true;
                            BindDays();
                        }
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        Session["FoodTracking"] = "";
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
    #region DietTargetClass
    public class Common
    {
        public string userId { get; set; }
        public string bookingId { get; set; }
        public string foodMenuId { get; set; }
        public string mealtypeId { get; set; }
        public string consumingStatus { get; set; }
        public string date { get; set; }
        public string createdBy { get; set; }
    }

    public class Diet
    {
        public string userId { get; set; }
        public string bookingId { get; set; }
        public string mealType { get; set; }
        public string mealTypeName { get; set; }
        public string imageUrl { get; set; }
        public string CheckTime { get; set; }
        public string fromTime { get; set; }
        public string toTime { get; set; }
        public string dietTypeId { get; set; }
        public string foodItemId { get; set; }
        public string typeIndicationImageUrl { get; set; }
        public string foodItemName { get; set; }
        public string protein { get; set; }
        public string carbs { get; set; }
        public string fat { get; set; }
        public string servingIn { get; set; }
        public string calories { get; set; }
        public string foodDietTimeId { get; set; }
        public string consumingStatus { get; set; }
        public string Tarcalories { get; set; }
        public string Consumedcal { get; set; }
        public string progress { get; set; }

    }

    public class ConsumedDiet
    {

        public string mealTypeName { get; set; }
        public string imageUrl { get; set; }

        public string fromTIme { get; set; }
        public string toTime { get; set; }
        public string foodMenuId { get; set; }
        public string consumingStatus { get; set; }
        public string foodItemName { get; set; }
        public string protein { get; set; }
        public string carbs { get; set; }
        public string fat { get; set; }
        public string servingIn { get; set; }
        public string calories { get; set; }
        public string uniqueId { get; set; }

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

    public class AllUserDiet
    {

        public string mealTypeId { get; set; }
        public string mealTypeName { get; set; }
        public string imageUrl { get; set; }
        public string fromTime { get; set; }
        public string toTime { get; set; }
        public string dietTypeId { get; set; }
        public string foodItemId { get; set; }
        public string foodItemName { get; set; }
        public string protein { get; set; }
        public string carbs { get; set; }
        public string fat { get; set; }
        public string servingIn { get; set; }
        public string calories { get; set; }
        public string dietTimeId { get; set; }

    }

    public class breakFastDetails
    {
        public string imageUrl { get; set; }
        public string mealTypeId { get; set; }
        public string mealtypeName { get; set; }
        public string foodItemId { get; set; }
        public string foodItemName { get; set; }
        public string protein { get; set; }
        public string carbs { get; set; }
        public string fat { get; set; }
        public string servingIn { get; set; }
        public string servingInId { get; set; }
        public string calories { get; set; }
        public string fromTime { get; set; }
        public string toTime { get; set; }
        public string dietTimeId { get; set; }

    }
    #endregion

    #endregion


    #region WorkOut
    #region BindWorkOut All
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
                string sUrl1 = Session["BaseUrl"].ToString().Trim() + "CategoryWorkOutPlan/GetPublicCategoryWorkOutPlan?gymOwnerId=" + Session["gymOwnerId"].ToString().Trim() + ""
                      + "&branchId=" + Session["branchId"].ToString() + "&categoryId=" + Session["categoryId"].ToString() + "";

                HttpResponseMessage response = client.GetAsync(sUrl1).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["CategoryWorkOutPlan"].ToString();

                    if (StatusCode == 1)
                    {

                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);

                        if (dt.Rows.Count > 0)
                        {
                            List<WorkOut> WorkOuts = JsonConvert.DeserializeObject<List<WorkOut>>(ResponseMsg);
                            divUserWorkOut.Visible = false;
                            divAllWork.Visible = true;

                            foreach (var tom in WorkOuts)
                            {
                                int Count = tom.WorkOutList.Count;
                                tom.VideoCount = Count.ToString();
                            }
                            dtlWorkOut.DataSource = WorkOuts;
                            dtlWorkOut.DataBind();
                            for (int i = 0; i < dtlWorkOut.Items.Count; i++)
                            {
                                Label lblVideoCounts = dtlWorkOut.Items[i].FindControl("lblVideoCounts") as Label;
                                decimal VideoCount = Convert.ToDecimal(lblVideoCounts.Text);
                                decimal CompCount = 1;
                                decimal per = (CompCount / VideoCount) * 100;
                                HtmlGenericControl Progress5 = dtlWorkOut.Items[i].FindControl("Progress5") as HtmlGenericControl;
                                Label lblVideoCount = dtlWorkOut.Items[i].FindControl("lblVideoCount") as Label;
                                lblVideoCount.Text = Convert.ToInt16(CompCount) + "/" + Convert.ToInt16(VideoCount) + "Videos".ToString();
                                int pro = Convert.ToInt16(per);
                                if (pro <= 25)
                                {

                                    Progress5.Attributes.Add("Class", "progressBar25");
                                }
                                if (pro > 25 && pro <= 50)
                                {

                                    Progress5.Attributes.Add("Class", "progressBar50");
                                }
                                if (pro > 50 && pro <= 75)
                                {

                                    Progress5.Attributes.Add("Class", "progressBar75");

                                }
                                if (pro > 75 && pro <= 100)
                                {

                                    Progress5.Attributes.Add("Class", "progressBar100");
                                }
                                Progress5.Attributes.Add("Value", Convert.ToInt16(per).ToString());
                            }
                        }
                    }
                    else
                    {
                        dtlWorkOut.DataSource = null;
                        dtlWorkOut.DataBind();

                    }
                }
                else
                {
                    dtlWorkOut.DataSource = null;
                    dtlWorkOut.DataBind();
                }


            }

        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }


    }
    protected void ToggleView_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = sender as ImageButton;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;
        var dtlWorkOutList = gvrow.FindControl("dtlWorkOutList") as DataList;
        if (dtlWorkOutList.Visible == false)
        {
            dtlWorkOutList.Visible = true;

        }
        else
        {
            dtlWorkOutList.Visible = false;
        }


    }
    protected void dtlWorkOut_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            var WorkOut = e.Item.DataItem as WorkOut;
            var dtlWorkOutList = e.Item.FindControl("dtlWorkOutList") as DataList;

            dtlWorkOutList.DataSource = WorkOut.WorkOutList;
            dtlWorkOutList.DataBind();
            if (e.Item.ItemIndex == 0)
            {
                dtlWorkOutList.Visible = true;
            }
            else
            {
                dtlWorkOutList.Visible = false;
            }
        }
    }
    #endregion

    #region BindWorkOut User
    public void BindWorkOutListUser()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "UserWorkOutPlan/GetCategoryTypeBasedonDateDayCategory?"
                    + "userId=" + Session["userId"].ToString() + "&date=" + TodayDate.ToString("yyyy-MM-dd") + "" +
                    "&day=" + day.Trim() + "&categoryId=" + Session["categoryId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        dtlWorkOut.Visible = true;
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        List<CategoryTypeBasedonDateDay_Out> dtworkOut = JsonConvert.DeserializeObject<List<CategoryTypeBasedonDateDay_Out>>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            Session["WorkOutDt"] = dt;

                            divUserWorkOut.Visible = true;
                            divAllWork.Visible = false;
                            dtlWorkOutUser.DataSource = dt;
                            dtlWorkOutUser.DataBind();

                            DataTable WorkCat = (DataTable)Session["WorkOutDt"];
                            for (int i = 0; i < dtlWorkOutUser.Items.Count; i++)
                            {
                                decimal VideoCount = Convert.ToDecimal(WorkCat.Rows[i]["VideoCount"].ToString());
                                decimal CompCount = Convert.ToDecimal(WorkCat.Rows[i]["CompletedCount"].ToString());
                                decimal per = (CompCount / VideoCount) * 100;
                                HtmlGenericControl Progress5 = dtlWorkOutUser.Items[i].FindControl("Progress5") as HtmlGenericControl;
                                Label lblVideoCount = dtlWorkOutUser.Items[i].FindControl("lblVideoCount") as Label;
                                lblVideoCount.Text = Convert.ToInt16(CompCount) + "/" + Convert.ToInt16(VideoCount) + "Videos".ToString();
                                int pro = Convert.ToInt16(per);
                                if (pro <= 25)
                                {

                                    Progress5.Attributes.Add("Class", "progressBar25");
                                }
                                if (pro > 25 && pro <= 50)
                                {

                                    Progress5.Attributes.Add("Class", "progressBar50");
                                }
                                if (pro > 50 && pro <= 75)
                                {

                                    Progress5.Attributes.Add("Class", "progressBar75");

                                }
                                if (pro > 75 && pro <= 100)
                                {

                                    Progress5.Attributes.Add("Class", "progressBar100");
                                }
                                Progress5.Attributes.Add("Value", Convert.ToInt16(per).ToString());
                            }

                        }
                        else
                        {

                            dtlWorkOutUser.DataSource = null;
                            dtlWorkOutUser.DataBind();
                        }
                    }
                    else
                    {
                        BindWorkout();
                        divUserWorkOut.Visible = false;
                        divAllWork.Visible = true;

                    }
                }

                else
                {
                    BindWorkout();
                    divUserWorkOut.Visible = false;
                    divAllWork.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }

    protected void ToggleViewUser_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = sender as ImageButton;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;
        var dtlWorkOutListUser = gvrow.FindControl("dtlWorkOutListUser") as DataList;
        ImageButton ToggleViewUser = gvrow.FindControl("ToggleViewUser") as ImageButton;
        if (dtlWorkOutListUser.Visible == false)
        {
            dtlWorkOutListUser.Visible = true;
            ToggleViewUser.CssClass = "imgShowHide";
        }
        else
        {
            dtlWorkOutListUser.Visible = false;
            ToggleViewUser.CssClass= "imgShowHide toggleViewClose";

        }
    }
    protected void dtlWorkOutUser_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            var dtlWorkOutList = e.Item.FindControl("dtlWorkOutListUser") as DataList;
            Label lblworkoutCatTypeId = e.Item.FindControl("lblworkoutCatTypeId") as Label;
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
                         + Session["userId"].ToString().Trim() + "&date=" + TodayDate.ToString("yyyy-MM-dd") + "&day=" + day.Trim() + "&workoutCatTypeId=" + lblworkoutCatTypeId.Text + "";

                    HttpResponseMessage response = client.GetAsync(sUrl).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var FinessList = response.Content.ReadAsStringAsync().Result;
                        int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                        string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                        if (StatusCode == 1)
                        {
                            dtlWorkOutList.Visible = true;
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                            List<GetTran_WorkoutTypeBasedonDateDay_Out> dtList = JsonConvert.DeserializeObject<List<GetTran_WorkoutTypeBasedonDateDay_Out>>(ResponseMsg);

                            if (dt.Rows.Count > 0)
                            {
                                DataTable WorkCat = (DataTable)Session["WorkOutDt"];
                                if (e.Item.ItemIndex == 0)
                                {
                                    WorkCat.Columns.Add("CompletedCount");

                                }

                                var Counts = dtList.AsEnumerable().Where(row => row.OverAllCompletedStatus == "Yes");
                                WorkCat.Rows[e.Item.ItemIndex]["CompletedCount"] = Counts.Count();
                                Session["WorkOutDt"] = WorkCat;
                                dtlWorkOutList.DataSource = dt;
                                dtlWorkOutList.DataBind();
                                if (e.Item.ItemIndex == 0)
                                {
                                    dtlWorkOutList.Visible = true;
                                }
                                else
                                {
                                    dtlWorkOutList.Visible = false;
                                }

                            }
                            else
                            {
                                dtlWorkOutList.DataBind();
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
    }

    protected void dtlWorkOutListUser_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Label lbluserId = e.Item.FindControl("lbluserId") as Label;
            Label lblworkoutCatTypeId = e.Item.FindControl("lblworkoutCatTypeId") as Label;
            Label lblworkoutTypeId = e.Item.FindControl("lblworkoutTypeId") as Label;

            try
            {
                var dtlSets = e.Item.FindControl("dtlSets") as DataList;
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

					//string sUrl = Session["BaseUrl"].ToString().Trim() + "UserWorkOutPlan/GetSetTypeBasedonDate?userId="
					//     + lbluserId.Text.Trim() + "&date=" + TodayDate + "&day=" + day.Trim() + "" +
					//     "&workoutCatTypeId=" + lblworkoutCatTypeId.Text.Trim() + "&workoutTypeId=" + lblworkoutTypeId.Text.Trim() + "";

					string sUrl = Session["BaseUrl"].ToString().Trim() + "UserWorkOutPlan/GetSetTypeBasedonDate?userId="
											+ lbluserId.Text.Trim() + "&date=" + TodayDate.ToString("yyyy-MM-dd") + "&day=" + day.Trim() + "" +
											"&workoutCatTypeId=" + lblworkoutCatTypeId.Text.Trim() + "&workoutTypeId=" + lblworkoutTypeId.Text.Trim() + "";

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
                                dtlSets.DataSource = null;
                                dtlSets.DataBind();

                            }
                        }
                        else
                        {
                            dtlSets.DataSource = null;
                            dtlSets.DataBind();

                           
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
    }
    #endregion

    #region check Changed event 
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
                        if (Session["mobileNo"] != null)
                        {
                            BindWorkOutListUser();
                        }
                        else
                        {

                            BindWorkout();
                        }
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

    #region WorkOut Class
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

    public class CategoryTypeBasedonDateDay_Out
    {
        public int gymOwnerId { get; set; }
        public string gymOwnerName { get; set; }
        public int branchId { get; set; }
        public string branchName { get; set; }
        public int workoutCatTypeId { get; set; }
        public string workoutCatTypeName { get; set; }
        public int bookingId { get; set; }
        public string day { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string VideoCount { get; set; }
        public int userId { get; set; }
    }
    public class WorkOut
    {
        public string workoutCatTypeId { get; set; }
        public string VideoCount { get; set; }
        public string workoutCatTypeName { get; set; }
        public List<WorkOutList> WorkOutList { get; set; }

    }
    public class WorkOutList
    {
        public string workoutCatTypeId { get; set; }
        public string workoutCatTypeName { get; set; }
        public string workoutTypeId { get; set; }
        public string workoutType { get; set; }
        public string categoryId { get; set; }
        public string categoryName { get; set; }
        public string video { get; set; }
        public string imageUrl { get; set; }
        public string UserUsed { get; set; }

    }
    public class GetTran_WorkoutTypeBasedonDateDay_Out
    {
        public int gymOwnerId { get; set; }
        public string gymOwnerName { get; set; }
        public int branchId { get; set; }
        public string branchName { get; set; }
        public int workoutCatTypeId { get; set; }
        public string workoutCatTypeName { get; set; }
        public int workoutTypeId { get; set; }
        public string workoutTypeName { get; set; }
        public int bookingId { get; set; }
        public string day { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public int userId { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public string video { get; set; }
        public string UserUsed { get; set; }
        public string OverAllCompletedStatus { get; set; }
    }
    #endregion
    protected void ImgLock_Click(object sender, ImageClickEventArgs e)
    {
        DivDietPopup.Visible = true;
    }

    protected void ImageWorkLock_Click(object sender, ImageClickEventArgs e)
    {
        DivWorkOutpOpup.Visible = true;
    }
    #endregion



    //Plan 



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
                string Endpoint = "fitnessCategoryPrice/GetPriceOnPublicWeb?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
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
                        BindCategoryBenifits();
                        dtlCategoryList.DataSource = dt;
                        dtlCategoryList.DataBind();
                        for (int i = 0; i < dtlCategoryList.Items.Count; i++)
                        {
                            var datalist = dtlCategoryList.Items[i].FindControl("dtlCategoryBenifit") as DataList;
                            if(Session["Benefit"].ToString() != "")
                            {
								DataTable dts = (DataTable)Session["Benefit"];
								datalist.DataSource = dts;
								datalist.DataBind();

							}
                            
                            
                        }
                        lblCategoryName.Text = dt.Rows[0]["categoryName"].ToString();
                        lblSummaryPlanHead.Text = dt.Rows[0]["trainingTypeName"].ToString();
                        lblSummaryDuration.Text = dt.Rows[0]["planDurationName"].ToString();
                        lblSummaryDisPlayAmt.Text = "₹" + dt.Rows[0]["displayAmount"].ToString();
                        lblSaveAmt.Text = "Saved" + " " + "₹" + dt.Rows[0]["SavedAmount"].ToString();
                        lblActualAmt.Text = "₹" + dt.Rows[0]["actualAmount"].ToString();
                        lblSummaryAmtFinal.Text = "₹" + dt.Rows[0]["netAmount"].ToString();
                        lblSummaryTotalAmt.Text = "₹" + dt.Rows[0]["netAmount"].ToString();
                        Session["planDuration"] = dt.Rows[0]["planDurationName"].ToString();
                        Session["PcategoryId"] = dt.Rows[0]["categoryId"].ToString();
                        Session["netamount"] = dt.Rows[0]["netamount"].ToString();
                        Session["trainingTypeName"] = dt.Rows[0]["trainingTypeName"].ToString();
                        Session["actualAmount"] = dt.Rows[0]["actualAmount"].ToString();
                        Session["gymOwnerId"] = dt.Rows[0]["gymOwnerId"].ToString();
                        Session["branchId"] = dt.Rows[0]["branchId"].ToString();
                        Session["branchName"] = dt.Rows[0]["branchName"].ToString();
                        Session["trainingTypeId"] = dt.Rows[0]["trainingTypeId"].ToString();
                        Session["planDurationId"] = dt.Rows[0]["planDuration"].ToString();
                        Session["trainingMode"] = dt.Rows[0]["trainingMode"].ToString();
                        Session["priceId"] = dt.Rows[0]["priceId"].ToString();
                        Session["price"] = dt.Rows[0]["price"].ToString();
                        Session["taxId"] = dt.Rows[0]["taxId"].ToString();
                        Session["taxName"] = dt.Rows[0]["taxName"].ToString();
                        Session["taxAmount"] = dt.Rows[0]["tax"].ToString();
                        Session["BuyPlan"] = "P";
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
    #region Bind Category Benifits

    public void BindCategoryBenifits()
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
                string Endpoint = "categoryBenefitMaster/GetCategoryBenefit?categoryId=" + Session["categoryId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        DataTable dt1 = dt.AsEnumerable().Take(5).CopyToDataTable();
                        Session["Benefit"] = dt1;


                    }
                    else
                    {
                        Session["Benefit"] = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
				{
					Session["Benefit"] = "";
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
        Label lblCttrainingTypeName = (Label)gvrow.FindControl("lblCttrainingTypeName");
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
        Label lbldisplayAmount = (Label)gvrow.FindControl("lbldisplayAmount");
        Label lblSavedAmount = (Label)gvrow.FindControl("lblSavedAmount");
        Label lblCtnetAmount = (Label)gvrow.FindControl("lblCnetAmount");



        lblSummaryPlanHead.Text = lblCttrainingTypeName.Text;
        lblSummaryDuration.Text = lblplanDurationName.Text;
        lblSummaryDisPlayAmt.Text = "₹" + lbldisplayAmount.Text;
        lblSaveAmt.Text = "Saved" + " " + "₹" + lblSavedAmount.Text;
        lblActualAmt.Text = lblactualAmount.Text;
        lblSummaryAmtFinal.Text = "₹" + lblCtnetAmount.Text;
        lblSummaryTotalAmt.Text = "₹" + lblCtnetAmount.Text;

        string PlanName = lblplanDurationName.Text;
        string[] pname;

        pname = PlanName.Split(' ');

        Session["planDuration"] = pname[0];
        Session["PcategoryId"] = lblcategoryId.Text;
        Session["netamount"] = lblTotalAmt.Text;
        Session["trainingTypeName"] = lblCttrainingTypeName.Text;
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

        //Response.Redirect("../PlanDetails/PlanDetails.aspx");
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

    #region Btn Buy Now
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

            string taxname;
            if (Session["taxName"].ToString().Trim() == null || Session["taxName"].ToString().Trim() == "")
            {
                taxname = "CGST-9.00, SGST-9.00";
            }
            else
            {
                taxname = Session["taxName"].ToString();
            }

            var Insertjavasctipt = new BookingClass();


            Insertjavasctipt.queryType = "insert";
            Insertjavasctipt.gymOwnerId = Session["gymOwnerId"].ToString();
            Insertjavasctipt.branchId = Session["branchId"].ToString();
            Insertjavasctipt.branchName = Session["branchName"].ToString();
            Insertjavasctipt.categoryId = Session["PcategoryId"].ToString();
            Insertjavasctipt.trainingTypeId = Session["trainingTypeId"].ToString();
            Insertjavasctipt.planDurationId = Session["planDurationId"].ToString();
            Insertjavasctipt.traningMode = Session["trainingMode"].ToString();
            Insertjavasctipt.phoneNumber = Session["mobileNo"].ToString();
            Insertjavasctipt.userId = Session["userId"].ToString();
            Insertjavasctipt.booking = "W";
            Insertjavasctipt.loginType = "U";
            Insertjavasctipt.priceId = Session["priceId"].ToString();
            Insertjavasctipt.price = Session["price"].ToString();
            Insertjavasctipt.taxId = Session["taxId"].ToString();
            Insertjavasctipt.taxName = taxname;
            Insertjavasctipt.taxAmount = Session["taxAmount"].ToString();
            Insertjavasctipt.totalAmount = Session["netamount"].ToString();
            Insertjavasctipt.paymentCyclesStatus = "F";
            Insertjavasctipt.paymentCycles = "0";
            Insertjavasctipt.paidAmount = "0";
            Insertjavasctipt.slotFromTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Insertjavasctipt.slotToTime = "";
            Insertjavasctipt.slotId = "0";
            Insertjavasctipt.paymentStatus = "N";
            Insertjavasctipt.paymentType = "69";
            Insertjavasctipt.transactionId = "";
            Insertjavasctipt.bankName = "";
            Insertjavasctipt.bankReferenceNumber = "";
            Insertjavasctipt.offerId = "0";
            Insertjavasctipt.offerAmount = "0";
            Insertjavasctipt.createdBy = Session["userId"].ToString();

            string output = JsonConvert.SerializeObject(Insertjavasctipt);
            hfSubscriptionInsert.Value = output;
            hfBaseurl.Value = Session["BaseUrl"].ToString() + "booking";
            hfToken.Value = Session["APIToken"].ToString();
            hfPaymentBaseurl.Value = Session["BaseUrl"].ToString() + "BookingDetails";
            hfNotificationSMSBaseurl.Value = Session["BaseUrl"].ToString() + "sendBookingNotification";
            hfNotificationSMSDatas.Value = Session["mobileNo"].ToString();
            hfNotificationSMSUserId.Value = Session["userId"].ToString();
            
        }
        else
        {
            DivLogin.Visible = true;
        }

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
                            Session["mobileNo"] = dt.Rows[0]["mobileNo"].ToString();
                            Session["userId"] = dt.Rows[0]["userId"].ToString();
                            Session["userRole"] = dt.Rows[0]["roleName"].ToString();
                            Session["mailId"] = dt.Rows[0]["mailId"].ToString();
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


    public class BookingClass
    {
        public string queryType { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string branchName { get; set; }
        public string categoryId { get; set; }
        public string booking { get; set; }
        public string trainingTypeId { get; set; }
        public string slotId { get; set; }
        public string planDurationId { get; set; }
        public string traningMode { get; set; }
        public string phoneNumber { get; set; }
        public string userId { get; set; }
        public string loginType { get; set; }
        public string priceId { get; set; }
        public string price { get; set; }
        public string taxId { get; set; }
        public string taxName { get; set; }
        public string taxAmount { get; set; }
        public string totalAmount { get; set; }
        public string paidAmount { get; set; }
        public string paymentStatus { get; set; }
        public string slotFromTime { get; set; }
        public string slotToTime { get; set; }
        public string paymentCycles { get; set; }
        public string paymentType { get; set; }
        public string paymentCyclesStatus { get; set; }
        public string transactionId { get; set; }
        public string bankName { get; set; }
        public string bankReferenceNumber { get; set; }
        public string offerId { get; set; }
        public string offerAmount { get; set; }
        public string createdBy { get; set; }

    }

    public class NotificationClass
    {
        public string queryType { get; set; }
        public string mobileNo { get; set; }
    }

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


    protected void btnHiddenCloseVideoPopup_Click(object sender, EventArgs e)
    {

        DivWorkOutpOpup.Visible = false;
        DivDietPopup.Visible = false;
    }
}