var beatApp = angular.module('beatApp');

beatApp.controller('loginController', function loginController($scope, $rootScope, $location, $anchorScroll, $http) {
    $scope.registerForm = {};
    $scope.loginForm = {};

    $scope.login = function () {
        var req = {
            method: 'POST',
            url: 'http://localhost:51404/api/Login', // 51149
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin' : '*'
            },
            data: {
                "email": $scope.loginForm.email,
                "password": $scope.loginForm.password
            }
        }

        $http(req).then(function successCallback(response) {
            // this callback will be called asynchronously
            // when the response is available
            console.log(response);
            $rootScope.user = angular.copy(response.data);
            swal("Hi " + $rootScope.user.Name, "You logged in successfuly", "success");

            $rootScope.$broadcast('userLoggedIn');

            //$location.hash('personal-zone');
            $anchorScroll('personal-zone');
            $scope.loginForm = {};
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
            console.log(response);
            swal("Error", "Incorrect E-mail or password", "error");
        });
    }

    $scope.register = function () {

        if ($scope.registerForm.password !== $scope.registerForm.variPassword) {
            swal("Error", "Password not the same", "error");
        }
        else {
            var req = {
                method: 'POST',
                url: 'http://localhost:51404/api/Users',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*'
                },
                data: {
                    "Name": $scope.registerForm.username,
                    "Password": $scope.registerForm.password,
                    "Email": $scope.registerForm.email
                }
            }

            $http(req).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                console.log(response);
                $rootScope.user = angular.copy(response.data);
                swal("Hi " + $rootScope.user.Name, "Welcome to B-eat!", "success");

                $rootScope.$broadcast('userLoggedIn');

                //$location.hash('personal-zone');
                $anchorScroll('personal-zone');
                $scope.registerForm = {};
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                console.log(response);
                swal("Error", "E-mail address already in use", "error");
            });
        }
    }
});
