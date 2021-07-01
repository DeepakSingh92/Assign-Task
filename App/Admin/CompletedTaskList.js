app.controller('CompTask', function ($scope, $http, $window, MyFactory) {

    $scope.AssignTo = 0;
    $scope.Project = '0';

    $scope.Liststatus = {}
    $scope.DetailTask = {};
    $scope.ListProject = {};
    $scope.ListClient = {};
    $scope.TaskList = {};
    $scope.ListAssignTo = {};

    $scope.ListTask = {};



    $scope.Api = MyFactory.GetApi();

    $scope.EmpCode = document.getElementById('UserCode').innerHTML;;
    $scope.UserCode = document.getElementById('UserCode').innerHTML;

    $scope.ToDate = new Date();
    $scope.FromDate = new Date();


    $scope.is_highlight = function (Test) {
        if (Test.Test < 1) {
            return true;
        }
        return false;
    };


    $scope.GetstatusList = function () {
        $http.get($scope.Api + '/Values/GettaskRecord/').success(function (data) {
            $scope.Liststatus = data;

        }).error(function (data) {
            //alert('error');
        });
    }        //added
    $scope.GetstatusList();



    //$scope.GetRecords2 = function () {
    //    $http.get($scope.Api + '/Task/GetTaskList22/', { params: { "UserId": $scope.UserCode, "Project": $scope.Project, "ItemType": 0, "AssignTo": 0 } }).success(function (data) {
    //        $scope.ListTask = data;

    //    }).error(function (data) {
    //        //alert('error');
    //    });
    //}


    $scope.GetRecordsp = function () {
        $http.get($scope.Api + '/Values/GetTaskList2/', { params: { "UserId": $scope.UserCode, "Project": $scope.Project } }).success(function (data) {
            $scope.ListTask = data;

        }).error(function (data) {
            //alert('error');
        });
    }           //added
    $scope.GetRecordsp();





    $scope.GetRecords = function () {
        $http.get($scope.Api + '/Values/TaskInfo/').success(function (data) {
            $scope.TaskList = data;
            $scope.ListProject = $scope.TaskList
            $scope.ListClient = $scope.TaskList[0].Client;
            $scope.ListAssignTo = $scope.TaskList[0].AssignTo;

            $scope.AssignTo = $scope.EmpCode;

        }).error(function (data) {
            //alert('error');
        });
    }           //added
    $scope.GetRecords();



    $scope.Search = function () {

        $http.get($scope.Api + '/Values/UserTaskStatus/', { params: { "FromDate": $scope.FromDate, "ToDate": $scope.ToDate, "AssignTo": $scope.AssignTo, } }).success(function (data) {
            $scope.DetailTask = data;

        }).error(function (data) {
            alert(data);
        });

    };              //added
});