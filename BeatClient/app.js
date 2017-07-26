var beatApp = angular.module('beatApp', ['ngRoute']);

beatApp.controller('BeatController', function BeatController($scope, $rootScope, $location, $anchorScroll) {
    $scope.name = 'Beat';
    $rootScope.user = undefined;

    $scope.logout = function () {
        $rootScope.user = undefined;
        $anchorScroll('page-top');
    }
});