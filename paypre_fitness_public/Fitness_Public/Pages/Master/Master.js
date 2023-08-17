function HideNavLists() {
    const Nav_classes = document.getElementById('lnkbtnClasses');
    const Nav_subscription = document.getElementById('lnkbtnSubscription');
    const Nav_testimonials = document.getElementById('lnkbtnTestimonials');
    const Nav_ddlBranch = document.getElementById('ddlBranch_Master_Nav');
    const Nav_lblBranch = document.getElementById('Master_lblBranch');

    Nav_classes.classList.add('d-none');
    Nav_subscription.classList.add('d-none');
    Nav_testimonials.classList.add('d-none');
    Nav_ddlBranch.classList.add('d-none');
    Nav_lblBranch.classList.remove('d-none');
}
HideNavLists();