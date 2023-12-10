"use strict";

var personalTrainersIndexPage = function () {
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
                            return data.shifts.map(shift => shift.value).join(' / ');
                        }
                    }
                ],
                actions: {
                    get: {
                        route: "PersonalTrainers/GetAll"
                    },
                    post: {
                        route: "PersonalTrainers/Details"
                    },
                    put: {
                        route: "PersonalTrainers/Details",
                        param: "UID"
                    }
                }
            });
        }
    };
}();