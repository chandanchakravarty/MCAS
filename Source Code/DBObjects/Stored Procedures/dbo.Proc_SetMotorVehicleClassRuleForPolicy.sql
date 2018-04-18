IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetMotorVehicleClassRuleForPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetMotorVehicleClassRuleForPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                              
Proc Name       : Proc_SetMotorVehicleClassRule          
Created by      : shafi          
Date            : 30/01/2006                             
Purpose     : Build                              
Revison History :                              
Used In  : Wolverine                                                  
          
------------------------------------------------------------                              
Date        Review By          Comments                              
drop proc Proc_SetMotorVehicleClassRuleForPolicy                        
------   ------------       -------------------------*/                              
    
CREATE  PROC dbo.Proc_SetMotorVehicleClassRuleForPolicy      
(                              
 @CUSTOMER_ID      int,                              
 @POLICY_ID   int,                              
 @POLICY_VERSION_ID  int,                              
 @DRIVER_ID       smallint ,                              
 @Vehicle_ID int,          
 @DRIVER_DOB datetime          
)                              
AS                      
                      
declare @NEW_DRIVER_DOB datetime                      
declare @CurrentDate datetime                      
declare @ClassLookupID int                      
declare @ClassValue varchar(5)                      
declare @Age int   
declare @DRIVER_DRIV_TYPE varchar(5)     
declare @APP_VEHICLE_PRIN_OCC_ID int   
declare @DRV_TYPE int  
declare @VEH_CODE  int
declare @POL_EFFECTIVE_DATE datetime    
SET @VEH_CODE = 11931  
SET @DRV_TYPE = 11942                                    
        
BEGIN                              
 SELECT @NEW_DRIVER_DOB=MAX(DRIVER_DOB) FROM POL_DRIVER_DETAILS WHERE                      
 CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND                    
 VEHICLE_ID=@VEHICLE_ID  AND IS_ACTIVE = 'Y'    
         
          
 SELECT @DRIVER_DRIV_TYPE=ISNULL(DRIVER_DRIV_TYPE,'') ,@APP_VEHICLE_PRIN_OCC_ID=APP_VEHICLE_PRIN_OCC_ID FROM POL_DRIVER_DETAILS WHERE                      
 CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND     
 DRIVER_ID=@DRIVER_ID  and IS_ACTIVE = 'Y'                     

  --Added by  Manoj Rathore on 11th May 2009 Itrack # 5120                      
 SELECT @POL_EFFECTIVE_DATE=ISNULL(APP_EFFECTIVE_DATE,'')  
 FROM POL_CUSTOMER_POLICY_LIST WHERE        
 CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND  POLICY_VERSION_ID = @POLICY_VERSION_ID and IS_ACTIVE = 'Y'   
    
 
   IF(@DRIVER_DRIV_TYPE != @DRV_TYPE AND  @APP_VEHICLE_PRIN_OCC_ID !=@VEH_CODE )    
   BEGIN                 
   IF (@DRIVER_DOB>=@NEW_DRIVER_DOB)                      
   BEGIN                      
    --SET @AGE = DATEDIFF(YY,@DRIVER_DOB,GETDATE()) 
    SET @AGE = dbo.GETAGE(@DRIVER_DOB,@POL_EFFECTIVE_DATE)                                   

    IF @AGE<23                         
    SET @CLASSVALUE='B'                      
    ELSE IF (@AGE>=23 AND @AGE<30)                         
    SET @CLASSVALUE='A'                      
    ELSE IF(@AGE>=30)                         
    SET @CLASSVALUE='C'                      
            
    SELECT @CLASSLOOKUPID=V.LOOKUP_UNIQUE_ID FROM MNT_LOOKUP_VALUES V JOIN MNT_LOOKUP_TABLES T                  
    ON  V.LOOKUP_ID=T.LOOKUP_ID                   
    WHERE T.LOOKUP_NAME='MCCLAS' AND V.LOOKUP_VALUE_CODE=@CLASSVALUE AND V.IS_ACTIVE='Y'                
      
    UPDATE POL_VEHICLES SET APP_VEHICLE_CLASS=@CLASSLOOKUPID WHERE                       
    CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND                      
    VEHICLE_ID=@VEHICLE_ID                      
   END   
  
   END         
END          
        
      
    
  
  


GO

