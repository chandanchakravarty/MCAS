/****** Object:  StoredProcedure [dbo].[Proc_GetBase_TransactionHistory_AI_PN]    Script Date: 09/16/2011 16:08:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetBase_TransactionHistory_AI_PN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetBase_TransactionHistory_AI_PN]
GO


/****** Object:  StoredProcedure [dbo].[Proc_GetBase_TransactionHistory_AI_PN]    Script Date: 09/16/2011 16:08:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.Proc_GetBase_TransactionHistory_AI_PN    
--Created by         :          
--Date               :  5 August 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[Proc_GetBase_TransactionHistory_AI_PN]      
CREATE  PROCEDURE [dbo].[Proc_GetBase_TransactionHistory_AI_PN]      
(        
 	@CUSTOMER_ID INT =NULL,
	@POLICY_ID INT  =NULL,
	@POLICY_NUMBER NVARCHAR(50)= NULL,
	@FOR_PN_OR_AI NCHAR(2),
	@HISTORY_YEAR SMALLINT,
	@LANG_ID INT,
	@CARRIER_CODE NVARCHAR(20)
 )        
AS       
BEGIN      
	
	DECLARE @TOTAL_DUE_ON_POLICY DECIMAL(18,2)
	
	DECLARE @ARREPORT TABLE 
		(                     
				[IDENT_COL] [int] NOT NULL ,              
				[SOURCE_ROW_ID] [int],        
				[SOURCE]    Varchar(50),        
				[PRINTED_ON_NOTICE] Char(1),        
				[SOURCE_TRAN_DATE] DateTime,                                   
				[SOURCE_EFF_DATE] DateTime,                                        
				[POSTING_DATE] DateTime,            
				[DESCRIPTION] VarChar(100) null,   
				[VERSION_NO] Varchar(10) null,                
				[TOTAL_AMOUNT] [decimal] (18,2)  ,                                                                            
				[TEMP_PREMIUM] [decimal](18,2) NULL ,                                                                            
				[INSF_FEE] [decimal] (18,2) NULL,                                                                         
				[LATE_FEE] [decimal](18,2) NULL ,                             
				[REINS_FEE] [decimal](18,2) NULL ,               
				[NSF_FEE] [decimal](18,2) NULL,              
				[ADJUSTED] [decimal](18,2),        
				[TYPE] VARCHAR(2),              
				[NOTICE_URL] Varchar(400), 
				--Ravindra(12-08-2008) : For RTL View Integration
				[RTL_BATCH_NUMBER]	Varchar(8), 
				[RTL_GROUP_NUMBER]	Varchar(8), 
		       
				TOTAL_FEE  [decimal](18,2) NULL,       
				TOTAL_PREMIUM [decimal](18,2) NULL ,
				TOTAL_PREMIUM_DUE	Decimal(18,2) 	     
		)
	
	INSERT INTO @ARREPORT exec Proc_GetTransactionHistory @CUSTOMER_ID , @POLICY_ID , @POLICY_NUMBER
	
	SELECT @TOTAL_DUE_ON_POLICY = SUM(CASE TYPE  WHEN 'F' THEN isnull(TOTAL_PREMIUM,0) *-1     ELSE isnull(TOTAL_PREMIUM,0) END )
FROM @ARREPORT 

--Account Inquiry
 IF(@FOR_PN_OR_AI='AI')
	BEGIN
		SELECT dbo.fun_GetLanguageBasedDateFormat(SOURCE_TRAN_DATE,@LANG_ID) as SRC_DATE,                        
		dbo.fun_GetLanguageBasedDateFormat(CONVERT(VARCHAR,SOURCE_TRAN_DATE),@LANG_ID) AS SOURCE_TRAN_DATE,                        
		dbo.fun_GetLanguageBasedDateFormat(SOURCE_EFF_DATE,@LANG_ID) AS SOURCE_EFF_DATE,                        
		dbo.fun_GetLanguageBasedDateFormat(POSTING_DATE,@LANG_ID) AS POSTING_DATE,                        
		DESCRIPTION + ISNULL(VERSION_NO,'') AS DESCRIPTION,           
		CASE WHEN TOTAL_AMOUNT  < 0 then TOTAL_AMOUNT   * -1                         
		ELSE TOTAL_AMOUNT END AS TOTAL_AMOUNT  ,                         
		CASE TYPE 
		WHEN 'F' THEN isnull(TOTAL_PREMIUM,0) *-1                        
		ELSE isnull(TOTAL_PREMIUM,0)                         
		END AS TOTAL_PREMIUM ,                        
		INSF_FEE,LATE_FEE  ,REINS_FEE  ,                               
		NSF_FEE  ,TYPE   ,TOTAL_FEE     ,TEMP_PREMIUM,                         
		ISNULL(NOTICE_URL, 'NA') AS NOTICE_URL,              
		ADJUSTED  , SOURCE_ROW_ID AS OPEN_ITEM_ID ,              
		@POLICY_ID AS POL_ID , 
		RTL_BATCH_NUMBER , RTL_GROUP_NUMBER ,
		@TOTAL_DUE_ON_POLICY as TOTAL_DUE_ON_POLICY
		FROM @ARREPORT                  
		WHERE YEAR(SOURCE_TRAN_DATE) >= @HISTORY_YEAR                         
		ORDER BY SRC_DATE    
	END
-- Premium Notice 
IF(@FOR_PN_OR_AI='PN')
	BEGIN
	
	DECLARE @OutTrans TABLE 
		(
			[IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,                   
			[BILL_DATE] VarChar(100),                                    
			[EFF_DATE] VarChar(100), 
			[DESCRIPTION] VarChar(100) null,
			[TOTAL_AMOUNT] VarChar(100),
			[DISPLAY_ORDER] Varchar(50)			  
		)	
		INSERT INTO @OutTrans
		
		SELECT                                       
		dbo.fun_GetLanguageBasedDateFormat(SOURCE_TRAN_DATE,@LANG_ID),                               
		dbo.fun_GetLanguageBasedDateFormat(POSTING_DATE,@LANG_ID),
		DESCRIPTION ,
		'$' + 
		CASE TYPE WHEN 'T' THEN CONVERT(VARCHAR(30),CONVERT(MONEY,TOTAL_PREMIUM_DUE),1) 
		WHEN 'A' THEN CONVERT(VARCHAR(30),CONVERT(MONEY,TEMP_PREMIUM),1) 
		ELSE
		CONVERT(VARCHAR(30),CONVERT(MONEY,TOTAL_AMOUNT),1)   
		END, 
		0                                    
		FROM @ARREPORT                               
		WHERE TYPE NOT IN('N','L','R','F')                                        
		AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'                                        
		          
		UNION
		                              
		SELECT                                       
		dbo.fun_GetLanguageBasedDateFormat(SOURCE_TRAN_DATE,@LANG_ID), 
		dbo.fun_GetLanguageBasedDateFormat(POSTING_DATE,@LANG_ID),
		DESCRIPTION ,
		'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(TOTAL_PREMIUM,0) * - 1),1) ,
		0 
		FROM @ARREPORT                               
		WHERE TYPE IN('F')                                        
		AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'   
		AND ISNULL(TOTAL_PREMIUM,0) <> 0

		UNION 		                                        
		                                        
		SELECT                                       
		dbo.fun_GetLanguageBasedDateFormat(SOURCE_TRAN_DATE,@LANG_ID),                         
		dbo.fun_GetLanguageBasedDateFormat(POSTING_DATE,@LANG_ID),                                       
		'Service Fees charged ' AS ACTIVITY_DESC,                                                            
		'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,INSF_FEE * - 1),1),   
		1                                       
		FROM @ARREPORT                  
		WHERE INSF_FEE <> 0                                         
		AND  TYPE NOT IN('N','L','R','F')                                        
		AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'                                        
		                                        
		UNION                                        
		                                        
		SELECT                                         
		dbo.fun_GetLanguageBasedDateFormat(SOURCE_TRAN_DATE,@LANG_ID),                              
		dbo.fun_GetLanguageBasedDateFormat(POSTING_DATE,@LANG_ID),                                       
		'NSF Fees charged ' as ACTIVITY_DESC,                                                            
		'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,NSF_FEE * - 1 ),1),   
		2                                     
		FROM @ARREPORT                                          
		WHERE NSF_FEE <> 0           
		AND  TYPE NOT IN('N','L','R','F')                                        
		AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'                                        
		                                        
		UNION         
		                                        
		SELECT                                         
		dbo.fun_GetLanguageBasedDateFormat(SOURCE_TRAN_DATE,@LANG_ID),
		dbo.fun_GetLanguageBasedDateFormat(POSTING_DATE,@LANG_ID),
		'Late Fees charged ' as ACTIVITY_DESC,                                                            
		'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,LATE_FEE * - 1 ),1),
		3                                        
		FROM @ARREPORT                                      
		WHERE LATE_FEE <> 0                                         
		AND  TYPE NOT IN('N','L','R','F')                                        
		AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'
		                        
		UNION                                         
		                                        
		SELECT                                       
		dbo.fun_GetLanguageBasedDateFormat(SOURCE_TRAN_DATE,@LANG_ID),                               
		dbo.fun_GetLanguageBasedDateFormat(POSTING_DATE,@LANG_ID),                            
		'Reinstatement Fees charged ' ,                                                           
		'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,REINS_FEE * - 1 ),1),
		4                               
		FROM @ARREPORT                                          
		WHERE REINS_FEE <> 0                                         
		AND  TYPE NOT IN('N','L','R','F')                                        
		AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'
		--ORDER BY SOURCE_TRAN_DATE  , DISPLAY_ORDER 

		DECLARE @IDENT_COLN INT
		DECLARE @TEMPINST VARCHAR(MAX)
		DECLARE @TEMPINST1 VARCHAR(MAX)
		SET @IDENT_COLN=1
		SET @TEMPINST=''
		SET @TEMPINST1=''
		
		WHILE(1=1)
			BEGIN
					IF NOT EXISTS (SELECT IDENT_COL FROM @OutTrans WHERE IDENT_COL = @IDENT_COLN)     
						BEGIN     
							BREAK    
						END 
						SET @TEMPINST=''
					SELECT @TEMPINST = '<ACCROW ID='''+CONVERT(NVARCHAR(20),@IDENT_COLN)+'''><BILL_DATE>'+BILL_DATE+	'</BILL_DATE><EFF_DATE>'+EFF_DATE+'</EFF_DATE><DESCRIPTION>'+DESCRIPTION+'</DESCRIPTION><TOTAL_AMOUNT>'+TOTAL_AMOUNT+'</TOTAL_AMOUNT></ACCROW>'	
					FROM @OutTrans where IDENT_COL = @IDENT_COLN ORDER BY DISPLAY_ORDER
					
					SET @TEMPINST1 = @TEMPINST1 + @TEMPINST
				SET @IDENT_COLN=@IDENT_COLN + 1
			END
		SELECT @TEMPINST1 AS TransactionHistory		
	END
End


GO

