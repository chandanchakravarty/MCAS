IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UM_CopyAppDwellingsInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UM_CopyAppDwellingsInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




create PROC Dbo.Proc_UM_CopyAppDwellingsInfo            
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
  
  --Current Date and Time
   Declare @Date varchar(50)                            
   set @Date = convert(varchar(50),getdate(),100)   
           
 --Get New Dwelling ID            
 SELECT @MAX_DWELLING_ID = ISNULL(MAX(DWELLING_ID),0) + 1          
 FROM APP_UMBRELLA_DWELLINGS_INFO          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
       APP_ID = @APP_ID AND          
       APP_VERSION_ID = @APP_VERSION_ID           
           
 IF @@ERROR <> 0          
 BEGIN          
  RETURN          
 END           
           
 SELECT  @TEMP = ISNULL(MAX(DWELLING_NUMBER),0)          
 FROM APP_UMBRELLA_DWELLINGS_INFO          
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
   
  
 SELECT * INTO #APP_UMBRELLA_DWELLINGS_INFO FROM APP_UMBRELLA_DWELLINGS_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and DWELLING_ID = @DWELLING_ID                   
     SELECT @TEMP_ERROR_CODE = @@ERROR                                          
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                    
                  
     UPDATE #APP_UMBRELLA_DWELLINGS_INFO SET DWELLING_ID = @MAX_DWELLING_ID,DWELLING_NUMBER = @NEXT_DWELLING_NUMBER,
	LOCATION_ID = @LOCATION_ID,CREATED_DATETIME = @DATE 
                    
     SELECT @TEMP_ERROR_CODE = @@ERROR                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                   
                        
     INSERT INTO APP_UMBRELLA_DWELLINGS_INFO SELECT * FROM #APP_UMBRELLA_DWELLINGS_INFO                   
     SELECT @TEMP_ERROR_CODE = @@ERROR                                          
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                    
                  
     DROP TABLE #APP_UMBRELLA_DWELLINGS_INFO                    
     SELECT @TEMP_ERROR_CODE = @@ERROR                                
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM     
          
   
             
 SET @NEWDWELLINGID = @MAX_DWELLING_ID               
      
---Copy Umbrella Rating    
  SELECT * INTO #APP_UMBRELLA_RATING_INFO FROM APP_UMBRELLA_RATING_INFO  WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID  and DWELLING_ID = @DWELLING_ID                  
     SELECT @TEMP_ERROR_CODE = @@ERROR                                        
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                  
                
     UPDATE #APP_UMBRELLA_RATING_INFO SET DWELLING_ID = @MAX_DWELLING_ID                 
     SELECT @TEMP_ERROR_CODE = @@ERROR                                        
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                  
                
     INSERT INTO APP_UMBRELLA_RATING_INFO SELECT * FROM #APP_UMBRELLA_RATING_INFO                
     SELECT @TEMP_ERROR_CODE = @@ERROR                                        
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                  
                
     DROP TABLE #APP_UMBRELLA_RATING_INFO                  
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

