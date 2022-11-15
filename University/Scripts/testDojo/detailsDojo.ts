//require(["dojo/dom", "dojo/on",  "dojo/request", "dijit/registry", "dojo/domReady", "dijit/form/TextBox"], 
/// <reference path="../typings/dojo/dojo.d.ts" />
/// <reference path ="../typings/toastr/toastr.d.ts" />

import * as dom from "dojo/dom";
import * as on from "dojo/on";
import * as toastr from "toastr";

on(dom.byId("edit"), "click", function (event) {
    if (event.submitter.matches("#edit")) {
        event.preventDefault();
    }
    console.log("clicked");
    testToastr();
})
function testToastr() {
    toastr.info("toastr appeared");
}


//    function (dom, on, request) {
//        on(dom.byId("edit"), "click", function () {
//            console.log("click");
            
//        })
//        request("/Student/GetCurrentStudent", { handleAs: "json" }).then(
//            function (response) {
//                console.log(response)
//                if (response.error) {
//                    console.log(response.error);
//                    return;
//                }
//                if (response) {
//                    dom.byId("name-form").value = response["FirstName"] + " " + response["LastName"];
//                    dom.byId("nid-form").value = response["NID"];
         
//                    dom.byId("guardian-form").value = response["GuardianName"];

//                    dom.byId("email-form").value = response["Email"];

//                    dom.byId("phone-form").value = response["PhoneNumber"];
//                    disableInputs();
//                }

//            });
//        function disableInputs() {
//            dom.query("input", dom.byId("form-detail")).forEach(
//                function (element) {
//                    element.disabled = "disabled";
//                }
//            );
//        }
//        function enableInputs() {
//            dom.query("input", dom.byId("form-detail")).forEach(
//                function (element) {
//                    element.disabled = "";
//                }
//            );
//        }
//    }
    
////);

