var lblNotifCount = document.getElementById('lblNotifCount');
var divshownotification = document.getElementById('divshownotification');
var lnkNotification = document.getElementById('lnkNotification');
var hfNotificationPosturl = document.getElementById('hfNotificationPosturl').value;
var hfNotificationURl = document.getElementById('hfNotificationURl').value;
var hfNotificationURlData = document.getElementById('hfNotificationURlData').value;
var Token = document.getElementById('hfTokenURl').value;

GetUserNotification();

document.getElementById('lnkNotification').onclick = function (e) {
    e.preventDefault();
    if (sessionStorage.getItem('NotificationGet') == 1) {
        if (sessionStorage.getItem('VisitNotification') == 0) {
            document.getElementById('divshownotification').classList.remove('d-none');
            sessionStorage.setItem('VisitNotification', 1)
        }
        else if (sessionStorage.getItem('VisitNotification') == null) {
            document.getElementById('divshownotification').classList.remove('d-none');
            sessionStorage.setItem('VisitNotification', 1)
        }
        else {
            document.getElementById('divshownotification').classList.add('d-none');
            sessionStorage.setItem('VisitNotification', 0)
        }
    }
    else {
        document.getElementById('divshownotification').classList.add('d-none');
        sessionStorage.setItem('VisitNotification', 0)
    }
    
    
}

//Get User Notification
function GetUserNotification() {
    var FormData = hfNotificationURlData;

    $.ajax({
        url: hfNotificationURl,
        type: 'GET',
        data: FormData,
        headers: {
            'Content-Type': 'application/json',
        },
        processData: false,
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + Token);
        },
        success: function (data) {
            if (data.StatusCode === 0) {
                sessionStorage.setItem('NotificationGet', 0)
                lblNotifCount.classList.add('d-none');
                //console.log(data.Response, 'Failure')
            }
            else {    
                lblNotifCount.classList.remove('d-none');
                sessionStorage.setItem('NotificationGet', 1)
                sessionStorage.setItem('NotificationData', JSON.stringify(data.Response))
                ShowUserNotification();
                //console.log(data.Response, 'Success')
            }
        },
        error: function (xhr, data, Response) {
            /*infoPopUp(data.Response);*/

        }
    });
    //if (sessionStorage.getItem('NotificationGet') == 1) {
    //    //console.log(sessionStorage.getItem('NotificationData'), '1')

    //    var datajson = JSON.parse(sessionStorage.getItem('NotificationData'));
    //    lblNotifCount.innerHTML = datajson.length;
    //    let Active__slider = `
    //           ${datajson.map((dtRow) => {
    //        return `<asp:Label runat ="server" CssClass="lblNotificationList" ID="lblNotificationList-${dtRow.notificationId}">${dtRow.notification}</asp:Label> </br>`
    //    }
    //    ).join('')}`;

    //    divshownotification.innerHTML = '';
    //    divshownotification.insertAdjacentHTML('afterbegin', Active__slider);
    //}
    
}

//Get User Notification
function ShowUserNotification() {
    if (sessionStorage.getItem('NotificationGet') == 1) {
        //console.log(sessionStorage.getItem('NotificationData'), '1')

        var datajson = JSON.parse(sessionStorage.getItem('NotificationData'));
        lblNotifCount.innerHTML = datajson.length;
        let Active__slider = `
               ${datajson.map((dtRow) => {
                   return `<p runat ="server" Class="lblNotificationList"  ClientIDMode="Static" onclick="sendValue(${dtRow.notificationId})" id="lblNotificationList-${dtRow.notificationId}">${dtRow.notification}</p> </br>`
        }
        ).join('')}`;

        divshownotification.innerHTML = '';
        divshownotification.insertAdjacentHTML('afterbegin', Active__slider);
    }

}

//Get PinId for Insert
function sendValue(param) {
    //console.log(param,'we get in sendvalue')
    sessionStorage.setItem('ReadedNotificationId', param);
    PostUserNotification();
}

//Post User Notification
function PostUserNotification() {
    //var FormData = sessionStorage.getItem('ReadedNotificationId');
    let NotificationData = {};
    NotificationData = {
        notificationId: sessionStorage.getItem('ReadedNotificationId')
    };
    sessionStorage.setItem('ReadedNotificationIddetails', JSON.stringify(NotificationData));

    var FormData = sessionStorage.getItem('ReadedNotificationIddetails');

    $.ajax({
        url: hfNotificationPosturl,
        type: 'POST',
        data: FormData,
        headers: {
            'Content-Type': 'application/json',
        },
        processData: false,
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + Token);
        },
        success: function (data) {
            if (data.StatusCode === 0) {
                console.log(data.Response, 'Failure Post')
            }
            else {
                //ShowUserNotification();
                GetUserNotification();
                console.log(data.Response, 'Success Post')
            }
        },
        error: function (xhr, data, Response) {
            /*infoPopUp(data.Response);*/

        }
    });

}