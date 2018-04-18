IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertClaimCoveragesReserveDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertClaimCoveragesReserveDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                                                                    
Proc Name             : Dbo.Proc_InsertClaimCoveragesReserveDetails                                                                    
Created by            : Santosh Kumar Gautam                                                                   
Date                  : 01 Dec 2010                                                                   
Purpose               : To insert the claim reserve details                          
Revison History       :                                                                    
Used In               : claim module                          
------------------------------------------------------------                                                                    
Date     Review By          Comments                                       
                              
drop Proc Proc_InsertClaimCoveragesReserveDetails                                                           
------   ------------       -------------------------*/                                                                    
--                                       
                                        
--                                     
                                  
CREATE PROCEDURE [dbo].[Proc_InsertClaimCoveragesReserveDetails]                                        
           
 @RESERVE_ID int output          
,@ACTIVITY_ID int            
,@RISK_ID int                        
,@CLAIM_ID int          
,@COVERAGE_ID int          
,@OUTSTANDING decimal(18,2)          
,@RI_RESERVE decimal(18,2)          
,@CO_RESERVE decimal(18,2)          
,@DEDUCTIBLE_1 decimal(18,2)  
,@ADJUSTED_AMOUNT decimal(18,2)
,@PERSONAL_INJURY char(1)  
,@VICTIM_ID INT
,@CREATED_BY int          
,@CREATED_DATETIME datetime          
                                                                                       
                                        
AS                                        
BEGIN                             
                          
  DECLARE @TRANSACTION_ID   INT           
              
  SELECT @RESERVE_ID=(ISNULL(MAX([RESERVE_ID]),0)+1),@TRANSACTION_ID= (ISNULL(MAX(TRANSACTION_ID),0)+1)        
  FROM [dbo].[CLM_ACTIVITY_RESERVE]   WITH(NOLOCK)        
  WHERE CLAIM_ID=@CLAIM_ID         
                            
            
    INSERT INTO [dbo].[CLM_ACTIVITY_RESERVE]          
           (          
            [CLAIM_ID]                 
           ,[RESERVE_ID]          
           ,[ACTUAL_RISK_ID]          
           ,[ACTIVITY_ID]          
           ,[COVERAGE_ID]                     
           ,[OUTSTANDING]           
           ,[PREV_OUTSTANDING]                      
           ,[OUTSTANDING_TRAN]             
           ,[TRANSACTION_ID]      
           ,[IS_ACTIVE]          
           ,[CREATED_BY]          
           ,[CREATED_DATETIME] 
           ,[DEDUCTIBLE_1]   
           ,[ADJUSTED_AMOUNT]  
           ,PERSONAL_INJURY   
           ,VICTIM_ID            
                    
          )          
     VALUES          
           (          
            @CLAIM_ID          
           ,@RESERVE_ID          
           ,@RISK_ID          
           ,@ACTIVITY_ID          
           ,@COVERAGE_ID                     
           ,@OUTSTANDING       
           ,0            --[PREV_OUTSTANDING]               
           ,@OUTSTANDING --[OUTSTANDING_TRAN]            
           ,@TRANSACTION_ID              
           ,'Y'          
           ,@CREATED_BY          
           ,@CREATED_DATETIME 
           ,@DEDUCTIBLE_1
           ,@ADJUSTED_AMOUNT 
           ,@PERSONAL_INJURY   
           ,@VICTIM_ID     
           )          
             
                            
END 
GO

