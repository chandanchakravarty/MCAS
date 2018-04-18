IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CopyAppDwellingsInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CopyAppDwellingsInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_CopyAppDwellingsInfo                  
Created by      : Pradeep                  
Date            : 12/05/2005                  
Purpose         : Copies a Dwellings info record                  
Revison History :                  
Used In         :   Wolverine                  
                  
Modified By : Pawan Papreja                 
Modified On : 20/01/2006               
Purpose   :Rewrite the whole Proc         
  
Modified By : Anurag Verma                 
Modified On : 20/07/2006               
Purpose   :putting where clause in fetching additional interest query  
  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/              
-- drop PROC dbo.Proc_CopyAppDwellingsInfo                  
CREATE  PROC dbo.Proc_CopyAppDwellingsInfo                  
(                  
 @CUSTOMER_ID int,                  
 @APP_ID int,                  
 @APP_VERSION_ID smallint,                  
 @DWELLING_ID smallint,                  
 @LOCATION_ID smallint,                  
 @SUB_LOC_ID smallint,                  
 @NEWDWELLINGID int output                  
                  
)                  
                  
AS                  
                  
DECLARE @MAX_DWELLING_ID int                
DECLARE @NEXT_DWELLING_NUMBER int                
DECLARE @TEMP int       
                 
BEGIN                  
        
 -- Get Current date and time to update for Created_Datetime field                         
   Declare @Date varchar(50)                                  
   set @Date = convert(varchar(50),getdate(),100)         
                 
 --Get New Dwlling ID                  
 SELECT @MAX_DWELLING_ID = ISNULL(MAX(DWELLING_ID),0) + 1                
 FROM APP_DWELLINGS_INFO                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                
       APP_ID = @APP_ID AND                
       APP_VERSION_ID = @APP_VERSION_ID                 
                 
 IF @@ERROR <> 0                
 BEGIN                
  RETURN                
 END                 
                 
 SELECT  @TEMP = ISNULL(MAX(DWELLING_NUMBER),0)                
 FROM APP_DWELLINGS_INFO                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                
       APP_ID = @APP_ID AND                
       APP_VERSION_ID = @APP_VERSION_ID                 
                 
 IF @@ERROR <> 0                
 BEGIN                
  RETURN                
 END                       
               
                  
 IF ( @TEMP >= 2147483647 )                  
 BEGIN                  
  SET @NEWDWELLINGID = -10                  
  RETURN                  
 END                  
       
 SET @NEXT_DWELLING_NUMBER = @TEMP + 1       
      
BEGIN TRAN                          
   DECLARE @TEMP_ERROR_CODE INT            
         
        
 SELECT * INTO #APP_DWELLINGS_INFO FROM APP_DWELLINGS_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and DWELLING_ID = @DWELLING_ID                         
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
                        
     UPDATE #APP_DWELLINGS_INFO SET DWELLING_ID = @MAX_DWELLING_ID,DWELLING_NUMBER = @NEXT_DWELLING_NUMBER,      
 LOCATION_ID = @LOCATION_ID,CREATED_DATETIME = @DATE,IS_ACTIVE = 'Y'       
                          
     SELECT @TEMP_ERROR_CODE = @@ERROR                                  
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
                              
     INSERT INTO APP_DWELLINGS_INFO SELECT * FROM #APP_DWELLINGS_INFO                         
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
                        
     DROP TABLE #APP_DWELLINGS_INFO                          
     SELECT @TEMP_ERROR_CODE = @@ERROR                                      
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM           
                
         
                   
 SET @NEWDWELLINGID = @MAX_DWELLING_ID     
  
--   APP_OTHER_STRUCTURE_DWELLING       
  
SELECT * INTO #APP_OTHER_STRUCTURE_DWELLING FROM APP_OTHER_STRUCTURE_DWELLING  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and DWELLING_ID = @DWELLING_ID                        
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     UPDATE #APP_OTHER_STRUCTURE_DWELLING SET DWELLING_ID = @MAX_DWELLING_ID                       
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     INSERT INTO APP_OTHER_STRUCTURE_DWELLING SELECT * FROM #APP_OTHER_STRUCTURE_DWELLING                      
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     DROP TABLE #APP_OTHER_STRUCTURE_DWELLING                       
     SELECT @TEMP_ERROR_CODE = @@ERROR                                   
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
            
