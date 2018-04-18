IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCompanyNameDetailList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCompanyNameDetailList]
GO

CREATE PROCEDURE [dbo].[Proc_GetCompanyNameDetailList]
@w_InsurerType [nvarchar](max),
@w_PartyTypeId [nvarchar](max),
@w_CompanyNameId [nvarchar](max)
AS
SET FMTONLY OFF;
IF OBJECT_ID('tempdb..#mytemptable') IS NOT NULL
DROP TABLE #mytemptable
CREATE TABLE #mytemptable(
[Address] [nvarchar](max),
[PostalCode] [nvarchar](max),
[country][nvarchar](max),
[Status] [nvarchar](max),
[Address2] [nvarchar](max),
[Address3] [nvarchar](max),
[City] [nvarchar](max),
[State] [nvarchar](max),
[FirstContactPersonName] [nvarchar](max),
[EmailAddress1] [nvarchar](max),
[OfficeNo1] [nvarchar](max),
[MobileNo1] [nvarchar](max),
[FaxNo1]  [nvarchar](max),
[SecondContactPersonName] [nvarchar](max),
[EmailAddress2] [nvarchar](max),
[OfficeNo2] [nvarchar](max),
[MobileNo2] [nvarchar](max),
[FaxNo2] [nvarchar](max),
[Remarks] [nvarchar](max)
)

BEGIN

IF @w_PartyTypeId = '1'
Begin
Insert INTO #mytemptable (Address,PostalCode,country,Status,Address2,Address3,City,State,FirstContactPersonName,EmailAddress1,OfficeNo1,MobileNo1,FaxNo1,SecondContactPersonName,EmailAddress2,OfficeNo2,MobileNo2,FaxNo2,Remarks) select Address,PostalCode,country,Status,Address2,Address3,City,State,FirstContactPersonName,EmailAddress1,OfficeNo1,MobileNo1,FaxNo1,SecondContactPersonName,EmailAddress2,OfficeNo2,MobileNo2,FaxNo2,Remarks from MNT_Cedant where CedantId=@w_CompanyNameId
END

IF @w_PartyTypeId = '2'
Begin
Insert INTO #mytemptable (Address,PostalCode,country,Status,Address2,Address3,City,State,FirstContactPersonName,EmailAddress1,OfficeNo1,MobileNo1,FaxNo1,SecondContactPersonName,EmailAddress2,OfficeNo2,MobileNo2,FaxNo2,Remarks) select Address1,PostCode,country,Status,Address2,Address3,city,Province,conper,EMail,TelNoOff,MobileNo,FaxNo,Classification,EmailAddress2,OffNo2,MobileNo2,Fax2,Remarks from MNT_Adjusters where AdjusterId=@w_CompanyNameId
END

IF @w_PartyTypeId = '3'
Begin
Insert INTO #mytemptable (Address,PostalCode,country,Status,Address2,Address3,City,State,FirstContactPersonName,EmailAddress1,OfficeNo1,MobileNo1,FaxNo1,SecondContactPersonName,EmailAddress2,OfficeNo2,MobileNo2,FaxNo2,Remarks) select Address1,PostCode,country,Status,Address2,Address3,city,Province,conper,EMail,TelNoOff,MobileNo,FaxNo,Classification,EmailAddress2,OffNo2,MobileNo2,Fax2,Remarks from MNT_Adjusters where AdjusterId=@w_CompanyNameId
END

IF @w_PartyTypeId = '4'
Begin
Insert INTO #mytemptable (Address,PostalCode,country,Status,Address2,Address3,City,State,FirstContactPersonName,EmailAddress1,OfficeNo1,MobileNo1,FaxNo1,SecondContactPersonName,EmailAddress2,OfficeNo2,MobileNo2,FaxNo2,Remarks) select Address1,PostalCode,country,Status,Address2,Address3,city,State,PersonInCharge,EMail,TelephoneOff,MobileNo,Fax,ContactPerson,EmailAddress2,OffNo2,MobileNo2,Fax2,Remarks from MNT_DepotMaster
 where DepotId =@w_CompanyNameId
END

select Address,PostalCode,country,Status,Address2,Address3,City,State,FirstContactPersonName,EmailAddress1,OfficeNo1,MobileNo1,FaxNo1,SecondContactPersonName,EmailAddress2,OfficeNo2,MobileNo2,FaxNo2,Remarks from #mytemptable

END



GO


