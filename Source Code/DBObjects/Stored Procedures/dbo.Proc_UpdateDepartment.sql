IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateDepartment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateDepartment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------              
Proc Name       : dbo.Proc_UpdateDepartment              
Created by      : Ashwani              
Date            : 3/9/2005              
Purpose         : To Upadte the data into Department table.              
Revison History :              
Used In         : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
CREATE PROC Dbo.Proc_UpdateDepartment              
(              
             
  @DEPT_ID       smallint,              
  @DEPT_CODE     nchar(6),              
  @DEPT_NAME     nvarchar(200),              
  @DEPT_ADD1     nvarchar(70),              
  @DEPT_ADD2     nvarchar(70),              
  @DEPT_CITY     nvarchar(50),              
  @DEPT_STATE    nvarchar(5),              
  @DEPT_ZIP      nvarchar(11),              
  @DEPT_COUNTRY     nvarchar(5),              
  @DEPT_PHONE     nvarchar(20),              
  @DEPT_EXT     nvarchar(5),              
  @DEPT_FAX     nvarchar(20),              
  @DEPT_EMAIL     nvarchar(50),    
  @IS_ACTIVE      nchar(1),       
  @MODIFIED_BY     int,              
  @LAST_UPDATED_DATETIME     datetime             
         
)              
AS              
BEGIN            
 /*Check for Unique Code of Department  */            
  IF NOT EXISTS (SELECT DEPT_ID FROM MNT_DEPT_LIST WHERE DEPT_CODE = @DEPT_CODE AND DEPT_ID <> @DEPT_ID)           
 BEGIN           
   UPDATE MNT_DEPT_LIST              
     SET                
    DEPT_CODE =@DEPT_CODE,        
    DEPT_NAME= @DEPT_NAME,         
    DEPT_ADD1 =@DEPT_ADD1,              
    DEPT_ADD2 =@DEPT_ADD2,         
    DEPT_CITY =@DEPT_CITY,          
    DEPT_STATE =@DEPT_STATE,              
    DEPT_ZIP =@DEPT_ZIP,            
    DEPT_COUNTRY=@DEPT_COUNTRY ,            
    DEPT_PHONE =@DEPT_PHONE,              
    DEPT_EXT =@DEPT_EXT,          
    DEPT_FAX= @DEPT_FAX,            
    DEPT_EMAIL = @DEPT_EMAIL,      
    IS_ACTIVE = @IS_ACTIVE,                
    MODIFIED_BY =@MODIFIED_BY,         
    LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME    
  WHERE DEPT_ID = @DEPT_ID    
 END  
        
END     




GO

