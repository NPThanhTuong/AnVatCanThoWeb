function HandleSortProduct(event) {
    const url = new URL(window.location.href);
    url.searchParams.set('sort', event.target.value);
    window.location = url;
}

function NextPage(totalPage) {
    const url = new URL(window.location.href);
    const curPage = parseInt(url.searchParams.get("page") || 1);
    
    if (curPage < totalPage) {
        url.searchParams.set('page', curPage + 1);
        window.location = url;
    }
}

function PrevPage(totalPage) {
    const url = new URL(window.location.href);
    const curPage = parseInt(url.searchParams.get("page") || 1);
    if (curPage > 1) {
        url.searchParams.set('page', curPage - 1);
        window.location = url;
    }
}

function SpecificPage(pageNum) {
    const url = new URL(window.location.href);
    url.searchParams.set('page', pageNum);
    window.location = url;
}

//Toggle heart icon
function AddToHeartList(idBtn) {
    const icon = document.querySelector('#' + idBtn + ' i');
    icon.classList.toggle('fas');
}
