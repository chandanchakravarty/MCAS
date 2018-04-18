IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_DELETE_PREVIOUS_IMPORT_REQUEST_RECORDS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_DELETE_PREVIOUS_IMPORT_REQUEST_RECORDS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------                                                        
Proc Name             : Dbo.PROC_MIG_DELETE_PREVIOUS_IMPORT_REQUEST_RECORDS                                                
Created by            : Santosh Kumar Gautam                                                       
Date                  : 09 May 2011                                                      
Purpose               : TO DELETE PREVIOUS RECORDS OF IMPORT REQUEST        
Revison History       :                                                        
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD           
------------------------------------------------------------                                                        
Date     Review By          Comments                           
                  
drop Proc PROC_MIG_DELETE_PREVIOUS_IMPORT_REQUEST_RECORDS                                               
------   ------------       -------------------------*/                                                        
--                           
                            
--                         
                      
CREATE PROCEDURE [dbo].[PROC_MIG_DELETE_PREVIOUS_IMPORT_REQUEST_RECORDS]            
                  
    @IMPORT_REQUEST_ID        INT          
AS                        
BEGIN                     
                

                
        DELETE FROM MIG_IMPORT_ERROR_DETAILS  		WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID        
        DELETE FROM MIG_CUSTOMER_POLICY_LIST  		WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID        
        DELETE FROM [MIG_POLICY_COVERAGES]    		WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID        
        DELETE FROM [MIG_POLICY_INSTALLMENT_CANCEL] WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID        
        --DELETE FROM [MIG_IMPORT_REQUEST]       WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID        
              
        UPDATE MIG_IMPORT_REQUEST       
        SET    REQUEST_STATUS='INPRG'      
        WHERE  IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID      
              
                
END 

GO

