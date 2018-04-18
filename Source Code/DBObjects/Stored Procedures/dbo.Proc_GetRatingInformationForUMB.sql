IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForUMB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForUMB]
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
             
                              
CREATE     PROC dbo.Proc_GetRatingInformationForUMB                                                                    
(                                                                    
 @CUSTOMERID      INT,                                                                    
 @APPID         INT,                                                                    
 @APPVERSIONID    INT                                                    
                                                                    
)                                                                    
AS                                                                    
BEGIN                                                                    
            
SET QUOTED_IDENTIFIER OFF             
DECLARE @TERITORYCODES NVARCHAR(100)             
DECLARE @TERMFACTOR         nvarchar(100)            
DECLARE @QUOTEEFFDATE         nvarchar(100)                                                                    
DECLARE @QUOTEEXPDATE        nvarchar(100)               
DECLARE @LOB_ID          nvarchar(100)            
DECLARE @NEW_BUSINESS    nvarchar(100)            
DECLARE @EFFECTIVE_DATE         nvarchar(100)                  
DECLARE @COUNTYS          nvarchar(100)                  
DECLARE @UMBRELLA_LIMIT   nvarchar(100)                  
DECLARE @MATURE_DISCOUNT  nvarchar(100)             
                 
------------ UnderLyingPolicy-LiabilityLimit--------------------            
DECLARE @HOMEOWNERS_POLICY_LIMIT nvarchar(100)                  
DECLARE @DWELLINGFIRE_POLICY_LIMIT nvarchar(100)                  
DECLARE @WATERCRAFT_POLICY_LIMIT nvarchar(100)                  
DECLARE @PERSONALAUTOPOLICYLOWERLIMIT nvarchar(100)           
DECLARE @PERSONALAUTOPOLICYUPPERLIMIT nvarchar(100)                 
DECLARE @MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT nvarchar(100)                  
DECLARE @MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT nvarchar(100)                  
DECLARE @UNDERLYING_MOTORIST_LIMIT nvarchar(100)            
DECLARE @UNINSUREDMOTORISTCSL NVARCHAR(20)          
DECLARE @UNINSUREDMOOTRISTBIPLIT NVARCHAR(20)          
DECLARE @UNDERINSUREDMOTORISTCSL NVARCHAR(20)          
DECLARE @UNDERINSUREDMOTORISTBISPLIT NVARCHAR(20)          
                
------------- Personal_Exposer----------------------------------            
DECLARE @OFFICE_PROMISES  nvarchar(100)                  
DECLARE @RENTAL_DWELLING_UNITS  nvarchar(100)                  
------------- AutoMobile_Motor_Vehicle_Exposer------------------            
DECLARE @TOTALNUMER_OF_DRIVERS  nvarchar(100)                  
DECLARE @AUTOMOBILE       nvarchar(100)                  
DECLARE @MOTORHOMES      nvarchar(100)                  
DECLARE @MOTORCYCLES      nvarchar(100)                  
DECLARE @AUTOMOBILE_INEXPERIENCED_DRIVER nvarchar(100)                  
DECLARE @MOTORHOMES_INEXPERIENCED_DRIVER nvarchar(100)              
DECLARE @MOTORCYCLES_INEXPERIENCED_DRIVER nvarchar(100)                  
DECLARE @EXCLUDE_UNINSURED_MOTORIST nvarchar(100)                  
DECLARE @WATERCRATFT_ID   nvarchar(100)                  
DECLARE @WATERCRAFT_LENGTH  nvarchar(100)                  
DECLARE @WATERCRAFT_RATEDSPEED  nvarchar(100)                  
DECLARE @WATERCRAFT_WEIGHT  nvarchar(100)            
DECLARE @INCEPTIONDATE   varchar(100)                  
DECLARE @DRIVERAGEOTHERS     NVARCHAR(20)                        
DECLARE @APP_AGENCY smallint            
DECLARE @APP_NUMBER VARCHAR(20)            
DECLARE @APP_VERSION VARCHAR(20)            
DECLARE @STATENAME VARCHAR(20)            
DECLARE @QQ_NUMBER VARCHAR(20)           
DECLARE @INTERVAL INT            
DECLARE @QUOTEEFFDATES DATETIME



---- CONSTANTS
DECLARE @LOOKUPVALUE_YES int              
DECLARE @LOOKUPVALUE_NO int
DECLARE @MATURE_AGE_LL int
DECLARE @MATURE_AGE_UL int
DECLARE @STATE_ID_MICHIGAN int
DECLARE @STATE_ID_INDIANA int
DECLARE @STATE_ID_WISCONSIN int

SET @LOOKUPVALUE_YES =10963
SET @LOOKUPVALUE_NO =10964
SET @MATURE_AGE_LL =50
SET @MATURE_AGE_UL =70 
SET @STATE_ID_MICHIGAN =22
SET @STATE_ID_INDIANA =14
SET @STATE_ID_WISCONSIN =49

 



-------- START ---------            
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   
   
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
    -- Applicant lobid, satename,termfactor,quoteefeectivedate, quoteexperydate, countys, umbrellalimits, qqnumber, appnumber,appversionnumber          
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
--picking general information of umbrella app.

 DECLARE @STATENAMEID smallINT          
   SELECT                                                                     
   @LOB_ID  =  ISNULL(APP_LOB,0) ,            
   @TERMFACTOR   =  ISNULL(APP_TERMS,''),            
   @QUOTEEFFDATE   =  CONVERT(VARCHAR(10),APP_EFFECTIVE_DATE,101),                                                                        
   @QUOTEEXPDATE   =  CONVERT(VARCHAR(10),APP_EXPIRATION_DATE,101),            
   @APP_NUMBER  = APP_NUMBER,            
   @APP_VERSION  = APP_VERSION,          
   @STATENAMEID  = STATE_ID     ,
   @APP_AGENCY =  APP_AGENCY_ID             
   FROM                                             
   APP_LIST WITH (NOLOCK)WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID          
     
-- umbrella policy limit       
   SELECT @UMBRELLA_LIMIT = CONVERT(VARCHAR(20),POLICY_LIMITS,101)       -- check for umbrella limit    
   FROM APP_UMBRELLA_LIMITS WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID          
--county name             
   SELECT @COUNTYS  = COUNTY          
    FROM APP_UMBRELLA_REAL_ESTATE_LOCATION WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID          

-- state name of the umbrella policy
   SELECT @STATENAME = upper(STATE_NAME) FROM MNT_COUNTRY_STATE_LIST WHERE STATE_ID=@STATENAMEID          

