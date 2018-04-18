IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                            
Proc Name   : dbo.Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES                           
Created by  : Pradeep                            
Date        : Apr 11, 2006                        
Purpose     :  Inserts an appropriate endorsemnt in                 
  POL_VEHICLE_ENDORSEMENTS                
Revison History  :                                  
------------------------------------------------------------                                        
Date     Review By          Comments                                      
-----------------------------------------------------------      
drop proc dbo.Proc_Insert_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES                    
*/      
                      
CREATE   PROCEDURE dbo.Proc_Insert_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES                    
(                     
 @CUSTOMER_ID int,                    
 @POLICY_ID int,                    
 @POLICY_VERSION_ID smallint,                     
 @ENDORSEMENT_ID smallint  ,    
 @POL_VEHICLE_ID smallint =null              
)                    
                    
As                    
               
DECLARE @IDENT_COL Int            
DECLARE @VEHICLE_ID Int 

DECLARE @LOB_ID INT --Added by Charles on 3-Aug-09 for Itrack 6201
                 
 SET  @IDENT_COL = 1              
DECLARE @APP_EFFECTIVE_DATE datetime             
  SELECT       
  @APP_EFFECTIVE_DATE= APP_EFFECTIVE_DATE,   
  @LOB_ID=POLICY_LOB --Added by Charles on 3-Aug-09 for Itrack 6201                            
  FROM POL_CUSTOMER_POLICY_LIST      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                  
    POLICY_ID = @POLICY_ID AND                                  
    POLICY_VERSION_ID = @POLICY_VERSION_ID                       
------------    
DECLARE @APP_USE_VEHICLE_ID INT      
if (@POL_VEHICLE_ID is not null)    
BEGIN    
  SELECT @APP_USE_VEHICLE_ID=APP_USE_VEHICLE_ID        
  FROM POL_VEHICLES        
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
  POLICY_ID = @POLICY_ID AND        
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
  VEHICLE_ID = @POL_VEHICLE_ID       
 END                        
--------------------    
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
   
 IF @LOB_ID = 3  --IF-ELSE Condition added by Charles on 4-Aug-09 for Itrack 6201
 BEGIN
	 INSERT INTO @TEMP_VEH        
	  (        
	  VEHICLE_ID        
	  )          
	  SELECT VEHICLE_ID        
	  FROM POL_VEHICLES        
	  WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
	   POLICY_ID = @POLICY_ID AND        
	   POLICY_VERSION_ID = @POLICY_VERSION_ID         
	   AND ISNULL(APP_USE_VEHICLE_ID,'0')=ISNULL(@APP_USE_VEHICLE_ID ,isnull(APP_USE_VEHICLE_ID,'0')) 
	   AND (ISNULL(COMPRH_ONLY,0)!=10963 AND @COVERAGE_TYPE!='PL')  --Added by Charles on 3-Aug-09 for Itrack 6201  
 END
 ELSE
 BEGIN
	INSERT INTO @TEMP_VEH        
	  (        
	  VEHICLE_ID        
	  )          
	  SELECT VEHICLE_ID        
	  FROM POL_VEHICLES        
	  WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
	   POLICY_ID = @POLICY_ID AND        
	   POLICY_VERSION_ID = @POLICY_VERSION_ID         
	   AND ISNULL(APP_USE_VEHICLE_ID,'0')=ISNULL(@APP_USE_VEHICLE_ID ,isnull(APP_USE_VEHICLE_ID,'0')) 	   
 END 
  
 WHILE 1 = 1                        
 BEGIN                        
                          
   IF NOT EXISTS                        
   (                        
     SELECT * FROM @TEMP_VEH                        
     WHERE IDENT_COL = @IDENT_COL                        
   )                        
   BEGIN                        
     BREAK                        
   END                        
                           
   SELECT @VEHICLE_ID = VEHICLE_ID               
   FROM @TEMP_VEH                        
   WHERE IDENT_COL = @IDENT_COL          
          
  SET  @IDENT_COL   = @IDENT_COL + 1             
          
  IF NOT EXISTS                
  (                
    SELECT * FROM POL_VEHICLE_ENDORSEMENTS                
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND                
     POLICY_ID = @POLICY_ID AND                
     POLICY_VERSION_ID = @POLICY_VERSION_ID AND                
     VEHICLE_ID = @VEHICLE_ID  AND              
     ENDORSEMENT_ID = @ENDORSEMENT_ID               
  )            
  BEGIN            
             
  DECLARE @VEH_END_ID Int            
   declare @EDITION_DATE varchar(10)          
 SELECT  @VEH_END_ID   = ISNULL(MAX ( VEHICLE_ENDORSEMENT_ID ),0) + 1            
 FROM POL_VEHICLE_ENDORSEMENTS           
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
  POLICY_ID = @POLICY_ID AND            
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND            
  VEHICLE_ID = @VEHICLE_ID       
 --BY PRAVESH FOR DEFAULT EDITION DATE                            
   SELECT    @EDITION_DATE =ENDORSEMENT_ATTACH_ID  FROM MNT_ENDORSEMENT_ATTACHMENT WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID AND                 
   @APP_EFFECTIVE_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-12') AND  @APP_EFFECTIVE_DATE<=ISNULL(DISABLED_DATE,'3000-12-12')                 
--           
          
   INSERT INTO POL_VEHICLE_ENDORSEMENTS                
   (                
    CUSTOMER_ID,                
    POLICY_ID,                
    POLICY_VERSION_ID,                
    VEHICLE_ID,                
    ENDORSEMENT_ID,            
    VEHICLE_ENDORSEMENT_ID,      
    EDITION_DATE                 
   )                
   VALUES                
(                
    @CUSTOMER_ID,                
    @POLICY_ID,                
    @POLICY_VERSION_ID,                
    @VEHICLE_ID,                
    @ENDORSEMENT_ID ,            
    @VEH_END_ID     ,      
    @EDITION_DATE          
  )                
 END               
          
 END          
                                
RETURN 1                    
                    
   
      
GO

