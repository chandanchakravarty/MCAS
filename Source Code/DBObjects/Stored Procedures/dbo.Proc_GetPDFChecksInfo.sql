IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFChecksInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFChecksInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN 
--drop proc dbo.Proc_GetPDFChecksInfo
--go 

/*----------------------------------------------------------                                                        
Proc Name       : dbo.Proc_GetPDFChecksInfo 1238                                                      
Created by      :  Narendra Bisht                                                        
Date            :  01/11/2006                                                        
Purpose         :  To get check pdf information                                                        
Revison History :                                                        
Used In         :    Wolverine                                                        
                                                        
Modified By		:  Mohit Agarwal                                                        
Modified Date	: 4-Nov-2006                                                        
                                        
Reviewed By		: Anurag verma                                        
Reviewed On		: 12-07-2007    


Modified By		:  Praveen kasana                                                        
Modified Date	: 27 feb 2008      
Itrack No : 3722                                                     


Modified By		: Ravindra                                              
Modified Date	: 03-03-2008      


Modified By		: Praveen                                              
Modified Date	: 06-24-2008  Itrack 4387


Modified By		: Ravindra
Modified Date	: 08-20-2008  Removed join on Account Posting Details for Claims info if multiple claims with same number it	
								would pick multiple records  
          
------------------------------------------------------------                                                        
Date     Review By          Comments                                                        
------   ------------       -------------------------*/                                                        
--drop PROC dbo.Proc_GetPDFChecksInfo 
                                                    
