IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ACTIVITY_PAYMENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ACTIVITY_PAYMENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN
--Drop PROC dbo.Proc_GetCLM_ACTIVITY_PAYMENT
--GO
/*----------------------------------------------------------                                                                                                                                    
Proc Name       : Proc_GetCLM_ACTIVITY_PAYMENT                                                                      
Created by      : Sumit Chhabra                                                                      
Date            : 02/06/2006                                                                                      
Purpose         : GEt  Claim Payment  data from CLM_ACTIVITY_PAYMENT                                                                           
Revison History :                                                                                      
Used In                   : Wolverine                                                                                                                              
------------------------------------------------------------                                                                                                                                    
MODIFIED BY   : Asfa Praveen                  
Date    : 24-Sept-2007                  
Purpose   : Fetch Deductible without Text field at Claims pages          
------------------------------------------------------------            
Date     Review By          Comments                                                                                                                                    
------   ------------       -------------------------*/                                                                                                                                    
--  Drop PROC dbo.Proc_GetCLM_ACTIVITY_PAYMENT            
--  Proc_GetCLM_ACTIVITY_PAYMENT 1263,4        
--  select * from CLM_ACTIVITY_PAYMENT            
            
CREATE PROC [dbo].[Proc_GetCLM_ACTIVITY_PAYMENT]            
 @CLAIM_ID int,                                                            
 @ACTIVITY_ID int                                                                
AS                                                                                                                                    
BEGIN                                                         
                                                      
DECLARE @VEHICLE_ID INT        
DECLARE @DUMMY_POLICY_ID INT        
        
DECLARE @TEMPSTR VARCHAR(8000)                                                
DECLARE @POLICY_COVERAGES VARCHAR(5)                                                      
DECLARE @DASH_OPERATOR VARCHAR(5)                                                 
declare @COVERAGE_TABLE  varchar(50)                                                
DECLARE @LOB_ID smallint                                                
declare @VEHICLE_COLUMN_NAME varchar(50)                                                
DECLARE @POLICY_VEHICLE_COLUMN_NAME VARCHAR(50)                                                
DECLARE @VEHICLE_TABLE VARCHAR(50)                                                
DECLARE @VEHICLE_TEXT VARCHAR(500)                       
DECLARE @STATE_ID smallint                                               
                                                
                                                
declare @AUTOMOBILE smallint                                                      
declare @MOTORCYCLE smallint                                                      
DECLARE @WATERCRART smallint                                                      
declare @HOMEOWNER smallint                                                      
declare @RENTAL smallint                                                      
declare @GENERAL_LIABILITY smallint                                                      
declare @UMBRELLA smallint                                                    
                           
                                                      
  
set @AUTOMOBILE = 2         
set @MOTORCYCLE = 3                                                      
set @WATERCRART = 4                                                      
set @HOMEOWNER = 1                  
set @RENTAL = 6             
set @GENERAL_LIABILITY = 7                                                      
set @UMBRELLA = 5                        
                                                     
SET @POLICY_COVERAGES = 'PL'                                                         
SET @DASH_OPERATOR = '-'                                                       
                                                
