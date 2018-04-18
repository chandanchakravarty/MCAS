IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_ADJUSTER_AUTHORITY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_ADJUSTER_AUTHORITY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

 
/*----------------------------------------------------------              
Proc Name       : dbo.Proc_InsertCLM_ADJUSTER_AUTHORITY        
Created by      : Sumit Chhabra            
Date            : 25/04/2006              
Purpose         : Insert data in CLM_ADJUSTER_AUTHORITY  for an adjuster and the chosen LOB      
Created by      : Sumit Chhabra             
Revison History :              
Used In        : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
CREATE PROC dbo.Proc_InsertCLM_ADJUSTER_AUTHORITY          
 @ADJUSTER_AUTHORITY_ID int output,      
 @LOB_ID int,      
 @LIMIT_ID int,      
 @ADJUSTER_ID int,    
 @EFFECTIVE_DATE datetime,      
 @CREATED_BY int,      
 @CREATED_DATETIME datetime,
 @NOTIFY_AMOUNT decimal(12,2)      
AS              
BEGIN            
  
--Check for the existence of a record with a similar Effective Date for current adjuster and LOB   
IF EXISTS   
 (  
  SELECT ADJUSTER_AUTHORITY_ID FROM CLM_ADJUSTER_AUTHORITY   
   WHERE ADJUSTER_ID=@ADJUSTER_ID AND EFFECTIVE_DATE=@EFFECTIVE_DATE AND LOB_ID=@LOB_ID AND IS_ACTIVE='Y'  
 )  
 return -1  
  
--Continue with proc if no duplicate effective date for current adjuster and lob is found  
 SELECT @ADJUSTER_AUTHORITY_ID = ISNULL(MAX(ADJUSTER_AUTHORITY_ID),0)+1 FROM CLM_ADJUSTER_AUTHORITY          
      
      
 INSERT INTO CLM_ADJUSTER_AUTHORITY       
 (      
  ADJUSTER_AUTHORITY_ID,      
  LOB_ID,      
  LIMIT_ID,      
 ADJUSTER_ID,    
  EFFECTIVE_DATE,      
  CREATED_BY,      
  CREATED_DATETIME,      
  IS_ACTIVE,
  NOTIFY_AMOUNT      
 )      
 VALUES      
 (      
  @ADJUSTER_AUTHORITY_ID,      
  @LOB_ID,      
  @LIMIT_ID,      
  @ADJUSTER_ID,    
  @EFFECTIVE_DATE,      
  @CREATED_BY,      
  @CREATED_DATETIME,      
  'Y',
  @NOTIFY_AMOUNT      
 )      
        
END         






GO

