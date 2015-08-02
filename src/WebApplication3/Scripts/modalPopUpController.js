function instanceController($scope, $modalInstance, currentItem, itemList, categories, growl) {

    $scope.currentItem = angular.copy(currentItem);
    $scope.itemList = itemList;
    $scope.categories = categories;

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

        //Display notification
        growl.success("Item was successfully updated");
    }

    $scope.addBook = function (item) {
        item.$save();
        //This is just to update the grid in the main view
        $scope.itemList.push(item);
        growl.success("Item was successfully added");
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