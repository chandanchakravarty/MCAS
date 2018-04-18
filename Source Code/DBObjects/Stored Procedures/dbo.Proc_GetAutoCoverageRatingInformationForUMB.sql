IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAutoCoverageRatingInformationForUMB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAutoCoverageRatingInformationForUMB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                              
Proc Name           : Proc_GetAutoCoverageRatingInformationForUMB                                                                    
Created by          : Neeraj singh                                                                             
Date                : 10-01-2006                                                                              
Purpose             : To get the information for creating the input xml                                                                               
Revison History     :                                                                              
Used In             : Wolverine                                                                              
------------------------------------------------------------                                                                              
Date     Review By          Comments                                                                              
------   ------------       -------------------------*/                        
                           
-- DROP PROC dbo.Proc_GetAutoCoverageRatingInformationForUMB                                        
CREATE     PROC dbo.Proc_GetAutoCoverageRatingInformationForUMB            
 (              
  @CUSTOMER_ID       	INT,                                                                              
  @ID           	INT,                                                                              
  @VERSION_ID      	INT,              
  @DATA_ACCESS_POINT 	INT,              
  @UMBRELLA_POLICY_ID 	nvarchar(20),
  @POLICY_COMPANY	NVARCHAR(300)                   
  )                                        
              
AS                                                                       
BEGIN                                                                              
                      
SET QUOTED_IDENTIFIER OFF               
                   
DECLARE @PERSONALAUTOPOLICYLOWERLIMIT  nvarchar(100)                         
DECLARE @PERSONALAUTOPOLICYUPPERLIMIT  nvarchar(100)                               
DECLARE @AUTOMOBILE         INT                                
DECLARE @AUTOMOBILE_INEXPERIENCED_DRIVER INT                                
DECLARE @AUTOMOBILE_DRIVER   INT              
DECLARE @MOTORHOME_DRIVER   INT              
DECLARE @STATE_ID    SMALLINT              
DECLARE @AUTOPD    VARCHAR(20)                  
DECLARE @AUTOCSL    VARCHAR(20)              
DECLARE @MOTORHOMES         INT                
DECLARE @MOTORHOMES_INEXPERIENCED_DRIVER INT               
DECLARE @TOTALNUMER_OF_DRIVERSOTHERS INT              
DECLARE @DRIVERAGEOTHERS NVARCHAR(20)              
DECLARE @MATUREAGEDISCOUNFORWOLVERINEVOILATIONS    VARCHAR(20)              
DECLARE @TOTALNUMER_OF_AUTO_DRIVERS INT              
DECLARE @MATUREAGEDISCOUNTAUTO    VARCHAR(20)             
DECLARE @VEHICLE_ID INT              
----------------------------------------------------------      
DECLARE @UNINSUREDMOTORISTCSL NVARCHAR(20)                            
DECLARE @UNINSUREDMOOTRISTBIPLIT NVARCHAR(20)        
DECLARE @UNINSUREDMOOTRISTBIPLITLOWER NVARCHAR(20)        
DECLARE @UNINSUREDMOOTRISTBIPLITUPPER NVARCHAR(20)        
DECLARE @UNINSUREDMOTORIST_PD_INDIANA_LIMIT NVARCHAR(20)                              
DECLARE @UNDERINSUREDMOTORISTCSL NVARCHAR(20)                            
DECLARE @UNDERINSUREDMOTORISTBISPLIT NVARCHAR(20)      
DECLARE @UNINSUREDMOTORISTCSLREJECT NVARCHAR(20)                   
DECLARE @UNINSUREDMOOTRISTBIPLITREJECT NVARCHAR(20)                   
DECLARE @UNDERINSUREDMOTORISTCSLREJECT NVARCHAR(20)                   
DECLARE @UNDERINSUREDMOTORISTBISPLITREJECT NVARCHAR(20)       
DECLARE @EXCLUDE_UNINSURED_MOTORIST  NVARCHAR(20)    
DECLARE @EXCLUDE_UNINSURED_MOTORIST_OTHERS INT                     
---------------------------------------------------------      
--SET @PERSONALAUTOPOLICYLOWERLIMIT ='0'          
--SET @PERSONALAUTOPOLICYUPPERLIMIT ='0'              
--SET @AUTOMOBILE =0  
SET @AUTOMOBILE_INEXPERIENCED_DRIVER =0                
SET @AUTOMOBILE_DRIVER =0           
SET @MOTORHOME_DRIVER =0              
SET @AUTOPD ='0'              
SET @AUTOCSL ='0'              
SET @MOTORHOMES =0              
SET @MOTORHOMES_INEXPERIENCED_DRIVER =0              
SET @TOTALNUMER_OF_DRIVERSOTHERS =0              
SET @TOTALNUMER_OF_AUTO_DRIVERS =0              
SET @MOTORHOMES =0              
SET @VEHICLE_ID=1            
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
DECLARE @AUTO_BI_SPLIT_MICIGAN int              
DECLARE @AUTO_BI_SPLIT__INDIANA int              
DECLARE @AUTO_PD_MICIGAN int              
DECLARE @AUTO_PD_INDIANA int              
DECLARE @AUTO_CSL_INDIANA int              
DECLARE @AUTO_CSL_MICIGAN int              
DECLARE @BISPLIT_INDIANA_COV_CODE NVARCHAR(20)              
DECLARE @BISPLIT_MICIGAN_COV_CODE NVARCHAR(20)              
DECLARE @PD_INDIANA_COV_CODE NVARCHAR(20)              
DECLARE @PD_MICIGAN_COV_CODE NVARCHAR(20)              
DECLARE @MOTORHOME_LOOKUP int              
DECLARE @INEXPERINCE_DRIVERS_UPPER_AGE int              
DECLARE @INEXPERINCE_DRIVERS_EXPERINCE_AGE int              
DECLARE @SUSPENDED_COM_LOOKUP_VALUE int                
DECLARE @NOTRATEDDRIVER int        
 -----------------------------------------------------------     
DECLARE @AUTO_CSL_INDIANA_OTHERS NVARCHAR(20)     
DECLARE @AUTO_CSL_MICHIGAN_OTHERS NVARCHAR(20)     
DECLARE @UNINSUREDMOTORIST_CSL_INDIANA int                  
DECLARE @UNINSUREDMOTORIST_BISPLIT_INDIANA int       
DECLARE @UNINSUREDMOTORIST_PD_INDIANA int                 
DECLARE @UNDERINSUREDMOTORIST_BISPLIT_INDIANA int                  
DECLARE @UNDERINSUREDMOTORIST_CSL_INDIANA int        
DECLARE @UNINSUREDMOTORIST_BISPLIT_INDIANA_COV_CODE NVARCHAR(20)                  
DECLARE @UNINSUREDMOTORIST_CSL_INDIANA_COV_CODE NVARCHAR(20)                  
DECLARE @UNDERINSUREDMOTORIST_CSL_INDIANA_COV_CODE NVARCHAR(20)                  
DECLARE @UNDERINSUREDMOTORIST_BISPLIT_INDIANA_COV_CODE NVARCHAR(20)          
DECLARE @UNDERINSUREDMOTORIST_BISPLIT_INDIANA_REJECT NVARCHAR(20)                  
DECLARE @UNDERINSUREDMOTORIST_CSL_INDIANA_REJECT NVARCHAR(20)                  
DECLARE @UNINSUREDMOTORIST_BISPLIT_INDIANA_REJECT NVARCHAR(20)                  
DECLARE @UNINSUREDMOTORIST_CSL_INDIANA_REJECT NVARCHAR(20)                     
------------------------------------------------------------             
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
SET @AUTO_BI_SPLIT_MICIGAN =114              
SET @AUTO_BI_SPLIT__INDIANA =2              
SET @AUTO_PD_MICIGAN =115              
SET @AUTO_PD_INDIANA =4              
SET @AUTO_CSL_MICIGAN =113              
SET @AUTO_CSL_INDIANA =1              
SET @BISPLIT_MICIGAN_COV_CODE ='BISPL'              
SET @BISPLIT_INDIANA_COV_CODE ='BISPL'    
SET @PD_MICIGAN_COV_CODE ='PD'              
SET @PD_INDIANA_COV_CODE ='PD' 
SET @MOTORHOME_LOOKUP =11958               
SET @INEXPERINCE_DRIVERS_UPPER_AGE=25              
SET @INEXPERINCE_DRIVERS_EXPERINCE_AGE=3              
set @SUSPENDED_COM_LOOKUP_VALUE =11618         
SET @NOTRATEDDRIVER =11931       
SET @UNINSUREDMOTORIST_PD_INDIANA = 36     
SET @AUTO_CSL_INDIANA_OTHERS='SLL'     
SET @AUTO_CSL_MICHIGAN_OTHERS='SLL'     
------------------------------------------------      
SET @UNINSUREDMOTORIST_CSL_INDIANA_REJECT ='REJECT'                  
SET @UNINSUREDMOTORIST_BISPLIT_INDIANA_REJECT ='REJECT'                  
SET @UNDERINSUREDMOTORIST_CSL_INDIANA_REJECT ='REJECT'                  
SET @UNDERINSUREDMOTORIST_BISPLIT_INDIANA_REJECT ='REJECT'             
SET @UNINSUREDMOTORIST_CSL_INDIANA_COV_CODE='PUNCS'                   
SET @UNINSUREDMOTORIST_BISPLIT_INDIANA_COV_CODE='PUMSP'                   
SET @UNDERINSUREDMOTORIST_CSL_INDIANA_COV_CODE='UNCSL'                  
SET @UNDERINSUREDMOTORIST_BISPLIT_INDIANA_COV_CODE='UNDSP'           
SET @UNINSUREDMOTORIST_CSL_INDIANA = 9                  
SET @UNINSUREDMOTORIST_BISPLIT_INDIANA = 12                  
SET @UNDERINSUREDMOTORIST_BISPLIT_INDIANA = 34                  
SET @UNDERINSUREDMOTORIST_CSL_INDIANA = 14                  
------------------------------------------------            
IF ( @DATA_ACCESS_POINT = @POLICY)              
BEGIN              
              
 SELECT @STATE_ID=STATE_ID -- Check for state id               
 FROM POL_CUSTOMER_POLICY_LIST                          
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@ID AND POLICY_VERSION_ID=@VERSION_ID            
            
