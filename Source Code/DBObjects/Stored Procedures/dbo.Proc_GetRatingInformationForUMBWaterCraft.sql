IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForUMBWaterCraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForUMBWaterCraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                  
Proc Name           : Proc_GetRatingInformationForUmbrella                                                        
Created by          : Neeraj singh                                                                 
Date                : 06-11-2006                                                                  
Purpose             : To get the information for creating the input xml                                                                   
Revison History     :                                                                  
Used In             : Wolverine                                                                  
------------------------------------------------------------                                                                  
Date     Review By          Comments                                                                  
------   ------------       -------------------------*/               
--    
-- DROP PROC dbo.Proc_GetRatingInformationForUMBWaterCraft                  
CREATE PROC dbo.Proc_GetRatingInformationForUMBWaterCraft        
                                                                      
 @CUSTOMER_ID      	INT,                                                                      
 @ID         		INT,                                                                      
 @VERSION_ID    	INT,            
 @WATERCRAFT_ID 	INT,
 @DATA_ACCESS_POINT     INT,
 @POLICY_COMPANY	NVARCHAR(300)                                                                 
AS                                                               
BEGIN                                                                      
              
SET QUOTED_IDENTIFIER OFF               
DECLARE @BOATTYPE NVARCHAR(100)      
DECLARE @BOATSTYLE NVARCHAR(100)      
DECLARE @BOATTYPECODE NVARCHAR(100)              
DECLARE @WATERCRAFT_LENGTH  nvarchar(100)                    
DECLARE @WATERCRAFT_RATEDSPEED  nvarchar(100)                    
DECLARE @WATERCRAFT_WEIGHT  nvarchar(100)              
DECLARE @APP_AGENCY INT            
DECLARE @BOATID INT            
DECLARE @POLICY int  
DECLARE @APPLICTION int  
DECLARE @OTHERS int 


SET @POLICY =1  
SET @APPLICTION =2  
SET @OTHERS =3  

-- If fetched from policy
IF ( @DATA_ACCESS_POINT = @POLICY)  
BEGIN           
 
   	SELECT  
		@WATERCRAFT_RATEDSPEED    = CONVERT(VARCHAR(20),MAX_SPEED),              
       		@WATERCRAFT_WEIGHT    = CONVERT(VARCHAR(20),WATERCRAFT_HORSE_POWER),              
	        @WATERCRAFT_LENGTH    = LENGTH,        
      		@BOATTYPE  = ISNULL(LOOKUP_VALUE_DESC,''),      
      		@BOATTYPECODE=ISNULL(LOOKUP_VALUE_CODE,''),                                              
       		@BOATSTYLE = ISNULL(TYPE,'')                
      		FROM POL_WATERCRAFT_INFO WITH (NOLOCK) INNER JOIN MNT_LOOKUP_VALUES WITH (NOLOCK) ON TYPE_OF_WATERCRAFT = LOOKUP_UNIQUE_ID WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@ID AND POLICY_VERSION_ID=@VERSION_ID and POL_WATERCRAFT_INFO.IS_ACTIVE='Y' AND BOAT_ID=@WATERCRAFT_ID             
         	ORDER BY   BOAT_ID                        
 
END      

-- if fetched from application  
IF ( @DATA_ACCESS_POINT = @APPLICTION)  
BEGIN  
       SELECT       
        	@WATERCRAFT_RATEDSPEED    = CONVERT(VARCHAR(20),MAX_SPEED),              
         	@WATERCRAFT_WEIGHT    = CONVERT(VARCHAR(20),WATERCRAFT_HORSE_POWER),              
         	@WATERCRAFT_LENGTH    = LENGTH,        
        	@BOATTYPE  = ISNULL(LOOKUP_VALUE_DESC,''),      
        	@BOATTYPECODE=ISNULL(LOOKUP_VALUE_CODE,''),                                              
         	@BOATSTYLE = ISNULL(TYPE,'')                
        	FROM APP_WATERCRAFT_INFO WITH (NOLOCK) INNER JOIN MNT_LOOKUP_VALUES WITH (NOLOCK) ON TYPE_OF_WATERCRAFT = LOOKUP_UNIQUE_ID WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@ID AND APP_VERSION_ID=@VERSION_ID and APP_WATERCRAFT_INFO.IS_ACTIVE='Y' AND BOAT_ID=@WATERCRAFT_ID             
           	ORDER BY   BOAT_ID        
END   

-- if fetched from other agency policies   
IF ( @DATA_ACCESS_POINT = @OTHERS)  
BEGIN 
      SELECT       
      		@WATERCRAFT_RATEDSPEED    = CONVERT(VARCHAR(20),MAX_SPEED),              
       		@WATERCRAFT_WEIGHT    = CONVERT(VARCHAR(20),WATERCRAFT_HORSE_POWER),              
       		@WATERCRAFT_LENGTH    = LENGTH,        
      		@BOATTYPE  = ISNULL(LOOKUP_VALUE_DESC,''),      
      		@BOATTYPECODE=ISNULL(LOOKUP_VALUE_CODE,''),                                              
       		@BOATSTYLE = ISNULL(TYPE,'')                
      		FROM APP_UMBRELLA_WATERCRAFT_INFO AUWI WITH (NOLOCK) 
		INNER JOIN MNT_LOOKUP_VALUES WITH (NOLOCK) 
		ON TYPE_OF_WATERCRAFT = LOOKUP_UNIQUE_ID 
		INNER JOIN APP_UMBRELLA_UNDERLYING_POLICIES AUUP WITH (NOLOCK)
		ON AUWI.CUSTOMER_ID=AUUP.CUSTOMER_ID AND AUWI.APP_ID=AUUP.APP_ID AND AUWI.APP_VERSION_ID=AUUP.APP_VERSION_ID AND POLICY_COMPANY=@POLICY_COMPANY
		WHERE AUWI.CUSTOMER_ID = @CUSTOMER_ID AND AUWI.APP_ID=@ID AND AUWI.APP_VERSION_ID=@VERSION_ID 
		and AUWI.IS_ACTIVE='Y' AND BOAT_ID=@WATERCRAFT_ID             
		AND AUUP.POLICY_COMPANY=@POLICY_COMPANY
         	ORDER BY   BOAT_ID          
END      
      
END          
      
      
BEGIN            
SELECT      
 @BOATTYPE           		AS BOATTYPE,      
 @BOATTYPECODE           	AS BOATTYPECODE,      
 @BOATSTYLE           		AS BOATSTYLE,      
 @WATERCRAFT_LENGTH         	AS LENGTH,              
 @WATERCRAFT_RATEDSPEED     	AS RATEDSPEED,              
 @WATERCRAFT_WEIGHT         	AS HORSEPOWER              
END            
    
    
    
  









GO

