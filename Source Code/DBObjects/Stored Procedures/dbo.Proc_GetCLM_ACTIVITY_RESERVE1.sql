IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ACTIVITY_RESERVE1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ACTIVITY_RESERVE1]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--begin tran  
--DROP PROC dbo.Proc_GetCLM_ACTIVITY_RESERVE  
--go  
/*----------------------------------------------------------                                                        
Proc Name       : dbo.Proc_GetCLM_ACTIVITY_RESERVE                                                                                             
                                                                                             
Created by      : Sumit Chhabra                                                                                                                                                                                                  
Date            : 30/05/2006                                                                                                                                                                                                    
Purpose         : Fetch data from CLM_ACTIVITY_RESERVE table for claim reserve screen                                   
Created by      : Sumit Chhabra                                                                                                                                                                                                   
Revison History :                                                                                                                                                                                                    
Used In        : Wolverine                                                                                                                                                                                                    
------------------------------------------------------------                                                        
MODIFIED BY  : Asfa Praveen                              
Date  : 14-Sept-2007                              
Purpose  : Modify Column-Name as per Grid .cs code                            
------------------------------------------------------------                                    
Date     Review By          Comments                                                                                                                                                                                                    
------   ------------       -------------------------*/                                                                
 --DROP PROC dbo.Proc_GetCLM_ACTIVITY_RESERVE   2432                                                                                
                                                                               
CREATE   PROC [dbo].[Proc_GetCLM_ACTIVITY_RESERVE1]                                                                     
(                                                                                                         
 @CLAIM_ID int    ,                                                                                          
 @ACTIVITY_ID int=null                    
)                       
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
     
DECLARE @VEHICLE_ID INT      
DECLARE @DUMMY_POLICY_ID INT                      
                      
                                         
DECLARE @CUSTOMER_ID INT                                             
DECLARE @POLICY_ID INT                                            
DECLARE @POLICY_VERSION_ID SMALLINT                                                  
DECLARE @LOB_ID INT                                                                                                        
DECLARE @COVERAGE_TABLE VARCHAR(100)                                                                                            
DECLARE @VEHICLE_COLUMN_NAME VARCHAR(100)                                                                               
DECLARE @TEMPSTR VARCHAR(5000)                                                                                                     
declare @POLICY_COVERAGES varchar(10)                                                                   
DECLARE @DASH_OPERATOR VARCHAR(5)                                                                        
DECLARE @VEHICLE_TEXT VARCHAR(500)                                                                       
declare @VEHICLE_TABLE VARCHAR(50)                                                                                                  
DECLARE @POLICY_VEHICLE_COLUMN_NAME VARCHAR(20)                                          
declare @STATE_ID smallint                                                                                                  
                                                                                                 
declare @AUTOMOBILE smallint                                                                                  
declare @MOTORCYCLE smallint                                                                                                      
DECLARE @WATERCRART smallint                                                                                                      
declare @HOMEOWNER smallint                                                                                             
declare @RENTAL smallint                                                                                                      
declare @GENERAL_LIABILITY smallint                                                                                                      
declare @UMBRELLA smallint                                                                                               
DECLARE @TEMP varchar(3000)                       
DECLARE @TRANSACTION_ID INT                                                                                                        
DECLARE @ACTIVTY_ID INT                      
                                                                                                      
                                                                                     
set @AUTOMOBILE = 2                                                                                                      
set @MOTORCYCLE = 3                                                                                                      
set @WATERCRART = 4                                                                                                      
set @HOMEOWNER = 1                                                                                                      
set @RENTAL = 6                                                                                                      
set @GENERAL_LIABILITY = 7                                                                                              
set @UMBRELLA = 5                                                                                           
                                                                  
SET @POLICY_COVERAGES = 'PL'                                                           
SET @DASH_OPERATOR = '-'                                                  
SET @VEHICLE_TEXT = ''                 
                      
SELECT @DUMMY_POLICY_ID = DUMMY_POLICY_ID FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID           
             
