
app.controller('loginController', ['$scope', 'loginSrv', 'cookiesSrv', function ($scope, loginSrv, cookiesSrv) {
    $scope.user = {
        userName: '',
        password: ''
    };

    $scope.login = function () {
        console.log('login');
        //var data = loginSrv.login($scope.user);
        //data.then(function (response) {
        //    console.log(response);
        //    if (response.status == 200) {
        //        cookiesSrv.set('userName', $scope.user.userName);
        //        console.log(cookiesSrv.get('userName'));
        //    }
        //});

        //var data1 = loginSrv.profile();
        //data1.then(function (result) { console.log(result) });
        //console.log('login done');
        var d = loginSrv.accessToken();
        d.then(function (response) {
            console.log(response);
        });
    }
}]);