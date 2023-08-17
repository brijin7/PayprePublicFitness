export function GalleryDescription() {
   
    const hfMealplanDescriptionJSON = JSON.parse(document.getElementById('hfMealplanDescription').value);
    const HomeMealplanDescriptionContent = document.getElementById('HomeMealplan-Description');
    const HomeMealplanDescription = document.getElementById('HomeMealplan-Description');

    if (hfMealplanDescriptionJSON.length == 0) {
        HomeMealplanDescription.classList.add('d-none');
        return;
    }
    HomeMealplanDescription.classList.remove('d-none');

    let hfMealplanDescriptionHtml =
        `<img class="imgFood" id="imageMealplanDescription" runat="server" clientidmode="Static" src=${hfMealplanDescriptionJSON[0].imageUrl} />
                    <div class="divMealDtls reveal fade-right">
                        <div class="text-center">
                            <label class="lblMealPlanHead">Meal Plan Description</label>
                        </div>
        ${ hfMealplanDescriptionJSON.map((elmt, _0, _1) => {
           
            return `
          <p id="Mealplan" runat="server">${elmt.description}</p>`;
        }).join('')
        } </div >`;
    HomeMealplanDescriptionContent.innerHTML = "";
    HomeMealplanDescriptionContent.insertAdjacentHTML("afterbegin", hfMealplanDescriptionHtml);

  
}