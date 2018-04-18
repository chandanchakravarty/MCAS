IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_CHECK_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_CHECK_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN
--DROP PROC dbo.Proc_InsertACT_CHECK_INFORMATION                        
--GO
CREATE PROC [dbo].[Proc_InsertACT_CHECK_INFORMATION]                        
(                        
 @CHECK_ID        int out,                        
 @CHECK_TYPE       nvarchar(10),                        
 @SELECT_FROM       nvarchar(10),                        
 @ACCOUNT_ID       int,                        
 @CHECK_NUMBER       nvarchar(40),                        
 @CHECK_DATE       datetime,                        
 @CHECK_AMOUNT       decimal(18,2),                        
 @CHECK_NOTE       nvarchar(200),                        
 @PAYEE_ENTITY_ID      int,                        
 @PAYEE_ENTITY_TYPE      nvarchar(10),                        
 @PAYEE_ENTITY_NAME      nvarchar(510),                        
 @PAYEE_ADD1       nvarchar(140),                        
 @PAYEE_ADD2       nvarchar(140),                        
 @PAYEE_CITY       nvarchar(80),                        
 @PAYEE_STATE       nvarchar(60),                        
 @PAYEE_ZIP        nvarchar(24),                        
 @PAYEE_NOTE       nvarchar(200),                        
 @CREATED_IN       nvarchar(2),                        
 @DIV_ID        smallint,                        
 @DEPT_ID        smallint,                        
 @PC_ID         smallint,                        
 @IS_COMMITED       nchar(2),                        
 @DATE_COMMITTED      datetime,                        
 @COMMITED_BY       int,                        
 @IN_RECON        nchar(2),                        
 @AVAILABLE_BALANCE      decimal(9),                        
 @CUSTOMER_ID       int,                        
 @POLICY_ID        smallint,                        
 @POLICY_VER_TRACKING_ID smallint,                        
 @GL_UPDATE        nchar(2),                        
 @IS_BNK_RECONCILED      nchar(2),                        
 @CHECKSIGN_1       nvarchar(200),                        
 @CHECKSIGN_2       nvarchar(200) ,                        
 @CHECK_MEMO       nvarchar(140),                        
 @IS_BNK_RECONCILED_VOID varchar(2),                        
 @IN_BNK_RECON       int,                        
 @SPOOL_STATUS       int,                        
 @TRAN_TYPE        int,                        
 @IS_DISPLAY_ON_STUB     char(1),                        
 @IS_ACTIVE        nchar(2),                        
 @CREATED_BY       int,                        
 @CREATED_DATETIME      datetime,                        
 @MODIFIED_BY       int,                        
 @LAST_UPDATED_DATETIME  datetime,                        
 @MANUAL_CHECK    Char(1),                        
 @OPEN_ITEM_ID    int ,      ---string with comma sep values --insert in new tble    
 @OPEN_ITEM_LIST  varchar(500)= null,
 @TEMP_CHECK_ID Int = null   ,          
 @MONTH INT = NULL,          
 @YEAR INT = NULL,          
 @COMM_TYPE varchar(10) = NULL,        
 @PAYMENT_MODE INT = null                   
)                        
AS                        
BEGIN                        
          
SET @COMM_TYPE =RTRIM(LTRIM(@COMM_TYPE))
--DECLARE @OPEN_ITEM_ID INT
--set @OPEN_ITEM_ID = DBO.PIECE(@OPEN_ITEM_LIST,',','1')
--SELECT @OPEN_ITEM_ID
--Declare constants for Check Type          
          
DECLARE @ACC_Check Int          
DECLARE @RPC_Check Int          
DECLARE @ROP_Check Int          
DECLARE @RSC_Check Int          
DECLARE @CC_Check Int          
DECLARE @VC_Check Int          
DECLARE @MOC_Check Int          
DECLARE @REC_Check Int          
          
SET @ACC_Check = 2472          
SET @RPC_Check = 2474          
SET @ROP_Check = 9935          
SET @RSC_Check = 9936          
SET @CC_Check = 9937          
SET @VC_Check = 9938          
SET @MOC_Check = 9940          
SET @REC_Check = 9945     
DECLARE @AGENCY_ID INT  
     
               
declare @START_CHECK_NUMBER int                         

