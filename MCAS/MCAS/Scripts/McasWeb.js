/***************************************************
Author         : Pravesh K Chandel
Created Date   : 30 July 2014
purpose        : Javascipt file to implement AngularJS in MCASweb project
Reviewed by    :
Modified by    :
Modified Date  :
 
***************************************************/
// Module for
//var MCASWeb = angular.module("ClaimRegistration", ["ngResource"]);
var MCASWeb = angular.module("ClaimRegistration", ['naturalSort']);
var PageModel;
//service
/*MCASWeb.factory("ClaimService", function ($http) {
var appService = {
fetchData: function (url, searchCriteria) {
var postData = { criteria: searchCriteria };
return $http.post(url, postData)
},
// load dropDown
loadDropDown: function (url, selectedValue) {
//var postData = { Code: selectedValue };
return $http.get(url);
},

// Load Country
loadCountries: function (selectedValue, url) {
var deferred = $q.defer();
var postData = { countryCode: selectedValue };

$http.get(url, { params: postData }).success(deferred.resolve).error(deferred.reject);
return deferred.promise;
},
// save Data
SaveData: function (url, pageModel) {
var deferred = $q.defer();
var postData = { eventMaster: rmEvent };

$http.post(url, postData).success(deferred.resolve).error(deferred.reject);
return deferred.promise;
}
};
return appService;
});*/

