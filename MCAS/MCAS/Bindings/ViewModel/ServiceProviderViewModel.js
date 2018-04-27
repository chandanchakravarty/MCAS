ClaimMainApp.controller("ServiceProviderViewModel", function ($scope, $http, viewModelHelper, validator) {
    $scope.viewModelHelper = viewModelHelper;
    $scope.createServiceProvider = new ClaimObj.serviceProviderModel;
    $scope.ServiceProviderId = ServiceProviderId;
    $scope.createServiceProvider.AccidentClaimId = AccidentClaimId;
    $scope.createServiceProvider.PolicyId = PolicyId;
    $scope.ActDone = ActDone;
    $scope.Viewmode = Viewmode;
    $scope.PartyTypeList = [];
    $scope.CompanyNameList = [];
    $scope.UserCountryList = [];
    $scope.StatusList = [];
    $scope.ServiceProviderOptionList = [];
    $scope.ClaimTypeList = [];
    $scope.TPVehicleNoList = [];
    $scope.baseUrl = BaseURL;
    $scope.checkDataVals = 0;
    $scope.checkDataVals2 = 0;
    //    $scope.minimise = true;
    $scope.addServiceProvider = 0;
    var createServiceProviderModelRules = [];
    //    $scope.MsgList = [];
    $scope.SuccessMsg = "";
    $scope.createServiceProvider.CreatedOnDate = "";
    $scope.createServiceProvider.CreatedOnTime = "";
    $scope.createServiceProvider.ModifiedOnDate = "";
    $scope.createServiceProvider.ModifiedOnTime = "";

    if ($scope.ActDone == "1") {
        $scope.SuccessMsg = "Record saved successfully.";
    } else if ($scope.ActDone == "2") {
        $scope.SuccessMsg = "Record updated successfully.";
    }

    function GetAllLookUps() {
        $http.get($scope.baseUrl + 'API/Service/GetLookUpsForServiceProvider?accidentId=' + $scope.createServiceProvider.AccidentClaimId, '')
            .success(function (result) {
                $scope.PartyTypeList = result.PartyTypeList;
                $scope.CompanyNameList = result.CompanyNameList;
                $scope.UserCountryList = result.UserCountryList;
                $scope.StatusList = result.StatusList;
                $scope.ServiceProviderOptionList = result.ServiceProviderOptionList;
                $scope.ClaimTypeList = result.ClaimTypeList;
                $scope.TPVehicleNoList = result.TPVehicleNoList;
                GetAllData();
            })
            .error(function (error) {
                console.log(error);
            });
    }

    if ($scope.ServiceProviderId != "-1") {
        $scope.addServiceProvider = 1;
        GetAllLookUps();
    }

    function GetAllData() {
        if (($scope.ServiceProviderId != "") && ($scope.ServiceProviderId > 0) && ($scope.ServiceProviderId != null) && ($scope.ServiceProviderId != undefined)) {
            $http.get($scope.baseUrl + 'API/Service/FetchServiceProvider?ServiceProviderId=' + $scope.ServiceProviderId, '')
            .success(function (result) {
                BindValues(result);
            })
            .error(function (error) {
                console.log(error);
            });
        }
    }

    $scope.$watchGroup(['createServiceProvider.PartyTypeId', 'createServiceProvider.ServiceProviderOption'], function () {
        if (($scope.createServiceProvider.PartyTypeId != "") && ($scope.createServiceProvider.PartyTypeId != null) && ($scope.createServiceProvider.PartyTypeId != undefined) ||
        ($scope.createServiceProvider.ServiceProviderOption != "") && ($scope.createServiceProvider.ServiceProviderOption != null) && ($scope.createServiceProvider.ServiceProviderOption != undefined)) {
            var partyId = $scope.createServiceProvider.PartyTypeId;
            partyId == "1" ? $("#addPartyType").html(AddInsurer) : partyId == "2" ? $("#addPartyType").html(AddSurveyor) : partyId == "3" ? $("#addPartyType").html(AddLawyer) : $("#addPartyType").html(AddWorkshop);
            var provider = $scope.createServiceProvider.ServiceProviderOption;
            $scope.CompanyNameList = [];
            if (($scope.checkDataVals > 1) || (($scope.ServiceProviderId == "") || ($scope.ServiceProviderId == 0) || ($scope.ServiceProviderId == null) || ($scope.ServiceProviderId == undefined))) {
                $scope.createServiceProvider.CountryId = "SG";
                $(".empty11").val('');
                emptyValues();
            } else {
                $scope.checkDataVals = $scope.checkDataVals + 1;
            }
            if (partyId != "") {
                debugger;
                $http.post($scope.baseUrl + 'API/Service/GetCompanyNameList?InsurerType=' + provider + '&PartyTypeId=' + partyId + '&Status=' + $scope.Viewmode + '')
                .success(function (result) {
                    $scope.CompanyNameList = result;
                })
                .error(function (error) {
                    console.log(error);
                });
            }
        }
    });

    $scope.$watch('createServiceProvider.CompanyNameId', function () {
        if (($scope.createServiceProvider.CompanyNameId != "") && ($scope.createServiceProvider.CompanyNameId != null) && ($scope.createServiceProvider.CompanyNameId != undefined)) {
            var tempServiceProviderOption = "";
            if ($scope.createServiceProvider.ServiceProviderOption == "1") {
                tempServiceProviderOption = "0";
            } else if ($scope.createServiceProvider.ServiceProviderOption == "2") {
                tempServiceProviderOption = "1";
            }
            $http.post($scope.baseUrl + 'ClaimProcessing/GetCompanyNameDetailList?InsurerType=' + tempServiceProviderOption + "&PartyTypeId=" + $scope.createServiceProvider.PartyTypeId + '&CompanyNameId=' + $scope.createServiceProvider.CompanyNameId, '')
            .success(function (result) {
                if (($scope.checkDataVals2 >= 1) || ($scope.ServiceProviderId == 0)) {
                    BindValuesByCompanyName(result[0]);
                } else {
                    $scope.checkDataVals2 = $scope.checkDataVals2 + 1;
                }
            })
            .error(function (error) {
                console.log(error);
            });
        }
    });

    var setupRules = function () {
        createServiceProviderModelRules.push(new validator.PropertyRule("ClaimTypeId", {
            required: { message: ClaimIdReq }
        }));
        createServiceProviderModelRules.push(new validator.PropertyRule("TPVehNo", {
            required: { message: TPVehNoReq }
        }));
        createServiceProviderModelRules.push(new validator.PropertyRule("PartyTypeId", {
            required: { message: PartyTypeIdReq }
        }));
        createServiceProviderModelRules.push(new validator.PropertyRule("ServiceProviderOption", {
            required: { message: ServiceProviderOptionReq }
        }));
        createServiceProviderModelRules.push(new validator.PropertyRule("CompanyNameId", {
            required: { message: CompanyNameIdReq }
        }));
        //createServiceProviderModelRules.push(new validator.PropertyRule("Address1", {
        //    required: { message: Address1Req }
        //}));
        //createServiceProviderModelRules.push(new validator.PropertyRule("CountryId", {
        //    required: { message: CountryIdReq }
        //}));
        //createServiceProviderModelRules.push(new validator.PropertyRule("Reference", {
        //    required: { message: ReferenceReq }
        //}));
        createServiceProviderModelRules.push(new validator.PropertyRule("StatusId", {
            required: { message: StatusIdReq }
        }));
    }

    setupRules();

    $scope.saveServiceProvider = function () {
        validator.ValidateModel($scope.createServiceProvider, createServiceProviderModelRules);
        viewModelHelper.modelIsValid = $scope.createServiceProvider.isValid;
        viewModelHelper.modelErrors = $scope.createServiceProvider.errors;
        if (viewModelHelper.modelIsValid) {
            //$scope.createServiceProvider.AppointedDate = $("#AppointedDate").val();
            if ($("#AppointedDate").val() != "") {
                $scope.createServiceProvider.AppointedDate = $("#AppointedDate").val().split("/")[1] + "/" + $("#AppointedDate").val().split("/")[0] + "/" + $("#AppointedDate").val().split("/")[2];
            } else {
                $scope.createServiceProvider.AppointedDate = null;
            }
            $http.post($scope.baseUrl + 'API/Service/CreateServiceProvider', $scope.createServiceProvider)
            .success(function (result) {
                console.log(result);
                //window.location.href = $scope.baseUrl + 'ClaimRegistrationProcessing/ClmSPDltPCNTX?Q=' + result;
                window.location.href = $scope.baseUrl + 'ClaimRegistrationProcessing/ClmSPDltPCNTX' + result;
            })
            .error(function (error) {
                $scope.checkErrors(error);
                console.log(error);
                //                $scope.MsgList = [];
                //                $scope.MsgList = angular.copy(error);
                //                $scope.minimise = true;
                //                $("ul.errorList").show(1000);
                //                $("html, body").animate({ scrollTop: 0 }, "slow");
            });
        }
        else {
            $scope.checkErrors($scope.createServiceProvider.errors);
            viewModelHelper.modelErrors = $scope.createServiceProvider.errors;
            //            $scope.MsgList = [];
            //            $scope.MsgList = angular.copy($scope.createServiceProvider.errors);
            //            $scope.minimise = true;
            //            $("ul.errorList").show(1000);
            //            $("html, body").animate({ scrollTop: 0 }, "slow");
        }
    };

    $scope.errorsClear = function () {
        $scope.ClaimTypeIdError = "";
        $scope.TPVehNoError = "";
        $scope.PartyTypeIdError = "";
        $scope.ServiceProviderOptionError = "";
        $scope.CompanyNameIdError = "";
        $scope.Address1Error = "";
        $scope.CountryIdError = "";
        $scope.ReferenceError = "";
        $scope.StatusIdError = "";
    };

    $scope.errorsClear();

    $scope.resetServiceProvider = function () {
        window.location.href = $scope.baseUrl + 'ClaimRegistrationProcessing/ClmSPDltPCNTX?PolicyId=0&AccidentClaimId=' + $scope.createServiceProvider.AccidentClaimId + "&ServiceProviderId=0";
    };

    $scope.$watch('createServiceProvider.ClaimTypeId', function () {
        $scope.ClaimTypeIdError = "";
    });
    $scope.$watch('createServiceProvider.TPVehNo', function () {
        $scope.TPVehNoError = "";
    });
    $scope.$watch('createServiceProvider.PartyTypeId', function () {
        $scope.PartyTypeIdError = "";
    });
    $scope.$watch('createServiceProvider.ServiceProviderOption', function () {
        $scope.ServiceProviderOptionError = "";
    });
    $scope.$watch('createServiceProvider.CompanyNameId', function () {
        $scope.CompanyNameIdError = "";
    });
    $scope.$watch('createServiceProvider.Address1', function () {
        $scope.Address1Error = "";
    });
    $scope.$watch('createServiceProvider.CountryId', function () {
        $scope.CountryIdError = "";
    });
    $scope.$watch('createServiceProvider.Reference', function () {
        $scope.ReferenceError = "";
    });
    $scope.$watch('createServiceProvider.StatusId', function () {
        $scope.StatusIdError = "";
    });

    $scope.checkErrors = function (errorList) {
        $scope.errorsClear();
        angular.forEach(errorList, function (value, key) {
            if (value.indexOf("Claim Type") >= 0) {
                $scope.ClaimTypeIdError = value;
            }
            if (value.indexOf("TP Vehicle No") >= 0) {
                $scope.TPVehNoError = value;
            }
            if (value.indexOf("Party Type") >= 0) {
                $scope.PartyTypeIdError = value;
            }
            if (value.indexOf("Service Provider") >= 0) {
                $scope.ServiceProviderOptionError = value;
            }
            if (value.indexOf("Company Name") >= 0) {
                $scope.CompanyNameIdError = value;
            }
            if (value.indexOf("Address1") >= 0) {
                $scope.Address1Error = value;
            }
            if (value.indexOf("Country") >= 0) {
                $scope.CountryIdError = value;
            }
            if ((value.indexOf("Ref No") >= 0) || (value.indexOf("Reference") >= 0)) {
                $scope.ReferenceError = value;
            }
            if (value.indexOf("Status") >= 0) {
                $scope.StatusIdError = value;
            }
        });
    };

    //    $scope.changeMsgDiv = function () {
    //        if ($scope.minimise) {
    //            $("ul.errorList").hide(1000);
    //        } else {
    //            $("ul.errorList").show(1000);
    //        }
    //        $scope.minimise = !$scope.minimise;
    //    };

    function BindValues(result) {
        $scope.createServiceProvider.AccidentClaimId = result.AccidentId;
        $scope.createServiceProvider.Address1 = result.Address1;
        $scope.createServiceProvider.Address2 = result.Address2;
        $scope.createServiceProvider.Address3 = result.Address3;
        $scope.createServiceProvider.ClaimTypeId = result.ClaimTypeId;
        $scope.createServiceProvider.ClaimantNameId = result.ClaimantNameId;
        $scope.createServiceProvider.CompanyNameId = result.CompanyNameId;
        $scope.createServiceProvider.ContactPersonName = result.ContactPersonName;
        $scope.createServiceProvider.ContactPersonName2nd = result.ContactPersonName2nd;
        $scope.createServiceProvider.CountryId = result.CountryId;
        $scope.createServiceProvider.EmailAddress = result.EmailAddress;
        $scope.createServiceProvider.EmailAddress2nd = result.EmailAddress2nd;
        $scope.createServiceProvider.Fax = result.Fax;
        $scope.createServiceProvider.Fax2nd = result.Fax2nd;
        $scope.createServiceProvider.Mobile = result.Mobile;
        $scope.createServiceProvider.Mobile2nd = result.Mobile2nd;
        $scope.createServiceProvider.OfficeNo = result.OfficeNo;
        $scope.createServiceProvider.OfficeNo2nd = result.OfficeNo2nd;
        $scope.createServiceProvider.PartyTypeId = result.PartyTypeId.toString();
        $scope.createServiceProvider.PostalCode = result.PostalCode;
        $scope.createServiceProvider.Reference = result.Reference;
        $scope.createServiceProvider.Remarks = result.Remarks;
        $scope.createServiceProvider.ServiceProviderId = result.ServiceProviderId;
        $scope.createServiceProvider.ServiceProviderOption = result.ServiceProviderTypeId;
        $scope.createServiceProvider.StatusId = result.StatusId.toString();
        $scope.createServiceProvider.TPVehNo = result.TPVehNo;
        $scope.createServiceProvider.VehNo = result.VehNo;
        if (result.AppointedDate != null) {
            $scope.createServiceProvider.AppointedDate = (result.AppointedDate.split("T")[0]).split("-")[2] + '/' + (result.AppointedDate.split("T")[0]).split("-")[1] + '/' + (result.AppointedDate.split("T")[0]).split("-")[0];
            $("#AppointedDate").val($scope.createServiceProvider.AppointedDate);
        }
        $scope.checkDataVals = 1;
        $scope.createServiceProvider.CreatedBy = result.CreatedBy;
        $scope.createServiceProvider.CreatedOn = result.CreatedOn;
        if ($scope.createServiceProvider.CreatedOn != null) {
            $scope.createServiceProvider.CreatedOnDate = ($scope.createServiceProvider.CreatedOn.split("T")[0]).split("-")[2] + "/" + ($scope.createServiceProvider.CreatedOn.split("T")[0]).split("-")[1] + "/" + ($scope.createServiceProvider.CreatedOn.split("T")[0]).split("-")[0];
            $scope.createServiceProvider.CreatedOnTime = $scope.createServiceProvider.CreatedOn.split("T")[1];
        }
        $scope.createServiceProvider.ModifiedBy = result.ModifiedBy;
        $scope.createServiceProvider.ModifiedOn = result.ModifiedOn;
        if ($scope.createServiceProvider.ModifiedOn != null) {
            $scope.createServiceProvider.ModifiedOnDate = ($scope.createServiceProvider.ModifiedOn.split("T")[0]).split("-")[2] + "/" + ($scope.createServiceProvider.ModifiedOn.split("T")[0]).split("-")[1] + "/" + ($scope.createServiceProvider.ModifiedOn.split("T")[0]).split("-")[0];
            $scope.createServiceProvider.ModifiedOnTime = $scope.createServiceProvider.ModifiedOn.split("T")[1];
        }
    }

    function BindValuesByCompanyName(result) {
        $scope.createServiceProvider.PostalCode = result.PostalCode1;
        $scope.createServiceProvider.StatusId = result.Status == "Active" ? "1" : "1" == result.Status ? "1" : "Inactive" == result.Status ? "2" : "2" == result.Status ? "2" : "0";
        $scope.createServiceProvider.Address1 = result.Address1;
        $scope.createServiceProvider.CountryId = result.CountryId;
        $scope.createServiceProvider.Address2 = result.Address2;
        $scope.createServiceProvider.Address3 = result.Address3;
        $scope.createServiceProvider.ContactPersonName = result.ContactPersonName;
        $scope.createServiceProvider.EmailAddress = result.EmailAddress;
        $scope.createServiceProvider.OfficeNo = result.OfficeNo;
        $scope.createServiceProvider.Mobile = result.Mobile;
        $scope.createServiceProvider.Fax = result.Fax;
        $scope.createServiceProvider.ContactPersonName2nd = result.ContactPersonName2nd;
        $scope.createServiceProvider.EmailAddress2nd = result.EmailAddress2nd;
        $scope.createServiceProvider.OfficeNo2nd = result.OfficeNo2nd;
        $scope.createServiceProvider.Mobile2nd = result.Mobile2nd;
        $scope.createServiceProvider.Fax2nd = result.Fax2nd;
        $scope.createServiceProvider.Remarks = result.Remarks;
    }

    function emptyValues() {
        $scope.createServiceProvider.ContactPersonName = "";
        $scope.createServiceProvider.Address1 = "";
        $scope.createServiceProvider.Address2 = "";
        $scope.createServiceProvider.Mobile = "";
        $scope.createServiceProvider.Address3 = "";
        $scope.createServiceProvider.Fax = "";
        $scope.createServiceProvider.EmailAddress = "";
        $scope.createServiceProvider.PostalCode = "";
        $scope.createServiceProvider.ContactPersonName2nd = "";
        $scope.createServiceProvider.OfficeNo2nd = "";
        $scope.createServiceProvider.Mobile2nd = "";
        $scope.createServiceProvider.Reference = "";
        $scope.createServiceProvider.Fax2nd = "";
        $scope.createServiceProvider.StatusId = "";
        $scope.createServiceProvider.EmailAddress2nd = "";
        $scope.createServiceProvider.Remarks = "";
    }


});