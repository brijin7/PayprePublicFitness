function loader(startOrEnd) {
    const loader = document.getElementById('divOverlay_loader');

    if (startOrEnd == "start") {
        loader.classList.remove('d-none');
    }
    else {
        loader.classList.add('d-none');
    }
}
loader('start');
