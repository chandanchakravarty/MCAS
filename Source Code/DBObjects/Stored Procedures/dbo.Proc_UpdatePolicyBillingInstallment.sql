IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyBillingInstallment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyBillingInstallment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
      
----------------------------------------------------------                          
Proc Name       : dbo.Proc_UpdatePolicyBillingInstallment                
Created by      : LALIT CHAUHAN              
Date            : 05/26/2010                          
Purpose         : Update Policy Billing Installment on basis of installment No.                          
Revison History :                          
Used In        : Ebix Advantage                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------      
  
Drop proc Proc_UpdatePolicyBillingInstallment   
Proc_UpdatePolicyBillingInstallment 2126,5,1,1,'05/21/2010 12:00:00 AM',10,1,1,1  

*/  
  
Create Proc [dbo].[Proc_UpdatePolicyBillingInstallment]   
(  
 @CUSTOMER_ID   INT,                     -- ID OF CUSTOMER WHOSE  POLICY PREMIUM BILL INSTALLMENT WILL BE POSTED           
 @POLICY_ID  INT,                        -- POLICY IDENTIFICATION NUMBER          
 @POLICY_VERSION_ID SMALLINT,            -- POLICY VERSION IDENTIFICATION NUMBER  
 @INSTALLMENT_NO     INT,                -- BILLING INSTALLMENT NO  
 @INSTALLMENT_EFFECTIVE_DATE DATETIME,  
 @INSTALLMENT_AMOUNT  DECIMAL(25,2),   
 @INTEREST_AMOUNT     DECIMAL(25,2),    
 @FEE                 DECIMAL(25,2),  
 @TAXES               DECIMAL(25,2),  
 @TRAN_PREMIUM_AMOUNT decimal(25,2) ,   -- Tran premium Amount
 @TRAN_INTEREST_AMOUNT decimal(25,2) ,  -- Tran Interest Amount
 @TRAN_FEE decimal(25,2) ,              -- tran fee 
 @TRAN_TAXES decimal(25,2) ,            -- tran taxes 
 @TRAN_TOTAL decimal(25,2) ,            -- Tran total   
 @MODIFIED_BY         INT,  
 @LAST_UPDATED_DATETIME DATETIME    ,
 @TRAN_TYPE NVARCHAR(6),
 @TOTAL_TRAN_PREMIUM DECIMAL(25,2),
 @TOTAL_PREMIUM   DECIMAL(25,2),
 @ROW_ID INT
)  
AS   
BEGIN  
 DECLARE @INSTALLMENT_PERCENTAGE NUMERIC(12,4),  
 @TOTAL_INSTALLMENT_AMOUNT  DECIMAL(25,2),  
 @TOTAL_BILLING_AMOUNT decimal(25,2)  
     
	  -- IF EXISTS (SELECT INSTALLMENT_NO FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID   
		 --AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND INSTALLMENT_NO=@INSTALLMENT_NO )   
	      
			--BEGIN  
				      
			--	   SELECT @TOTAL_BILLING_AMOUNT= TOTAL_AMOUNT FROM ACT_POLICY_INSTALL_PLAN_DATA WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID   
			--		  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
			--			IF(@@ERROR<>0)  
			--				RETURN  -1  
				       
			--		SELECT  @TOTAL_INSTALLMENT_AMOUNT =   @INSTALLMENT_AMOUNT + @INTEREST_AMOUNT + @FEE + @TAXES       
			--			 IF(@@ERROR<>0)  
			--				 RETURN  -1   
				          
			--		SELECT @INSTALLMENT_PERCENTAGE = ROUND((@TOTAL_INSTALLMENT_AMOUNT/@TOTAL_BILLING_AMOUNT)*100,4)  
			--			 IF(@@ERROR<>0)  
			--				RETURN  -1   
				      
			--		SELECT  @TOTAL_INSTALLMENT_AMOUNT = ROUND(@TOTAL_INSTALLMENT_AMOUNT,2)  
			--			IF(@@ERROR<>0)  
			--				RETURN  -1   
			
					 
			         SELECT @TOTAL_INSTALLMENT_AMOUNT = @INSTALLMENT_AMOUNT + @INTEREST_AMOUNT + @FEE + @TAXES  ;
			
				     IF(@TRAN_TYPE='END')
						 BEGIN
							   SELECT @TRAN_PREMIUM_AMOUNT = @INSTALLMENT_AMOUNT,
							   @TRAN_INTEREST_AMOUNT = @INTEREST_AMOUNT,
							   @TRAN_FEE = @FEE,
							   @TRAN_TAXES = @TAXES,
							   @TRAN_TOTAL = @TOTAL_INSTALLMENT_AMOUNT
							   SELECT @INSTALLMENT_PERCENTAGE =0--ROUND((@INSTALLMENT_AMOUNT*100)/@TOTAL_TRAN_PREMIUM,4);
						 END
				     ELSE
				     	 SELECT @INSTALLMENT_PERCENTAGE =0--ROUND((@INSTALLMENT_AMOUNT * 100)/ @TOTAL_PREMIUM,4);
				     
				     
					   UPDATE ACT_POLICY_INSTALLMENT_DETAILS SET  
					   INSTALLMENT_EFFECTIVE_DATE=@INSTALLMENT_EFFECTIVE_DATE,  
					   INSTALLMENT_AMOUNT=@INSTALLMENT_AMOUNT,  
					   INTEREST_AMOUNT=@INTEREST_AMOUNT,  
					   FEE=@FEE,  
					   TAXES=@TAXES,  
					   TOTAL=@TOTAL_INSTALLMENT_AMOUNT,  
					   PERCENTAG_OF_PREMIUM=@INSTALLMENT_PERCENTAGE,
					   TRAN_PREMIUM_AMOUNT = @TRAN_PREMIUM_AMOUNT,
					   TRAN_INTEREST_AMOUNT = @TRAN_INTEREST_AMOUNT,
					   TRAN_FEE = @TRAN_FEE,
					   TRAN_TAXES = @TRAN_TAXES,
					   TRAN_TOTAL = @TRAN_TOTAL,
					   			   
					   MODIFIED_BY=@MODIFIED_BY,  
					   LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME  
					     
						WHERE CUSTOMER_ID=@CUSTOMER_ID   
							AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND INSTALLMENT_NO=@INSTALLMENT_NO  
							AND ROW_ID = @ROW_ID
							
					  RETURN  1  
					  
					  IF(@@ERROR<>0)  
							RETURN  -1
		    
			--END  
		--ELSE  
		-- RETURN  -1    -----------If Value Not Exist For Update  
	    
	   
END
GO

