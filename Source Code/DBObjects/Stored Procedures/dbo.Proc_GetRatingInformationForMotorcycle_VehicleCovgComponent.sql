IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForMotorcycle_VehicleCovgComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForMotorcycle_VehicleCovgComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

/*--------------------------------------                            
Proc Name        :  dbo.Proc_GetRatingInformationForMotorcycle_VehicleCovgComponent                        
Created by       :  Shrikant Bhatt                            
Date             :  15-12-2005                      
Purpose          :  To get InputXML for Vehicle Coverages..                        
Revison History  :                          
Used In          :  Wolverine                            
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Modified by   : Ashwani               
Date     : 29 Mar. 2006              
Purpose   : Marked As <001> & <002>                    
-------------------------------------                            
Date     Review By          Comments                            
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

  
    
      
        
*/      
 -- drop proc Proc_GetRatingInformationForMotorcycle_VehicleCovgComponent  
CREATE   PROC dbo.Proc_GetRatingInformationForMotorcycle_VehicleCovgComponent 
(                      
@CUSTOMERID      INT,        
@APPID      INT,        
@APPVERSIONID    INT,        
@VEHICLEID      INT        
)                      
AS                      
                      
BEGIN                      
 SET QUOTED_IDENTIFIER OFF                      
        
DECLARE @ReturnValue   varchar(8000)                      
DECLARE @STATEID   varchar(5)                      
DECLARE @LOBID   varchar(3)                      
                       
                      
SET @ReturnValue = ''                      
                      
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

  
    
      
        
-- States  START                      
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

  
    
      
        
                     
 SELECT                       
  @STATEID=STATE_ID ,@LOBID= APP_LOB                      
  FROM                       
  APP_LIST WITH (NOLOCK)                       
 WHERE                       
  CUSTOMER_ID =@CUSTOMERID and APP_ID =@APPID and APP_VERSION_ID=@APPVERSIONID                      
                      
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

  
    
      
        
--COVERAGES FOR DIFFERENT STATES AS THE CODES MAY DIFFER FOR DIFFERENT STATES  START                      
--------------------------------------------------------------------------------------------------------------------------        
--- BEGINNING  OF MICHIGAN   START                      
----------------------------------------------------------------------------------------------------------------------------         
 IF @STATEID ='22'  -- MICHIGAN                      
