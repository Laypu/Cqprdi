
//jquery object
var _obj = {

    get: function (id, form) {
        return (form === undefined) ? $('#' + id) : form.find('#' + id);
    },
    getF: function (filter, form) {
        return (form === undefined) ? $(filter) : form.find(filter);
    },

    isShow: function (obj) {
        return obj.is(':visible');
    },

    //檢查欄位是否存在, true/fales
    exist: function (id, form) {
        return _obj.existF('#' + id, form);
    },

    existF: function (filter, form) {
        var field = (form === undefined) ? $(filter) : form.find(filter);
        return (field.length > 0);
    },

}; //class