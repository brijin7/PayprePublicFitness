var googleButton = document.getElementById('google-button');
// function to get response
function handleCredentialResponse(response) {
    var responsePayload = decodeJwtResponse(response.credential);
    document.getElementById('txtMail').classList.remove('d-none');
    document.getElementById('lblMail').classList.remove('d-none');
    document.getElementById('txtMobileNo').classList.add('d-none');
    document.getElementById('lblMobile').classList.add('d-none');
    document.getElementById('txtMail').value = responsePayload.email;
    googleButton.style.display = 'none';
    //document.getElementById("txtMobileNo").click();
    $(document.getElementById('txtMail')).change();
}

window.onload = function () {
    google.accounts.id.initialize({
        //client_id: "351957529447-531uvcccddpt51cu76k18dji3spg2tne.apps.googleusercontent.com",
        client_id: "135455860385-caqagcqpt8tbgp74e6rphncd3if3gi5r.apps.googleusercontent.com",
        callback: handleCredentialResponse,
        auto_select: false,
        auto: true
    });
    google.accounts.id.renderButton(
        document.getElementById("google-button"),
        { theme: 'filled_black', size: "medium", width: '200', shape: "pill" }
    );
    google.accounts.id.prompt();
}

// function to decode the response.credential
function decodeJwtResponse(token) {

    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
    return JSON.parse(jsonPayload);
}

$(document.getElementById('txtMobileNo')).keyup(function () {
    var count = document.getElementById('txtMobileNo').value
    if (count.length == 10) {
        document.getElementById('btnSendOTP').classList.remove('d-none');
        document.getElementById('btnCancel').classList.remove('d-none');
        document.getElementById('txtMobileNo').readOnly = true;
    }
});

$(document.getElementById('btnCancel')).click(function () {
    document.getElementById('txtMobileNo').readOnly = false;
    document.getElementById('txtMobileNo').value = "";
    document.getElementById('txtMail').value = "";
    document.getElementById('btnSendOTP').classList.add('d-none');
    document.getElementById('btnCancel').classList.add('d-none');
});