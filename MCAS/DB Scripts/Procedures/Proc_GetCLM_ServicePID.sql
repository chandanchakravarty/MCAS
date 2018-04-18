IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ServicePID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ServicePID]
GO

CREATE Proc [dbo].[Proc_GetCLM_ServicePID]
(
@ServiceProviderId nvarchar(100)
)
as
BEGIN
SET FMTONLY OFF;
SELECT [ServiceProviderId]
      ,[ClaimTypeId]
      ,[ClaimantNameId]
      ,[PartyTypeId]
      ,[ServiceProviderTypeId]
      ,[CompanyNameId]
      ,[AppointedDate]
      ,[Address1]
      ,[Address2]
      ,[Address3]
      ,[City]
      ,[State]
      ,[CountryId]
      ,[PostalCode]
      ,[Reference]
      ,[ContactPersonName]
      ,[EmailAddress]
      ,[OfficeNo]
      ,[Mobile]
      ,[Fax]
      ,[ContactPersonName2nd]
      ,[EmailAddress2nd]
      ,[OfficeNo2nd]
      ,[Mobile2nd]
      ,[Fax2nd]
      ,[StatusId]
      ,[Remarks]
      ,[Createdby]
      ,[Createddate]
      ,[Modifiedby]
      ,[Modifieddate]
      ,[AccidentId]
      ,[PolicyId]
      ,[IsActive]
      ,[ClaimRecordNo]
  FROM [dbo].[CLM_ServiceProvider] where [ServiceProviderId]=@ServiceProviderId
END
GO


