require(["dojo/dom", "dojo/on",  "dojo/request", "dojo/query" ,"dijit/registry", "dojo/domReady", "dijit/form/TextBox"], 
    function (dom, on, request, query) {
        on(dom.byId("edit"), "click", function () {
            console.log("click");

        })
        request("/Student/GetCurrentStudent", { handleAs: "json" }).then(
            function (response) {
                if (response.error) {
                    console.log(response.error);
                    return;
                }
                if (response) {
                    dom.byId("name-form").value = response["FirstName"] + " " + response["LastName"];
                    dom.byId("nid-form").value = response["NID"];

                    dom.byId("guardian-form").value = response["GuardianName"];

                    dom.byId("email-form").value = response["Email"];

                    dom.byId("phone-form").value = response["PhoneNumber"];
                    disableInputs();
                }

            });
        function disableInputs() {
            var nodes = dom.byId("form-detail");
            var nodelist = query(".input", nodes);
            nodelist.forEach(function (node) {
                
            })
            console.log(nodelist);
        }
        function enableInputs() {
           
        }
    }

);