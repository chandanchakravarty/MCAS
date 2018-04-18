IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAutoMotorBoatCoveragesForClaimsReserve]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAutoMotorBoatCoveragesForClaimsReserve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc dbo.Proc_GetAutoMotorBoatCoveragesForClaimsReserve
--go
/*----------------------------------------------------------                                                                   
Proc Name   : dbo.drop proc Proc_GetAutoMotorBoatCoveragesForClaimsReserve                                                                    
Created by  : Sumit Chhabra                                                                  
Date: 29 May,2006                                                                  
Purpose : Get the coverages for Claims Reserves                                                                    
Revison History  :                                                                     
------------------------------------------------------------                                                                  
Modified by  : Asfa Praveen              
Date  : 21-Sept-2007              
Purpose  : Modification in Deductible value fetching at Claim-reserve page without text-value              
------------------------------------------------------------                
Date Review By  Comments                                                                   
------   ------------   -------------------------*/                                         
--drop proc dbo.Proc_GetAutoMotorBoatCoveragesForClaimsReserve 1096                                                               
CREATE PROC [dbo].[Proc_GetAutoMotorBoatCoveragesForClaimsReserve]               
(                                                                  
 @CLAIM_ID INT                                                                   
)                                                                   
AS                                                                  
BEGIN                                                                  
DECLARE @CUSTOMER_ID int                                                                     
DECLARE @POLICY_ID int                                                                    
DECLARE @POLICY_VERSION_ID int                                                                     
DECLARE @LOB_ID VARCHAR(5)                                                                  
DECLARE @COVERAGE_TABLE VARCHAR(50)                                                                  
DECLARE @TABLE_COLUMN1 VARCHAR(50)                                                                  
DECLARE @TABLE_COLUMN2 VARCHAR(50)                                                                  
DECLARE @VEHICLE_TABLE VARCHAR(50)                                                      
DECLARE @POLICY_COVERAGES VARCHAR(5)                                              
DECLARE @DASH_OPERATOR VARCHAR(5)                                                 
declare @AUTOMOBILE smallint                                      
declare @MOTORCYCLE smallint                                      
DECLARE @WATERCRART smallint                                      
declare @HOMEOWNER smallint                                      
declare @RENTAL smallint                                      
declare @GENERAL_LIABILITY smallint                                      
declare @UMBRELLA smallint                                    
DECLARE @TEMP varchar(3000)                                   
declare @VEHICLE_TEXT varchar(500)                       
DECLARE @STATE_ID smallint     
DECLARE @ACTUAL_RISK_ID_COLUMN VARCHAR(50)                                  
DECLARE @ACTUAL_RISK_TYPE_COLUMN VARCHAR(50)   
DECLARE @POL_TABLE VARCHAR(100)                                      
declare @ALL_RISK smallint                                   
                                      
set @AUTOMOBILE = 2                                      
set @MOTORCYCLE = 3                                      
set @WATERCRART = 4                                      
set @HOMEOWNER = 1                                      
set @RENTAL = 6                                      
set @GENERAL_LIABILITY = 7                                      
set @UMBRELLA = 5    
SET @ALL_RISK = 9                                 
                                                   
SET @POLICY_COVERAGES = 'PL'                                                    
SET @DASH_OPERATOR = '-'     
SET @VEHICLE_TEXT = 'Vehicle # : '                                          
                                           
SELECT  @CUSTOMER_ID=PCPL.CUSTOMER_ID,@POLICY_ID=PCPL.POLICY_ID,@POLICY_VERSION_ID=PCPL.POLICY_VERSION_ID,                           
		@LOB_ID=PCPL.POLICY_LOB,@STATE_ID=isnull(PCPL.STATE_ID,0)                                    
FROM    POL_CUSTOMER_POLICY_LIST PCPL                              
JOIN    CLM_CLAIM_INFO CCI                              
	ON  PCPL.CUSTOMER_ID = CCI.CUSTOMER_ID 
	AND PCPL.POLICY_ID = CCI.POLICY_ID 
	AND PCPL.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID                                                                     
WHERE   CCI.CLAIM_ID=@CLAIM_ID                                                


IF(@CUSTOMER_ID=0 OR @POLICY_ID=0 OR @POLICY_VERSION_ID=0)                                                                    
	RETURN -1                      
	
   
                                                       
                                       
---2 Automobile                                                                  
---3 Motorcycle  

