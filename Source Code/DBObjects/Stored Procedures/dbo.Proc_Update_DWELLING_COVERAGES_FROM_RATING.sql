IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_DWELLING_COVERAGES_FROM_RATING]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_DWELLING_COVERAGES_FROM_RATING]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                
Proc Name   : dbo.Proc_Update_DWELLING_COVERAGES_FROM_RATING                                               
Created by  : Pradeep                                                
Date        : 12/30/2005                                              
Purpose     :  Adds/deletes relevant coverages                        
Revison History  :                
Modified BY : Praveen kasana                                
Modified On : 03 Jan 2006                               
Purpose : Add Coverage provisions for Rental Dwelling   
Proc_Update_DWELLING_COVERAGES_FROM_RATING 837,382,1,1                                                    
------------------------------------------------------------                                                            
Date     Review By          Comments                                                          
-----------------------------------------------------------*/  

--drop proc Proc_Update_DWELLING_COVERAGES_FROM_RATING                                        
CREATE   PROCEDURE Proc_Update_DWELLING_COVERAGES_FROM_RATING                                        
(                                         
  @CUSTOMER_ID int,                                        
  @APP_ID int,                                        
  @APP_VERSION_ID smallint,                                         
  @DWELLING_ID smallint                                             
)                                        
                                        
As                                      
                            
                            
BEGIN                            
                          
 DECLARE @COV_ID Int                            
 DECLARE @STATE_ID Int                          
 DECLARE @LOB_ID Int                          
 DECLARE @POLICY_TYPE Int                         
 DECLARE @IS_UNDER_CONSTRUCTION NChar(1)              
       
               
-----------------------------------------------------                      
            
 --Get Rating Info                
 SELECT @IS_UNDER_CONSTRUCTION = IS_UNDER_CONSTRUCTION                     
 FROM APP_HOME_RATING_INFO                     
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                      
  APP_ID = @APP_ID AND                      
  APP_VERSION_ID = @APP_VERSION_ID AND                      
  DWELLING_ID = @DWELLING_ID                      
                      
                 
 SELECT   @LOB_ID = APP_LOB,
          @STATE_ID = STATE_ID 			                        
 FROM APP_LIST WHERE                      
 CUSTOMER_ID = @CUSTOMER_ID AND                      
 APP_ID = @APP_ID AND                      
 APP_VERSION_ID = @APP_VERSION_ID                      
                        
  
--HOME--------------------------------------------------------------  
IF ( @LOB_ID = 1 )  
BEGIN  
 IF ( @IS_UNDER_CONSTRUCTION = '1' )      
   BEGIN  
  --Dwelling Under Construction (HO-14) EBDUC  
  EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                              
        @CUSTOMER_ID, --@CUSTOMER_ID                              
        @APP_ID, --@APP_ID                              
        @APP_VERSION_ID,--@APP_VERSION_ID                              
        @DWELLING_ID, --@DWELLING_ID                              
        0,  --@COVERAGE_ID                              
        0,  --@COVERAGE_CODE_ID                              
        NULL, --@LIMIT_1                              
        null,  --@LIMIT_2                              
        null,  --@DEDUCTIBLE_1                              
        null,  --@DEDUCTIBLE_2                              
        'S1',  --@COVERAGE_TYPE                              
        'EBDUC'  --@COVERAGE_CODE     
 END  
 ELSE  
 BEGIN  
  EXEC Proc_DELETE_DWELLING_COVERAGES 

      @CUSTOMER_ID,--@CUSTOMER_ID     int,                                
      @APP_ID,--@APP_ID     int,                                
      @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                
      @DWELLING_ID,--@DWELLING_ID smallint,
      'EBDUC'--@COVERAGE_CODE VarChar(10)       
 END      
END                      
------END OF HOME---------------------------------------------------------------  
                            
