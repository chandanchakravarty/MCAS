IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCustomerVehicleToPolVehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCustomerVehicleToPolVehicle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_InsertCustomerVehicleToPolVehicle                           
                         
/*----------------------------------------------------------                                                  
Proc Name       : dbo.Proc_InsertCustomerVehicleToPolVehicle                                                  
                                          
Purpose         : To copy from  POL_VEHICLES to POL_VEHICLES and to POL_VEHICLES_coverages.                                                  
Revison History :                                                  
Used In         :   Ebix Advantage web                                                  
                                                
                                              
Modified By		:     Sibin                                              
Modified Date	: 26-09-2008                                       
Purpose			: CLASS field should be blank, as no driver added till now and copying Health Care premeir Carrier andcopying Following fields            
					- Is this Vehicle used for Business or Permanent Residence?            
				    - Snowplow Conditions            
				    
Modified By		:     Lalit Chauhan	                                              
Modified Date	:     Oct 05 ,2010
Purpose			:     INSERT Risk For Default broker in POL_REMUNERATION When copy vehical from other policy
            
------------------------------------------------------------                                                  
Date     Review By          Comments            
Drop PROCEDURE dbo.Proc_InsertCustomerVehicleToPolVehicle                                       
------   ------------       -------------------------*/            
          
CREATE PROCEDURE [dbo].[Proc_InsertCustomerVehicleToPolVehicle]                                          
(                                          
                                                       
 @FROM_CUSTOMER_ID int,                                          
 @TO_CUSTOMER_ID int,                                          
 @FROM_POLICY_ID int,                                          
 @TO_POLICY_ID int,                                          
 @FROM_POLICY_VERSION_ID int,                                          
 @TO_POLICY_VERSION_ID int,                                          
 @FROM_VEHICLE_ID int,                                           
 @COVERAGE_TO_BE_COPY  Char(1) ,  --DEFAULT VALUE 'N' MEANS COVERAGE DETAILS NOT TO BE COPY.                                            
 @CREATED_BY_USER_ID  int  =14 ,                          
 @NEW_VEH_ID Int = NULL              OUTPUT                        
)                                          
AS                                          
BEGIN                  
DECLARE @AUTO_LOB smallint                                        
Declare @To_Vehicle_Id int     -- CONTAINS THE RUNING NUMBER FOR VEHICLE_ID                                          
Declare @To_Vehicle_Num int    -- CONTAINS THE RUNING NUMBER FOR VEHICLE number                                          
                                          
set @AUTO_LOB = 2                
SELECT  @To_Vehicle_Id = ISNULL(MAX(VEHICLE_ID),0) + 1 ,                                          
@To_Vehicle_Num =  ISNULL(MAX(INSURED_VEH_NUMBER),0) + 1                                           
 FROM           POL_VEHICLES                                           
 WHERE       CUSTOMER_ID=@TO_CUSTOMER_ID AND POLICY_ID=@TO_POLICY_ID AND POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                                             
                          
SET @NEW_VEH_ID =  @To_Vehicle_Id                               
                                          
 DECLARE @IS_ACTIVE CHAR(1)                                  
 SET @IS_ACTIVE='Y'                  
