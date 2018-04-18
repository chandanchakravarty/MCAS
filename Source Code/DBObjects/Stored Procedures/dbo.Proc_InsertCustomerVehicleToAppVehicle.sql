IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCustomerVehicleToAppVehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCustomerVehicleToAppVehicle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--drop proc Proc_InsertCustomerVehicleToAppVehicle                     
                    
                    
/*----------------------------------------------------------                     
                    
Proc Name : dbo.Proc_InsertCustomerVehicleToAppVehicle                     
                    
Created by : Mohit                     
                    
Date : 09/05/2005                     
                    
Purpose : To copy from app_vehicles to app_vehicles and to app_vehicle_coverages.                     
                    
Revison History :                     
                    
Used In : Wolverine                     
                    
                    
Modified By : Vijay Arora                     
                    
Modified Date : 13-10-2005                     
                    
Purpose : To also copy the vehicle use, class and type fields.                     
                    
                    
Modified By : Vijay Arora                     
                    
Modified Date : 18-10-2005                     
                    
Purpose : To copy the vehicle class of motorcycle.                     
                    
                    
Modified By : Ashwani                     
                    
Modified Date : 06 Feb. 2006                     
                    
Purpose : To copy the Additional Interest                     
                    
                    
Modified By : Sibin                     
                    
Modified Date : 25-09-2008                     
                    
Purpose : CLASS field should be blank,  
 as no driver added till now and copying Health Care premeir Carrier andcopying Following fields                     
                    
- Is this Vehicle used for Business or Permanent Residence?                     
                    
- Snowplow Conditions                     
                    
                    
------------------------------------------------------------                     
                    
Date Review By Comments                     
                    
------ ------------ -------------------------*/                     
                    
--DROP PROCEDURE dbo.Proc_InsertCustomerVehicleToAppVehicle                     
                    
CREATE PROCEDURE dbo.Proc_InsertCustomerVehicleToAppVehicle                     
                    
(                     
                    
                    
@FROM_CUSTOMER_ID int,                     
                    
@TO_CUSTOMER_ID int,                     
                    
@FROM_APP_ID int,                     
                    
@TO_APP_ID int,                     
                    
@FROM_APP_VERSION_ID int,                     
                    
@TO_APP_VERSION_ID int,                     
                    
@FROM_VEHICLE_ID int,                     
                    
@COVERAGE_TO_BE_COPY Char(1) , --DEFAULT VALUE 'N' MEANS COVERAGE DETAILS NOT TO BE COPY.                     
                    
@CREATED_BY_USER_ID int =14 ,                     
                    
@NEW_VEH_ID Int = NULL OUTPUT                     
                    
)                     
                    
AS                     
                    
BEGIN                     
                    
declare @AUTO_LOB int                     
                    
Declare @To_Vehicle_Id int -- CONTAINS THE RUNING NUMBER FOR VEHICLE_ID                     
                    
Declare @To_Vehicle_Num int -- CONTAINS THE RUNING NUMBER FOR VEHICLE number                     
                    
                    
set @AUTO_LOB = 2                     
                    
                    
SELECT @To_Vehicle_Id = ISNULL(MAX(VEHICLE_ID),0) + 1 ,                     
                    
@To_Vehicle_Num = ISNULL(MAX(INSURED_VEH_NUMBER),0) + 1                     
                    
FROM APP_VEHICLES WITH(NOLOCK)                     
                    
WHERE CUSTOMER_ID=@TO_CUSTOMER_ID AND APP_ID=@TO_APP_ID AND APP_VERSION_ID=@TO_APP_VERSION_ID         
                    
                    
SET @NEW_VEH_ID = @To_Vehicle_Id                     
                    
-- Added by Ashwani <06 Feb. 2006 >                     
                    
DECLARE @IS_ACTIVE CHAR(1)                     
                    
SET @IS_ACTIVE='Y'                     
                    
                    
                    
                    
                    
INSERT INTO APP_VEHICLES                     
                    
