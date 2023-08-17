
export function ClassesHome() {
    const hfclassescontainerJSON = JSON.parse(document.getElementById('hfclassescontainer').value);
    const HomeclassesJSONContent = document.getElementById('Classes-Container');

    if (hfclassescontainerJSON.length === 0) {
        HomeclassesJSONContent.classList.add('d-none');
        return;
    }
    HomeclassesJSONContent.classList.remove('d-none');

    let HomeclassesHtml = hfclassescontainerJSON.map((elmt) => {
        return `
            <div class="col-12 ClassesCol-Container">
                <div class="ClassesCommon">
                    <img src="${elmt.imageUrl}" class="imgTrainers" />
                    <label class="lblClassName d-none">${elmt.categoryId}</label>
                    <label class="lblClassName">${elmt.categoryName}</label>
                    <button class="btnCBuyNow">Buy Plan</button>    
                </div>
            </div>
        `;
    }).join('');

    HomeclassesJSONContent.innerHTML = "";
    HomeclassesJSONContent.insertAdjacentHTML("afterbegin", HomeclassesHtml);

    HomeclassesJSONContent.addEventListener('click', function (event) {
        if (event.target.classList.contains('btnCBuyNow')) {
            const categoryId = event.target.previousElementSibling.previousElementSibling.textContent;
            buyNowClick(categoryId);
        }
    });

    function buyNowClick(categoryId) {
        
        var xhr = new XMLHttpRequest();
        xhr.open('POST', 'Home.aspx/btnBuyNow_Click', true);
        xhr.setRequestHeader('Content-Type', 'application/json; charset=utf-8');
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4 && xhr.status === 200) {
                // Handle the server response if needed
                var response = JSON.parse(xhr.responseText);
                console.log(response);
                // Perform any additional client-side actions after successful server-side execution
                // For example, you can redirect to another page
                window.location.href = 'Pages/Booking/Booking.aspx';
            }
        };
        var data = JSON.stringify({ categoryId: categoryId });
        xhr.send(data);
    }

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
            var nextbuttonhide = totalSlides - 4;
            if (currentSlide === 0) {
                document.querySelector(".Classes-Container").querySelector(".owl-prev").style.display = "none";
            }
            else {
                document.querySelector(".Classes-Container").querySelector(".owl-prev").style.display = "block";
            }

            //if (currentSlide >= totalSlides - 1) {
            //    document.querySelector(".Classes-Container").querySelector(".owl-next").style.display = "none";
            //    console.log('3');
            //}
            //else if (currentSlide == lastpreviousSlides) {
            //    document.querySelector(".Classes-Container").querySelector(".owl-next").style.display = "none";
            //    console.log('4');
            //}
            // if (currentSlide >= totalSlides - 1) {
            //    document.querySelector(".Classes-Container").querySelector(".owl-next").style.display = "none";
            //    console.log('3');
            //}
            //else
            if (currentSlide === nextbuttonhide) {
                document.querySelector(".Classes-Container").querySelector(".owl-next").style.display = "none";               
            }
            else {
                document.querySelector(".Classes-Container").querySelector(".owl-next").style.display = "block";
            }
        }
    });
    document.querySelector('.owl-prev').innerHTML = `<img src="Images/Testimonials/white-left.svg" class="btnRightLeft" />`;
    document.querySelector('.owl-next').innerHTML = `<img src="Images/Testimonials/white-right.svg" class="btnRightLeft" />`;


 
}


