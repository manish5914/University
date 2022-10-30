function registerStudent() {
    var studentDetails = getValues();
    $.ajax({
        type: "POST",
        url: "/Student/Register",
        data: studentDetails,
        success: function (data) {
            if (data.url) {
                console.log("added student");
                window.location = data.url;
            }
            if (data.error) {
                alert(data.error);
            }
        },
        error: function () {
            console.log("What broke ?, Your soul did");
        }
    });
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
    for (let index = minSubjects; index < maxSubjects; index++) {
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
    $.ajax({
        type: "POST",
        url: "/Student/GetSubjects",
        data: "",
        success: function (data) {
            var jsonSubjects = JSON.parse(data);
            $.each(jsonSubjects, function (key, value) {
                var subjectoption = document.createElement("option");
                subjectoption.value = value.SubjectId;
                subjectoption.text = value.SubjectName;
                $("#subject" + currentSubject).append(subjectoption);    
            });
        }, error: function () {
            console.log("Error");
        }
    });
    $.ajax({
        type: "POST",
        url: "/Student/GetGrades",
        data: "",
        success: function (data) {
            var jsonGrades = JSON.parse(data);
            $.each(jsonGrades, function (key, value) {
                var gradeoption = document.createElement("option");
                gradeoption.value = value.Grade;
                gradeoption.text = value.Grade;
                $("#grade" + currentSubject).append(gradeoption);           
            });
        }
    });
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
function validate() {
    console.log(validateInputs());
}
function validateInputs() {
    const maxNameLength = 50
    const nidLength = 14;
    const phoneNumberLength = 8;
    const phoneNumberPattern = '^\\d{' + phoneNumberLength + '}$';
    const emailPattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    var fname = $("#firstname").val();
    var lname = $("#lastname").val();
    var nid = $("#nid").val();
    var email = $("#email").val();
    var phonenumber = $("#phonenumber").val();
    var guardianname = $("#guardianname").val();
    var dob = $("#dob").val();
    var subjects = [];
    var grades = [];
    for (let index = minSubjects; index <= currentSubject; index++) {
        subjects.push($("#subject" + index).val());
        grades.push($("#grade" + index).val());
    }
    var phoneNumberRegExp = new RegExp(phoneNumberPattern);
    var emailRegExp = new RegExp(emailPattern)
    if (fname.length == 0 || fname.length > maxNameLength) {
        return "First Name is invalid";
    }
    if (lname.length == 0 || lname.length > maxNameLength) {
        return "Last Name is invalid";
    }
    if (nid.length != nidLength) {
        return "NID is invalid";
    }
    if (!dob) {
        return "No dob inserted";
    }
    if (!emailRegExp.test(email)) {
        return "Email is invalid";
    }
    if (!phoneNumberRegExp.test(phonenumber)) {
        
        return "Phone Number is invalid";
    }
    if (guardianname.length == 0 || guardianname.length > maxNameLength) {
        return "Guardian Number is invalid";
    }
    
    if (hasDuplicates(subjects)) {
        return "Duplicate Subject selected";
    }
    return "All Good";
}
function hasDuplicates(array) {
    return (new Set(array)).size !== array.length;
}