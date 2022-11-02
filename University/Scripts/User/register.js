﻿function registerUser() {
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
    email = $("#email").val();
    if (!emailRegExp.test(email)) {
        toastr.info("Email is invalid");
        return false;
    }
    else { return true; }
}
function validatePassword() {
    password = $("#password").val();
    confirmPassword = $("#confirmPassword").val();

    if (password.length < 8) {
        toastr.info("Password is too Short");
        return false;
    }
    if (password != confirmPassword) {
        toastr.info("Passwords does not match");
        return false;   
    }
    return true;
}