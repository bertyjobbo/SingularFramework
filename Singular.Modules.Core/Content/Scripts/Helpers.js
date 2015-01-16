
// console.log switch
var origMethod = console.log;
console.log = function (a, b) {

    if (origMethod !== undefined) {

        if (window.DEBUG_MODE != undefined && window.DEBUG_MODE === true) {
            if (b === undefined) {
                origMethod(a, "   { Second parameter either not supplied or undefined (console.log(a,b)) }");
            }
            else {
                origMethod(a, b);
            }
        }
    }
}

// string helpers
var strng = {
    IsNullOrEmpty: function (str) {

        if (str === undefined || str === null)
            return true;
        if (!strng.IsString(str)) {            
            console.log(str);
            throw "Parameter 'str' in not a string - see preceding console.log entry.";
        }
        return str === "";
    },
    IsNullOrWhiteSpace: function (str) {
        if (strng.IsNullOrEmpty(str))
            return true;
        return str.match(/^\s*$/);
    },
    IsString: function (str) {
        return typeof str == 'string' || str instanceof String
    }
};

// object helpers
var objct = {

    IsEmpty: function (obj) {
        return Object.keys(obj).length === 0;
    },
    IsNullOrEmpty: function (obj) {
        return obj === undefined || obj === null || objct.IsEmpty(obj);
    }

}