CREATE PROC [dbo].[Proc_GetPDFChecksInfo]                                                              
(                                                              
	@CHECKID int,                        
	@CALLEDFOR varchar(50)=null                                                              
)                                                              
AS
BEGIN

	DECLARE @COVERAGES_DESC VARCHAR(1000)                      
	DECLARE @COV_TEMP VARCHAR(500)     
	DECLARE @IS_PRINTED NVARCHAR(40)
	SELECT @IS_PRINTED=ISNULL(IS_PRINTED,'N') FROM ACT_CHECK_INFORMATION WHERE CHECK_ID=@CHECKID
	/*IF (@IS_PRINTED='N' OR @IS_PRINTED='')
		BEGIN
			--Logic to Assign check numbers before fetching records                                                    
			exec ProcAssignCheckNumber @CHECKID   
		END
	*/
	DECLARE @CONTRACT_NUMBER nvarchar(25)  

	--Ravindra(02-20-2008) Contract Number should be picked based on ID not the description
	/*
	SELECT @CONTRACT_NUMBER=D.CONTRACT_NUMBER  
	FROM MNT_REINSURANCE_CONTRACT D      
	INNER JOIN MNT_REINSURANCE_CONTRACT_TYPE M   ON M.CONTRACTTYPEID = D.CONTRACT_TYPE      
	INNER JOIN MNT_REINSURANCE_MAJORMINOR_PARTICIPATION MM ON MM.CONTRACT_ID = D.CONTRACT_ID     
	INNER JOIN MNT_REIN_COMAPANY_LIST  MRCL ON MM.REINSURANCE_COMPANY = MRCL.REIN_COMAPANY_ID      
	WHERE MM.PARTICIPATION_ID = (SELECT TOP 1 PARTICIPATION_ID FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION WHERE     
	MNT_REINSURANCE_MAJORMINOR_PARTICIPATION.CONTRACT_ID = D.CONTRACT_ID)   
	AND ISNULL(MRCL.REIN_COMAPANY_NAME,'') =  (SELECT TOP 1 PAYEE_ENTITY_NAME FROM ACT_CHECK_INFORMATION WHERE CHECK_ID=@CHECKID)  
	*/

	SELECT @CONTRACT_NUMBER  = MRC.CONTRACT_NUMBER
	FROM ACT_CHECK_INFORMATION CHK         
	INNER JOIN MNT_REINSURANCE_CONTRACT MRC ON MRC.CONTRACT_ID = CHK.PAYEE_ENTITY_ID
	WHERE CHK.CHECK_ID=@CHECKID

	-- ADDED BY ASFA(14-JAN-2008)- FOR UNMATCHED POLICY
	-- BLOCK BEGINS
	DECLARE @DummyPolicyId INT
	SELECT @DummyPolicyId = ISNULL(DUMMY_POLICY_ID,0) FROM CLM_CLAIM_INFO CCI JOIN CLM_ACTIVITY CA ON CA.CLAIM_ID=CCI.CLAIM_ID WHERE CHECK_ID=@CHECKID
	

	IF(@DummyPolicyId != 0)
	BEGIN
		select 
		aci.IS_COMMITED,aci.SPOOL_STATUS,
		upper(aci.PAYEE_ENTITY_NAME) AS PAYEE_ENTITY_NAME,
		ISNULL(CP2.NAME,ISNULL(CPY.FIRST_NAME,'') + ' ' + ISNULL(CPY.LAST_NAME,'')) PAYEE_ENTITY_NAME1,  
		aci.PAYEE_ADD1,aci.PAYEE_ADD2,aci.PAYEE_CITY,aci.PAYEE_STATE,aci.PAYEE_ZIP ,aci.CHECK_MEMO,                                                             
		--(ACT_BANK_INFORMATION) as                                                           
		aci.CHECK_TYPE,    ISNULL(ACI.COMM_TYPE,'') AS COMM_TYPE ,
        	CASE ISNULL(ACI.COMM_TYPE,'')  WHEN  'CAC' THEN UPPER(AGN.AGENCY_DISPLAY_NAME) ELSE '' END AS AGENCY_NAME ,               
		CASE WHEN ISNULL(upper(aci.PAYEE_ADD1),'') = '' THEN '' ELSE upper(aci.PAYEE_ADD1)+case when isnull(upper(aci.PAYEE_ADD2),'') = '' then '' else ', ' end END 
		+ISNULL(upper(aci.PAYEE_ADD2),'') AS ADDRESS_LINE_1   ,
		upper(aci.PAYEE_CITY) +    
		CASE WHEN ISNULL(upper(mcsl.STATE_NAME),'')= '' THEN '' ELSE ', ' END   +                                     
		ISNULL(upper(mcsl.STATE_NAME),'')+' '+ISNULL(upper(aci.PAYEE_ZIP),'') as ADDRESS_LINE_2,        

		CASE WHEN ISNULL(CDP.DUMMY_ADD1,'') = '' THEN '' ELSE CDP.DUMMY_ADD1 + case when isnull(CDP.DUMMY_ADD2,'') = '' then '' else ', ' end END
		+ISNULL(CDP.DUMMY_ADD2,'') as InsAddress1 , 
		CASE WHEN ISNULL(CDP.DUMMY_CITY,'') = '' THEN '' ELSE CDP.DUMMY_CITY+', ' END+                                            
		ISNULL((select top 1 STATE_NAME from MNT_COUNTRY_STATE_LIST  WHERE convert(varchar,STATE_ID) = CDP.DUMMY_STATE)                                  
		,'')+' '+ISNULL(CDP.DUMMY_ZIP,'') as InsAddress2,

        
		 ISNULL(CDP.INSURED_NAME,'') AS INSURED,                                        
		(select top 1 LOOKUP_VALUE_DESC from mnt_lookup_values where LOOKUP_UNIQUE_ID =aci.CHECK_TYPE) as CHECK_TYPE_DESC  ,                                    
		(select top 1 ACC_DISP_NUMBER from ACT_GL_ACCOUNTS WHERE ACCOUNT_ID=aci.ACCOUNT_ID)as ACC_DISP_NUMBER ,                                                              
		RIGHT('000000'+aci.CHECK_NUMBER,6) CHECK_NUMBER ,                                                         
		convert(varchar,aci.CHECK_DATE,101) as CHECK_DATE,           
		ISNULL(CONVERT(VARCHAR,aci.MONTH) + '/01/' + CONVERT(VARCHAR,aci.YEAR),convert(varchar,aci.CHECK_DATE,101))  as STMT_DATE,                                                             
		ISNULL(aci.CLAIM_TO_ORDER_DESC,'') AS CLAIM_TO_ORDER_DESC,                                                            
		'$'+convert(varchar(30),convert(money,aci.CHECK_AMOUNT),1) AS CHECK_AMOUNT ,    
                                                          
		case  when aci.GL_UPDATE='2' then 'VOID' else                                                              
		case when aci.IS_BNK_RECONCILED='Y' then 'RECONCILIED'                                                               
		when isnull(aci.IS_BNK_RECONCILED,'N')='N' or aci.IS_BNK_RECONCILED ='' then 'UNRECONCILIED' end                                                              
		end as Status,                               
		(select top 1 ACC_DISP_NUMBER from ACT_GL_ACCOUNTS WHERE ACCOUNT_ID=aci.OFFSET_ACC_ID) as OFFSET_ACC_ID,                       
		(select top 1 ACC_DISP_NUMBER from ACT_GL_ACCOUNTS WHERE ACCOUNT_ID=acdd.ACCOUNT_ID) as DIST_ACC_NUM,           
		acdd.NOTE, CASE WHEN @CONTRACT_NUMBER IS NOT NULL THEN @CONTRACT_NUMBER   
		ELSE REPLACE(aci.PAYEE_ENTITY_NAME,',', ' And ') END REINS_ID,    --ITRack 2994 14-Nov-07                         
		(select top 1 ACC_DISP_NUMBER from ACT_GL_ACCOUNTS WHERE ACCOUNT_ID=aacd.ACCOUNT_FOR_ADJUSTMENT) as AGENCY_ACC_NUM,   
		convert(varchar,convert(money,acdd.DISTRIBUTION_AMOUNT),1) AS DISTRIBUTION_AMOUNT,                                                            
		case isnull(aci.AVAILABLE_BALANCE,0) when 0 then 'No' else 'Yes' end as "Cleared",                                     
		/*case check_type when '9935' then CASE WHEN acoi.ITEM_TRAN_CODE LIKE '%CANCP%' THEN 'CANCEL'        
		WHEN acoi.ITEM_TRAN_CODE LIKE '%ENDP%' THEN 'CHANGE' ELSE 'OVER PAYMENT' END    
		when '2472' then ''     
		when '2474' then CASE WHEN acoi.ITEM_TRAN_CODE LIKE '%CANCP%' THEN 'CANCEL'          
		WHEN acoi.ITEM_TRAN_CODE LIKE '%ENDP%' THEN 'CHANGE' ELSE 'CHANGE' END                                                            
		when '9936' then CASE WHEN acoi.ITEM_TRAN_CODE LIKE '%CANCP%' THEN 'CANCEL'          
		WHEN acoi.ITEM_TRAN_CODE LIKE '%ENDP%' THEN 'CHANGE' ELSE 'REFUND' END                                        
		when '9937' then ''                                                            
		when '9938' then ''                                                            
		when '9940' then ''                                                            
		when '9945' then ''                                                            
		end */
		-- Changed by Shikha for itrack #5615.
		CASE WHEN CHECK_TYPE IN (2474,9935,9936)THEN 'REFUND' ELSE '' END + ' ' + isnull(CDP.policy_number ,'') chk_description,                                                            
		UPPER((select top 1 dbo.udf_Num_ToWords (aci.CHECK_AMOUNT))) AS check_amt,right('000000000'+abi.BANK_MICR_CODE,9) BANK_MICR_CODE,              
		right('0000000000'+abi.BANK_NUMBER,10) BANK_NUMBER,                                                           
		ISNULL(ABI.ROUTE_POSITION_CODE1 , '' ) + '-' + ISNULL(ABI.ROUTE_POSITION_CODE2,'') 
		+ '/' + ISNULL(ABI.ROUTE_POSITION_CODE3,'') AS BANK_NUMBER_TOP,
		UPPER(ABI.BANK_NAME) AS BANK_NAME , 
		UPPER(ISNULL(ABI.BANK_CITY,'') + ' ' + ISNULL(BS.STATE_NAME, '' )) AS BANK_ADDRESS ,

		abi.ROUTE_POSITION_CODE1,abi.ROUTE_POSITION_CODE2,abi.ROUTE_POSITION_CODE3,abi.ROUTE_POSITION_CODE4,                                                    
		abi.SIGN_FILE_1, abi.SIGN_FILE_2,                                                        
		acd.DEPOSIT_NUMBER,            
		convert(varchar,acd.DATE_COMMITED,101) AS DEPOSIT_TRAN_DATE,           
		convert(varchar,acoi.SOURCE_EFF_DATE,101) AS CHANGE_EFF_DATE,           
		(select top 1 ISNULL(ACC_DISP_NUMBER,'') from ACT_GL_ACCOUNTS WHERE ACCOUNT_ID=aci.ACCOUNT_ID) AS ACC_NUMBER,                                                        
		UPPER(aci.CHECK_NOTE) AS CHECK_NOTE, 
		(SELECT CASE WHEN T1.ACC_PARENT_ID IS NULL 
		THEN T1.ACC_DESCRIPTION + ' : ' +  ISNULL(T1.ACC_DISP_NUMBER,'')  
		ELSE  ISNULL(T2.ACC_DESCRIPTION,'') + ' - ' + T1.ACC_DESCRIPTION
		END AS ACC_DESCRIPTION
		FROM ACT_GL_ACCOUNTS t1 
		LEFT OUTER JOIN  ACT_GL_ACCOUNTS T2 ON T2.ACCOUNT_ID = T1.ACC_PARENT_ID 
		WHERE  t1.account_id = acdd.ACCOUNT_ID	) AS ACC_DESCRIPTION,
		-- ISNULL(ppp.PROCESS_ID, 0) AS PROCESS_ID,                                               
		CASE WHEN mal.AGENCY_DISPLAY_NAME IS NULL THEN '' ELSE 'AGENCY: ' + UPPER(mal.AGENCY_DISPLAY_NAME) END AS AGENCY_DISPLAY_NAME,                                                    
		avcd.REF_INVOICE_NO, avcd.REF_INVOICE_REF_NO, convert(varchar(30),convert(money,avcd.AMOUNT_TO_APPLY),1) AS AMOUNT_TO_APPLY,                                                    
		convert(varchar(30),convert(money,aacd.AMT_UNCOLLECTED_PREMIUM_AB*-1),1) AS AMT_UNCOLLECTED_PREMIUM_AB,                                               
		convert(varchar(30),convert(money,aacd.AMT_COMMISSION_PAYABLE_AB*-1),1) AS AMT_COMMISSION_PAYABLE_AB,                                               
		convert(varchar(30),convert(money,aacd.AMT_COMMISSION_PAYABLE_DB*-1),1) AS AMT_COMMISSION_PAYABLE_DB,                     
		convert(varchar(30),convert(money,aacd.AMT_AGAINST_OP *-1),1) AS AMOUNT_TO_APPLY_OP,                            
		--Ravindra(03-03-2008): No need to change sigh of DIFFERENCE_AMOUNT
		convert(varchar(30),convert(money,aacd.DIFFERENCE_AMOUNT),1) AS DIFFERENCE_AMOUNT,                            
		aacd.DESCRIPTION,                       
		aci.MONTH, aci.YEAR,                             
		--AAPD.TRAN_DESC AS CLAIM_DETAILS, 
		CCI.CLAIM_NUMBER AS CLAIM_DETAILS , 
		isnull(CP.FEDRERAL_ID,'') AS FEDERAL_ID,                    
		(select top 1 ACC_DISP_NUMBER from ACT_GL_ACCOUNTS where ACCOUNT_ID in (select DISTINCT AST_UNCOLL_PRM_AGENCY FROM ACT_GENERAL_LEDGER)) AS AST_UNCOLL_PRM_AGENCY,                         
		(select top 1 ACC_DISP_NUMBER from ACT_GL_ACCOUNTS where ACCOUNT_ID in (select DISTINCT LIB_COMM_PAYB_AGENCY_BILL FROM ACT_GENERAL_LEDGER)) AS LIB_COMM_PAYB_AGENCY_BILL,               
		(select ACC_DISP_NUMBER from ACT_GL_ACCOUNTS where ACCOUNT_ID in (select DISTINCT LIB_COMM_PAYB_DIRECT_BILL FROM ACT_GENERAL_LEDGER)) AS LIB_COMM_PAYB_DIRECT_BILL,                                           
		--Ravindra(05-30-2008): Join should be on agency_id column , for Complete App bonus checks entity id 
		-- contains CSR ID 
		(
			select (RTRIM(LTRIM(ISNULL(MAL.AGENCY_CODE,'')))+ ' - '+ convert(nvarchar(40),isnull(mal.NUM_AGENCY_CODE,''))) 
			from  MNT_AGENCY_LIST mal left outer join ACT_CHECK_INFORMATION aci 
			--on aci.PAYEE_ENTITY_ID = mal.AGENCY_ID 
			on aci.AGENCY_ID = mal.AGENCY_ID 
			where CHECK_ID=@CHECKID AND CHECK_TYPE=2472 
		) AS AGENCY_CODE
		from  ACT_CHECK_INFORMATION aci                                                            
		left outer join ACT_BANK_INFORMATION ABI on abi.account_id=aci.account_id      
		LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST BS ON ABI.BANK_STATE = CONVERT(VARCHAR,BS.STATE_ID)                  
		LEFT OUTER JOIN ACT_CUSTOMER_OPEN_ITEMS acoi ON acoi.IDEN_ROW_ID = aci.OPEN_ITEM_ROW_ID                                                          
		LEFT OUTER JOIN ACT_CURRENT_DEPOSIT_LINE_ITEMS acdli ON acdli.CD_LINE_ITEM_ID = acoi.SOURCE_ROW_ID                                                          
		LEFT OUTER JOIN ACT_CURRENT_DEPOSITS acd ON acd.DEPOSIT_ID = acdli.DEPOSIT_ID                                                
		
		--LEFT OUTER JOIN ACT_ACCOUNTS_POSTING_DETAILS AAPD ON ACI.CHECK_ID = AAPD.SOURCE_ROW_ID AND ACI.CHECK_AMOUNT = AAPD.TRANSACTION_AMOUNT                                           
		
		left outer join ACT_VENDOR_CHECK_DISTRIBUTION avcd on avcd.check_id = aci.check_id                                                    
		left outer join ACT_AGENCY_CHECK_DISTRIBUTION aacd on aacd.check_id = aci.check_id                                                         
		LEFT OUTER JOIN MNT_AGENCY_LIST mal ON mal.AGENCY_ID=aacd.AGENCY_ID                                                        
		LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL ON ACI.PAYEE_STATE = CONVERT(VARCHAR,MCSL.STATE_ID)                                            
		
		--LEFT OUTER JOIN CLM_CLAIM_INFO CCI ON ISNULL(AAPD.TRAN_DESC,'') = CCI.CLAIM_NUMBER                                        
	
		LEFT OUTER JOIN CLM_ACTIVITY CA ON aci.CHECK_ID=CA.CHECK_ID 
		LEFT OUTER JOIN CLM_CLAIM_INFO CCI ON CCI.CLAIM_ID = CA.CLAIM_ID

		LEFT OUTER JOIN CLM_PAYEE CPY ON CCI.CLAIM_ID = CPY.CLAIM_ID AND CA.ACTIVITY_ID=CPY.ACTIVITY_ID
		LEFT OUTER JOIN CLM_PARTIES CP2 ON CP2.CLAIM_ID = CPY.CLAIM_ID AND dbo.instring(replace(CPY.PARTY_ID,',',' ') ,CP2.PARTY_ID)>0
		LEFT OUTER JOIN CLM_DUMMY_POLICY CDP ON CCI.DUMMY_POLICY_ID=CDP.DUMMY_POLICY_ID
		LEFT OUTER JOIN CLM_PARTIES CP ON CCI.CLAIM_ID = CP.CLAIM_ID AND ISNULL(ACI.PAYEE_ENTITY_ID,0) = CP.PARTY_ID                                        
		LEFT OUTER JOIN ACT_DISTRIBUTION_DETAILS acdd ON acdd.GROUP_ID = aci.CHECK_ID AND acdd.GROUP_TYPE = 'CHQ'      
		LEFT OUTER JOIN MNT_AGENCY_LIST AGN ON ACI.AGENCY_ID = AGN.AGENCY_ID                                     
		where  aci.CHECK_ID = @CHECKID  and isnull(aci.IS_COMMITED,'N')='Y' and ISNULL(aci.SPOOL_STATUS,0)=0 --and aci.GL_UPDATE <> 2                          
		                                  
		IF(ISNULL(@CALLEDFOR,'') = '')                        
		BEGIN                  
     
			--Get Coverages                      

			SET @COVERAGES_DESC = ''                      
			DECLARE  CURCOVERAGE CURSOR  FOR                        
			(SELECT MC.COV_DES FROM ACT_CHECK_INFORMATION ACI         
			LEFT OUTER JOIN  CLM_ACTIVITY CA ON ACI.CHECK_ID = CA.CHECK_ID INNER JOIN CLM_ACTIVITY_RESERVE CAR ON CAR.CLAIM_ID = CA.CLAIM_ID   
			INNER JOIN CLM_ACTIVITY_PAYMENT CAP ON CAP.CLAIM_ID = CA.CLAIM_ID AND CAP.ACTIVITY_ID = CA.ACTIVITY_ID AND CAP.RESERVE_ID = CAR.RESERVE_ID                      
			LEFT JOIN MNT_CLAIM_COVERAGE MC ON  MC.COV_ID = CAR.COVERAGE_ID                      
			WHERE  ACI.CHECK_ID = @CHECKID AND CAP.PAYMENT_AMOUNT != 0                

			UNION                

			SELECT MC.COV_DES FROM ACT_CHECK_INFORMATION ACI                       
			LEFT OUTER JOIN  CLM_ACTIVITY CA ON ACI.CHECK_ID = CA.CHECK_ID                   
			INNER JOIN CLM_ACTIVITY_RESERVE CAR ON CAR.CLAIM_ID = CA.CLAIM_ID                   
			INNER JOIN CLM_ACTIVITY_EXPENSE CAP ON CAP.CLAIM_ID = CA.CLAIM_ID AND CAP.ACTIVITY_ID = CA.ACTIVITY_ID AND CAP.RESERVE_ID = CAR.RESERVE_ID                      
			LEFT JOIN MNT_CLAIM_COVERAGE MC ON  MC.COV_ID = CAR.COVERAGE_ID          
			WHERE  ACI.CHECK_ID = @CHECKID AND CAP.PAYMENT_AMOUNT != 0)                   
			
			OPEN CURCOVERAGE                          
			FETCH NEXT FROM CURCOVERAGE INTO @COV_TEMP                      
                      
			SET @COVERAGES_DESC = @COVERAGES_DESC + @COV_TEMP                       

			WHILE @@FETCH_STATUS = 0                           
			BEGIN                       

				FETCH NEXT FROM CURCOVERAGE INTO @COV_TEMP                      
				IF @@FETCH_STATUS = 0                      
				SET @COVERAGES_DESC = @COVERAGES_DESC +  ', 