------------------------------------------------------------------------------------------------------------------------------         
                       
  BEGIN                      
   SELECT    @ReturnValue =                       
       (CASE      
 when COV_CODE = 'MEDPM1' then     
  case APP_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT        
         when 'Reject' then  ''     
         WHEN'1st Party Medical-$300' THEN  (+'<MEDICALTYPE>FULL</MEDICALTYPE>' +'<MedPmLimit>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR) +'</MedPmLimit>'+'<MEDICALDEDUCTIBLE>'+ CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1,0) AS VARCHAR)

  
+'</MEDICALDEDUCTIBLE>'+ '<Type>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR)+'</Type>'+'<MedPm></MedPm>')    
   WHEN '1st Party Medical-Full' THEN (+'<MEDICALTYPE>FULL</MEDICALTYPE>' +'<MedPmLimit>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR) +'</MedPmLimit>'+'<MEDICALDEDUCTIBLE>'+ CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1,0) AS VARCHAR)+'</MEDICALDEDUCTIBLE>'+ '<Type>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR)+'</Type>'+'<MedPm></MedPm>')    
   WHEN '$1000 Medical' THEN (+'<MEDICALTYPE>EXCESS</MEDICALTYPE>'  +'<MedPmLimit>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR) +'</MedPmLimit>'+'<MEDICALDEDUCTIBLE>'+ CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1,0) AS VARCHAR)+'</MEDICALDEDUCTIBLE>'+ '<Type>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR)+'</Type>'+'<MedPm></MedPm>')    
   WHEN '1st Party Medical-Excess' THEN (+'<MEDICALTYPE>EXCESS</MEDICALTYPE>' +'<MedPmLimit>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR) +'</MedPmLimit>'+'<MEDICALDEDUCTIBLE>'+ CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1,0) AS VARCHAR)+'

  
</MEDICALDEDUCTIBLE>'+ '<Type>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR)+'</Type>'+'<MedPm></MedPm>')    
 else '' end       
 when COV_CODE = 'MEDPM2' then (+ case isnull(APP_VEHICLE_COVERAGES.LIMIT_1,-1)    
      when -1 then +''        
      else +'<MEDICALTYPE>EXCESS</MEDICALTYPE>'+'<MedPmLimit>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR) +'</MedPmLimit>'+'<MEDICALDEDUCTIBLE>'+ CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1,0) AS VARCHAR)+'</MEDICALDEDUCTIBLE>'+'<Type>'+'$1000 Medical'+'</Type>'+'<MedPm></MedPm>'     
      end     
    )    
 WHEN COV_CODE  =  'BISPL'  THEN '<BI>'  + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)+'/'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0) AS VARCHAR)+ '</BI><BILIMIT1>'                 
          +CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)+'</BILIMIT1><BILIMIT2>'+CAST(CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0) as int)/1000 AS VARCHAR)+'</BILIMIT2>'                      
      WHEN COV_CODE  = 'PD'  THEN '<PD>'    + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</PD>'                   
      WHEN COV_CODE  = 'RLCSL'  THEN '<CSL>'   + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</CSL>'                      
      WHEN COV_CODE  = 'PUMSP' THEN '<UMSPLIT>' + case  CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'/' + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0) AS VARCHAR)            
                                                  WHEN  '0/0' THEN ''            
                                                  ELSE CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'/' + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0) AS VARCHAR)            
               END             
               + '</UMSPLIT>'                      
         +'<UMSPLITLIMIT1>'+ CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'</UMSPLITLIMIT1>'                      
         +'<UMSPLITLIMIT2>'+ CAST(CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0) as int)/1000 AS VARCHAR)  +'</UMSPLITLIMIT2>'                      
     WHEN COV_CODE  = 'PUNCS' THEN '<UMCSL>' + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'</UMCSL>' --<PDLIMIT></PDLIMIT><PDDEDUCTIBLE></PDDEDUCTIBLE>'                      
     WHEN COV_CODE  = 'UMPD'  THEN '<PDLimit>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'</PDLimit>'                      
   +'<PDDEDUCTIBLE>' +CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)  +'</PDDEDUCTIBLE>'                      
     WHEN COV_CODE  = 'ROAD'  THEN '<ROADSERVICE>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'</ROADSERVICE>'                      
     WHEN COV_CODE  = 'OTC'  THEN '<ComprehensiveDeductible>'+CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)  +'</ComprehensiveDeductible>'                      
     WHEN COV_CODE  = 'COLL'  THEN '<CollisionDeductible>'+CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)  +'</CollisionDeductible>'                      
     WHEN COV_CODE  = 'EBM15'  THEN '<Helmet>'    + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</Helmet>'                     
     WHEN COV_CODE  = 'EBM49'  THEN '<McycleTrailer>'    + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</McycleTrailer>'                     
     WHEN COV_CODE  = 'PDC14'  THEN '<AddlPD>'    + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</AddlPD>'                    
        + '<INCREASEDVALUE>0</INCREASEDVALUE>'                     
  +'<MedPmType></MedPmType>'       
    
       ELSE ''                      
                     END     
) + @ReturnValue                                
   FROM         MNT_COVERAGE WITH (NOLOCK)                      
     left outer JOIN  APP_VEHICLE_COVERAGES WITH (NOLOCK) ON APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                      
   AND (APP_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMERID)                       
     AND (APP_VEHICLE_COVERAGES.APP_ID  = @APPID)                       
     AND (APP_VEHICLE_COVERAGES.APP_VERSION_ID  = @APPVERSIONID)                       
     AND   (APP_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLEID)                      
   WHERE                           
    MNT_COVERAGE.STATE_ID  = @STATEID  AND                       
    MNT_COVERAGE.LOB_ID = @LOBID        
    
    
    
    
    
                  
  END                   
