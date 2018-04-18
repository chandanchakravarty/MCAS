IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWatercraftRule_GenInfo_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWatercraftRule_GenInfo_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                                                    
Proc Name                : Dbo.Proc_GetWatercraftRule_GenInfo_Pol  920,56,1                                               
Created by               : Ashwani                                                
Date                     : 02 Mar 2006                
Purpose                  : To get the Watercraft Gen Information for policy rules                                               
Revison History          :                                                    
Used In                  : Wolverine                                                    
------------------------------------------------------------                                                    
Date     Review By          Comments                                                    
------   ------------       -------------------------*/                 
-- DROP PROC dbo.Proc_GetWatercraftRule_GenInfo_Pol 
CREATE PROCEDURE dbo.Proc_GetWatercraftRule_GenInfo_Pol        
(                                                    
@CUSTOMER_ID    int,  
@POLICY_ID    int,  
@POLICY_VERSION_ID   int  
)                                                    
as                                                        
begin                     
	--1                    
	--DECLARE @HAS_CURR_ADD_THREE_YEARS CHAR  
	--DECLARE @HAS_CURR_ADD_THREE_YEARS_DESC VARCHAR(50)  
	--2                     
	DECLARE @PHY_MENTL_CHALLENGED CHAR                                          
	DECLARE @PHY_MENTL_CHALLENGED_DESC VARCHAR(50)                    
	--3                    
	DECLARE @DRIVER_SUS_REVOKED CHAR                     
	DECLARE @DRIVER_SUS_REVOKED_DESC VARCHAR(50)                    
	--4                    
	DECLARE @IS_CONVICTED_ACCIDENT CHAR                                          
	DECLARE @IS_CONVICTED_ACCIDENT_DESC VARCHAR(50)                    
	--5                   
	DECLARE @DRINK_DRUG_VOILATION CHAR                          
	--6                     
	DECLARE @MINOR_VIOLATION CHAR                                          
	--7                    
	--DECLARE @ANY_OTH_INSU_COMP CHAR                                          
	--DECLARE @OTHER_POLICY_NUMBER_LIST VARCHAR(50)                
	--8                     
	DECLARE @ANY_LOSS_THREE_YEARS CHAR                                          
	DECLARE @ANY_LOSS_THREE_YEARS_DESC VARCHAR(50)                    
	--9                    
	DECLARE @COVERAGE_DECLINED CHAR                
	DECLARE @COVERAGE_DECLINED_DESC VARCHAR(50)                 
	--10                     
	--DECLARE @IS_CREDIT CHAR                       
	--DECLARE @CREDIT_DETAILS VARCHAR(50)                    
	--11                    
	DECLARE @IS_RENTED_OTHERS CHAR                                          
	DECLARE  @IS_RENTED_OTHERS_DESC VARCHAR(50)                    
	--12                    
	DECLARE @IS_REGISTERED_OTHERS CHAR                                          
	DECLARE @IS_REGISTERED_OTHERS_DESC VARCHAR(50)                    
	--13                    
	DECLARE @PARTICIPATE_RACE NCHAR(1)                      
	DECLARE @PARTICIPATE_RACE_DESC VARCHAR(50)                    
	--14                    
	DECLARE @CARRY_PASSENGER_FOR_CHARGE CHAR                
	DECLARE @CARRY_PASSENGER_FOR_CHARGE_DESC VARCHAR(50)                    
	--15                  
	DECLARE @IS_PRIOR_INSURANCE_CARRIER CHAR              
	DECLARE @PRIOR_INSURANCE_CARRIER_DESC VARCHAR(50)               
	         
	-- 17       
	DECLARE @IS_BOAT_COOWNED NCHAR(1)      
	DECLARE @IS_BOAT_COOWNED_DESC NVARCHAR(100)      
	  
	--18               
	DECLARE @ANY_BOAT_AMPHIBIOUS NCHAR(1)                
	DECLARE @ANY_BOAT_AMPHIBIOUS_DESC NVARCHAR(100)      
	  
	--19  
	DECLARE @MULTI_POLICY_DISC_APPLIED NCHAR(1)                
	DECLARE @MULTI_POLICY_DISC_APPLIED_DESC NVARCHAR(100)    
	        
	--20        
	DECLARE @ANY_BOAT_RESIDENCE NCHAR(1)  
	DECLARE @ANY_BOAT_RESIDENCE_DESC NVARCHAR(100)           
	--21 
	DECLARE @IS_BOAT_USED_IN_ANY_WATER NCHAR(1)
	DECLARE @IS_BOAT_USED_IN_ANY_WATER_DESC NVARCHAR(100)
	
	DECLARE @IS_RECORD_EXISTS CHAR           
                
                   
