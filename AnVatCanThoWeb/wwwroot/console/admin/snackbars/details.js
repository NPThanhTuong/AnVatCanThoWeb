function deleteProduct(event, form) {
    event.preventDefault();
    Swal.fire({
        title: "Bạn chắc chắn muốn xoá ?",
        showDenyButton: true,
        confirmButtonText: "Xoá",
        denyButtonText: "Huỷ"
    }).then((result) => {
        if (result.isConfirmed) {
            form.submit();
        }
    });
}

$(document).ready(() => {
    if(errorMessage) {
        Swal.fire({
            icon: "error",
            title: "Lỗi",
            text: errorMessage,
        });
    }
});