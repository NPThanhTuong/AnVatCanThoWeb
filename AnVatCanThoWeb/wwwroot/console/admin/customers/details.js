$(document).ready(() => {
    if(errorMessage) {
        Swal.fire({
            icon: "error",
            title: "Lỗi",
            text: errorMessage,
        });
    }
});