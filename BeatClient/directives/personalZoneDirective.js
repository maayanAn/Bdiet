//var beatApp = angular.module('beatApp', []);
beatApp.directive('personalZone', function () {
    return {
        controller: 'personalZoneController',
        restrict: 'E',
        //        replace: 'false',
        templateUrl: 'html/personalZone.html'
        //            template: '<div>Hello fromt Directive</div>'
    };
});