app.controller('UserEntry', function ($scope, $http, $window, MyFactory) {


    $scope.VisitorUID = 0;

    $scope.UserName = '';
    $scope.Mobile = '';
    $scope.MailID = '';
    $scope.Password = '';
    $scope.UserType = '';
    $scope.PassID = 0;
    $scope.Id = 0;
    $scope.ECPassword = '';
    $scope.EmpCode = 0;
    $scope.IsShowUsertype = false;
    $scope.Deactivate = 0;
    $scope.TeamLeader = 0;

    $scope.Usetypelist = {};
    $scope.ListUserEntry = {};
    $scope.ListUserEntry2 = {};
    $scope.ListUserEntry3 = {};
    $scope.TaskList = {};
    $scope.Api = MyFactory.GetApi();

    $scope.EmpCode = document.getElementById('UserCode').innerHTML;
  






    $scope.GetUserTypeData = function () {
        $http.get($scope.Api + '/Values/GetUserType/').success(function (data) {
            $scope.Usetypelist = data;
        }).error(function (data) {
            alert('error');
        });
    }                   //added              
    $scope.GetUserTypeData();



    
    $scope.Getlistedit = function () {
        if ($scope.VisitorUID > 0) {
            $http.get($scope.Api + '/Values/GetUserListById/', { params: { "VisitorUID": $scope.VisitorUID } }).success(function (data) {
                $scope.ListUserEntry = data;
                $scope.UserName = $scope.ListUserEntry[0].UserName;
                $scope.MailID = $scope.ListUserEntry[0].MailID;
                $scope.Mobile = $scope.ListUserEntry[0].Mobile;
                $scope.UserType = $scope.ListUserEntry[0].UserType;
                $scope.Password = $scope.ListUserEntry[0].Password;
            }).error(function (data) {
                //alert('error');
            });
        }
    }                       //added           
    $scope.Getlistedit();

    $scope.ValidateEmail = function (inputText) {
        var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        if (inputText.match(mailformat)) {
            return true;
        }
        else {
            alert("You have entered an invalid email address!");
            return false;
        }
    }

    $scope.showconfirmbox = function () {
        if ($window.confirm("Do you want to Save Submit?"))
            $scope.result = "Yes";
        else
            $scope.result = "No";
    }

    $scope.GetRecords = function () {
        $http.get($scope.Api + '/Values/TaskInfo/').success(function (data) {
            $scope.TaskList = data;
            $scope.ListAssignTo = $scope.TaskList[0].AssignTo;
        }).error(function (data) {
            //alert('error');
        });
    }                           //added
    $scope.GetRecords();

    $scope.GetuserRecords = function () {
        $http.get($scope.Api + '/Values/GetUserList/', { params: { "userid": $scope.EmpCode } }).success(function (data) {
            $scope.ListUserEntry = data;
            $scope.UserType = $scope.ListUserEntry[0].UserType;
           
            if ($scope.UserType == 1) {
                $scope.IsShowUsertype = true;
            }
            else {
                $scope.IsShowUsertype = false;
            }
        }).error(function (data) {
            //alert('error');
        });
    }                   //added
    $scope.GetuserRecords();


    //phone no allow ony int value
    $scope.AllowDigitonly = function ($event) {
        if (isNaN(String.fromCharCode($event.keyCode))) {
            alert("Enter Number Only");
            $event.preventDefault();
        }
    };

    $scope.ValidateEmail = function (inputText) {
        var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        if (inputText.match(mailformat)) {
            return true;
        }
        else {
            alert("You have entered an invalid email address!");
            return false;
        }
    }



    $scope.submit = function () {

        if (!$scope.ValidateEmail($scope.MailID)) {
            return;
        }
        if ($scope.UserName == '' || $scope.UserName == null) {
            alert("Please Enter UserName");
            return false;
        }
        else if ($scope.Mobile == '' || $scope.Mobile == null) {
            alert("Please Enter Mobile");
            return false;
        }
        else if ($scope.MailID == '' || $scope.MailID == null) {
            alert("Please Enter MailID");
            return false;
        }
        else if ($scope.UserType == '' || $scope.UserType == null) {
            alert("Please Enter UserType");
            return false;
        }
        $scope.ListUserEntry2 = {};
        $scope.ListUserEntry2.Id = $scope.VisitorUID
        $scope.ListUserEntry2.Mobile = $scope.Mobile;
        $scope.ListUserEntry2.MailID = $scope.MailID;
        $scope.ListUserEntry2.Password = $scope.Password;
        $scope.ListUserEntry2.UserType = $scope.UserType;
        $scope.ListUserEntry2.PassID = $scope.PassID;
        $scope.ListUserEntry2.Deactivate = $scope.Deactivate;
        $scope.ListUserEntry2.TeamLeader = $scope.TeamLeader;
        $scope.ListUserEntry2.UserName = $scope.UserName;

        $http.post($scope.Api + '/Values/UserEntry/', $scope.ListUserEntry2).success(function (response) {

            alert(response);
            if (response == "Saved" || response == "UpDate") {


                $scope.UserName = '';
                $scope.Mobile = '';
                $scope.MailID = '';
                $scope.Password = '';
                $scope.Id = '';
                $scope.UserType = '';
                $scope.GetRecords();
            }

        }).error(function (data) {
            alert('Error');
        });

    }                       //added


    $scope.exportUserList = function () {
        var blob = new Blob([document.getElementById('exportable3').innerHTML], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
        });
        saveAs(blob, "User List.xls");
    };



    $scope.ValidatePassword = function () {
        $scope.ListUserEntry3 = {};
        var result = false;
        var password = document.getElementById("txtPassword").value;
        var confirmPassword = document.getElementById("txtConfirmPassword").value;
        if (password != confirmPassword) {
            alert("Passwords do not match.");
            result = false;
        }
        else {
            result = $scope.submit();
            $scope.GetRecords();
            $scope.ClearControl();

        }
        return result;

    };

    $scope.fnClick = function (Code) {
        angular.forEach($scope.ListUserEntry, function (value, index) {
            if ($scope.ListUserEntry[index].ID == Code) {
                $scope.ID = $scope.ListUserEntry[index].ID;
                $scope.UserName = $scope.ListUserEntry[index].UserName;
                $scope.Mobile = $scope.ListUserEntry[index].Mobile;
                $scope.MailID = $scope.ListUserEntry[index].MailID;
                $scope.PassWord = $scope.ListUserEntry[index].PassWord;
                $scope.ECPassword = $scope.ListUserEntry[index].PassWord;
                $scope.MailID = $scope.ListUserEntry[index].MailID;
                $scope.UserType = $scope.ListUserEntry[index].UserType;
                $scope.UserType1 = $scope.UserType;
                $scope.Deactivate = $scope.ListUserEntry[index].Deactivate;
                $scope.TeamLeader = $scope.ListUserEntry[index].TeamLeader;
            }
        });
    }                      


});