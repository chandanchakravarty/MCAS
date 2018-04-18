IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_UMBRELLA_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_UMBRELLA_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                            
Proc Name       : dbo.Proc_SAVE_UMBRELLAL_COVERAGES                            
Created by      : Pravesh                            
Date            : 11/10/2006                            
Purpose     :Inserts a record in APP_UMBRELLA_COVERAGES
Revison History :                            
Used In  : Wolverine                
Proc_SAVE_UMBRELLA_COVERAGES 943,42,1,-1,NULL,'UBPULP'            
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
CREATE PROC dbo.Proc_SAVE_UMBRELLA_COVERAGES                            
(                            
 @CUSTOMER_ID     int,                            
 @APP_ID     int,                            
 @APP_VERSION_ID     smallint,                            
 @COVERAGE_ID int,                            
 @COVERAGE_CODE_ID int=NULL,
 @COVERAGE_CODE VARCHAR(20)=NULL,
 @CREATED_BY   INT=NULL,
 @CREATED_DATETIME DATETIME=NULL,
 @MODIFIED_BY     INT=NULL,
 @LAST_UPDATED_DATETIME DATETIME=NULL
)                            
AS                            
                            
DECLARE @COVERAGE_ID_MAX smallint               
                     
BEGIN                        
     
                             
  DECLARE @STATEID SmallInt                                
  DECLARE @LOBID NVarCHar(5)             
            
    SELECT @STATEID = STATE_ID,                                
    @LOBID = APP_LOB                                
    FROM APP_LIST                                
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                
    APP_ID = @APP_ID AND                                
    APP_VERSION_ID = @APP_VERSION_ID  

	IF ISNULL(@COVERAGE_CODE,'') <> '' 
	BEGIN
                 PRINT @COVERAGE_CODE
        	EXEC @COVERAGE_CODE_ID =  Proc_GetCOVERAGE_ID @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@COVERAGE_CODE
	END 
	IF(@COVERAGE_CODE_ID = 0)
	BEGIN
        	RAISERROR('Coverage Not found',16,1)
	END

    PRINT @COVERAGE_CODE_ID
  IF NOT EXISTS                            
  (                            
    SELECT * FROM APP_UMBRELLA_COVERAGES                            
    where CUSTOMER_ID = @CUSTOMER_ID and                             
     APP_ID=@APP_ID and           
     APP_VERSION_ID = @APP_VERSION_ID  AND                            
     COVERAGE_CODE_ID = @COVERAGE_CODE_ID                            
  )                            
                              
  BEGIN                 
             
   SELECT  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1                         
   FROM APP_UMBRELLA_COVERAGES                            
   where CUSTOMER_ID = @CUSTOMER_ID and                             
    APP_ID=@APP_ID and                             
    APP_VERSION_ID = @APP_VERSION_ID                             
                         
    INSERT INTO APP_UMBRELLA_COVERAGES                            
    (                            
      CUSTOMER_ID,                            
      APP_ID,                            
      APP_VERSION_ID,                            
      COVERAGE_ID,                            
      COVERAGE_CODE_ID,                            
      CREATED_BY,
      CREATED_DATETIME
    )                            
    VALUES                            
    (                            
      @CUSTOMER_ID,                            
      @APP_ID,                            
      @APP_VERSION_ID,                         
      @COVERAGE_ID_MAX,                            
      @COVERAGE_CODE_ID,
      @CREATED_BY,
      @CREATED_DATETIME
                            
    )                        
                         
  END             
                            
           
  ELSE --End of Insert                           
                            
    BEGIN        
                                            
  --Update                            
  UPDATE APP_UMBRELLA_COVERAGES                            
  SET APP_VERSION_ID = @APP_VERSION_ID,                             
       MODIFIED_BY = @MODIFIED_BY,
    LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                            
   APP_ID = @APP_ID AND                            
   APP_VERSION_ID = @APP_VERSION_ID AND                            
   COVERAGE_CODE_ID = @COVERAGE_CODE_ID 
           
 END        
      

if(@coverage_code_id=1048 or @coverage_code_id=1037) --Exclusion - Designated Recreational Motor Vehicle Endorsement
	update app_umbrella_recreational_vehicles set is_boat_excluded=10963 
		where  CUSTOMER_ID = @CUSTOMER_ID AND                            
			   APP_ID = @APP_ID AND                            
			   APP_VERSION_ID = @APP_VERSION_ID 
      
IF @@ERROR <> 0      
BEGIN      
 RAISERROR ('Unable to add Coverages.', 16, 1)      
      
END           
--************************************************************      
END         
         
           
 


GO

