function generateUUID() {
    var d = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return uuid;
};
//合併2個js物件
function json_addProps(source, target) {
    if (!target)
        target = {};
    Object.keys(source).map(function (key, index) {
        target[key] = source[key];
    });
    return target;
}

//dataTables共同設定
function dataTables_config(tableSelector, url, inputConfig,pagerID) {

    pagerID = pagerID || 'page_number';

    var dt = $(tableSelector);
    var config = {
        processing: true,
        serverSide: true,
        filter: false,      //關閉文字搜尋欄位
        ordering: false,
        paginate: true,     //翻頁功能
        info: false,        //顯示表格的相關資訊，包括當前頁面紀錄，以及總記錄頁面數量
        pagingType: "full_numbers",
        //多國語
        language: {
            "emptyTable": "無資料...",
            "processing": "處理中...",
            "loadingRecords": "載入中...",
            "lengthMenu": "顯示 _MENU_ 項結果",
            "zeroRecords": "沒有符合的結果",
            "info": "顯示第 _START_ 至 _END_ 項結果，共 _TOTAL_ 項",
            "infoEmpty": "顯示第 0 至 0 項結果，共 0 項",
            "infoFiltered": "(從 _MAX_ 項結果中過濾)",
            "infoPostFix": "",
            "search": "搜尋:",
            "paginate": {
                "first": "第一頁",
                "previous": "上一頁",
                "next": "下一頁",
                "last": "最後一頁"
            },
            "aria": {
                "sortAscending": ": 升冪排列",
                "sortDescending": ": 降冪排列"
            }
        },
        //自訂工具列
        //dom: '<"toolbar">frtip',
        //dom: 't<"table-toolbar"<"col-md-4 col-sm-12 col-xs-12 page_total"i><"col-md-8 col-sm-12 col-xs-12 page_icon pc_page"p>>',
        dom: 't',
        drawCallback: function (settings) {
            var dataTables = this.api().table();

            //以下-設定[分頁]
            //var info = api.page.info();
            var info = this.api().page.info();
            var template = '';
            if (typeof langTotal === 'undefined' || langTotal === null)
                template = '總筆數：{0}，顯示{1}~{2}'; //template = '總筆數：{0}，顯示{1}~{2}，已選擇{3}';
            else
                template = langTotal + '：{0}，' + langDisplay + '{1}~{2}'; //template = langTotal + '：{0}，' + langDisplay + '{1}~{2}，' + langPageSize + '{3}';
            $('#' + pagerID).find('div.page_total').html(str_format(template, info.recordsDisplay, info.start + 1, info.end, info.length));
            $('#' + pagerID).find('#btnFirst').unbind('click').click(function (e) {
                e.preventDefault();
                //clearMobileList();
                dataTables.page('first').draw('page');;
            });
            $('#' + pagerID).find('#btnPrevious').unbind('click').click(function (e) {
                e.preventDefault();
                //clearMobileList();
                dataTables.page('previous').draw('page');;
            });
            $('#' + pagerID).find('#btnNext').unbind('click').click(function (e) {
                e.preventDefault();
                //clearMobileList();
                dataTables.page('next').draw('page');;
            });
            $('#' + pagerID).find('#btnLast').unbind('click').click(function (e) {
                e.preventDefault();
                //clearMobileList();
                dataTables.page('last').draw('page');;
            });

            $('#' + pagerID).find('#sltPage').html('');
            for (var i = 1; i <= info.pages; i++) {
                if (i == info.page + 1)
                    $('#' + pagerID).find('#sltPage').append(str_format('<option value="{0}" selected>{0}</option>', i));
                else
                    $('#' + pagerID).find('#sltPage').append(str_format('<option value="{0}">{0}</option>', i));
            }           

            $('#' + pagerID).find('#sltPage').change(function (e) {
                e.preventDefault();
                //clearMobileList();
                dataTables.page(parseInt($('#' + pagerID).find('#sltPage').val()) - 1).draw('page');;
            })
        },

        ajax: {
            url: url,
            data: function (arg) {
                arg.findJson = mySearch;    //以字串型式傳入
            },
            type:'post'
        }
    };

    //加入外部傳入的自定義組態
    if (inputConfig)
        config = json_addProps(inputConfig, config);

    return dt.DataTable(config);
}

function str_format() {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i + 1]);
    }
    return s;
}


function CheckTaiwanDate(obj) {
    var reg = /^[0-1]?\d{1,2}[0-1]\d[0-3]\d$/;
    
    return reg.test(obj);
}