---Copy Home Rating          
  SELECT * INTO #APP_HOME_RATING_INFO FROM APP_HOME_RATING_INFO  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and DWELLING_ID = @DWELLING_ID                        
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     UPDATE #APP_HOME_RATING_INFO SET DWELLING_ID = @MAX_DWELLING_ID                       
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     INSERT INTO APP_HOME_RATING_INFO SELECT * FROM #APP_HOME_RATING_INFO                      
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     DROP TABLE #APP_HOME_RATING_INFO                        
     SELECT @TEMP_ERROR_CODE = @@ERROR                                   
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                     
           
           
---Copy ADDITIONAL INTEREST       
   SELECT * INTO #APP_HOME_OWNER_ADD_INT FROM APP_HOME_OWNER_ADD_INT    
   WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID    
   AND DWELLING_ID = @DWELLING_ID AND ISNULL(UPPER(IS_ACTIVE),'N')='Y'                    
  
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     UPDATE #APP_HOME_OWNER_ADD_INT SET DWELLING_ID = @MAX_DWELLING_ID,CREATED_DATETIME = @DATE                        
   SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     INSERT INTO APP_HOME_OWNER_ADD_INT SELECT * FROM #APP_HOME_OWNER_ADD_INT                      
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     DROP TABLE #APP_HOME_OWNER_ADD_INT                       
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
      
          
---Copy Dwelling_Coverage            
SELECT * INTO #APP_DWELLING_COVERAGE FROM APP_DWELLING_COVERAGE WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID      and DWELLING_ID = @DWELLING_ID                                   
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     UPDATE #APP_DWELLING_COVERAGE SET DWELLING_ID = @MAX_DWELLING_ID      
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     INSERT INTO APP_DWELLING_COVERAGE SELECT * FROM #APP_DWELLING_COVERAGE                      
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     DROP TABLE #APP_DWELLING_COVERAGE                      
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                
      
 --copy APP_DWELLING_SECTION_COVERAGES            
       SELECT * INTO #APP_DWELLING_SECTION_COVERAGES FROM APP_DWELLING_SECTION_COVERAGES WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and DWELLING_ID = @DWELLING_ID                                                   
  
  
    
                   
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     UPDATE #APP_DWELLING_SECTION_COVERAGES SET DWELLING_ID = @MAX_DWELLING_ID      
     SELECT @TEMP_ERROR_CODE = @@ERROR                                  
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     INSERT INTO APP_DWELLING_SECTION_COVERAGES SELECT * FROM #APP_DWELLING_SECTION_COVERAGES                      
     SELECT @TEMP_ERROR_CODE = @@ERROR                               
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM           
      
 --REMOVE Identity Fraud Expense Coverage (HO-455) , IF LOcation is not PRIMARY      
 Declare @IS_PRIMARY_LOC varchar(2)       
       Select @IS_PRIMARY_LOC = IS_PRIMARY FROM APP_LOCATIONS WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and LOCATION_ID = @LOCATION_ID                                                                           
 IF  @IS_PRIMARY_LOC = 'N'      
 BEGIN      
  DELETE FROM APP_DWELLING_SECTION_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and       
  APP_VERSION_ID=@APP_VERSION_ID and DWELLING_ID = @MAX_DWELLING_ID and   COVERAGE_CODE_ID IN (84,158)                                                                    
 END          
                 
     DROP TABLE #APP_DWELLING_SECTION_COVERAGES                       
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM       
    
  --Copy Other Locations    
    
  SELECT * INTO #APP_OTHER_LOCATIONS FROM APP_OTHER_LOCATIONS WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID    and DWELLING_ID = @DWELLING_ID                                                    
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     UPDATE #APP_OTHER_LOCATIONS SET DWELLING_ID = @MAX_DWELLING_ID      
     SELECT @TEMP_ERROR_CODE = @@ERROR                           
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     INSERT INTO APP_OTHER_LOCATIONS SELECT * FROM #APP_OTHER_LOCATIONS                      
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     DROP TABLE #APP_OTHER_LOCATIONS                      
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM       
           
          
---Copy Endorsement            
SELECT * INTO #APP_DWELLING_ENDORSEMENTS FROM APP_DWELLING_ENDORSEMENTS WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID    and DWELLING_ID = @DWELLING_ID                                                    
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     UPDATE #APP_DWELLING_ENDORSEMENTS SET DWELLING_ID = @MAX_DWELLING_ID      
     SELECT @TEMP_ERROR_CODE = @@ERROR                           
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     INSERT INTO APP_DWELLING_ENDORSEMENTS SELECT * FROM #APP_DWELLING_ENDORSEMENTS                      
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     DROP TABLE #APP_DWELLING_ENDORSEMENTS                      
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                   
      
           
               
               
COMMIT TRAN                                                                           
   RETURN @NEWDWELLINGID                                                  
                                               
  PROBLEM:                                     
   ROLLBACK TRAN                                                
   SET @NEWDWELLINGID = -1                                 
   RETURN @NEWDWELLINGID      
END



GO

