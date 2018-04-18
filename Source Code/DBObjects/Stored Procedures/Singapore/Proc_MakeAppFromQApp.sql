IF  EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[DBO].[Proc_MakeAppFromQApp]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [DBO].[Proc_MakeAppFromQApp]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO             
/*                
Created By  : Charles Gomes                
Comments  : Convert Quick Application to Application              
Created Date : 23-Jun-10              
Modified by:Sonal              
Modified date:20 july 2010              
*/              
 --DROP PROC dbo.Proc_MakeAppFromQApp   28754,363,1,'V1000389APP'           
CREATE PROC [dbo].[Proc_MakeAppFromQApp]              
(              
@CUSTOMER_ID INT,              
@POLICY_ID INT,               
@POLICY_VERSION_ID SMALLINT,             
@APP_NUMBER NVARCHAR(75)             
)              
AS              
BEGIN              
 DECLARE @QQ_ID INT              
 DECLARE @QQ_NUMBER VARCHAR(20)              
 DECLARE @AGENCY_ID INT           
           
 IF not EXISTS(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE              
 CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)           
 BEGIN          
 UPDATE POL_CUSTOMER_POLICY_LIST SET CUSTOMER_ID = @CUSTOMER_ID WHERE              
  CUSTOMER_ID = -100 AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID            
 END            
 IF not EXISTS(SELECT CUSTOMER_ID FROM QQ_MOTOR_QUOTE_DETAILS WITH(NOLOCK) WHERE              
 CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)           
 BEGIN          
 UPDATE QQ_MOTOR_QUOTE_DETAILS SET CUSTOMER_ID = @CUSTOMER_ID              
  WHERE CUSTOMER_ID=-100 and  POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID            
 END           
           
 IF not EXISTS(SELECT CUSTOMER_ID FROM CLT_QUICKQUOTE_LIST WITH(NOLOCK) WHERE              
 CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@POLICY_ID AND APP_VERSION_ID=@POLICY_VERSION_ID)           
 BEGIN          
UPDATE CLT_QUICKQUOTE_LIST SET QQ_APP_NUMBER= @APP_NUMBER,CUSTOMER_ID = @CUSTOMER_ID WHERE              
  CUSTOMER_ID = -100 AND APP_ID=@POLICY_ID AND APP_VERSION_ID=@POLICY_VERSION_ID              
 END           
  --ADDED BY KULDEEP ON 14/1/2012 WHEN QUOTE IS NOT DISPLAYED AFTER MAKE APP    
  SELECT @QQ_ID=QQ_ID FROM CLT_QUICKQUOTE_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@POLICY_ID AND APP_VERSION_ID=@POLICY_VERSION_ID    
  IF not EXISTS(SELECT CUSTOMER_ID FROM QQ_CUSTOMER_PARTICULAR WITH(NOLOCK) WHERE              
 CUSTOMER_ID = @CUSTOMER_ID AND QUOTE_ID=@QQ_ID)           
 BEGIN          
