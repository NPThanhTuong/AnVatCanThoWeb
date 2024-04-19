// Pusher
$(document).ready(function () {
    var pusher = new Pusher('a6a81aa4ce5b84db0557', {
        cluster: 'ap1',
        encrypted: true
    })
    var channel = pusher.subscribe('comments_channel');

    channel.bind('push_comment_event', function (data) {

        const commentList = $('#comment-list');
        const html = `
        <div class="d-flex align-items-start gap-3 mt-4">
            <img src="/images/User/${data.CustomerAvatar}" class="comment_avatar-user" alt="avatar user" />
            <div>
                <div class="d-flex align-items-center">
                    <h5 class="fs-6 fw-bold mb-0 me-2">${data.CustomerDisplayName}</h5>
                    <span class="fs-6 fw-light text-muted">${(new Date()).toLocaleString()}</span>
                </div>
                <p class="my-1">
                    ${data.comment.Content}
                </p>

                <button class="comment-like-btn text-dark-gray">
                    <i class="fas fa-thumbs-up"></i>
                    <span class="ms-1">Thích</span>
                </button>
            </div>
        </div>`;

        commentList.prepend(html);
    });
});

const commentFrm = $('#comment-form').on('submit', function (e) {
    e.preventDefault();
    const ProductId = parseInt($('input[name="ProductId"]').val());
    const SnackBarId = parseInt($('input[name="SnackBarId"]').val());
    const CustomerId = parseInt($('input[name="CustomerId"]').val());
    const CustomerDisplayName = $('input[name="CustomerDisplayName"]').val();
    const CustomerAvatar = $('input[name="CustomerAvatar"]').val();
    const Content = $('textarea[name="Content"]').val();

    $.ajax({
        url: "/product/comment",
        method: "POST",
        data: {
            ProductId,
            SnackBarId,
            CustomerId,
            Content,
            CustomerDisplayName,
            CustomerAvatar
        }
    }).done(function (data) {
        $('textarea[name="Content"]').val('');
    });
});

function renderComment(comments) {
    const commentList = $('#comment-list');

    const htmls = comments.map(function (comment) {
        return `
        <div class="d-flex align-items-start gap-3 mt-4">
            <img src="/images/User/${comment.avatar}" class="comment_avatar-user" alt="avatar user" />
            <div>
                <div class="d-flex align-items-center">
                    <h5 class="fs-6 fw-bold mb-0 me-2">${comment.displayName}</h5>
                    <span class="fs-6 fw-light text-muted">${(new Date(comment.createdAt)).toLocaleString()}</span>
                </div>
                <p class="my-1">
                    ${comment.content}
                </p>
            </div>
        </div>`;
    });

    commentList.html(htmls.join(" "));
}

$('#sortCommentSelect').on('change', HandleSortProduct);

function HandleSortProduct(event) {
    const url = new URL(window.location.href);
    url.searchParams.set('sort', event.target.value);
    url.searchParams.set('page', 1);
    url.hash = 'comment-list'
    window.location = url;
}

function NextPage(totalPage) {
    const url = new URL(window.location.href);
    const curPage = parseInt(url.searchParams.get("page") || 1);

    if (curPage < totalPage) {
        url.searchParams.set('page', curPage + 1);
        url.hash = 'comment-list'
        window.location = url;
    }
}

function PrevPage(totalPage) {
    const url = new URL(window.location.href);
    const curPage = parseInt(url.searchParams.get("page") || 1);
    if (curPage > 1) {
        url.searchParams.set('page', curPage - 1);
        url.hash = 'comment-list'
        window.location = url;
    }
}