IF @DUMMY_POLICY_ID IS NOT NULL                      
BEGIN                      
 SELECT @VEHICLE_ID = INSURED_VEHICLE_ID FROM CLM_INSURED_VEHICLE WHERE CLAIM_ID=@CLAIM_ID                      
  
 SELECT @STATE_ID=ISNULL(DUMMY_STATE,0) FROM CLM_DUMMY_POLICY WHERE CLAIM_ID=@CLAIM_ID                                                                                                                    
 IF (@ACTIVITY_ID IS NOT NULL)         
 BEGIN                      
 SELECT @TRANSACTION_ID = ISNULL(MAX(TRANSACTION_ID),0) FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND        ACTIVITY_ID=@ACTIVITY_ID               
 END              
 ELSE             
 BEGIN                   
 SELECT @ACTIVTY_ID=ISNULL(MAX(CR.ACTIVITY_ID),0) FROM CLM_ACTIVITY CA      
 INNER JOIN CLM_ACTIVITY_RESERVE CR       
 ON CA.ACTIVITY_ID = CASE CR.ACTIVITY_ID  WHEN 0 THEN 1 ELSE CR.ACTIVITY_ID  END      
 WHERE CA.CLAIM_ID=@CLAIM_ID AND CA.ACTIVITY_STATUS =11801              
  
 --This is done as in CLM_ACTIVITY table activity_id is set as 1 for a 'New Reserve' but in CLM_ACTIVITY_RESERVE table activity id is set to 0 for a new reserve. So we are not able to get correct transaction for a New Reserve      
 IF(@ACTIVTY_ID = 1)      
  SET @ACTIVTY_ID = 0      
   SELECT @TRANSACTION_ID = ISNULL(MAX(TRANSACTION_ID),0) FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND        ACTIVITY_ID = @ACTIVTY_ID      
 END                  
   
 SELECT '' AS MCCA_ATTACHMENT_POINT, '' AS MCCA_APPLIES, @VEHICLE_ID AS VEHICLE_ID, @STATE_ID AS STATE_ID, MCC.CLAIM_ID, MCC.COV_DES AS COV_DESC,                        
 MCC.LIMIT_1 AS LIMIT, MCC.DEDUCTIBLE_1 AS DEDUCTIBLE, MCC.COV_ID_CLAIM,                       
 CAR.OUTSTANDING, CAR.RI_RESERVE, CAR.CLAIM_ID, CAR.ACTION_ON_PAYMENT  AS CLM_RESERVE_ACTION_ON_PAYMENT,                       
 ISNULL(CAR.DRACCTS,0) AS CLM_RESERVE_DRACCTS,  ISNULL(CAR.CRACCTS,0) AS CLM_RESERVE_CRACCTS, CAR.RESERVE_ID,                       
 CAR.REINSURANCE_CARRIER, MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER, CAR.COVERAGE_ID AS COV_ID                      
 FROM MNT_CLAIM_COVERAGE MCC                      
 LEFT OUTER JOIN CLM_ACTIVITY_RESERVE CAR                      
 ON MCC.CLAIM_ID = CAR.CLAIM_ID AND MCC.COV_ID = CAR.COVERAGE_ID                      
 LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV                       
 ON MLV.LOOKUP_UNIQUE_ID = CAR.REINSURANCE_CARRIER                      
 LEFT OUTER JOIN CLM_ACTIVITY_PAYMENT CAP                       
 ON CAR.CLAIM_ID = CAP.CLAIM_ID AND CAR.RESERVE_ID = CAP.RESERVE_ID                       
 WHERE CAR.CLAIM_ID = @CLAIM_ID                      
 AND CAR.TRANSACTION_ID=@TRANSACTION_ID                      
 ORDER BY CAR.RESERVE_ID                      
