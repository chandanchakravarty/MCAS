IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AutoReserveClaimActivity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AutoReserveClaimActivity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


   
 /*----------------------------------------------------------                                                  
Proc Name             : Dbo.Proc_AutoReserveClaimActivity                                                  
Created by            : Santosh Kumar Gautam                                                 
Date                  : 25 Jan 2011                                             
Purpose               : To update reser0.ve amount of claim activity
Revison History       :                
Modified by			:P K Chandel
Date				:10 Feb 2011
Puprpose			: Update Reserve of claims one by one                                  
Used In               : claim module        
------------------------------------------------------------                                                  
Date     Review By          Comments                     
            
drop Proc Proc_AutoReserveClaimActivity     406,'01/26/2011','w001',1                           
------   ------------       -------------------------*/                                                  
              
CREATE PROCEDURE [dbo].[Proc_AutoReserveClaimActivity]
     
 @CREATED_BY            int,  
 @CREATED_DATETIME      datetime  ,
 @LANG_ID               INT=1 ,
 @CLAIM_ID				int         
AS                      
BEGIN               
         
  DECLARE  @ACTIVITY_REASON       int =11773   -- RESERVE ACTITY    
  DECLARE  @ACTION_ON_PAYMENT     int=166      --CHANGE RESERVE ACTITY   
  DECLARE  @ACTIVITY_ID           int 
  --DECLARE  @CLAIM_ID              int  
  DECLARE  @ROW_COUNTER           int  =0
  DECLARE  @CUSTOMER_ID           int  =0
  DECLARE  @POLICY_ID             int  =0
  DECLARE  @POLICY_VERSION_ID     int  =0
  DECLARE  @CLAIM_NUMBER          VARCHAR(10)
  DECLARE  @FACTOR                DECIMAL(15,2)=1
         
  --DECLARE @TEMP_CLAIM_LIST TABLE
  --(
  --  ROW_ID INT IDENTITY,
  --  CLAIM_ID INT
  --)      
         

  ---------------------------------------------------------------------------------------        
   -- FETCH INFLATION RATE OF TODAY'S DATE AND IF NO RECORDS EXIST OF TODAY'S RECORD THEN 
   -- FETCH PREVIOUS RECORD FROM TODAY DATE
  ---------------------------------------------------------------------------------------
   SELECT  TOP 1 @FACTOR=INFLATION_RATE FROM MNT_MONETORY_INDEX
   WHERE CONVERT (VARCHAR,[DATE],101)<= CONVERT (VARCHAR,GETDATE(),101)
   ORDER BY [DATE] DESC       
   
   
   ------------------------------------------------------------------------        
   -- UPDATE CLAIM ACTIVITY RESERVE FOR CLAIM WITH FOLLOWING CONDITIONS
   --  1. CLAIM SHOULD HAVE OPEN STATUS
   --  2. CLAIM DOES NOT HAVE ANY IN PROGRESS ACTIVITY
   --  3. LAST ACTIVITY SHOULD NOT A CLOSE ACTIVITY
   ------------------------------------------------------------------------
  
  
  --INSERT INTO @TEMP_CLAIM_LIST
  --(
  -- CLAIM_ID
  --)
  --(
  
  ---- FETCH CLAIM ID (UNCOMPLETED) AND RELATED LAST ACTIVITY ID HAVING STATUS COMPLETED AND OUSTANDING AMOUNT IS ZER0  
  
  
	 -- SELECT P.CLAIM_ID FROM CLM_ACTIVITY P INNER JOIN     
	 -- (  
		--	 -- FETCH CLAIM ID (UNCOMPLETED) AND RELATED LAST ACTIVITY ID  
		--  SELECT MAX(A.ACTIVITY_ID) AS ACTIVITY_ID ,L.CLAIM_ID    
		--  FROM   CLM_CLAIM_INFO L INNER JOIN  
		--		 CLM_ACTIVITY A ON A.CLAIM_ID=L.CLAIM_ID   
		--  where  L.CLAIM_STATUS=11739 -- FOR INCOMPLETED CLAIM  
		--  group by L.CLAIM_ID  
	    
	 -- )T ON P.CLAIM_ID=T.CLAIM_ID AND P.ACTIVITY_ID=T.ACTIVITY_ID   
	    
	 -- WHERE    P.ACTIVITY_STATUS=11801  -- FOR COMPLETED ACTIVITY (CLAIM DOES NOT HAVE ANY IN PROGRESS ACTIVITY)
	 --      AND P.ACTIVITY_REASON<>167   -- FOR CLOSE ACTIVITY(LAST ACTIVITY SHOULD NOT A CLOSE ACTIVITY)
   
  --)

 ------------------------------------------------------------------------        
 -- UPDATE CLAIM ACTIVITY RESERVE FOR EACH CLAIM IN @TEMP_CLAIM_LIST TABLE
 ------------------------------------------------------------------------

--SELECT @ROW_COUNTER= COUNT(ROW_ID) FROM @TEMP_CLAIM_LIST

