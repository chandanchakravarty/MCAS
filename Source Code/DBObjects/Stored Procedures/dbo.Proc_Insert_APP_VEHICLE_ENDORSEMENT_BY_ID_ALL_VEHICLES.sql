IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                              
Proc Name   : dbo.Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES                             
Created by  : Pradeep                              
Date        : 25 Nov,2005                            
Purpose     :  Inserts an appropriate endorsemnt in                   
  APP_VEHICLE_ENDORSEMENTS                  
Revison History  :                                    
------------------------------------------------------------                                          
Date     Review By          Comments                                        
-----------------------------------------------------------
drop proc dbo.Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES                      
*/                        
CREATE   PROCEDURE dbo.Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES                      
(                       
 @CUSTOMER_ID int,                      
 @APP_ID int,                      
 @APP_VERSION_ID smallint,                       
 @ENDORSEMENT_ID smallint,        
 @APP_VEHICLE_ID smallint =null --Added by Charles on 10-Jul-09 for Itrack 6082        
)                      
                      
As                      
                 
DECLARE @IDENT_COL Int              
DECLARE @VEHICLE_ID Int                        
 SET  @IDENT_COL = 1                          
---for edition date          
DECLARE @APP_EFFECTIVE_DATE datetime                                
DECLARE @EDITION_DATE VARCHAR(10)  

DECLARE @LOB_ID INT --Added by Charles on 3-Aug-09 for Itrack 6201
                                                    
 SELECT  @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE,
 @LOB_ID=APP_LOB --Added by Charles on 3-Aug-09 for Itrack 6201                                        
 FROM APP_LIST  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                  
 APP_ID = @APP_ID AND                                  
 APP_VERSION_ID = @APP_VERSION_ID            
---              
--Added by Charles on 10-Jul-09 for Itrack 6082        
DECLARE @USE_VEHICLE INT            
if (@APP_VEHICLE_ID is not null)          
BEGIN          
  SELECT @USE_VEHICLE=USE_VEHICLE              
  FROM APP_VEHICLES              
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                  
 APP_ID = @APP_ID AND                                  
 APP_VERSION_ID = @APP_VERSION_ID  AND              
  VEHICLE_ID = @APP_VEHICLE_ID             
 END            
--Added till here             
                     
 --Holds effective dates, inception dates of all versions of this current policy                          
 DECLARE @TEMP_VEH TABLE                          
 (                          
  IDENT_COL Int Identity (1,1),                          
  VEHICLE_ID Int             
 )                         
  
  --Added by Charles on 3-Aug-09 for Itrack 6201
    DECLARE @COVERAGE_TYPE NCHAR(20)
	SELECT  @COVERAGE_TYPE=COVERAGE_TYPE FROM MNT_COVERAGE WITH(NOLOCK) WHERE COV_ID=
	(SELECT ISNULL(SELECT_COVERAGE,0) FROM MNT_ENDORSMENT_DETAILS WITH(NOLOCK) WHERE ENDORSMENT_ID=@ENDORSEMENT_ID)
  --Added till here 

  IF @LOB_ID = 3 --IF-ELSE Condition added by Charles on 4-Aug-09 for Itrack 6201
  BEGIN
	  INSERT INTO  @TEMP_VEH          
	  (          
	  VEHICLE_ID          
	  )            
	  SELECT VEHICLE_ID FROM APP_VEHICLES          
	  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND              
	   APP_ID = @APP_ID AND              
	   APP_VERSION_ID = @APP_VERSION_ID         
	   AND ISNULL(USE_VEHICLE,'0')=ISNULL(@USE_VEHICLE ,isnull(USE_VEHICLE,'0'))
	   AND (ISNULL(COMPRH_ONLY,0)!=10963 AND @COVERAGE_TYPE!='PL')  --Added by Charles on 3-Aug-09 for Itrack 6201 
  END
  ELSE
  BEGIN
	  INSERT INTO  @TEMP_VEH          
	  (          
	  VEHICLE_ID          
	  )            
	  SELECT VEHICLE_ID FROM APP_VEHICLES          
	  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND              
	   APP_ID = @APP_ID AND              
	   APP_VERSION_ID = @APP_VERSION_ID         
	   AND ISNULL(USE_VEHICLE,'0')=ISNULL(@USE_VEHICLE ,isnull(USE_VEHICLE,'0'))
  END      
          
 WHILE 1 = 1                          
 BEGIN                          
                            
   IF NOT EXISTS                          
   (                          
     SELECT * FROM @TEMP_VEH                          
     WHERE IDENT_COL = @IDENT_COL                         )                          
   BEGIN                          
     BREAK                          
   END                          
                             
   SELECT @VEHICLE_ID = VEHICLE_ID                 
   FROM @TEMP_VEH                          
   WHERE IDENT_COL = @IDENT_COL            
            
  SET  @IDENT_COL   = @IDENT_COL + 1               
            
  IF NOT EXISTS                  
  (                  
    SELECT * FROM APP_VEHICLE_ENDORSEMENTS                  
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND                  
     APP_ID = @APP_ID AND                  
     APP_VERSION_ID = @APP_VERSION_ID AND                  
     VEHICLE_ID = @VEHICLE_ID  AND             
     ENDORSEMENT_ID = @ENDORSEMENT_ID                 
  )              
  BEGIN              
               
  DECLARE @VEH_END_ID Int              
               
 SELECT  @VEH_END_ID   = ISNULL(MAX ( VEHICLE_ENDORSEMENT_ID ),0) + 1              
 FROM APP_VEHICLE_ENDORSEMENTS              
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
  APP_ID = @APP_ID AND              
  APP_VERSION_ID = @APP_VERSION_ID AND              
  VEHICLE_ID = @VEHICLE_ID              
  --BY PRAVESH FOR DEFAULT EDITION DATE                      
   SELECT  @EDITION_DATE =ENDORSEMENT_ATTACH_ID  FROM MNT_ENDORSEMENT_ATTACHMENT WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID AND           
   @APP_EFFECTIVE_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-12') AND  @APP_EFFECTIVE_DATE<=ISNULL(DISABLED_DATE,'3000-12-12')           
--          
          
   INSERT INTO APP_VEHICLE_ENDORSEMENTS                  
   (                  
    CUSTOMER_ID,                  
    APP_ID,                  
    APP_VERSION_ID,                  
    VEHICLE_ID,                  
    ENDORSEMENT_ID,              
    VEHICLE_ENDORSEMENT_ID ,           
    EDITION_DATE                
   )                  
   VALUES                  
   (                  
    @CUSTOMER_ID,                  
    @APP_ID,                  
    @APP_VERSION_ID,                  
    @VEHICLE_ID,                  
    @ENDORSEMENT_ID ,              
    @VEH_END_ID  ,          
    @EDITION_DATE               
  )                  
 END                 
            
 END            
                
                  
           
RETURN 1                      
                 
     
GO

