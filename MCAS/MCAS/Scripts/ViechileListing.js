var app = angular.module("VS", []);
app.service('VSService', ['$http', '$q', function ($http, $q, events) {
    var appService = {
        fetchData: function (url, searchCriteria) {
            var postData = {
                criteria: searchCriteria
                };
            return $http.post(url, postData)
        }
    };
    return appService;
} ]);

app.controller("VSController", ['VSService', '$scope', '$filter', function (VSService, $scope, $filter) {
    $scope.loading = true;
    var orderBy = $filter('orderBy');
    $scope.model = PageModel;
    $scope.model.itemsPerPage = 10;
    $scope.model.currentPage = 0;
    $scope.model.orderByField = 'VehicleMasterId';
    $scope.model.orderByColumn = 'VehicleMasterId';
    $scope.model.reverseSort = true;
    $scope.model.sortOrder = "sorting";
    var resultPromise = VSService.fetchData("GetVsList", $scope.model);
    resultPromise.success(function (data) {
        $scope.Claims = data;
        $scope.currentPage = 0;
        $scope.Claims.Count = data[0].datalength;
        $scope.Claims.startlength = data[0].startlength;
        $scope.Claims.endlength = data[0].endlength;
        $scope.range($scope.Claims.Count);
        $scope.loading = false;
    });
    $scope.GetPagList = function (n) {
        $scope.loading = true;
        if (!$scope.model && PageModel)
            $scope.model = PageModel;
        $scope.model.pageno = n;
        $scope.model.currentPage = n;
        var resultPromise = VSService.fetchData("GetVsList", $scope.model);
        resultPromise.success(function (data) {
            $scope.Claims = data;
            $scope.Claims.Count = data[0].datalength;
            $scope.Claims.startlength = data[0].startlength;
            $scope.Claims.endlength = data[0].endlength;
            $scope.currentPage = n;
            $scope.range($scope.Claims.Count);
            $scope.loading = false;
        });
    };
    $scope.sortOrder = function (sortOrd, orderByFieldName) {
        if (orderByFieldName == $scope.orderByField) {
            return sortOrd == false ? "sorting_asc" : "sorting_desc"
        }
        else if (orderByFieldName == $scope.model.orderByColumn && typeof ($scope.orderByField) == "function") {
            return sortOrd == false ? "sorting_asc" : "sorting_desc"
        }
        else {
            return "sorting";
        }
    }
    $scope.order = function (orderByField, reverseSort) {
        $scope.loading = true;
        $scope.model.pageno = $scope.currentPage;
        $scope.model.orderByColumn = orderByField;
        $scope.model.reverseSort = reverseSort;
        var resultPromise = VSService.fetchData("GetVsList", $scope.model);
        resultPromise.success(function (data) {
            $scope.Claims = data;
            $scope.Claims.Count = data[0].datalength;
            $scope.Claims.startlength = data[0].startlength;
            $scope.Claims.endlength = data[0].endlength;
            $scope.range($scope.Claims.Count);
            $scope.loading = false;
        });
    };
    $scope.change = function () {
        $scope.loading = true;
        $scope.model.currentPage = 0;
        var resultPromise = VSService.fetchData("GetVsList", $scope.model);
        resultPromise.success(function (data) {
            $scope.Claims = data;
            $scope.Claims.Count = data[0].datalength;
            $scope.Claims.startlength = data[0].startlength;
            $scope.Claims.endlength = data[0].endlength;
            $scope.range($scope.Claims.Count);
            $scope.loading = false;
        });
    };
    $scope.Clear = function () {
        $scope.loading = true;
        $scope.model.BusNo = "";
        $scope.model.ChasisNo = "";
        $scope.model.Model = "";
        $scope.model.Make = "";
        $scope.model.VehicleClassCode = "";
        $scope.model.Type = "";
        $scope.model.itemsPerPage = 10;
        $scope.currentPage = 0;
        $scope.model.currentPage = 0;
        var resultPromise = VSService.fetchData("GetVsList", $scope.model);
        resultPromise.success(function (data) {
            $scope.Claims = data;
            $scope.Claims.Count = data[0].datalength;
            $scope.Claims.startlength = data[0].startlength;
            $scope.Claims.endlength = data[0].endlength;
            $scope.range($scope.Claims.Count);
            $scope.loading = false;
        });
    };
    $scope.range = function (data) {
        var rangeSize = 5;
        var ret = [];
        var start;
        var pcount = Math.ceil(data / $scope.model.itemsPerPage) - 1;

        start = $scope.model.currentPage;
        if (start > pcount - rangeSize) {
            start = (pcount - rangeSize >= 0) ? (pcount - rangeSize + 1) : 0;
        }


        for (var i = start; i < start + rangeSize; i++) {
            if (i <= pcount) {
                ret.push(i);
            }
        }
        return ret;
    };
    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            var pagno = $scope.currentPage;
            $scope.GetPagList(pagno - 1);
        }
    };
    $scope.prevPageDisabled = function () {
        return $scope.currentPage === 0 ? "disabled" : "";
    };

    $scope.nextPage = function () {
        if (((($scope.currentPage * 10) + 10) === $scope.Claims.Count) || (Math.min((($scope.currentPage * 10) + 10), $scope.Claims.Count) === $scope.Claims.Count)) {
            return false;
        }
        if ($scope.currentPage < $scope.Claims.Count) {
            var pagno = $scope.currentPage;
            $scope.GetPagList(pagno + 1);
        }
    };
    $scope.nextPageDisabled = function () {
        return (($scope.currentPage * 10) + 10) === $scope.Claims.Count ? "disabled" : Math.min((($scope.currentPage * 10) + 10), $scope.Claims.Count) === $scope.Claims.Count ? "disabled" : "";
    };

} ]);


String.prototype.IsnullOrEmpty = function () {
    return (this != null && this != "");
}

