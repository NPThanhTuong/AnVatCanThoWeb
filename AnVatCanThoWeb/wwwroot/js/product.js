function HandleSortProduct(event) {
    const url = new URL(window.location.href);
    url.searchParams.set('sort', event.target.value);
    url.searchParams.set('page', 1);
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
    console.log(pageNum);
    const url = new URL(window.location.href);
    url.searchParams.set('page', pageNum);
    window.location = url;
}


function AddToCart(productId, isDetailPage = false, returnUrl = "%2FProduct") {
    let quantity = 1;
    if (isDetailPage) {
        quantity = $('input[name="quantity"]').val();
    }

    $.ajax({
        type: 'POST',
        url: '/Product/AddToCart',
        data: { id: productId, quantity: parseInt(quantity) },
        success: function (data) {
            $("#cart-amount").text(data.itemAmount);
            toastr.success("Thêm sản phẩm vào giỏ hàng thành công");
        },
        error: function (res) {
            if (res.status === 401) {
                window.location = `/Auth/Login?ReturnUrl=${returnUrl}`;
            }
        }
    });
}

function HandleOrderProduct(productId, returnUrl) {
    const quantity = $('input[name="quantity"]').val();

    $.ajax({
        type: 'POST',
        url: '/Product/AddToCart',
        data: { id: productId, quantity: parseInt(quantity) },
        success: function (data) {
            $("#cart-amount").text(data.itemAmount);
            toastr.success("Thêm sản phẩm vào giỏ hàng thành công");
            window.location = '/Product/Order';
        },
        error: function (res) {
            if (res.status === 401) {
                window.location = `/Auth/Login?ReturnUrl=${returnUrl}`;
            }
        }
    });
    
}
