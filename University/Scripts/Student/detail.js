function loadUser() {
    loadUserAjax().then((response) => {
        $("#name").text(response.FirstName + " " + response.LastName);
        $("#nid").text(response.NID);
        $("#email").text(response.Email);
        $("#status").text(response.Status);
        $("#userid").text(response.UserId);
        $("#phonenumber").text(response.PhoneNumber);
        $("#guardianname").text(response.GuardianName);

    }).catch((reject) => {
        toastr.error(reject);
    })
}
function loadUserAjax() {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "GET",
            url: "/Student/GetCurrentStudent",
            datatype: "json",
            success: function (data) {
                if (data.error) {
                    reject(data.error);
                }
                resolve(data);
            },
            error: function (error) {
                reject(error);
            }
        });
    });
}
function Logout() {
    LogoutAjax().then((response) => {
        window.location = response.url;
    }).catch((reject) => {
        toastr.error(reject);
    })
}
function LogoutAjax() {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "GET",
            url: "/User/Logout",
            success: function (data) {
                resolve(data);
            },
            error: function () {
                reject("Cannot Logout");
            }
        });
    });
}