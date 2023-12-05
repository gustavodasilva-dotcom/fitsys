"use strict";

class helperDetailsPage {
    constructor(options) {
        return (async () => {
            this._normalizeOptions(options);
            await this._getDetailsData();
            this._setupEvents();
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
                    params: {}
                },
                put: {
                    path: null,
                    param: null
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

        this.model = { ...this.model, ...this.options.defaultValues };
        this.model = this.options.getFormValues(this);

        const postRoute = this.options.routes.post;

        const ajaxParams = {
            async: true,
            type: "POST",
            url: helperFunctions.getBaseRoute(postRoute.path),
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(this.model)
        };
        
        const result = await $.ajax(ajaxParams);

        if (result.statusCode !== helperConstants.statusCodes.OK) {
            helperPopUp.dialog.warning(helperConstants.messages.unexpected, result.message);
            return;
        }
    }
    _setupEvents = function () {
        this.options.setupEvents(this);
        this.$(this.options.footer.submitButton).click((e) => { this._sendForm(e) });
    }
};