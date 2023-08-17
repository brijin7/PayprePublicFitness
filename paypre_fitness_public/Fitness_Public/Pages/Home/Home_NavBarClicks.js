export function HomeNavBarClick() {

    InitializeScrollOnNavClick();
}
//classes, subscription and testimonials scroll

function InitializeScrollOnNavClick() {
    
    let lnkbtnClasses = document.getElementById('lnkbtnClasses');
    let lnkbtnSubscription = document.getElementById('lnkbtnSubscription');
    let lnkbtnTestimonials = document.getElementById('lnkbtnTestimonials');
    let btnLnkImgDown = document.getElementById('btnLnkImgDown');

    let SectionClasses = document.getElementById('scrollClasses');
    let SectionSubscription = document.getElementById('scrollSubscription');
    let SectionTestimonials = document.getElementById('scrollTestimonials');

    lnkbtnClasses.onclick = function () {
        SectionClasses.scrollIntoView
            ({
                behavior: "smooth", block: "start"
            });
    }

    btnLnkImgDown.onclick = function () {
        SectionClasses.scrollIntoView
            ({
                behavior: "smooth", block: "start"
            });
    }

    lnkbtnSubscription.onclick = function () {
        SectionSubscription.scrollIntoView
            ({
                behavior: "smooth", block: "start"
            });
    }

    lnkbtnTestimonials.onclick = function () {
        SectionTestimonials.scrollIntoView
            ({
                behavior: "smooth", block: "start"
            });
    }
}