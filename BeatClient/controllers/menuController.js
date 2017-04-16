var beatApp = angular.module('beatApp');

beatApp.controller('menuController', function menuController($scope) {

    $scope.breakfast = 'פרוסת לחם, שתי כפות גבינה, ביצה קשה, עגבניה, מלפפון';
    $scope.morningSnack = 'חטיף אנרגיה, פרי';
    $scope.lunch = 'מנת חזה עוף, כוס פחמימה';
    $scope.afternoonSnack = 'חטיף אנרגיה, פרי';
    $scope.dinner = 'פרוסת לחם, שתי כפות קטג, עגבניה, מלפפון';

});