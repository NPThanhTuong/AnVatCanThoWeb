function HandleSortProduct(event) {
    const url = new URL(window.location.href);
    url.searchParams.set('sort', event.target.value);
    window.location = url;
}

//Toggle heart icon
function AddToHeartList(idBtn) {
    const icon = document.querySelector('#' + idBtn + ' i');
    icon.classList.toggle('fas');
}
