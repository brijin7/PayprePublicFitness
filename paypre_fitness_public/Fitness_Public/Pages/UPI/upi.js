'use strict';

//UPI Details
var MerchantName = document.getElementById('hfMerchantName').value;
var TranUpiId = document.getElementById('hfTranUpiId').value;
var MerchantId = document.getElementById('hfMerchantId').value;
var MerchantCode = document.getElementById('hfMerchantCode').value;
var mode = document.getElementById('hfmode').value
var orgid = document.getElementById('hforgid').value;
var sign = document.getElementById('hfsign').value;
var bookingId = document.getElementById('hfBookingId').value;
var amount = document.getElementById('hfAmount').value;


//Post Upi Details
const PostUpiDetails = async function () {
    try {
        //console.log('intent://prematix.com/fitness/ER?url-para=upi://pay?pa=' + TranUpiId + '&pn=' + MerchantName + '&tid=' + MerchantId + '&tr='
        //    + bookingId + '&tn=' + MerchantName + '&am=' + amount + '&cu=INR&url=&mc=' + MerchantCode + '&type=fitness#Intent;scheme=https;action=android.intent.action.VIEW;package=com.prematix.paypreupilite;end')

        var win = window.open('intent://prematix.com/fitness/ER?url-para=upi://pay?pa=' + TranUpiId + '&pn=' + MerchantName + '&tid=' + MerchantId + '&tr='
            + bookingId + '&tn=' + MerchantName + '&am=' + amount + '&cu=INR&url=&mc=' + MerchantCode + '&type=fitness1#Intent;scheme=https;action=android.intent.action.VIEW;package=com.prematix.paypreupilite;end');

            //Mam sented
            //       var win = window.open('intent://paypre.in/fitness/ER?url-para=upi://pay?pa=' + TranUpiId + '&pn=' + MerchantName + '&tid=' + MerchantId + '&tr='
            //+ TranOrderNo + '&tn=' + TranName + '&am=' + Amt + '&cu=INR&url=&mc=' + MerchantCode + '&type=fitness#Intent;scheme=https;action=android.intent.action.VIEW;package=com.prematix.paypreupilite;end');
     
    }
    catch (err) {
        //failurePopUp(err);
    }

}


document.getElementById('payment').onclick = function (e) {
    e.preventDefault();
    PostUpiDetails();
}


