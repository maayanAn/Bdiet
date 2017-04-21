//var beatApp = angular.module('beatApp', []);
beatApp.directive('login', function () {
    return {
                controller: 'loginController',
        restrict: 'E',
        //        replace: 'false',
        templateUrl: 'html/login.html'
        //            template: '<div>Hello fromt Directive</div>'
    };
});