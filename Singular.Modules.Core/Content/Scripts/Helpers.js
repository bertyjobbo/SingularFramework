var strng = {
    IsNullOrEmpty: function (str) {
        return str === undefined || str === null || str === "";
    },
    IsNullOrWhiteSpace: function (str) {
        return strng.IsNullOrEmpty(str) || str.match(/^\s*$/);
    }
};