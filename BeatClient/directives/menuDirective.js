//var beatApp = angular.module('beatApp', []);
beatApp.directive('menu', function () {
    return {
        controller: 'menuController',
        restrict: 'E',
        templateUrl: 'html/menu.html'
    };
});