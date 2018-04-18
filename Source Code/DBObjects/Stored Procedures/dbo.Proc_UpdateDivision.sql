IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateDivision]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateDivision]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROC Proc_UpdateDivision    
    
CREATE PROC [dbo].[Proc_UpdateDivision]            
(                          
            
  @DIV_ID       smallint,                          
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
  @IS_ACTIVE    nchar(1),                   
  @MODIFIED_BY  int,                          
  @LAST_UPDATED_DATETIME     datetime,        
  @NAIC_CODE varchar(10),      
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
 /*Check for Unique Code of Department  */                        
  IF NOT EXISTS (SELECT DIV_ID FROM MNT_DIV_LIST WHERE DIV_CODE = @DIV_CODE AND DIV_ID <> @DIV_ID)                       
   BEGIN            
   Update MNT_DIV_LIST            
       SET                            
     DIV_CODE  =@DIV_CODE,                    
     DIV_NAME = @DIV_NAME,                     
     DIV_ADD1  =@DIV_ADD1,                          
     DIV_ADD2  =@DIV_ADD2,                     
     DIV_CITY  =@DIV_CITY,                      
     DIV_STATE  =@DIV_STATE,                          
     DIV_ZIP  =@DIV_ZIP,                        
     DIV_COUNTRY =@DIV_COUNTRY ,                        
     DIV_PHONE  =@DIV_PHONE,                          
     DIV_EXT  =@DIV_EXT,                      
     DIV_FAX = @DIV_FAX,                        
     DIV_EMAIL  = @DIV_EMAIL,                  
     IS_ACTIVE  = @IS_ACTIVE,                            
     MODIFIED_BY  =@MODIFIED_BY,                     
     LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME,        
     NAIC_CODE = @NAIC_CODE,      
     BRANCH_CODE=@BRANCH_CODE,      
     BRANCH_CNPJ=@BRANCH_CNPJ,   
     DATE_OF_BIRTH = @DATE_OF_BIRTH,  
     REGIONAL_IDENTIFICATION = @REGIONAL_IDENTIFICATION,  
     REG_ID_ISSUE_DATE = @REG_ID_ISSUE_DATE,  
     ACTIVITY = @ACTIVITY,  
     REG_ID_ISSUE = @REG_ID_ISSUE  
     WHERE DIV_ID = @DIV_ID                
  End            
END            
          
GO

