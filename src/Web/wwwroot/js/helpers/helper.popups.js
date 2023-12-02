"use strict";

const helperPopUp = function () {
    return {
        _icons: {
            success: "success",
            error: "error",
            warning: "warning",
            info: "info",
            question: "question"
        },
        toast: {
            _base: Swal.mixin({
                toast: true,
                position: "top-end",
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.onmouseenter = Swal.stopTimer;
                    toast.onmouseleave = Swal.resumeTimer;
                }
            }),
            success: function (title, options) {
                this._base.fire({
                    ...options,
                    icon: helperPopUp._icons.success,
                    title
                });
            },
            error: function (title, options) {
                this._base.fire({
                    ...options,
                    icon: helperPopUp._icons.error,
                    title
                });
            }
        },
        dialog: {
            _base: async function (options) {
                const defaultOptions = {
                    title: null,
                    text: null,
                    icon: helperPopUp._icons.warning,
                    input: null,
                    inputLabel: "",
                    inputPlaceholder: "",
                    inputValue: "",
                    inputAttributes: {},
                    showCancelButton: false,
                    showConfirmButton: true,
                    confirmButtonText: "Confirmar",
                    cancelButtonText: "Cancelar",
                    customClass: {
                        htmlContainer: "popup-htmlContainer"
                    },
                    validator: {
                        validateEmpty: true,
                        emptyMessage: "Message cannot be null or empty."
                    },
                    reverseButtons: false
                };
                const option = { ...defaultOptions, ...options }
                if (option.validator.validateEmpty)
                    option.inputValidator = (value) => {
                        if (!value) {
                            return option.validator.emptyMessage;
                        }
                    };
                return Swal.fire(option);
            },
            confirm: async function (title, text) {
                return this._base({
                    icon: helperPopUp._icons.question,
                    showCancelButton: true,
                    showConfirmButton: true,
                    confirmButtonText: "Sim",
                    cancelButtonText: "Não",
                    title,
                    text: text.replaceAll('\r\n', '<br>')
                });
            },
            warning: async function (title, text) {
                return this._base({
                    icon: helperPopUp._icons.warning,
                    showCancelButton: false,
                    showConfirmButton: true,
                    confirmButtonText: "OK",
                    title,
                    text: text.replaceAll('\r\n', '<br>')
                });
            },
            question: async function (title, text) {
                return this._base({
                    icon: helperPopUp._icons.question,
                    showCancelButton: true,
                    showConfirmButton: true,
                    confirmButtonText: "Sim",
                    cancelButtonText: "Não",
                    title,
                    text: text.replaceAll('\r\n', '<br>')
                });
            }
        }
    };
}();