-- TERITORY CODES          
  SELECT @TERITORYCODES = TERR --CONVERT(NVARCHAR(20),TERR,101)          
  FROM MNT_TERRITORY_CODES WHERE lobID=@LOB_ID AND STATE = @STATENAMEID          
      
      
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   -- OFFICE PROMISES AND RENTAL DWELLING UNIT  --default to 0 for the  time being           
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
SET @OFFICE_PROMISES  =0     
SET @RENTAL_DWELLING_UNITS  =0	
/*  
           
  SELECT            
   @OFFICE_PROMISES  = CONVERT(VARCHAR(20),NEED_OF_UNITS,101),            
   @RENTAL_DWELLING_UNITS  =    CONVERT(VARCHAR(20),DWELLING_NUMBER,101)            
  FROM            
   APP_DWELLINGS_INFO WITH (NOLOCK)            
  WHERE                                                                     
   CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID           
           
  */         

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      -- AUTO COVERAGES AND DWELLING FIRE AND MOTORCYCLES AND MOTORHOMES FOR WOLVERINE POLICY           
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- fetch records according to policies selected in underlying sch. policies
DECLARE @MOTORHOMESWITHWOLVERINE INT 
DECLARE @APP_LOBOTHER INT 
DECLARE @MOTORHOMESOTHERS INT  
DECLARE @MOTORCYCLESWITHEWOLVERINE INT  
DECLARE @MOTORCYCLESOTHERS INT 
DECLARE @INEXPERIENCEDRIVERAUTOWITHWOLVERINE INT
DECLARE @AUTOMOBILE_DRIVERWITHWOLVERINE INT
DECLARE @INEXPERIENCEDRIVERMOTORHOMEWITHWOLVERINE INT
DECLARE @INEXPERIENCEDRIVERMOTORCYCLEWITHWOLVERINE INT 
DECLARE @AUTOINEXPDRIVERAGEOTHERS INT  
DECLARE @MOTORHOMEINEXPDRIVEROTHERS INT  
DECLARE @MOTORCYCLEINEXPDRIVEROTHERS INT  
DECLARE @UNINSUREDMOOTRISTBIPLITREJECT NVARCHAR(20) 
DECLARE @UNDERINSUREDMOTORISTCSLREJECT NVARCHAR(20) 
DECLARE @UNDERINSUREDMOTORISTBISPLITREJECT NVARCHAR(20) 
DECLARE @UNINSUREDMOTORISTCSLREJECT NVARCHAR(20)  
DECLARE @TOTALNUMER_OF_DRIVERSOTHERS INT  
DECLARE @MOTORCYCLEDRIVERWITHWOLVERINE INT 
DECLARE @MOTORHOMEDRIVERWITHWOLVERINE INT
DECLARE @AUTOMOBILEWITHWOLVERINE INT   
DECLARE @TOTAL_EXPAUTO int       
DECLARE @TOTAL_EXPMOTORHOME int      
DECLARE @TOTAL_EXPMOTORCYCLE int        
DECLARE @AUTOUPPERLIMIT VARCHAR(20)         
DECLARE @AUTOLOWERLIMIT VARCHAR(20)    
DECLARE @AUTOPD VARCHAR(20)    
DECLARE @AUTOCSL VARCHAR(20)
DECLARE @MOTORPD VARCHAR(20)    
DECLARE @MOTORCSL VARCHAR(20)    
DECLARE @MOTORLOWERLIMIT VARCHAR(20)        
DECLARE @MOTORUPERLIMIT VARCHAR(20)           
DECLARE @VEHCLEIDAUTOPER INT          
DECLARE @VEHCLEIDAUTOCOM INT          
DECLARE @TOTAL_AGEAUTO INT          
DECLARE @VEHCLEIDMOTOR INT          
DECLARE @TOTAL_AGEMOTOR INT          
DECLARE @MOTOR_INEXPERIENCED_DRIVER INT          
DECLARE @TOTAL_AGEMOTOR_WOLVERINE INT          
DECLARE @AUTOMOBILE_DRIVER INT          
DECLARE @MOTORHOME_DRIVER INT           
DECLARE @MOTOR_DRIVER INT                          
DECLARE @CHECKPOLICYNUMBER nvarchar(20)          
DECLARE @APPCUSTOMERID int          
declare @POLID int          
DECLARE @POLVERSIONID smallint          
DECLARE @POL_NUMBER nvarchar(20)          
DECLARE @POL_VERSION NVARCHAR(20)          
DECLARE @POL_lob NVARCHAR (20)          
DECLARE @UMBRELLA_APPNUMBER nvarchar(150) 
DECLARE @MATUREAGEDISCOUNFORWOLVERINEVOILATIONS NVARCHAR(20)
DECLARE @MATUREAGEDISCOUNFORWOLVERINEAGE NVARCHAR(20)         
DECLARE @STATEID SMALLINT          
DECLARE APPNUMBER_CURSOR CURSOR FOR          
           


  -- for each policy in the underlying sch policy  screen
  SELECT POLICY_NUMBER FROM APP_UMBRELLA_UNDERLYING_POLICIES 
	WHERE CUSTOMER_ID = @CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID          
  

  -- loop for each policy   
  OPEN APPNUMBER_CURSOR           
  FETCH NEXT FROM  APPNUMBER_CURSOR INTO @UMBRELLA_APPNUMBER           

  WHILE @@FETCH_STATUS = 0      
    begin  
       
          -- CHECK FOR WOLVERINE POLICY
 	  if exists(select * FROM POL_CUSTOMER_POLICY_LIST            
	     WHERE SUBSTRING(POLICY_NUMBER,1,8) =  ltrim(rtrim(substring(@UMBRELLA_APPNUMBER,1,8) ))          
	     and POLICY_DISP_VERSION =  ltrim(rtrim(substring(@UMBRELLA_APPNUMBER,11,14) )))
	  begin
		-- FETCH POLICY DETAILS FOR WOLVERINE CUSTOMER
		select
		     @POLID = POLICY_ID,          
	       	     @POLVERSIONID = POLICY_VERSION_ID,          
		     @POL_NUMBER = POLICY_NUMBER,          
		     @POL_VERSION = POLICY_DISP_VERSION,          
		     @POL_lob = POLICY_LOB,          
		     @STATEID = STATE_ID          
		FROM POL_CUSTOMER_POLICY_LIST            
     		WHERE SUBSTRING(POLICY_NUMBER,1,8) =  ltrim(rtrim(substring(@UMBRELLA_APPNUMBER,1,8) ))          
		     and POLICY_DISP_VERSION =  ltrim(rtrim(substring(@UMBRELLA_APPNUMBER,11,14) ))
		
		-- CHECK FOR MATURE AGE DISCOUNT IN CASE OF DRIVER IS WITH WOLVERINE(VIOLATIONS)
		IF EXISTS (SELECT  * 
		 	   FROM  POL_DRIVER_DETAILS WITH (NOLOCK) 
				INNER JOIN POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID
			        WHERE POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=@CUSTOMERID          
				        AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=@POLID       
				        AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=@POLVERSIONID
					AND ISNULL(VIOLATIONS,@LOOKUPVALUE_YES) = @LOOKUPVALUE_NO  
					AND ((( DATEDIFF(YEAR,POL_DRIVER_DETAILS.DRIVER_DOB,POL_CUSTOMER_POLICY_LIST.APP_EFFECTIVE_DATE) > @MATURE_AGE_LL ) AND DATEDIFF(YEAR,POL_DRIVER_DETAILS.DRIVER_DOB,POL_CUSTOMER_POLICY_LIST.APP_EFFECTIVE_DATE) < @MATURE_AGE_UL )))
			SET @MATUREAGEDISCOUNFORWOLVERINEVOILATIONS = 'Y'
		ELSE
		 	SET @MATUREAGEDISCOUNFORWOLVERINEVOILATIONS = 'N'

		-- IF STATE IS MICHIGAN - DIVERT FOR COVERAGES FOR DIFFERENT LOBS
  	     if(@STATEID = @STATE_ID_MICHIGAN)  
	    	 begin          
 	    		IF (@POL_LOB = 1)  -- HOMEOWNERS WOLVERINE FOR MICIGAN
			       BEGIN          
			       	SELECT @HOMEOWNERS_POLICY_LIMIT= convert(varchar(20),ISNULL(LIMIT_1,0),101)  -- PERSONAL LIABILITY         
			       	FROM POL_DWELLING_SECTION_COVERAGES  
				WHERE CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID  AND COVERAGE_CODE_ID = 10             
			       END  
	     		IF (@POL_LOB = 2)  -- AUTO WOLVERINE FOR MICIGAN            
	      		       BEGIN          
			        SELECT                -- BI SPLIT LOWER LIMIT AND BI SPLIT UPPER LIMIT
			        @PERSONALAUTOPOLICYLOWERLIMIT = convert(varchar(20),ISNULL(LIMIT_1,0),101) , 
				@PERSONALAUTOPOLICYUPPERLIMIT = convert(varchar(20),ISNULL(LIMIT_2,0),101)             
			        FROM POL_VEHICLE_coverages   WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND (COVERAGE_CODE_ID =114 ) and VEHICLE_ID = 1
			     
			          
			 	SELECT          	-- PD LIMIT 
			        @AUTOPD = convert(varchar(20),ISNULL(LIMIT_1,0),101)             
			        FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND (COVERAGE_CODE_ID=115) and VEHICLE_ID = 1      
			  
			  	SELECT            	-- CSL LIMIT
			        @AUTOCSL = convert(varchar(20),ISNULL(LIMIT_1,0),101)       --CSL      
			        FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND (COVERAGE_CODE_ID=113) and VEHICLE_ID = 1      
			  
			  	-- NUMBER OF AUTOMOBILES (PERSONAL EXCLUDING MOTORHOME AND COMMERCIAL-PERSONAL UMBRELLA DOESNT CONSIDER THESE 2 IN AUTO)
	   		       SELECT @VEHCLEIDAUTOPER = COUNT(VEHICLE_ID)          
	                       FROM POL_VEHICLES  WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID  AND POLICY_VERSION_ID=@POLVERSIONID           
	       		       AND  (APP_VEHICLE_PERTYPE_ID IS NOT NULL) AND (APP_VEHICLE_PERTYPE_ID != 11336 )
					
				SET @VEHCLEIDAUTOCOM=0        
			                  
	    		        SET @AUTOMOBILEWITHWOLVERINE = @VEHCLEIDAUTOPER -- TOTAL NUMBER OF AUTO FOR MICIGAN
	            
	           
			     ------------------------------------------------------------
				-- AUTOMOBILE INEXPERIENCED DRIVER          
   				 -- NO OF driverS in the household under age 25 AND 
				 -- NO OF DIRVERS with less than three (3) years driving experience.			     
			     -------------------------------------------------------------

			
				SELECT -- CHECK INEXPERINCED DRIVER AUTO (PERSONAL AUTO EXCLUDING MOTORHOME AND COMMERCIAL-PERSONAL UMBRELLA DOESNT CONSIDER THESE 2 IN AUTO)
				 @INEXPERIENCEDRIVERAUTOWITHWOLVERINE= count(POL_DRIVER_DETAILS.DRIVER_ID) 
				FROM POL_DRIVER_DETAILS 
					INNER JOIN POL_DRIVER_ASSIGNED_VEHICLE ON
					POL_DRIVER_DETAILS.CUSTOMER_ID=POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID 
					AND POL_DRIVER_DETAILS.POLICY_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID  
					AND POL_DRIVER_DETAILS.POLICY_VERSION_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID 
					AND  POL_DRIVER_DETAILS.DRIVER_ID=POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID
				
					INNER JOIN POL_VEHICLES ON 
					POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID=POL_VEHICLES.CUSTOMER_ID 
					AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID=POL_VEHICLES.POLICY_ID  
					AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID=POL_VEHICLES.POLICY_VERSION_ID 
					AND  POL_DRIVER_ASSIGNED_VEHICLE.VEHICLE_ID=POL_VEHICLES.VEHICLE_ID
				
					INNER JOIN POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID
					WHERE POL_CUSTOMER_POLICY_LIST.customer_id=@CUSTOMERID
					and POL_CUSTOMER_POLICY_LIST.policy_id=@POLID
					and POL_CUSTOMER_POLICY_LIST.policy_version_id=@POLVERSIONID
					
					GROUP BY DRIVER_DOB,APP_EFFECTIVE_DATE,DATE_LICENSED ,POL_VEHICLES.APP_VEHICLE_PERTYPE_ID,POL_VEHICLES.APP_VEHICLE_COMCLASS_ID
					HAVING (( DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) <25) OR ( DATEDIFF(YEAR,DATE_LICENSED,APP_EFFECTIVE_DATE) <3))
						AND POL_VEHICLES.APP_VEHICLE_PERTYPE_ID != 11336 and 
						 isnull(POL_VEHICLES.APP_VEHICLE_COMCLASS_ID  ,0)=0

				

				SELECT -- CHECK INEXPERINCED DRIVER AUTO (PERSONAL AUTO EXCLUDING MOTORHOME AND COMMERCIAL-PERSONAL UMBRELLA DOESNT CONSIDER THESE 2 IN AUTO)
				 @INEXPERIENCEDRIVERAUTOWITHWOLVERINE= count(POL_DRIVER_DETAILS.DRIVER_ID) 
				FROM POL_DRIVER_DETAILS 
					INNER JOIN POL_DRIVER_ASSIGNED_VEHICLE ON
					POL_DRIVER_DETAILS.CUSTOMER_ID=POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID 
					AND POL_DRIVER_DETAILS.POLICY_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID  
					AND POL_DRIVER_DETAILS.POLICY_VERSION_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID 
					AND  POL_DRIVER_DETAILS.DRIVER_ID=POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID
				
					INNER JOIN POL_VEHICLES ON 
					POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID=POL_VEHICLES.CUSTOMER_ID 
					AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID=POL_VEHICLES.POLICY_ID  
					AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID=POL_VEHICLES.POLICY_VERSION_ID 
					AND  POL_DRIVER_ASSIGNED_VEHICLE.VEHICLE_ID=POL_VEHICLES.VEHICLE_ID
				
					INNER JOIN POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID
					WHERE POL_CUSTOMER_POLICY_LIST.customer_id=@CUSTOMERID
					and POL_CUSTOMER_POLICY_LIST.policy_id=@POLID
					and POL_CUSTOMER_POLICY_LIST.policy_version_id=@POLVERSIONID
					
					GROUP BY DRIVER_DOB,APP_EFFECTIVE_DATE,DATE_LICENSED ,POL_VEHICLES.APP_VEHICLE_PERTYPE_ID,POL_VEHICLES.APP_VEHICLE_COMCLASS_ID
					HAVING --(( DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) <25) OR ( DATEDIFF(YEAR,DATE_LICENSED,APP_EFFECTIVE_DATE) <3))
						 POL_VEHICLES.APP_VEHICLE_PERTYPE_ID != 11336 and 
						 isnull(POL_VEHICLES.APP_VEHICLE_COMCLASS_ID  ,0)=0

			     --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
			          -- NUMBER OF MOTOR HOMES FOR WOLVERINE MICIGAN
			     ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
			      
			       SELECT           
			        @MOTORHOMESWITHWOLVERINE = ISNULL(COUNT(VEHICLE_ID),0)          
			                  
			       FROM POL_VEHICLES           
			       WHERE CUSTOMER_ID=@CUSTOMERID          
			       AND POLICY_ID=@POLID          
			       AND POLICY_VERSION_ID=@POLVERSIONID           
			       AND APP_VEHICLE_PERTYPE_ID=11336           
                 
                

			     ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
			            -- MOTOR HOME INEXPERIENCED DRIVER          
			     ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
			  
			    
				SELECT 
				@INEXPERIENCEDRIVERMOTORHOMEWITHWOLVERINE=count(distinct(POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID)) -- CHECK INEXPERINCED DRIVER AUTO
				FROM POL_DRIVER_DETAILS right outer join
				 POL_DRIVER_ASSIGNED_VEHICLE ON
				 POL_DRIVER_DETAILS.CUSTOMER_ID=POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID 
				AND POL_DRIVER_DETAILS.POLICY_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID  
				AND POL_DRIVER_DETAILS.POLICY_VERSION_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID 
				AND  POL_DRIVER_DETAILS.DRIVER_ID=POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID
				
				RIGHT OUTER JOIN POL_VEHICLES ON 
			
				POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID=POL_VEHICLES.CUSTOMER_ID 
				AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID=POL_VEHICLES.POLICY_ID  
				AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID=POL_VEHICLES.POLICY_VERSION_ID 
				AND  POL_DRIVER_ASSIGNED_VEHICLE.VEHICLE_ID=POL_VEHICLES.VEHICLE_ID
	
				INNER JOIN  POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID
				WHERE POL_CUSTOMER_POLICY_LIST.customer_id=@CUSTOMERID
				and POL_CUSTOMER_POLICY_LIST.policy_id=@POLID
				and POL_CUSTOMER_POLICY_LIST.policy_version_id=@POLVERSIONID
				AND POL_VEHICLES.APP_VEHICLE_PERTYPE_ID != 11336
				AND (( DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) <25) OR ( DATEDIFF(YEAR,DATE_LICENSED,APP_EFFECTIVE_DATE) <3))
				   
			
				SELECT 
				@MOTORHOMEDRIVERWITHWOLVERINE=count(distinct(POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID)) -- CHECK TOTAL DRIVER AUTO
				FROM POL_DRIVER_DETAILS right outer join
				 POL_DRIVER_ASSIGNED_VEHICLE ON
				 POL_DRIVER_DETAILS.CUSTOMER_ID=POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID 
				AND POL_DRIVER_DETAILS.POLICY_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID  
				AND POL_DRIVER_DETAILS.POLICY_VERSION_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID 
				AND  POL_DRIVER_DETAILS.DRIVER_ID=POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID
				
				RIGHT OUTER JOIN POL_VEHICLES ON 
			
				POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID=POL_VEHICLES.CUSTOMER_ID 
				AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID=POL_VEHICLES.POLICY_ID  
				AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID=POL_VEHICLES.POLICY_VERSION_ID 
				AND  POL_DRIVER_ASSIGNED_VEHICLE.VEHICLE_ID=POL_VEHICLES.VEHICLE_ID
				
				INNER JOIN  POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID
				WHERE POL_CUSTOMER_POLICY_LIST.customer_id=@CUSTOMERID
				and POL_CUSTOMER_POLICY_LIST.policy_id=@POLID
				and POL_CUSTOMER_POLICY_LIST.policy_version_id=@POLVERSIONID
				AND POL_VEHICLES.APP_VEHICLE_PERTYPE_ID != 11336
				
				END
			    
		     IF (@POL_lob = 3)            -- MOTORCYCLE WOLVERINE FOR MICIGAN
		       BEGIN          
			        SELECT           
			        @MOTORLOWERLIMIT = convert(varchar(20),ISNULL(LIMIT_1,0),101),   -- BI SPLIT LOWER AND UPPER LIMIT FOR MICIGAN
				@MOTORUPERLIMIT = convert(varchar(20),ISNULL(LIMIT_2,0),101)             
			        FROM POL_VEHICLE_coverages  WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND (COVERAGE_CODE_ID=207 ) and VEHICLE_ID = 1        
			        
				SELECT           
			        @MOTORCSL = convert(varchar(20),ISNULL(LIMIT_1,0),101)             -- CSL LIMIT FOR WOLVERINE MOTORCYCLE POLICY
			        FROM POL_VEHICLE_coverages  WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND (COVERAGE_CODE_ID=206)  and VEHICLE_ID = 1       
			       
				SELECT           
			        @MOTORPD = convert(varchar(20),ISNULL(LIMIT_1,0),101)             -- PD LIMIT FOR WOLVERINE MOTOR CYCLE POLICY IN MICIGAN
			        FROM POL_VEHICLE_coverages  WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND (COVERAGE_CODE_ID=115)  and VEHICLE_ID = 1       
			       
				SELECT           
			        @UNINSUREDMOTORISTCSL= convert(varchar(20),ISNULL(LIMIT_1,0),101)           -- UNINSURED MOTORIST CSL FOR MICIGAN
			        FROM POL_VEHICLE_coverages   WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND COVERAGE_CODE_ID=211  and VEHICLE_ID = 1         
			        SELECT           
			        @UNINSUREDMOOTRISTBIPLIT= convert(varchar(20),ISNULL(LIMIT_2,0),101)           -- UNINSURED MOTORIST BISPLIT FOR MICIGAN
			        FROM POL_VEHICLE_coverages    WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND COVERAGE_CODE_ID=212  and VEHICLE_ID = 1         
			        SET @MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT = @MOTORLOWERLIMIT        
			        SET @MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT = @MOTORUPERLIMIT     
          

			   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
			        -- NUMBER OF MOTORCYCLES          
			    -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
			  
			      SELECT           
			       @MOTORCYCLESWITHEWOLVERINE = ISNULL(COUNT(VEHICLE_ID),'0')          
			                 
			      FROM POL_VEHICLES           
			      WHERE CUSTOMER_ID=@CUSTOMERID          
			      AND POLICY_ID=@POLID          
			      AND POLICY_VERSION_ID=@POLVERSIONID           
			      AND MOTORCYCLE_TYPE !=''           
                 
			  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
			        -- NUMBER OF INEXPERINCED DRIVER MOTORCYCLE          
			   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
			  
			 	SELECT 
				@INEXPERIENCEDRIVERMOTORCYCLEWITHWOLVERINE=count(distinct(POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID)) -- CHECK INEXPERINCED DRIVER AUTO
				FROM POL_DRIVER_DETAILS right outer join
				 POL_DRIVER_ASSIGNED_VEHICLE ON
				 POL_DRIVER_DETAILS.CUSTOMER_ID=POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID 
				AND POL_DRIVER_DETAILS.POLICY_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID  
				AND POL_DRIVER_DETAILS.POLICY_VERSION_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID 
				AND  POL_DRIVER_DETAILS.DRIVER_ID=POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID
				
				RIGHT OUTER JOIN POL_VEHICLES ON 
			
				POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID=POL_VEHICLES.CUSTOMER_ID 
				AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID=POL_VEHICLES.POLICY_ID  
				AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID=POL_VEHICLES.POLICY_VERSION_ID 
				AND  POL_DRIVER_ASSIGNED_VEHICLE.VEHICLE_ID=POL_VEHICLES.VEHICLE_ID
	
				INNER JOIN  POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID
				WHERE POL_CUSTOMER_POLICY_LIST.customer_id=1141
				and POL_CUSTOMER_POLICY_LIST.policy_id=14
				and POL_CUSTOMER_POLICY_LIST.policy_version_id=1
				AND POL_VEHICLES.MOTORCYCLE_TYPE != ''
				AND (( DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) <25) OR ( DATEDIFF(YEAR,DATE_LICENSED,APP_EFFECTIVE_DATE) <3))
			
				SELECT 
				@MOTORCYCLEDRIVERWITHWOLVERINE=count(distinct(POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID)) -- CHECK TOTAL DRIVER AUTO
				FROM POL_DRIVER_DETAILS right outer join
				 POL_DRIVER_ASSIGNED_VEHICLE ON
				 POL_DRIVER_DETAILS.CUSTOMER_ID=POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID 
				AND POL_DRIVER_DETAILS.POLICY_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID  
				AND POL_DRIVER_DETAILS.POLICY_VERSION_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID 
				AND  POL_DRIVER_DETAILS.DRIVER_ID=POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID
				
				RIGHT OUTER JOIN POL_VEHICLES ON 
			
				POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID=POL_VEHICLES.CUSTOMER_ID 
				AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID=POL_VEHICLES.POLICY_ID  
				AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID=POL_VEHICLES.POLICY_VERSION_ID 
				AND  POL_DRIVER_ASSIGNED_VEHICLE.VEHICLE_ID=POL_VEHICLES.VEHICLE_ID
				
				INNER JOIN  POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID
				WHERE POL_CUSTOMER_POLICY_LIST.customer_id=@CUSTOMERID
				and POL_CUSTOMER_POLICY_LIST.policy_id=@POLID
				and POL_CUSTOMER_POLICY_LIST.policy_version_id=@POLVERSIONID
				AND POL_VEHICLES.MOTORCYCLE_TYPE != ''      
		      END          
             
		     IF(@POL_lob = 4)          -- CHECK WATERCRAFT POLICY FOR WOLVERINE IN MICIGAN
		       BEGIN          
			       SELECT          
			       @WATERCRAFT_POLICY_LIMIT = convert(varchar(20),ISNULL(LIMIT_1,0),101)          
			       FROM POL_WATERCRAFT_COVERAGE_INFO  WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND COVERAGE_CODE_ID=65          
		       END          
			              
	       	     IF(@POL_lob = 6)          -- CHECK RANTAL DWELLING POLICY FOR WOLVERINE IN MICIGAN
	       	       BEGIN          
			       SELECT          
			       @DWELLINGFIRE_POLICY_LIMIT = convert(varchar(20),ISNULL(LIMIT_1,0))          
			       FROM POL_DWELLING_SECTION_COVERAGES  WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND COVERAGE_CODE_ID=797          
			END   
  		       
      END    
     
		   -- IF STATE IS INDIANA
		 if(@STATEID = 14)          
		  begin          
		   IF (@POL_lob = 1)          -- CHECK HOMEOWNERS POLICY FOR INDIANA
		       BEGIN          
		       SELECT           
		       @HOMEOWNERS_POLICY_LIMIT= convert(varchar(20),ISNULL(LIMIT_1,0),101)           
		       FROM POL_DWELLING_SECTION_COVERAGES WHERE CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID  AND COVERAGE_CODE_ID=170            
		       END          
		            
		          
		     IF (@POL_lob = 2)          -- CHECK AUTO POLICY FOR INDIANA
		       BEGIN          
		       SELECT        		--BI SPLIT FOR WOLVERINE CUSTOMER IN INDIANA  
		       @AUTOLOWERLIMIT = convert(varchar(20),ISNULL(LIMIT_1,0),101), --Lower Limit    
		       @AUTOUPPERLIMIT = convert(varchar(20),ISNULL(LIMIT_2,0),101)  -- Upper Limit         
		       FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND (COVERAGE_CODE_ID=2)  and VEHICLE_ID = 1     
		        
		       SELECT        		--PD LIMIT FOR CUSTOMER IN INDIANA
		       @AUTOPD = convert(varchar(20),ISNULL(LIMIT_1,0),101)             
		       FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND (COVERAGE_CODE_ID=4) and VEHICLE_ID = 1      
		 	SELECT        			--CSL  LIMIT FOR CUSTOMER IN INDIANA
		       @AUTOCSL = convert(varchar(20),ISNULL(LIMIT_1,0),101)             
		       FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND (COVERAGE_CODE_ID=1)  and VEHICLE_ID = 1     
		  
		       SET @PERSONALAUTOPOLICYLOWERLIMIT = @AUTOLOWERLIMIT
		       SET @PERSONALAUTOPOLICYUPPERLIMIT = @AUTOUPPERLIMIT
		              
		                
		      ---------------------------------------------------------------------------------------------          
		            -- NUMBER OF AUTOMOBILE FOR WOLVERINE CUSTOMER IN INDIANA
		      ---------------------------------------------------------------------------------------------          
		       SELECT           
		       @VEHCLEIDAUTOPER = COUNT(VEHICLE_ID)          -- CHECK FOR PERSONAL CUSTOMER IN INDIANA
		                  
		       FROM POL_VEHICLES           
		       WHERE CUSTOMER_ID=@CUSTOMERID          
		       AND POLICY_ID=@POLID          
		       AND POLICY_VERSION_ID=@POLVERSIONID           
		       AND( ISNULL(APP_VEHICLE_PERTYPE_ID,'') !='' 
			AND APP_VEHICLE_PERTYPE_ID!=11336)        
		                 
		       SELECT           
		       @VEHCLEIDAUTOCOM = COUNT(VEHICLE_ID)          -- CHECK FOR COMERCIAL CUSTOMER IN INDIANA
		                  
		       FROM POL_VEHICLES           
		       WHERE CUSTOMER_ID=@CUSTOMERID          
		       AND POLICY_ID=@POLID          
		       AND POLICY_VERSION_ID=@POLVERSIONID           
		       AND ISNULL(APP_VEHICLE_COMCLASS_ID,'')!=''    
			
			IF ( @VEHCLEIDAUTOPER IS NULL)
			SET @VEHCLEIDAUTOPER=0        
		                  
			IF ( @VEHCLEIDAUTOCOM IS NULL)
			SET @VEHCLEIDAUTOCOM=0        
		                        
		                  
		       SET @AUTOMOBILEWITHWOLVERINE = ISNULL((@VEHCLEIDAUTOPER + @VEHCLEIDAUTOCOM),0)          
		            
		            
		    ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
		  	     -- AUTOMOBILE INEXPERIENCED DRIVER          
		    -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		  
		    
		   
			SELECT 
			@INEXPERIENCEDRIVERAUTOWITHWOLVERINE=count(distinct(POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID)) -- CHECK INEXPERINCED DRIVER AUTO
			FROM POL_DRIVER_DETAILS right outer join
			 POL_DRIVER_ASSIGNED_VEHICLE ON
			 POL_DRIVER_DETAILS.CUSTOMER_ID=POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID 
			AND POL_DRIVER_DETAILS.POLICY_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID  
			AND POL_DRIVER_DETAILS.POLICY_VERSION_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID 
			AND  POL_DRIVER_DETAILS.DRIVER_ID=POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID
			
			RIGHT OUTER JOIN POL_VEHICLES ON 
		
			POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID=POL_VEHICLES.CUSTOMER_ID 
			AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID=POL_VEHICLES.POLICY_ID  
			AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID=POL_VEHICLES.POLICY_VERSION_ID 
			AND  POL_DRIVER_ASSIGNED_VEHICLE.VEHICLE_ID=POL_VEHICLES.VEHICLE_ID
			
			INNER JOIN  POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID
			WHERE POL_CUSTOMER_POLICY_LIST.customer_id=@CUSTOMERID
			and POL_CUSTOMER_POLICY_LIST.policy_id=@POLID
			and POL_CUSTOMER_POLICY_LIST.policy_version_id=@POLVERSIONID
			AND POL_VEHICLES.APP_VEHICLE_PERTYPE_ID != 11336
			AND (( DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) <25) OR ( DATEDIFF(YEAR,DATE_LICENSED,APP_EFFECTIVE_DATE) <3))
			   
		
			SELECT 
			@AUTOMOBILE_DRIVERWITHWOLVERINE=count(distinct(POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID)) -- CHECK TOTAL DRIVER AUTO
			FROM POL_DRIVER_DETAILS right outer join
			 POL_DRIVER_ASSIGNED_VEHICLE ON
			 POL_DRIVER_DETAILS.CUSTOMER_ID=POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID 
			AND POL_DRIVER_DETAILS.POLICY_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID  
			AND POL_DRIVER_DETAILS.POLICY_VERSION_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID 
			AND  POL_DRIVER_DETAILS.DRIVER_ID=POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID
			
			RIGHT OUTER JOIN POL_VEHICLES ON 
		
			POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID=POL_VEHICLES.CUSTOMER_ID 
			AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID=POL_VEHICLES.POLICY_ID  
			AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID=POL_VEHICLES.POLICY_VERSION_ID 
			AND  POL_DRIVER_ASSIGNED_VEHICLE.VEHICLE_ID=POL_VEHICLES.VEHICLE_ID
			
			INNER JOIN  POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID
			WHERE POL_CUSTOMER_POLICY_LIST.customer_id=@CUSTOMERID
			and POL_CUSTOMER_POLICY_LIST.policy_id=@POLID
			and POL_CUSTOMER_POLICY_LIST.policy_version_id=@POLVERSIONID
			AND POL_VEHICLES.APP_VEHICLE_PERTYPE_ID != 11336
			
		    -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		       -- NUMBER OF MOTOR HOMES            
		    -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		     
		       SELECT           
		       @MOTORHOMESWITHWOLVERINE = ISNULL(COUNT(VEHICLE_ID),'0')          -- CHECK FOR NUMBER OF INDIANA CUSTOMER'S MOTORHOMES 
		                  
		       FROM POL_VEHICLES           
		       WHERE CUSTOMER_ID=@CUSTOMERID          
		       AND POLICY_ID=@POLID          
		       AND POLICY_VERSION_ID=@POLVERSIONID           
		       AND APP_VEHICLE_PERTYPE_ID=11336           
		                 
		              
		    -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		          -- MOTOR HOME INEXPERIENCED DRIVER          
		    -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		               
		       
			SELECT 
			@INEXPERIENCEDRIVERMOTORHOMEWITHWOLVERINE=count(distinct(POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID)) -- CHECK INEXPERINCED DRIVER AUTO
			FROM POL_DRIVER_DETAILS right outer join
			 POL_DRIVER_ASSIGNED_VEHICLE ON
			 POL_DRIVER_DETAILS.CUSTOMER_ID=POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID 
			AND POL_DRIVER_DETAILS.POLICY_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID  
			AND POL_DRIVER_DETAILS.POLICY_VERSION_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID 
			AND  POL_DRIVER_DETAILS.DRIVER_ID=POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID
			
			RIGHT OUTER JOIN POL_VEHICLES ON 
		
			POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID=POL_VEHICLES.CUSTOMER_ID 
			AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID=POL_VEHICLES.POLICY_ID  
			AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID=POL_VEHICLES.POLICY_VERSION_ID 
			AND  POL_DRIVER_ASSIGNED_VEHICLE.VEHICLE_ID=POL_VEHICLES.VEHICLE_ID
			
			INNER JOIN  POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID
			WHERE POL_CUSTOMER_POLICY_LIST.customer_id=@CUSTOMERID
			and POL_CUSTOMER_POLICY_LIST.policy_id=@POLID
			and POL_CUSTOMER_POLICY_LIST.policy_version_id=@POLVERSIONID
			AND POL_VEHICLES.APP_VEHICLE_PERTYPE_ID = 11336
			AND (( DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) <25) OR ( DATEDIFF(YEAR,DATE_LICENSED,APP_EFFECTIVE_DATE) <3))
			   
		
			SELECT 
			@MOTORHOMEDRIVERWITHWOLVERINE=count(distinct(POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID)) -- CHECK TOTAL DRIVER AUTO
			FROM POL_DRIVER_DETAILS right outer join
			 POL_DRIVER_ASSIGNED_VEHICLE ON
			 POL_DRIVER_DETAILS.CUSTOMER_ID=POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID 
			AND POL_DRIVER_DETAILS.POLICY_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID  
			AND POL_DRIVER_DETAILS.POLICY_VERSION_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID 
			AND  POL_DRIVER_DETAILS.DRIVER_ID=POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID
			
			RIGHT OUTER JOIN POL_VEHICLES ON 
		
			POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID=POL_VEHICLES.CUSTOMER_ID 
			AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID=POL_VEHICLES.POLICY_ID  
			AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID=POL_VEHICLES.POLICY_VERSION_ID 
			AND  POL_DRIVER_ASSIGNED_VEHICLE.VEHICLE_ID=POL_VEHICLES.VEHICLE_ID
			
			INNER JOIN  POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID
			WHERE POL_CUSTOMER_POLICY_LIST.customer_id=@CUSTOMERID
			and POL_CUSTOMER_POLICY_LIST.policy_id=@POLID
			and POL_CUSTOMER_POLICY_LIST.policy_version_id=@POLVERSIONID
			AND POL_VEHICLES.APP_VEHICLE_PERTYPE_ID = 11336
		    END
		    
		     IF (@POL_lob = 3)            -- CHECK MOTORCYCLE POLICY FOR INDIANA
		       BEGIN          
		       SELECT           
		       @MOTORLOWERLIMIT = convert(varchar(20),ISNULL(LIMIT_1,0),101),   -- CHECK FOR BISPLIT LIMIT FOR INDIANA WOLVERINE CUSTOMER
			@MOTORUPERLIMIT = convert(varchar(20),ISNULL(LIMIT_2,0),101)             
		       FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND (COVERAGE_CODE_ID=127 )  and VEHICLE_ID = 1     
		         
			SELECT           
		        @MOTORCSL = convert(varchar(20),ISNULL(LIMIT_1,0),101)              -- CHECK FOR CSL LIMIT FOR INDIANA WOLVERINE CUSTOMER
		        FROM POL_VEHICLE_coverages  WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND (COVERAGE_CODE_ID=126)  and VEHICLE_ID = 1       
		       
			SELECT           
		        @MOTORPD = convert(varchar(20),ISNULL(LIMIT_1,0),101)              -- CHECK FOR PD LIMIT FOR INDIANA WOLVERINE CUSTOMER
		        FROM POL_VEHICLE_coverages  WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND (COVERAGE_CODE_ID=128)  and VEHICLE_ID = 1       
		
		
		
			SELECT           
		       @UNINSUREDMOTORISTCSL= convert(varchar(20),ISNULL(LIMIT_1,0),101)            -- CHECK FOR UNINSUREDMOTORISTCSL LIMIT FOR INDIANA WOLVERINE CUSTOMER
		       FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND COVERAGE_CODE_ID=131  and VEHICLE_ID = 1         
		       SELECT           
		       @UNINSUREDMOOTRISTBIPLIT= convert(varchar(20),ISNULL(LIMIT_2,0),101)           -- CHECK FOR UNINSUREDMOTORISTBISPLIT LIMIT FOR INDIANA WOLVERINE CUSTOMER
		       FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND COVERAGE_CODE_ID=132  and VEHICLE_ID = 1         
		       SELECT @UNDERINSUREDMOTORISTCSL= convert(varchar(20),ISNULL(LIMIT_1,0),101)    -- -- CHECK FOR UNINSUREDMOTORISTCSL LIMIT FOR INDIANA WOLVERINE CUSTOMER
		       FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID and COVERAGE_CODE_ID=133  and VEHICLE_ID = 1           
		       SELECT @UNDERINSUREDMOTORISTBISPLIT= convert(varchar(20),ISNULL(LIMIT_2,0),101)             
		       FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID and COVERAGE_CODE_ID=214   and VEHICLE_ID = 1          
		          
		 	SET @MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT = @MOTORLOWERLIMIT     
		      	SET @MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT = @MOTORUPERLIMIT   
		
			SELECT           
		       @UNINSUREDMOTORISTCSLREJECT= LTRIM(RTRIM(LIMIT1_AMOUNT_TEXT))            -- CHECK FOR UNINSUREDMOTORISTCSL LIMIT FOR INDIANA WOLVERINE CUSTOMER
		       FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND COVERAGE_CODE_ID=131  and VEHICLE_ID = 1         
		       SELECT           
		       @UNINSUREDMOOTRISTBIPLITREJECT= LTRIM(RTRIM(LIMIT1_AMOUNT_TEXT))           -- CHECK FOR UNINSUREDMOTORISTBISPLIT LIMIT FOR INDIANA WOLVERINE CUSTOMER
		       FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND COVERAGE_CODE_ID=132  and VEHICLE_ID = 1         
		       SELECT @UNDERINSUREDMOTORISTCSLREJECT= LTRIM(RTRIM(LIMIT1_AMOUNT_TEXT))    -- -- CHECK FOR UNINSUREDMOTORISTCSL LIMIT FOR INDIANA WOLVERINE CUSTOMER
		       FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID and COVERAGE_CODE_ID=133  and VEHICLE_ID = 1           
		       SELECT @UNDERINSUREDMOTORISTBISPLITREJECT= LTRIM(RTRIM(LIMIT2_AMOUNT_TEXT))             
		       FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID and COVERAGE_CODE_ID=214   and VEHICLE_ID = 1          
		       
		                
		   ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
		 	   -- NUMBER OF MOTORCYCLES          
		   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		   
		      SELECT           
		      @MOTORCYCLESWITHEWOLVERINE = ISNULL(COUNT(VEHICLE_ID),0)          -- CHECK FOR NUMBER OF MOTORCYCE IN INDIANA WOLVERINE
		      FROM POL_VEHICLES           
		      WHERE CUSTOMER_ID=@CUSTOMERID          
		      AND POLICY_ID=@POLID          
		      AND POLICY_VERSION_ID=@POLVERSIONID           
		      AND MOTORCYCLE_TYPE !=''           
		                 
		   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		       -- NUMBER OF INEXPERINCED DRIVER MOTORCYCLE          
		   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		    
		
			SELECT 
			@INEXPERIENCEDRIVERMOTORCYCLEWITHWOLVERINE=count(distinct(POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID)) -- CHECK INEXPERINCED DRIVER AUTO
			FROM POL_DRIVER_DETAILS right outer join
			 POL_DRIVER_ASSIGNED_VEHICLE ON
			 POL_DRIVER_DETAILS.CUSTOMER_ID=POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID 
			AND POL_DRIVER_DETAILS.POLICY_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID  
			AND POL_DRIVER_DETAILS.POLICY_VERSION_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID 
			AND  POL_DRIVER_DETAILS.DRIVER_ID=POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID
			
			RIGHT OUTER JOIN POL_VEHICLES ON 
		
			POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID=POL_VEHICLES.CUSTOMER_ID 
			AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID=POL_VEHICLES.POLICY_ID  
			AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID=POL_VEHICLES.POLICY_VERSION_ID 
			AND  POL_DRIVER_ASSIGNED_VEHICLE.VEHICLE_ID=POL_VEHICLES.VEHICLE_ID
			
			INNER JOIN  POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID
			WHERE POL_CUSTOMER_POLICY_LIST.customer_id=1141
			and POL_CUSTOMER_POLICY_LIST.policy_id=14
			and POL_CUSTOMER_POLICY_LIST.policy_version_id=1
			AND POL_VEHICLES.MOTORCYCLE_TYPE != ''
			AND (( DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) <25) OR ( DATEDIFF(YEAR,DATE_LICENSED,APP_EFFECTIVE_DATE) <3))
		
			SELECT 
			@MOTORCYCLEDRIVERWITHWOLVERINE=count(distinct(POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID)) -- CHECK TOTAL DRIVER AUTO
			FROM POL_DRIVER_DETAILS right outer join
			 POL_DRIVER_ASSIGNED_VEHICLE ON
			 POL_DRIVER_DETAILS.CUSTOMER_ID=POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID 
			AND POL_DRIVER_DETAILS.POLICY_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID  
			AND POL_DRIVER_DETAILS.POLICY_VERSION_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID 
			AND  POL_DRIVER_DETAILS.DRIVER_ID=POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID
			
			RIGHT OUTER JOIN POL_VEHICLES ON 
		
			POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID=POL_VEHICLES.CUSTOMER_ID 
			AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID=POL_VEHICLES.POLICY_ID  
			AND POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID=POL_VEHICLES.POLICY_VERSION_ID 
			AND  POL_DRIVER_ASSIGNED_VEHICLE.VEHICLE_ID=POL_VEHICLES.VEHICLE_ID
			
			INNER JOIN  POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID
			WHERE POL_CUSTOMER_POLICY_LIST.customer_id=@CUSTOMERID
			and POL_CUSTOMER_POLICY_LIST.policy_id=@POLID
			and POL_CUSTOMER_POLICY_LIST.policy_version_id=@POLVERSIONID
			AND POL_VEHICLES.MOTORCYCLE_TYPE != ''
		
		
		  	END          
		       IF(@POL_lob = 4)     -- CHECVK FOR WATERCRAFT POLICY IN INDIANA WOLVERINE
		        BEGIN          
		          SELECT          
		          @WATERCRAFT_POLICY_LIMIT = convert(varchar(20),ISNULL(LIMIT_1,0),101)          
		          FROM POL_WATERCRAFT_COVERAGE_INFO  WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND COVERAGE_CODE_ID=19          
		        END          
		             
		       IF(@POL_lob = 6)          -- CHECK FOR RENTAL DWELLING POLICY IN WOLVERINE
		        BEGIN          
		          SELECT          
		          @DWELLINGFIRE_POLICY_LIMIT = convert(varchar(20),ISNULL(LIMIT_1,0),101)         
		          FROM POL_DWELLING_SECTION_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and POLICY_ID = @POLID and POLICY_VERSION_ID = @POLVERSIONID AND COVERAGE_CODE_ID=777          
		        END           
		               
		   END          
	END
	---------------------------------------------
	--wolverine policy case(end)
	---------------------------------------------
	else

	begin
		---------------------------------------------
		--others policy case(start)
		---------------------------------------------

		IF EXISTS(
		SELECT     
		 *    -- CHECK FOR MATURED AGE DISCOUNT IN OTHERS POLICY CASE
		 FROM    
		  APP_LIST WITH (NOLOCK) INNER JOIN APP_UMBRELLA_DRIVER_DETAILS UDD ON    
		 APP_LIST.CUSTOMER_ID=UDD.CUSTOMER_ID AND APP_LIST.APP_ID=UDD.APP_ID AND APP_LIST.APP_VERSION_ID=UDD.APP_VERSION_ID   
		 WHERE                                                             
		  APP_LIST.CUSTOMER_ID=@CUSTOMERID AND APP_LIST.APP_ID=@APPID AND APP_LIST.APP_VERSION_ID=@APPVERSIONID  
		AND ((( DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) > @MATURE_AGE_LL ) AND DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) < @MATURE_AGE_UL )))   
		SET @DRIVERAGEOTHERS='Y'
		ELSE
		SET @DRIVERAGEOTHERS='N'
   
  
		--------------------------------------------------------------------------------------------------------  
		   -- number of officepromises and rentaldwellingunit  
		--------------------------------------------------------------------------------------------------------  
		 SELECT    
		  @OFFICE_PROMISES  = CONVERT(VARCHAR(20),isnull(RESIDENCES_OWNER_OCCUPIED,'0'),101),    -- office promises
		  @RENTAL_DWELLING_UNITS  =    CONVERT(VARCHAR(20),NUM_OF_RENTAL_UNITS,101)    		-- rantal Dwelling
		 FROM    
		  APP_UMBRELLA_LIMITS WITH (NOLOCK)    
		 WHERE                                                             
		  CUSTOMER_ID=@CUSTOMERID  AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID      
		  
  
  
		-------------------------------------------------------------------------------------------------------  
		   -- NUMBER OF DRIVERS  
		-------------------------------------------------------------------------------------------------------  
		 SELECT   
		 @TOTALNUMER_OF_DRIVERSOTHERS = COUNT(DRIVER_ID)   -- TOTAL NUMBER OF DRIVERS IN OTHERS CASE
		 FROM    
		  APP_UMBRELLA_DRIVER_DETAILS WITH (NOLOCK)     
		  WHERE                                                             
		  CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID= @APPVERSIONID  
		  
		---------------------------------------------------------------------------------------------  
		   -- NUMBER OF AUTOMOBILE  
		---------------------------------------------------------------------------------------------  
		DECLARE @AUTOMOBILEWITHOTHERS INT 
		DECLARE @AUTOCOM INT   
		DECLARE @AUTOPER INT  
		 SELECT  
		  @AUTOCOM =  COUNT(VEHICLE_TYPE_COM)   -- CHECK FOR COMERCIAL AUTO IN OTHERS CASE
		  FROM  APP_UMBRELLA_VEHICLE_INFO WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND VEHICLE_TYPE_COM!=''   
		   
		 SELECT  
		  @AUTOPER =  COUNT(VEHICLE_TYPE_PER)  -- CHECK FOR PERSONAL AUTO IN OTHERS CASE
		  FROM  APP_UMBRELLA_VEHICLE_INFO WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND VEHICLE_TYPE_PER!=''  
		   
		 IF (@AUTOCOM IS NULL )
			SET @AUTOCOM =0
		
		 IF (@AUTOPER IS NULL )
			SET @AUTOPER =0
		
		
		
		 	SET @AUTOMOBILEWITHOTHERS = ISNULL((@AUTOCOM + @AUTOPER),0)  
                       
   
		--------------------------------------------------------------------------------------------------------  
		--SELECT * FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID=11958   
		   --NUMBER OF INEXPERINCED DRIVER(AUTO)   
		--------------------------------------------------------------------------------------------------------  
		DECLARE @AUTO_ID_NUMBER INT 
		DECLARE AUTO_ID_CURSOR CURSOR FOR  
		 SELECT  VEHICLE_ID 
			FROM APP_UMBRELLA_VEHICLE_INFO WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID =@APPID AND APP_VERSION_ID = @APPVERSIONID AND ( VEHICLE_TYPE_PER is not null AND   VEHICLE_TYPE_PER !=11958)
		  
		 OPEN AUTO_ID_CURSOR   
		 FETCH NEXT FROM  AUTO_ID_CURSOR INTO @AUTO_ID_NUMBER   
		 WHILE @@FETCH_STATUS = 0  
		  BEGIN   
		    SELECT   
		 	@AUTOINEXPDRIVERAGEOTHERS =  COUNT(driver_id)		-- INEXPERINCE DRIVER AUTO OTHERS CASE
		    FROM APP_UMBRELLA_DRIVER_DETAILS  
		     AUVI INNER JOIN APP_LIST AL ON AUVI.customer_id=AL.CUSTOMER_ID AND AUVI.APP_ID=AL.APP_ID AND AUVI.APP_VERSION_ID=AL.APP_VERSION_ID  where AL.customer_id=@CUSTOMERID and AL.app_id=@APPID AND AL.APP_VERSION_ID=@APPVERSIONID  and AUVI.VEHICLE_ID = @AUTO_ID_NUMBER  
		       AND ((DATEDIFF(YEAR,AUVI.DRIVER_DOB,AL.APP_EFFECTIVE_DATE)<25) or (DATEDIFF(YEAR,AUVI.DATE_LICENSED,AL.APP_EFFECTIVE_DATE)<3))
		       
		    FETCH NEXT FROM  AUTO_ID_CURSOR INTO @AUTO_ID_NUMBER   
		    END  ---END WHILE  
		    --SELECT @AUTOMOBILE_INEXPERIENCED_DRIVER  
		 CLOSE AUTO_ID_CURSOR  
		 DEALLOCATE AUTO_ID_CURSOR  
		  
		--------------------------------------------------------------------------------------------------------  
		   -- NUMBER OF MOTORHOMES  
		--------------------------------------------------------------------------------------------------------  
		 SELECT  
		  @MOTORHOMESOTHERS =  COUNT(VEHICLE_TYPE_PER)    -- CHECK FOR MOTOR HOMES IN OTHERS CASE
		  FROM  APP_UMBRELLA_VEHICLE_INFO WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND VEHICLE_TYPE_PER =11958   
		 
		--------------------------------------------------------------------------------------------------------  
		--SELECT * FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID=11958   
		   --NUMBER OF INEXPERINCED DRIVER(motorhomes)   
		--------------------------------------------------------------------------------------------------------  
		DECLARE @VEHICLE_ID_NUMBER SMALLINT  
		DECLARE VEHICLE_ID_CURSOR CURSOR FOR  
		 SELECT   
		  
		 VEHICLE_ID FROM APP_UMBRELLA_VEHICLE_INFO WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID =@APPID AND APP_VERSION_ID = @APPVERSIONID AND VEHICLE_TYPE_PER =11958  
		  
		 OPEN VEHICLE_ID_CURSOR   
		 FETCH NEXT FROM  VEHICLE_ID_CURSOR INTO @VEHICLE_ID_NUMBER   
		 WHILE @@FETCH_STATUS = 0  
		  BEGIN   
		        SELECT   
		 	@MOTORHOMEINEXPDRIVEROTHERS =  COUNT(driver_id)			-- CHECK FOR INEXPERINCE DRIVER MOTORHOMES OTHERS CASE
		    FROM APP_UMBRELLA_DRIVER_DETAILS  
		     AUVI INNER JOIN APP_LIST AL ON AUVI.customer_id=AL.CUSTOMER_ID AND AUVI.APP_ID=AL.APP_ID AND AUVI.APP_VERSION_ID=AL.APP_VERSION_ID  where AL.customer_id=@CUSTOMERID and AL.app_id=@APPID AND AL.APP_VERSION_ID=@APPVERSIONID  and AUVI.VEHICLE_ID = @VEHICLE_ID_NUMBER  
		       AND ((DATEDIFF(YEAR,AUVI.DRIVER_DOB,AL.APP_EFFECTIVE_DATE)<25) or (DATEDIFF(YEAR,AUVI.DATE_LICENSED,AL.APP_EFFECTIVE_DATE)<3))
		      FETCH NEXT FROM  VEHICLE_ID_CURSOR INTO @VEHICLE_ID_NUMBER   
		     END  ---END WHILE  
		   CLOSE VEHICLE_ID_CURSOR  
		 DEALLOCATE VEHICLE_ID_CURSOR  
		 
		--------------------------------------------------------------------------------------------------------  
		--SELECT * FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID>11110 NUMBER OF INEXPERINCED DRIVER(AUTO)  
		   -- number of motorcycles  
		--------------------------------------------------------------------------------------------------------  
		  
		 SELECT  
		  @MOTORCYCLESOTHERS =  COUNT(MOTORCYCLE_TYPE)  -- CHECK FOR NUMBER OF MOTOR CYCLE IN OTHERS CASE
		  FROM  APP_UMBRELLA_VEHICLE_INFO WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND MOTORCYCLE_TYPE != 11958 AND MOTORCYCLE_TYPE !=''  
		--select * from APP_UMBRELLA_VEHICLE_INFO where customer_id=1135 and app_id=3 and APP_VERSION_ID =1  
		---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		  
		   -- NUMBER OF INEXPERINCED DRIVER MOTORCYCLE  
		---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		  
		DECLARE @VEHICLE_ID_NUMBER_MOTORCYCLE SMALLINT  
		
		DECLARE VEHICLE_ID_CURSOR_MOTORCYCLE CURSOR FOR  -- CHECK FOR NUMBER OF INEXPERIENCE DRIVER IN MOTOR CYCLE IN OTHERS CASE
		 SELECT   
		  
		 VEHICLE_ID FROM APP_UMBRELLA_VEHICLE_INFO WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID =@APPID AND APP_VERSION_ID = @APPVERSIONID AND MOTORCYCLE_TYPE IS NOT NULL  
		  
		 OPEN VEHICLE_ID_CURSOR_MOTORCYCLE   
		 FETCH NEXT FROM  VEHICLE_ID_CURSOR_MOTORCYCLE INTO @VEHICLE_ID_NUMBER_MOTORCYCLE   
		 WHILE @@FETCH_STATUS = 0  
		  BEGIN   
		   --SELECT @VEHICLE_ID_NUMBER_MOTORCYCLE  
		    SELECT   
		 	@MOTORCYCLEINEXPDRIVEROTHERS =  COUNT(driver_id)
		    FROM APP_UMBRELLA_DRIVER_DETAILS  
		     AUVI INNER JOIN APP_LIST AL ON AUVI.customer_id=AL.CUSTOMER_ID AND AUVI.APP_ID=AL.APP_ID AND AUVI.APP_VERSION_ID=AL.APP_VERSION_ID  where AL.customer_id=@CUSTOMERID and AL.app_id=@APPID AND AL.APP_VERSION_ID=@APPVERSIONID  and AUVI.VEHICLE_ID = @VEHICLE_ID_NUMBER_MOTORCYCLE  
		       AND ((DATEDIFF(YEAR,AUVI.DRIVER_DOB,AL.APP_EFFECTIVE_DATE)<25) or (DATEDIFF(YEAR,AUVI.DATE_LICENSED,AL.APP_EFFECTIVE_DATE)<3))
			 FETCH NEXT FROM  VEHICLE_ID_CURSOR_MOTORCYCLE INTO @VEHICLE_ID_NUMBER_MOTORCYCLE   
		     END 
		 CLOSE VEHICLE_ID_CURSOR_MOTORCYCLE  
		 DEALLOCATE VEHICLE_ID_CURSOR_MOTORCYCLE 
		---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		  
		   -- others case coverages  (start)
		---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		DECLARE @STATEIDS SMALLINT
		
		SELECT
		@STATEIDS = STATE_ID
		 
		FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID
		
		if(@STATEIDS = 22)
		
		begin
		
		select   
		   @APP_LOBOTHER = POLICY_LOB  
		   FROM APP_UMBRELLA_UNDERLYING_POLICIES    
		   WHERE CUSTOMER_ID = @CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER  
		  
			if(@APP_LOBOTHER = 1)
			BEGIN  
		     	SELECT   
		     	@HOMEOWNERS_POLICY_LIMIT= convert(varchar(20),COVERAGE_AMOUNT,101)   -- CHECK HOMEOWNERS COVERAGE
		     	FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='PL'  
		    	END  
		   --SELECT @HOMEOWNERS_POLICY_LIMIT  
		   	IF (@APP_LOBOTHER = 2)  
		   	 BEGIN  
		    	 SELECT   
		    	 @PERSONALAUTOPOLICYLOWERLIMIT = substring((convert(nvarchar(20),COVERAGE_AMOUNT)),1,charindex('/',(convert(nvarchar(20),COVERAGE_AMOUNT)))-1),			-- CHECK FOR BI SPLIT LIMIT COVERAGE
			 @PERSONALAUTOPOLICYUPPERLIMIT = substring((convert(nvarchar(20),COVERAGE_AMOUNT)),charindex('/',(convert(nvarchar(20),COVERAGE_AMOUNT)))+1,15)     
		    	 FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='BISPL'  
		    	 SELECT   
		    	 @AUTOPD = convert(varchar(20),COVERAGE_AMOUNT)				-- CHECK FOR PD AUTO LIMIT COVERAGE IN OTHERS CASE OF STATE MICICGAN
			 FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='PD'  
		    	END  
		   
		   	IF (@APP_LOBOTHER = 3)    
		   	 BEGIN  
		    	 SELECT   
		     	@MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT = substring((convert(nvarchar(20),COVERAGE_AMOUNT)),1,charindex('/',(convert(nvarchar(20),COVERAGE_AMOUNT)))-1),		-- CHECK FORMOTORCYLE BI SPLIT LIMIT COVERAGE OF MICIGAN
			 @MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT = substring((convert(nvarchar(20),COVERAGE_AMOUNT)),charindex('/',(convert(nvarchar(20),COVERAGE_AMOUNT)))+1,15)     
		     	FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='BISPL'  
		     	SELECT   
		     	@MOTORCSL = convert(varchar(20),COVERAGE_AMOUNT,101)  						-- CHECK FOR MOTORCYCLE CSL LIMIT FOR STATE MICIGAN
		     	FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='RLCSL'  
		     	SELECT  
		     	@UNINSUREDMOTORISTCSL= convert(varchar(20),COVERAGE_AMOUNT,101)  		-- CHECK FOR MOTORCYCLE UNINSURED CSL LIMIT FOR STATE MICIGAN IN OTHERS CASE 
		     	FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='PUNCS'  
		    	 SELECT   
		     	@UNINSUREDMOOTRISTBIPLIT= convert(varchar(20),COVERAGE_AMOUNT,101)   			-- -- CHECK FOR MOTORCYCLE UNISURED BISPLIT  LIMIT FOR STATE MICIGAN
		     	FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='PUMSP'  
			END  
			
		  	IF(@APP_LOBOTHER = 6)  
		    	BEGIN  
		     	SELECT   
		     	@DWELLINGFIRE_POLICY_LIMIT= convert(varchar(20),COVERAGE_AMOUNT,101)   	-- CHECK FOR DWELLING FIRE POLICY LIMIT FOR STATE MICIGAN IN OTHERS CASE
		     	FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='PL'  
		    	END  
		     
		  	 IF(@APP_LOBOTHER = 4)  
		    	BEGIN  
		    	 SELECT   
		    	 @WATERCRAFT_POLICY_LIMIT= convert(varchar(20),COVERAGE_AMOUNT,101)   	-- CHECK FOR WATERCRAFT POLICY LIMIT FOR STATE MICIGAN IN OTHERS CASE
		    	 FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='LCCSL'  
		    	END  
		       
		   end  
		   IF (@STATEIDS =14)  
		   begin  
		   	IF (@APP_LOBOTHER = 1)  
		    	BEGIN  
		    	SELECT   
		     	@HOMEOWNERS_POLICY_LIMIT= convert(varchar(20),COVERAGE_AMOUNT,101)   	-- 	-- CHECK FOR HOMEOWNERS POLICY LIMIT FOR STATE INDIANA IN OTHERS CASE
		     	FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='PL'  
		    	END  
		   
		      
		   	IF (@APP_LOBOTHER = 2)  
		    	BEGIN  
		     	SELECT   
		    	 @PERSONALAUTOPOLICYLOWERLIMIT = substring((convert(nvarchar(20),COVERAGE_AMOUNT)),1,charindex('/',(convert(nvarchar(20),COVERAGE_AMOUNT)))-1),		---- CHECK FOR AUTO POLICY BISPLIT LIMIT FOR STATE INDIANA IN OTHERS CASE
			 @PERSONALAUTOPOLICYUPPERLIMIT = substring((convert(nvarchar(20),COVERAGE_AMOUNT)),charindex('/',(convert(nvarchar(20),COVERAGE_AMOUNT)))+1,15)     
		    	 FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='BISPL'  
			SELECT 
		    	 @AUTOPD = convert(varchar(20),COVERAGE_AMOUNT)				-- CHECK FOR AUTO POLICY PD LIMIT FOR STATE INDIANA IN OTHERS CASE
			 FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='PD'  
			
			END  
		   
		      
		   	IF (@APP_LOBOTHER = 3)    
		   	 BEGIN  
		    	 SELECT   
		     	@MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT = substring((convert(nvarchar(20),COVERAGE_AMOUNT)),1,charindex('/',(convert(nvarchar(20),COVERAGE_AMOUNT)))-1),			-- CHECK FOR MOTORCYCLE POLICY BISPLIT LIMIT FOR STATE INDIANA IN OTHERS CASE
			 @MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT = substring((convert(nvarchar(20),COVERAGE_AMOUNT)),charindex('/',(convert(nvarchar(20),COVERAGE_AMOUNT)))+1,15)     
		     	FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='BISPL'  
		     	SELECT   
		     	@MOTORCSL = convert(varchar(20),COVERAGE_AMOUNT,101)  					-- CHECK FOR MOTORCYCLE POLICY CSL LIMIT FOR STATE INDIANA IN OTHERS CASE
		     	FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='RLCSL'  
		     	
			SELECT   
		     	@MOTORPD = convert(varchar(20),COVERAGE_AMOUNT,101)  					-- CHECK FOR MOTORCYCLE POLICY PD LIMIT FOR STATE INDIANA IN OTHERS CASE
		     	FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='PD'  
		     	
		
			SELECT   
		    	 @UNINSUREDMOTORISTCSL= convert(varchar(20),COVERAGE_AMOUNT,101)   			-- CHECK FOR UNINSUREDMOTORIST CSL POLICY LIMIT FOR STATE INDIANA IN OTHERS CASE
		    	 FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='PUNCS'  
		    	 SELECT   
		     	@UNINSUREDMOOTRISTBIPLIT= convert(varchar(20),COVERAGE_AMOUNT,101)   			-- CHECK FOR UNINSURED BISPLIT POLICY LIMIT FOR STATE INDIANA IN OTHERS CASE
		    	 FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='PUMSP'  
		     	SELECT   
		     	@UNDERINSUREDMOTORISTCSL= convert(varchar(20),COVERAGE_AMOUNT,101)   			-- CHECK FOR UNDERINSUREDMOTORIST CSL POLICY LIMIT FOR STATE INDIANA IN OTHERS CASE
		     	FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='UNCSL'  
		     	SELECT   
		     	@UNDERINSUREDMOTORISTBISPLIT= convert(varchar(20),COVERAGE_AMOUNT,101)   		-- CHECK FOR UNDERINSUREDMOTORIST BISPLIT POLICY LIMIT FOR STATE INDIANA IN OTHERS CASE
		     	FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='UNDSP'  
		    	END  
		   	IF(@APP_LOBOTHER = 6)  
		    	BEGIN  
		    	 SELECT   
		     	@DWELLINGFIRE_POLICY_LIMIT= convert(varchar(20),COVERAGE_AMOUNT,101)   	-- CHECK FOR DWELLING FIRE POLICY LIMIT FOR STATE INDIANA IN OTHERS CASE
		     	FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='PL'  
		    	END  
		     
		   	IF(@APP_LOBOTHER = 4)  
		   	 BEGIN  
		    	 SELECT   
		    	 @WATERCRAFT_POLICY_LIMIT= convert(varchar(20),COVERAGE_AMOUNT)   -- CHECK FOR WATERCRAFT POLICY LIMIT FOR STATE INDIANA IN OTHERS CASE
		    	 FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and (LTRIM(RTRIM(COV_CODE))='LCCSL'  )
		    	END  
		  	-- SELECT @WATERCRAFT_POLICY_LIMIT    
		   end  
		
		--SELECT * FROM APP_UMBRELLA_UNDERLYING_POLICIES WHERE  CUSTOMER_ID=1141 and APP_ID = 35 and APP_VERSION_ID = 1  and COV_CODE='LCCSL'  