--           
-- --if chexck number is not supplied next no. is auto assigned                        
-- if @CHECK_NUMBER=null or len(@CHECK_NUMBER)=0                        
-- begin    
--  select @START_CHECK_NUMBER=START_CHECK_NUMBER from ACT_BANK_INFORMATION with (nolock) where ACCOUNT_ID=@ACCOUNT_ID                        
--  select @CHECK_NUMBER=isnull(max(CAST(CHECK_NUMBER as Int)),@START_CHECK_NUMBER-1)+1 from                 
-- ACT_CHECK_INFORMATION with (nolock) where ACCOUNT_ID=@ACCOUNT_ID           
-- end                        
                
--Check if current check no. already exists in system                        
-- if exists (select CHECK_ID from ACT_CHECK_INFORMATION with (nolock) where ACCOUNT_ID=@ACCOUNT_ID and CHECK_NUMBER=@CHECK_NUMBER and IS_IN_CURRENT_SEQUENCE='Y')                        
-- Begin                        
--  set @CHECK_ID=-1                         
--  return -1                        
-- end     

if @PAYMENT_MODE = 0  
	set    @PAYMENT_MODE = null                 
                    
--check if check no. exceeds max check no. allotted to current series. Auto reset is performed if it is.                         
declare @maxCheckNo int                        
select @maxCheckNo=END_CHECK_NUMBER from ACT_BANK_INFORMATION with (nolock) where ACCOUNT_ID=@ACCOUNT_ID    
                    
if  @CHECK_NUMBER>@maxCheckNo                        
Begin --Auto reset                        
	update ACT_CHECK_INFORMATION set IS_IN_CURRENT_SEQUENCE='N' where ACCOUNT_ID=@ACCOUNT_ID                        
	set @CHECK_NUMBER=@START_CHECK_NUMBER                  
	--set @CHECK_ID=-2                        
	--return -1                        
end                         
                        
                        
--Start-Offset account for the selected check type is fetched from posting interface of ACT_GENERAL_LEDGER                        
declare @OFFSET_ACC_ID int                        
declare @FISCAL_ID int                        
select @FISCAL_ID=FISCAL_ID from ACT_GENERAL_LEDGER with (nolock) where @CHECK_DATE>=FISCAL_BEGIN_DATE and @CHECK_DATE<=FISCAL_END_DATE                        
                        
        
select @OFFSET_ACC_ID =                         
(case @CHECK_TYPE                         
when @ACC_Check then                         
(select  LIB_COMM_PAYB_AGENCY_BILL                         
from ACT_GENERAL_LEDGER with (nolock)                        
where FISCAL_ID = (@FISCAL_ID))                        
when @RPC_Check  then                         
(select  AST_UNCOLL_PRM_CUSTOMER                         
from ACT_GENERAL_LEDGER with (nolock)                        
where FISCAL_ID = (@FISCAL_ID))                        
when @ROP_Check  then                         
(select  AST_UNCOLL_PRM_CUSTOMER                         
from ACT_GENERAL_LEDGER with (nolock)                        
where FISCAL_ID = (@FISCAL_ID))                        
when @RSC_Check  then                         
(select AST_UNCOLL_PRM_CUSTOMER      -- Ravindra (03-02-2007) Actual cash transaction will alawys  affect Debtors        
--AST_PRM_WRIT_SUSPENSE_DIRECT_BILL                         
from ACT_GENERAL_LEDGER with (nolock)                        
where FISCAL_ID = (@FISCAL_ID))                        
when @CC_Check  then                         
(select  LIB_COMM_PAYB_AGENCY_BILL                         
from ACT_GENERAL_LEDGER with (nolock)                        
where FISCAL_ID = (@FISCAL_ID))                        
when @VC_Check   then                         
(select  LIB_VENDOR_PAYB                         
from ACT_GENERAL_LEDGER with (nolock)                        
where FISCAL_ID = (@FISCAL_ID))                        
end)              
          
 --End-Offset account for the selected check type is fetched from posting interface of ACT_GENERAL_LEDGER                        
    
/**************************************************************************************          
 Updating open items of entity for which check is created  For Customer checks: 
   Premium Refund Checks for Return Premium Payment  
   Premium Refund Checks for Over Payment                        
   Premium Refund Checks for Suspense Amount                           
***************************************************************************************** */          
DECLARE @UPDATE_QUERY VARCHAR(2000)
SET @UPDATE_QUERY = 'UPDATE ACT_CUSTOMER_OPEN_ITEMS SET IS_CHECK_CREATED=''Y''  WHERE IDEN_ROW_ID IN ' 


