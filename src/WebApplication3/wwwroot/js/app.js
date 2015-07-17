(function () {
    'use strict';

    loadGoogleCharts();

    var theApp = angular.module('moviesApp', ['moviesServices', 'ui.bootstrap','ngRoute','customDirectives','customFilters']);

    theApp.config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/book', {
                templateUrl: '/pages/test.html'
            })
            .when('/', {
                templateUrl: '/pages/index.html'
            })
            .otherwise({
                redirectTo: '/'
            });
    }]);

    theApp.controller('moviesController', ['$scope','Movies','News', '$modal', moviesController])
    .controller('instanceController', ['$scope', '$modalInstance', 'currentItem', 'itemList', instanceController]);


    function loadGoogleCharts()
    {
        // Load the Visualization API and the piechart package.
        google.load('visualization', '1.0', { 'packages': ['corechart'] });
        // Set a callback to run when the Google Visualization API is loaded.
        //google.setOnLoadCallback(drawChart);


    }

    function moviesController($scope, Movies, News, $modal) {
        $scope.testCollection = null,
        $scope.testFeeds = null,
        $scope.newsToDisplay = 'Select news';
        $scope.currentPage = 1;
        $scope.itemsPerPage = 5;

        $scope.defaults = {
            property: "title",
            direction: "+"
        }

        $scope.filter = {
            property: $scope.defaults.property,
            direction: $scope.defaults.direction
        }

        $scope.changeFilter = function (property) {
            if ($scope.filter.property == property) {
                if ($scope.filter.direction == "+") {
                    $scope.filter.direction = "-";
                }
                else {
                    $scope.filter.direction = "+";
                }
            }
            else {
                $scope.filter.property = property;
                $scope.filter.direction = $scope.defaults.direction;
            }
        }

        $scope.realFilter = function () {
            return $scope.filter.direction + $scope.filter.property;
        }

        Movies.query(function (result) {
            $scope.testCollection = result;
        });

        News.fetch({ q: "http://rss.slashdot.org/Slashdot/slashdot", num: 10 }, {}, function (data) {
            var feed = data.responseData.feed;
            $scope.testFeeds = feed;
        });

        $scope.$watch("testCollection", function (oldValue, newValue) {
            drawChart();
        }, true);

        $scope.deleteBook = function (item) {
            var id = item._id;
            if (id == null) {
                id = item.id;
            }

            //This is just to update the grid in the main view
            var indexToDelete = -1;

            angular.forEach($scope.testCollection, function (value, index) {
                if (value._id == id || value.id == id) {
                    indexToDelete = index;
                    return;
                }
            });

            if (indexToDelete > -1) {
                Movies.delete({ id: id });
                $scope.testCollection.splice(indexToDelete, 1);
            }
        }

        $scope.open = function (currentItem) {

            if (currentItem == null)
            {
                currentItem = new Movies();
            }

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/pages/modalContent.html',
                controller: 'instanceController',
                resolve: {
                    currentItem: function () {
                        return currentItem;
                    },
                    itemList: function () {
                        return $scope.testCollection;
                    }
                }
            });

            modalInstance.result.then(function (selectedItem) {
                //$scope.selected = selectedItem;
            }, function () {
                //alert('Modal dismissed at: ' + new Date());
            });
        };

        // Callback that creates and populates a data table,
        // instantiates the pie chart, passes in the data and
        // draws it.
        function drawChart() {

            var collection = [];
            
            function findItemInCollection(item, collection)
            {
                var theValue;
                angular.forEach(collection, function (value, index) {
                    if (value[0] ==item)
                    {
                        theValue = value;
                        return;
                    }
                });
                return theValue;
            }

            if ($scope.testCollection != null) {
                angular.forEach($scope.testCollection, function (value, index) {
                    var col = findItemInCollection(value.author, collection);

                    if (col == null) {
                        collection.push([value.author, 1]);
                    }
                    else {
                        col[1] = col[1] + 1;
                    }
                });
            }
            // Create the data table.
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Author');
            data.addColumn('number', 'Number');
            data.addRows(collection);

            // Set chart options
            var options = {
                'title': 'Books by author',
                'width': 400,
                'height': 300
            };

            // Instantiate and draw our chart, passing in some options.
            if (document.getElementById('chart_div')!=null)
            {
                var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
                chart.draw(data, options);
            }
        }

        $scope.displayNewContent = function(index)
        {
            $scope.newsToDisplay = $scope.testFeeds.entries[index].content;
        }
    }

    function instanceController($scope, $modalInstance, currentItem, itemList) {

        $scope.currentItem = angular.copy(currentItem);
        $scope.itemList = itemList;

        $scope.categories = [
            { Key: "Sci-fi", Value: 1 },
            { Key: "Comedy", Value: 2 },
            { Key: "Terror", Value: 3 }
        ];

        $scope.editBook = function (item) {
            //var id = $scope.testCollection[index]._id;

            var user = item;

            if (user._id == null) {
                user._id = user.id;
            }

            //Movies.update({ id:id }, user);
            user.$update();

            //This is just to update the grid in the main view
            var indexToUpdate = null;

            angular.forEach($scope.itemList, function (value, index) {
                if (value.id == user.id || value._id == user._id) {
                    indexToUpdate = index;
                    return;
                }
            });
            $scope.itemList[indexToUpdate] = user;
        }

        $scope.addBook = function (item) {
            item.$save();
            //This is just to update the grid in the main view
            $scope.itemList.push(item);
        }

        $scope.ok = function (book) {
            if (book.id == null && book._id == null) {
                $scope.addBook(book);
            }
            else {
                $scope.editBook(book);
            }
            $modalInstance.close();
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    };
})();