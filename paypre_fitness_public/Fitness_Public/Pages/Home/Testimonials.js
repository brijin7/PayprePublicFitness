export function Testimonials() {

    const hfTestimonialsImageJSON = JSON.parse(document.getElementById('hfTestimonialsImage').value);
    const hftestimonialDatasJSON = JSON.parse(document.getElementById('hftestimonialDatas').value);
    const testimonialimageContent = document.getElementById('testimonialimage');
    const testimonialcontent = document.getElementById('testimonialcontent');

    if (hfTestimonialsImageJSON.length == 0) {
        testimonialimageContent.classList.add('d-none');
        return;
    }
    if (hftestimonialDatasJSON.length == 0) {
        testimonialcontent.classList.add('d-none');
        return;
    }
    testimonialimageContent.classList.remove('d-none');
    testimonialcontent.classList.remove('d-none');

    let hftestimonialimageHtml =
        `<img class="imgReview"  runat="server" src=${hfTestimonialsImageJSON[0].imageUrl} />`;
    testimonialimageContent.innerHTML = "";
    testimonialimageContent.insertAdjacentHTML("afterbegin", hftestimonialimageHtml);


    let hftestimonialDatasHtml =

        hftestimonialDatasJSON.map((elmt, _0, _1) => {
            let feedrating;
            if (elmt.feedbackRating == 1) {
                feedrating = `<img  runat="server" class="ImgStar" src="Pages/Home/Image/Star.svg" />`
            }
            else if (elmt.feedbackRating == 2) {
                feedrating = `<img  runat="server" class="ImgStar" src="Pages/Home/Image/Star.svg" />
                               <img  runat="server" class="ImgStar" src="Pages/Home/Image/Star.svg" />`
            }
            else if (elmt.feedbackRating == 3) {
                feedrating = `<img  runat="server" class="ImgStar" src="Pages/Home/Image/Star.svg" />
                               <img  runat="server" class="ImgStar" src="Pages/Home/Image/Star.svg" />
                               <img  runat="server" class="ImgStar" src="Pages/Home/Image/Star.svg" />`
            }
            else if (elmt.feedbackRating == 4) {
                feedrating = `<img  runat="server" class="ImgStar" src="Pages/Home/Image/Star.svg" />
                               <img  runat="server" class="ImgStar" src="Pages/Home/Image/Star.svg" />
                                <img  runat="server" class="ImgStar" src="Pages/Home/Image/Star.svg" />
                               <img  runat="server" class="ImgStar" src="Pages/Home/Image/Star.svg" />`
            }
            else {
                feedrating = `<img  runat="server" class="ImgStar" src="Pages/Home/Image/Star.svg" />
                               <img  runat="server" class="ImgStar" src="Pages/Home/Image/Star.svg" />
                               <img  runat="server" class="ImgStar" src="Pages/Home/Image/Star.svg" />
                                <img  runat="server" class="ImgStar" src="Pages/Home/Image/Star.svg" />
                               <img  runat="server" class="ImgStar" src="Pages/Home/Image/Star.svg" />`

            }
            return `
          <div class="divReviewList reveal fade-right">
                    <div class="row">
                        <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                            <div class="divReviewProfile">
                                    <img class="imgReviewProfile"  runat="server" src=${elmt.imageUrl} />                               
                            </div>
                        </div>
                        <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                            <div class="divReviewDtls">
                                <p>"${elmt.feedbackComment}"</p>
                                <label class="lblReviewerName">- ${elmt.firstName}</label>
                                <br />
                              ${feedrating}
                            </div>
                        </div>
                    </div>
                </div>`;
        }).join('');
    testimonialcontent.innerHTML = "";
    testimonialcontent.insertAdjacentHTML("afterbegin", hftestimonialDatasHtml);

}