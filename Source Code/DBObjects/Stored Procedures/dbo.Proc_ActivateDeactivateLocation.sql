IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateLocation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------      
Proc Name       : Proc_ActivateDeactivateLocation  
Created by      : Vijay      
Date            : 25 Mar,2005      
Purpose         : To Activate/Deactivate the record in Customer table      
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
--DROP  proc dbo.Proc_ActivateDeactivateLocation 
CREATE  PROC [dbo].[Proc_ActivateDeactivateLocation]  
(      
 @CUSTOMER_ID Int,  
 @APP_ID Int,  
 @APP_VERSION_ID SmallInt,  
  @LOCATION_ID  SmallInt,      
  @IS_ACTIVE   NChar(1)      
)      
AS      
BEGIN

IF @IS_ACTIVE = 'Y'  
BEGIN
IF EXISTS(SELECT LOCATION_ID FROM  APP_LOCATIONS 
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE = 'Y')    

	 RETURN 0
END
 
 IF @IS_ACTIVE = 'N'  
 BEGIN  
     
  IF EXISTS  
  (  
   SELECT CUSTOMER_ID FROM APP_DWELLINGS_INFO  
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
         APP_ID =  @APP_ID AND  
         APP_VERSION_ID = @APP_VERSION_ID AND  
         LOCATION_ID =  @LOCATION_ID and ISNULL(IS_ACTIVE,'Y')='Y'      
  )    
  BEGIN  
   RETURN -1  
  END  
   
  IF EXISTS  
  (  
   SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SUB_INSU  
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
         APP_ID =  @APP_ID AND  
         APP_VERSION_ID = @APP_VERSION_ID AND  
         LOCATION_ID =  @LOCATION_ID and ISNULL(IS_ACTIVE,'Y')='Y'       
  )    
  BEGIN  
   RETURN -2  
  END   
   
 END  
  
 UPDATE APP_LOCATIONS  
 SET       
    Is_Active  = @IS_ACTIVE     
 WHERE      
      LOCATION_ID    = @LOCATION_ID AND  
  CUSTOMER_ID =  @CUSTOMER_ID AND  
  APP_ID = @APP_ID AND  
  LOCATION_ID = @LOCATION_ID     
  
 RETURN 1  
      
END  


GO

