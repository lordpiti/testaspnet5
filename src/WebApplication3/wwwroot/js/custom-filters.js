angular.module("customFilters", [])

.filter('to_trusted', ['$sce', function ($sce) {
    return function (text) {
        return $sce.trustAsHtml(text);
    };
}])

.filter('startFrom', function () {
    return function (input, start) {
        start = +start; //parse to int
        if (input != null)
            return input.slice(start);
    };
});