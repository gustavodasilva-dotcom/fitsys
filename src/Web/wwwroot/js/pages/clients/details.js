"use strict";

var clientDetailsPage = function () {
    return {
        init: async function (options) {
            
            this.$page = await new helperDetailsPage({
                ...options,
                footer: {
                    submitButton: "#btnSubmit",
                    resetButton: "#btnReset"
                },
                routes: {
                    get: {
                        path: "Clients/Get",
                        param: "Id"
                    },
                    post: {
                        path: "Clients/Insert",
                        params: {}
                    },
                    put: {
                        path: "Clients/Details",
                        param: "Id"
                    }
                },
                defaultValues: {
                    user: {
                        name: null,
                        email: null,
                        password: null
                    },
                    client: {
                        weight: 0,
                        height: 0,
                        birthday: null
                    }
                },
                validate: (handler) => {
                    if (!handler.$("#iptClientName").val())
                        return {
                            isValid: false,
                            message: "The user name is required",
                            callback: (handler) => { handler.$("#iptClientName").focus() }
                        };

                    return {
                        isValid: true
                    };
                },
                setupEvents: (handler) => {
                    const _upload = async (event) => {
                        const $input = $(event.currentTarget);
                        const handler = $input.data('handler');

                        const file = $input[0]?.files[0];

                        if (file) {
                            var reader = new FileReader();

                            reader.onload = function (e) {
                                handler.$("#imgUserProfile").attr('src', e.target.result).fadeIn('slow');
                            };
                            reader.readAsDataURL(file);

                            const form = new FormData();
                            form.append(file.name, file);

                            const result = await $.ajax({
                                async: true,
                                type: "POST",
                                url: helperFunctions.getBaseRoute("Files/Upload"),
                                contentType: false,
                                processData: false,
                                data: form
                            });

                            if (result.statusCode !== helperConstants.statusCodes.OK) {
                                helperPopUp.dialog.warning(helperConstants.messages.unexpected, result.message);
                                return;
                            }

                            handler.profile = result.data;
                        }
                    };

                    handler.$("#iptUploadUserProfile").data('handler', handler).change(_upload);
                },
                fillFormInputs: (handler) => {
                    const model = handler.model;

                    handler.$("#iptClientName").val(model.user.name);
                    handler.$("#iptClientEmail").val(model.user.email);
                    handler.$("#iptClientWeight").val(model.client.weight);
                    handler.$("#iptClientHeight").val(model.client.height);
                    handler.$("#iptClientBirthday").val((model.client.birthday ?? "").JsonDateToInputDate());
                },
                getFormValues: (handler) => {
                    const model = handler.model;

                    model.user.name = handler.$("#iptClientName").val();
                    model.user.email = handler.$("#iptClientEmail").val();
                    model.client.weight = handler.$("#iptClientWeight").val();
                    model.client.height = handler.$("#iptClientHeight").val();
                    model.client.birthday = handler.$("#iptClientBirthday").val();
                    
                    return model;
                }
            });
        }
    };
}();