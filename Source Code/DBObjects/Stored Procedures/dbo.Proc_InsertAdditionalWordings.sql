IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAdditionalWordings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAdditionalWordings]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name       : dbo.Proc_InsertAdditionalWordings          
Created by      :  Swarup Pal         
Date                :  22-Feb-2007         
Purpose         :  To add data into MNT_PROCESS_WORDINGS         
Revison History :          
Used In         :    Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
-- drop proc dbo.Proc_InsertAdditionalWordings        
CREATE     PROC dbo.Proc_InsertAdditionalWordings           
(                    
 @WORDINGS_ID     int,                    
 @STATE_ID     int,                    
 @PROCESS_ID     int,       
 @LOB_ID  int,      
 @PDF_WORDINGS varchar(7000),      
 @CREATED_BY int = null,      
 @CREATED_DATETIME datetime = null,      
 @MODIFIED_BY int = null,      
 @LAST_UPDATED_DATETIME datetime = null,      
 @IS_ACTIVE nchar(1)      
)                    
AS       
BEGIN          
 IF EXISTS (SELECT WORDINGS_ID FROM MNT_PROCESS_WORDINGS      
  WHERE STATE_ID = @STATE_ID AND PROCESS_ID = @PROCESS_ID    
   AND LOB_ID = @LOB_ID)    
   BEGIN    
  RETURN -1    
   END    
 ELSE    
   BEGIN          
 select @WORDINGS_ID=isnull(Max(WORDINGS_ID),0)+1 from MNT_PROCESS_WORDINGS                    
 INSERT INTO MNT_PROCESS_WORDINGS                    
 (                    
 WORDINGS_ID,      
 STATE_ID,      
 PROCESS_ID,      
 LOB_ID,      
 PDF_WORDINGS,      
 CREATED_BY,      
 CREATED_DATETIME,      
 MODIFIED_BY,      
 LAST_UPDATED_DATETIME,      
 IS_ACTIVE      
         
 )                    
 VALUES                    
 (                    
 @WORDINGS_ID,      
 @STATE_ID,      
 @PROCESS_ID,      
 @LOB_ID,      
 @PDF_WORDINGS,      
 @CREATED_BY,      
 @CREATED_DATETIME,      
 @MODIFIED_BY,      
 @LAST_UPDATED_DATETIME,      
 @IS_ACTIVE      
  )   
END                    
END                   
      
      
      
    
  


GO

