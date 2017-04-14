var beatApp = angular.module('beatApp');

beatApp.controller('loginController', function loginController($scope) {
    $scope.registerForm = {};
    $scope.loginForm = {};

    $scope.login = function () {
        alert('logging in');
        console.log($scope.loginForm);
    }

    $scope.register = function () {
        alert('registering');
        console.log($scope.registerForm);
    }
});