IF EXISTS(SELECT CUSTOMER_ID FROM POL_WATERCRAFT_GEN_INFO WHERE        
 CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)                                  
BEGIN                                   
SET @IS_RECORD_EXISTS='N'          
SELECT                   
--MANDATORY FIELDS                 
                 
	@PHY_MENTL_CHALLENGED=isnull(PHY_MENTL_CHALLENGED,''),@PHY_MENTL_CHALLENGED_DESC=isnull(PHY_MENTL_CHALLENGED_DESC,''),                    
	@DRIVER_SUS_REVOKED =isnull(DRIVER_SUS_REVOKED,''),@DRIVER_SUS_REVOKED_DESC =isnull(DRIVER_SUS_REVOKED_DESC,''),                    
	@IS_CONVICTED_ACCIDENT =isnull(IS_CONVICTED_ACCIDENT,''),@IS_CONVICTED_ACCIDENT_DESC=isnull(IS_CONVICTED_ACCIDENT_DESC,''),                
	@DRINK_DRUG_VOILATION =isnull(DRINK_DRUG_VOILATION,''),@MINOR_VIOLATION =isnull(MINOR_VIOLATION,''),                    
	--@ANY_OTH_INSU_COMP =isnull(ANY_OTH_INSU_COMP,''),@OTHER_POLICY_NUMBER_LIST =isnull(OTHER_POLICY_NUMBER_LIST,''),                    
	@ANY_LOSS_THREE_YEARS =isnull(ANY_LOSS_THREE_YEARS ,''),@ANY_LOSS_THREE_YEARS_DESC =isnull(ANY_LOSS_THREE_YEARS_DESC,''),                    
	@COVERAGE_DECLINED =isnull(COVERAGE_DECLINED,''),@COVERAGE_DECLINED_DESC =isnull(COVERAGE_DECLINED_DESC,''),                    
	--@IS_CREDIT =isnull(IS_CREDIT,''),@CREDIT_DETAILS =isnull(CREDIT_DETAILS,''),                    
	@IS_RENTED_OTHERS =isnull(IS_RENTED_OTHERS,''),@IS_RENTED_OTHERS_DESC =isnull(IS_RENTED_OTHERS_DESC,''),                    
	@IS_REGISTERED_OTHERS =isnull(IS_REGISTERED_OTHERS,''),@IS_REGISTERED_OTHERS_DESC =isnull(IS_REGISTERED_OTHERS_DESC,''),                    
	@PARTICIPATE_RACE =isnull(PARTICIPATE_RACE,''),@PARTICIPATE_RACE_DESC =isnull(PARTICIPATE_RACE_DESC ,''),                    
	@CARRY_PASSENGER_FOR_CHARGE =isnull(CARRY_PASSENGER_FOR_CHARGE,''),@CARRY_PASSENGER_FOR_CHARGE_DESC =isnull(CARRY_PASSENGER_FOR_CHARGE_DESC ,''),                    
	@IS_PRIOR_INSURANCE_CARRIER=isnull(IS_PRIOR_INSURANCE_CARRIER,''),@PRIOR_INSURANCE_CARRIER_DESC=isnull(PRIOR_INSURANCE_CARRIER_DESC,''),              
	@IS_BOAT_COOWNED=isnull(IS_BOAT_COOWNED,''),@IS_BOAT_COOWNED_DESC=isnull(IS_BOAT_COOWNED_DESC,'') ,   
	@ANY_BOAT_AMPHIBIOUS=isnull(ANY_BOAT_AMPHIBIOUS,''),@ANY_BOAT_AMPHIBIOUS_DESC=isnull(ANY_BOAT_AMPHIBIOUS_DESC,''),  
	@MULTI_POLICY_DISC_APPLIED=isnull(MULTI_POLICY_DISC_APPLIED,''),@MULTI_POLICY_DISC_APPLIED_DESC=isnull(MULTI_POLICY_DISC_APPLIED_PP_DESC,''),  
	@ANY_BOAT_RESIDENCE=isnull(ANY_BOAT_RESIDENCE,''),@ANY_BOAT_RESIDENCE_DESC=isnull(ANY_BOAT_RESIDENCE_DESC,''),
	@IS_BOAT_USED_IN_ANY_WATER=isnull(IS_BOAT_USED_IN_ANY_WATER,''),
	@IS_BOAT_USED_IN_ANY_WATER_DESC=isnull(IS_BOAT_USED_IN_ANY_WATER_DESC,'')     
