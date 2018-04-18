IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNewInsuredVehicleNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNewInsuredVehicleNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetNewInsuredVehicleNumber  
Created by           : Nidhi  
Date                    : 28/04/2005  
Purpose               : To get the new insured vehicle number  
Revison History :  
Used In                :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--DROP proc Proc_GetNewInsuredVehicleNumber
CREATE PROC Dbo.Proc_GetNewInsuredVehicleNumber  
(  
@FROM VARCHAR(5) = null,  
@CUSTOMER_ID INT,  
@APP_ID INT=NULL,  
@APP_VERSION_ID INT=NULL  
)  
AS  
  
  
--IF @FROM<>'CLT'  
  
 IF  @FROM='UMB'  
   
  BEGIN  
    
  SELECT    (isnull(MAX(INSURED_VEH_NUMBER),0)) +1 as INSUREDVEHICLENUMBER  
  FROM         APP_UMBRELLA_VEHICLE_INFO   
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID  
  END  
  
  
 ELSE   
  BEGIN  
    
  SELECT    (isnull(MAX(INSURED_VEH_NUMBER),0)) +1 as INSUREDVEHICLENUMBER  
  FROM         APP_VEHICLES   
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID  
  END  
  
/*ELSE  
  
 BEGIN  
 SELECT    (isnull(MAX(INSURED_VEH_NUMBER),0)) +1 as INSUREDVEHICLENUMBER  
 FROM         CLT_CUSTOMER_VEHICLES  
 WHERE CUSTOMER_ID=@CUSTOMER_ID   
 END  */



GO

