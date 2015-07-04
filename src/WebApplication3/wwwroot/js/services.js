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

    moviesServices.factory('News', ['$resource', function ($resource) {
        return $resource('http://ajax.googleapis.com/ajax/services/feed/load', {}, {
            fetch: { method: 'JSONP', params: { v: '1.0', callback: 'JSON_CALLBACK' } }
        });
    }]);
})();