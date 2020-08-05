
var _str = {

    //variables is empty or not
    isEmpty: function (data) {
        return (data === undefined || data == null || data == '')
    },

    //format string like c# String.Format()
    format: function () {
        var s = arguments[0];
        for (var i = 0; i < arguments.length - 1; i++) {
            var reg = new RegExp("\\{" + i + "\\}", "gm");
            s = s.replace(reg, arguments[i + 1]);
        }
        return s;
    },

    //傳回切除首尾的字串
    getCut2: function (str, find1, find2) {
        if (_str.isEmpty(str))
            return '';
        var pos = str.indexOf(find1);
        if (pos < 0)
            return str;
        var pos2 = str.indexOf(find2, pos + 1);
        return (pos2 < 0)
            ? str.substring(pos + find1.length)
            : str.substring(pos + find1.length, pos2)
    },

    toBool: function (val) {
        return (val == '1' || val == true || val == 'True');
    },

    /* move to _html.js
    //see: https://stackoverflow.com/questions/14346414/how-do-you-do-html-encode-using-javascript
    htmlEncode: function (value) {
        return $('<div/>').text(value).html();
    },
    */

}; //class