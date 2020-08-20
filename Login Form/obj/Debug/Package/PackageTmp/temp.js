
const url = "/api/Temp";

function onload() {

    var user = sessionStorage.getItem("username");
    if (user == null)
        window.location.replace("/index.html");
    $.get("/api/TempLogin/CanAccess/" + user, function (data) {
    }).done(function (data) {
        if (data === false) {
            window.location.replace("/index.html");
            return;
        }
    });

    $("#Messages").text("Hello, " + sessionStorage.getItem("username") + "!");
    $.ajax({
        url: url + "/GetAll",
        type: "GET",
        success: function (data, status, xhr) {

            var rowIdNumber = 0;
            $.each(data, function (key, item) {

                console.log(key);
                if (key % 4 === 0) {
                    rowIdNumber = key;
                    $(`<div id=Div${key} class="rowDiv">`).appendTo($("#Store"));
                   // $(`#Div${key}`).css("display", "inline-flex");
                   // $(`#Div${key}`).css("height", "auto");
                   // $(`#Div${key}`).css("overflow", "overflow-y");
                   // $(`#Div${key}`).css("width", "auto");
                    $(`#Div${key}`).addClass("row");
                    $(`#Div${key}`).css("margin-left", "3%");
                    //$(`#Div${key}`).css("margin-bottom", "3%");
                }
                $(`<div id=IndivDiv${key} class="indivDiv">`).appendTo($(`#Div${rowIdNumber}`));
                $(`#IndivDiv${key}`).css("justify-content", "center");
                //$(`#IndivDiv${key}`).css("display", "block");
                $(`#IndivDiv${key}`).css("padding", "0");
                $(`#IndivDiv${key}`).css("width", "100%");
                $(`#IndivDiv${key}`).addClass("col-sm");
                $(`#IndivDiv${key}`).css("border", "2px dashed red");
                $(`#IndivDiv${key}`).css("text-align", "center");
                $(`#IndivDiv${key}`).css("background-color", "rgba(0, 255, 255, 0.75)");
                $(`<h3 id=Number${key}>`).appendTo($(`#IndivDiv${key}`));
                $(`#Number${key}`).text("" + (key+1) + "");
                $(`#Number${key}`).css("float", "left");
                $(`<p id=MessageBox${key}>`).appendTo($(`#IndivDiv${key}`));
                $(`#MessageBox${key}`).css("color", "black");
                $(`#MessageBox${key}`).css("background-color", "white");
                $(`<input type=number min=1 placeholder="Quant" id=InputField${key}>`).appendTo(`#IndivDiv${key}`);
                $(`#InputField${key}`).css("float", "right");
                $(`#InputField${key}`).css("width", "19%");
                $(`#InputField${key}`).keydown(enterKeyHandler);
                $(`<h3 id=ItemName${key}>`).appendTo($(`#IndivDiv${key}`));
                $(`#ItemName${key}`).text(item.name);
                $(`#ItemName${key}`).css("clear", "both");
                $(`<button id=AddToCartButton${key}>`).appendTo($(`#IndivDiv${key}`));
                $(`#AddToCartButton${key}`).text("Add To Cart");
                $(`#AddToCartButton${key}`).click(buttonHandler);
                $(`#AddToCartButton${key}`).css("float","right");
                $(`<img id=ItemImage${key}>`).appendTo($(`#IndivDiv${key}`));
                $(`#ItemImage${key}`).attr("src", item.image);
                $(`#ItemImage${key}`).css("transform", "scale(.5)");
                $(`<h3 id=ItemDescrip${key}>`).appendTo($(`#IndivDiv${key}`));
                $(`#ItemDescrip${key}`).text(item.description);
                $(`<h3 id=ItemPrice${key}>`).appendTo($(`#IndivDiv${key}`));
                $(`#ItemPrice${key}`).text("$" + item.cost);
                $(`<h4 id=ItemQuant${key}>`).appendTo($(`#IndivDiv${key}`));
                $(`#ItemQuant${key}`).text("Quantity Left: " + (item.quantity + item.showQuantity));
                $(`#ItemQuant${key}`).css("float", "right");
                $(`#ItemQuant${key}`).css("margin-right", "2%");
                $(`#ItemQuant${key}`).css("clear", "both");
               // if (key % 3 === 0) {
                   // $(`<br>`).appendTo($("#Store"));
               // }
            });    
        },
        error: function (jqXHR, textStatus, errorMessage) {
            alert("api failed");
        }
    });

    setInterval(function () {

        $.ajax({
            url: url + "/GetQuantities",
            type: "GET",
            success: function (data, status, xhr) {

                $.each(data, function (key, item) {
                    $(`#ItemQuant${key}`).text("Quantity Left: " + (item[0] + item[1]));
                });
            },
            error: function (jqXHR, textStatus, errorMessage) {
                console.log("api error");
            }
        });
    },1000);
}

