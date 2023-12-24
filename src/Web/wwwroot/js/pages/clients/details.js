"use strict";

var workoutRoutineModal = function () {
    return {
        options: {},
        model: {},
        $modal: null,
        $table: null,
        $: function (elm) {
            return $(elm, this.$modal);
        },
        init: async function (options) {
            this.options = options;

            if (!this.$modal) {
                await this._loadModal();
                await this._setupEvents();
            }

            this._clearControls();
            this._loadValues();

            return this;
        },
        _loadModal: async function () {
            await $.get("/Employees/_modalWorkoutRoutine").then((result) => {
                const $modal = $('<div>');
                $modal.html(result);

                $(document.body).append($modal);

                this.$modal = $modal;
            });
        },
        _setupEvents: async function () {
            await helperFunctions.loadSelectOptions({
                $element: this.$("#sltWorkoutRoutineExercise"),
                data: {
                    source: async () => {
                        return await $.ajax({
                            async: true,
                            type: "GET",
                            url: helperFunctions.getBaseRoute("Exercises/GetAll"),
                        });
                    }
                },
                attributes: {
                    getValue: (data) => {
                        return data.uid;
                    },
                    getText: (data) => {
                        return data.name;
                    }
                },
                extra: {
                    blankOption: true
                }
            });

            this.$("#btnSaveWorkout").on('click', (e) => this._addWorkoutToRoutine(e));
            this.$("#btnSaveChanges").on('click', () => this._saveWorkoutRoutine());
            this.$("[data-bs-dismiss='modal']").on('click', () => this.hide());
        },
        _loadValues: function () {
            const model = this.options.model;

            this.$("#iptWorkoutRoutineName").val(model.name);
            this.model = [...this.options.model.exercises ?? []];

            this._renderRoutineWorkouts();
        },
        _addWorkoutToRoutine: function (e) {
            e.preventDefault();

            this.model.push({
                uid: helperConstants.EmptyGuid,
                uidExercise: this.$("#sltWorkoutRoutineExercise option:selected").val(),
                sets: +this.$("#iptWorkoutRoutineSets").val(),
                reps: +this.$("#iptWorkoutRoutineReps").val(),
                exercise: this.$("#sltWorkoutRoutineExercise option:selected").data('option-obj')
            });

            this._renderRoutineWorkouts();
            this._clearFields();
        },
        _renderRoutineWorkouts: function () {
            const _deleteWorkoutRoutine = (data) => {
                const index = this.model.indexOf(data);
                if (index > -1)
                    this.model.splice(index, 1);

                this._renderRoutineWorkouts();
            };

            const $tbody = this.$("#tableWorkoutRoutines tbody");
            $tbody.empty();

            this.model.forEach((data) => {
                const $tr = this.$('<tr>').addClass('odd');

                const $tdOptions = this.$('<td>');

                const $editButton = this.$('<button>').addClass('btn btn-info');
                $editButton.on('click', (e) => {
                    e.preventDefault();
                    alert('Edit');
                });

                const $deleteButton = this.$('<button>').addClass('btn btn-danger');
                $deleteButton.on('click', (e) => {
                    e.preventDefault();
                    _deleteWorkoutRoutine(data);
                });

                $tdOptions.append($editButton.append(this.$('<i>').addClass('bi bi-pencil-square')));
                $tdOptions.append($deleteButton.append(this.$('<i>').addClass('bi bi-trash-fill')));
                $tr.append($tdOptions);

                const $tdExercise = this.$('<td>').text(data.exercise.name);
                $tr.append($tdExercise);

                const $tdSets = this.$('<td>').text(data.sets);
                $tr.append($tdSets);

                const $tdReps = this.$('<td>').text(data.reps);
                $tr.append($tdReps);

                $tbody.append($tr);
            });
        },
        _saveWorkoutRoutine: function () {
            this.options.model.name = this.$("#iptWorkoutRoutineName").val();
            this.options.model.exercises = [...this.model];

            this.hide();

            if (this.options.callback)
                this.options.callback();
        },
        _clearFields: function () {
            this.$("#sltWorkoutRoutineExercise").val("");
            this.$("#iptWorkoutRoutineSets").val("");
            this.$("#iptWorkoutRoutineReps").val("");
        },
        _clearControls: function () {
            this.model = [];
        },
        hide: function () {
            this.$("#modalWorkoutRoutine").modal('hide');
        },
        show: function () {
            this.$("#modalWorkoutRoutine").modal('show');
        }
    }
}();

