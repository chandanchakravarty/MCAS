IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMotorcycleCoverageRatingInformationForUMB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMotorcycleCoverageRatingInformationForUMB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                                    
Proc Name           : Proc_GetMotorcycleCoverageRatingInformationForUMB                                                                          
Created by          : Neeraj singh                                                                                   
Date                : 10-01-2006                                                                                    
Purpose             : To get the information for creating the input xml                                                                                     
Revison History     :                                                                                    
Used In             : Wolverine                                                                                    
------------------------------------------------------------                                                                                    
Date     Review By          Comments                                                                                    
------   ------------       -------------------------*/                              
                            
-- DROP PROC dbo.Proc_GetMotorcycleCoverageRatingInformationForUMB              
CREATE     PROC dbo.Proc_GetMotorcycleCoverageRatingInformationForUMB                     
 (                    
  @CUSTOMER_ID		INT,                                                                                    
  @ID           	INT,                                                                                    
  @VERSION_ID      	INT,                  
  @UMBRELLA_POLICY_ID 	nvarchar(20),                    
  @DATA_ACCESS_POINT 	INT,
  @POLICY_COMPANY	NVARCHAR(300)                            
  )                                              
                    
AS                                                                             
BEGIN                                                                                    
                            
SET QUOTED_IDENTIFIER OFF                     
                         
DECLARE @MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT nvarchar(100)                                      
DECLARE @MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT nvarchar(100)                                      
DECLARE @EXCLUDE_UNINSURED_MOTORIST nvarchar(100)                       
DECLARE @UNDERLYING_MOTORIST_LIMIT nvarchar(100)                                
DECLARE @UNINSUREDMOTORISTCSL NVARCHAR(20)                              
DECLARE @UNINSUREDMOOTRISTBIPLIT NVARCHAR(20)          
DECLARE @UNINSUREDMOOTRISTBIPLITLOWER NVARCHAR(20)          
DECLARE @UNINSUREDMOOTRISTBIPLITUPPER NVARCHAR(20)                              
DECLARE @UNDERINSUREDMOTORISTCSL NVARCHAR(20)                              
DECLARE @UNDERINSUREDMOTORISTBISPLIT NVARCHAR(20)                         
DECLARE @MOTORCYCLES         INT                                      
DECLARE @MOTORCYCLES_INEXPERIENCED_DRIVER INT                                      
DECLARE @STATE_ID    SMALLINT                    
DECLARE @MOTORPD VARCHAR(20)                        
DECLARE @MOTORCSL VARCHAR(20)                      
DECLARE @TOTALNUMER_OF_DRIVERSOTHERS INT                     
DECLARE @UNINSUREDMOTORISTCSLREJECT NVARCHAR(20)                     
DECLARE @UNINSUREDMOOTRISTBIPLITREJECT NVARCHAR(20)                     
DECLARE @UNDERINSUREDMOTORISTCSLREJECT NVARCHAR(20)                     
DECLARE @UNDERINSUREDMOTORISTBISPLITREJECT NVARCHAR(20)                     
                    
DECLARE @DRIVERAGEOTHERS nvarchar(20)                    
DECLARE @MATUREAGEDISCOUNFORWOLVERINEVOILATIONS  VARCHAR(20)                    
DECLARE @TOTALNUMER_OF_MOTORCYCLE_DRIVERS   INT                    
DECLARE @MATUREAGEDISCOUNTMOTORCYCLE     VARCHAR(20)         
                   
SET @MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT ='0'                   
SET @MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT ='0'                    
SET @MOTORCYCLES =0                    
SET @MOTORCYCLES_INEXPERIENCED_DRIVER =0                    
SET @TOTALNUMER_OF_MOTORCYCLE_DRIVERS =0                    
SET @MOTORPD ='0'                
SET @MOTORCSL ='0'                    
SET @TOTALNUMER_OF_DRIVERSOTHERS =0                    
                    
------------ UnderLyingPolicy-AUTO LiabilityLimit--------------------                          
                    
                    
--- CONSTANTS                    
DECLARE @LOOKUPVALUE_YES int                                  
DECLARE @LOOKUPVALUE_NO int                    
DECLARE @MATURE_AGE_LL int                    
DECLARE @MATURE_AGE_UL int                    
DECLARE @STATE_ID_MICHIGAN int                    
DECLARE @STATE_ID_INDIANA int                  
DECLARE @STATE_ID_WISCONSIN int                    
DECLARE @POLICY int                    
DECLARE @APPLICTION int                    
DECLARE @OTHERS int                    
DECLARE @MOTORCYCLE_BI_SPLIT_MICIGAN int                    
DECLARE @MOTORCYCLE_BI_SPLIT__INDIANA int                    
DECLARE @MOTORCYCLE_PD_MICIGAN int                    
DECLARE @MOTORCYCLE_PD_INDIANA int                    
DECLARE @MOTORCYCLE_CSL_INDIANA int                    
DECLARE @MOTORCYCLE_CSL_MICIGAN int                    
DECLARE @BISPLIT_INDIANA_COV_CODE NVARCHAR(20)                    
DECLARE @BISPLIT_MICIGAN_COV_CODE NVARCHAR(20)                    
DECLARE @PD_INDIANA_COV_CODE NVARCHAR(20)                    
DECLARE @PD_MICIGAN_COV_CODE NVARCHAR(20)                    
DECLARE @INEXPERINCE_DRIVERS_UPPER_AGE int                    
DECLARE @INEXPERINCE_DRIVERS_EXPERINCE_AGE int                 
 DECLARE @UNINSUREDMOTORIST_CSL_MICIGAN int                    
