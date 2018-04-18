IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckDoNotRenew]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckDoNotRenew]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_CheckDoNotRenew        
Created by      : Pravesh k Chandel       
Date            : 15 Jan 2007      
Purpose        : to Check Policy for do not renew
Revison History :            
Used In         : Wolverine     
DROP PROC Proc_CheckDoNotRenew
*/
create proc dbo.Proc_CheckDoNotRenew
(
@CUSTOMER_ID INT,        
@POLICY_ID INT,        
@POLICY_VERSION_ID SMALLINT, 
 @RETVAL int OUTPUT     
)
as
begin

DECLARE @DONOT_RENEW CHAR  
  
SELECT @DONOT_RENEW=NOT_RENEW FROM POL_CUSTOMER_POLICY_LIST with(nolock) 
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
  
 IF (@DONOT_RENEW ='Y')  
    BEGIN    
    set @RETVAL=1  
    RETURN 1 --NOT RENEW  
   End   
ELSE
 set @RETVAL=2  
      
  
 RETURN @RETVAL          

end




GO

