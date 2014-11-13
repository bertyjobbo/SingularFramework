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
Singular.Common.MasterApp = angular.module("Singular.Common.MasterApp", ["ng", "ngAnimate", "alt.route", "alt.ui", "alt.repeat"]);

// closure
(function (app) {

    // app controller
    app.controller("SingularMasterAppController", ["$scope", "$rootScope", "$timeout", function ($scope, $rootScope, $timeout) {

        // ui
        $rootScope.Ui = {

            // show loader?
            LoaderVisible: false,
            ShowLoader: function () {
                $rootScope.$broadcast("SgLoaderShow");
            },
            HideLoader: function () {
                $rootScope.$broadcast("SgLoaderHide");
            }

        };

        // show hide
        $rootScope.$on("SgLoaderShow", function () {
            console.log("showLoader")
            $rootScope.Ui.LoaderVisible = true;
        });
        $rootScope.$on("SgLoaderHide", function () {
            console.log("hideLoader")
            $rootScope.Ui.LoaderVisible = false;
        });


        // go
        $scope.Init = function () {
            
        }

    }]);

})(Singular.Common.MasterApp);