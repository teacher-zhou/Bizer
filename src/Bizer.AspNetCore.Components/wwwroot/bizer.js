import './bootstrap.bundle.min.js';

let collapse = {
    getInstance: function (element) {
        if (!element) {
            throw 'element is undefined';
        }

        var instance = new bootstrap.Collapse(element);
        if (instance) {
            return instance;
        }
        throw 'cannot get collapse instance';
    },
    hide: function (element) {
        let instance = collapse.getInstance(element);
        if (instance) {
            instance.hide();
        }
    },
    show: function (element) {
        let instance = collapse.getInstance(element);
        if (instance) {
            instance.show();
        }
    },
}

export { collapse };