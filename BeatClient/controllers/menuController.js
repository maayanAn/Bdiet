var beatApp = angular.module('beatApp');

beatApp.controller('menuController', function menuController($scope, $rootScope) {

    var calcMenu = function () {

        var req = {
            method: 'GET',
            url: 'http://localhost:51404/api/Menu'
        }

        $http(req).then(function successCallback(response) {
            //this callback will be called asynchronously
            //when the response is available
            var breakfast = response.data.Breakfast;
            var MidMorning = response.data.MidMorning;
            var Lunch = response.data.Lunch;
            var Afternoon = response.data.Afternoon;
            var Dinner = response.data.Dinner;

            $scope.menusList = [
                {
                    userId: $rootScope.user.id,
                    menu: {
                        breakfast: Breakfast,
                        morningSnack: MidMorning,
                        lunch: Lunch,
                        afternoonSnack: Afternoon,
                        dinner: Dinner
                    }
                }
            ];

            $scope.menu = menusList[0].menu;

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
            js.push({ name: "None" });
            js2.push({ name: "None" });
        });
    }
    

    //$scope.menusList = [
    //    {
    //        userId: 1,
    //        menu: {
    //            breakfast: 'Two slices of bread, yellow cheese, tomato, an egg, cucumber',
    //            morningSnack: 'energy bar, a frut',
    //            lunch: 'So carbohydrate, chicken',
    //            afternoonSnack: 'energy bar, a frut',
    //            dinner: 'Two slices of bread, white cheese, tomato, cucumber'
    //        }
    //    }
    //];

    $scope.menu = undefined;

    //var getMenu = function () {
    //    $scope.menu = undefined;
    //    for (var i = 0; i < $scope.menusList.length; i++) {
    //        if ($rootScope.user.id == $scope.menusList[i].userId) {
    //            $scope.menu = $scope.menusList[i].menu;
    //        }
    //    }
    //}

    $rootScope.$on('userLoggedIn', function (evt) {
        calcMenu();
        //getMenu();
    });

    //var calcMenu = function () {
    //    var newMenu = angular.copy($scope.menusList[0]);
    //    newMenu.userId = $rootScope.user.id;

    //    $scope.menusList.push(newMenu);
    //    $scope.menu = newMenu.menu;
    //}

    $rootScope.$on('calcMenu', function (evt) {
        calcMenu();
    });
});