MCASWeb.service('ClaimService', ['$http', '$q', function ($http, $q, events) {
    var appService = {
        fetchData: function (url, searchCriteria, searchval, column,dir) {
            var postData = {
            criteria: searchCriteria,
            searchvalue: searchval,
            orderBycolumn : column,
            direction: dir
            };
            return $http.post(url, postData)
        },
        fetchDataforchange: function (url, searchCriteria, searchval, column, dir) {
            var deferred = $q.defer();
            var postData = {
                criteria: searchCriteria,
                searchvalue: searchval,
                orderBycolumn: column,
                direction: dir
            };
            $http.post(url, postData).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        // load dropDown
        loadDropDown: function (url, selectedValue) {
            //var postData = { Code: selectedValue };
            return $http.get(url);
        },

        // Load Country
        loadCountries: function (selectedValue, url) {
            var deferred = $q.defer();
            var postData = { countryCode: selectedValue };

            $http.get(url, { params: postData }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        // save Data
        SaveData: function (url, pageModel) {
            var deferred = $q.defer();
            var postData = { eventMaster: rmEvent };

            $http.post(url, postData).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
    };
    return appService;
} ]);

MCASWeb.filter('offset', function () {
    return function (input, start) {
        start = parseInt(start, 10);
        return input.slice(start);
    };
});
MCASWeb.filter("pagingFilter", function () {
    return function (input, currentPage, pageSize) {
        return input ? input.slice(currentPage * pageSize, currentPage * (pageSize + 1)) : [];
    }
});

MCASWeb.filter("MCASWebFilter", function () {
    return function (dataArray, searchTerm, searchColumns) {
        if (!dataArray) {
            return;
        }
        else if (!searchTerm) {
            return dataArray;
        }
        // Otherwise, continue.
        else {
            // Convert filter text to lower case.
            var term = searchTerm.toLowerCase();
            // Return the array and filter it by looking for any occurrences of the search term in each items for given columns;
            var filteredData = dataArray.filter(function (item) {
                var collArray = searchColumns.split(',');
                var found = false;
                for (var i = 0; i < collArray.length; i++) {
                    if (item[collArray[i]] && item[collArray[i]] != 'undefined') {
                        var termInId = item[collArray[i]].toLowerCase().indexOf(term) > -1;
                        found = termInId;
                        if (found) break;
                    }
                }
                return found;
            });
            return filteredData;
        }
    }
});

MCASWeb.directive("paging", function () {
    return {

        //template: '<div><button ng-disabled="!hasPrevious()" ng-click="onPrev()"> Previous </button> {{start()}} - {{end()}} out of {{size()}} <button ng-disabled="!hasNext()" ng-click="onNext()"> Next </button><div ng-transclude=""></div> </div>',
        template: '<div class="col-lg-12 text-center"> <ul class="pagination no-margin"> <li ng-class="!hasPrevious()"> <a href ng-click="onPrev()">« Prev</a></li><li ng-repeat="n in range()"ng-class="{active: n == currentPage}" ng-click="setPage(n)"><a href="#">{{n+1}}</a></li><li ng-class="!hasNext()"><a href ng-click="onNext()">Next »</a></li></ul></div>',
        restrict: 'AEC', transclude: true,
        scope: { 'currentPage': '=', 'pageSize': '=', 'data': '&' },
        link: function ($scope, element, attrs) {
            $scope.size = function () {
                return angular.isDefined($scope.data()) ? $scope.data().length : 0;
            };
            $scope.end = function () { return $scope.start() + $scope.pageSize; };
            $scope.start = function () {
                return $scope.currentPage * $scope.pageSize;
            };
            $scope.page = function () {
                return !!$scope.size() ? ($scope.currentPage + 1) : 0;
            };
            $scope.hasNext = function () {
                return $scope.page() < ($scope.size() / $scope.pageSize);
            };
            $scope.onNext = function () {
                $scope.currentPage = parseInt($scope.currentPage) + 1;
            };
            $scope.hasPrevious = function () {
                return !!$scope.currentPage;
            };
            $scope.onPrev = function () {
                $scope.currentPage = $scope.currentPage - 1;
            };
            $scope.setPage = function (n) {
                $scope.currentPage = n;
            };
            $scope.pageCount = function () {
                return Math.ceil($scope.size() / $scope.pageSize) - 1;
            };
            $scope.range = function () {
                var rangeSize = 5;
                var ret = [];
                var start;
                start = $scope.currentPage;
                if (start > $scope.pageCount() - rangeSize) {
                    if ($scope.pageCount() - rangeSize > 0) {
                        start = $scope.pageCount() - rangeSize + 1;
                    }
                    else {
                        start = 0;
                    }
                }
                for (var i = start; i < start + rangeSize; i++) {
                    if (i <= $scope.pageCount()) {
                        ret.push(i);
                    }
                }
                return ret;
            };

            try {
                if (typeof ($scope.data) == "undefined") {
                    $scope.data = [];
                }
                if (typeof ($scope.currentPage) == "undefined") {
                    $scope.currentPage = 0;
                }
                if (typeof ($scope.pageSize) == "undefined") {
                    $scope.pageSize = 10;
                }
            } catch (e) { console.log(e); }
        }
    }
});

MCASWeb.directive('datepicker', function () {
    return {
        // Enforce the angularJS default of restricting the directive to
        // attributes only
        restrict: 'A',
        // Always use along with an ng-model
        require: '?ngModel',
        // This method needs to be defined and passed in from the
        // passed in to the directive from the view controller
        scope: {
            select: '&'        // Bind the select function we refer to the right scope
        },
        link: function (scope, element, attrs, ngModel) {
            if (!ngModel) return;
            var optionsObj = {};
            optionsObj.dateFormat = 'dd/mm/yy';
            var updateModel = function (dateTxt) {
                scope.$apply(function () {
                    // Call the internal AngularJS helper to
                    // update the two way binding
                    ngModel.$setViewValue(dateTxt);
                });
            };

            optionsObj.onSelect = function (dateTxt, picker) {
                updateModel(dateTxt);
                if (scope.select) {
                    scope.$apply(function () {
                        scope.select({ date: dateTxt });
                    });
                }
            };
            ngModel.$render = function () {
                // Use the AngularJS internal 'binding-specific' variable
                element.datepicker('setDate', ngModel.$viewValue || '');
            };
            element.datepicker(optionsObj);
        }
    };
});

MCASWeb.directive('uiDate', function () {
    return {
        require: '?ngModel',
        link: function ($scope, element, attrs, controller) {
            var originalRender, updateModel, usersOnSelectHandler;
            if ($scope.uiDate == null) $scope.uiDate = {};
            if (controller != null) {
                updateModel = function (value, picker) {
                    return $scope.$apply(function () {
                        return controller.$setViewValue(element.datepicker("getDate"));
                    });
                };
                if ($scope.uiDate.onSelect != null) {
                    usersOnSelectHandler = $scope.uiDate.onSelect;
                    $scope.uiDate.onSelect = function (value, picker) {
                        updateModel(value);
                        return usersOnSelectHandler(value, picker);
                    };
                } else {
                    $scope.uiDate.onSelect = updateModel;
                }
                originalRender = controller.$render;
                controller.$render = function () {
                    originalRender();
                    return element.datepicker("setDate", controller.$viewValue);
                };
            }
            return element.datepicker($scope.uiDate);
        }
    };
});
MCASWeb.directive('datepicker1', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            element.datepicker();
            element.bind('blur keyup change', function () {
                var model = attrs.ngModel;
                if (model.indexOf(".") > -1) scope[model.replace(/\.[^.]*/, "")][model.replace(/[^.]*\./, "")] = element.val();
                else scope[model] = element.val();
            });
        }
    };
});
MCASWeb.directive('onChange', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        scope: {
            OnChange: '='
        },
        link: function (scope, elm, attr) {
            if (attr.type === 'radio' || attr.type === 'checkbox') {
                return;
            }
            // store value when get focus
            elm.bind('focus', function () {
                scope.value = elm.val();

            });

            // execute the event when loose focus and value was change
            elm.bind('blur', function () {
                var currentValue = elm.val();
                if (scope.value !== currentValue) {
                    if (scope.OnChange) {
                        scope.OnChange();
                    }
                }
            });
        }
    };
});
// without using angular service
//ClaimRegistration.controller("ClaimRegitrationController", function ($scope, $http) { 
// using angular service
MCASWeb.controller("ClaimRegitrationController", ['ClaimService', '$scope', '$filter', function (ClaimService, $scope, $filter) {
    $scope.loading = true;
    var orderBy = $filter('orderBy');
    $scope.model = PageModel;
    $scope.itemsPerPage = 10;
    $scope.currentPage = 0;
    $scope.orderByField = 'AccidentDate';
    $scope.orderByColumn = 'AccidentDate';
    $scope.reverseSort = true;
    $scope.sortOrder = "sorting";
    $scope.divDisplay = false;
//    var resultPromise = ClaimService.fetchData(window.rootUrl + "ClaimProcessing/GetClaims", $scope.model, "", $scope.orderByColumn, $scope.reverseSort);
//    resultPromise.success(function (data) {
//        $scope.Claims = data;
//        angular.forEach($scope.Claims, function (value, key) {
//            if (value.AccidentId == null) {
//                if (value.ClaimNo == null) {
//                    $scope.Claims.splice(key, 1);
//                }
//            }
//        });
//        $scope.currentPage = 0;
//        $scope.Claims.Count = data[0].datalength;
//        $scope.Claims.startlength = data[0].startlength;
//        $scope.Claims.endlength = data[0].endlength;
//        $scope.range($scope.Claims.Count);
//        $scope.loading = false;
//    });

    $scope.GetPagList = function (n) {
        $scope.loading = true;
        $scope.currentPage = n;
        if (!$scope.model && PageModel)
            $scope.model = PageModel;
        $scope.model.pageno = n;

        var resultPromise = ClaimService.fetchData(window.rootUrl + "ClaimProcessing/GetClaims", $scope.model, $('#querysearch').val(), $scope.orderByColumn, $scope.reverseSort);
        resultPromise.success(function (data) {
            $scope.Claims = data;
            $scope.Claims.Count = data[0].datalength;
            $scope.Claims.startlength = data[0].startlength;
            $scope.Claims.endlength = data[0].endlength;
            $scope.currentPage = n;
            $scope.range($scope.Claims.Count);
            $scope.loading = false;
            $scope.divDisplay = true;
        });
    };


    $scope.GetPolicyList = function () {
        //            if ($("#inputDateofLoss").val() === "") {
        //                alert("Please Provide Accident Date.")
        //                return false;
        //            }
        if ($("#inputDateofLoss").val() != "") {
            if (!/^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/.test($("#inputDateofLoss").val())) {
                alert("Accident Date must be in 'dd/mm/yyyy' format.", "inputDateofLoss");
                event.preventDefault();
                return false;
            }
        }
        if (!$scope.model && PageModel)
            $scope.model = PageModel;
        if ($scope.model)
            $scope.model.LossDate = $('#inputDateofLoss').val();
        $scope.currentPage = 0;
        $scope.model.pageno = 0;
        $('#querysearch').val("");
        $scope.loading = true;
        var resultPromise = ClaimService.fetchData(window.rootUrl + "ClaimProcessing/GetClaims", $scope.model, $('#querysearch').val(), $scope.orderByColumn, $scope.reverseSort);
        resultPromise.success(function (data) {
            $scope.Claims = data;
            $scope.Claims.Count = data[0].datalength;
            $scope.Claims.startlength = data[0].startlength;
            $scope.Claims.endlength = data[0].endlength;
            $scope.currentPage = 0;
            $scope.range($scope.Claims.Count);
            $scope.loading = false;
            $scope.divDisplay = true;
        });
        return false;
    };
    $scope.sortOrder = function (sortOrd, orderByFieldName) {
        if (orderByFieldName == $scope.orderByField) {
            return sortOrd == false ? "sorting_asc" : "sorting_desc"
        }
        else if (orderByFieldName == $scope.orderByColumn && typeof ($scope.orderByField) == "function") {
            return sortOrd == false ? "sorting_asc" : "sorting_desc"
        }
        else {
            return "sorting";
        }
    }
    $scope.order = function (orderByField, reverseSort) {
        $scope.loading = true;
        if ($scope.Claims.Count !== undefined) {
            bool = false;
            if (!$scope.model && PageModel)
                $scope.model = PageModel;
            $scope.model.pageno = $scope.currentPage;
            $scope.orderByColumn = orderByField;
            $scope.reverseSort = reverseSort;
            var resultPromise = ClaimService.fetchData(window.rootUrl + "ClaimProcessing/GetClaims", $scope.model, $('#querysearch').val(), orderByField, reverseSort);
            resultPromise.success(function (data) {
                $scope.Claims = data;
                $scope.Claims.Count = data[0].datalength;
                $scope.Claims.startlength = data[0].startlength;
                $scope.Claims.endlength = data[0].endlength;
                $scope.range($scope.Claims.Count);
                $scope.loading = false;
                $scope.divDisplay = true;
            });
        }
        $scope.loading = false;
    };

    $scope.change = function () {
        $scope.loading = true;
        $scope.currentPage = 0;
        $scope.model.pageno = 0;
        if ($scope.Claims !== undefined) {
            if (!$scope.model && PageModel)
                $scope.model = PageModel;
            if ($scope.model)
                $scope.model.LossDate = $('#inputDateofLoss').val();
            var resultPromise = ClaimService.fetchDataforchange(window.rootUrl + "ClaimProcessing/GetClaims", $scope.model, $('#querysearch').val(), $scope.orderByColumn, $scope.reverseSort);
            resultPromise.then(function (data) {
                $scope.Claims = data;
                $scope.Claims.Count = data[0].datalength;
                $scope.Claims.startlength = data[0].startlength;
                $scope.Claims.endlength = data[0].endlength;
                $scope.range($scope.Claims.Count);
                $scope.loading = false;
                $scope.divDisplay = true;
            });
        }

    };
    // table data and paging
    $scope.range = function (data) {
        var rangeSize = 5;
        var ret = [];
        var start;
        var pcount = Math.ceil(data / $scope.itemsPerPage) - 1;

        start = $scope.currentPage;
        if (start > pcount - rangeSize) {
            if (pcount - rangeSize >= 0) {
                start = pcount - rangeSize + 1;
            }
            else {
                start = 0;
            }
        }


        for (var i = start; i < start + rangeSize; i++) {
            if (i <= pcount) {
                ret.push(i);
            }
        }
        return ret;
    };
    $scope.setPageSize = function (pageSize) { $scope.dataPageSize = pageSize; }
    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            var pagno = $scope.currentPage;
            $scope.GetPagList(pagno - 1);
        }
    };
    $scope.prevPageDisabled = function () {
        return $scope.currentPage === 0 ? "disabled" : "";
    };

    $scope.pageCount = function () {
        var dataLength = $scope.Claims.length;
        if ($scope.Claimsfiltered && $scope.Claimsfiltered.length)
            dataLength = $scope.Claimsfiltered.length;
        return Math.ceil(dataLength / $scope.itemsPerPage) - 1;
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

    $scope.setPage = function (n) {
        $scope.currentPage = n;
    };

    $scope.filterFunction = function (element) {
        return element.PolicyNo.match(/^/) ? true : false;
    };
    // end table pagination

    //fetch search results
    $scope.Products = [];
    $scope.Countries = [];
    $scope.SubClasses = [];
    $scope.Organizations = [];
    // loading Product
    //var resultProducts = ClaimService.loadDropDown(getProductUrl, "");
    var resultProducts = ClaimService.loadDropDown(window.rootUrl + "ClaimProcessing/FillProducts", "");
    resultProducts.success(function (data) {
        $scope.Products = data;
        $scope.loading = false;
    });
    $scope.mainClassCode = $scope.Products[0];
    // loading countries
    //var resultCountries = ClaimService.loadDropDown(getCountriesUrl, "");
    var resultCountries = ClaimService.loadDropDown(window.rootUrl + "ClaimProcessing/FillCountries", "");
    resultCountries.success(function (data) {
        if (!$scope.model && PageModel)
            $scope.model = PageModel;
        $scope.Countries = data;
        $scope.model.orgCountry = $scope.Countries[0].CountryShortCode;
        $scope.loading = false;
    });


    // loading organization
    //var resultCountries = ClaimService.loadDropDown(getCountriesUrl, "");
    var resultOrganization = ClaimService.loadDropDown(window.rootUrl + "ClaimProcessing/FillOrganization", "");
    resultOrganization.success(function (data) {
        if (!$scope.model && PageModel)
            $scope.model = PageModel;
        $scope.Organizations = data;
        $scope.model.organization = $scope.Organizations[0].CountryOrgazinationCode;
        $scope.loading = false;
    });




    // loading subclasses
    //var resultSubClasses = ClaimService.loadDropDown(getSubClassURL, "");
    var resultSubClasses = ClaimService.loadDropDown(window.rootUrl + "ClaimProcessing/FillSubClass", "");
    resultSubClasses.success(function (data) {
        $scope.SubClasses = data;
        $scope.loading = false;
    });
    $scope.subClassCode = $scope.SubClasses[0];

} ]);

