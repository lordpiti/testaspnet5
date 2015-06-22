(function () {
    'use strict';

    var moviesServices = angular.module('moviesServices', ['ngResource']);

    moviesServices.factory('Movies', ['$resource',
      function ($resource) {
          return $resource('/api/books/:id', { id: '@_id' }, {
              query: { method: 'GET', params: {}, isArray: true },
              update: { method: 'PUT', params: {} }
          })
      }
    ]);
})();