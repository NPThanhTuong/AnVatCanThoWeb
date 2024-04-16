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

// Pusher
(function () {
    // Enable pusher logging - don't include this in production
    Pusher.logToConsole = true;

    var serverUrl = "/",
        comments = [],
        pusher = new Pusher('a6a81aa4ce5b84db0557', {
            cluster: 'ap1',
            encrypted: true
        }),
        // Subscribing to the 'flash-comments' Channel
        channel = pusher.subscribe('comments_channel');


})();

const commentFrm = $('#comment-form').on('submit', function (e) {
    e.preventDefault();
    console.log(e);
});
var pusher = new Pusher('a6a81aa4ce5b84db0557', {
    cluster: 'ap1'
});

var channel = pusher.subscribe('my-channel');
channel.bind('push_comment_event', function (data) {
    alert(JSON.stringify(data));
});