(        
                    
CUSTOMER_ID,APP_ID,APP_VERSION_ID,VEHICLE_ID,                     
                    
INSURED_VEH_NUMBER,VEHICLE_YEAR,MAKE,MODEL,VIN,BODY_TYPE,GRG_ADD1,GRG_ADD2,GRG_CITY,                     
                    
GRG_COUNTRY,GRG_STATE,GRG_ZIP,REGISTERED_STATE,TERRITORY,CLASS,REGN_PLATE_NUMBER,                     
                    
ST_AMT_TYPE,AMOUNT,SYMBOL,VEHICLE_AGE,IS_OWN_LEASE,PURCHASE_DATE,IS_NEW_USED,                     
                    
MILES_TO_WORK,VEHICLE_USE,VEH_PERFORMANCE,MULTI_CAR,ANNUAL_MILEAGE,PASSIVE_SEAT_BELT,                 
                    
AIR_BAG,ANTI_LOCK_BRAKES,DEACTIVATE_REACTIVATE_DATE,IS_ACTIVE,CREATED_BY,                     
                    
CREATED_DATETIME,VEHICLE_CC,MOTORCYCLE_TYPE,UNINS_MOTOR_INJURY_COVE,UNINS_PROPERTY_DAMAGE_COVE,UNDERINS_MOTOR_INJURY_COVE,                     
                    
vehicle_type,USE_VEHICLE,CLASS_COM,                     
                    
--CLASS_PER ,                    
                    
VEHICLE_TYPE_PER,VEHICLE_TYPE_COM,APP_VEHICLE_CLASS,                     
                    
--INCLUDED Column IS_SUSPENDED by SIBIN on 11 Nov 2008 to enable copying of IS_SUSPENDED                     
                    
IS_SUSPENDED,                     
                    
                  
--INCLUDED Column BUSS_PERM_RESI by SIBIN to enable copying of BUSS_PERM_RESI                     
                    
BUSS_PERM_RESI,                     
                    
RADIUS_OF_USE,TRANSPORT_CHEMICAL,COVERED_BY_WC_INSU,                     
                    
                    
--INCLUDED Column SNOWPLOW_CONDS by SIBIN to enable copying of SNOWPLOW_CONDS                     
                    
                    
SNOWPLOW_CONDS,                     
                    
CLASS_DESCRIPTION,CYCL_REGD_ROAD_USE,      
--INCLUDED Column CAR_POOL by SIBIN to enable copying of CAR_POOL - Done on 29 Jan 09 for Itrack Issue 5370                
CAR_POOL,  
COMPRH_ONLY --Added By kasana in case of MOTORCYCLE                     
                    
                    
)                     
                    
SELECT                     
                    
@TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Vehicle_Id,                     
                    
@To_Vehicle_Num, VEHICLE_YEAR,MAKE,MODEL,VIN,BODY_TYPE,GRG_ADD1,GRG_ADD2,                     
                    
GRG_CITY,GRG_COUNTRY,GRG_STATE,GRG_ZIP,REGISTERED_STATE,TERRITORY,CLASS,                   
                    
REGN_PLATE_NUMBER,ST_AMT_TYPE,AMOUNT,SYMBOL,VEHICLE_AGE,IS_OWN_LEASE,PURCHASE_DATE,                     
                    
IS_NEW_USED,MILES_TO_WORK,VEHICLE_USE,VEH_PERFORMANCE, MULTI_CAR,                     
                    
ANNUAL_MILEAGE,PASSIVE_SEAT_BELT,AIR_BAG,ANTI_LOCK_BRAKES,DEACTIVATE_REACTIVATE_DATE,                     
                    
@IS_ACTIVE,@CREATED_BY_USER_ID,GETDATE(),VEHICLE_CC,MOTORCYCLE_TYPE,UNINS_MOTOR_INJURY_COVE,UNINS_PROPERTY_DAMAGE_COVE,UNDERINS_MOTOR_INJURY_COVE,                     
                    
vehicle_type,USE_VEHICLE,CLASS_COM,                     
                    
--CLASS_PER,                     
                    
VEHICLE_TYPE_PER,VEHICLE_TYPE_COM,                     
                    
APP_VEHICLE_CLASS,                     
                    
--INCLUDED Column IS_SUSPENDED by SIBIN on 11 Nov 2008 to enable copying of IS_SUSPENDED                     
                    
IS_SUSPENDED,                     
                    
--INCLUDED Column BUSS_PERM_RESI by SIBIN to enable copying of BUSS_PERM_RESI                     
              
                    
BUSS_PERM_RESI,                     
                    
