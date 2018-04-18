IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_UPDATE_HOME_COVERAGE_BY_APPLICANT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_UPDATE_HOME_COVERAGE_BY_APPLICANT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                        
Proc Name       : PROC_UPDATE_HOME_COVERAGE_BY_APPLICANT                
Created by      : SHAFI               
Date            : 4 July 2006                                 
Purpose         :UPDATE COVERAGE OF CO-APPLICANT  ON BASES OCCUPATION  
  
Modified By  :Pravesh K chandel  
Modified Date :28 JUly 09  
Purpose   : Add app id and app version id to handle if Co applicant changed on application  
Revison History :                                        
Used In  : Wolverine                                        
------------------------------------------------------------                                        
Date     Review By          Comments                                        
------   ------------       -------------------------*/                             
--DROP PROC Dbo.PROC_UPDATE_HOME_COVERAGE_BY_APPLICANT                               
CREATE           PROC [dbo].[PROC_UPDATE_HOME_COVERAGE_BY_APPLICANT]                    
(                                        
 @CUSTOMER_ID     int,                                        
 @APPLICANT_ID     int,  
 @APP_ID  INT   =NULL,  
@APP_VERSION_ID INT =NULL        
)                     
                    
AS                     
                    
BEGIN    
 DECLARE @APPID INT  
 DECLARE @APPVERSIONID INT  
 DECLARE @DWELLING INT  
 DECLARE @COVERAGECODE VARCHAR(20)  
 SET @COVERAGECODE='PERIJ'      
 DECLARE  @HO82 int  
  
/*  
 if occupation staisfies the condition then delete the Ho-82 from all application where this co-applicant exists  
*/  
IF EXISTS (  
   SELECT  APP.APPLICANT_ID   FROM   
   CLT_APPLICANT_LIST CLT WITH(NOLOCK) INNER JOIN APP_APPLICANT_LIST  APP WITH(NOLOCK)  
   ON CLT.APPLICANT_ID=APP.APPLICANT_ID  
   WHERE  
   CLT.CUSTOMER_ID=@CUSTOMER_ID AND   
   CLT.APPLICANT_ID=@APPLICANT_ID AND   
   APP.APP_ID=ISNULL(@APP_ID,APP.APP_ID) AND -- ADDED BY PRAVESH ON 28 JULY 09  
   APP.APP_VERSION_ID=ISNULL(@APP_VERSION_ID,APP.APP_VERSION_ID) AND  -- ADDED BY PRAVESH ON 28 JULY 09  
   CLT.CO_APPLI_OCCU IN(280,250,275,11817,1181,432,11825,11818,11819,561,11820,11821,11822,11823,11824,602,607) AND  
            ISNULL(CLT.IS_ACTIVE,'Y')='Y'  
          )  
 BEGIN     
    
     DECLARE APP_CURSOR CURSOR                
     FOR                 
      SELECT DWE.CUSTOMER_ID, DWE.APP_ID,DWE.APP_VERSION_ID,DWE.DWELLING_ID FROM APP_DWELLINGS_INFO  DWE WITH(NOLOCK)  
              INNER JOIN APP_APPLICANT_LIST APP ON   
                        DWE.CUSTOMER_ID=APP.CUSTOMER_ID AND   
                        DWE.APP_ID =APP.APP_ID AND   
                        DWE.APP_VERSION_ID=APP.APP_VERSION_ID   
               
    WHERE          
             APP.CUSTOMER_ID=@CUSTOMER_ID AND   
    APP.APPLICANT_ID=@APPLICANT_ID   
    AND APP.APP_ID=ISNULL(@APP_ID,APP.APP_ID)  -- ADDED BY PRAVESH ON 28 JULY 09  
    AND APP.APP_VERSION_ID=ISNULL(@APP_VERSION_ID,APP.APP_VERSION_ID)  -- ADDED BY PRAVESH ON 28 JULY 09  
  
  
         OPEN APP_CURSOR                
       
         FETCH NEXT FROM APP_CURSOR                
      INTO @CUSTOMER_ID,@APPID,@APPVERSIONID ,@DWELLING  
  
   WHILE @@FETCH_STATUS = 0                
   BEGIN   
   EXEC Proc_DeleteHomeCoverage @CUSTOMER_ID,@APPID,@APPVERSIONID,@DWELLING,@COVERAGECODE  
            FETCH NEXT FROM APP_CURSOR                
   INTO @CUSTOMER_ID,@APPID,@APPVERSIONID ,@DWELLING  
     
     
   END   
    
  CLOSE  APP_CURSOR                
  DEALLOCATE APP_CURSOR           
        
     
     
   
  END  