var DiaryController = function ($scope, $http) {
    var resultPromise = $http.get("/Home/GetDiaries");
    resultPromise.success(function (data) {
        $scope.Diaries = data;
    });

    $scope.newClaims = {
        0: "No new Claim",
        other: "{} new Claims"
    }
}



MCASWeb.controller("JobScheduleEnquiryController", ['ClaimService', '$scope', '$filter', function (ClaimService, $scope, $filter) {
    $scope.loading = true;
    var orderBy = $filter('orderBy');
    $scope.model = PageModel;
    $scope.itemsPerPage = 10;
    $scope.currentPage = 0;
    $scope.orderByField = 'SNo';
    $scope.reverseSort = false;
    $scope.sortOrder = "sorting";
    $scope.divDisplay = false;
//    var resultPromise = ClaimService.fetchData(window.rootUrl + "ClaimProcessing/GetJobenquirysearch", $scope.model);
//    resultPromise.success(function (data) {
//        $scope.Claims = data;
//        $scope.currentPage = 0;
//        $scope.order('SNo', false);
//        $scope.model.Count = data.length;
//        $scope.range($scope.Claims);
//        $scope.loading = false;
//    });

    $scope.GetPolicyList = function () {
        if (!$scope.model && PageModel)
            $scope.model = PageModel;
        if ($scope.model)
            if ("" != $("#inputScheduleStartDate").val() && !validateDate($("#inputScheduleStartDate").val())) return alert("Please Enter Schedule From Date in 'dd/mm/yyyy' format."), !1;
        if ("" != $("#inputScheduleToDate").val() && !validateDate($("#inputScheduleToDate").val())) return alert("Please Enter Schedule To Date in 'dd/mm/yyyy' format."), !1;
        $scope.model.ScheduleStartDate = $('#inputScheduleStartDate').val();
        $scope.model.ScheduleToDate = $('#inputScheduleToDate').val();
        $scope.loading = true;
        var resultPromise = ClaimService.fetchData(window.rootUrl + "ClaimProcessing/GetJobenquirysearch", $scope.model);
        resultPromise.success(function (data) {
            $scope.Claims = data;
            $scope.model.Count = data.length;
            $scope.currentPage = 0;
            $scope.range($scope.Claims);
//            $scope.order(orderByField, reverseSort);
            $scope.loading = false;
            $scope.divDisplay = true;
        });
        return false;
    };
    //---Start-FillJobType
    var resultFillJobType = ClaimService.loadDropDown(window.rootUrl + "ClaimProcessing/FillJobType", "");
    resultFillJobType.success(function (data) {
        if (!$scope.model && PageModel)
            $scope.model = PageModel;
        $scope.DataFillJobType = data;
        $scope.model.JobTypedefault = $scope.DataFillJobType[0].Id;
    });
    //---End-FillJobType
    //---Start-FillJobStatus
    var resultFillJobStatus = ClaimService.loadDropDown(window.rootUrl + "ClaimProcessing/FillJobStatus", "");
    resultFillJobStatus.success(function (data) {
        if (!$scope.model && PageModel)
            $scope.model = PageModel;
        $scope.DataJobStatus = data;
        $scope.model.JobStatusDefault = $scope.DataJobStatus[0].Id;
    });
    //---End-FillJobStatus
    $scope.sortOrder = function (sortOrd, orderByFieldName) {
        if (orderByFieldName == $scope.orderByField) {
            return sortOrd == false ? "sorting_asc" : "sorting_desc"
        }
        else {
            return "sorting";
        }
    }
    $scope.order = function (orderByField, reverseSort) {
        $scope.loading = true;
        $scope.Claims = orderBy($scope.Claims, orderByField, reverseSort);
        $scope.loading = false;
    };

    $scope.change = function () {
        $scope.loading = true;
        $scope.currentPage = 0;
        $scope.loading = false;
    };
    $scope.range = function (data) {
        var rangeSize = 5;
        var ret = [];
        var start;
        var pcount = Math.ceil(data.length / $scope.itemsPerPage) - 1;

        start = $scope.currentPage;
        if (start > pcount - rangeSize) {
            if (pcount - rangeSize >= 0) {
                start = pcount - rangeSize + 1;
            }
            else {
                start = 0;
            }
        }


        for (var i = start; i < start + rangeSize; i++) {
            if (i <= pcount) {
                ret.push(i);
            }
        }
        return ret;
    };
    $scope.setPageSize = function (pageSize) { $scope.dataPageSize = pageSize; }
    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
        }
    };
    $scope.prevPageDisabled = function () {
        return $scope.currentPage === 0 ? "disabled" : "";
    };

    $scope.pageCount = function () {
        var dataLength = $scope.Claims.length;
        if ($scope.Claimsfiltered && $scope.Claimsfiltered.length)
            dataLength = $scope.Claimsfiltered.length;
        return Math.ceil(dataLength / $scope.itemsPerPage) - 1;
    };

    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.pageCount()) {
            $scope.currentPage++;
        }
    };

    $scope.nextPageDisabled = function () {
        return $scope.currentPage === $scope.pageCount() ? "disabled" : "";
    };

    $scope.setPage = function (n) {
        $scope.currentPage = n;
    };
    $scope.filterFunction = function (element) {
        return element.PolicyNo.match(/^/) ? true : false;
    };
} ]);




