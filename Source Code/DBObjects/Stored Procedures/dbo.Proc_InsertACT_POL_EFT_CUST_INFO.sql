IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_POL_EFT_CUST_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_POL_EFT_CUST_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN
--DROP PROC Proc_InsertACT_POL_EFT_CUST_INFO
--GO
/*----------------------------------------------------------              
Proc Name       : dbo.Proc_InsertACT_POL_EFT_CUST_INFO          
Created by      : Praveen kasana            
Date            : 16 Jan 2006          
Purpose      : Evaluation              
Revison History :              
          
Modified By  : Ravindra          
Modified Date : 04-14-2007          
Purpose  : Readjust Due dates of Installments of EFT type which are not released           
Used In  : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
drop PROC [dbo].[Proc_InsertACT_POL_EFT_CUST_INFO]      
------   ------------       -------------------------*/              
       
CREATE PROC [dbo].[Proc_InsertACT_POL_EFT_CUST_INFO]              
(              
 @CUSTOMER_ID        int,              
 @POLICY_ID          int,              
 @POLICY_VERSION_ID     smallint,              
 @FEDERAL_ID      varchar(100),              
 @DFI_ACC_NO     NVARCHAR(100),             
 @TRANSIT_ROUTING_NO varchar(100),          
 @ACCOUNT_TYPE   varchar(4),          
 @CREATED_BY        int,              
 @CREATED_DATETIME   datetime,              
 @MODIFIED_BY  INT  = null,          
 @LAST_UPDATED_DATETIME datetime,          
 @EFT_TENTATIVE_DATE      int   ,        
 @REVERIFIED_AC int = null        
          
)              
AS              
BEGIN           
      
  /*ITRACK 4636*/        
 DECLARE @CURRENT_TERM INT 
 DECLARE @RetVal INT
 SELECT @CURRENT_TERM = ISNULL(CURRENT_TERM,0) FROM POL_CUSTOMER_POLICY_LIST   WITH(NOLOCK)      
 WHERE CUSTOMER_ID = @CUSTOMER_ID         
 AND   POLICY_ID   = @POLICY_ID         
 AND   POLICY_VERSION_ID = @POLICY_VERSION_ID          
         
              
 If NOT EXISTS(SELECT CUSTOMER_ID FROM ACT_POL_EFT_CUST_INFO WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID          
 AND POLICY_VERSION_ID = @POLICY_VERSION_ID)                
 BEGIN              
          
        
  INSERT INTO ACT_POL_EFT_CUST_INFO              
  (              
   CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, FEDERAL_ID,              
   DFI_ACC_NO, TRANSIT_ROUTING_NO, CREATED_BY, CREATED_DATETIME,ACCOUNT_TYPE,EFT_TENTATIVE_DATE,REVERIFIED_AC          
  )              
 /* VALUES              
  (              
   @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, @FEDERAL_ID,              
   @DFI_ACC_NO, @TRANSIT_ROUTING_NO, @CREATED_BY, @CREATED_DATETIME ,@ACCOUNT_TYPE,@EFT_TENTATIVE_DATE ,@REVERIFIED_AC         
  )*/             
        
  SELECT  CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,@FEDERAL_ID,        
   @DFI_ACC_NO, @TRANSIT_ROUTING_NO, @CREATED_BY, @CREATED_DATETIME ,@ACCOUNT_TYPE,@EFT_TENTATIVE_DATE ,@REVERIFIED_AC         
  FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)        
 WHERE CUSTOMER_ID = @CUSTOMER_ID         
 AND   POLICY_ID   = @POLICY_ID         
 AND CURRENT_TERM = @CURRENT_TERM        
         
               
  exec @RetVal = Proc_ReAdjustEFTDate   @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, @EFT_TENTATIVE_DATE          
 --return 1               
return @RetVal

           
 END              
 ELSE          
 BEGIN          
        
--reverify Update        
--Update Reverify flag on basis of change        
DECLARE @NEW_IS_VERIFIED INT        
DECLARE @OLD_DFI_ACC_NO nvarchar(100)        
DECLARE @OLD_TRANSIT_ROUTING_NO  varchar(100)        
        
SET @NEW_IS_VERIFIED = 10964 --NO        
        
SELECT          
@OLD_DFI_ACC_NO = DFI_ACC_NO ,        
@OLD_TRANSIT_ROUTING_NO = TRANSIT_ROUTING_NO         
FROM ACT_POL_EFT_CUST_INFO with(nolock)        
WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID             
        
--compare DFI_ACCOUNT_NUMBER and ROUTING_NUMBER       
IF((@OLD_DFI_ACC_NO <> @DFI_ACC_NO)        
OR (@OLD_TRANSIT_ROUTING_NO <> @TRANSIT_ROUTING_NO) )      
        
BEGIN        
 UPDATE POL_EFT       
