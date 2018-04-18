IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLossRatio]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLossRatio]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--DROP PROC dbo.Proc_GetLossRatio 
--go

/*----------------------------------------------------------            
Proc Name       : dbo.Proc_GetLossRatio          
Created by      : Sukhveer Singh          
Date            : 18-08-2006          
Purpose         : Get the data for Report named Loss Ratio.          
Revison History :            
Used In        : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/  
--Shikha (1/10/2009). Where clause conditions added against itrack #6393.
--DROP PROC dbo.Proc_GetLossRatio  
        
CREATE PROC [dbo].[Proc_GetLossRatio]            
@DATE_FROM datetime = '',  -- Valid format : mm/dd/yyyy        
@DATE_TO datetime = '',    -- Valid format : mm/dd/yyyy        
@M_CUSTOMERID varchar(8000)='0', -- INT          
@M_AGENCYID varchar(8000)='0',   -- INT      
@M_LOB varchar(8000) = '',   -- NVARCHAR       
@M_UNDERWRITER varchar(8000)='', -- INT        
@FIRST_SORT varchar(50) = '',          
@SECOND_SORT varchar(50) = '',          
@THIRD_SORT varchar(50) = ''          
 
AS           
DECLARE @sql VARCHAR(8000) 
DECLARE @WHERE VARCHAR(8000)         

SET @WHERE = ''
  
BEGIN
if @DATE_FROM='' or @DATE_FROM is null 
	set @DATE_FROM=''            
if @DATE_TO='' or @DATE_TO is null 
	set @DATE_TO=''
if @M_CUSTOMERID='' or @M_CUSTOMERID is null 
	set @M_CUSTOMERID='0'            
if @M_AGENCYID='' or @M_AGENCYID is null 
	set @M_AGENCYID='0' 
if @M_UNDERWRITER='' or @M_UNDERWRITER is null 
	set @M_UNDERWRITER='0'   
         
select @sql = 'SELECT sub1.agency_display_name, sub1.claimant_name, sub1.POLICY_NUMBER, sub1.claim_number, sub1.loss_date, sub1.outstanding_reserve,           
	sub1.premium_amount *' + 'DATEDIFF(DAY, '''+CONVERT(VARCHAR, @DATE_FROM,101) + '''' +',''' + CONVERT(VARCHAR, @DATE_TO,101) + '''' + ')/'          
	+ 'DATEDIFF(DAY,sub1.effective_date,sub1.expiry_date) AS Earned, sub1.totalpaid, sub1.recoveries, sub1.totalpaid - sub1.recoveries as TotalIncurred,          
	sub1.premium_amount *' + 'DATEDIFF(DAY, '''+CONVERT(VARCHAR, @DATE_FROM,101) + '''' +',''' + CONVERT(VARCHAR, @DATE_TO,101) + '''' + ')/'          
	+ 'DATEDIFF(DAY,sub1.effective_date,sub1.expiry_date) - sub1.totalpaid - sub1.recoveries as Ratio,            
	sub1.lookup_value_desc as Status from
	(SELECT Agnlst.AGENCY_DISPLAY_NAME, INFO.CLAIMANT_NAME, POLCUSTLST.POLICY_NUMBER, INFO.CLAIM_NUMBER , INFO.LOSS_DATE , INFO.OUTSTANDING_RESERVE,          
	Case when POLMAST.process_desc = ''' + 'Endorsement'' ' +'Then (SELECT effective_dateTIME FROM POL_POLICY_PROCESS WHERE CUSTOMER_ID = INFO.CUSTOMER_ID          
	AND POLICY_ID = INFO.POLICY_ID AND POLICY_VERSION_ID = INFO.POLICY_VERSION_ID AND ROW_ID =1)          
	ELSE (SELECT app_effective_date FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = INFO.CUSTOMER_ID AND POLICY_ID = INFO.POLICY_ID AND POLICY_VERSION_ID = INFO.POLICY_VERSION_ID)          
	END as ''' + 'EFFECTIVE_DATE'','          
	+' Case when POLMAST.process_desc = ''' + 'Endorsement'' ' +'Then (SELECT expiry_date FROM POL_POLICY_PROCESS WHERE CUSTOMER_ID = INFO.CUSTOMER_ID          
	AND POLICY_ID = INFO.POLICY_ID AND POLICY_VERSION_ID = INFO.POLICY_VERSION_ID AND ROW_ID =1)          
	ELSE (SELECT app_expiration_date FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = INFO.CUSTOMER_ID AND POLICY_ID = INFO.POLICY_ID AND POLICY_VERSION_ID = INFO.POLICY_VERSION_ID)          
	END as ''' + 'EXPIRY_DATE'' ,'     
	+ 'ACTDET.PREMIUM_AMOUNT, ' +'SUB1.TOTALPAID,INFO.RECOVERIES, LOOKUP.lookup_value_desc          
	FROM CLM_CLAIM_INFO INFO           
	LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST POLCUSTLST ON POLCUSTLST.CUSTOMER_ID = INFO.CUSTOMER_ID AND POLCUSTLST.POLICY_ID = INFO.POLICY_ID AND POLCUSTLST.POLICY_VERSION_ID = INFO.POLICY_VERSION_ID
	LEFT OUTER JOIN MNT_AGENCY_LIST AGNLST ON POLCUSTLST.AGENCY_ID = AGNLST.AGENCY_ID
	INNER JOIN MNT_LOOKUP_VALUES LOOKUP ON INFO.CLAIM_STATUS = LOOKUP.LOOKUP_UNIQUE_ID          
	LEFT OUTER JOIN ACT_PREMIUM_PROCESS_DETAILS ACTDET ON INFO.CUSTOMER_ID = ACTDET.CUSTOMER_ID AND  INFO.POLICY_ID = ACTDET.POLICY_ID AND INFO.POLICY_VERSION_ID = ACTDET.POLICY_VERSION_ID          
	LEFT OUTER JOIN (SELECT customer_id,policy_id,SUM(total_paid) AS TOTALPAID from ACT_CUSTOMER_OPEN_ITEMS  WHERE UPPER(ISNULL(ITEM_TRAN_CODE_TYPE,'''')) <> ''' + 'FEES'' '          
	+' GROUP BY customer_id,policy_id) SUB1 ON INFO.customer_id = SUB1.customer_id AND INFO.policy_id = SUB1.policy_id'          
	+' LEFT OUTER JOIN POL_POLICY_PROCESS POLPRO ON INFO.CUSTOMER_ID = POLPRO.CUSTOMER_ID AND INFO.POLICY_ID = POLPRO.POLICY_ID AND INFO.POLICY_VERSION_ID = POLPRO.POLICY_VERSION_ID'          
	+' LEFT OUTER JOIN POL_PROCESS_MASTER POLMAST ON POLPRO.PROCESS_ID = POLMAST.PROCESS_ID'          
	
	         
IF (@DATE_FROM <> '')
	BEGIN
		SET @WHERE = @WHERE + 'CONVERT(DATETIME,CONVERT(VARCHAR,INFO.CREATED_DATETIME,101),101) >= ''' + CONVERT(VARCHAR, @DATE_FROM,101) + '''' 
        --CONVERT(DATETIME,CONVERT(VARCHAR,CPL.POL_VER_EFFECTIVE_DATE,101),101)
	END
IF(ISNULL(@WHERE,'') = '')
	BEGIN
		IF (@DATE_TO <> '')    
		BEGIN
			SET @WHERE = @WHERE + 'CONVERT(DATETIME,CONVERT(VARCHAR,INFO.CREATED_DATETIME,101),101) <= ''' + CONVERT(VARCHAR, @DATE_TO,101) + ''''	
		END
	END
ELSE
	BEGIN
		IF (@DATE_TO <> '')    
		BEGIN
			SET @WHERE = @WHERE + ' AND ' + 'CONVERT(DATETIME,CONVERT(VARCHAR,INFO.CREATED_DATETIME,101),101) <= ''' + CONVERT(VARCHAR, @DATE_TO,101) + ''''	
		END
	END
--CUSTOMER_ID
IF(ISNULL(@WHERE,'') = '')
	BEGIN  
		IF (@M_CUSTOMERID <> '0')          
		BEGIN          
			SELECT @WHERE = @WHERE + 'INFO.CUSTOMER_ID IN (' + @M_CUSTOMERID + ')'          
		END
	END
ELSE
	BEGIN
		IF (@M_CUSTOMERID <> '0')          
		BEGIN          
			SELECT @WHERE = @WHERE + ' AND ' + 'INFO.CUSTOMER_ID IN (' + @M_CUSTOMERID + ')'          
		END
	END
--AGENCY_ID         
IF(ISNULL(@WHERE,'') = '')
	BEGIN          
		IF (@M_AGENCYID <> '0')          
		BEGIN          
			SELECT @WHERE = @WHERE + 'AGNLST.AGENCY_ID IN (' + @M_AGENCYID + ')'          
		END
	END
ELSE
	BEGIN
		IF (@M_AGENCYID <> '0')          
		BEGIN          
			SELECT @WHERE = @WHERE + ' AND ' + 'AGNLST.AGENCY_ID IN (' + @M_AGENCYID + ')'          
		END
	END
--LOB_ID
IF(ISNULL(@WHERE,'') = '')
	BEGIN
		IF (ISNULL(@M_LOB,'0') <> '0' AND @M_LOB<>'' )
		BEGIN
			SELECT @M_LOB = REPLACE(@M_LOB,',',''',''')  
			SELECT @WHERE = @WHERE + 'INFO.LOB_ID IN (''' + @M_LOB + ''')'      
		END
	END
ELSE
	BEGIN
		IF (ISNULL(@M_LOB,'0') <> '0' AND @M_LOB<>'' )      
		BEGIN      
			SELECT @M_LOB = REPLACE(@M_LOB,',',''',''')  
			SELECT @WHERE = @WHERE + ' AND ' + 'INFO.LOB_ID IN (''' + @M_LOB + ''')'      
		END    
	END    
--UNDERWRITER
IF(ISNULL(@WHERE,'') = '')
	BEGIN        
		IF (@M_UNDERWRITER <> '0')          
		BEGIN          
			SELECT @WHERE = @WHERE + 'POLCUSTLST.UNDERWRITER IN (' + @M_UNDERWRITER + ')'         
		END          
	END
ELSE
	BEGIN
		IF (@M_UNDERWRITER <> '0')          
		BEGIN          
			SELECT @WHERE = @WHERE + ' AND ' + 'POLCUSTLST.UNDERWRITER IN (' + @M_UNDERWRITER + ')'         
		END
	END          

IF (@WHERE <> '')          
	BEGIN
		SELECT @sql = @sql + ' WHERE ' + @WHERE + ')sub1'  
	END
ELSE
	BEGIN
		SELECT @sql = @sql + ')sub1'
	END

--Condition added FOR Itrack Issue #5662  
IF (@FIRST_SORT =  @SECOND_SORT and @SECOND_SORT  = @THIRD_SORT)          
	  BEGIN     
		IF (@FIRST_SORT= '' AND @SECOND_SORT='' AND @THIRD_SORT='')    
		BEGIN			
			SELECT @sql =@sql + ' ORDER BY agency_display_name' 	
		END  
		ELSE  
	   SELECT @sql =@sql + ' ORDER BY ' +  @FIRST_SORT           
	  END   
ELSE
      
IF (@FIRST_SORT= '' AND @SECOND_SORT='' AND @THIRD_SORT='')    
BEGIN	
    SELECT @sql =@sql + ' ORDER BY agency_display_name' 	
END
ELSE
BEGIN
	IF (@FIRST_SORT<>'')          
	  BEGIN          
	   SELECT @sql =@sql + ' ORDER BY ' + @FIRST_SORT           
	  END 

	IF (@SECOND_SORT<>'' AND @FIRST_SORT <>'')          
	  BEGIN          
	   SELECT @sql =@sql + ', ' + @SECOND_SORT          
	  END          
	ELSE IF (@SECOND_SORT<>'' AND @FIRST_SORT = '')          
		BEGIN          
	   SELECT @sql =@sql + ' ORDER BY ' + @SECOND_SORT          
		END           
	          
	IF (@THIRD_SORT <> '' AND (@FIRST_SORT <>'' OR @SECOND_SORT <>''))          
	  BEGIN          
	   SELECT @sql =@sql + ', ' + @THIRD_SORT          
	  END          
	
ELSE IF (@THIRD_SORT<>'' AND (@FIRST_SORT ='' AND @SECOND_SORT =''))          
	  BEGIN          
	   SELECT @sql =@sql + ' ORDER BY ' + @THIRD_SORT           
	  END            
END

--PRINT(@sql)         
EXEC(@sql)
        
END          
          
--Proc_GetLossRatio '02/02/1996','10/02/2006','0','0','0','0',NULL,NULL,NULL    

--go
--exec Proc_GetLossRatio '','','0','0','0','0','agency_display_name','agency_display_name','agency_display_name'    
--exec Proc_GetLossRatio '9/22/2009','9/30/2009','1680,1769,1772','88,101,146','','0','','','LOSS_DATE'
--exec Proc_GetLossRatio '','','','','','0','agency_display_name','POLICY_NUMBER','CLAIM_NUMBER'    
--rollback tran



GO