' + @COV_TEMP                           
			END                          
                           
			CLOSE CURCOVERAGE                          
			DEALLOCATE CURCOVERAGE                          

			--Fetch data for Claim Checks Printing Reference Section                                    
			SELECT                        
			CTD.DETAIL_TYPE_DESCRIPTION AS ACTIVITY_REASON,CP.INVOICE_NUMBER,
			mal.AGENCY_DISPLAY_NAME AS CLAIM_AGENCY,                                    
			ISNULL(CDP.INSURED_NAME,'') AS CLAIM_INSURED,                                     
			CDP.POLICY_NUMBER, CCI.CLAIM_NUMBER,ACI.CHECK_NUMBER, CCI.CLAIM_ID,                                    
			CA.DESCRIPTION,                                    
			CONVERT(VARCHAR(30),GETDATE(),101) AS CHECK_PRINTED_DATE,                                    
			CONVERT(VARCHAR(30),CCI.LOSS_DATE,101) AS LOSS_DATE,                         
			@COVERAGES_DESC AS COVERAGES_DESC                                    
			FROM                                     
			ACT_CHECK_INFORMATION ACI LEFT OUTER JOIN  CLM_ACTIVITY CA ON ACI.CHECK_ID = CA.CHECK_ID                                    
			LEFT OUTER JOIN CLM_CLAIM_INFO CCI ON CA.CLAIM_ID = CCI.CLAIM_ID  
			LEFT OUTER JOIN CLM_DUMMY_POLICY CDP ON CCI.DUMMY_POLICY_ID=CDP.DUMMY_POLICY_ID                                  
			LEFT OUTER JOIN CLM_PAYEE CP ON CP.CLAIM_ID = CA.CLAIM_ID AND CP.ACTIVITY_ID=CA.ACTIVITY_ID                      
			LEFT OUTER JOIN CLM_TYPE_DETAIL CTD ON CTD.DETAIL_TYPE_ID = CA.ACTION_ON_PAYMENT
			left outer join ACT_AGENCY_CHECK_DISTRIBUTION aacd on aacd.check_id = aci.check_id                                                         
			LEFT OUTER JOIN MNT_AGENCY_LIST mal ON mal.AGENCY_ID=aacd.AGENCY_ID                                    
			WHERE ACI.CHECK_ID=@CHECKID                               
		END
	END
	else
	begin
		select 
		UPPER(MAL.AGENCY_DISPLAY_NAME) AS AGN_NAME , 
		CASE WHEN ISNULL(UPPER(MAL.AGENCY_ADD1),'') = '' THEN '' ELSE UPPER(MAL.AGENCY_ADD1)
		+CASE WHEN ISNULL(UPPER(MAL.AGENCY_ADD2),'') = '' THEN '' ELSE ', ' END END 
		+ISNULL(UPPER(MAL.AGENCY_ADD2),'') AS AGN_ADDRESS_LINE_1   ,
		UPPER(MAL.AGENCY_CITY) +    
		CASE WHEN ISNULL(UPPER(MAL.AGENCY_CITY),'')= '' THEN '' ELSE ', ' END   +                                     
		ISNULL(UPPER(AGNSTATE.STATE_NAME),'')+' '+ISNULL(UPPER(MAL.AGENCY_ZIP),'') AS AGN_ADDRESS_LINE_2, 

		aci.IS_COMMITED,aci.SPOOL_STATUS, upper(aci.PAYEE_ENTITY_NAME) AS  PAYEE_ENTITY_NAME,
		ISNULL(CP2.NAME,ISNULL(CPY.FIRST_NAME,'') + ' ' + ISNULL(CPY.LAST_NAME,'')) PAYEE_ENTITY_NAME1,  
		aci.PAYEE_ADD1,aci.PAYEE_ADD2,aci.PAYEE_CITY,aci.PAYEE_STATE,aci.PAYEE_ZIP ,aci.CHECK_MEMO,                              
		--(ACT_BANK_INFORMATION) as                                                           
		aci.CHECK_TYPE, ISNULL(ACI.COMM_TYPE,'') AS COMM_TYPE ,           
		CASE ISNULL(ACI.COMM_TYPE,'')  WHEN  'CAC' THEN UPPER(AGN.AGENCY_DISPLAY_NAME) ELSE '' END AS AGENCY_NAME ,           
		CASE WHEN ISNULL(upper(aci.PAYEE_ADD1),'') = '' THEN '' ELSE upper(aci.PAYEE_ADD1)+case when isnull(upper(aci.PAYEE_ADD2),'') = '' then '' else ', ' end END 
		+ISNULL(upper(aci.PAYEE_ADD2),'') AS ADDRESS_LINE_1   ,
		upper(aci.PAYEE_CITY) +    
		CASE WHEN ISNULL(upper(mcsl.STATE_NAME),'')= '' THEN '' ELSE ', ' END   +                                     
		ISNULL(upper(mcsl.STATE_NAME),'')+' '+ISNULL(upper(aci.PAYEE_ZIP),'') as ADDRESS_LINE_2, 
     
		 CASE WHEN ISNULL(ccl.CUSTOMER_ADDRESS1,'') = '' THEN '' ELSE ccl.CUSTOMER_ADDRESS1 + case when isnull(ccl.CUSTOMER_ADDRESS2,'') = '' then '' else ', ' end END
		+ISNULL(ccl.CUSTOMER_ADDRESS2,'') AS InsAddress1 ,
		CASE WHEN ISNULL(ccl.CUSTOMER_CITY,'') = '' THEN '' ELSE ccl.CUSTOMER_CITY+', ' END+                   
		ISNULL((select top 1 STATE_NAME from MNT_COUNTRY_STATE_LIST  WHERE convert(varchar,STATE_ID) = CCL.CUSTOMER_STATE) ,'')
		+' '+ISNULL(ccl.CUSTOMER_ZIP,'') as InsAddress2,  
      
		ISNULL(CCL.CUSTOMER_FIRST_NAME,'') + ' ' + 		CASE WHEN ISNULL(CCL.CUSTOMER_MIDDLE_NAME,'')='' THEN '' ELSE ISNULL(CCL.CUSTOMER_MIDDLE_NAME,'') 		+ ' ' END + ISNULL(CCL.CUSTOMER_LAST_NAME  ,'') AS INSURED,                                		(select top 




1

 LOOKUP_VALUE_DESC from mnt_lookup_values where LOOKUP_UNIQUE_ID =aci.CHECK_TYPE) as CHECK_TYPE_DESC  ,                                    
		(select top 1 ACC_DISP_NUMBER from ACT_GL_ACCOUNTS WHERE ACCOUNT_ID=aci.ACCOUNT_ID)as ACC_DISP_NUMBER ,                                                              
		RIGHT('000000'+aci.CHECK_NUMBER,6) CHECK_NUMBER ,                                                         
		convert(varchar,aci.CHECK_DATE,101) as CHECK_DATE,           
		ISNULL(CONVERT(VARCHAR,aci.MONTH) + '/01/' + CONVERT(VARCHAR,aci.YEAR),convert(varchar,aci.CHECK_DATE,101))  as STMT_DATE,   
		ISNULL(aci.CLAIM_TO_ORDER_DESC,'') AS CLAIM_TO_ORDER_DESC,                                                            
		'$'+convert(varchar(30),convert(money,aci.CHECK_AMOUNT),1) AS CHECK_AMOUNT ,                                                              
		case  when aci.GL_UPDATE='2' then 'VOID' else                                                              
		case when aci.IS_BNK_RECONCILED='Y' then 'RECONCILIED'                                                               
		when isnull(aci.IS_BNK_RECONCILED,'N')='N' or aci.IS_BNK_RECONCILED ='' then 'UNRECONCILIED' end                                                              
		end as Status,                                          
		(select top 1 ACC_DISP_NUMBER from ACT_GL_ACCOUNTS WHERE ACCOUNT_ID=aci.OFFSET_ACC_ID) as OFFSET_ACC_ID,                               
		(select top 1 ACC_DISP_NUMBER from ACT_GL_ACCOUNTS WHERE ACCOUNT_ID=acdd.ACCOUNT_ID) as DIST_ACC_NUM,           
		acdd.NOTE, CASE WHEN @CONTRACT_NUMBER IS NOT NULL THEN @CONTRACT_NUMBER   
		ELSE REPLACE(aci.PAYEE_ENTITY_NAME,',', ' And ') END REINS_ID,    --ITRack 2994 14-Nov-07               
		(select top 1 ACC_DISP_NUMBER from ACT_GL_ACCOUNTS WHERE ACCOUNT_ID=aacd.ACCOUNT_FOR_ADJUSTMENT) as AGENCY_ACC_NUM,                       
		convert(varchar,convert(money,acdd.DISTRIBUTION_AMOUNT),1) AS DISTRIBUTION_AMOUNT,                                                            
		case isnull(aci.AVAILABLE_BALANCE,0) when 0 then 'No' else 'Yes' end as "Cleared",                              
		/*case check_type when '9935' then CASE WHEN acoi.ITEM_TRAN_CODE LIKE '%CANCP%' THEN 'CANCEL'   
		WHEN acoi.ITEM_TRAN_CODE LIKE '%ENDP%' THEN 'CHANGE' ELSE 'OVER PAYMENT' END                                                           
		when '2472' then ''             
		when '2474' then CASE WHEN acoi.ITEM_TRAN_CODE LIKE '%CANCP%' THEN 'CANCEL'          
		WHEN acoi.ITEM_TRAN_CODE LIKE '%ENDP%' THEN 'CHANGE' ELSE 'CHANGE' END        
		when '9936' then CASE WHEN acoi.ITEM_TRAN_CODE LIKE '%CANCP%' THEN 'CANCEL'          
		WHEN acoi.ITEM_TRAN_CODE LIKE '%ENDP%' THEN 'CHANGE' ELSE 'REFUND' END               
		when '9937' then ''                                                            
		when '9938' then ''                                                            
		when '9940' then ''                     
		when '9945' then ''                 
		end */
		--Changed by Shikha for itrack #5615.
		CASE WHEN CHECK_TYPE IN (2474,9935,9936)THEN 'REFUND' ELSE '' END + ' ' + isnull(pcpl.policy_number ,'') chk_description,                                                
		UPPER((select top 1 dbo.udf_Num_ToWords (aci.CHECK_AMOUNT)) ) AS check_amt,right('000000000'+abi.BANK_MICR_CODE,9) BANK_MICR_CODE,              
		right('0000000000'+abi.BANK_NUMBER,10) BANK_NUMBER,                                              
		ISNULL(ABI.ROUTE_POSITION_CODE1 , '' ) + '-' + ISNULL(ABI.ROUTE_POSITION_CODE2,'') 
		+ '/' + ISNULL(ABI.ROUTE_POSITION_CODE3,'') AS BANK_NUMBER_TOP,
		UPPER(ABI.BANK_NAME ) AS BANK_NAME , 
		UPPER(ISNULL(ABI.BANK_CITY,'') + ' ' + ISNULL(BS.STATE_NAME, '' ) )  AS BANK_ADDRESS ,
	
		abi.ROUTE_POSITION_CODE1,abi.ROUTE_POSITION_CODE2,abi.ROUTE_POSITION_CODE3,abi.ROUTE_POSITION_CODE4,                                                    
		abi.SIGN_FILE_1, abi.SIGN_FILE_2,                                 
		acd.DEPOSIT_NUMBER,            
		convert(varchar,acd.DATE_COMMITED,101) AS DEPOSIT_TRAN_DATE,           
		convert(varchar,acoi.SOURCE_EFF_DATE,101) AS CHANGE_EFF_DATE,           
		(select top 1 ISNULL(ACC_DISP_NUMBER,'') from ACT_GL_ACCOUNTS WHERE ACCOUNT_ID=aci.ACCOUNT_ID) AS ACC_NUMBER,                                                        
		UPPER(aci.CHECK_NOTE) as CHECK_NOTE, 
		--(select top 1 ISNULL(ACC_DESCRIPTION,'') from ACT_GL_ACCOUNTS WHERE ACCOUNT_ID=acdd.ACCOUNT_ID) AS ACC_DESCRIPTION,
		--Modified by Praveen K
		(SELECT CASE WHEN T1.ACC_PARENT_ID IS NULL 
		THEN T1.ACC_DESCRIPTION  --+ ' : ' +  ISNULL(T1.ACC_DISP_NUMBER,'')  
		ELSE  ISNULL(T2.ACC_DESCRIPTION,'') + ' - ' + T1.ACC_DESCRIPTION
		END AS ACC_DESCRIPTION
		FROM ACT_GL_ACCOUNTS t1 
		LEFT OUTER JOIN  ACT_GL_ACCOUNTS T2 ON T2.ACCOUNT_ID = T1.ACC_PARENT_ID 
		WHERE  t1.account_id = acdd.ACCOUNT_ID	) AS ACC_DESCRIPTION,
		ISNULL(ppp.PROCESS_ID, 0) AS PROCESS_ID,                                               
		CASE WHEN mal.AGENCY_DISPLAY_NAME IS NULL THEN '' ELSE 'AGENCY: ' + UPPER(mal.AGENCY_DISPLAY_NAME) END AS AGENCY_DISPLAY_NAME,                                                    
		avcd.REF_INVOICE_NO, avcd.REF_INVOICE_REF_NO, convert(varchar(30),convert(money,avcd.AMOUNT_TO_APPLY),1) AS AMOUNT_TO_APPLY,                                                    
		convert(varchar(30),convert(money,aacd.AMT_UNCOLLECTED_PREMIUM_AB*-1),1) AS AMT_UNCOLLECTED_PREMIUM_AB,                                               
		convert(varchar(30),convert(money,aacd.AMT_COMMISSION_PAYABLE_AB*-1),1) AS AMT_COMMISSION_PAYABLE_AB,                                   
		convert(varchar(30),convert(money,aacd.AMT_COMMISSION_PAYABLE_DB*-1),1) AS AMT_COMMISSION_PAYABLE_DB,                                    
		convert(varchar(30),convert(money,aacd.AMT_AGAINST_OP*-1),1) AS AMOUNT_TO_APPLY_OP,                  
		--Ravindra(03-03-2008): No need to change sigh of DIFFERENCE_AMOUNT
		convert(varchar(30),convert(money,aacd.DIFFERENCE_AMOUNT),1) AS DIFFERENCE_AMOUNT,                            
		aacd.DESCRIPTION,                                                    
		aci.MONTH, aci.YEAR,                            
		--AAPD.TRAN_DESC AS CLAIM_DETAILS, 
		CCI.CLAIM_NUMBER AS CLAIM_DETAILS , 
		isnull(CP.FEDRERAL_ID,'') AS FEDERAL_ID, 
		(select top 1 ACC_DISP_NUMBER from ACT_GL_ACCOUNTS where ACCOUNT_ID in (select DISTINCT AST_UNCOLL_PRM_AGENCY FROM ACT_GENERAL_LEDGER)) AS AST_UNCOLL_PRM_AGENCY,                          
		(select top 1 ACC_DISP_NUMBER from ACT_GL_ACCOUNTS where ACCOUNT_ID in (select DISTINCT LIB_COMM_PAYB_AGENCY_BILL FROM ACT_GENERAL_LEDGER)) AS LIB_COMM_PAYB_AGENCY_BILL,                          
		(select ACC_DISP_NUMBER from ACT_GL_ACCOUNTS where ACCOUNT_ID in (select DISTINCT LIB_COMM_PAYB_DIRECT_BILL FROM ACT_GENERAL_LEDGER)) AS LIB_COMM_PAYB_DIRECT_BILL, 
		--Ravindra(05-30-2008): Join should be on agency_id column , for Complete App bonus checks entity id 
		-- contains CSR ID 
		(
			select (RTRIM(LTRIM(ISNULL(MAL.AGENCY_CODE,'')))+ ' - '+ convert(nvarchar(40),isnull(mal.NUM_AGENCY_CODE,''))) 
			from  MNT_AGENCY_LIST mal left outer join ACT_CHECK_INFORMATION aci 
			--on aci.PAYEE_ENTITY_ID = mal.AGENCY_ID 
			on aci.AGENCY_ID = mal.AGENCY_ID 
			where CHECK_ID=@CHECKID AND CHECK_TYPE=2472 
		) AS AGENCY_CODE
		from  ACT_CHECK_INFORMATION aci                                     
		left outer join pol_customer_policy_list pcpl           
		on pcpl.customer_id= aci.customer_id and pcpl.policy_id=aci.policy_id and aci.POLICY_VER_TRACKING_ID = pcpl.POLICY_VERSION_ID                                                        
		LEFT OUTER JOIN                           
		CLT_CUSTOMER_LIST CCL ON aci.CUSTOMER_ID= CCL.CUSTOMER_ID       
		left outer join ACT_BANK_INFORMATION ABI           
		on abi.account_id=aci.account_id             
		LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST BS ON ABI.BANK_STATE = CONVERT(VARCHAR,BS.STATE_ID)                            
		LEFT OUTER JOIN MNT_AGENCY_LIST mal ON                                                        
		mal.AGENCY_ID=pcpl.AGENCY_ID           
		LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST AGNSTATE ON MAL.AGENCY_STATE = AGNSTATE.STATE_ID                                              
		LEFT OUTER JOIN ACT_CUSTOMER_OPEN_ITEMS acoi ON            
		acoi.IDEN_ROW_ID = aci.OPEN_ITEM_ROW_ID          
		LEFT OUTER JOIN ACT_CURRENT_DEPOSIT_LINE_ITEMS acdli ON                                                          
		acdli.CD_LINE_ITEM_ID = acoi.SOURCE_ROW_ID                                                          
		LEFT OUTER JOIN ACT_CURRENT_DEPOSITS acd ON                                                          
		acd.DEPOSIT_ID = acdli.DEPOSIT_ID                                                
		
