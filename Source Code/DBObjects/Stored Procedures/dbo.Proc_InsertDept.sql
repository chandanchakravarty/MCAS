IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertDept]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertDept]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------            
Proc Name       : dbo.Proc_InsertDept            
Created by      : Ashwani            
Date            : 3/9/2005            
Purpose         : To insert the data into Department table.            
Revison History :            
Used In         : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
CREATE PROC Dbo.Proc_InsertDept            
(            
           
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
  @CREATED_BY     int,            
  @CREATED_DATETIME     datetime,            
  @DEPT_ID       smallint output            
)            
AS            
BEGIN          
 /*Check for Unique Code of Department  */          
 Declare @Count numeric          
  SELECT @Count = Count(@DEPT_CODE)           
  FROM MNT_Dept_LIST WHERE DEPT_CODE = @DEPT_CODE           
 IF @Count >= 1           
 BEGIN          
          
   SELECT @DEPT_ID = -1   
   
 END          
 ELSE         
  BEGIN         
   INSERT INTO MNT_DEPT_LIST            
   (           
            
  DEPT_CODE,      
  DEPT_NAME,       
  DEPT_ADD1,            
  DEPT_ADD2,       
  DEPT_CITY,        
  DEPT_STATE,            
  DEPT_ZIP,          
  DEPT_COUNTRY,         
  DEPT_PHONE,            
  DEPT_EXT,        
  DEPT_FAX,          
  DEPT_EMAIL,    
  IS_ACTIVE,              
  CREATED_BY,       
  CREATED_DATETIME          
            
   )            
   VALUES            
   (            
  @DEPT_CODE,         
  @DEPT_NAME,        
  @DEPT_ADD1,            
  @DEPT_ADD2,       
  @DEPT_CITY,        
  @DEPT_STATE,            
  @DEPT_ZIP,         
  @DEPT_COUNTRY,       
  @DEPT_PHONE,            
  @DEPT_EXT,        
  @DEPT_FAX,         
  @DEPT_EMAIL,     
  'Y'       ,    
  @CREATED_BY,       
  @CREATED_DATETIME          
            
   )            
    SELECT @DEPT_ID = Max(DEPT_ID) FROM MNT_DEPT_LIST        
     END          
          
END      




GO

