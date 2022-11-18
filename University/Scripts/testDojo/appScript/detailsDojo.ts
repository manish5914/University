import * as ready from "dojo/ready";
import * as dom from "dojo/dom";
import * as on from "dojo/on";
import * as request from "dojo/request";
import * as query from "dojo/query";
import * as nodeList from "dojo/NodeList-dom";
import * as array from "dojo/_base/array";
import * as domAttr from "dojo/dom-attr";
import * as ToggleButton from "dijit/form/ToggleButton";
import * as validate from "dojox/validate/_base";

ready(new function () {
    const disabled = "true"; 
    function disableInputs() {
        var inputs = query(".input");
        inputs.forEach(function (input) {
            input.disabled = disabled;
        }, this);
    }
    function enableInputs() {
        var inputs = query(".input");
        inputs.forEach(function (input) {
            domAttr.remove(input, "disabled");
        }, this);
    }
    var editButton = dom.byId("edit");
    on(editButton, "click", function () {
        enableInputs();
        editButton.innerHTML = "Cancel";
    })
    on(dom.byId("logoutButton"), "click", function () {
        Logout();
    });
    function Logout() {
        request.get("/User/Logout", {
            handleAs: "json",
            data: "",
            preventCache: false,
            query: "",
            timeout: 0
        }).then((...args: any[]) => {
            var response = args[0];
            toastr.success("Logging out...");
            window.location = response.url;
        }).otherwise((...args: any[]) => {
            toastr.error(args[0]);
        });
    
    }
    new ToggleButton({
        showLabel: true,
        checked: false,
        onChange: function (val) {
            this.set('label', val);
            if (val) {
                enableInputs();
                this.set('label', "Cancel");
            }
            else {
                disableInputs();
                this.set('label', "Edit Detail");
            }
        },
        label: "Edit Details"
    }, dom.byId("edit"));
    function getValues() {
        
        return { FirstName: dom.byId("name-form").value, LastName: dom.byId("surname-form").value, NID: dom.byId("nid-form").value, GuardianName: dom.byId("guardian-form").value, Email: dom.byId("email-form").value, PhoneNumber: dom.byId("phone-form").value };
    }
    function isNameValid() {
        console.log(validate.isText(dom.byId("name-form").value, { minlength: 1 })); 
    }
    on(dom.byId("name-form"), "blur", function () {
        isNameValid();
    })
    request.get("/Student/GetCurrentStudent", {
        handleAs: 'json',
        data: "",
        preventCache: false,
        query: "",
        timeout: 0
    }).then((...args: any[]) => {
        var response = args[0];
        if (response.error) {
            toastr.error(response.error);
            console.log(response.error);
            return;
        }
        if (response) {
            dom.byId("name-form").value = response["FirstName"];
            dom.byId("surname-form").value = response["LastName"];
            dom.byId("nid-form").value = response["NID"];
            dom.byId("guardian-form").value = response["GuardianName"];
            dom.byId("email-form").value = response["Email"];
            dom.byId("phone-form").value = response["PhoneNumber"];
            disableInputs();

        }
    });
});


    