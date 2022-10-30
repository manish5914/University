function registerStudent() {
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
    $.ajax({
        type: "POST",
        url: "/Student/Register",
        data: student,
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
        console.log(currentSubject);
    }
}
function removeSubject() {
    if (currentSubject > minSubjects) {
        $("#resultsection" + currentSubject).remove();
        currentSubject--;
        console.log(currentSubject);
    }   
}
function validate() {

}