if (@CHECK_TYPE=@RPC_Check or @CHECK_TYPE=@ROP_Check)-- or @CHECK_TYPE=@RSC_Check                         
BEGIN                        
	/*update ACT_CUSTOMER_OPEN_ITEMS                         
	set IS_CHECK_CREATED='Y'                         
	--where IDEN_ROW_ID=@OPEN_ITEM_ID
	WHERE IDEN_ROW_ID IN (@OPEN_ITEM_LIST)*/

	--getting payee information FOR AUTO CHECKS                        
	IF @MANUAL_CHECK='N'                        
	select                         
	@PAYEE_ENTITY_NAME= ISNULL(CUSTOMER_FIRST_NAME,'')+' '+ISNULL(CUSTOMER_LAST_NAME,'') , @PAYEE_ENTITY_ID=cl.CUSTOMER_ID,@PAYEE_ADD1=CUSTOMER_ADDRESS1,                        
	@PAYEE_ADD2=CUSTOMER_ADDRESS2,                        
	@PAYEE_CITY=CUSTOMER_CITY,                        
	@PAYEE_STATE=CUSTOMER_STATE,                        
	@PAYEE_ZIP=CUSTOMER_ZIP                        
	from                         
	CLT_CUSTOMER_LIST  cl with (nolock)                         
	inner join ACT_CUSTOMER_OPEN_ITEMS  coi with (nolock) on cl.CUSTOMER_ID=coi.CUSTOMER_ID                        
	WHERE COI.IDEN_ROW_ID = @OPEN_ITEM_ID                        
	

SET @UPDATE_QUERY = @UPDATE_QUERY + '(' + @OPEN_ITEM_LIST + ')'
EXEC (@UPDATE_QUERY)

END      
  
  
--In Suspense Checks Default Agency Info in Check Payee Info   
--Modified on 21 Nov 2007  
IF(@CHECK_TYPE = @RSC_CHECK)  
BEGIN  
	/*UPDATE ACT_CUSTOMER_OPEN_ITEMS                         
	SET IS_CHECK_CREATED='Y'                         
	--WHERE IDEN_ROW_ID=@OPEN_ITEM_ID      
	WHERE IDEN_ROW_ID IN (@OPEN_ITEM_LIST)*/
	SET @UPDATE_QUERY = @UPDATE_QUERY + '(' + @OPEN_ITEM_LIST + ')'
	EXEC (@UPDATE_QUERY)
	IF @MANUAL_CHECK='N'         
	BEGIN  
		DECLARE @IS_HOME_EMPLOYEE INT   

		SELECT   
		@AGENCY_ID =  AGENCY_ID ,  
		@IS_HOME_EMPLOYEE = ISNULL(IS_HOME_EMP,0)  
		FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)   
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VER_TRACKING_ID  

		IF(@IS_HOME_EMPLOYEE = 0)  
		BEGIN  
			SELECT   
			@PAYEE_ENTITY_NAME = ISNULL(AGENCY_DISPLAY_NAME,''),  
			@PAYEE_ENTITY_ID = MNT_AGENCY_LIST.AGENCY_ID,  
			@PAYEE_ADD1 = ISNULL(AGENCY_ADD1,''),  
			@PAYEE_ADD2 = ISNULL(AGENCY_ADD2,''),  
			@PAYEE_CITY = AGENCY_CITY,  
			@PAYEE_STATE = AGENCY_STATE,  
			@PAYEE_ZIP = AGENCY_ZIP   
			FROM MNT_AGENCY_LIST WITH(NOLOCK)  
			WHERE AGENCY_ID = @AGENCY_ID  
		END  
		ELSE  
		BEGIN  
		   SELECT                         
		   @PAYEE_ENTITY_NAME= ISNULL(CUSTOMER_FIRST_NAME,'')+' '+ISNULL(CUSTOMER_LAST_NAME,'') ,  
		   @PAYEE_ENTITY_ID=CL.CUSTOMER_ID,  
		   @PAYEE_ADD1=CUSTOMER_ADDRESS1,                        
		   @PAYEE_ADD2=CUSTOMER_ADDRESS2,                        
		   @PAYEE_CITY=CUSTOMER_CITY,                        
		   @PAYEE_STATE=CUSTOMER_STATE,                        
		   @PAYEE_ZIP=CUSTOMER_ZIP                        
		   FROM                         
		   CLT_CUSTOMER_LIST  CL WITH (NOLOCK)                         
		   INNER JOIN ACT_CUSTOMER_OPEN_ITEMS  COI WITH (NOLOCK) ON CL.CUSTOMER_ID=COI.CUSTOMER_ID                        
		   WHERE COI.IDEN_ROW_ID = @OPEN_ITEM_ID       
		   
		END  
	END  