-- WHILE (@ROW_COUNTER>0)
-- BEGIN        

--   SELECT @CLAIM_ID =CLAIM_ID 
-- FROM   @TEMP_CLAIM_LIST 
--   WHERE  ROW_ID=@ROW_COUNTER
   
   SELECT @CUSTOMER_ID       = CUSTOMER_ID,
          @POLICY_ID         = POLICY_ID,
          @POLICY_VERSION_ID = POLICY_VERSION_ID,
          @CLAIM_NUMBER      = CLAIM_NUMBER
   FROM  CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID
    
   --SET @ROW_COUNTER=@ROW_COUNTER-1
   
   ------------------------------------------------------------        
   -- CREATE CHANGE RESERVE ACTIVITY 
   ------------------------------------------------------------     
   EXEC Proc_InsertCLM_ACTIVITY 
	 @CLAIM_ID				=@CLAIM_ID     ,
	 @ACTIVITY_ID			=@ACTIVITY_ID OUT,       
	 @ACTIVITY_REASON		=@ACTIVITY_REASON,    
	 @REASON_DESCRIPTION    ='',
	 @CREATED_BY            ='',               
	 @ACTIVITY_STATUS       =11800, -- INCOMPLETE
	 @RESERVE_TRAN_CODE     =0,            
	 @ACTION_ON_PAYMENT     =@ACTION_ON_PAYMENT,         
	 @ACCOUNTING_SUPPRESSED =0  , 
	 @IS_VOID_ACTIVITY      ='Y',      
     @COI_TRAN_TYPE         =14849 --FULL
   ------------------------------------------------------------
   -- CREATE TRANSACTION LOG FOR ABOVE CREATED ACTIVITY
   ------------------------------------------------------------
  EXEC Proc_InsertTransactionLog
	 @TRANS_TYPE_ID          =324, -- ID FROM TRANSACTIONTYPELIST
	 @CLIENT_ID              =@CUSTOMER_ID,                              
	 @POLICY_ID              =@POLICY_ID,                              
	 @POLICY_VER_TRACKING_ID =@POLICY_VERSION_ID,                              
	 @RECORDED_BY            =@CREATED_BY,                              
	 @RECORDED_BY_NAME       ='',                              
	 @RECORD_DATE_TIME       =@CREATED_DATETIME,                              
	 @TRANS_DESC             ='',                              
	 @ENTITY_ID              =0,                              
	 @ENTITY_TYPE            =NULL,                              
	 @IS_COMPLETED           =NULL,                              
	 @CHANGE_XML             ='',                              
	 @APP_ID                 =0,                              
	 @APP_VERSION_ID         =0,                              
	 @QUOTE_ID               =0,                              
	 @QUOTE_VERSION_ID       =0,                     
	 @LANG_ID                =@LANG_ID,         
     @CUSTOM_INFO            =@CLAIM_NUMBER              

   ------------------------------------------------------------        
   -- COPY THE DATA OF LAST COMPLETED ACTIVITY    
   ------------------------------------------------------------     
    EXEC [Proc_CopyReserveDetails] @CLAIM_ID,@ACTIVITY_ID,@FACTOR
  
  
   ------------------------------------------------------------
   -- CREATE TRANSACTION LOG FOR CHANGED RESERVE
  -- ------------------------------------------------------------
  EXEC Proc_InsertTransactionLog
	 @TRANS_TYPE_ID          =287, -- ID FROM TRANSACTIONTYPELIST
	 @CLIENT_ID              =@CUSTOMER_ID,                              
	 @POLICY_ID              =@POLICY_ID,                              
	 @POLICY_VER_TRACKING_ID =@POLICY_VERSION_ID,                              
	 @RECORDED_BY            =@CREATED_BY,                              
	 @RECORDED_BY_NAME       ='',                              
	 @RECORD_DATE_TIME       =@CREATED_DATETIME,                              
	 @TRANS_DESC             ='',                              
	 @ENTITY_ID              =0,                              
	 @ENTITY_TYPE            =NULL,                              
	 @IS_COMPLETED           =NULL,                              
	 @CHANGE_XML             ='',                              
	 @APP_ID                 =0,                              
	 @APP_VERSION_ID         =0,                              
	 @QUOTE_ID               =0,                              
	 @QUOTE_VERSION_ID       =0,                     
	 @LANG_ID                =@LANG_ID,         
     @CUSTOM_INFO            =@CLAIM_NUMBER     
     
   --------------------------------------------------------------        
   -- CALCULATE BREAKDOWN  
   --------------------------------------------------------------       
    EXEC [Proc_CalculateBreakdown] @CLAIM_ID,@ACTIVITY_ID
              
   --------------------------------------------------------------        
   ---- COMPLETE ACTIVITY  
   --------------------------------------------------------------       
    EXEC [Proc_CompleteClaimActivities] @CLAIM_ID,@ACTIVITY_ID,@ACTIVITY_REASON,@ACTION_ON_PAYMENT,'Y'  
      
   
 --END







 
          
END                      
    
    
    
  



GO

