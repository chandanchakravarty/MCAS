IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteVendorCheckDistribution]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteVendorCheckDistribution]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_InsertVendorCheckDistribution    
Created by      : Praveen Kasana  
Date            : 22-06-2007  
Purpose        :  
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--DROP PROC dbo.Proc_DeleteVendorCheckDistribution  
CREATE PROC dbo.Proc_DeleteVendorCheckDistribution  
(    
 @CHECK_ID INT =NULL,  
 @IDEN_ROW_ID Int  
   
)    
AS    
BEGIN    
DECLARE @AMOUNT DECIMAL(18,2)  
  
 DELETE FROM ACT_VENDOR_CHECK_DISTRIBUTION   
 WHERE IDEN_ROW_ID = @IDEN_ROW_ID and check_id =@CHECK_ID 

 IF EXISTS(SELECT CHECK_ID FROM TEMP_ACT_CHECK_INFORMATION WHERE CHECK_ID = @CHECK_ID)  
 BEGIN  
  SELECT @AMOUNT = ISNULL(SUM(ISNULL(AMOUNT_TO_APPLY,0)),0) FROM ACT_VENDOR_CHECK_DISTRIBUTION  
     WHERE CHECK_ID = @CHECK_ID --  and IDEN_ROW_ID = @IDEN_ROW_ID
      
  UPDATE TEMP_ACT_CHECK_INFORMATION  
  SET CHECK_AMOUNT = @AMOUNT  
  WHERE CHECK_ID = @CHECK_ID  
 END  
  
   
END  
  
  
  
  
  







GO

