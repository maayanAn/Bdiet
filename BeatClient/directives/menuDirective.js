//var beatApp = angular.module('beatApp', []);
beatApp.directive('menu', function () {
    return {
        controller: 'menuController',
        restrict: 'E',
        //        replace: 'false',
        templateUrl: 'html/menu.html'
        //            template: '<div>Hello fromt Directive</div>'
    };
});