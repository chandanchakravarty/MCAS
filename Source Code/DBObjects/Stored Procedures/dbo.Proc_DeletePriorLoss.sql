IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePriorLoss]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePriorLoss]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name    : dbo.Proc_DeletePriorLoss      
Created by   : Gaurav      
Date         : June 2, 2005  
Purpose     : Deletes a record from APP_PRIOR_LOSS_INFO  
Revison History :  
Used In  :   Wolverine         
 ------------------------------------------------------------                  
Date     Review By          Comments                
       
------   ------------       -------------------------*/     
--drop proc Proc_DeletePriorLoss 
CREATE    PROC dbo.Proc_DeletePriorLoss  
(  
 @CUSTOMER_ID Int,  
 @LOSS_ID smallint  
   
   
)  
  
AS  
  
  
  
BEGIN  
   
 

 IF EXISTS(SELECT * FROM  PRIOR_LOSS_HOME WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOSS_ID=@LOSS_ID)
	BEGIN   
		DELETE FROM  PRIOR_LOSS_HOME WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOSS_ID=@LOSS_ID
	END
DELETE FROM APP_PRIOR_LOSS_INFO  
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND  
   LOSS_ID = @LOSS_ID  
  
 IF @@ERROR <> 0  
 BEGIN  
  RETURN -1  
 END  
  
 RETURN 1  
END  





GO

