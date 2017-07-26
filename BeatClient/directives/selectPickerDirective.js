// directive for multi select element
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
            // init the select picker
            $(element).selectpicker();
        }
    }
});