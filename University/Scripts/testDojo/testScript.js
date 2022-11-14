require(["dojo/dom", "dojo/on", "dojo/ready", "dojo/request"],
    function (dom, on, ready, request) {
        var form = dom.byId("form");
        var emailElem = dom.byId("email");
        var passwordElem = dom.byId("password");
        var loginBtn = dom.byId("loginbtn");

        on(loginBtn, "click", function () {
            if (form) {
                var email = emailElem.value;
                var password = passwordElem.value;
                if (!(validateEmail() && validatePassword())) {
                    toastr.error("InValid input");
                }
                request.post("/User/Login", {
                    data: {
                        Email: email,
                        Password: password
                    }, handleAs:"json"
                }).then(
                    function (response) {
                        if (response.url) {
                            window.location = response.url;
                        }
                        else if (response.error) {
                            toastr.error(response.error);
                        }
                    },
                    function(error) {
                        toastr.error(error);
                    }
                );
            }
        })

    });

function validateEmail() {
    const emailPattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    var emailRegExp = new RegExp(emailPattern)
    var email = $("#email");
    if (!emailRegExp.test(email.val())) {
        email.addClass("invalid");
        toastr.info("Email is invalid");
        return false;
    }
    else {
        if (email.hasClass("invalid")) {
            email.removeClass("invalid");
        }
        return true;
    }
}
function validatePassword() {
    password = $("#password");
    if (password.val().length < 8) {
        password.addClass("invalid");
        toastr.info("Password is too Short");
        return false;
    }
    if (password.hasClass("invalid")) {
        password.removeClass("invalid");
    }
    return true;
}