DECLARE @UNINSUREDMOTORIST_CSL_INDIANA int                    
DECLARE @UNINSUREDMOTORIST_BISPLIT_MICIGAN int                    
DECLARE @UNINSUREDMOTORIST_BISPLIT_INDIANA int                    
DECLARE @UNDERINSUREDMOTORIST_BISPLIT_INDIANA int                    
DECLARE @UNDERINSUREDMOTORIST_CSL_INDIANA int                    
DECLARE @MOTORCYCL_CSL_MICIGAN_COV_CODE NVARCHAR(20)                    
DECLARE @MOTORCYCL_CSL_INDIANA_COV_CODE NVARCHAR(20)                    
DECLARE @UNINSUREDMOTORIST_CSL_MICIGAN_COV_CODE NVARCHAR(20)                    
DECLARE @UNINSUREDMOTORIST_BISPLIT_MICIGAN_COV_CODE NVARCHAR(20)                    
DECLARE @UNINSUREDMOTORIST_BISPLIT_INDIANA_COV_CODE NVARCHAR(20)                    
DECLARE @UNINSUREDMOTORIST_CSL_INDIANA_COV_CODE NVARCHAR(20)                    
DECLARE @UNDERINSUREDMOTORIST_CSL_INDIANA_COV_CODE NVARCHAR(20)                    
DECLARE @UNDERINSUREDMOTORIST_BISPLIT_INDIANA_COV_CODE NVARCHAR(20)                    
DECLARE @VEHICLE_ID SMALLINT                    
DECLARE @UNDERINSUREDMOTORIST_BISPLIT_INDIANA_REJECT NVARCHAR(20)                    
DECLARE @UNDERINSUREDMOTORIST_CSL_INDIANA_REJECT NVARCHAR(20)                    
DECLARE @UNINSUREDMOTORIST_BISPLIT_INDIANA_REJECT NVARCHAR(20)                    
DECLARE @UNINSUREDMOTORIST_CSL_INDIANA_REJECT NVARCHAR(20)                    
              
SET @VEHICLE_ID = 1                    
SET @LOOKUPVALUE_YES =1096                    
SET @LOOKUPVALUE_NO =10964                    
SET @MATURE_AGE_LL =50                    
SET @MATURE_AGE_UL =70                     
SET @STATE_ID_MICHIGAN =22                    
SET @STATE_ID_INDIANA =14                    
SET @STATE_ID_WISCONSIN =49                    
SET @POLICY =1                    
SET @APPLICTION =2                    
SET @OTHERS =3                    
SET @MOTORCYCLE_BI_SPLIT_MICIGAN =207                    
SET @MOTORCYCLE_BI_SPLIT__INDIANA =127                    
SET @MOTORCYCLE_PD_MICIGAN =208      
SET @MOTORCYCLE_PD_INDIANA =128                    
SET @MOTORCYCLE_CSL_MICIGAN =206    --select * from mnt_coverage where COV_id=128 and state_id=14                
SET @MOTORCYCLE_CSL_INDIANA =126                    
SET @UNINSUREDMOTORIST_CSL_INDIANA_REJECT ='REJECT'                    
SET @UNINSUREDMOTORIST_BISPLIT_INDIANA_REJECT ='REJECT'                    
SET @UNDERINSUREDMOTORIST_CSL_INDIANA_REJECT ='REJECT'                    
SET @UNDERINSUREDMOTORIST_BISPLIT_INDIANA_REJECT ='REJECT'             
                    
                    
SET @BISPLIT_INDIANA_COV_CODE ='BISPL'                    
SET @BISPLIT_MICIGAN_COV_CODE ='BISPL'                    
SET @MOTORCYCL_CSL_MICIGAN_COV_CODE ='RLCSL'                    
SET @MOTORCYCL_CSL_INDIANA_COV_CODE ='RLCSL'                    
                    
SET @UNINSUREDMOTORIST_CSL_MICIGAN_COV_CODE ='PUNCS'                    
SET @UNINSUREDMOTORIST_BISPLIT_MICIGAN_COV_CODE ='PUMSP'                    
SET @UNINSUREDMOTORIST_CSL_INDIANA_COV_CODE='PUNCS'                     
SET @UNINSUREDMOTORIST_BISPLIT_INDIANA_COV_CODE='PUMSP'                     
SET @UNDERINSUREDMOTORIST_CSL_INDIANA_COV_CODE='UNCSL'                    
SET @UNDERINSUREDMOTORIST_BISPLIT_INDIANA_COV_CODE='UNDSP'                    
SET @PD_INDIANA_COV_CODE='PD'                 
SET @PD_MICIGAN_COV_CODE='PD'                       
SET @INEXPERINCE_DRIVERS_UPPER_AGE=25                    
SET @INEXPERINCE_DRIVERS_EXPERINCE_AGE=3                    
SET @UNINSUREDMOTORIST_CSL_MICIGAN = 211                    
SET @UNINSUREDMOTORIST_CSL_INDIANA = 131                    
SET @UNINSUREDMOTORIST_BISPLIT_INDIANA = 132                    
SET @UNDERINSUREDMOTORIST_BISPLIT_INDIANA = 214                    
SET @UNDERINSUREDMOTORIST_CSL_INDIANA = 133                    
                    
                    
                    
