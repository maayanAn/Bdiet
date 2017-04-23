var beatApp = angular.module('beatApp');

beatApp.controller('menuController', function menuController($scope, $rootScope) {
    $scope.menusList = [
        {
            userId: 1,
            menu: {
                breakfast: 'Two slices of bread, yellow cheese, tomato, an egg, cucumber',
                morningSnack: 'energy bar, a frut',
                lunch: 'So carbohydrate, chicken',
                afternoonSnack: 'energy bar, a frut',
                dinner: 'Two slices of bread, white cheese, tomato, cucumber'
            }
        }
    ];

    $scope.menu = undefined;

    var getMenu = function () {
        $scope.menu = undefined;
        for (var i = 0; i < $scope.menusList.length; i++) {
            if ($rootScope.user.id == $scope.menusList[i].userId) {
                $scope.menu = $scope.menusList[i].menu;
            }
        }
    }

    $rootScope.$on('userLoggedIn', function (evt) {
        getMenu();
    });

    var calcMenu = function () {
        var newMenu = angular.copy($scope.menusList[0]);
        newMenu.userId = $rootScope.user.id;

        $scope.menusList.push(newMenu);
        $scope.menu = newMenu.menu;
    }

    $rootScope.$on('calcMenu', function (evt) {
        calcMenu();
    });
});
