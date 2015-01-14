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
Singular.Modules.SingularFormsAuthApp = angular.module("Singular.Modules.SingularFormsAuthApp", ["Singular.Common.MasterApp"]);

// closure
(function (app) {

    // add controller
    app.controller("loginController", ["$scope", "$location", function ($scope, $location) {

        $scope.indexAction = function () {

            // get query string
            var qstr = $location.search();
            $location.url($location.path())

            // model
            $scope.Model = {
                Email: "",
                Password: "",
                RememberMe: true,
                RedirectUrl: qstr === undefined ? "" : qstr.returnUrl
            }


            /*
             * OBVIOUSLY THIS ALL NEEDS TO GO IN THE SERVICE!!!
             * 
             * (INCLUDING THE "NON PROP" ONE) - add "generalErrors" to TransactionResult
             * 
             */
            $scope.SgValidationErrors = {};
            $scope.SgValidationErrors["Model.Email"] = "Error email";
            $scope.SgValidationErrors["Model.Password"] = "Error passowrd";            
            $scope.SgValidationErrors["NonProp_1"] = "Your username or password are SHITE";
            $scope.NonPropSgValidationErrors = function () {
                
                var output = [];
                for (var x in $scope.SgValidationErrors) {
                    if (x.indexOf("NonProp") === 0) {
                        output.push($scope.SgValidationErrors[x]);
                    }
                }
                return output;
            }


            // login
            $scope.Login = function () {
                console.log($scope.Model)
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