END                      
ELSE                      
BEGIN                      
 SELECT                                           
 @CUSTOMER_ID=PCPL.CUSTOMER_ID,@POLICY_ID=PCPL.POLICY_ID,@POLICY_VERSION_ID=PCPL.POLICY_VERSION_ID,                                           
 @LOB_ID = ISNULL(POLICY_LOB,0),@STATE_ID=ISNULL(PCPL.STATE_ID,0)                                          
 FROM                             
 POL_CUSTOMER_POLICY_LIST PCPL JOIN CLM_CLAIM_INFO CCI                                           
 ON          
 PCPL.CUSTOMER_ID = CCI.CUSTOMER_ID AND PCPL.POLICY_ID = CCI.POLICY_ID AND                                                                       
 PCPL.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID                                                        
 WHERE                                      
 CCI.CLAIM_ID=@CLAIM_ID                                                                      
   
 IF(@ACTIVITY_ID IS NOT NULL)        
 begin                    
  SELECT @TRANSACTION_ID= ISNULL(MAX(TRANSACTION_ID),0) FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y' AND ACTIVITY_ID=@ACTIVITY_ID;        
 end                      
 ELSE                    
 begin    
  --Added for Itrack Issue 5548 on 18 June 2009        
  -- SELECT @TRANSACTION_ID= ISNULL(MAX(TRANSACTION_ID),0) FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y';                   
  
  SELECT @ACTIVTY_ID=ISNULL(MAX(CR.ACTIVITY_ID),0) FROM CLM_ACTIVITY CA      
  INNER JOIN CLM_ACTIVITY_RESERVE CR       
  ON CA.ACTIVITY_ID = CASE CR.ACTIVITY_ID  WHEN 0 THEN 1 ELSE CR.ACTIVITY_ID  END     
  AND CA.CLAIM_ID=CR.CLAIM_ID     
  WHERE CA.CLAIM_ID=@CLAIM_ID AND CA.ACTIVITY_STATUS =11801       
  --AND ACTION_ON_PAYMENT IN (165,166,167,168,205)      
  
  --This is done as in CLM_ACTIVITY table activity_id is set as 1 for a 'New Reserve' but in CLM_ACTIVITY_RESERVE table activity id is set to 0 for a new reserve. So we are not able to get correct transaction for a New Reserve      
  IF(@ACTIVTY_ID=1)      
   SET @ACTIVTY_ID=0      
  
   SELECT @TRANSACTION_ID = ISNULL(MAX(TRANSACTION_ID),0) FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID = @ACTIVTY_ID AND IS_ACTIVE='Y';        
  end                 
  /* SELECT                                                                                                   
  
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
  POLICY_VERSION_ID=@POLICY_VERSION_ID*/                                                                                                                           
                                                                  
  IF(@LOB_ID IS NULL OR @LOB_ID=0)                                   
   RETURN                                                                                                                        
                                                                                           
                                                                                             
  IF(@LOB_ID=@HOMEOWNER OR @LOB_ID=@RENTAL)       
  BEGIN                                                                                 
   
   SET @COVERAGE_TABLE = 'POL_DWELLING_SECTION_COVERAGES'                                                                                          
   SET @VEHICLE_COLUMN_NAME = 'PVC.DWELLING_ID '                                                               
   SET @VEHICLE_TABLE = 'CLM_INSURED_LOCATION'                                                                                         
   SET @POLICY_VEHICLE_COLUMN_NAME = 'POLICY_LOCATION_ID'                       
   -- SET @VEHICLE_TEXT = 'PVC.DWELLING_ID AS DWELLING, ' //Commented by Asfa (14-Sept-2007)   
   SET @VEHICLE_TEXT = 'PVC.DWELLING_ID AS VEHICLE, '                            
  
   SET @POLICY_COVERAGES = 'S1'                                                                                                    
   --Select SectionI coverages                                                                            
   SET @TEMPSTR='SELECT CAST(ISNULL(CAR.PRIMARY_EXCESS,0) AS INT) PRIMARY_EXCESS,CAR.ATTACHMENT_POINT, '  +                              
   ' CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.CLAIM_ID,CAR.ACTION_ON_PAYMENT AS CLM_RESERVE_ACTION_ON_PAYMENT, ' +                               
   ' CAR.CRACCTS AS CLM_RESERVE_CRACCTS,CAR.DRACCTS AS CLM_RESERVE_DRACCTS,CAR.RESERVE_ID, '+                             
   --       ' CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES, ' + @VEHICLE_COLUMN_NAME + ' ,' +     //Commented by Asfa (14-Sept-2007)                            
   ' CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES, ' + @VEHICLE_COLUMN_NAME + ' VEHICLE_ID,' +                                                          
  
  
   '  MC.COV_ID, MC.COV_CODE, MC.COV_DES AS COV_DESC ,' +  CAST(@STATE_ID AS VARCHAR) + ' AS STATE_ID, ' +                                                                                                            
   CAST(@VEHICLE_TEXT AS VARCHAR(500)) +                                                                                          
   ' CASE MC.LIMIT_TYPE ' +                                            
   ' WHEN 2 THEN ' +                                
   ' SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0, + CHARINDEX(''.'',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)) + ' +  
    ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,''''))            + ''/'' + ' +                        
  
                                              
   ' SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_2),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_2),1),0)) +  ' +                                                                                         
   ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'''')) ' +                      
   --Done for Itrack Issue 5720 on 30 April 2009                                              
   ' ELSE ISNULL(SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)),'''') + CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT1_AMOUNT_TEXT,'''')) ' +                                   
   ' END AS LIMIT ,' +                                                                                                                                                 
   ' CASE MC.DEDUCTIBLE_TYPE ' +             
   ' WHEN 2 THEN SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0)) + ''' + ' ' + '''+' +                          
   --  ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) + ''/'' + ' +                                         
   ' ''/'' + ' +                                             
   ' SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_2),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_2),1),0)) ' +                          
   --    ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE2_AMOUNT_TEXT,'''')) ' +      
   ' ELSE SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0)) ' +                         
   --       ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) ' +                                                                                                                          
   ' END AS DEDUCTIBLE, '  +                                                                                                                      
   ' MC.COVERAGE_TYPE, ISNULL(CAST(PVC.DEDUCTIBLE AS VARCHAR(10)),'''') + ISNULL(PVC.DEDUCTIBLE_TEXT,'''') AS DEDUCTIBLE2, '  +                                                                                          
   --Done on 16 Feb for Itrack # 7031  
   'CASE WHEN ISNULL(MC.RANK,0)= 0 THEN MCE.RANK ELSE MC.RANK END AS RANK' + 'CAR.ACTUAL_RISK_ID, CAR.ACTUAL_RISK_TYPE,CAR.VEHICLE_ID' +  
   ' FROM CLM_CLAIM_INFO CCI JOIN CLM_ACTIVITY_RESERVE CAR  ON CCI.CLAIM_ID = CAR.CLAIM_ID ' +                                                               
   ' JOIN ' + @COVERAGE_TABLE + ' PVC ON CCI.POLICY_ID = PVC.POLICY_ID AND ' +                                                                                                    
   ' CCI.POLICY_VERSION_ID = PVC.POLICY_VERSION_ID AND ' +                                                         
   ' CCI.CUSTOMER_ID = PVC.CUSTOMER_ID AND CAR.COVERAGE_ID=PVC.COVERAGE_CODE_ID AND ' +                                                           
   ' CAR.VEHICLE_ID = ' + @VEHICLE_COLUMN_NAME +                                                                                                                           
   ' JOIN MNT_COVERAGE MC ON MC.COV_ID = CAR.COVERAGE_ID ' +    
   --Done for Itrack Issue 7031 on 16 Feb 2010   
   'LEFT OUTER JOIN MNT_COVERAGE_EXTRA MCE ON MCE.COV_ID = CAR.COVERAGE_ID' +                                                               
   ' LEFT JOIN POL_DWELLINGS_INFO PDI ON PDI.CUSTOMER_ID=CCI.CUSTOMER_ID AND PDI.POLICY_ID=CCI.POLICY_ID AND ' +                              
   ' PDI.POLICY_VERSION_ID=CCI.POLICY_VERSION_ID AND PDI.DWELLING_ID = PVC.DWELLING_ID ' +                                                           
   ' LEFT JOIN ' + CAST(@VEHICLE_TABLE AS VARCHAR) +  ' CIV ON CCI.CLAIM_ID = CIV.CLAIM_ID AND CIV.' + CAST(@POLICY_VEHICLE_COLUMN_NAME  AS VARCHAR) + '=PDI.LOCATION_ID ' +                                               
   --       ' LEFT OUTER JOIN ' + CAST(@VEHICLE_TABLE AS VARCHAR) + ' CIV ON CAR.VEHICLE_ID = CIV.' + CAST(@POLICY_VEHICLE_COLUMN_NAME  AS VARCHAR)   +                                                                                                        
  
  
  
   
  
   ' LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ' +                                                                            
   ' ON MLV.LOOKUP_UNIQUE_ID = CAR.REINSURANCE_CARRIER ' +                                                                                
   ' WHERE ' +                                                                                                            
   ' CCI.CLAIM_ID=' + CAST(@CLAIM_ID AS VARCHAR(50))  +                       
   ' AND MC.COVERAGE_TYPE=' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + '''' +                                          
   ' AND CIV.CLAIM_ID=' + CAST(@CLAIM_ID AS VARCHAR(50))   +                                                           
   -- Added by Asfa (12-Oct-2007) - To fetch the last reserve info.                      
   ' AND CAR.TRANSACTION_ID=' + CAST(@TRANSACTION_ID AS VARCHAR(50))   +                      
   -- ' ORDER BY CAR.RESERVE_ID '   
   --Done on 16 Feb for Itrack # 7031                            
   ' ORDER BY CAR.VEHICLE_ID,CAR.ACTUAL_RISK_TYPE , CAR.ACTUAL_RISK_ID ,CAR.RESERVE_ID,RANK '           
                                             
  END                                                                                            
  ELSE IF(@LOB_ID=@AUTOMOBILE OR @LOB_ID=@MOTORCYCLE)               
  BEGIN                    
   SET @COVERAGE_TABLE = 'POL_VEHICLE_COVERAGES'                                                                                                                   
   SET @VEHICLE_COLUMN_NAME = 'PVC.VEHICLE_ID '                                                                                                  
   SET @VEHICLE_TABLE = 'CLM_INSURED_VEHICLE'                                                                                                                         
   SET @POLICY_VEHICLE_COLUMN_NAME = 'POLICY_VEHICLE_ID'                                                                                                  
   SET @VEHICLE_TEXT = '(CAST(CIV.VIN AS VARCHAR(50))+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                                                                            
   '+CAST(CIV.VEHICLE_YEAR AS VARCHAR(20))+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                        
   '+CAST(CIV.MAKE AS VARCHAR(50))+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                                                                        
   '+CAST(CIV.MODEL AS VARCHAR(50))) AS VEHICLE, '                                                                                                 
  
   SET @TEMPSTR='SELECT DISTINCT CAST(ISNULL(CAR.PRIMARY_EXCESS,0) AS INT) PRIMARY_EXCESS,CAR.ATTACHMENT_POINT, ' +                       
   'CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.CLAIM_ID,CAR.ACTION_ON_PAYMENT AS CLM_RESERVE_ACTION_ON_PAYMENT, ' +                      
   'CAR.CRACCTS AS CLM_RESERVE_CRACCTS,CAR.DRACCTS AS CLM_RESERVE_DRACCTS,CAR.RESERVE_ID, '+                               
   ' CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES, ' +                                                  
   ' CAR.COVERAGE_ID AS COV_ID, MC.COV_CODE, ' +  CAST(@STATE_ID AS VARCHAR) + ' AS STATE_ID, '   --COV_DES as COV_DESC ,'+                        
     
   if(@LOB_ID=@AUTOMOBILE)                                              
   begin              
                          
    SET @TEMPSTR= @TEMPSTR +                                               
    ' CASE CAST(CAR.COVERAGE_ID AS VARCHAR) WHEN ''50001'' THEN ''Medical'' WHEN ''50002'' THEN ''Work Loss''' +                                                                                       
    ' WHEN ''50003'' THEN ''Death Benefits'' WHEN ''50004'' THEN ''Survivors Benefits''  ' +                        
    ' WHEN ''50005'' THEN ''BI'' WHEN ''50006'' THEN ''PD'' ' +                                         
    ' WHEN ''50007'' THEN ''BI'' WHEN ''50008'' THEN ''PD'' ' +                                         
    ' WHEN ''50009'' THEN ''BI'' WHEN ''50010'' THEN ''PD'' ' +    
    --Done for Itrack Issue 6625 on 24 Oct 09  
    ' WHEN ''50011'' THEN ''BI'' WHEN ''50012'' THEN ''PD'' ' +   
    ' WHEN ''50013'' THEN ''BI'' WHEN ''50014'' THEN ''PD'' ' +                                         
    ' WHEN ''50015'' THEN ''BI'' WHEN ''50016'' THEN ''PD'' ELSE MC.COV_DES ' +                                        
    ' END AS COV_DESC, '   
   
    -- SET @TEMPSTR= @TEMPSTR +                                               
    --     ' CASE CAST(CAR.COVERAGE_ID AS VARCHAR) WHEN'  
    --CASE  WHEN ISNULL(MC.COV_DES,'')='' THEN MC1.COV_DES ELSE MC.COV_DES END AS COV_DES                                              
   end                                              
   else                                              
   begin   
    SET @TEMPSTR= @TEMPSTR + ' MC.COV_DES as COV_DESC , '                                 
   end                                              
    
   --Done on 16 Feb for Itrack # 7031  
   SET @TEMPSTR = @TEMPSTR +  'CASE WHEN ISNULL(MC.RANK,0)= 0 THEN MCE.RANK ELSE MC.RANK END AS RANK ,'  
  
   SET @TEMPSTR= @TEMPSTR +                                              
   ' CASE MC.LIMIT_TYPE ' +                                                                                                                           
   ' WHEN 2 THEN ' +                                                    
   ' substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0, + charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) + ' +                                                                            
   ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,''''))            + ''/'' + ' +                                    
   ' substring(convert(varchar(30),convert(money,PVC.LIMIT_2),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_2),1),0)) +  ' +                                                    
   ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'''')) ' +                                                        --Done for Itrack Issue 5720 on 30 April 2009                              
   ' ELSE isnull(substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)),'''') + CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT1_AMOUNT_TEXT,'''')) ' +              
  
   ' END AS LIMIT ,' +                                                                                                                                                 
   ' CASE MC.DEDUCTIBLE_TYPE ' +                                                
   ' WHEN 2 THEN Substring(convert(varchar(30),convert(money,PVC.Deductible_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_1),1),0)) + ''' + ' ' + '''+' +                                                                        
  
   --      ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) + ''/'' + ' +                                                                     
   ' ''/'' + ' +                                                                     
   ' substring(convert(varchar(30),convert(money,PVC.Deductible_2),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_2),1),0))  ' +                                                                        
   --      ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE2_AMOUNT_TEXT,'''')) ' +                                                          
   ' ELSE substring(convert(varchar(30),convert(money,PVC.Deductible_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_1),1),0)) ' +                                                                     
  
   --    ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) ' +                                                                                                      
   ' END AS DEDUCTIBLE, '  +                                                                                                                      
   ' MC.COVERAGE_TYPE, '  +     
   'CAR.ACTUAL_RISK_ID, CAR.ACTUAL_RISK_TYPE,CAR.VEHICLE_ID' +                                                                                                                         
   /*' FROM CLM_CLAIM_INFO CCI JOIN CLM_ACTIVITY_RESERVE CAR  ON CCI.CLAIM_ID = CAR.CLAIM_ID ' +                                                                                                                           
   ' JOIN ' + @COVERAGE_TABLE + ' PVC ON CCI.POLICY_ID = PVC.POLICY_ID AND ' +                                                                                          
   ' CCI.POLICY_VERSION_ID = PVC.POLICY_VERSION_ID AND ' +                                                        
   ' CCI.CUSTOMER_ID = PVC.CUSTOMER_ID AND CAR.COVERAGE_ID=PVC.COVERAGE_CODE_ID ' +                                                                                               
   ' JOIN MNT_COVERAGE MC ON MC.COV_ID = CAR.COVERAGE_ID WHERE ' +            
   ' CCI.CLAIM_ID=' + CAST(@CLAIM_ID AS VARCHAR(50)) + ' AND MC.COVERAGE_TYPE=' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + ''''*/                                                                                        
   ' FROM CLM_CLAIM_INFO CCI ' +                                      
   ' LEFT JOIN CLM_ACTIVITY_RESERVE CAR ON CAR.CLAIM_ID = CCI.CLAIM_ID ' +                                                                                         
   ' LEFT JOIN POL_VEHICLE_COVERAGES PVC ON PVC.CUSTOMER_ID = CCI.CUSTOMER_ID AND PVC.POLICY_ID = CCI.POLICY_ID AND PVC.POLICY_VERSION_ID=CCI.POLICY_VERSION_ID AND CAR.COVERAGE_ID = PVC.COVERAGE_CODE_ID ' +                                                 
  
  
  
  
   
  ' LEFT JOIN MNT_COVERAGE MC ON MC.COV_ID = CAR.COVERAGE_ID  ' +   
   --Done for Itrack Issue 7031 on 16 Feb 2010   
   'LEFT OUTER JOIN MNT_COVERAGE_EXTRA MCE ON MCE.COV_ID = CAR.COVERAGE_ID' +                                                                                  
   ' LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ' +                                              
   ' ON MLV.LOOKUP_UNIQUE_ID = CAR.REINSURANCE_CARRIER ' +                                                            
   ' WHERE CAR.CLAIM_ID = ' + CAST(@CLAIM_ID AS VARCHAR(50)) +                       
