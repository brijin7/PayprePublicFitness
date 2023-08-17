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
using System.Configuration;

public partial class Pages_DietWorkOut_DietWorkOut : System.Web.UI.Page
{
    IFormatProvider objEnglishDate = new System.Globalization.CultureInfo("en-GB", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
            divDiet.Visible = true;
            divWorkOut.Visible = false;
            btnDiet.CssClass = "divcontaineractive";
            btnWorkOut.CssClass = "divcontainer";
            BindDiet();
        }
    }


    #region btnDiet Click Event
    protected void btnDiet_Click(object sender, EventArgs e)
    {
        divDiet.Visible = true;
        divWorkOut.Visible = false;
        btnDiet.CssClass = "divcontaineractive";
        btnWorkOut.CssClass = "divcontainer";
        BindDiet();
    }

    #endregion
    #region btnWorkOut Click Event
    protected void btnWorkOut_Click(object sender, EventArgs e)
    {
        divDiet.Visible = false;
        divWorkOut.Visible = true;
        btnWorkOut.CssClass = "divcontaineractive";
        btnDiet.CssClass = "divcontainer";
        BindWorkOutList();

    }
    #endregion

    #region Diet
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
                            divDiet.Visible = true ;
                          
                            DateTime Fromdate = DateTime.Parse(dt.Rows[0]["fromDate"].ToString(),objEnglishDate);
                            DateTime Todate = DateTime.Parse(dt.Rows[0]["toDate"].ToString(),objEnglishDate);

                            var TDiff = Todate.Subtract(Fromdate);
                            String DaysDiff = TDiff.TotalDays.ToString();
                            int Week = NumberOfWeeks(Fromdate, Todate);
                            int Sessions = Convert.ToInt32(DaysDiff);
                            string categoryName = dt.Rows[0]["categoryName"].ToString();
                            DietWorkname.Text = categoryName.Trim();
                            Weeks.InnerText = Week + " Weeks " + " | " + Sessions + " Sessions ";
                            ViewState["bookingId"] = dt.Rows[0]["bookingId"].ToString();
                            BindTargetDiet();
                            BindConsumedDiet();
                        }
                        else
                        {
                            divDiet.Visible = false;
                        }
                    }
                    else
                    {
                        divDiet.Visible = false;
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
        }

        return WeekCount;
    }
    #endregion
    #region BindTargetDiet
    public void BindTargetDiet()
    {
        try
        {
            Session["FoodTracking"] = "";
            DateTime TodayDate = DateTime.Now;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());


                string sUrl = Session["BaseUrl"].ToString().Trim() + "userFoodMenu/listPublicUserDietFood?userId=" + Session["userId"].ToString().Trim() + ""
                    + "&bookingId=" + ViewState["bookingId"].ToString() + "&fromDate=" + TodayDate.ToString("yyyy-MM-dd") + "";

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

                            dtlDietTarget.DataSource = dtMealtype;
                            dtlDietTarget.DataBind();
                        }
                        else
                        {
                            dtlDietTarget.DataBind();
                        }
                    }
                    else
                    {
                        divDiet.Visible = false;
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
    #region Diet Item Data Bound
    protected void dtlDietTarget_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            List<Diet> foodItems = new List<Diet>();
            foodItems = (List<Diet>)Session["foodItem"];
            var dataList = e.Item.FindControl("dtlDietTargetItems") as DataList;
            Label lblmealTypeName = e.Item.FindControl("lblmealTypeName") as Label;
            if (lblmealTypeName.Text == "Breakfast")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Breakfast").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
            if (lblmealTypeName.Text == "Snacks1")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Snacks1").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
            if (lblmealTypeName.Text == "Lunch")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Lunch").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
            if (lblmealTypeName.Text == "Snacks2")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Snacks2").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();

            }
            if (lblmealTypeName.Text == "Dinner")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Dinner").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
            if (lblmealTypeName.Text == "Snacks3")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Snacks3").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
        }

    }

    protected void OnClick(object sender, EventArgs e)
    {
        int repeatcolumn = Convert.ToInt32(hfColumnRepeat.Value);
        this.RsetepeatColumns(repeatcolumn);
    }

    private void RsetepeatColumns(int repeatcolumn = 5)
    {
        for (int i = 0; i < dtlDietTarget.Items.Count; i++)
        {
            DataList dataList = dtlDietTarget.Items[i].FindControl("dtlDietTargetItems") as DataList;
            dataList.RepeatColumns = repeatcolumn;
        }
    }
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
                        BindDiet();
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
    #region Btn Finished Click
    protected void btnFinished_Click(object sender, EventArgs e)
    {
        InsertDietTracking();
    }
    #endregion
    #region BindConsumedDiet
    public void BindConsumedDiet()
    {
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "tranUserFoodTracking/userConsumingDietList?userId=" + Session["userId"].ToString() + "&"
                    + "date=" + TodayDate.ToString("yyyy-MM-dd") + "";

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
                            List<ConsumedDiet> foodItem = JsonConvert.DeserializeObject<List<ConsumedDiet>>(ResponseMsg);
                            Session["ConsumedDiet"] = foodItem;

                            var dtMealtype = (from row in foodItem.AsEnumerable()
                                              group row by new
                                              {
                                                  mealTypeName = row.mealTypeName,
                                                  fromTIme = row.fromTIme,
                                                  toTime = row.toTime,

                                              } into t1
                                              select new
                                              {
                                                  mealTypeName = t1.Key.mealTypeName,
                                                  fromTIme = t1.Key.fromTIme,
                                                  toTime = t1.Key.toTime,
                                                  calories = t1.Sum(x => Convert.ToInt16(x.calories)),

                                              })
                                 .Select(g =>
                                 {
                                     var h = dt.NewRow();

                                     h["mealTypeName"] = g.mealTypeName;
                                     h["fromTIme"] = g.fromTIme;
                                     h["toTime"] = g.toTime;
                                     h["calories"] = g.calories;

                                     return h;
                                 }).CopyToDataTable();

                            dtlConsumed.DataSource = dtMealtype;
                            dtlConsumed.DataBind();
                        }
                        else
                        {
                           
                            dtlConsumed.DataBind();
                        }
                    }
                    else
                    {
                       
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }

                else
                {
                   
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Datalist Consumed ItemDataBound
    protected void dtlConsumed_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            List<ConsumedDiet> foodItems = new List<ConsumedDiet>();
            foodItems = (List<ConsumedDiet>)Session["ConsumedDiet"];
            var dataList = e.Item.FindControl("dtlConsumedItems") as DataList;
            Label lblmealTypeName = e.Item.FindControl("lblmealTypeName") as Label;
            if (lblmealTypeName.Text == "Breakfast")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Breakfast").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
            if (lblmealTypeName.Text == "Snacks1")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Snacks1").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
            if (lblmealTypeName.Text == "Lunch")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Lunch").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
            if (lblmealTypeName.Text == "Snacks2")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Snacks2").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();

            }
            if (lblmealTypeName.Text == "Dinner")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Dinner").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
            if (lblmealTypeName.Text == "Snacks3")
            {
                var dtdiet = foodItems.Where(x => x.mealTypeName == "Snacks3").Select(x => x);
                dataList.DataSource = dtdiet;
                dataList.DataBind();
            }
        }

    }
    #endregion
    #endregion 
    #region Work Out
    #region BindWorkOut
    public void BindWorkOutList()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "UserWorkOutPlan/GetCategoryTypeBasedonDateDay?"
                    + "userId=" + Session["userId"].ToString() + "&date=" + TodayDate.ToString("yyyy-MM-dd") + "&day=" + day.Trim() + "";
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
                        if (dt.Rows.Count > 0)
                        {
                            dt.Columns.Add("imageUrl");
                            for(int i=0;i<dt.Rows.Count;i++)
                            {
                                if (dt.Rows[i]["workoutCatTypeName"].ToString() == "ABS" ||
                                    dt.Rows[i]["workoutCatTypeName"].ToString() == "Abs")
                                {
                                    dt.Rows[i]["imageUrl"] = "../../Images/DietWotkOut/ABS.svg";
                                }

                                else if(dt.Rows[i]["workoutCatTypeName"].ToString() == "TRICEPS" ||
                                    dt.Rows[i]["workoutCatTypeName"].ToString() == "Triceps")
                                {
                                    dt.Rows[i]["imageUrl"] = "../../Images/DietWotkOut/Triceps.svg";

                                }
                                else if (dt.Rows[i]["workoutCatTypeName"].ToString() == "CHEST" ||
                                    dt.Rows[i]["workoutCatTypeName"].ToString() == "Chest")
                                {
                                    dt.Rows[i]["imageUrl"] = "../../Images/DietWotkOut/Chest.svg";
                                }
                                else if (dt.Rows[i]["workoutCatTypeName"].ToString() == "CALF" ||
                                   dt.Rows[i]["workoutCatTypeName"].ToString() == "Calf")
                                {
                                    dt.Rows[i]["imageUrl"] = "../../Images/DietWotkOut/Calf.svg";
                                }
                                else if (dt.Rows[i]["workoutCatTypeName"].ToString() == "SHOULDER" ||
                                  dt.Rows[i]["workoutCatTypeName"].ToString() == "Shoulder")
                                {
                                    dt.Rows[i]["imageUrl"] = "../../Images/DietWotkOut/Shoulder.svg";
                                }
                                else if (dt.Rows[i]["workoutCatTypeName"].ToString() == "YOGA" ||
                                  dt.Rows[i]["workoutCatTypeName"].ToString() == "Yoga")
                                {
                                    dt.Rows[i]["imageUrl"] = "../../Images/DietWotkOut/Yoga.svg";
                                }
                                else if (dt.Rows[i]["workoutCatTypeName"].ToString() == "ZUMBA" ||
                                  dt.Rows[i]["workoutCatTypeName"].ToString() == "Zumba")
                                {
                                    dt.Rows[i]["imageUrl"] = "../../Images/DietWotkOut/Zumba.svg";
                                }
                                 else if (dt.Rows[i]["workoutCatTypeName"].ToString() == "HIIT" ||
                                  dt.Rows[i]["workoutCatTypeName"].ToString() == "HIIT")
                                {
                                    dt.Rows[i]["imageUrl"] = "../../Images/DietWotkOut/Chest.svg";
                                }
                                else
                                {
                                    dt.Rows[i]["imageUrl"] = "../../Images/DietWotkOut/ph2.png";
                                }

                            }

                            dtlWorkOut.DataSource = dt;
                            dtlWorkOut.DataBind();
                        }
                        else
                        {
                            dtlWorkOut.DataBind();
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
    #region WorkOutTypeClick
    protected void lnkWorkOut_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;
        Label lblbookingId = (Label)gvrow.FindControl("lblbookingId");
        Label lblgymOwnerId = (Label)gvrow.FindControl("lblgymOwnerId");
        Label lblbranchId = (Label)gvrow.FindControl("lblbranchId");
        Label lbluserId = (Label)gvrow.FindControl("lbluserId");
        Label lblWorkOutType = (Label)gvrow.FindControl("lblWorkOutType");
        Label lblworkoutCatTypeId = (Label)gvrow.FindControl("lblworkoutCatTypeId");
        HtmlControl divWorkOutContainer = gvrow.FindControl("divWorkOutContainer") as HtmlControl;
        divWorkOutContainer.Attributes["class"] = "col-12 col-sm-2 col-md-2 col-lg-2 divWorkOutContainerSelection";
        Session["WorkOut"] = lblWorkOutType.Text;
        Session["WorkOutId"] = lblworkoutCatTypeId.Text;

        Response.Redirect("WorkOut.aspx");
    }
    #endregion
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

    #endregion



}