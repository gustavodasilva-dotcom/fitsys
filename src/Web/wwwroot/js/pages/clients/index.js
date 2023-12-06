﻿"use strict";

var clientIndexPage = function () {
    return {
        init: function (options) {

            this.$grid = new helperDataTable({
                ...options,
                columns: [
                    {
                        caption: "Name",
                        getFieldValue: (data) => {
                            return data.user.name;
                        }
                    },
                    {
                        caption: "Email",
                        getFieldValue: (data) => {
                            return data.user.email;
                        }
                    },
                    {
                        caption: "Birthday",
                        getFieldValue: (data) => {
                            return (data.client.birthday ?? "").JsonDateToInputDate();
                        }
                    }
                ],
                actions: {
                    get: {
                        route: "Clients/GetAll"
                    },
                    post: {
                        route: "Clients/Details"
                    },
                    put: {
                        route: "Clients/Details",
                        param: "UID"
                    }
                }
            });
        }
    }
}();