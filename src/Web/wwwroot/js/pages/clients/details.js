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
                        param: "UID"
                    },
                    post: {
                        path: "Clients/Insert",
                        callback: (_handler) => helperPopUp.toast.success("Client created successfully")
                    },
                    put: {
                        path: "Clients/Update",
                        param: "UID",
                        callback: (_handler) => helperPopUp.toast.success("Client updated successfully")
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
                    },
                    profile: null
                },
                validate: (handler) => {
                    if (!handler.$("#iptClientName").val())
                        return {
                            isValid: false,
                            message: "The client name is required",
                            callback: (handler) => { handler.$("#iptClientName").focus() }
                        };

                    if (!handler.$("#iptClientEmail").val())
                        return {
                            isValid: false,
                            message: "The client email is required",
                            callback: (handler) => { handler.$("#iptClientEmail").focus() }
                        };

                    if (!handler.$("#iptClientWeight").val())
                        return {
                            isValid: false,
                            message: "The client weight is required",
                            callback: (handler) => { handler.$("#iptClientWeight").focus() }
                        };

                    if (!handler.$("#iptClientHeight").val())
                        return {
                            isValid: false,
                            message: "The client height is required",
                            callback: (handler) => { handler.$("#iptClientHeight").focus() }
                        };

                    if (!handler.$("#iptClientBirthday").val())
                        return {
                            isValid: false,
                            message: "The client birthday is required",
                            callback: (handler) => { handler.$("#iptClientBirthday").focus() }
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
                            
                            const pathSplit = result.data.split('/');
                            const basePath = pathSplit.slice(pathSplit.length - 2).join('/');

                            handler.profile = basePath;
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
                    
                    if (model.profile) {
                        handler.profile = model.profile;
                        const imageUrl = helperFunctions.getBaseRoute(handler.profile);
                        handler.$("#imgUserProfile").attr('src', imageUrl).fadeIn('slow');
                    }
                },
                getFormValues: (handler) => {
                    const model = handler.model;
                    
                    model.user.name = handler.$("#iptClientName").val();
                    model.user.email = handler.$("#iptClientEmail").val();
                    model.client.weight = handler.$("#iptClientWeight").val();
                    model.client.height = handler.$("#iptClientHeight").val();
                    model.client.birthday = handler.$("#iptClientBirthday").val();
                    model.profile = handler.profile;

                    return model;
                }
            });
        }
    };
}();