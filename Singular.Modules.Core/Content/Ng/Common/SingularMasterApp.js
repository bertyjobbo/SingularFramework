/// <reference path="C:\ffd248e91f0c43bbbe5921e4\Git\SingularFramework\Singular.Modules.Core\Scripts/angular.js" />
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
Singular.Common.MasterApp = angular.module("Singular.Common.MasterApp", ["ng", "Singular.Common.SingularCommon", "ngAnimate", "alt.route", "alt.ui", "alt.repeat"]);

// closure
(function (app) {

    // app controller
    app.controller("SingularMasterAppController", ["$scope", "$rootScope", "$timeout", "$location", function ($scope, $rootScope, $timeout, $location) {

        
        // go
        $scope.Init = function () {

        }

    }]);    

})(Singular.Common.MasterApp);