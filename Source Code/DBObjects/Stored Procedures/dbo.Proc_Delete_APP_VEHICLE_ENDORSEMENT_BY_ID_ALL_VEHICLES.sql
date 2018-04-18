IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_APP_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_APP_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_Delete_APP_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES       
/*----------------------------------------------------------                      
Proc Name   : dbo.Proc_Delete_APP_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES                     
Created by  : Pradeep                      
Date        : 25 Nov,2005                    
Purpose     :  Deletes an appropriate endorsemnt in           
    APP_VEHICLE_ENDORSEMENTS          
Revison History  :                            
------------------------------------------------------------                                  
Date     Review By          Comments                                
-----------------------------------------------------------*/                
CREATE   PROCEDURE Proc_Delete_APP_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES              
(               
 @CUSTOMER_ID int,              
 @APP_ID int,              
 @APP_VERSION_ID smallint,               
 @ENDORSEMENT_ID smallint,    
 @APP_VEHICLE_ID SMALLINT=NULL                
)              
              
As      
--Added by Charles on 10-Jul-09 for Itrack 6082    
DECLARE @USE_VEHICLE INT   
DECLARE @COMPRH_ONLY INT --Added by Charles on 4-Aug-09 for Itrack 6201        
if (@APP_VEHICLE_ID IS NOT NULL)      
BEGIN      
  SELECT @USE_VEHICLE=USE_VEHICLE, 
	@COMPRH_ONLY=COMPRH_ONLY --Added by Charles on 4-Aug-09 for Itrack 6201             
  FROM APP_VEHICLES          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                              
 APP_ID = @APP_ID AND                              
 APP_VERSION_ID = @APP_VERSION_ID  AND          
  VEHICLE_ID = @APP_VEHICLE_ID         
 END         
--Added till here              
               
 IF  EXISTS          
 (          
  SELECT * FROM APP_VEHICLE_ENDORSEMENTS          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
   APP_ID = @APP_ID AND          
   APP_VERSION_ID = @APP_VERSION_ID AND          
   ENDORSEMENT_ID = @ENDORSEMENT_ID         
 )          
 DELETE FROM APP_VEHICLE_ENDORSEMENTS        
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
   APP_ID = @APP_ID AND          
   APP_VERSION_ID = @APP_VERSION_ID AND          
   ENDORSEMENT_ID = @ENDORSEMENT_ID    
 AND VEHICLE_ID IN  --Added by Charles on 10-Jul-09 for Itrack 6082    
  (      
  SELECT VEHICLE_ID          
  FROM APP_VEHICLES          
 WHERE  CUSTOMER_ID = @CUSTOMER_ID AND          
  APP_ID = @APP_ID AND          
  APP_VERSION_ID = @APP_VERSION_ID          
   AND ISNULL(USE_VEHICLE,'0')=ISNULL(@USE_VEHICLE ,isnull(USE_VEHICLE,'0'))   
 AND ISNULL(COMPRH_ONLY,'0')=ISNULL(@COMPRH_ONLY ,isnull(COMPRH_ONLY,'0'))--Added by Charles on 4-Aug-09 for Itrack 6201     

  )             
         
              
              
RETURN 1              
              
              
              
            
          
        
GO

