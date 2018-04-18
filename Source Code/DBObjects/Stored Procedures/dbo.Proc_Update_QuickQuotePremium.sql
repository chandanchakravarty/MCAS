 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_QuickQuotePremium]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_QuickQuotePremium]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_Update_QuickQuotePremium]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In         : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   



create proc dbo.Proc_Update_QuickQuotePremium  
(  
 @CUSTOMER_ID int,  
 @POLICY_ID int,  
 @POLICY_VERSION_ID int,   
 @PREMIUM decimal(18,2) = null  
)  
  
As  
  
if exists(select * from POL_CUSTOMER_POLICY_LIST where POLICY_ID = @POLICY_ID  
and POLICY_VERSION_ID = @POLICY_VERSION_ID and CUSTOMER_ID = @CUSTOMER_ID)  
Begin  
  
UPDATE POL_CUSTOMER_POLICY_LIST   
SET RECEIVED_PRMIUM = @PREMIUM  
where POLICY_ID = @POLICY_ID  
and POLICY_VERSION_ID = @POLICY_VERSION_ID   
and CUSTOMER_ID = @CUSTOMER_ID  
End  
  