
import { Trainersdetails } from '../Home/Trainer_Details.js'
export function TrainersNameHome() {  
    const hftrainersHomeNameJSON = JSON.parse(document.getElementById('hftrainersHomeName').value);
    const hftrainerdetailsBaseUrl = document.getElementById('hftrainerdetailsBaseUrl').value;
    const TokenTrainers = document.getElementById('hfTokenTrainers').value;
    const HomeTrainersJSONContent = document.getElementById('Trainers-Container');
    let divtrainerdetails = document.getElementById('divtrainerdetails');

    if (hftrainersHomeNameJSON.length === 0) {
        HomeTrainersJSONContent.classList.add('d-none');
        return;
    }
    HomeTrainersJSONContent.classList.remove('d-none');

    let HomeclassesHtml = hftrainersHomeNameJSON.map((elmt) => {
        return `
            <div class="col-12 ClassesCol-Container">
                <div class="ClassesCommon">
                    <img src="${elmt.photoLink}" class="imgTrainer" />
                    <label class="lblTrainerNameId d-none">${elmt.trainerId}</label>
                    <label class="lblTrainerName">${elmt.firstName}</label>
                </div>
            </div>
        `;
    }).join('');

    HomeTrainersJSONContent.innerHTML = "";
    HomeTrainersJSONContent.insertAdjacentHTML("afterbegin", HomeclassesHtml);   


    HomeTrainersJSONContent.addEventListener('click', (event) => {
        if (event.target.classList.contains('imgTrainer')) {
            const trainerId = event.target.parentElement.querySelector('.lblTrainerNameId').textContent;
            buyNowClick(trainerId);
        }
    });

    const buyNowClick = (param) => {
        
        sessionStorage.setItem('LtrainerId', param);
        GetTrainerDetails(param)
    };

    $('.Trainers-Container.owl-carousel').owlCarousel({
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
            document.querySelector(".Trainers-Container").querySelector(".owl-prev").style.display = "none";
        },
        center: false,
        onTranslated: function () {
            var currentSlide = this.current();
            var totalSlides = this.items().length;
            var nextbuttonhide = totalSlides - 4;
            if (currentSlide === 0) {
                document.querySelector(".Trainers-Container").querySelector(".owl-prev").style.display = "none";
            }
            else {
                document.querySelector(".Trainers-Container").querySelector(".owl-prev").style.display = "block";;
            }

            //if (currentSlide >= totalSlides - 1) {
            //    document.querySelector(".Trainers-Container").querySelector(".owl-next").style.display = "none";
            //}
            //else if (currentSlide == lastpreviousSlides) {
            //    document.querySelector(".Trainers-Container").querySelector(".owl-next").style.display = "none";
            //}
            if (currentSlide === nextbuttonhide) {
                document.querySelector(".Trainers-Container").querySelector(".owl-next").style.display = "none";
            }
            else {
                document.querySelector(".Trainers-Container").querySelector(".owl-next").style.display = "block";
            }
        }
    });
    document.querySelector('.owl-prev').innerHTML = `<img src="Images/Testimonials/white-left.svg" class="btnRightLeft" />`;
    document.querySelector('.owl-next').innerHTML = `<img src="Images/Testimonials/white-right.svg" class="btnRightLeft" />`;


    //QR Generator
    function GetTrainerDetails(param) {
        
        $.ajax({
            url: hftrainerdetailsBaseUrl + param,
            type: 'GET',
            data: FormData,
            headers: {
                'Content-Type': 'application/json',
            },
            processData: false,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + TokenTrainers);
            },
            success: function (data) {
                if (data.StatusCode === 0) {
                    
                    divtrainerdetails.classList.add('d-none');
                    console.log(data.TrainerDetails, 'Failure')
                }
                else {
                    
                    //hftrainerdetailsJSON = data.TrainerDetails;
                    sessionStorage.setItem('hftrainerdetailsJSON', JSON.stringify(data.TrainerDetails));
                    divtrainerdetails.classList.remove('d-none');
                    //console.log(hftrainerdetailsJSON, 'Success')
                    console.log(sessionStorage.getItem('hftrainerdetailsJSON'), 'Success')                    
                    Trainersdetails();

                }
            },
            error: function (xhr, data, Response) {
                /*infoPopUp(data.Response);*/

            }
        });
    }
}








//HomeTrainersJSONContent.addEventListener('click', function (event) {
//    if (event.target.classList.contains('imgTrainer')) {
//        const trainerId = event.target.parentElement.querySelector('.lblTrainerNameId').textContent;
//        buyNowClick(trainerId);
//    }
//});

//function buyNowClick(trainerId) {
//    
//    var xhr = new XMLHttpRequest();
//    xhr.open('POST', 'Home.aspx/imgBtnClass_Click1', true);
//    xhr.setRequestHeader('Content-Type', 'application/json; charset=utf-8');
//    xhr.onreadystatechange = function () {
//        
//        if (xhr.readyState === 4 && xhr.status === 200) {
//            // Handle the server response if needed
//            var response = JSON.parse(xhr.responseText);
//            // Update the visibility of the div based on the response
//            var divtrainerdetails = document.getElementById('divtrainerdetails');
//            if (response) {
//                divtrainerdetails.style.display = 'block';
//            } else {
//                divtrainerdetails.style.display = 'none';
//            }

//        }
//    };
//    var data = JSON.stringify({ trainerId: trainerId });
//    xhr.send(data);
//}