ELSE  
/*  
 if occupation  does not staisfies condition then if ho-24 is taken default Ho-82 for all application where this co-applicant exists  
*/  
  
 BEGIN  
       DECLARE APP_CURSOR CURSOR                
     FOR                 
      SELECT  DWE.CUSTOMER_ID, DWE.APP_ID,DWE.APP_VERSION_ID,DWE.DWELLING_ID   
             FROM APP_DWELLING_SECTION_COVERAGES DWE  
              INNER JOIN APP_APPLICANT_LIST APP ON   
                        DWE.CUSTOMER_ID=APP.CUSTOMER_ID AND   
                DWE.APP_ID =APP.APP_ID AND   
                        DWE.APP_VERSION_ID=APP.APP_VERSION_ID   
                INNER JOIN MNT_COVERAGE  MNT ON DWE.COVERAGE_CODE_ID=MNT.COV_ID  
             WHERE          
             APP.CUSTOMER_ID=@CUSTOMER_ID AND   
    APP.APPLICANT_ID=@APPLICANT_ID   
    AND APP.APP_ID=ISNULL(@APP_ID,APP.APP_ID)      -- ADDED BY PRAVESH ON 28 JULY 09  
    AND APP.APP_VERSION_ID=ISNULL(@APP_VERSION_ID,APP.APP_VERSION_ID)  -- ADDED BY PRAVESH ON 28 JULY 09  
                AND MNT.COV_CODE='EBP24'  
  
         OPEN APP_CURSOR    
  
        FETCH NEXT FROM APP_CURSOR                
      INTO @CUSTOMER_ID,@APPID,@APPVERSIONID ,@DWELLING  
      
   WHILE @@FETCH_STATUS = 0                
   BEGIN   
    EXEC  @HO82 =  Proc_GetCOVERAGE_ID @CUSTOMER_ID,      
                    @APPID,                                                              
                    @APPVERSIONID,                    
                    @COVERAGECODE  
             IF ( @HO82 = 0 )                                                        
      BEGIN                                                        
    RAISERROR ('COV_ID not found for  Personal Ho-82',                                                        
    16, 1)                                                        
               END      
             ELSE  
               BEGIN  
     exec Proc_SAVE_HOME_COVERAGES  
                     @CUSTOMER_ID,                            
                     @APPID,                                                              
                     @APPVERSIONID,   
                     @DWELLING,  
                     -1,  
                     @HO82,  
                     NULL,  
                     NULL,  
                     NULL,  
                     NULL,  
                     NULL,  
                     NULL,  
                     NULL,  
                     NULL,  
                     NULL,  
                     NULL,  
                     "S2",  
                     NULL,  
                     NULL,  
                     NULL,   
                     NULL,  
                     NULL,  
                     NULL,  
                     NULL,  
                     NULL   
                    END   
           FETCH NEXT FROM APP_CURSOR                
   INTO @CUSTOMER_ID,@APPID,@APPVERSIONID ,@DWELLING  
     
   END   
    
  CLOSE  APP_CURSOR                
  DEALLOCATE APP_CURSOR       
     
 END  
  
    
    
           
END                                       
                  
                
GO