MCASWeb.controller("ClaimRecoveryProcessingController", ['ClaimService', '$scope', '$filter', function (ClaimService, $scope, $filter) {
    $scope.loading = true;
    var orderBy = $filter('orderBy');
    $scope.model = PageModel;
    $scope.itemsPerPage = 10;
    $scope.currentPage = 0;
    $scope.orderByField = 'PolicyNo';
    $scope.orderByColumn = 'PolicyNo';
    $scope.reverseSort = false;
    $scope.sortOrder = "sorting";
    $scope.divDisplay = false;


    //    var resultPromise = ClaimService.fetchData(window.rootUrl + "ClaimRecoveryProcessing/GetRecoveryClaims", $scope.model);
    //    resultPromise.success(function (data) {
    //        $scope.Claims = data;
    //        $scope.currentPage = 0;
    //        $scope.order('PolicyNo', false);
    //        $scope.model.Count = data.length;
    //        $scope.range($scope.Claims);
    //        $scope.loading = false;
    //    });

    $scope.GetPolicyList = function () {
        //        if ($("#inputDateofLoss").val() === "") {
        //            alert("Please Provide Accident Date.")
        //            return false;
        //        }
        if (!$scope.model && PageModel)
            $scope.model = PageModel;
        if ($scope.model)
            $scope.model.LossDate = $('#inputDateofLoss').val();
        $scope.loading = true;
        var resultPromise = ClaimService.fetchData(window.rootUrl + "ClaimRecoveryProcessing/GetRecoveryClaims", $scope.model);
        resultPromise.success(function (data) {
            $scope.Claims = data;
            $scope.model.Count = data.length;
            $scope.currentPage = 0;
            $scope.range($scope.Claims);
            //            $scope.order(orderByField, reverseSort);
            $scope.loading = false;
            $scope.divDisplay = true;
        });
        return false;
    };

    $scope.sortOrder = function (sortOrd, orderByFieldName) {
        if (orderByFieldName == $scope.orderByField) {
            return sortOrd == false ? "sorting_asc" : "sorting_desc"
        }
        else if (orderByFieldName == $scope.orderByColumn && typeof ($scope.orderByField) == "function") {
            return sortOrd == false ? "sorting_asc" : "sorting_desc"
        }
        else {
            return "sorting";
        }
    }
    $scope.order = function (orderByField, reverseSort) {
        $scope.loading = true;
        $scope.Claims = orderBy($scope.Claims, orderByField, reverseSort);
        $scope.loading = false;
    };

    $scope.change = function () {
        $scope.loading = true;
        $scope.currentPage = 0;
        $scope.loading = false;
    };
    // table data and paging
    $scope.range = function (data) {
        var rangeSize = 5;
        var ret = [];
        var start;
        var pcount = Math.ceil(data.length / $scope.itemsPerPage) - 1;

        start = $scope.currentPage;
        if (start > pcount - rangeSize) {
            if (pcount - rangeSize >= 0) {
                start = pcount - rangeSize + 1;
            }
            else {
                start = 0;
            }
        }


        for (var i = start; i < start + rangeSize; i++) {
            if (i <= pcount) {
                ret.push(i);
            }
        }
        return ret;
    };
    $scope.setPageSize = function (pageSize) { $scope.dataPageSize = pageSize; }
    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
        }
    };
    $scope.prevPageDisabled = function () {
        return $scope.currentPage === 0 ? "disabled" : "";
    };

    $scope.pageCount = function () {
        var dataLength = $scope.Claims.length;
        if ($scope.Claimsfiltered && $scope.Claimsfiltered.length)
            dataLength = $scope.Claimsfiltered.length;
        return Math.ceil(dataLength / $scope.itemsPerPage) - 1;
    };

    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.pageCount()) {
            $scope.currentPage++;
        }
    };

    $scope.nextPageDisabled = function () {
        return $scope.currentPage === $scope.pageCount() ? "disabled" : "";
    };

    $scope.setPage = function (n) {
        $scope.currentPage = n;
    };

    $scope.filterFunction = function (element) {
        return element.PolicyNo.match(/^/) ? true : false;
    };
    // end table pagination

    //fetch search results
    //    $scope.Products = [];
    $scope.Countries = [];
    //    $scope.SubClasses = [];
    // loading Product
    //var resultProducts = ClaimService.loadDropDown(getProductUrl, "");
    //    var resultProducts = ClaimService.loadDropDown(window.rootUrl + "ClaimRecoveryProcessing/FillProducts", "");
    //    resultProducts.success(function (data) {
    //        $scope.Products = data;
    //    });
    //    $scope.mainClassCode = $scope.Products[0];
    // loading countries
    //var resultCountries = ClaimService.loadDropDown(getCountriesUrl, "");
    var resultCountries = ClaimService.loadDropDown(window.rootUrl + "ClaimRecoveryProcessing/FillCountries", "");
    resultCountries.success(function (data) {
        if (!$scope.model && PageModel)
            $scope.model = PageModel;
        $scope.Countries = data;
        $scope.loading = false;
        $scope.model.orgCountry = $scope.Countries[0].CountryShortCode;
    });
    $scope.orgCountry = $scope.Countries[0];
    // loading subclasses
    //var resultSubClasses = ClaimService.loadDropDown(getSubClassURL, "");
    //    var resultSubClasses = ClaimService.loadDropDown(window.rootUrl + "ClaimRecoveryProcessing/FillSubClass", "");
    //    resultSubClasses.success(function (data) {
    //        $scope.SubClasses = data;
    //    });
    //    $scope.subClassCode = $scope.SubClasses[0];

} ]);

