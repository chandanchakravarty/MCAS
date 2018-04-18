  
-- =============================================    
-- Author:  <Author,,Neha>    
-- Create date: <Create Date,15-feb-2010,>    
-- Description: <Description,RITreatiesForPolicy,> 28075,4,1  
-- drop proc   Proc_RITreatiesForPolicy 28075,5,1  @LANG_ID INT   =1   
  
-- =============================================    
--exec Proc_RITreatiesForPolicy 28070,907,1,1   
ALTER PROCEDURE [dbo].[Proc_RITreatiesForPolicy]   
(      
  @CUSTOMER_ID INT ,      
  @POLICY_ID INT ,      
  @POLICY_VERSION_ID SMALLINT,  
  @LANG_ID INT   =3  
  )     
     
AS    
BEGIN    
  
DECLARE @PORTABLE_EQUIP INT   
  
--if exists(select * from POL_POLICY_PROCESS where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID   
--and NEW_POLICY_VERSION_ID= @POLICY_VERSION_ID and PROCESS_STATUS='COMPLETE')  
--BEGIN   
  
IF EXISTS(SELECT * FROM POL_PRODUCT_LOCATION_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID  AND POLICY_VERSION_ID= @POLICY_VERSION_ID)        
BEGIN  
SELECT @PORTABLE_EQUIP=PORTABLE_EQUIPMENT  FROM POL_PRODUCT_LOCATION_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID  AND POLICY_VERSION_ID= @POLICY_VERSION_ID   
END  
  
  SELECT         
   ISNULL(MRCL.REIN_COMAPANY_NAME,'&nbsp;') AS 'REIN_COMAPANY_NAME',      
   ISNULL(CONTRACT_NUMBER,'&nbsp;') AS 'CONTRACT',       
     
   dbo.fun_FormatCurrency( PRBD.TRAN_PREMIUM , @LANG_ID )TRAN_PREMIUM,  
              
   dbo.fun_FormatCurrency  (PRBD.REIN_PREMIUM ,@LANG_ID) REIN_PREMIUM,       
          
   --dbo.fun_FormatCurrency (PRBD.REIN_CEDED ,@LANG_ID) REIN_CEDED,    
   PRBD.RISK_ID ,  
   dbo.fun_FormatCurrency (PRBD.LAYER_AMOUNT, @LANG_ID) LAYER_AMOUNT,  
   ISNULL (round(convert(Decimal(10,3),PRBD.RETENTION_PER),3),0)AS 'RETENTION_PER', --Changed by Aditya for TFS BUG # 165     
   Round(convert(Decimal(10,3), PRBD.COMM_PERCENTAGE),3)COMM_PERCENTAGE ,   --Changed by Aditya for TFS BUG # 165  
   dbo.fun_FormatCurrency (PRBD.LAYER ,  @LANG_ID) LAYER,     
   dbo.fun_FormatCurrency (PRBD.COMM_AMOUNT ,  @LANG_ID)COMM_AMOUNT ,    
   dbo.fun_FormatCurrency(  PRBD.TOTAL_INS_VALUE  ,  @LANG_ID)TOTAL_INS_VALUE,   
   round( convert(Decimal(10,2),PRBD.RATE), 2)CONTRACT_COMM_PERCENTAGE ,  --Changed by Aditya for TFS BUG # 165  
   dbo.fun_FormatCurrency( PRBD.REIN_CEDED,@LANG_ID)CESSION_AMOUNT_LAYER  
    
  FROM       
   POL_REINSURANCE_BREAKDOWN_DETAILS PRBD WITH(NOLOCK)       
   INNER JOIN       
   POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK) ON       
   PRBD.CUSTOMER_ID = PCPL.CUSTOMER_ID AND      
   PRBD.POLICY_ID = PCPL.POLICY_ID AND      
   PRBD.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID      
   LEFT JOIN      
   MNT_REIN_COMAPANY_LIST MRCL WITH(NOLOCK) ON MRCL.REIN_COMAPANY_ID=PRBD.MAJOR_PARTICIPANT           
   WHERE     
   PCPL.CUSTOMER_ID = @CUSTOMER_ID       
   AND PCPL.POLICY_ID = @POLICY_ID       
   AND PCPL.POLICY_VERSION_ID = @POLICY_VERSION_ID    
   ORDER BY CASE WHEN @PORTABLE_EQUIP='10964' THEN @LANG_ID  ELSE CAST(PRBD.RISK_ID AS INT) END  
   --ORDER BY PRBD.RISK_ID ASC --- changes by praveer for itrack no 1501  
 END    
  --END   
    