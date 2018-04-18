IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckCreditBalanceEligibility]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckCreditBalanceEligibility]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                  
Proc Name        : dbo.Proc_CheckCreditBalanceEligibility                  
Created by       : Manoj Rathore          
Date             : 26 Aug 2009        
Purpose          : Check that policy has credit balance from prior term or not before launching the policy.          
Revison History  :                  
Used In          :   Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments         
DROP PROC dbo.Proc_CheckCreditBalanceEligibility           
------   ------------       -------------------------*/                  
CREATE PROC dbo.Proc_CheckCreditBalanceEligibility                  
(                  
 @CUSTOMER_ID     int,                  
 @POLICY_ID     int,                  
 @POLICY_VERSION_ID     smallint, 
 @RETVAL int OUTPUT          
)                  
AS                  
                  
BEGIN                  
 
   DECLARE @CURRENT_TERM smallint
   SELECT @CURRENT_TERM=CURRENT_TERM FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID 
   AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
  
   DECLARE @BALANCE_PRIORTERM DECIMAL
   SELECT @BALANCE_PRIORTERM=SUM(ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID , 0) )    
   FROM ACT_CUSTOMER_OPEN_ITEMS OI  INNER JOIN POL_CUSTOMER_POLICY_LIST CPL   
   ON OI.CUSTOMER_ID = CPL.CUSTOMER_ID   AND OI.POLICY_ID = CPL.POLICY_ID   
   AND OI.POLICY_VERSION_ID = CPL.POLICY_VERSION_ID   
   WHERE  
	(  
  		OI.UPDATED_FROM IN ('C','D','F','J')  
  	OR (  
    		OI.UPDATED_FROM IN ('P') AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'  
	   )  
 	 ) AND 
    CPL.CUSTOMER_ID=@CUSTOMER_ID AND CPL.POLICY_ID=@POLICY_ID AND 
    CPL.CURRENT_TERM= @CURRENT_TERM - 1
      

  IF (ISNULL(@BALANCE_PRIORTERM,0) >=0) 
  BEGIN         
  	SET @RETVAL = 1 	
  END	      
  ELSE          
  BEGIN       
       SET @RETVAL=2         
  END
       
 RETURN @RETVAL            
END        


GO

