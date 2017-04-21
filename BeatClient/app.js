var beatApp = angular.module('beatApp', ['ngRoute']);

beatApp.controller('BeatController', function BeatController($scope, $rootScope) {
    $scope.name = 'Beat';
    $rootScope.user = undefined;

});