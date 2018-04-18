IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetMotorVehicleClassRuleOnChange]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetMotorVehicleClassRuleOnChange]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                  
Proc Name       : Proc_SetMotorVehicleClassRuleOnChange      
Created by      : Sumit Chhabra              
Date            : 16/12/2005                                  
Purpose     : Build                                  
Revison History :                                  
Used In  : Wolverine                                                      
              
------------------------------------------------------------                                  
Date        Review By          Comments                                  
-- drop proc Proc_SetMotorVehicleClassRuleOnChange                            
------   ------------       -------------------------*/                                  
CREATE  PROC dbo.Proc_SetMotorVehicleClassRuleOnChange                                  
(                                  
 @CUSTOMER_ID      int,                                  
 @APP_ID   int,                                  
 @APP_VERSION_ID  int,                                  
 @DRIVER_ID       smallint                                   
)                                  
AS                          
                          
declare @NEW_DRIVER_DOB datetime                          
declare @CurrentDate datetime                          
declare @ClassLookupID int                          
declare @ClassValue varchar(5)                          
declare @Age int                          
declare @Vehicle_ID int            
declare @OLD_DRIVER_DOB datetime              
declare @DRIVER_DOB datetime   
declare @DRIVER_DRIV_TYPE varchar(5)     
declare @APP_VEHICLE_PRIN_OCC_ID int   
declare @DRV_TYPE int  
declare @VEH_CODE  int
declare @AppEffDate datetime    
SET @VEH_CODE = 11931  
SET @DRV_TYPE = 11942           
                          
BEGIN    
 SELECT @OLD_DRIVER_DOB=DRIVER_DOB,@VEHICLE_ID=VEHICLE_ID FROM APP_DRIVER_DETAILS WHERE                          
 CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND                        
 DRIVER_ID=@DRIVER_ID            
     
 SELECT @NEW_DRIVER_DOB=MAX(DRIVER_DOB) FROM APP_DRIVER_DETAILS WHERE                          
 CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND                        
 VEHICLE_ID=@VEHICLE_ID  and IS_ACTIVE = 'Y' AND DRIVER_ID<>@DRIVER_ID  
  
 SELECT @DRIVER_DRIV_TYPE=ISNULL(DRIVER_DRIV_TYPE,'') ,@APP_VEHICLE_PRIN_OCC_ID=APP_VEHICLE_PRIN_OCC_ID  
 FROM APP_DRIVER_DETAILS WHERE      
 CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND         
 DRIVER_ID = @DRIVER_ID and IS_ACTIVE = 'Y'                       

  --Added by Manoj Rathore on 12th May 2009 Itrack # 5120
 SELECT @AppEffDate=ISNULL(APP_EFFECTIVE_DATE,'')  FROM APP_LIST WHERE                      
 CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID and IS_ACTIVE = 'Y'                     
 
    
 IF(@DRIVER_DRIV_TYPE != @DRV_TYPE AND  @APP_VEHICLE_PRIN_OCC_ID !=@VEH_CODE )    
 BEGIN    
   IF (@OLD_DRIVER_DOB>=@NEW_DRIVER_DOB)                          
   BEGIN                  
   SELECT @DRIVER_DOB=MAX(DRIVER_DOB) FROM APP_DRIVER_DETAILS WHERE                          
   CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND                        
   VEHICLE_ID=@VEHICLE_ID  and IS_ACTIVE = 'Y' AND DRIVER_ID<>@DRIVER_ID  
          
   --SET @AGE = DATEDIFF(YY,@DRIVER_DOB,GETDATE())                                         
   SET @AGE = dbo.Getage(@DRIVER_DOB,@AppEffDate) 
   IF @AGE<23                             
   SET @CLASSVALUE='B'                          
   ELSE IF (@AGE>=23 AND @AGE<30)                             
   SET @CLASSVALUE='A'                          
   ELSE IF(@AGE>=30)                             
   SET @CLASSVALUE='C'   
          
   SELECT @CLASSLOOKUPID=V.LOOKUP_UNIQUE_ID FROM MNT_LOOKUP_VALUES V JOIN MNT_LOOKUP_TABLES T                      
   ON  V.LOOKUP_ID=T.LOOKUP_ID                       
   WHERE T.LOOKUP_NAME='MCCLAS' AND V.LOOKUP_VALUE_CODE=@CLASSVALUE AND V.IS_ACTIVE='Y'                    
                     
   UPDATE APP_VEHICLES SET APP_VEHICLE_CLASS=@CLASSLOOKUPID WHERE                           
   CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND                          
   VEHICLE_ID=@VEHICLE_ID                          
   END   
 END             
END              
            
          
        
      
      
    
  
  


GO

