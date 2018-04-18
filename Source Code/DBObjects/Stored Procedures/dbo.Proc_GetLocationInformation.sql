IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLocationInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLocationInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
 /*----------------------------------------------------------                                          
Proc Name             : Dbo.Proc_GetLocationInformation                                          
Created by            : Santosh Kumar Gautam                                         
Date                  : 09/11/2010                                         
Purpose               : To retrieve the location id and address information                                           
Revison History       :                                          
Used In               : To fill dropdown at risk information page.(CLAIM module)                                          
------------------------------------------------------------                                          
Date     Review By          Comments             
    
drop Proc Proc_GetLocationInformation                                 
------   ------------       -------------------------*/                                          
--             
              
--           
        
CREATE PROCEDURE [dbo].[Proc_GetLocationInformation]              
               
@CUSTOMER_ID         INT,                                                                          
@POLICTY_ID          INT,                                                                          
@POLICY_VERSION_ID   INT,  
@CLAIM_ID            INT                                                                          
                                                                    
              
AS              
BEGIN   
  
SELECT   P.LOCATION AS LOCATION_ID ,  
         (ISNULL(L.NAME,'')+'-'+ISNULL(L.LOC_ADD1,'')+'-'+  
         ISNULL(CONVERT(VARCHAR(10), L.LOC_NUM),'')+'-' +  
         ISNULL(L.LOC_ADD2,'')+'-'+ISNULL(L.DISTRICT,'')+'-'+   
         ISNULL(L.LOC_CITY,''))   
         AS LOCATION  
FROM POL_LOCATIONS L WITH(NOLOCK) INNER JOIN  
     POL_PERILS    P ON  P.LOCATION>0 AND P.LOCATION=L.LOCATION_ID INNER JOIN  
     CLM_CLAIM_INFO I ON I.CLAIM_ID=@CLAIM_ID  
WHERE (P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICTY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')     
      
END              
              
       
GO

