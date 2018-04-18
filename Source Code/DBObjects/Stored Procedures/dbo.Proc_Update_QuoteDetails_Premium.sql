 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_QuoteDetails_Premium]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_QuoteDetails_Premium]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_Update_QuoteDetails_Premium]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In         : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   




CREATE PROC dbo.Proc_Update_QuoteDetails_Premium  
(  
 @CUSTOMER_ID int,  
 @POLICY_ID int,  
 @POLICY_VERSION_ID int,  
 @QUOTE_ID int,  
 @BASE_PREMIUM varchar(15),---decimal(18,2),  
 @DEMERIT_DISC_AMT varchar(15),  
 @GST_AMOUNT varchar(15),  
 @FINAL_PREMIUM varchar(15)  
)  
	
As  
  
Begin  
  
if exists(select * from QQ_MOTOR_QUOTE_DETAILS   
where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID   
and POLICY_VERSION_ID = @POLICY_VERSION_ID)-- and QUOTE_ID = @QUOTE_ID)  
Begin  
  
UPDATE QQ_MOTOR_QUOTE_DETAILS  
set BASE_PREMIUM =  @BASE_PREMIUM,  
DEMERIT_DISC_AMT = @DEMERIT_DISC_AMT,  
GST_AMOUNT = @GST_AMOUNT,  
FINAL_PREMIUM = @FINAL_PREMIUM  
where CUSTOMER_ID = @CUSTOMER_ID   
and POLICY_ID = @POLICY_ID   
and POLICY_VERSION_ID = @POLICY_VERSION_ID   
--and QUOTE_ID = @QUOTE_ID  
  
  
end  
  
  
End