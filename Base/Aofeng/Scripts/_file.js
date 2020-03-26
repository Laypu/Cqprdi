
//檔案上傳欄位(單筆) & 檔案處理
var _file = {

    //get label filter of file
    labelF: function(id) {
        return '.xd-' + id + '-label';
    },
    //get file filter
    fileF: function (id) {
        return '.xd-' + id;
    },

    //初始化, 在一開始載入畫面資料時呼叫
    init: function(id, path, form) {
        //_obj.getF(_file.fileF(id), form).val('');
        var file = _obj.getF(_file.fileF(id), form);
        file.val('');
        file.data('fun', '');
        _file.setPath(id, path, form);
        /*
        //file element 要 reset
        var file = _obj.getF(_file.fileF(id), form);
        //var $el = $('#example-file');
        file.wrap('<form>').closest('form').get(0).reset();
        file.unwrap();
        */
    },

    //顯示路徑(檔名only)
    setPath: function (id, path, form) {
        var label = _obj.getF(_file.labelF(id), form);
        var link = label.find('a');
        var fileName = _file.getFileName(path);
        link.text(fileName);    //儲存路徑的地方
        link.attr('href', path);

        //顯示label和刪除link
        if (_str.isEmpty(fileName))
            label.hide();
        else
            label.show();
    },

    getFileName: function(path) {
        var name = path;
        var pos = path.lastIndexOf('/');
        if (pos > 0)
            name = path.substring(pos + 1);
        return name;
    },

    //private
    getObject: function (id, form) {
        return _obj.getF(_file.fileF(id), form);
    },

    //增加一個路徑
    addPath: function (id, path, form) {
        var obj = _file.getObject(id, form);
        _file.setStatus(obj, 'A');
        _file.setPath(id, path, form);
    },

    //刪除路徑(設定 data-deleted 屬性)
    deletePath: function (id, form) {
        var obj = _file.getObject(id, form);
        _file.setStatus(obj, 'D');
        _file.setPath(id, '', form);
    },

    //private
    setStatus: function(obj, status) {
        $(obj).data('fun', status);
    },

    //路徑是否刪除
    isDeleted: function (id, form) {
        var obj = _file.getObject(id, form);
        return (obj.data('fun') == "D");
    },

    //是否有 "異動" 的上傳檔案
    getFile: function (id, form) {
        var obj = _file.getObject(id, form);
        var files = obj.get(0).files;
        return (files.length > 0) ? files[0] : null;
    },

    //是否上傳檔案, 包含原本存在和後來選取的檔案(判斷link內容是否空白)
    //page沒有foucs時, visible會傳回false, 所以在這裡判斷 href
    hasFile: function (id, form) {
        //return _obj.getF(_file.labelF(id), form).find('a').is(':visible');
        return !_str.isEmpty(_obj.getF(_file.labelF(id), form).find('a').attr('href'));
    },

    //row add file for upload & save row
    rowAddFile: function (data, row, id, serverId, fm) {
        var file = _file.getFile(id, fm);
        if (file != null)
            data.append(serverId, file);
        else if (_file.isDeleted(id, fm))
            row['_' + id] = 1;

    },
}; //class