$("#AboutMessages").text("Hello, " + sessionStorage.getItem("usernamet1") + "!");
$("#ContactMessages").text("Hello, " + sessionStorage.getItem("usernamet1") + "!");

var url = "/api/NewHome";
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

    $("#PasswordModal").css("display", "none");
    $("#ItemModal").css("display", "none");
    $("#Modal").css("display", "none");
});

$(".modal").click(function () {
    if (event.target.className == "modal") {
        $(event.target).hide();
    }
});

function logout() {

    var jqxhr = $.get("/api/NewLogin/LogOut/" + sessionStorage.getItem("username"), function () {
    }).done(function (data) {

        $("#AboutMessages").text(jqxhr.getResponseHeader("message"));
        $("#AboutMessages").css("background-color", "pink");
        $("#AboutMessages").css("font-size", "120%");
        $("#ContactMessages").text(jqxhr.getResponseHeader("message"));
        $("#ContactMessages").css("background-color", "pink");
        $("#ContactMessages").css("font-size", "120%");
        setTimeout(function () {
            window.location = jqxhr.getResponseHeader("location");
        }, 1000);
        return;
    });
}

function ShowPasswordChangeModel() {


    $(document.documentElement).animate({
        scrollTop: $("#NavBar").offset().top
    }, 2000);
    $("#Name").text("Hi, " + sessionStorage.getItem("usernamet1") + "!");
    $("#PasswordModal").css("display", "block");
    $("#ChangePasswordForm").submit(function SubmissionHandler(event) {
        event.preventDefault();
        let password = $("#Password").val();
        let confirmPassword = $("#ConfirmPassword").val();
        if (password !== confirmPassword) {
            $("#PasswordModelMessages").text("Passwords Don't Match");
            setTimeout(function () {
                $("#PasswordModelMessages").text("");
            }, 2000)
            return;
        }

        let oldPassword = $("#OldPassword").val();

        $.ajax({
            url: "/api/NewParent/PostChangePassword/" + sessionStorage.getItem("usernamet1") + "&" + oldPassword + "&" + password,
            method: "Post",
            success: function (data, status, xhr) {
                if (data === false) {
                    $("#PasswordModelMessages").text("Old Password Incorrect");
                    return;
                }
                $("#PasswordModelMessages").text("Password Changed Successfully");
                setTimeout(function () {
                    $("#PasswordModelMessages").text("");
                }, 2000)
            }
        });
    });
}

function ConfirmAccountDeletion() {

    var retVal = confirm("Are You Sure You Want To Delete Your Account? I will hate to see you leave.");
    if (retVal == true) {
        alert("Your Account Has Been Successfully Deleted! Your Cart Will Be Emptied and You Will Be Logged Out!");
        emptyCart();
        this.logout();
        $.ajax({
            url: "/api/NewParent/PostRemoveUser/" + sessionStorage.getItem("username") + "&" + "P4s9LnYKCquF4CVU",
            type: "POST",
            error: function (data) {
                alert("error");
            }
        });
    }
    else {
        alert("Your Account Has NOT Been Deleted");
    }
}

function emptyCart() {
    $.ajax({
        url: url + "/PostEmptyCart/" + sessionStorage.getItem("username"),
        method: "POST",
        error: function (data) {
            alert("error");
        }
    });
}