MCASWeb.controller("ClaimEnquiryController", ['ClaimService', '$scope', '$filter', function (ClaimService, $scope, $filter) {
    $scope.loading = true;
    var orderBy = $filter('orderBy');
    $scope.model = PageModel;
    $scope.itemsPerPage = 10;
    $scope.currentPage = 0;
    $scope.orderByField = 'PolicyNo';
    $scope.orderByColumn = 'PolicyNo';
    $scope.reverseSort = false;
    $scope.sortOrder = "sorting";
    $scope.divDisplay = false;

//    var resultPromise = ClaimService.fetchData(window.rootUrl + "ClaimProcessing/GetEnquiryClaims", $scope.model);
//    resultPromise.success(function (data) {
//        $scope.Claims = data;
//        $scope.currentPage = 0;
//        $scope.order('PolicyNo', false);
//        $scope.model.Count = data.length;
//        $scope.range($scope.Claims);
//        $scope.loading = false;
//    });

    $scope.GetPolicyList = function () {
//        if ($("#inputDateofLoss").val() === "") {
//            alert("Please Provide Accident Date.")
//            return false;
//        }
        //ValidateForm();
        if (!$scope.model && PageModel)
            $scope.model = PageModel;
        if ($scope.model)
            $scope.model.AccidentDateFrom = $('#inputAccidentdatefrom').val();
        $scope.model.AccidentDateTo = $('#inputAccidentdateto').val();
        $scope.model.ClaimRegisDateFrom = $('#inputClaimRegistrationdatefrom').val();
        $scope.model.ClaimRegisDateTo = $('#inputClaimRegistrationdateto').val();
        $scope.model.LossDate = $("#inputDateofLoss").val();
        $scope.loading = true;
        var resultPromise = ClaimService.fetchData(window.rootUrl + "ClaimProcessing/GetEnquiryClaims", $scope.model);
        resultPromise.success(function (data) {
            $scope.Claims = data;
            $scope.model.Count = data.length;
            $scope.currentPage = 0;
            $scope.range($scope.Claims);
            //            $scope.order(orderByField, reverseSort);
            $scope.loading = false;
            $scope.divDisplay = true;
        });
        return false;
    };

    $scope.sortOrder = function (sortOrd, orderByFieldName) {
        if (orderByFieldName == $scope.orderByField) {
            return sortOrd == false ? "sorting_asc" : "sorting_desc"
        }
        else if (orderByFieldName == $scope.orderByColumn && typeof ($scope.orderByField) == "function") {
            return sortOrd == false ? "sorting_asc" : "sorting_desc"
        }
        else {
            return "sorting";
        }
    }
    $scope.order = function (orderByField, reverseSort) {
        $scope.loading = true;
        $scope.Claims = orderBy($scope.Claims, orderByField, reverseSort);
        $scope.loading = false;
    };

    $scope.change = function () {
        $scope.loading = true;
        $scope.currentPage = 0;
        $scope.loading = false;
    };
    // table data and paging
    $scope.range = function (data) {
        var rangeSize = 5;
        var ret = [];
        var start;
        var pcount = Math.ceil(data.length / $scope.itemsPerPage) - 1;

        start = $scope.currentPage;
        if (start > pcount - rangeSize) {
            if (pcount - rangeSize >= 0) {
                start = pcount - rangeSize + 1;
            }
            else {
                start = 0;
            }
        }


        for (var i = start; i < start + rangeSize; i++) {
            if (i <= pcount) {
                ret.push(i);
            }
        }
        return ret;
    };
    $scope.setPageSize = function (pageSize) { $scope.dataPageSize = pageSize; }
    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
        }
    };
    $scope.prevPageDisabled = function () {
        return $scope.currentPage === 0 ? "disabled" : "";
    };

    $scope.pageCount = function () {
        var dataLength = $scope.Claims.length;
        if ($scope.Claimsfiltered && $scope.Claimsfiltered.length)
            dataLength = $scope.Claimsfiltered.length;
        return Math.ceil(dataLength / $scope.itemsPerPage) - 1;
    };

    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.pageCount()) {
            $scope.currentPage++;
        }
    };

    $scope.nextPageDisabled = function () {
        return $scope.currentPage === $scope.pageCount() ? "disabled" : "";
    };

    $scope.setPage = function (n) {
        $scope.currentPage = n;
    };
    $scope.filterFunction = function (element) {
        return element.PolicyNo.match(/^/) ? true : false;
    };
    // end table pagination

    //fetch search results
    $scope.OrgCountries = [];
    $scope.LegalClaimCase = [];

    var resultCountries = ClaimService.loadDropDown(window.rootUrl + "ClaimProcessing/FillOrganization", "");
    resultCountries.success(function (data) {
        if (!$scope.model && PageModel)
            $scope.model = PageModel;
        $scope.OrgCountries = data;
        $scope.loading = false;
        $scope.model.orgCountry = $scope.OrgCountries[0].OrganizationName;
    });




    var resultLegalCase = ClaimService.loadDropDown(window.rootUrl + "ClaimProcessing/FillClaimLegalCase", "");
    resultLegalCase.success(function (data) {
        if (!$scope.model && PageModel)
            $scope.model = PageModel;
        $scope.LegalClaimCase = data;
        $scope.loading = false;
        $scope.model.ClaimLegelCase = $scope.LegalClaimCase[0].AdjusterAppointed_Id;
    });


} ]);


