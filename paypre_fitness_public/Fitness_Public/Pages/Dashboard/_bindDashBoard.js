export function callWebService() {
    let fromDate = document.getElementById('txtDashboard_FromDate').value;
    let toDate = document.getElementById('txtDashboard_ToDate').value;

    return $.ajax(
        {
            type: "POST",
            url: "Dashboard.aspx/getDashboardDetails",
            data: JSON.stringify({ fromDate, toDate }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        }
    );
}

function successalert(sMsg) {
    swal({
        title: 'Fitness',
        text: sMsg,
        icon: "success"
    });
}
function infoalert(sMsg) {
    swal({
        title: 'Fitness',
        text: sMsg,
        icon: "info"
    });
}
function erroralert(sMsg) {
    swal({
        title: 'Fitness',
        text: sMsg,
        icon: "error"
    });
}