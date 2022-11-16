import * as ready from "dojo/ready";
import * as dom from "dojo/dom";
import * as on from "dojo/on";
import * as request from "dojo/request";
import * as query from "dojo/query";
import * as nodeList from "dojo/NodeList-dom";
import * as array from "dojo/_base/array"
ready(new function () {
    //console.log("ready dom");
    //console.log(dom.byId("name"));
    //    toastr.error("error but worked");
    on(dom.byId("edit"), "click", function () {
        console.log("click");
    })
    const data = request.get("/Student/GetCurrentStudent", {
        handleAs: 'json',
        data: "",
        preventCache: false,
        query: "",
        timeout: 0
    });
    data.then((...args: any[]) => {
        var response = args[0];
        if (response.error) {
            toastr.error(response.error);
            console.log(response.error);
            return;
        }
        if (response) {
            dom.byId("name-form").value = response["FirstName"] + " " + response["LastName"];
            dom.byId("nid-form").value = response["NID"];
            dom.byId("guardian-form").value = response["GuardianName"];
            dom.byId("email-form").value = response["Email"];
            dom.byId("phone-form").value = response["PhoneNumber"];
            toggleInputs(true);
        }
    });
    function toggleInputs(status:boolean) {
        var inputs = query(".input");
        inputs.forEach(function (input) {
            input.disabled = status;
        }, this);
    }
});


    