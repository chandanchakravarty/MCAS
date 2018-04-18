IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_WATERCRAFT_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_WATERCRAFT_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc dbo.Proc_GetAPP_WATERCRAFT_ENDORSEMENTS          
          
/*                              
----------------------------------------------------------                                  
Proc Name       : dbo.Proc_GetAPP_WATERCRAFT_ENDORSEMENTS                              
Created by      : Pradeep                                
Date            : Oct 11, 2005                               
Purpose         : Selects records from Watercraft Endorsements                                  
Revison History :                                  
Used In         : Wolverine                                  

Modified By 	: Ravindra
Modified On	: 05-25-2006
Purpose		: Grandfathered Implementation 

Modified By 	: Ravindra
Modified On	: 07-31-2006
Purpose		: Added code to fetch Coverage Type Basis
------------------------------------------------------------                                  
Date     Review By          Comments                                  
------   ------------       -------------------------                                 
*/                              
                              
CREATE            PROCEDURE dbo.Proc_GetAPP_WATERCRAFT_ENDORSEMENTS                              
(                              
 @CUSTOMER_ID int,                              
 @APP_ID int,                              
 @APP_VERSION_ID smallint,                              
 @BOAT_ID smallint,                            
 @APP_TYPE Char(1)                            
)                              
                              
As                      
                      
                    
DECLARE @STATE_ID SmallInt                      
DECLARE @LOB_ID NVarCHar(5)                      
DECLARE @APP_EFFECTIVE_DATE datetime                    
                      
 SELECT @STATE_ID = STATE_ID,                      
 	@LOB_ID = APP_LOB,                      
	@APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE
 FROM APP_LIST                      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                      
	APP_ID = @APP_ID AND                      
	APP_VERSION_ID = @APP_VERSION_ID                      

SET @LOB_ID = 4    

--Table 1                        
	SELECT  ED.ENDORSMENT_ID,                
		ED.DESCRIPTION as ENDORSEMENT, 
    		ED.ENDORSEMENT_CODE,                
		ED.TYPE,           
		VE.REMARKS,                
		VE.VEHICLE_ENDORSEMENT_ID   ,              
		NULL as Selected,
		ED.RANK,
		ED.EFFECTIVE_FROM_DATE,
		ED.EFFECTIVE_TO_DATE ,
                VE.EDITION_DATE              
	FROM MNT_ENDORSMENT_DETAILS  ED                 
	LEFT OUTER JOIN APP_WATERCRAFT_ENDORSEMENTS  VE ON                
		ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID 
		AND VE.CUSTOMER_ID = @CUSTOMER_ID 
		AND VE.APP_ID = @APP_ID 
		AND VE.APP_VERSIOn_ID =  @APP_VERSION_ID 
		AND VE.BOAT_ID = @BOAT_ID          
	WHERE ED.STATE_ID = @STATE_ID 
		AND ED.LOB_ID = @LOB_ID
		AND ED.ENDORS_ASSOC_COVERAGE = 'N' 
		AND ED.IS_ACTIVE='Y'                      
		AND @APP_EFFECTIVE_DATE BETWEEN ISNULl(ED.EFFECTIVE_FROM_DATE,'1950-1-1') 
					and     ISNULL(ED.EFFECTIVE_TO_DATE,'3000-12-12')
		AND @APP_EFFECTIVE_DATE <=  ISNULL(ED.DISABLED_DATE,'3000-12-12')
		OR 
		(
			VE.ENDORSEMENT_ID is not null and ED.ENDORS_ASSOC_COVERAGE = 'N' 
		)
	UNION            
	SELECT  ED.ENDORSMENT_ID,                
		ED.DESCRIPTION as ENDORSEMENT,                
    		ED.ENDORSEMENT_CODE,                
		ED.TYPE,                
		VE.REMARKS,                
		VE.VEHICLE_ENDORSEMENT_ID ,           
		(              
			SELECT COUNT(*)                
			FROM MNT_ENDORSMENT_DETAILS MED                
			INNER JOIN APP_WATERCRAFT_COVERAGE_INFO  AVC ON                
			MED.SELECT_COVERAGE =  AVC.COVERAGE_CODE_ID                
			WHERE MED.ENDORSMENT_ID = ED.ENDORSMENT_ID
			AND AVC.CUSTOMER_ID = @CUSTOMER_ID
			AND APP_ID = @APP_ID
			AND APP_VERSION_ID = @APP_VERSION_ID
			AND MED.STATE_ID = @STATE_ID
			AND MED.LOB_ID = @LOB_ID 
			AND AVC.BOAT_ID = @BOAT_ID 
			AND MED.IS_ACTIVE='Y'               
		)as Selected,
		ED.RANK,
		ED.EFFECTIVE_FROM_DATE,
		ED.EFFECTIVE_TO_DATE,
		VE.EDITION_DATE                                      
	FROM MNT_ENDORSMENT_DETAILS  ED                 
	LEFT OUTER JOIN APP_WATERCRAFT_ENDORSEMENTS  VE ON                
		ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID
		AND VE.BOAT_ID = @BOAT_ID  
		AND VE.CUSTOMER_ID = @CUSTOMER_ID
		AND VE.APP_ID = @APP_ID 
		AND VE.APP_VERSIOn_ID = @APP_VERSION_ID             
	WHERE ED.STATE_ID = @STATE_ID
		AND ED.LOB_ID = @LOB_ID 
		AND ED.ENDORS_ASSOC_COVERAGE = 'Y'
		AND ED.IS_ACTIVE='Y'              
		AND @APP_EFFECTIVE_DATE BETWEEN ISNULl(ED.EFFECTIVE_FROM_DATE,'1950-1-1') 
					and     ISNULL(ED.EFFECTIVE_TO_DATE,'3000-12-12')
		AND @APP_EFFECTIVE_DATE <=  ISNULL(ED.DISABLED_DATE,'3000-12-12')
		OR 
		(
			VE.ENDORSEMENT_ID is not null and ED.ENDORS_ASSOC_COVERAGE = 'Y' 
		)
	ORDER BY ED.RANK   
                
                      
--Table 3                  
--Get the State for the application                  
EXEC Proc_GetApplicationState @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID                  
                  
--Table 3                        
--Get the Watercraft info from APP_WATERCRAFT_INFO          
SELECT TYPE_OF_WATERCRAFT,        
 LENGTH,        
 INSURING_VALUE,        
 YEAR,
 TYPE,
 COV_TYPE_BASIS         
FROM APP_WATERCRAFT_INFO INNER JOIN  MNT_LOOKUP_VALUES ON
    APP_WATERCRAFT_INFO.TYPE_OF_WATERCRAFT=MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID
WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                              
   APP_ID = @APP_ID AND                              
   APP_VERSION_ID = @APP_VERSION_ID  AND          
   BOAT_ID = @BOAT_ID           
                          
                        
-- for addtion date                    
select ENDORSEMENT_ATTACH_ID,ENDORSEMENT_ID,ATTACH_FILE,VALID_DATE EFFECTIVE_FROM_DATE,EFFECTIVE_TO_DATE,DISABLED_DATE,FORM_NUMBER,
 EDITION_DATE from MNT_ENDORSEMENT_ATTACHMENT 
   where    VALID_DATE <= @APP_EFFECTIVE_DATE  AND   @APP_EFFECTIVE_DATE<ISNULL(EFFECTIVE_TO_DATE,'3000-01-01')       
                        
                      
                    

                
              
            
          
        
      
    
  
  













GO

