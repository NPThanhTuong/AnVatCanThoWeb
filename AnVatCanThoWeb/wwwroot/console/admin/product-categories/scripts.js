$(document).ready(() => {
    let table = new DataTable('#my-datatable', {
        responsive: true,
        paging: false,
        searching: false,
        ordering:  false,
        data: [],
        columns: [
            { data: 'id' },
            { data: 'name' },
            { data: 'description' },
            {
                data: "id",
                render: function (data, type, row) {
                    return `
                    <div class="flex-row d-flex gap-2">
                        <a href="/Admin/ProductCategories/Edit/${data}" class="btn btn-info"><i class="fa-solid fa-pen-to-square"></i></a>
                        <form onsubmit="deleteCategory(event, this)" method="POST" action="/Admin/ProductCategories/Delete/${data}">
                            <button class="btn btn-danger"><i class="fa-solid fa-trash"></i></button>
                        </form>
                    </div>`;
                },
            }
        ]
    });
    reloadData();
    $("#product-types").on("change", reloadData);
    
    if(errorMessage) {
        Swal.fire({
            icon: "error",
            title: "Lỗi",
            text: errorMessage,
        });
    }
    
    function reloadData() {
        let id = $("#product-types").val();
        if(id === "-1") {
            return;
        }
        id = parseInt(id, 10);
        $.ajax({
            url: `/Admin/api/product-types/${id}/product-categories`,
            type: "GET",
            dataType: "json",
            success: (data) => {
                table.clear();
                table.rows.add(data);
                table.draw();
            }
        })
    }
});

function deleteCategory(event, form) {
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