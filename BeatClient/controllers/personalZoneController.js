var beatApp = angular.module('beatApp');

beatApp.controller('personalZoneController', function personalZoneController($scope, $rootScope, $location, $anchorScroll, $http) {
    $scope.hasReceivedBloodTests = false;

    var generalAllergies = [];
    generalPreferences = [];
    $(function (ngModelCtrl) {
        $http({
            method: 'GET',
            url: 'http://localhost:51149/api/PersonalZone'            
        }).then(function successCallback(response) {
             //this callback will be called asynchronously
             //when the response is available
            var allergyList = response.data.allergiesList;
            var preferenceList = response.data.preferencesList;
            for (i = 0; i < allergyList.length; i++) {
                generalAllergies.push({ name: allergyList[i].Name });
            }
            for (i = 0; i < preferenceList.length; i++) {
                generalPreferences.push({ name: preferenceList[i].Name });
            }

            $scope.allergies = generalAllergies;
            $scope.preferences = generalPreferences;

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
            js.push({ name: "None" });
            js2.push({ name: "None" });
        });
    });


    CheckValidLists = function () {
        var noneList = ["None"];
        if ($scope.selectedAllergies === undefined)
            $scope.selectedAllergies = noneList;
        if ($scope.selectedNutritionalPreferences === undefined)
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
                    "userId": $rootScope.user.UserId
                }
            }

            $http(req).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                console.log(response);
                $rootScope.user = angular.copy(response.data);
                $rootScope.$broadcast('calcMenu');

                $anchorScroll('menu');
//                $scope.loginForm = {};
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

            $http({
                method: 'GET',
                url: 'http://localhost:51149/api/BloodTestsResults',
                data: {
                    "userId": $rootScope.user.UserId
                }
            }).then(function successCallback(response) {
                $scope.bloodElements = response.data;
                $scope.hasReceivedBloodTests = true;
            }, function errorCallback(response) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                    console.log(response);
                    swal("Error", "Please try again later", "error");
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