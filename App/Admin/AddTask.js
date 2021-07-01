app.controller('AddTask', function ($scope, $http, $window, MyFactory) {

    $scope.Priority = '';
    $scope.Project = '';
    $scope.Client = '';
    $scope.Task = '';
    $scope.TaskDesc = '';
    $scope.AssignDate = new Date();
    $scope.AssignTo = '';
    $scope.TaskID = 0;
    $scope.Client2 = 0;
    $scope.TaskPoint = '10';

    $scope.Id = '';
    $scope.ActualTime = '';
    $scope.EstTime = '';
    $scope.Status = '';
    $scope.ItemType = '';
    $scope.ImpStatus = '';

    $scope.Checkedby = '';
    $scope.EstDate = '';
    $scope.CompletionDate = '';
    $scope.CompletedVer = '';
    $scope.Unimple = '';
    $scope.Setting = '';
    $scope.Closing = '';
    $scope.ID = 0;
    $scope.EmpCode = 0;
    $scope.CallID = 0;

    $scope.ListTask = {};
    $scope.TaskList = {};
    $scope.TaskList1 = {};
    $scope.ListProject = {};
    $scope.ListClient = {};
    $scope.ListAssignTo = {};
    $scope.TaskList2 = {};
    $scope.AsslignList = [];

    $scope.ItemTypeID = '1';
    $scope.Priority1 = '';
    $scope.PriorityID = '2';
    $scope.StatusID = '1';
    $scope.Listpriority = [];
    $scope.Listitemtype = [];
    $scope.Liststatus = [];


    $scope.fileList = [];
    $scope.curFile;
    $scope.ImageProperty = {
        file: ''
    }

    var date = new Date();
    //$scope.VchDate = new Date(('0' + (date.getMonth() + 1)).slice(-2) + '/' + ('0' + date.getDate()).slice(-2) + '/' + date.getFullYear());
    //$scope.CompletionDate = $scope.VchDate;
    //$scope.EstDate = $scope.VchDate;
    //$scope.AssignDate = $scope.VchDate;

    $scope.Api = MyFactory.GetApi();

    $scope.TaskID = document.getElementById('lbltaskid').innerHTML;

    $scope.CallID = document.getElementById('CallTaskId').innerHTML;

    $scope.EmpCode = document.getElementById('UserCode').innerHTML;

    $scope.GetitemtypeList = function () {
        $http.get($scope.Api + '/Values/GetItemTypeRecord/').success(function (data) {
            $scope.Listitemtype = data;
        }).error(function (data) {
            //alert('error');
        });
    }                                   //added
    $scope.GetitemtypeList();

    $scope.GetpriorityList = function () {
        $http.get($scope.Api + '/Values/GetpriorityRecord/').success(function (data) {
            $scope.Listpriority = data;

        }).error(function (data) {
            //alert('error');
        });
    }                                   //added
    $scope.GetpriorityList();

    $scope.GetstatusList = function () {
        $http.get($scope.Api + '/Values/GettaskRecord/').success(function (data) {
            $scope.Liststatus = data;

        }).error(function (data) {
            //alert('error');
        });
    }                                       //added
    $scope.GetstatusList();


    $scope.showconfirmbox = function () {
        if ($window.confirm("Do you want to Save Task?"))
            $scope.result = "Yes";
        else
            $scope.result = "No";
    }

    $scope.AddAssign = function () {
        angular.forEach($scope.ListAssignTo, function (value, index) {
            if ($scope.ListAssignTo[index].Code == $scope.AssignTo) {

                $scope.RowData = {};
                $scope.RowData.Code = $scope.AssignTo;
                $scope.RowData.Name = $scope.ListAssignTo[index].Desc;
                if ($scope.AsslignList.indexOf($scope.RowData) == -1) {
                    $scope.AsslignList.push($scope.RowData);
                }
            }
        });
    }
    $scope.AddAssign();


    $scope.GetRecords = function () {
        $http.get($scope.Api + '/Values/GetProjectName/').success(function (data) {
            $scope.ListProject = data;

        }).error(function (data) {
            //alert('error');
        });
    }                                           //added
    $scope.GetRecords();

    $scope.GetRecords1 = function () {
        $http.get($scope.Api + '/Values/GetUserName/').success(function (data) {
            $scope.ListAssignTo = data;

        }).error(function (data) {
            //alert('error');
        });
    }                                           //added
    $scope.GetRecords1();


    //$scope.GetRecords = function () {

    //    $http.get($scope.Api + '/Task/TaskInfo/').success(function (data) {
    //        $scope.TaskList = data;

    //        $scope.ListClient = $scope.TaskList[0].Client;
    //        $scope.ListAssignTo = $scope.TaskList[0].AssignTo;



    //    }).error(function (data) {
    //        //alert('error');
    //    });
    //}
    //$scope.GetRecords();


    $scope.Getlistedit = function (){
        if ($scope.TaskID > 0) {

            $http.get($scope.Api + '/Values/GetTask/', { params: { "TaskID": $scope.TaskID } }).success(function (data) {

                $scope.TaskList1 = data;

                $scope.Project = $scope.TaskList1[0].Projectid;
                $scope.ID = $scope.TaskList1[0].ID;
                $scope.Client2 = $scope.TaskList1[0].Clientid;
                $scope.Task = $scope.TaskList1[0].Task;
                $scope.AssignDate = $scope.TaskList1[0].AssignDate;
                $scope.AssignTo = $scope.TaskList1[0].AssignToId;
                $scope.ActualTime = $scope.TaskList1[0].ActualTime;
                $scope.EstTime = $scope.TaskList1[0].EstTime;
                $scope.StatusID = "" + $scope.TaskList1[0].Status;
                $scope.ImpStatus = "" + $scope.TaskList1[0].ImpStatus;
                $scope.Checkedby = "" + $scope.TaskList1[0].Checkedby;
                $scope.EstDate = $scope.TaskList1[0].EstDate;
                $scope.CompletionDate = $scope.TaskList1[0].CompletionDate;
                $scope.CompletedVer = $scope.TaskList1[0].CompletedVer;
                $scope.Unimple = $scope.TaskList1[0].Unimple;
                $scope.Setting = $scope.TaskList1[0].Setting;
                $scope.Closing = $scope.TaskList1[0].Closing;

                $scope.Taskid = $scope.TaskList1[0].Taskid;
                $scope.TaskDesc = $scope.TaskList1[0].TaskDesc;
                $scope.TaskPoint = $scope.TaskList1[0].TaskPoint;
                $scope.PriorityID = $scope.TaskList1[0].Priority;
                $scope.ItemTypeID = $scope.TaskList1[0].ItemType;

                $scope.Client = $scope.Client2
                //alert($scope.Project);

            }).error(function (data) {
                //alert('error');
            });
        }
    }                                           //added
    $scope.Getlistedit();


    $scope.Getcallistedit = function () {
        if ($scope.CallID > 0) {

            $http.get($scope.Api + '/Values/ConvertCallToTask/', { params: { "CallID": $scope.CallID } }).success(function (data) {

                $scope.TaskList2 = data;

                $scope.Project = $scope.TaskList2[0].Projectid;
                $scope.Client2 = $scope.TaskList2[0].Clientid;
                $scope.AssignTo = $scope.TaskList2[0].AssignToId;
                $scope.Task = $scope.TaskList2[0].Task;
                $scope.AssignDate = $scope.TaskList2[0].AssignDate;
                $scope.Client = $scope.Client2;

            }).error(function (data) {
                //alert('error');
            });
        }
    }                                        //added
    $scope.Getcallistedit();



    $scope.SaveRecords = function () {
        if ($scope.fileList.length > 5) {
            alert("Only 5 Attachment allowed ");
            return;
        }
        else if ($scope.Project == '' || $scope.Project == null) {
            alert("Please Enter Project Name");
            return false;
        }
        else if ($scope.ItemTypeID == '' || $scope.ItemTypeID == null) {
            alert("Please Enter Item Type");
            return false;
        }
        else if ($scope.Task == '' || $scope.Task == null) {
            alert("Please Enter Task Name");
            return false;
        }




        var date2 = new Date($scope.EstDate);
        var VchDate2 = date2.toLocaleString();
        VchDate2 = VchDate2.replace('at ', '');

        var date3 = new Date($scope.CompletionDate);
        var VchDate3 = date3.toLocaleString();
        VchDate3 = VchDate3.replace('at ', '');


        $scope.ListTask.Project = $scope.Project;
        $scope.ListTask.Client = $scope.Client;
        $scope.ListTask.Task = $scope.Task;
        $scope.ListTask.TaskDesc = $scope.TaskDesc;
        $scope.ListTask.AssignDate = $('#ADate')[0].value;
        $scope.ListTask.AssignTo = $scope.AssignTo;
        $scope.ListTask.ActualTime = $scope.ActualTime;
        $scope.ListTask.EstTime = $scope.EstTime;
        $scope.ListTask.Status = $scope.StatusID;
        $scope.ListTask.ImpStatus = $scope.ImpStatus;
        $scope.ListTask.Checkedby = $scope.Checkedby;
        $scope.ListTask.EstDate = $('#EDate')[0].value;
        $scope.ListTask.CompletionDate = $('#CDate')[0].value;
        $scope.ListTask.CompletedVer = $scope.CompletedVer;
        $scope.ListTask.Unimple = $scope.Unimple;
        $scope.ListTask.Setting = $scope.Setting;
        $scope.ListTask.Closing = $scope.Closing;
        $scope.ListTask.EmpCode = $scope.EmpCode;
        $scope.ListTask.TaskID = $scope.TaskID;
        $scope.ListTask.CallID = $scope.CallID;
        $scope.ListTask.ItemType = $scope.ItemTypeID;
        $scope.ListTask.Priority = $scope.PriorityID;
        $scope.ListTask.TaskPoint = $scope.TaskPoint;
        $scope.ListTask.AsslignList = $scope.AsslignList;

        //$scope.Taskid = $scope.Taskid;

        $http.post($scope.Api + '/Values/SaveTask/', $scope.ListTask).success(function (data) {
            var array = data.split(',');

            $scope.ID = array[0];
            alert(array[1]);


            for (var i = 0; i < $scope.fileList.length; i++) {

                $scope.Attachment = $scope.Attachment + $scope.fileList[i].file.name;
                //$scope.Attachment = $scope.fileList[i].file.name;
                $scope.UploadFileIndividual($scope.fileList[i].file,
                    $scope.fileList[i].file.name,
                    $scope.fileList[i].file.type,
                    $scope.fileList[i].file.size,
                    $scope.ID,
                    i);

            }

            if (array[1] == "Saved" || array[1] == "UpDate") {
                $scope.ClearControl();
            }


        }).error(function (data) {
            alert('Error');
        });
    }                                   //added

    //.success(function (response) {
    //    alert(response);
    //    if (response == "Saved" || response == "UpDate") {
    //        $scope.ClearControl();
    //    }
    //})

    $scope.ClearControl = function () {
        $scope.Project = '';
        $scope.PriorityID = '';
        $scope.Client = '';
        $scope.Task = '';
        $scope.TaskDesc = '';
        $scope.ItemTypeID = '';
        $scope.AssignDate = $scope.VchDate;
        $scope.AssignTo = '';
        $scope.TaskID = 0;
        $scope.ActualTime = '';
        $scope.EstTime = '';
        $scope.StatusID = '';
        $scope.ImpStatus = '';
        $scope.TaskPoint = '';
        $scope.Checkedby = '';
        $scope.EstDate = $scope.VchDate;
        $scope.CompletionDate = $scope.VchDate;
        $scope.CompletedVer = '';
        $scope.Unimple = '';
        $scope.Setting = '';
        $scope.Closing = '';
        $scope.ID = 0;
        $scope.EmpCode = 0;
        $scope.fileList = [];
        $scope.Getlistedit();
        $scope.GetRecords();

        $scope.AsslignList = {};
    }




    $scope.setFile = function (element) {

        // get the files
        var files = element.files;
        for (var i = 0; i < files.length; i++) {
            $scope.ImageProperty.file = files[i];

            $scope.fileList.push($scope.ImageProperty);
            $scope.ImageProperty = {};
            $scope.$apply();

        }
    }

    $scope.UploadFileIndividual = function (fileToUpload, name, type, size, ID, index) {
        //Create XMLHttpRequest Object
        var reqObj = new XMLHttpRequest();
        //var fileExtension = type//'.' + name.file.name.split('.').pop();

        //name.file.name =$scope.VoucherDate +'.jpg' // new Date().getTime() + fileExtension;
        //event Handler
        reqObj.upload.addEventListener("progress", uploadProgress, false)
        reqObj.addEventListener("load", uploadComplete, false)
        reqObj.addEventListener("error", uploadFailed, false)
        reqObj.addEventListener("abort", uploadCanceled, false)


        //open the object and set method of call(get/post), url to call, isasynchronous(true/False)
        reqObj.open("POST", "/Home/UploadFiles", true);

        //set Content-Type at request header.For file upload it's value must be multipart/form-data
        reqObj.setRequestHeader("Content-Type", "multipart/form-data");

        //Set Other header like file name,size and type
        reqObj.setRequestHeader('X-File-Name', name);
        reqObj.setRequestHeader('X-File-Type', type);
        reqObj.setRequestHeader('X-File-Size', size);
        reqObj.setRequestHeader('ID', "" + ID);

        // send the file
        reqObj.send(fileToUpload);

        function uploadProgress(evt) {
            if (evt.lengthComputable) {

                var uploadProgressCount = Math.round(evt.loaded * 100 / evt.total);

                document.getElementById('P' + index).innerHTML = uploadProgressCount;

                if (uploadProgressCount == 100) {
                    document.getElementById('P' + index).innerHTML =
                        '<i class="fa fa-refresh fa-spin" style="color:maroon;"></i>';
                }

            }
        }

        function uploadComplete(evt) {
            /* This event is raised when the server  back a response */

            document.getElementById('P' + index).innerHTML = 'Saved';
            $scope.NoOfFileSaved++;
            $scope.$apply();
        }

        function uploadFailed(evt) {
            document.getElementById('P' + index).innerHTML = 'Upload Failed..';
        }

        function uploadCanceled(evt) {

            document.getElementById('P' + index).innerHTML = 'Canceled....';
        }

    }


});