--		LEFT OUTER JOIN ACT_ACCOUNTS_POSTING_DETAILS AAPD ON                                          
--		ACI.CHECK_ID = AAPD.SOURCE_ROW_ID                                          
--		AND ACI.CHECK_AMOUNT = AAPD.TRANSACTION_AMOUNT                                           

		--Sumit:Check on amount has been added because the accounts posting table will have two entries for any                                          
		--accounting task (one for credit and one for debit). Hence to fetch unique record another condition                                           
		--on amount has been added at amount                                          

		left outer join POL_POLICY_PROCESS ppp on ppp.customer_id= aci.customer_id and ppp.policy_id=aci.policy_id and ppp.new_POLICY_VERSION_ID = pcpl.POLICY_VERSION_ID and pcpl.policy_status= ppp.POLICY_CURRENT_STATUS                                

		left outer join ACT_VENDOR_CHECK_DISTRIBUTION avcd on avcd.check_id = aci.check_id                                                  

		left outer join ACT_AGENCY_CHECK_DISTRIBUTION aacd on aacd.check_id = aci.check_id                                                         

		LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL ON ACI.PAYEE_STATE = CONVERT(VARCHAR,MCSL.STATE_ID)                                            
--		LEFT OUTER JOIN CLM_CLAIM_INFO CCI ON ISNULL(AAPD.TRAN_DESC,'') = CCI.CLAIM_NUMBER                                        

		LEFT OUTER JOIN CLM_ACTIVITY CA ON aci.CHECK_ID=CA.CHECK_ID 
		LEFT OUTER JOIN CLM_CLAIM_INFO CCI ON CCI.CLAIM_ID = CA.CLAIM_ID

		LEFT OUTER JOIN CLM_PAYEE CPY ON CCI.CLAIM_ID = CPY.CLAIM_ID AND CA.ACTIVITY_ID=CPY.ACTIVITY_ID
		LEFT OUTER JOIN CLM_PARTIES CP2 ON CP2.CLAIM_ID = CPY.CLAIM_ID AND dbo.instring(replace(CPY.PARTY_ID,',',' ') ,CP2.PARTY_ID)>0                                       
		LEFT OUTER JOIN CLM_PARTIES CP ON CCI.CLAIM_ID = CP.CLAIM_ID AND ISNULL(ACI.PAYEE_ENTITY_ID,0) = CP.PARTY_ID                                        
		LEFT OUTER JOIN ACT_DISTRIBUTION_DETAILS acdd ON acdd.GROUP_ID = aci.CHECK_ID AND acdd.GROUP_TYPE = 'CHQ'         
		LEFT OUTER JOIN MNT_AGENCY_LIST AGN ON ACI.AGENCY_ID = AGN.AGENCY_ID             
		where  aci.CHECK_ID = @CHECKID  and isnull(aci.IS_COMMITED,'N')='Y' and ISNULL(aci.SPOOL_STATUS,0)=0 --and aci.GL_UPDATE <> 2                                                        
        
        
                          
		IF(ISNULL(@CALLEDFOR,'') = '')                       
		BEGIN                                              
     
			--Get Coverages                      


			SET @COVERAGES_DESC = ''                      
			DECLARE  CURCOVERAGE CURSOR  FOR          
			(SELECT 
			/*CASE CAR.COVERAGE_ID WHEN 50001 THEN 'Medical'                      
			WHEN 50002 THEN 'Work Loss'                      
			WHEN 50003 THEN 'Death Benefits'                      
			WHEN 50004 THEN 'Survivor''s Benefits'  
			WHEN 50005 THEN 'Single Limits Liability (BI Split Limits)'                      
			WHEN 50006 THEN 'Single Limits Liability (PD)'               
			WHEN 50007 THEN 'Uninsured Motorists (BI Split Limits)'                      
			WHEN 50008 THEN 'Uninsured Motorists (PD)'      
			WHEN 50009 THEN 'Underinsured Motorists (BI Split Limits)'                      
			WHEN 50010 THEN 'Underinsured Motorists (PD)'                      
			ELSE MC.COV_DES END AS COV_DES ,*/
			
			--Added for Itrack Issue 6626 on 26 Oct 09
			CASE  WHEN ISNULL(MC.COV_DES,'')='' THEN MC1.COV_DES ELSE MC.COV_DES END AS COV_DES 

			FROM ACT_CHECK_INFORMATION ACI         
			LEFT OUTER JOIN  CLM_ACTIVITY CA                                     
			ON                                     
			ACI.CHECK_ID = CA.CHECK_ID    
			INNER JOIN CLM_ACTIVITY_RESERVE CAR                      
			ON CAR.CLAIM_ID = CA.CLAIM_ID                      
			--AND CAR.ACTIVITY_ID = CA.ACTIVITY_ID              
			INNER JOIN CLM_ACTIVITY_PAYMENT CAP                      
			ON CAP.CLAIM_ID = CA.CLAIM_ID                   
			AND CAP.ACTIVITY_ID = CA.ACTIVITY_ID                      
			AND CAP.RESERVE_ID = CAR.RESERVE_ID                      
			LEFT JOIN MNT_COVERAGE MC ON  MC.COV_ID = CAR.COVERAGE_ID                      
			LEFT JOIN MNT_COVERAGE_extra MC1 ON  MC1.COV_ID = CAR.COVERAGE_ID     
			WHERE  ACI.CHECK_ID = @CHECKID AND CAP.PAYMENT_AMOUNT != 0                

			UNION                

			SELECT 
			/*CASE CAR.COVERAGE_ID WHEN 50001 THEN 'Medical'                      
			WHEN 50002 THEN 'Work Loss' WHEN 50003 THEN 'Death Benefits'                      
			WHEN 50004 THEN 'Survivor''s Benefits'                      
			WHEN 50005 THEN 'Single Limits Liability (BI Split Limits)'       
			WHEN 50006 THEN 'Single Limits Liability (PD)'                      
			WHEN 50007 THEN 'Uninsured Motorists (BI Split Limits)'                 
			WHEN 50008 THEN 'Uninsured Motorists (PD)'                   
			WHEN 50009 THEN 'Underinsured Motorists (BI Split Limits)'                      
			WHEN 50010 THEN 'Underinsured Motorists (PD)'         
			ELSE MC.COV_DES END AS COV_DES ,*/

			--Added for Itrack Issue 6626 on 26 Oct 09
			CASE  WHEN ISNULL(MC.COV_DES,'')='' THEN MC1.COV_DES ELSE MC.COV_DES END AS COV_DES 

			FROM ACT_CHECK_INFORMATION ACI                       
			LEFT OUTER JOIN  CLM_ACTIVITY CA                               
			ON                                     
			ACI.CHECK_ID = CA.CHECK_ID                                    
			INNER JOIN CLM_ACTIVITY_RESERVE CAR                      
			ON CAR.CLAIM_ID = CA.CLAIM_ID                      
			--AND CAR.ACTIVITY_ID = CA.ACTIVITY_ID                      
			INNER JOIN CLM_ACTIVITY_EXPENSE CAP   
			ON CAP.CLAIM_ID = CA.CLAIM_ID                      
			AND CAP.ACTIVITY_ID = CA.ACTIVITY_ID      
			AND CAP.RESERVE_ID = CAR.RESERVE_ID                      
			LEFT JOIN MNT_COVERAGE MC ON  MC.COV_ID = CAR.COVERAGE_ID   
			LEFT JOIN MNT_COVERAGE_extra MC1 ON  MC1.COV_ID = CAR.COVERAGE_ID                    

			WHERE  ACI.CHECK_ID = @CHECKID AND CAP.PAYMENT_AMOUNT != 0)      
             
			OPEN CURCOVERAGE                          
			FETCH NEXT FROM CURCOVERAGE INTO @COV_TEMP                      
                      
			SET @COVERAGES_DESC = @COVERAGES_DESC + @COV_TEMP                       

			WHILE @@FETCH_STATUS = 0                           
			BEGIN                       

				FETCH NEXT FROM CURCOVERAGE INTO @COV_TEMP                      
				IF @@FETCH_STATUS = 0             
				SET @COVERAGES_DESC = @COVERAGES_DESC +  ', 
