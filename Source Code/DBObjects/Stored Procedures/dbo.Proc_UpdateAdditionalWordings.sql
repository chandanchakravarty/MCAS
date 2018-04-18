IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAdditionalWordings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAdditionalWordings]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_UpdateAdditionalWordings          
Created by      :  Swarup Pal         
Date                :  22-Feb-2007         
Purpose         :  To update data of MNT_PROCESS_WORDINGS         
Revison History :          
Used In         :    Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
-- drop proc dbo.Proc_UpdateAdditionalWordings 17,14,28,2,'this is testing again',null,null,null,null,'Y'       
CREATE     PROC dbo.Proc_UpdateAdditionalWordings           
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
DECLARE @FLAG INT  
  
IF EXISTS (SELECT * FROM MNT_PROCESS_WORDINGS WHERE LOB_ID=@LOB_ID AND STATE_ID=@STATE_ID AND PROCESS_ID=@PROCESS_ID)  
   SET @FLAG = 1  
ELSE  
   SET @FLAG = 0  
IF @FLAG = 1  
    BEGIN  
      --DELETE FROM MNT_PROCESS_WORDINGS WHERE WORDINGS_ID = @WORDINGS_ID  
 Update  MNT_PROCESS_WORDINGS                      
 set                      
-- WORDINGS_ID   =  @WORDINGS_ID,                      
-- STATE_ID      =  @STATE_ID,                      
-- PROCESS_ID    =  @PROCESS_ID  ,            
-- LOB_ID        =  @LOB_ID,                  
 PDF_WORDINGS      =  @PDF_WORDINGS ,            
 MODIFIED_BY   =  @MODIFIED_BY,                
 LAST_UPDATED_DATETIME   =  @LAST_UPDATED_DATETIME,                    
 IS_ACTIVE     =  @IS_ACTIVE                
                       
 where  LOB_ID=@LOB_ID AND STATE_ID=@STATE_ID AND PROCESS_ID=@PROCESS_ID AND WORDINGS_ID =@WORDINGS_ID 
 RETURN 1  
     END  
     ELSE  
     BEGIN
       --DELETE FROM MNT_PROCESS_WORDINGS WHERE WORDINGS_ID = @WORDINGS_ID  
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
      RETURN 2    
    END   
END          
     
  








GO

