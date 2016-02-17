function wizardController($scope) {

    $scope.goToNextStep = function(callback, step)
    {
        callback();
        $scope.currentStep = $scope.steps[step.next-1];
    }
    $scope.goToPreviousStep = function (step) {
        $scope.currentStep = $scope.steps[step.previous - 1]; //Or find in the array the one which order==step.previous
    }

    $scope.steps = [
        {
            title: "Initial",
            order: 1,
            next: 2,
            template: 'pages/step1.html'
        },
        {
            title: "Middle",
            order: 2,
            next: 3,
            previous: 1,
            template: 'pages/step2.html'
        },
        {
            title: "Final",
            order: 3,
            previous: 2,
            template: 'pages/step3.html'
        }
    ];

    $scope.currentStep = $scope.steps[0];
}