MCASWeb.controller("ClaimPaymentProcessingController", ['ClaimService', '$scope', '$filter', function (ClaimService, $scope, $filter) {
    $scope.loading = true;
    var orderBy = $filter('orderBy');
    $scope.model = PageModel;
    $scope.itemsPerPage = 10;
    $scope.currentPage = 0;
    $scope.orderByField = 'serialno';
    $scope.orderByColumn = 'serialno';
    $scope.reverseSort = false;
    $scope.sortOrder = "sorting";
    $scope.divDisplay = false;

    //    var resultPromise = ClaimService.fetchData(window.rootUrl + "ClaimProcessing/GetClaimPayment", $scope.model);
    //    resultPromise.success(function (data) {
    //        if (!$scope.model && PageModel)
    //            $scope.model = PageModel;
    //        $scope.Claims = data;
    //        $scope.currentPage = 0;
    //        $scope.order('PolicyNo', false);
    //        $scope.model.Count = data.length;
    //        $scope.range($scope.Claims);
    //        $scope.loading = false;
    //    });
    $scope.cp = 0;
    $scope.GetPolicyList = function (p) {
        //        if ($("#inputDateofLoss").val() === "") {
        //            alert("Please Provide Accident Date.")
        //            return false;
        //        }
        if (!$scope.model && PageModel)
            $scope.model = PageModel;
        if ($scope.model)
            $scope.model.LossDate = $('#inputDateofLoss').val();
        $scope.model.currPage = p;
        $scope.cp = p;
        $scope.loading = true;
        var resultPromise = ClaimService.fetchData(window.rootUrl + "ClaimProcessing/GetClaimPayment", $scope.model);
        resultPromise.success(function (data) {

            $scope.Claims = data;
            //console.log($scope.Claims);
            //console.log($scope.Claims[$scope.Claims.length - 1].TotalnofRec);
            if (data.length != 0) {
                $scope.model.Count = $scope.Claims[$scope.Claims.length - 1].TotalnofRec;
            }
            else { 
             $scope.model.Count = data.length; 
            }
            
            $scope.currentPage = $scope.cp;
            // $scope.range($scope.Claims, $scope.Claims[$scope.Claims.length - 1].TotalnofRec);
            //            $scope.order(orderByField, reverseSort);
            $scope.loading = false;
            $scope.divDisplay = true;
        });
        return false;
    };

    $scope.sortOrder = function (sortOrd, orderByFieldName) {
        if (orderByFieldName == $scope.orderByField) {
            return sortOrd == false ? "sorting_asc" : "sorting_desc"
        }
        else if (orderByFieldName == $scope.orderByColumn && typeof ($scope.orderByField) == "function") {
            return sortOrd == false ? "sorting_asc" : "sorting_desc"
        }
        else {
            return "sorting";
        }
    }

    $scope.order = function (orderByField, reverseSort) {
        $scope.loading = true;
        $scope.Claims = orderBy($scope.Claims, orderByField, reverseSort);
        $scope.loading = false;
    };

    $scope.change = function () {

        $scope.loading = true;
        $scope.currentPage = 0;
        $scope.loading = false;
    };

    // table data and paging
    $scope.range = function (data, totalnoofrec) {
        var rangeSize = 5;
        var ret = [];
        var start;
        //        var pcount = Math.ceil(data.length / $scope.itemsPerPage) - 1;
        var pcount = Math.ceil($scope.model.Count / $scope.itemsPerPage) - 1;
        //ng-class="{active: n == currentPage}"
        //| offset:currentPage*itemsPerPage | limitTo:itemsPerPage|orderBy:orderByField:reverseSort |filter:query
        start = $scope.currentPage;
        if (start > pcount - rangeSize) {
            if (pcount - rangeSize >= 0) {
                start = pcount - rangeSize + 1;
            }
            else {
                start = 0;
            }
        }


        for (var i = start; i < start + rangeSize; i++) {
            if (i <= pcount) {
                ret.push(i);
            }
        }
        return ret;
    };

    //    $scope.range = function (data) {
    //        debugger;
    //        var rangeSize = 5;
    //        var ret = [];
    //        var start;
    //        var pcount = Math.ceil(data.length / $scope.itemsPerPage) - 1;

    //        start = $scope.currentPage;
    //        if (start > pcount - rangeSize) {
    //            if (pcount - rangeSize >= 0) {
    //                start = pcount - rangeSize + 1;
    //            }
    //            else {
    //                start = 0;
    //            }
    //        }


    //        for (var i = start; i < start + rangeSize; i++) {
    //            if (i <= pcount) {
    //                ret.push(i);
    //            }
    //        }
    //        return ret;
    //    };

    $scope.setPageSize = function (pageSize) { $scope.dataPageSize = pageSize; }
    $scope.prevPage = function ($event) {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
        }
        if (($scope.currentPage) <= 0) {
            $scope.currentPage = 0;
            $scope.cp = 1;
        }
        if ($scope.currentPage > 0) {
            $scope.GetPolicyList($scope.cp - 1);
        }
    };
    $scope.prevPageDisabled = function () {
        return $scope.currentPage === 0 ? "disabled" : "";
    };

    $scope.pageCount = function () {
        //        var dataLength = $scope.Claims.length;
        var dataLength = $scope.model.Count;
        //        if ($scope.Claimsfiltered && $scope.Claimsfiltered.length)
        //            dataLength = $scope.Claimsfiltered.length;
        return Math.ceil(dataLength / $scope.itemsPerPage) - 1;
    };

    $scope.nextPage = function ($event) {
        if ($scope.currentPage < $scope.pageCount()) {
            $scope.currentPage++;
        }
        if (($scope.currentPage) >= $scope.pageCount()) {
            $scope.currentPage = $scope.pageCount();
            $scope.cp = $scope.pageCount() - 1;
        }
        if ($scope.currentPage < $scope.pageCount()) {
            $scope.GetPolicyList($scope.cp + 1);
        }
    };

    $scope.nextPageDisabled = function () {

        return $scope.currentPage === $scope.pageCount() ? "disabled" : "";
    };

    $scope.setPage = function (n) {
        $scope.currentPage = n;
    };

    $scope.filterFunction = function (element) {
        //  return element.PolicyNo.match(/^/) ? true : false;
    };
    // end table pagination

    //fetch search results
    $scope.ClaimType = [];

    var resultCountries = ClaimService.loadDropDown(window.rootUrl + "ClaimProcessing/FillClaimType", "");
    resultCountries.success(function (data) {
        $scope.ClaimType = data;
        if (!$scope.model && PageModel)
            $scope.model = PageModel;
        $scope.loading = false;
        $scope.model.ClaimantType = $scope.ClaimType[0].Id;
    });

} ]);

