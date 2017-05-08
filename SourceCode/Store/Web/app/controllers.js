
app.controller('loginController', ['$scope', 'loginSrv', 'cookiesSrv', function ($scope, loginSrv, cookiesSrv) {
    $scope.user = {
        userName: '',
        password: ''
    };
    $scope.message = '';

    $scope.login = function () {
        console.log('login');
        var loginResult = loginSrv.login($scope.user);
        loginResult.then(function (response) {
            console.log(response);
            if (response.status == 200) {
                $scope.message = 'Đăng nhập thành công ' + response.data;
                cookiesSrv.set('userName', $scope.user.userName);
                cookiesSrv.set('token', response.data);
                console.log(cookiesSrv.get('userName'));
            } else {
                $scope.message = response.data;
            }
        });

        //var data1 = loginSrv.profile();
        //data1.then(function (result) { console.log(result) });
        //console.log('login done');
        //var d = loginSrv.accessToken();
        //d.then(function (response) {
        //    console.log(response);
        //});
    }
    $scope.token = function () {
        var tokenResult = loginSrv.accessToken();
        tokenResult.then(function (response) {
            if (response.status == 200) {
                $scope.message = 'Success';
            } else {
                $scope.message = response.statusText;
            }
            console.log(response);
        });
    }
}]);