fROM POL_WATERCRAFT_GEN_INFO                    
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                
END                                   
ELSE                                  
BEGIN                                   
	SET @IS_RECORD_EXISTS='Y'          
	--1                    
	--SET @HAS_CURR_ADD_THREE_YEARS  =''                         
	--SET @HAS_CURR_ADD_THREE_YEARS_DESC  =''                     
	--2                     
	SET @PHY_MENTL_CHALLENGED  =''                     
	SET @PHY_MENTL_CHALLENGED_DESC  =''                     
	--3                    
	SET @DRIVER_SUS_REVOKED  =''       -- IF 'Y'  SHOULD BE REFERRED TO UNDERWRITER                      
	SET @DRIVER_SUS_REVOKED_DESC  =''                     
	--4                    
	SET @IS_CONVICTED_ACCIDENT  =''                     
	SET @IS_CONVICTED_ACCIDENT_DESC  =''                     
	--5                    
	SET @DRINK_DRUG_VOILATION =''     
	--6        
	SET @MINOR_VIOLATION  =''                     
	--7                     
	--SET @ANY_OTH_INSU_COMP  =''                     
	--SET @OTHER_POLICY_NUMBER_LIST  =''                     
	--8     
	SET @ANY_LOSS_THREE_YEARS  =''           
	SET @ANY_LOSS_THREE_YEARS_DESC  =''                     
	--9                     
	SET @COVERAGE_DECLINED  =''                     
	SET @COVERAGE_DECLINED_DESC  =''                     
	--10                    
	--SET @IS_CREDIT  =''                     
	--SET @CREDIT_DETAILS  =''                     
	--11                    
	SET @IS_RENTED_OTHERS  =''                     
	SET @IS_RENTED_OTHERS_DESC  =''                     
	--12                    
	SET @IS_REGISTERED_OTHERS  =''                     
	SET @IS_REGISTERED_OTHERS_DESC  =''                     
	--13                    
	SET @PARTICIPATE_RACE  =''                     
	SET @PARTICIPATE_RACE_DESC  =''                     
	--14                    
	SET @CARRY_PASSENGER_FOR_CHARGE  =''                     
	SET @CARRY_PASSENGER_FOR_CHARGE_DESC  =''                   
	--15                  
	SET @IS_PRIOR_INSURANCE_CARRIER =''              
	SET @PRIOR_INSURANCE_CARRIER_DESC =''              
	--16        
	SET @IS_BOAT_COOWNED_DESC =''      
	SET @IS_BOAT_COOWNED=''      
	--17  
	SET @ANY_BOAT_AMPHIBIOUS=''              
	SET @ANY_BOAT_AMPHIBIOUS_DESC=''                     
	--18  
	SET @MULTI_POLICY_DISC_APPLIED=''  
	SET @MULTI_POLICY_DISC_APPLIED_DESC=''  
	--19        
	SET @ANY_BOAT_RESIDENCE=''        
	SET @ANY_BOAT_RESIDENCE_DESC=''        
	--20
	SET @IS_BOAT_USED_IN_ANY_WATER_DESC=''
	SET @IS_BOAT_USED_IN_ANY_WATER=''                
                
