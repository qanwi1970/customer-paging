'use strict';

var customerServices = angular.module('customerServices', ['ngResource']);

customerServices.factory('customerService', [
    '$resource',
    function($resource) {
        return $resource('/api/CustomerService', {}, {
            getCustomers: {
                method: 'GET',
                isArray: true,
                transformRequest: function(data, headersGetter) {
                    var headers = headersGetter();
                    headers['Range'] = 'customers=' + fromCustomer + '-' + toCustomer;
                }
            }
        });
    }
]);