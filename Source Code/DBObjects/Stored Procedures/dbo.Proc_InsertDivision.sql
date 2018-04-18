IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertDivision]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertDivision]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------            
Proc Name       : dbo.Proc_InsertDivision            
Created by      : sonal           
Date            : 13 may 2010           
Purpose         : To add record in division table            
Revison History :            
Used In         :   wolvorine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/        
-- drop procedure Proc_InsertDivision          
CREATE PROC [dbo].[Proc_InsertDivision]            
(                            
  @DIV_CODE     nchar(6),                          
  @DIV_NAME     nvarchar(200),                          
  @DIV_ADD1     nvarchar(70),                          
  @DIV_ADD2     nvarchar(70),                          
  @DIV_CITY     nvarchar(40),                          
  @DIV_STATE    nvarchar(5),                          
  @DIV_ZIP      nvarchar(11),                          
  @DIV_COUNTRY  nvarchar(5),                          
  @DIV_PHONE    nvarchar(20),                          
  @DIV_EXT      nvarchar(5),                          
  @DIV_FAX      nvarchar(20),                          
  @DIV_EMAIL    nvarchar(50),                
  @CREATED_BY  int,                          
  @CREATED_DATETIME datetime ,          
  @DIV_ID       numeric  OUTPUT,        
  @NAIC_CODE    varchar(10),       
  @BRANCH_CODE nvarchar(10),       
  @BRANCH_CNPJ varchar(30),  
  @DATE_OF_BIRTH datetime,  
  @REGIONAL_IDENTIFICATION nvarchar(80),  
  @REG_ID_ISSUE_DATE datetime,  
  @ACTIVITY int,  
  @REG_ID_ISSUE nvarchar(80)  
          
          
)            
AS            
BEGIN            
 /*Checking whether the code already exists or not  */            
 Declare @Count numeric            
 SELECT @Count = Count(*)             
  FROM MNT_DIV_LIST            
  WHERE DIV_CODE = @DIV_CODE             
            
 IF @Count = 0             
 BEGIN            
  INSERT INTO MNT_DIV_LIST(            
   DIV_CODE,            
   DIV_NAME,            
   DIV_ADD1,            
   DIV_ADD2,            
   DIV_CITY,            
   DIV_STATE,            
   DIV_ZIP,            
   DIV_COUNTRY,            
   DIV_PHONE,            
   DIV_EXT,            
   DIV_FAX,            
   DIV_EMAIL,            
   IS_ACTIVE,            
   CREATED_BY,            
   CREATED_DATETIME,        
   NAIC_CODE,       
   BRANCH_CODE,    
   BRANCH_CNPJ,  
   DATE_OF_BIRTH,  
   REGIONAL_IDENTIFICATION,  
   REG_ID_ISSUE_DATE,  
   ACTIVITY,  
   REG_ID_ISSUE  
   )            
  VALUES(            
   @DIV_CODE,            
   @DIV_NAME,            
   @DIV_ADD1,            
   @DIV_ADD2,            
   @DIV_CITY,            
   @DIV_STATE,            
   @DIV_ZIP,            
   @DIV_COUNTRY,            
   @DIV_PHONE,            
   @DIV_EXT,            
   @DIV_FAX,            
   @DIV_EMAIL,            
   'Y',            
   @CREATED_BY,            
   @CREATED_DATETIME,        
   @NAIC_CODE,        
   @BRANCH_CODE,    
   @BRANCH_CNPJ,  
   @DATE_OF_BIRTH,  
   @REGIONAL_IDENTIFICATION,  
   @REG_ID_ISSUE_DATE,  
   @ACTIVITY,  
   @REG_ID_ISSUE       
   )            
  SELECT @DIV_ID = Max(DIV_ID)            
   FROM MNT_DIV_LIST            
            
 END            
 ELSE            
 BEGIN            
  /*Record already exist*/            
  SELECT @DIV_ID = -1            
 END            
             
END          
          
GO

