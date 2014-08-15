'use strict';

var customerControllers = angular.module('customerControllers', ['customerServices']);

var fromCustomer = 0;
var toCustomer = 0;

customerControllers.controller('customerListController', [
    '$scope', 'customerService', function($scope, customerService) {
        init();

        $scope.getPreviousPage = function() {
            $scope.customers = [];
            $scope.fromCustomer -= $scope.pageSize;
            if ($scope.fromCustomer < 0) {
                $scope.fromCustomer = 0;
            }
            $scope.toCustomer = $scope.fromCustomer + $scope.pageSize - 1;

            getData();
        };

        $scope.getNextPage = function() {
            $scope.customers = [];
            $scope.fromCustomer += $scope.pageSize;
            $scope.toCustomer = $scope.fromCustomer + $scope.pageSize - 1;

            getData();
        };

        function init() {
            $scope.fromCustomer = 0;
            $scope.pageSize = 25;
            $scope.toCustomer = $scope.pageSize - 1;

            getData();
        };

        function getData() {
            fromCustomer = $scope.fromCustomer;
            toCustomer = $scope.toCustomer;

            customerService.getCustomers({},
                function (value, headers) {
                    $scope.customers = value;
                    var rangeFields = headers('Content-Range').split(/\s|-|\//);
                    if (parseInt(rangeFields[2]) == (parseInt(rangeFields[3] - 1))) {
                        $('.paging-controls .glyphicon-arrow-right').addClass('disabled');
                    } else {
                        $('.paging-controls .glyphicon-arrow-right').removeClass('disabled');
                    }
                    if ($scope.fromCustomer > 0) {
                        $('.paging-controls .glyphicon-arrow-left').removeClass('disabled');
                    } else {
                        $('.paging-controls .glyphicon-arrow-left').addClass('disabled');
                    }
                }, function (response) {
                    console.log(response);
                });
        };
    }
]);