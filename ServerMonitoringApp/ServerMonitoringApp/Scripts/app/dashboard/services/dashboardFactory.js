(function () {
    'use strict';

    angular
        .module('app.dashboard')
        .factory('dashboardFactory', dashboardFactory);

    dashboardFactory.$inject = ['$rootScope','SweetAlert','$'];

    function dashboardFactory($rootScope, SweetAlert, $) {
        return {
            proxy: null,
            test: function () { SweetAlert.swal("Good job!", "You have done very good job!", "success"); },
            initialize: function (acceptGreetCallback) { }
        }
    }
})();