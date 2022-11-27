// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

'use strict';

// ************************************
// Create a Closure
// ************************************
var mainController = (function () {
    // ************************************
    // Private Variables
    // ************************************
    let searchValues = null;

    // ************************************
    // Private Functions
    // ************************************
    function disableAllClicks() {
        $("a").css("cursor", "arrow").click(false);
        $("input[type='button']")
            .attr("disabled", "disabled");
        $("button").attr("disabled", "disabled");
    }

    function pleaseWait(ctl) {
        // Was a control passed in?
        if (ctl) {
            // Look for a data-waitmsg="message"
            // on the control clicked on
            let msg = $(ctl).data("waitmsg");
            if (msg) {
                $("#theWaitMessage").html(msg);
            }
        }

        $("#pleaseWait").removeClass("d-none");
        $("header").addClass("pleaseWaitArea");
        $("main").addClass("pleaseWaitArea");
        $("footer").addClass("pleaseWaitArea");

        disableAllClicks();
    }

    function formSubmit() {
        $("form").submit(function () {
            if ($("form").valid()) {
                pleaseWait(this);
            }
        });
    }

    function setSearchValues(value) {
        searchValues = value;
    }

    function isSearchFilledIn() {
        return searchValues;
    }

    function setSearchArea() {
        $("#searchBody").collapse(isSearchFilledIn() ? "show" : "hide");
    }


    // ************************************
    // Public Functions
    // ************************************
    return {
        "pleaseWait": pleaseWait,
        "disableAllClicks": disableAllClicks,
        "formSubmit": formSubmit,
        "setSearchValues": setSearchValues,
        "isSearchFilledIn": isSearchFilledIn,
        "setSearchArea": setSearchArea
    }
})();