RADIUS_OF_USE,TRANSPORT_CHEMICAL,COVERED_BY_WC_INSU,                     
                    
                    
--INCLUDED Column SNOWPLOW_CONDS by SIBIN to enable copying of SNOWPLOW_CONDS                     
                    
                    
SNOWPLOW_CONDS,                     
                    
CLASS_DESCRIPTION,CYCL_REGD_ROAD_USE,      
--INCLUDED Column CAR_POOL by SIBIN to enable copying of CAR_POOL - Done on 29 Jan 09 for Itrack Issue 5370                
CAR_POOL ,  
COMPRH_ONLY                  
                    
FROM APP_VEHICLES WITH(NOLOCK)                     
                    
WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID                     
                    
AND APP_ID=@FROM_APP_ID                     
                    
AND APP_VERSION_ID=@FROM_APP_VERSION_ID                    
                    
AND VEHICLE_ID=@FROM_VEHICLE_ID                     
                    
---------------------------------------------------------------------------------                     
                    
-- IN CASE OF M/C CLASS =NULL UPDATED BY ASHWANI ON 10 FEB.                     
                    
DECLARE @APP_LOB INT                     
                    
                    
SELECT @APP_LOB=APP_LOB FROM APP_LIST WITH(NOLOCK)                     
                    
WHERE CUSTOMER_ID=@TO_CUSTOMER_ID AND APP_ID=@TO_APP_ID AND APP_VERSION_ID=@TO_APP_VERSION_ID                     
                    
                    
IF (@APP_LOB=3)                     
                    
BEGIN                     
                    
UPDATE APP_VEHICLES                     
                    
SET APP_VEHICLE_CLASS=NULL                     
                    
WHERE CUSTOMER_ID=@TO_CUSTOMER_ID AND APP_ID=@TO_APP_ID AND APP_VERSION_ID=@TO_APP_VERSION_ID                     
                    
AND VEHICLE_ID=@TO_VEHICLE_ID                     
                    
END                     
                    
                    
                    
                    
                    
                    
-- ADDED BY ASHWANI 06 FEB. 2006 -- COPY THE ADDITIONAL INTEREST INFO                     
                    
                    
                    
/*DECLARE @ADD_INT_ID INT                  
                    
SELECT @ADD_INT_ID= ISNULL(MAX(ISNULL(ADD_INT_ID,0)),0)                     
                    
FROM APP_ADD_OTHER_INT                     
                    
WHERE CUSTOMER_ID = @TO_CUSTOMER_ID AND APP_ID=@TO_APP_ID AND APP_VERSION_ID = @TO_APP_VERSION_ID */                     
                    
                    
INSERT INTO APP_ADD_OTHER_INT                     
                    
(                     
                    
CUSTOMER_ID, APP_ID, APP_VERSION_ID, HOLDER_ID, VEHICLE_ID, MEMO, NATURE_OF_INTEREST, RANK, LOAN_REF_NUMBER,                     
                    
IS_ACTIVE, CREATED_BY, CREATED_DATETIME, MODIFIED_BY, LAST_UPDATED_DATETIME, ADD_INT_ID, HOLDER_NAME, HOLDER_ADD1,                     
                    
HOLDER_ADD2, HOLDER_CITY, HOLDER_COUNTRY, HOLDER_STATE, HOLDER_ZIP                     
                    
)                     
                    
SELECT                     
                    
@TO_CUSTOMER_ID, @TO_APP_ID, @TO_APP_VERSION_ID, HOLDER_ID, @To_Vehicle_Id, MEMO, NATURE_OF_INTEREST, RANK,                     
                    
LOAN_REF_NUMBER,@IS_ACTIVE, @CREATED_BY_USER_ID, GETDATE(),NULL, NULL,ADD_INT_ID, HOLDER_NAME, HOLDER_ADD1,                     
                    
HOLDER_ADD2, HOLDER_CITY, HOLDER_COUNTRY, HOLDER_STATE, HOLDER_ZIP                     
                  
FROM APP_ADD_OTHER_INT WITH(NOLOCK)                     
                    
WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID                     
                    
AND APP_ID=@FROM_APP_ID                     
                    
