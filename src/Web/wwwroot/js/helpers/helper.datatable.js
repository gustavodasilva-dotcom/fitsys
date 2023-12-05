"use strict";

class helperDataTable {
    constructor(options) {
        return (async () => {
            this._normalizeOptions(options);
            await this._createDataTable();
        })();
    }
    $(elm) {
        return $(elm, this.options.context);
    }
    _normalizeOptions(options) {
        const defaultOptions = {
            wrapper: null,
            columns: [],
            actions: {
                get: {
                    route: null,
                    params: null,
                    fallback: null
                },
                post: {
                    route: null,
                    params: null,
                    fallback: null
                },
                delete: {
                    route: null,
                    params: null,
                    fallback: null
                }
            }
        };
        this.options = { ...defaultOptions, ...options };
    }
    async _getData() {
        const action = this.options.actions.get;

        if (!action)
            return;

        if (typeof action.fallback === 'function') {
            this.model = action.fallback();
        } else {
            const result = await $.get({
                async: true,
                type: "GET",
                url: helperFunctions.getBaseRoute(action.route),
                data: action.params ?? action.params
            });

            if (result.statusCode !== helperConstants.statusCodes.OK) {
                helperPopUp.dialog.warning(helperConstants.messages.unexpected, result.message);
                return;
            }

            this.model = result.data;
        }
    }
    async _createDataTable() {
        const _appendButtonOptions = () => {
            const postRoute = this.options.actions.post;

            const $btn = this.$('<a>').addClass('btn btn-primary mb-3');
            $btn.attr('href', helperFunctions.getBaseRoute(postRoute.route));
            $btn.text("New");

            this.$wrapper.append($btn);

            const $icon = this.$('<i>').addClass('bi bi-plus');
            $btn.append($icon);
        };

        const _createHead = () => {
            const $tr = this.$('<tr>');
            $tr.append(this.$('<th>').attr('scope', 'col').text('#'));

            this.options.columns.forEach((column) => {
                $tr.append(this.$('<th>').attr('scope', 'col').text(column.caption));
            });

            const $thead = this.$('<thead>');
            $thead.append($tr);

            this.$table.append($thead);
        };

        const _createRows = () => {
            const $body = this.$('<tbody>');
            var rowNumber = 0;

            this.model.forEach((data) => {
                rowNumber += 1;

                const $tr = this.$('<tr>');
                $tr.append(this.$('<td>').text(rowNumber));

                this.options.columns.forEach((column) => {
                    const value = column.getFieldValue(data);
                    $tr.append(this.$('<td>').text(value));
                });

                $body.append($tr);
            });

            this.$table.append($body);
        };

        const $table = this.$('<table>').addClass('table table-hover');
        this.$table = $table;

        this.$wrapper = $(this.options.wrapper);

        await this._getData();

        _appendButtonOptions();
        _createHead();
        _createRows();

        this.$wrapper.append(this.$table);
        this.tableInstane = this.$table.dataTable();
    }
};