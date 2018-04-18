IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_RECREATIONAL_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_RECREATIONAL_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name       : dbo.Proc_UpdateCLM_RECREATIONAL_VEHICLES              
Created by      : Sumit Chhabra    
Date            : 11/08/2006              
Purpose       : Updates a record in CLM_RECREATIONAL_VEHICLES                 
Revison History :              
Used In  : Wolverine              
            
            
Modified by :     
Modified Date :   
Purpose  :   
    
   
         
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
--drop proc Proc_UpdateCLM_RECREATIONAL_VEHICLES              
CREATE  PROC dbo.Proc_UpdateCLM_RECREATIONAL_VEHICLES              
(              
 @CLAIM_ID int,                          
 @REC_VEH_ID smallint,              
 @COMPANY_ID_NUMBER     int,              
 @YEAR int,              
 @MAKE nvarchar(75),              
 @MODEL nvarchar(75),              
 @SERIAL nvarchar(75),              
 @STATE_REGISTERED int,              
 @VEHICLE_TYPE int,              
 @HORSE_POWER nvarchar(10),              
 @REMARKS nvarchar(500),                  
 @MODIFIED_BY int,              
 @LAST_UPDATED_DATETIME datetime           
)              
AS              
              
BEGIN              
                 
 IF EXISTS              
 (              
  SELECT * FROM CLM_RECREATIONAL_VEHICLES              
  WHERE CLAIM_ID = @CLAIM_ID AND                         
   COMPANY_ID_NUMBER = @COMPANY_ID_NUMBER AND              
   REC_VEH_ID <> @REC_VEH_ID              
 )              
 BEGIN              
  RETURN -2              
 END              
               
 UPDATE CLM_RECREATIONAL_VEHICLES              
 SET            
  COMPANY_ID_NUMBER = @COMPANY_ID_NUMBER,              
  YEAR = @YEAR,              
  MAKE = @MAKE,              
  MODEL = @MODEL,              
  SERIAL = @SERIAL,              
  STATE_REGISTERED = @STATE_REGISTERED,              
  VEHICLE_TYPE = @VEHICLE_TYPE,              
  HORSE_POWER = @HORSE_POWER,              
  REMARKS = @REMARKS,                        
  MODIFIED_BY = @MODIFIED_BY,              
  LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME  
 WHERE CLAIM_ID = @CLAIM_ID AND                            
       REC_VEH_ID = @REC_VEH_ID              
              
 RETURN 1               
              
END              
              
              
              
              
              
              
            
          
        
      
    
    
    
  



GO

