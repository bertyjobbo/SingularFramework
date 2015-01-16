/// <reference path="C:\ffd248e91f0c43bbbe5921e4\Git\SingularFramework\Singular.Modules.Core\Scripts/angular.js" />
'use strict';

/*
 * Singular
 * 
 * Common functionality
 * 
 */

// check existing
if (Singular == undefined) var Singular = {};
if (Singular.Common == undefined) Singular.Common = {};

// master app
Singular.Common.SingularCommon = angular.module("Singular.Common.SingularCommon", ["ng"]);

//
(function (app) {

    // service
    app.service("sgCrudService", ["$q", "$http", function ($q, $http) {

        var ts = this;
        ts.Post = function ($scope, url, data) {

            // check $scope
            if ($scope.NonPropSgValidationErrors === undefined) {
                $scope.NonPropSgValidationErrors = function () {
                    var output = [];
                    for (var x in $scope.SgValidationErrors) {
                        if (x.indexOf("NonProp") === 0) {
                            output.push($scope.SgValidationErrors[x]);
                        }
                    }
                    return output;
                }
            }

            // defer
            var deferred = $q.defer();

            // send
            $http
                .post(url, data)
                .success(function (d) {
                    var errors = convertResponseToErrors(d);
                    if (!objct.IsEmpty(errors)) {
                        $scope.SgValidationErrors = errors;
                    };


                    deferred.resolve(d);


                })
                .error(function (err, code, func, request) {
                    $scope.SgValidationErrors = {};
                    $scope.SgValidationErrors["NonProp_sgCrudService.Post.Error"] = window.GENERAL_ERROR_MESSAGE;
                    deferred.resolve({
                        Error: err,
                        StatusCode: code,
                        Request: request
                    });

                });

            return deferred.promise;
        }
        var convertResponseToErrors = function (resp) {
            var output = {};
            // todo - convert errors
            return output;
        }

        ///*
        // * OBVIOUSLY THIS ALL NEEDS TO GO IN THE SERVICE!!!
        // * 
        // * (INCLUDING THE "NON PROP" ONE) - add "generalErrors" to TransactionResult
        // * 
        // */
        //$scope.SgValidationErrors = {};
        //$scope.SgValidationErrors["Model.Email"] = "Error email";
        //$scope.SgValidationErrors["Model.Password"] = "Error passowrd";            
        //$scope.SgValidationErrors["NonProp_1"] = "Your username or password are SHITE";
        //$scope.NonPropSgValidationErrors = function () {

        //    var output = [];
        //    for (var x in $scope.SgValidationErrors) {
        //        if (x.indexOf("NonProp") === 0) {
        //            output.push($scope.SgValidationErrors[x]);
        //        }
        //    }
        //    return output;
        //}

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

    // config
    app.config(["$provide", "$httpProvider", function ($provide, $httpProvider) {

        // register the interceptor as a service
        $provide.factory('myHttpInterceptor', ["$q", "$rootScope", function ($q, $rootScope) {

            $rootScope.IsLoading = function () {
                return $rootScope.Ui.LoaderVisible === true;
            };
            $rootScope.SetLoading = function (loading) {
                $rootScope.Ui.LoaderVisible = loading;
                return $rootScope;
            }
            $rootScope.HasHttpErrors = function () {
                return $rootScope.__requestError != undefined || $rootScope.__responseError != undefined;
            };
            $rootScope.ClearHttpErrors = function () {
                $rootScope.__requestError = undefined;
                $rootScope.__responseError = undefined;
                return $rootScope;
            };
            $rootScope.HasHttpRequestError = function () {
                return $rootScope.__requestError != undefined;
            };
            $rootScope.HasHttpResponseError = function () {
                return $rootScope.__responseError != undefined;
            };
            $rootScope.GetHttpRequestError = function () {
                return $rootScope.__requestError || { data: undefined, config: undefined, status: undefined, statusText: undefined, headers: undefined };
            };
            $rootScope.GetHttpResponseError = function () {
                return $rootScope.__responseError || { data: undefined, config: undefined, status: undefined, statusText: undefined, headers: undefined };;
            };


            return {
                // optional method
                'request': function (config) {
                    $rootScope.ClearHttpErrors().SetLoading(true);
                    return config;
                },

                // optional method
                'requestError': function (rejection) {
                    $rootScope.__requestError = rejection;
                    $rootScope.SetLoading(false);
                    console.log("Request error: ", rejection);
                    return $q.reject(rejection);

                },

                // optional method
                'response': function (response) {
                    $rootScope.SetLoading(false);
                    return response;
                },

                // optional method
                'responseError': function (rejection) {
                    $rootScope.__responseError = rejection;
                    $rootScope.SetLoading(false);
                    console.log("Response error: ", rejection);
                    return $q.reject(rejection);
                }
            };
        }]);

        // push
        $httpProvider.interceptors.push('myHttpInterceptor');

    }]);

    // app start
    app.run(["$rootScope", function ($rootScope) {

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
        $rootScope.$on("$routeChangeStart", function () {
            $rootScope.Ui.ShowLoader();
        });
        $rootScope.$on("$routeChangeSuccess", function () {
            $rootScope.Ui.HideLoader();
        });
        $rootScope.$on("$routeChangeError", function (e, errObj, code, route) {

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

    }]);

})(Singular.Common.SingularCommon);