END                         
                    
-- 1                                           
--if(@HAS_CURR_ADD_THREE_YEARS='1')                
--begin                                           
--set @HAS_CURR_ADD_THREE_YEARS='Y'                
--end                                
--else if(@HAS_CURR_ADD_THREE_YEARS='0')                                          
--begin                                           
--set @HAS_CURR_ADD_THREE_YEARS='N'                                          
--end                                 
-- 2                   
IF(@PHY_MENTL_CHALLENGED='1')                                          
	BEGIN                                           
	SET @PHY_MENTL_CHALLENGED ='Y'                           
	END                                
ELSE IF(@PHY_MENTL_CHALLENGED ='0')                                          
	BEGIN                                           
	SET @PHY_MENTL_CHALLENGED ='N'                                          
	END                              
-- 3                    
IF(@DRIVER_SUS_REVOKED='1')                                          
	BEGIN                                           
	SET @DRIVER_SUS_REVOKED='Y'                                          
	END          
ELSE IF(@DRIVER_SUS_REVOKED='0')                                          
	BEGIN                                           
	SET @DRIVER_SUS_REVOKED='N'                                          
	END                              
-- 4                    
IF(@IS_CONVICTED_ACCIDENT ='1')                                          
	BEGIN                                           
	SET @IS_CONVICTED_ACCIDENT ='Y'                                          
	END                                
ELSE IF(@IS_CONVICTED_ACCIDENT ='0')                                          
	BEGIN                                           
	SET @IS_CONVICTED_ACCIDENT ='N'                                          
	END                     
-- 5                    
IF(@DRINK_DRUG_VOILATION ='1')                                          
	BEGIN      
	SET @DRINK_DRUG_VOILATION ='Y'                                          
	END             
ELSE IF(@DRINK_DRUG_VOILATION ='0')                      
	BEGIN                                           
	SET @DRINK_DRUG_VOILATION ='N'                                          
	END                     
-- 6                                  
IF(@MINOR_VIOLATION ='1')     
	BEGIN     
	SET @MINOR_VIOLATION ='Y'                                          
	END                                
ELSE IF(@MINOR_VIOLATION ='0')                                          
	BEGIN                                           
	SET @MINOR_VIOLATION ='N'                                   
	END                                    
-- 7                    
/*if(@ANY_OTH_INSU_COMP ='1')                        
begin                                           
set @ANY_OTH_INSU_COMP='Y'                                          
end                                
else if(@ANY_OTH_INSU_COMP='0')                                          
begin                                           
set @ANY_OTH_INSU_COMP='N'                                          
end  */                  
-- 8                    
IF(@ANY_LOSS_THREE_YEARS ='1')                                          
	BEGIN                                           
	SET @ANY_LOSS_THREE_YEARS ='Y'                                          
	END                                
ELSE IF(@ANY_LOSS_THREE_YEARS ='0')                                          
	BEGIN                                           
	SET @ANY_LOSS_THREE_YEARS ='N'                                          
	END                              
--  9                       
IF(@COVERAGE_DECLINED ='1')                                          
	BEGIN                                           
	SET @COVERAGE_DECLINED ='Y'                                          
	END                            
ELSE IF(@COVERAGE_DECLINED ='0')                                          
	BEGIN                                           
	SET @COVERAGE_DECLINED ='N'                                          
	END                      
--10                    
--if(@IS_CREDIT ='1')                                          
--begin                                           
--set @IS_CREDIT ='Y'                                          
--end                                
--else if(@IS_CREDIT ='0')                                      
--begin                                           
--set @IS_CREDIT ='N'                                          
--end                           
-- 11                                          
IF(@IS_RENTED_OTHERS ='1')                                   
	BEGIN                                           
	SET @IS_RENTED_OTHERS ='Y'                           
	END                                
ELSE IF(@IS_RENTED_OTHERS ='0')                                          
	BEGIN                                           
	SET @IS_RENTED_OTHERS ='N'                     
	END                                
