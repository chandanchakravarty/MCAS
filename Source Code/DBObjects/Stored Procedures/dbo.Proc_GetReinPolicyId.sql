IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetReinPolicyId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetReinPolicyId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                    
Proc Name             : Dbo.[Proc_GetReinPolicyId]                                                    
Created by            : Aditya Goel                                               
Date                  : 07/01/2011    
Modified By           : Shubhanshu Pandey                                                 
Purpose               : To get Reinsurance Breakdown details          
Revison History       :                                                    
Used In               : Reports module          
------------------------------------------------------------                                                    
Date     Review By          Comments                       
  
drop Proc [Proc_GetReinPolicyId]  2764,262,1,1,null                                   
------   ------------       -------------------------*/      
    
CREATE PROC [dbo].[Proc_GetReinPolicyId]            
  (  
	@CUSTOMER_ID INT = NULL,  
	@POLICY_ID INT =NULL,  
	@POLICY_VERSION_ID SMALLINT = NULL,  
	@LANG_ID INT   =1  ,
	@POLICY_NUMBER nvarchar(25) = NULL
  )          
AS                                                                                      
BEGIN                                --THE FOLLOWING CODE GETS DATA FOR RI    
  
   IF (@CUSTOMER_ID IS NULL AND @POLICY_ID IS NULL AND @POLICY_VERSION_ID IS NULL AND @POLICY_NUMBER IS  NULL ) OR (@CUSTOMER_ID = 0 AND @POLICY_ID = 0 AND @POLICY_VERSION_ID = 0 AND @POLICY_NUMBER = '')  
  BEGIN           
   SELECT   
		PCPL.POLICY_DISP_VERSION, 
		ISNULL(CONTRACT_NUMBER,'&nbsp;') AS 'CONTRACT',    
		PCPL.POLICY_NUMBER,    
		dbo.fun_FormatCurrency( PRBD.TOTAL_INS_VALUE,@LANG_ID)TOTAL_INS_VALUE,  
		dbo.fun_FormatCurrency( PRBD.LAYER_AMOUNT,@LANG_ID)LAYER_AMOUNT,  
		dbo.fun_FormatCurrency( PRBD.TRAN_PREMIUM,@LANG_ID)TRAN_PREMIUM,  
        dbo.fun_FormatCurrency( PRBD.REIN_PREMIUM,@LANG_ID)REIN_PREMIUM,  
        dbo.fun_FormatCurrency( PRBD.COMM_AMOUNT,@LANG_ID)COMM_AMOUNT,  
        round( convert(Decimal(10,3),PRBD.RETENTION_PER), 3)RETENTION_PER ,  
        round( convert(Decimal(10,3),PRBD.COMM_PERCENTAGE), 3)COMM_PERCENTAGE,  
        ISNULL(MRCL.REIN_COMAPANY_NAME,' ') AS REIN_COMAPANY_NAME,  
        PRBD.LAYER,       
        round( convert(Decimal(10,3),PRBD.RATE), 3)CONTRACT_COMM_PERCENTAGE ,  
		dbo.fun_FormatCurrency( PRBD.REIN_CEDED,@LANG_ID)CESSION_AMOUNT_LAYER,  
		REPLACE(REPLACE(REPLACE(PRBD.RISK_ID, '.', '@'), ',', '.'), '@', ',') AS RISK_ID  
 FROM   
		POL_REINSURANCE_BREAKDOWN_DETAILS PRBD WITH(NOLOCK)  
     RIGHT OUTER JOIN   
		POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK) ON (PRBD.CUSTOMER_ID = PCPL.CUSTOMER_ID 
													   AND PRBD.POLICY_ID = PCPL.POLICY_ID
													   AND PRBD.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID)  
	 LEFT JOIN   
	    MNT_REIN_COMAPANY_LIST MRCL WITH(NOLOCK) ON (MRCL.REIN_COMAPANY_ID=PRBD.MAJOR_PARTICIPANT)
                         
  END  
    ELSE   
      BEGIN  
      
      IF (@POLICY_NUMBER IS NOT NULL AND @POLICY_NUMBER <> '')
			SELECT 
				@CUSTOMER_ID = CUSTOMER_ID ,
				@POLICY_ID = POLICY_ID  
			FROM POL_CUSTOMER_POLICY_LIST 
			WHERE POLICY_NUMBER = @POLICY_NUMBER
    
    
     SELECT    
		PCPL.POLICY_DISP_VERSION,  
        ISNULL(CONTRACT_NUMBER,'&nbsp;') AS 'CONTRACT',    
		PCPL.POLICY_NUMBER, 
		dbo.fun_FormatCurrency( PRBD.TOTAL_INS_VALUE,@LANG_ID)TOTAL_INS_VALUE,  
		dbo.fun_FormatCurrency( PRBD.LAYER_AMOUNT,@LANG_ID)LAYER_AMOUNT,  
		dbo.fun_FormatCurrency( PRBD.TRAN_PREMIUM,@LANG_ID)TRAN_PREMIUM,  
        dbo.fun_FormatCurrency( PRBD.REIN_PREMIUM,@LANG_ID)REIN_PREMIUM,  
        dbo.fun_FormatCurrency( PRBD.COMM_AMOUNT,@LANG_ID)COMM_AMOUNT,  
        round( convert(Decimal(10,3),PRBD.RETENTION_PER), 3)RETENTION_PER ,  
        round( convert(Decimal(10,3),PRBD.COMM_PERCENTAGE), 3)COMM_PERCENTAGE,  
        ISNULL(MRCL.REIN_COMAPANY_NAME,' ') AS REIN_COMAPANY_NAME,  
        PRBD.LAYER,       
        round( convert(Decimal(10,3),PRBD.RATE), 3)CONTRACT_COMM_PERCENTAGE ,  
		dbo.fun_FormatCurrency( PRBD.REIN_CEDED,@LANG_ID)CESSION_AMOUNT_LAYER,  
		REPLACE(REPLACE(REPLACE(PRBD.RISK_ID, '.', '@'), ',', '.'), '@', ',') AS RISK_ID  
   FROM   
		POL_REINSURANCE_BREAKDOWN_DETAILS PRBD WITH(NOLOCK)   
     LEFT OUTER JOIN   
		POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK) ON (PRBD.CUSTOMER_ID = PCPL.CUSTOMER_ID AND  
													   PRBD.POLICY_ID = PCPL.POLICY_ID AND  
													   PRBD.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID ) 
     LEFT OUTER JOIN  
       MNT_REIN_COMAPANY_LIST MRCL WITH(NOLOCK) ON MRCL.REIN_COMAPANY_ID=PRBD.MAJOR_PARTICIPANT 
  
  WHERE 
       PCPL.CUSTOMER_ID = @CUSTOMER_ID   
       AND PCPL.POLICY_ID = @POLICY_ID   
     --AND PCPL.POLICY_VERSION_ID = @POLICY_VERSION_ID  
      END  
        
        
      --THE FOLLOWING CODE GETS DATA FOR COI      
  SELECT  
	   T3.POLICY_DISP_VERSION, 
	   T2.REIN_COMAPANY_CODE,   
	   T2.REIN_COMAPANY_NAME,  
	   REPLACE(REPLACE(REPLACE(sum(INSTALLMENT_AMOUNT), '.', '@'), ',', '.'), '@', ',') AS INSTALLMENT_AMOUNT,  
	   REPLACE(REPLACE(REPLACE(sum(INTEREST_AMOUNT), '.', '@'), ',', '.'), '@', ',') AS INTEREST_AMOUNT,  
	   REPLACE(REPLACE(REPLACE(sum(TOTAL), '.', '@'), ',', '.'), '@', ',') AS TOTAL,  
	   REPLACE(REPLACE(REPLACE(sum(CO_COMM_AMT), '.', '@'), ',', '.'), '@', ',') AS CO_COMM_AMT   
  FROM  
	ACT_POLICY_CO_BILLING_DETAILS T1 WITH(NOLOCK)  
  LEFT OUTER JOIN  
	MNT_REIN_COMAPANY_LIST T2 WITH(NOLOCK) ON (T1.CO_COMPANY_ID = T2.REIN_COMAPANY_ID)  
  LEFT OUTER JOIN
	POL_CUSTOMER_POLICY_LIST T3  WITH(NOLOCK) ON  (T1.CUSTOMER_ID = T3.CUSTOMER_ID AND T1.POLICY_ID =T3.POLICY_ID AND T1.POLICY_VERSION_ID = T3.POLICY_VERSION_ID)
  WHERE  
   T1.CUSTOMER_ID = @CUSTOMER_ID AND   
   T1.POLICY_ID = @POLICY_ID --AND   
   --T1.POLICY_VERSION_ID = @POLICY_VERSION_ID  
  GROUP BY  
      T2.REIN_COMAPANY_CODE, T2.REIN_COMAPANY_NAME ,T3.POLICY_DISP_VERSION  
  
           
END 

GO

