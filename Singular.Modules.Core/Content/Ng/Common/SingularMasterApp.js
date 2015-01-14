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
        $scope.$on("$routeChangeError", function (e, errObj, code, route) {

            if (code === 403) {
                window.location = $rootScope.RootedUrl("Singular/Core/Sys/AccessDenied");
                return;
            }
            if (code === 404) {
                window.location = $rootScope.RootedUrl("Singular/Core/Sys/PageNotFound");
                return;
            }
            if (code === 500) {
                window.location = $rootScope.RootedUrl("Singular/Core/Sys/Error");
                return;
            }
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

    // ngValidation directive
    app.directive("sgValidationErrors", ["$compile", function ($compile) {
        return {
            restrict: "A",
            link: function (scope, element, attrs) {
                
                // validation prop name
                var valPropName = attrs.sgValidationErrors;

                // remove attr
                element.removeAttr("sg-validation-errors");
                element.removeAttr("data-sg-validation-errors");

                // html
                var html = "<ul><li ng-repeat=\"(key, sgve) in NonProp" + valPropName + "()\">{{sgve}}</li></ul>";
                element.html(html);

                // compile
                $compile(element)(scope);
            }
        }
    }]);

    // sg-property-error 
    app.directive("sgPropertyError", [function () {
        return {
            restrict: "A",
            link: function (scope, element, attrs) {

                // get model prop name                
                var propName = attrs.sgPropertyError;

                // hide
                element[0].style.display = "none";

                // watch
                scope.$watch(function () { return scope.$eval(propName); }, function (newVal, oldVal) {
                    
                    if (!strng.IsNullOrEmpty(newVal)) {
                        
                        // show
                        element[0].style.display = "inherit";
                        element.html(newVal);

                    }
                    else {
                        element[0].style.display = "none";
                        element.html("");
                    }

                }, true);
            }
        }
    }]);

    // sg-property-error-control 
    app.directive("sgPropertyErrorControl", [function () {
        return {
            restrict: "A",
            link: function (scope, element, attrs) {
                
                // get model prop name                
                var propName = attrs.sgPropertyErrorControl;

                // watch
                scope.$watch(function () { return scope.$eval(propName); }, function (newVal, oldVal) {
                    
                    if (!strng.IsNullOrEmpty(newVal)) {

                        element.addClass("ng-dirty");
                        element.addClass("sg-dirty");
                        element.addClass("ng-invalid");
                        element.addClass("sg-invalid");

                    }
                    else {
                        element.removeClass("ng-dirty");
                        element.removeClass("sg-dirty");
                        element.removeClass("ng-invalid");
                        element.removeClass("sg-invalid");
                    }

                }, true);
            }
        }
    }]);

})(Singular.Common.MasterApp);