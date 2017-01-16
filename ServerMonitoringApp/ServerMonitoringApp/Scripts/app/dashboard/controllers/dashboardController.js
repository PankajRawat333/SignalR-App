(function () {
    'use strict';

    angular
        .module('app.dashboard')
        .controller('dashboardController', dashboardController);

    dashboardController.$inject = ['$scope', 'signalRHubProxy','SweetAlert'];

    function dashboardController($scope, signalRHubProxy, SweetAlert) {
        var serverDashboardHubProxy = signalRHubProxy(
            signalRHubProxy.defaultServer, 'dashboard');
        var serverEventHubProxy = signalRHubProxy(
            signalRHubProxy.defaultServer, 'event');
        //serverDashboardHubProxy.start();
        //serverDashboardHubProxy.invoke('getConnectedClient', function (data) {
        //});

        serverDashboardHubProxy.on('onHitRecorded', function (data) {
            $scope.vm = JSON.parse(data);
        });
        serverEventHubProxy.on('onHitRecorded', function (data) {
            $scope.events = JSON.parse(data);
        });

        $scope.getServerTime = function () {
            serverDashboardHubProxy.invoke('getConnectedClient', function (data) {
            });
            serverEventHubProxy.invoke('getApplicationEvent', function (data) {
            });
        };

        $scope.getMessage = function () {
            SweetAlert.swal("Good job!", "You have done very good job!", "success");
        };
    }
})();