--end
		--INSERT INTO APP_UMBRELLA_UNDERLYING_POLICIES VALUES (1141,25,1,'76787YUY','4','FDGFG',NULL,'2006-02-09 00:00:00.000','2007-02-09 00:00:00.000',568566,'Y',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)
		--INSERT INTO APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES VALUES (1141,25,1,'76787YUY', 'Watercraft Liability',57365985,NULL,NULL,NULL,NULL,NULL,0,'LCCSL')
		--WHERE  CUSTOMER_ID=114 and APP_ID = @APPID and APP_VERSION_ID = @APPVERSIONID AND POLICY_NUMBER = @UMBRELLA_APPNUMBER and COV_CODE='LCCSL'  
		---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		  
		   -- others case coverages  (end)
		---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
end     ---------------------------------------------
--others policy case(end)
---------------------------------------------    
          
FETCH NEXT FROM  APPNUMBER_CURSOR INTO @UMBRELLA_APPNUMBER           
END  ---END WHILE          
          
          
CLOSE APPNUMBER_CURSOR          
DEALLOCATE APPNUMBER_CURSOR  

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- MATURED AGE CHECK(START)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF(UPPER(@MATUREAGEDISCOUNFORWOLVERINEVOILATIONS) = 'Y' OR UPPER(@MATUREAGEDISCOUNFORWOLVERINEAGE) = 'Y' OR UPPER(@DRIVERAGEOTHERS) = 'Y')
	SET @MATURE_DISCOUNT='Y'
