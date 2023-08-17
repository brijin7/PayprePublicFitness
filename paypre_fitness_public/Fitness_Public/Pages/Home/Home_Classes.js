export function Classes() {
    const classesJSON = JSON.parse(document.getElementById('Hdn_Home_Classes').value);
    const SectionClasses = document.getElementById('Section-Classes');
    const MasterlnkbtnClasses = document.getElementById('lnkbtnClasses');
    let ClassesContainer = document.getElementById('Classes-Container');


    if (classesJSON.length == 0) {
        SectionClasses.classList.add('d-none');
        MasterlnkbtnClasses.classList.add('d-none');
    }
    else {
        SectionClasses.classList.remove('d-none');
        MasterlnkbtnClasses.classList.remove('d-none');

        let classesHTML = bindClassesHTML(classesJSON);
        sessionStorage.setItem('classescount', classesJSON.length)

        ClassesContainer.innerHTML = "";
        ClassesContainer.insertAdjacentHTML("afterbegin", classesHTML);

        let classCards = document.querySelectorAll('.classes-card');
        const btnHomeClasses = document.getElementById('btn_Home_Classes');

        let HdnCategoryId = document.getElementById('Hdn_Home_Classes_CategoryId');
        let HdnCategoryName = document.getElementById('Hdn_Home_Classes_CategoryName');

        let HdnDispAmount = document.getElementById('Hdn_Home_Classes_DispAmount');
        let HdnSaveAmount = document.getElementById('Hdn_Home_Classes_SaveAmount');

        classCards.forEach((elmt, _0, _1) => {
            elmt.onclick = function () {
                HdnCategoryId.value = elmt.id.split('-')[0];
                HdnCategoryName.value = elmt.id.split('-')[1];
                btnHomeClasses.click();
            }
        });

        $('.Classes-Container.owl-carousel').owlCarousel({
            loop: false,
            nav: true,
            mouseDrag: true,
            touchDrag: true,
            dots: true,
            margin: 20,
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
                document.querySelector(".Classes-Container").querySelector(".owl-prev").style.display = "none"; 
            }, 
            center: false,
            onTranslated: function () {
                var currentSlide = this.current();
                var totalSlides = this.items().length;
                var lastpreviousSlides = totalSlides-2;
                if (currentSlide === 0) {
                    document.querySelector(".Classes-Container").querySelector(".owl-prev").style.display = "none"; 
                }
                else {
                    document.querySelector(".Classes-Container").querySelector(".owl-prev").style.display = "block";;
                }

                if (currentSlide >= totalSlides - 1) {
                    document.querySelector(".Classes-Container").querySelector(".owl-next").style.display = "none";  
                }
                //else if (currentSlide == lastpreviousSlides) {
                //    document.querySelector(".Classes-Container").querySelector(".owl-next").style.display = "none";  
                //}
                else {
                    document.querySelector(".Classes-Container").querySelector(".owl-next").style.display = "block";  
                }
            }
        });
        document.querySelector('.owl-prev').innerHTML = `<img src="Images/Testimonials/white-left.svg" class="btnRightLeft" />`;
        document.querySelector('.owl-next').innerHTML = `<img src="Images/Testimonials/white-right.svg" class="btnRightLeft" />`;

    }
}


function bindClassesHTML(classes) {
    return classes.map((elmt, _0, _1) => {
        let imgUrl = 'Images/Classes/Default.png';

        if (elmt.imageUrl != null && elmt.imageUrl != "") {
            imgUrl = elmt.imageUrl;
        }

        return `<div class="col-12  ClassesCol-Container">
                   <div class="ClassesCommon" >
                    <a id="${elmt.categoryId}-${elmt.categoryName}" title="${elmt.categoryName}" class="classes-card" href="javascript:void(0)">
                        <img src="${imgUrl}" alt="${elmt.categoryName}" class="CmnClassesImg" />
                    <div class="row col-12" style="margin:5%;text-align: justify;">
                    <div class="row col-7" >
                        <Label ID="Label1" runat="server" disabled="disabled" class="lblStart">Start with Just</Label><br>
                        <Label ID="Label2" runat="server" disabled="disabled" class="lblAct">₹${elmt.actualAmount}</Label><br>
                        <Label ID="Label2" runat="server" disabled="disabled" class="lblDisp">₹${elmt.displayAmount}</Label><br>
                        <Label ID="Label3" runat="server" disabled="disabled" class="lblSave">Save ₹${elmt.SavedAmount}</Label>
                    </div>
                    <div class="row col-5" style="align-content: center;">
                        <label id="lblViewPlan" Class="lblViewPlan" >View Plan ></Button>
                    </div>
                </div>
                </a>
             </div>
            </div>`;

    }).join('');
}



// <asp:Label runat="server" Text="Start with Just"></asp:Label></n>
 // <asp:Label runat="server" id=${elmt.displayAmount}></asp:Label>

 //<asp:Label runat="server" id=${elmt.SavedAmount}></asp:Label>