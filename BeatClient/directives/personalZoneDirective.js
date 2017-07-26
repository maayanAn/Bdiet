beatApp.directive('personalZone', ['$timeout', function ($timeout) {
    return {
        controller: 'personalZoneController',
        restrict: 'E',
        templateUrl: 'html/personalZone.html',
        scope: {
            options: '=',
            ngModel: '=',
            onChanged: '&'
        },
        link: function (scope, element, attrs, ngModelCntrl) {

            // init the select picker (multi select)
            $(function (ngModelCtrl) {
                $(".selectpicker").selectpicker();
            });

            scope.innerModel = scope.ngModel;

            // Set the changed value
            scope.selectpickerChanged = function () {
                ngModelCntrl.$setViewValue(scope.innerModel);
                scope.onChanged();
            };

            scope.$watch(() => scope.options, function (newOptions) {
                $timeout(function () {
                    $(".selectpicker").selectpicker('refresh');
                }, 2000);
            }, true);

        }
    }
}]);