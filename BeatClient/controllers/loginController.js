var beatApp = angular.module('beatApp');

beatApp.controller('loginController', function loginController($scope, $rootScope, $location, $anchorScroll) {
    $scope.registerForm = {};
    $scope.loginForm = {};

    $scope.usersTemp = [{
        id: 1,
        username: "Yossi",
        password: "123",
        email: "yossi@gmail.com"
    }]

    $scope.login = function () {
        var isFound = false;

        for (var i = 0; i < $scope.usersTemp.length && !isFound; i++) {
            if ($scope.loginForm.email == $scope.usersTemp[i].email && $scope.loginForm.password == $scope.usersTemp[i].password) {
                $rootScope.user = angular.copy($scope.usersTemp[i]);
                //$location.path('/personalZone');
                
                swal("Hi " + $rootScope.user.username, "You logged in successfuly", "success");
                
                $rootScope.$broadcast('userLoggedIn');

                //$location.hash('personal-zone');
                $anchorScroll('personal-zone');
                isFound = true;
                $scope.loginForm = {};

            }
        }

        if (!isFound) {
            swal("Error", "Incorrect E-mail or password", "error");
        }
    }
    var getNextId = function () {
        var id = $scope.usersTemp[0].id;
        for (var i = 1; i < $scope.usersTemp.length; i++) {
            if ($scope.usersTemp[i].id > id) {
                id = $scope.usersTemp[i].id;
            }
        }

        return ++id;
    }

    $scope.register = function () {
        var isFail = false;

        if ($scope.registerForm.password != $scope.registerForm.variPassword) {
            swal("Error", "Password not the same", "error");
            isFail = true;
        }

        for (var i = 0; i < $scope.usersTemp.length && !isFail; i++) {
            if ($scope.registerForm.email == $scope.usersTemp[i].email) {
                swal("Error", "E-mail address already in use", "error");
                isFail = true;
            }
        }
        if (!isFail) {
            var nextId = getNextId();
            $scope.usersTemp.push({
                id: nextId,
                username: $scope.registerForm.username,
                password: $scope.registerForm.password,
                email: $scope.registerForm.email
            });

            $scope.registerForm = {};
            $rootScope.user = angular.copy($scope.usersTemp[$scope.usersTemp.length - 1]);
            
            swal("Hi " + $rootScope.user.username, "Welcome to B-eat!", "success");
            
            //$location.path('/personalZone');
            $rootScope.$broadcast('userLoggedIn');
            //$location.hash('personal-zone');
            $anchorScroll('personal-zone');


        }

    }
});