SELECT @DUMMY_POLICY_ID = DUMMY_POLICY_ID FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID        
  IF @DUMMY_POLICY_ID IS NOT NULL        
     BEGIN        
 SELECT @VEHICLE_ID = INSURED_VEHICLE_ID FROM CLM_INSURED_VEHICLE WHERE CLAIM_ID=@CLAIM_ID        
        
 SELECT @STATE_ID=ISNULL(DUMMY_STATE,0) FROM CLM_DUMMY_POLICY WHERE CLAIM_ID=@CLAIM_ID                                                                                                         
        
 SELECT '' AS MCCA_ATTACHMENT_POINT, '' AS MCCA_APPLIES, @VEHICLE_ID AS VEHICLE_ID, @STATE_ID AS STATE_ID, MCC.COV_ID, MCC.CLAIM_ID, MCC.COV_DES AS COV_DESC, MCC.LIMIT_1 AS LIMIT, MCC.DEDUCTIBLE_1 AS DEDUCTIBLE, MCC.COV_ID_CLAIM,         
 CAR.OUTSTANDING, CAR.RI_RESERVE, CAR.RESERVE_ID, CAR.REINSURANCE_CARRIER, MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER, CAR.ACTION_ON_PAYMENT AS CLM_RESERVE_ACTION_ON_PAYMENT,        
 CAP.PAYMENT_ID, ISNULL(CAP.DRACCTS,0) as DrAccts,  ISNULL(CAP.CRACCTS,0) as CrAccts,CAP.PAYMENT_AMOUNT,CAP.ACTION_ON_PAYMENT,CAP.PAYMENT_METHOD, ISNULL(CAP.CHECK_NUMBER,'') AS CHECK_NUMBER          
 FROM MNT_CLAIM_COVERAGE MCC        
 LEFT OUTER JOIN CLM_ACTIVITY_RESERVE CAR        
 ON MCC.CLAIM_ID = CAR.CLAIM_ID AND MCC.COV_ID = CAR.COVERAGE_ID        
 LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV         
 ON MLV.LOOKUP_UNIQUE_ID = CAR.REINSURANCE_CARRIER        
 LEFT OUTER JOIN CLM_ACTIVITY_PAYMENT CAP         
 ON CAR.CLAIM_ID = CAP.CLAIM_ID AND CAR.RESERVE_ID = CAP.RESERVE_ID         
 WHERE CAP.CLAIM_ID = @CLAIM_ID        
 AND CAP.ACTIVITY_ID=@ACTIVITY_ID        
 ORDER BY CAP.RESERVE_ID        
     END        
  ELSE        
     BEGIN        
             
 SELECT @LOB_ID = ISNULL(POLICY_LOB,0),@STATE_ID=ISNULL(PCPL.STATE_ID,0) FROM POL_CUSTOMER_POLICY_LIST PCPL JOIN CLM_CLAIM_INFO CCI ON                                                
    PCPL.CUSTOMER_ID = CCI.CUSTOMER_ID AND PCPL.POLICY_ID = CCI.POLICY_ID AND                                     PCPL.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID                                                 
    WHERE CCI.CLAIM_ID=@CLAIM_ID                                                
                                                 
                                                 
 IF(@LOB_ID=@HOMEOWNER OR @LOB_ID=@RENTAL)                                                                            
 BEGIN                                                                          
  SET @COVERAGE_TABLE = 'POL_DWELLING_SECTION_COVERAGES'                                                                            
   SET @VEHICLE_COLUMN_NAME = 'DWELLING_ID '                                                       
   SET @VEHICLE_TABLE = 'CLM_INSURED_LOCATION'                                              
   SET @POLICY_VEHICLE_COLUMN_NAME = 'POLICY_LOCATION_ID'                                                  
   SET @VEHICLE_TEXT = 'CAST(PVC.DWELLING_ID AS VARCHAR) AS DWELLING, '                                      
   SET @POLICY_COVERAGES = 'S1'                                
 --select SECTION 1 coverages                                                      
 SET @TEMPSTR = ' SELECT CAST(ISNULL(CAR.PRIMARY_EXCESS,0) AS INT) PRIMARY_EXCESS,CAR.ATTACHMENT_POINT,CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.RESERVE_ID, ' +                                                                   
         ' CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES, ' +            
 ' CAP.PAYMENT_ID,  ISNULL(CAP.DRACCTS,0) as DrAccts,  ISNULL(CAP.CRACCTS,0) as CrAccts,CAP.PAYMENT_AMOUNT,CAP.ACTION_ON_PAYMENT,CAP.PAYMENT_METHOD, ISNULL(CAP.CHECK_NUMBER,'''') AS CHECK_NUMBER , ' +                                                  
     
         ' PVC.' + CAST(@VEHICLE_COLUMN_NAME AS VARCHAR(50)) + ' , ' + CAST(@VEHICLE_TEXT AS VARCHAR(500)) +                             
      ' MC.COV_ID, MC.COV_CODE, COV_DES as COV_DESC , ' + CAST(@STATE_ID AS VARCHAR) + ' AS STATE_ID, ' +                                                                                    
         ' CASE MC.LIMIT_TYPE ' +                                                 
         ' WHEN 2 THEN ' +                                                 
         ' Substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) + ' +                                                 
         ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'''')) + ''/'' +  ' +                                                                               
         ' Substring(convert(varchar(30),convert(money,PVC.LIMIT_2),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_2),1),0)) + ' +                                                                               
         ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'''')) ' +                                                                                 
         ' ELSE Substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) +  ' +                                                 
       ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT1_AMOUNT_TEXT,'''')) ' +                                                                                 
         ' END AS LIMIT, ' +                                                                                 
         ' CASE MC.DEDUCTIBLE_TYPE ' +                                                                                 
         ' WHEN 2 THEN Substring(convert(varchar(30),convert(money,PVC.Deductible_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_1),1),0)) ' +                                                                               
 --        ' + CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) + ''/'' + ' +                                                                                 
         ' + ''/'' + ' +                                                                                 
         ' Substring(convert(varchar(30),convert(money,PVC.Deductible_2),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_2),1),0))    ' +                                                 
 --        ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE2_AMOUNT_TEXT,'''')) ' +                                                                                 
         ' ELSE Substring(convert(varchar(30),convert(money,PVC.Deductible_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_1),1),0))  ' +                                                 
 --        ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) ' +                                                                                 
         ' END AS DEDUCTIBLE, ' +                                           
         ' ISNULL(CAST(PVC.DEDUCTIBLE AS VARCHAR(10)),'''') + ISNULL(PVC.DEDUCTIBLE_TEXT,'''') AS DEDUCTIBLE2 '  +                                                                                    
         --Done on 16 Feb for Itrack # 7031    
   'CASE WHEN ISNULL(MC.RANK,0)= 0 THEN MCE.RANK ELSE MC.RANK END AS RANK ,' +     
   'CAP.ACTUAL_RISK_ID AS ACTUAL_RISK_ID, CAP.ACTUAL_RISK_TYPE AS ACTUAL_RISK_TYPE,CAP.VEHICLE_ID' +     
   ' FROM ' +                                         
         ' CLM_CLAIM_INFO CCI ' +                                                             
         ' LEFT JOIN ' +                                                 
         ' CLM_ACTIVITY_RESERVE CAR  ON CCI.CLAIM_ID = CAR.CLAIM_ID ' +                                               
         ' LEFT JOIN ' +              
  CAST(@COVERAGE_TABLE AS VARCHAR(50)) + ' PVC ON CCI.POLICY_ID = PVC.POLICY_ID AND ' +                                     
         ' CCI.POLICY_VERSION_ID = PVC.POLICY_VERSION_ID AND ' +                                                 
         ' CCI.CUSTOMER_ID = PVC.CUSTOMER_ID AND CAR.COVERAGE_ID=PVC.COVERAGE_CODE_ID AND ' +                                                 
         ' CAR.VEHICLE_ID=PVC.' + CAST(@VEHICLE_COLUMN_NAME AS VARCHAR(50)) +                                                 
   ' LEFT JOIN  ' +           
         ' MNT_COVERAGE MC ON MC.COV_ID = CAR.COVERAGE_ID ' +     
 --Done for Itrack Issue 7031 on 16 Feb 2010    
   'LEFT OUTER JOIN MNT_COVERAGE_EXTRA MCE ON MCE.COV_ID = CAR.COVERAGE_ID'  +                                              
         ' LEFT JOIN ' +                                                 
         ' CLM_ACTIVITY_PAYMENT CAP ' +                                                 
         ' ON ' +                                                 
         ' CAR.CLAIM_ID = CAP.CLAIM_ID AND ' +                                      
         ' CAR.RESERVE_ID = CAP.RESERVE_ID ' +                                               
         ' LEFT OUTER JOIN ' +                                                 
          CAST(@VEHICLE_TABLE AS VARCHAR(50)) + ' CIV ON ' +                                      
         ' CAP.CLAIM_ID = CIV.CLAIM_ID AND ' +                                                        
         ' CIV.' +  CAST(@POLICY_VEHICLE_COLUMN_NAME AS VARCHAR(50)) + ' = CAP.VEHICLE_ID ' +                                         
     ' LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ' +                                     
     ' ON MLV.LOOKUP_UNIQUE_ID = CAR.REINSURANCE_CARRIER ' +                                           
         ' WHERE ' +                                                 
         ' CAP.CLAIM_ID=' + CAST(@CLAIM_ID AS VARCHAR) + ' AND ' +                                                 
         ' CAP.ACTIVITY_ID =' + CAST(@ACTIVITY_ID AS VARCHAR) + ' AND ' +                                                         
         '  MC.COVERAGE_TYPE=' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + '''' +                                   
     --' ORDER BY CAR.RESERVE_ID '      
  --Done on 16 Feb for Itrack # 7031     
  'ORDER BY CAP.VEHICLE_ID,CAP.ACTUAL_RISK_TYPE , CAP.ACTUAL_RISK_ID  ,RANK'                            
                                 
 END                                                                          
 ELSE IF(@LOB_ID=@AUTOMOBILE OR @LOB_ID=@MOTORCYCLE)                                                                            
 BEGIN                                                  
  SET @COVERAGE_TABLE = ' POL_VEHICLE_COVERAGES '                                                  
  SET @VEHICLE_COLUMN_NAME = 'VEHICLE_ID '                                                  
  SET @VEHICLE_TABLE = 'CLM_INSURED_VEHICLE'                                                                         
  SET @POLICY_VEHICLE_COLUMN_NAME = 'POLICY_VEHICLE_ID'                                              
  SET @VEHICLE_TEXT = '(CAST(CIV.VIN AS VARCHAR(50))+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                       
                '+CAST(CIV.VEHICLE_YEAR AS VARCHAR(20))+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +              
                '+CAST(CIV.MAKE AS VARCHAR(50))+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                       
                '+CAST(CIV.MODEL AS VARCHAR(50))) AS VEHICLE, '                                                   
  --Select only policy level coverages                                                                             
  SET @TEMPSTR = ' SELECT DISTINCT ' +                                                 
         ' CAST(ISNULL(CAR.PRIMARY_EXCESS,0) AS INT) PRIMARY_EXCESS,CAR.ATTACHMENT_POINT,CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.CLAIM_ID,CAR.RESERVE_ID, ' +                   
         ' CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES, ' +                                                 
         ' CAP.PAYMENT_ID,  ISNULL(CAP.DRACCTS,0) as DrAccts,  ISNULL(CAP.CRACCTS,0) as CrAccts,CAP.PAYMENT_AMOUNT,CAP.PAYMENT_METHOD, ISNULL(CAP.CHECK_NUMBER,'''') AS CHECK_NUMBER, CAP.ACTION_ON_PAYMENT,'''' AS VEHICLE,CAR.COVERAGE_ID AS COV_ID,        

  MC.COV_CODE,' +                       
      CAST(@STATE_ID AS VARCHAR) + ' AS STATE_ID, ' +                       
         --' COV_DES as COV_DESC , ' +        
      
  ' CASE CAST(CAR.COVERAGE_ID AS VARCHAR) WHEN ''50001'' THEN ''Medical'' WHEN ''50002'' THEN ''Work Loss''' +                                
     ' WHEN ''50003'' THEN ''Death Benefits'' WHEN ''50004'' THEN ''Survivors Benefits'' ' +                     
     ' WHEN ''50005'' THEN ''BI'' WHEN ''50006'' THEN ''PD'' ' +                         
     ' WHEN ''50007'' THEN ''BI'' WHEN ''50008'' THEN ''PD'' ' +                         
     ' WHEN ''50009'' THEN ''BI'' WHEN ''50010'' THEN ''PD'' ' +      
  --Done for Itrack Issue 6625 on 24 Oct 09    
  ' WHEN ''50011'' THEN ''BI'' WHEN ''50012'' THEN ''PD'' ' +     
     ' WHEN ''50013'' THEN ''BI'' WHEN ''50014'' THEN ''PD'' ' +                                           
     ' WHEN ''50015'' THEN ''BI'' WHEN ''50016'' THEN ''PD'' ELSE MC.COV_DES ' +                         
     ' END AS COV_DESC, ' +          
     
 --Done on 16 Feb for Itrack # 7031    
  'CASE WHEN ISNULL(MC.RANK,0)= 0 THEN MCE.RANK ELSE MC.RANK END AS RANK ,'  +                       
         ' CASE MC.LIMIT_TYPE ' +                                                 
         ' WHEN 2 THEN ' +                                                 
         ' substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) + ' +                                                                               
         ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'''')) + ''/'' + ' +                                                                                
         ' substring(convert(varchar(30),convert(money,PVC.LIMIT_2),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_2),1),0)) + ' +                                                                              
         ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'''')) ' +                                                                                 
         ' ELSE substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) + ' +                                                 
         ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT1_AMOUNT_TEXT,'''')) ' +                                                 
         ' END AS LIMIT, ' +                                                 
   ' CASE MC.DEDUCTIBLE_TYPE ' +                                                                                 
         ' WHEN 2 THEN Substring(convert(varchar(30),convert(money,PVC.Deductible_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_1),1),0)) ' +                                         
 --        ' + CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) + ''/'' + ' +                                                  
         ' + ''/'' + ' +                                                  
         ' Substring(convert(varchar(30),convert(money,PVC.Deductible_2),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_2),1),0))   ' +       
 --        ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE2_AMOUNT_TEXT,'''')) ' +                                                                                 
         ' ELSE Substring(convert(varchar(30),convert(money,PVC.Deductible_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_1),1),0))  ' +                                 
 --        ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) ' +                        
        ' END AS DEDUCTIBLE, ' +    
  'CAP.ACTUAL_RISK_ID AS ACTUAL_RISK_ID, CAP.ACTUAL_RISK_TYPE AS ACTUAL_RISK_TYPE,CAP.VEHICLE_ID' +                                               
         ' FROM ' +                                
         ' CLM_CLAIM_INFO CCI ' +                          
         ' LEFT JOIN ' +                                                                         
         ' CLM_ACTIVITY_RESERVE CAR  ON CCI.CLAIM_ID = CAR.CLAIM_ID ' +                                                 
         ' LEFT JOIN ' +                                         
           CAST(@COVERAGE_TABLE AS VARCHAR(50)) + ' PVC ON CCI.POLICY_ID = PVC.POLICY_ID AND ' +                                                 
         ' CCI.POLICY_VERSION_ID = PVC.POLICY_VERSION_ID AND ' +                                                                      
         ' CCI.CUSTOMER_ID = PVC.CUSTOMER_ID AND CAR.COVERAGE_ID=PVC.COVERAGE_CODE_ID ' +                                     
         ' LEFT JOIN ' +                                                  
         ' MNT_COVERAGE MC ON MC.COV_ID = CAR.COVERAGE_ID ' +      
   --Done for Itrack Issue 7031 on 16 Feb 2010    
   'LEFT OUTER JOIN MNT_COVERAGE_EXTRA MCE ON MCE.COV_ID = CAR.COVERAGE_ID' +                                               
         ' LEFT JOIN ' +                                                 
         ' CLM_ACTIVITY_PAYMENT CAP ' +                                           
  ' ON ' +                            
         ' CAR.CLAIM_ID = CAP.CLAIM_ID AND ' +                                                       
         ' CAR.RESERVE_ID = CAP.RESERVE_ID ' +                                     
     ' LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ' +                                     
     ' ON MLV.LOOKUP_UNIQUE_ID = CAR.REINSURANCE_CARRIER ' +                                     
         ' WHERE ' +                                                 
         ' CAP.CLAIM_ID=' + CAST(@CLAIM_ID AS VARCHAR) + '  AND ' +                                                 
         ' CAP.ACTIVITY_ID =' +   CAST(@ACTIVITY_ID AS VARCHAR) +                                               
         --' MC.COVERAGE_TYPE=' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + ''''                                             
     --' ( MC.COVERAGE_TYPE=' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + '''' + ' OR MC.COVERAGE_TYPE IS NULL)' +    
  ' AND  ( CASE WHEN CAR.COVERAGE_ID IN (50017,50018,50019,50020,50021,50022,50023,50024,50025,50026,50027,50028 ) THEN ''RL''' +          
   ' ELSE MC.COVERAGE_TYPE END =' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + '''' +                                                                                   
   ' OR (CAR.COVERAGE_ID NOT IN (50017,50018,50019,50020,50021,50022,50023,50024,50025,50026,50027,50028 ) ' +          
   ' AND MC.COVERAGE_TYPE  IS NULL))' +                                   
     --' ORDER BY CAR.RESERVE_ID '      
 --Done on 16 Feb for Itrack # 7031     
 ' ORDER BY CAP.VEHICLE_ID,CAP.ACTUAL_RISK_TYPE , CAP.ACTUAL_RISK_ID  ,CAR.RESERVE_ID,RANK'                                   
      
 END                                                                          
 ELSE IF(@LOB_ID=@WATERCRART)                
 BEGIN                                                                          
   SET @COVERAGE_TABLE = 'POL_WATERCRAFT_COVERAGE_INFO'                                                                            
--   SET @VEHICLE_COLUMN_NAME = 'BOAT_ID '                             
   SET @VEHICLE_COLUMN_NAME = 'CAR.VEHICLE_ID '                                                        
   SET @VEHICLE_TABLE = 'CLM_INSURED_BOAT'                                              
   SET @POLICY_VEHICLE_COLUMN_NAME = 'POLICY_BOAT_ID'                                                  
   SET @VEHICLE_TEXT = '(CAST(CIV.YEAR AS VARCHAR)+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                                             
                 '+CAST(CIV.MAKE AS VARCHAR)+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                                        
                 '+CAST(CIV.MODEL AS VARCHAR)+' + '''' + CAST(@DASH_OPERATOR AS VARCHAR) +   '''' +                               
                 '+CAST(CIV.SERIAL_NUMBER AS VARCHAR)) AS VEHICLE, '                                                   
   --Since we do not have any policy level coverages for watercraft, lets display blank table                  
   SET @TEMPSTR = ' SELECT NULL '                      
 END                  
 ELSE IF (@LOB_ID=@UMBRELLA)                
BEGIN                
   SET @TEMPSTR = 'SELECT CAR.PRIMARY_EXCESS,CAR.ATTACHMENT_POINT,CAR.OUTSTANDING, CAR.RI_RESERVE, CAR.RESERVE_ID,' +                 
      ' CAR.REINSURANCE_CARRIER,CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES, CAP.PAYMENT_ID,  ISNULL(CAP.DRACCTS,0) as DrAccts,  ISNULL(CAP.CRACCTS,0) as CrAccts,' +        
      ' MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,MC.COV_DES AS COV_DESC,CAR.COVERAGE_ID AS COV_ID, ' +                 
      ' CAR.POLICY_LIMITS,CAR.RETENTION_LIMITS,CAP.PAYMENT_AMOUNT, CAP.PAYMENT_AMOUNT,CAP.PAYMENT_METHOD, ISNULL(CAP.CHECK_NUMBER,'''') AS CHECK_NUMBER,CAP.ACTION_ON_PAYMENT ' +                 
      ' FROM CLM_ACTIVITY_RESERVE CAR LEFT OUTER JOIN CLM_CLAIM_INFO CCI ON CAR.CLAIM_ID=CCI.CLAIM_ID ' +                 
      ' LEFT OUTER JOIN MNT_COVERAGE MC ON CAR.COVERAGE_ID = MC.COV_ID  LEFT OUTER JOIN ' +                 
      ' CLM_ACTIVITY_PAYMENT CAP ON CAP.RESERVE_ID = CAR.RESERVE_ID AND CAP.CLAIM_ID = CAR.CLAIM_ID ' +                 
      ' LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON MLV.LOOKUP_UNIQUE_ID =  CAR.REINSURANCE_CARRIER ' +                 
      ' WHERE CCI.IS_ACTIVE=''Y'' AND CAR.IS_ACTIVE=''Y'' AND MC.IS_ACTIVE=''Y'' AND MLV.IS_ACTIVE=''Y''' +                 
      ' AND CAR.CLAIM_ID=' + CAST(@CLAIM_ID AS VARCHAR) + ' AND CAP.ACTIVITY_ID=' + CAST(@ACTIVITY_ID AS VARCHAR) +    
   -- ' ORDER BY CAR.RESERVE_ID '                
   --Done on 16 Feb for Itrack # 7031     
 ' ORDER BY CAP.VEHICLE_ID,CAP.ACTUAL_RISK_TYPE , CAP.ACTUAL_RISK_ID  ,RANK'      
 END                    
                 
 --PRINT @TEMPSTR            
 EXEC(@TEMPSTR)                            
                                                 
                   
   --Done by Ankit for #Itrack 7642                                                                                                           
   --SET @VEHICLE_COLUMN_NAME = 'CAR.VEHICLE_ID '                                                  
                   
                 
 --select vehicle specific coverages                                                     
 SET @TEMPSTR = ' SELECT CAST(ISNULL(CAR.PRIMARY_EXCESS,0) AS INT) PRIMARY_EXCESS,CAR.ATTACHMENT_POINT,CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.RESERVE_ID, ' +                                                                   
         ' CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES, ' +                                                     
         ' CAP.PAYMENT_ID,  ISNULL(CAP.DRACCTS,0) as DrAccts,  ISNULL(CAP.CRACCTS,0) as CrAccts,CAP.PAYMENT_AMOUNT,CAP.PAYMENT_METHOD, ISNULL(CAP.CHECK_NUMBER,'''') AS CHECK_NUMBER,CAP.ACTION_ON_PAYMENT, ' +  CAST(@STATE_ID AS VARCHAR) + ' AS STATE_ID, '

  
    
    
    
    
    
    
    
    
    
    
    
      
        
        
                                                
 IF(@LOB_ID=@WATERCRART)          
  BEGIN        
    SET @TEMPSTR = @TEMPSTR + CAST(@VEHICLE_COLUMN_NAME AS VARCHAR(50)) + ' VEHICLE_ID, ' + CAST(@VEHICLE_TEXT AS VARCHAR(500))         
  END        
 ELSE        
  BEGIN       
  --ANKIT #ITRACK 7642   
    -- SET @TEMPSTR = @TEMPSTR + ' PVC.' + CAST(@VEHICLE_COLUMN_NAME AS VARCHAR(50)) + ' VEHICLE_ID, ' + CAST(@VEHICLE_TEXT AS VARCHAR(500))        
  SET @TEMPSTR = @TEMPSTR + ' CAR.' + CAST(@VEHICLE_COLUMN_NAME AS VARCHAR(50)) + ' VEHICLE_ID, ' + CAST(@VEHICLE_TEXT AS VARCHAR(500))        
  END        
        
                  
--  SET @TEMPSTR = @TEMPSTR + ' MC.COV_ID, MC.COV_CODE, COV_DES as COV_DESC , ' +                         
  SET @TEMPSTR = @TEMPSTR + ' CAR.COVERAGE_ID COV_ID, MC.COV_CODE, '        
-- COV_DES as COV_DESC , ' +                                                                                    
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
   --SET @TEMPSTR= @TEMPSTR + ' MC.COV_DES as COV_DESC , '                   
  end        
    --Done on 16 Feb for Itrack # 7031    
  SET @TEMPSTR = @TEMPSTR +  'CASE WHEN ISNULL(MC.RANK,0)= 0 THEN MCE.RANK ELSE MC.RANK END AS RANK ,'    
    
   SET @TEMPSTR= @TEMPSTR + ' CASE MC.LIMIT_TYPE ' +                                                 
         ' WHEN 2 THEN ' +                                                 
         ' Substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) + ' +                                                 
         ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'''')) + ''/'' +  ' +                                                                   
         ' Substring(convert(varchar(30),convert(money,PVC.LIMIT_2),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_2),1),0)) + ' +                         
         ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'''')) ' +                                                                                 
         ' ELSE Substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) +  ' +                                                 
       ' CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT1_AMOUNT_TEXT,'''')) ' +                                                                                 
         ' END AS LIMIT, ' +                                                                                 
         ' CASE MC.DEDUCTIBLE_TYPE ' +                        
         ' WHEN 2 THEN Substring(convert(varchar(30),convert(money,PVC.Deductible_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_1),1),0)) ' +                                                                               
 --        ' + CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) + ''/'' + ' +                                                                                 
         ' + ''/'' + ' +                                                 
         ' Substring(convert(varchar(30),convert(money,PVC.Deductible_2),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_2),1),0))    ' +                
 --    ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE2_AMOUNT_TEXT,'''')) ' +                                                                                 
         ' ELSE Substring(convert(varchar(30),convert(money,PVC.Deductible_1),1),0,charindex(''.'',convert(varchar(30),convert(money,PVC.Deductible_1),1),0))  ' +                                                 
 --        ' CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'''')) ' +                                                                                 
         ' END AS DEDUCTIBLE, ' +    
   'CAP.ACTUAL_RISK_ID AS ACTUAL_RISK_ID, CAP.ACTUAL_RISK_TYPE AS ACTUAL_RISK_TYPE'                                             
   IF(@LOB_ID=@HOMEOWNER or @LOB_ID=@RENTAL)                                               
          SET @TEMPSTR = @TEMPSTR + ', ISNULL(CAST(PVC.DEDUCTIBLE AS VARCHAR(10)),'''') + ISNULL(PVC.DEDUCTIBLE_TEXT,'''') AS DEDUCTIBLE2 '                            
         SET @TEMPSTR = @TEMPSTR + ' FROM ' +                                                 
         ' CLM_CLAIM_INFO CCI ' +                                                                         
         ' LEFT JOIN ' +                                                 
         ' CLM_ACTIVITY_RESERVE CAR  ON CCI.CLAIM_ID = CAR.CLAIM_ID ' +                                                 
         ' LEFT JOIN ' +                                                 
           CAST(@COVERAGE_TABLE AS VARCHAR(50)) + ' PVC ON CCI.POLICY_ID = PVC.POLICY_ID AND ' +                                             
         ' CCI.POLICY_VERSION_ID = PVC.POLICY_VERSION_ID AND ' +                                                 
         --' CCI.CUSTOMER_ID = PVC.CUSTOMER_ID AND CAR.COVERAGE_ID=PVC.COVERAGE_CODE_ID AND '
         --ADD BY ANKIT FOR ITRACK #7642          
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
			 ELSE CAR.COVERAGE_ID END = PVC.COVERAGE_CODE_ID AND ' 
         
               
  IF(@LOB_ID=@WATERCRART)                             
   BEGIN        
           SET @TEMPSTR = @TEMPSTR + ' CAR.VEHICLE_ID=PVC.BOAT_ID'        
   END        
  ELSE        
   BEGIN      
             SET @TEMPSTR = @TEMPSTR + ' CAR.VEHICLE_ID=PVC.' + CAST(@VEHICLE_COLUMN_NAME AS VARCHAR(50))        
   END        
        
        
         SET @TEMPSTR = @TEMPSTR + ' LEFT JOIN  ' +                                                                   
         ' MNT_COVERAGE MC ON MC.COV_ID = CAR.COVERAGE_ID ' +        
   --Done for Itrack Issue 7031 on 16 Feb 2010     
  'LEFT OUTER JOIN MNT_COVERAGE_EXTRA MCE ON MCE.COV_ID = CAR.COVERAGE_ID' +                                           
       ' LEFT JOIN ' +                                                 
         ' CLM_ACTIVITY_PAYMENT CAP ' +                 
         ' ON ' +                                                 
         ' CAR.CLAIM_ID = CAP.CLAIM_ID AND ' +                                                
         ' CAR.RESERVE_ID = CAP.RESERVE_ID ' +                                               
         ' LEFT OUTER JOIN ' +                                                 
          CAST(@VEHICLE_TABLE AS VARCHAR(50)) + ' CIV ON ' +                                                       
         ' CAP.CLAIM_ID = CIV.CLAIM_ID AND ' +                                                        
         ' CIV.' +  CAST(@POLICY_VEHICLE_COLUMN_NAME AS VARCHAR(50)) + ' = CAP.VEHICLE_ID ' +                                         
     ' LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ' +                                  
     ' ON MLV.LOOKUP_UNIQUE_ID = CAR.REINSURANCE_CARRIER ' +                                           
         ' WHERE ' +                                                 
         ' CAP.CLAIM_ID=' + CAST(@CLAIM_ID AS VARCHAR) + ' AND ' +                                                 
         ' CAP.ACTIVITY_ID =' + CAST(@ACTIVITY_ID AS VARCHAR)                                 
     IF(@LOB_ID<>@HOMEOWNER AND @LOB_ID<>@RENTAL)                            
          SET @TEMPSTR = @TEMPSTR + '  AND CIV.CLAIM_ID = ' + CAST(@CLAIM_ID AS VARCHAR)                                                   