SET IS_VERIFIED = @NEW_IS_VERIFIED         
-- WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID            ----EDIT bY Raghav----      
FROM ACT_POL_EFT_CUST_INFO  AS POL_EFT  WITH(NOLOCK)      
INNER JOIN POL_CUSTOMER_POLICY_LIST  POL WITH(NOLOCK)       
ON POL_EFT.CUSTOMER_ID  = POL.CUSTOMER_ID        
AND POL_EFT.POLICY_ID  = POL.POLICY_ID        
 WHERE POL_EFT.CUSTOMER_ID = @CUSTOMER_ID AND POL_EFT.POLICY_ID = @POLICY_ID         
AND POL.CURRENT_TERM = @CURRENT_TERM      
      
      
END        
--End         
        
 /*UPDATE ACT_POL_EFT_CUST_INFO SET           
 FEDERAL_ID = @FEDERAL_ID,          
 DFI_ACC_NO = @DFI_ACC_NO,          
 TRANSIT_ROUTING_NO = @TRANSIT_ROUTING_NO,          
 ACCOUNT_TYPE = @ACCOUNT_TYPE,          
 EFT_TENTATIVE_DATE = @EFT_TENTATIVE_DATE,          
 MODIFIED_BY = @MODIFIED_BY,        
 REVERIFIED_AC = @REVERIFIED_AC          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  */        
        
--Itrack 4636        
        
UPDATE POL_EFT SET           
 POL_EFT.FEDERAL_ID   = @FEDERAL_ID,          
 POL_EFT.DFI_ACC_NO   = @DFI_ACC_NO,          
 POL_EFT.TRANSIT_ROUTING_NO  = @TRANSIT_ROUTING_NO,          
 POL_EFT.ACCOUNT_TYPE   = @ACCOUNT_TYPE,          
 POL_EFT.EFT_TENTATIVE_DATE  = @EFT_TENTATIVE_DATE,          
 POL_EFT.MODIFIED_BY   = @MODIFIED_BY,        
 POL_EFT.REVERIFIED_AC   = @REVERIFIED_AC          
FROM ACT_POL_EFT_CUST_INFO  AS POL_EFT WITH(NOLOCK)       
INNER JOIN POL_CUSTOMER_POLICY_LIST  POL WITH(NOLOCK)       
ON POL_EFT.CUSTOMER_ID  = POL.CUSTOMER_ID        
AND POL_EFT.POLICY_ID  = POL.POLICY_ID        
 WHERE POL_EFT.CUSTOMER_ID = @CUSTOMER_ID AND POL_EFT.POLICY_ID = @POLICY_ID         
AND POL.CURRENT_TERM = @CURRENT_TERM        
        
          
  exec @RetVal = Proc_ReAdjustEFTDate   @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, @EFT_TENTATIVE_DATE          
          
 --return 2     --Udpated         
return @RetVal

 END          
              
           
END              
          
          
--GO
--EXEC Proc_InsertACT_POL_EFT_CUST_INFO 1577,64,2,0,'IbzdFYYNcB7UxEd1P+M+xOmdi+Z9pH51zlUqSB5Wfro=','IbzdFYYNcB7UxEd1P+M+xBKLYwL0xMfaF15VZnkYgT8=',100,340,'1/11/2010',340,'1/11/2010',15,10963       
--
----SELECT A.SOURCE_ROW_ID, A.POLICY_VERSION_ID AS VER ,A.RISK_ID , A.TRANS_DESC,A.TOTAL_DUE,A.TOTAL_PAID,  
----A.SWEEP_DATE,A.ITEM_TRAN_CODE_TYPE,A.ITEM_TRAN_CODE,A.AGENCY_COMM_PERC, A.NET_PREMIUM, A.GROSS_AMOUNT,  
----ISNULL(B.INSTALLMENT_NO,50) INS_NO,B.INSTALLMENT_NO  AS INS_NO,B.RELEASED_STATUS,A.RISK_ID,A.RISK_TYPE,  
----A.* FROM ACT_CUSTOMER_OPEN_ITEMS A WITH(NOLOCK)  
----left JOIN ACT_POLICY_INSTALLMENT_DETAILS B WITH(NOLOCK) ON A.INSTALLMENT_ROW_ID  = B.ROW_ID  
----LEFT JOIN MNT_LOOKUP_VALUES MNT ON   
----B.PAYMENT_MODE = MNT.LOOKUP_UNIQUE_ID   
----WHERE A.CUSTOMER_ID= 1577 AND A.POLICY_ID = 57 --AND ITEM_TRAN_CODE_TYPE = 'PREM' --AND A.SOURCE_ROW_ID = 79  
----ORDER BY ISNULL(B.CURRENT_TERM,50),ISNULL(B.INSTALLMENT_NO,50) ,  
----A.IDEN_ROW_ID ,A.SOURCE_EFF_DATE,A.UPDATED_FROM  
----
----SELECT * FROM ACT_POLICY_INSTALLMENT_DETAILS WHERE CUSTOMER_ID= 1577 AND POLICY_ID = 57
----
--ROLLBACK TRAN        
        
          
          
          
          
          
            
            
          
          
          
          
          
          
          
          
        
        
        
        
        
        
        
        
      
      
    
  
  
  
  



GO

