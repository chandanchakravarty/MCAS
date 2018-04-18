IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateRemuneration]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateRemuneration]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                        
Proc Name       : dbo.[Proc_UpdateRemuneration]                
Created by      : LALIT CHAUHAN            
Date            : 04/26/2010                        
Purpose         :UPDATE RECORDS IN POL_REMUNERATION TABLE.                        
Revison History :                        
Used In        : Ebix Advantage                        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------        
DROP PROC dbo.[Proc_UpdateRemuneration]         
        
*/         
/****** Script for POL_REMUNERATION into DATABASE  ******/           
          
CREATE PROC [dbo].[Proc_UpdateRemuneration]                
(                
@CUSTOMER_ID  INT,    
@POLICY_ID INT,    
@POLICY_VERSION_ID INT,       
@REMUNERATION_ID  INT out ,                  
@COMMISSION_PERCENT numeric(8,4),                
@COMMISSION_TYPE INT,                
@BRANCH NUMERIC(8),           
@MODIFIED_BY INT,        
@LAST_UPDATED_DATETIME DATETIME ,               
@AMOUNT DECIMAL(12,2)=null,        
@LEADER INT,        
@NAME NVARCHAR(100)=null,        
@PRODUCT_RISK_ID INT=NULL,        
@CO_APPLICANT_ID INT = NULL,        
@BROKER_ID INT = NULL     
    
)                
AS                
 BEGIN        
DECLARE  @LOB_ID NVARCHAR(10),        
   @COMMISSION_LEVEL VARCHAR(2),    
   @SUM numeric(8,4)  ,    
   @leadercount int ,
   @COMM_PERCENT DECIMAL(8,4),
   @FEES_PERCENT DECIMAL(8,4),
   @PRO_LABORE_PERCENT DECIMAL(8,4) ,
   @TRANSACTION_TYPE INT   
   --@TransactionType int    
          
          
          
          
  --SELECT  @CUSTOMER_ID= CUSTOMER_ID,@POLICY_ID=POLICY_ID, @POLICY_VERSION_ID=POLICY_VERSION_ID FROM POL_REMUNERATION WHERE REMUNERATION_ID=@REMUNERATION_ID        
      
   --SELECT @TransactionType=TRANSACTION_TYPE  FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID     
   --AND   POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
       
   SELECT  @SUM=sum(COMMISSION_PERCENT)+@COMMISSION_PERCENT from POL_REMUNERATION WITH(NOLOCK) 
   WHERE CUSTOMER_ID=@CUSTOMER_ID  AND  POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID 
   and REMUNERATION_ID!=@REMUNERATION_ID  AND COMMISSION_TYPE=@COMMISSION_TYPE AND CO_APPLICANT_ID=@CO_APPLICANT_ID  
    
     select @COMM_PERCENT=COMMISSION_PERCENT,@FEES_PERCENT=FEES_PERCENT,
	  @PRO_LABORE_PERCENT=PRO_LABORE_PERCENT from POL_APPLICANT_LIST WITH(NOLOCK)
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND
      APPLICANT_ID=@CO_APPLICANT_ID   
      select @TRANSACTION_TYPE=TRANSACTION_TYPE  from POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID
           AND  POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
      
        
 IF(@SUM > 100 )--and @TransactionType<>14560 )    
 BEGIN        
      
 return -2    
     END    
         
     ELSE IF EXISTS(SELECT LEADER from POL_REMUNERATION with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND     
    POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND LEADER=10963 and     
    REMUNERATION_ID<>@REMUNERATION_ID and @LEADER=10963 and @TRANSACTION_TYPE in (14559,14561,14679) )     
    BEGIN           
    RETURN -3     
   END   
   ELSE IF EXISTS(SELECT LEADER from POL_REMUNERATION with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND     
    POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND LEADER=10963 and     
    REMUNERATION_ID<>@REMUNERATION_ID and @LEADER=10963 and @TRANSACTION_TYPE= 14560 and
     CO_APPLICANT_ID = @CO_APPLICANT_ID )     
    BEGIN           
    RETURN -3     
   END  
   ELSE IF EXISTS(SELECT BROKER_ID  from POL_REMUNERATION with(nolock)  WHERE CUSTOMER_ID=@CUSTOMER_ID
    AND   POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND BROKER_ID=@BROKER_ID 
    AND  COMMISSION_TYPE=@COMMISSION_TYPE and REMUNERATION_ID<>@REMUNERATION_ID 
    AND CO_APPLICANT_ID=@CO_APPLICANT_ID )
		  BEGIN
		  RETURN -4
		  END
		  
		ELSE IF(@COMMISSION_TYPE=43 AND @COMM_PERCENT=0 AND @TRANSACTION_TYPE=14560)
	      BEGIN
	         RETURN -5
	      END
	      
	      ELSE IF(@COMMISSION_TYPE=44 AND @FEES_PERCENT=0 AND @TRANSACTION_TYPE=14560)
	      BEGIN
	         RETURN -6
	      END
	        ELSE IF(@COMMISSION_TYPE=45 AND @PRO_LABORE_PERCENT=0 AND @TRANSACTION_TYPE=14560)
	      BEGIN
	         RETURN -7
	      END  
		  
        
 ELSE    
  BEGIN    
           
  SELECT @COMMISSION_LEVEL =  COMMISSION_LEVEL FROM MNT_LOB_MASTER  WITH(NOLOCK) WHERE LOB_ID=(SELECT POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID)        
        
 IF (UPPER(@COMMISSION_LEVEL)='RL')        
 BEGIN             
           
 IF (@LEADER=10963)        
 BEGIN        
  UPDATE POL_REMUNERATION SET LEADER=10964 WHERE CUSTOMER_ID=@CUSTOMER_ID       
  AND POLICY_ID=@POLICY_ID AND       
  POLICY_VERSION_ID=@POLICY_VERSION_ID AND BROKER_ID = @BROKER_ID AND  RISK_ID=isnull(@PRODUCT_RISK_ID,0)        -- changed for TFS#2765  
  AND CO_APPLICANT_ID=@CO_APPLICANT_ID         
 END         
                
