angular.module("customDirectives", [])

.directive("dropdownDirective", function () {
    return {
        restrict: "E",
        scope: {
            itemsList: "=",
            propertyToBind: "="
        },
        transclude: true,
        templateUrl: '/pages/dropdownTemplate.html',
        controller: function ($scope) {
        }
    } 
});