IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertClausesDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertClausesDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
  
/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_InsertClausesDetails                  
Created by      : Praveen Kumar       
Date            : 02/05/2010                 
Purpose   :To insert records in MNT_CLAUSES table.                  
Revison History :                  
Used In        : Ebix Advantage                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
--DROP PROC dbo.Proc_InsertClausesDetails       
   
CREATE PROC [dbo].[Proc_InsertClausesDetails]        
(        
@CLAUSE_ID int out,      
@LOB_ID int=NULL,      
@SUBLOB_ID int=NULL,     
@CLAUSE_TITLE NVARCHAR(200)=NULL,     
@CLAUSE_DESCRIPTION TEXT= NULL,    
@IS_ACTIVE NCHAR(1)=NULL,      
@CREATED_BY INT=NULL,      
@CREATED_DATETIME DATETIME=NULL,      
@MODIFIED_BY INT=NULL,      
@LAST_UPDATED_DATETIME DATETIME=NULL ,
@CLAUSE_TYPE INT=NULL,
@PROCESS_TYPE INT=NULL,
@ATTACH_FILE_NAME NVARCHAR(255)=NULL,
@CLAUSE_CODE NVARCHAR(10)=NULL      
    
)      
AS      
BEGIN      
  
  
      
 if not EXISTS(SELECT CLAUSE_ID FROM MNT_CLAUSES WITH(NOLOCK)     
 WHERE LOB_ID = @LOB_ID AND SUBLOB_ID = @SUBLOB_ID AND UPPER(CLAUSE_TITLE)=UPPER(@CLAUSE_TITLE) AND CLAUSE_CODE=@CLAUSE_CODE)   
 BEGIN  
    SELECT  @CLAUSE_ID=isnull(Max(CLAUSE_ID),0)+1 from MNT_CLAUSES WITH(NOLOCK)  
INSERT INTO MNT_CLAUSES      
(      
CLAUSE_ID,LOB_ID,  SUBLOB_ID, CLAUSE_TITLE,CLAUSE_DESCRIPTION,    
IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME, CLAUSE_TYPE,PROCESS_TYPE,ATTACH_FILE_NAME,CLAUSE_CODE  
 )      
 VALUES      
 (  @CLAUSE_ID, @LOB_ID,@SUBLOB_ID, @CLAUSE_TITLE,@CLAUSE_DESCRIPTION,    
 @IS_ACTIVE,@CREATED_BY,@CREATED_DATETIME,@MODIFIED_BY,@LAST_UPDATED_DATETIME,@CLAUSE_TYPE,@PROCESS_TYPE,@ATTACH_FILE_NAME,
 @CLAUSE_CODE   
 )   
   
 RETURN 1;  
 END  
 ELSE  
     
      return -4;    
   
END
GO