----------------------------------------------------------------------------------------------------------------------------         
              
  --MICHIGAN   END                      
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------         
                       
 ELSE IF @STATEID ='14'   --INDIANA                      
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------         
         
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
 WHEN COV_CODE  = 'OTC'   THEN '<ComprehensiveDeductible>'+CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)  +'</ComprehensiveDeductible>'                      
 WHEN COV_CODE  = 'COLL'  THEN '<CollisionDeductible>'+CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)  +'</CollisionDeductible>'                      
 WHEN COV_CODE  = 'ROAD'  THEN '<ROADSERVICE>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'</ROADSERVICE>'                      
 WHEN COV_CODE  = 'EBM15' THEN '<Helmet>'    + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</Helmet>'                     
 WHEN COV_CODE  = 'EBM49' THEN '<McycleTrailer>'    + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</McycleTrailer>'                     
 WHEN COV_CODE  = 'PDC14' THEN '<AddlPD>'    + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</AddlPD>'                     
 WHEN COV_CODE  = 'EEM48'    THEN '<INCREASEDVALUE>'    + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</INCREASEDVALUE>'                      
 WHEN COV_CODE  = 'BISPL' THEN '<BI>'  + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)+'/'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0) AS VARCHAR)+ '</BI><BILIMIT1>' +CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)+'</BILIMIT1>'  + 
    
      
 '<BILIMIT2>'+ rtrim(ltrim(CAST( CAST((isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0)) AS int)/1000 as VARCHAR)))+'</BILIMIT2>'                      
 WHEN COV_CODE  = 'PD'    THEN '<PD>'+ CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</PD>'                      
 WHEN COV_CODE  = 'MEDPM' THEN '<MedPm>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'</MedPm>'  +'<MedPmType>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR)+'</MedPmType>' +'<MedPmLimit></MedPmLimit>' +'<MEDICALTYPE>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR)+'</MEDICALTYPE>'                 
     
 -- +'<Type></Type>'  -- CHANGE LATER      ..we add it later in the proc for indiana                
 WHEN COV_CODE  = 'UMPD'  THEN '<PDLimit>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'</PDLimit>'                  
 +'<PDDEDUCTIBLE>' +CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)  +'</PDDEDUCTIBLE>'                           
              
 WHEN COV_CODE  = 'UNDSP'  THEN case isnull(APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0)               
         when 0  then ''                
         else  '<isUnderInsuredMotorists>Y</isUnderInsuredMotorists>'              
      --<001 start>              
      +'<UIMSPLIT>' + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)+'/'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0) AS VARCHAR) + '</UIMSPLIT>'              
      + '<UIMSPLITLIMIT1>'  + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)+ '</UIMSPLITLIMIT1>'              
      + '<UIMSPLITLIMIT2>'+ rtrim(ltrim(CAST( CAST((isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0)) AS int)/1000 as VARCHAR)))+'</UIMSPLITLIMIT2>'              
      --<001 end>              
       end -- only in case of  indiana                  
 WHEN COV_CODE  = 'UNCSL'  THEN case isnull(APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0)               
         when 0  then ''               
         else  '<isUnderInsuredMotorists>Y</isUnderInsuredMotorists>'               
      --<002 start>              
      + '<UIMCSL>'+ CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)+ '</UIMCSL>'                       
      --<002 end>              
           end -- only in case of  indiana                  
               
 WHEN COV_CODE  = 'RLCSL'  THEN '<CSL>'   + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  + '</CSL>'                      
 WHEN COV_CODE  = 'PUMSP' THEN '<UMSPLIT>' + case  CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'/' + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0) AS VARCHAR)            
                                                  WHEN  '0/0' THEN ''            
                                                  ELSE CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'/' + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0) AS VARCHAR)            
               END             
               + '</UMSPLIT>'                       
 +'<UMSPLITLIMIT1>'+ CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'</UMSPLITLIMIT1>'                      
 +'<UMSPLITLIMIT2>'+ CAST(CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0) as int)/1000 AS VARCHAR)  +'</UMSPLITLIMIT2>'                      
 WHEN COV_CODE  = 'PUNCS' THEN '<UMCSL>' + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)  +'</UMCSL>' --<PDLIMIT></PDLIMIT><PDDEDUCTIBLE></PDDEDUCTIBLE>'                      
                
              
              
 ELSE ''                      
     END ) + @ReturnValue                               
   FROM                              
     MNT_COVERAGE  WITH (NOLOCK) left outer JOIN  APP_VEHICLE_COVERAGES  WITH (NOLOCK) ON APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                      
      AND (APP_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMERID)                      
    AND (APP_VEHICLE_COVERAGES.APP_ID = @APPID)                       
    AND (APP_VEHICLE_COVERAGES.APP_VERSION_ID = @APPVERSIONID)                       
    AND (APP_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLEID)                      
   WHERE                           
    MNT_COVERAGE.STATE_ID =@STATEID and MNT_COVERAGE.LOB_ID=@LOBID                      
                         
                        
                         
  END                      
                      
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------         
        
--- INDIANA   END                      
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------         
             
------------------------                      
--COVERAGES FOR DIFFERENT STATES AS THE CODES MAY DIFFER FOR DIFFERENT STATES  END                      
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

  
    
      
        
        
------------------------                      
--FINAL SELECT   START                      
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

  
    
      
        
         
                 
  DECLARE @RetVal  VARCHAR(8000)                    
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
	       MNT_COVERAGE WITH (NOLOCK)  INNER JOIN  APP_VEHICLE_COVERAGES WITH (NOLOCK) ON APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                      
	        AND (APP_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMERID)                      
	      AND (APP_VEHICLE_COVERAGES.APP_ID = @APPID)                       
	      AND (APP_VEHICLE_COVERAGES.APP_VERSION_ID = @APPVERSIONID)                       
	      AND (APP_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLEID)                      
	     WHERE                           
	      MNT_COVERAGE.STATE_ID =@STATEID AND MNT_COVERAGE.LOB_ID=@LOBID AND MNT_COVERAGE.COV_CODE in ('UMPD','E17'))
        
   BEGIN 
 	  SET @RETVAL = @RETVAL + '<TYPE>BIPD</TYPE>'               
   END
	ELSE IF EXISTS                
	  (SELECT COV_ID            
    FROM      
       MNT_COVERAGE WITH (NOLOCK)  INNER JOIN  APP_VEHICLE_COVERAGES WITH (NOLOCK) ON APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                      
        AND (APP_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMERID)                      
      AND (APP_VEHICLE_COVERAGES.APP_ID = @APPID)                       
      AND (APP_VEHICLE_COVERAGES.APP_VERSION_ID = @APPVERSIONID)                       
      AND (APP_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLEID)                      
     WHERE                           
      MNT_COVERAGE.STATE_ID =@STATEID AND MNT_COVERAGE.LOB_ID=@LOBID AND  MNT_COVERAGE.COV_CODE in ('PUMSP','PUNCS'))
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

 
      
        
        
------------------------                      
--FINAL SELECT   END                      
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


   end                    




GO

