app.controller('ReferenceEntry', function ($scope, $http, $window, MyFactory) {
    debugger;
    $scope.VisitorRID = 0;

    $scope.Id = '';
    $scope.ReferenceName = '';
    $scope.PhoneNo = '';
    $scope.ContactPerson = '';
    $scope.EmailId = '';

    $scope.ID = 0;
    $scope.ReferenceID = 0;
    $scope.ListReferenceName = [];
    //$scope.ListReferenceName = {};
    $scope.ReferenceEntry = {};

    $scope.ListReferenceEntry = {};

    try {
        $scope.VisitorRID = document.getElementById('lbltaskid').innerHTML;
        //$scope.VisitorRID = document.getElementById('lbltaskid').innerHTML;
    }
    catch (e) {

    }

    $scope.Api = MyFactory.GetApi();

    $scope.GetRefList = function () {

        $http.get($scope.Api + '/Values/GetRefRecord/').success(function (data) {
            $scope.ListReferenceName = data;

        }).error(function (data) {
            //alert('error');
        });
    }        //Added
    $scope.GetRefList();

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

    $scope.SubmitReference = function () {

        if ($scope.ReferenceName == '' || $scope.ReferenceName == null) {
            alert("Please Enter Reference Name");
            return false;
        }
        if ($scope.ContactPerson == '' || $scope.ContactPerson == null) {
            alert("Please Enter ContactPerson");
            return false;
        } if ($scope.PhoneNo == '' || $scope.PhoneNo == null) {
            alert("Please Enter PhoneNo");
            return false;
        } if ($scope.EmailId == '' || $scope.EmailId == null) {
            alert("Please Enter EmailId");
            return false;
        }
        if (!$scope.ValidateEmail($scope.EmailId)) {
            return;
        }
        $scope.ReferenceEntry.ID = $scope.VisitorRID;
        $scope.ReferenceEntry.ReferenceName = $scope.ReferenceName;
        $scope.ReferenceEntry.PhoneNo = $scope.PhoneNo;
        $scope.ReferenceEntry.ContactPerson = $scope.ContactPerson;
        $scope.ReferenceEntry.EmailId = $scope.EmailId;
        //$scope.ReferenceEntry.Password = $scope.Password;

        $http.post($scope.Api + '/Values/SaveReferenceMaster/', $scope.ReferenceEntry).success(function (response) {





        }).success(function (response) {
            alert(response);
            if (response == "Saved" || response == "UpDate") {
                $scope.ClearControl();
            }
        }).error(function (data) {
            alert('Error');
        });

    }

    $scope.ClearControl = function () {
        $scope.ReferenceName = '';
        $scope.PhoneNo = '';
        $scope.ContactPerson = '';
        $scope.EmailId = '';

        $scope.ID = 0;
        $scope.GetRecords();
    }

    $scope.fnClick = function (code) {

        angular.forEach($scope.ListReferenceName, function (value, index) {
            if ($scope.ListReferenceName[index].ID == code) {
                $scope.ID = $scope.ListReferenceName[index].ID;
                $scope.ReferenceName = $scope.ListReferenceName[index].ReferenceName;
                $scope.EmailId = $scope.ListReferenceName[index].EmailId;
                $scope.PhoneNo = $scope.ListReferenceName[index].PhoneNo;
                $scope.ContactPerson = $scope.ListReferenceName[index].ContactPerson;
                //$scope.Password = $scope.ListReferenceEntry[index].Password;
            }


        });
    }




    if ($scope.VisitorRID > 0) {
       
        $http.get($scope.Api + '/Values/GetReferenceRecordById/', { params: { "VisitorRID": $scope.VisitorRID } }).success(function (data) {
            $scope.ListReferenceName = data;

            $scope.ReferenceName = $scope.ListReferenceName[0].ReferenceName;
            $scope.EmailId = $scope.ListReferenceName[0].EmailId;
            $scope.PhoneNo = $scope.ListReferenceName[0].PhoneNo;
            $scope.ContactPerson = $scope.ListReferenceName[0].ContactPerson;

        }).error(function (data) {
            //alert('error');
        });
    }               //added




});