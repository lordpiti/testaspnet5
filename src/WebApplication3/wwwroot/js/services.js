(function () {
    'use strict';

    var moviesServices = angular.module('moviesServices', ['ngResource', 'environments.config']);

    moviesServices.factory('Movies', ['$resource', 'apiUrl',
      function ($resource, apiUrl) {

          return $resource(apiUrl+'/api/books/:id', { id: '@_id' }, {
              query: { method: 'GET', params: {}, isArray: true },
              update: { method: 'PUT', params: {} }
          })
      }
    ]);
})();