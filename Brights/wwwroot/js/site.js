// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$.xhrPool = [];

$.xhrPool.abortAll = function () {
    $(this).each(function (idx, jqXHR) {
        jqXHR.abort();
    });
    $.xhrPool.length = 0;
};

$("button").click(function () {
    $.xhrPool.abortAll();
    $("#myTable > tbody").html("");
    var urls = document.getElementById("urlArray").value;
    if ($.trim($('#urlArray').val())) {
        $("#grid").attr("hidden", false);
    }

    var urlArray = urls.split("\n");
    for (let item of urlArray) {
        if($.trim(item) === ""){
            continue;
        }
        $("#loading").show();

        $.ajax({
            type: 'POST',
            url: 'Home/Check',
            contentType: 'application/json',
            data: JSON.stringify({ Url: item }),

            beforeSend: function (jqXHR) {
                $.xhrPool.push(jqXHR);
            },
            success: function (data) {

                $("#myTable").find('tbody')
                    .append($('<tr>')
                        .append($('<td>')
                            .append($('<a>')
                                .attr("href", data.url)
                                .text(data.url)
                            )
                        )
                        .append($('<td>')
                            .text(data.statusCode)
                        )
                        .append($('<td>')
                            .text(data.title)
                        )
                    );

            },
            error: function (ex) {
                console.log(ex);
            }
        });
    }
});

$(document).ajaxStop(function () {
    $("#loading").hide();
});