MCASWeb.controller("ClaimDocPrintedController", ['ClaimService', '$scope', '$filter', function (ClaimService, $scope, $filter) {
    var orderBy = $filter('orderBy');
    $scope.model = PageModel;
    $scope.itemsPerPage = 10;
    $scope.currentPage = 0;
    $scope.orderByField = 'DocumentName';
    $scope.orderByColumn = 'DocumentName';
    $scope.reverseSort = false;
    $scope.sortOrder = "sorting";
    $scope.divDisplay = false;
    $scope.dt = new Date();
//    var resultPromise = ClaimService.fetchData(window.rootUrl + "ClaimMasters/GetClaimPrintedDocument", $scope.model);
//    resultPromise.success(function (data) {
//        $scope.Claims = data;
//        $scope.currentPage = 0;
//        $scope.order('DocumentName', false);
//        $scope.model.Count = data.length;
//        $scope.range($scope.Claims);
//    });

    $scope.GetPolicyList = function () {
        if (!$scope.model && PageModel)
            $scope.model = PageModel;

        var resultPromise = ClaimService.fetchData(window.rootUrl + "ClaimMasters/GetClaimPrintedDocument", $scope.model);
        resultPromise.success(function (data) {
            $scope.Claims = data;
            $scope.model.Count = data.length;
            $scope.currentPage = 0;
            $scope.divDisplay = true;
            $scope.range($scope.Claims);
            $scope.order(orderByField, reverseSort);
        });
        return false;
    };
    $scope.sortOrder = function (sortOrd, orderByFieldName) {
        if (orderByFieldName == $scope.orderByField) {
            return sortOrd == false ? "sorting_asc" : "sorting_desc"
        }
        else if (orderByFieldName == $scope.orderByColumn && typeof ($scope.orderByField) == "function") {
            return sortOrd == false ? "sorting_asc" : "sorting_desc"
        }
        else {
            return "sorting";
        }
    }
    $scope.order = function (orderByField, reverseSort) {
        $scope.Claims = orderBy($scope.Claims, orderByField, reverseSort);
    };
    $scope.change = function () {
        $scope.currentPage = 0;
    };
    // table data and paging
    $scope.range = function (data) {
        var rangeSize = 5;
        var ret = [];
        var start;
        var pcount = Math.ceil(data.length / $scope.itemsPerPage) - 1;

        start = $scope.currentPage;
        if (start > pcount - rangeSize) {
            if (pcount - rangeSize >= 0) {
                start = pcount - rangeSize + 1;
            }
            else {
                start = 0;
            }
        }


        for (var i = start; i < start + rangeSize; i++) {
            if (i <= pcount) {
                ret.push(i);
            }
        }
        return ret;
    };
    $scope.setPageSize = function (pageSize) { $scope.dataPageSize = pageSize; }
    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
        }
    };
    $scope.prevPageDisabled = function () {
        return $scope.currentPage === 0 ? "disabled" : "";
    };

    $scope.pageCount = function () {
        var dataLength = $scope.Claims.length;
        if ($scope.Claimsfiltered && $scope.Claimsfiltered.length)
            dataLength = $scope.Claimsfiltered.length;
        return Math.ceil(dataLength / $scope.itemsPerPage) - 1;
    };

    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.pageCount()) {
            $scope.currentPage++;
        }
    };

    $scope.nextPageDisabled = function () {
        return $scope.currentPage === $scope.pageCount() ? "disabled" : "";
    };

    $scope.setPage = function (n) {
        $scope.currentPage = n;
    };
    $scope.filterFunction = function (element) {
        return element.PolicyNo.match(/^/) ? true : false;
    };
    // end table pagination


} ]);

MCASWeb.controller("TaskDashBoardController", ['ClaimService', '$scope', '$filter', function (ClaimService, $scope, $filter) {
    var orderBy = $filter('orderBy');
    $scope.model = PageModel;
    $scope.itemsPerPage = 10;
    $scope.currentPage = 0;
    $scope.orderByField = 'PolicyNo';
    $scope.reverseSort = false;
    $scope.sortOrder = "sorting";

    var resultPromise = ClaimService.fetchData(window.rootUrl + "Home/FetchTaskList", $scope.model);
    resultPromise.success(function (data) {
        $scope.Claims = data;
        $scope.currentPage = 0;
        $scope.order('PolicyNo', false);
        $scope.model.Count = data.length;
        $scope.range($scope.Claims);
    });

    $scope.sortOrder = function (sortOrd, orderByFieldName) {
        if (orderByFieldName == $scope.orderByField) {
            return sortOrd == false ? "sorting_asc" : "sorting_desc"
        }
        else {
            return "sorting";
        }
    }
    $scope.order = function (orderByField, reverseSort) {
        $scope.Claims = orderBy($scope.Claims, orderByField, reverseSort);
    };


    $scope.range = function (data) {
        var rangeSize = 5;
        var ret = [];
        var start;
        var pcount = Math.ceil(data.length / $scope.itemsPerPage) - 1;

        start = $scope.currentPage;
        if (start > pcount - rangeSize) {
            if (pcount - rangeSize >= 0) {
                start = pcount - rangeSize + 1;
            }
            else {
                start = 0;
            }
        }


        for (var i = start; i < start + rangeSize; i++) {
            if (i <= pcount) {
                ret.push(i);
            }
        }
        return ret;
    };
    $scope.setPageSize = function (pageSize) { $scope.dataPageSize = pageSize; }
    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
        }
    };
    $scope.prevPageDisabled = function () {
        return $scope.currentPage === 0 ? "disabled" : "";
    };

    $scope.pageCount = function () {
        var dataLength = $scope.Claims.length;
        if ($scope.Claimsfiltered && $scope.Claimsfiltered.length)
            dataLength = $scope.Claimsfiltered.length;
        return Math.ceil(dataLength / $scope.itemsPerPage) - 1;
    };

    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.pageCount()) {
            $scope.currentPage++;
        }
    };

    $scope.nextPageDisabled = function () {
        return $scope.currentPage === $scope.pageCount() ? "disabled" : "";
    };

    $scope.setPage = function (n) {
        $scope.currentPage = n;
    };

    $scope.filterFunction = function (element) {
        return element.PolicyNo.match(/^/) ? true : false;
    };
} ]);