ELSE
	SET @MATURE_DISCOUNT ='N'
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- MATURED AGE CHECK(END)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- TOTAL NUMBER OF AUTOMOBILE(START)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF(@AUTOMOBILEWITHOTHERS IS NULL)
	SET  @AUTOMOBILEWITHOTHERS =0

IF(@AUTOMOBILEWITHWOLVERINE IS NULL)
	SET  @AUTOMOBILEWITHWOLVERINE =0

SET @AUTOMOBILE = ISNULL((@AUTOMOBILEWITHOTHERS + @AUTOMOBILEWITHWOLVERINE),'0')

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- TOTAL NUMBER OF AUTOMOBILE(END)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- TOTAL NUMBER OF MOTORHOMES(START)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF(@MOTORHOMESOTHERS IS NULL)
	SET  @MOTORHOMESOTHERS =0

IF(@MOTORHOMESWITHWOLVERINE IS NULL)
	SET  @MOTORHOMESWITHWOLVERINE =0

SET @MOTORHOMES = ISNULL((@MOTORHOMESOTHERS + @MOTORHOMESWITHWOLVERINE),'0')

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- TOTAL NUMBER OF MOTORHOMES(END)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- TOTAL NUMBER OF MOTORCYCLES(START)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF(@MOTORCYCLESWITHEWOLVERINE IS NULL)
	SET  @MOTORCYCLESWITHEWOLVERINE =0

