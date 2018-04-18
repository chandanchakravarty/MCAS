IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForMotorcycle_Policy_VehicleCovgComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForMotorcycle_Policy_VehicleCovgComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

     
/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--    
         
       
           
            
              
Proc Name        :  dbo.Proc_GetRatingInformationForMotorcycle_Policy_VehicleCovgComponent                    
Created by       :  Praveen Singh                 
Date             :  09-01-2006                  
Purpose          :  To Activate/Deactivate the record of Policy driver                    
Revison History :                        
Used In          :  Wolverine                        
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
        
          
Modified by  : Ashwani                 
Date   : 29 Mar. 2006                
Purpose  : Marked As <001>                 
-------------------------------------                        
Date     Review By          Comments                        
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
        
          
            
*/              
  --  drop proc  Proc_GetRatingInformationForMotorcycle_Policy_VehicleCovgComponent         
CREATE  PROC dbo.Proc_GetRatingInformationForMotorcycle_Policy_VehicleCovgComponent                  
(                  
@CUSTOMERID     INT,                  
@POLID          INT,                  
@POLVERSIONID    INT,                  
@VEHICLEID       INT                   
)                  
AS                  
                  
BEGIN                  
 SET QUOTED_IDENTIFIER OFF                  
                 
DECLARE @ReturnValue    Varchar(8000)                  
DECLARE @STATEID    Varchar(5)                  
DECLARE @LOBID    Varchar(3)                  
                   
                  
SET @ReturnValue = ''                  
                  
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
        
          
-- States  START                  
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
        
          
                 
 SELECT                   
  @STATEID=STATE_ID ,@LOBID= POLICY_LOB                  
  FROM                   
  POL_CUSTOMER_POLICY_LIST  WITH (NOLOCK)              
                
 WHERE                   
  CUSTOMER_ID =@CUSTOMERID and POLICY_ID =@POLID and POLICY_VERSION_ID=@POLVERSIONID                      
                        
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
        
          
                
--COVERAGES FOR DIFFERENT STATES AS THE CODES MAY DIFFER FOR DIFFERENT STATES  START                        
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
        
          
          
 IF @STATEID ='22'  -- MICHIGAN         
  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   
    
      
        
          
                 
  -- BEGINNING  OF MICHIGAN   START                        
  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
  
    
      
        
          
              
                         
  BEGIN                        
   SELECT    @ReturnValue =                         
       (CASE         
 when COV_CODE = 'MEDPM1' then       
  case POL_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT          
         when 'Reject' then  ''       
         WHEN'1st Party Medical-$300' THEN  (+'<MEDICALTYPE>FULL</MEDICALTYPE>' +'<MedPmLimit>'+
CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR) +'</MedPmLimit>'+'<MEDICALDEDUCTIBLE>'
+ CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1,0) AS VARCHAR)
  
    
+'</MEDICALDEDUCTIBLE>'+ '<Type>'+CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR)+'</Type>'+'<MedPm></MedPm>')      
   WHEN '1st Party Medical-Full' THEN (+'<MEDICALTYPE>FULL</MEDICALTYPE>' +'<MedPmLimit>'+
CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR) +'</MedPmLimit>'+'<MEDICALDEDUCTIBLE>'+ 
CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1,0) AS VARCHAR)+'</MEDICALDEDUCTIBLE>'+ '<Type>'+
CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR)+'</Type>'+'<MedPm></MedPm>')      
   WHEN '$1000 Medical' THEN (+'<MEDICALTYPE>EXCESS</MEDICALTYPE>'  +'<MedPmLimit>'+
CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR) +'</MedPmLimit>'+'<MEDICALDEDUCTIBLE>'+
 CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1,0) AS VARCHAR)+'</MEDICALDEDUCTIBLE>'+ '<Type>'+
CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR)+'</Type>'+'<MedPm></MedPm>')      
   WHEN '1st Party Medical-Excess' THEN (+'<MEDICALTYPE>EXCESS</MEDICALTYPE>' +'<MedPmLimit>'+
CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR) +'</MedPmLimit>'+'<MEDICALDEDUCTIBLE>'+
 CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1,0) AS VARCHAR)+'
  
    
</MEDICALDEDUCTIBLE>'+ '<Type>'+CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR)+'</Type>'+
'<MedPm></MedPm>')      
 else '' end         
 when COV_CODE = 'MEDPM2' then (+ case isnull(POL_VEHICLE_COVERAGES.LIMIT_1,-1)      
      when -1 then +''          
      else +'<MEDICALTYPE>EXCESS</MEDICALTYPE>'+'<MedPmLimit>'+CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR) +'</MedPmLimit>'+'<MEDICALDEDUCTIBLE>'+ CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1,0) AS VARCHAR)+
