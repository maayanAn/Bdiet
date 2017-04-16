var beatApp = angular.module('beatApp', ['ngRoute']);

beatApp.config(['$locationProvider', '$routeProvider',
        function config($locationProvider, $routeProvider) {
            $locationProvider.hashPrefix('!');

            $routeProvider.
                when('/', {
                    templateUrl: 'html/homePage.html',
                    controller: 'homePageController'
                }).
                when('/personalZone', {
                    templateUrl: 'html/personalZone.html',
                    controller: 'personalZoneController'
                }).
                when('/menu', {
                    templateUrl: 'html/menu.html',
                    controller: 'menuController'
                }).
                when('/login', {
                    templateUrl: 'html/login.html',
                    controller: 'loginController'
                }).
                when('/register', {
                    templateUrl: 'html/register.html',
                    controller: 'loginController'
                }).
                otherwise('/');
        }
    ]);

beatApp.controller('BeatController', function BeatController($scope, $rootScope) {
    $scope.name = 'Beat';
    $rootScope.user = undefined;

});