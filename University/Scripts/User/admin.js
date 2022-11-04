
function getCurrentUser() {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "GET",
            url: "/User/GetCurrentUser",
            datatype: "json",
            success: function (data) {
                resolve(data);
            },
            error: function () {
                reject("No user received");
            }
        });
    });
}
var currentUserRole = 0;
function start() {
    document.getElementById("downloadcsv")
        .addEventListener("click", function () {
            getStudents().then((response) => {
                var text = jsonToCsv(response);
                var filename = "students_details.csv";
                downloadCSV(filename, text);
            }).catch((reject) => {
                toastr.error(reject);
            });
        }, false);
    getCurrentUser().then((response) => {
        if (response) {
            if (response.currentUser != null) {
                $("#userName").text("Your are logged in as " + response.currentUser + " with User Id: " + response.currentUserId + " as Role: " + response.currentUserRole);
                currentUserRole = response.currentUserRole;
            }
            else {
                toastr.error("No user received");
            }
        }
    }).catch((error) => {
        toastr.error(error);
    })
}
function getStudents() {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "GET",
            url: "/User/GetStudents",
            datatype: "json",
            success: function (data) {
                if (data != null) {
                    resolve(JSON.parse(data));
                }
            },
            error: function (error) {
                reject("No Students");
            }
        });
    });
}
const rejectedGrade = 10;
const numberOfSeats = 15;
let approvedStudent = [];
let topStudentCounter = 0;
function fillTables() {
    getStudents().then((response) => {
        if (response != null) {
            $("#table-rows").empty();
            topStudentCounter = 0;
            $.each(response, function (key, value) {              
                let studentDetail = value;
                let studentRow = document.createElement("tr");
                let studentId;
                $.each(Object.keys(studentDetail), function (key, value) {
                    studentId = studentDetail["studentid"];
                    var studentData = document.createElement("td");
                    if (value == "TotalGrade" && studentDetail[value] < rejectedGrade) {
                        studentRow.className = "rejectedStudent";
                    }

                    studentData.textContent = studentDetail[value];
                    studentRow.append(studentData);

                });
                if (topStudentCounter < numberOfSeats) {
                    approvedStudent.push(studentId)
                    topStudentCounter++;
                    studentRow.className = "approvedStudent";
                }
                $("#table-rows").append(studentRow);
            });
        }
    }).catch((error) => {
        console.log("eroor");
        toastr.error(error);
    });
}
function approveStudents() {
    let approvedStudents = {};
    approvedStudents.Students = approvedStudent;
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "POST",
            url: "/User/ApproveStudents",
            data: approvedStudents,
            success: function (data) {
                if (data.success != null) {
                    resolve(data.success);
                }
                if (data.error != null) {
                    reject(data.error);
                }
            }, error: function () {
                reject("An Error occured");
            }
        })
    })
};
function adminApprove() {
    approveStudents().then((response) => {
        toastr.info(response);
    }).catch((reject) => {
        toastr.error(reject)
    });
}
function downloadCSV(file, text) {
    var element = document.createElement('a');
    element.setAttribute('href', 'data:text/csv;charset=utf-8, ' + encodeURIComponent(text));
    element.setAttribute('download', file);
    $("#csv").append(element);
    element.click();
    $("#csv").remove(element);
}
function jsonToCsv(students) {
    const header = Object.keys(students[0]);
    const headerString = header.join(',');
    const replacer = (key, value) => { return "" ? value == null : value };
    const rowItems = students.map((row) =>
        header
            .map((fieldName) => JSON.stringify(row[fieldName], replacer))
            .join(',')
    );
    const csv = [headerString, ...rowItems].join('\r\n');
    return csv;
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