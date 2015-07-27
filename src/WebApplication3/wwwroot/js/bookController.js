function moviesController($scope, $location, Movies, Categories, News, $modal) {
    $scope.testCollection = null,
    $scope.currentPage = 1;
    $scope.itemsPerPage = 5;
    $scope.categories = [];

    $scope.currentStep = "Books";

    Categories.query(function (result) {
        angular.forEach(result, function (value, index) {
            $scope.categories.push({ Key: value.name, Value: value.id });
        });
    });

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

        if (currentItem == null) {
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
                },
                categories: function () {
                    return $scope.categories;
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

        function findItemInCollection(item, collection) {
            var theValue;
            angular.forEach(collection, function (value, index) {
                if (value[0] == item) {
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
        if (document.getElementById('chart_div') != null) {
            var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
            chart.draw(data, options);
        }
    }

}