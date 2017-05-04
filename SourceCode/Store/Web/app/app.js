var app = angular.module('app', []);

app.service('loginSrv', ['$http', function ($http) {
    return {
        login: function () {
            return $http.get('/api/user/login').then(function (response) {
                return response;
            });
        }
    }
}]);
app.controller('loginController', ['$scope', '$http', 'loginSrv', function ($scope, $http, loginSrv) {
    $scope.user = {
        userName: '',
        password: ''
    };

    $scope.login = function () {
        console.log('login');
        var data = loginSrv.login();
        data.then(function (response) {
            console.log('service');
            console.log(response);
        });
        $http.get('/api/dashboard/login1').then(function (response) {
            console.log(response);
        });
        console.log('login done');
    }
}]);