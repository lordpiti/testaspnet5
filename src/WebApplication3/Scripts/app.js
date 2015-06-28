(function () {
    'use strict';

    angular.module('moviesApp', ['moviesServices', 'ui.bootstrap'])
        .controller('moviesController', ['$scope', 'Movies', '$modal', moviesController])
        .controller('instanceController', ['$scope', '$modalInstance', 'currentItem','itemList', instanceController]);


    function moviesController($scope, Movies, $modal) {

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

        $scope.deleteBook = function (item) {
            var id = item._id;
            if (id == null) {
                id = item.id;
            }

            var indexToDelete = -1;

            $.each($scope.testCollection, function (index, value) {
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

        $scope.items = ['item1', 'item2', 'item3'];

        $scope.open = function (currentItem) {

            if (currentItem == null)
            {
                currentItem = new Movies();
            }

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'myModalContent.html',
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
    }

    function instanceController($scope, $modalInstance, currentItem, itemList) {

        $scope.currentItem = currentItem;
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
        }

        $scope.addBook = function (item) {
            item.$save();
            $scope.itemList.push(currentItem);
        }

        $scope.ok = function (book) {
            if (book.id == null && book._id == null) {
                $scope.addBook(book);
            }
            else {
                $scope.editBook(book);
            }
            $modalInstance.close($scope.selected.item);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    };
})();