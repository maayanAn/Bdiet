//var beatApp = angular.module('beatApp', []);
beatApp.directive('register', function () {
    return {
        controller: 'loginController',
        restrict: 'E',
        templateUrl: 'html/register.html'
    };
});