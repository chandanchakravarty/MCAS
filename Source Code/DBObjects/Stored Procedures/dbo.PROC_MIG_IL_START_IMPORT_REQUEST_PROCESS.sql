
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_START_IMPORT_REQUEST_PROCESS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_START_IMPORT_REQUEST_PROCESS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
 /*----------------------------------------------------------                                                      
Proc Name             : Dbo.[PROC_MIG_IL_START_IMPORT_REQUEST_PROCESS]                                                     
Created by            : Santosh Kumar Gautam                                                     
Date                  : 17 Aug 2011                                                 
Purpose               : TO INSERT IMPORT REQUEST IN QUQUE FOR PROCESSING    
Revison History       :                                                      
Used In               : INITIAL LOAD         
------------------------------------------------------------                                                      
Date     Review By          Comments                         
                
drop Proc [PROC_MIG_IL_START_IMPORT_REQUEST_PROCESS]                                             
------   ------------       -------------------------*/                                                      
--                         
                          
--                       
                    
CREATE PROCEDURE [dbo].[PROC_MIG_IL_START_IMPORT_REQUEST_PROCESS]          
         
    @IMPORT_REQUEST_ID        INT
AS                      
BEGIN                   
              
       
    UPDATE MIG_IL_IMPORT_REQUEST
    SET REQUEST_STATUS='INQUEUE'
    WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID
       
               
       
              
END                          