IF @LOB_ID=@ALL_RISK                                     
BEGIN                         

	                
	SET @COVERAGE_TABLE='POL_PRODUCT_COVERAGES'                                                                     
	SET @TABLE_COLUMN1='POL_VEHICLE_ID'                                                                  
	SET @TABLE_COLUMN2='POL_VEHICLE_ID'                                                                  
	SET @VEHICLE_TABLE='POL_INSURED_PRODUCT'
	--SET @POL_TABLE = 'POL_VEHICLES'
	SET @ACTUAL_RISK_ID_COLUMN ='AVC.VEHICLE_ID' 
  
	--IF(@LOB_ID=@AUTOMOBILE)    
	--	SET @ACTUAL_RISK_TYPE_COLUMN = 'PV.APP_VEHICLE_PERTYPE_ID'
	--ELSE IF(@LOB_ID=@MOTORCYCLE)   
	--	SET @ACTUAL_RISK_TYPE_COLUMN = 'PV.MOTORCYCLE_TYPE'                              
	
	--SET @VEHICLE_TEXT = '(CAST(CIV.VIN AS VARCHAR(50))+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                               
 --               '+CAST(CIV.VEHICLE_YEAR AS VARCHAR(20))+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                               
 --               '+CAST(CIV.MAKE AS VARCHAR(50))+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                               
 --               '+CAST(CIV.MODEL AS VARCHAR(50))) AS VEHICLE, '                                               

	--SET @TEMP = 
	SELECT DISTINCT  ''  AS PRIMARY_EXCESS,'' AS ATTACHMENT_POINT,'' AS OUTSTANDING,'' AS RI_RESERVE,'' AS RESERVE_ID,                                                                  
					 '' AS REINSURANCE_CARRIER,'' AS MCCA_ATTACHMENT_POINT,'' AS MCCA_APPLIES, C.RANK,                                                                     
					 C.COV_ID,C.COV_CODE,COV_DES AS COV_DESC,  + CAST(@STATE_ID AS VARCHAR) AS STATE_ID,  COVERAGE_TYPE, PV.POL_VEHICLE_ID AS ACTUAL_RISK_ID,                                                                     
					 CASE C.LIMIT_TYPE                                                            
						WHEN 2 THEN SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_1),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT2_AMOUNT_TEXT,'')) + '/' +      
									SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_2),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),              
									CONVERT(MONEY,AVC.LIMIT_2),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT2_AMOUNT_TEXT,''))                                         
						ELSE 
									ISNULL(SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_1),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_1),1),0)),'')+     
									CONVERT(VARCHAR(60),ISNULL(AVC.LIMIT1_AMOUNT_TEXT,''))                                                                     
						END AS LIMIT,                     
					CASE C.DEDUCTIBLE_TYPE                                                                   
						WHEN 2 THEN SUBSTRING(CONVERT(VARCHAR(30), CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0,CHARINDEX('.',             
									CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0))+ ' ' + '/' + SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),               
									CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),0))                                                                 
						ELSE		SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0)) + ' '                                      
						END AS DEDUCTIBLE,   
					CASE  @LOB_ID 
						WHEN 2 THEN 'PP'
						WHEN 3 THEN 'CYCL'
						END AS ACTUAL_RISK_TYPE   	
       FROM   POL_PRODUCT_COVERAGES   AVC                                                                 
              INNER JOIN MNT_COVERAGE C ON AVC.COVERAGE_CODE_ID = C.COV_ID  
		      INNER JOIN CLM_CLAIM_INFO CCI ON CCI.CUSTOMER_ID = AVC.CUSTOMER_ID AND CCI.POLICY_ID = AVC.POLICY_ID AND CCI.POLICY_VERSION_ID= AVC.POLICY_VERSION_ID
		      INNER JOIN POL_INSURED_PRODUCT PV ON   CCI.CLAIM_ID = PV.CLAIM_ID                                                              
	   WHERE                                                                   
			  CCI.CUSTOMER_ID = CAST(@CUSTOMER_ID AS VARCHAR)   AND                              
			  CCI.POLICY_ID = CAST(@POLICY_ID AS VARCHAR)   AND                                                                     
			  CCI.POLICY_VERSION_ID =  CAST(@POLICY_VERSION_ID AS VARCHAR)
			  AND PV.POL_VEHICLE_ID  IN(SELECT TOP 1  POL_VEHICLE_ID  
										FROM   POL_INSURED_PRODUCT   
										WHERE CLAIM_ID= CAST(@CLAIM_ID AS VARCHAR)  AND IS_ACTIVE='Y' ORDER BY POL_VEHICLE_ID   )                                               
		      AND C.COVERAGE_TYPE= CAST(@POLICY_COVERAGES AS VARCHAR) 
	   ORDER BY C.RANK                    
              
