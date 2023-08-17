export function Testimonials() {
    const testimonials = document.getElementById('Hdn_Home_Testimonials').value;
    const testimonialsCarousel = document.getElementById('HomeTestimonials-Carousel');
    const sectionTestimonials = document.getElementById('Section-Testimonials');
    const master_lnkbtnTestimonials = document.getElementById('lnkbtnTestimonials');

    let testimonialsJSON = JSON.parse(testimonials);
    let testimonialsHTML;

    if (testimonials.length == 0) {
        sectionTestimonials.classList.add('d-none');
        master_lnkbtnTestimonials.classList.add('d-none');
        return;
    }
    master_lnkbtnTestimonials.classList.remove('d-none');
    sectionTestimonials.classList.remove('d-none');

    testimonialsHTML = testimonialsJSON.map((currentValue, index, _0) => {       
            return `<img id="Trans_${index}" src="${currentValue.imageUrl}" class="ImgTestimonials" />`
    }
    ).join('');

    testimonialsCarousel.innerHTML = "";
    testimonialsCarousel.insertAdjacentHTML("afterbegin", testimonialsHTML);


    $('.testimonials-carousel.owl-carousel').owlCarousel({
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
            document.querySelector(".testimonials-carousel").querySelector(".owl-prev").style.display = "none";
        },
        onTranslated: function () {
            var currentSlide = this.current();
            var totalSlides = this.items().length;
            var lastpreviousSlides = totalSlides - 2;
            if (currentSlide === 0) {
                document.querySelector(".testimonials-carousel").querySelector(".owl-prev").style.display = "none";
            } else {
                document.querySelector(".testimonials-carousel").querySelector(".owl-prev").style.display = "block";;
            }
            if (currentSlide >= totalSlides - 1) {
                document.querySelector(".testimonials-carousel").querySelector(".owl-next").style.display = "none";
            }
            //else if (currentSlide == lastpreviousSlides) {
            //    document.querySelector(".testimonials-carousel").querySelector(".owl-next").style.display = "none";
            //}
            else {
                document.querySelector(".testimonials-carousel").querySelector(".owl-next").style.display = "block";
            }
        }
    });

    document.querySelector('.owl-prev').innerHTML = `<img src="Images/Testimonials/white-left.svg" class="btnRightLeft" />`;
    document.querySelector('.owl-next').innerHTML = `<img src="Images/Testimonials/white-right.svg" class="btnRightLeft" />`;

    let divTestimonialsSelectedImage = document.getElementById('divTestimonialsSelectedImage');
    let divTestimonialOverlay = document.getElementById('divTestimonialOverlay');
    let AllImages = document.querySelectorAll('.ImgTestimonials');
    let owlnav = document.querySelector('.owl-nav');
    let SelectedTransFormation = document.getElementById('SelectedTransFormation');

    //Setting Click Event
    function SetClickEvent() {
        AllImages.forEach((Img) => {
            Img.onclick = ImageClick
        })

        divTestimonialOverlay.onclick = function () {
            this.classList.toggle('d-none');
            divTestimonialsSelectedImage.classList.toggle('d-none');
            owlnav.classList.remove('d-none');

            document.querySelectorAll('.ImgTestimonials').forEach((Img) => { Img.classList.remove('ImgTestimonials_Scale') });
            RemoveSelected();
            document.getElementsByTagName('body')[0].classList.remove('overflow-hidden');
        }
    }

    //on image click this function will be called
    function ImageClick() {
        divTestimonialOverlay.classList.toggle('d-none');
        divTestimonialsSelectedImage.classList.toggle('d-none');

        SelectedTransFormation.src = this.src;
        this.classList.add('selected');

        AllImages.forEach((Img) => {
            Img.classList.add('ImgTestimonials_Scale');
        })

        owlnav.classList.add('d-none');

        document.getElementsByTagName('body')[0].classList.add('overflow-hidden');
    }

    //setting events to left and right button
    function btnWhiteClickInitializer() {
        let btnTestWhiteLeft = document.getElementById('btnTestWhiteLeft');
        let btnTestWhiteRight = document.getElementById('btnTestWhiteRight');

        btnTestWhiteLeft.onclick = btnWhiteLeftClick
        btnTestWhiteRight.onclick = btnWhiteRightClick
    }

    //btn White left click
    function btnWhiteLeftClick() {
        let CurrentlySelectedImageId = document.querySelector('.ImgTestimonials.selected').id.split('_')[1];
        let PreviousImageId;
        if (CurrentlySelectedImageId == '0') {
            PreviousImageId = testimonialsJSON.length - 1;
            SelectedTransFormation.src = testimonialsJSON[PreviousImageId].imageUrl;
        }
        else {
            PreviousImageId = Number(CurrentlySelectedImageId) - 1;
            SelectedTransFormation.src = testimonialsJSON[PreviousImageId].imageUrl;
        }
        RemoveSelected();
        document.getElementById(`Trans_${PreviousImageId}`).classList.add('selected');
    }

    //btn White Right click
    function btnWhiteRightClick() {
        let CurrentlySelectedImageId = document.querySelector('.ImgTestimonials.selected').id.split('_')[1];

        let NextImageId;

        if (CurrentlySelectedImageId == (testimonialsJSON.length - 1)) {
            NextImageId = 0;
            SelectedTransFormation.src = testimonialsJSON[NextImageId].imageUrl;
        }
        else {
            NextImageId = Number(CurrentlySelectedImageId) + 1;
            SelectedTransFormation.src = testimonialsJSON[NextImageId].imageUrl;
        }
        RemoveSelected();
        document.getElementById(`Trans_${NextImageId}`).classList.add('selected');
    }
    //Remove Selected in container
    function RemoveSelected() {
        document.querySelectorAll('.ImgTestimonials').forEach((elmt) => {
            elmt.classList.remove('selected')
        });
    }

    SetClickEvent();
   btnWhiteClickInitializer();  
}

