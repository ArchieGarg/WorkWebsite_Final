
$("#messages").css("font-size", "30px");
$("#form").submit(function (event) {
    event.preventDefault();
    var user = $("#user").val();
    var pass = $("#pass").val();
    var confirmPass = $("#confirmPass").val();

    if (pass !== confirmPass) {

        $("#messages").text("Passwords Don't Match!");
        $("#messages").css("background-color", "yellow");
        setTimeout(function () {

            $("#messages").text("");
            $("#messages").css("Display", "hidden");
        }, 1500);
        return;
    }

    var dropdownVal = $("#TenantDropdown option:selected").text();

    $("#messages").css("Display", "visible");
    $("#messages").css("background-color", "blue");
    if (dropdownVal === "Legacy") {
        console.log("Making Legacy Account")
        MakeT2RequestAndHandle(user, pass);
    } else if (dropdownVal === "Modern") {
        console.log("Making Modern Account")
        MakeT1RequestAndHandle(user, pass);
    } else if (dropdownVal === "Legacy and Modern") {

        console.log("Making Both Accounts")
        MakeT1RequestAndHandle(user, pass);
        MakeT2RequestAndHandle(user, pass);
        setTimeout(function () {
            $("#messages").text("Success Acount is Created On Both Tenents");
        }, 1250);
    }
    setTimeout(function () {
        window.location.replace("\index.html");
    }, 2000);
});

function MakeT1RequestAndHandle(user, pass) {

    $.ajax({

        url: "/api/NewSignUp/AddToList_" + user + "&" + pass,
        type: "POST",
        success: function (data) {
            $("#messages").text("Success Acount is Created On Tenent1 NOT on Tenent2");
        },
        error: function (data) {
            alert("api error");
        }
    });
}

function MakeT2RequestAndHandle(user, pass) {

    $.ajax({

        url: "/api/SignUp/AddToList_" + user + "&" + pass,
        type: "POST",
        success: function (data) {
            $("#messages").text("Success Acount is Created On Tenent2 NOT on Tenent1");
        },
        error: function (data) {
            alert("api error");
        }
    });
}