INSERT INTO POL_VEHICLES                  
(                  
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,VEHICLE_ID,INSURED_VEH_NUMBER,VEHICLE_YEAR,                  
MAKE,MODEL,VIN,BODY_TYPE,GRG_ADD1,GRG_ADD2,GRG_CITY,GRG_COUNTRY,GRG_STATE,GRG_ZIP,                  
REGISTERED_STATE,TERRITORY,CLASS,ANTI_LOCK_BRAKES,MOTORCYCLE_TYPE,REGN_PLATE_NUMBER,                  
ST_AMT_TYPE,VEHICLE_TYPE,AMOUNT,SYMBOL,VEHICLE_AGE,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,                  
VEHICLE_CC,IS_OWN_LEASE,PURCHASE_DATE,IS_NEW_USED,MILES_TO_WORK,VEHICLE_USE,VEH_PERFORMANCE,MULTI_CAR,                  
ANNUAL_MILEAGE,PASSIVE_SEAT_BELT,AIR_BAG,                                      
--UNINS_MOTOR_INJURY_COVE,                                      
--UNINS_PROPERTY_DAMAGE_COVE,                                      
--UNDERINS_MOTOR_INJURY_COVE,                                      
APP_USE_VEHICLE_ID,             
                                     
--APP_VEHICLE_PERCLASS_ID,  To disable Copying of Class.Done by Sibin           
                     
APP_VEHICLE_COMCLASS_ID,                                        
APP_VEHICLE_PERTYPE_ID,                                        
APP_VEHICLE_COMTYPE_ID,                  
SAFETY_BELT,          
          
--INCLUDED Column BUSS_PERM_RESI,SNOWPLOW_CONDS and by SIBIN to enable copying of BUSS_PERM_RESI,SNOWPLOW_CONDS             
BUSS_PERM_RESI,          
SNOWPLOW_CONDS,        
          
              
RADIUS_OF_USE,TRANSPORT_CHEMICAL,COVERED_BY_WC_INSU,CLASS_DESCRIPTION,CYCL_REGD_ROAD_USE ,          
COMPRH_ONLY,IS_SUSPENDED,      
--INCLUDED Column CAR_POOL by SIBIN to enable copying of CAR_POOL - Done on 29 Jan 09 for Itrack Issue 5370                
CAR_POOL                                
)                                          
SELECT                  
@TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@To_Vehicle_Id,                                          
@To_Vehicle_Num,VEHICLE_YEAR,MAKE,MODEL,VIN,BODY_TYPE,GRG_ADD1,                  
GRG_ADD2,GRG_CITY,GRG_COUNTRY,GRG_STATE,GRG_ZIP,REGISTERED_STATE,TERRITORY,                  
CLASS,ANTI_LOCK_BRAKES,MOTORCYCLE_TYPE,REGN_PLATE_NUMBER,ST_AMT_TYPE,                  
VEHICLE_TYPE,AMOUNT,SYMBOL,VEHICLE_AGE,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,                  
VEHICLE_CC,IS_OWN_LEASE,PURCHASE_DATE,IS_NEW_USED,MILES_TO_WORK,VEHICLE_USE,VEH_PERFORMANCE,                  
MULTI_CAR,ANNUAL_MILEAGE,PASSIVE_SEAT_BELT,AIR_BAG,                  
--UNINS_MOTOR_INJURY_COVE,                   
--UNINS_PROPERTY_DAMAGE_COVE,                                      
--UNDERINS_MOTOR_INJURY_COVE,                                      
APP_USE_VEHICLE_ID,            
                                      
--APP_VEHICLE_PERCLASS_ID,  To disable Copying of Class.Done by Sibin           
          
APP_VEHICLE_COMCLASS_ID,                                        
APP_VEHICLE_PERTYPE_ID,                                        
APP_VEHICLE_COMTYPE_ID,                  
SAFETY_BELT,             
          
--INCLUDED Column BUSS_PERM_RESI,SNOWPLOW_CONDS and by SIBIN to enable copying of BUSS_PERM_RESI,SNOWPLOW_CONDS             
BUSS_PERM_RESI,          
SNOWPLOW_CONDS,          
              
RADIUS_OF_USE,TRANSPORT_CHEMICAL,COVERED_BY_WC_INSU,CLASS_DESCRIPTION,CYCL_REGD_ROAD_USE ,          
COMPRH_ONLY,IS_SUSPENDED,      
--INCLUDED Column CAR_POOL by SIBIN to enable copying of CAR_POOL - Done on 29 Jan 09 for Itrack Issue 5370                
CAR_POOL                     
                  
FROM       POL_VEHICLES                                          
WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                          
AND         POLICY_ID=@FROM_POLICY_ID                             
AND         POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                          
AND         VEHICLE_ID=@FROM_VEHICLE_ID                                    