function enterKeyHandler(e) {

    if (e.keyCode === 13) {  //checks whether the pressed key is "Enter"
        var id = (this.id + "").substring("InputField".length);
        var name = $(`#ItemName${id}`).text();
        var quantity = $(`#InputField${id}`).val();
        if (quantity === "") {
            return;
        }
        $.ajax({

            url: url + "/PostAddItemToUserCart/" + sessionStorage.getItem("username") + "&" + name + "&" + quantity,
            type: "POST",
            success: function (data, status, xhr) {
                $(`#MessageBox${id}`).text(data);
            },
            error: function (data, status, xhr) {
                alert("api failed");
            }
        });
        $(`#InputField${id}`).val("");
    }
}

function undo() {

    $.ajax({
        url: url + "/PostUndoDelete/" + sessionStorage.getItem("username"),
        method: "POST",
        success: function (data, status, xhr) {
            $(`#ModalMessage`).text(data);
            setTimeout(function () {
                $("#ModalMessage").text("");
            }, 1000);
            showCart(true);
        },
        error: function (jqXHR, status, message) {
            console.log("failed to undo, api issue");
        }
    });
}

function buttonHandler() {
    var id = (this.id + "").substring("AddToCartButton".length);
    var name = $(`#ItemName${id}`).text();
    var quantity = $(`#InputField${id}`).val();
    if (quantity === "") {
        return;
    }
    $.ajax({

        url: url + "/PostAddItemToUserCart/" + sessionStorage.getItem("username") + "&" + name + "&" + quantity,
        type: "POST",
        success: function (data, status, xhr) {
            $(`#MessageBox${id}`).text(data);
        },
        error: function (data, status, xhr) {
            alert("api failed");
        }
    });
    $(`#InputField${id}`).val("");
}

function buttonHandlerRemove() {

    $.ajax({
        url: url + "/PostRemoveItemFromCart/" + sessionStorage.getItem("username") + "&" + $(`table > tbody > tr#${this.id} > td.name${this.id}`).text(),
        method: "POST",
        success: function (data, status, xhr) {
            $("#ModalMessage").text(data);
            showCart(true);
            setTimeout(function () {
                $("#ModalMessage").text("");
            }, 3000);
        },
        error: function (data, status, xhr) {
            $("#ModalMessage").text("API FAILED");
        }
    });
}

function showCart(fromUndo) {

    if (!fromUndo) {
        $(document.documentElement).animate({
            scrollTop: $("#NavBar").offset().top
        }, 2000);
    } else{
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
            $('<td>', { text: ""}).appendTo($(`#${i}`));
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

        $("#Messages").text(jqxhr.getResponseHeader("message"));
        $("#Messages").css("background-color", "pink");
        $("#Messages").css("font-size", "120%");
        setTimeout(function () {
            window.location = jqxhr.getResponseHeader("location");
        }, 1000);
        return;
    });
}