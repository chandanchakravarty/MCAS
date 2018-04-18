IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertXOLInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertXOLInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------                                                            
Proc Name             : Dbo.Proc_InsertXOLInformation                                                            
Created by            : Santosh Kumar Gautam                                                           
Date                  : 16 March 2011                                                           
Purpose               : To insert the XOL information details                  
Revison History       :                                                            
Used In               : Maintenance module                  
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc Proc_InsertXOLInformation                                                   
------   ------------       -------------------------*/                                                            
--                               
                                
--                             
                          
CREATE PROCEDURE [dbo].[Proc_InsertXOLInformation]                                
                 
 @XOL_ID				 int   output
,@LOB_ID				 int 
,@CONTRACT_ID			 int
,@RECOVERY_BASE			 int 
,@LOSS_DEDUCTION		 decimal(18, 2) 
,@AGGREGATE_LIMIT		 decimal(18, 2) 
,@MIN_DEPOSIT_PREMIUM    decimal(18, 2) 
,@FLAT_ADJ_RATE          decimal( 7, 4) 
,@REINSTATE_PREMIUM_RATE decimal( 7, 4) 
,@REINSTATE_NUMBER		 int 
,@PREMIUM_DISCOUNT		 decimal( 7, 4) 
,@MIN_CLAIM_LIMIT        int
,@CREATED_BY			 int            
,@CREATED_DATETIME       datetime            
            

                                
AS                                
BEGIN                     
                    
                    
  SELECT @XOL_ID=(ISNULL(MAX([XOL_ID]),0)+1)  FROM [dbo].[MNT_XOL_INFORMATION]            
              
 INSERT INTO [dbo].[MNT_XOL_INFORMATION]            
           (
             XOL_ID				     
			,LOB_ID		
			,CONTRACT_ID		 
			,RECOVERY_BASE			 
			,LOSS_DEDUCTION		 
			,AGGREGATE_LIMIT		 
			,MIN_DEPOSIT_PREMIUM    
			,FLAT_ADJ_RATE          
			,REINSTATE_PREMIUM_RATE 
			,REINSTATE_NUMBER		 
			,PREMIUM_DISCOUNT	
			,MIN_CLAIM_LIMIT
			,IS_ACTIVE
			,CREATED_BY
			,CREATED_DATETIME	
           )            
     VALUES            
           (            
            @XOL_ID				
           ,@LOB_ID	
           ,@CONTRACT_ID			
           ,@RECOVERY_BASE			     
           ,@LOSS_DEDUCTION		          
           ,@AGGREGATE_LIMIT		
           ,@MIN_DEPOSIT_PREMIUM   
           ,@FLAT_ADJ_RATE                     
           ,@REINSTATE_PREMIUM_RATE         
           ,@REINSTATE_NUMBER		         
           ,@PREMIUM_DISCOUNT
           ,@MIN_CLAIM_LIMIT		 
           ,'Y'            
           ,@CREATED_BY            
           ,@CREATED_DATETIME            
           )            
            
                    
END 




GO