IF(@MOTORCYCLESOTHERS IS NULL)
	SET  @MOTORCYCLESOTHERS =0

SET @MOTORCYCLES = ISNULL((@MOTORCYCLESWITHEWOLVERINE + @MOTORCYCLESOTHERS),'0')

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- TOTAL NUMBER OF MOTORCYCLES(END)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- TOTAL NUMBER OF DRIVERS(START)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
if (   @AUTOMOBILE_DRIVERWITHWOLVERINE is null)
	set @AUTOMOBILE_DRIVERWITHWOLVERINE ='0'
     
if (   @MOTORHOMEDRIVERWITHWOLVERINE is null)
	set @MOTORHOMEDRIVERWITHWOLVERINE ='0'

if (   @MOTORCYCLEDRIVERWITHWOLVERINE is null)
	set @MOTORCYCLEDRIVERWITHWOLVERINE ='0'

if (   @TOTALNUMER_OF_DRIVERSOTHERS is null)
	set @TOTALNUMER_OF_DRIVERSOTHERS ='0'
          
   	SET @TOTALNUMER_OF_DRIVERS = isnull((@AUTOMOBILE_DRIVERWITHWOLVERINE + @MOTORHOMEDRIVERWITHWOLVERINE + @TOTALNUMER_OF_DRIVERSOTHERS + @TOTALNUMER_OF_DRIVERSOTHERS),0)   
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- TOTAL NUMBER OF DRIVERS(END)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- TOTAL NUMBER OF INEXPERIENCE DRIVERS AUTO (START)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
if (   @INEXPERIENCEDRIVERAUTOWITHWOLVERINE is null)
set @INEXPERIENCEDRIVERAUTOWITHWOLVERINE ='0'
     