SET @UNINSUREDMOTORIST_BISPLIT_MICIGAN = 212                    
IF ( @DATA_ACCESS_POINT = @POLICY)                    
BEGIN                    
                    
 SELECT @STATE_ID=STATE_ID -- Check for state id                     
 FROM POL_CUSTOMER_POLICY_LIST                                
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@ID AND POLICY_VERSION_ID=@VERSION_ID                    
                
DECLARE VEHICLE_ID_CURSOR CURSOR FOR                  
SELECT               
  TOP 1 POL_VEHICLES.VEHICLE_ID               
  FROM POL_VEHICLES INNER JOIN POL_DRIVER_ASSIGNED_VEHICLE ON POL_VEHICLES.CUSTOMER_ID=POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID AND POL_VEHICLES.POLICY_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID AND POL_VEHICLES.POLICY_VERSION_ID=POL_DRIVER_ASSIGNED_VEHICLE




 
 
    
      
         
         
            
.POLICY_VERSION_ID              
 WHERE POL_VEHICLES.CUSTOMER_ID=@CUSTOMER_ID AND POL_VEHICLES.POLICY_ID=@ID AND POL_VEHICLES.POLICY_VERSION_ID=@VERSION_ID AND POL_VEHICLES.IS_ACTIVE='Y' --AND  POL_VEHICLES.APP_VEHICLE_PERTYPE_ID != @SUSPENDED_COM_LOOKUP_VALUE                
 OPEN VEHICLE_ID_CURSOR                   
     FETCH NEXT FROM  VEHICLE_ID_CURSOR INTO @VEHICLE_ID                   
              
     CLOSE VEHICLE_ID_CURSOR                  
     DEALLOCATE VEHICLE_ID_CURSOR               
              
                    
 IF (@STATE_ID = @STATE_ID_MICHIGAN)   -- AUTO POLICY FOR MICIGAN                        
  BEGIN                     
           SELECT                               
           @MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT = convert(varchar(20),ISNULL(LIMIT_1,0)),   -- BI SPLIT LOWER AND UPPER LIMIT FOR MICIGAN                    
    @MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT = convert(varchar(20),ISNULL(LIMIT_2,0))                                 
           FROM POL_VEHICLE_coverages  WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@MOTORCYCLE_BI_SPLIT_MICIGAN ) and VEHICLE_ID = @VEHICLE_ID       
                               
    SELECT                       
           @MOTORCSL = convert(varchar(20),ISNULL(LIMIT_1,0))             -- CSL LIMIT FOR WOLVERINE MOTORCYCLE POLICY                    
           FROM POL_VEHICLE_coverages  WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@MOTORCYCLE_CSL_MICIGAN)  and VEHICLE_ID = @VEHICLE_ID                           
                              
    SELECT                               
           @MOTORPD = convert(varchar(20),ISNULL(LIMIT_1,0))             -- PD LIMIT FOR WOLVERINE MOTOR CYCLE POLICY IN MICIGAN                    
           FROM POL_VEHICLE_coverages  WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@MOTORCYCLE_PD_MICIGAN)  and VEHICLE_ID = @VEHICLE_ID                           
         
  END                    
 IF (@STATE_ID = @STATE_ID_INDIANA)   -- MOTORCYCLE POLICY FOR INDIANA                      
  BEGIN                    
   SELECT                               
           @MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT = convert(varchar(20),ISNULL(LIMIT_1,0)),   -- CHECK FOR BISPLIT LIMIT FOR INDIANA WOLVERINE CUSTOMER                    
    @MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT = convert(varchar(20),ISNULL(LIMIT_2,0))                                 
           FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@MOTORCYCLE_BI_SPLIT__INDIANA )  and VEHICLE_ID = @VEHICLE_ID                         
                               
    SELECT                               
           @MOTORCSL = convert(varchar(20),ISNULL(LIMIT_1,0))              -- CHECK FOR CSL LIMIT FOR INDIANA WOLVERINE CUSTOMER                    
           FROM POL_VEHICLE_coverages  WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@MOTORCYCLE_CSL_INDIANA)  and VEHICLE_ID = @VEHICLE_ID                         
                             
    SELECT                               
           @MOTORPD = convert(varchar(20),ISNULL(LIMIT_1,0))              -- CHECK FOR PD LIMIT FOR INDIANA WOLVERINE CUSTOMER                    
           FROM POL_VEHICLE_coverages  WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@MOTORCYCLE_PD_INDIANA)  and VEHICLE_ID = @VEHICLE_ID                           
 
 END                      
                        
                    
  -- CHECK FOR MATURE AGE DISCOUNT IN CASE OF DRIVER IS WITH WOLVERINE(VIOLATIONS)                    
  IF EXISTS ( SELECT  *                     
    FROM  POL_DRIVER_DETAILS WITH (NOLOCK)                     
    INNER JOIN POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND                   
POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND                   
POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID                    
           WHERE POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=@CUSTOMER_ID                              
            AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=@ID                           
            AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=@VERSION_ID                    
     AND ISNULL(VIOLATIONS,@LOOKUPVALUE_YES) = @LOOKUPVALUE_NO                      
     AND ((( DATEDIFF(YEAR,POL_DRIVER_DETAILS.DRIVER_DOB,POL_CUSTOMER_POLICY_LIST.APP_EFFECTIVE_DATE) > @MATURE_AGE_LL ) AND DATEDIFF(YEAR,POL_DRIVER_DETAILS.DRIVER_DOB,POL_CUSTOMER_POLICY_LIST.APP_EFFECTIVE_DATE) < @MATURE_AGE_UL )))                    





    SET @MATUREAGEDISCOUNFORWOLVERINEVOILATIONS = 'Y'                    
  ELSE                    
     SET @MATUREAGEDISCOUNFORWOLVERINEVOILATIONS = 'N'                    
                    
                 
       -- NUMBER OF MOTORCYCLES       
                        
          SELECT                               
          @MOTORCYCLES = ISNULL(COUNT(VEHICLE_ID),0)                              
          FROM POL_VEHICLES                               
          WHERE CUSTOMER_ID=@CUSTOMER_ID                              
          AND POLICY_ID=@ID                              
          AND POLICY_VERSION_ID=@VERSION_ID                               
          AND MOTORCYCLE_TYPE is not null                         
                                
         ------------------------------------------------------------                    
     -- MOTORCYCLE INEXPERIENCED DRIVER                              
         -- NO OF driverS in the household under age @INEXPERINCE_DRIVERS_UPPER_AGE AND                     
      -- NO OF DIRVERS with less than three (@INEXPERINCE_DRIVERS_EXPERINCE_AGE) years driving experience.                            
         -------------------------------------------------------------                    
    SELECT -- CHECK INEXPERINCED DRIVER AUTO (PERSONAL AUTO EXCLUDING MOTORHOME AND COMMERCIAL-PERSONAL UMBRELLA DOESNT CONSIDER THESE 2 IN AUTO)                    
    @MOTORCYCLES_INEXPERIENCED_DRIVER= count(POL_DRIVER_DETAILS.DRIVER_ID)                     
    FROM POL_DRIVER_DETAILS                     
     /*INNER JOIN POL_DRIVER_ASSIGNED_VEHICLE ON                    
     POL_DRIVER_DETAILS.CUSTOMER_ID=POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID                     
     AND POL_DRIVER_DETAILS.POLICY_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID                      
     AND POL_DRIVER_DETAILS.POLICY_VERSION_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_VERSION_ID                     
     AND  POL_DRIVER_DETAILS.DRIVER_ID=POL_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID     */               
                        
     INNER JOIN POL_VEHICLES ON                     
     POL_DRIVER_DETAILS.CUSTOMER_ID=POL_VEHICLES.CUSTOMER_ID                     
     AND POL_DRIVER_DETAILS.POLICY_ID=POL_VEHICLES.POLICY_ID                      
     AND POL_DRIVER_DETAILS.POLICY_VERSION_ID=POL_VEHICLES.POLICY_VERSION_ID                     
     AND  POL_DRIVER_DETAILS.VEHICLE_ID=POL_VEHICLES.VEHICLE_ID                    
                        
     INNER JOIN POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND                   
 POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND                   
 POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID                    
     WHERE POL_CUSTOMER_POLICY_LIST.customer_id=@CUSTOMER_ID                    
     and POL_CUSTOMER_POLICY_LIST.policy_id=@ID                    
     and POL_CUSTOMER_POLICY_LIST.policy_version_id=@VERSION_ID                    
                         
     GROUP BY DRIVER_DOB,APP_EFFECTIVE_DATE,DATE_LICENSED , POL_VEHICLES.MOTORCYCLE_TYPE                     
     HAVING (( DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) <@INEXPERINCE_DRIVERS_UPPER_AGE) OR ( DATEDIFF(YEAR,DATE_LICENSED,APP_EFFECTIVE_DATE) <@INEXPERINCE_DRIVERS_EXPERINCE_AGE))                    
      and POL_VEHICLES.MOTORCYCLE_TYPE is not null                    
     -- check total number of drivers                    
               
 SELECT                     
     @TOTALNUMER_OF_MOTORCYCLE_DRIVERS= count(POL_DRIVER_DETAILS.DRIVER_ID)                     
     FROM POL_DRIVER_DETAILS                     
       WHERE POL_DRIVER_DETAILS.customer_id=@CUSTOMER_ID                    
       and POL_DRIVER_DETAILS.policy_id=@ID                    
       and POL_DRIVER_DETAILS.policy_version_id=@VERSION_ID                    
                   
                
                  
                         
