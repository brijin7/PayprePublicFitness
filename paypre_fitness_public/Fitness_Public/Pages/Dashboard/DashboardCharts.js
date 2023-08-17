'use strict';
import { bindDate } from '../Dashboard/_flatPickerDateValidations.js'
import { callWebService } from '../Dashboard/_bindDashBoard.js'


let DashboardData;
let ajaxResult;

window.onload = async function () {
    bindDate();
    await BindChart();
    await searchInitializer();
    loader('end');

    //setTimeout(function () {
    //    loader('end');
    //}, 1000);
}
window.onresize = function () {
    caloriesStatus();
    activities();
    foodNutriant();
}

function caloriesStatus() {
    var data;
    let resultTable = [['date', 'caloriesIntake', 'caloriesBurnt']];
    if (DashboardData.length == 0 || DashboardData[0].caloriesDtls.length == 0) {
        data = google.visualization.arrayToDataTable([
            ['Date', 'Calories Intake', 'Calories Burnt'],
            ['', 0, 0],
        ]);
    }
    else {
        DashboardData[0].caloriesDtls.forEach((elmt) => {
            let cal_date = elmt.date;
            let cal_caloriesIntake = elmt.caloriesIntake;
            let cal_caloriesBurnt = elmt.caloriesBurnt;

            let Cal_rows = [];
            Cal_rows = [cal_date, cal_caloriesIntake, cal_caloriesBurnt];

            resultTable.push(Cal_rows);
        });
        data = google.visualization.arrayToDataTable(resultTable);
    }

    var options = {
        title: 'Calories Stats',
        curveType: 'function',
        pointSize: 5,
        pointShape: { type: 'Circle ' },
        colors: ['#54c8ff', '#2185d0'],
        legend: { position: 'bottom' },
    };

    var chart = new google.visualization.LineChart(document.getElementById('curve_Calories'));
    chart.draw(data, options);
}


function activities() {
    let data;
    let resultTable = [['date', 'allocated activities', 'completed activities']];

    if (DashboardData.length == 0 || DashboardData[0].activitiesDtls.length == 0) {
        data = google.visualization.arrayToDataTable([
            ['Date', 'Allocated', 'Completed'],
            ['-', 0, 0],
        ]);
    }
    else {
        DashboardData[0].activitiesDtls.forEach((elmt) => {
            let act_date = elmt.date;
            let act_Allocated = elmt.Allocated;
            let act_Completed = elmt.Completed;

            let act_rows = [];
            act_rows = [act_date, act_Allocated, act_Completed];

            resultTable.push(act_rows);

        });
        data = google.visualization.arrayToDataTable(resultTable);
    }

    var options = {
        title: 'Your Activity',
        titleTextStyle: {
            color: '#212529',
            fontSize: 12,
            bold: true,
        },
        legend: { position: 'none' },
        colors: ['#54c8ff', '#2185d0'],
    };

    var chart = new google.charts.Bar(document.getElementById('chart_Activities'));

    chart.draw(data, google.charts.Bar.convertOptions(options));
}

function foodNutriant() {
    let data;
    let resultTable = [['date', 'calories', 'fat', 'carbs', 'protein']];

    if (DashboardData.length == 0 || DashboardData[0].foodNutriants.length == 0) {
        data = google.visualization.arrayToDataTable([
            ['Date', 'calories', 'fat', 'carbs', 'protein'],
            ['', 0, 0, 0, 0],
        ]);
    }
    else {
        DashboardData[0].foodNutriants.forEach((elmt) => {
            let food_Date = elmt.date;
            let food_calories = elmt.calories;
            let food_fat = elmt.fat;
            let food_carbs = elmt.carbs;
            let food_protein = elmt.protein;

            let food_row = [];
            food_row = [food_Date, food_calories, food_fat, food_carbs, food_protein];

            resultTable.push(food_row);
        });
        data = google.visualization.arrayToDataTable(resultTable);
    }

    var options = {
        title: 'Food',
        titleTextStyle: {
            color: '#212529',
            fontSize: 12,
            bold: true,
        },
        curveType: 'function',
        pointSize: 5,
        pointShape: { type: 'Circle ' },
        colors: ['#54c8ff', '#1558d6', '#2185d0', '#335597'],
        legend: { position: 'bottom' },
    };

    var chart = new google.visualization.LineChart(document.getElementById('curve_FoodNutriants'));

    chart.draw(data, options);
}


async function searchInitializer() {
    const btnsearch = document.getElementById('btnSearch_Dashboard');

    btnsearch.onclick = async function (e) {
        loader('start');
        e.preventDefault();
        await BindChart();
        loader('end');
    };
}


async function BindChart() {
    await callWebService().done(function (response) {
        ajaxResult = response.d;
    }).fail(function (xhr, status, error) {
        console.error(xhr.responseText);
        ajaxResult = '[]';
    });
    DashboardData = JSON.parse(ajaxResult);
    google.charts.load('current', { 'packages': ['corechart', 'bar'] });
    google.charts.setOnLoadCallback(caloriesStatus);
    google.charts.setOnLoadCallback(activities);
    google.charts.setOnLoadCallback(foodNutriant);
}

function loader(startOrEnd) {
    const loader = document.getElementById('divOverlay_loader');

    if (startOrEnd == "start") {
        loader.classList.remove('d-none');
    }
    else {
        loader.classList.add('d-none');
    }
}