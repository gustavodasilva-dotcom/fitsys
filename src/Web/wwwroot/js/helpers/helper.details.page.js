"use strict";

class helperDetailsPage {
    constructor(options) {
        return (async () => {
            this._normalizeOptions(options);
            await this._setupEvents();
            await this._getDetailsData();
        })();
    }
    $ = function (elm) {
        return $(elm, this.options.wrapper);
    }
    _normalizeOptions = function (options) {
        const defaultOptions = {
            wrapper: null,
            footer: {
                submitButton: null,
                resetButton: null
            },
            routes: {
                post: {
                    path: null,
                    callback: () => { }
                },
                put: {
                    path: null,
                    param: null,
                    callback: () => { }
                }
            },
            defaultValues: {},
            validate: () => { },
            setupEvents: () => { },
            fillFormInputs: () => { },
            getFormValues: () => { }
        };
        this.options = { ...defaultOptions, ...options };
    }
    _getDetailsData = async function () {
        const getRoute = this.options.routes.get;

        const params = new URL(window.location.href);
        const value = params.searchParams.get(getRoute.param);

        if (value) {
            const data = new Object();
            data[getRoute.param] = value;

            const result = await $.ajax({
                async: true,
                type: "GET",
                url: helperFunctions.getBaseRoute(getRoute.path),
                data
            });

            if (result.statusCode !== helperConstants.statusCodes.OK) {
                helperPopUp.dialog.warning(helperConstants.messages.unexpected, result.message);
                return;
            }

            this.model = result.data;
            this.options.fillFormInputs(this);
        }
    }
    _sendForm = async function (e) {
        e.preventDefault();

        const validation = this.options.validate(this);

        if (validation && !validation.isValid) {
            if (validation.callback) validation.callback(this);
            helperPopUp.dialog.warning(helperConstants.messages.unexpected, validation.message);
            return;
        }

        this.model = { ...this.options.defaultValues, ...this.model };
        this.model = this.options.getFormValues(this);

        const ajaxParams = {
            async: true,
            contentType: "application/json",
            dataType: "json"
        };

        const getRoute = this.options.routes.get;

        const params = new URL(window.location.href);
        const value = params.searchParams.get(getRoute.param);

        var callback = () => { };

        if (value) {
            const putRoute = this.options.routes.put;
            callback = putRoute.callback;

            const data = new Object();
            data[putRoute.param] = value;

            Object.assign(ajaxParams, {
                type: "PUT",
                url: [
                    helperFunctions.getBaseRoute(putRoute.path),
                    new URLSearchParams(data).toString()
                ].join('?'),
                data: JSON.stringify(this.model)
            });
        } else {
            const postRoute = this.options.routes.post;
            callback = postRoute.callback;

            Object.assign(ajaxParams, {
                type: "POST",
                url: helperFunctions.getBaseRoute(postRoute.path),
                data: JSON.stringify(this.model)
            });
        }

        const result = await $.ajax(ajaxParams);

        if (result.statusCode !== helperConstants.statusCodes.OK) {
            helperPopUp.dialog.warning(helperConstants.messages.unexpected, result.message);
            return;
        }

        callback(this);
    }
    _setupEvents = async function () {
        await this.options.setupEvents(this);
        this.$(this.options.footer.submitButton).click((e) => { this._sendForm(e) });
    }
};