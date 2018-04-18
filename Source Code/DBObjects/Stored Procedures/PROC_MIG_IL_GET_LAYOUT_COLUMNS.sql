IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_GET_LAYOUT_COLUMNS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_GET_LAYOUT_COLUMNS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                            
Proc Name             : Dbo.[PROC_MIG_IL_GET_LAYOUT_COLUMNS]                                                        
Created by            : Santosh Kumar Gautam                                                           
Date                  : 25 Oct 2011                                                          
Purpose               : CREATE NEW CONTACT
Revison History       :                                                            
Used In               : INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc [PROC_MIG_IL_GET_LAYOUT_COLUMNS]  22
------   ------------       -------------------------*/                                                             

CREATE PROCEDURE [dbo].[PROC_MIG_IL_GET_LAYOUT_COLUMNS]   
      
 @COLUMN_TYPE INT,
 @SUSEP_LOB_CODE NVARCHAR(5)=NULL
   
AS                                
BEGIN                         
    
 SET NOCOUNT ON;    
 IF(@SUSEP_LOB_CODE='')
 SET @SUSEP_LOB_CODE=NULL
 
 SELECT POLICY_LAYOUT_ID,COLUMN_NAME
 FROM MIG_IL_LAYOUT_COLUMNS WITH(NOLOCK)
 WHERE [COLUMN_TYPE]=@COLUMN_TYPE AND IS_ACTIVE='Y'
 AND (@SUSEP_LOB_CODE IS NULL OR SUSEP_LOB_CODE = @SUSEP_LOB_CODE) 
  
            
END 



GO

