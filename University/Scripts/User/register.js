function registerUser() {
    if (validateEmail() && validatePassword()) {
        var email = $("#email").val();
        var password = $("#password").val();
        var user = {
            ID: 0,
            Email: email,
            Password: password
        };
        registerUserAjax(user).then((response) => {
            if (response.url) {
                toastr.success("Registered User, Redirecting");
                window.location = response.url;
            }
            else {
                toastr.error("User not registered");
            }
        }).catch((reject) => {
            toastr.error(reject);
        })
    } else {
        toastr.error("Incorrect inputs");
    }

}
function registerUserAjax(user) {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "/User/RegisterUser/",
                data: user,
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        resolve(data);
                    }
                }, error: function (error) {

                    reject(error)
                }
            });
        })
}

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
    confirmPassword = $("#confirmPassword");

    if (password.val().length < 8) {
        password.addClass("invalid");
        toastr.info("Password is too Short");
        return false;
    }
    if (password.val() != confirmPassword.val()) {
        confirmPassword.addClass("invalid");
        toastr.info("Passwords does not match");
        return false;   
    }
    if (password.hasClass("invalid")) {
        password.removeClass("invalid");
    }
    if (confirmPassword.hasClass("invalid")) {
        confirmPassword.removeClass("invalid");
    }
    return true;
}