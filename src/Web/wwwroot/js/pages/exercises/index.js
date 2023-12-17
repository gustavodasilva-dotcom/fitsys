"use strict";

var exercisesIndexPage = function () {
    return {
        init: function (options) {
            
            this.$grid = new helperDataTable({
                ...options,
                columns: [
                    {
                        caption: "Name",
                        getFieldValue: (data) => {
                            return data.name;
                        }
                    },
                    {
                        caption: "Muscle Group(s)",
                        getFieldValue: (data) => {
                            return data.muscleGroups.map(group => group.description).join(' / ');
                        }
                    },
                    {
                        caption: "Gym Equipment(s)",
                        getFieldValue: (data) => {
                            return data.gymEquipments.map(equip => equip.description).join(' / ');
                        }
                    }
                ],
                actions: {
                    get: {
                        route: "Exercises/GetAll"
                    },
                    post: {
                        route: "Exercises/Details"
                    },
                    put: {
                        route: "Exercises/Details",
                        param: "UID"
                    }
                }
            });
        }
    };
}();