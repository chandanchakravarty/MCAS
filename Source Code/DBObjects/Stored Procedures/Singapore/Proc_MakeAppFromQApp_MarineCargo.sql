IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MakeAppFromQApp_MarineCargo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MakeAppFromQApp_MarineCargo]
GO
CREATE PROC [dbo].[Proc_MakeAppFromQApp_MarineCargo] --28962,580,1,'M8998347APP'                   
(                    
@CUSTOMER_ID INT,                    
@POLICY_ID INT,                     
@POLICY_VERSION_ID SMALLINT,                   
@APP_NUMBER NVARCHAR(75)                   
)                    
AS                    
BEGIN                    
 DECLARE @QQ_ID INT                    
 DECLARE @QQ_NUMBER VARCHAR(20)                    
 DECLARE @AGENCY_ID INT                 
                 
 IF not EXISTS(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE                    
 CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)                 
 BEGIN                
 UPDATE POL_CUSTOMER_POLICY_LIST SET CUSTOMER_ID = @CUSTOMER_ID WHERE                    
  CUSTOMER_ID = -100 AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                  
 END                  
 IF not EXISTS(SELECT CUSTOMER_ID FROM QQ_INVOICE_PARTICULAR_MARINE WITH(NOLOCK) WHERE                    
 CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)                 
 BEGIN                
 UPDATE QQ_INVOICE_PARTICULAR_MARINE SET CUSTOMER_ID = @CUSTOMER_ID                    
  WHERE CUSTOMER_ID=-100 and  POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                  
 END                 
          
--Added by kuldeep on 23-mar-2012
IF not EXISTS(SELECT CUSTOMER_ID FROM QQ_MARINECARGO_RISK_DETAILS    WITH(NOLOCK) WHERE                    
 CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)                 
 BEGIN                
 UPDATE QQ_MARINECARGO_RISK_DETAILS SET CUSTOMER_ID = @CUSTOMER_ID                    
  WHERE CUSTOMER_ID=-100 and  POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                  
 END 
 --till here                 
 IF not EXISTS(SELECT CUSTOMER_ID FROM CLT_QUICKQUOTE_LIST WITH(NOLOCK) WHERE                    
 CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@POLICY_ID AND APP_VERSION_ID=@POLICY_VERSION_ID)                 
 BEGIN                
UPDATE CLT_QUICKQUOTE_LIST SET QQ_APP_NUMBER= @APP_NUMBER,CUSTOMER_ID = @CUSTOMER_ID WHERE                    
  CUSTOMER_ID = -100 AND APP_ID=@POLICY_ID AND APP_VERSION_ID=@POLICY_VERSION_ID                    
 END                 
  --ADDED BY KULDEEP ON 14/1/2012 WHEN QUOTE IS NOT DISPLAYED AFTER MAKE APP          
  SELECT @QQ_ID=QQ_ID FROM CLT_QUICKQUOTE_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@POLICY_ID AND APP_VERSION_ID=@POLICY_VERSION_ID          
  IF not EXISTS(SELECT CUSTOMER_ID FROM QQ_CUSTOMER_PARTICULAR WITH(NOLOCK) WHERE                    
 CUSTOMER_ID = @CUSTOMER_ID AND QUOTE_ID=@QQ_ID)                 
 BEGIN                