-- FETCH TOP VEHICLE FROM POL_VEHICLE            
 DECLARE VEHICLE_ID_CURSOR CURSOR FOR                
SELECT             
  TOP 1 POL_VEHICLES.VEHICLE_ID             
  FROM POL_VEHICLES INNER JOIN POL_DRIVER_ASSIGNED_VEHICLE ON POL_VEHICLES.CUSTOMER_ID=POL_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID AND POL_VEHICLES.POLICY_ID=POL_DRIVER_ASSIGNED_VEHICLE.POLICY_ID AND POL_VEHICLES.POLICY_VERSION_ID=POL_DRIVER_ASSIGNED_VEHICLE




  
.    
      
        
          
POLICY_VERSION_ID            
 WHERE POL_VEHICLES.CUSTOMER_ID=@CUSTOMER_ID AND POL_VEHICLES.POLICY_ID=@ID AND POL_VEHICLES.POLICY_VERSION_ID=@VERSION_ID AND POL_VEHICLES.IS_ACTIVE='Y' AND  POL_VEHICLES.APP_VEHICLE_PERTYPE_ID != @SUSPENDED_COM_LOOKUP_VALUE              
 OPEN VEHICLE_ID_CURSOR                 
     FETCH NEXT FROM  VEHICLE_ID_CURSOR INTO @VEHICLE_ID                 
            
     CLOSE VEHICLE_ID_CURSOR                
     DEALLOCATE VEHICLE_ID_CURSOR             
            
            
 IF (@STATE_ID = @STATE_ID_MICHIGAN)   -- AUTO POLICY FOR MICIGAN                  
  BEGIN               
           SELECT                -- BI SPLIT LOWER LIMIT AND BI SPLIT UPPER LIMIT              
           @PERSONALAUTOPOLICYLOWERLIMIT = convert(varchar(20),ISNULL(LIMIT_1,0)) ,               
        @PERSONALAUTOPOLICYUPPERLIMIT = convert(varchar(20),ISNULL(LIMIT_2,0))                           
           FROM POL_VEHICLE_coverages   WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID =@AUTO_BI_SPLIT_MICIGAN ) and VEHICLE_ID = @VEHICLE_ID              
                      
                           
     SELECT           -- PD LIMIT               
           @AUTOPD = convert(varchar(20),ISNULL(LIMIT_1,0))                           
           FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@AUTO_PD_MICIGAN) and VEHICLE_ID = @VEHICLE_ID                    
                   
      SELECT             -- CSL LIMIT              
           @AUTOCSL = convert(varchar(20),ISNULL(LIMIT_1,0))       --CSL                    
           FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@AUTO_CSL_MICIGAN) and VEHICLE_ID = @VEHICLE_ID                    
                   
                    
  END              
 IF (@STATE_ID = @STATE_ID_INDIANA)   -- AUTO POLICY FOR INDIANA                
  BEGIN              
    SELECT          --BI SPLIT FOR WOLVERINE CUSTOMER IN INDIANA                
           @PERSONALAUTOPOLICYLOWERLIMIT = convert(varchar(20),ISNULL(LIMIT_1,0)), --Lower Limit                  
           @PERSONALAUTOPOLICYUPPERLIMIT = convert(varchar(20),ISNULL(LIMIT_2,0))  -- Upper Limit                       
           FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@AUTO_BI_SPLIT__INDIANA)  and VEHICLE_ID = @VEHICLE_ID                   
                        
           SELECT          --PD LIMIT FOR CUSTOMER IN INDIANA              
           @AUTOPD = convert(varchar(20),ISNULL(LIMIT_1,0))                           
           FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@AUTO_PD_INDIANA) and VEHICLE_ID = @VEHICLE_ID                    
     SELECT           --CSL  LIMIT FOR CUSTOMER IN INDIANA             
           @AUTOCSL = convert(varchar(20),ISNULL(LIMIT_1,0))                           
           FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@AUTO_CSL_INDIANA)  and VEHICLE_ID = @VEHICLE_ID                   
        
----------------------------------------------------------------------------------------------      
 SELECT                             
           @UNINSUREDMOTORISTCSL= convert(varchar(20),ISNULL(LIMIT_1,0))            -- CHECK FOR UNINSUREDMOTORISTCSL LIMIT FOR INDIANA WOLVERINE CUSTOMER                  
           FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND COVERAGE_CODE_ID=@UNINSUREDMOTORIST_CSL_INDIANA  and VEHICLE_ID = @VEHICLE_ID                           
        SELECT                             
           @UNINSUREDMOOTRISTBIPLITLOWER= convert(varchar(20),ISNULL(LIMIT_1,0))            -- CHECK FOR UNINSUREDMOTORISTBISPLIT LIMIT FOR INDIANA WOLVERINE CUSTOMER                  
           FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND COVERAGE_CODE_ID=@UNINSUREDMOTORIST_BISPLIT_INDIANA  and VEHICLE_ID = @VEHICLE_ID                           
        SELECT                             
           @UNINSUREDMOOTRISTBIPLITUPPER= convert(varchar(20),ISNULL(LIMIT_2,0))             -- CHECK FOR UNINSUREDMOTORISTBISPLIT LIMIT FOR INDIANA WOLVERINE CUSTOMER                  
           FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND COVERAGE_CODE_ID=@UNINSUREDMOTORIST_BISPLIT_INDIANA  and VEHICLE_ID = @VEHICLE_ID                           
               
 SELECT @UNDERINSUREDMOTORISTCSL= convert(varchar(20),ISNULL(LIMIT_1,0))    -- -- CHECK FOR UNINSUREDMOTORISTCSL LIMIT FOR INDIANA WOLVERINE CUSTOMER                  
           FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID and COVERAGE_CODE_ID=@UNDERINSUREDMOTORIST_CSL_INDIANA  and VEHICLE_ID = @VEHICLE_ID                             
        SELECT @UNDERINSUREDMOTORISTBISPLIT= convert(varchar(20),ISNULL(LIMIT_2,0))                               
           FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID and COVERAGE_CODE_ID=@UNDERINSUREDMOTORIST_BISPLIT_INDIANA   and VEHICLE_ID = @VEHICLE_ID                            
                
 select @UNINSUREDMOTORIST_PD_INDIANA_LIMIT = convert(varchar(20),isnull(LIMIT_1,0))      
    from POL_VEHICLE_COVERAGES where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID and COVERAGE_CODE_ID=@UNINSUREDMOTORIST_PD_INDIANA   and VEHICLE_ID = @VEHICLE_ID                                         
          SET @UNINSUREDMOOTRISTBIPLIT = @UNINSUREDMOOTRISTBIPLITLOWER + '/' + @UNINSUREDMOOTRISTBIPLITUPPER + '/'+ @UNINSUREDMOTORIST_PD_INDIANA_LIMIT                       
           
 SELECT                       
           @UNINSUREDMOTORISTCSLREJECT= LTRIM(RTRIM(LIMIT1_AMOUNT_TEXT))            -- CHECK FOR UNINSUREDMOTORISTCSL LIMIT FOR INDIANA WOLVERINE CUSTOMER                  
           FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND COVERAGE_CODE_ID=@UNINSUREDMOTORIST_CSL_INDIANA  and VEHICLE_ID = @VEHICLE_ID                           
           SELECT                             
           @UNINSUREDMOOTRISTBIPLITREJECT= LTRIM(RTRIM(LIMIT1_AMOUNT_TEXT))           -- CHECK FOR UNINSUREDMOTORISTBISPLIT LIMIT FOR INDIANA WOLVERINE CUSTOMER                  
           FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND COVERAGE_CODE_ID=@UNINSUREDMOTORIST_BISPLIT_INDIANA  and VEHICLE_ID = @VEHICLE_ID                           
           SELECT @UNDERINSUREDMOTORISTCSLREJECT= LTRIM(RTRIM(LIMIT1_AMOUNT_TEXT))    -- -- CHECK FOR UNINSUREDMOTORISTCSL LIMIT FOR INDIANA WOLVERINE CUSTOMER                  
           FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID and COVERAGE_CODE_ID=@UNDERINSUREDMOTORIST_CSL_INDIANA  and VEHICLE_ID = @VEHICLE_ID                             
           SELECT @UNDERINSUREDMOTORISTBISPLITREJECT= LTRIM(RTRIM(LIMIT2_AMOUNT_TEXT))                               
           FROM POL_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID and COVERAGE_CODE_ID=@UNDERINSUREDMOTORIST_BISPLIT_INDIANA   and VEHICLE_ID = @VEHICLE_ID                            
         
