CREATE PROCEDURE [dbo].[Proc_GetCLM_ServicePID]
	@ServiceProviderId [nvarchar](100)
WITH EXECUTE AS CALLER
AS
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
      ,[VehNo]
      ,[TPVehNo]  
  FROM [dbo].[CLM_ServiceProvider] where [ServiceProviderId]=@ServiceProviderId  
END


