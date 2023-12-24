"use strict";

const helperFunctions = function () {
    return {
        getBaseRoute: function (route) {
            return [
                window.location.origin,
                route
            ].join('/');
        },
        normalizePath: function (path) {
            return path.replace(/\\/g, '/');
        },
        loadSelectOptions: async function (options) {
            const defaultOptions = {
                $element: null,
                data: {
                    source: null
                },
                attributes: {
                    getValue: (data) => { },
                    getText: (data) => { },
                },
                extra: {
                    blankOption: false
                }
            };

            options = { ...defaultOptions, ...options };

            var dataSource = [];

            if (!options.data.source)
                throw new Error("No data source defined");
            if (typeof options.data.source === 'object')
                dataSource = options.data.source;
            if (typeof options.data.source === 'function') {
                const result = await options.data.source();
                if (result.statusCode !== helperConstants.statusCodes.OK) {
                    helperPopUp.dialog.warning(helperConstants.messages.unexpected, result.message);
                    return;
                }
                dataSource = [...result.data];
            }
            
            if (options.extra.blankOption) {
                const $option = $('<option>').text("").val("");
                options.$element.append($option);
            }

            dataSource.forEach((data) => {
                const value = options.attributes.getValue(data);
                const text = options.attributes.getText(data);

                const $option = $('<option>').text(text)
                    .data('option-obj', data).val(value);

                options.$element.append($option);
            });
        }
    };
}();