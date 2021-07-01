app.controller('UserTaskStatusCtrl', function ($scope, $http, $window, MyFactory) {


    $scope.ProjectID = 0;
    $scope.AssignTo = '0';
    $scope.DetailTask = {};
    $scope.ListProject = {};
    $scope.ListClient = {};
    $scope.TaskList = {};
    $scope.ListAssignTo = {};
    $scope.ListTask = {};
    $scope.ReportTypeId = '1';

    $scope.Api = MyFactory.GetApi();

    $scope.EmpCode = document.getElementById('UserCode').innerHTML;;


    $scope.ToDate = new Date();


    $scope.FromDate = new Date();

    $scope.GetReportType = function () {
        $http.get($scope.Api + '/Values/GetReportType/').success(function (data) {
            $scope.ReportTypeList = data;

        }).error(function (data) {
            //alert('error');
        });
    }                       //added
    $scope.GetReportType();

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
    }                          //added
    $scope.GetRecords();

    $scope.Search = function () {

        $http.get($scope.Api + '/Values/UserTaskStatusNew/', { params: { "FromDate": $scope.FromDate, "ToDate": $scope.ToDate, "AssignTo": $scope.AssignTo } }).success(function (data) {
            $scope.DetailTask = data;

        }).error(function (data) {
            alert(data);
        });

    };                              //added


    $scope.Search1 = function () {

        $http.get($scope.Api + '/Values/UserTaskStatusNew1/').success(function (data) {
            $scope.DetailTask = data;

        }).error(function (data) {
            alert(data);
        });

    };                                          //added
    $scope.Search1();

    $scope.exportUserReportData = function () {
        var blob = new Blob([document.getElementById('exportUserReporttable').innerHTML], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
        });
        saveAs(blob, "User Task Status List.xls");
    };


});