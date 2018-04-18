  
 /*----------------------------------------------------------                              
 Proc Name       : dbo.Proc_GetPolicyPremiumXML                    
 Created by      : Lalit Chauhan              
 Date            : May 26, 2010                
 Purpose         : Get Policy Coverages Premium Sum              
 Revison History :                              
 Used In     : Ebix Advantage                              
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------              
Drop Proc Proc_GetPolicyCoveragePremiumSum   28070, 363, 1,16,      
      select a.susep_lob_code as ae,a.* from mnt_lob_master a where lob_ID > 8 order by a.susep_lob_code        
*/                              
alter PROC [dbo].[Proc_GetPolicyCoveragePremiumSum]              
(                              
  @CUSTOMER_ID  INT,                        
  @POLICY_ID  INT,                      
  @POLICY_VERSION_ID INT,              
  @LOB_ID INT ,          
  @PREMIUM DECIMAL(25,2) = NULL OUT,           
  @CALLED_FROM NVARCHAR(50) = NULL          
)                              
AS                              
BEGIN                   
 DECLARE @WRITTEN_PREMIUM  DECIMAL(25,2),@FEES DECIMAL(25,2)   ,@CHANGE_INFO DECIMAL(25,2) ,      
 @SUM_FUL_TERM_PREMIUM DECIMAL(25,2)      
            
     IF(@LOB_ID > 8)          
   BEGIN          
     IF (@LOB_ID in (9,26) )  ---All Risks and Named Perils ,Engineering risk               
       BEGIN                
       SELECT @WRITTEN_PREMIUM = SUM(ISNULL(WRITTEN_PREMIUM,0))  ,      
       @CHANGE_INFO = SUM(ISNULL(CHANGE_INFORCE_PREM,0)),      
       @SUM_FUL_TERM_PREMIUM =  SUM(ISNULL(POL_COV.FULL_TERM_PREMIUM,0))         
       FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)                  
      LEFT OUTER JOIN                    
        POL_PERILS POL_RISKINFO WITH(NOLOCK) ON                      
         POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND                 
         POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND                
         POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND                 
         POL_COV.RISK_ID=POL_RISKINFO.PERIL_ID       
         --AND             
      -- ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'       --remove is active risk condition ,deactivated risk premium have refuncd premium to be billed.i-track # 1126     
      WHERE                     
       POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID                 
            
     END  --end named Perils              
                 
      ELSE IF (@LOB_ID in (10,11,12,14,16,19,25,27,32) ) --For Comprehensive Condominium,Comprehensive Company ,General Civil Liability  ,Diversified Risks ,Robbery,Traditional Fire,Global bank,judicial Guarantee        
      BEGIN        
        
   IF (@LOB_ID = 16)           
   BEGIN  
    DECLARE @SUB_LOB_ID INT  
    SELECT @SUB_LOB_ID = POLICY_SUBLOB FROM POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK) WHERE   CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND   
    POLICY_VERSION_ID = @POLICY_VERSION_ID  
      
     SELECT @WRITTEN_PREMIUM = SUM(ISNULL(WRITTEN_PREMIUM,0))           
     ,@CHANGE_INFO = SUM(ISNULL(CHANGE_INFORCE_PREM,0))         
       ,@SUM_FUL_TERM_PREMIUM =  SUM(ISNULL(POL_COV.FULL_TERM_PREMIUM,0))         
       FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)                  
      LEFT OUTER JOIN                    
    POL_PRODUCT_LOCATION_INFO POL_RISKINFO WITH(NOLOCK) ON                      
    POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND                 
    POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND                
    POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND                 
    POL_COV.RISK_ID=POL_RISKINFO.PRODUCT_RISK_ID  --AND            
      WHERE                     
      POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID                 
      /*select only main coverage for ppremium.only main coverages premium should billed.  
      sub coverages of main coverages premium shoul not include,Changed by lalit  May 24,2011.itrack  1052*/  
      AND COVERAGE_CODE_ID NOT IN(    
      SELECT COV_ID FROM MNT_COVERAGE WITH(NOLOCK) WHERE LOB_ID = @LOB_ID AND SUB_LOB_ID = @SUB_LOB_ID  
      AND COV_REF_CODE IN(SELECT COV_ID FROM MNT_COVERAGE WITH(NOLOCK) WHERE LOB_ID = @LOB_ID AND SUB_LOB_ID = @SUB_LOB_ID))  
   END  
   ELSE   
     SELECT @WRITTEN_PREMIUM = SUM(ISNULL(WRITTEN_PREMIUM,0))           
          ,@CHANGE_INFO = SUM(ISNULL(CHANGE_INFORCE_PREM,0))         
   ,@SUM_FUL_TERM_PREMIUM =  SUM(ISNULL(POL_COV.FULL_TERM_PREMIUM,0))         
           FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)                  
       LEFT OUTER JOIN                    
        POL_PRODUCT_LOCATION_INFO POL_RISKINFO WITH(NOLOCK) ON                      
        POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND                 
        POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND                
        POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND                 
        POL_COV.RISK_ID=POL_RISKINFO.PRODUCT_RISK_ID  --AND            
       -- ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'    
  WHERE                     
      POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID                 
        
       END                  
                 
    ELSE IF (@LOB_ID=13)   ---For maritime              
       BEGIN                
     SELECT @WRITTEN_PREMIUM = SUM(ISNULL(WRITTEN_PREMIUM,0))        
     ,@CHANGE_INFO = SUM(ISNULL(CHANGE_INFORCE_PREM,0))            
           ,@SUM_FUL_TERM_PREMIUM =  SUM(ISNULL(POL_COV.FULL_TERM_PREMIUM,0))         
           FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)                
      LEFT OUTER JOIN                    
      POL_MARITIME POL_RISKINFO WITH(NOLOCK) ON                      
        POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND                 
        POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND                
        POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND                 
        POL_COV.RISK_ID=POL_RISKINFO.MARITIME_ID -- AND            
       -- ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'                
     WHERE                     
      POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID                 
                 
       END  --end meritime              
                     
    ELSE IF (@LOB_ID=20 or @LOB_ID=23)  --For National & international Carogo transport              
       BEGIN               
      SELECT @WRITTEN_PREMIUM = SUM(ISNULL(WRITTEN_PREMIUM,0))          
      ,@CHANGE_INFO = SUM(ISNULL(CHANGE_INFORCE_PREM,0))          
      ,@SUM_FUL_TERM_PREMIUM =  SUM(ISNULL(POL_COV.FULL_TERM_PREMIUM,0))         
            FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)             
      LEFT OUTER JOIN                    
       POL_COMMODITY_INFO POL_RISKINFO WITH(NOLOCK) ON                      
         POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND                 
         POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND                
         POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND                 
         POL_COV.RISK_ID=POL_RISKINFO.COMMODITY_ID   --AND            
         --ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'                      
       WHERE                     
        POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID                 
                      
       END     --END National & international cargo transport              
                       
    ELSE IF (@LOB_ID in (15,21,33,34))  --For Individual Personal Accident info,Dpvat(Cat. 3 e 4),Mortgage ,Group life             
       BEGIN               
      SELECT @WRITTEN_PREMIUM = SUM(ISNULL(WRITTEN_PREMIUM,0))           
      ,@CHANGE_INFO = SUM(ISNULL(CHANGE_INFORCE_PREM,0))         
      ,@SUM_FUL_TERM_PREMIUM =  SUM(ISNULL(POL_COV.FULL_TERM_PREMIUM,0))         
       FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)              
      LEFT OUTER JOIN                    
         POL_PERSONAL_ACCIDENT_INFO POL_RISKINFO WITH(NOLOCK) ON                      
         POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND                 
         POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND                
         POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND                 
         POL_COV.RISK_ID=POL_RISKINFO.PERSONAL_INFO_ID   --  AND             
         --ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'                    
       WHERE                     
        POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID                 
                     
       END     --END National & international cargo transport              
                        
    ELSE IF (@LOB_ID in(17,18,28,29,30,31,36))  --For Facultative Liability AND Civil Liability Transportation ,Aeronautic,Motor,Cargo Transportation civil Liability/DpVat 3 e 4/DPVAT(Cat.1,2,9 e 10)        
       BEGIN               
      SELECT @WRITTEN_PREMIUM = SUM(ISNULL(WRITTEN_PREMIUM,0))           
      ,@CHANGE_INFO = SUM(ISNULL(CHANGE_INFORCE_PREM,0))      
      ,@SUM_FUL_TERM_PREMIUM =  SUM(ISNULL(POL_COV.FULL_TERM_PREMIUM,0))         
          FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)              
      LEFT OUTER JOIN                    
         POL_CIVIL_TRANSPORT_VEHICLES  POL_RISKINFO WITH(NOLOCK) ON                      
         POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND                 
         POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND                
         POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND                 
         POL_COV.RISK_ID=POL_RISKINFO.VEHICLE_ID     -- AND             
       --  ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'                   
       WHERE                     
        POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID                 
                  
       END     --END Facultative Liability and Civil Liability Transportation              
                
                             
    ELSE IF (@LOB_ID=22 )  --For Personal Accident for Passengers              
       BEGIN               
      SELECT @WRITTEN_PREMIUM = SUM(ISNULL(WRITTEN_PREMIUM,0))           
      ,@CHANGE_INFO = SUM(ISNULL(CHANGE_INFORCE_PREM,0))      
      ,@SUM_FUL_TERM_PREMIUM =  SUM(ISNULL(POL_COV.FULL_TERM_PREMIUM,0))         
             FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)             
      LEFT OUTER JOIN                    
         POL_PASSENGERS_PERSONAL_ACCIDENT_INFO POL_RISKINFO WITH(NOLOCK) ON                      
         POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND                 
         POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND                
         POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND                 
         POL_COV.RISK_ID=POL_RISKINFO.PERSONAL_ACCIDENT_ID   -- AND             
         --ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'                     
       WHERE                     
        POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID                 
                     
       END     --END Personal Accident for Passengers          
               
     ELSE IF (@LOB_ID in (35,37) )  --Rural Lien         
       BEGIN                 
              
      SELECT @WRITTEN_PREMIUM = SUM(ISNULL(WRITTEN_PREMIUM,0))           
      ,@CHANGE_INFO = SUM(ISNULL(CHANGE_INFORCE_PREM,0))      
      ,@SUM_FUL_TERM_PREMIUM =  SUM(ISNULL(POL_COV.FULL_TERM_PREMIUM,0))         
             FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)             
      LEFT OUTER JOIN                    
         POL_PENHOR_RURAL_INFO POL_RISKINFO WITH(NOLOCK) ON                      
         POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND                 
         POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND                
         POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND                 
         POL_COV.RISK_ID=POL_RISKINFO.PENHOR_RURAL_ID   -- AND             
       --  ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'                     
       WHERE                     
        POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID                 
                     
       END     --END Rural Lien           
                   
   END          
  ELSE          
   BEGIN               
    EXEC Proc_GetProductsCoveragesPremium @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@LOB_ID,@WRITTEN_PREMIUM OUT,@FEES OUT          
   END            
             
  SET @PREMIUM = @WRITTEN_PREMIUM          
            
  IF(@CALLED_FROM <> 'RULES' OR  @CALLED_FROM IS NULL)          
  BEGIN          
    SELECT @WRITTEN_PREMIUM AS TOTAL_COVERAGE_PREMIUM,       
    @FEES AS TOTAL_STATE_FEES,          
       POL_LIST.INSTALL_PLAN_ID,          
       POL_LIST.POLICY_CURRENCY,          
    POL_LIST.APP_TERMS AS APP_TERMS ,          
    POL_LIST.BILL_TYPE AS BILL_TYPE,          
    ISNULL(POL_LIST.POLICY_EFFECTIVE_DATE,APP_EFFECTIVE_DATE) AS POLICY_EFFECTIVE_DATE,            
    ISNULL(POL_LIST.POLICY_EXPIRATION_DATE,APP_EXPIRATION_DATE) AS POLICY_EXPIRATION_DATE,        
    POL_LIST.CO_INSURANCE AS CO_INSURANCE        
    ,ISNULL(@CHANGE_INFO,0) AS  CHANGE_INFO_PREMIUM      
    ,ISNULL(@SUM_FUL_TERM_PREMIUM ,0) AS TOTAL_INFO_PREMIUM    ,  
    ACPD.PLAN_TYPE AS PLAN_TYPE, -- changed by praveer for itrack no 1567  
    MLM.IOF_PERCENTAGE AS IOF_PERCENTAGE -- changed by praveer for itrack no 1761  
    FROM POL_CUSTOMER_POLICY_LIST POL_LIST  WITH(NOLOCK)   
      LEFT OUTER JOIN MNT_LOB_MASTER MLM WITH(NOLOCK) ON MLM.LOB_ID=POL_LIST.POLICY_LOB -- changed by praveer for itrack no 1761  
 LEFT OUTER JOIN ACT_INSTALL_PLAN_DETAIL ACPD WITH(NOLOCK)  ON ACPD.IDEN_PLAN_ID=POL_LIST.INSTALL_PLAN_ID -- changed by praveer for itrack no 1567  
    WHERE POL_LIST.CUSTOMER_ID = @CUSTOMER_ID AND           
       POL_LIST.POLICY_ID = @POLICY_ID AND           
       POL_LIST.POLICY_VERSION_ID = @POLICY_VERSION_ID               
                  
         END          
   IF(@@ERROR<>0)            
   RETURN -1            
               
END   