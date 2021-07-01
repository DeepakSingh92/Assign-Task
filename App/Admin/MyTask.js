
app.controller('MyTask', function ($scope, $http, $window, MyFactory) {

    $scope.EmpCode = 0;
    $scope.CheckIn = 1;
    $scope.CheckOut = 0;
    $scope.TaskId = '';
    $scope.check_in = 0;
    $scope.TaskIdd = '';
    $scope.IDdd = '';
    $scope.ListTask = {};
    $scope.ManageList = {};
    $scope.ListProject = {};
    $scope.ListClient = {};
    $scope.TaskList = {};
    $scope.ListAssignTo = {};
    $scope.ItemTypeID = 0;
    $scope.Project = 0;
    $scope.StatusID = 0;
    $scope.PriorityID = 0;
    var abc = '';
    $scope.ListTask1 = {};
    //$scope.TaskIdd = document.getElementById('TaskIdd').innerHTML;

    $scope.StatusID = '';
    $scope.Reason = '';
    $scope.ListUserEntry2 = {};

    $scope.Api = MyFactory.GetApi();

    $scope.EmpCode = document.getElementById('UserCode').innerHTML;
    $scope.UserCode = document.getElementById('UserCode').innerHTML;

    $scope.GetitemtypeList = function () {
        $http.get($scope.Api + '/Values/GetItemTypeRecord/').success(function (data) {
            $scope.Listitemtype = data;
        }).error(function (data) {
            //alert('error');
        });
    }           //added
    $scope.GetitemtypeList();


    $scope.GetpriorityList = function () {
        $http.get($scope.Api + '/Values/GetpriorityRecord/').success(function (data) {
            $scope.Listpriority = data;

        }).error(function (data) {
            //alert('error');
        });
    }
    $scope.GetpriorityList();                           //added

    $scope.GetstatusList = function () {
        $http.get($scope.Api + '/Values/GettaskRecord/').success(function (data) {
            $scope.Liststatus = data;

        }).error(function (data) {
            //alert('error');
        });
    }
    $scope.GetstatusList();                             //added

    $http.get($scope.Api + '/Values/TaskInfo/').success(function (data) {
        $scope.TaskList = data;
        $scope.ListProject = $scope.TaskList
        $scope.ListClient = $scope.TaskList[0].Client;
        $scope.ListAssignTo = $scope.TaskList[0].AssignTo;


    }).error(function (data) {
        //alert('error');
    });                                                                                          //added

    $scope.GetRecords = function () {

        $http.get($scope.Api + '/Values/GetTaskList/', { params: { "UserId": $scope.EmpCode } }).success(function (data) {
            $scope.ManageList = data;

        }).error(function (data) {
            //alert('error');
        });
    }
    $scope.GetRecords();                                                    //added



    $scope.EditTask = function (value) {
        $window.location.href = '/Home/AddTask?VisitId=' + value;
    }                       // Not done


    $scope.fnClick = function (TaskId, CheckOut) {

        $scope.ListTask.EmpCode = $scope.EmpCode;
        $scope.ListTask.CheckIn = $scope.CheckIn;
        $scope.ListTask.TaskId = TaskId;
        $scope.ListTask.CheckOut = CheckOut;
        $scope.IDdd = TaskId;
        $scope.TaskIdd = TaskId;

        if (CheckOut == 2) {
            $scope.IsShow = true;

        }
        else {
            $scope.IsShow = false;


            $http.post($scope.Api + '/Values/CheckInStatus/', $scope.ListTask).success(function (data) {
                $scope.GetRecordsnew();
                if (CheckOut == 1) {
                    alert(data);
                    // $window.location.reload();
                }

                //$scope.ManageList = data;

            });
        }
    }               //added



    $scope.cancel = function () {
        //$scope.IsShow = false;
        // $scope.GetRecordsnew();
        $window.location.reload();

    }                                //ok


    $scope.submit = function (TaskId) {

            if ($scope.Reason == '' || $scope.Reason == null) {
                alert("Please Enter Reason");
            return false;
            }
            else if ($scope.StatusID == '' || $scope.StatusID == null) {
                alert("Please Enter StatusID");
                return false;
            }


        $scope.ListUserEntry2.StatusID = $scope.StatusID;
        $scope.ListUserEntry2.Reason = $scope.Reason;

        $scope.ListUserEntry2.EmpId = $scope.EmpCode;

        $scope.ListUserEntry2.TaskID = document.getElementById('getTaskValue').innerHTML;



        $http.post($scope.Api + '/Values/Checkoutstatus/', $scope.ListUserEntry2).success(function (response) {
            $scope.GetRecordsnew();
            alert(response);
            $window.location.reload();
        }).error(function (data) {
            alert('Error');
        });

    }                       //added

    $scope.exportMyTaskData = function () {
        var blob = new Blob([document.getElementById('exportMyTasktable').innerHTML], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
        });
        saveAs(blob, "My Task List.xls");
    };


    $scope.GetRecordsnew = function () {
        $http.get($scope.Api + '/Values/GetTaskList1/', { params: { "UserId": $scope.UserCode, "Project": $scope.Project, "ItemTypeID": $scope.ItemTypeID, "StatusID": $scope.StatusID, "PriorityID": $scope.PriorityID } }).success(function (data) {
            $scope.ListTask1 = data;

        }).error(function (data) {
            //alert('error');
        });
    }                       //added
    $scope.GetRecordsnew();

});