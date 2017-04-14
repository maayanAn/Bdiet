var beatApp = angular.module('beatApp');

beatApp.controller('personalZoneController', function personalZoneController($scope, $location) {
    $scope.hasReceivedBloodTests = false;
    $scope.preferences = [
        { name: "none" },
        { name: "no fish" },
        { name: "no eggs" },
        { name: "low Cholesterol" },
        { name: "anemia" },
        { name: "diabetes" },
        { name: "vegan" },
        { name: "vegetarian" }
    ];
    $scope.allergies = [
        { name: "none" },
        { name: "Corn allergy" },
        { name: "Peanut allergy" },
        { name: "Nuts allergy" },
        { name: "Dairy allergy" },
        { name: "Wheat allergy" }
    ];
    $scope.bloodElements = [
        { name: "Iron", range: "0.0<x<3.0", value: "0.2" },
        { name: "Cholesterol", range: "1.0<x<20.0", value: "12" },
        { name: "Vitamin E", range: "0.0<x<1.5", value: "0.8" },
        { name: "Vitamin C", range: "0.0<x<2.0", value: "0.4" }
    ];
    $scope.createMenu = function () {
        if (!$scope.hasReceivedBloodTests) {
            alert("we have to receive your blood tests first! please click on the right button");
        } else {
            //redirect to menu page
            $location.path("/menu");
        }
    };

    $scope.getBloodTests = function () {
        alert("connecting with your health services, this might take a few seconds..")
        // get users blood tests results in xml format
        $scope.hasReceivedBloodTests = true;

    };

});