var workoutRoutinesTab = function () {
    return {
        $: function (elm) {
            return $(elm, this.options.wrapper);
        },
        options: {},
        models: [],
        templates: {
            workoutRoutine: null
        },
        init: function (setup) {
            this.options = setup.options;

            this._loadTemplates();
            this._setupEvents();
        },
        renderModel: function (model) {
            this.models = model;
            this._renderWorkoutCards();
        },
        _loadTemplates: function () {
            const $card = this.$("#cardWorkoutRoutine").clone();
            $card.removeClass('d-none');
            this.templates.workoutRoutine = $card;

            this.$("#cardWorkoutRoutine").remove();
        },
        _renderWorkoutCards: function () {
            const $container = this.$("#cardsContainer");
            $container.empty();

            this.models.forEach((routine) => {
                const _deleteCard = async (routine) => {
                    const dialog = await helperPopUp.dialog.question(
                        helperConstants.messages.question,
                        helperConstants.messages.delete
                    );

                    if (dialog.isConfirmed) {
                        const $card = this.$("#cardWorkoutRoutine").data('number', routine.number);
                        if ($card.length > 0)
                            $card.remove();

                        const index = this.models.indexOf(routine);
                        if (index > -1)
                            this.models.splice(index, 1);

                        this._renderWorkoutCards();
                    }
                };

                const _openModal = () => {
                    workoutRoutineModal.init({
                        model: routine,
                        callback: () => this._renderWorkoutCards()
                    }).then((result) => {
                        result.show();
                    });
                };

                const $card = this.templates.workoutRoutine.clone();
                $card.data('number', routine.number);

                $("#titleWorkoutName", $card).text(routine.name);
                $("#titleWorkoutAmount", $card).text([routine.exercises.length, 'exercises'].join(' '));
                $("#btnEditWorkout", $card).on('click', () => _openModal());
                $("#btnDeleteWorkout", $card).on('click', () => _deleteCard(routine));

                const $lastRow = $container.find('div.row:last');
                const amountCards = $('div#cardWorkoutRoutine', $lastRow).length;

                if (amountCards > 0 && amountCards < 2)
                    $lastRow.append($card);
                else {
                    const $row = this.$('<div>').addClass('row');
                    $row.append($card);
                    $container.append($row);
                }
            });
        },
        _addWorkoutCard: function () {
            this.models.push({
                uid: helperConstants.EmptyGuid,
                number: this.models.length + 1,
                name: [
                    "Untitled workout",
                    this.models.length + 1
                ].join(' '),
                exercises: [],
            });

            this._renderWorkoutCards();
        },
        _setupEvents: function () {
            this.$("#btnAddWorkout").on('click', () => this._addWorkoutCard());
        }
    }
}();

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
                    weight: 0,
                    height: 0,
                    person: {
                        name: null,
                        birthday: null,
                        profile: null
                    },
                    user: {
                        email: null,
                        password: null
                    },
                    workouts: []
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

                            const pathSplit = helperFunctions.normalizePath(result.data).split('/');
                            const basePath = pathSplit.slice(pathSplit.length - 2).join('/');

                            handler.profile = basePath;
                        }
                    };

                    handler.$("#iptUploadUserProfile").data('handler', handler).change(_upload);

                    workoutRoutinesTab.init({
                        options: handler.options
                    });
                },
                fillFormInputs: (handler) => {
                    const model = handler.model;

                    handler.$("#iptClientName").val(model.person.name);
                    handler.$("#iptClientEmail").val(model.user.email);
                    handler.$("#iptClientWeight").val(model.weight);
                    handler.$("#iptClientHeight").val(model.height);
                    handler.$("#iptClientBirthday").val((model.person.birthday ?? "").JsonDateToInputDate());

                    if (model.person.profile) {
                        handler.profile = model.person.profile;
                        const imageUrl = helperFunctions.getBaseRoute(handler.profile);
                        handler.$("#imgUserProfile").attr('src', imageUrl).fadeIn('slow');
                    }

                    workoutRoutinesTab.renderModel(model.workouts);
                },
                getFormValues: (handler) => {
                    const model = handler.model;

                    model.weight = handler.$("#iptClientWeight").val();
                    model.height = handler.$("#iptClientHeight").val();
                    model.person.name = handler.$("#iptClientName").val();
                    model.person.birthday = handler.$("#iptClientBirthday").val();
                    model.person.profile = handler.profile;
                    model.user.email = handler.$("#iptClientEmail").val();
                    model.workouts = workoutRoutinesTab.models;

                    return model;
                }
            });
        }
    };
}();
