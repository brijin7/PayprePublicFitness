export function onBranchChange() {
    const ddlBranch = document.getElementById('ddlBranch_Master_Nav');
    const btnOnBranchChange = document.getElementById('btn_Home_BranchChange');
    
    ddlBranch.onchange = function () {
        btnOnBranchChange.click();
    };
}