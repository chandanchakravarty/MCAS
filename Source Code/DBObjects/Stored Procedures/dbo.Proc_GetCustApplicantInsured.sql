IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustApplicantInsured]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustApplicantInsured]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name          : Dbo.Proc_GetCustApplicantInsured            
Created by           : Mohit Gupta            
Date                    : 02/09/2005            
Purpose               :             
Revison History :            
Used In                :   Wolverine              
------------------------------------------------------------            
Date     Review By          Comments            
drop proc  Proc_GetCustApplicantInsured 
exec      Proc_GetCustApplicantInsured  2727    
------   ------------       -------------------------*/            
CREATE PROCEDURE [dbo].[Proc_GetCustApplicantInsured]            
(            
 @CUSTOMERID int             
)            
AS            
BEGIN            
            
SELECT            
T1.APPLICANT_ID APPLICANT_ID,            
T3.LOOKUP_VALUE_DESC TITLE,            
T1.FIRST_NAME+ ' ' + isnull(T1. MIDDLE_NAME,' ')+' '+ isnull(T1.LAST_NAME,' ')           
 +(CASE T1.SUFFIX WHEN '' THEN '' ELSE +' '+isnull(T1.SUFFIX,' ') END)APPLICANTNAME,--Added 'Suffix' for Itrack Issue 5485 on 17 April 2009          
T1.ADDRESS1+ ','+ ISNULL(T1.NUMBER,'')+isnull(','+T1. ADDRESS2,' ')  ADDRESS,MNT_ACTIVITY_MASTER.ACTIVITY_DESC POSITION,             
T1.CITY CITY,            
T2.STATE_NAME STATE ,            
T1.ZIP_CODE ZIP_CODE   ,    
'' AS COMMISSION_PERCENT ,    
'' AS FEES_PERCENT ,  
'' AS  PRO_LABORE_PERCENT  
FROM      CLT_APPLICANT_LIST T1 LEFT OUTER JOIN MNT_LOOKUP_VALUES T3   
ON T1.TITLE=T3.LOOKUP_UNIQUE_ID LEFT OUTER JOIN MNT_LOOKUP_VALUES T4   
ON T4.LOOKUP_UNIQUE_ID=T1.POSITION LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST T2
    
ON T1.STATE=T2.STATE_ID AND T1.COUNTRY=T2.COUNTRY_ID    
LEFT OUTER JOIN 
MNT_ACTIVITY_MASTER
on
MNT_ACTIVITY_MASTER.ACTIVITY_ID= T1.TITLE            
WHERE T1. IS_ACTIVE='Y'     
          AND             
    T1.CUSTOMER_ID=@CUSTOMERID            
END 
GO

