'use strict';

// ************************************
// Create a Closure for Cart Expiration
// ************************************
var cartController = (function () {
    // ************************************
    // Private Variables
    // ************************************
    let dateExpires;

    // ************************************
    // Private Functions
    // ************************************
    function setExpiration(expires) {
        dateExpires = new Date(expires);
    }

    function clearExpiration() {
        dateExpires = null;
    }

    function getLength() {
        if (dateExpires) {
            // Find the length between now
            // and the count down date
            return dateExpires.getTime() -
                new Date().getTime();
        }
        else {
            return 0;
        }
    }

    function isExpired() {
        if (getLength() <= 0) {
            clearExpiration();
        }

        return (dateExpires === null);
    }

    function getMessage() {
        // Get milliseconds until cart expires
        let length = getLength();

        // Is cart still valid?
        if (length <= 0) {
            clearExpiration();
            return "Your cart has expired";
        }
        else {
            // Calculate minutes and seconds
            let minutes = Math.floor(
                (length % (1000 * 60 * 60)) / (1000 * 60));
            let seconds = Math.floor(
                (length % (1000 * 60)) / 1000);

            return `Your cart expires in ${minutes}
            minute(s) and  ${seconds}  second(s)`;
        }
    }

    // ************************************
    // Public Functions
    // ************************************
    return {
        "setExpiration": setExpiration,
        "clearExpiration": clearExpiration,
        "isExpired": isExpired,
        "getMessage": getMessage
    }
})();
