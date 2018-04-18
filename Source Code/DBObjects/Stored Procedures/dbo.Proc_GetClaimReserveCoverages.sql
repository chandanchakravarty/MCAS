IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimReserveCoverages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimReserveCoverages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                                                            
Proc Name       : dbo.Proc_GetClaimReserveCoverages          
Created by      : Sumit Chhabra                                                                                                          
Date            : 30/06/2006                                                                                                            
Purpose         : Fetch data from CLM_ACTIVITY_RESERVE table for claim reserve screen                                                                                        
Created by      : Sumit Chhabra                                                                                                           
Revison History :                                                                                                            
Used In        : Wolverine                                                                                                            
------------------------------------------------------------                                                                                                            
Date     Review By          Comments                                                                                                            
------   ------------       -------------------------*/                                                                                                            
CREATE PROC dbo.Proc_GetClaimReserveCoverages                                                                                                  
@CLAIM_ID int                                        
AS                                                                                                            
BEGIN               
          
/*                                    
1 HOME Homeowners                                     
2 AUTOP Automobile                                     
3 CYCL Motorcycle                                     
4 BOAT Watercraft                                     
5 UMB Umbrella                                     
6 REDW Rental                                     
7 GENL General Liability                                     
*/                                    
                                                          
DECLARE @CUSTOMER_ID INT                                    
DECLARE @POLICY_ID INT                                    
DECLARE @POLICY_VERSION_ID SMALLINT                                    
DECLARE @LOB_ID INT                                          
DECLARE @TEMPTABLENAME VARCHAR(100)                                    
DECLARE @TEMPCOLUMNNAME VARCHAR(100)                                    
DECLARE @TEMPSTR VARCHAR(8000)                        
declare @POLICY_COVERAGES varchar(10)                    
DECLARE @DASH_OPERATOR VARCHAR(5)                           
DECLARE @VEHICLE_TEXT VARCHAR(15)             
    
DECLARE @TABLE_COLUMN1 VARCHAR(25)             
DECLARE @VEHICLE_TABLE VARCHAR(25)             
                                
SET @POLICY_COVERAGES = 'PL'                                
SET @DASH_OPERATOR = '-'                    
SET @VEHICLE_TEXT = 'Vehicle # : '                          
                  
 SELECT                                     
  @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID                                     
 FROM                                     
  CLM_CLAIM_INFO                                     
 WHERE                                    
  CLAIM_ID=@CLAIM_ID                                    
                                    
 SELECT                                    
  @LOB_ID=POLICY_LOB                                     
 FROM                                     
  POL_CUSTOMER_POLICY_LIST                                     
 WHERE                                     
  CUSTOMER_ID=@CUSTOMER_ID AND                           
  POLICY_ID=@POLICY_ID AND                                    
  POLICY_VERSION_ID=@POLICY_VERSION_ID                                    
                                    
IF(@LOB_ID IS NULL OR @LOB_ID=0)                                    
 RETURN                             
            
                                    
                                    
                                    
IF(@LOB_ID=1 OR @LOB_ID=6)                                    
BEGIN                                  
 SET @TEMPTABLENAME = 'POL_DWELLING_SECTION_COVERAGES'                            
 SET @TEMPCOLUMNNAME = 'PVC.DWELLING_ID '                                  
END                                  
ELSE IF(@LOB_ID=2 OR @LOB_ID=3)                                    
BEGIN                                  
  SET @TEMPTABLENAME = 'POL_VEHICLE_COVERAGES'                                    
 SET @TEMPCOLUMNNAME = 'PVC.VEHICLE_ID '    
  SET @TABLE_COLUMN1='POLICY_VEHICLE_ID'                                
  SET @VEHICLE_TABLE='CLM_INSURED_VEHICLE'                                                     
END                                  
ELSE IF(@LOB_ID=4)                                    
BEGIN                                  
  SET @TEMPTABLENAME = 'POL_WATERCRAFT_COVERAGE_INFO'                                    
  SET @TEMPCOLUMNNAME = 'PVC.BOAT_ID '                                  
END                                  
ELSE                                     
 RETURN          

                
                               
                        
