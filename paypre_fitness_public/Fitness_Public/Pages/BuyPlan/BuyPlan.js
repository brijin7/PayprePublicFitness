﻿///Start - Dynamic Repeat column Jquery script
$(document).ready(function () {
    // create local storage or check if local storage if not null then remove already exist
    // Null checking you need to make because only ones you need to call it
    if (localStorage.getItem('windowSizeratio') != null) {
        localStorage.removeItem('windowSizeratio');
    } else {
        // Assign ratio value as per your resize window setting
        var value = "0";
        if ($(this).width() > 0 && $(this).width() <= 540) {
            value = "540";
        }
        else if ($(this).width() > 540 && $(this).width() <= 980) {
            value = "980";
        } else if ($(this).width() > 980) {
            value = "981";
        }
        //Assign localstorage value
        localStorage.setItem('windowSizeratio', value);
    }
    // Call this function which will set hidden field value also it will call the button click event.
    resize($(this).width());
});
window.onresize = function (event) {
    // On resize window function will get call
    resize($(this).width());
};
function resize(width) {
    //Assign localstorage value to variable if it exist else it will undefined
    var ratio = localStorage.getItem('windowSizeratio');
    //Check the width also you need to check it’s not in same ratio which was it before
    //So it will not call repeatedly for many times as infinite by checking ratio value which assigned from local storage.
    //And assign hidden field value and call the buttonclick event from jquery fuction so it will set the RepeatColumns value.
    if (width > 0 && width <= 540 && ratio != "540" && $("[id*=hfColumnRepeat]").val() != "1") {
        $("[id*=hfColumnRepeat]").val(1);
        $("[id*=btnfake]").click();
    }
    else if (width > 540 && width <= 980 && ratio != "980" && $("[id*=hfColumnRepeat]").val() != "2") {
        $("[id*=hfColumnRepeat]").val(2);
        $("[id*=btnfake]").click();
    }
    else if (width > 980 && ratio != "981" && $("[id*=hfColumnRepeat]").val() != "3") {
        $("[id*=hfColumnRepeat]").val(3);
        $("[id*=btnfake]").click();
    }
}
///End - Dynamic Repeat column Jquery script
'use strict';

$(document.getElementById('lbltrainingTypeName')).click(function (e) {
    document.getElementById("popup-1").style.display = "Block";
});

$(document.getElementById('popup-2')).click(function (e) {
    document.getElementById("popup-1").style.display = "none";
});

$(document.getElementById('slotselect')).click(function (e) {
    document.getElementById('slotselect').classList.add("app-borderactive");
    document.getElementById('slotselect').classList.remove("app-border");
    document.getElementById('slottime').classList.remove("d-none");
});

$(document.getElementById('slotselect1')).click(function (e) {
    document.getElementById('slotselect1').classList.add("app-borderactive");
    document.getElementById('slotselect1').classList.remove("app-border");
    document.getElementById('slottime1').classList.remove("d-none");
});


$(document.getElementById('slotselect2')).click(function (e) {
    document.getElementById('slotselect2').classList.add("app-borderactive");
    document.getElementById('slotselect2').classList.remove("app-border");
    document.getElementById('slottime2').classList.remove("d-none");
});


$(document.getElementById('slotselect3')).click(function (e) {
    document.getElementById('slotselect3').classList.add("app-borderactive");
    document.getElementById('slotselect3').classList.remove("app-border");
    document.getElementById('slottime3').classList.remove("d-none");
});


$(document.getElementById('slotselect4')).click(function (e) {
    document.getElementById('slotselect4').classList.add("app-borderactive");
    document.getElementById('slotselect4').classList.remove("app-border");
    document.getElementById('slottime4').classList.remove("d-none");
});


$(document.getElementById('slotselect5')).click(function (e) {
    document.getElementById('slotselect5').classList.add("app-borderactive");
    document.getElementById('slotselect5').classList.remove("app-border");
    document.getElementById('slottime5').classList.remove("d-none");
});


$(document.getElementById('slotselect6')).click(function (e) {
    document.getElementById('slotselect6').classList.add("app-borderactive");
    document.getElementById('slotselect6').classList.remove("app-border");
    document.getElementById('slottime6').classList.remove("d-none");
});

$(document.getElementById('slotselect7')).click(function (e) {
    document.getElementById('slotselect7').classList.add("app-borderactive");
    document.getElementById('slotselect7').classList.remove("app-border");
    document.getElementById('slottime7').classList.remove("d-none");
});
