   
   
 /*----------------------------------------------------------                                          
Proc Name       : [dbo].[proc_INSERT_CURRENTRATING]                                        
Created by      : Amit K. Mishra                                         
Date            : October 11, 2011                                         
Purpose         : To insert the data into Current Rating table.                                          
Revison History :                                          
Used In         : SINGAPORE Specific                                  
                                         
------------------------------------------------------------                                          
Date     Review By          Comments                                          
------   ------------       -------------------------*/                          
           
        
ALTER Proc [dbo].[Proc_INSERT_CURRENTRATING]          
  @COMPANY_ID int,          
  @COMPANY_TYPE nvarchar(6),          
  @AGENCY_ID int,          
  @RATING nvarchar(6),          
  @EFFECTIVE_YEAR int,          
  @CREATED_BY int,          
  @CREATED_DATETIME datetime,          
  @MODIFIED_BY int,          
  @LAST_UPDATED_DATETIME datetime          
As          
BEGIN     
DECLARE @RATING_ID int         
IF EXISTS   
 (    
    SELECT * FROM MNT_CURRENT_RATING_LIST WITH(NOLOCK) WHERE COMPANY_ID =@COMPANY_ID And     
    COMPANY_TYPE=@COMPANY_TYPE     
   )               
BEGIN      
DECLARE @RATING_LOG_ID INT      
DECLARE @RATINGID INT      
DECLARE @EXISTING_RATING NVARCHAR(6)  
DECLARE @EXIST_RATING_YEAR INT       
    SELECT @RATINGID=RATING_ID,@EXISTING_RATING=RATING FROM MNT_CURRENT_RATING_LIST WITH(NOLOCK)   
    WHERE COMPANY_ID =@COMPANY_ID And COMPANY_TYPE=@COMPANY_TYPE      
    
IF @RATING!=@EXISTING_RATING      
 BEGIN    
   SELECT @RATING_LOG_ID = isnull(Max(RATING_LOG_ID),0)+1 FROM MNT_RATING_LIST_LOG      
  INSERT INTO MNT_RATING_LIST_LOG    
  (    
   RATING_LOG_ID,  
   RATING_ID,    
   AGENCY_ID,      
   RATED,      
   EFFECTIVE_YEAR,      
   IS_ACTIVE,      
   CREATED_BY,      
   CREATED_DATETIME,      
   MODIFIED_BY,      
   LAST_UPDATED_DATETIME    
  )    
  SELECT    
   @RATING_LOG_ID,    
   RATING_ID,     
   AGENCY_ID,    
   RATING,         
   EFFECTIVE_YEAR,    
   1,          
   CREATED_BY,          
   CREATED_DATETIME,          
   MODIFIED_BY,          
   LAST_UPDATED_DATETIME     
   FROM MNT_CURRENT_RATING_LIST WITH (NOLOCK)    
   WHERE Rating_ID=@RATINGID    
     
 UPDATE MNT_CURRENT_RATING_LIST SET AGENCY_ID = @AGENCY_ID, RATING=@RATING,EFFECTIVE_YEAR=@EFFECTIVE_YEAR WHERE RATING_ID=@RATINGID     
 RETURN 1    
END  
ELSE IF EXISTS(SELECT * FROM MNT_RATING_LIST_LOG WHERE RATING_ID=@RATINGID)  
BEGIN  
SELECT @EXIST_RATING_YEAR=EFFECTIVE_YEAR FROM MNT_RATING_LIST_LOG where RATING_ID=@RATINGID And EFFECTIVE_YEAR > @EFFECTIVE_YEAR  
  
END     
 ELSE    
  BEGIN    
   UPDATE MNT_CURRENT_RATING_LIST SET AGENCY_ID=@AGENCY_ID,RATING=@RATING WHERE RATING_ID=@RATINGID    
   RETURN 3      
  END    
 END    
ELSE      
BEGIN      
 SELECT @RATING_ID = isnull(Max(RATING_ID),0)+1 FROM MNT_CURRENT_RATING_LIST  With(NOLOCK)          
 INSERT INTO MNT_CURRENT_RATING_LIST          
       (          
     RATING_ID,          
     COMPANY_ID,          
     COMPANY_TYPE,          
     AGENCY_ID,          
     RATING,          
     EFFECTIVE_YEAR,          
     CREATED_BY,          
     CREATED_DATETIME,          
     MODIFIED_BY,          
     LAST_UPDATED_DATETIME          
       )          
   VALUES          
  (          
    @RATING_ID ,          
    @COMPANY_ID ,          
    @COMPANY_TYPE,          
    @AGENCY_ID ,          
    @RATING ,          
    @EFFECTIVE_YEAR,          
    @CREATED_BY ,          
    @CREATED_DATETIME ,          
    @MODIFIED_BY,			
    @LAST_UPDATED_DATETIME          
  )   
 RETURN 2          
 END    
      
END

--delete from MNT_RATING_LIST_LOG
--delete from MNT_CURRENT_RATING_LIST

--select * from MNT_RATING_LIST_LOG
--select * from MNT_CURRENT_RATING_LIST