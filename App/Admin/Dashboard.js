
app.controller('Task', function ($scope, $http, $window, MyFactory) {

    $scope.SuppCall = 1;
    $scope.TotalCall = ''
    $scope.PendingCall = ''
    $scope.TotalTask = ''
    $scope.PendingTask = ''


    $scope.GetPendingTasklist = {};
    $scope.GetTotalTasklist = {};
    $scope.GetTotalPendingCalllist = {};
    $scope.GetTotalCalllist = {};
    $scope.ListTask = {};
    $scope.CheckInList = {};
    $scope.CheckInList1 = {};
    $scope.UserEntry = {};
    $scope.GetSupportCalllist = {};
    $scope.ListTask1 = {};
    $scope.ListCliRefTask = {};
    $scope.TaskCount = 0;
    $scope.ProjectCount = 0;
    $scope.UserCount = 0;
    $scope.ClientCount = 0;
    $scope.CompletedProject = 0;
    $scope.CompletedTask = 0;
    $scope.BugCount = 0;
    $scope.CompletedBug = 0;
    $scope.Completedproject = 0;
    $scope.ListSummaryTable = {};

    $scope.Api = MyFactory.GetApi();



    $scope.UserCode = document.getElementById('UserCode').innerHTML;

    $scope.GetSummaryTable = function () {
        $http.get($scope.Api + '/Values/GetSummaryTable/').success(function (data) {
            $scope.ListSummaryTable = data;

        }).error(function (data) {
            //alert('error');
        });
    }                          //added
    $scope.GetSummaryTable();


    $scope.is_highlight = function (Test) {
        if (Test.Test < 1) {
            return true;
        }
        return false;
    };


    $scope.GetRecords = function () {
        $http.get($scope.Api + '/Values/GetTaskList/', { params: { "UserId": $scope.UserCode } }).success(function (data) {
            $scope.ListTask = data;

        }).error(function (data) {
            //alert('error');
        });
    }                               //added
    $scope.GetRecords();

    $scope.logout = function () {

        $http.get($scope.Api + '/Task/GetTaskList/', { params: { "UserId": $scope.UserCode } }).success(function (data) {


        }).error(function (data) {
            //alert('error');
        });

    }                                  //dispute



    $scope.GetRecords = function () {
        $http.get($scope.Api + '/Values/CheckInList/', { params: { "UserId": $scope.UserCode } }).success(function (data) {
            $scope.CheckInList = data;

        }).error(function (data) {
            //alert('error');
        });
    }                                //added
    $scope.GetRecords();

    $scope.GetRecords = function () {
        $http.get($scope.Api + '/Values/TodayUserTask/', { params: { "UserId": $scope.UserCode } }).success(function (data) {
            $scope.CheckInList1 = data;

        }).error(function (data) {
            //alert('error');
        });
    }                               //added
    $scope.GetRecords();

    $scope.DashboardPendingList = function () {

        $http.get($scope.Api + '/Values/DashBoardPendinglist/', { params: { "UserId": $scope.UserCode } }).success(function (data) {
            $scope.ListTask1 = data;
            $scope.PendingCall = $scope.ListTask1[0].PendingCall;
            $scope.TotalCall = $scope.ListTask1[0].TotalCalls;
            $scope.TotalTask = $scope.ListTask1[0].TotalTask;
            $scope.PendingTask = $scope.ListTask1[0].PendingTask;


        }).error(function (data) {
            //alert('error');
        });

    }                    //added

    //$scope.GetSupportCallData = function () {

    //    //$http.get($scope.Api + '/Task/GetSupCallListDeshboard/', { params: { "UserId": $scope.UserCode } }).success(function (data) {
    //    $http.get($scope.Api + '/Values/GetSupCallFilterList/', { params: { "SuppCall": $scope.SuppCall, "EmpCode": $scope.UserCode, } }).success(function (data) {
    //        $scope.GetSupportCalllist = data;

    //    }).error(function () {
    //        /*alert('error');*/
    //    });
    //}                       //not added call
    //$scope.GetSupportCallData();


    //$scope.GetTotalCall = function () {
    //    $http.get($scope.Api + '/Values/GetTodayTotalCallList/', { params: { "EmpCode": $scope.UserCode } }).success(function (data) {
    //        $scope.GetTotalCalllist = data;

    //    }).error(function () {
    //        /*alert('error');*/
    //    });
    //}                               //not added call
    //$scope.GetTotalCall();


    //$scope.GetTotalPendingCall = function () {
    //    $http.get($scope.Api + '/Values/GetTodayPendingCallList/', { params: { "EmpCode": $scope.UserCode } }).success(function (data) {
    //        $scope.GetTotalPendingCalllist = data;

    //    }).error(function () {
    //       /* alert('error');*/
    //    });
    //}                       //not added call
    //$scope.GetTotalPendingCall();


    $scope.GetTotalTask = function () {
        $http.get($scope.Api + '/Values/GetTotalTaskList/', { params: { "EmpCode": $scope.UserCode } }).success(function (data) {
            $scope.GetTotalTasklist = data;

        }).error(function () {
            /*alert('error');*/
        });
    }                               //added
    $scope.GetTotalTask();


    $scope.GetAllHeaderTotal = function () {
        $http.get($scope.Api + '/Values/GetAllHeaderTotal/', { params: { "EmpCode": $scope.UserCode } }).success(function (data) {
            $scope.TaskCount = data.TotalTask;
            $scope.ProjectCount = data.TotalProject;
            $scope.UserCount = data.TotalUser;

            $scope.ClientCount = data.TotalClient;
            $scope.CompletedTask = data.TotalCompletedTask;
            $scope.BugCount = data.TotalBug;
            $scope.Completedproject = data.TotalCompletedProject;


        }).error(function () {
            /*alert('error');*/
        });
    }                       //added
    $scope.GetAllHeaderTotal();


    $scope.GetPendingTask = function () {
        $http.get($scope.Api + '/Values/GetPendingTaskList/', { params: { "EmpCode": $scope.UserCode } }).success(function (data) {
            $scope.GetPendingTasklist = data;

        }).error(function () {
           /* alert('error');*/
        });
    }                           //added
    $scope.GetPendingTask();


    $scope.exportTodayTotalCallData = function () {
        var blob = new Blob([document.getElementById('exportTodayTotalCalltable').innerHTML], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
        });
        saveAs(blob, "Today Total Calls.xls");
    };


    $scope.exportTodayPendingCallData = function () {
        var blob = new Blob([document.getElementById('exportTodayPendingCalltable').innerHTML], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
        });
        saveAs(blob, "Today Total Calls.xls");
    };


    $scope.exportTotalTaskData = function () {
        var blob = new Blob([document.getElementById('exportTotalTasktable').innerHTML], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
        });
        saveAs(blob, "Today Total Task.xls");
    };


    $scope.exportPendingTaskData = function () {
        var blob = new Blob([document.getElementById('exportPendingTasktable').innerHTML], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
        });
        saveAs(blob, "Today Total Pending Task.xls");
    };


});