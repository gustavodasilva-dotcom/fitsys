"use strict";

var personalTrainersDetailsPage = function () {
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
                        path: "PersonalTrainers/Get",
                        param: "UID"
                    },
                    post: {
                        path: "PersonalTrainers/Insert",
                        callback: (_handler) => helperPopUp.toast.success("Personal trainer created successfully")
                    },
                    put: {
                        path: "PersonalTrainers/Update",
                        param: "UID",
                        callback: (_handler) => helperPopUp.toast.success("Personal trainer updated successfully")
                    }
                },
                defaultValues: {
                    shifts: [],
                    person: {
                        name: null,
                        birthday: null,
                        profile: null
                    },
                    user: {
                        email: null,
                        password: null
                    }
                },
                validate: (handler) => {
                    if (!handler.$("#iptPersonalName").val())
                        return {
                            isValid: false,
                            message: "The personal trainer name is required",
                            callback: (handler) => { handler.$("#iptPersonalName").focus() }
                        };

                    if (!handler.$("#iptPersonalEmail").val())
                        return {
                            isValid: false,
                            message: "The personal trainer email is required",
                            callback: (handler) => { handler.$("#iptPersonalEmail").focus() }
                        };

                    if (!handler.$("#iptPersonalBirthday").val())
                        return {
                            isValid: false,
                            message: "The personal trainer birthday is required",
                            callback: (handler) => { handler.$("#iptPersonalBirthday").focus() }
                        };
                    
                    if (handler.$("#sltPersonalShift").val().length <= 0)
                        return {
                            isValid: false,
                            message: "The personal trainer working shifts are required",
                            callback: (handler) => { handler.$("#sltPersonalShift").focus() }
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
                    
                    helperConstants.enums.shifts.loadToCombo(handler.$("#sltPersonalShift"));
                },
                fillFormInputs: (handler) => {
                    const model = handler.model;

                    handler.$("#iptPersonalName").val(model.person.name);
                    handler.$("#iptPersonalEmail").val(model.user.email);
                    handler.$("#iptPersonalBirthday").val((model.person.birthday ?? "").JsonDateToInputDate());
                    handler.$("#sltPersonalShift").val(model.shifts).change();

                    if (model.person.profile) {
                        handler.profile = model.person.profile;
                        const imageUrl = helperFunctions.getBaseRoute(handler.profile);
                        handler.$("#imgUserProfile").attr('src', imageUrl).fadeIn('slow');
                    }
                },
                getFormValues: (handler) => {
                    const model = handler.model;

                    model.shifts = handler.$("#sltPersonalShift").val().map((value) => +value);
                    model.person.name = handler.$("#iptPersonalName").val();
                    model.person.birthday = handler.$("#iptPersonalBirthday").val();
                    model.person.profile = handler.profile;
                    model.user.email = handler.$("#iptPersonalEmail").val();

                    return model;
                }
            });
        }
    };
}();