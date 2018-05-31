var app = angular.module('app', ['ngResource']);
app.controller('HomeController', function ($scope, $http) {
    "use strict";
    $scope.selectedDate = new Date().toLocaleDateString("en-US");
    $("#dvMaxEngineer").hide();
    $("#datepicker").datepicker({
        beforeShowDay: $.datepicker.noWeekends,
            onSelect: function (date) {
                //var selectedDayString = date.toLocaleDateString("en-US");
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

    $scope.GetAssignedSchedules = function () {
       
        var date = new Date();
        var defaultDate = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
        var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
        var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
        var firstDayString = firstDay.toLocaleDateString("en-US");
        var lastDayString = lastDay.toLocaleDateString("en-US");
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
            $scope.list = response.data.d;
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


    $scope.GetUpdatedAssignedSchedules = function () {

        var date = new Date();
        var defaultDate = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
        var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
        var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
        var firstDayString = firstDay.toLocaleDateString("en-US");
        var lastDayString = lastDay.toLocaleDateString("en-US");
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
            $scope.list = response.data.d;
            $('#calendar').fullCalendar('removeEvents');
            $('#calendar').fullCalendar('addEventSource', response.data.d);
            $('#calendar').fullCalendar('rerenderEvents');
        });

    };
    $scope.GetAssignedSchedules();

    $scope.GetLuckyEngineer = function () {
        var date = new Date($scope.selectedDate);
        var selectedDayString = date.toLocaleDateString("en-US");
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
                
                $scope.selectedDate = date;
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
        var selectedDayString = date.toLocaleDateString("en-US");
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
                $scope.selectedDate = new Date().toLocaleDateString("en-US");
                $scope.GetUpdatedAssignedSchedules();
            }

        });
    }
    $scope.DeleteAllSchedules = function () {
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
       
});