END                    
IF ( @DATA_ACCESS_POINT = @APPLICTION)                    
BEGIN                    
              
SELECT @STATE_ID=STATE_ID -- Check for state id                     
 FROM APP_LIST       
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@ID AND APP_VERSION_ID=@VERSION_ID                
              
-- check for top eligible vehicle_id                
              
DECLARE VEHICLE_ID_CURSOR CURSOR FOR                  
SELECT               
  TOP 1 APP_VEHICLES.VEHICLE_ID               
  FROM APP_VEHICLES INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE ON APP_VEHICLES.CUSTOMER_ID=APP_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID AND APP_VEHICLES.APP_ID=APP_DRIVER_ASSIGNED_VEHICLE.APP_ID AND APP_VEHICLES.APP_VERSION_ID=APP_DRIVER_ASSIGNED_VEHICLE.APP_VERSION_ID              
 WHERE APP_VEHICLES.CUSTOMER_ID=@CUSTOMER_ID AND APP_VEHICLES.APP_ID=@ID AND APP_VEHICLES.APP_VERSION_ID=@VERSION_ID AND APP_VEHICLES.IS_ACTIVE='Y'-- AND  APP_VEHICLES.VEHICLE_TYPE_PER != @SUSPENDED_COM_LOOKUP_VALUE                
 OPEN VEHICLE_ID_CURSOR                   
     FETCH NEXT FROM  VEHICLE_ID_CURSOR INTO @VEHICLE_ID                   
              
     CLOSE VEHICLE_ID_CURSOR                  
     DEALLOCATE VEHICLE_ID_CURSOR                   
                    
 IF (@STATE_ID = @STATE_ID_MICHIGAN)   -- AUTO POLICY FOR MICIGAN                        
  BEGIN                     
           SELECT                            @MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT = convert(varchar(20),ISNULL(LIMIT_1,0)), -- BI SPLIT LOWER AND UPPER LIMIT FOR MICIGAN                    
    @MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT = convert(varchar(20),ISNULL(LIMIT_2,0))                                 
           FROM APP_VEHICLE_coverages  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@MOTORCYCLE_BI_SPLIT_MICIGAN ) and VEHICLE_ID = @VEHICLE_ID                            
                               
    SELECT                               
           @MOTORCSL = convert(varchar(20),ISNULL(LIMIT_1,0))             -- CSL LIMIT FOR WOLVERINE MOTORCYCLE POLICY                    
           FROM APP_VEHICLE_coverages  WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@MOTORCYCLE_CSL_MICIGAN)  and VEHICLE_ID = @VEHICLE_ID                           
                              
    SELECT                               
           @MOTORPD = convert(varchar(20),ISNULL(LIMIT_1,0))             -- PD LIMIT FOR WOLVERINE MOTOR CYCLE POLICY IN MICIGAN                    
           FROM APP_VEHICLE_coverages  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@MOTORCYCLE_PD_MICIGAN)  and VEHICLE_ID = @VEHICLE_ID                           
                              
                 
  END                    
 IF (@STATE_ID = @STATE_ID_INDIANA)   -- MOTORCYCLE POLICY FOR INDIANA                
  BEGIN                    
    SELECT                               
           @MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT = convert(varchar(20),ISNULL(LIMIT_1,0)),   -- CHECK FOR BISPLIT LIMIT FOR INDIANA WOLVERINE CUSTOMER                    
    @MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT = convert(varchar(20),ISNULL(LIMIT_2,0))                                 
           FROM APP_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@MOTORCYCLE_BI_SPLIT__INDIANA )  and VEHICLE_ID = @VEHICLE_ID                         
                               
    SELECT                               
           @MOTORCSL = convert(varchar(20),ISNULL(LIMIT_1,0))              -- CHECK FOR CSL LIMIT FOR INDIANA WOLVERINE CUSTOMER                    
           FROM APP_VEHICLE_coverages  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@MOTORCYCLE_CSL_INDIANA)  and VEHICLE_ID = @VEHICLE_ID    
                             
    SELECT                       
           @MOTORPD = convert(varchar(20),ISNULL(LIMIT_1,0))              -- CHECK FOR PD LIMIT FOR INDIANA WOLVERINE CUSTOMER                    
           FROM APP_VEHICLE_coverages  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@MOTORCYCLE_PD_INDIANA)  and VEHICLE_ID = @VEHICLE_ID                           
                      
  END                      
                        
                   
  -- CHECK FOR MATURE AGE DISCOUNT IN CASE OF DRIVER IS WITH WOLVERINE(VIOLATIONS)                    
  IF EXISTS ( SELECT  *                     
    FROM  APP_DRIVER_DETAILS WITH (NOLOCK)                     
    INNER JOIN APP_LIST ON APP_LIST.CUSTOMER_ID=APP_DRIVER_DETAILS.CUSTOMER_ID AND APP_LIST.APP_ID=APP_DRIVER_DETAILS.APP_ID AND APP_LIST.APP_VERSION_ID=APP_DRIVER_DETAILS.APP_VERSION_ID                    
           WHERE APP_LIST.CUSTOMER_ID=@CUSTOMER_ID                              
            AND APP_LIST.APP_ID=@ID                           
            AND APP_LIST.APP_VERSION_ID=@VERSION_ID                    
     AND ISNULL(VIOLATIONS,@LOOKUPVALUE_YES) = @LOOKUPVALUE_NO                      
     AND ((( DATEDIFF(YEAR,APP_DRIVER_DETAILS.DRIVER_DOB,APP_LIST.APP_EFFECTIVE_DATE) > @MATURE_AGE_LL ) AND DATEDIFF(YEAR,APP_DRIVER_DETAILS.DRIVER_DOB,APP_LIST.APP_EFFECTIVE_DATE) < @MATURE_AGE_UL )))                    
    SET @MATUREAGEDISCOUNFORWOLVERINEVOILATIONS = 'Y'                    
  ELSE                    
     SET @MATUREAGEDISCOUNFORWOLVERINEVOILATIONS = 'N'                    
                    
                      
       -- NUMBER OF MOTORCYCLES                              
                        
          SELECT                               
          @MOTORCYCLES = ISNULL(COUNT(VEHICLE_ID),0)                              
          FROM APP_VEHICLES                               
          WHERE CUSTOMER_ID=@CUSTOMER_ID                              
          AND APP_ID=@ID                              
          AND APP_VERSION_ID=@VERSION_ID            
          AND MOTORCYCLE_TYPE !=''                         
                                
         ------------------------------------------------------------                    
     -- MOTORCYCLE INEXPERIENCED DRIVER                              
         -- NO OF driverS in the household under age @INEXPERINCE_DRIVERS_UPPER_AGE AND                     
      -- NO OF DIRVERS with less than three (@INEXPERINCE_DRIVERS_EXPERINCE_AGE) years driving experience.                            
         -------------------------------------------------------------                    
                    
            
    SELECT                     
    @MOTORCYCLES_INEXPERIENCED_DRIVER=count(distinct(APP_DRIVER_DETAILS.DRIVER_ID)) -- CHECK INEXPERINCED DRIVER MotorCycle                    
    FROM APP_DRIVER_DETAILS                     
                          
    inner JOIN APP_VEHICLES ON                     
                      
    APP_DRIVER_DETAILS.CUSTOMER_ID=APP_VEHICLES.CUSTOMER_ID                     
    AND APP_DRIVER_DETAILS.APP_ID=APP_VEHICLES.APP_ID                      
    AND APP_DRIVER_DETAILS.APP_VERSION_ID=APP_VEHICLES.APP_VERSION_ID                     
   -- AND  APP_DRIVER_DETAILS.VEHICLE_ID=APP_VEHICLES.VEHICLE_ID                    
                       
    INNER JOIN  APP_LIST ON APP_LIST.CUSTOMER_ID=APP_DRIVER_DETAILS.CUSTOMER_ID AND APP_LIST.APP_ID=APP_DRIVER_DETAILS.APP_ID AND APP_LIST.APP_VERSION_ID=APP_DRIVER_DETAILS.APP_VERSION_ID                    
    WHERE APP_LIST.customer_id=@CUSTOMER_ID                    
    and APP_LIST.APP_id=@ID                    
    and APP_LIST.APP_version_id=@VERSION_ID                    
    AND APP_VEHICLES.MOTORCYCLE_TYPE != ''                    
    AND (( DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) <@INEXPERINCE_DRIVERS_UPPER_AGE) OR ( DATEDIFF(YEAR,DATE_LICENSED,APP_EFFECTIVE_DATE) <@INEXPERINCE_DRIVERS_EXPERINCE_AGE))                    
                   
  SELECT             
  @TOTALNUMER_OF_MOTORCYCLE_DRIVERS=COUNT (APP_DRIVER_DETAILS.DRIVER_ID) -- CHECK TOTAL DRIVER MOTORCYCLE                    
  FROM APP_DRIVER_DETAILS             
       WHERE APP_DRIVER_DETAILS.CUSTOMER_ID=@CUSTOMER_ID                  
       AND APP_DRIVER_DETAILS.APP_ID=@ID                 
       AND APP_DRIVER_DETAILS.APP_VERSION_ID=@VERSION_ID              
              
                   
                 
                
                         
                    
