"use strict";

var exercisesDetailsPage = function () {
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
                        path: "Exercises/Get",
                        param: "UID"
                    },
                    post: {
                        path: "Exercises/Insert",
                        callback: (_handler) => helperPopUp.toast.success("Exercise created successfully")
                    },
                    put: {
                        path: "Exercises/Update",
                        param: "UID",
                        callback: (_handler) => helperPopUp.toast.success("Exercise updated successfully")
                    }
                },
                defaultValues: {
                    name: null,
                    steps: {
                        ops: []
                    },
                    muscleGroups: []
                },
                validate: (handler) => {
                    
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
                            debugger
                            const pathSplit = result.data.split('/');
                            const basePath = pathSplit.slice(pathSplit.length - 2).join('/');

                            const $li = $('<li>').attr('data-target', '#exerciseImages')
                                .attr('data-slide-to', '0')
                                .addClass('active');
                            handler.$(".carousel-indicators").append($li);

                            const $img = $('<img>').addClass('d-block')
                                .attr('src', helperFunctions.getBaseRoute(basePath));

                            const $div = $('<div>').addClass('carousel-item active')
                            $div.append($img);

                            handler.$(".carousel-inner").append($div);
                        }
                    };
                    
                    handler.$("#iptUploadExerciseImage").data('handler', handler).change(_upload);

                    this.$editor = new Quill(".stepsEditor", {
                        placeholder: "Type the exercise steps...",
                        theme: "snow"
                    });

                    await helperConstantValues.loadConstants({
                        value: helperConstantValues.enums.muscle,
                        $element: handler.$("#sltExerciseMuscleGroups")
                    });
                },
                fillFormInputs: (handler) => {
                    const model = handler.model;
                    
                    handler.$("#iptExerciseName").val(model.name);
                    this.$editor.setContents(model.steps);
                    handler.$("#sltExerciseMuscleGroups").val(model.muscleGroups.map(group => group.uid)).change();
                },
                getFormValues: (handler) => {
                    const model = handler.model;
                    
                    model.name = handler.$("#iptExerciseName").val();
                    model.steps = this.$editor.getContents();
                    model.muscleGroups = handler.$("#sltExerciseMuscleGroups").val();

                    return model;
                }
            });
        }
    };
}();