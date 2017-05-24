var beatApp = angular.module('beatApp');

beatApp.controller('personalZoneController', function personalZoneController($scope, $rootScope, $location, $anchorScroll, $http) {
    $scope.hasReceivedBloodTests = false;

    var js = [];
    js2 = [];
    $(function (ngModelCtrl) {
        $http({
            method: 'GET',
            url: 'http://localhost:51149/api/PersonalZone'
        }).then(function successCallback(response) {
            // this callback will be called asynchronously
            // when the response is available

            var t1 = (response.data[0]).split(',');
            var t2 = (response.data[1]).split(',');
            for (i = 0; i < t1.length; i++) {
                js.push({ name: t1[i] });
            }
            for (i = 0; i < t2.length; i++) {
                js2.push({ name: t2[i] });
            }

            a(js, js2);

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
        function a(js1, js2) {
            $scope.allergies = js1;
            $scope.preferences = js2;
        }
    });


    CheckValidLists = function () {
        var noneList = ["none"];
        if ($scope.selectedAllergies == null)
            $scope.selectedAllergies = noneList;
        if ($scope.selectedNutritionalPreferences == null)
            $scope.selectedNutritionalPreferences = noneList;
    }

    $scope.createMenu = function () {
        if (!$scope.hasReceivedBloodTests) {
            swal("Error", "we have to receive your blood tests first! please click on the right button", "error");
        } else {

            CheckValidLists();

            var req = {
                method: 'POST',
                url: 'http://localhost:51149/api/PersonalZone',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*'
                },
                data: {
                    "userAllergies": $scope.selectedAllergies,
                    "userPreferences": $scope.selectedNutritionalPreferences,
                    "userId": "0"
                }
            }

            $http(req).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                console.log(response);
                $rootScope.$broadcast('calcMenu');

                //$location.hash('menu');
                $anchorScroll('menu');
                $scope.loginForm = {};
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                console.log(response);
                swal("Error", "Please try again later", "error");
            });
        }
    };

    $scope.getBloodTests = function () {
        if (!$scope.hasReceivedBloodTests) {
            swal("Blood Tests", "connecting with your health services, this might take a few seconds..", "info");


            $(function () {
                var result = $.getJSON('http://localhost:51149/api/BloodTestsResults', function (bloodTestResponse) {
                    $scope.bloodElements = bloodTestResponse;
                });

                result.complete(function () {
                    $scope.hasReceivedBloodTests = true;
                })
            });
        }
    };
});

beatApp.directive('selectpicker', function () {
    return {
        restrict: 'E',
        scope: {
            array: '=',
            model: '=',
            class: '='
        },
        template: '<select multiple class="selectpicker" ng-model="model" ng-options="o.name as o.name for o in array"></select>',
        replace: true,
        link: function (scope, element, attrs) {
            $(element).selectpicker();
        }
    }
});