var beatApp = angular.module('beatApp', ['ngRoute']);

beatApp.config(['$locationProvider', '$routeProvider',
        function config($locationProvider, $routeProvider) {
            $locationProvider.hashPrefix('!');

            $routeProvider.
                when('/personalZone', {
                    template: 'html/personalZone.html',
                    controller: 'personalZoneController'
                }).
                when('/menu', {
                    template: 'html/menu.html',
                    controller: 'menuController'
                }).
                when('/login', {
                    template: 'html/login.html',
                    controller: 'loginController'
                }).
                when('/register', {
                    template: 'html/register.html',
                    controller: 'loginController'
                }).
                when('/', {
                    template: 'html/home.html',
                    controller: 'homeController'
                }).
                otherwise('/');
        }
    ]);

beatApp.controller('BeatController', function BeatController($scope) {
    $scope.name = 'hi Beat';


});