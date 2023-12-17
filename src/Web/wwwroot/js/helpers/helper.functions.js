"use strict";

const helperFunctions = function () {
    return {
        getBaseRoute: function (route) {
            return [
                window.location.origin,
                route
            ].join('/');
        },
        normalizePath: function (path) {
            return path.replace(/\\/g, '/');
        }
    };
}();