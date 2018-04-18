IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFHomeownerCommonDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFHomeownerCommonDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name          : Proc_GetPDFHomeownerCommonDetails            
Created by         : Pravesh K Chandel
Date               : 11-Sep-2009   
Purpose            :  getting Common  Datasets while generating Pdfs
Revison History    :            
Used In            : Wolverine              
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/     
--drop PROCEDURE dbo.Proc_GetPDFHomeownerCommonDetails   
CREATE     PROCEDURE dbo.Proc_GetPDFHomeownerCommonDetails           
(            
 @CUSTOMERID   int,            
 @POLID                int,            
 @VERSIONID   int,            
 @CALLEDFROM  VARCHAR(20)            
)            
AS            
BEGIN        
/*
1. Proc_GetPDFPolicyDetails
2. Proc_GetPDFApplicantDetails
3. Proc_GetPDFSolidFuelDetails
4. Proc_GetPDFRecreationalVehiclesDetails
5. PROC_GETPDF_HOME_SCHEDULE_ARTICLES_INFO
6. Proc_GetPDFHomeownerUnderwritingDetails
*/
---Tabel 0
exec Proc_GetPDFPolicyDetails @CUSTOMERID,@POLID,@VERSIONID,@CALLEDFROM
---Tabel 1 and 2
exec Proc_GetPDFApplicantDetails @CUSTOMERID,@POLID,@VERSIONID,@CALLEDFROM
---Tabel 3 
exec Proc_GetPDFHomeownerUnderwritingDetails @CUSTOMERID,@POLID,@VERSIONID,@CALLEDFROM
---Tabel 4 and 5 
exec Proc_GetPDFRecreationalVehiclesDetails @CUSTOMERID,@POLID,@VERSIONID,@CALLEDFROM
---Tabel 6 and 7
exec PROC_GETPDF_HOME_SCHEDULE_ARTICLES_INFO @CUSTOMERID,@POLID,@VERSIONID,@CALLEDFROM
---Tabel 8
exec Proc_GetPDFSolidFuelDetails @CUSTOMERID,@POLID,@VERSIONID,@CALLEDFROM
--TABLE 9
exec Proc_GetPDFAcord81GeneralInfo @CUSTOMERID,@POLID,@VERSIONID,@CALLEDFROM

END


GO

