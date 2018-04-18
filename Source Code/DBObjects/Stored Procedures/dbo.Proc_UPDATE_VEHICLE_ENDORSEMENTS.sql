IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_VEHICLE_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_VEHICLE_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_UPDATE_VEHICLE_ENDORSEMENTS      
      
/*----------------------------------------------------------                            
Proc Name       : dbo.Proc_UPDATE_VEHICLE_ENDORSEMENTS                            
Created by      : Pradeep                            
Date            : 12/23/2005                            
Purpose      :Inserts linked endorsemnts for this coverage                          
Revison History :                            
Used In  : Wolverine                            
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
CREATE           PROC dbo.Proc_UPDATE_VEHICLE_ENDORSEMENTS        
(                            
 @CUSTOMER_ID     int,                            
 @APP_ID     int,                            
 @APP_VERSION_ID     smallint,                            
 @VEHICLE_ID smallint           
)         
        
AS         
        
BEGIN        
   
   return
  DECLARE @STATEID SmallInt                                
  DECLARE @LOBID NVarCHar(5)             
        
  DECLARE @ENDORSEMENT_ID Int                                                 
  DECLARE @VEHICLE_ENDORSEMENT_ID int                              
                        
                   
        
    SELECT @STATEID = STATE_ID,                                
    @LOBID = APP_LOB                                
  FROM APP_LIST                                
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                
    APP_ID = @APP_ID AND                                
    APP_VERSION_ID = @APP_VERSION_ID           
        
    
/*         
         
  SELECT  @ENDORSEMENT_ID = ENDORSMENT_ID                        
  FROM MNT_ENDORSMENT_DETAILS MED                        
  WHERE SELECT_COVERAGE = @COVERAGE_CODE_ID                        
  AND STATE_ID = @STATEID AND                        
   LOB_ID = @LOBID AND            
   ENDORS_ASSOC_COVERAGE = 'Y' AND            
   IS_ACTIVE='Y'                      
                      
 --print(@STATEID)                      
 --print(@LOBID)                      
                       
 --print (@COVERAGE_CODE_ID)                         
 --print(ISNULL(@ENDORSEMENT_ID,0))                      
                      
          --Linked endorsements---------------------------------------------------------------------------------        
  IF ( @ENDORSEMENT_ID IS NOT NULL )                        
  BEGIN            
    IF NOT EXISTS                
    (                
    SELECT * FROM APP_VEHICLE_ENDORSEMENTS                
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                
    APP_ID = @APP_ID AND                                
    APP_VERSION_ID = @APP_VERSION_ID AND                        
    VEHICLE_ID = @VEHICLE_ID  AND                
    ENDORSEMENT_ID =  @ENDORSEMENT_ID                       
    )                
   BEGIN              
    SELECT @VEHICLE_ENDORSEMENT_ID = ISNULL(MAX(VEHICLE_ENDORSEMENT_ID),0) + 1                        
     FROM APP_VEHICLE_ENDORSEMENTS                        
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                
       APP_ID = @APP_ID AND                                
       APP_VERSION_ID = @APP_VERSION_ID AND                        
       VEHICLE_ID = @VEHICLE_ID                   
                    
     INSERT INTO APP_VEHICLE_ENDORSEMENTS                        
     (                        
       CUSTOMER_ID,        
       APP_ID,                        
       APP_VERSION_ID,                        
       VEHICLE_ID,                        
       ENDORSEMENT_ID,                     
       VEHICLE_ENDORSEMENT_ID                        
     )              
     VALUES                        
     (                        
       @CUSTOMER_ID,                        
       @APP_ID,                        
       @APP_VERSION_ID,                        
       @VEHICLE_ID,                        
       @ENDORSEMENT_ID,                        
       @VEHICLE_ENDORSEMENT_ID                        
     )                        
      END                
      --End of linked endorsements ------------------        
        
 END        
  */    
-- 	IF EXISTS      
-- 	(      
-- 			SELECT * FROM APP_VEHICLE_COVERAGES     WITH(NOLOCK) 
-- 			WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
-- 			APP_ID = @APP_ID AND      
-- 			APP_VERSION_ID = @APP_VERSION_ID AND      
-- 			VEHICLE_ID = @VEHICLE_ID AND      
-- 			COVERAGE_CODE_ID in(14,34)      
-- 	)      
-- 	BEGIN      
-- 	--Coordination of Benefits $300 Deductible (A-91)        
-- 			EXEC Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID       
-- 			@CUSTOMER_ID,              
-- 			@APP_ID,              
-- 			@APP_VERSION_ID,              
-- 			15,              
-- 			@VEHICLE_ID           
-- 			IF @@ERROR <> 0              
-- 			BEGIN              
-- 			RETURN              
-- 			END      
-- 	
-- 	END      
-- 	ELSE      
-- 	BEGIN      
-- 			EXEC Proc_Delete_APP_VEHICLE_ENDORSEMENT_BY_ID      
-- 			@CUSTOMER_ID,--@CUSTOMER_ID int,              
-- 			@APP_ID,--@APP_ID int,              
-- 			@APP_VERSION_ID,--@APP_VERSION_ID smallint,               
-- 			15,--@ENDORSEMENT_ID smallint,          
-- 			@VEHICLE_ID--@VEHICLE_ID smallint              
-- 	END       
    
   --Insert Endorsement linked to PIP--------------------------------------------------------        
  IF (@STATEID = 22 AND @LOBID = 2)        
 BEGIN        
  --Part A – Personal Injury Protection         
  IF EXISTS      
  (      
 SELECT * FROM APP_VEHICLE_COVERAGEs      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
  APP_ID = @APP_ID AND      
  APP_VERSION_ID = @APP_VERSION_ID AND      
  VEHICLE_ID = @VEHICLE_ID AND      
  COVERAGE_CODE_ID = 116 AND      
  LIMIT1_AMOUNT_TEXT = 'Excess Medical'      
      
   )      
  BEGIN      
  --Coordination of Benefits $300 Deductible (A-91)        
     EXEC Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID       
     @CUSTOMER_ID,              
             @APP_ID,              
            @APP_VERSION_ID,              
            42,              
            @VEHICLE_ID           
             
      IF @@ERROR <> 0              
        BEGIN              
          RETURN              
        END      
                
    END      
    ELSE      
    BEGIN      
  EXEC Proc_Delete_APP_VEHICLE_ENDORSEMENT_BY_ID      
   @CUSTOMER_ID,--@CUSTOMER_ID int,              
   @APP_ID,--@APP_ID int,              
   @APP_VERSION_ID,--@APP_VERSION_ID smallint,               
   42,--@ENDORSEMENT_ID smallint,          
   @VEHICLE_ID--@VEHICLE_ID smallint              
       
       
    END       
        
          
      
 END        
 -------------------------------------------------------------------------------------------------        
            
END                           
      
    
  







GO

