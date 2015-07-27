
    loadGoogleCharts();

    var theApp = angular.module('moviesApp', ['moviesServices', 'ui.bootstrap','ngRoute','customDirectives','customFilters']);

    theApp.config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/rss', {
                templateUrl: '/pages/rss.html'
            })
            .when('/', {
                templateUrl: '/pages/index.html'
            })
            .otherwise({
                redirectTo: '/'
            });
    }]);

theApp.controller('moviesController', ['$scope', '$location', 'Movies', 'Categories', 'News', '$modal', moviesController])
    .controller('rssController', ['$scope', '$location', 'News', rssController])
    .controller('instanceController', ['$scope', '$modalInstance', 'currentItem', 'itemList','categories', instanceController]);


    function loadGoogleCharts()
    {
        // Load the Visualization API and the piechart package.
        google.load('visualization', '1.0', { 'packages': ['corechart'] });
        // Set a callback to run when the Google Visualization API is loaded.
        //google.setOnLoadCallback(drawChart);


    } 