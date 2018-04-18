(function (cr) {
    var serviceProviderModel = function () {
        var self = this;
        self.ClaimTypeId = 4;
        self.TPVehNo = '';
        self.PartyTypeId = '';
        self.ServiceProviderOption = '2';
        self.CompanyNameId = '';
        self.ContactPersonName = '';
        self.Address1 = '';
        self.OfficeNo = '';
        self.Address2 = '';
        self.Mobile = '';
        self.Address3 = '';
        self.Fax = '';
        self.CountryId = '';
        self.EmailAddress = '';
        self.PostalCode = '';
        self.ContactPersonName2nd = '';
        self.AppointedDate = new Date();
        self.OfficeNo2nd = '';
        self.VehNo = '';
        self.Mobile2nd = '';
        self.Reference = '';
        self.Fax2nd = '';
        self.StatusId = '';
        self.EmailAddress2nd = '';
        self.Remarks = '';
        self.CreatedBy = '';
        self.CreatedOn = new Date();
        self.ModifiedBy = '';
        self.ModifiedOn = new Date();

        //Cutom Fields
        self.ClaimantNameId = 1;
        self.AccidentClaimId = 0;
        self.PolicyId = 0;
        self.ServiceProviderId = 0;
    };
    cr.serviceProviderModel = serviceProviderModel;
} (window.ClaimObj));