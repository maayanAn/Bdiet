var beatApp = angular.module('beatApp');

beatApp.controller('personalZoneController', function personalZoneController($scope, $rootScope, $location, $anchorScroll, $http) {
    // init veriables
    $scope.hasReceivedBloodTests = false;
    var generalAllergies = [];
    var generalPreferences = [];
    $scope.allergies = [];
    $scope.preferences = [];

    // on page init
    $(function (ngModelCtrl) {
        // send the request and handle the response (success or error)
        $http({
            method: 'GET',
            url: 'http://localhost:51149/api/PersonalZone' //dev
            //url: 'http://db.cs.colman.ac.il/BEat/api/PersonalZone'   //prod         
        }).then(function successCallback(response) {
            //this callback will be called asynchronously when the response is available
            console.log(response);
            
            var allergyList = response.data.allergiesList;
            var preferenceList = response.data.preferencesList;

            // adding the allergies list from the server response
            for (i = 0; i < allergyList.length; i++) {
                generalAllergies.push({ name: allergyList[i].Name });
            }

            // adding the prefernces list from the server response
            for (i = 0; i < preferenceList.length; i++) {
                generalPreferences.push({ name: preferenceList[i].Name });
            }

            $scope.allergies = generalAllergies;
            $scope.preferences = generalPreferences;
            }, function errorCallback(response) {
                console.log(response);
            // called asynchronously if an error occurs
            // or server returns response with an error status.
            $scope.allergies.push({ name: "None" });
            $scope.preferences.push({ name: "None" });
        });
    });

    // check if the alergies and prefernces lists are valid
    CheckValidLists = function () {
        var noneList = ["None"];

        if ($scope.selectedAllergies === undefined) {
            $scope.selectedAllergies = noneList;
        }
           
        if ($scope.selectedNutritionalPreferences === undefined) {
            $scope.selectedNutritionalPreferences = noneList;
        } 
    }

    $scope.createMenu = function () {
        if (!$scope.hasReceivedBloodTests) {
            swal("Error", "we have to receive your blood tests first! please click on the right button", "error");
        } else {
            CheckValidLists();

            // sending the users personal date to the server
            var req = {
                method: 'POST',
                url: 'http://localhost:51149/api/PersonalZone',
                //url: 'http://db.cs.colman.ac.il/BEat/api/PersonalZone',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*'
                },
                data: {
                    "userAllergies": $scope.selectedAllergies,
                    "userPreferences": $scope.selectedNutritionalPreferences,
                    "UserId": $rootScope.user.UserId
                }
            }

            $http(req).then(function successCallback(response) {
                // this callback will be called asynchronously when the response is available
                console.log(response);

                // calling the calc menu event (to be cougth in menu controller)
                $rootScope.$broadcast('calcMenu');

                // scrolling the page to the menu section
                $anchorScroll('menu');
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
           
            var req = {
                method: 'GET',
                url: 'http://localhost:51149/api/BloodTestsResults?UserId=' + $rootScope.user.UserId 
                //url: 'http://db.cs.colman.ac.il/BEat/api/BloodTestsResults?UserId=' + $rootScope.user.UserId 
            }

            $http(req).then(function successCallback(response) {
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

    // clearing the data when a user is logged in
    $rootScope.$on('userLoggedIn', function (evt) {
        $scope.bloodElements = undefined;
        $scope.hasReceivedBloodTests = false;
    });
});

