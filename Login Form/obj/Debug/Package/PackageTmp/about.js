function showCart(fromUndo) {

    if (!fromUndo) {
        $(document.documentElement).animate({
            scrollTop: $("#NavBar").offset().top
        }, 2000);
    } else {
        $(document.documentElement).animate({
            scrollTop: $("#NavBar").offset().top
        }, 0);
    }
    $("#Modal").css("display", "block");
    $(".modal-content > table > tbody > tr").remove();
    $.get(url + "/GetUserLocalCart/" + sessionStorage.getItem("username"))
        .done(function (data) {
            var i = 0;
            var cost = 0;
            // On success, 'data' contains a list of products.
            $.each(data, function (key, item) {
                $(`<tr id = ${i}>`).appendTo($('tbody'));
                //, { text: item.name + "\t" + item.quantity + "\t" + item.cost + "\t" + item.description }
                $('<td>', { text: key + 1 }).appendTo($(`#${i}`));
                $(`<td class=name${key}>`).appendTo($(`#${i}`));
                $(`.name${key}`).text(item.name);
                $('<td>', { text: item.quantity }).appendTo($(`#${i}`));
                $('<td>', { text: "$" + (item.cost / item.quantity) }).appendTo($(`#${i}`));
                $('<td>', { text: "$" + item.cost }).appendTo($(`#${i}`));
                cost += item.cost;
                $(`<td id = lastElement${i}>`).appendTo($(`#${i}`));
                $(`<button id=${i}>X</button>`).appendTo($(`#lastElement${i}`));
                $(`#lastElement${i} > button`).click(buttonHandlerRemove);
                i++;
            });
            $(`<tr id = ${i}>`).appendTo($('tbody'));
            $('<td>', { text: "" }).appendTo($(`#${i}`));
            $('<td>', { text: "" }).appendTo($(`#${i}`));
            $('<td>', { text: "" }).appendTo($(`#${i}`));
            $('<td>', { text: "" }).appendTo($(`#${i}`));
            $('<hr>').appendTo($(`#${i}`));
            i++;
            $(`<tr id = ${i}>`).appendTo($('tbody'));
            $('<td>', { text: "" }).appendTo($(`#${i}`));
            $('<td>', { text: "" }).appendTo($(`#${i}`));
            $('<td>', { text: "" }).appendTo($(`#${i}`));
            $('<td>', { text: "" }).appendTo($(`#${i}`));
            $('<td>', { text: "Grand Total: $" + cost }).appendTo($(`#${i}`));
        });
}

$(".close-modal").click(function () {

    $("#Modal").css("display", "none");
});

function logout() {

    var jqxhr = $.get("/api/TempLogin/LogOut/" + sessionStorage.getItem("username"), function () {
    }).done(function (data) {

        $("#AboutMessages").text(jqxhr.getResponseHeader("message"));
        $("#AboutMessages").css("background-color", "pink");
        $("#AboutMessages").css("font-size", "120%");
        setTimeout(function () {
            window.location = jqxhr.getResponseHeader("location");
        }, 1000);
        return;
    });
}