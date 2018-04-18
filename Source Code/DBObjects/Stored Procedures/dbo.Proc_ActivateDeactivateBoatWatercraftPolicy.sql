IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateBoatWatercraftPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateBoatWatercraftPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                  
Proc Name       : dbo.Proc_ActivateDeactivateWatercraft          
Created by      : -            
Date            : -                            
Purpose         :Activate/ Deactivate Watercraft         
Revison History :                  
Used In         : Wolverine                  
------------------------------------------------------------            
Modified By  : Swastika    
Date   : 30th Mar'06    
Purpose  : To check the boat_id is being used in POL_WATERCRAFT_EQUIP_DETAILLS or POL_WATERCRAFT_TRAILER_INFO,    
    then we can't deactivate the Boat.    
            
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/    
--drop proc Proc_ActivateDeactivateBoatWatercraftPolicy    
CREATE PROC DBO.Proc_ActivateDeactivateBoatWatercraftPolicy                    
    
(                    
    
@CUSTOMER_ID     INT,                    
@POLICY_ID     INT,                    
@POLICY_VERSION_ID     SMALLINT,                    
@BOAT_ID     SMALLINT,            
@IS_ACTIVE NCHAR(2),    
@RESULT  INT  =NULL OUTPUT            
)                    
    
AS                    
 
BEGIN                    
IF(@IS_ACTIVE='N')    
 BEGIN     
  IF EXISTS( SELECT  ASSOCIATED_BOAT FROM POL_WATERCRAFT_TRAILER_INFO  WHERE         
    CUSTOMER_ID = @CUSTOMER_ID AND  POLICY_ID =  @POLICY_ID AND POLICY_VERSION_ID =  @POLICY_VERSION_ID AND         
    ASSOCIATED_BOAT= @BOAT_ID AND ISNULL(IS_ACTIVE,'N') = 'Y')--IS_ACTIVE added by Charles on 17-Nov-09 for Itrack 6600     
    OR EXISTS (SELECT  ASSOCIATED_BOAT FROM POL_WATERCRAFT_EQUIP_DETAILLS  WHERE         
    CUSTOMER_ID = @CUSTOMER_ID AND  POLICY_ID =  @POLICY_ID AND POLICY_VERSION_ID =  @POLICY_VERSION_ID AND         
    ASSOCIATED_BOAT= @BOAT_ID AND ISNULL(IS_ACTIVE,'N') = 'Y')--IS_ACTIVE added by Charles on 23-Oct-09 for Itrack 6600     
    BEGIN     
     SET @RESULT=-2    
     RETURN @RESULT    
    END     
  ELSE    
   BEGIN          
     
    UPDATE POL_WATERCRAFT_INFO     
     SET IS_ACTIVE=@IS_ACTIVE     
     WHERE CUSTOMER_ID=@CUSTOMER_ID AND             
     POLICY_ID=@POLICY_ID AND            
     POLICY_VERSION_ID=@POLICY_VERSION_ID AND            
     BOAT_ID=@BOAT_ID       
      
    UPDATE  POL_WATERCRAFT_DRIVER_DETAILS    
     SET VEHICLE_ID=NULL    
     WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID     
     AND VEHICLE_ID = @BOAT_ID         
     SET @RESULT=1    
     RETURN @RESULT   
   END    
 END    
ELSE    
BEGIN     
  
 UPDATE POL_WATERCRAFT_INFO     
 SET IS_ACTIVE=@IS_ACTIVE     
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND          
      BOAT_ID=@BOAT_ID        
 SET @RESULT=1    
 RETURN @RESULT        
     
     
END    
END       
    
  

GO