------------------------------------------------------------------------------------------------      
      
      
      
      
      
END                
                  
              
  -- CHECK FOR MATURE AGE DISCOUNT IN CASE OF DRIVER IS WITH WOLVERINE(VIOLATIONS)              
  IF EXISTS ( SELECT  *               
    FROM  POL_DRIVER_DETAILS WITH (NOLOCK)               
    INNER JOIN POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID             
  AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID              
           WHERE POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=@CUSTOMER_ID                        
            AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=@ID                     
            AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=@VERSION_ID              
     AND ISNULL(VIOLATIONS,@LOOKUPVALUE_YES) = @LOOKUPVALUE_NO                
     AND ((( DATEDIFF(YEAR,POL_DRIVER_DETAILS.DRIVER_DOB,POL_CUSTOMER_POLICY_LIST.APP_EFFECTIVE_DATE) > @MATURE_AGE_LL ) AND DATEDIFF(YEAR,POL_DRIVER_DETAILS.DRIVER_DOB,POL_CUSTOMER_POLICY_LIST.APP_EFFECTIVE_DATE) < @MATURE_AGE_UL )))              
    SET @MATUREAGEDISCOUNFORWOLVERINEVOILATIONS = 'Y'              
  ELSE              
     SET @MATUREAGEDISCOUNFORWOLVERINEVOILATIONS = 'N'              
              
              
              
              
                 
    -- NUMBER OF AUTOMOBILES (PERSONAL EXCLUDING MOTORHOME AND COMMERCIAL-PERSONAL UMBRELLA DOESNT CONSIDER THESE 2 IN AUTO)              
              SELECT @AUTOMOBILE = COUNT(VEHICLE_ID)                        
                         FROM POL_VEHICLES  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@ID  AND POLICY_VERSION_ID=@VERSION_ID                         
             AND  (APP_VEHICLE_PERTYPE_ID IS NOT NULL) AND (APP_VEHICLE_PERTYPE_ID != 11336 )              
                  
                          
        ------------------------------------------------------------              
    -- AUTOMOBILE INEXPERIENCED DRIVER                        
        -- NO OF driverS in the household under age @INEXPERINCE_DRIVERS_UPPER_AGE AND               
     -- NO OF DIRVERS with less than three (@INEXPERINCE_DRIVERS_EXPERINCE_AGE) years driving experience.                      
        -------------------------------------------------------------              
              
                 
    SELECT -- CHECK INEXPERINCED DRIVER AUTO (PERSONAL AUTO EXCLUDING MOTORHOME AND COMMERCIAL-PERSONAL UMBRELLA DOESNT CONSIDER THESE 2 IN AUTO)              
    @AUTOMOBILE_INEXPERIENCED_DRIVER= count(distinct(POL_DRIVER_DETAILS.DRIVER_ID))               
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
                  
     INNER JOIN POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND             
POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID              
     WHERE POL_CUSTOMER_POLICY_LIST.customer_id=@CUSTOMER_ID              
     and POL_CUSTOMER_POLICY_LIST.policy_id=@ID              
     and POL_CUSTOMER_POLICY_LIST.policy_version_id=@VERSION_ID              
     and POL_DRIVER_ASSIGNED_VEHICLE.APP_VEHICLE_PRIN_OCC_ID != @NOTRATEDDRIVER              
     GROUP BY DRIVER_DOB,APP_EFFECTIVE_DATE,DATE_LICENSED ,POL_VEHICLES.APP_VEHICLE_PERTYPE_ID,POL_VEHICLES.APP_VEHICLE_COMCLASS_ID              
     HAVING (( DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) <@INEXPERINCE_DRIVERS_UPPER_AGE) OR ( DATEDIFF(YEAR,DATE_LICENSED,APP_EFFECTIVE_DATE) <@INEXPERINCE_DRIVERS_EXPERINCE_AGE))              
      AND POL_VEHICLES.APP_VEHICLE_PERTYPE_ID != 11336 and               
       isnull(POL_VEHICLES.APP_VEHICLE_COMCLASS_ID  ,0)=0              
              
                  
              
    /*SELECT -- CHECK INEXPERINCED DRIVER AUTO (PERSONAL AUTO EXCLUDING MOTORHOME AND COMMERCIAL-PERSONAL UMBRELLA DOESNT CONSIDER THESE 2 IN AUTO)              
    @AUTOMOBILE_DRIVER= count(POL_DRIVER_DETAILS.DRIVER_ID)               
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
                  
     INNER JOIN POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID             
  AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID              
     WHERE POL_CUSTOMER_POLICY_LIST.customer_id=@CUSTOMER_ID              
     and POL_CUSTOMER_POLICY_LIST.policy_id=@ID              
     and POL_CUSTOMER_POLICY_LIST.policy_version_id=@VERSION_ID              
                   
     GROUP BY DRIVER_DOB,APP_EFFECTIVE_DATE,DATE_LICENSED ,POL_VEHICLES.APP_VEHICLE_PERTYPE_ID,POL_VEHICLES.APP_VEHICLE_COMCLASS_ID              
     HAVING  POL_VEHICLES.APP_VEHICLE_PERTYPE_ID != 11336 and               
     isnull(POL_VEHICLES.APP_VEHICLE_COMCLASS_ID  ,0)=0              
        */          
     SELECT  @AUTOMOBILE_DRIVER = count(DRIVER_ID) from POL_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@ID and POLICY_VERSION_ID=@VERSION_ID              
              -- NUMBER OF MOTOR HOMES               
                 
                       
          SELECT      
          @MOTORHOMES = ISNULL(COUNT(VEHICLE_ID),0)                        
          FROM POL_VEHICLES                         
          WHERE CUSTOMER_ID=@CUSTOMER_ID                        
          AND POLICY_ID=@ID                        
          AND POLICY_VERSION_ID=@VERSION_ID                         
          AND APP_VEHICLE_PERTYPE_ID=11336                         
                               
              
          -- MOTOR HOME INEXPERIENCED DRIVER                        
                      
           SELECT -- CHECK INEXPERINCED DRIVER AUTO (PERSONAL AUTO EXCLUDING MOTORHOME AND COMMERCIAL-PERSONAL UMBRELLA DOESNT CONSIDER THESE 2 IN AUTO)              
     @MOTORHOMES_INEXPERIENCED_DRIVER= count(distinct(POL_DRIVER_DETAILS.DRIVER_ID))               
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
                  
     INNER JOIN POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID             
AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID              
     WHERE POL_CUSTOMER_POLICY_LIST.customer_id=@CUSTOMER_ID              
     and POL_CUSTOMER_POLICY_LIST.policy_id=@ID              
     and POL_CUSTOMER_POLICY_LIST.policy_version_id=@VERSION_ID              
     and POL_DRIVER_ASSIGNED_VEHICLE.APP_VEHICLE_PRIN_OCC_ID != @NOTRATEDDRIVER              
     GROUP BY DRIVER_DOB,APP_EFFECTIVE_DATE,DATE_LICENSED ,POL_VEHICLES.APP_VEHICLE_PERTYPE_ID,POL_VEHICLES.APP_VEHICLE_COMCLASS_ID              
     HAVING (( DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) <@INEXPERINCE_DRIVERS_UPPER_AGE) OR ( DATEDIFF(YEAR,DATE_LICENSED,APP_EFFECTIVE_DATE) <@INEXPERINCE_DRIVERS_EXPERINCE_AGE))              
      AND POL_VEHICLES.APP_VEHICLE_PERTYPE_ID = 11336 and               
      isnull(POL_VEHICLES.APP_VEHICLE_COMCLASS_ID  ,0)=0              
              
                  
              
    SELECT -- CHECK INEXPERINCED DRIVER AUTO (PERSONAL AUTO EXCLUDING MOTORHOME AND COMMERCIAL-PERSONAL UMBRELLA DOESNT CONSIDER THESE 2 IN AUTO)              
    @MOTORHOME_DRIVER= count(POL_DRIVER_DETAILS.DRIVER_ID)               
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
                  
     INNER JOIN POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND             
POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_DRIVER_DETAILS.POLICY_VERSION_ID              
     WHERE POL_CUSTOMER_POLICY_LIST.customer_id=@CUSTOMER_ID              
     and POL_CUSTOMER_POLICY_LIST.policy_id=@ID              
     and POL_CUSTOMER_POLICY_LIST.policy_version_id=@VERSION_ID              
                   
     GROUP BY DRIVER_DOB,APP_EFFECTIVE_DATE,DATE_LICENSED ,POL_VEHICLES.APP_VEHICLE_PERTYPE_ID,POL_VEHICLES.APP_VEHICLE_COMCLASS_ID              
     HAVING   POL_VEHICLES.APP_VEHICLE_PERTYPE_ID = 11336 and               
       isnull(POL_VEHICLES.APP_VEHICLE_COMCLASS_ID  ,0)=0              
              
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
 WHERE APP_VEHICLES.CUSTOMER_ID=@CUSTOMER_ID AND APP_VEHICLES.APP_ID=@ID AND APP_VEHICLES.APP_VERSION_ID=@VERSION_ID AND APP_VEHICLES.IS_ACTIVE='Y' AND  APP_VEHICLES.VEHICLE_TYPE_PER != @SUSPENDED_COM_LOOKUP_VALUE              
 OPEN VEHICLE_ID_CURSOR                 
     FETCH NEXT FROM  VEHICLE_ID_CURSOR INTO @VEHICLE_ID                 
            
     CLOSE VEHICLE_ID_CURSOR                
     DEALLOCATE VEHICLE_ID_CURSOR             
            
             
            
 IF (@STATE_ID = @STATE_ID_MICHIGAN)   -- AUTO POLICY FOR MICIGAN                  
  BEGIN               
           SELECT                -- BI SPLIT LOWER LIMIT AND BI SPLIT UPPER LIMIT              
           @PERSONALAUTOPOLICYLOWERLIMIT = convert(varchar(20),ISNULL(LIMIT_1,0)) ,               
    @PERSONALAUTOPOLICYUPPERLIMIT = convert(varchar(20),ISNULL(LIMIT_2,0))                    
           FROM APP_VEHICLE_coverages   WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID =@AUTO_BI_SPLIT_MICIGAN ) and VEHICLE_ID = @VEHICLE_ID              
                      
                           
     SELECT           -- PD LIMIT               
           @AUTOPD = convert(varchar(20),ISNULL(LIMIT_1,0))                           
           FROM APP_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@AUTO_PD_MICIGAN) and VEHICLE_ID = @VEHICLE_ID                    
                   
      SELECT             -- CSL LIMIT              
           @AUTOCSL = convert(varchar(20),ISNULL(LIMIT_1,0))       --CSL                    
           FROM APP_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@AUTO_CSL_MICIGAN) and VEHICLE_ID = @VEHICLE_ID                    
                   
                    
  END              
 IF (@STATE_ID = @STATE_ID_INDIANA)   -- AUTO POLICY FOR INDIANA                
  BEGIN              
    SELECT          --BI SPLIT FOR WOLVERINE CUSTOMER IN INDIANA                
           @PERSONALAUTOPOLICYLOWERLIMIT = convert(varchar(20),ISNULL(LIMIT_1,0)), --Lower Limit                  
           @PERSONALAUTOPOLICYUPPERLIMIT = convert(varchar(20),ISNULL(LIMIT_2,0))  -- Upper Limit                       
           FROM APP_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@AUTO_BI_SPLIT__INDIANA)  and VEHICLE_ID = @VEHICLE_ID                   
                        
           SELECT          --PD LIMIT FOR CUSTOMER IN INDIANA              
           @AUTOPD = convert(varchar(20),ISNULL(LIMIT_1,0))                           
           FROM APP_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@AUTO_PD_INDIANA) and VEHICLE_ID = @VEHICLE_ID                    
     SELECT           --CSL  LIMIT FOR CUSTOMER IN INDIANA              
           @AUTOCSL = convert(varchar(20),ISNULL(LIMIT_1,0))                           
           FROM APP_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND (COVERAGE_CODE_ID=@AUTO_CSL_INDIANA)  and VEHICLE_ID = @VEHICLE_ID                   
       
---------------------------------------------------------------------------------------------------      
 SELECT                             
           @UNINSUREDMOTORISTCSL= convert(varchar(20),ISNULL(LIMIT_1,0))            -- CHECK FOR UNINSUREDMOTORISTCSL LIMIT FOR INDIANA WOLVERINE CUSTOMER                  
           FROM APP_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND COVERAGE_CODE_ID=@UNINSUREDMOTORIST_CSL_INDIANA  and VEHICLE_ID = @VEHICLE_ID                   
           SELECT                             
           @UNINSUREDMOOTRISTBIPLITLOWER= convert(varchar(20),ISNULL(LIMIT_1,0))           -- CHECK FOR UNINSUREDMOTORISTBISPLIT LIMIT FOR INDIANA WOLVERINE CUSTOMER                  
           FROM APP_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND COVERAGE_CODE_ID=@UNINSUREDMOTORIST_BISPLIT_INDIANA  and VEHICLE_ID = @VEHICLE_ID                           
           SELECT                             
           @UNINSUREDMOOTRISTBIPLITUPPER= convert(varchar(20),ISNULL(LIMIT_2,0))           -- CHECK FOR UNINSUREDMOTORISTBISPLIT LIMIT FOR INDIANA WOLVERINE CUSTOMER                  
           FROM APP_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND COVERAGE_CODE_ID=@UNINSUREDMOTORIST_BISPLIT_INDIANA  and VEHICLE_ID = @VEHICLE_ID                           
 SELECT @UNDERINSUREDMOTORISTCSL= convert(varchar(20),ISNULL(LIMIT_1,0),101)   -- -- CHECK FOR UNINSUREDMOTORISTCSL LIMIT FOR INDIANA WOLVERINE CUSTOMER                  
           FROM APP_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID and COVERAGE_CODE_ID=@UNDERINSUREDMOTORIST_CSL_INDIANA  and VEHICLE_ID = @VEHICLE_ID                             
           SELECT @UNDERINSUREDMOTORISTBISPLIT= convert(varchar(20),ISNULL(LIMIT_2,0),101)                               
           FROM APP_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID and COVERAGE_CODE_ID=@UNDERINSUREDMOTORIST_BISPLIT_INDIANA   and VEHICLE_ID = @VEHICLE_ID                            
           select @UNINSUREDMOTORIST_PD_INDIANA_LIMIT = convert(varchar(20),isnull(LIMIT_1,0))      
    from APP_VEHICLE_coverages where CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID and COVERAGE_CODE_ID=@UNINSUREDMOTORIST_PD_INDIANA   and VEHICLE_ID = @VEHICLE_ID                                         
            
        
    SET @UNINSUREDMOOTRISTBIPLIT = @UNINSUREDMOOTRISTBIPLITLOWER + '/' + @UNINSUREDMOOTRISTBIPLITUPPER + '/'+ @UNINSUREDMOTORIST_PD_INDIANA_LIMIT                   
                      
    SELECT                             
           @UNINSUREDMOTORISTCSLREJECT= LTRIM(RTRIM(LIMIT1_AMOUNT_TEXT))            -- CHECK FOR UNINSUREDMOTORISTCSL LIMIT FOR INDIANA WOLVERINE CUSTOMER                  
           FROM APP_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND COVERAGE_CODE_ID=@UNINSUREDMOTORIST_CSL_INDIANA  and VEHICLE_ID = @VEHICLE_ID                           
           SELECT                             
           @UNINSUREDMOOTRISTBIPLITREJECT= LTRIM(RTRIM(LIMIT1_AMOUNT_TEXT))         -- CHECK FOR UNINSUREDMOTORISTBISPLIT LIMIT FOR INDIANA WOLVERINE CUSTOMER                         
  FROM APP_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND COVERAGE_CODE_ID=@UNINSUREDMOTORIST_BISPLIT_INDIANA  and VEHICLE_ID = @VEHICLE_ID                           
           SELECT @UNDERINSUREDMOTORISTCSLREJECT= LTRIM(RTRIM(LIMIT1_AMOUNT_TEXT))    -- -- CHECK FOR UNINSUREDMOTORISTCSL LIMIT FOR INDIANA WOLVERINE CUSTOMER              
           FROM APP_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID and COVERAGE_CODE_ID=@UNDERINSUREDMOTORIST_CSL_INDIANA  and VEHICLE_ID = @VEHICLE_ID                             
           SELECT @UNDERINSUREDMOTORISTBISPLITREJECT= LTRIM(RTRIM(LIMIT2_AMOUNT_TEXT))                               
           FROM APP_VEHICLE_coverages WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID and COVERAGE_CODE_ID=@UNDERINSUREDMOTORIST_BISPLIT_INDIANA   and VEHICLE_ID = @VEHICLE_ID                         
       
