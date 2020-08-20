
const uri = "/api/TempLogin/";

function initialize() {

    var user = sessionStorage.getItem("username");
    if (user == null)
        return;
    $.get(uri + "Ready/" + user)
        .done(function (data) {

            if (data === "yes") {
            } else if (data === "no") {
                window.location = "about:blank";
            }
        });
}

$("#form").submit(function (event) {

    event.preventDefault();
    var user = $("#username").val();
    var pass = $("#password").val();

    if (user === "" && pass === "") {
        $("#message").text("Fill In Both Fields!");
        $("#message").css("background-color", "yellow");
        setTimeout(function () {

            $("#message").text("");
            $("#message").css("display", "hidden");
        }, 1250)
    }

    var jqxhr = $.post(uri + "Validate_" + user + "&" + pass)
        .done(function (data) {

            $("#message").text(jqxhr.getResponseHeader("message"));
            if (jqxhr.getResponseHeader("message") === "Success") {
                $("#message").css("background-color", "green");
                sessionStorage.setItem("username", jqxhr.getResponseHeader("username"))
            } else if (jqxhr.getResponseHeader("message") === "Failure") {
                $("#message").css("background-color", "yellow");
                setTimeout(function () {

                    $("#message").text("");
                    $("#message").css("display", "hidden");
                }, 1250);
                return;
            } else if (jqxhr.getResponseHeader("message") === "LockedOut") {
                setTimeout(function () {

                    $.get(uri + "Reset/" + sessionStorage.getItem("username"))
                        .done(function (data) { });
                }, 1250);
                window.location = "about:blank";
            }

            setTimeout(function () {
                window.location = jqxhr.getResponseHeader("location");
            }, 1250);
        });
});
