import './bootstrap.bundle.min.js';

let util = {
    getElementById: function (elementId) {
        if (!elementId) {
            throw 'element id "' + elementId + '" is undifined';
        }
        return document.querySelector('#' + elementId);
    },
    getElementByRef: function (elementRef) {
        if (!elementRef) {
            throw 'element "' + elementRef + '" is undifined';
        }
        return elementRef;
    }
}

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

let modal = {
    instance: null,
    open: function (elementRef, options, dotNetReference) {
        let modalElement = util.getElementByRef(elementRef);

        modalElement.addEventListener('show.bs.modal', e => {
            console.log('show.bs.modal event is raised');
            dotNetReference.invokeMethodAsync("OnOpeningAsync");
        });

        modalElement.addEventListener('shown.bs.modal', e => {
            console.log('shown.bs.modal event is raised');
            dotNetReference.invokeMethodAsync("OnOpenedAsync");
        });

        modalElement.addEventListener('hide.bs.modal', e => {
            console.log('hide.bs.modal event is raised');
            dotNetReference.invokeMethodAsync("OnClosingAsync");
        });

        modalElement.addEventListener('hidden.bs.modal', e => {
            console.log('hidden.bs.modal event is raised');
            dotNetReference.invokeMethodAsync("OnClosedAsync");
        });

        this.instance = new bootstrap.Modal(modalElement, options);

        this.instance.show();
    },
    close: function (elementRef, options, dotNetReference) {
        this.instance.hide();
        //this.instance.dispose();
    }
}

// ------------------动画效果没搞定，暂时不用以下代码------------------
//let toast = {
//    instance: null,
//    show: function (elementRef, options,dotnetReference) {

//        let element = util.getElementByRef(elementRef);

//        element.addEventListener('shown.bs.toast', e => {
//            console.log('shown.bs.toast event is raised');

//            setTimeout(() => {
//                element.classList.remove('fade');
//            }, 100);
//        });
//        element.addEventListener('hide.bs.toast', e => {
//            console.log('hide.bs.toast event is raised');

//            element.classList.add('fade');
//        });
//        element.addEventListener('hidden.bs.toast', e => {
//            console.log('hidden.bs.toast event is raised');
//            dotnetReference.invokeMethodAsync("OnHiddenAsync");

//            element.classList.remove('fade', 'hide');
//        });

//        this.instance = new bootstrap.Toast(element, options);

//        this.instance.show();

//    },
//    hide: function () {
//        this.instance.hide();
//    }
//}
//----------------------------------------------------

export { collapse, modal };