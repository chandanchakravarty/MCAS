IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePriorPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePriorPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name    : dbo.Proc_DeletePriorPolicy      
Created by   : Gaurav      
Date         : June 2, 2005  
Purpose     : Deletes a record from APP_PRIOR_CARRIER_INFO  
Revison History :  
Used In  :   Wolverine         
 ------------------------------------------------------------                  
Date     Review By          Comments                
       
------   ------------       -------------------------*/ 
--drop proc Proc_DeletePriorPolicy     
CREATE    PROC dbo.Proc_DeletePriorPolicy  
(  
 @CUSTOMER_ID Int,  
 @APP_PRIOR_CARRIER_INFO_ID smallint  
   
   
)  
  
AS  
  
  
  
BEGIN  
   
 
  
  DELETE FROM APP_PRIOR_CARRIER_INFO  
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND  
   APP_PRIOR_CARRIER_INFO_ID = @APP_PRIOR_CARRIER_INFO_ID  

   
    
 IF @@ERROR <> 0  
 BEGIN  
  RETURN -1  
 END  
  
 RETURN 1  
END  



GO

