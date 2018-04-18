IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GET_TEMP_CHECK_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GET_TEMP_CHECK_DATA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- drop proc dbo.PROC_GET_TEMP_CHECK_DATA        
CREATE PROCEDURE [dbo].[PROC_GET_TEMP_CHECK_DATA]          
(          
 @LOGGED_IN_USER_ID INT,      
 @CHECK_TYPE INT        
)          
AS          
BEGIN       
DECLARE @YES Int,      
 @No Int,      
 @EFT_MODE Int,      
 @CHECK_MODE Int    
 --@DistAmt decimal(18,2)     
SET @YES = 10963      
SET @NO  = 10964      
SET @EFT_MODE = 11976      
SET @CHECK_MODE = 11787 -- 11975       
--     
-- select @DistAmt = sum(DISTRIBUTION_AMOUNT)from TEMP_ACT_DISTRIBUTION_DETAILS act    
-- inner join TEMP_ACT_CHECK_INFORMATION tmp on  act.GROUP_ID=tmp.CHECK_ID group by GROUP_TYPE,GROUP_ID    
    
    
SELECT           
CHECK_ID,          
CHECK_TYPE,          
ACCOUNT_ID,          
CHECK_DATE,          
CHECK_AMOUNT,          
CHECK_NOTE,          
PAYEE_ENTITY_ID,          
PAYEE_ENTITY_TYPE,          
PAYEE_ENTITY_NAME,          
PAYEE_ADD1,          
PAYEE_ADD2,          
PAYEE_CITY,          
PAYEE_STATE,          
PAYEE_ZIP,          
PAYEE_NOTE,          
CUSTOMER_ID,          
POLICY_ID,          
POLICY_VER_TRACKING_ID,          
tmp.CREATED_BY,    
CASE ISNULL(tmp.PAYMENT_MODE,0)    
When 0 THEN      
  CASE ISNULL(VENDOR.ALLOWS_EFT,@NO)       
   WHEN @YES THEN @EFT_MODE      
   ELSE @CHECK_MODE END       
 ELSE tmp.PAYMENT_MODE END      
 AS PAYMENT_MODE ,      
 /*CASE ISNULL(VENDOR.ALLOWS_EFT,@NO)       
  WHEN @YES THEN 'YES'      
 ELSE 'NO' END  AS ALLOW_EFT*/    
CASE     
WHEN ISNULL(VENDOR.ACCOUNT_ISVERIFIED,@NO) = @YES     
AND  ISNULL(VENDOR.ALLOWS_EFT,@NO)  = @YES    
THEN 'YES'     
ELSE 'NO' END    
AS ALLOW_EFT  ,    
           
(select sum(DISTRIBUTION_AMOUNT)from TEMP_ACT_DISTRIBUTION_DETAILS where GROUP_ID=tmp.CHECK_ID group by GROUP_TYPE,GROUP_ID) as DistributedAmount          
,    
case    
when    
(select sum(DISTRIBUTION_AMOUNT)from TEMP_ACT_DISTRIBUTION_DETAILS where GROUP_ID=tmp.CHECK_ID group by GROUP_TYPE,GROUP_ID) < check_amount    
 then 'Partially Distributed'    
when  (select sum(DISTRIBUTION_AMOUNT)from TEMP_ACT_DISTRIBUTION_DETAILS where GROUP_ID=tmp.CHECK_ID group by GROUP_TYPE,GROUP_ID) = check_amount then 'Fully Distributed'    
else 'Not Distributed'    
end as 'DISTRIBUTION_STATUS'  
  
           
FROM TEMP_ACT_CHECK_INFORMATION tmp (NOLOCK) LEFT JOIN MNT_VENDOR_LIST VENDOR on  VENDOR.VENDOR_ID = tmp.PAYEE_ENTITY_ID    
WHERE TMP.CREATED_BY=@LOGGED_IN_USER_ID           
AND CHECK_TYPE = @CHECK_TYPE    
END          
       
    
    
    
    
    
    
    
    
GO