---------------------------------------------------------------------------------------------------      
      
 END                
                     
    IF EXISTS (SELECT  *               
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
              
               
              
              
              
              
              
   -- NUMBER OF AUTOMOBILES (PERSONAL EXCLUDING MOTORHOME AND COMMERCIAL-PERSONAL UMBRELLA DOESNT CONSIDER THESE 2 IN AUTO)              
              SELECT @AUTOMOBILE = COUNT(VEHICLE_ID)                        
                         FROM APP_VEHICLES  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@ID  AND APP_VERSION_ID=@VERSION_ID                         
                  AND  (VEHICLE_TYPE_PER IS NOT NULL) AND (VEHICLE_TYPE_PER != 11336 )              
                  
                          
        ------------------------------------------------------------              
    -- AUTOMOBILE INEXPERIENCED DRIVER                        
        -- NO OF driverS in the household under age @INEXPERINCE_DRIVERS_UPPER_AGE AND               
     -- NO OF DIRVERS with less than three (@INEXPERINCE_DRIVERS_EXPERINCE_AGE) years driving experience.                      
        -------------------------------------------------------------              
              
                 
    SELECT -- CHECK INEXPERINCED DRIVER AUTO (PERSONAL AUTO EXCLUDING MOTORHOME AND COMMERCIAL-PERSONAL UMBRELLA DOESNT CONSIDER THESE 2 IN AUTO)              
    @AUTOMOBILE_INEXPERIENCED_DRIVER= count(DISTINCT(APP_DRIVER_DETAILS.DRIVER_ID))               
    FROM APP_DRIVER_DETAILS               
     INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE ON              
     APP_DRIVER_DETAILS.CUSTOMER_ID=APP_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID               
     AND APP_DRIVER_DETAILS.APP_ID=APP_DRIVER_ASSIGNED_VEHICLE.APP_ID                
     AND APP_DRIVER_DETAILS.APP_VERSION_ID=APP_DRIVER_ASSIGNED_VEHICLE.APP_VERSION_ID               
     AND  APP_DRIVER_DETAILS.DRIVER_ID=APP_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID              
                  
     INNER JOIN APP_VEHICLES ON               
     APP_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID=APP_VEHICLES.CUSTOMER_ID               
     AND APP_DRIVER_ASSIGNED_VEHICLE.APP_ID=APP_VEHICLES.APP_ID                
     AND APP_DRIVER_ASSIGNED_VEHICLE.APP_VERSION_ID=APP_VEHICLES.APP_VERSION_ID               
     AND  APP_DRIVER_ASSIGNED_VEHICLE.VEHICLE_ID=APP_VEHICLES.VEHICLE_ID              
                  
     INNER JOIN APP_LIST ON APP_LIST.CUSTOMER_ID=APP_DRIVER_DETAILS.CUSTOMER_ID AND APP_LIST.APP_ID=APP_DRIVER_DETAILS.APP_ID AND APP_LIST.APP_VERSION_ID=APP_DRIVER_DETAILS.APP_VERSION_ID              
     WHERE APP_LIST.customer_id=@CUSTOMER_ID              
     and APP_LIST.APP_ID=@ID              
     and APP_LIST.APP_VERSION_ID=@VERSION_ID              
     and APP_DRIVER_ASSIGNED_VEHICLE.APP_VEHICLE_PRIN_OCC_ID != @NOTRATEDDRIVER               
     GROUP BY DRIVER_DOB,APP_EFFECTIVE_DATE,DATE_LICENSED ,APP_VEHICLES.VEHICLE_TYPE_PER,APP_VEHICLES.VEHICLE_TYPE_COM              
     HAVING (( DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) <@INEXPERINCE_DRIVERS_UPPER_AGE) OR ( DATEDIFF(YEAR,DATE_LICENSED,APP_EFFECTIVE_DATE) <@INEXPERINCE_DRIVERS_EXPERINCE_AGE))              
      AND APP_VEHICLES.VEHICLE_TYPE_PER != 11336 and               
       isnull(APP_VEHICLES.VEHICLE_TYPE_COM  ,0)=0              
              
                  
              
    /*SELECT -- CHECK INEXPERINCED DRIVER AUTO (PERSONAL AUTO EXCLUDING MOTORHOME AND COMMERCIAL-PERSONAL UMBRELLA DOESNT CONSIDER THESE 2 IN AUTO)              
    @AUTOMOBILE_DRIVER= count(APP_DRIVER_DETAILS.DRIVER_ID)               
    FROM APP_DRIVER_DETAILS               
     INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE ON              
     APP_DRIVER_DETAILS.CUSTOMER_ID=APP_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID               
     AND APP_DRIVER_DETAILS.APP_ID=APP_DRIVER_ASSIGNED_VEHICLE.APP_ID                
     AND APP_DRIVER_DETAILS.APP_VERSION_ID=APP_DRIVER_ASSIGNED_VEHICLE.APP_VERSION_ID               
     AND  APP_DRIVER_DETAILS.DRIVER_ID=APP_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID              
            
     INNER JOIN APP_VEHICLES ON               
     APP_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID=APP_VEHICLES.CUSTOMER_ID               
     AND APP_DRIVER_ASSIGNED_VEHICLE.APP_ID=APP_VEHICLES.APP_ID                
     AND APP_DRIVER_ASSIGNED_VEHICLE.APP_VERSION_ID=APP_VEHICLES.APP_VERSION_ID               
     AND  APP_DRIVER_ASSIGNED_VEHICLE.VEHICLE_ID=APP_VEHICLES.VEHICLE_ID              
                  
     INNER JOIN APP_LIST ON APP_LIST.CUSTOMER_ID=APP_DRIVER_DETAILS.CUSTOMER_ID AND APP_LIST.APP_ID=APP_DRIVER_DETAILS.APP_ID AND APP_LIST.APP_VERSION_ID=APP_DRIVER_DETAILS.APP_VERSION_ID              
     WHERE APP_LIST.customer_id=@CUSTOMER_ID              
     and APP_LIST.APP_id=@ID              
     and APP_LIST.APP_version_id=@VERSION_ID              
                   
     GROUP BY DRIVER_DOB,APP_EFFECTIVE_DATE,DATE_LICENSED ,APP_VEHICLES.VEHICLE_TYPE_PER,APP_VEHICLES.VEHICLE_TYPE_COM              
     HAVING  APP_VEHICLES.VEHICLE_TYPE_PER != 11336 and               
     isnull(APP_VEHICLES.VEHICLE_TYPE_COM  ,0)=0    */          
                  
     SELECT  @AUTOMOBILE_DRIVER = count(DRIVER_ID) from APP_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_id=@ID and APP_VERSION_ID=@VERSION_ID               
           -- NUMBER OF MOTOR HOMES               
                 
                       
          SELECT                 
          @MOTORHOMES = ISNULL(COUNT(VEHICLE_ID),0)                        
          FROM APP_VEHICLES                         
          WHERE CUSTOMER_ID=@CUSTOMER_ID                        
          AND APP_ID=@ID                        
          AND APP_VERSION_ID=@VERSION_ID                         
          AND VEHICLE_TYPE_PER=11336                         
                               
              
          -- MOTOR HOME INEXPERIENCED DRIVER                        
                      
           SELECT -- CHECK INEXPERINCED DRIVER AUTO (PERSONAL AUTO EXCLUDING MOTORHOME AND COMMERCIAL-PERSONAL UMBRELLA DOESNT CONSIDER THESE 2 IN AUTO)              
     @MOTORHOMES_INEXPERIENCED_DRIVER= count(DISTINCT(APP_DRIVER_DETAILS.DRIVER_ID))               
    FROM APP_DRIVER_DETAILS               
     INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE ON       
     APP_DRIVER_DETAILS.CUSTOMER_ID=APP_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID               
     AND APP_DRIVER_DETAILS.APP_ID=APP_DRIVER_ASSIGNED_VEHICLE.APP_ID                
     AND APP_DRIVER_DETAILS.APP_VERSION_ID=APP_DRIVER_ASSIGNED_VEHICLE.APP_VERSION_ID               
     AND  APP_DRIVER_DETAILS.DRIVER_ID=APP_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID              
                  
     INNER JOIN APP_VEHICLES ON               
     APP_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID=APP_VEHICLES.CUSTOMER_ID               
     AND APP_DRIVER_ASSIGNED_VEHICLE.APP_ID=APP_VEHICLES.APP_ID                
     AND APP_DRIVER_ASSIGNED_VEHICLE.APP_VERSION_ID=APP_VEHICLES.APP_VERSION_ID               
     AND  APP_DRIVER_ASSIGNED_VEHICLE.VEHICLE_ID=APP_VEHICLES.VEHICLE_ID              
                  
     INNER JOIN APP_LIST ON APP_LIST.CUSTOMER_ID=APP_DRIVER_DETAILS.CUSTOMER_ID AND APP_LIST.APP_ID=APP_DRIVER_DETAILS.APP_ID AND APP_LIST.APP_VERSION_ID=APP_DRIVER_DETAILS.APP_VERSION_ID              
     WHERE APP_LIST.customer_id=@CUSTOMER_ID              
     and APP_LIST.APP_id=@ID              
     and APP_LIST.APP_version_id=@VERSION_ID              
     and APP_DRIVER_ASSIGNED_VEHICLE.APP_VEHICLE_PRIN_OCC_ID != @NOTRATEDDRIVER            
     GROUP BY DRIVER_DOB,APP_EFFECTIVE_DATE,DATE_LICENSED ,APP_VEHICLES.VEHICLE_TYPE_PER,APP_VEHICLES.VEHICLE_TYPE_COM              
     HAVING (( DATEDIFF(YEAR,DRIVER_DOB,APP_EFFECTIVE_DATE) <@INEXPERINCE_DRIVERS_UPPER_AGE) OR ( DATEDIFF(YEAR,DATE_LICENSED,APP_EFFECTIVE_DATE) <@INEXPERINCE_DRIVERS_EXPERINCE_AGE))              
      AND APP_VEHICLES.VEHICLE_TYPE_PER = 11336 and               
      isnull(APP_VEHICLES.VEHICLE_TYPE_COM  ,0)=0              
              
                  
              
    SELECT -- CHECK INEXPERINCED DRIVER AUTO (PERSONAL AUTO EXCLUDING MOTORHOME AND COMMERCIAL-PERSONAL UMBRELLA DOESNT CONSIDER THESE 2 IN AUTO)              
    @MOTORHOME_DRIVER= count(APP_DRIVER_DETAILS.DRIVER_ID)               
    FROM APP_DRIVER_DETAILS               
     INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE ON              
     APP_DRIVER_DETAILS.CUSTOMER_ID=APP_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID               
     AND APP_DRIVER_DETAILS.APP_ID=APP_DRIVER_ASSIGNED_VEHICLE.APP_ID                
     AND APP_DRIVER_DETAILS.APP_VERSION_ID=APP_DRIVER_ASSIGNED_VEHICLE.APP_VERSION_ID               
     AND  APP_DRIVER_DETAILS.DRIVER_ID=APP_DRIVER_ASSIGNED_VEHICLE.DRIVER_ID              
                  
     INNER JOIN APP_VEHICLES ON               
     APP_DRIVER_ASSIGNED_VEHICLE.CUSTOMER_ID=APP_VEHICLES.CUSTOMER_ID               
     AND APP_DRIVER_ASSIGNED_VEHICLE.APP_ID=APP_VEHICLES.APP_ID                
     AND APP_DRIVER_ASSIGNED_VEHICLE.APP_VERSION_ID=APP_VEHICLES.APP_VERSION_ID               
     AND  APP_DRIVER_ASSIGNED_VEHICLE.VEHICLE_ID=APP_VEHICLES.VEHICLE_ID              
                  
     INNER JOIN APP_LIST ON APP_LIST.CUSTOMER_ID=APP_DRIVER_DETAILS.CUSTOMER_ID AND APP_LIST.APP_ID=APP_DRIVER_DETAILS.APP_ID AND APP_LIST.APP_VERSION_ID=APP_DRIVER_DETAILS.APP_VERSION_ID              
     WHERE APP_LIST.customer_id=@CUSTOMER_ID              
     and APP_LIST.APP_id=@ID              
     and APP_LIST.APP_version_id=@VERSION_ID              
                   
     GROUP BY DRIVER_DOB,APP_EFFECTIVE_DATE,DATE_LICENSED ,APP_VEHICLES.VEHICLE_TYPE_PER,APP_VEHICLES.VEHICLE_TYPE_COM              
     HAVING   APP_VEHICLES.VEHICLE_TYPE_PER = 11336 and               
       isnull(APP_VEHICLES.VEHICLE_TYPE_COM  ,0)=0              
              
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
      AND AUUP.POLICY_COMPANY=@POLICY_COMPANY AND POLICY_NUMBER=@UMBRELLA_POLICY_ID) 
   SET @DRIVERAGEOTHERS='Y'              
  ELSE              
   SET @DRIVERAGEOTHERS='N'              
               
  IF (@STATE_ID = @STATE_ID_MICHIGAN)   -- auto  FOR MICIGAN              
               
   BEGIN              
    SELECT                 
         @PERSONALAUTOPOLICYLOWERLIMIT = substring((convert(nvarchar(20),COVERAGE_AMOUNT)),1,charindex('/',(convert(nvarchar(20),COVERAGE_AMOUNT)))-1),   -- CHECK FOR BI SPLIT LIMIT COVERAGE              
	 @PERSONALAUTOPOLICYUPPERLIMIT = substring((convert(nvarchar(20),COVERAGE_AMOUNT)),charindex('/',(convert(nvarchar(20),COVERAGE_AMOUNT)))+1,15)                   
         FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND POLICY_NUMBER = @UMBRELLA_POLICY_ID and COV_CODE=@BISPLIT_MICIGAN_COV_CODE AND POLICY_COMPANY=@POLICY_COMPANY  
 
             
    SELECT                 
         @AUTOPD = convert(varchar(20),COVERAGE_AMOUNT)    -- CHECK FOR PD AUTO LIMIT COVERAGE IN OTHERS CASE OF STATE MICICGAN              
    FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND POLICY_NUMBER = @UMBRELLA_POLICY_ID and COV_CODE=@PD_MICIGAN_COV_CODE AND POLICY_COMPANY=@POLICY_COMPANY             

   
 SELECT           --CSL  LIMIT FOR CUSTOMER IN INDIANA              
         @AUTOCSL = convert(varchar(20),ISNULL(COVERAGE_AMOUNT,0))                           
           FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND COV_CODE=@AUTO_CSL_MICHIGAN_OTHERS AND POLICY_COMPANY=@POLICY_COMPANY                    
          