'</MEDICALDEDUCTIBLE>'+'<Type>'+ '$1000 Medical'+'</Type>'+'<MedPm></MedPm>'       
      end       
    )                         
      WHEN COV_CODE  =  'BISPL'  THEN '<BI>'  + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)+'/'+
CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0) AS VARCHAR)+ '</BI><BILIMIT1>'                   
          +CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)+'</BILIMIT1><BILIMIT2>'+
CAST(CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0) as int)/1000 AS VARCHAR)+'</BILIMIT2>'                        
      WHEN COV_CODE  = 'PD'  THEN '<PD>'    + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</PD>'                     
      WHEN COV_CODE  = 'RLCSL'  THEN '<CSL>'   + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + 
'</CSL>'                        
      WHEN COV_CODE  = 'PUMSP' THEN '<UMSPLIT>' + case  CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, '0') AS VARCHAR)  +'/' + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_2, '0') AS VARCHAR)              
   WHEN  '0/0' THEN '0' ELSE CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'/' +
 CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0) AS VARCHAR)              
               END               
               + '</UMSPLIT>'                        
         +'<UMSPLITLIMIT1>'+ CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'</UMSPLITLIMIT1>'                        
         +'<UMSPLITLIMIT2>'+ CAST(CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0) as int)/1000 AS VARCHAR)  +
'</UMSPLITLIMIT2>'                        
     WHEN COV_CODE  = 'PUNCS' THEN '<UMCSL>' + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +
'</UMCSL>' --<PDLIMIT></PDLIMIT><PDDEDUCTIBLE></PDDEDUCTIBLE>'                        
     WHEN COV_CODE  = 'UMPD'  THEN '<PDLimit>'+CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +
'</PDLimit>'                        
   +'<PDDEDUCTIBLE>' +CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)  +'</PDDEDUCTIBLE>'                        
     WHEN COV_CODE  = 'ROAD'  THEN '<ROADSERVICE>'+CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +
'</ROADSERVICE>'                        
     WHEN COV_CODE  = 'OTC'  THEN '<ComprehensiveDeductible>'+CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)  +'</ComprehensiveDeductible>'                        
     WHEN COV_CODE  = 'COLL'  THEN '<CollisionDeductible>'+CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)  +'</CollisionDeductible>'                        
     /*            
WHEN COV_CODE  = 'MEDPM' THEN '<MedPmLimit>'+CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'</MedPmLimit>'                     
            +'<Type>'+CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR)+'</Type>'                        
   +'<MedPm></MedPm>'                                
            
*/            
       
     WHEN COV_CODE  = 'EBM15'  THEN '<Helmet>'    + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + 
'</Helmet>'                       
     WHEN COV_CODE  = 'EBM49'  THEN '<McycleTrailer>'    + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</McycleTrailer>'                       
     WHEN COV_CODE  = 'PDC14'  THEN '<AddlPD>'    + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + 
'</AddlPD>'                      
        + '<INCREASEDVALUE>0</INCREASEDVALUE>'                       
  +'<MedPmType></MedPmType>'                    
       ELSE ''                        
                     END ) + @ReturnValue                                  
   FROM         MNT_COVERAGE WITH (NOLOCK)                        
     LEFT OUTER JOIN  POL_VEHICLE_COVERAGES WITH (NOLOCK) ON POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                        
   AND (POL_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMERID)                         
     AND (POL_VEHICLE_COVERAGES.POLICY_ID  = @POLID)                         
     AND (POL_VEHICLE_COVERAGES.POLICY_VERSION_ID  = @POLVERSIONID)                         
     AND   (POL_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLEID)                        
   WHERE                             
    MNT_COVERAGE.STATE_ID  = @STATEID  AND                         
    MNT_COVERAGE.LOB_ID = @LOBID                        
  END                        
  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
  
    
      
        
          
           
                         
  --MICHIGAN   END                        
  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
  
    
      
        
          
                 
                        
 ELSE IF @STATEID ='14'   --INDIANA                        
  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
  
    
      
        
          
            
                
 /*  Motorcycle - INDIANA ... QQ and Procs                
  Praveen / Ashwani pl send a nodes                
 <UIMSPLIT>                 
 <UIMSPLITLIMIT1>                 
 <UIMSPLITLIMIT2>                 
 <UIMCSL> */                   
                         
  ---INDIANA   START                        
  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
  
    
      
        
          
           
                        
  BEGIN                        
   SELECT    @ReturnValue =                         
       (CASE                           
 WHEN COV_CODE  = 'OTC'  THEN '<ComprehensiveDeductible>'+CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)  +'</ComprehensiveDeductible>'                        
 WHEN COV_CODE  = 'COLL'  THEN '<CollisionDeductible>'+CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)  +'</CollisionDeductible>'                        
 WHEN COV_CODE  = 'ROAD'  THEN '<ROADSERVICE>'+CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +
'</ROADSERVICE>'                        
 WHEN COV_CODE  = 'EBM15'  THEN '<Helmet>'    + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + 
'</Helmet>'                       
 WHEN COV_CODE  = 'EBM49'  THEN '<McycleTrailer>'    + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</McycleTrailer>'                       
 WHEN COV_CODE  = 'PDC14'  THEN '<AddlPD>'    + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +
 '</AddlPD>'                       
 WHEN COV_CODE  = 'EE'  THEN '<INCREASEDVALUE>'    + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</INCREASEDVALUE>'                        
 WHEN COV_CODE  = 'BISPL'  THEN '<BI>'  + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)+'/'+
CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0) AS VARCHAR)+ '</BI><BILIMIT1>' +CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)+'</BILIMIT1>' + '<BILIMIT2>'+ 
rtrim(ltrim(CAST( CAST((isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0)) AS int)/1000 as VARCHAR)))+'</BILIMIT2>'                        
 WHEN COV_CODE  = 'PD'  THEN '<PD>'+ CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</PD>'                   
 WHEN COV_CODE  = 'MEDPM'  THEN '<MedPm>'+CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'</MedPm>'                     
 +'<MedPmType>'+CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR)+'</MedPmType>'                        
 +'<MedPmLimit></MedPmLimit>'+'<MEDICALTYPE>'+CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR)+'</MEDICALTYPE>'                      
 -- +'<Type></Type>'  -- CHANGE LATER      ..we add it later in the proc for indiana                  
 WHEN COV_CODE  = 'UMPD'  THEN '<PDLimit>'+CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'</PDLimit>'                    
 +'<PDDEDUCTIBLE>' +CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)  +'</PDDEDUCTIBLE>'                             
                
 WHEN COV_CODE  = 'UNDSP'  THEN case isnull(POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0)                 
         when 0  then ''                  
         else  '<isUnderInsuredMotorists>Y</isUnderInsuredMotorists>'                
      --<001 start>                
      +'<UIMSPLIT>' + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)+'/'+CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0) AS VARCHAR) + '</UIMSPLIT>'                
      + '<UIMSPLITLIMIT1>'  + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)+ '</UIMSPLITLIMIT1>'                
      + '<UIMSPLITLIMIT2>'+ rtrim(ltrim(CAST( CAST((isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0)) AS int)/1000 as VARCHAR)))+'</UIMSPLITLIMIT2>'                
      --<001 end>                
       end -- only in case of  indiana                    
 WHEN COV_CODE  = 'UNCSL'  THEN case isnull(POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0)                 
         when 0  then ''                 
         else  '<isUnderInsuredMotorists>Y</isUnderInsuredMotorists>'                 
      --<002 start>                
      + '<UIMCSL>'+ CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)+ '</UIMCSL>'                         
      --<002 end>                
           end -- only in case of  indiana                    
                 
 WHEN COV_CODE  = 'RLCSL'  THEN '<CSL>'   + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</CSL>'                        
 WHEN COV_CODE  = 'PUMSP' THEN '<UMSPLIT>' + case  CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, '0') AS VARCHAR)  +'/' + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_2, '0') AS VARCHAR)              
                                                  WHEN  '0/0' THEN '0'              
                                                  ELSE CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'/' + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0) AS VARCHAR)              
               END               
               + '</UMSPLIT>'                          
 +'<UMSPLITLIMIT1>'+ CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'</UMSPLITLIMIT1>'                        
 +'<UMSPLITLIMIT2>'+ CAST(CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0) as int)/1000 AS VARCHAR)  +'</UMSPLITLIMIT2>'                        
 WHEN COV_CODE  = 'PUNCS' THEN '<UMCSL>' + CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'</UMCSL>' --<PDLIMIT></PDLIMIT><PDDEDUCTIBLE></PDDEDUCTIBLE>'                        
                  
                
                
 ELSE ''                        
     END ) + @ReturnValue                                 
   FROM                                
     MNT_COVERAGE  WITH (NOLOCK) left OUTER JOIN  POL_VEHICLE_COVERAGES  WITH (NOLOCK) ON POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                        
      AND (POL_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMERID)                        
    AND (POL_VEHICLE_COVERAGES.POLICY_ID = @POLID)                         
    AND (POL_VEHICLE_COVERAGES.POLICY_VERSION_ID = @POLVERSIONID)                         
    AND (POL_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLEID)                        
   WHERE                             
    MNT_COVERAGE.STATE_ID =@STATEID and MNT_COVERAGE.LOB_ID=@LOBID                        
                           
                          
              
  END         
                        
  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
  
    
     
        
           
  ----------------------------------------------------INDIANA   END                
  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
  
    
      
       
           
           
  declare @RetVal  varchar(8000)                      
  SELECT @RetVal =                     
  CASE                      
  when charindex('<isUnderInsuredMotorists>',@ReturnValue) > 0  then @ReturnValue                     
  when charindex('<isUnderInsuredMotorists>',@ReturnValue) = 0                 
 then @ReturnValue + '<isUnderInsuredMotorists>N</isUnderInsuredMotorists>'                
     + '<UIMSPLIT> </UIMSPLIT>'            
            + '<UIMSPLITLIMIT1> </UIMSPLITLIMIT1>'                
     + '<UIMSPLITLIMIT2> </UIMSPLITLIMIT2>'                
     + '<UIMCSL> </UIMCSL>'                            
  else @ReturnValue                    
  end --As Coverages 0               
              
 select @RetVal=                  
  case                  
  when charindex('<UMSPLIT>',@ReturnValue) > 0  then @ReturnValue                         
  when charindex('<UMSPLIT>',@ReturnValue) = 0                     
  then @RetVal + '<UMSPLIT></UMSPLIT>'                                  
  else @RetVal                                  
 end               
              
              
               
                  /* Add 'type' node in case of INDIANA state/                  
      1. If BI coverage is present then 'BI ONLY'                  
      2. If BI and UMPD both do not exists then '' or 'NO COVERAGE'                     
      3. IF UMPD exists then check the deductible and 'BI/PD with 0 deductible' or 'BI/PD with 300 deductible' */                  
  IF @STATEID ='14'   --INDIANA                     
    BEGIN                  
     IF EXISTS                  
  (SELECT COV_ID                 
    FROM                                
       MNT_COVERAGE WITH (NOLOCK)  inner JOIN  POL_VEHICLE_COVERAGES WITH (NOLOCK) ON POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                        
        AND (POL_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMERID)                        
      AND (POL_VEHICLE_COVERAGES.POLICY_ID = @POLID)                         
      AND (POL_VEHICLE_COVERAGES.POLICY_VERSION_ID = @POLVERSIONID)                         
      AND (POL_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLEID)                        
     WHERE                             
      MNT_COVERAGE.STATE_ID =@STATEID and MNT_COVERAGE.LOB_ID=@LOBID and MNT_COVERAGE.COV_CODE in ('UMPD','E17'))                  
    BEGIN   
 SET @RETVAL = @RETVAL + '<TYPE>BIPD</TYPE>'  
    END  
  ELSE IF EXISTS       
  ( SELECT COV_ID                 
    FROM                                
       MNT_COVERAGE  WITH (NOLOCK) inner JOIN  POL_VEHICLE_COVERAGES WITH (NOLOCK) ON POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                        
        AND (POL_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMERID)                        
      AND (POL_VEHICLE_COVERAGES.POLICY_ID = @POLID)                         
      AND (POL_VEHICLE_COVERAGES.POLICY_VERSION_ID = @POLVERSIONID)                         
      AND (POL_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLEID)                        
     WHERE                             
      MNT_COVERAGE.STATE_ID =@STATEID and MNT_COVERAGE.LOB_ID=@LOBID and  
     MNT_COVERAGE.COV_CODE in ('PUMSP','PUNCS'))                  
   BEGIN                 
    SET @RETVAL = @RETVAL + '<TYPE>BI ONLY</TYPE>'   
   END  
                        
  ELSE                   
  BEGIN                  
   SET @RetVal = @RetVal + '<Type></Type>'                  
  END                  
     
 END                   
                   
                   
                  
  SELECT @RetVal as Coverages                    
-- SELECT @ReturnValue As Coverages                        
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                    
END                    
                    
  
GO