/*Added by Lalit For add risk in remuneration when copy vehicle*/

EXEC Proc_UpdateRisk_Renumeration    
			@TO_CUSTOMER_ID,@TO_POLICY_ID ,      
			@TO_POLICY_VERSION_ID,@To_Vehicle_Id,@CREATED_BY_USER_ID 

                       
DECLARE @POLICY_LOB INT                      
                      
SELECT @POLICY_LOB=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST                       
WHERE CUSTOMER_ID=@TO_CUSTOMER_ID  AND POLICY_ID=@TO_POLICY_ID  AND  POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                                          
                       
IF (@POLICY_LOB=3)                       
BEGIN                       
 UPDATE POL_VEHICLES       SET APP_VEHICLE_CLASS=NULL                      
 WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID  AND POLICY_ID=@TO_POLICY_ID  AND  POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                                          
 AND  VEHICLE_ID=@TO_VEHICLE_ID                       
END                       
                              
                                
  INSERT INTO POL_ADD_OTHER_INT                                          
  (                                          
    CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, HOLDER_ID, VEHICLE_ID, MEMO, NATURE_OF_INTEREST, RANK, LOAN_REF_NUMBER,                                
     IS_ACTIVE, CREATED_BY, CREATED_DATETIME, MODIFIED_BY, LAST_UPDATED_DATETIME, ADD_INT_ID, HOLDER_NAME, HOLDER_ADD1,                
     HOLDER_ADD2, HOLDER_CITY, HOLDER_COUNTRY, HOLDER_STATE, HOLDER_ZIP                                         
  )                                          
  SELECT                                          
    @TO_CUSTOMER_ID, @TO_POLICY_ID, @TO_POLICY_VERSION_ID, HOLDER_ID, @To_Vehicle_Id, MEMO, NATURE_OF_INTEREST, RANK,                                
    LOAN_REF_NUMBER,@IS_ACTIVE, @CREATED_BY_USER_ID, GETDATE(),NULL, NULL,ADD_INT_ID, HOLDER_NAME, HOLDER_ADD1,                                
    HOLDER_ADD2, HOLDER_CITY, HOLDER_COUNTRY, HOLDER_STATE, HOLDER_ZIP                                               
  FROM       POL_ADD_OTHER_INT                                          
  WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                          
  AND         POLICY_ID=@FROM_POLICY_ID                                          
AND         POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                          
  AND         VEHICLE_ID=@FROM_VEHICLE_ID                               
-----------------------------------------------------------------------------------                                          
                   
 --Copy Policy level coverages------------------------------------------                  
 DECLARE @EXISTING_VEH_ID Int                    
                       
