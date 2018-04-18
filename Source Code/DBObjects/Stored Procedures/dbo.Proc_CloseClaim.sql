IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CloseClaim]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CloseClaim]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*---------------------------------------------------------- 
Proc Name       : Proc_CloseClaim                                          
Created by      : Santosh Kumar Gautam                                                                   
Date            : 17 Jan 2010                                                                  
Purpose         : USED CLOSE CLAIM BY EOD PROCESS                                                                                                           
Revison History :            
modified by		:P K Chandel
modified Date	:10 Feb 2011
Pupose			: Close Claim one by one                                                              
Used In         : CLAIM                                                                                            
------------------------------------------------------------                                                                          
Date     Review By          Comments                                                                          
------   ------------       -------------------------                                      
*/                                                                          
--DROP PROC [Proc_CloseClaim] 406,'02/19/2011',2,582
CREATE PROC [dbo].[Proc_CloseClaim] 
 @CREATED_BY            int,  
 @CREATED_DATETIME      datetime  ,
 @LANG_ID               INT=1 ,
 @CLAIM_ID        int                                                     
AS                                                                          
BEGIN                                            

        
  --DECLARE  @ACTIVITY_REASON       int =11773   -- RESERVE ACTITY    
  --DECLARE  @ACTION_ON_PAYMENT     int=166      --CHANGE RESERVE ACTITY   
  --DECLARE  @ACTIVITY_ID           int 
  
  --DECLARE  @ROW_COUNTER           int  =0
  DECLARE  @CUSTOMER_ID           int  =0
  DECLARE  @POLICY_ID             int  =0
  DECLARE  @POLICY_VERSION_ID     int  =0
  DECLARE  @CLAIM_NUMBER          NVARCHAR(100)
  
  --DECLARE @TEMP_CLAIM_LIST TABLE
  --(
  --  ROW_ID INT IDENTITY,
  --  CLAIM_ID INT
  --)      
  
  
-----------------------------------------------------------
-- MARK COMPLETE ALL CLAIMS RECORDS THOSE LAST ACTIVITY IS 
-- COMPLETED AND TOTAL OUTSTANDING AMOUNT IS ZERO
-----------------------------------------------------------


 
-- INSERT INTO @TEMP_CLAIM_LIST
--  (
--   CLAIM_ID
--  )
--  (
--	  -- FETCH CLAIM ID (UNCOMPLETED) AND RELATED LAST ACTIVITY ID HAVING STATUS COMPLETED AND OUSTANDING AMOUNT IS ZER0
--	  SELECT P.CLAIM_ID FROM CLM_ACTIVITY P INNER JOIN   
--	  (
--		 -- FETCH CLAIM ID (UNCOMPLETED) AND RELATED LAST ACTIVITY ID
--		 SELECT MAX(A.ACTIVITY_ID) AS ACTIVITY_ID ,L.CLAIM_ID  
--		 FROM   CLM_CLAIM_INFO L INNER JOIN
--				CLM_ACTIVITY A ON A.CLAIM_ID=L.CLAIM_ID 
--		 where  L.CLAIM_STATUS=11739 -- FOR INCOMPLETED CLAIM
--		 group by L.CLAIM_ID
		 
--	  )T ON P.CLAIM_ID=T.CLAIM_ID AND P.ACTIVITY_ID=T.ACTIVITY_ID 
	  
--	  WHERE P.ACTIVITY_STATUS=11801 AND  P.CLAIM_RESERVE_AMOUNT=0.00
--  )
 
--SELECT @ROW_COUNTER= COUNT(ROW_ID) FROM @TEMP_CLAIM_LIST

 --WHILE (@ROW_COUNTER>0)
 --BEGIN        

 --  SELECT @CLAIM_ID =CLAIM_ID 
 --  FROM   @TEMP_CLAIM_LIST 
 --  WHERE  ROW_ID=@ROW_COUNTER
   
   SELECT @CUSTOMER_ID       = CUSTOMER_ID,
          @POLICY_ID         = POLICY_ID,
          @POLICY_VERSION_ID = POLICY_VERSION_ID,
          @CLAIM_NUMBER      = CLAIM_NUMBER
   FROM   CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID
    
   --SET @ROW_COUNTER=@ROW_COUNTER-1
   
   ------------------------------------------------------------        
   -- CLOSE CLAIM
   ------------------------------------------------------------     
	  UPDATE CLM_CLAIM_INFO 
	  SET CLAIM_STATUS          =11740,-- (CLOSE STATUS CODE)
	      CLOSED_DATE		    =@CREATED_DATETIME,
	      LAST_UPDATED_DATETIME =@CREATED_DATETIME
	  WHERE CLAIM_ID=@CLAIM_ID
	  
	IF(@LANG_ID=1)
	  SET @CLAIM_NUMBER='CLAIM NUMBER = '+@CLAIM_NUMBER
	ELSE
	  SET @CLAIM_NUMBER='Número de Reivindicação = '+@CLAIM_NUMBER
	  
	  PRINT  @CLAIM_NUMBER
   ------------------------------------------------------------
   -- CREATE TRANSACTION LOG FOR CLAIM CLOSED
   ------------------------------------------------------------
   EXEC Proc_InsertTransactionLog
	 @TRANS_TYPE_ID          =325, -- ID FROM TRANSACTIONTYPELIST
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

 
 --END
 
 
 -- SINGLE QUERY TO ACHIEVE ABOVE TASK BUT WE CANNOT CREATE LOG
 
--UPDATE CLM_CLAIM_INFO 
--SET CLAIM_STATUS=11740 
--FROM CLM_CLAIM_INFO M INNER JOIN
--(
  
--  -- FETCH CLAIM ID (UNCOMPLETED) AND RELATED LAST ACTIVITY ID HAVING STATUS COMPLETED AND OUSTANDING AMOUNT IS ZER0
--  SELECT P.CLAIM_ID,P.ACTIVITY_ID FROM CLM_ACTIVITY P INNER JOIN   
--  (
--     -- FETCH CLAIM ID (UNCOMPLETED) AND RELATED LAST ACTIVITY ID
--	 SELECT MAX(A.ACTIVITY_ID) AS ACTIVITY_ID ,L.CLAIM_ID  
--	 FROM   CLM_CLAIM_INFO L INNER JOIN
--	        CLM_ACTIVITY A ON A.CLAIM_ID=L.CLAIM_ID 
--	 where  L.CLAIM_STATUS=11739 -- FOR INCOMPLETED CLAIM
--	 group by L.CLAIM_ID
	 
--  )T ON P.CLAIM_ID=T.CLAIM_ID AND P.ACTIVITY_ID=T.ACTIVITY_ID 
  
--  WHERE P.ACTIVITY_STATUS=11801 AND  P.CLAIM_RESERVE_AMOUNT=0.00
  
  
-- )N ON M.CLAIM_ID=N.CLAIM_ID 

                   
END     

 

GO

