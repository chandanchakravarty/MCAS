    
 /*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_Insert_POL_BILLING_INFO]              
Created by      : SNEHA          
Date            : 16/11/2011                      
Purpose         :INSERT RECORDS IN POL_BILLING_INFO TABLE.                      
Revison History :                      
Used In        : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC dbo.[Proc_Insert_POL_BILLING_INFO]        
      
*/  

CREATE PROC  [dbo].[Proc_Insert_POL_BILLING_INFO] 
(
@BILLING_ID					[INT] OUT,
@CUSTOMER_ID				[NVARCHAR](10) ,
@POLICY_ID					[NVARCHAR](10) ,
@POLICY_VERSION_ID			[NVARCHAR](10) ,
@LOB_ID						[NVARCHAR](10) ,
@BILLING_TYPE				[NVARCHAR](10) ,
@BILLING_PLAN				[NVARCHAR](10) ,
@DOWN_PAYMENT_MODE			[NVARCHAR](10) ,
@PROXY_SIGN_OBTAIN			[NVARCHAR](10) ,
@UNDERWRITER				[NVARCHAR](50) ,
@ROLLOVER					[NVARCHAR](10) ,
@RECIVED_PREMIUM			[NVARCHAR](20) ,
@COMP_APP_BONUS_APPLIES		[nchar](2) ,
@CURRENT_RESIDENCE			[NVARCHAR](50) ,
@IS_ACTIVE					[nchar](1) ,
@CREATED_BY					INT,        
@CREATED_DATETIME			DATETIME    
) 
AS
BEGIN

SELECT @BILLING_ID =ISNULL(MAX(BILLING_ID),0)+1 FROM POL_BILLING_INFO

INSERT INTO POL_BILLING_INFO

(
BILLING_ID		,
CUSTOMER_ID		,
POLICY_ID		,	
POLICY_VERSION_ID	,
LOB_ID		,		
BILLING_TYPE	,			
BILLING_PLAN	,			
DOWN_PAYMENT_MODE,			
PROXY_SIGN_OBTAIN,			
UNDERWRITER	,			
ROLLOVER		,			
RECIVED_PREMIUM,			
COMP_APP_BONUS_APPLIES		,
CURRENT_RESIDENCE			,
IS_ACTIVE,
CREATED_BY,        
CREATED_DATETIME 					
) 
VALUES
(
@BILLING_ID		,
@CUSTOMER_ID		,
@POLICY_ID		,	
@POLICY_VERSION_ID	,
@LOB_ID	,			
@BILLING_TYPE	,			
@BILLING_PLAN	,			
@DOWN_PAYMENT_MODE,			
@PROXY_SIGN_OBTAIN,			
@UNDERWRITER	,			
@ROLLOVER		,			
@RECIVED_PREMIUM,			
@COMP_APP_BONUS_APPLIES		,
@CURRENT_RESIDENCE			,
@IS_ACTIVE					,
@CREATED_BY,        
@CREATED_DATETIME        
     
)
END