AND APP_VERSION_ID=@FROM_APP_VERSION_ID                     
                    
AND VEHICLE_ID=@FROM_VEHICLE_ID                     
            
-----------------------------------------------------------------------------------                     
                    
                    
--Copy Policy level coverages------------------------------------------                     
                    
DECLARE @EXISTING_VEH_ID Int                     
                    
                    
--************************************************************************                     
                    
CREATE TABLE [#APP_VEHICLE_COVERAGES]                     
                    
(                     
                    
[CUSTOMER_ID] [int] NOT NULL ,                     
                    
[APP_ID] [int] NOT NULL ,                     
                    
[APP_VERSION_ID] [smallint] NOT NULL ,                     
                    
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
                    
[SIGNATURE_OBTAINED] [nchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_APP_VEHICLE_COVERAGES_SIGNATURE_OBTAINED] DEFAULT ('N'),                     
                    
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
                    
SELECT * FROM APP_VEHICLES WITH(NOLOCK)                     
                    
WHERE CUSTOMER_ID=@TO_CUSTOMER_ID                     
       
AND APP_ID=@TO_APP_ID                     
                    
AND APP_VERSION_ID=@TO_APP_VERSION_ID                     
                    
AND VEHICLE_ID <> @To_Vehicle_Id                     
                    
                    
)                     
                    
BEGIN                     
                    
SELECT @EXISTING_VEH_ID = MIN(VEHICLE_ID)                     
                    
FROM APP_VEHICLES WITH(NOLOCK)                     
                    
WHERE CUSTOMER_ID=@TO_CUSTOMER_ID                     
                    
AND APP_ID=@TO_APP_ID                     
                    
AND APP_VERSION_ID=@TO_APP_VERSION_ID                     
                    
AND VEHICLE_ID <> @To_Vehicle_Id                     
                    
                    
INSERT INTO #APP_VEHICLE_COVERAGES                    
                    
(                     
                    
CUSTOMER_ID,APP_ID,APP_VERSION_ID,VEHICLE_ID,COVERAGE_ID,                     
                    
COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,LIMIT_2_TYPE,                     
                    
DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                     
                    
WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE,LIMIT_ID,DEDUC_ID,                     
                    
                    
--INCLUDED Column ADD_INFORMATION by SIBIN to enable copying of ADD_INFORMATION                     
                    
                    
ADD_INFORMATION ,SIGNATURE_OBTAINED,    
--INCLUDED Columns by SIBIN    
 LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,DEDUCTIBLE1_AMOUNT_TEXT,DEDUCTIBLE2_AMOUNT_TEXT                       
                    
)                     
                    
SELECT                     
                    
@TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@TO_VEHICLE_ID,                     
                    
COVERAGE_ID,                     
                    
COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,LIMIT_2_TYPE,                     
                    
DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                     
                    
WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE ,LIMIT_ID,DEDUC_ID,                    
                    
                    
--INCLUDED Column ADD_INFORMATION by SIBIN to enable copying of ADD_INFORMATION                     
                    
                    
ADD_INFORMATION ,SIGNATURE_OBTAINED,    
--INCLUDED Columns by SIBIN    
 LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,DEDUCTIBLE1_AMOUNT_TEXT,DEDUCTIBLE2_AMOUNT_TEXT                    
                    
FROM APP_VEHICLE_COVERAGES WITH(NOLOCK)                     
                    
INNER JOIN MNT_COVERAGE WITH(NOLOCK) ON                     
                    
APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                     
                    
WHERE CUSTOMER_ID=@TO_CUSTOMER_ID                     
                    
AND APP_ID=@TO_APP_ID                     
                    
AND APP_VERSION_ID=@TO_APP_VERSION_ID                     
                    
AND VEHICLE_ID = @EXISTING_VEH_ID                     
                    
AND MNT_COVERAGE.COVERAGE_TYPE = 'PL'            
                    
                    
END                     
                    
ELSE                     
                    
--No vehicle exists in the current application                     
                    
--Copy PL coverages from source vehicle                     
                    
BEGIN                     
                    
INSERT INTO #APP_VEHICLE_COVERAGES                     
                    
(                     
                    
CUSTOMER_ID,APP_ID,APP_VERSION_ID,VEHICLE_ID,COVERAGE_ID,                     
                    
COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,LIMIT_2_TYPE,                     
                    
DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                     
                    
WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE ,LIMIT_ID,DEDUC_ID,                     
                    
                    
--INCLUDED Column ADD_INFORMATION by SIBIN to enable copying of ADD_INFORMATION                     
                    
ADD_INFORMATION ,SIGNATURE_OBTAINED,    
 --INCLUDED Columns by SIBIN    
 LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,DEDUCTIBLE1_AMOUNT_TEXT,DEDUCTIBLE2_AMOUNT_TEXT                                      
)                     
                    
SELECT                     
                    
@TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@TO_VEHICLE_ID,                     
                    
COVERAGE_ID,                     
                    
COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,LIMIT_2_TYPE,                     
                    
DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                     
                    
WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE ,LIMIT_ID,DEDUC_ID,                     
                    
                    
--INCLUDED Column ADD_INFORMATION by SIBIN to enable copying of ADD_INFORMATION                     
                    
                    
ADD_INFORMATION ,SIGNATURE_OBTAINED,    
 --INCLUDED Columns by SIBIN    
 LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,DEDUCTIBLE1_AMOUNT_TEXT,DEDUCTIBLE2_AMOUNT_TEXT                    
                    
FROM APP_VEHICLE_COVERAGES WITH(NOLOCK)                     
                    
INNER JOIN MNT_COVERAGE WITH(NOLOCK) ON                     
                    
APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                     
                    
WHERE CUSTOMER_ID = @FROM_CUSTOMER_ID                     
                    
AND APP_ID = @FROM_APP_ID                     
                    
AND APP_VERSION_ID =@FROM_APP_VERSION_ID                     
                 
AND VEHICLE_ID = @FROM_VEHICLE_ID                     
                    
AND MNT_COVERAGE.COVERAGE_TYPE = 'PL'                     
                    
END                     
                    
                    
                    
                    
--End of Copy policy level coverages-----------------------------------                     
                    
                    
                    
IF ( @COVERAGE_TO_BE_COPY = 'Y') -- COPY ONLY IF 'Y' IS PASSED AS PARAMETER.                     
                    
BEGIN                     
                    
                    
--Vehicle Level coverages                     
                    
INSERT INTO #APP_VEHICLE_COVERAGES                     
             
(                     
                    
CUSTOMER_ID,APP_ID,APP_VERSION_ID,VEHICLE_ID,COVERAGE_ID,                     
                    
COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,LIMIT_2_TYPE,                     
                    
DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                     
                    
WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE ,LIMIT_ID,DEDUC_ID,                  
--Added by Sibin for Itrack Issue 4812 on 10 Dec 08                 
DEDUCTIBLE1_AMOUNT_TEXT ,SIGNATURE_OBTAINED,    
 --INCLUDED Columns by SIBIN    
 LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,DEDUCTIBLE2_AMOUNT_TEXT,ADD_INFORMATION               
                
)                     
                    
SELECT                     
                    
@TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@TO_VEHICLE_ID,                  
                    
COVERAGE_ID,                     
                    
COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,LIMIT_2_TYPE,                     
                    
DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                     
                    
WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE ,LIMIT_ID,DEDUC_ID,                  
--Added by Sibin for Itrack Issue 4812 on 10 Dec 08     
DEDUCTIBLE1_AMOUNT_TEXT,SIGNATURE_OBTAINED,    
 --INCLUDED Columns by SIBIN    
 LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,DEDUCTIBLE2_AMOUNT_TEXT,ADD_INFORMATION                  
                    
                    
FROM APP_VEHICLE_COVERAGES WITH(NOLOCK)                     
                    
INNER JOIN MNT_COVERAGE WITH(NOLOCK) ON                     
                    
APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                     
                    
WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID                     
                    
AND APP_ID=@FROM_APP_ID                     
                    
AND APP_VERSION_ID=@FROM_APP_VERSION_ID                     
                    
AND VEHICLE_ID=@FROM_VEHICLE_ID                     
                    
AND MNT_COVERAGE.COVERAGE_TYPE = 'RL'                     
                    
                    
                    
                    
--------------------------------------------------------------------------------                     
                    
--<ADDED BY ASHWANI ON 06 FEB 2006-- ENDORSEMENT TO BE COPIED WITH VEHICLE>                     
             
INSERT INTO APP_VEHICLE_ENDORSEMENTS                
                    
(                     
                    
CUSTOMER_ID, APP_ID, APP_VERSION_ID, VEHICLE_ID, ENDORSEMENT_ID, REMARKS, VEHICLE_ENDORSEMENT_ID , EDITION_DATE --Edition Date added by Charles on 21-Sep-09                   
                    
)                     
                    
SELECT                     
                    
@TO_CUSTOMER_ID, @TO_APP_ID, @TO_APP_VERSION_ID, @TO_VEHICLE_ID, ENDORSEMENT_ID, REMARKS, VEHICLE_ENDORSEMENT_ID , EDITION_DATE --Edition Date added by Charles on 21-Sep-09                   
                    
FROM APP_VEHICLE_ENDORSEMENTS WITH(NOLOCK)                     
                    
WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID                     
                    
AND APP_ID=@FROM_APP_ID AND APP_VERSION_ID=@FROM_APP_VERSION_ID AND VEHICLE_ID=@FROM_VEHICLE_ID                     
                    
                    
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
                    
INSERT INTO APP_VEHICLE_COVERAGES                     
                    
(                     
                    
CUSTOMER_ID,APP_ID,APP_VERSION_ID,VEHICLE_ID,COVERAGE_ID,                     
                    
COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,LIMIT_2_TYPE,                     
                    
DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                     
                    
WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE ,LIMIT_ID,DEDUC_ID, ADD_INFORMATION,                  
--Added by Sibin for Itrack Issue 4812 on 10 Dec 08                 
DEDUCTIBLE1_AMOUNT_TEXT,SIGNATURE_OBTAINED,    
--INCLUDED Columns by SIBIN    
LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,DEDUCTIBLE2_AMOUNT_TEXT                   
                 
                    
)                     
                    
SELECT                     
                    
CUSTOMER_ID,APP_ID,APP_VERSION_ID,VEHICLE_ID,                     
                    
IDENT_COL,                     
                    
COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,LIMIT_2_TYPE,                     
                    
DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                     
                    
WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE,LIMIT_ID,DEDUC_ID, ADD_INFORMATION,                  
--Added by Sibin for Itrack Issue 4812 on 10 Dec 08                 
DEDUCTIBLE1_AMOUNT_TEXT ,SIGNATURE_OBTAINED,    
 --INCLUDED Columns by SIBIN    
 LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT ,DEDUCTIBLE2_AMOUNT_TEXT                
                
                    
FROM #APP_VEHICLE_COVERAGES                     
                    
                    
--DROP TABLE #APP_VEHICLE_COVERAGES                      
                    
                    
--Insert data at APP_MISCELLANEOUS_EQUIPMENT_VALUES for Automobile LOB                     
                    
if(@APP_LOB=@AUTO_LOB)                     
                    
begin                     
                    
INSERT INTO APP_MISCELLANEOUS_EQUIPMENT_VALUES                     
                    
(                     
                    
CUSTOMER_ID, APP_ID, APP_VERSION_ID, VEHICLE_ID, ITEM_ID,ITEM_DESCRIPTION,ITEM_VALUE,IS_ACTIVE,                     
                    
CREATED_BY, CREATED_DATETIME                     
                    
)                     
                    
SELECT                     
                    
@TO_CUSTOMER_ID, @TO_APP_ID, @TO_APP_VERSION_ID, @TO_VEHICLE_ID,ITEM_ID,ITEM_DESCRIPTION,ITEM_VALUE, @IS_ACTIVE, @CREATED_BY_USER_ID, GETDATE()                     
                
FROM                     
                    
APP_MISCELLANEOUS_EQUIPMENT_VALUES WITH(NOLOCK)                     
                    
WHERE                     
                    
CUSTOMER_ID=@FROM_CUSTOMER_ID AND                     
                    
APP_ID=@FROM_APP_ID AND                     
                    
APP_VERSION_ID=@FROM_APP_VERSION_ID AND                     
                    
VEHICLE_ID=@FROM_VEHICLE_ID                     
                    
end                     
                    
                    
END   
  
GO

