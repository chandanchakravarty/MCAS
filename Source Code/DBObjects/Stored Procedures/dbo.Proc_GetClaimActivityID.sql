IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimActivityID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimActivityID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       :  dbo.Proc_GetClaimActivityID          
Created by      :  Sumit Chhabra        
Date            :  6/16/2006          
Purpose         :  To find activity ID corresponding to claim id        
Revison History :          
Used In         :   Wolverine          
-------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE proc dbo.Proc_GetClaimActivityID          
@CLAIM_ID int          
as          
begin          
 declare @ACTIVITY_ID int        
 declare @NEW_RESERVE int       
 declare @INCOMPLETE int     
 declare @RESERVE_UPDATE int  
  
 set @ACTIVITY_ID = 0        
 set @NEW_RESERVE =11836      
 set @INCOMPLETE=11800    
 set @RESERVE_UPDATE =11773  
  
/*  
  11773 Reserve Update  
 11774 Expense Payment   
 11775 Claim Payment   
 11776 Recovery   
 11777 Reinsurance   
 11805 First Notification   
 11836 New Reserve   
*/    
--if no new reserve activity exists, do add it    
 SELECT @ACTIVITY_ID=ACTIVITY_ID FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_REASON=@NEW_RESERVE
      
 return  @ACTIVITY_ID     
      
end   
  



GO