--12                          
IF(@IS_REGISTERED_OTHERS  ='1')                                   
	BEGIN                                           
	SET @IS_REGISTERED_OTHERS  ='Y'                           
	END                                
ELSE IF(@IS_REGISTERED_OTHERS  ='0')                                          
	BEGIN                                           
	SET @IS_REGISTERED_OTHERS  ='N'                                          
	END                                     
--13 
--Type of watercraft is Sailboat (11372)        
    
IF EXISTS(SELECT TYPE_OF_WATERCRAFT FROM POL_WATERCRAFT_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID   
AND  POLICY_VERSION_ID=@POLICY_VERSION_ID AND (TYPE_OF_WATERCRAFT<>11372 and TYPE_OF_WATERCRAFT<>11672) AND LENGTH<26)     
BEGIN                
IF(@PARTICIPATE_RACE='1')                  
	BEGIN       
	SET @PARTICIPATE_RACE ='Y'                           
	END                                
ELSE IF(@PARTICIPATE_RACE='0')                                          
	BEGIN                                           
	SET @PARTICIPATE_RACE='N'                                          
	END 
END                                    
--14   
IF(@CARRY_PASSENGER_FOR_CHARGE ='1')                                   
	BEGIN                                           
	SET @CARRY_PASSENGER_FOR_CHARGE  ='Y'                           
	END                                
ELSE IF(@CARRY_PASSENGER_FOR_CHARGE ='0')  
	BEGIN                            
	SET @CARRY_PASSENGER_FOR_CHARGE ='N'                 
	END              
--15              
IF(@IS_PRIOR_INSURANCE_CARRIER ='1')                                   
	BEGIN                                           
	SET @IS_PRIOR_INSURANCE_CARRIER  ='Y'                           
	END                                
ELSE IF(@IS_PRIOR_INSURANCE_CARRIER ='0')                                          
	BEGIN                                           
	SET @IS_PRIOR_INSURANCE_CARRIER ='N'         
	END       
  
--16              
IF(@ANY_BOAT_AMPHIBIOUS='1')                            
BEGIN                                                     
SET @ANY_BOAT_AMPHIBIOUS ='Y'                                     
END                                          
ELSE IF(@ANY_BOAT_AMPHIBIOUS='0')                                                    
BEGIN                                                     
SET @ANY_BOAT_AMPHIBIOUS='N'                                                    
END    
  
--17            
               
IF(@MULTI_POLICY_DISC_APPLIED='1')                            
	BEGIN                                                     
	SET @MULTI_POLICY_DISC_APPLIED ='Y'                                     
	END                                          
ELSE IF(@MULTI_POLICY_DISC_APPLIED='0')                                                    
	BEGIN                                                     
	SET @MULTI_POLICY_DISC_APPLIED='N'                                                    
	END     
  
--19 Boat residence        
        
IF(@ANY_BOAT_RESIDENCE='1')                            
	BEGIN                                                     
	SET @ANY_BOAT_RESIDENCE ='Y'                                     
	END                                          
ELSE IF(@ANY_BOAT_RESIDENCE='0')                                                    
	BEGIN                                                    
	SET @ANY_BOAT_RESIDENCE='N'                                                    
	END    

--20 Boat used in any water Description      
          
IF(@IS_BOAT_USED_IN_ANY_WATER='1')                              
	BEGIN                                                       
	SET @IS_BOAT_USED_IN_ANY_WATER  ='Y'                                       
	END                                         
ELSE IF(@IS_BOAT_USED_IN_ANY_WATER ='0')                                                      
	BEGIN                                                      
	SET @IS_BOAT_USED_IN_ANY_WATER ='N'                                                      
	END    
--20 Boat cowned
IF(@IS_BOAT_COOWNED='1')                              
	BEGIN                                                       
	SET @IS_BOAT_COOWNED ='Y'                                       
	END                                            
ELSE IF(@IS_BOAT_COOWNED='0')                                                      
	BEGIN                                                       
	SET @IS_BOAT_COOWNED='N'                                                      
	END 		
