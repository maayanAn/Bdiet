var beatApp = angular.module('beatApp');

beatApp.controller('loginController', function loginController($scope, $rootScope, $location, $anchorScroll, $http) {
    $scope.registerForm = {};
    $scope.loginForm = {};

    $scope.login = function () {
        var req = {
            method: 'POST',
            url: 'http://localhost:51149/api/Login', // dev
            //url: 'http://db.cs.colman.ac.il/BEat/api/Login', // prod
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin' : '*'
            },
            data: {
                "email": $scope.loginForm.email,
                "password": $scope.loginForm.password
            }
        }

        // send the request and handle the response (success or error)
        $http(req).then(function successCallback(response) {
            // this callback will be called asynchronously when the response is available
            console.log(response);

            $rootScope.user = angular.copy(response.data);
            swal("Hi " + $rootScope.user.Name, "You logged in successfuly", "success");

            // alerting the other controllers that a user has logged in
            $rootScope.$broadcast('userLoggedIn');

            // scroll the page to the personal zone section
            $anchorScroll('personal-zone');

            // clear the login form
            $scope.loginForm = {};
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
            console.log(response);
            swal("Error", "Incorrect E-mail or password", "error");
        });
    }

    $scope.register = function () {
        // Validating the password
        if ($scope.registerForm.password !== $scope.registerForm.variPassword) {
            swal("Error", "Password not the same", "error");
        }
        else {
            var req = {
                method: 'POST',
                url: 'http://localhost:51149/api/Users', // dev
                //url: 'http://db.cs.colman.ac.il/BEat/api/Users', // prod
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

            // send the request and handle the response (success or error)
            $http(req).then(function successCallback(response) {
                // this callback will be called asynchronously when the response is available
                console.log(response);

                $rootScope.user = angular.copy(response.data);
                swal("Hi " + $rootScope.user.Name, "Welcome to B-eat!", "success");

                // alerting the other controllers that a user has logged in
                $rootScope.$broadcast('userLoggedIn');

                // scroll the page to the personal zone section
                $anchorScroll('personal-zone');

                // clear the register form
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
