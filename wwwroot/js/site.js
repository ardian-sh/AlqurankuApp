$(document).ready(function () {
    $("#loadinggif").hide();

    window.addEventListener('scroll', function () {
        if (window.scrollY > 50) {
            $("#searchsurat").css("top", "")
            $("#searchsurat").css("top","57px")
        } else {
            $("#searchsurat").css("top", "")
            $("#searchsurat").css("top", "70px")
        }
    });

    $('[data-toggle="tooltip"]').tooltip();
});

$('#surat').select2({
    minimumInputLength: 1,
    language: {
        inputTooShort: function () {
            return 'Cari berdasarkan nama surat';
        },
        searching: function () {
            return "Sedang mencari...";
        },
        noResults: function (params) {
            return "Data tidak ditemukan";
        }
    },
    theme: 'bootstrap4',
    allowClear: true,
    placeholder: 'Cari berdasarkan nama surat',
    ajax: {
        type: "GET",
        dataType: 'json',
        url: "/select-box-surat",
        delay: 800,
        data: function (params) {
            return {
                search: params.term
            }
        },
        processResults: function (data) {
            if (data.isuccess) {
                return {
                    results: data.items
                };
            }
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    }
});

$("#surat").change(function () {

    var surat = $("#surat").val();
    if (surat != null || surat != "") {
        $("#loadinggif").show();
        window.location.href = "/search-surat?surat=" + surat + "";
    }
});

function Mylink() {
    $("#loadinggif").show();
}