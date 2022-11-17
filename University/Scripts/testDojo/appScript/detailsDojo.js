define(["require", "exports", "dojo/ready", "dojo/dom", "dojo/on", "dojo/request", "dojo/query", "dojo/dom-attr", "dijit/form/ToggleButton"], function (require, exports, ready, dom, on, request, query, domAttr, ToggleButton) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    ready(new function () {
        var disabled = "true";
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
        });
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
            }).then(function () {
                var args = [];
                for (var _i = 0; _i < arguments.length; _i++) {
                    args[_i] = arguments[_i];
                }
                var response = args[0];
                toastr.success("Logging out...");
                window.location = response.url;
            }).otherwise(function () {
                var args = [];
                for (var _i = 0; _i < arguments.length; _i++) {
                    args[_i] = arguments[_i];
                }
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
        }
        request.get("/Student/GetCurrentStudent", {
            handleAs: 'json',
            data: "",
            preventCache: false,
            query: "",
            timeout: 0
        }).then(function () {
            var args = [];
            for (var _i = 0; _i < arguments.length; _i++) {
                args[_i] = arguments[_i];
            }
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
});
//# sourceMappingURL=detailsDojo.js.map