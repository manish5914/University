function loginUser() {
    var email = $("#email").val();
    var password = $("#password").val();
    console.log(email, password);
    var user = {
        Email: email,
        Password: password
    }
    $.ajax({
        type: "POST",
        url: "/User/Login",
        data: user,
        dataType: "json",
        success: function (data) {
            if (data.url) {
                window.location = data.url;
            }
            else if (data.error) {
                alert(data.error);
            }
        }, error: function () {
            console.log("Error");
        }
    });
}
function buttonclick() {
    alert("button");
}