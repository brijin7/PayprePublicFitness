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
using System.Linq.Expressions;

public partial class Pages_Meals_Meal : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            Session["BaseUrlToken"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrlToken"].Trim();
            Session["Master_Branch"] = Session["branchId"].ToString();
            Master.DdlBranch.Visible = false;
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

            GetMealType();
        }
    }
    protected void DtlDays_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Label lbl = e.Item.FindControl("lblDay") as Label;
            Label lblDates = e.Item.FindControl("lblDates") as Label;
            HtmlControl div = e.Item.FindControl("divDays") as HtmlControl;
            if (e.Item.ItemIndex == 0)
            {
                lbl.CssClass = "lblDays lblDaysActive lblDaysSelect";
                lblDates.CssClass = "lblDays lblDaysActive lblDaysSelect";
                div.Attributes["class"] = "divDays DivSelectDate";
            }
            else
            {
                lbl.CssClass = "lblDays lblDaysActive";
                lblDates.CssClass = "lblDays lblDaysActive";
                div.Attributes["class"] = "divDays";
            }
        }
    }
    #endregion


    protected void imgMealType_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnk = sender as ImageButton;
        DataListItem dtl = lnk.NamingContainer as DataListItem;
        Label lblmealType = dtl.FindControl("lblmealType") as Label;
        Label lbldietTypeId = dtl.FindControl("lbldietTypeId") as Label;
        Label lblbookingId = dtl.FindControl("lblbookingId") as Label;
        Label lbluserId = dtl.FindControl("lbluserId") as Label;
        Label lblfromTime = dtl.FindControl("lblfromTime") as Label;
        Label lbltoTime = dtl.FindControl("lbltoTime") as Label;
        ViewState["mealType"] = lblmealType.Text;
        ViewState["bookingId"] = lblbookingId.Text;
        ViewState["userId"] = lbluserId.Text;
        ViewState["dietTypeId"] = lbldietTypeId.Text;
        Label lblmealTypeName = dtl.FindControl("lblmealTypeName") as Label;
        lblMealTypeHead.Text = lblmealTypeName.Text;
        lblMealTime.Text = lblfromTime.Text +" - "+ lbltoTime.Text;
        GetFoodItem();
        FoodItemEdit();
        divMyMeals.Visible = false;
        divMealType.Visible = true;

        for (int i = 0; i < dtlMealTypeList.Items.Count; i++)
        {
            Label lblmealTypes = dtlMealTypeList.Items[i].FindControl("lblmealType") as Label;
            LinkButton lnkMealType = dtlMealTypeList.Items[i].FindControl("lnkMealType") as LinkButton;

            if (lblmealType.Text == lblmealTypes.Text)
            {
                lnkMealType.CssClass = "lblMealTypeHead lblMealTypeHeadSelect";
            }
            else
            {
                lnkMealType.CssClass = "lblMealTypeHead";

            }

        }
    }

    protected void lnkMealType_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        DataListItem dtl = lnk.NamingContainer as DataListItem;
        Label lblmealType = dtl.FindControl("lblmealType") as Label;
        Label lbldietTypeId = dtl.FindControl("lbldietTypeId") as Label;
        Label lblbookingId = dtl.FindControl("lblbookingId") as Label;
        Label lbluserId = dtl.FindControl("lbluserId") as Label;
        Label lblfromTime = dtl.FindControl("lblfromTime") as Label;
        Label lbltoTime = dtl.FindControl("lbltoTime") as Label;
        ViewState["mealType"] = lblmealType.Text;
        ViewState["bookingId"] = lblbookingId.Text;
        ViewState["userId"] = lbluserId.Text;
        ViewState["dietTypeId"] = lbldietTypeId.Text;
        lblMealTypeHead.Text = lnk.Text;
        lblMealTime.Text = lblfromTime.Text + " - " + lbltoTime.Text;
        GetFoodItem();
        FoodItemEdit();
        divMyMeals.Visible = false;
        divMealType.Visible = true;

        for (int i = 0; i < dtlMealTypeList.Items.Count; i++)
        {
            Label lblmealTypes = dtlMealTypeList.Items[i].FindControl("lblmealType") as Label;
            LinkButton lnkMealType = dtlMealTypeList.Items[i].FindControl("lnkMealType") as LinkButton;

            if (lblmealType.Text == lblmealTypes.Text)
            {
                lnkMealType.CssClass = "lblMealTypeHead lblMealTypeHeadSelect";
            }
            else
            {
                lnkMealType.CssClass = "lblMealTypeHead";

            }

        }
    }


    protected void imgMealSwap_Click(object sender, EventArgs e)
    {
        divMealEdit.Visible = true;
        LinkButton lnk = sender as LinkButton;
        DataListItem dtl = lnk.NamingContainer as DataListItem;
        Label lbluniqueId = dtl.FindControl("lbluniqueId") as Label;
        Label lbldietTimeId = dtl.FindControl("lbldietTimeId") as Label;
        Label lblfoodItemId = dtl.FindControl("lblfoodItemId") as Label;
        Label lblfromTime = dtl.FindControl("lblfromTime") as Label;
        Label lbltoTime = dtl.FindControl("lbltoTime") as Label;
        Label lblUserfoodDietTimeId = dtl.FindControl("lblUserfoodDietTimeId") as Label;
        Label lblfoodItemName = dtl.FindControl("lblfoodItemName") as Label;

        ViewState["uniqueId"] = lbluniqueId.Text;
        ViewState["fromTime"] = lblfromTime.Text;
        ViewState["toTime"] = lbltoTime.Text;
        ViewState["UserfoodDietTimeId"] = lblUserfoodDietTimeId.Text;
    }


    protected void imgSwapFood_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnk = sender as ImageButton;
        DataListItem dtl = lnk.NamingContainer as DataListItem;
        Label lblfoodItemId = dtl.FindControl("lblfoodItemId") as Label;
        Label lbldietTimeId = dtl.FindControl("lbldietTimeId") as Label;
        Label lblfoodItemName = dtl.FindControl("lblfoodItemName") as Label;
        ViewState["dietTimeId"] = lbldietTimeId.Text;
        ViewState["foodItemId"] = lblfoodItemId.Text;
        ViewState["foodItemName"] = lblfoodItemName.Text;
        UpdateFoodItem();

    }

    public void UpdateFoodItem()
    {
        try
        {

            DateTime FromTime = Convert.ToDateTime(ViewState["fromTime"].ToString());
            DateTime ToTime = Convert.ToDateTime(ViewState["toTime"].ToString());
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new EditFood()
                {
                    uniqueId = ViewState["uniqueId"].ToString(),
                    dietTimeId = ViewState["dietTimeId"].ToString(),
                    foodItemId = ViewState["foodItemId"].ToString(),
                    fromTime = FromTime.ToString("HH:mm"),
                    ToTime = ToTime.ToString("HH:mm"),
                    foodItemName = ViewState["foodItemName"].ToString(),
                    UserfoodDietTimeId = ViewState["UserfoodDietTimeId"].ToString(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("userFoodMenu/update", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        divMealEdit.Visible = false;
                        GetFoodItem();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

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

    protected void btnSwapClose_Click(object sender, EventArgs e)
    {
        divMealEdit.Visible = false;

    }



    #region Meal Type 
    public void GetMealType()
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
                string Endpoint = "userFoodMenu/listUserDietFood?userId=" + Session["userId"].ToString() + "&bookingId=" + Session["bookingId"].ToString() + "" +
                    "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["UserFoodMenu"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {

                            dt.Columns.Add("imageUrl");
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i]["mealTypeName"].ToString() == "Breakfast")
                                {
                                    dt.Rows[i]["imageUrl"] = "Image/food10.jpg";
                                }

                                else if (dt.Rows[i]["mealTypeName"].ToString() == "Lunch")
                                {
                                    dt.Rows[i]["imageUrl"] = "Image/food8.jpg";

                                }
                                else if (dt.Rows[i]["mealTypeName"].ToString() == "Dinner")
                                {
                                    dt.Rows[i]["imageUrl"] = "Image/food14.jpg";
                                }
                                else if (dt.Rows[i]["mealTypeName"].ToString() == "Snacks1")
                                {
                                    dt.Rows[i]["imageUrl"] = "Image/food1.jpg";
                                }
                                else if (dt.Rows[i]["mealTypeName"].ToString() == "Snacks2")
                                {
                                    dt.Rows[i]["imageUrl"] = "Image/food12.jpg";
                                }
                                else if (dt.Rows[i]["mealTypeName"].ToString() == "Snacks3")
                                {
                                    dt.Rows[i]["imageUrl"] = "Image/food11.jpg";
                                }
                                else
                                {
                                    dt.Rows[i]["imageUrl"] = "Image/food7.jpg";
                                }

                            }


                            dtlMyMeals.DataSource = dt;
                            dtlMyMeals.DataBind();

                            dtlMealTypeList.DataSource = dt;
                            dtlMealTypeList.DataBind();


                        }
                        else
                        {
                            dtlMyMeals.DataBind();
                            dtlMealTypeList.DataBind();
                        }
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                }
                else
                {
                    dtlMyMeals.DataSource = null;
                    dtlMyMeals.DataBind();
                    dtlMealTypeList.DataSource = null;
                    dtlMealTypeList.DataBind();
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
    #region Food item 
    public void GetFoodItem()
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
                string Endpoint = "userFoodMenu/listUserDietFood?userId=" + Session["userId"].ToString() + "&bookingId=" + Session["bookingId"].ToString() + "" +
                    "&mealTypeId=" + ViewState["mealType"].ToString() + "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "&fromDate="+ Date + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["UserFoodMenu"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            int TotalTarget = dt.AsEnumerable().Sum(row => Convert.ToInt16(row["calories"]));
                            int Consumed = dt.AsEnumerable()
                            .Where(row => row["consumingStatus"].ToString() == "Y")
                             .Sum(row => Convert.ToInt32(row["calories"]));
                            PrgDay.Attributes["max"] = TotalTarget.ToString();
                            PrgDay.Attributes["value"] = Consumed.ToString();

                            lblTarget.Text = "Target " + TotalTarget.ToString() + " k";
                            lblConsumed.Text = "Consumed " + Consumed.ToString() + " k";

                            dtlFooditem.DataSource = dt;
                            dtlFooditem.DataBind();

                        }
                        else
                        {
                            dtlFooditem.DataBind();

                        }
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                }
                else
                {
                    dtlFooditem.DataSource = null;
                    dtlFooditem.DataBind();
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

    #region Food Click 
    //protected void imgFoodClick_Click(object sender, ImageClickEventArgs e)
    //{
    //    DateTime TodayDate = DateTime.Now;

    //    ImageButton lnkbtn = sender as ImageButton;
    //    DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;

    //    Label lblbookingId = (Label)gvrow.FindControl("lblbookingId");
    //    Label lblmealType = (Label)gvrow.FindControl("lblmealType");
    //    Label lbluserId = (Label)gvrow.FindControl("lbluserId");
    //    Label lblfoodItemId = (Label)gvrow.FindControl("lblfoodItemId");

    //    List<UserFoodTracking> FoodTracking = new List<UserFoodTracking>();

    //    FoodTracking.AddRange(new List<UserFoodTracking>
    //        {
    //            new UserFoodTracking {userId=lbluserId.Text,bookingId=lblbookingId.Text,foodMenuId=lblfoodItemId.Text,
    //            mealtypeId=lblmealType.Text,consumingStatus="Y",date=TodayDate.ToString("yyyy-MM-dd")
    //            ,createdBy=  lbluserId.Text
    //            }

    //        });

    //    try
    //    {
    //        using (var client = new HttpClient())
    //        {
    //            client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
    //            client.DefaultRequestHeaders.Clear();
    //            client.DefaultRequestHeaders.Accept.Clear();
    //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
    //            var Insert = new InsertTranUserFoodTracking()
    //            {
    //                lstTranUserFoodTracking = FoodTracking.ToList(),
    //            };
    //            HttpResponseMessage response = client.PostAsJsonAsync("tranUserFoodTracking/insert", Insert).Result;
    //            var Fitness = response.Content.ReadAsStringAsync().Result;
    //            int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
    //            string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();
    //            if (response.IsSuccessStatusCode)
    //            {

    //                if (StatusCode == 1)
    //                {

    //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
    //                    GetFoodItem();
    //                }
    //                else
    //                {

    //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
    //                }
    //            }
    //            else
    //            {
    //                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
    //    }

    //}

    protected void dtlFooditem_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "ConsumeFood")
        {
            // Retrieve the data from the DataList controls
            ImageButton imgFoodClick = (ImageButton)e.Item.FindControl("imgFoodClick");
            Label lblbookingId = (Label)e.Item.FindControl("lblbookingId");
            Label lblmealType = (Label)e.Item.FindControl("lblmealType");
            Label lbluserId = (Label)e.Item.FindControl("lbluserId");
            Label lblfoodItemId = (Label)e.Item.FindControl("lblfoodItemId");

            string bookingId = lblbookingId.Text;
            string mealType = lblmealType.Text;
            string userId = lbluserId.Text;
            string foodItemId = lblfoodItemId.Text;

            // Create the UserFoodTracking object
            DateTime todayDate = DateTime.Now;
            List<UserFoodTracking> foodTracking = new List<UserFoodTracking>();

            foodTracking.Add(new UserFoodTracking
            {
                userId = userId,
                bookingId = bookingId,
                foodMenuId = foodItemId,
                mealtypeId = mealType,
                consumingStatus = "Y",
                date = todayDate.ToString("yyyy-MM-dd"),
                createdBy = userId
            });

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                    var insert = new InsertTranUserFoodTracking()
                    {
                        lstTranUserFoodTracking = foodTracking
                    };

                    HttpResponseMessage response = client.PostAsJsonAsync("tranUserFoodTracking/insert", insert).Result;
                    var fitness = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(fitness)["StatusCode"].ToString());
                    string responseMsg = JObject.Parse(fitness)["Response"].ToString();

                    if (response.IsSuccessStatusCode)
                    {
                        if (statusCode == 1)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + responseMsg.Trim() + "');", true);
                            GetFoodItem();
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + responseMsg.Trim() + "');", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + responseMsg.Trim() + "');", true);
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

    #region Bind Food Item Edit
    public void FoodItemEdit()
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
                string Endpoint = "GetFoodItemBasedOnMealType?mealType=" + ViewState["mealType"].ToString() + "" +
                    "&dietTypeId=" + ViewState["dietTypeId"].ToString() + "&userId=" + ViewState["userId"].ToString() + "" +
                    "&bookingId=" + ViewState["bookingId"].ToString() + "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            dtlFooditemEdit.DataSource = dt;
                            dtlFooditemEdit.DataBind();
                        }
                        else
                        {
                            dtlFooditemEdit.DataBind();
                        }
                    }
                    else
                    {
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    dtlFooditemEdit.DataSource = null;
                    dtlFooditemEdit.DataBind();
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

    #region Class 

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

    public class EditFood
    {
        public string uniqueId { get; set; }
        public string dietTimeId { get; set; }
        public string foodItemId { get; set; }
        public string fromTime { get; set; }
        public string foodItemName { get; set; }
        public string ToTime { get; set; }
        public string updatedBy { get; set; }
        public string UserfoodDietTimeId { get; set; }


    }

    #endregion

    protected void dtlFooditemEdit_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ImageButton imgFoodClick = (ImageButton)e.Item.FindControl("imgFoodClick");

            // Assign the OnClientClick event using the ClientID of the ImageButton control
            imgFoodClick.Attributes["onclick"] = "return confirmAndInvokeImgMealTypeClick('" + imgFoodClick.ClientID + "');";
        }
    }


}