if (   @AUTOINEXPDRIVERAGEOTHERS is null)
set @AUTOINEXPDRIVERAGEOTHERS ='0'

  	SET @AUTOMOBILE_INEXPERIENCED_DRIVER = isnull((@INEXPERIENCEDRIVERAUTOWITHWOLVERINE + @AUTOINEXPDRIVERAGEOTHERS),0)   
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- TOTAL NUMBER OF INEXPERIENCE DRIVERS AUTO (END)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- TOTAL NUMBER OF INEXPERIENCE DRIVERS MOTORHOME (START)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
if (   @INEXPERIENCEDRIVERMOTORHOMEWITHWOLVERINE is null)
set @INEXPERIENCEDRIVERMOTORHOMEWITHWOLVERINE ='0'
     
if (   @MOTORHOMEINEXPDRIVEROTHERS is null)
set @MOTORHOMEINEXPDRIVEROTHERS ='0'

  	SET @MOTORHOMES_INEXPERIENCED_DRIVER = isnull((@INEXPERIENCEDRIVERMOTORHOMEWITHWOLVERINE + @MOTORHOMEINEXPDRIVEROTHERS),0)   
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- TOTAL NUMBER OF INEXPERIENCE DRIVERS MOTORHOME (END)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- TOTAL NUMBER OF INEXPERIENCE DRIVERS MOTORCYCLE (START)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
if (   @INEXPERIENCEDRIVERMOTORCYCLEWITHWOLVERINE is null)
set @INEXPERIENCEDRIVERMOTORCYCLEWITHWOLVERINE ='0'
     
