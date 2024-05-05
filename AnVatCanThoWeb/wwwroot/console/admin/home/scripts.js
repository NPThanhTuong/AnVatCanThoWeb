function handleSubmit(event) {
    event.preventDefault();
    const searchInput = $("#search-input");
    
    const query = searchInput.val();
    appendNewQueryValue(query);
    scrollDownResultBox();

    resolveQuery(query);
}

function appendNewQueryValue(search) {
    const newElm = $("<div></div>");
    newElm.addClass("alert alert-primary");
    newElm.append($(`<strong>Truy vấn: </strong>`));
    newElm.append(search);
    $("#result-box").append(newElm);
    makeFormDisabled(true);
}

function appendNewResult(result) {
    const newElm = $("<div></div>");
    newElm.addClass("alert alert-light");
    newElm.append($(`<strong>Kết quả: </strong>`));
    newElm.append(result);
    $("#result-box").append(newElm);
    makeFormDisabled(false);
}

function makeFormDisabled(disabled = true) {
    $("#search-button").attr("disabled", disabled);
    $("#search-input").attr("disabled", disabled);
}

function scrollDownResultBox() {
    $("#result-box-parent").animate({ scrollTop: $('#result-box-parent').prop("scrollHeight")}, 1000);
}

function turnDataIntoTable(data) {
    const table = $("<table class='dataTable display'></table>");

    const firstRow = data[0];
    const tableHead = $("<thead></thead>");
    const headerTr = $("<tr></tr>");
    Object.keys(firstRow).forEach((field) => {
        headerTr.append($(`<th>${field}</th>`));
    });
    tableHead.append(headerTr);
    table.append(tableHead);

    const tableBody = $("<tbody></tbody>");
    for(let row of data) {
        const bodyTr = $("<tr></tr>");
        Object.keys(row).forEach((field) => {
            if(typeof row[field] === "object") {
                bodyTr.append($(`<td>Không có dữ liệu</td>`));
            } else {
                bodyTr.append($(`<td>${row[field]}</td>`));
            }
        });
        tableBody.append(bodyTr);
    }
    table.append(tableBody);
    
    return table;
}

function resolveQuery(query) {
    $.ajax({
       url: "/api/admin/natural-query",
        type: "POST",
        dataType: "json",
        data: {
            naturalQuery: query
        },
        timeout: 10000,
        success: (data) => {
            if(data.length === 0) {
               appendNewResult("Không có dữ liệu");
               scrollDownResultBox();
               return;
            }
            
            const table = turnDataIntoTable(data);
            appendNewResult(table);
            scrollDownResultBox();
        },
        error: () => {
            appendNewResult("Không thể xử lý truy vấn này. Có thể do truy vấn quá nhanh, vui lòng chờ 5 giây và thử lại.");
            scrollDownResultBox();
        }
    });
}