<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RichmondGroup._Default" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadConent" runat="server">


    <script src="Scripts/custom/home.js"></script>
    <style>
        body {
            margin: 40px 10px;
            padding: 0;
            font-family: "Lucida Grande",Helvetica,Arial,Verdana,sans-serif;
            font-size: 14px;
        }

        #calendar {
            max-width: 900px;
            margin: 0 auto;
        }

        .fc-sat, .fc-sun {
            background-color: lightgray !important;
        }
    </style>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container" ng-app="app" ng-controller="HomeController">
        <div class="row" style="padding-top: 15px; padding-bottom: 15px; border: solid; border-color: darkblue;">
            <div class="col-md-12" style="text-align: center">
                <label>Date:</label>
                <input type="text" id="datepicker" ng-model="selectedDate">
                <button class="btn btn-med btn-success" ng-click="GetLuckyEngineer()" type="button" id="btnChooseEngineer">Choose Lucky Engineer</button>
                <button class="btn btn-med btn-danger" ng-click="DeleteAllSchedules()" type="button" id="btnDeleteAll">Delete All</button>
            </div>
            <div class="col-md-12" id="dvMaxEngineer" style="text-align: center">
                <h5 style="color: orangered">Maximum number of engineers have already assigned for this date.</h5>
            </div>
        </div>
        <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
        </div>
        <div class="row" style="padding-top: 20px;">
            <div id="calendar"></div>

        </div>


    </div>


</asp:Content>
