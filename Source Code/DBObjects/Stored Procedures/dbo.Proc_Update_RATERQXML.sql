 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_RATERQXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_RATERQXML]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_Update_RATERQXML]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In         : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   




create proc dbo.Proc_Update_RATERQXML  
(  
 @CUSTOMER_ID int,  
 @POLICY_ID int,  
 @POLICY_VERSION_ID int,  
 @REQUEST_XML varchar(MAX) = null,  
 @RESPONSE_XML varchar(MAX)= null,  
 @QUOTE_ID int = null  
)  
  
As  
  
IF (ISNULL(@REQUEST_XML,'') != '')  
Begin  
if exists(select * from POL_CUSTOMER_POLICY_LIST where POLICY_ID = @POLICY_ID  
and POLICY_VERSION_ID = @POLICY_VERSION_ID and CUSTOMER_ID = @CUSTOMER_ID)  
Begin  
  
UPDATE POL_CUSTOMER_POLICY_LIST   
SET RULE_INPUT_XML = @REQUEST_XML  
where POLICY_ID = @POLICY_ID  
and POLICY_VERSION_ID = @POLICY_VERSION_ID   
and CUSTOMER_ID = @CUSTOMER_ID  
End  
  
End  
  
IF (ISNULL(@RESPONSE_XML,'') != '')  
Begin  
if exists(select * from POL_CUSTOMER_POLICY_LIST where POLICY_ID = @POLICY_ID  
and POLICY_VERSION_ID = @POLICY_VERSION_ID and CUSTOMER_ID = @CUSTOMER_ID)  
Begin  
  
UPDATE POL_CUSTOMER_POLICY_LIST   
SET POLICY_PREMIUM_XML = @RESPONSE_XML  
where POLICY_ID = @POLICY_ID  
and POLICY_VERSION_ID = @POLICY_VERSION_ID   
and CUSTOMER_ID = @CUSTOMER_ID  
  
End  
  
End