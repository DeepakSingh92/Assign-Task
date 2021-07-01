/// <reference path="../Scripts/angular.min.js" />


var app = angular.module('app', []);


app.factory('MyFactory', function () {
    return {
        GetApi: function () {
            return "http://localhost:4522/api";
        }
    }
});
