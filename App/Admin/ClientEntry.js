app.controller('ClientEntry', function ($scope, $http, $window, MyFactory) {


    $scope.VisitorCID = 0;
    $scope.Id = '';
    $scope.ClientName = '';
    $scope.ReferenceID = '';
    $scope.PhoneNo = '';
    $scope.ContactPerson = '';
    $scope.EmailId = '';
    $scope.Password = '';
    $scope.ID = 0;
    //$scope.ReferenceID = 0;
    $scope.ListReferenceName = [];


    $scope.ClientEntry = {};

    $scope.ListClientEntry = {};

    $scope.Api = MyFactory.GetApi();


    $scope.VisitorCID = document.getElementById('lblclientid').innerHTML;



    $scope.GetRefList = function () {
        $http.get($scope.Api + '/Values/GetRefRecord/').success(function (data) {
            $scope.ListReferenceName = data;

        }).error(function (data) {
            //alert('error');
        }); 
    }                           //added
    $scope.GetRefList();

    $scope.Getlistedit = function () {
        if ($scope.VisitorCID > 0) {

            $http.get($scope.Api + '/Values/GetClientRecordById/', { params: { "VisitorCID": $scope.VisitorCID } }).success(function (data) {
                $scope.ListClientEntry = data;
                $scope.ClientName = $scope.ListClientEntry[0].ClientName;
                $scope.PhoneNo = $scope.ListClientEntry[0].PhoneNo;
                $scope.ContactPerson = $scope.ListClientEntry[0].ContactPerson;
                $scope.EmailId = $scope.ListClientEntry[0].EmailId;
                $scope.ReferenceID = $scope.ListClientEntry[0].ReferenceID;

            }).error(function (data) {
                //alert('error');
            });
        }
    }                           //added
    $scope.Getlistedit();


    $scope.GetRecords = function () {
        $http.get($scope.Api + '/Values/GetClientRecord/').success(function (data) {
            $scope.ListClientEntry = data;

        }).error(function (data) {
            //alert('error');
        });
    }                           //added
    $scope.GetRecords();


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



    //phone no allow ony int value
    $scope.AllowDigitonly = function ($event) {
        if (isNaN(String.fromCharCode($event.keyCode))) {
            alert("Enter Number Only");
            $event.preventDefault();
        }
    };

    $scope.SubmitClient = function () {

        if ($scope.ClientName == '' || $scope.ClientName == null) {
            alert("Please Enter Client Name");
            return false;
        }
        if ($scope.PhoneNo == '' || $scope.PhoneNo == null) {
            alert("Please Enter Mobile No.");
            return false;
        } if ($scope.EmailId == '' || $scope.EmailId == null) {
            alert("Please Enter EmailId");
            return false;
        }
        if (!$scope.ValidateEmail($scope.EmailId)) {
            return;
        }


        $scope.ClientEntry.ID = $scope.VisitorCID;
        $scope.ClientEntry.ClientName = $scope.ClientName;
        $scope.ClientEntry.PhoneNo = $scope.PhoneNo;
        $scope.ClientEntry.ContactPerson = $scope.ContactPerson;
        $scope.ClientEntry.EmailId = $scope.EmailId;
        //$scope.ClientEntry.Password = $scope.Password;
        $scope.ClientEntry.ReferenceID = $scope.ReferenceID;

        $http.post($scope.Api + '/Values/SaveClientMaster/', $scope.ClientEntry).success(function (response) {


        }).success(function (response) {
            alert(response);
            if (response == "Saved" || response == "UpDate") {
                $scope.ClearControl();
            }
        }).error(function (data) {
            alert('Error');
        });

    }                           //added

    $scope.ClearControl = function () {
        $scope.ClientName = '';
        $scope.ReferenceID = '';
        $scope.PhoneNo = '';
        $scope.ContactPerson = '';
        $scope.EmailId = '';
        $scope.Password = '';
        $scope.ID = 0;
        $scope.GetRecords();
    }

    $scope.fnClick = function (code) {

        angular.forEach($scope.ListClientEntry, function (value, index) {

            if ($scope.ListClientEntry[index].ID == code) {
                $scope.ID = $scope.ListClientEntry[index].ID;
                $scope.ClientName = $scope.ListClientEntry[index].ClientName;
                $scope.EmailId = $scope.ListClientEntry[index].EmailId;
                $scope.PhoneNo = $scope.ListClientEntry[index].PhoneNo;
                $scope.ContactPerson = $scope.ListClientEntry[index].ContactPerson;
                $scope.Password = $scope.ListClientEntry[index].Password;


            }


        });
    }

});