if (   @MOTORCYCLEINEXPDRIVEROTHERS is null)
set @MOTORCYCLEINEXPDRIVEROTHERS ='0'

  	SET @MOTORCYCLES_INEXPERIENCED_DRIVER = isnull((@INEXPERIENCEDRIVERMOTORCYCLEWITHWOLVERINE + @MOTORCYCLEINEXPDRIVEROTHERS),0)   
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- TOTAL NUMBER OF INEXPERIENCE DRIVERS MOTORCYCLE (END)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------





-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
     -- coverages null check
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
  


	if (@WATERCRAFT_POLICY_LIMIT is NULL)
	SET @WATERCRAFT_POLICY_LIMIT = '0'
	
	if(@DWELLINGFIRE_POLICY_LIMIT is null)
	set @DWELLINGFIRE_POLICY_LIMIT = '0'
	  
	if(@UNDERINSUREDMOTORISTBISPLIT is null)
	set @UNDERINSUREDMOTORISTBISPLIT = '0'
	
	if(@UNDERINSUREDMOTORISTCSL is null)
	set @UNDERINSUREDMOTORISTCSL = '0'
	
	if(@UNINSUREDMOOTRISTBIPLIT is null)
	set @UNINSUREDMOOTRISTBIPLIT = '0'
	
	if(@UNINSUREDMOTORISTCSL is null)
	set @UNINSUREDMOTORISTCSL = '0'
	
	if(@MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT is null)
	set @MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT = '0'
	
	IF(@MOTORPD IS NULL)
	SET @MOTORPD = '0'
	
	IF(@MOTORCSL IS NULL)
	SET @MOTORCSL = '0'
	
	if(@HOMEOWNERS_POLICY_LIMIT is null)
	set @HOMEOWNERS_POLICY_LIMIT = '0'
	
	if(@PERSONALAUTOPOLICYLOWERLIMIT is null)
	set @PERSONALAUTOPOLICYLOWERLIMIT = '0'
	
	if(@MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT is null)
	set @MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT = '0'
	
	if(@AUTOPD is null)
	set @AUTOPD = '0'

	if(@AUTOCSL is null)
	set @AUTOCSL = '0'


	if(@PERSONALAUTOPOLICYUPPERLIMIT is null)
	set @PERSONALAUTOPOLICYUPPERLIMIT = '0'

	if(@MOTORCYCLES is null)
	set @MOTORCYCLES = '0'