UPDATE POL_REMUNERATION                
 SET                
COMMISSION_PERCENT=@COMMISSION_PERCENT,                
COMMISSION_TYPE=@COMMISSION_TYPE,                     
MODIFIED_BY=@MODIFIED_BY,            
BRANCH=@BRANCH,                
AMOUNT=@AMOUNT,        
NAME=@NAME,        
LEADER=@LEADER,        
LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME   ,        
CO_APPLICANT_ID = @CO_APPLICANT_ID,        
BROKER_ID= @BROKER_ID        
WHERE                
CUSTOMER_ID = @CUSTOMER_ID AND        
POLICY_ID = @POLICY_ID AND      
POLICY_VERSION_ID = @POLICY_VERSION_ID AND       
REMUNERATION_ID=@REMUNERATION_ID AND      
RISK_ID=isnull(@PRODUCT_RISK_ID,0)        -- changed for TFS#2765                  
END        
        
ELSE        
        
BEGIN        
IF (@LEADER=10963)        
 BEGIN        
  UPDATE POL_REMUNERATION SET LEADER=10964       
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID       
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID   AND       
  BROKER_ID = @BROKER_ID  AND CO_APPLICANT_ID=@CO_APPLICANT_ID  
 END         
                
UPDATE POL_REMUNERATION                
 SET                
COMMISSION_PERCENT=@COMMISSION_PERCENT,                
COMMISSION_TYPE=@COMMISSION_TYPE,                     
MODIFIED_BY=@MODIFIED_BY,            
BRANCH=@BRANCH,                
AMOUNT=@AMOUNT,        
NAME=@NAME,        
LEADER=@LEADER,        
LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME   ,        
CO_APPLICANT_ID=@CO_APPLICANT_ID,         
BROKER_ID= @BROKER_ID        
WHERE      
CUSTOMER_ID = @CUSTOMER_ID AND       
POLICY_ID = @POLICY_ID AND       
POLICY_VERSION_ID = @POLICY_VERSION_ID AND                
REMUNERATION_ID=@REMUNERATION_ID         
END      
    
END      
return 1    
        
END 
GO