END              
              
               
  IF (@STATE_ID = @STATE_ID_INDIANA)              
   BEGIN              
               
    SELECT                 
        @PERSONALAUTOPOLICYLOWERLIMIT = substring((convert(nvarchar(20),COVERAGE_AMOUNT)),1,charindex('/',(convert(nvarchar(20),COVERAGE_AMOUNT)))-1),  ---- CHECK FOR AUTO POLICY BISPLIT LIMIT FOR STATE INDIANA IN OTHERS CASE              
     @PERSONALAUTOPOLICYUPPERLIMIT = substring((convert(nvarchar(20),COVERAGE_AMOUNT)),charindex('/',(convert(nvarchar(20),COVERAGE_AMOUNT)))+1,15)                   
        FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND POLICY_NUMBER = @UMBRELLA_POLICY_ID and COV_CODE=@BISPLIT_INDIANA_COV_CODE AND POLICY_COMPANY=@POLICY_COMPANY    

            
    SELECT               
        @AUTOPD = convert(varchar(20),COVERAGE_AMOUNT)    -- CHECK FOR AUTO POLICY PD LIMIT FOR STATE INDIANA IN OTHERS CASE              
    FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND POLICY_NUMBER = @UMBRELLA_POLICY_ID and COV_CODE=@PD_INDIANA_COV_CODE AND POLICY_COMPANY=@POLICY_COMPANY             

   
   SELECT           --CSL  LIMIT FOR CUSTOMER IN INDIANA              
         @AUTOCSL = convert(varchar(20),ISNULL(COVERAGE_AMOUNT,0))                           
           FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND COV_CODE=@AUTO_CSL_INDIANA_OTHERS AND POLICY_COMPANY=@POLICY_COMPANY                    
          
    