END  
  
--END @RSC_CHECK         
              
 -- Vendor Checks              
IF @CHECK_TYPE=@VC_Check 
BEGIN   
	SELECT DISTINCT                        
	@PAYEE_ENTITY_NAME=ISNULL(COMPANY_NAME,''),          
	@PAYEE_ENTITY_ID=VL.VENDOR_ID,            
	@PAYEE_ADD1=CHK_MAIL_ADD1,                        
	@PAYEE_ADD2=CHK_MAIL_ADD2,                        
	@PAYEE_CITY=CHK_MAIL_CITY,                        
	@PAYEE_STATE=CHK_MAIL_STATE,                        
	@PAYEE_ZIP=CHK_MAIL_ZIP                   
	FROM                         
	MNT_VENDOR_LIST  VL WITH (NOLOCK)                         
	INNER JOIN TEMP_ACT_CHECK_INFORMATION  CI WITH (NOLOCK) ON CI.PAYEE_ENTITY_ID=VL.VENDOR_ID        
	WHERE CI.PAYEE_ENTITY_ID = @PAYEE_ENTITY_ID                
END              
        
IF @CHECK_TYPE = @REC_Check           
BEGIN           
 
	--From Maintanenece state being saved as TEXT state name   
	SELECT TOP 1      
	@PAYEE_ENTITY_NAME =MRCL.REIN_COMAPANY_NAME,  
	@PAYEE_ADD1 = MRCL.M_REIN_COMPANY_ADD_1,      
	@PAYEE_ADD2 = MRCL.M_RREIN_COMPANY_ADD_2,      
	@PAYEE_CITY = MRCL.M_REIN_COMPANY_CITY,      
	--@PAYEE_STATE = MRCL.M_REIN_COMPANY_STATE,      
	@PAYEE_STATE = SL.STATE_ID,  
	@PAYEE_ZIP= MRCL.M_REIN_COMPANY_ZIP        
	FROM MNT_REINSURANCE_CONTRACT MRC 
	INNER JOIN MNT_REINSURANCE_MAJORMINOR_PARTICIPATION MM ON MM.CONTRACT_ID = MRC.CONTRACT_ID      
	INNER JOIN MNT_REIN_COMAPANY_LIST  MRCL ON MM.REINSURANCE_COMPANY = MRCL.REIN_COMAPANY_ID      
	--INNER JOIN MNT_CONTRACT_NAME M  ON M.CONTRACT_NAME_ID  = MRC.CONTRACT_NAME_ID        
	LEFT JOIN MNT_COUNTRY_STATE_LIST SL  ON SL.STATE_NAME  = MRCL.M_REIN_COMPANY_STATE     
	where  MM.PARTICIPATION_ID = (SELECT TOP 1 PARTICIPATION_ID FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION WHERE   
	MNT_REINSURANCE_MAJORMINOR_PARTICIPATION.CONTRACT_ID = @PAYEE_ENTITY_ID)  
	--WHERE MRC.CONTRACT_ID = @PAYEE_ENTITY_ID      

      
END          
          
-- Agency Checks              
IF @CHECK_TYPE=@ACC_Check          
BEGIN 
	SELECT 
	@PAYEE_ENTITY_NAME= ISNULL(PAYEE_ENTITY_NAME , '') ,
	@PAYEE_ENTITY_ID=PAYEE_ENTITY_ID,
	@PAYEE_ADD1=PAYEE_ADD1,          
	@PAYEE_ADD2=PAYEE_ADD2,          
	@PAYEE_CITY=PAYEE_CITY,
	@PAYEE_STATE=PAYEE_STATE,
	@PAYEE_ZIP=PAYEE_ZIP  ,
	@AGENCY_ID  = AGENCY_ID ,
	@COMM_TYPE = COMM_TYPE   ,
	@PAYEE_ENTITY_TYPE = PAYEE_ENTITY_TYPE
	FROM TEMP_ACT_CHECK_INFORMATION
	WHERE CHECK_ID = @TEMP_CHECK_ID  
END              
                        
          
select @CHECK_ID= isnull(max(CHECK_ID)+1,1) from ACT_CHECK_INFORMATION with (nolock)                        
     
