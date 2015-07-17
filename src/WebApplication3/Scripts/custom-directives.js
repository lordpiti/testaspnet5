angular.module("customDirectives", [])

.directive("dropdownDirective", function () {
    return {
        restrict: "E",
        scope: {
            itemsList: "="
        },
        templateUrl: '/pages/dropdownTemplate.html',
        controller: function ($scope) {

        }
    } 
});