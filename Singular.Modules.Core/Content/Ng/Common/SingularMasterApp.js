﻿/// <reference path="C:\ffd248e91f0c43bbbe5921e4\Git\SingularFramework\Singular.Modules.Core\Scripts/angular.js" />
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
            $timeout(function () {
                $rootScope.Ui.LoaderVisible = true;
            }, 500);            
        });
        $rootScope.$on("SgLoaderHide", function () {
            $rootScope.Ui.LoaderVisible = false;
        });


        // go
        $scope.Init = function () {
            
        }

    }]);

})(Singular.Common.MasterApp);