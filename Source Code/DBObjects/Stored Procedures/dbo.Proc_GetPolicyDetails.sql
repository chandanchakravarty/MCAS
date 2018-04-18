IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name       : dbo.Proc_GetPolicyDetails         
Created by      : Vijay Arora          
Date            : 28/10/2005              
Purpose        : Get the policy Details.        
Revison History :              
modified by  : pravesh K. Chandel  
Dated  : 7 march 2007  
purpose  : to fetch policy information on the basis policy id and policy version id as well as app id as app versin id  
Used In         : Wolverine                
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
--drop proc dbo.Proc_GetPolicyDetails 28033,0,0,41,1 
CREATE PROC  [dbo].[Proc_GetPolicyDetails]          
@CUSTOMER_ID INT,          
@APP_ID INT=null,          
@APP_VERSION_ID SMALLINT =null,       
@POLICY_ID INT=null,          
@POLICY_VERSION_ID SMALLINT =null       
AS          
BEGIN   
  
if ( @APP_ID is not null and @APP_VERSION_ID is not null and @APP_ID !=0 and @APP_VERSION_ID !=0)        
 SELECT POLICY_ID,POLICY_VERSION_ID,POLICY_LOB,POLICY_SUBLOB,POLICY_NUMBER,POLICY_LOB AS LOB_ID , APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,     
 POLICY_DISP_VERSION,IS_ACTIVE ,PAYOR,CO_INSURANCE,ISNULL(POLICY_STATUS,'') AS POLICY_STATUS ,ISNULL(APP_STATUS,'') as APP_STATUS,  
 TRANSACTION_TYPE, --Added By Lalit Nov 08,2010,
 APP_NUMBER  --Added By Lalit Nov 22,2010,
 FROM POL_CUSTOMER_POLICY_LIST  with(nolock)      
 WHERE CUSTOMER_ID = @CUSTOMER_ID  AND POLICY_ID = @POLICY_ID  AND POLICY_VERSION_ID = @POLICY_VERSION_ID           
else  
 SELECT POLICY_ID,POLICY_VERSION_ID,POLICY_LOB,POLICY_SUBLOB,POLICY_NUMBER,POLICY_LOB AS LOB_ID , APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE, datediff( day,APP_EFFECTIVE_DATE, APP_EXPIRATION_DATE) AS POLICY_DAYS ,  
 POLICY_DISP_VERSION,POL_CUSTOMER_POLICY_LIST.IS_ACTIVE ,isnull(IS_REWRITE_POLICY,'N') as IS_REWRITE_POLICY,ISNULL(POLICY_STATUS,'') AS POLICY_STATUS ,ISNULL(APP_STATUS,'') as APP_STATUS,  
 ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME,  
 APP_ID,APP_VERSION_ID,PAYOR,CO_INSURANCE,  
 TRANSACTION_TYPE, --Added By Lalit Nov 08,2010  
 APP_NUMBER  --Added By Lalit Nov 22,2010,
 FROM POL_CUSTOMER_POLICY_LIST  with(nolock)      
 LEFT JOIN CLT_CUSTOMER_LIST CLT   
 ON CLT.CUSTOMER_ID = POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID  
  
 WHERE POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID = @CUSTOMER_ID  AND POL_CUSTOMER_POLICY_LIST.POLICY_ID = @POLICY_ID  AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID = @POLICY_VERSION_ID        
  
         
END    
  
  
  
  
  
  
GO