END 
                                                                
ELSE IF @LOB_ID=@AUTOMOBILE OR @LOB_ID=@MOTORCYCLE                                     
BEGIN                                         
	
  
	IF(@LOB_ID=@AUTOMOBILE)    
		SET @ACTUAL_RISK_TYPE_COLUMN = 'PV.APP_VEHICLE_PERTYPE_ID'
	ELSE IF(@LOB_ID=@MOTORCYCLE)   
		SET @ACTUAL_RISK_TYPE_COLUMN = 'PV.MOTORCYCLE_TYPE'                              
	
	SET @VEHICLE_TEXT = '(CAST(CIV.VIN AS VARCHAR(50))+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                               
                '+CAST(CIV.VEHICLE_YEAR AS VARCHAR(20))+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                               
                '+CAST(CIV.MAKE AS VARCHAR(50))+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                               
                '+CAST(CIV.MODEL AS VARCHAR(50))) AS VEHICLE, '                                               

	SET @TEMP = 'SELECT DISTINCT ' +                                                                   
       --DUMMY COLUMNS ONLY FOR GRID-BINDING                                                                  
       ' ''''  AS PRIMARY_EXCESS,'''' AS ATTACHMENT_POINT,'''' AS OUTSTANDING,'''' AS RI_RESERVE,'''' AS RESERVE_ID,' +                                                                   
       ' '''' AS REINSURANCE_CARRIER,'''' AS MCCA_ATTACHMENT_POINT,'''' AS MCCA_APPLIES, C.RANK, ' +                     
       --  ' AVC.' + CAST(@TABLE_COLUMN2 AS VARCHAR) + ' AS VEHICLE_ID,C.COV_ID,C.COV_CODE,COV_DES AS COV_DESC, ' +                                                                      
       ' C.COV_ID,C.COV_CODE,COV_DES AS COV_DESC, ' + CAST(@STATE_ID AS VARCHAR) + ' AS STATE_ID, ' +                                                                      
       ' CASE C.LIMIT_TYPE ' +                                                            
       ' WHEN 2 THEN ' +            
       ' SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_1),1),0, ' +                                                                
       ' CHARINDEX(''.'',CONVERT(VARCHAR(30),' +                                                                
       ' CONVERT(MONEY,AVC.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT2_AMOUNT_TEXT,'''')) + ''/'' +       
		SUBSTRING(CONVERT(VARCHAR(30),' + ' CONVERT(MONEY,AVC.LIMIT_2),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30),' +               
		' CONVERT(MONEY,AVC.LIMIT_2),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT2_AMOUNT_TEXT,'''')) ' +                         --ISNULL CONDITION ADDED FOR ITRACK 5720 on 27 April 2009                                          
		' ELSE ISNULL(SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_1),1),0,CHARINDEX(''.'', ' +                                                                   
		' CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_1),1),0)),'''') +       
		CONVERT(VARCHAR(60),ISNULL(AVC.LIMIT1_AMOUNT_TEXT,'''')) ' +                                                                     
		' END AS LIMIT, ' +                      
		' CASE C.DEDUCTIBLE_TYPE ' +                                                                   
		' WHEN 2 THEN SUBSTRING(CONVERT(VARCHAR(30),' +                   
		' CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0,CHARINDEX(''.'', ' +             
                                                      
		-- Commented by Asfa (21-Sept-2007)              
		/* ' CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0,CHARINDEX(''.'', ' +                                                                     
			   ' CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0))+ '' '' + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) + ''/'' + SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30), '



		  
		 +                 
			   ' CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),0))  + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,'''')) ' +                                                                     
			   ' ELSE SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0))                
		 + '' '' + CONVERT(VARCHAR(20),ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) ' +                                        
		*/                     

       ' CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0))+ '' '' + ''/'' + SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30), ' +               
       ' CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),0))   ' +                                                                   
       ' ELSE SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0)) + '' '' ' +                                      
       ' END AS DEDUCTIBLE, '  +                                                                                
	   ' COVERAGE_TYPE,' +
	   'PV.VEHICLE_ID AS ACTUAL_RISK_ID,' + 
		'CASE ' + @LOB_ID + 
		'WHEN 2 THEN ''PP''
			WHEN 3 THEN ''CYCL''
		END AS ACTUAL_RISK_TYPE ' +  	

       ' FROM ' + @COVERAGE_TABLE +  '  AVC ' +                                                                 
       ' INNER JOIN ' +                                 
       ' MNT_COVERAGE C ' +                                                                   
	   'ON  ' +                                    
		' AVC.COVERAGE_CODE_ID = C.COV_ID ' +  
		'INNER JOIN POL_LOCATIONS PV
		ON   PV.CUSTOMER_ID = AVC.CUSTOMER_ID AND
		PV.POLICY_ID = AVC.POLICY_ID AND
		PV.POLICY_VERSION_ID= AVC.POLICY_VERSION_ID
		AND PV.VEHICLE_ID  = AVC.VEHICLE_ID  
		' +                                                                 
		' WHERE ' +                                                                   
		' PV.CUSTOMER_ID = ' + CAST(@CUSTOMER_ID AS VARCHAR)  + ' AND ' +                               
		' PV.POLICY_ID = ' + CAST(@POLICY_ID AS VARCHAR)  + ' AND ' +                                                                      
		' PV.POLICY_VERSION_ID = ' + CAST(@POLICY_VERSION_ID AS VARCHAR)   + ' AND PV.' +   @TABLE_COLUMN2 + ' IN  ' +        
		'(SELECT TOP 1 ' +  @TABLE_COLUMN1 + ' FROM  ' + @VEHICLE_TABLE +                                                                      
		' WHERE ' +                          
		' CLAIM_ID=' + CAST(@CLAIM_ID AS VARCHAR) + ' AND IS_ACTIVE=''Y'' ORDER BY ' + @TABLE_COLUMN1  + ' )' +                                                 
		' AND C.COVERAGE_TYPE= ' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + '''' + ' ORDER BY C.RANK '                    
                              
END                                                                  
ELSE IF @LOB_ID=@WATERCRART ---4 Watercraft                                                                  
BEGIN                                                                  

	SET @COVERAGE_TABLE='POL_WATERCRAFT_COVERAGE_INFO'                               
	SET @TABLE_COLUMN1='POLICY_BOAT_ID'                                                                  
	SET @TABLE_COLUMN2='BOAT_ID'                                                                  
	SET @VEHICLE_TABLE='CLM_INSURED_BOAT'
	SET @POL_TABLE = 'POL_WATERCRAFT_INFO' 
	SET @ACTUAL_RISK_ID_COLUMN = 'AVC.BOAT_ID'
	SET @ACTUAL_RISK_TYPE_COLUMN = 'AVC.BOAT_ID'                                      
	SET @VEHICLE_TEXT = '(CAST(CIV.YEAR AS VARCHAR)+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                               
            '+CAST(CIV.MAKE AS VARCHAR)+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                               
                '+CAST(CIV.MODEL AS VARCHAR)+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                               
                '+CAST(CIV.SERIAL_NUMBER AS VARCHAR)) AS VEHICLE, '  
	                                 
	 --Since we do not have any policy level coverages for watercraft, lets display a blank table                              
	 SET @TEMP = 'SELECT NULL '                              
END                                                
ELSE
BEGIN
	RETURN -2                                       
END
                                                                
---5 Umbrella                                                                  
---6 Rental                                                                  
---7 General Liability                                        
                                                                  
--Temporary variable that will hold the select query                                
            
--print @TEMP                                   
EXEC (@TEMP)    
                                         
SET @TEMP = 'SELECT' +                                                                   
      --DUMMY COLUMNS ONLY FOR GRID-BINDING                                                                  
      ' ''''  AS PRIMARY_EXCESS,'''' AS ATTACHMENT_POINT,'''' AS OUTSTANDING,'''' AS RI_RESERVE,'''' AS RESERVE_ID,' +                                                                   
      ' '''' AS REINSURANCE_CARRIER,'''' AS MCCA_ATTACHMENT_POINT,'''' AS MCCA_APPLIES, C.RANK, ' +                                                                     
      ' AVC.' + CAST(@TABLE_COLUMN2 AS VARCHAR) + ' AS VEHICLE_ID,C.COV_ID,C.COV_CODE,COV_DES AS COV_DESC, ' +  CAST(@STATE_ID AS VARCHAR) + ' AS STATE_ID, ' +                                               
      CAST(@VEHICLE_TEXT AS VARCHAR(500)) +                                   
      ' CASE C.LIMIT_TYPE ' +                                                                   
      ' WHEN 2 THEN ' +                                                                   
' SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_1),1),0, ' +                                                                   
      ' CHARINDEX(''.'',CONVERT(VARCHAR(30),' +                                                                   
      ' CONVERT(MONEY,AVC.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT2_AMOUNT_TEXT,'''')) + ''/'' + SUBSTRING(CONVERT(VARCHAR(30),' +                                                                   
      ' CONVERT(MONEY,AVC.LIMIT_2),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30),' +                                                                   
      ' CONVERT(MONEY,AVC.LIMIT_2),1),0)) + CONVERT(VARCHAR(20),ISNULL(AVC.LIMIT2_AMOUNT_TEXT,'''')) ' +                                                                   
      ' ELSE ISNULL(SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_1),1),0,CHARINDEX(''.'', ' +                                                                   
      ' CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_1),1),0)),'''') +       
	  CONVERT(VARCHAR(60),ISNULL(AVC.LIMIT1_AMOUNT_TEXT,'''')) ' + ' END AS LIMIT, ' +                  
      ' CASE C.DEDUCTIBLE_TYPE ' +                                                                   
      ' WHEN 2 THEN SUBSTRING(CONVERT(VARCHAR(30),' +                                                                   
      ' CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0,CHARINDEX(''.'', ' +                
      ' CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0))+ '' '' + ''/'' + SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30), ' +               
      ' CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),0))   ' +                                                                   
      ' ELSE SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),0)) + '' '' ' +                                       
      ' END AS DEDUCTIBLE, '  +                                                                                
      ' COVERAGE_TYPE, ' + @ACTUAL_RISK_ID_COLUMN +' AS ACTUAL_RISK_ID,' +  
	  'CASE ' + @LOB_ID + 
	 ' WHEN 2 THEN ''PP'' 
		 WHEN 3 THEN ''CYCL''
		 WHEN 6 THEN ''REDW''
		 WHEN 4 THEN ''B''
		 END AS ACTUAL_RISK_TYPE ' +                                                 
      ' FROM ' + @COVERAGE_TABLE +  '  AVC ' +
	  ' INNER JOIN ' + @POL_TABLE + ' PV 
	  ON 
	  PV.CUSTOMER_ID = AVC.CUSTOMER_ID AND
	  PV.POLICY_ID = AVC.POLICY_ID AND
	  PV.POLICY_VERSION_ID= AVC.POLICY_VERSION_ID' +        
	  '  AND PV.'  + @TABLE_COLUMN2  +  '  =  AVC.'  + @TABLE_COLUMN2 + 
      ' INNER JOIN ' +                                                                   
      ' MNT_COVERAGE C ' +                                                                   
      ' ON  ' +                                                              
      ' AVC.COVERAGE_CODE_ID = C.COV_ID ' +                                            
      ' LEFT OUTER JOIN ' + CAST(@VEHICLE_TABLE AS VARCHAR) +                                     
      ' CIV ON AVC.' + CAST(@TABLE_COLUMN2 AS VARCHAR) + ' = CIV.' + CAST(@TABLE_COLUMN1 AS VARCHAR) +
    ' WHERE ' +                               
    ' PV.CUSTOMER_ID = ' + CAST(@CUSTOMER_ID AS VARCHAR)  +                                          
      ' AND PV.POLICY_ID = ' + CAST(@POLICY_ID AS VARCHAR)  +                                           
      ' AND PV.POLICY_VERSION_ID = ' + CAST(@POLICY_VERSION_ID AS VARCHAR)   +                                           
      ' AND CIV.CLAIM_ID=' + CAST(@CLAIM_ID AS VARCHAR) +                                                        
--      ' AND C.COVERAGE_TYPE<>' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + '''' + ' ORDER BY C.RANK '                                                                   
      '  AND CIV.IS_ACTIVE=''Y'' AND C.COVERAGE_TYPE<>' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + '''' + ' ORDER BY AVC.' + CAST(@TABLE_COLUMN2 AS VARCHAR) + ',C.RANK'                
          
                        
--print @TEMP                    
EXEC (@TEMP)                    
 
--exec Proc_GetExistingWaterEquipCovgForClaims @CLAIM_ID

END

--go
--exec Proc_GetAutoMotorBoatCoveragesForClaimsReserve 1
--rollback tran


GO

