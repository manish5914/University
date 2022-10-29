function registerStudent() {
    var fname = $("#firstname").val();
    var lname = $("#lastname").val();
    var nid = $("#nid").val();
    var email = $("#email").val();
    var phonenumber = $("#phonenumber").val();
    var guardianname = $("#guardianname").val();
    var dob = $("#dob").val();

    var subject1 = $("#subject1").val();
    var subject2 = $("#subject2").val();
    var subject3 = $("#subject3").val();

    var grade1 = $("#grade1").val();
    var grade2 = $("#grade2").val();
    var grade3 = $("#grade3").val();


    console.log(fname, lname, nid, email, phonenumber, guardianname, subject1, grade1, subject2, grade2, subject3, grade3, dob);
    var student = {
        NID: nid,
        FirstName: fname,
        LastName: lname,
        Email: email,
        PhoneNumber: phonenumber,
        GuardianName: guardianname,
        DateOfBirth: dob,
        Subjects: [subject1, subject2, subject3],
        Grades: [grade1, grade2, grade3]
    };

    console.log(student);
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
            //console.log(jsonSubjects);

            $.each(jsonSubjects, function (key, value) {
               
                    $("#subject" + (currentSubject)).append("<option value=" + value.SubjectId + ">" + value.SubjectName + "</option>");
               
                //console.log(value);
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
            //console.log(jsonGrades);
            $.each(jsonGrades, function (key, value) {
                
                    $("#grade" + currentSubject).append("<option value=" + value.Grade + ">" + value.Grade + "</option>")
                
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

