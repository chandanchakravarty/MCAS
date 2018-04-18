IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_POL_UMBRELLA_DEFAULT_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_POL_UMBRELLA_DEFAULT_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_SAVE_POL_VEHICLE_DEFAULT_COVERAGES            
Created by      : PRAVESH            
Date            : 16/10/06    
Purpose     :Inserts a record in POL_UMBRELLA_COVERAGES            
Revison History :            
Used In  : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------
drop proc dbo.Proc_SAVE_POL_UMBRELLA_DEFAULT_COVERAGES   
*/            
CREATE PROCEDURE dbo.Proc_SAVE_POL_UMBRELLA_DEFAULT_COVERAGES      
(            
 @CUSTOMER_ID     int,            
 @POLICY_ID     int,            
 @POLICY_VERSION_ID     smallint,            
 @COVERAGE_ID int,            
 @COVERAGE_CODE NVarChar(10)=null,   
 @COVERAGE_CODE_ID int=null,           
 @CREATED_BY INT=NULL,
 @CREATED_DATETIME  DATETIME=NULL,
 @MODIFIED_BY         INT=NULL,
 @LAST_UPDATED_DATETIME  DATETIME=NULL
)            
AS            
           
DECLARE @COVERAGE_ID_MAX smallint            
           
BEGIN            
 -- Get  the Coverage ID            
if (@COVERAGE_CODE_ID is null)
  EXECUTE @COVERAGE_CODE_ID =  Proc_GetCOVERAGE_IDForPolicy @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@COVERAGE_CODE            
 
 IF ( @COVERAGE_CODE_ID = 0 )            
 BEGIN            
  --RAISERROR ('Coverage Code not found in MNT_COVERAGES. Could not insert into Coverages', 16, 1)            
  RETURN -1            
 END            
       
     
 IF NOT EXISTS            
 (             
  SELECT * FROM POL_UMBRELLA_COVERAGES            
  where CUSTOMER_ID = @CUSTOMER_ID and             
   POL_ID=@POLICY_ID and             
   POL_VERSION_ID = @POLICY_VERSION_ID AND            
   COVERAGE_CODE_ID = @COVERAGE_CODE_ID            
 )            
 BEGIN            
  select  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1 from POL_UMBRELLA_COVERAGES            
  where CUSTOMER_ID = @CUSTOMER_ID and             
   POL_ID=@POLICY_ID and             
   POL_VERSION_ID = @POLICY_VERSION_ID             
  INSERT INTO POL_UMBRELLA_COVERAGES            
  (            
   CUSTOMER_ID,            
   POL_ID,            
   POL_VERSION_ID,            
   COVERAGE_ID,            
   COVERAGE_CODE_ID,
   CREATED_BY,
   CREATED_DATETIME
  )            
  VALUES            
  (            
   @CUSTOMER_ID,            
   @POLICY_ID,            
   @POLICY_VERSION_ID,            
   @COVERAGE_ID_MAX,            
   @COVERAGE_CODE_ID,            
   @CREATED_BY,
   @CREATED_DATETIME
  )            
 --RETURN @COVERAGE_CODE_ID           
 END           

if(@coverage_code_id=1048 or @coverage_code_id=1037) --Exclusion - Designated Recreational Motor Vehicle Endorsement
	update POL_umbrella_recreational_vehicles set is_boat_excluded=10963 
		where  CUSTOMER_ID = @CUSTOMER_ID AND                            
			   POLICY_ID = @POLICY_ID AND                            
			   POLICY_VERSION_ID = @POLICY_VERSION_ID   
             
IF @@ERROR <> 0      
BEGIN      
 RAISERROR ('Unable to add linked endorsments.', 16, 1)      
      
END           
  
 RETURN @COVERAGE_CODE_ID            
              
END     




GO

