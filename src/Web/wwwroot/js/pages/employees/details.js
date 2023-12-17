"use strict";

var employeesDetailsPage = function () {
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
                        path: "Employees/Get",
                        param: "UID"
                    },
                    post: {
                        path: "Employees/Insert",
                        callback: (_handler) => helperPopUp.toast.success("Employee created successfully")
                    },
                    put: {
                        path: "Employees/Update",
                        param: "UID",
                        callback: (_handler) => helperPopUp.toast.success("Employee updated successfully")
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
                    if (!handler.$("#iptEmployeeName").val())
                        return {
                            isValid: false,
                            message: "The employee name is required",
                            callback: (handler) => { handler.$("#iptEmployeeName").focus() }
                        };

                    if (!handler.$("#iptEmployeeEmail").val())
                        return {
                            isValid: false,
                            message: "The employee email is required",
                            callback: (handler) => { handler.$("#iptEmployeeEmail").focus() }
                        };

                    if (!handler.$("#iptEmployeeBirthday").val())
                        return {
                            isValid: false,
                            message: "The employee birthday is required",
                            callback: (handler) => { handler.$("#iptEmployeeBirthday").focus() }
                        };
                    
                    if (handler.$("#sltEmployeeShift").val().length <= 0)
                        return {
                            isValid: false,
                            message: "The employee working shifts are required",
                            callback: (handler) => { handler.$("#sltEmployeeShift").focus() }
                        };

                    return {
                        isValid: true
                    };
                },
                setupEvents: async (handler) => {
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
                    
                    await helperConstantValues.loadConstants({
                        value: helperConstantValues.enums.shifts,
                        $element: handler.$("#sltEmployeeShift")
                    });
                },
                fillFormInputs: (handler) => {
                    const model = handler.model;

                    handler.$("#iptEmployeeName").val(model.person.name);
                    handler.$("#iptEmployeeEmail").val(model.user.email);
                    handler.$("#iptEmployeeBirthday").val((model.person.birthday ?? "").JsonDateToInputDate());
                    handler.$("#sltEmployeeShift").val(model.shifts.map(shift => shift.uid)).change();

                    if (model.person.profile) {
                        handler.profile = model.person.profile;
                        const imageUrl = helperFunctions.getBaseRoute(handler.profile);
                        handler.$("#imgUserProfile").attr('src', imageUrl).fadeIn('slow');
                    }
                },
                getFormValues: (handler) => {
                    const model = handler.model;

                    model.shifts = handler.$("#sltEmployeeShift").val();
                    model.person.name = handler.$("#iptEmployeeName").val();
                    model.person.birthday = handler.$("#iptEmployeeBirthday").val();
                    model.person.profile = handler.profile;
                    model.user.email = handler.$("#iptEmployeeEmail").val();

                    return model;
                }
            });
        }
    };
}();