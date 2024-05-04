$(document).ready(function () {
    //Lấy quận huyện
    $.getJSON('https://esgoo.net/api-tinhthanh/2/' + 92 + '.htm', function (data_quan) {
        if (data_quan.error == 0) {
            $("#districtInput").html('<option value="0">Quận Huyện</option>');
            $("#wardInput").html('<option value="0">Phường Xã</option>');
            $.each(data_quan.data, function (key_quan, val_quan) {
                $("#districtInput").append('<option value="' + val_quan.id + '">' + val_quan.name + '</option>');
            });
            //Lấy phường xã  
            $("#districtInput").on('change', function (e) {
                var idquan = $(this).val();
               
                var quan = data_quan.data.find(function (el) {
                    return el.id === idquan
                });
                $('input[name="DistrictName"]').val(quan.name);

                $.getJSON('https://esgoo.net/api-tinhthanh/3/' + idquan + '.htm', function (data_phuong) {
                    if (data_phuong.error == 0) {
                        $("#wardInput").html('<option value="0">Phường Xã</option>');
                        $.each(data_phuong.data, function (key_phuong, val_phuong) {
                            $("#wardInput").append('<option value="' + val_phuong.id + '">' + val_phuong.name + '</option>');
                        });

                        $("#wardInput").on('change', function (e) {
                            var idphuong = $(this).val();
                            var phuong = data_phuong.data.find(function (el) {
                                return el.id === idphuong
                            });

                            $('input[name="WardName"]').val(phuong.name);
                        });
                    }
                });
            });
        }
    });


    function HandleSubmitAddAddress(event) {
        $.ajax({
            url: '/Address/AddAddress',
            method: "POST",
            data: {
                DistrictName: $('#districtInput').text(),
                WardName: $('#wardInput').text(),
            }
        })
    }
});