--=====================================================
--=============================== Itrack No. 3593 ===========================

   DECLARE @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS VARCHAR
   SET @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='N'
	
   IF(@MULTI_POLICY_DISC_APPLIED='1' OR @MULTI_POLICY_DISC_APPLIED='Y')
   BEGIN
		DECLARE @POLICY_LOB INT,
			@POLICY_NUMBER VARCHAR(100),
			@COUNT INT,
		        @MULTI_POLICY_DISC_APPLIED_PP_DESC VARCHAR(100),
			@BASE_POLICY_VERSION_ID INT,  
	  		@NEW_POLICY_VERSION_ID INT ,
			@PROCESS_ID  INT,
			@COUNT_POL_STATUS INT,
			@POLICY_STATUS varchar(20),
			@COUNT_POLICY_NUMBER INT 

		SELECT @POLICY_LOB=POLICY_LOB,
		       @POLICY_NUMBER=POLICY_NUMBER,
		       @MULTI_POLICY_DISC_APPLIED_PP_DESC=SUBSTRING(MULTI_POLICY_DISC_APPLIED_PP_DESC,0,9),
		       @POLICY_STATUS=POLICY_STATUS  FROM POL_CUSTOMER_POLICY_LIST POL
		INNER JOIN POL_WATERCRAFT_GEN_INFO  POL_WATER ON 	
		POL.CUSTOMER_ID=POL_WATER.CUSTOMER_ID AND POL.POLICY_ID=POL_WATER.POLICY_ID AND POL.POLICY_VERSION_ID=POL_WATER.POLICY_VERSION_ID
		WHERE POL.CUSTOMER_ID=@CUSTOMER_ID AND POL.POLICY_ID=@POLICY_ID AND POL.POLICY_VERSION_ID=@POLICY_VERSION_ID 

		SELECT @BASE_POLICY_VERSION_ID = MAX(POLICY_VERSION_ID),  
	       	       @NEW_POLICY_VERSION_ID  = MAX(NEW_POLICY_VERSION_ID)
		       FROM POL_POLICY_PROCESS WITH(NOLOCK)  
	        WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID 
		
		SELECT  @PROCESS_ID=PROCESS_ID
		FROM POL_POLICY_PROCESS WITH(NOLOCK)  
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@BASE_POLICY_VERSION_ID
		
		SELECT @COUNT_POL_STATUS=COUNT(POLICY_STATUS)  FROM POL_CUSTOMER_POLICY_LIST 
		WHERE POLICY_NUMBER = @MULTI_POLICY_DISC_APPLIED_PP_DESC AND POLICY_STATUS = 'INACTIVE'
		SELECT @COUNT_POLICY_NUMBER=COUNT(POLICY_NUMBER)  FROM POL_CUSTOMER_POLICY_LIST 
		WHERE POLICY_NUMBER = @MULTI_POLICY_DISC_APPLIED_PP_DESC 

		-- To Select All Eligible Policy 
		--Homeowners - 1 Automobile - 2 Motorcycle - 3	Watercraft - 4	Umbrella   - 5	Rental     - 6	General Liability - 7
                
		  IF (@POLICY_LOB IN (2,3,4))                 
		  BEGIN                
			SELECT   @COUNT=COUNT(POLICY_NUMBER) FROM POL_CUSTOMER_POLICY_LIST, MNT_AGENCY_LIST                
			WHERE CUSTOMER_ID = @CUSTOMER_ID     
			AND POL_CUSTOMER_POLICY_LIST.AGENCY_ID = MNT_AGENCY_LIST.AGENCY_ID             
			AND POLICY_NUMBER <>  @POLICY_NUMBER            
			AND POL_CUSTOMER_POLICY_LIST.IS_ACTIVE = 'Y' AND APP_EXPIRATION_DATE > GETDATE() AND POLICY_STATUS = 'NORMAL'          
			AND POLICY_LOB = 1 -- ORDER BY POLICY_NUMBER              
			AND POLICY_NUMBER!=@MULTI_POLICY_DISC_APPLIED_PP_DESC
		  END      
		 
		-- Umbrella and Rental Dwelling
		  ELSE IF (@POLICY_LOB=5 OR @POLICY_LOB=7)                
		  BEGIN
			
			SET @COUNT = -1
			
		  END 

	-->>1: ==>>>NEW BUSINESS AND REWRITE 
		--If there is a yes in the Field Is multi-policy discount applied?* 
		--and there are no Eligible policies - make sure that 
		--there are details in the field Multi-policy discount description 
		--if there are detail allow the discount 
		
		IF(@COUNT >0 AND ( @PROCESS_ID IN(24,25,31,32)) )
		BEGIN 
			SET @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'
		END
	-->>2: ==>>>RENEWAL 
		--If there are no Eligible Policies and there is Yes in the Field multi-policy discount applied?*
		--program to see if the policy number in the Field Multi-policy discount description is active 
		--If policy is not active or does not exist on the database
		--This will goes as a Refer to Underwriters
		--	Note for the referral - Multi Policy Discount Eligibility
		--	If referral is accepted - allow discount		
		
		ELSE IF(@COUNT=0  AND (@COUNT_POL_STATUS>0 OR @COUNT_POLICY_NUMBER=0)  AND (@PROCESS_ID IN(5,18)))
		BEGIN 
			SET @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'
		END
		ELSE IF(@COUNT=0)
		BEGIN 
			SET @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'
		END
    END

