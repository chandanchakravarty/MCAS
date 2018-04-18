IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateLitigationInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateLitigationInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

          
 /*----------------------------------------------------------                                                        
Proc Name             : Dbo.Proc_UpdateLitigationInformation                                                        
Created by            : Santosh Kumar Gautam                                                       
Date                  : 22/11/2010                                                       
Purpose               : To update the litigation information details              
Revison History       :                                                        
Used In               : claim module              
------------------------------------------------------------                                                        
Date     Review By          Comments                           
                  
drop Proc Proc_UpdateLitigationInformation                                               
------   ------------       -------------------------*/                                                        
--                           
                            
--                         
                      
CREATE PROCEDURE [dbo].[Proc_UpdateLitigationInformation]                     
            
 @LITIGATION_ID     int              
,@JUDICIAL_PROCESS_NO nvarchar(20)        
,@JUDICIAL_COMPLAINT_STATE int        
,@PLAINTIFF_NAME nvarchar(40)        
,@PLAINTIFF_CPF nvarchar(40)        
,@PLAINTIFF_REQUESTED_AMOUNT decimal(14,2)        
,@DEFEDANT_OFFERED_AMOUNT decimal(14,2)        
,@ESTIMATE_CLASSIFICATION int        
,@OPERATION_REASON int        
,@JUDICIAL_PROCESS_DATE datetime      
,@EXPERT_SERVICE_ID int -- FOR LAWYER      
,@MODIFIED_BY int              
,@LAST_UPDATED_DATETIME datetime             
          
                                                                                  
                            
AS                            
BEGIN                     
                
   UPDATE [dbo].[CLM_LITIGATION_INFORMATION]        
   SET         
       [JUDICIAL_PROCESS_NO] =@JUDICIAL_PROCESS_NO        
      ,[JUDICIAL_COMPLAINT_STATE] = @JUDICIAL_COMPLAINT_STATE        
      ,[PLAINTIFF_NAME] = @PLAINTIFF_NAME        
      ,[PLAINTIFF_CPF] = @PLAINTIFF_CPF        
      ,[PLAINTIFF_REQUESTED_AMOUNT] = @PLAINTIFF_REQUESTED_AMOUNT        
      ,[DEFEDANT_OFFERED_AMOUNT] = @DEFEDANT_OFFERED_AMOUNT       
      ,[ESTIMATE_CLASSIFICATION] = @ESTIMATE_CLASSIFICATION        
      ,[OPERATION_REASON] = @OPERATION_REASON        
      ,[EXPERT_SERVICE_ID]=@EXPERT_SERVICE_ID      
      ,[JUDICIAL_PROCESS_DATE]=@JUDICIAL_PROCESS_DATE      
      ,[MODIFIED_BY]         =@MODIFIED_BY          
      ,[LAST_UPDATED_DATETIME]       =@LAST_UPDATED_DATETIME          
 WHERE(LITIGATION_ID=@LITIGATION_ID )             
        
                   
                
END 

GO