-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
  
    
     
   -- EXCLUDE UNINSUREDMOTORISTFACTOR          
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
          
   --UNINSURUNDERINSURED MOTORIST LIMIT          
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  END
      
           
---------------------------RETURN VALUES--------------------------            
BEGIN            
SELECT            
 @LOB_ID           AS LOB_ID,          
 @TERITORYCODES   AS TERRITORYCODES,          
 @STATENAME    AS STATENAME,          
 'Y'           AS NEWBUSINESSFACTOR,            
 @QUOTEEFFDATE          AS QUOTEEFFDATE,                                                                   
 @QUOTEEXPDATE         AS QUOTEEXPDATE,            
 @QQ_NUMBER       AS QQNUMBER,            
 @COUNTYS          AS COUNTY,                  
 @UMBRELLA_LIMIT   AS UUMBRELLA_LIMIT,                   
 @MATURE_DISCOUNT   AS MATUREDISCOUNT,        
 
 ------------- Personal_Exposer----------------------------------            
 @OFFICE_PROMISES       AS OFFICEPREMISES,          
 --  '0'       AS OFFICEPREMISES,          
 @RENTAL_DWELLING_UNITS        AS RENTALDWELLINGUNIT,            
 --  '0'        AS RENTALDWELLINGUNIT,            
------------ UnderLyingPolicy-LiabilityLimit--------------------            
 @WATERCRAFT_POLICY_LIMIT     AS WATERCRAFTPOLICY, 
 @HOMEOWNERS_POLICY_LIMIT     AS HOMEOWNERPOLICY,               
 @DWELLINGFIRE_POLICY_LIMIT     AS DWELLINGFIREPOLICY,               
                
 @PERSONALAUTOPOLICYLOWERLIMIT     AS PERSONALAUTOPOLICYLOWERLIMIT,           
 @PERSONALAUTOPOLICYUPPERLIMIT AS PERSONALAUTOPOLICYUPPERLIMIT,         
 @AUTOPD AS AUTOPD,      
 @AUTOCSL AS AUTOCSL,  
 @MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT      AS MOTORCYCLEPOLICYLOWERLIMIT,           
 @MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT AS MOTORCYCLEPOLICYUPPERLIMIT,  
 @MOTORPD  as MOTORPD,
 @MOTORCSL as MOTORCSL,    
        
 @UNDERLYING_MOTORIST_LIMIT      AS UNINSUNDERINSMOTORISTLIMIT,          
 @UNINSUREDMOTORISTCSL AS    UNINSUNDERINSMOTORISTLIMITCSL,          
 @UNINSUREDMOOTRISTBIPLIT AS UNINSINSMOTORISTLIMITBISPLIT,          
 @UNDERINSUREDMOTORISTCSL AS UNDERINSMOTORISTLIMITCSL,          
 @UNDERINSUREDMOTORISTBISPLIT AS UNDERINSMOTORISTLIMITBISPLIT,          
------------- AutoMobile_Motor_Vehicle_Exposer------------------            
 @TOTALNUMER_OF_DRIVERS         AS TOTALNUMBERDRIVERS,            
 @AUTOMOBILE            AS AUTOMOBILES,            
 @AUTOMOBILE_INEXPERIENCED_DRIVER       AS INEXPDRIVERSAUTO,          
 @MOTORHOMES            AS MOTOTHOMES,            
 @MOTORHOMES_INEXPERIENCED_DRIVER       AS INEXPDRIVERSMOTORHOME,          
 @MOTORCYCLES      AS MOTORCYCLES,                  
 @MOTORCYCLES_INEXPERIENCED_DRIVER      AS INEXPDRIVERSMOTORCYCL,            
 --@EXCLUDE_UNINSURED_MOTORIST       AS EXCLUDEUNINSMOTORIST            
  'N'  AS EXCLUDEUNINSMOTORIST                                                       
 END                                                              
        
   
      
    
 --EXECUTE Proc_GetRatingInformationForUMB 1141,37,1
  







GO

