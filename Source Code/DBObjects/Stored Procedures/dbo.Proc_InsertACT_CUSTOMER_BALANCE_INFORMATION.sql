IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_CUSTOMER_BALANCE_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_CUSTOMER_BALANCE_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*--------------------------------------------------------------------------          
Proc Name       : dbo.Proc_InsertACT_CUSTOMER_BALANCE_INFORMATION          
Created by      : Mohit Agarwal          
Date            : 9 Jul 2007         
Purpose         : To insert records For Notices in ACT_CUSTOMER_BALANCE_INFORMATION           
Revison History :            
Used In         : Wolverine           
------------------------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------------------------------------*/            
-- drop proc dbo.Proc_InsertACT_CUSTOMER_BALANCE_INFORMATION           
CREATE PROC [dbo].[Proc_InsertACT_CUSTOMER_BALANCE_INFORMATION]           
(            
 @CUSTOMER_ID int,    
 @POLICY_ID int,    
 @POLICY_VERSION_ID int,    
 @OPEN_ITEM_ROW_ID int=null,  
 @AMOUNT decimal(9,2),    
 @AMOUNT_DUE decimal(18,2)=0.0,  
 @TRAN_DESC varchar(250),  
 @UPDATED_FROM varchar(10),  
 @CREATED_DATE DateTime,  
 @IS_PRINTED int=null,  
 @PRINT_DATE DateTime=null,  
 @SOURCE_ROW_ID int=null,  
 @DUE_DATE DateTime=null,  
 @PRINTED_ON_NOTICE varchar(1)=null,  
 @NOTICE_URL varchar(400),
 @NOTICE_TYPE varchar(20) ,
 @MIN_DUE	Decimal(18,2) = null , 
 @TOTAL_PREMIUM_DUE Decimal(18,2) = null
)            
AS            
BEGIN            
  
-- Premium Notice to be generated against active version
IF (  ISNULL(@NOTICE_TYPE , '' ) = 'PREM_NOTICE')
BEGIN 
	SELECT @POLICY_VERSION_ID = MAX(POLICY_VERSION_ID) FROM 
	POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID 
	AND POLICY_STATUS = 'NORMAL'

END  
SELECT @CREATED_DATE = GETDATE()
INSERT INTO ACT_CUSTOMER_BALANCE_INFORMATION            
(            
CUSTOMER_ID,  
POLICY_ID,  
POLICY_VERSION_ID,  
OPEN_ITEM_ROW_ID,  
AMOUNT,  
AMOUNT_DUE,  
TRAN_DESC,  
UPDATED_FROM,  
CREATED_DATE,  
IS_PRINTED,  
PRINT_DATE,  
SOURCE_ROW_ID,  
DUE_DATE,  
PRINTED_ON_NOTICE,  
NOTICE_URL,  
NOTICE_TYPE  , 
MIN_DUE , TOTAL_PREMIUM_DUE
)            
VALUES            
(            
 @CUSTOMER_ID,  
@POLICY_ID,  
@POLICY_VERSION_ID,  
@OPEN_ITEM_ROW_ID,  
@AMOUNT,  
@AMOUNT_DUE,  
@TRAN_DESC,  
@UPDATED_FROM,  
@CREATED_DATE,  
@IS_PRINTED,  
@PRINT_DATE,  
@SOURCE_ROW_ID,  
@DUE_DATE,  
@PRINTED_ON_NOTICE,  
@NOTICE_URL,
@NOTICE_TYPE    ,
@MIN_DUE    , @TOTAL_PREMIUM_DUE
)       

END            
    
    






GO

