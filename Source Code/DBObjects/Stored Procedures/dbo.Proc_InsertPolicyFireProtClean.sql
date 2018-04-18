IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyFireProtClean]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyFireProtClean]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_InsertPolicyFireProtClean  
Created by      : Anurag Verma 
Date            : 18/11/2005  
Purpose         : To add record in fire prot clean table  
Revison History :  
Used In         :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC dbo.Proc_InsertPolicyFireProtClean
(  
 @CUSTOMER_ID                 int,  
 @POL_ID                      int,  
 @POL_VERSION_ID              smallint,  
 @IS_SMOKE_DETECT0R           nchar(1),
 @IS_PROTECTIVE_MAT_FLOOR     nchar(1),
 @IS_PROTECTIVE_MAT_WALLS     nchar(1),
 @PROT_MAT_SPACED             nvarchar(265),  
 @STOVE_SMOKE_PIPE_CLEANED     nvarchar(20) ,  
 @STOVE_CLEANER               nvarchar(20),  
 @REMARKS                     nvarchar(2000),  
 @FUEL_ID                     smallint
    
)  
AS  
  
BEGIN 
 
  
 INSERT INTO POL_HOME_OWNER_FIRE_PROT_CLEAN  
  (  
  FUEL_ID,
  CUSTOMER_ID ,  
  POLICY_ID  ,  
  POLICY_VERSION_ID   ,   
  IS_SMOKE_DETECTOR ,         
 
  IS_PROTECTIVE_MAT_FLOOR ,                        
  IS_PROTECTIVE_MAT_WALLS ,    
  PROT_MAT_SPACED ,            
  STOVE_SMOKE_PIPE_CLEANED,
  STOVE_CLEANER ,                            
  REMARKS                     
  
  )     
 values  (  
  @FUEL_ID,
  @CUSTOMER_ID,  
  @POL_ID ,    
  @POL_VERSION_ID ,         
  @IS_SMOKE_DETECT0R,
  @IS_PROTECTIVE_MAT_FLOOR ,                        
  @IS_PROTECTIVE_MAT_WALLS ,    
  @PROT_MAT_SPACED ,            
  @STOVE_SMOKE_PIPE_CLEANED,
  @STOVE_CLEANER ,                            
  @REMARKS  
    
        )  
                
  
END




GO

