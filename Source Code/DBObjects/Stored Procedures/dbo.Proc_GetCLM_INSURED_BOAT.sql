IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_INSURED_BOAT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_INSURED_BOAT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetCLM_INSURED_BOAT 824,6          
Created by      : Sumit Chhabra      
Date            : 5/24/2006          
Purpose         :To retrieve records from CLM_INSURED_BOAT .          
Revison History :          
Used In        : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
-- DROP  PROC dbo.Proc_GetCLM_INSURED_BOAT          
CREATE   PROC dbo.Proc_GetCLM_INSURED_BOAT          
(          
@CLAIM_ID int,        
@BOAT_ID int    
)          
AS          
begin        
         
 SELECT          
  CLAIM_ID,        
  BOAT_ID,        
  SERIAL_NUMBER,        
  CASE WHEN [YEAR] = 0 THEN NULL ELSE [YEAR] END AS [YEAR],          
  MAKE,        
  MODEL,        
  BODY_TYPE,        
  LENGTH,        
  WEIGHT,        
  CASE WHEN HORSE_POWER = 0.0 THEN NULL ELSE HORSE_POWER END AS HORSE_POWER,        
  OTHER_HULL_TYPE,        
  PLATE_NUMBER,        
  STATE,             
  IS_ACTIVE,  
  POLICY_BOAT_ID,  
  WHERE_BOAT_SEEN,  
  INCLUDE_TRAILER        
 FROM    
  CLM_INSURED_BOAT       
 WHERE    
  CLAIM_ID=@CLAIM_ID AND    
  BOAT_ID=@BOAT_ID    
END        
      
    
  
  
  
  
  
  
GO

