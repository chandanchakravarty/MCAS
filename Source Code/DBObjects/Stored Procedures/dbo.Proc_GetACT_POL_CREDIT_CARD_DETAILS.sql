IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetACT_POL_CREDIT_CARD_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetACT_POL_CREDIT_CARD_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetACT_POL_CREDIT_CARD_DETAILS            
Created by      : Swastika Gaur  
Date            : 29 May 2007  
Purpose         : Get Info from Proc_GetACT_POL_CREDIT_CARD_DETAILS  
Revison History :          
Used In         :      
Modifed bY	: Praveen 
Purpose		:For Paypal Data

-----------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          

-- drop proc dbo.Proc_GetACT_POL_CREDIT_CARD_DETAILS  
CREATE PROCEDURE [dbo].[Proc_GetACT_POL_CREDIT_CARD_DETAILS]            
(          
 @CUSTOMER_ID int,        
 @POLICY_ID int,        
 @POLICY_VERSION_ID int        
)          
AS          
Declare @PAY_PAL_REF_ID varchar(200)  
BEGIN          
	SELECT  @PAY_PAL_REF_ID =  ISNULL(PAY_PAL_REF_ID,'') 

	/*CARD_NO, CARD_HOLDER_NAME,CARD_DATE_VALID_FROM,
	CARD_DATE_VALID_TO, 
	CARD_CVV_NUMBER,CARD_TYPE,
	CUSTOMER_FIRST_NAME,
	CUSTOMER_MIDDLE_NAME,
	CUSTOMER_LAST_NAME,
	CUSTOMER_ADDRESS1,
	CUSTOMER_ADDRESS2,
	CUSTOMER_CITY,
	CUSTOMER_COUNTRY,
	CUSTOMER_STATE,
	CUSTOMER_ZIP */

	FROM ACT_POL_CREDIT_CARD_DETAILS  WITH(NOLOCK)
	WHERE  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  

SELECT  ISNULL(@PAY_PAL_REF_ID,'') AS PAY_PAL_REF_ID
       
END     


        
        
        
      
    
  
  
  
  
  
  
  
  
  
  

















GO

