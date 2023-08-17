$(document).ready(function () {
    
   
    if (localStorage.getItem('windowSizeratio') != null) {
        localStorage.removeItem('windowSizeratio');
    } else {
      
        var value = "0";
        if ($(this).width() > 0 && $(this).width() <= 580) {
            value = "580";
        }
        else if ($(this).width() > 580 && $(this).width() <= 980) {
            value = "980";
        } else if ($(this).width() > 980) {
            value = "981";
        }
       
        localStorage.setItem('windowSizeratio', value);
    }
   
    resize($(this).width());
});
window.onresize = function (event) {
    
  
    resize($(this).width());
};
function resize(width) {
    
  
    var ratio = localStorage.getItem('windowSizeratio');
    
    if (width > 0 && width < 718 && ratio != "718" && $("[id*=hfColumnRepeat]").val() != "1") {
        $("[id*=hfColumnRepeat]").val(1);
        $("[id*=btnfake]").click();
    }
    else if (width >= 718 && width <= 980 && ratio != "980" && $("[id*=hfColumnRepeat]").val() != "2") {
        $("[id*=hfColumnRepeat]").val(2);
        $("[id*=btnfake]").click();
    }
    else if (width > 980 && ratio != "981" && $("[id*=hfColumnRepeat]").val() != "3") {
        $("[id*=hfColumnRepeat]").val(3);
        $("[id*=btnfake]").click();
    }
}