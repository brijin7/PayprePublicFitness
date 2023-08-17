export function Trainersdetails() {
    const hftrainerdetailsJSON = JSON.parse(sessionStorage.getItem('hftrainerdetailsJSON'));
    const trainerdetailscertficatesContent = document.getElementById('trainerdetailscertficates');

    if (hftrainerdetailsJSON.length == 0) {
        trainerdetailscertficatesContent.classList.add('d-none');
        return;
    }
    trainerdetailscertficatesContent.classList.remove('d-none');
  

    let hfTrainersdetailsHtml = `
    <div class="row">
        <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-4 pe-0 ps-0 mb-5">
            <div class="DivHead">
                <asp:Label runat="server" class="lblAccHead">Certificate of Excellence</asp:Label>
            </div>
            <div class="carouselDiv">
                <div class="carousel right">
                    <div class="slide"></div>
                    <div class="wrap">
                        <ul>
                            ${hftrainerdetailsJSON
            .map((elmt, _0, _1) => {
                return `
                                        <li>
                                            <label id="CCloseBtn_${elmt.uniqueId}" class="CClose d-none">X</label>
                                            <label class="lblC1Text">
                                                ${elmt.specialistTypeName}
                                                <br />
                                                Achievement
                                            </label>
                                            <label class="lblC1Text1">
                                                ${elmt.qualification}
                                                <br />
                                            </label>
                                            <img id="imgC1_${elmt.uniqueId}" class="imgC8" src="${elmt.certificates}" />
                                        </li>`;
            })
            .join('')}
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>`;

    trainerdetailscertficatesContent.innerHTML = "";
    trainerdetailscertficatesContent.insertAdjacentHTML("afterbegin", hfTrainersdetailsHtml);

    function CClose(param) {
        var imgC1 = document.getElementById('imgC1_' + param);
        var CCloseBtn = document.getElementById('CCloseBtn_' + param);
        imgC1.classList.remove("C1");
        CCloseBtn.classList.add('d-none');
    }

    function Showtrainerdetails(param) {
        var imgC1 = document.getElementById('imgC1_' + param);
        var CCloseBtn = document.getElementById('CCloseBtn_' + param);

        imgC1.addEventListener('click', function () {
            imgC1.classList.add("C1");
            CCloseBtn.classList.remove('d-none');
        });

        CCloseBtn.addEventListener('click', function () {
            CClose(param);
        });
    }

    // Call Showtrainerdetails() for each trainer
    hftrainerdetailsJSON.forEach(function (elmt) {
        Showtrainerdetails(elmt.uniqueId);
    });
}