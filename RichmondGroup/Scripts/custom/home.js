var app = angular.module('app', ['ngResource']);
app.controller('HomeController', function ($scope, $http) {
    "use strict";

    //Set initial Values
    //Date formatter
    $scope.ConvertDateToSend = function (currentDt) {
        var mm = currentDt.getMonth() + 1;
        mm = (mm < 10) ? '0' + mm : mm;
        var dd = currentDt.getDate();
        dd = (dd < 10) ? '0' + dd : dd;
        var yyyy = currentDt.getFullYear();
        var date = mm + '/' + dd + '/' + yyyy;
        return date;
    }
    $scope.selectedDate = $scope.ConvertDateToSend(new Date());
    $("#dvMaxEngineer").hide();
    $("#datepicker").datepicker({
        beforeShowDay: $.datepicker.noWeekends,
        onSelect: function (date) {
           // var selectedDayString = $scope.ConvertDateToSend(date);
            var query = "?selectedDayString=" + date;
            var post = $http({
                method: "POST",
                url: "Default.aspx/GetNumberofScheduleForDate" + query,
                dataType: 'json',
                data: {}
    ,            //data: { name: $scope.Name },
                headers: { "Content-Type": "application/json" }
            });
            post.then(function (response) {
                if (response.data.d < 2) {
                    $("#btnChooseEngineer").removeAttr("disabled");
                    $("#dvMaxEngineer").hide();
                    $scope.selectedDate = date;
                } else {
                    $("#btnChooseEngineer").attr("disabled", "disabled");
                    $("#dvMaxEngineer").show();
                    //alert("Maximum number of engineers have already assigned for this date.");
                }

            });
        }
    });
    //End

    //Populate all past schedules
    $scope.GetAssignedSchedules = function () {

        var date = new Date();
        var defaultDate = $scope.ConvertDateToSend(date);// date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
        var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
        var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
        var firstDayString = $scope.ConvertDateToSend(firstDay); //firstDay.toLocaleDateString("en-US");
        var lastDayString = $scope.ConvertDateToSend(lastDay); //lastDay.toLocaleDateString("en-US");
        var query = "?fromString=" + firstDayString + "&toString=" + lastDayString;
        var post = $http({
            method: "POST",
            url: "Default.aspx/GetAssignedSchedules" + query,
            dataType: 'json',
            data: {}
,            //data: { name: $scope.Name },
            headers: { "Content-Type": "application/json" }
        });
        post.then(function (response) {
            $('#calendar').fullCalendar({
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,basicWeek,basicDay'
                },
                defaultDate: defaultDate,
                navLinks: true, // can click day/week names to navigate views
                editable: true,
                eventLimit: true, // allow "more" link when too many events
                events: response.data.d
            });

        });

    };
    $scope.GetAssignedSchedules();
    //End

    //Refresh current Schedules after update
    $scope.GetUpdatedAssignedSchedules = function () {

        var date = new Date();
        var defaultDate = $scope.ConvertDateToSend(date); //date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
        var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
        var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
        var firstDayString = $scope.ConvertDateToSend(firstDay); //firstDay.toLocaleDateString("en-US");
        var lastDayString = $scope.ConvertDateToSend(lastDay); //lastDay.toLocaleDateString("en-US");
        var query = "?fromString=" + firstDayString + "&toString=" + lastDayString;
        var post = $http({
            method: "POST",
            url: "Default.aspx/GetAssignedSchedules" + query,
            dataType: 'json',
            data: {}
,            //data: { name: $scope.Name },
            headers: { "Content-Type": "application/json" }
        });
        post.then(function (response) {
            $('#calendar').fullCalendar('removeEvents');
            $('#calendar').fullCalendar('addEventSource', response.data.d);
            $('#calendar').fullCalendar('rerenderEvents');
            $scope.selectedDate = new Date().toLocaleDateString("en-US");
        });

    };
    //End

    //Check for if there are already 2 schedules for a selected date
    //if not than add random engineer which meets requirements
    //if there are 2 schedules for a selected date, prompt to select another date
    $scope.GetLuckyEngineer = function () {
        var date = new Date($scope.selectedDate);
        var selectedDayString = $scope.ConvertDateToSend(date); // date.toLocaleDateString("en-US");
        var query = "?selectedDayString=" + selectedDayString;
        var post = $http({
            method: "POST",
            url: "Default.aspx/GetNumberofScheduleForDate" + query,
            dataType: 'json',
            data: {}
,            //data: { name: $scope.Name },
            headers: { "Content-Type": "application/json" }
        });
        post.then(function (response) {
            if (response.data.d < 2) {

                $scope.AddEngineer();
            } else {
                $("#btnChooseEngineer").attr("disabled", "disabled");
                $("#dvMaxEngineer").show();
                //alert("Maximum number of engineers have already assigned for this date.");
            }

        });
    }
    $scope.AddEngineer = function () {
        var date = new Date($scope.selectedDate);
        var selectedDayString = $scope.ConvertDateToSend(date); //date.toLocaleDateString("en-US");
        var query = "?selectedDayString=" + selectedDayString;
        var post = $http({
            method: "POST",
            url: "Default.aspx/AssignEngineer" + query,
            dataType: 'json',
            data: {}
,            //data: { name: $scope.Name },
            headers: { "Content-Type": "application/json" }
        });
        post.then(function (response) {
            if (response.data.d > 0) {

                $scope.GetUpdatedAssignedSchedules();
            }

        });
    }
    //End

    //Remove all the schedules
    $scope.DeleteAllSchedules = function () {
        if (confirm("Are you sure you would like to delete all schedules?")) {
            var post = $http({
                method: "POST",
                url: "Default.aspx/DeleteAllSchedules",
                dataType: 'json',
                data: {}
,            //data: { name: $scope.Name },
                headers: { "Content-Type": "application/json" }
            });
            post.then(function (response) {
                if (response.data.d > 0) {
                    $scope.selectedDate = new Date().toLocaleDateString("en-US");
                    $scope.GetUpdatedAssignedSchedules();
                }
            });
        }
        return false;

    }
    //End

   
});