---Start of rental---------                      
IF ( @LOB_ID = 6 )                      
BEGIN                      
        
  print(@IS_UNDER_CONSTRUCTION)      
  IF ( @IS_UNDER_CONSTRUCTION = '1' )      
  BEGIN      
   --Builders Risk (DP-1143)                       
   EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                              
      @CUSTOMER_ID, --@CUSTOMER_ID                              
      @APP_ID, --@APP_ID                              
      @APP_VERSION_ID,--@APP_VERSION_ID                              
      @DWELLING_ID, --@DWELLING_ID                              
      0,  --@COVERAGE_ID                              
      0,  --@COVERAGE_CODE_ID                              
      NULL, --@LIMIT_1                              
      null,  --@LIMIT_2                              
      null,  --@DEDUCTIBLE_1                              
      null,  --@DEDUCTIBLE_2                              
      'S1',  --@COVERAGE_TYPE                              
      'BR1143'  --@COVERAGE_CODE                              
                              
  --Installation Floater - Building Materials (IF-184)      
  EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                              
      @CUSTOMER_ID, --@CUSTOMER_ID                              
      @APP_ID, --@APP_ID                              
      @APP_VERSION_ID,--@APP_VERSION_ID                              
      @DWELLING_ID, --@DWELLING_ID      
      0,  --@COVERAGE_ID                              
      0,  --@COVERAGE_CODE_ID                              
      NULL, --@LIMIT_1                              
      null,  --@LIMIT_2                              
      null,  --@DEDUCTIBLE_1                              
      null,  --@DEDUCTIBLE_2                              
      'S1',  --@COVERAGE_TYPE                              
      'IF184'  --@COVERAGE_CODE          
           
  --Installation Floater - Non-Structural Equipment (IF-184)    
  EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                              
      @CUSTOMER_ID, --@CUSTOMER_ID                              
      @APP_ID, --@APP_ID                              
      @APP_VERSION_ID,--@APP_VERSION_ID                              
      @DWELLING_ID, --@DWELLING_ID                              
      0,  --@COVERAGE_ID                              
      0,  --@COVERAGE_CODE_ID                              
      NULL, --@LIMIT_1                              
      null,  --@LIMIT_2                              
      null,  --@DEDUCTIBLE_1                              
      null,  --@DEDUCTIBLE_2                              
      'S1',  --@COVERAGE_TYPE                              
      'IFNSE'  --@COVERAGE_CODE          
                                     
       
  END      
  ELSE      
  BEGIN      

 DECLARE @BR1143 Int
 DECLARE @IF184 Int
 DECLARE @IFNSE Int
 /*
14       791
22       811
*/
 IF ( @STATE_ID = 22 )
 BEGIN
  SET @BR1143 = 809
  SET @IF184 =  810
 SET @IFNSE =   811	
 END
 
IF ( @STATE_ID = 14 )
 BEGIN
   SET @BR1143 =  789
  SET @IF184   =  790
 SET @IFNSE    =  791	
 END


 --Delete Builders Risk (DP-1143) and Installation Floater - Building Materials (IF-184)      
 EXEC Proc_DELETE_DWELLING_COVERAGES_BY_ID       
   @CUSTOMER_ID,--@CUSTOMER_ID     int,                                
   @APP_ID,--@APP_ID     int,                                
@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                
   @DWELLING_ID,--@DWELLING_ID smallint,                                
   @BR1143--@COVERAGE_CODE VarChar(10)        
      
 EXEC Proc_DELETE_DWELLING_COVERAGES_BY_ID       
   @CUSTOMER_ID,--@CUSTOMER_ID     int,                                
   @APP_ID,--@APP_ID     int,                                
   @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                
  @DWELLING_ID,--@DWELLING_ID smallint,                                
   @IF184--@COVERAGE_CODE VarChar(10)          
    
 EXEC Proc_DELETE_DWELLING_COVERAGES_BY_ID       
   @CUSTOMER_ID,--@CUSTOMER_ID     int,                                
   @APP_ID,--@APP_ID     int,                              
   @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                
   @DWELLING_ID,--@DWELLING_ID smallint,                                
   @IFNSE--@COVERAGE_CODE VarChar(10)       
      
  END       
                
END                      
                      
------------------Rnd of Rental---------------------------------                      
                   
                                    
END                     
                
                     
         
        
      
    
  









GO

