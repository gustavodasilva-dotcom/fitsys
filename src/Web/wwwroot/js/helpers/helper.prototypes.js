"use strict";

const helperPrototypes = function () {
    return {
        init: function () {
            String.prototype.JsonDateToInputDate = function () {
                return this.valueOf().split('T')[0];
            };
        }
    }
}();