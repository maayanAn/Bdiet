var beatApp = angular.module('beatApp');

beatApp.controller('menuController', function menuController($scope, $rootScope, $http) {
    $scope.isWaiting = false;
    $scope.menu = undefined;

    var calcMenu = function () {
        var req = {
            method: 'GET',
            url: 'http://localhost:51149/api/Menu?id=' + $rootScope.user.UserId // dev
            //url: 'http://db.cs.colman.ac.il/BEat/api/Menu?id=' + $rootScope.user.UserId // prod
        }

        // send the request and handle the response (success or error)
        $http(req).then(function successCallback(response) {
            //this callback will be called asynchronously when the response is available
            var Breakfast = response.data.Breakfast;
            var MidMorning = response.data.MidMorning;
            var Lunch = response.data.Lunch;
            var Afternoon = response.data.Afternoon;
            var Dinner = response.data.Dinner;

            $scope.menusList = [
                {
                    UserId: $rootScope.user.id,
                    menu: {
                        breakfast: Breakfast,
                        morningSnack: MidMorning,
                        lunch: Lunch,
                        afternoonSnack: Afternoon,
                        dinner: Dinner
                    }
                }
            ];

            $scope.menu = $scope.menusList[0].menu;

            // disable the spinner
            $scope.isWaiting = false;
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
            console.log(response);
            swal("Error", "Please try again later", "error");

            // disable the spinner
            $scope.isWaiting = false;
        });
    }

    // clearing the menu when a user is logged in
    $rootScope.$on('userLoggedIn', function (evt) {
        $scope.menusList = undefined;
        $scope.menu = undefined;
    });
 
    // creating the menu when the personal zone 'create my menu' button is clicked
    $rootScope.$on('calcMenu', function (evt) {
        $scope.isWaiting = true;
        calcMenu();
    });
});
