
const divupi = document.getElementById('divupi');
const divQRcode = document.getElementById('QRcode');
const Timer = document.getElementById('Timer');
const qrtimer = document.getElementById('qrtimer');
const rqheader = document.getElementById('rqheader');

if (divupi != null) {
    const subscriptionJSON = document.getElementById('hfSubscriptionInsert').value;
    //const hfNotificationSMSDatasJSON = document.getElementById('hfNotificationSMSDatas').value;
    const Baseurl = document.getElementById('hfBaseurl').value;
    const Token = document.getElementById('hfToken').value;
    const NotificationSMSBaseurl = document.getElementById('hfNotificationSMSBaseurl').value;
    const PaymentBaseurl = document.getElementById('hfPaymentBaseurl').value;
    if (sessionStorage.getItem("PaymentAfterBookingId") != null) {
        document.getElementById('verifyandpay').value = "Resend SMS";
        let NotificationData = {};
        NotificationData = {
            queryType: "SendSMSforbookingclasses",
            mobileNo: document.getElementById('hfNotificationSMSDatas').value,
            Userid: document.getElementById('hfNotificationSMSUserId').value,
            link: 'https://prematix.com/fitness/Pages/upi/upi.aspx?Plan=C&BookingId=' + sessionStorage.getItem("PaymentAfterBookingId")
        };
        sessionStorage.setItem('Notificationsmsdetails', JSON.stringify(NotificationData));
    }

    //var fiveMinutess = 60 * 10,
    var fiveMinutess = 60 * 10,
        display = document.querySelector('#paytimer');
    PayTstartTimer(fiveMinutess, display);
    //upi Click
    document.getElementById('divupi').onclick = function () {
        clearInterval(window.bookingresponse);
        clearInterval(window.paytimer);
        divQRcode.removeAttribute("class");
        divupi.setAttribute("class", "active");
        document.getElementById('a').style.display = 'block';
        document.getElementById('g').style.display = 'none';
        sessionStorage.setItem('Bookingby', 'SMS');
        //var fiveMinutess = 60 * 10,
        var fiveMinutess = 60 * 10,
            display = document.querySelector('#paytimer');
        PayTstartTimer(fiveMinutess, display);
        if (sessionStorage.getItem("PaymentAfterBookingId") != null) {
            document.getElementById('fname').classList.remove('d-none');
            document.getElementById('verifyandpay').classList.remove('d-none');
            document.getElementById('verifyandpay').value = "Resend SMS";
        }
        else {
            QRBookingDetails();
        }
    };
    //QR Click
    document.getElementById('QRcode').onclick = function (e) {
        e.preventDefault();
        clearInterval(window.bookingresponse);
        divupi.removeAttribute("class");
        divQRcode.setAttribute("class", "active");
        sessionStorage.setItem('Bookingby', 'QR');
        document.getElementById('rqheader').style.display = 'none';
        clearInterval(window.timer);
        //var fiveMinutes = 60 * 5,
        var fiveMinutes = 60 * 5,
            display = document.querySelector('#qrtimer');
        QRTstartTimer(fiveMinutes, display);
        document.getElementById('a').style.display = 'none';
        document.getElementById('g').style.display = 'block';
        if (sessionStorage.getItem("PaymentAfterBookingId") == null) {
            QRBookingDetails();
        }
        else {
            document.getElementById('qrerrorresponse').innerHTML = "Already Booked!";
            document.getElementById('qrcode').classList.add('d-none');
            function blinkText() {
                var blink = document.getElementById('qrerrorresponse');
                blink.style.visibility = blink.style.visibility === 'hidden' ? 'visible' : 'hidden';
            }
            window.bookingresponse = setInterval(blinkText, 1000);
        }

    };

    //QR Timer
    function QRTstartTimer(duration, display) {
        document.getElementById('rqheader').style.display = 'block';
        let timeInSeconds = duration;
        window.timer = setInterval(() => {
            timeInSeconds--;
            var minutes = Math.floor(timeInSeconds / 60);
            var seconds = timeInSeconds % 60;
            minutes = minutes < 10 ? "0" + minutes : minutes;
            seconds = seconds < 10 ? "0" + seconds : seconds;
            display.textContent = minutes + ":" + seconds;
            if (minutes == 0 && seconds == 0) {
                document.getElementById('qrtimer').innerHTML = '00:00';
                document.getElementById('Divpaymentdetails').classList.add('d-none');
                document.getElementById('Divsessionover').classList.remove('d-none');
                clearInterval(window.timer);
                sessionStorage.clear();
                clearInterval(window.paymentresponse);
            }
        }, 1000);
        //************************************************** */
    }

    //Pay Timer
    function PayTstartTimer(duration, display) {
        document.getElementById('payheader').style.display = 'block';
        let timeInSeconds = duration;
        window.paytimer = setInterval(() => {
            timeInSeconds--;
            var minutes = Math.floor(timeInSeconds / 60);
            var seconds = timeInSeconds % 60;
            minutes = minutes < 10 ? "0" + minutes : minutes;
            seconds = seconds < 10 ? "0" + seconds : seconds;
            display.textContent = minutes + ":" + seconds;
            if (minutes == 0 && seconds == 0) {
                document.getElementById('paytimer').innerHTML = '00:00';
                //document.getElementsByClassName("Paymentpage")[0].classList.remove("paymentpopupblur");
                //SessionPopupBag.style.display = 'block';
                document.getElementById('Divpaymentdetails').classList.add('d-none');
                document.getElementById('Divsessionover').classList.remove('d-none');
                clearInterval(window.timer);
                sessionStorage.clear();
                clearInterval(window.paymentresponse);               
            }
        }, 1000);
        //************************************************** */
    }

    //QR Generator
    function QRBookingDetails() {
        $.ajax({
            url: Baseurl,
            type: 'POST',
            data: subscriptionJSON,
            headers: {
                'Content-Type': 'application/json',
            },
            processData: false,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + Token);
            },
            success: function (data) {
                if (data.StatusCode === 0) {
                    //document.getElementById('rqheader').classList.add('d-none');
                    document.getElementById('qrcode').classList.add('d-none');
                    document.getElementById('fname').classList.add('d-none');
                    document.getElementById('verifyandpay').classList.add('d-none');
                    document.getElementById('qrerrorresponse').classList.remove('d-none');
                    document.getElementById('payrrorresponse').classList.remove('d-none');
                    if (sessionStorage.getItem("Bookingby") == 'QR') {
                        if (sessionStorage.getItem("PaymentAfterBookingId") != null) {
                            //document.getElementById('rqheader').classList.remove('d-none');
                            //document.getElementById('qrcode').classList.remove('d-none');
                            ////var qrdatas = 'https://paypre.in/upi.aspx?BookingId=' + sessionStorage.getItem("PaymentAfterBookingId");
                            //var qrdatas = 'https://prematix.com/fitness/Pages/upi/upi.aspx?Plan=S&BookingId=' + sessionStorage.getItem("PaymentAfterBookingId");
                            //new QRCode(document.getElementById(`qrcode`), {
                            //    text: `${qrdatas}`,
                            //    width: 128,
                            //    height: 128,
                            //    colorDark: "#000000",
                            //    colorLight: "#ffffff",
                            //    correctLevel: QRCode.CorrectLevel.H
                            //})
                        }
                        else {
                            document.getElementById('qrerrorresponse').innerHTML = data.Response;
                            function blinkText() {
                                var blink = document.getElementById('qrerrorresponse');
                                blink.style.visibility = blink.style.visibility === 'hidden' ? 'visible' : 'hidden';
                            }
                            window.bookingresponse = setInterval(blinkText, 1000);
                        }
                    }
                    else {

                        document.getElementById('payrrorresponse').innerHTML = data.Response;
                        function blinkText() {
                            var blink = document.getElementById('payrrorresponse');
                            blink.style.visibility = blink.style.visibility === 'hidden' ? 'visible' : 'hidden';
                        }
                        window.bookingresponse = setInterval(blinkText, 1000);
                    }

                }
                else {
                    document.getElementById('rqheader').classList.remove('d-none');
                    document.getElementById('qrcode').classList.remove('d-none');
                    document.getElementById('fname').classList.remove('d-none');
                    document.getElementById('verifyandpay').classList.remove('d-none');
                    document.getElementById('qrerrorresponse').classList.add('d-none');
                    document.getElementById('payrrorresponse').classList.add('d-none');
                    if (sessionStorage.getItem("Bookingby") == 'QR') {
                        const str = data.Response;
                        const arr = str.split("~");
                        sessionStorage.setItem("PaymentAfterBookingId", arr[0])
                        document.getElementById("hfBookingId").value = sessionStorage.getItem("PaymentAfterBookingId");
                        var qrdatas = 'https://prematix.com/fitness/Pages/upi/upi.aspx?Plan=S&BookingId=' + sessionStorage.getItem("PaymentAfterBookingId");
                        new QRCode(document.getElementById(`qrcode`), {
                            text: `${qrdatas}`,
                            width: 128,
                            height: 128,
                            colorDark: "#000000",
                            colorLight: "#ffffff",
                            correctLevel: QRCode.CorrectLevel.H
                        })
                        window.paymentresponse = setInterval(BookingPaymentstatus, 1000)
                    }
                    else {
                        const str = data.Response;
                        const arr = str.split("~");
                        sessionStorage.setItem("PaymentAfterBookingId", arr[0])
                        document.getElementById("hfBookingId").value = sessionStorage.getItem("PaymentAfterBookingId");
                        window.paymentresponse = setInterval(BookingPaymentstatus, 1000)
                        let NotificationData = {};
                        NotificationData = {
                            queryType: "SendSMSforbookingclasses",
                            mobileNo: document.getElementById('hfNotificationSMSDatas').value,
                            Userid: document.getElementById('hfNotificationSMSUserId').value,
                            link: 'https://prematix.com/fitness/Pages/upi/upi.aspx?Plan=C&BookingId=' + sessionStorage.getItem("PaymentAfterBookingId")
                        };
                        sessionStorage.setItem('Notificationsmsdetails', JSON.stringify(NotificationData));
                        SendingNotificationSMS();
                        //console.log("Booked Successfully") // Method used to sent sms.
                    };
                }
            },
            error: function (xhr, data, Response) {
                /* sessionStorage.setItem('Inserteddata', data)*/
                //infoPopUp(data.Response);
            }
        });
    }

    //QR Click
    document.getElementById('verifyandpay').onclick = function (e) {
        e.preventDefault();
        sessionStorage.setItem('Bookingby', 'SMS');
        if (sessionStorage.getItem("PaymentAfterBookingId") != null) {
            SendingNotificationSMS();
            //console.log("Already Booked"); // Method used to sent sms.
        }
        else {
            QRBookingDetails();
        }

    };

    //QR Generator
    function BookingPaymentstatus() {
        var FormData = 'BookingId=' + sessionStorage.getItem("PaymentAfterBookingId");

        $.ajax({
            url: PaymentBaseurl,
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
                    console.log(data.Response, 'Failure')
                }
                else {
                    if (data.Response[0].paymentStatus == "P") {
                        console.log(data.Response[0].paymentStatus, 'Status')
                        document.getElementById('Divpaymentdetails').classList.add('d-none');
                        document.getElementById('Divpaymentsuccess').classList.remove('d-none');
                        clearInterval(window.paymentresponse);
                        window.redirectpage = setInterval(redirect, 5000);
                    }

                }
            },
            error: function (xhr, data, Response) {
                /*infoPopUp(data.Response);*/

            }
        });
    }

    //upi Click
    document.getElementById('closepaymentpopup').onclick = function () {
        document.getElementById('Divpaymentsuccess').classList.add('d-none');
    };

    function redirect() {
        clearInterval(window.redirectpage);
        window.location.replace("../Dashboard/Dashboard.aspx"); 
    }

    //Sending Notification SMS
    function SendingNotificationSMS() {
        var notificationdetails = sessionStorage.getItem('Notificationsmsdetails');
        $.ajax({
            url: NotificationSMSBaseurl,
            type: 'POST',
            data: notificationdetails,
            headers: {
                'Content-Type': 'application/json',
            },
            processData: false,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + Token);
            },
            success: function (data) {
                if (data.StatusCode === 0) {
                    console.log('Failure');
                }
                else {
                    console.log('Success');
                }
            },
            error: function (xhr, data, Response) {
            }
        });
    }

}
else {
    sessionStorage.clear();
}