END                    
                    
IF ( @DATA_ACCESS_POINT = @OTHERS)                    
BEGIN                    
                     
  SELECT @STATE_ID=STATE_ID -- Check for state id                     
  FROM APP_LIST                                
       WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@ID AND APP_VERSION_ID=@VERSION_ID                    
   -- CHECK FOR MATURED AGE DISCOUNT IN OTHERS POLICY CASE                    
  IF EXISTS(SELECT *                        
      FROM APP_LIST WITH (NOLOCK) INNER JOIN APP_UMBRELLA_DRIVER_DETAILS UDD ON                        
      APP_LIST.CUSTOMER_ID=UDD.CUSTOMER_ID AND APP_LIST.APP_ID=UDD.APP_ID AND APP_LIST.APP_VERSION_ID=UDD.APP_VERSION_ID                       
      INNER JOIN APP_UMBRELLA_UNDERLYING_POLICIES AUUP WITH (NOLOCK)
      ON UDD.CUSTOMER_ID=AUUP.CUSTOMER_ID AND UDD.APP_ID=AUUP.APP_ID AND UDD.APP_VERSION_ID=AUUP.APP_VERSION_ID
      WHERE APP_LIST.CUSTOMER_ID=@CUSTOMER_ID AND APP_LIST.APP_ID=@ID AND APP_LIST.APP_VERSION_ID=@VERSION_ID                      
      AND ((( DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) > @MATURE_AGE_LL ) AND DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) < @MATURE_AGE_UL ))                       
      AND AUUP.POLICY_COMPANY=@POLICY_COMPANY AND POLICY_NUMBER =@UMBRELLA_POLICY_ID) 
   SET @DRIVERAGEOTHERS='Y'                    
  ELSE                    
   SET @DRIVERAGEOTHERS='N'                    
                     
  IF (@STATE_ID = @STATE_ID_MICHIGAN)  -- auto  FOR MICIGAN                    
                     
   BEGIN                    
    SELECT                       
         @MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT = substring((convert(nvarchar(20),COVERAGE_AMOUNT)),1,charindex('/',(convert(nvarchar(20),COVERAGE_AMOUNT)))-1),  -- CHECK FORMOTORCYLE BI SPLIT LIMIT COVERAGE OF MICIGAN                    
     @MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT = substring((convert(nvarchar(20),COVERAGE_AMOUNT)),charindex('/',(convert(nvarchar(20),COVERAGE_AMOUNT)))+1,15)                         
         FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND POLICY_NUMBER = @UMBRELLA_POLICY_ID and COV_CODE=@BISPLIT_MICIGAN_COV_CODE AND POLICY_COMPANY=@POLICY_COMPANY  



                   
 SELECT                       
         @MOTORPD = convert(varchar(20),COVERAGE_AMOUNT)       -- CHECK FOR MOTORCYCLE POLICY PD LIMIT FOR STATE INDIANA IN OTHERS CASE                    
         FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND POLICY_NUMBER = @UMBRELLA_POLICY_ID and COV_CODE=@PD_MICIGAN_COV_CODE AND POLICY_COMPANY=@POLICY_COMPANY        


              
     SELECT                       
         @MOTORCSL = convert(varchar(20),COVERAGE_AMOUNT)        -- CHECK FOR MOTORCYCLE CSL LIMIT FOR STATE MICIGAN                    
         FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND POLICY_NUMBER = @UMBRELLA_POLICY_ID and COV_CODE=@MOTORCYCL_CSL_MICIGAN_COV_CODE AND POLICY_COMPANY=@POLICY_COMPANY                      
        
   END                    
                     
          
  IF (@STATE_ID = @STATE_ID_INDIANA)                  
