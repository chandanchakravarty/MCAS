IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertRemuneration]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertRemuneration]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                
Proc Name       : dbo.[[Proc_InsertRemuneration]]        
Created by      : LALIT CHAUHAN    
Date            : 04/26/2010                
Purpose         :INSERT RECORDS IN POL_REMUNERATION TABLE.                
Revison History :                
Used In        : Ebix Advantage                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------
DROP PROC dbo.[Proc_InsertRemuneration]  

*/ 
/****** Script for POL_REMUNERATION into DATABASE  ******/     

CREATE PROC [dbo].[Proc_InsertRemuneration]
(          
@REMUNERATION_ID int out,        
@CUSTOMER_ID int,          
@POLICY_ID int,          
@POLICY_VERSION_ID int,          
@BROKER_ID int,          
@COMMISSION_PERCENT numeric(8,4),          
@COMMISSION_TYPE int,          
@IS_ACTIVE nchar(1),          
@CREATED_BY int,
@CREATED_DATETIME datetime,          
@BRANCH numeric(8)  ,
@AMOUNT decimal(12,2),
@LEADER INT,
@NAME NVARCHAR(100),
@PRODUCT_RISK_ID INT=NULL,
@CO_APPLICANT_ID INT = NULL
)          
As  

DECLARE @SUM numeric(8,4) ,
@leadercount int,
@COMM_PERCENT DECIMAL(8,4),
@FEES_PERCENT DECIMAL(8,4),
@PRO_LABORE_PERCENT DECIMAL(8,4),
@TRANSACTION_TYPE int

--@TransactionType int  
   
 BEGIN          
      


IF (@LEADER=10963)  
 BEGIN  
  UPDATE POL_REMUNERATION SET LEADER=10964 
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND 
		POLICY_VERSION_ID=@POLICY_VERSION_ID AND RISK_ID=@PRODUCT_RISK_ID
		AND BROKER_ID = @BROKER_ID AND CO_APPLICANT_ID=@CO_APPLICANT_ID
 END 
 
		 --SELECT @TransactionType=TRANSACTION_TYPE  FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID 
		 --AND   POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
	     
		 SELECT  @SUM=sum(COMMISSION_PERCENT)+@COMMISSION_PERCENT from POL_REMUNERATION WITH(NOLOCK) 
		 	   
		 WHERE CUSTOMER_ID=@CUSTOMER_ID AND   POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID 
		AND COMMISSION_TYPE=@COMMISSION_TYPE AND CO_APPLICANT_ID=@CO_APPLICANT_ID
		
		SELECT @leadercount=COUNT(*) from POL_REMUNERATION with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND 
		  POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND LEADER=10963
		  
		  select @COMM_PERCENT=COMMISSION_PERCENT,@FEES_PERCENT=FEES_PERCENT,
		  @PRO_LABORE_PERCENT=PRO_LABORE_PERCENT from POL_APPLICANT_LIST WITH(NOLOCK)
          WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND
          APPLICANT_ID=@CO_APPLICANT_ID 
          
          select @TRANSACTION_TYPE=TRANSACTION_TYPE  from POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID
           AND  POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
		
	  
    IF(@SUM > 100)     --AND @TransactionType<>14560
		 BEGIN    		  
		   RETURN -2
		 END
	 ELSE IF(@LEADER=10963 and @leadercount>=1 and @TRANSACTION_TYPE in (14559,14561,14679))
		 BEGIN
		   RETURN -3
		 END
	  ELSE IF EXISTS(SELECT LEADER from POL_REMUNERATION with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND     
    POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND LEADER=10963 and     
    @LEADER=10963 and @TRANSACTION_TYPE=14560 
    and CO_APPLICANT_ID = @CO_APPLICANT_ID)     
    BEGIN           
    RETURN -3     
   END  
	ELSE IF EXISTS(SELECT BROKER_ID  from POL_REMUNERATION with(nolock)  WHERE CUSTOMER_ID=@CUSTOMER_ID
	      AND  POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND BROKER_ID=@BROKER_ID 
		  AND  COMMISSION_TYPE=@COMMISSION_TYPE AND CO_APPLICANT_ID=@CO_APPLICANT_ID)
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
	SELECT @REMUNERATION_ID=ISNULL(MAX(REMUNERATION_ID),0)+1 FROM POL_REMUNERATION WITH(NOLOCK)

 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID   
 INSERT INTO POL_REMUNERATION          
 (          
REMUNERATION_ID,          
CUSTOMER_ID,          
POLICY_ID,          
POLICY_VERSION_ID,          
BROKER_ID,          
COMMISSION_PERCENT,          
COMMISSION_TYPE,          
IS_ACTIVE,          
CREATED_BY,          
CREATED_DATETIME,          
BRANCH    ,
AMOUNT,
NAME,
LEADER,
RISK_ID  ,
CO_APPLICANT_ID   
 )          
 VALUES          
 (          
@REMUNERATION_ID,          
@CUSTOMER_ID,          
@POLICY_ID ,          
@POLICY_VERSION_ID,          
@BROKER_ID,          
@COMMISSION_PERCENT,          
@COMMISSION_TYPE ,          
@IS_ACTIVE,          
@CREATED_BY,          
@CREATED_DATETIME,    
@BRANCH,
@AMOUNT,
@NAME,
@LEADER, 
isnull(@PRODUCT_RISK_ID,0),
@CO_APPLICANT_ID         
          
 )   
  return 1    
 END   
 

 END  
GO