// 驗證正整數
function CheckInteger(obj) {
    var reg = /^(0|[0-9]*[1-9][0-9]*)$/;
    // var reg = /^(0|[1-9][0-9]*)$/;
    return reg.test(obj);
}

function CheckIntegerGreater(obj) {
    var reg = /^[0-9]\d*$/;
    // var reg = /^(0|[1-9][0-9]*)$/;
    return reg.test(obj);
}

function CheckUserID(obj) {
    var reg = /^.[A-Za-z0-9]+$/;
    // var reg = /^(0|[1-9][0-9]*)$/;
    return reg.test(obj);
}

function CheckPassword(obj) {
    var reg = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,30}$/;
    // var reg = /^(0|[1-9][0-9]*)$/;
    return reg.test(obj);
}

function CheckYYYYMMDD(obj) {
    var reg = /^(19[5-9][0-9]|20[0-4][0-9]|2050)[-/](0?[1-9]|1[0-2])[-/](0?[1-9]|[12][0-9]|3[01])$/;
    return reg.test(obj);
}

function CheckYYYMMDD(obj) {
    var reg = /^(1[0-4][0-9])[-/](0?[1-9]|1[0-2])[-/](0?[1-9]|[12][0-9]|3[01])$/;
    var reg2 = /^(1[0-4][0-9])(0?[1-9]|1[0-2])(0?[1-9]|[12][0-9]|3[01])$/;
    return reg.test(obj) || reg2.test(obj);
}

function HtmlEncode(data) {
    return $('<div/>').text(value).html();
}

function GetCheckBoxValue() {
    return $('.xd-check1:not(:disabled):checked').map(function () { return $(this).val(); }).get().join(',');
}

function GetCheckBoxValueByClass(className) {

    return $('.' + className + ':not(:disabled):checked').map(function () { return $(this).val(); }).get().join(',');
}

function PostNonAjaxIframeBody(action, method, input, id) {
    debugger;
    var form;
    form = $('<form />', {
        action: action,
        method: method,
        style: 'display: none;',
        target: 'downloadIframe'
    });
    if (typeof input !== 'undefined') {
        $.each(input, function (name, value) {
            $('<input />', {
                type: 'hidden',
                name: name,
                value: value
            }).appendTo(form);
        });
    }
    form.appendTo('#' + id).submit().remove();
}

function BankSelectValue() {
    var level1 = $('#drpLevel1').val();
    var level2 = $('#drpLevel2').val() || '';
    var level3 = $('#drpLevel3').val() || '';

    if (level3 != '') {
        return level3;
    }
    else if (level2 != '') {
        return level2;
    }
    else if (level1 != '') {
        return level1;
    }
}



//身分證字號或外籍人士居留証驗證
/*
 * 第一個字元代表地區，轉換方式為：A轉換成1,0兩個字元，B轉換成1,1……但是Z、I、O分別轉換為33、34、35
 * 第二個字元代表性別，1代表男性，2代表女性
 * 第三個字元到第九個字元為流水號碼。
 * 第十個字元為檢查號碼。
 * 每個相對應的數字相乘，如A123456789代表1、0、1、2、3、4、5、6、7、8，相對應乘上1987654321，再相加。
 * 相加後的值除以模數，也就是10，取餘數再以模數10減去餘數，若等於檢查碼，則驗證通過
 */
