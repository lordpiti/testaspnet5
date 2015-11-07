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
})
.directive('customOnChange', function() {
    'use strict';

    return {
        restrict: "A",
        link: function (scope, element, attrs) {
            var onChangeFunc = element.scope()[attrs.customOnChange];
            element.bind('change', onChangeFunc);
        }
    };
});