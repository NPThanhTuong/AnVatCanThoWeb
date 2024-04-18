$(document).ready(() => {
    const table = new DataTable('#my-datatable', {
        responsive: true,
        paging: false,
        searching: false,
        ordering:  false,
    });

    const urlParams = new URLSearchParams(window.location.search);
    
    const districtName = $("#districtName");
    districtName.on("change", function(event) {
        const value = districtName.val();
        loadWards(value, true);
    });
    $.ajax({
        url: `/Admin/api/districts`,
        type: "GET",
        dataType: "json",
        success: (data) => {
            const currentDistrictName = urlParams.get("currentDistrictName") || urlParams.get("districtName");
            data.forEach(elm => {
                districtName.append($(`<option value="${elm.name}">${elm.name}</option>`));
            });
            districtName.val(currentDistrictName);
            loadWards(currentDistrictName);
        },
        error: () => {
            districtName.prop("disabled", true);
        }
    });
    
    function loadWards(district, reselectDistrict = false) {
        const wardName = $("#wardName");
        if(!district) {
            wardName.prop("disabled", true);
            wardName.find("option:gt(0)").remove();
            return;
        }
        
        $.ajax({
            url: `/Admin/api/wards?districtName=${district}`,
            type: "GET",
            dataType: "json",
            success: (data) => {
                const currentWardName = !reselectDistrict ? (urlParams.get("currentWardName") || urlParams.get("wardName")) : "";
                wardName.prop("disabled", false);
                wardName.find("option:gt(0)").remove();
                data.forEach(elm => wardName.append(
                    $(`<option value="${elm.name}">${elm.name}</option>`)
                ));
                wardName.val(currentWardName);
            },
            error: () => {
                wardName.prop("disabled", true);
            }
        });
    }
});