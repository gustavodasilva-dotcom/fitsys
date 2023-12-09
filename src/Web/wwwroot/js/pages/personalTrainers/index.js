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
                            const shifts = data.shifts.map((shift) => {
                                const values = Object.keys(helperConstants.enums.shifts.values);
                                return values[shift];
                            });

                            return shifts.join(' / ');
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