IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_HOME_ENDORSEMENTS_FROM_LIMITS_FOR_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_HOME_ENDORSEMENTS_FROM_LIMITS_FOR_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc   Proc_UPDATE_HOME_ENDORSEMENTS_FROM_LIMITS_FOR_POLICY
     
/*----------------------------------------------------------                                  
Proc Name       : dbo.Proc_UPDATE_HOME_ENDORSEMENTS_FROM_LIMITS_FOR_POLICY                                  
Created by      : SHAFI                                  
Date            : 06 FEB 2006                               
Purpose         :Inserts linked endorsemnts and Deletes  for the Endorsement    
Revison History :                                  
Used In  : Wolverine                                  
------------------------------------------------------------                                  
Date     Review By          Comments                                  
------   ------------       -------------------------*/                                  
CREATE           PROC Dbo.Proc_UPDATE_HOME_ENDORSEMENTS_FROM_LIMITS_FOR_POLICY              
(                                  
  @CUSTOMER_ID     int,                                  
  @POL_ID     int,                                  
  @POL_VERSION_ID     smallint,    
  @DWELLING_ID smallint            
)               
              
AS               
              
BEGIN              
               
   DECLARE @STATEID SmallInt                                      
   DECLARE @LOBID NVarCHar(5)                   
   DECLARE @PRODUCT Int            
   DECLARE @END_ID Int             
   DECLARE @PERSONAL_LIAB_LIMIT Decimal(18,0)    
   DECLARE @MED_PAY_EACH_PERSON Decimal(18,0)    
           
 SELECT @STATEID = STATE_ID,                                      
 @LOBID = POLICY_LOB ,            
 @PRODUCT = POLICY_TYPE                                      
 FROM POL_CUSTOMER_POLICY_LIST                                  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                      
 POLICY_ID = @POL_ID AND                                      
 POLICY_VERSION_ID = @POL_VERSION_ID                 
               
  SELECT @PERSONAL_LIAB_LIMIT = PERSONAL_LIAB_LIMIT,                                      
  @MED_PAY_EACH_PERSON = MED_PAY_EACH_PERSON            
  FROM POL_DWELLING_COVERAGE                                   
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                      
  POLICY_ID = @POL_ID AND                                      
  POLICY_VERSION_ID = @POL_VERSION_ID  AND    
  DWELLING_ID = @DWELLING_ID    
       
    
          
  --Rental dwelling------------------------------------------        
 IF (  @LOBID = 6 )          
 BEGIN          
     
  --Michigan        
  IF ( @STATEID = 22 )          
  BEGIN          
          
       
   IF (   @PERSONAL_LIAB_LIMIT IS NOT NULL AND @MED_PAY_EACH_PERSON IS NOT NULL )    
   BEGIN     
    --- NEW for E and F      
    ---273 22 6 B O LP-124 Owners,Landlords & Tenants Liability Insurance      
    EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID,  273, @DWELLING_ID         
        
    --274 22 6 B O LP-417 Calendar Date or Time Failure Exclusion      
    EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY  @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID,  274, @DWELLING_ID           
        
    --269 22 6 B O DP-392 Limited Lead Liability DP-392 Limited Lead Liability      
    EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID,  269, @DWELLING_ID           
        
    --268 22 6 B O DP-382 Lead Liability Exclusion      
    --EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID,  268, @DWELLING_ID        
    --End new        
   END    
   ELSE    
   BEGIN      
    EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY       
    @CUSTOMER_ID,--@CUSTOMER_ID     int,                                
    @POL_ID,--@POL_ID     int,                                
    @POL_VERSION_ID,--@POL_VERSION_ID     smallint,       
    273,--@ENDORSEMENT_ID smallint,                             
    @DWELLING_ID--@DWELLING_ID smallint,                                
    ---    
    EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID       
    @CUSTOMER_ID,--@CUSTOMER_ID     int,                                
    @POL_ID,--@POL_ID     int,                                
    @POL_VERSION_ID,--@POL_VERSION_ID     smallint,       
    274,--@ENDORSEMENT_ID smallint,                             
    @DWELLING_ID--@DWELLING_ID smallint,                                
    ---    
    EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID       
    @CUSTOMER_ID,--@CUSTOMER_ID     int,                                
    @POL_ID,--@POL_ID     int,                                
    @POL_VERSION_ID,--@POL_VERSION_ID     smallint,       
    269,--@ENDORSEMENT_ID smallint,                             
    @DWELLING_ID--@DWELLING_ID smallint,                                
    ---    
    /*EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID       
    @CUSTOMER_ID,--@CUSTOMER_ID     int,                                
    @POL_ID,--@POL_ID     int,                                
    @POL_VERSION_ID,--@POL_VERSION_ID     smallint,       
    268,--@ENDORSEMENT_ID smallint,                             
    @DWELLING_ID--@DWELLING_ID smallint,*/    
   END    
      
        END      
  --Indiana        
  IF ( @STATEID = 14 )          
  BEGIN          
   IF ( @PERSONAL_LIAB_LIMIT IS NOT NULL AND @MED_PAY_EACH_PERSON IS NOT NULL )    
   BEGIN     
    -----NEW for E nad F      
    --260 14 6 B O LP-124 Owners, Landlords & Tenants Liability Insurance LP-124 Owners      
    EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID,  260, @DWELLING_ID       
        
    --261 14 6 B O LP-417 Calendar Date or Time Failure Exclusion      
    EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID,  261, @DWELLING_ID       
        
    --255 14 6 B O DP-382 Lead Liability Exclusion      
    --EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID,  255, @DWELLING_ID       
    ------End new        
   END    
   ELSE    
   BEGIN    
    EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY       
    @CUSTOMER_ID,--@CUSTOMER_ID     int,                                
    @POL_ID,--@POL_ID     int,                                
    @POL_VERSION_ID,--@POL_VERSION_ID     smallint,       
    260,--@ENDORSEMENT_ID smallint,                             
    @DWELLING_ID--@DWELLING_ID smallint,                                
    ---    
    EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY       
    @CUSTOMER_ID,--@CUSTOMER_ID     int,                                
    @POL_ID,--@POL_ID     int,                                
    @POL_VERSION_ID,--@POL_VERSION_ID     smallint,       
    261,--@ENDORSEMENT_ID smallint,                             
    @DWELLING_ID--@DWELLING_ID smallint,                                
    ---    
    /*EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID       
    @CUSTOMER_ID,--@CUSTOMER_ID     int,                                
    @POL_ID,--@POL_ID     int,                                
    @POL_VERSION_ID,--@POL_VERSION_ID     smallint,       
    255,--@ENDORSEMENT_ID smallint,                             
    @DWELLING_ID--@DWELLING_ID smallint,          */    
   END        
  -----------End of rental       
  END                                 
             
 END    
END    
        
      
    
    
    
  



GO

