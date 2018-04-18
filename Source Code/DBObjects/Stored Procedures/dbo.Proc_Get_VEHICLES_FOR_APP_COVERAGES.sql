IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_VEHICLES_FOR_APP_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_VEHICLES_FOR_APP_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name   : dbo.Proc_Get_VEHICLES_FOR_APP_COVERAGES      
Created by  : Pradeep      
Date        : Feb 3, 2005  
Purpose     : Get all vehicles for App level coverages   

modified by		:Pravesh k Chandel
Modified Date	:29 Dec 2008
Purpose			: get only those vehicle where PL level Covg Applicable  

modified by		:Pravesh k Chandel
Modified Date	:24 APRIL 2009
Purpose			: get only SAME TYPE OF vehicle where PL level Covg Applicable  

Revison History  :                      
 ------------------------------------------------------------                            
Date     Review By          Comments                          
DROP PROCEDURE dbo.Proc_Get_VEHICLES_FOR_APP_COVERAGES                      
------   ------------       -------------------------*/                
CREATE PROCEDURE dbo.Proc_Get_VEHICLES_FOR_APP_COVERAGES     
(      
	 @VEHICLE_ID smallint,      
	 @CUSTOMER_ID int,      
	 @APP_ID int,      
	 @APP_VERSION_ID int
      
)          
AS               
BEGIN        
        
-- ADDED BY PRAVEH ON 24 APRIL 09 iTRACK 5731
DECLARE    @USE_VEHICLE INT
SELECT @USE_VEHICLE =USE_VEHICLE 
	FROM   APP_VEHICLES    WITH(NOLOCK)        
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
     APP_ID = @APP_ID AND            
     APP_VERSION_ID = @APP_VERSION_ID AND            
     VEHICLE_ID = @VEHICLE_ID
-- END HERE

SELECT VEHICLE_ID
	FROM APP_VEHICLES
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID AND
		VEHICLE_ID <> @VEHICLE_ID AND
		ISNULL(VEHICLE_TYPE_PER,0) NOT IN (11870,11618,11337) AND ISNULL(VEHICLE_TYPE_COM,0) NOT IN (11341)
		AND ISNULL(IS_SUSPENDED,0)!=10963	 	  
		AND isnull(USE_VEHICLE,'0') = isnull(@USE_VEHICLE,'0') 
		AND ISNULL(COMPRH_ONLY,'0')!=10963	 	  
    
END      
      
      
    
  








GO

