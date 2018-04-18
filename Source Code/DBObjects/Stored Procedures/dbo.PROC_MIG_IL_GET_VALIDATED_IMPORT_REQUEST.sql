
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_GET_VALIDATED_IMPORT_REQUEST]    Script Date: 12/02/2011 16:43:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_GET_VALIDATED_IMPORT_REQUEST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_GET_VALIDATED_IMPORT_REQUEST]
GO
 


/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_GET_VALIDATED_IMPORT_REQUEST]    Script Date: 12/02/2011 16:43:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



 /*----------------------------------------------------------                                                            
Proc Name             : Dbo.[PROC_MIG_IL_GET_VALIDATED_IMPORT_REQUEST]                                                            
Created by            : Santosh Kumar Gautam                                                           
Date                  : 15 SEPT 2011                                                          
Purpose               : TO GET IMPORT REQUEST LIST DETAILS            
Revison History       :                                                            
Used In               : INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc [PROC_MIG_IL_GET_VALIDATED_IMPORT_REQUEST]                                                   
------   ------------       -------------------------*/                                                            
--                               
                                
--                             
                          
CREATE PROCEDURE [dbo].[PROC_MIG_IL_GET_VALIDATED_IMPORT_REQUEST]                         
                               
AS                                
BEGIN        

 
 IF NOT EXISTS(SELECT * FROM MIG_IL_IMPORT_REQUEST WITH(NOLOCK) WHERE REQUEST_STATUS='IMPRG')
 BEGIN
	SELECT IMPORT_REQUEST_ID 
	FROM MIG_IL_IMPORT_REQUEST WITH(NOLOCK)
	WHERE REQUEST_STATUS='IMQUEUE'  -- READY TO START IMPORTING
	ORDER BY IMPORT_REQUEST_ID
 END   
                   
                        
                   
END 










GO