--Policy level coverages                         
 set @TEMPSTR='SELECT DISTINCT CAR.PRIMARY_EXCESS,CAR.ATTACHMENT_POINT,CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.RESERVE_ID, '+                                  
      ' CAR.REINSURANCE_CARRIER,CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES, ' + --@TEMPCOLUMNNAME + ' AS VEHICLE_ID,' +                                   
      '  MC.COV_ID, MC.COV_CODE, COV_DES as COV_DESC ,'+                                  
       ' CASE MC.LIMIT_TYPE ' +                                   
       ' WHEN 2 THEN ' +                                   
        ' substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0, + charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) + ' +                                   
      ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,''''))            + ''/'' + ' +                                                       
       ' substring(convert(varchar(30),convert(money,PVC.LIMIT_2),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) +  ' +                                   
      ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'''')) ' +                                   
   ' ELSE substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT1_AMOUNT_TEXT,'''')) ' +                                   
       ' END AS LIMIT ,' +                                                         
       ' CASE MC.DEDUCTIBLE_TYPE ' +                                   
       ' WHEN 2 THEN Substring(convert(varchar(30),convert(money,PVC.Deductible_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_1),1),0)) + ''' + ' ' + '''+' +                               
      ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) + ''/'' + ' +                                   
      ' substring(convert(varchar(30),convert(money,PVC.Deductible_2),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_2),1),0))  + ' +                                   
      ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE2_AMOUNT_TEXT,'''')) ' +                                                        
       ' ELSE substring(convert(varchar(30),convert(money,PVC.Deductible_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_1),1),0)) + ''' + ' ' + '''+' +                     
      ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) ' +                                   
       ' END AS DEDUCTIBLE, '  +                                    
       ' MC.COVERAGE_TYPE '  +                                    
       ' FROM CLM_CLAIM_INFO CCI JOIN CLM_ACTIVITY_RESERVE CAR  ON CCI.CLAIM_ID = CAR.CLAIM_ID ' +                                   
       ' JOIN ' + @TEMPTABLENAME + ' PVC ON CCI.POLICY_ID = PVC.POLICY_ID AND ' +                                                  
       ' CCI.POLICY_VERSION_ID = PVC.POLICY_VERSION_ID AND ' +                                   
   ' CCI.CUSTOMER_ID = PVC.CUSTOMER_ID AND CAR.COVERAGE_ID=PVC.COVERAGE_CODE_ID ' +          
       ' JOIN MNT_COVERAGE MC ON MC.COV_ID = CAR.COVERAGE_ID WHERE ' +                                                      
 ' CCI.CLAIM_ID=' + CAST(@CLAIM_ID AS VARCHAR(50)) + ' AND MC.COVERAGE_TYPE=' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + ''''   +     
    
 ' UNION ' +     
                 
  'SELECT DISTINCT ' +                                 
 ' 0  AS PRIMARY_EXCESS,0 AS ATTACHMENT_POINT,0 AS OUTSTANDING,0 AS RI_RESERVE,0 AS RESERVE_ID,' +                                 
 ' 0 AS REINSURANCE_CARRIER,0 AS MCCA_ATTACHMENT_POINT,0 AS MCCA_APPLIES,' +                            
  ' C.COV_ID,C.COV_CODE,COV_DES as COV_DESC, ' +                                    
  ' CASE C.LIMIT_TYPE ' +                                 
 ' WHEN 2 THEN ' +                                 
 ' substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0, ' +                                 
 ' charindex(''.'',convert(varchar(30),' +                                 
 ' convert(money,PVC.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'''')) + ''/'' + substring(convert(varchar(30),' +                  ' convert(money,PVC.LIMIT_2),1),0,charindex(''.'',convert(varchar(30),' +                        
   
        
 ' convert(money,PVC.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'''')) ' +                                 
  ' ELSE substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0,charindex(''.'', ' +                                 
 ' convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT1_AMOUNT_TEXT,'''')) ' +                                   
  ' END AS LIMIT, ' +                                 
  ' CASE C.DEDUCTIBLE_TYPE ' +                                 
 ' WHEN 2 THEN Substring(convert(varchar(30),' +                                 
 ' convert(money,PVC.Deductible_1),1),0,charindex(''.'', ' +                                 
 ' convert(varchar(30),convert(money,PVC.Deductible_1),1),0))+ '' '' + CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) + ''/'' + substring(convert(varchar(30),convert(money,PVC.Deductible_2),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_2),1),0))  + CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE2_AMOUNT_TEXT,'''')) ' +                                 
  ' ELSE substring(convert(varchar(30),convert(money,PVC.Deductible_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_1),1),0)) + '' '' + CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) ' +                             
  
    
  ' END AS DEDUCTIBLE, '  +                                              
  ' COVERAGE_TYPE ' +                     
  ' FROM ' + CAST(@TEMPTABLENAME AS VARCHAR) +  '  PVC ' +                                 
 ' INNER JOIN ' +                                 
  ' MNT_COVERAGE C ' +                                 
  ' ON  ' +                                 
  ' PVC.COVERAGE_CODE_ID = C.COV_ID ' +                                 
  ' WHERE ' +                                 
  ' CUSTOMER_ID = ' + CAST(@CUSTOMER_ID AS VARCHAR)  + ' AND ' +                                    
  ' POLICY_ID = ' + CAST(@POLICY_ID AS VARCHAR)  + ' AND ' +                                    
  ' POLICY_VERSION_ID = ' + CAST(@POLICY_VERSION_ID AS VARCHAR)   + ' AND ' +   @TEMPCOLUMNNAME + ' IN  ' +                                 
  '(SELECT ' +  @TABLE_COLUMN1 + ' FROM  ' + @VEHICLE_TABLE +                                    
  ' WHERE ' +                                   
  ' CLAIM_ID=' + CAST(@CLAIM_ID AS VARCHAR) + ')' +          
 ' AND C.COVERAGE_TYPE=' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + ''''               
                    
PRINT @TEMPSTR    
EXEC (@TEMPSTR)                         
                                      
          
--Vehicle level coverages          
 set @TEMPSTR='SELECT CAR.PRIMARY_EXCESS,CAR.ATTACHMENT_POINT,CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.RESERVE_ID, '+                                  
      ' CAR.REINSURANCE_CARRIER,CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES, ' + @TEMPCOLUMNNAME + ' AS VEHICLE_ID,' +                                   
      '  MC.COV_ID, MC.COV_CODE, COV_DES as COV_DESC ,'+                      
    '(CAST(CIV.VIN AS VARCHAR)+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                     
   '+CAST(CIV.VEHICLE_YEAR AS VARCHAR)+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                     
   '+CAST(CIV.MAKE AS VARCHAR)+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                     
   '+CAST(CIV.MODEL AS VARCHAR)) AS VEHICLE, ' +                        
       ' CASE MC.LIMIT_TYPE ' +                                   
       ' WHEN 2 THEN ' +                                   
        ' substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0, + charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) + ' +                                   
      ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,''''))            + ''/'' + ' +                                                       
       ' substring(convert(varchar(30),convert(money,PVC.LIMIT_2),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) +  ' +                                   
      ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'''')) ' +                                   
   ' ELSE substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT1_AMOUNT_TEXT,'''')) ' +                                   
       ' END AS LIMIT ,' +                                                         
       ' CASE MC.DEDUCTIBLE_TYPE ' +                                   
       ' WHEN 2 THEN Substring(convert(varchar(30),convert(money,PVC.Deductible_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_1),1),0)) + ''' + ' ' + '''+' +                               
      ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) + ''/'' + ' +                                   
      ' substring(convert(varchar(30),convert(money,PVC.Deductible_2),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_2),1),0))  + ' +                                   
      ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE2_AMOUNT_TEXT,'''')) ' +                                                        
       ' ELSE substring(convert(varchar(30),convert(money,PVC.Deductible_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_1),1),0)) + ''' + ' ' + '''+' +                               
      ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) ' +                                   
       ' END AS DEDUCTIBLE, '  +                                    
       ' MC.COVERAGE_TYPE '  +                                    
       ' FROM CLM_CLAIM_INFO CCI JOIN CLM_ACTIVITY_RESERVE CAR  ON CCI.CLAIM_ID = CAR.CLAIM_ID ' +                                   
       ' JOIN ' + @TEMPTABLENAME + ' PVC ON CCI.POLICY_ID = PVC.POLICY_ID AND ' +                                                  
       ' CCI.POLICY_VERSION_ID = PVC.POLICY_VERSION_ID AND ' +                                   
       ' CCI.CUSTOMER_ID = PVC.CUSTOMER_ID AND CAR.COVERAGE_ID=PVC.COVERAGE_CODE_ID AND ' +                                   
       ' CAR.VEHICLE_ID = ' + @TEMPCOLUMNNAME +                                   
       ' JOIN MNT_COVERAGE MC ON MC.COV_ID = CAR.COVERAGE_ID ' +                     
    ' LEFT OUTER JOIN CLM_INSURED_VEHICLE CIV ON CAR.VEHICLE_ID = CIV.POLICY_VEHICLE_ID ' +                     
    'WHERE ' +                       
       ' CCI.CLAIM_ID=' + CAST(@CLAIM_ID AS VARCHAR(50))  + ' AND MC.COVERAGE_TYPE<>' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + '''' +                     
    ' AND CIV.CLAIM_ID=' + CAST(@CLAIM_ID AS VARCHAR(50))  +        
          
  ' UNION ' +                     
          
  'SELECT' +                                       
          
   ' 0  AS PRIMARY_EXCESS,0 AS ATTACHMENT_POINT,0 AS OUTSTANDING,0 AS RI_RESERVE,0 AS RESERVE_ID,' +                                       
   ' 0 AS REINSURANCE_CARRIER,0 AS MCCA_ATTACHMENT_POINT,0 AS MCCA_APPLIES,' +                                         
    CAST(@TEMPCOLUMNNAME AS VARCHAR) + ' AS VEHICLE_ID,C.COV_ID,C.COV_CODE,COV_DES as COV_DESC, ' +                
   '(CAST(CIV.VIN AS VARCHAR)+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +               
   '+CAST(CIV.VEHICLE_YEAR AS VARCHAR)+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +               
   '+CAST(CIV.MAKE AS VARCHAR)+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +               
   '+CAST(CIV.MODEL AS VARCHAR)) AS VEHICLE, ' +                                         
    ' CASE C.LIMIT_TYPE ' +                                       
   ' WHEN 2 THEN ' +                                       
   ' substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0, ' +                                       
  ' charindex(''.'',convert(varchar(30),' +                                       
   ' convert(money,PVC.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'''')) + ''/'' + substring(convert(varchar(30),' +                                       
   ' convert(money,PVC.LIMIT_2),1),0,charindex(''.'',convert(varchar(30),' +                                       
   ' convert(money,PVC.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'''')) ' +                                       
    ' ELSE substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0,charindex(''.'', ' +                                       
   ' convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) + CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT1_AMOUNT_TEXT,'''')) ' +                                         
    ' END AS LIMIT, ' +                                       
    ' CASE C.DEDUCTIBLE_TYPE ' +                                       
   ' WHEN 2 THEN Substring(convert(varchar(30),' +                                       
   ' convert(money,PVC.Deductible_1),1),0,charindex(''.'', ' +                                       
   ' convert(varchar(30),convert(money,PVC.Deductible_1),1),0))+ '' '' + CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) + ''/'' + substring(convert(varchar(30),convert(money,PVC.Deductible_2),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_2),1),0))  + CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE2_AMOUNT_TEXT,'''')) ' +                                       
    ' ELSE substring(convert(varchar(30),convert(money,PVC.Deductible_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_1),1),0)) + '' '' + CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) ' +                           
  
              
    ' END AS DEDUCTIBLE, '  +                                                    
    ' COVERAGE_TYPE ' +                           
    ' FROM ' + CAST(@TEMPTABLENAME AS VARCHAR) +  '  PVC ' +                                       
   ' INNER JOIN ' +                                       
    ' MNT_COVERAGE C ' +                                       
    ' ON  ' +                                       
    ' PVC.COVERAGE_CODE_ID = C.COV_ID ' +                
   ' LEFT OUTER JOIN CLM_INSURED_VEHICLE CIV ON PVC.VEHICLE_ID = CIV.POLICY_VEHICLE_ID ' +               
		
    ' WHERE ' +                                       
    ' CUSTOMER_ID = ' + CAST(@CUSTOMER_ID AS VARCHAR)  +              
    ' AND POLICY_ID = ' + CAST(@POLICY_ID AS VARCHAR)  +               
    ' AND POLICY_VERSION_ID = ' + CAST(@POLICY_VERSION_ID AS VARCHAR)   +       
   ' AND CIV.CLAIM_ID=' + CAST(@CLAIM_ID AS VARCHAR) +                
   ' AND C.COVERAGE_TYPE<>' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + '''' +	 ' ORDER BY VEHICLE_ID '      
        
print @TEMPSTR    
EXEC (@TEMPSTR)              
          
END          
        
      
    
  


GO

