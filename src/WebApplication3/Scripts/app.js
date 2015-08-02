
    loadGoogleCharts();

    var theApp = angular.module('moviesApp', ['moviesServices', 'ui.bootstrap', 'ngRoute', 'customDirectives', 'customFilters', 'angular-growl']);

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

    theApp.config(['growlProvider', function (growlProvider) {
        growlProvider.globalPosition('bottom-right');
    }]);

theApp.controller('booksController', ['$scope', '$location', 'Movies', 'Categories', 'News', '$modal', 'growl', booksController])
    .controller('rssController', ['$scope', '$location', 'News', rssController])
    .controller('instanceController', ['$scope', '$modalInstance', 'currentItem', 'itemList', 'categories', 'growl', instanceController]);


    function loadGoogleCharts()
    {
        // Load the Visualization API and the piechart package.
        google.load('visualization', '1.0', { 'packages': ['corechart'] });
        // Set a callback to run when the Google Visualization API is loaded.
        //google.setOnLoadCallback(drawChart);


    } 