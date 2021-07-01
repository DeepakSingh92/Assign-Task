app.controller('ProjectTaskStatusCtrl', function ($scope, $http, $window, MyFactory) {
    $scope.Project = '0';
    $scope.AssignTo = 0;
    $scope.DetailTask1 = {};
    $scope.ListProject = {};
    $scope.ListClient = {};
    $scope.TaskList = {};
    $scope.ListAssignTo = {};
    $scope.ListTask = {};
    $scope.ReportTypeId = '1';
    $scope.ReportTypeList = {};
    $scope.Api = MyFactory.GetApi();

    $scope.EmpCode = document.getElementById('UserCode').innerHTML;;


    $scope.ToDate = new Date();


    $scope.FromDate = new Date();





    //$scope.GetRecords = function () {
    //    $http.get($scope.Api + '/Task/GetProjectName/').success(function (data) {
    //        $scope.ListProject = data;

    //    }).error(function (data) {
    //        //alert('error');
    //    });
    //}
    //$scope.GetRecords();


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
    }               //added
    $scope.GetRecords();



    $scope.Search = function () {

        $http.get($scope.Api + '/Values/ProjectTaskStatusnew/', { params: { "FromDate": $scope.FromDate, "ToDate": $scope.ToDate, "ProjectID": $scope.Project, "ReportTypeId": $scope.ReportTypeId } }).success(function (data) {
            $scope.DetailTask1 = data;

        }).error(function (data) {
            alert(data);
        });

    };              //added

    $scope.Search1 = function () {

        $http.get($scope.Api + '/Values/ProjectTaskStatusnew1/').success(function (data) {
            $scope.DetailTask1 = data;

        }).error(function (data) {
            alert(data);
        });

    };              //added
    $scope.Search1();



    $scope.exportUserReportData = function () {
        var blob = new Blob([document.getElementById('exportUserReporttable').innerHTML], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
        });
        saveAs(blob, "User Task Status List.xls");
    };

    $scope.GetReportType = function () {
        $http.get($scope.Api + '/Values/GetReportType/').success(function (data) {
            $scope.ReportTypeList = data;

        }).error(function (data) {
            //alert('error');
        });
    }
    $scope.GetReportType();


    $scope.GetDate1 = function () {
        debugger;
        var current_date = new Date();
        $scope.FromDate.setDate(current_date.getDate() - 1);


    }


});