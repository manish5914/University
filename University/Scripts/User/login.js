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
        toast.error("Incorrect inputs")
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
    email = $("#email").val();
    if (!emailRegExp.test(email)) {
        toastr.info("Email is invalid");
        return false;
    }
    else { return true; }
}
function validatePassword() {
    password = $("#password").val();
    if (password.length < 8) {
        toastr.info("Password is too Short");
        return false;
    }
    return true;
}