--      SET @TEMPSTR = @TEMPSTR + ' AND MC.COVERAGE_TYPE<>' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + '''' +                                   
--             ' ORDER BY CAR.RESERVE_ID '          
        
    
    
  if(@LOB_ID<>@WATERCRART)                                
   begin        
  --Ankit ITrack #7642    
      --SET @TEMPSTR = @TEMPSTR + ' AND MC.COVERAGE_TYPE<>' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + ''''     
 SET @TEMPSTR = @TEMPSTR + ' AND CASE WHEN CAR.COVERAGE_ID IN (50017,50018,50019,50020,50021,50022,50023,50024,50025,50026,50027,50028 ) THEN  ''RL''' +    
  + ' ELSE MC.COVERAGE_TYPE END<>' + '''' + CAST(@POLICY_COVERAGES AS VARCHAR)  + ''''          
   end        
  print @POLICY_COVERAGES    
 if(@LOB_ID = @WATERCRART)                                                
 begin                              
  SET @TEMPSTR = @TEMPSTR + ' AND CAP.ACTUAL_RISK_TYPE  <> ''EQUIP'''    
 end     
    
          SET @TEMPSTR = @TEMPSTR +      
 --' ORDER BY CAR.RESERVE_ID '                                         
 --Done on 16 Feb for Itrack # 7031     
 ' ORDER BY CAP.VEHICLE_ID,CAP.ACTUAL_RISK_TYPE , CAP.ACTUAL_RISK_ID  ,RANK'      
                      
 --PRINT @TEMPSTR                
 EXEC(@TEMPSTR)                                                
                                                   
      END        
    
EXEC Proc_GetOldWaterEquipCovgForClaimsPayments @CLAIM_ID,@ACTIVITY_ID        
    
 --Get Payees for the current claim and activity                                                      
SELECT ISNULL(PAYEE_PARTIES_ID,'') AS PAYEE_PARTIES_ID FROM CLM_ACTIVITY                                       
 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID            
                    
END    
    
--go    
--exec Proc_GetCLM_ACTIVITY_PAYMENT 3271,2    
--rollback tran

GO

