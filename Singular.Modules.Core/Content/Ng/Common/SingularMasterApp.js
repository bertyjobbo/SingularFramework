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
Singular.Common.MasterApp = angular.module("Singular.Common.MasterApp", ["ng", "ngAnimate", "alt.route", "alt.ui", "alt.repeat"]);

// closure
(function (app) {

    // app controller
    app.controller("SingularMasterAppController", ["$scope", "$rootScope", "$timeout", "$location", function ($scope, $rootScope, $timeout, $location) {

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

        // rooted url
        $rootScope.RootedUrl = function (restOfUrl) {
            return window.ROOT_URL + restOfUrl;
        }

        // show hide
        $rootScope.$on("SgLoaderShow", function () {

            $rootScope.Ui.LoaderVisible = true;

        });
        $rootScope.$on("SgLoaderHide", function () {
            $rootScope.Ui.LoaderVisible = false;
        });

        // show hide when loading views
        $scope.$on("$routeChangeStart", function () {

            $rootScope.Ui.LoaderVisible = true;

        });
        $scope.$on("$routeChangeSuccess", function () {

            $rootScope.Ui.LoaderVisible = false;
        });
        $scope.$on("$routeChangeError", function (rout) {
            window.location = $rootScope.RootedUrl("Singular/Core/Sys/PageNotFound");
        });

        // alert
        $rootScope.Alert = function (str) {
            alert(str);
        };

        // log
        $rootScope.Log = function (obj1, obj2) {
            if (obj2)
                console.log(obj1, obj2);
            else
                console.log(obj1);

        };


        // go
        $scope.Init = function () {

        }

    }]);

})(Singular.Common.MasterApp);