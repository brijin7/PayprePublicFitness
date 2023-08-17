export function Subscription() {
    const subscriptionJSON = JSON.parse(document.getElementById('Hdn_Home_Subscription').value);
    const HomeSubscriptionCarousel = document.getElementById('HomeSubscription-Carousel');
    const sectionSubscription = document.getElementById('Section-Subscription');
    const master_lnkbtnSubscription = document.getElementById('lnkbtnSubscription');

    if (subscriptionJSON.length == 0) {
        sectionSubscription.classList.add('d-none');
        master_lnkbtnSubscription.classList.add('d-none');
        return;
    }
    master_lnkbtnSubscription.classList.remove('d-none');
    sectionSubscription.classList.remove('d-none');

    let subscriptionHtml = subscriptionJSON.map((elmt, _0, _1) => {
        let starHtml = '';
        for (let i = 1; i <= elmt.credits; i++) {
            starHtml += '<img src="Images/Subscription/Star.svg" class="ImgStar" />';
        }
        
        let liSubBenefits = '';
        if (elmt.SubsBenefits.length != 0) {
            let ArrSubBenefits = elmt.SubsBenefits.split(",");
            for (let i = 1; i < ArrSubBenefits.length; i++) {
                if (ArrSubBenefits[i - 1].value != '') {
                    liSubBenefits += '<li>' + ArrSubBenefits[i - 1] + '</li>';
                }
            }
        }
        return `
            <div class="col-12 col-sm-8 col-md-10 col-lg-10 col-xl-10 mx-auto subscription-card">
            <div class="SubscriptionCommon" >
                        <div class="SubscriptionPlan-Container">
                        <div class="Stars-Container">
                            ${starHtml}                            </div>
                            <div class="SubscriptionPlan-Desc">
                            <div class="Desc_1">${elmt.packageName}</div>
                                <div class="Desc_2">subscription plan</div>
                            </div>
                        </div>
                        <div class="Subscription-Amount Description">
                            <div class="Desc_1">
                                <ul class="liBnfts">${liSubBenefits}                                                                       
                                </ul>
                            </div>
                        </div>
                        <div class="Subscription-Amount">
                            <div class="Subscription-Amount-Container">
                                <div class="ActualAmount">
                                <div class="Actual-Amount">₹ ${(elmt.actualAmount)} </div>
                                </div>
                                <div class="Amount">
                                    <div class="Rupee-Symbol">₹</div>
                                <div class="Rupee-Amount">${Number(elmt.displayAmount)}</div>
                                <div class="Days"> / ${Number(elmt.noOfDays)} Days</div>
                                </div>
                                <div>
                                <div class="Saved-Amount">Save ₹ ${(elmt.savedAmount)}</div>
                                </div>

                            </div>
                        </div>
                        <div class="Subscription-Buy">
                        <a id="${elmt.subscriptionPlanId}" href="javascript:void(0)" class="btn btnBuyCommon">buy >
                                      
                            </a>
                        </div>
                    </div >
                </div >     
                `;
    }).join('');
    HomeSubscriptionCarousel.innerHTML = "";
    HomeSubscriptionCarousel.insertAdjacentHTML("afterbegin", subscriptionHtml);

    $('.subscription-carousel.owl-carousel').owlCarousel({
        loop: false,
        nav: true,
        mouseDrag: true,
        touchDrag: true,
        dots: false,
        responsive: {
            0: {
                items: 1
                
            },
            600: {
                items: 2
            },
            900: {
                items: 3
            },
            1200: {
                items: 4
            }
        },
        onInitialized: function () {
            document.querySelector(".subscription-carousel").querySelector(".owl-prev").style.display = "none";
        },
        onTranslated: function () {
            var currentSlide = this.current();
            var totalSlides = this.items().length;
            /*  var lastpreviousSlides = totalSlides - 2;*/
            var nextbuttonhide = totalSlides - 4;
            if (currentSlide === 0) {
                document.querySelector(".subscription-carousel").querySelector(".owl-prev").style.display = "none";
            } else {
                document.querySelector(".subscription-carousel").querySelector(".owl-prev").style.display = "block";;
            }
            //if (currentSlide >= totalSlides - 1) {
            //    document.querySelector(".subscription-carousel").querySelector(".owl-next").style.display = "none";
            //}
            //else if (currentSlide == lastpreviousSlides) {
            //    document.querySelector(".subscription-carousel").querySelector(".owl-next").style.display = "none";
            //}
            if (currentSlide === nextbuttonhide) {
                document.querySelector(".subscription-carousel").querySelector(".owl-next").style.display = "none";
            }
            else {
                document.querySelector(".subscription-carousel").querySelector(".owl-next").style.display = "block";
            }
        }
    });

    document.querySelector('.owl-prev').innerHTML = `<img src="Images/Testimonials/white-left.svg" class="btnRightLeft" />`;
    document.querySelector('.owl-next').innerHTML = `<img src="Images/Testimonials/white-right.svg" class="btnRightLeft" />`;

    function SetClickEventOnSubscription() {
        let btnBuyCommon = document.querySelectorAll('.btnBuyCommon');
        let btnBuySubscription = document.getElementById('btn_Home_Subscription');
        let SubscriptionId = document.getElementById('Hdn_Home_SubscriptionId');
        btnBuyCommon.forEach(
      
            (elmt) => {
                
                //console.log(elmt.id, '...')
                //if (elmt.id == 1) {
                //   
                //    document.querySelector('.owl-prev').classList.add('d-none');
                //}
                //else {
                //    //document.querySelector('.owl-prev').classList.remove('d-none');
                //}
                elmt.onclick = function () {
                   
                    SubscriptionId.value = this.id;
                    btnBuySubscription.click();
                }
            }
        );
    }
    SetClickEventOnSubscription();
}