--************************************************************************                    
CREATE TABLE [#POL_VEHICLE_COVERAGES]                     
(                    
 [CUSTOMER_ID] [int] NOT NULL ,                    
 [POLICY_ID] [int] NOT NULL ,                    
 [POLICY_VERSION_ID] [smallint] NOT NULL ,                    
 [VEHICLE_ID] [smallint] NOT NULL ,                    
 [COVERAGE_ID] [int] NOT NULL ,                    
 [COVERAGE_CODE_ID] [int] NOT NULL ,                    
 [LIMIT_OVERRIDE] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                    
 [LIMIT_1] [decimal](18, 0) NULL ,                    
 [LIMIT_1_TYPE] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                    
 [LIMIT_2] [decimal](18, 0) NULL ,                    
 [LIMIT_2_TYPE] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                    
 [DEDUCT_OVERRIDE] [nchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                    
 [DEDUCTIBLE_1] [decimal](18, 0) NULL ,                    
 [DEDUCTIBLE_1_TYPE] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                    
 [DEDUCTIBLE_2] [decimal](18, 0) NULL ,                    
 [DEDUCTIBLE_2_TYPE] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                    
 [WRITTEN_PREMIUM] [decimal](18, 2) NULL ,                    
 [FULL_TERM_PREMIUM] [decimal](18, 2) NULL ,                    
 [IS_SYSTEM_COVERAGE] [nchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                    
 [LIMIT1_AMOUNT_TEXT] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                    
 [LIMIT2_AMOUNT_TEXT] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,               
 [DEDUCTIBLE1_AMOUNT_TEXT] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                    
 [DEDUCTIBLE2_AMOUNT_TEXT] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                    
 [SIGNATURE_OBTAINED] [nchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_POL_VEHICLE_COVERAGES_SIGNATURE_OBTAINED] DEFAULT ('N'),                    
 [LIMIT_ID] [int] NULL ,                    
 [DEDUC_ID] [int] NULL ,                    
  [IDENT_COL] Int IDENTITY(1,1),           
          
--Added by Sibin to include Health Care premier Carrier.Length changed to 100 from 40 - Changed by Sibin           
 [ADD_INFORMATION] NVARCHAR(100)                     
                     
)                    
--**************************************************************************                    
                        
                    
  --Policy Level coverages                        
   -- If previous vehicles exists                     
   IF EXISTS                    
   (                    
 SELECT * FROM POL_VEHICLES                    
 WHERE    CUSTOMER_ID=@TO_CUSTOMER_ID                                          
   AND         POLICY_ID=@TO_POLICY_ID                                          
   AND         POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                                          
   AND         VEHICLE_ID <> @To_Vehicle_Id                    
                    
   )                    
   BEGIN                    
 SELECT @EXISTING_VEH_ID = MIN(VEHICLE_ID)                    
 FROM POL_VEHICLES                    
 WHERE    CUSTOMER_ID=@TO_CUSTOMER_ID                                          
   AND         POLICY_ID=@TO_POLICY_ID                                          
   AND         POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                                          
   AND         VEHICLE_ID <> @To_Vehicle_Id                      
                      
 INSERT INTO #POL_VEHICLE_COVERAGES                                          
    (                                          
     CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,VEHICLE_ID,COVERAGE_ID,                                          
     COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,LIMIT_2_TYPE,                                          
     DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                                          
     WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE,LIMIT_ID,DEDUC_ID,           
          
 --INCLUDED Column ADD_INFORMATION by SIBIN to enable copying of ADD_INFORMATION             
                
       ADD_INFORMATION ,    
 --INCLUDED Columns by SIBIN    
 DEDUCTIBLE1_AMOUNT_TEXT,DEDUCTIBLE2_AMOUNT_TEXT,LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,SIGNATURE_OBTAINED                                           
    )                                          
    SELECT                                          
    @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@TO_VEHICLE_ID,                    
    COVERAGE_ID,                     
    COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,LIMIT_2_TYPE,                                          
    DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                                          
    WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE ,LIMIT_ID,DEDUC_ID,           
          
 --INCLUDED Column ADD_INFORMATION by SIBIN to enable copying of ADD_INFORMATION             
                
       ADD_INFORMATION,    
 --INCLUDED Columns by SIBIN    
 DEDUCTIBLE1_AMOUNT_TEXT,DEDUCTIBLE2_AMOUNT_TEXT,LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,SIGNATURE_OBTAINED            
                                          
    FROM       POL_VEHICLE_COVERAGES                    
    INNER JOIN MNT_COVERAGE ON                    
  POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                                           
    WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                                          
    AND         POLICY_ID=@TO_POLICY_ID                                          
    AND         POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                                          
    AND         VEHICLE_ID = @EXISTING_VEH_ID                      
    AND      MNT_COVERAGE.COVERAGE_TYPE = 'PL'                       
                     
   END                    
   ELSE                    
   BEGIN                    
 INSERT INTO #POL_VEHICLE_COVERAGES                                          
    (                                          
     CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,VEHICLE_ID,COVERAGE_ID,                                          
     COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,LIMIT_2_TYPE,                                          
 DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                                          
     WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE ,LIMIT_ID,DEDUC_ID,           
          
 --INCLUDED Column ADD_INFORMATION by SIBIN to enable copying of ADD_INFORMATION             
                
       ADD_INFORMATION,    
 --INCLUDED Columns by SIBIN    
 DEDUCTIBLE1_AMOUNT_TEXT,DEDUCTIBLE2_AMOUNT_TEXT ,LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,SIGNATURE_OBTAINED                                           
    )                                          
    SELECT                                          
    @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@TO_VEHICLE_ID,                    
  COVERAGE_ID,                     
    COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,LIMIT_2_TYPE,                                          
    DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                                          
    WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE ,LIMIT_ID,DEDUC_ID,             
          
 --INCLUDED Column ADD_INFORMATION by SIBIN to enable copying of ADD_INFORMATION             
                
       ADD_INFORMATION,    
 --INCLUDED Columns by SIBIN    
 DEDUCTIBLE1_AMOUNT_TEXT,DEDUCTIBLE2_AMOUNT_TEXT,LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,SIGNATURE_OBTAINED              
                                        
    FROM       POL_VEHICLE_COVERAGES                    
    INNER JOIN MNT_COVERAGE ON                    
  POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                                           
    WHERE   CUSTOMER_ID = @FROM_CUSTOMER_ID                                          
    AND         POLICY_ID = @FROM_POLICY_ID                                          
    AND         POLICY_VERSION_ID  =@FROM_POLICY_VERSION_ID                                          
    AND         VEHICLE_ID = @FROM_VEHICLE_ID                      
    AND      MNT_COVERAGE.COVERAGE_TYPE = 'PL'                        
   END                      
                       
                   
                       --End of Copy policy level coverages-----------------------------------                  
                  
                                         
 IF ( @COVERAGE_TO_BE_COPY = 'Y')  -- COPY ONLY IF  'Y' IS PASSED AS PARAMETER.                                       
  BEGIN                    
                        
    --Vehicle Level coverages                                            
   INSERT INTO #POL_VEHICLE_COVERAGES                                          
   (                                          
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,VEHICLE_ID,COVERAGE_ID,                             
 COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,LIMIT_2_TYPE,                                          
 DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                                          
  WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE ,LIMIT_ID,DEDUC_ID,        
 --Added by Sibin for Itrack Issue 4812 on 10 Dec 08           
 DEDUCTIBLE1_AMOUNT_TEXT ,    
 --INCLUDED Columns by SIBIN    
 LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,SIGNATURE_OBTAINED,DEDUCTIBLE2_AMOUNT_TEXT                                        
   )                                          
   SELECT                                          
   @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@TO_VEHICLE_ID,                    
 COVERAGE_ID,                     
   COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,LIMIT_2_TYPE,                                          
   DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                                          
   WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE ,LIMIT_ID,DEDUC_ID,        
 --Added by Sibin for Itrack Issue 4812 on 10 Dec 08           
 DEDUCTIBLE1_AMOUNT_TEXT ,    
 --INCLUDED Columns by SIBIN    
 LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,SIGNATURE_OBTAINED,DEDUCTIBLE2_AMOUNT_TEXT                    
                              
   FROM       POL_VEHICLE_COVERAGES                    
   INNER JOIN MNT_COVERAGE ON                    
 POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                                    
   WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                          
   AND         POLICY_ID=@FROM_POLICY_ID                                          
   AND         POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                          
   AND         VEHICLE_ID=@FROM_VEHICLE_ID                      
AND      MNT_COVERAGE.COVERAGE_TYPE = 'RL'                       
                   
   -- ENDORSEMENT TO BE COPIED WITH VEHICLE                                        
   INSERT INTO POL_VEHICLE_ENDORSEMENTS                            
   (                            
  CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, VEHICLE_ID, ENDORSEMENT_ID, REMARKS, VEHICLE_ENDORSEMENT_ID , EDITION_DATE --Edition Date added by Charles on 21-Sep-09                               
 )                            
  SELECT                             
   @TO_CUSTOMER_ID, @TO_POLICY_ID, @TO_POLICY_VERSION_ID, @TO_VEHICLE_ID, ENDORSEMENT_ID, REMARKS, VEHICLE_ENDORSEMENT_ID , EDITION_DATE --Edition Date added by Charles on 21-Sep-09                               
   FROM   POL_VEHICLE_ENDORSEMENTS                            
   WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                          
   AND POLICY_ID=@FROM_POLICY_ID AND  POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID AND VEHICLE_ID=@FROM_VEHICLE_ID                         
                          
   /*                           
    --Copy default coverages and endorsements for the new vehicle--------------------------                          
   EXEC Proc_Update_VEHICLE_COVERAGES                                                                                                
   @TO_CUSTOMER_ID,--@CUSTOMER_ID int,                                                              
   @TO_APP_ID,--@APP_ID int,                                                              
   @TO_APP_VERSION_ID,--@APP_VERSION_ID smallint,                                                               
   @To_Vehicle_Id--@VEHICLE_ID smallint                                    
   ------------------------------------------------------------------------                          
  */                          
                           
  END                   
                  
--Insert from temporary table                    
    INSERT INTO POL_VEHICLE_COVERAGES                                          
   (                                          
  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,VEHICLE_ID,COVERAGE_ID,                                          
  COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,LIMIT_2_TYPE,                                          
 DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                                          
  WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE ,LIMIT_ID,DEDUC_ID,           
 --Added by Sibin for Itrack Issue 4812 on 10 Dec 08           
 DEDUCTIBLE1_AMOUNT_TEXT,         
 --INCLUDED Column ADD_INFORMATION by SIBIN to enable copying of ADD_INFORMATION             
                
       ADD_INFORMATION,    
 --INCLUDED Columns by SIBIN    
 LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,SIGNATURE_OBTAINED,DEDUCTIBLE2_AMOUNT_TEXT                                           
   )                                          
   SELECT                                          
   CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,VEHICLE_ID,                    
 IDENT_COL,                     
   COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,LIMIT_2_TYPE,                                          
   DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                                          
   WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE,LIMIT_ID,DEDUC_ID,          
  --Added by Sibin for Itrack Issue 4812 on 10 Dec 08           
 DEDUCTIBLE1_AMOUNT_TEXT ,        
 --INCLUDED Column ADD_INFORMATION by SIBIN to enable copying of ADD_INFORMATION             
                
       ADD_INFORMATION ,    
 --INCLUDED Columns by SIBIN    
 LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,SIGNATURE_OBTAINED,DEDUCTIBLE2_AMOUNT_TEXT           
                                              
   FROM       #POL_VEHICLE_COVERAGES       
                  
DROP TABLE #POL_VEHICLE_COVERAGES                    
                
--Insert data at POL_MISCELLANEOUS_EQUIPMENT_VALUES for Automobile LOB                
if(@POLICY_LOB=@AUTO_LOB)                
begin                
 INSERT INTO POL_MISCELLANEOUS_EQUIPMENT_VALUES                                          
 (                                          
  CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, VEHICLE_ID, ITEM_ID,ITEM_DESCRIPTION,ITEM_VALUE,IS_ACTIVE,                
  CREATED_BY, CREATED_DATETIME                
 )                                          
 SELECT                                          
  @TO_CUSTOMER_ID, @TO_POLICY_ID, @TO_POLICY_VERSION_ID, @TO_VEHICLE_ID,ITEM_ID,ITEM_DESCRIPTION,ITEM_VALUE,                
  @IS_ACTIVE, @CREATED_BY_USER_ID, GETDATE()                
 FROM                       
  POL_MISCELLANEOUS_EQUIPMENT_VALUES                                          
 WHERE                   
  CUSTOMER_ID=@FROM_CUSTOMER_ID AND                
        POLICY_ID=@FROM_POLICY_ID AND                
     POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID AND                
        VEHICLE_ID=@FROM_VEHICLE_ID                    
end                  
                                       
END                   
                
                
                  
                  
					
GO

