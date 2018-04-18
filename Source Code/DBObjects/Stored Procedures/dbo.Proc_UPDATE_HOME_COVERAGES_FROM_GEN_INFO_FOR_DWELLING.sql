IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_HOME_COVERAGES_FROM_GEN_INFO_FOR_DWELLING]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_HOME_COVERAGES_FROM_GEN_INFO_FOR_DWELLING]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                    
Proc Name       : dbo.Proc_UPDATE_HOME_COVERAGES_FROM_GEN_INFO                                    
Created by      : Pradeep                                    
Date            : 02/03/2006                               
Purpose       :  Updates coverages based on App general information  
Revison History :                                    
Used In  : Wolverine                                    
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------*/                                    
CREATE           PROC Dbo.Proc_UPDATE_HOME_COVERAGES_FROM_GEN_INFO_FOR_DWELLING                                  
(                                    
 @CUSTOMER_ID     int,                                    
 @APP_ID     int,                                    
 @APP_VERSION_ID     smallint,  
 @DWELLING_ID Int                                    
             
)                                    
AS                                    
                                    
BEGIN    
   
 DECLARE   @NON_SMOKER_CREDIT NChar(1)  
 DECLARE @HO220 Int  
 DECLARE @STATE_ID Int  
 DECLARE @LOB_ID Int   
  
 SET  @HO220 = 240  
       
SELECT @STATE_ID = STATE_ID,  
 @LOB_ID = APP_LOB  
FROM APP_LIST  
WHERE CUSTOMER_ID =  @CUSTOMER_ID AND  
  APP_ID = @APP_ID AND  
  APP_VERSION_ID = @APP_VERSION_ID   
--For Indiana state   
IF ( @LOB_ID = 1 AND @STATE_ID = 14 )  
BEGIN  
  
 SELECT @NON_SMOKER_CREDIT = ISNULL(NON_SMOKER_CREDIT,'0')  
 FROM APP_HOME_OWNER_GEN_INFO  
 WHERE CUSTOMER_ID =  @CUSTOMER_ID AND  
 APP_ID = @APP_ID AND  
 APP_VERSION_ID = @APP_VERSION_ID   
  
IF ( @NON_SMOKER_CREDIT = '0' OR @NON_SMOKER_CREDIT = '' )  
BEGIN  
 IF EXISTS  
 (  
  SELECT * FROM APP_DWELLING_ENDORSEMENTS  
  WHERE   CUSTOMER_ID =  @CUSTOMER_ID AND  
  APP_ID = @APP_ID AND  
  APP_VERSION_ID = @APP_VERSION_ID AND  
  ENDORSEMENT_ID = @HO220 AND  
  DWELLING_ID = @DWELLING_ID  
 )  
  
 DELETE FROM APP_DWELLING_ENDORSEMENTS  
 WHERE   CUSTOMER_ID =  @CUSTOMER_ID AND  
 APP_ID = @APP_ID AND  
 APP_VERSION_ID = @APP_VERSION_ID AND  
 ENDORSEMENT_ID = @HO220 AND  
 DWELLING_ID = @DWELLING_ID  
   
 RETURN  
  
END  
        --Perform update  
 IF ( @NON_SMOKER_CREDIT = '1' )  
 BEGIN  
  IF NOT EXISTS  
  (  
   SELECT * FROM   
   APP_DWELLING_ENDORSEMENTS  
   WHERE CUSTOMER_ID =  @CUSTOMER_ID AND  
    APP_ID = @APP_ID AND  
    APP_VERSION_ID = @APP_VERSION_ID AND  
    DWELLING_ID = @DWELLING_ID AND  
    ENDORSEMENT_ID = @HO220  
  )  
  BEGIN  
   INSERT INTO APP_DWELLING_ENDORSEMENTS    
	(	
		CUSTOMER_ID,
		APP_ID,
		APP_VERSION_ID,
		DWELLING_ID,
		ENDORSEMENT_ID,
		REMARKS,
		DWELLING_ENDORSEMENT_ID
	) 
      SELECT @CUSTOMER_ID,    
       @APP_ID,    
       @APP_VERSION_ID,    
       @DWELLING_ID,     
       @HO220,    
       NULL,     
      (    
       SELECT ISNULL(MAX(DWELLING_ENDORSEMENT_ID),0) + 1    
       FROM APP_DWELLING_ENDORSEMENTS    
       WHERE CUSTOMER_ID = @CUSTOMER_ID AND     
        APP_ID = @APP_ID AND     
        APP_VERSION_ID = @APP_VERSION_ID     
        AND DWELLING_ID = @DWELLING_ID   
      )    
  END   
 END  
    
   
END --  OF LOB AND STATE  
                 
END                 



GO

