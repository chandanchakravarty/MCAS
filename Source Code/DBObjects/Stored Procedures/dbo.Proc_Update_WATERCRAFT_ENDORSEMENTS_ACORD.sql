IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_WATERCRAFT_ENDORSEMENTS_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_WATERCRAFT_ENDORSEMENTS_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name       : dbo.Proc_Update_WATERCRAFT_ENDORSEMENTS_ACORD              
Created by      : Pradeep              
Date            : 12/27/2005              
Purpose      : Saves records in Watercraft Coverages and inserts            
      dependent endorsements in APP_WATERCRAFT_ENDORSEMENTS from   
  Quick quote             
Revison History :              
Used In  : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
-- drop proc Proc_Update_WATERCRAFT_ENDORSEMENTS_ACORD  
CREATE           PROC Dbo.Proc_Update_WATERCRAFT_ENDORSEMENTS_ACORD              
(              
  @CUSTOMER_ID     int,              
  @APP_ID     int,              
  @APP_VERSION_ID     smallint,              
  @VEHICLE_ID smallint,              
  @COVERAGE_CODE_ID int  
)  
AS  
BEGIN  
  --Insert dependent Endorsements for this coverage                  
 DECLARE @ENDORSEMENT_ID Int                  
 DECLARE @STATEID SmallInt                          
 DECLARE @LOBID NVarCHar(5)                          
 DECLARE @VEHICLE_ENDORSEMENT_ID int                        
 --N for New Business, R for renewal                        
 --DECLARE @APP_TYPE Char(1)                        
                         
 --SET @APP_TYPE = 'N'                        
                           
 SELECT @STATEID = STATE_ID,                          
 @LOBID = APP_LOB                          
 FROM APP_LIST                          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                          
  APP_ID = @APP_ID AND                          
  APP_VERSION_ID = @APP_VERSION_ID                          
    
 SET @LOBID = 4        
           
 SELECT  @ENDORSEMENT_ID = ENDORSMENT_ID                  
 FROM MNT_ENDORSMENT_DETAILS MED                  
 WHERE SELECT_COVERAGE = @COVERAGE_CODE_ID                  
  AND STATE_ID = @STATEID AND                  
  LOB_ID = @LOBID                  
                
 IF ( @ENDORSEMENT_ID IS NOT NULL )                  
 BEGIN                  
  SELECT @VEHICLE_ENDORSEMENT_ID = ISNULL(MAX(VEHICLE_ENDORSEMENT_ID),0) + 1                  
  FROM APP_WATERCRAFT_ENDORSEMENTS                  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                          
   APP_ID = @APP_ID AND                          
   APP_VERSION_ID = @APP_VERSION_ID AND                  
   BOAT_ID = @VEHICLE_ID                   
  IF NOT EXISTS (SELECT  ENDORSEMENT_ID FROM APP_WATERCRAFT_ENDORSEMENTS  
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND                          
    APP_ID = @APP_ID AND                          
    APP_VERSION_ID = @APP_VERSION_ID             
    AND ENDORSEMENT_ID = @ENDORSEMENT_ID
    AND BOAT_ID=@VEHICLE_ID )  
  BEGIN  
   INSERT INTO APP_WATERCRAFT_ENDORSEMENTS                  
   (                  
    CUSTOMER_ID,                  
    APP_ID,                  
    APP_VERSION_ID,                  
    BOAT_ID,                  
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
           
 END              
END  
  
  
  
  
  
  



GO

