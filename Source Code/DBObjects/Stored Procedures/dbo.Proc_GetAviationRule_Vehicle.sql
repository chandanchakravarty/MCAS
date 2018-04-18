IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAviationRule_Vehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAviationRule_Vehicle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                                                                                  
Proc Name                : Dbo.Proc_GetAviationRule_Vehicle                                                                                
Created by               : Pravesh K Chandel
Date                     : 15 Jan 2010
Purpose                  : To get the aviation Vehicle detail for Policy                                                  
Revison History          :              
------------------------------------------------------------                                                                                  
Date     Review By          Comments                                                                                  
------   ------------       -------------------------*/                                                                                  
-- drop proc dbo.Proc_GetAviationRule_Vehicle                                                                         
create proc Proc_GetAviationRule_Vehicle                                                                         
( 
 @CUSTOMERID    int,                                                                                  
 @APPID    int,                                                                                  
 @APPVERSIONID   int,                                                                        
 @VEHICLEID int ,
 @DESC   nvarchar(10)
)                                                                                  
AS                                                                                      
BEGIN                                                                               
-- Vehicle Info                                                           
 DECLARE @VEHICLE_YEAR VARCHAR(4)                                                          
 DECLARE @MAKE NVARCHAR(75)                                                          
 DECLARE @MODEL NVARCHAR(75)                                                          
 DECLARE @VIN NVARCHAR(75)                                                    
 DECLARE @VEHICLE_USE NVARCHAR(5)                                                    
  DECLARE @MODEL_NAME CHAR         
 DECLARE @MAKE_NAME CHAR                                                
 DECLARE @USE_VEHICLE NVARCHAR(5)                                     
 DECLARE @APP_EFFECTIVE_DATE DATETIME,@APP_INCEPTION_DATE DATETIME
            
 SELECT @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE ,@APP_INCEPTION_DATE=APP_INCEPTION_DATE            
  FROM APP_LIST with(nolock) WHERE  CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID                                    
            
IF EXISTS (SELECT CUSTOMER_ID FROM APP_AVIATION_VEHICLES  with(nolock) WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID)                                                          
BEGIN                                                     
 SELECT @VEHICLE_YEAR=ISNULL(VEHICLE_YEAR,''),
 @MAKE=ISNULL(UPPER(MAKE),''),
 @MODEL=ISNULL(UPPER(MODEL),''),            
 @USE_VEHICLE=ISNULL(USE_VEHICLE,'')
                                                 
FROM APP_AVIATION_VEHICLES                                 
WHERE CUSTOMER_ID=@CUSTOMERID 
AND APP_ID=@APPID  
AND  APP_VERSION_ID=@APPVERSIONID 
AND VEHICLE_ID=@VEHICLEID                    
END                                                                                               
------------------------------------------------------------            
                         
 SELECT                        
 @VEHICLE_YEAR as VEHICLE_YEAR,                                                          
 @MAKE as MAKE,                          
 @MODEL as MODEL,                                                      
 @USE_VEHICLE as USE_VEHICLE, 
 'N' as CRAFT_CATEGORY_OTHER ,
'Y' as OTHER_CRAFT

 END 


GO

