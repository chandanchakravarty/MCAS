IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFHomeownerRiskDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFHomeownerRiskDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name          : Proc_GetPDFHomeownerRiskDetails            
Created by         : Pravesh K Chandel
Date               : 11-Sep-2009   
Purpose            :  getting HOme Risk Datasets while generating Pdfs
Revison History    :            
Used In            : Wolverine              
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/     
--drop PROCEDURE dbo.Proc_GetPDFHomeownerRiskDetails
CREATE     PROCEDURE dbo.Proc_GetPDFHomeownerRiskDetails           
(            
 @CUSTOMERID   int,            
 @POLID                int,            
 @VERSIONID   int,            
 @CALLEDFROM  VARCHAR(20)            
)            
AS            
BEGIN           

/*
1. Proc_GetPDFHomeowner_Dwelling_Details
2.Proc_GetPDFRVAdditionalInterests
3.PROC_GETPDF_HOME_SCHEDULE_ARTICLES_DETAILS

4.Proc_GetPDFHomeOwner_Coverage_Details
5.Proc_GetPDFHomeowner_otherstructures
6.Proc_GetPDFHomeowner_Additional_Interest
*/
--Table 1
exec Proc_GetPDFHomeowner_Dwelling_Details  @CUSTOMERID,@POLID,@VERSIONID,@CALLEDFROM
--Table 2
exec Proc_GetPDFHomeowner_otherstructures  @CUSTOMERID,@POLID,@VERSIONID,null,@CALLEDFROM
--Table-3 and 4
exec Proc_GetPDFHomeOwner_Coverage_Details @CUSTOMERID,@POLID,@VERSIONID,null,@CALLEDFROM
--Table 5
exec Proc_GetPDFHomeowner_Additional_Interest @CUSTOMERID,@POLID,@VERSIONID,0,@CALLEDFROM
--Table 6
exec Proc_GetPDFRVAdditionalInterests  @CUSTOMERID,@POLID,@VERSIONID,null,@CALLEDFROM
--Table 7
exec PROC_GETPDF_HOME_SCHEDULE_ARTICLES_DETAILS   @CUSTOMERID,@POLID,@VERSIONID,null,@CALLEDFROM

END




GO