UPDATE QQ_CUSTOMER_PARTICULAR SET CUSTOMER_ID = @CUSTOMER_ID WHERE                    
  CUSTOMER_ID = -100 AND   QUOTE_ID=@QQ_ID                 
 END            
  --TILL HERE            
             
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE                    
 CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)                    
 BEGIN                    
                     
                        
  UPDATE POL_CUSTOMER_POLICY_LIST SET APP_STATUS = 'APPLICATION', APP_NUMBER = @APP_NUMBER WHERE                    
  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                    
                      
                     
                      
  UPDATE CLT_QUICKQUOTE_LIST SET QQ_APP_NUMBER= @APP_NUMBER WHERE                    
  CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@POLICY_ID AND APP_VERSION_ID=@POLICY_VERSION_ID                    
                      
                     
                        
   IF EXISTS(SELECT CUSTOMER_ID FROM QOT_CUSTOMER_QUOTE_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@POLICY_ID AND APP_VERSION_ID=@POLICY_VERSION_ID)                    
   BEGIN                    
                      
    DECLARE @QUOTE_VERSION_ID smallint                                           
    DECLARE @QUOTE_NUMBER nvarchar(75)                     
    DECLARE @QUOTE_ID int                    
                      
    SELECT @QUOTE_ID = ISNULL(MAX(QUOTE_ID),0) + 1 from QOT_CUSTOMER_QUOTE_LIST_POL    WITH(NOLOCK)                            
    WHERE CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID ;                                       
                      
                          
       -- declare @QUOTE_NUMBER nvarchar(75)                               
    SELECT @QUOTE_NUMBER= 'Q-' + APP_NUMBER                                         
    FROM POl_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID  and POLICY_ID =@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                     
                       
    INSERT INTO [dbo].[QOT_CUSTOMER_QUOTE_LIST_POL](CUSTOMER_ID,QUOTE_ID,QUOTE_VERSION_ID,POLICY_ID,POLICY_VERSION_ID,                                          
    QUOTE_TYPE,QUOTE_NUMBER,QUOTE_DESCRIPTION,IS_ACCEPTED,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,QUOTE_XML,QUOTE_INPUT_XML,                          
    RATE_EFFECTIVE_DATE,BUSINESS_TYPE )                    
    SELECT @CUSTOMER_ID,@QUOTE_ID,1,@POLICY_ID,@POLICY_VERSION_ID,QUOTE_TYPE,@QUOTE_NUMBER,QUOTE_DESCRIPTION,IS_ACCEPTED,IS_ACTIVE,                    
    CREATED_BY,CREATED_DATETIME,QUOTE_XML,QUOTE_INPUT_XML,RATE_EFFECTIVE_DATE,BUSINESS_TYPE                     
    FROM QOT_CUSTOMER_QUOTE_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@POLICY_ID AND APP_VERSION_ID=@POLICY_VERSION_ID                    
   END                    
                      
                       
                      
  --SELECT @QQ_ID = ISNULL(MAX(QQ_ID),0)+1 FROM CLT_QUICKQUOTE_LIST WITH(NOLOCK)                    
                      
  --INSERT INTO [dbo].[CLT_QUICKQUOTE_LIST]                    
  --     ([CUSTOMER_ID],[QQ_ID],[QQ_NUMBER],[QQ_TYPE],[QQ_XML],[QQ_APP_NUMBER],[IS_ACTIVE],[QQ_RATING_REPORT],[QQ_XML_TIME]                    
  --       ,[QQ_RATING_TIME],[QQ_STATE],[APP_ID],[APP_VERSION_ID])                            
  --      SELECT CUSTOMER_ID, @QQ_ID, @QQ_NUMBER, NULL, NULL, APP_NUMBER, 'Y', NULL, NULL, NULL, NULL, POLICY_ID, POLICY_VERSION_ID                    
  --      FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                    
                            
  --       --Maintain co-applicant details                    
           --UNCOMMENT BY KULDEEP                 
         DECLARE @APPLICANT_ID INT                     
   DECLARE @IS_PRIMARY_APPLICANT INT                    
                       
   SET @APPLICANT_ID=0                                  
   SET @IS_PRIMARY_APPLICANT=0                              
                                  
   SELECT @APPLICANT_ID = APPLICANT_ID                                   
   FROM CLT_APPLICANT_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND IS_PRIMARY_APPLICANT = 1                                  
                                    
                                  
  IF (@APPLICANT_ID > 0)                                  
  BEGIN                                  
                        
   SET @IS_PRIMARY_APPLICANT=1                              
  END                             
                            
   IF NOT EXISTS(SELECT CUSTOMER_ID FROM POL_APPLICANT_LIST  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)                    
   BEGIN                            
   INSERT INTO POL_APPLICANT_LIST                                                                                                     
   (                                                         
    POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID, APPLICANT_ID,CREATED_DATETIME,IS_PRIMARY_APPLICANT                                      
   )                         
   VALUES                                                                                                                               
   (@POLICY_ID,@POLICY_VERSION_ID,@CUSTOMER_ID,@APPLICANT_ID,GETDATE(),@IS_PRIMARY_APPLICANT)                                      
   END                     
            --TILL HERE                
        --Maintain renumeration tab for quick application                 
         SELECT @AGENCY_ID = CUSTOMER_AGENCY_ID FROM CLT_CUSTOMER_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID                    
   IF (ISNULL(@AGENCY_ID,0) <> 0 )                    
    exec Proc_UpdateDefaultBroker @AGENCY_ID,@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,-1                    
    --Setting Commission Default to 100% this only on temporary basis (by kuldeep 10_jan_2012) to commit the policy and bypass the rule                
    update POL_REMUNERATION set COMMISSION_PERCENT=100 WHERE CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID ;                
                 
   --For Vehicle info  BY  KULDEEP ON 7_JAN_2012                  
   DECLARE @MARITIME_ID SMALLint      
   DECLARE @VOYAGE_INFO_ID SMALLint                 
   DECLARE @VOYAGE_FROM nvarchar(4)                  
   DECLARE @VOYAGE_TO nvarchar(75)                  
   DECLARE @VESSEL nvarchar(75)                  
   DECLARE @IS_ACTIVE nchar(1)                  
   DECLARE @THENCE_TO nvarchar(50)                  
   DECLARE @CREATED_BY INT      
   DECLARE @QUANTITY_DESCRIPTION NVARCHAR(50)    
      
   SELECT @MARITIME_ID= ISNULL(MAX(MARITIME_ID),0) + 1 from POL_MARITIME    WITH(NOLOCK)                            
    WHERE CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID    
        
    --print @MARITIME_ID    
        
    SELECT @VOYAGE_INFO_ID= ISNULL(MAX(VOYAGE_INFO_ID),0) + 1 from POL_MARINECARGO_VOYAGE_INFORMATION    WITH(NOLOCK)                            
    --WHERE CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID    
        
    --print @VOYAGE_INFO_ID    
                     
   SELECT @VOYAGE_FROM=VOYAGE_FROM,@VOYAGE_TO=VOYAGE_TO,@VESSEL=VESSEL,@THENCE_TO=THENCE_TO,@QUANTITY_DESCRIPTION=QUANTITY_DESCRIPTION    
   FROM QQ_MARINECARGO_RISK_DETAILS                  
   WHERE CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID     
       
                    
  IF NOT EXISTS(SELECT * FROM POL_MARITIME WHERE CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and MARITIME_ID = @MARITIME_ID)                
  BEGIN                
   INSERT INTO [POL_MARITIME]                  
           ([POLICY_ID]                  
           ,[POLICY_VERSION_ID]                  
           ,[CUSTOMER_ID]                  
           ,[MARITIME_ID]      
           ,[VESSEL_NUMBER]                
           ,[NAME_OF_VESSEL]         
           ,[IS_ACTIVE])            
           
     VALUES                  
           (@POLICY_ID,@POLICY_VERSION_ID,@CUSTOMER_ID, @MARITIME_ID,@MARITIME_ID,@VESSEL, 'Y')                  
 --           EXEC Proc_UpdateRisk_Renumeration                      
 --   @CUSTOMER_ID,                         
 --@POLICY_ID   ,                        
 --@POLICY_VERSION_ID,                      
 --@MARITIME_ID,                    
 --@CREATED_BY                  