INSERT INTO ACT_CHECK_INFORMATION                        
(                        
CHECK_ID, CHECK_TYPE, SELECT_FROM, ACCOUNT_ID,          
-- CHECK_NUMBER,              -- Check Number will be asssigned at printing          
CHECK_DATE, CHECK_AMOUNT, CHECK_NOTE, PAYEE_ENTITY_ID, PAYEE_ENTITY_TYPE,                        
PAYEE_ENTITY_NAME, PAYEE_ADD1, PAYEE_ADD2, PAYEE_CITY, PAYEE_STATE, PAYEE_ZIP,                        
PAYEE_NOTE, CREATED_IN, DIV_ID, DEPT_ID, PC_ID, IS_COMMITED, DATE_COMMITTED,                        
COMMITED_BY, IN_RECON, AVAILABLE_BALANCE, CUSTOMER_ID, POLICY_ID, POLICY_VER_TRACKING_ID,                        
GL_UPDATE, IS_BNK_RECONCILED, CHECKSIGN_1, CHECKSIGN_2, CHECK_MEMO, IS_BNK_RECONCILED_VOID,                        
IN_BNK_RECON, SPOOL_STATUS, TRAN_TYPE, IS_DISPLAY_ON_STUB, IS_ACTIVE, CREATED_BY,                        
CREATED_DATETIME, MODIFIED_BY, LAST_UPDATED_DATETIME, OFFSET_ACC_ID, IS_IN_CURRENT_SEQUENCE,                        
MANUAL_CHECK, OPEN_ITEM_ROW_ID ,[MONTH],[YEAR],COMM_TYPE,PAYMENT_MODE    , AGENCY_ID                    
)                        
VALUES                        
(                        
@CHECK_ID, @CHECK_TYPE, @SELECT_FROM, @ACCOUNT_ID,           
--  @CHECK_NUMBER,                        
@CHECK_DATE, @CHECK_AMOUNT, @CHECK_NOTE, @PAYEE_ENTITY_ID, @PAYEE_ENTITY_TYPE,                        
@PAYEE_ENTITY_NAME, @PAYEE_ADD1, @PAYEE_ADD2, @PAYEE_CITY, @PAYEE_STATE, @PAYEE_ZIP,                        
@PAYEE_NOTE, @CREATED_IN, @DIV_ID, @DEPT_ID, @PC_ID, @IS_COMMITED, @DATE_COMMITTED,                        
@COMMITED_BY, @IN_RECON, @AVAILABLE_BALANCE, @CUSTOMER_ID, @POLICY_ID, @POLICY_VER_TRACKING_ID,                        
@GL_UPDATE, @IS_BNK_RECONCILED, @CHECKSIGN_1, @CHECKSIGN_2, @CHECK_MEMO, @IS_BNK_RECONCILED_VOID,                        
@IN_BNK_RECON, @SPOOL_STATUS, @TRAN_TYPE, @IS_DISPLAY_ON_STUB, @IS_ACTIVE, @CREATED_BY,     
@CREATED_DATETIME, @MODIFIED_BY, @LAST_UPDATED_DATETIME, @OFFSET_ACC_ID, 'Y',   
@MANUAL_CHECK, @OPEN_ITEM_ID  ,@MONTH,@YEAR   ,@COMM_TYPE,@PAYMENT_MODE   , @AGENCY_ID                  
)                        

  
--Added by Shikha for #5615 on 12/11/09. 
DECLARE @IDEN_ID INT 
DECLARE @AMOUNT	 DECIMAL(18,2)
DECLARE CurIden_iD CURSOR
FOR SELECT IDEN_ROW_ID, AMOUNT from FnGetIdenType (@OPEN_ITEM_LIST)
OPEN CurIden_iD
FETCH NEXT FROM CurIden_iD INTO @IDEN_ID, @AMOUNT
WHILE (@@FETCH_STATUS = 0)
BEGIN
	INSERT INTO ACT_CHECK_OPEN_ITEMS (CHECK_ID, IDEN_ROW_ID, AMOUNT)
	VALUES (@CHECK_ID,@IDEN_ID, @AMOUNT)
FETCH NEXT FROM CurIden_iD INTO @IDEN_ID,@AMOUNT
END
CLOSE CurIden_iD
DEALLOCATE CurIden_iD 
--End of addition     
          
IF @CHECK_TYPE = @VC_Check         -- Vendor Checks          
BEGIN              
	UPDATE  ACT_VENDOR_CHECK_DISTRIBUTION SET CHECK_ID = @CHECK_ID,           
	IS_CHECK_CREATED = 'Y'          
	WHERE CHECK_ID = @TEMP_CHECK_ID          
END          
          
