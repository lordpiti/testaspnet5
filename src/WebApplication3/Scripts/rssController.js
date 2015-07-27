function rssController($scope, $location, News) {
    $scope.currentStep = "News";
    $scope.testFeeds = null,
    $scope.newsToDisplay = 'Select news';

    News.fetch({ q: "http://rss.slashdot.org/Slashdot/slashdot", num: 10 }, {}, function (data) {
        var feed = data.responseData.feed;
        $scope.testFeeds = feed;
    });

    $scope.displayNewContent = function (index) {
        $scope.newsToDisplay = $scope.testFeeds.entries[index].content;
    }
}