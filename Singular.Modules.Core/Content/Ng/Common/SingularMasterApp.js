/// <reference path="C:\Git\SingularFramework\Singular.Modules.Core\Scripts/angular.js" />
'use strict';

/*
 * SingularMasterApp
 * 
 * Common functionality / controllers etc
 * 
 */

// check existing
if (Singular == undefined) var Singular = {};
if (Singular.Common == undefined) Singular.Common = {};
 
// master app
Singular.Common.MasterApp = angular.module("Singular.Common.MasterApp", ["ng","alt.route","alt.ui","alt.repeat"]);

// closure
(function (app) {
    
    // app controller
    app.controller("SingularMasterAppController", ["$scope", function ($scope) {
        
    }]);

})(Singular.Common.MasterApp);