--   ' AND ( MC.COVERAGE_TYPE=' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + '''' +                       
--   ' OR MC.COVERAGE_TYPE IS NULL)' +    
   ' AND  ( CASE WHEN CAR.COVERAGE_ID IN (50017,50018,50019,50020,50021,50022,50023,50024,50025,50026,50027,50028 ) THEN ''RL''' +        
   ' ELSE MC.COVERAGE_TYPE END =' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + '''' +                                                                                 
   ' OR (CAR.COVERAGE_ID NOT IN (50017,50018,50019,50020,50021,50022,50023,50024,50025,50026,50027,50028 ) ' +        
   ' AND MC.COVERAGE_TYPE  IS NULL))' +  
            -- Added by Asfa (12-Oct-2007) - To fetch the last reserve info.                      
   ' AND CAR.TRANSACTION_ID=' + CAST(@TRANSACTION_ID AS VARCHAR(50))   +                      
   --' ORDER BY CAR.RESERVE_ID '   
   --Done on 16 Feb for Itrack # 7031   
   ' ORDER BY CAR.VEHICLE_ID,CAR.ACTUAL_RISK_TYPE , CAR.ACTUAL_RISK_ID ,CAR.RESERVE_ID,RANK'                                                                                
                                                                         
  END                                                                                                              
  ELSE IF(@LOB_ID=@WATERCRART)                                                                                        
  BEGIN                                                                                                                          
   SET @COVERAGE_TABLE = 'POL_WATERCRAFT_COVERAGE_INFO'                               
   --  SET @VEHICLE_COLUMN_NAME = 'PVC.BOAT_ID '                                                                      
   SET @VEHICLE_COLUMN_NAME = 'CAR.VEHICLE_ID '                                                                      
   SET @VEHICLE_TABLE = 'CLM_INSURED_BOAT'                                                                                                  
   SET @POLICY_VEHICLE_COLUMN_NAME = 'POLICY_BOAT_ID'                                           
   SET @VEHICLE_TEXT = '(CAST(CIV.YEAR AS VARCHAR)+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                                             
   '+CAST(CIV.MAKE AS VARCHAR)+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                                                                                             
   '+CAST(CIV.MODEL AS VARCHAR)+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                         
   '+CAST(CIV.SERIAL_NUMBER AS VARCHAR)) AS VEHICLE, '                              
   --Since we do not have any policy level coverages for watercraft, display a blank table only                                                 
   SET @TEMPSTR = ' SELECT NULL '          
  END         
  ELSE         
   RETURN               
                               
  EXEC (@TEMPSTR)   
    
   --Done by Ankit for #Itrack 7642                                                                                                         
     SET @VEHICLE_COLUMN_NAME = 'CAR.VEHICLE_ID '                                                                                                                
                                                                                                                                                    
  SET @TEMPSTR='SELECT DISTINCT CAST(ISNULL(CAR.PRIMARY_EXCESS,0) AS INT) PRIMARY_EXCESS,CAR.ATTACHMENT_POINT, ' +                               
  ' CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.CLAIM_ID,CAR.ACTION_ON_PAYMENT AS CLM_RESERVE_ACTION_ON_PAYMENT, ' +                               
  ' CAR.CRACCTS AS CLM_RESERVE_CRACCTS,CAR.DRACCTS AS CLM_RESERVE_DRACCTS,CAR.RESERVE_ID, '+                                                                                                                           
  
  --       ' CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES, ' + @VEHICLE_COLUMN_NAME + ' ,' +   //Commented by Asfa (14-Sept-2007)                            
  ' CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES, ' + @VEHICLE_COLUMN_NAME + ' VEHICLE_ID,' +                              
  --        '  MC.COV_ID, MC.COV_CODE, '                      
  '  CAR.COVERAGE_ID COV_ID, MC.COV_CODE, '                      
  if(@LOB_ID=@WATERCRART)                                              
  begin                                  
  SET @TEMPSTR= @TEMPSTR +   
  --CASE WHEN CAR.COVERAGE_ID >= 50001 THEN ''Section 1 - Covered Property Damage - Actual Cash Value''    
  ' CASE WHEN CAR.COVERAGE_ID = 20001 THEN ''Section 1 - Covered Property Damage - Actual Cash Value''   
  WHEN CAR.COVERAGE_ID = 20002 THEN ''Section 1 - Covered Property Damage - Actual Cash Value''  
  WHEN CAR.COVERAGE_ID = 20003 THEN ''Section 1 - Covered Property Damage - Actual Cash Value''  ' +                                                                              
  ' ELSE MC.COV_DES ' +                            
  ' END AS COV_DESC, '                                         
 end                    
 else                                              
 begin                                              
  --ADD BY ANKIT FOR ITRACK #7642                                              
   SET @TEMPSTR= @TEMPSTR +   
  ' CASE CAST(CAR.COVERAGE_ID AS VARCHAR) '+      
  ' WHEN ''50017'' THEN ''Additional Physical Damage Coverage (M-14) - Collision'' WHEN ''50018'' THEN ''Additional Physical Damage Coverage (M-14) - Collision'' ' +      
  ' WHEN ''50019'' THEN ''Additional Physical Damage Coverage (M-14) - Other Than Collision'' WHEN ''50020'' THEN ''Additional Physical Damage Coverage (M-14) - Other Than Collision'' ' +      
  ' WHEN ''50021'' THEN ''Helmet & Riding Apparel Coverage (M-15) - Collision'' WHEN ''50022'' THEN ''Helmet & Riding Apparel Coverage (M-15) - Collision'' ' +      
  ' WHEN ''50023'' THEN ''Helmet & Riding Apparel Coverage (M-15) - Other Than Collision'' WHEN ''50024'' THEN ''Helmet & Riding Apparel Coverage (M-15) - Other Than Collision'' ' +   
  ' WHEN ''50025'' THEN ''Loan/Lease (PP 03 35) - Collision'' WHEN ''50026'' THEN ''Loan/Lease (PP 03 35) - Other Than Collision'' ' +    
  ' WHEN ''50027'' THEN ''Loan/Lease (PP 03 35) - Collision'' WHEN ''50028'' THEN ''Loan/Lease (PP 03 35) - Other Than Collision'' ELSE MC.COV_DES' +   
  ' END as COV_DESC , '                                 
 end                                              
    
 --Done on 16 Feb for Itrack # 7031  
 SET @TEMPSTR = @TEMPSTR +  'CASE WHEN ISNULL(MC.RANK,0)= 0 THEN MCE.RANK ELSE MC.RANK END AS RANK ,'  
                  
 SET @TEMPSTR= @TEMPSTR + CAST(@STATE_ID AS VARCHAR) + ' AS STATE_ID, ' +                                               
 CAST(@VEHICLE_TEXT AS VARCHAR(500)) +                                                                                                   
 ' CASE MC.LIMIT_TYPE ' +                                                                                                                           
 ' WHEN 2 THEN ' +                                                                                                                           
 ' SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0, + CHARINDEX(''.'',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)) + ' +                                               
 ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,''''))            + ''/'' + ' +                                                           
 ' SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_2),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_2),1),0)) +  ' +                                                                                                               
 ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'''')) ' +                                                    --Done for Itrack Issue 5720 on 30 April 2009      
 ' ELSE ISNULL(SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)),'''') + CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT1_AMOUNT_TEXT,'''')) ' +                         
 ' END AS LIMIT ,' +                                                                        
 ' CASE MC.DEDUCTIBLE_TYPE ' +                                                                                                                           
 ' WHEN 2 THEN SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0)) + ''' + ' ' + '''+' +                                                                       
  
  --' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) + ''/'' + ' +                                                                                    
 ' ''/'' + ' +                                                                                                              
 ' SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_2),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_2),1),0)) ' +                                                                                                      
 --       ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE2_AMOUNT_TEXT,'''')) ' +                                                                                                                           
 ' ELSE SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0,CHARINDEX(''.'',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0)) ' +                         
 --       ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) ' +                   
 ' END AS DEDUCTIBLE, '  +                                                                                                                           
 ' MC.COVERAGE_TYPE, ' +     
 'CAR.ACTUAL_RISK_ID, CAR.ACTUAL_RISK_TYPE'       
                                                              
 IF(@LOB_ID=@HOMEOWNER or @LOB_ID=@RENTAL)                                    
  SET @TEMPSTR = @TEMPSTR + ', ISNULL(CAST(PVC.DEDUCTIBLE AS VARCHAR(10)),'''') + ISNULL(PVC.DEDUCTIBLE_TEXT,'''') AS DEDUCTIBLE2 '                                                                         
                                                                   
 SET @TEMPSTR = @TEMPSTR +                                                                   
 ' FROM CLM_CLAIM_INFO CCI JOIN CLM_ACTIVITY_RESERVE CAR  ON CCI.CLAIM_ID = CAR.CLAIM_ID ' +                                                                                   
 ' LEFT OUTER JOIN ' + @COVERAGE_TABLE + ' PVC ON CCI.POLICY_ID = PVC.POLICY_ID AND ' +                                                                                                                         
 ' CCI.POLICY_VERSION_ID = PVC.POLICY_VERSION_ID AND ' +                                                                                    
  --Done by Ankit for #Itrack 7642                                                                                       
  --' CCI.CUSTOMER_ID = PVC.CUSTOMER_ID AND CAR.COVERAGE_ID=PVC.COVERAGE_CODE_ID AND '     
   ' CCI.CUSTOMER_ID = PVC.CUSTOMER_ID AND '+  
   ' CASE CAR.COVERAGE_ID WHEN 50021 THEN 203  
  WHEN 50023 THEN 203        
  WHEN 50024 THEN 219  
  WHEN 50022 THEN 219  
  WHEN 50020 THEN 1023  
  WHEN 50018 THEN 1023   
  WHEN 50017 THEN 1024  
  WHEN 50019 THEN 1024   
  WHEN 50027 THEN 46  
  WHEN 50028 THEN 46  
  WHEN 50025 THEN 249  
  WHEN 50026 THEN 249  
   ELSE CAR.COVERAGE_ID END =PVC.COVERAGE_CODE_ID AND '  
                                                                         
 IF(@LOB_ID=@WATERCRART)                                              
 BEGIN                      
  SET @TEMPSTR = @TEMPSTR + ' CAR.VEHICLE_ID = PVC.BOAT_ID'                    
 END          
 ELSE                      
 BEGIN        
    --Done by Ankit for #Itrack 7642                                                                                                             
         SET @VEHICLE_COLUMN_NAME = 'PVC.VEHICLE_ID '   
  SET @TEMPSTR = @TEMPSTR + ' CAR.VEHICLE_ID = ' + @VEHICLE_COLUMN_NAME                       
 END                      
              
    SET @TEMPSTR = @TEMPSTR + ' LEFT OUTER JOIN MNT_COVERAGE MC ON MC.COV_ID = CAR.COVERAGE_ID ' +    
 --Done for Itrack Issue 7031 on 16 Feb 2010  
  'LEFT OUTER JOIN MNT_COVERAGE_EXTRA MCE ON MCE.COV_ID = CAR.COVERAGE_ID'                                                                    
                  
 IF(@LOB_ID=@HOMEOWNER OR @LOB_ID=@RENTAL)                                    
 BEGIN                                                              
  SET @TEMPSTR = @TEMPSTR + ' LEFT JOIN POL_DWELLINGS_INFO PDI ON PDI.CUSTOMER_ID=CCI.CUSTOMER_ID AND PDI.POLICY_ID=CCI.POLICY_ID AND ' +                                                              
  ' PDI.POLICY_VERSION_ID=CCI.POLICY_VERSION_ID AND PDI.LOCATION_ID = PVC.DWELLING_ID ' +                                                     
  ' LEFT JOIN ' + CAST(@VEHICLE_TABLE AS VARCHAR) +  ' CIV ON CCI.CLAIM_ID = CIV.CLAIM_ID AND CIV.POLICY_LOCATION_ID = PDI.LOCATION_ID '                                                           
 END                                                              
 ELSE                         
  SET @TEMPSTR = @TEMPSTR + ' LEFT OUTER JOIN ' + CAST(@VEHICLE_TABLE AS VARCHAR) + ' CIV ON CAR.VEHICLE_ID = CIV.' + CAST(@POLICY_VEHICLE_COLUMN_NAME  AS VARCHAR)                                                                 
                                                         
    SET @TEMPSTR = @TEMPSTR + ' LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ' +                                                                                     
 ' ON MLV.LOOKUP_UNIQUE_ID = CAR.REINSURANCE_CARRIER ' +                                                                                     
 ' WHERE ' +                                                        
 ' CCI.CLAIM_ID=' + CAST(@CLAIM_ID AS VARCHAR(50))  +                       
 -- ' AND MC.COVERAGE_TYPE<>' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + ''''   +                                                          
 ' AND CIV.CLAIM_ID=' + CAST(@CLAIM_ID AS VARCHAR(50))   +                                                  
 -- Added by Asfa (12-Oct-2007) - To fetch the last reserve info.         
 ' AND CAR.TRANSACTION_ID=' + CAST(@TRANSACTION_ID AS VARCHAR(50))       
                     
 if(@LOB_ID<>@WATERCRART)                                              
 begin       
  --DONE BY ANKIT FOR ITRACK # 7642   
  -- SET @TEMPSTR = @TEMPSTR + ' AND MC.COVERAGE_TYPE<>' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + ''''    
    SET @TEMPSTR = @TEMPSTR + ' AND CASE WHEN CAR.COVERAGE_ID IN (50017,50018,50019,50020,50021,50022,50023,50024,50025,50026,50027,50028 ) THEN  ''RL''' +  
  + ' ELSE MC.COVERAGE_TYPE END<>' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + ''''                    
 end                      
   
  
 if(@LOB_ID = @WATERCRART)                                              
 begin                            
  SET @TEMPSTR = @TEMPSTR + ' AND ACTUAL_RISK_TYPE  <> ''EQUIP'''  
 end                      
                   
   
                   
    SET @TEMPSTR = @TEMPSTR +   
 --' ORDER BY CAR.RESERVE_ID '     
 --Done on 16 Feb for Itrack # 7031   
  ' ORDER BY VEHICLE_ID,ACTUAL_RISK_TYPE , ACTUAL_RISK_ID ,CAR.RESERVE_ID ,RANK '                                                                              
                                                                             
                                                                                       
                                    
 --print @TEMPSTR                              
 EXEC (@TEMPSTR)        
  
 END         
   
 exec Proc_GetOldWaterEquipCovgForClaims @CLAIM_ID,@TRANSACTION_ID         
  
END  
  
--go  
--exec Proc_GetCLM_ACTIVITY_RESERVE 3536  
--rollback tran  

GO

