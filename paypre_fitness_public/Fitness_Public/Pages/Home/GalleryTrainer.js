export function TrainerDescription() {
   
    const hfTrainerDescriptionJSON = JSON.parse(document.getElementById('hfTrainerDescription').value);
    const HomeTrainerDescriptionJSONContent = document.getElementById('HomeTrainer-Description');
    const HomeTrainerDescription = document.getElementById('HomeTrainer-Description');

    if (hfTrainerDescriptionJSON.length == 0) {
        HomeTrainerDescription.classList.add('d-none');
        return;
    }
    HomeTrainerDescription.classList.remove('d-none');

    let hfTrainerDescriptionHtml =
        `<div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                <div class="divTrainerDtls reveal fade-left">
                    <label class="lblTrainerDec">Trainers Description</label>
        ${hfTrainerDescriptionJSON.map((elmt, _0, _1) => {
           
            return `
          <p>${elmt.description}</p>`;
        }).join('')
        }   </div>
            </div>
            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                <div class="divTrainerImg reveal fade-right">
                    <img class="imgTrainers"  src=${hfTrainerDescriptionJSON[0].imageUrl} />
                </div>
            </div>`;
    HomeTrainerDescriptionJSONContent.innerHTML = "";
    HomeTrainerDescriptionJSONContent.insertAdjacentHTML("afterbegin", hfTrainerDescriptionHtml);


}