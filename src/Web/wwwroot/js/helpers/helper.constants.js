"use strict";

const helperConstants = function () {
    return {
        statusCodes: {
            OK: 200,
            BadRequest: 400
        },
        messages: {
            unexpected: "Oh..."
        },
        enums: {
            shifts: {
                values: {
                    Morning: 1,
                    Afternoon: 2,
                    Night: 3
                },
                loadToCombo: function ($elm) {
                    const keys = Object.keys(this.values);

                    if ($elm.attr('multiple'))
                        $elm.select2();
                    else
                        $elm.append('<option>');
                    
                    keys.forEach((key) => {
                        const value = this.values[key];

                        const $option = $('<option>', $elm);
                        $option.val(value).text(key);

                        $elm.append($option);
                    });
                }
            }
        }
    }
}();