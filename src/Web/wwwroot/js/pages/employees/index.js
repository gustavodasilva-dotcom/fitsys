"use strict";

var employeesIndexPage = function () {
    return {
        init: function (options) {

            this.$grid = new helperDataTable({
                ...options,
                columns: [
                    {
                        caption: "Name",
                        getFieldValue: (data) => {
                            return data.person.name;
                        }
                    },
                    {
                        caption: "Email",
                        getFieldValue: (data) => {
                            return data.user.email;
                        }
                    },
                    {
                        caption: "Shift(s)",
                        getFieldValue: (data) => {
                            return data.shifts.map(shift => shift.description).join(' / ');
                        }
                    }
                ],
                actions: {
                    get: {
                        route: "Employees/GetAll"
                    },
                    post: {
                        route: "Employees/Details"
                    },
                    put: {
                        route: "Employees/Details",
                        param: "UID"
                    }
                }
            });
        }
    };
}();