IF @CHECK_TYPE = @ACC_Check -- Agency Commission Check           
BEGIN              
	--Ravindra(06-18-2008): Change in approach .Use temp table for storing temp data
--	UPDATE  ACT_AGENCY_CHECK_DISTRIBUTION  SET CHECK_ID = @CHECK_ID           
--	WHERE CHECK_ID = @TEMP_CHECK_ID          
	

	INSERT INTO ACT_AGENCY_CHECK_DISTRIBUTION 
	(	
		CHECK_ID , AGENCY_ID , MONTH_NUMBER , MONTH_YEAR ,CHECK_AMOUNT ,AMT_UNCOLLECTED_PREMIUM_AB ,
		AMOUNT_TO_APPLY_ABP , AMT_COMMISSION_PAYABLE_AB , AMOUNT_TO_APPLY_ABC , AMT_COMMISSION_PAYABLE_DB ,
		AMOUNT_TO_APPLY_DBC , AMT_AGAINST_OP , AMOUNT_TO_APPLY_OP , AMT_AGENCY_BALANCE , DIFFERENCE_AMOUNT ,
		ACCOUNT_FOR_ADJUSTMENT , RECON_GROUP_ID , CREATED_BY, CREATED_DATETIME, MODIFIED_BY ,
		LAST_UPDATED_DATETIME ,DESCRIPTION
	)
	SELECT 
		@CHECK_ID , AGENCY_ID , MONTH_NUMBER , MONTH_YEAR ,CHECK_AMOUNT ,AMT_UNCOLLECTED_PREMIUM_AB ,
		AMOUNT_TO_APPLY_ABP , AMT_COMMISSION_PAYABLE_AB , AMOUNT_TO_APPLY_ABC , AMT_COMMISSION_PAYABLE_DB ,
		AMOUNT_TO_APPLY_DBC , AMT_AGAINST_OP , AMOUNT_TO_APPLY_OP , AMT_AGENCY_BALANCE , DIFFERENCE_AMOUNT ,
		ACCOUNT_FOR_ADJUSTMENT , RECON_GROUP_ID , CREATED_BY, CREATED_DATETIME, MODIFIED_BY ,
		LAST_UPDATED_DATETIME ,DESCRIPTION
	FROM ACT_AGENCY_CHECK_DISTRIBUTION_TEMP  
	WHERE CHECK_ID = @TEMP_CHECK_ID          

	IF(@COMM_TYPE = 'CAC') 
	BEGIN 
		UPDATE ACT_AGENCY_STATEMENT SET IS_CHECK_CREATED = 'Y'          
		WHERE MONTH_NUMBER = @MONTH           
		AND MONTH_YEAR =  @YEAR          
		AND COMM_TYPE =  @COMM_TYPE           
		AND AGENCY_ID = @AGENCY_ID   
	END
	ELSE IF(@COMM_TYPE = 'OP')          
	BEGIN           
		UPDATE ACT_AGENCY_STATEMENT SET IS_CHECK_CREATED = 'Y'          
		WHERE MONTH_NUMBER = @MONTH           
		AND MONTH_YEAR  =  @YEAR          
		AND ITEM_STATUS =  @COMM_TYPE           
		AND AGENCY_ID  = @PAYEE_ENTITY_ID       
	END          
	ELSE          
	BEGIN		
		UPDATE ACT_AGENCY_STATEMENT SET IS_CHECK_CREATED = 'Y'          
		WHERE MONTH_NUMBER = @MONTH           
		AND MONTH_YEAR =  @YEAR          
		AND COMM_TYPE =  @COMM_TYPE           
		AND AGENCY_ID = @PAYEE_ENTITY_ID   
	END          
END          
END                        
 
--GO
--EXEC Proc_InsertACT_CHECK_INFORMATION 1,9935,'',3,'','11/12/2009','55.55','',0,'','','','','','','','','',0,0,0,'','11/12/2009',0,'','0.0',234,1,1,'','','','','','',0,0,0,'',null,340,'11/12/2009',340,'11/12/2009','N',67064,'67064,67065,67066,67067'
--SELECT * FROM ACT_CUSTOMER_OPEN_ITEMS WHERE CUSTOMER_ID = 234 AND POLICY_ID = 1 AND IDEN_ROW_ID IN (67064,67065,67066,67067,67068,67069,67070,67071,91081)
--SELECT * FROM ACT_CHECK_OPEN_ITEMS     
--ROLLBACK TRAN                       
                
--SELECT * FROM ACT_CHECK_OPEN_ITEMS        
























GO