function studIdNumberIdentify(nationality, idNumber) {

    studIdNumber = idNumber.toUpperCase();

    //本國人
    if (nationality == 0) {

        //驗證填入身分證字號長度及格式
        if (studIdNumber.length != 10) {
            //alert("長度不足");
            return false;
        }
        //格式，用正則表示式比對第一個字母是否為英文字母
        if (isNaN(studIdNumber.substr(1, 9)) ||
            (!/^[A-Z]$/.test(studIdNumber.substr(0, 1)))) {
            //alert("格式錯誤");
            return false;
        }

        var idHeader = "ABCDEFGHJKLMNPQRSTUVXYWZIO"; //按照轉換後權數的大小進行排序
        //這邊把身分證字號轉換成準備要對應的
        studIdNumber = (idHeader.indexOf(studIdNumber.substring(0, 1)) + 10) + '' + studIdNumber.substr(1, 9);
        //開始進行身分證數字的相乘與累加，依照順序乘上1987654321
        s = parseInt(studIdNumber.substr(0, 1)) +
            parseInt(studIdNumber.substr(1, 1)) * 9 +
            parseInt(studIdNumber.substr(2, 1)) * 8 +
            parseInt(studIdNumber.substr(3, 1)) * 7 +
            parseInt(studIdNumber.substr(4, 1)) * 6 +
            parseInt(studIdNumber.substr(5, 1)) * 5 +
            parseInt(studIdNumber.substr(6, 1)) * 4 +
            parseInt(studIdNumber.substr(7, 1)) * 3 +
            parseInt(studIdNumber.substr(8, 1)) * 2 +
            parseInt(studIdNumber.substr(9, 1));

        checkNum = parseInt(studIdNumber.substr(10, 1));
        //模數 - 總和/模數(10)之餘數若等於第九碼的檢查碼，則驗證成功
        //若餘數為0，檢查碼就是0
        if ((s % 10) == 0 || (10 - s % 10) == checkNum) {
            return true;
        }
        else {
            return false;
        }

    }
    //外籍生，居留證號規則跟身分證號差不多，只是第二碼也是英文字母代表性別，跟第一碼轉換二位數字規則相同，但只取餘數
    else {

        //驗證填入身分證字號長度及格式
        if (studIdNumber.length != 10) {
            //alert("長度不足");
            return false;
        }
        //格式，用正則表示式比對第一個字母是否為英文字母
        if (isNaN(studIdNumber.substr(2, 8)) ||
            (!/^[A-Z]$/.test(studIdNumber.substr(0, 1))) ||
            (!/^[A-Z]$/.test(studIdNumber.substr(1, 1)))) {
            //alert("格式錯誤");
            return false;
        }

        var idHeader = "ABCDEFGHJKLMNPQRSTUVXYWZIO"; //按照轉換後權數的大小進行排序
        //這邊把身分證字號轉換成準備要對應的
        studIdNumber = (idHeader.indexOf(studIdNumber.substring(0, 1)) + 10) +
            '' + ((idHeader.indexOf(studIdNumber.substr(1, 1)) + 10) % 10) + '' + studIdNumber.substr(2, 8);
        //開始進行身分證數字的相乘與累加，依照順序乘上1987654321

        s = parseInt(studIdNumber.substr(0, 1)) +
            parseInt(studIdNumber.substr(1, 1)) * 9 +
            parseInt(studIdNumber.substr(2, 1)) * 8 +
            parseInt(studIdNumber.substr(3, 1)) * 7 +
            parseInt(studIdNumber.substr(4, 1)) * 6 +
            parseInt(studIdNumber.substr(5, 1)) * 5 +
            parseInt(studIdNumber.substr(6, 1)) * 4 +
            parseInt(studIdNumber.substr(7, 1)) * 3 +
            parseInt(studIdNumber.substr(8, 1)) * 2 +
            parseInt(studIdNumber.substr(9, 1));

        //檢查號碼 = 10 - 相乘後個位數相加總和之尾數。
        checkNum = parseInt(studIdNumber.substr(10, 1));
        //模數 - 總和/模數(10)之餘數若等於第九碼的檢查碼，則驗證成功
        ///若餘數為0，檢查碼就是0
        if ((s % 10) == 0 || (10 - s % 10) == checkNum) {
            return true;
        }
        else {
            return false;
        }

    }

}

function checkTwID(id) {
    //建立字母分數陣列(A~Z)
    var city = new Array(1, 10, 19, 28, 37, 46, 55, 64, 39, 73, 82, 2, 11, 20, 48, 29, 38, 47, 56, 65, 74, 83, 21, 3, 12, 30)
    id = id.toUpperCase();
    //使用「正規表達式」檢驗格式
    if (id.search(/^[A-Z](1|2)\d{8}$/i) == -1) {
        return false;
    } else {
        //將字串分割為陣列(IE必需這麼做才不會出錯)
        id = id.split('');
        //計算總分
        var total = city[id[0].charCodeAt(0) - 65];
        for (var i = 1; i <= 8; i++) {
            total += eval(id[i]) * (9 - i);
        }
        //補上檢查碼(最後一碼)
        total += eval(id[9]);
        //檢查比對碼(餘數應為0);
        return ((total % 10 == 0));
    }
}

function DateBetweenVaild(from, to) {
    if ($('#' + from).val() != '' && $('#' + to).val() != '') {
        var fromDate = new Date($('#' + from).val());
        var toDate = new Date($('#' + to).val());

        if (toDate < fromDate) {
            return false;
        }
        else {
            return true;
        }
    }
    else {
        return true;
    }
}

function DateClear(from, to) {
    $('#' + from).val('');
    $('#' + to).val('');
}

function Pop(str) {
    $('#popMessage').find('.modal-body').text(str);
    $('#popMessage').modal('show');
    setTimeout(function () {
        $("#popMessage").modal('hide');
    }, 900);
}