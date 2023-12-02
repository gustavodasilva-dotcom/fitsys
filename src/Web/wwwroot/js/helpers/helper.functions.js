"use strict";

const helperFunctions = function () {
    return {
        getFullRoute: function (route) {
            return [
                window.location.origin,
                route
            ].join('/');
        }
    };
}();