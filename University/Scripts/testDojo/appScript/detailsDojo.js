define(["require", "exports", "dojo/ready", "dojo/dom", "dojo/on", "dojo/request", "dojo/query"], function (require, exports, ready, dom, on, request, query) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    ready(new function () {
        //console.log("ready dom");
        //console.log(dom.byId("name"));
        //    toastr.error("error but worked");
        on(dom.byId("edit"), "click", function () {
            console.log("click");
        });
        var data = request.get("/Student/GetCurrentStudent", {
            handleAs: 'json',
            data: "",
            preventCache: false,
            query: "",
            timeout: 0
        });
        data.then(function () {
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
                dom.byId("name-form").value = response["FirstName"] + " " + response["LastName"];
                dom.byId("nid-form").value = response["NID"];
                dom.byId("guardian-form").value = response["GuardianName"];
                dom.byId("email-form").value = response["Email"];
                dom.byId("phone-form").value = response["PhoneNumber"];
                toggleInputs(true);
            }
        });
        function toggleInputs(status) {
            var inputs = query(".input");
            inputs.forEach(function (input) {
                input.disabled = status;
            }, this);
        }
    });
});
//# sourceMappingURL=detailsDojo.js.map