END                
    
    
IF NOT EXISTS(SELECT * FROM  POL_MARINECARGO_VOYAGE_INFORMATION WHere VOYAGE_INFO_ID = @VOYAGE_INFO_ID)              
  BEGIN                
   INSERT INTO [POL_MARINECARGO_VOYAGE_INFORMATION]                  
           ([POLICY_ID]                  
           ,[POLICY_VERSION_ID]                  
           ,[CUSTOMER_ID]                  
           ,[VOYAGE_INFO_ID]    
           ,[RISK_ID]                  
           ,[VOYAGE_FROM]      
           ,[VOYAGE_TO]       
           ,[IS_ACTIVE]    
           ,[THENCE_TO]    
           ,[QUANTITY_DESCRIPTION]    
           )            
           
     VALUES                  
           (@POLICY_ID,@POLICY_VERSION_ID,@CUSTOMER_ID, @VOYAGE_INFO_ID, @VOYAGE_INFO_ID,@VOYAGE_FROM,@VOYAGE_TO,'Y',@THENCE_TO,@QUANTITY_DESCRIPTION )                  
 --           EXEC Proc_UpdateRisk_Renumeration                      
 --   @CUSTOMER_ID,                         
 --@POLICY_ID   ,                        
 --@POLICY_VERSION_ID,                      
 --@MARITIME_ID,                    
 --@CREATED_BY                  
END    
--TILL HERE                  
 END                 
                    
END 