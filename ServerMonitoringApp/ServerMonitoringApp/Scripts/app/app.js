(function () {
    'use strict';

    angular.module('app', ['app.dashboard', 'oitozero.ngSweetAlert'])
    .run(appRun)
    .value('ServerUrl','http://localhost/ServerMonitoringApp');

    function appRun($rootScope) {
        $rootScope.Author = 'Pankaj Rawat';
    }
})();