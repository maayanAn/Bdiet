var beatApp = angular.module('beatApp');

beatApp.controller('loginController', function loginController($scope, $rootScope, $location) {
    $scope.registerForm = {};
    $scope.loginForm = {};

    $scope.usersTemp = [{
        username: "יוסי",
        password: "123",
        email: "yossi@gmail.com"
    }]

    $scope.login = function () {
        var isFound = false;

        for (var i = 0; i < $scope.usersTemp.length && !isFound; i++) {
            if ($scope.loginForm.email == $scope.usersTemp[i].email && $scope.loginForm.password == $scope.usersTemp[i].password) {
                $rootScope.user = $scope.usersTemp[i].username;
                $location.path('/personalZone');
                isFound = true;
            }
        }

        if (!isFound)
        {
            alert('האימייל ו/או הסיסמא שהוכנסו שגוים');
        }
    }

    $scope.register = function () {
        var isFail = false;

        if ($scope.registerForm.password != $scope.registerForm.variPassword) {
            alert('סיסמא לא תואמת בשני השדות');
            isFail = true;
        }

        for (var i = 0; i < $scope.usersTemp.length && !isFail; i++) {
            if ($scope.registerForm.email == $scope.usersTemp[i].email) {
                alert('כתובת האימייל שהוכנסה נמצאת בשימוש');
                isFail = true;
            }
        }
        if (!isFail) {
            $scope.usersTemp.push({
                username: $scope.registerForm.username,
                password: $scope.registerForm.password,
                email: $scope.registerForm.email
            });

            $rootScope.user = $scope.registerForm.username;
            $location.path('/personalZone');
        }
        
    }
});
