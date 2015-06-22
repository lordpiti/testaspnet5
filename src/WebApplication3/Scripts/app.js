(function () {
    'use strict';

    angular.module('moviesApp', ['moviesServices']).controller('moviesController', ['$scope', 'Movies', moviesController]);


    function moviesController($scope, Movies) {

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

        $scope.realFilter=function() {
            return $scope.filter.direction + $scope.filter.property;
        } 

        Movies.query(function (result) {
            $scope.testCollection = result;
        });

        $scope.newBook = { title: "default title" };

        $scope.editBook = function (item) {
            //var id = $scope.testCollection[index]._id;

            var user = item;

            if (user._id == null) {
                user._id = user.id;
            }

            //Movies.update({ id:id }, user);
            user.$update();
        }

        $scope.addBook = function (title) {
            $scope.newTodoModel = new Movies();
            $scope.newTodoModel.title = title;
            $scope.newTodoModel.$save();
            //Movies.save($scope.newTodoModel);

            $scope.testCollection.push($scope.newTodoModel);
        }

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
    }
})();