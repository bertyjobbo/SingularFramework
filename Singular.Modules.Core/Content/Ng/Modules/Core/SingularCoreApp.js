/// <reference path="C:\Git\SingularFramework\Singular.Modules.Core\Scripts/angular.js" />
'use strict';

/*
 * SingularCoreApp
 * 
 * /singular/core
 * 
 */

// check existing
if (Singular == undefined) var Singular = {};
if (Singular.Modules == undefined) Singular.Modules = {};

// master app
Singular.Modules.SingularCoreApp = angular.module("Singular.Modules.SingularCoreApp", ["Singular.Common.MasterApp"]);

// closure
(function (app) {

    console.log("In core app");   

})(Singular.Modules.SingularCoreApp);