/*
 根據專案內容, 自訂函數內容
 */
var _xp = {

    dtCheckBox: function (key, status) {
        var str = "<label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'>" +
            "     <input type='checkbox' class='checkboxes xd-check1'{0} value='{1}'>" +
            "     <span></span>" +
            "</label>";
        return _str.format(str, status ? ' disabled' : '', key);
    },
    dtCheckCustomBox: function (key, status, className) {
        var str = "<label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'>" +
            "     <input type='checkbox' class='checkboxes " + className + "'{0} value='{1}'>" +
            "     <span></span>" +
            "</label>";
        return _str.format(str, status ? ' disabled' : '', key);
    },

    //dt欄位: Status(checkbox)
    //row checkbox欄位 for set status
    //onClickFn : 自定義function
    dtCheckStatus: function (key, status, table, column, disabled, keyColumn, column2, colunm2value) {

        column2 = column2 || "";
        colunm2value = colunm2value || "";

        status = (status == '1' || status == 'True');
        var str = "<label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'>" +
            "     <input type='checkbox' class='checkboxes ' {0} {4} onclick=_xp.onClickSetStatus(this,\'{1}\',\'{2}\',\'{3}\',\'{5}\',\'{6}\',\'{7}\')>" +
            "     <span id='checkAll'></span>" +
            "</label>";

        return _str.format(str, status ? ' checked' : '', key, table, column, disabled ? 'disabled' : '', keyColumn,column2,colunm2value);
    },
    //更新status
    onClickSetStatus: function (me, key, table, column, keyColumn, column2, colunm2value) {
       
        column2 = column2 || "";
        colunm2value = colunm2value || "";

        var status = $(me).is(':checked');   //me為checkbox !!

        $.ajax({
            url: '../Common/SetStatus',
            data: {
                key: key,
                status: status,
                table: table,
                column: column,
                keyColumn: keyColumn,
                column2: column2,
                colunm2value: colunm2value
            },
            type: 'post'
        }).then(function (data) {
            if (data == "True") {
                alert('資料更新完成。');
            }
            else {
                alert('資料更新失敗。');
            }

        })

    },
    Sort: function (id, action, tableName, column, column2, columnValue, column3, column3Value, callBack) {

        column2 = column2 || "";  //default value
        columnValue = columnValue || ""; //default value
        column3 = column3 || "";
        column3Value = column3Value || "";

        $.ajax({
            url: '../Common/sort',
            data: {
                table: tableName,
                id: id,
                action: action,
                column: column,
                column2: column2,
                columnValue: columnValue,
                column3: column3,
                column3Value: column3Value
            },
            type: 'post',
            dataType: 'json'
        }).then(function (data) {
            if (data.result) {
                alert('資料更新完成。');

                callBack = callBack || SearchData;
                callBack();
            }
            else {
                alert(data.message);
            }

        })
    },
    SortChange: function (id, obj, tableName, column, column2, columnValue, column3, column3Value,callBack) {

        column2 = column2 || "";  //default value
        columnValue = columnValue || ""; //default value
        column3 = column3 || "";
        column3Value = column3Value || "";

        var target = $(obj).val();

        if (!CheckInteger(target)) {
            alert('必須輸入整數！');
        }
        else if (target == "0") {
            alert('不得輸入0！');
        }
        else {
            $.ajax({
                url: '../Common/sortChange',
                data: {
                    table: tableName,
                    id: id,
                    target: target,
                    column: column,
                    column2: column2,
                    columnValue: columnValue,
                    column3: column3,
                    column3Value: column3Value
                },
                dataType: 'json',
                type: 'post'
            }).then(function (data) {
                if (data.result) {
                    alert('資料更新完成。');

                    callBack = callBack || SearchData;
                    callBack();
                }
                else {
                    alert(data.message);
                }
            })
        }

    },
    onClickCustomAll: function (status, className) {
        if (status == true)
            $('.' + className + ':not(:disabled)').prop("checked", true);
        else
            $('.' + className + '').prop("checked", false);
    },
    onClickCustomSub: function (chkClassName,tableID, chkAllID) {
        
        $("#" + chkAllID).prop("checked", $('#' + tableID).find('.' + chkClassName +':not(:checked)').length == 0);
    },
    onClickAll: function (status) {
        if (status == true)
            $('.xd-check1:not(:disabled)').prop("checked", true);
        else
            $('.xd-check1').prop("checked", false);
    },
    //單一 Table
    onClickAll2: function (status) {
        if (status == true) {
            $('.xd-check1:not(:disabled)').prop("checked", true);
            $('.xd-check2:not(:disabled)').prop("checked", true);
        }

        else {
            $('.xd-check1').prop("checked", false);
            $('.xd-check2').prop("checked", false);
        }

    },
    onClickMiddle: function (obj, status) {

        if (status == true) {
            $(obj).parents('tr').find('.xd-check2:not(:disabled)').prop("checked", true);
        }
        else {
            $(obj).parents('tr').find('.xd-check2').prop("checked", false);
        }

        if ($('.xd-check2:not(:checked)').length == 0) {
            $('#chkAll').prop('checked', true);
        }
        else {
            $('#chkAll').prop('checked', false);
        }

    },
    onClickLast: function (obj) {
        if ($(obj).parents('tr').find('.xd-check2:not(:checked)').length == 0) {
            $(obj).parents('tr').find('.xd-check1').prop('checked', true);
            this.onClickMiddle($(obj).parents('tr').find('.xd-check1'), true);
        }
        else {
            $(obj).parents('tr').find('.xd-check1').prop('checked', false);
            $('#chkAll').prop('checked', false);
        }
    },
    //Multi Table
    onClickCustomAll2: function (status, tableID) {       
        if (status == true) {
            $('#' + tableID).find('.xd-check1:not(:disabled)').prop("checked", true);
            $('#' + tableID).find('.xd-check2:not(:disabled)').prop("checked", true);
        }
        else {
            $('#' + tableID).find('.xd-check1').prop("checked", false);
            $('#' + tableID).find('.xd-check2').prop("checked", false);
        }

    },   
    onClickCustomMiddle: function (obj, status, tableID,chkAllID) {       
        if (status == true) {
            $(obj).parents('tr').find('.xd-check2:not(:disabled)').prop("checked", true);
        }
        else {
            $(obj).parents('tr').find('.xd-check2').prop("checked", false);
        }

        if ($('#' + tableID).find('.xd-check2:not(:checked)').length == 0) {
            $('#' + chkAllID).prop('checked', true);
        }
        else {
            $('#' + chkAllID).prop('checked', false);
        }

    },
    onClickCustomLast: function (obj, tableID, chkAllID) {
        if ($(obj).parents('tr').find('.xd-check2:not(:checked)').length == 0) {
            $(obj).parents('tr').find('.xd-check1').prop('checked', true);
            this.onClickCustomMiddle($(obj).parents('tr').find('.xd-check1'), true, tableID, chkAllID);
        }
        else {
            $(obj).parents('tr').find('.xd-check1').prop('checked', false);
            $('#' + chkAllID).prop('checked', false);
        }
    },   
    onClickMenuMiddle: function (obj, tableID, chkAllID, parentID) {      
        if (obj != undefined) {
            var that = this;
            var status = $(obj).prop('checked');
            $('#' + tableID).find('input[data-parent=' + $(obj).prop('id') + ']').prop('checked', status);
            $('#' + tableID).find('input[data-parent=' + $(obj).prop('id') + ']').each(function () {
                if ($('#' + tableID).find('input[data-parent=' + $(this).prop('id') + ']').length > 0) {
                    that.onClickMenuMiddleRe(this, tableID, chkAllID, $(this).data('parent'));
                }
                
            })
        }  
        
        if ($('#' + tableID).find(".checkitem:not(:checked)").length == 0) {
            $('#' + chkAllID).prop('checked', true);
        }
        else {

            $('#' + chkAllID).prop('checked', false);
        }
  
        if ($(obj).prop('checked')) {
            $('#' + tableID).find('#' + parentID).first().prop('checked', true);
        }
        else {
            $('#' + tableID).find('#' + parentID).first().prop('checked', false);
        }

    },
    onClickMenuMiddleRe: function (obj, tableID, chkAllID, parentID) {       
        if (obj != undefined) {
            var that = this;
            var status = $(obj).prop('checked');
            $('#' + tableID).find('input[data-parent=' + $(obj).prop('id') + ']').prop('checked', status);
        
            $('#' + tableID).find('input[data-parent=' + $(obj).prop('id') + ']').each(function () {
                if ($('#' + tableID).find('input[data-parent=' + $(this).prop('id') + ']').length > 0) {
                    that.onClickMenuMiddle(this, tableID, chkAllID, $(this).data('parent'));
                }                
            })
        }
       

        if ($('#' + tableID).find(".checkitem:not(:checked)").length == 0) {
            $('#' + chkAllID).prop('checked', true);
        }
        else {

            $('#' + chkAllID).prop('checked', false);
        }
    },
    onClickMenuLast: function (obj, tableID, chkAllID, parentID,topID) {

        if ($('#' + tableID).find(".checkitem:not(:checked)").length == 0) {
            $('#' + chkAllID).prop('checked', true);
        }
        else {
            
            $('#' + chkAllID).prop('checked', false);
        }

        if ($(obj).prop('checked')) {            
            $('#' + parentID).prop('checked', true);
            $('#' + tableID).find('#' + topID).prop('checked', true);
        }

    },
    onDeleteAll: function (table, column, callBack) {

        if (!confirm('確認是否刪除該筆資料？刪除請按「確定」。')) {
            return false;
        }
        
        $.ajax({
            url: '../Common/DeleteAll',
            data: {
                table: table,
                column: column,
                ids: GetCheckBoxValue()
            },
            type: 'post',
            dataType: 'json'
        }).then(function (data) {
            if (data.result) {
                alert('資料更新完成。');

                callBack = callBack || SearchData;
                callBack();
            }
            else {
                alert(data.message);
            }
        })
    },
    onDeleteAll2: function (table, column, column2, columnValue) {

        if (!confirm('確認是否刪除該筆資料？刪除請按「確定」。')) {
            return false;
        }

        $.ajax({
            url: '../Common/DeleteAll2',
            data: {
                table: table,
                column: column,
                column2: column2,
                columnValue: columnValue,
                ids: GetCheckBoxValue()
            },
            type: 'post',
            dataType: 'json'
        }).then(function (data) {
            if (data.result) {
                alert('資料更新完成。');
                SearchData();
            }
            else {
                alert(data.message);
            }
        })
    },
    RemoveCheck: function (chkClassName) {
        $('.' + chkClassName + ':checked').parent().parent().parent().remove();
    },
    onChangeFile: function (id, me) {
        if (_str.isEmpty(me.value)) {
            _file.deletePath(id, _me.formEdit);
            //檢查檔案大小 50M
        } else if (me.files[0].size > 50971520) {
            _tool.msg('上傳檔案不可大於50M !');
            me.value = '';
            //return false;
        } else {
            debugger;
            _file.addPath(id, me.value, _me.formEdit);
        }
    },
    onClickDeleteFile: function (id) {
        _file.deletePath(id, _me.formEdit);
    }

};//_xp