MCASWeb.controller("MandateDashBoardController", ['ClaimService', '$scope', '$filter', function (ClaimService, $scope, $filter) {
    var orderBy = $filter('orderBy');
    $scope.model = PageModel;
    $scope.itemsPerPage = 10;
    $scope.currentPage = 0;
    $scope.orderByField = 'PolicyNo';
    $scope.reverseSort = false;
    $scope.sortOrder = "sorting";

    var resultPromise = ClaimService.fetchData(window.rootUrl + "Home/FetchMandateList", $scope.model);
    resultPromise.success(function (data) {
        $scope.Claims = data;
        $scope.currentPage = 0;
        $scope.order('PolicyNo', false);
        $scope.model.Count = data.length;
        $scope.range($scope.Claims);
    });

    $scope.sortOrder = function (sortOrd, orderByFieldName) {
        if (orderByFieldName == $scope.orderByField) {
            return sortOrd == false ? "sorting_asc" : "sorting_desc"
        }
        else {
            return "sorting";
        }
    }
    $scope.order = function (orderByField, reverseSort) {
        $scope.Claims = orderBy($scope.Claims, orderByField, reverseSort);
    };

    $scope.range = function (data) {
        var rangeSize = 5;
        var ret = [];
        var start;
        var pcount = Math.ceil(data.length / $scope.itemsPerPage) - 1;

        start = $scope.currentPage;
        if (start > pcount - rangeSize) {
            if (pcount - rangeSize >= 0) {
                start = pcount - rangeSize + 1;
            }
            else {
                start = 0;
            }
        }


        for (var i = start; i < start + rangeSize; i++) {
            if (i <= pcount) {
                ret.push(i);
            }
        }
        return ret;
    };
    $scope.setPageSize = function (pageSize) { $scope.dataPageSize = pageSize; }
    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
        }
    };
    $scope.prevPageDisabled = function () {
        return $scope.currentPage === 0 ? "disabled" : "";
    };

    $scope.pageCount = function () {
        var dataLength = $scope.Claims.length;
        if ($scope.Claimsfiltered && $scope.Claimsfiltered.length)
            dataLength = $scope.Claimsfiltered.length;
        return Math.ceil(dataLength / $scope.itemsPerPage) - 1;
    };

    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.pageCount()) {
            $scope.currentPage++;
        }
    };

    $scope.nextPageDisabled = function () {
        return $scope.currentPage === $scope.pageCount() ? "disabled" : "";
    };

    $scope.setPage = function (n) {
        $scope.currentPage = n;
    };

    $scope.filterFunction = function (element) {
        return element.PolicyNo.match(/^/) ? true : false;
    };
} ]);
function validateDate(testdate) {
    var date_regex = /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/;
    return date_regex.test(testdate);
};
//// module for Menus
//var MCASWebMenu = angular.module("MCASMenu", []);
//var MenuList =[];
//MCASWebMenu.controller("MCASMenusController", function ($scope, $http) {

//    if (!$scope.Menus || $scope.Menus.length == 0) {
//        var resultPromise = $http.get(window.rootUrl + "Home/GetMenuList");
//        resultPromise.success(function (data) {
//            MenuList = data;
//            $scope.Menus = MenuList;
//        });
//    }
//});


// Create a module for naturalSorting
angular.module("naturalSort", [])

// The core natural service
.factory("naturalService", ["$locale", function ($locale) {
    "use strict";
    // amount of extra zeros to padd for sorting
    var padding = function (value) {
        return "00000000000000000000".slice(value.length);
    },

    // Converts a value to a string.  Null and undefined are converted to ''
		toString = function (value) {
		    if (value === null || value === undefined) return '';
		    return '' + value;
		},

    // Calculate the default out-of-order date format (dd/MM/yyyy vs MM/dd/yyyy)
        natDateMonthFirst = $locale.DATETIME_FORMATS.shortDate.charAt(0) === "M",
    // Replaces all suspected dates with a standardized yyyy-m-d, which is fixed below
        fixDates = function (value) {
            // first look for dd?-dd?-dddd, where "-" can be one of "-", "/", or "."
            return toString(value).replace(/(\d\d?)[-\/\.](\d\d?)[-\/\.](\d{4})/, function ($0, $m, $d, $y) {
                // temporary holder for swapping below
                var t = $d;
                // if the month is not first, we'll swap month and day...
                if (!natDateMonthFirst) {
                    // ...but only if the day value is under 13.
                    if (Number($d) < 13) {
                        $d = $m;
                        $m = t;
                    }
                } else if (Number($m) > 12) {
                    // Otherwise, we might still swap the values if the month value is currently over 12.
                    $d = $m;
                    $m = t;
                }
                // return a standardized format.
                return $y + "-" + $m + "-" + $d;
            });
        },

    // Fix numbers to be correctly padded
        fixNumbers = function (value) {
            // First, look for anything in the form of d.d or d.d.d...
            return value.replace(/(\d+)((\.\d+)+)?/g, function ($0, integer, decimal, $3) {
                // If there's more than 2 sets of numbers...
                if (decimal !== $3) {
                    // treat as a series of integers, like versioning,
                    // rather than a decimal
                    return $0.replace(/(\d+)/g, function ($d) {
                        return padding($d) + $d;
                    });
                } else {
                    // add a decimal if necessary to ensure decimal sorting
                    decimal = decimal || ".0";
                    return padding(integer) + integer + decimal + padding(decimal);
                }
            });
        },

    // Finally, this function puts it all together.
        natValue = function (value) {
            return fixNumbers(fixDates(value));
        };

    // The actual object used by this service
    return {
        naturalValue: natValue,
        naturalSort: function (a, b) {
            a = natVale(a);
            b = natValue(b);
            return (a < b) ? -1 : ((a > b) ? 1 : 0);
        }
    };
} ])

// Attach a function to the rootScope so it can be accessed by "orderBy"
.run(["$rootScope", "naturalService", function ($rootScope, naturalService) {
    "use strict";
    $rootScope.natural = function (field) {
        return function (item) {
            return naturalService.naturalValue(item[field]);
        };
    };
} ]);