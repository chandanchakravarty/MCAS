IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXOLInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXOLInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                
Proc Name             : Dbo.Proc_GetXOLInformation                                                
Created by            : Santosh Kumar Gautam                                               
Date                  : 16 March 2011                                           
Purpose               : To get XOL information details      
Revison History       :                                                
Used In               : Maintenance module      
------------------------------------------------------------                                                
Date     Review By          Comments                   
          
drop Proc Proc_GetXOLInformation                                       
------   ------------       -------------------------*/        
      
CREATE PROC [dbo].[Proc_GetXOLInformation]        
      
@XOL_ID   int        
      
AS                                                                                  
BEGIN           
        
        
        
   SELECT 
		XOL_ID
		,LOB_ID
		,RECOVERY_BASE
		,LOSS_DEDUCTION
		,AGGREGATE_LIMIT
		,MIN_DEPOSIT_PREMIUM
		,FLAT_ADJ_RATE
		,REINSTATE_PREMIUM_RATE
		,REINSTATE_NUMBER
		,PREMIUM_DISCOUNT
		,IS_ACTIVE
		,CREATED_BY
		,CREATED_DATETIME
		,MODIFIED_BY
		,LAST_UPDATED_DATETIME 
		,USED_AGGREGATE_LIMIT
		,MIN_CLAIM_LIMIT		
   FROM MNT_XOL_INFORMATION
   WHERE XOL_ID=@XOL_ID
        
        
   
        
END        
      
      
      

GO