UPDATE QQ_CUSTOMER_PARTICULAR SET CUSTOMER_ID = @CUSTOMER_ID WHERE              
  CUSTOMER_ID = -100 AND   QUOTE_ID=@QQ_ID           
 END      
  --TILL HERE      
       
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE              
 CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)              
 BEGIN              
               
                  
  UPDATE POL_CUSTOMER_POLICY_LIST SET APP_STATUS = 'APPLICATION', APP_NUMBER = @APP_NUMBER WHERE              
  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID              
                
               
                
  UPDATE CLT_QUICKQUOTE_LIST SET QQ_APP_NUMBER= @APP_NUMBER WHERE              
  CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@POLICY_ID AND APP_VERSION_ID=@POLICY_VERSION_ID              
                
               
                  
   IF EXISTS(SELECT CUSTOMER_ID FROM QOT_CUSTOMER_QUOTE_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@POLICY_ID AND APP_VERSION_ID=@POLICY_VERSION_ID)              
   BEGIN              
                
    DECLARE @QUOTE_VERSION_ID smallint                                     
    DECLARE @QUOTE_NUMBER nvarchar(75)               
    DECLARE @QUOTE_ID int              
                
    SELECT @QUOTE_ID = ISNULL(MAX(QUOTE_ID),0) + 1 from QOT_CUSTOMER_QUOTE_LIST_POL    WITH(NOLOCK)                      
    WHERE CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID ;                                 
                
                    
       -- declare @QUOTE_NUMBER nvarchar(75)                                        
    SELECT @QUOTE_NUMBER= 'Q-' + APP_NUMBER                                   
    FROM POl_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID  and POLICY_ID =@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID               
                 
    INSERT INTO [dbo].[QOT_CUSTOMER_QUOTE_LIST_POL](CUSTOMER_ID,QUOTE_ID,QUOTE_VERSION_ID,POLICY_ID,POLICY_VERSION_ID,                                    
    QUOTE_TYPE,QUOTE_NUMBER,QUOTE_DESCRIPTION,IS_ACCEPTED,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,QUOTE_XML,QUOTE_INPUT_XML,                    
    RATE_EFFECTIVE_DATE,BUSINESS_TYPE )              
    SELECT @CUSTOMER_ID,@QUOTE_ID,1,@POLICY_ID,@POLICY_VERSION_ID,QUOTE_TYPE,@QUOTE_NUMBER,QUOTE_DESCRIPTION,IS_ACCEPTED,IS_ACTIVE,              
    CREATED_BY,CREATED_DATETIME,QUOTE_XML,QUOTE_INPUT_XML,RATE_EFFECTIVE_DATE,BUSINESS_TYPE               
    FROM QOT_CUSTOMER_QUOTE_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@POLICY_ID AND APP_VERSION_ID=@POLICY_VERSION_ID              
   END              
                
                 
                
  --SELECT @QQ_ID = ISNULL(MAX(QQ_ID),0)+1 FROM CLT_QUICKQUOTE_LIST WITH(NOLOCK)              
                
  --INSERT INTO [dbo].[CLT_QUICKQUOTE_LIST]              
  --     ([CUSTOMER_ID],[QQ_ID],[QQ_NUMBER],[QQ_TYPE],[QQ_XML],[QQ_APP_NUMBER],[IS_ACTIVE],[QQ_RATING_REPORT],[QQ_XML_TIME]              
  --       ,[QQ_RATING_TIME],[QQ_STATE],[APP_ID],[APP_VERSION_ID])                      
  --      SELECT CUSTOMER_ID, @QQ_ID, @QQ_NUMBER, NULL, NULL, APP_NUMBER, 'Y', NULL, NULL, NULL, NULL, POLICY_ID, POLICY_VERSION_ID              
  --      FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID              
                      
  --       --Maintain co-applicant details              
           --UNCOMMENT BY KULDEEP           
         DECLARE @APPLICANT_ID INT               
   DECLARE @IS_PRIMARY_APPLICANT INT              
                 
   SET @APPLICANT_ID=0                            
   SET @IS_PRIMARY_APPLICANT=0                        
                            
   SELECT @APPLICANT_ID = APPLICANT_ID                             
   FROM CLT_APPLICANT_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND IS_PRIMARY_APPLICANT = 1                            
                              
                            
  IF (@APPLICANT_ID > 0)                            
  BEGIN                            
                  
   SET @IS_PRIMARY_APPLICANT=1                        
  END                       
                      
   IF NOT EXISTS(SELECT CUSTOMER_ID FROM POL_APPLICANT_LIST  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)              
   BEGIN                      
   INSERT INTO POL_APPLICANT_LIST                                                                                               
   (                                                   
    POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID, APPLICANT_ID,CREATED_DATETIME,IS_PRIMARY_APPLICANT                                
   )                   
   VALUES                                                                                                                         
   (@POLICY_ID,@POLICY_VERSION_ID,@CUSTOMER_ID,@APPLICANT_ID,GETDATE(),@IS_PRIMARY_APPLICANT)                                
   END               
            --TILL HERE          
        --Maintain renumeration tab for quick application              
         SELECT @AGENCY_ID = CUSTOMER_AGENCY_ID FROM CLT_CUSTOMER_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID              
   IF (ISNULL(@AGENCY_ID,0) <> 0 )              
    exec Proc_UpdateDefaultBroker @AGENCY_ID,@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,-1              
    --Setting Commission Default to 100% this only on temporary basis (by kuldeep 10_jan_2012) to commit the policy and bypass the rule          
    update POL_REMUNERATION set COMMISSION_PERCENT=100 WHERE CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID ;          
           
   --For Vehicle info  BY  KULDEEP ON 7_JAN_2012            
   DECLARE @VEHICLE_ID SMALLint            
   DECLARE @VEHICLE_YEAR nvarchar(4)            
   DECLARE @MAKE nvarchar(75)            
   DECLARE @MODEL nvarchar(75)            
   DECLARE @VEHICLE_CC int            
   DECLARE @VEHICLE_TYPE int            
   DECLARE @IS_ACTIVE nchar(1)            
   DECLARE @TOTAL_DRIVERS int            
   DECLARE @RISK_CURRENCY int            
   DECLARE @VEHICLE_COVERAGE nvarchar(50)            
   DECLARE @CREATED_BY INT
   DECLARE @CHASSIS_NO NVARCHAR(50) --added by avijit
   DECLARE @REGISTRATION_NO NVARCHAR (50) --added by avijit
   DECLARE @ENGINE_NO NVARCHAR (50)       --added by avijit    
   SELECT @VEHICLE_ID= ISNULL(MAX(VEHICLE_ID),0) + 1 from POL_VEHICLES    WITH(NOLOCK)                      
    WHERE CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID ;            
   SELECT @VEHICLE_YEAR=YEAR_OF_REG,@MAKE=MAKE,@MODEL=MODEL,@VEHICLE_TYPE=MODEL_TYPE,@VEHICLE_CC=ENG_CAPACITY,
		  @TOTAL_DRIVERS=NO_OF_DRIVERS,@RISK_CURRENCY=3,@VEHICLE_COVERAGE=COVERAGE_TYPE,@CREATED_BY=CREATED_BY,
		  @CHASSIS_NO=CHASSIS_NO,@REGISTRATION_NO=REGISTRATION_NO,@ENGINE_NO=ENGINE_NO FROM QQ_MOTOR_QUOTE_DETAILS            
   WHERE CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID            
  IF @VEHICLE_CC IS NOT NULL          
  BEGIN          
   INSERT INTO [POL_VEHICLES]            
           ([POLICY_ID]            
           ,[POLICY_VERSION_ID]            
           ,[CUSTOMER_ID]            
           ,[VEHICLE_ID]            
           ,[VEHICLE_YEAR]            
           ,[MAKE]            
           ,[MODEL]            
           ,[VEHICLE_CC]            
           ,[VEHICLE_TYPE]            
           ,[IS_ACTIVE]            
           ,[TOTAL_DRIVERS]            
           ,[RISK_CURRENCY]            
           ,[VEHICLE_COVERAGE]            
           ,[VIN]            
           ,INSURED_VEH_NUMBER  
           ,[MOTORCYCLE_TYPE]
           ,[CHASIS_NUMBER] --added by avijit
           ,[REGN_PLATE_NUMBER])--added by avijit            
     VALUES            
           (@POLICY_ID,@POLICY_VERSION_ID,@CUSTOMER_ID, @VEHICLE_ID,@VEHICLE_YEAR,@MAKE,@MODEL, @VEHICLE_CC, @VEHICLE_TYPE, 'Y',            
            @TOTAL_DRIVERS, @RISK_CURRENCY, @VEHICLE_COVERAGE,@ENGINE_NO,@VEHICLE_ID,@VEHICLE_TYPE,@CHASSIS_NO,@REGISTRATION_NO)            
            EXEC Proc_UpdateRisk_Renumeration                
    @CUSTOMER_ID,                   
 @POLICY_ID   ,                  
 @POLICY_VERSION_ID,                
 @VEHICLE_ID,              
 @CREATED_BY            
END          
--TILL HERE            
 END           
              
END 