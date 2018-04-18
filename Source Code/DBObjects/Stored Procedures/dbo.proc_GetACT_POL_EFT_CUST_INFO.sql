IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_GetACT_POL_EFT_CUST_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_GetACT_POL_EFT_CUST_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name       : dbo.proc_GetACT_POL_EFT_CUST_INFO          
Created by      : Praveen kasana   
Date            : 17-jan-2006  
Purpose         : Get Info from ACT_POL_EFT_CUST_INFO 
Revison History :          
Used In         :              

-----------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          

-- drop proc proc_GetACT_POL_EFT_CUST_INFO  1151,112,1         
create PROCEDURE dbo.proc_GetACT_POL_EFT_CUST_INFO          
(          
 @CUSTOMER_ID int,        
 @POLICY_ID int,        
 @POLICY_VERSION_ID int        
)          
AS          
BEGIN          


SELECT  
FEDERAL_ID,DFI_ACC_NO,TRANSIT_ROUTING_NO,
ACCOUNT_TYPE,EFT_TENTATIVE_DATE,

CASE WHEN CONVERT(VARCHAR,IS_VERIFIED) = '10964' THEN 'No'
 WHEN CONVERT(VARCHAR(10),IS_VERIFIED) = '10963' THEN 'Yes' ELSE 
'No' END AS IS_VERIFIED,
CONVERT(VARCHAR,VERIFIED_DATE,110) AS VERIFIED_DATE,
REVERIFIED_AC


FROM ACT_POL_EFT_CUST_INFO  
WHERE  CUSTOMER_ID = @CUSTOMER_ID AND        
  POLICY_ID = @POLICY_ID AND        
  POLICY_VERSION_ID = @POLICY_VERSION_ID      
  
END     

 
 
  
  
  
  
  
  
  
  









GO

