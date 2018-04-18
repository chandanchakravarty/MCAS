IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_IncompleteActivityCLM_ACTIVITY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_IncompleteActivityCLM_ACTIVITY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name       : dbo.Proc_IncompleteActivityCLM_ACTIVITY          
Created by      : Sumit Chhabra          
Date            : 06/19/2006          
Purpose       : Check for the existence of any activity that is still not complete or authorised    
Revison History :          
Used In   : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
DROP PROC Dbo.Proc_IncompleteActivityCLM_ACTIVITY          
------   ------------       -------------------------*/          
CREATE PROC Dbo.Proc_IncompleteActivityCLM_ACTIVITY          
(          
@CLAIM_ID int  ,
@ACTIVITY_ID int=null  
)          
AS          
BEGIN          
declare @ACTIVITY_STATUS_INCOMPLETE int    
declare @ACTIVITY_STATUS_AWAITING_AUTHORIZATION int    
declare @RETURN_VALUE int    
declare @ACTIVITY_STATUS_COMPLETE int 
   
set @RETURN_VALUE = 0    
set @ACTIVITY_STATUS_INCOMPLETE = 11800    
set @ACTIVITY_STATUS_AWAITING_AUTHORIZATION = 11803    
set @ACTIVITY_STATUS_COMPLETE = 11801

if(@ACTIVITY_ID is null)
begin
if exists(    
      SELECT ACTIVITY_ID FROM CLM_ACTIVITY WHERE     
	  CLAIM_ID=@CLAIM_ID AND     
		(ACTIVITY_STATUS = @ACTIVITY_STATUS_INCOMPLETE OR     
		ACTIVITY_STATUS = @ACTIVITY_STATUS_AWAITING_AUTHORIZATION
		)
      )    
 set @RETURN_VALUE = 1    
    
end
 --Condition added For Itrack Issue #5926 
else
begin
if exists(    
			 SELECT ACTIVITY_ID FROM  CLM_ACTIVITY WHERE     
			  CLAIM_ID=@CLAIM_ID AND     
				(ACTIVITY_STATUS = @ACTIVITY_STATUS_INCOMPLETE OR     
				ACTIVITY_STATUS = @ACTIVITY_STATUS_AWAITING_AUTHORIZATION
				)
			   AND ACTIVITY_ID < ISNULL(@ACTIVITY_ID,'-1'))

		begin  
			set @RETURN_VALUE = 1    
		end
		else if exists( SELECT ACTIVITY_ID FROM  CLM_ACTIVITY WHERE     
			  CLAIM_ID=@CLAIM_ID AND     
				(ACTIVITY_STATUS = @ACTIVITY_STATUS_COMPLETE)
			   AND ACTIVITY_ID > ISNULL(@ACTIVITY_ID,'-1'))
			begin
				set @RETURN_VALUE = 2   
			end

end

return @RETURN_VALUE    
END







GO

