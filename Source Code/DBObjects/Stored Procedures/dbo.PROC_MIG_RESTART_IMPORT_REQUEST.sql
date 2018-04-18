IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_RESTART_IMPORT_REQUEST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_RESTART_IMPORT_REQUEST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                                                                
Proc Name             : Dbo.PROC_MIG_RESTART_IMPORT_REQUEST                                                                
Created by            : Santosh Kumar Gautam                                                               
Date                  : 30 May 2011                                                              
Purpose               : TO RESTART THE REQUEST     
Revison History       :                                                                
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD                   
------------------------------------------------------------                                                                
Date     Review By          Comments                                   
                          
drop Proc PROC_MIG_RESTART_IMPORT_REQUEST  18                                            
------   ------------       -------------------------*/                                                                
--                                   
                                    
--                                 
                              
CREATE PROCEDURE [dbo].[PROC_MIG_RESTART_IMPORT_REQUEST]        
      
@IMPORT_REQUEST_ID    INT
AS                                    
BEGIN                             
         
 UPDATE MIG_IMPORT_REQUEST 
 SET    REQUEST_STATUS ='INQUEUE', 
        IS_PROCESSED   ='N'      ,
        IS_ACTIVE      ='N'
 WHERE  IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID

END   
  
  
  
  
  
GO

