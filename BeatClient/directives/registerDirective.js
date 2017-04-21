//var beatApp = angular.module('beatApp', []);
beatApp.directive('register', function () {
    return {
        controller: 'loginController',
        restrict: 'E',
        //        replace: 'false',
        templateUrl: 'html/register.html'
        //            template: '<div>Hello fromt Directive</div>'
    };
});