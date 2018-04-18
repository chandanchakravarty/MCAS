IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_Delete_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES  
/*----------------------------------------------------------                    
Proc Name   : dbo.Proc_Delete_APP_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES                   
Created by  : Pradeep                    
Date        : 25 Nov,2005                  
Purpose     :  Deletes an appropriate endorsemnt in  POL_VEHICLE_ENDORSEMENTS        
Revison History  :                          
------------------------------------------------------------                                
Date     Review By          Comments                              
-----------------------------------------------------------*/         
       
CREATE   PROCEDURE Proc_Delete_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES            
(             
 @CUSTOMER_ID int,            
 @POLICY_ID int,            
 @POLICY_VERSION_ID smallint,             
 @ENDORSEMENT_ID smallint  ,  
 @POL_VEHICLE_ID SMALLINT=NULL      
)            
            
As            
  
DECLARE @APP_USE_VEHICLE_ID INT 
DECLARE @COMPRH_ONLY INT --Added by Charles on 4-Aug-09 for Itrack 6201        
   
IF (@POL_VEHICLE_ID IS NOT NULL)  
BEGIN  
  SELECT @APP_USE_VEHICLE_ID=APP_USE_VEHICLE_ID,
	@COMPRH_ONLY=COMPRH_ONLY --Added by Charles on 4-Aug-09 for Itrack 6201                   
  FROM POL_VEHICLES      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
  POLICY_ID = @POLICY_ID AND      
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND      
  VEHICLE_ID = @POL_VEHICLE_ID     
 END          
            
 IF EXISTS        
 (        
  SELECT * FROM POL_VEHICLE_ENDORSEMENTS        
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
   POLICY_ID = @POLICY_ID AND        
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
   ENDORSEMENT_ID = @ENDORSEMENT_ID       
 )        
 DELETE FROM POL_VEHICLE_ENDORSEMENTS      
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
   POLICY_ID = @POLICY_ID AND        
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
   ENDORSEMENT_ID = @ENDORSEMENT_ID  
  AND VEHICLE_ID IN  
  (  
  SELECT VEHICLE_ID      
  FROM POL_VEHICLES      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
   POLICY_ID = @POLICY_ID AND      
   POLICY_VERSION_ID = @POLICY_VERSION_ID       
   AND ISNULL(APP_USE_VEHICLE_ID,'0')=ISNULL(@APP_USE_VEHICLE_ID ,isnull(APP_USE_VEHICLE_ID,'0'))  
 AND ISNULL(COMPRH_ONLY,'0')=ISNULL(@COMPRH_ONLY ,isnull(COMPRH_ONLY,'0'))--Added by Charles on 4-Aug-09 for Itrack 6201     
  
  )       
       
            
            
RETURN 1            
            
  
GO

