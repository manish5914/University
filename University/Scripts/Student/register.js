function registerStudent() {
    if (validateInputs()) {
        var studentDetails = getValues();
        registerStudentAjax(studentDetails).then((response) => {
            if (response.url) {
                console.log("added student");
                window.location = response.url;
            }
            if (response.error) {
                toastr.error(response.error);
            }
        }).catch((reject) => {
            toastr.error(reject);
        })
    }
}
function registerStudentAjax(studentDetails) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "POST",
            url: "/Student/Register",
            data: studentDetails,
            success: function (data) {
                resolve(data);
            },
            error: function () {
                reject("What broke ?, Your soul did");
            }
        });
    })
}
function getValues() {
    var fname = $("#firstname").val();
    var lname = $("#lastname").val();
    var nid = $("#nid").val();
    var email = $("#email").val();
    var phonenumber = $("#phonenumber").val();
    var guardianname = $("#guardianname").val();
    var dob = $("#dob").val();
    var subjects = [];
    var grades = [];
    for (let index = minSubjects; index <= maxSubjects; index++) {
        subjects.push($("#subject" + index).val());
        grades.push($("#grade" + index).val());
    }
    var student = {
        NID: nid,
        FirstName: fname,
        LastName: lname,
        Email: email,
        PhoneNumber: phonenumber,
        GuardianName: guardianname,
        DateOfBirth: dob,
        Subjects: subjects,
        Grades: grades
    };
    return student;
}
var numberofinput = 3;
function fillList() {
    GetSubjectsAjax().then((response) => {
        var jsonSubjects = JSON.parse(response);
        $.each(jsonSubjects, function (key, value) {
            var subjectoption = document.createElement("option");
            subjectoption.value = value.SubjectId;
            subjectoption.text = value.SubjectName;
            $("#subject" + currentSubject).append(subjectoption);
        });
    }).catch((reject) => {
        toastr.error(reject);
    })
    GetGradesAjax().then((response) => {
        var jsonGrades = JSON.parse(response);
        $.each(jsonGrades, function (key, value) {
            var gradeoption = document.createElement("option");
            gradeoption.value = value.Grade;
            gradeoption.text = value.Grade;
            $("#grade" + currentSubject).append(gradeoption);
        });
    }).catch((reject) => {
        toastr.error(reject);
    })
}
function GetGradesAjax() {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "POST",
            url: "/Student/GetGrades",
            data: "",
            success: function (data) {
                resolve(data);
            },
            error: function () {
                reject("No Grades Received");
            }
        });
    })
}
function GetSubjectsAjax() {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "POST",
            url: "/Student/GetSubjects",
            data: "",
            success: function (data) {
                resolve(data);
                
            }, error: function () {
                reject("No Subject Received");
            }
        });
    })
}
const maxSubjects = 3;
const minSubjects = 1;
let currentSubject = 0;
function addSubject() {
    if (currentSubject < maxSubjects) {
        currentSubject++;
        let main = document.createElement("div");
        main.id = "resultsection" + currentSubject;
        main.className = "resultsection"

        let subjectselect = document.createElement("select");
        subjectselect.id = "subject" + currentSubject;
        subjectselect.className = "subject-input";

        let gradeselect = document.createElement("select");
        gradeselect.id = "grade" + currentSubject;
        gradeselect.className = "grade-input";

        main.append(subjectselect);
        main.append(gradeselect);
        $("#resultsinput").append(main);
        fillList();
    }
}
function removeSubject() {
    if (currentSubject > minSubjects) {
        $("#resultsection" + currentSubject).remove();
        currentSubject--;
    }   
}
function validateInputs() {
    if (!validateFirstName()) {
        return false;
    }
    if (!validateLastName()) {
        return false;
    }
    if (!validateNID()) {
        return false;
    }
    if (!validateDateOfBirth()) {
        return false;
    }
    if (!validateEmail()) {
        return false;
    }
    if (!validatePhoneNumber()) {
        return false;
    }
    if (!validateGuardianName()) {
        return false;
    }
    if (!validateSubjects()) {
        return false;
    }
    return true;
}
function validateSubjects() {
    var subjects = [];
    var grades = [];
    for (let index = minSubjects; index <= currentSubject; index++) {
        subjects.push($("#subject" + index).val());
        grades.push($("#grade" + index).val());
    }
    if (hasDuplicates(subjects)) {
        toastr.info("Duplicate Subjects added");
        return false; 
    }
    return true;
}
function validateDateOfBirth() {
    var dob = $("#dob");
    if (!dob.val()) {
        dob.addClass("invalid");
        toastr.info("No dob inserted");
        return false; 
    }
    if (dob.hasClass("invalid")) {
        dob.removeClass("invalid")
    }
    return true;
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
const maxNameLength = 50;
function validateFirstName() {
    var name = $("#firstname");
    if (name.val().length == 0 || name.val().length > maxNameLength) {
        name.addClass("invalid");
        toastr.info("Name is invalid");
        return false; 
    }
    if (name.hasClass("invalid")) {
        name.removeClass("invalid")
    }
    return true;
}
function validateLastName() {
    var lastname = $("#lastname");
    if (lastname.val().length == 0 || lastname.val().length > maxNameLength) {
        lastname.addClass("invalid");
        toastr.info("Last Name is invalid");
        return false;
    }
    if (lastname.hasClass("invalid")) {
        lastname.removeClass("invalid");
    }
    return true;
}
function validateGuardianName() {
    var guardianname = $("#guardianname");
    if (guardianname.val().length == 0 || guardianname.val().length > maxNameLength) {
        guardianname.addClass("invalid");
        toastr.info("Guardian Name is invalid");
        return false;
    }
    if (guardianname.hasClass("invalid")) {
        guardianname.removeClass("invalid");
    }
    return true;
}
function validateNID() {
    const nidLength = 14;
    var nid = $("#nid");
    if (nid.val().length != nidLength) {
        nid.addClass("invalid");
        toastr.info("NID is invalid");
        return false;
    }
    if (nid.hasClass("invalid")) {
        nid.removeClass("invalid");
    }
    return true;
}
function validatePhoneNumber(){
    const phoneNumberLength = 8;
    const phoneNumberPattern = '^\\d{' + phoneNumberLength + '}$';
    var phonenumber = $("#phonenumber");
    var phoneNumberRegExp = new RegExp(phoneNumberPattern);
    if (!phoneNumberRegExp.test(phonenumber.val())) {
        phonenumber.addClass("invalid");
        toastr.info("Phone Number is invalid");
        return false;
    }
    if (phonenumber.hasClass("invalid")) {
        phonenumber.removeClass("invalid");
    }
    return true;
}
function hasDuplicates(array) {
    return (new Set(array)).size !== array.length;
}