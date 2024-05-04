var exampleModal = document.getElementById('ratingModal')
exampleModal.addEventListener('show.bs.modal', function (event) {
    var button = event.relatedTarget
    // Extract info from data-bs-* attributes
    var recipient = button.getAttribute('data-bs-whatever')
    const dataArr = recipient.split('_')
    // If necessary, you could initiate an AJAX request here
    // and then do the updating in a callback.
    //
    // Update the modal's content.

    const productIdInput = document.querySelector('input[name="ProductId"]');
    const snackBarIdInput = document.querySelector('input[name="SnackBarId"]');
    const customerId = document.querySelector('input[name="CustomerId"]');

    productIdInput.value = dataArr[0];
    snackBarIdInput.value = dataArr[1];
    customerId.value = dataArr[2];
})
