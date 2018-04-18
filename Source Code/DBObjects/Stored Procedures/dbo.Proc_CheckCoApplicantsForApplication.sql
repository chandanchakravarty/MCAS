IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckCoApplicantsForApplication]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckCoApplicantsForApplication]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_CheckCoApplicantsForApplication  
Created by           : Mohit Gupta  
Date                    : 02/09/2005  
Purpose               :   
Revison History :  
Used In                :   Wolverine    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop PROCEDURE Proc_CheckCoApplicantsForApplication  
CREATE   PROCEDURE dbo.Proc_CheckCoApplicantsForApplication  
(  
 @CUSTOMERID  int,  
 @APPID               int,  
 @APPVERSIONID  int  
)  
AS  
BEGIN  
  
/*SELECT   
T1.APPLICANT_ID,  
T4.LOOKUP_VALUE_DESC TITLE,  
T1.FIRST_NAME+ ' ' + isnull(T1. MIDDLE_NAME,' ')+' '+ isnull(T1.LAST_NAME,' ') APPLICANTNAME,  
T1.ADDRESS1+' '+isnull(T1.ADDRESS2,'') ADDRESS,  
T1.CITY  CITY,  
T3.STATE_NAME  STATE,  
T1.ZIP_CODE  ZIP_CODE,  
T2.IS_PRIMARY_APPLICANT  
FROM     CLT_APPLICANT_LIST T1,  
    APP_APPLICANT_LIST T2,  
    MNT_COUNTRY_STATE_LIST T3,  
    MNT_LOOKUP_VALUES T4   
WHERE   T1.APPLICANT_ID=T2.APPLICANT_ID       AND   
 T1.STATE=T3.STATE_ID                   AND    
 T1.TITLE=T4.LOOKUP_UNIQUE_ID            AND  
 T2.CUSTOMER_ID=@CUSTOMERID          AND  
 T2.APP_ID=@APPID                                    AND   
 T2.APP_VERSION_ID=@APPVERSIONID   AND  
 T1. IS_ACTIVE='Y'  */


SELECT 
	T2.APPLICANT_ID,  
	T4.LOOKUP_VALUE_DESC TITLE,  
	T2.FIRST_NAME+ ' ' + isnull(T2. MIDDLE_NAME,' ')+' '+ isnull(T2.LAST_NAME,' ') APPLICANTNAME,  
	T2.ADDRESS1+' '+isnull(T2.ADDRESS2,'') ADDRESS,  
	T2.CITY  CITY,  
	T3.STATE_NAME  STATE,  
	T2.ZIP_CODE  ZIP_CODE,  
	T1.IS_PRIMARY_APPLICANT  
FROM 
	APP_APPLICANT_LIST T1 
LEFT OUTER JOIN 
	CLT_APPLICANT_LIST T2
ON 
	T1.APPLICANT_ID = T2.APPLICANT_ID 
LEFT OUTER JOIN 
	MNT_COUNTRY_STATE_LIST T3 
ON 
	T2.STATE = T3.STATE_ID
LEFT OUTER JOIN 
	MNT_LOOKUP_VALUES T4 
ON 
	T2.TITLE=T4.LOOKUP_UNIQUE_ID    
WHERE 
	T1.CUSTOMER_ID=@CUSTOMERID AND 
	T1.APP_ID=@APPID AND 
	T1.APP_VERSION_ID=@APPVERSIONID AND 
	T2.IS_ACTIVE='Y'
  
  
  
END  
  



GO

