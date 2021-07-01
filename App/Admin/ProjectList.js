app.controller('ProjectListData', function ($scope, $http, $window, MyFactory) {

    $scope.fromFactory = MyFactory.GetApi();

    $scope.VisitorPID = 0;
    $scope.Id = '';
    $scope.ProjectName = '';
    $scope.ProjectDescription = '';
    $scope.ReferenceName = '';
    $scope.Client = '';
    $scope.ProjectManager = '';
    $scope.ProjectLeader = '';
    $scope.AssignTo = '';
    $scope.Priority = '';

    $scope.Priority1 = '';

    $scope.ContactPerson = '';
    $scope.PhoneNo = '';
    $scope.ProjectValue = '';
    $scope.ProjectAdvance = '';


    //$scope.EstimatedStartDate = new Date().getTime();
    //$scope.EstimatedTargetDate = new Date().getTime();
    //$scope.ProjectCompletionDate = new Date().getTime();

    $scope.EstimatedStartDate = '';
    $scope.EstimatedTargetDate = '';
    $scope.ProjectCompletionDate = '';
    $scope.AsslignList = [];
    $scope.RowData = {};
    $scope.ID = 0;
    $scope.UnderSave = false;

    $scope.ListAssignTo = {};
    $scope.ListClient = {};

    $scope.ListProjectEntry = {};
    $scope.ListUserEntry2 = {};
    $scope.ListUserEntry3 = {};
    $scope.liTsk = {};




    $scope.ReferenceID = 0;
    $scope.ListReferenceName = [];
    $scope.Listpriority = [];
    $scope.PriorityID = '2';

    $scope.fileList = [];
    $scope.Api = MyFactory.GetApi();

    $scope.ImageProperty = {
        file: ''
    }



    $scope.VisitorPID = document.getElementById('lbltaskid').innerHTML;

    $scope.GetRecordsUserName = function () {
        $http.get($scope.fromFactory + '/Values/GetUserName/').success(function (data) {
            $scope.ListAssignTo = data;

        }).error(function (data) {
            //alert('error');
        });
    }
    $scope.GetRecordsUserName();

    $scope.GetRefList = function () {
        $http.get($scope.fromFactory + '/Values/GetRefRecord/').success(function (data) {
            $scope.ListReferenceName = data;

        }).error(function (data) {
            //alert('error');
        });
    }
    $scope.GetRefList();

    $scope.GetpriorityList = function () {
        $http.get($scope.fromFactory + '/Values/GetpriorityRecord/').success(function (data) {
            $scope.Listpriority = data;

        }).error(function (data) {
            //alert('error');
        });
    }
    $scope.GetpriorityList();






    $scope.AddAssign = function () {
        $scope.RowData = {};
        angular.forEach($scope.ListAssignTo, function (value, index) {
            if ($scope.ListAssignTo[index].Code == $scope.AssignTo) {
                $scope.RowData.Code = $scope.AssignTo;
                $scope.RowData.Name = $scope.ListAssignTo[index].Desc;


            }
        });

        angular.forEach($scope.AsslignList, function (value, index) {
            if (value.Code == $scope.RowData.Code) {
                return;
            }
        });


        $scope.AsslignList.push($scope.RowData);

    }



    //$scope.AddContact = function () {
    //    angular.forEach($scope.ListAssignTo, function (value, index) {
    //        if ($scope.ListAssignTo[index].Code == $scope.AssignTo) {

    //            $scope.RowData = {};
    //            $scope.RowData.Code = $scope.AssignTo;
    //            $scope.RowData.Name = $scope.ListAssignTo[index].Desc;
    //            if ($scope.AsslignList.indexOf($scope.RowData) == -1) {
    //                $scope.AsslignList.push($scope.RowData);
    //            }
    //        }
    //    });
    //}
    //$scope.AddContact = function () {


    //$scope.Getclientlistedit = function () {
    //    $http.get($scope.Api + '/Task/TaskInfo/').then(function (data) {
    //        $scope.TaskList = data;
    //        $scope.ListClient = $scope.TaskList[0].Client;

    //    }).error(function (data) {
    //        //alert('error');
    //    });
    //}
    //$scope.Getclientlistedit();

    $scope.Getclientlistedit = function () {
        $http.get($scope.fromFactory + '/Values/GetClientRecord/').success(function (data) {
            $scope.ListClient = data;

        }).error(function (data) {
            //alert('error');
        });
    }
    $scope.Getclientlistedit();

    $scope.Getlistedit = function () {
        if ($scope.VisitorPID > 0) {

            $http.get($scope.fromFactory + '/Values/GetProjectRecordById/', { params: { "VisitorPID": $scope.VisitorPID } }).success(function (data) {

                $scope.ListProjectEntry = data;

                $scope.ProjectName = $scope.ListProjectEntry[0].ProjectName;
                $scope.ProjectLeader = $scope.ListProjectEntry[0].ProjectLeader;
                $scope.ContactPerson = $scope.ListProjectEntry[0].ContactPerson;
                $scope.PhoneNo = $scope.ListProjectEntry[0].PhoneNo;
                $scope.ReferenceID = $scope.ListProjectEntry[0].ReferenceName;
                $scope.PriorityID = $scope.ListProjectEntry[0].Priority;
                $scope.AssignTo = $scope.ListProjectEntry[0].AssignTo;
                $scope.Client = $scope.ListProjectEntry[0].Client;
                $scope.ProjectDescription = $scope.ListProjectEntry[0].ProjectDescription;

                $scope.ProjectManager = $scope.ListProjectEntry[0].ProjectManager;
                $scope.ProjectValue = $scope.ListProjectEntry[0].ProjectValue;
                //$scope.ProjectPoint = $scope.ListProjectEntry[0].ProjectPoint;
                $scope.ProjectAdvance = $scope.ListProjectEntry[0].ProjectAdvance;
                $scope.ProjectCompletionDate = $scope.ListProjectEntry[0].ProjectCompletionDate;
                $scope.EstimatedStartDate = $scope.ListProjectEntry[0].EstimatedStartDate;
                $scope.EstimatedTargetDate = $scope.ListProjectEntry[0].EstimatedTargetDate;
            }).error(function (data) {
                //alert('error');
            });
        }
    }
    $scope.Getlistedit();


    $scope.GetRecordslist = function () {

        $http.get($scope.fromFactory + '/Values/GetProjectRecord/').success(function (data) {
            $scope.ListProjectEntry = data;

        }).error(function (data) {
            //alert('error');
        });

    }
    $scope.GetRecordslist();

    //phone no allow ony int value
    $scope.AllowDigitonly = function ($event) {
        if (isNaN(String.fromCharCode($event.keyCode))) {
            alert("Enter Number Only");
            $event.preventDefault();
        }
    };



    $scope.SubmitDisc = function () {

        if (!$scope.UnderSave) {

            $scope.UnderSave = true;
            if ($scope.ProjectName == '' || $scope.ProjectName == null) {
                alert("Please Enter Project Name");
                $scope.UnderSave = false;
                return false;
            }

            else if ($scope.ProjectManager == '' || $scope.ProjectManager == null) {
                alert("Please Enter Project Manager");
                $scope.UnderSave = false;
                return false;
            }
            //else if ($scope.AssignTo == '' || $scope.AssignTo == null) {
            //    alert("Please Enter AssignTo");
            //    $scope.UnderSave = false;
            //    return false;
            //}
            else if ($scope.Client == '' || $scope.Client == null) {
                alert("Please Enter Client Name");
                $scope.UnderSave = false;
                return false;
            }



            $scope.ListUserEntry2.ID = $scope.VisitorPID;
            $scope.ListUserEntry2.ProjectName = $scope.ProjectName;
            $scope.ListUserEntry2.ProjectLeader = $scope.ProjectLeader;
            $scope.ListUserEntry2.ContactPerson = $scope.ContactPerson;
            $scope.ListUserEntry2.PhoneNo = $scope.PhoneNo;
            $scope.ListUserEntry2.EstimatedTargetDate = $('#CoDate')[0].value;
            $scope.ListUserEntry2.ReferenceID = $scope.ReferenceID;
            $scope.ListUserEntry2.Priority = $scope.PriorityID;
            $scope.ListUserEntry2.Client = $scope.Client;
            $scope.ListUserEntry2.ProjectManager = $scope.ProjectManager;
            $scope.ListUserEntry2.ProjectDescription = $scope.ProjectDescription;
            $scope.ListUserEntry2.AssignTo = $scope.AssignTo;
            $scope.ListUserEntry2.ProjectValue = $scope.ProjectValue;
            $scope.ListUserEntry2.ProjectAdvance = $scope.ProjectAdvance;
            $scope.ListUserEntry2.EstimatedStartDate = $('#EsDate')[0].value;
            $scope.ListUserEntry2.AsslignList = $scope.AsslignList;

            $http.post($scope.Api + '/Values/SaveProjectMaster/', $scope.ListUserEntry2).success(function (data) {
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
            $scope.UnderSave = false;
        }

    }
    $scope.Priority = function (ID) {
        $scope.IsShow = true;



    }

    $scope.cancel = function () {
        $scope.IsShow = false;
        $window.location.reload();


    }



    // When the user clicks on div, open the popup
    $scope.myFunction = function () {
        $scope.show = true;
    }

    $scope.SavePriority = function (ID) {

        //    if ($scope.Reason == '' || $scope.Reason == null) {
        //        alert("Please Enter Reason");
        //    return false;
        //}
        //    else if ($scope.StatusID == '' || $scope.StatusID == null) {
        //        alert("Please Enter StatusID");
        //        return false;
        //    }


        //$scope.ListUserEntry3.StatusID = $scope.StatusID;
        $scope.ListUserEntry3.PriorityID = $scope.PriorityID;
        $scope.ListUserEntry3.ID = $scope.VisitorPID;


        // $scope.ListUserEntry3.TaskID = document.getElementById('getTaskValue').innerHTML;



        $http.post($scope.fromFactory + '/Values/SavePriority/', $scope.ListUserEntry3).success(function (response) {

            alert(response);
            $window.location.reload();
        }).error(function (data) {
            alert('Error');
        });
    }


    $scope.ClearControl = function () {
        $scope.Client = '';
        $scope.PriorityID = '';
        $scope.ReferenceID = '';
        $scope.ProjectName = '';
        $scope.ProjectLeader = '';
        $scope.ContactPerson = '';
        $scope.PhoneNo = '';
        $scope.estimatedstartdate = '';
        $scope.estimatedtargetdate = '';
        $scope.ProjectManager = '';
        $scope.ProjectDescription = '';
        $scope.AssignTo = '';
        $scope.ProjectValue = '';
        $scope.ProjectAdvance = '';
        $scope.ID = 0;
        $scope.GetRecordslist();
        $scope.fileList = [];
        $scope.AsslignList = [];
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


    //$scope.UploadFileIndividual = function (fileToUpload, name, type, size, ID, index) {
    //    //Create XMLHttpRequest Object
    //    var reqObj = new XMLHttpRequest();
    //    //var fileExtension = type//'.' + name.file.name.split('.').pop();

    //    //name.file.name =$scope.VoucherDate +'.jpg' // new Date().getTime() + fileExtension;
    //    //event Handler
    //    reqObj.upload.addEventListener("progress", uploadProgress, false)
    //    reqObj.addEventListener("load", uploadComplete, false)
    //    reqObj.addEventListener("error", uploadFailed, false)
    //    reqObj.addEventListener("abort", uploadCanceled, false)


    //    //open the object and set method of call(get/post), url to call, isasynchronous(true/False)
    //    reqObj.open("POST", "/Home/UploadProjectFiles", true);

    //    //set Content-Type at request header.For file upload it's value must be multipart/form-data
    //    reqObj.setRequestHeader("Content-Type", "multipart/form-data");

    //    //Set Other header like file name,size and type
    //    reqObj.setRequestHeader('X-File-Name', name);
    //    reqObj.setRequestHeader('X-File-Type', type);
    //    reqObj.setRequestHeader('X-File-Size', size);
    //    reqObj.setRequestHeader('ID', "" + ID);

    //    // send the file
    //    reqObj.send(fileToUpload);

    //    function uploadProgress(evt) {
    //        if (evt.lengthComputable) {

    //            var uploadProgressCount = Math.round(evt.loaded * 100 / evt.total);

    //            document.getElementById('P' + index).innerHTML = uploadProgressCount;

    //            if (uploadProgressCount == 100) {
    //                document.getElementById('P' + index).innerHTML =
    //                    '<i class="fa fa-refresh fa-spin" style="color:maroon;"></i>';
    //            }

    //        }
    //    }

    //    function uploadComplete(evt) {
    //        /* This event is raised when the server  back a response */

    //        document.getElementById('P' + index).innerHTML = 'Saved';
    //        $scope.NoOfFileSaved++;
    //        $scope.$apply();
    //    }

    //    function uploadFailed(evt) {
    //        document.getElementById('P' + index).innerHTML = 'Upload Failed..';
    //    }

    //    function uploadCanceled(evt) {

    //        document.getElementById('P' + index).innerHTML = 'Canceled....';
    //    }

    //}


    $scope.fnClick = function (code) {

        angular.forEach($scope.ListProjectEntry, function (value, index) {
            if ($scope.ListProjectEntry[index].ID == code) {
                $scope.ID = $scope.ListProjectEntry[index].ID;
                $scope.ProjectName = $scope.ListProjectEntry[index].ProjectName;
                $scope.ProjectLeader = $scope.ListProjectEntry[index].ProjectLeader;
                $scope.PhoneNo = $scope.ListProjectEntry[index].PhoneNo;
                $scope.ContactPerson = $scope.ListProjectEntry[index].ContactPerson;
                $scope.EstimatedStartDate = $scope.ListProjectEntry[index].EstimatedStartDate;
                $scope.EstimatedTargetDate = $scope.ListProjectEntry[index].EstimatedTargetDate;
                $scope.ProjectValue = $scope.ListProjectEntry[index].ProjectValue;
                $scope.ProjectAdvance = $scope.ListProjectEntry[index].ProjectAdvance;
                $scope.AssignTo = $scope.ListProjectEntry[index].AssignTo;
                $scope.ProjectDescription = $scope.ListProjectEntry[index].ProjectDescription;
                $scope.ProjectManager = $scope.ListProjectEntry[index].ProjectManager;
                $scope.ReferenceID = $scope.ListProjectEntry[index].ReferenceID;
                $scope.Priority = $scope.ListProjectEntry[index].Priority;
                $scope.Client = $scope.ListProjectEntry[index].Client;

            }


        });
    }



});