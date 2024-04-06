$(document).ready(() => {
    const select = $("#product-types");
    const tableBody = $("#table-body")
    select.on("change", function() {
       let id = $(this).val();
       if(id === "-1") {
           return;
       }
       
       id = parseInt(id, 10);
       $.ajax({
           url: `/Admin/api/product-types/${id}/product-categories`,
           type: "GET",
           dataType: "json",
           success: (data) => {
               for(let row of data) {
                   tableBody.html("");
                   tableBody.append(createNewRow(row));
               }
           }
       })
    });
});

function createNewRow(data) {
    const tr = $("<tr></tr>");
    const id = $("<td></td>").append(data.id);
    const name = $("<td></td>").append(data.name);
    const des = $("<td></td>").append(data.description);
    const actions = $("<td></td>");
    
    tr.append(id, name, des, actions);
    
    return tr;
}