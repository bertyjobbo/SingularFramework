/// <reference path="C:\ffd248e91f0c43bbbe5921e4\Git\SingularFramework\Singular.Modules.Core\Scripts/angular.js" />
/// <reference path="../../Scripts/Helpers.js" />
'use strict';

/*
 * SingularLoginApp
 * 
 * /singular/core/formsauth
 * 
 */

// check existing
if (Singular == undefined) var Singular = {};
if (Singular.Modules == undefined) Singular.Modules = {};

// master app
Singular.Modules.SingularFormsAuthApp = angular.module("Singular.Modules.SingularFormsAuthApp",
    ["Singular.Common.SingularCommon", "Singular.Common.MasterApp"]);

// closure
(function (app) {

    // add controller
    app.controller("loginController", ["$scope", "$location", "sgCrudService", function ($scope, $location, sgCrudService) {

        $scope.indexAction = function () {

            // get query string
            var qstr = $location.search();

            if (!objct.IsNullOrEmpty(qstr)) {
                $location.url($location.path())
            }


            // model
            $scope.Model = {
                Email: "",
                Password: "",
                RememberMe: true,
                RedirectUrl: qstr === undefined ? "" : qstr.returnUrl
            }


            // login
            $scope.Login = function () {
                var url = $scope.RootedUrl("SingularApi/Core/FormsAuth/Login");
                sgCrudService
                    .Post($scope, url, $scope.Model)
                    .then(function (data) {                        
                        if (data.Success()) {
                            window.location = $scope.Model.ReturnUrl;
                        }
                    });
            };

            // valid?
            $scope.EmailIsValid = function () {
                return !strng.IsNullOrWhiteSpace($scope.Model.Email);
            }
            $scope.PasswordIsValid = function () {
                return !strng.IsNullOrWhiteSpace($scope.Model.Password);
            }
            $scope.FormIsValid = function () {
                return $scope.EmailIsValid() && $scope.PasswordIsValid();
            }
        }




    }]);

})(Singular.Modules.SingularFormsAuthApp);