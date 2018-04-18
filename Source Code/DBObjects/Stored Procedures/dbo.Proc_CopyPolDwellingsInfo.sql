IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CopyPolDwellingsInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CopyPolDwellingsInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.Proc_CopyPolDwellingsInfo                
Created by      : shafi                
Date            : 23/02/06         
Purpose         : Copies a Dwellings info record                
Revison History :                
Used In         :   Wolverine                
                
Modified By : Pawan Papreja               
Modified On : 20/01/2006             
Purpose   :Rewrite the whole Proc       
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/            
--drop PROC dbo.Proc_CopyPolDwellingsInfo                
CREATE PROC dbo.Proc_CopyPolDwellingsInfo                            
(                
 @CUSTOMER_ID int,                
 @policy_ID int,                
 @policy_VERSION_ID smallint,                
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
 FROM pol_DWELLINGS_INFO              
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
       policy_ID = @policy_ID AND              
       policy_VERSION_ID = @policy_VERSION_ID               
               
 IF @@ERROR <> 0              
 BEGIN              
  RETURN              
 END               
               
 SELECT  @TEMP = ISNULL(MAX(DWELLING_NUMBER),0)              
 FROM pol_DWELLINGS_INFO              
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
       policy_ID = @policy_ID AND              
       policy_VERSION_ID = @policy_VERSION_ID               
               
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
       
      
 SELECT * INTO #pol_DWELLINGS_INFO FROM pol_DWELLINGS_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID and policy_ID=@policy_ID and policy_VERSION_ID=@policy_VERSION_ID and DWELLING_ID = @DWELLING_ID                       
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     UPDATE #POL_DWELLINGS_INFO SET DWELLING_ID = @MAX_DWELLING_ID,DWELLING_NUMBER = @NEXT_DWELLING_NUMBER,    
 LOCATION_ID = @LOCATION_ID,CREATED_DATETIME = @DATE, IS_ACTIVE = 'Y'     
                        
     SELECT @TEMP_ERROR_CODE = @@ERROR                                
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                       
                            
     INSERT INTO POL_DWELLINGS_INFO SELECT * FROM #POL_DWELLINGS_INFO                       
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     DROP TABLE #POL_DWELLINGS_INFO                        
     SELECT @TEMP_ERROR_CODE = @@ERROR                                    
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM         
              
       
                 
 SET @NEWDWELLINGID = @MAX_DWELLING_ID                   
          
 ---Copy Other Structures     
  SELECT * INTO #POL_OTHER_STRUCTURE_DWELLING FROM POL_OTHER_STRUCTURE_DWELLING  WHERE  CUSTOMER_ID=@CUSTOMER_ID and policy_ID=@policy_ID and policy_VERSION_ID=@policy_VERSION_ID  and DWELLING_ID = @DWELLING_ID                        
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     UPDATE #POL_OTHER_STRUCTURE_DWELLING SET DWELLING_ID = @MAX_DWELLING_ID                       
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     INSERT INTO POL_OTHER_STRUCTURE_DWELLING SELECT * FROM #POL_OTHER_STRUCTURE_DWELLING                      
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     DROP TABLE #POL_OTHER_STRUCTURE_DWELLING                       
     SELECT @TEMP_ERROR_CODE = @@ERROR                                   
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
                       
  
---Copy Home Rating      
  SELECT * INTO #POL_HOME_RATING_INFO FROM POL_HOME_RATING_INFO  WHERE  CUSTOMER_ID=@CUSTOMER_ID and policy_ID=@policy_ID and policy_VERSION_ID=@policy_VERSION_ID  and DWELLING_ID = @DWELLING_ID                      
     SELECT @TEMP_ERROR_CODE = @@ERROR                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
                    
     UPDATE #POL_HOME_RATING_INFO SET DWELLING_ID = @MAX_DWELLING_ID                     
     SELECT @TEMP_ERROR_CODE = @@ERROR                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
                    
     INSERT INTO POL_HOME_RATING_INFO SELECT * FROM #POL_HOME_RATING_INFO                    
     SELECT @TEMP_ERROR_CODE = @@ERROR                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
                    
     DROP TABLE #POL_HOME_RATING_INFO                      
     SELECT @TEMP_ERROR_CODE = @@ERROR                                 
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                   
         
         
    
         
---Copy ADDITIONAL INTEREST     
 SELECT * INTO #POL_HOME_OWNER_ADD_INT FROM POL_HOME_OWNER_ADD_INT   
 WHERE  CUSTOMER_ID=@CUSTOMER_ID and policy_ID=@policy_ID and policy_VERSION_ID=@policy_VERSION_ID  and DWELLING_ID = @DWELLING_ID   
 AND ISNULL(IS_ACTIVE,'N')='Y'                    
     SELECT @TEMP_ERROR_CODE = @@ERROR                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
                    
     UPDATE #POL_HOME_OWNER_ADD_INT SET DWELLING_ID = @MAX_DWELLING_ID,CREATED_DATETIME = @DATE                      
   SELECT @TEMP_ERROR_CODE = @@ERROR                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
                    
     INSERT INTO POL_HOME_OWNER_ADD_INT SELECT * FROM #POL_HOME_OWNER_ADD_INT                    
     SELECT @TEMP_ERROR_CODE = @@ERROR                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
                    
     DROP TABLE #POL_HOME_OWNER_ADD_INT                     
     SELECT @TEMP_ERROR_CODE = @@ERROR                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM            
    
        
  --Copy Other Locations    
    
  SELECT * INTO #POL_OTHER_LOCATIONS FROM POL_OTHER_LOCATIONS WHERE  CUSTOMER_ID=@CUSTOMER_ID and policy_ID=@policy_ID and policy_VERSION_ID=@policy_VERSION_ID  and DWELLING_ID = @DWELLING_ID                     
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     UPDATE #POL_OTHER_LOCATIONS SET DWELLING_ID = @MAX_DWELLING_ID      
     SELECT @TEMP_ERROR_CODE = @@ERROR                           
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     INSERT INTO POL_OTHER_LOCATIONS SELECT * FROM #POL_OTHER_LOCATIONS                      
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                        
                      
     DROP TABLE #POL_OTHER_LOCATIONS                      
     SELECT @TEMP_ERROR_CODE = @@ERROR                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM            
    
 --copy POL_DWELLING_SECTION_COVERAGES          
       SELECT * INTO #POL_DWELLING_SECTION_COVERAGES FROM POL_DWELLING_SECTION_COVERAGES WHERE CUSTOMER_ID=@CUSTOMER_ID and policy_ID=@policy_ID and policy_VERSION_ID=@policy_VERSION_ID and DWELLING_ID = @DWELLING_ID                                       
 
  
  
  
   
                
                 
     SELECT @TEMP_ERROR_CODE = @@ERROR                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
                    
     UPDATE #POL_DWELLING_SECTION_COVERAGES SET DWELLING_ID = @MAX_DWELLING_ID    
     SELECT @TEMP_ERROR_CODE = @@ERROR                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
                    
     INSERT INTO POL_DWELLING_SECTION_COVERAGES SELECT * FROM #POL_DWELLING_SECTION_COVERAGES                    
     SELECT @TEMP_ERROR_CODE = @@ERROR                             
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM         
    
 --REMOVE Identity Fraud Expense Coverage (HO-455) , IF LOcation is not PRIMARY    
 Declare @IS_PRIMARY_LOC varchar(2)     
       Select @IS_PRIMARY_LOC = IS_PRIMARY FROM POL_LOCATIONS WHERE  CUSTOMER_ID=@CUSTOMER_ID and policy_ID=@policy_ID and policy_VERSION_ID=@policy_VERSION_ID and LOCATION_ID = @LOCATION_ID                                                                 
 
  
  
  
   
        
 IF  @IS_PRIMARY_LOC = 'N'    
 BEGIN    
  DELETE FROM POL_DWELLING_SECTION_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and policy_ID=@policy_ID and     
  policy_VERSION_ID=@policy_VERSION_ID and DWELLING_ID = @MAX_DWELLING_ID and   COVERAGE_CODE_ID IN (84,158)                                                                  
 END        
               
     DROP TABLE #POL_DWELLING_SECTION_COVERAGES                     
     SELECT @TEMP_ERROR_CODE = @@ERROR                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM     
        
---Copy Endorsement          
SELECT * INTO #POL_DWELLING_ENDORSEMENTS FROM POL_DWELLING_ENDORSEMENTS WHERE  CUSTOMER_ID=@CUSTOMER_ID and policy_ID=@policy_ID and policy_VERSION_ID=@policy_VERSION_ID    and DWELLING_ID = @DWELLING_ID                                                  
     SELECT @TEMP_ERROR_CODE = @@ERROR                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
                    
     UPDATE #POL_DWELLING_ENDORSEMENTS SET DWELLING_ID = @MAX_DWELLING_ID    
     SELECT @TEMP_ERROR_CODE = @@ERROR                         
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
                    
     INSERT INTO POL_DWELLING_ENDORSEMENTS SELECT * FROM #POL_DWELLING_ENDORSEMENTS                    
     SELECT @TEMP_ERROR_CODE = @@ERROR                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
                    
     DROP TABLE #POL_DWELLING_ENDORSEMENTS                    
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

