//var beatApp = angular.module('beatApp', []);

//beatApp.controller('BeatController', function BeatController($scope) {
//    $scope.name = 'hi Beat';
//});

var phonecatApp = angular.module('phonecatApp', []);

// Define the `PhoneListController` controller on the `phonecatApp` module
phonecatApp.controller('PhoneListController', function PhoneListController($scope) {
    $scope.phones = [
      {
          name: 'Nexus S',
          snippet: 'Fast just got faster with Nexus S.'
      }, {
          name: 'Motorola XOOM™ with Wi-Fi',
          snippet: 'The Next, Next Generation tablet.'
      }, {
          name: 'MOTOROLA XOOM™',
          snippet: 'The Next, Next Generation tablet.'
      }
    ];
});