---------------------------------------------------------------------------         
    SELECT                     
         @UNINSUREDMOTORISTCSL= convert(varchar(20),COVERAGE_AMOUNT)      -- CHECK FOR UNINSUREDMOTORIST CSL POLICY LIMIT FOR STATE INDIANA IN OTHERS CASE                  
         FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND POLICY_NUMBER = @UMBRELLA_POLICY_ID and COV_CODE=@UNINSUREDMOTORIST_CSL_INDIANA_COV_CODE AND POLICY_COMPANY=@POLICY_COMPANY                    
    SELECT                  
         @UNINSUREDMOOTRISTBIPLIT= convert(varchar(20),COVERAGE_AMOUNT)      -- CHECK FOR UNINSURED BISPLIT POLICY LIMIT FOR STATE INDIANA IN OTHERS CASE                  
         FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND POLICY_NUMBER = @UMBRELLA_POLICY_ID and COV_CODE=@UNINSUREDMOTORIST_BISPLIT_INDIANA_COV_CODE AND POLICY_COMPANY=

@POLICY_COMPANY                   

 
    SELECT                     
         @UNDERINSUREDMOTORISTCSL= convert(varchar(20),COVERAGE_AMOUNT)      -- CHECK FOR UNDERINSUREDMOTORIST CSL POLICY LIMIT FOR STATE INDIANA IN OTHERS CASE  
         FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND POLICY_NUMBER = @UMBRELLA_POLICY_ID and COV_CODE=@UNDERINSUREDMOTORIST_CSL_INDIANA_COV_CODE AND POLICY_COMPANY=


@POLICY_COMPANY                    


    SELECT                     
         @UNDERINSUREDMOTORISTBISPLIT= convert(varchar(20),COVERAGE_AMOUNT)     -- CHECK FOR UNDERINSUREDMOTORIST BISPLIT POLICY LIMIT FOR STATE INDIANA IN OTHERS CASE                  
         FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND POLICY_NUMBER = @UMBRELLA_POLICY_ID and COV_CODE=@UNDERINSUREDMOTORIST_BISPLIT_INDIANA_COV_CODE AND POLICY_COMPANY=@POLICY_COMPANY                 

 
     -- exclude uninsured motorist coverage    
   SELECT    
 @EXCLUDE_UNINSURED_MOTORIST_OTHERS = EXCLUDE_UNINSURED_MOTORIST     
 FROM APP_UMBRELLA_UNDERLYING_POLICIES
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID =@ID     
 AND APP_VERSION_ID = @VERSION_ID AND  POLICY_NUMBER =@UMBRELLA_POLICY_ID AND POLICY_COMPANY=@POLICY_COMPANY    
    
     
---------------------------------------------------------------------------      
END                
                 
    -- NUMBER OF DRIVERS                
/*
    
  SELECT                 
   @TOTALNUMER_OF_DRIVERSOTHERS = COUNT(Distinct(DRIVER_ID))   -- TOTAL NUMBER OF DRIVERS IN OTHERS CASE              
     FROM  APP_UMBRELLA_DRIVER_DETAILS AUDD WITH (NOLOCK) inner join  APP_UMBRELLA_VEHICLE_INFO AUVI on    
              	AUDD.CUSTOMER_ID=AUVI.CUSTOMER_ID    
 		AND AUDD.APP_ID=AUVI.APP_ID    
 		AND AUDD.APP_VERSION_ID=AUVI.APP_VERSION_ID    
 		AND AUDD.VEHICLE_ID=AUVI.VEHICLE_ID    
      WHERE AUDD.CUSTOMER_ID=@CUSTOMER_ID AND AUDD.APP_ID= @ID AND AUDD.APP_VERSION_ID= @VERSION_ID AND AUVI.OTHER_POLICY=@UMBRELLA_POLICY_ID    
    
    */
    
    -- NUMBER OF AUTOMOBILE                
      SELECT                
      @AUTOMOBILE =  COUNT(VEHICLE_TYPE_PER)  -- CHECK FOR PERSONAL AUTO IN OTHERS CASE              
      FROM  APP_UMBRELLA_VEHICLE_INFO AUVI
      INNER JOIN APP_UMBRELLA_UNDERLYING_POLICIES AUUP WITH (NOLOCK)
      ON AUVI.CUSTOMER_ID=AUUP.CUSTOMER_ID AND AUVI.APP_ID=AUUP.APP_ID AND AUVI.APP_VERSION_ID=AUUP.APP_VERSION_ID AND OTHER_POLICY=POLICY_NUMBER
      WHERE AUVI.CUSTOMER_ID = @CUSTOMER_ID AND AUVI.APP_ID = @ID AND AUVI.APP_VERSION_ID = @VERSION_ID AND (VEHICLE_TYPE_PER!='' AND VEHICLE_TYPE_PER!=@MOTORHOME_LOOKUP)   
      AND IS_EXCLUDED = 0  and OTHER_POLICY= @UMBRELLA_POLICY_ID AND AUUP.POLICY_COMPANY=@POLICY_COMPANY 
                      
       --NUMBER OF INEXPERINCED DRIVER(AUTO)                 
                 
    DECLARE @AUTO_ID_NUMBER INT       
    DECLARE @TOTAL_INEXP_DRIVER INT       
 set @TOTAL_INEXP_DRIVER =0  
    DECLARE AUTO_ID_CURSOR CURSOR FOR                
    SELECT  VEHICLE_ID               
    FROM APP_UMBRELLA_VEHICLE_INFO AUVI      
    INNER JOIN APP_UMBRELLA_UNDERLYING_POLICIES AUUP WITH (NOLOCK)
    ON AUVI.CUSTOMER_ID=AUUP.CUSTOMER_ID AND AUVI.APP_ID=AUUP.APP_ID AND AUVI.APP_VERSION_ID=AUUP.APP_VERSION_ID AND OTHER_POLICY=POLICY_NUMBER
    WHERE AUVI.CUSTOMER_ID=@CUSTOMER_ID AND AUVI.APP_ID =@ID AND AUVI.APP_VERSION_ID = @VERSION_ID AND ( VEHICLE_TYPE_PER is not null AND   VEHICLE_TYPE_PER !=11958) and OTHER_POLICY=@UMBRELLA_POLICY_ID   AND IS_EXCLUDED = 0            
    AND AUUP.POLICY_COMPANY=@POLICY_COMPANY 
               
     OPEN AUTO_ID_CURSOR                 
     FETCH NEXT FROM  AUTO_ID_CURSOR INTO @AUTO_ID_NUMBER                 
     WHILE @@FETCH_STATUS = 0                
       BEGIN                 
          SELECT                 
       @TOTAL_INEXP_DRIVER =  COUNT(driver_id)  -- INEXPERINCE DRIVER AUTO OTHERS CASE              
        FROM APP_UMBRELLA_DRIVER_DETAILS AUVI 
	INNER JOIN APP_LIST AL 
	ON AUVI.customer_id=AL.CUSTOMER_ID AND AUVI.APP_ID=AL.APP_ID AND AUVI.APP_VERSION_ID=AL.APP_VERSION_ID  
	INNER JOIN APP_UMBRELLA_UNDERLYING_POLICIES AUUP WITH (NOLOCK)
    	ON AUVI.CUSTOMER_ID=AUUP.CUSTOMER_ID AND AUVI.APP_ID=AUUP.APP_ID AND AUVI.APP_VERSION_ID=AUUP.APP_VERSION_ID
	where AL.customer_id=@CUSTOMER_ID and AL.app_id=@ID             
	AND AL.APP_VERSION_ID=@VERSION_ID  and AUVI.VEHICLE_ID = @AUTO_ID_NUMBER                 
        AND ((DATEDIFF(YEAR,AUVI.DRIVER_DOB,AL.APP_EFFECTIVE_DATE)<@INEXPERINCE_DRIVERS_UPPER_AGE) or (DATEDIFF(YEAR,AUVI.DATE_LICENSED,AL.APP_EFFECTIVE_DATE)<@INEXPERINCE_DRIVERS_EXPERINCE_AGE))              
	AND AUUP.POLICY_COMPANY=@POLICY_COMPANY 

 set @AUTOMOBILE_INEXPERIENCED_DRIVER =@TOTAL_INEXP_DRIVER+@AUTOMOBILE_INEXPERIENCED_DRIVER  
                       
        FETCH NEXT FROM  AUTO_ID_CURSOR INTO @AUTO_ID_NUMBER                 
                 
 END  ---END WHILE                
                
     CLOSE AUTO_ID_CURSOR                
     DEALLOCATE AUTO_ID_CURSOR                
               
     -- NUMBER OF MOTORHOMES                
    SELECT                
      @MOTORHOMES =  COUNT(VEHICLE_TYPE_PER)    -- CHECK FOR MOTOR HOMES IN OTHERS CASE              
      FROM  APP_UMBRELLA_VEHICLE_INFO AUVI
	INNER JOIN APP_UMBRELLA_UNDERLYING_POLICIES AUUP WITH (NOLOCK)
    	ON AUVI.CUSTOMER_ID=AUUP.CUSTOMER_ID AND AUVI.APP_ID=AUUP.APP_ID AND AUVI.APP_VERSION_ID=AUUP.APP_VERSION_ID AND OTHER_POLICY=POLICY_NUMBER
	WHERE AUVI.CUSTOMER_ID = @CUSTOMER_ID AND AUVI.APP_ID = @ID AND AUVI.APP_VERSION_ID = @VERSION_ID AND VEHICLE_TYPE_PER =11958    
	AND IS_EXCLUDED = 0   and OTHER_POLICY = @UMBRELLA_POLICY_ID AND AUUP.POLICY_COMPANY=@POLICY_COMPANY
        
    --NUMBER OF INEXPERINCED DRIVER(motorhomes)                 
    DECLARE @VEHICLE_ID_NUMBER SMALLINT   
 DECLARE @TOTAL_NUMBERDRIVER INT                
    DECLARE VEHICLE_ID_CURSOR CURSOR FOR                
    SELECT                 
      VEHICLE_ID FROM APP_UMBRELLA_VEHICLE_INFO AUVI
	INNER JOIN APP_UMBRELLA_UNDERLYING_POLICIES AUUP WITH (NOLOCK)
    	ON AUVI.CUSTOMER_ID=AUUP.CUSTOMER_ID AND AUVI.APP_ID=AUUP.APP_ID AND AUVI.APP_VERSION_ID=AUUP.APP_VERSION_ID AND OTHER_POLICY=POLICY_NUMBER
	WHERE AUVI.CUSTOMER_ID=@CUSTOMER_ID AND AUVI.APP_ID =@ID AND AUVI.APP_VERSION_ID = @VERSION_ID AND VEHICLE_TYPE_PER =11958   AND IS_EXCLUDED = 0               
	AND AUUP.POLICY_COMPANY=@POLICY_COMPANY
                   
     OPEN VEHICLE_ID_CURSOR                 
    FETCH NEXT FROM  VEHICLE_ID_CURSOR INTO @VEHICLE_ID_NUMBER                 
    WHILE @@FETCH_STATUS = 0                
       BEGIN                 
             SELECT                 
       @TOTAL_NUMBERDRIVER =  COUNT(driver_id)   -- CHECK FOR INEXPERINCE DRIVER MOTORHOMES OTHERS CASE              
        FROM APP_UMBRELLA_DRIVER_DETAILS AUVI 
	INNER JOIN APP_LIST AL ON AUVI.customer_id=AL.CUSTOMER_ID AND AUVI.APP_ID=AL.APP_ID AND AUVI.APP_VERSION_ID=AL.APP_VERSION_ID  
	INNER JOIN APP_UMBRELLA_UNDERLYING_POLICIES AUUP WITH (NOLOCK)
    	ON AUVI.CUSTOMER_ID=AUUP.CUSTOMER_ID AND AUVI.APP_ID=AUUP.APP_ID AND AUVI.APP_VERSION_ID=AUUP.APP_VERSION_ID
	where AL.customer_id=@CUSTOMER_ID and AL.app_id=@ID AND AL.APP_VERSION_ID=@VERSION_ID  and AUVI.VEHICLE_ID = @VEHICLE_ID_NUMBER                
	AND ((DATEDIFF(YEAR,AUVI.DRIVER_DOB,AL.APP_EFFECTIVE_DATE)<@INEXPERINCE_DRIVERS_UPPER_AGE) or (DATEDIFF(YEAR,AUVI.DATE_LICENSED,AL.APP_EFFECTIVE_DATE)<@INEXPERINCE_DRIVERS_EXPERINCE_AGE))              
	AND AUUP.POLICY_COMPANY=@POLICY_COMPANY
     
  SET  @MOTORHOMES_INEXPERIENCED_DRIVER =@MOTORHOMES_INEXPERIENCED_DRIVER + @TOTAL_NUMBERDRIVER    
 FETCH NEXT FROM  VEHICLE_ID_CURSOR INTO @VEHICLE_ID_NUMBER                 
          END  ---END WHILE                
       CLOSE VEHICLE_ID_CURSOR    
     DEALLOCATE VEHICLE_ID_CURSOR                
