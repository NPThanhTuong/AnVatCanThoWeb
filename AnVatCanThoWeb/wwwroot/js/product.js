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



// Subscribing to the 'flash-comments' Channel
$(document).ready(function () {
    
    // Enable pusher logging - don't include this in production
    Pusher.logToConsole = true;

    var pusher = new Pusher('a6a81aa4ce5b84db0557', {
        cluster: 'ap1',
        encrypted: true
    })
    var channel = pusher.subscribe('comments_channel');

    channel.bind('push_comment_event', function (data) {
        const commentList = $('#comment-list');
        const html = `
        <div class="d-flex align-items-start gap-3 mt-4">
            <img src="/images/UserIndex/Can-Tho-bridge.jpg" class="comment_avatar-user" alt="avatar user" />
            <div>
                <div class="d-flex align-items-center">
                    <h5 class="fs-6 fw-bold mb-0 me-2">${data.CustomerDisplayName}</h5>
                    <span class="fs-6 fw-light text-muted">5 tháng trước</span>
                </div>
                <p class="my-1">
                    ${data.comment.Content}
                </p>
                @if (userId != null) {
                    <button class="comment-like-btn text-dark-gray">
                        <i class="fas fa-thumbs-up"></i>
                        <span class="ms-1">Thích</span>
                    </button>
                }
            </div>
        </div>`;
        console.log({ data });
        console.log({ commentList });
        commentList.prepend(html);

        
    });
});

const commentFrm = $('#comment-form').on('submit', function (e) {
    e.preventDefault();
    const ProductId = parseInt($('input[name="ProductId"]').val());
    const SnackBarId = parseInt($('input[name="SnackBarId"]').val());
    const CustomerId = parseInt($('input[name="CustomerId"]').val());
    const CustomerDisplayName = $('input[name="CustomerDisplayName"]').val();
    const Content = $('textarea[name="Content"]').val();

    $.ajax({
        url: "/product/comment",
        method: "POST",
        data: {
            ProductId,
            SnackBarId,
            CustomerId,
            Content,
            CustomerDisplayName
        }
    }).done(function (data) {
        $('textarea[name="Content"]').val('');

    });
});


//var pusher = new Pusher('a6a81aa4ce5b84db0557', {
//    cluster: 'ap1'
//});

//var channel = pusher.subscribe('my-channel');
//channel.bind('push_comment_event', function (data) {
//    alert(JSON.stringify(data));
//});
