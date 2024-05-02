function HandleChangeQuantity(event, unitPrice, selectorPrice, productId) {
    const quantity = parseInt(event.target.value);
    let total = 0;
    $.ajax({
        type: 'POST',
        url: '/Product/AddToCart',
        data: { id: productId },
        success: function (data) {
            $('#' + selectorPrice).html(
                (quantity * unitPrice).toLocaleString('it-IT', {
                    style: 'currency',
                    currency: 'VND'
                }));

            $('.totalPriceItem').map(function () {
                total += (parseInt($(this).text()) * 1000);
            });

            $('.totalPrice').each(function () {
                $(this).html(total.toLocaleString('it-IT', {
                    style: 'currency',
                    currency: 'VND'
                }))
            });
        },
        error: function (res) {
            console.log(res)
        }
    });
    

}
