//Login services
app.service('loginSrv', ['$http', 'cookiesSrv', function ($http, cookiesSrv) {
    return {
        login: function (user) {
            console.log('login service');
            var req = {
                method: 'POST',
                url: '/api/user/login',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;',
                },
                params: { UserName: user.userName, Password: user.password }
            };
            return $http(req).then(function (success) {
                return success;//Success
            }, function (ex) {
                return ex;// error
            });
        },
        profile: function () {
            var req = {
                method: 'GET',
                url: '/api/user/profile',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;',
                },
            };
            return $http(req).then(function (success) {
                return success;
            }, function (ex) {
                return ex;
            });
        },
        accessToken: function () {
            var req = {
                method: 'POST',
                url: '/api/user/token',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;',
                    'token': cookiesSrv.get('token'),
                }
            };
            return $http(req).then(function (success) { return success; }, function (ex) { return ex; });
        }
    };
}]);


//Use for cookie Services
app.service('cookiesSrv', ['$cookies', function ($cookies) {
    return {
        get: function (name) {
            return $cookies.get(name);
        },
        set: function (name, value) {
            $cookies.put(name, value);
        }
    };
}]);