--================ End =======================================  
                  
SELECT                
	--1                    
	--@HAS_CURR_ADD_THREE_YEARS  as HAS_CURR_ADD_THREE_YEARS ,                
	--case @HAS_CURR_ADD_THREE_YEARS                
	--when 'Y'then  @HAS_CURR_ADD_THREE_YEARS_DESC    
	--end as HAS_CURR_ADD_THREE_YEARS_DESC,                    
	--2                     
	@PHY_MENTL_CHALLENGED  AS PHY_MENTL_CHALLENGED ,                    
	CASE @PHY_MENTL_CHALLENGED                     
	WHEN 'Y'THEN  @PHY_MENTL_CHALLENGED_DESC                      
	END AS PHY_MENTL_CHALLENGED_DESC ,                    
	--3                    
	@DRIVER_SUS_REVOKED AS DRIVER_SUS_REVOKED ,                    
	CASE @DRIVER_SUS_REVOKED                
	WHEN 'Y'THEN  @DRIVER_SUS_REVOKED_DESC                      
	END AS DRIVER_SUS_REVOKED_DESC,                    
	--4                    
	@IS_CONVICTED_ACCIDENT  AS IS_CONVICTED_ACCIDENT,                    
	CASE @IS_CONVICTED_ACCIDENT                     
	WHEN 'Y'THEN  @IS_CONVICTED_ACCIDENT_DESC                      
	END AS IS_CONVICTED_ACCIDENT_DESC ,                    
	--5          
	@DRINK_DRUG_VOILATION  AS DRINK_DRUG_VOILATION ,                    
	--6                     
	@MINOR_VIOLATION AS MINOR_VIOLATION ,                    
	--7                    
	/*@ANY_OTH_INSU_COMP  AS ANY_OTH_INSU_COMP ,                    
	CASE @ANY_OTH_INSU_COMP                     
	WHEN 'Y'THEN  @OTHER_POLICY_NUMBER_LIST                      
	END AS OTHER_POLICY_NUMBER_LIST ,*/                    
	--8                     
	@ANY_LOSS_THREE_YEARS  AS ANY_LOSS_THREE_YEARS ,                    
	CASE @ANY_LOSS_THREE_YEARS                    
	WHEN 'Y'THEN  @ANY_LOSS_THREE_YEARS_DESC                      
	END AS ANY_LOSS_THREE_YEARS_DESC ,                    
	--9            
	@COVERAGE_DECLINED  AS COVERAGE_DECLINED ,                     
	CASE @COVERAGE_DECLINED                     
	WHEN 'Y'THEN  @COVERAGE_DECLINED_DESC                      
	END AS COVERAGE_DECLINED_DESC ,                    
	--10                     
	--@IS_CREDIT  AS IS_CREDIT  ,                    
	--CASE @IS_CREDIT                     
	--WHEN 'Y'THEN  @CREDIT_DETAILS                      
	--END AS CREDIT_DETAILS ,                    
	--11                    
	@IS_RENTED_OTHERS  AS IS_RENTED_OTHERS ,                    
	CASE @IS_RENTED_OTHERS                     
	WHEN 'Y'THEN  @IS_RENTED_OTHERS_DESC                      
	END AS IS_RENTED_OTHERS_DESC ,                    
	--MANADATORY FIELDS             
	--12        
	@IS_REGISTERED_OTHERS  AS IS_REGISTERED_OTHERS  ,                    
	CASE @IS_REGISTERED_OTHERS                     
	WHEN '1'THEN  @IS_REGISTERED_OTHERS_DESC                      
	END AS IS_REGISTERED_OTHERS_DESC,                    
	--13                    
	@PARTICIPATE_RACE  AS PARTICIPATE_RACE ,                     
	CASE @PARTICIPATE_RACE                     
	WHEN '1'THEN  @PARTICIPATE_RACE_DESC                      
	END AS PARTICIPATE_RACE_DESC ,                    
	--14              
	@CARRY_PASSENGER_FOR_CHARGE  AS CARRY_PASSENGER_FOR_CHARGE ,                    
	CASE @CARRY_PASSENGER_FOR_CHARGE                     
	WHEN '1'THEN  @CARRY_PASSENGER_FOR_CHARGE_DESC                      
	END AS CARRY_PASSENGER_FOR_CHARGE_DESC ,                    
	--15                    
	@IS_PRIOR_INSURANCE_CARRIER  AS IS_PRIOR_INSURANCE_CARRIER ,                    
	CASE @IS_PRIOR_INSURANCE_CARRIER                     
	WHEN '1'THEN  @PRIOR_INSURANCE_CARRIER_DESC              
	END AS PRIOR_INSURANCE_CARRIER_DESC ,                 
	       
	--16      
	@IS_BOAT_COOWNED AS IS_BOAT_COOWNED,      
	CASE @IS_BOAT_COOWNED                     
	WHEN 'Y'THEN  @IS_BOAT_COOWNED_DESC              
	END AS IS_BOAT_COOWNED_DESC,      
	--      
	  
	--17              
	@ANY_BOAT_AMPHIBIOUS  AS ANY_BOAT_AMPHIBIOUS,                               
	CASE @ANY_BOAT_AMPHIBIOUS              
	WHEN '1'THEN  @ANY_BOAT_AMPHIBIOUS_DESC                                
	END AS ANY_BOAT_AMPHIBIOUS_DESC ,    
	  
	--18            
	@MULTI_POLICY_DISC_APPLIED  AS MULTI_POLICY_DISC_APPLIED,                    
	CASE @MULTI_POLICY_DISC_APPLIED          
	WHEN 'Y'THEN  @MULTI_POLICY_DISC_APPLIED_DESC                                
	END AS MULTI_POLICY_DISC_APPLIED_DESC ,     
	  
	--19        
	@ANY_BOAT_RESIDENCE AS ANY_BOAT_RESIDENCE,        
	CASE @ANY_BOAT_RESIDENCE              
	WHEN '1'THEN  @ANY_BOAT_RESIDENCE_DESC                                
	END AS ANY_BOAT_RESIDENCE_DESC , 
	--20
	@IS_BOAT_USED_IN_ANY_WATER  AS IS_BOAT_USED_IN_ANY_WATER ,          
	CASE @IS_BOAT_USED_IN_ANY_WATER                 
	WHEN '1'THEN  @IS_BOAT_USED_IN_ANY_WATER_DESC                                  
	END AS IS_BOAT_USED_IN_ANY_WATER_DESC ,  
	--  
	@IS_RECORD_EXISTS AS IS_RECORD_EXISTS,
	@MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS AS MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS            
                   
END  















GO

