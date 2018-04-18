  
 --exec  [Proc_InsertMarineQQCustomerGenInfo] 9999,11109,'xyz co op',256,926,'2012-03-23','Y'                  
                                            
CREATE   PROC [dbo].[Proc_InsertMarineQQCustomerGenInfo]                                                        
(                                                        
	@CUSTOMER_ID     int  OUTPUT, 
	@CUSTOMER_TYPE VARCHAR(40),                                                       
	@CUSTOMER_FIRST_NAME VARCHAR(40),
	@CUSTOMER_BUSINESS_TYPE VARCHAR(40),
	@CREATED_BY   INT,  
	@CREATED_DATETIME DATETIME,                                                        
    @IS_ACTIVE     CHAR(1)                             
)                                                        
AS                                                  
BEGIN                                                        
                                           
 --if Exists(SELECT CUSTOMER_CODE FROM CLT_CUSTOMER_LIST WITH(NOLOCK) WHERE CUSTOMER_CODE = LTRIM(RTRIM(@CUSTOMER_CODE)))                                                        
 --BEGIN                                                        
 -- SELECT @CUSTOMER_ID = -1                                                        
 --END                                                        
 --ELSE                                                        
 --  BEGIN                                                       
   INSERT INTO CLT_CUSTOMER_LIST                                                        
   (                                                        
     CUSTOMER_TYPE,                             
     CUSTOMER_FIRST_NAME,                                                        
                          
     CUSTOMER_BUSINESS_TYPE,                                                        
                                             
     CREATED_BY,                                           
     CREATED_DATETIME,                                                        
     IS_ACTIVE                                                   
                                       
   )                                                        
   VALUES                                              
   (                                                         
     @CUSTOMER_TYPE,                                                        
                                         
     @CUSTOMER_FIRST_NAME,                                                        
                                                 
     @CUSTOMER_BUSINESS_TYPE,                                
                                                
     @CREATED_BY,                                                        
     @CREATED_DATETIME,                                          
     'Y'                                                       
                              
   )                                                        
                                                           
     SELECT @CUSTOMER_ID = Max(CUSTOMER_ID) FROM CLT_CUSTOMER_LIST WITH(NOLOCK)                                                   
    -- SELECT @Cust_Id= Max(CUSTOMER_ID) FROM CLT_CUSTOMER_LIST     WITH(NOLOCK)                                                 
                                                           
                                                           
                                
                                                           
    --- Added by mohit                                                         
    -- Inserting customer to clt_customer_list                                                       
    -- if customer is of type personal then only entry for that is to be made in CLT_APPLICANT_LIST.                                                       
    --11110                                                         
                                                          
     declare @APPLICANT_ID int                                                 
                                                  
    select @APPLICANT_ID=isnull(Max(APPLICANT_ID),0)+1 from CLT_APPLICANT_LIST  WITH(NOLOCK)                                 
    INSERT INTO CLT_APPLICANT_LIST                                                        
    ( 
     APPLICANT_ID,                                                            
     APPLICANT_TYPE,    
    
     CUSTOMER_ID,    
     FIRST_NAME,    
       CREATED_BY,
       CREATED_DATETIME,                                
     CO_APPLI_OCCU,    
     IS_ACTIVE     
               
     )                                                  
    VALUES                                                   
     ( @APPLICANT_ID,                                                           
      @CUSTOMER_TYPE,    
      
      @CUSTOMER_ID,    
      @CUSTOMER_FIRST_NAME,    
        @CREATED_BY,    
      @CREATED_DATETIME  ,
     @CUSTOMER_BUSINESS_TYPE,
      'Y'   
        
   )                                 
                        
   END  
  