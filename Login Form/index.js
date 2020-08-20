
const uri1 = "/api/NewLogin/";
const uri2 = "/api/Login/";

function initialize() {

    var user = sessionStorage.getItem("username");
    if (user == null)
        return;
    var r1 = false;
    var r2 = false;

    $.get(uri1 + "Ready/" + user)
        .done(function (data) {

            if (data === "yes") {
                r1 = true;
            }
        });
    $.get(uri2 + "Ready/" + user)
        .done(function (data) {

            if (data === "yes") {
                r2 = true;
            }
        });
    setTimeout(function () {

        if (!r1 && !r2) {
            window.location = "about:blank";
        }
    }, 2000);
}

$("#form").submit(function (event) {

    event.preventDefault();
    var user = $("#username").val();
    var pass = $("#password").val();

    if (user === "" && pass === "") {
        $("#message1").text("Fill In Both Fields!");
        $("#message1").css("background-color", "yellow");
        setTimeout(function () {

            $("#message1").text("");
            $("#message1").css("display", "hidden");
        }, 1250)
    }

    var jqxhr1 = $.post(uri1 + "Validate_" + user + "&" + pass)
        .done(function (data) {

            $("#message1").text("Tenant 1: " + jqxhr1.getResponseHeader("message"));
            if (jqxhr1.getResponseHeader("message") === "Success") {
                $("#message1").css("background-color", "green");
                sessionStorage.setItem("usernamet1", jqxhr1.getResponseHeader("username"))
            } else if (jqxhr1.getResponseHeader("message") === "Failure") {
                $("#message1").css("background-color", "yellow");
                setTimeout(function () {

                    $("#message1").text("");
                    $("#message1").css("display", "hidden");
                }, 1250);
            } else if (jqxhr1.getResponseHeader("message") === "LockedOut") {
                setTimeout(function () {

                    $.get(uri1 + "Reset/" + sessionStorage.getItem("username"))
                        .done(function (data) { });
                }, 1250);
                window.location = "about:blank";
            }
        });

    var jqxhr2 = $.post(uri2 + "Validate_" + user + "&" + pass)
        .done(function (data) {
            $("#message2").text("Tenant 2: " + jqxhr2.getResponseHeader("message"));
            if (jqxhr2.getResponseHeader("message") === "Success") {
                $("#message2").css("background-color", "green");
                sessionStorage.setItem("usernamet2", jqxhr2.getResponseHeader("username"))
            } else if (jqxhr2.getResponseHeader("message") === "Failure") {
                $("#message2").css("background-color", "yellow");
                setTimeout(function () {
                    $("#message2").text("");
                    $("#message2").css("display", "hidden");
                }, 1250);
            } else if (jqxhr2.getResponseHeader("message") === "LockedOut") {
                setTimeout(function () {

                    $.get(uri2 + "Reset/" + sessionStorage.getItem("username"))
                        .done(function (data) { });
                }, 1250);
                window.location = "about:blank";
            }
        });

    setTimeout(function () {

        if (jqxhr1.getResponseHeader("location") != null) {
            setTimeout(function () {

                if (jqxhr2.getResponseHeader("location") != null) {
                    sessionStorage.setItem("tenant", "both");
                } else {
                    sessionStorage.setItem("tenant", "t1");
                }
                window.location = jqxhr1.getResponseHeader("location");
            }, 1250);
        } else if (jqxhr2.getResponseHeader("location") != null) {
            setTimeout(function () {
                sessionStorage.setItem("tenant", "t2")
                window.location = jqxhr2.getResponseHeader("location");
            }, 1250);
        }
    }, 2000);
});
