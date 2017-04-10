var beatApp = angular.module('beatApp', []);

beatApp.controller('BeatController', function BeatController($scope) {
    $scope.name = 'hi Beat';
});