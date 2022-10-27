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
function fillList() {

    var numberofinput = 3;
    $.ajax({
        type: "POST",
        url: "/Student/GetSubjects",
        data: "",

        success: function (data) {

            var jsonSubjects = JSON.parse(data);
            console.log(jsonSubjects);

            $.each(jsonSubjects, function (key, value) {
                for (let i = 0; i < numberofinput; i++) {
                    $("#subject" + (i + 1)).append("<option value=" + value.SubjectId + ">" + value.SubjectName + "</option>");
                }
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
            console.log(jsonGrades);
            $.each(jsonGrades, function (key, value) {
                for (let i = 0; i < numberofinput; i++) {
                    $("#grade" + (i + 1)).append("<option value=" + value.Grade + ">" + value.Grade + "</option>")
                }
            });
        }
    });
}