END               
      
          
  --TOTAL NUMBER OF AUTO DRIVERS              
           
              
SET @TOTALNUMER_OF_AUTO_DRIVERS =  @AUTOMOBILE_DRIVER --+ @TOTALNUMER_OF_DRIVERSOTHERS                 
           
  -- MATURE AGE DISCOUNT              
IF( UPPER(@MATUREAGEDISCOUNFORWOLVERINEVOILATIONS)='Y' OR UPPER(@DRIVERAGEOTHERS)='Y')              
SET @MATUREAGEDISCOUNTAUTO = 'Y'              
ELSE              
SET @MATUREAGEDISCOUNTAUTO = 'N'              
END               
---------------------------------------------------------------------------      
IF (UPPER(@UNINSUREDMOTORISTCSLREJECT)=@UNINSUREDMOTORIST_CSL_INDIANA_REJECT OR UPPER(@UNINSUREDMOOTRISTBIPLITREJECT)=@UNINSUREDMOTORIST_BISPLIT_INDIANA_REJECT OR UPPER(@UNDERINSUREDMOTORISTCSLREJECT)=@UNDERINSUREDMOTORIST_CSL_INDIANA_REJECT OR          




  
    
      
       
UPPER(@UNDERINSUREDMOTORISTBISPLITREJECT)=@UNDERINSUREDMOTORIST_BISPLIT_INDIANA_REJECT OR @EXCLUDE_UNINSURED_MOTORIST_OTHERS = 1)                  
                  
SET @EXCLUDE_UNINSURED_MOTORIST ='Y'                  
ELSE                  
SET @EXCLUDE_UNINSURED_MOTORIST ='N'                    
---------------------------------------------------------------------------              
              
              
BEGIN                          
SELECT                
              
 @PERSONALAUTOPOLICYLOWERLIMIT     AS PERSONALAUTOPOLICYLOWERLIMIT,                         
 @PERSONALAUTOPOLICYUPPERLIMIT AS PERSONALAUTOPOLICYUPPERLIMIT,                       
 @AUTOPD AS AUTOPD,                    
 @AUTOCSL AS AUTOCSL,      
-------------------------------------------------------------------      
  isnull(@UNINSUREDMOTORISTCSL,'0') AS    UNINSUNDERINSMOTORISTLIMITCSL,                            
  isnull(@UNINSUREDMOOTRISTBIPLIT,'0') AS UNINSINSMOTORISTLIMITBISPLIT,                            
  isnull(@UNDERINSUREDMOTORISTCSL,'0') AS UNDERINSMOTORISTLIMITCSL,                            
  isnull(@UNDERINSUREDMOTORISTBISPLIT,'0') AS UNDERINSMOTORISTLIMITBISPLIT,                
-------------------------------------------------------------------      
 @AUTOMOBILE            AS AUTOMOBILES,                          
 @AUTOMOBILE_INEXPERIENCED_DRIVER       AS INEXPDRIVERSAUTO,                        
 @MOTORHOMES            AS MOTOTHOMES,                          
 @MOTORHOMES_INEXPERIENCED_DRIVER       AS INEXPDRIVERSMOTORHOME,                        
 @TOTALNUMER_OF_AUTO_DRIVERS  AS  TOTALNUMER_OF_AUTO_MOTORHOME_DRIVERS,              
 @MATUREAGEDISCOUNTAUTO  AS  MATUREAGEDISCOUNTAUTOMOTORHOM,      
  @EXCLUDE_UNINSURED_MOTORIST       AS EXCLUDEUNINSMOTORIST               
END               
           
          
        
      
    
  











GO

