var beatApp = angular.module('beatApp');

beatApp.controller('menuController', function menuController($scope) {

    $scope.breakfast = 'Two slices of bread, yellow cheese, tomato, an egg, cucumber';
    $scope.morningSnack = 'energy bar, a frut';
    $scope.lunch = 'So carbohydrate, chicken';
    $scope.afternoonSnack = 'energy bar, a frut';
    $scope.dinner = 'Two slices of bread, white cheese, tomato, cucumber';

});
