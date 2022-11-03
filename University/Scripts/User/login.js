function loginUser() {
    if (validateEmail() && validatePassword()) {
        var email = $("#email").val();
        var password = $("#password").val();
        var user = {
            Email: email,
            Password: password
        }
        loginUserAjax(user).then((response) => {
            if (response.url) {
                window.location = response.url;
            }
            else if (response.error) {
                toastr.error(response.error);
            }
        }).catch((reject) => {
            toastr.error(reject);
        })
    }
    else {
        toastr.error("Incorrect inputs")
    }
}
function loginUserAjax(user) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "POST",
            url: "/User/Login",
            data: user,
            dataType: "json",
            success: function (data) {
                resolve(data);
               
            }, error: function () {
                reject("No User found");
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