BEGIN                    
                     
    SELECT                       
         @MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT = substring((convert(nvarchar(20),COVERAGE_AMOUNT)),1,charindex('/',(convert(nvarchar(20),COVERAGE_AMOUNT)))-1),   -- CHECK FOR MOTORCYCLE POLICY BISPLIT LIMIT FOR STATE INDIANA IN OTHERS CASE                



 

  
   
     @MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT = substring((convert(nvarchar(20),COVERAGE_AMOUNT)),charindex('/',(convert(nvarchar(20),COVERAGE_AMOUNT)))+1,15)                    
         FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND POLICY_NUMBER = @UMBRELLA_POLICY_ID and COV_CODE=@BISPLIT_MICIGAN_COV_CODE AND POLICY_COMPANY=@POLICY_COMPANY   


                   
         SELECT                       
         @MOTORCSL = convert(varchar(20),COVERAGE_AMOUNT)       -- CHECK FOR MOTORCYCLE POLICY CSL LIMIT FOR STATE INDIANA IN OTHERS CASE                    
         FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND POLICY_NUMBER = @UMBRELLA_POLICY_ID and COV_CODE=@MOTORCYCL_CSL_INDIANA_COV_CODE AND POLICY_COMPANY=@POLICY_COMPANY                      
                            
    SELECT                       
         @MOTORPD = convert(varchar(20),COVERAGE_AMOUNT)       -- CHECK FOR MOTORCYCLE POLICY PD LIMIT FOR STATE INDIANA IN OTHERS CASE                    
         FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND POLICY_NUMBER = @UMBRELLA_POLICY_ID and COV_CODE=@PD_INDIANA_COV_CODE AND POLICY_COMPANY=@POLICY_COMPANY        


              
                            
            
   END                      
                       
    -- NUMBER OF DRIVERS                      
                      
    SELECT                       
     @TOTALNUMER_OF_MOTORCYCLE_DRIVERS = COUNT(DRIVER_ID)   -- TOTAL NUMBER OF DRIVERS IN OTHERS CASE                    
     FROM  APP_UMBRELLA_DRIVER_DETAILS AUDD WITH (NOLOCK) 
     inner join  APP_UMBRELLA_VEHICLE_INFO AUVI 
     on AUDD.CUSTOMER_ID=AUVI.CUSTOMER_ID      
     AND AUDD.APP_ID=AUVI.APP_ID      
     AND AUDD.APP_VERSION_ID=AUVI.APP_VERSION_ID      
     AND AUDD.MOT_VEHICLE_ID=AUVI.VEHICLE_ID      
     INNER JOIN APP_UMBRELLA_UNDERLYING_POLICIES AUUP WITH (NOLOCK)
     ON AUVI.CUSTOMER_ID=AUUP.CUSTOMER_ID AND AUVI.APP_ID=AUUP.APP_ID AND AUVI.APP_VERSION_ID=AUUP.APP_VERSION_ID AND OTHER_POLICY=POLICY_NUMBER
     WHERE AUDD.CUSTOMER_ID=@CUSTOMER_ID AND AUDD.APP_ID= @ID AND AUDD.APP_VERSION_ID= @VERSION_ID AND AUVI.OTHER_POLICY=@UMBRELLA_POLICY_ID      
     AND AUUP.POLICY_COMPANY=@POLICY_COMPANY 
    -- number of motorcycles                      
                      
    SELECT                      
      @MOTORCYCLES =  COUNT(MOTORCYCLE_TYPE)  -- CHECK FOR NUMBER OF MOTOR CYCLE IN OTHERS CASE                    
      FROM  APP_UMBRELLA_VEHICLE_INFO AUVI WITH (NOLOCK) 
      INNER JOIN APP_UMBRELLA_UNDERLYING_POLICIES AUUP WITH (NOLOCK)
      ON AUVI.CUSTOMER_ID=AUUP.CUSTOMER_ID AND AUVI.APP_ID=AUUP.APP_ID AND AUVI.APP_VERSION_ID=AUUP.APP_VERSION_ID AND OTHER_POLICY=POLICY_NUMBER
      WHERE AUVI.CUSTOMER_ID = @CUSTOMER_ID AND AUVI.APP_ID = @ID AND AUVI.APP_VERSION_ID = @VERSION_ID AND MOTORCYCLE_TYPE != 11958 
      AND MOTORCYCLE_TYPE !=''  AND IS_EXCLUDED = 0 AND AUUP.POLICY_COMPANY=@POLICY_COMPANY AND OTHER_POLICY=@UMBRELLA_POLICY_ID 
                        
    DECLARE @VEHICLE_ID_NUMBER_MOTORCYCLE SMALLINT                      
    DECLARE @TOTAL_NUMBER_DRIVER INT      
 SET @TOTAL_NUMBER_DRIVER = 0    
    DECLARE VEHICLE_ID_CURSOR_MOTORCYCLE CURSOR FOR  -- CHECK FOR NUMBER OF INEXPERIENCE DRIVER IN MOTOR CYCLE IN OTHERS CASE                    

     SELECT                       
     VEHICLE_ID FROM APP_UMBRELLA_VEHICLE_INFO AUVI WITH (NOLOCK)
     INNER JOIN APP_UMBRELLA_UNDERLYING_POLICIES AUUP WITH (NOLOCK)
     ON AUVI.CUSTOMER_ID=AUUP.CUSTOMER_ID AND AUVI.APP_ID=AUUP.APP_ID AND AUVI.APP_VERSION_ID=AUUP.APP_VERSION_ID AND OTHER_POLICY=POLICY_NUMBER
     WHERE AUVI.CUSTOMER_ID=@CUSTOMER_ID AND AUVI.APP_ID =@ID AND AUVI.APP_VERSION_ID = @VERSION_ID AND MOTORCYCLE_TYPE IS NOT NULL  
     AND IS_EXCLUDED = 0 AND AUUP.POLICY_COMPANY=@POLICY_COMPANY
                        
     OPEN VEHICLE_ID_CURSOR_MOTORCYCLE                       
     FETCH NEXT FROM  VEHICLE_ID_CURSOR_MOTORCYCLE INTO @VEHICLE_ID_NUMBER_MOTORCYCLE                       
     WHILE @@FETCH_STATUS = 0                      
       BEGIN                       
        --SELECT @VEHICLE_ID_NUMBER_MOTORCYCLE                      
        SELECT                       
        @TOTAL_NUMBER_DRIVER =  COUNT(driver_id)                    
        FROM APP_UMBRELLA_DRIVER_DETAILS AUVI 
	INNER JOIN APP_LIST AL ON AUVI.customer_id=AL.CUSTOMER_ID AND AUVI.APP_ID=AL.APP_ID AND                   
	AUVI.APP_VERSION_ID=AL.APP_VERSION_ID
	INNER JOIN APP_UMBRELLA_UNDERLYING_POLICIES AUUP WITH (NOLOCK)
	ON AUVI.CUSTOMER_ID=AUUP.CUSTOMER_ID AND AUVI.APP_ID=AUUP.APP_ID AND AUVI.APP_VERSION_ID=AUUP.APP_VERSION_ID  
	where AL.customer_id=@CUSTOMER_ID and AL.app_id=@ID AND                   
	AL.APP_VERSION_ID=@VERSION_ID  and AUVI.MOT_VEHICLE_ID = @VEHICLE_ID_NUMBER_MOTORCYCLE                      
        AND ((DATEDIFF(YEAR,AUVI.DRIVER_DOB,AL.APP_EFFECTIVE_DATE)<@INEXPERINCE_DRIVERS_UPPER_AGE) or (DATEDIFF(YEAR,AUVI.DATE_LICENSED,AL.APP_EFFECTIVE_DATE)<@INEXPERINCE_DRIVERS_EXPERINCE_AGE))                    
	AND AUUP.POLICY_COMPANY=@POLICY_COMPANY

 SET @MOTORCYCLES_INEXPERIENCED_DRIVER = @MOTORCYCLES_INEXPERIENCED_DRIVER + @TOTAL_NUMBER_DRIVER
         
