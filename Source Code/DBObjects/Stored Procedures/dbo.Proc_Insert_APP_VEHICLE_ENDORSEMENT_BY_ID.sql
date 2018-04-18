IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID
/*----------------------------------------------------------                  
Proc Name   : dbo.Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID                 
Created by  : Pradeep                  
Date        : 25 Nov,2005                
Purpose     :  Inserts an appropriate endorsemnt in       
  APP_VEHICLE_ENDORSEMENTS      
Revison History  :                        
------------------------------------------------------------                              
Date     Review By          Comments                            
-----------------------------------------------------------*/            
CREATE   PROCEDURE Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID          
(           
 @CUSTOMER_ID int,          
 @APP_ID int,          
 @APP_VERSION_ID smallint,           
 @ENDORSEMENT_ID smallint,      
 @VEHICLE_ID smallint          
)          
          
As          
           
 IF NOT EXISTS      
 (      
  SELECT * FROM APP_VEHICLE_ENDORSEMENTS      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
   APP_ID = @APP_ID AND      
   APP_VERSION_ID = @APP_VERSION_ID AND      
   VEHICLE_ID = @VEHICLE_ID  AND    
   ENDORSEMENT_ID = @ENDORSEMENT_ID     
 )  
 BEGIN  
   
 DECLARE @VEH_END_ID Int  
DECLARE @EDITION_DATE VARCHAR(10)
DECLARE @APP_EFFECTIVE_DATE DATETIME

  SELECT    @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE                                     
  FROM APP_LIST    WHERE CUSTOMER_ID = @CUSTOMER_ID AND  APP_ID = @APP_ID 
AND  APP_VERSION_ID = @APP_VERSION_ID               
            
  
SELECT  @VEH_END_ID   = ISNULL(MAX ( VEHICLE_ENDORSEMENT_ID ),0) + 1  
FROM APP_VEHICLE_ENDORSEMENTS  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
 APP_ID = @APP_ID AND  
 APP_VERSION_ID = @APP_VERSION_ID AND  
 VEHICLE_ID = @VEHICLE_ID  
--BY PRAVESH FOR DEFAULT EDITION DATE            
   SELECT    @EDITION_DATE =EDITION_DATE  FROM MNT_ENDORSEMENT_ATTACHMENT WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID AND 
   @APP_EFFECTIVE_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-12') AND  @APP_EFFECTIVE_DATE<=ISNULL(DISABLED_DATE,'3000-12-12') 
--
 INSERT INTO APP_VEHICLE_ENDORSEMENTS      
 (      
  CUSTOMER_ID,      
  APP_ID,      
  APP_VERSION_ID,      
  VEHICLE_ID,      
  ENDORSEMENT_ID,  
  VEHICLE_ENDORSEMENT_ID ,
  EDITION_DATE      
 )      
 VALUES      
 (      
  @CUSTOMER_ID,      
  @APP_ID,      
  @APP_VERSION_ID,      
  @VEHICLE_ID,      
  @ENDORSEMENT_ID ,  
  @VEH_END_ID,
  @EDITION_DATE     
 )      
END          
          
RETURN 1          
          
          
          
        
      
    
  





GO

