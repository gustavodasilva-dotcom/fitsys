"use strict";

const helperConstantValues = function () {
    return {
        enums: {
            shifts: 1,
            muscle: 2
        },
        loadConstants: async function (options) {
            const defaultValues = {
                value: null,
                $element: null
            };

            options = { ...defaultValues, ...options };

            const { value, $element } = options;

            const result = await $.ajax({
                async: true,
                type: "GET",
                url: helperFunctions.getBaseRoute("Constants/Get"),
                data: { Constant: value }
            });

            if (result.statusCode !== helperConstants.statusCodes.OK) {
                helperPopUp.dialog.warning(helperConstants.messages.unexpected, result.message);
                return;
            }

            if ($element.attr('multiple'))
                $element.select2();
            else
                $element.append('<option>');

            const data = result.data;

            data.values.forEach((obj) => {
                const $option = $('<option>', $element);
                $option.val(obj.uid).text(obj.value);

                $element.append($option);
            });
        }
    };
}();