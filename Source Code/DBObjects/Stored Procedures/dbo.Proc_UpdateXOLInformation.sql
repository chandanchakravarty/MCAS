IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateXOLInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateXOLInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

          
 /*----------------------------------------------------------                                                        
Proc Name             : Dbo.Proc_UpdateXOLInformation                                                        
Created by            : Santosh Kumar Gautam                                                       
Date                  : 16 March 2011                                                  
Purpose               : To update the XOL information details              
Revison History       :                                                        
Used In               : Maintenance module              
------------------------------------------------------------                                                        
Date     Review By          Comments                           
                  
drop Proc Proc_UpdateXOLInformation                                               
------   ------------       -------------------------*/                                                        
--                           
                            
--                         
                      
CREATE PROCEDURE [dbo].[Proc_UpdateXOLInformation]                     
            
 @XOL_ID				 int   
,@LOB_ID				 int 
,@RECOVERY_BASE			 int 
,@LOSS_DEDUCTION		 decimal(18, 2) 
,@AGGREGATE_LIMIT		 decimal(18, 2) 
,@MIN_DEPOSIT_PREMIUM    decimal(18, 2) 
,@FLAT_ADJ_RATE          decimal( 7, 4) 
,@REINSTATE_PREMIUM_RATE decimal( 7, 4) 
,@REINSTATE_NUMBER		 int 
,@PREMIUM_DISCOUNT		 decimal( 7, 4)   
,@MIN_CLAIM_LIMIT        int   
,@MODIFIED_BY			 int              
,@LAST_UPDATED_DATETIME  datetime             
          
                                                                                  
                            
AS                            
BEGIN                     
                
   UPDATE [dbo].[MNT_XOL_INFORMATION]        
   SET         
      LOB_ID				    = @LOB_ID				 		 
     ,RECOVERY_BASE			    = @RECOVERY_BASE			 
     ,LOSS_DEDUCTION			= @LOSS_DEDUCTION		 
     ,AGGREGATE_LIMIT			= @AGGREGATE_LIMIT		 
     ,MIN_DEPOSIT_PREMIUM       = @MIN_DEPOSIT_PREMIUM     
     ,FLAT_ADJ_RATE				= @FLAT_ADJ_RATE          
     ,REINSTATE_PREMIUM_RATE	= @REINSTATE_PREMIUM_RATE 
     ,REINSTATE_NUMBER			= @REINSTATE_NUMBER		 
     ,PREMIUM_DISCOUNT			= @PREMIUM_DISCOUNT	
     ,MIN_CLAIM_LIMIT			= @MIN_CLAIM_LIMIT	 
     ,[MODIFIED_BY]             = @MODIFIED_BY          
     ,[LAST_UPDATED_DATETIME]   = @LAST_UPDATED_DATETIME          
 WHERE(XOL_ID=@XOL_ID AND IS_ACTIVE='Y')             
        
                   
                
END 
GO

