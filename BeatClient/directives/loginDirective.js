beatApp.directive('login', function () {
    return {
        controller: 'loginController',
        restrict: 'E',
        templateUrl: 'html/login.html'
    };
});