' + @COV_TEMP                          
			END                          
       
			CLOSE CURCOVERAGE                          
			DEALLOCATE CURCOVERAGE                          

			--Fetch data for Claim Checks Printing Reference Section                                    
			SELECT   
			CTD.DETAIL_TYPE_DESCRIPTION AS ACTIVITY_REASON,CP.INVOICE_NUMBER,MGL.AGENCY_DISPLAY_NAME AS CLAIM_AGENCY,                                    
			ISNULL(CCL.CUSTOMER_FIRST_NAME,'') + ' ' + CASE WHEN ISNULL(CCL.CUSTOMER_MIDDLE_NAME,'')='' THEN '' ELSE ISNULL(CCL.CUSTOMER_MIDDLE_NAME,'') + ' ' END + ISNULL(CCL.CUSTOMER_LAST_NAME  ,'') AS CLAIM_INSURED,                 
			PCPL.POLICY_NUMBER, CCI.CLAIM_NUMBER,ACI.CHECK_NUMBER, CCI.CLAIM_ID,                                    
			CA.DESCRIPTION,                                    
			CONVERT(VARCHAR(30),GETDATE(),101) AS CHECK_PRINTED_DATE, 
			CONVERT(VARCHAR(30),CCI.LOSS_DATE,101) AS LOSS_DATE,                         
			@COVERAGES_DESC AS COVERAGES_DESC                                    
			FROM 
			ACT_CHECK_INFORMATION ACI LEFT OUTER JOIN  CLM_ACTIVITY CA                                     
			ON                                     
			ACI.CHECK_ID = CA.CHECK_ID                 
			LEFT OUTER JOIN    
			CLM_CLAIM_INFO CCI ON CA.CLAIM_ID = CCI.CLAIM_ID                                    
			LEFT OUTER JOIN                                     
			POL_CUSTOMER_POLICY_LIST PCPL                                     
			ON                                     
			CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID                              
			AND                                     
			CCI.POLICY_ID=PCPL.POLICY_ID                                    
			AND                                     
			PCPL.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID                                    
			LEFT OUTER JOIN                           
			CLT_CUSTOMER_LIST CCL ON CCI.CUSTOMER_ID= CCL.CUSTOMER_ID                                    
			LEFT OUTER JOIN                                     
			MNT_AGENCY_LIST MGL ON MGL.AGENCY_ID = PCPL.AGENCY_ID                                    
			LEFT OUTER JOIN                                     
			CLM_PAYEE CP ON CP.CLAIM_ID = CA.CLAIM_ID AND CP.ACTIVITY_ID=CA.ACTIVITY_ID                      
			LEFT OUTER JOIN                        
			CLM_TYPE_DETAIL CTD ON CTD.DETAIL_TYPE_ID = CA.ACTION_ON_PAYMENT     
			WHERE                                     
			ACI.CHECK_ID=@CHECKID                               
		END                                                           
	END   
end

--go
--exec Proc_GetPDFChecksInfo 2912
--rollback tran







GO