FETCH NEXT FROM  VEHICLE_ID_CURSOR_MOTORCYCLE INTO @VEHICLE_ID_NUMBER_MOTORCYCLE                       
           
 END     
                    
     CLOSE VEHICLE_ID_CURSOR_MOTORCYCLE                      
     DEALLOCATE VEHICLE_ID_CURSOR_MOTORCYCLE                     
                      
END                     
                    
  -- MATURE AGE DISCOUNT                    
IF( UPPER(@MATUREAGEDISCOUNFORWOLVERINEVOILATIONS)='Y' OR UPPER(@DRIVERAGEOTHERS)='Y')                    
SET @MATUREAGEDISCOUNTMOTORCYCLE = 'Y'                    
ELSE                    
SET @MATUREAGEDISCOUNTMOTORCYCLE = 'N'                    
                    
                   
                    
END                     
                     
                    
                    
                    
BEGIN                                
SELECT                      
                    
  @MOTORCYCLE_POLICY_LIMIT_LOWER_LIMIT      AS MOTORCYCLEPOLICYLOWERLIMIT,                               
  @MOTORCYCLE_POLICY_LIMIT_UPPER_LIMIT AS MOTORCYCLEPOLICYUPPERLIMIT,                      
  @MOTORPD  as MOTORPD,                    
  @MOTORCSL as MOTORCSL,                        
  @UNDERLYING_MOTORIST_LIMIT      AS UNINSUNDERINSMOTORISTLIMIT,                              
                     
  @MOTORCYCLES      AS MOTORCYCLES,                                      
  @MOTORCYCLES_INEXPERIENCED_DRIVER      AS INEXPDRIVERSMOTORCYCL,                                
  @TOTALNUMER_OF_MOTORCYCLE_DRIVERS AS TOTALNUMEROFMOTORCYCLEDRIVERS,                             
  @MATUREAGEDISCOUNTMOTORCYCLE  AS  MATUREAGEDISCOUNTMOTORCYCLE                    
                     
END                     
                    
              
            
          
        
      
    
  











GO

