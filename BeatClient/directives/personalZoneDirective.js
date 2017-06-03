//var beatApp = angular.module('beatApp', []);
beatApp.directive('personalZone', ['$timeout', function ($timeout) {
    return {
        controller: 'personalZoneController',
        restrict: 'E',
        //        replace: 'false',
        templateUrl: 'html/personalZone.html',
        //            template: '<div>Hello fromt Directive</div>'
        scope: {
            options: '=',
            ngModel: '=',
            onChanged: '&'
        },
        link: function (scope, element, attrs, ngModelCntrl) {
            $(function (ngModelCtrl) {
                $(".selectpicker").selectpicker();
            });
            scope.innerModel = scope.ngModel;
            scope.selectpickerChanged = function () {
                ngModelCntrl.$setViewValue(scope.innerModel);
                scope.onChanged();
            };

            scope.$watch(() => scope.options, function (newOptions) {
                $timeout(function () {
                    $(".selectpicker").selectpicker('refresh');
                }, 500);
            }, true);

        }
    }
}]);