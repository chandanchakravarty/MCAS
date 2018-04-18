IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyInfoFromPolNumberPDF]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyInfoFromPolNumberPDF]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_GetPolicyInfoFromPolNumberPDF                    
Created by      : Mohit Agarwal                   
Date            : 9-Aug-2007    
Purpose         : To get Policy Information from policy number                    
Revison History :                        
Used In         : Wolverine                        
                
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/                        
    
--drop PROCEDURE dbo.Proc_GetPolicyInfoFromPolNumberPDF 
CREATE PROCEDURE [dbo].[Proc_GetPolicyInfoFromPolNumberPDF]                    
(                        
 @POLICY_NUMBER varchar(100)    , 
 @USER_ID		Int = NULL 
)                            
AS                      
BEGIN     
--DECLARE @POLICY_INFO VARCHAR (100)    
    
SELECT     ISNULL(CCL.CUSTOMER_FIRST_NAME,'') + ' ' + CASE WHEN ISNULL(CCL.CUSTOMER_MIDDLE_NAME,'') = '' THEN '' ELSE CCL.CUSTOMER_MIDDLE_NAME + ' ' END                              
  + ISNULL(CCL.CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME,                                                        
  RTRIM(CCL.CUSTOMER_ADDRESS1) AS CUSTOMER_ADDRESS1, RTRIM(CCL.CUSTOMER_ADDRESS2) AS CUSTOMER_ADDRESS2,                                                         
  RTRIM(CCL.CUSTOMER_CITY) + ',' AS CUSTOMER_CITY, MCSL.STATE_CODE AS STATE_CODE, CCL.CUSTOMER_ZIP,                                                        
  MAL.AGENCY_DISPLAY_NAME                                                       
     
FROM POL_CUSTOMER_POLICY_LIST POL
LEFT OUTER JOIN MNT_AGENCY_LIST MAL ON MAL.AGENCY_ID = POL.AGENCY_ID
LEFT OUTER JOIN CLT_CUSTOMER_LIST CCL ON CCL.CUSTOMER_ID = POL.CUSTOMER_ID
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL ON MCSL.STATE_ID = CCL.CUSTOMER_STATE

WHERE POLICY_NUMBER=@POLICY_NUMBER    
--SET @POLICY_INFO = POLICY_INFO    
--RETURN @POLICY_INFO    


SELECT   AGN.AGENCY_DISPLAY_NAME  AS AGENCY_DISPLAY_NAME  ,  ISNULL(USR.USER_FNAME,'') + ' ' + ISNULL(USR.USER_LNAME,'') AS USERNAME
FROM MNT_USER_LIST USR 
INNER JOIN MNT_AGENCY_LIST AGN 
ON USR.USER_SYSTEM_ID = AGN.AGENCY_CODE
WHERE USR.USER_ID = @USER_ID 
END    
    
    


    
  





GO

