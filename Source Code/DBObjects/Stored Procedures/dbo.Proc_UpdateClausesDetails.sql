IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateClausesDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateClausesDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

        
/*----------------------------------------------------------                      
Proc Name       : dbo.Proc_UpdateClausesDetails                      
Created by      : Praveen Kumar           
Date            : 02/05/2010                     
Purpose   :To Update records in MNT_CLAUSES table.                      
Revison History :                      
Used In   : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/                      
--DROP PROC dbo.Proc_UpdateClausesDetails           
          
CREATE PROC [dbo].[Proc_UpdateClausesDetails]            
(            
@CLAUSE_ID int,          
@LOB_ID int=NULL,          
@SUBLOB_ID int=NULL,         
@CLAUSE_TITLE NVARCHAR(200)=NULL,         
@CLAUSE_DESCRIPTION TEXT=NULL,        
@IS_ACTIVE NCHAR(1)=NULL,            
@MODIFIED_BY INT=NULL,          
@LAST_UPDATED_DATETIME DATETIME=NULL,
@CLAUSE_TYPE INT=NULL,
@PROCESS_TYPE INT=NULL,
@ATTACH_FILE_NAME NVARCHAR(255)=NULL,
@CLAUSE_CODE NVARCHAR(10)=NULL  
       
)          
AS          
BEGIN  
 IF EXISTS(SELECT CLAUSE_CODE FROM MNT_CLAUSES WITH(NOLOCK) WHERE LOB_ID = @LOB_ID AND SUBLOB_ID = @SUBLOB_ID and CLAUSE_CODE=@CLAUSE_CODE and CLAUSE_ID<>@CLAUSE_ID )  
 BEGIN  
 RETURN -2  
 END  
        
    -- and CLAUSE_ID!=@CLAUSE_ID     
if NOT EXISTS(SELECT CLAUSE_ID FROM MNT_CLAUSES WITH(NOLOCK)       
 WHERE LOB_ID = @LOB_ID AND SUBLOB_ID = @SUBLOB_ID and CLAUSE_ID!=@CLAUSE_ID  AND UPPER(CLAUSE_TITLE)=UPPER(@CLAUSE_TITLE)
 )   
 BEGIN    
UPDATE MNT_CLAUSES          
SET          
          
 LOB_ID=@LOB_ID,        
 SUBLOB_ID=@SUBLOB_ID,         
 CLAUSE_TITLE=@CLAUSE_TITLE,        
 CLAUSE_DESCRIPTION=@CLAUSE_DESCRIPTION,        
 IS_ACTIVE=@IS_ACTIVE,        
       
 MODIFIED_BY=@MODIFIED_BY,        
 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME ,
 CLAUSE_TYPE =@CLAUSE_TYPE ,
 PROCESS_TYPE=@PROCESS_TYPE,
 ATTACH_FILE_NAME=@ATTACH_FILE_NAME,
 CLAUSE_CODE=@CLAUSE_CODE      
            
 WHERE          
 CLAUSE_ID=@CLAUSE_ID      
     
 RETURN 1;      
 END    
 ELSE    
 --4 is for showing message "Product and Line of Business combination already exists".  
---return -4;      
      return -5;      
END 
GO

