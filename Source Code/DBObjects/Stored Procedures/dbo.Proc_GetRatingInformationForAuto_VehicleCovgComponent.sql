IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForAuto_VehicleCovgComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForAuto_VehicleCovgComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Proc_GetRatingInformationForAuto_VehicleCovgComponent 900,151,1,1                      
                      
/*----------------------------------------------------------                                        
Proc Name            : Dbo.Proc_GetRatingInformationForAuto_VehicleCovgComponent                                        
Created by              : Nidhi.                                        
Date                    :11/10/2005                                        
Purpose                 : To get the coverage information for creating the input xml                                          
Revison History      :                                        
Used In                 :   Creating InputXML for vehicle  15                           
                                 
------------------------------------------------------------                                        
Date     Review By          Comments                                        
------   ------------       -------------------------*/                                        
--  
CREATE PROC dbo.Proc_GetRatingInformationForAuto_VehicleCovgComponent   
(                                        
@CUSTOMERID    int,                                        
@APPID    int,                                        
@APPVERSIONID   int,                                        
@VEHICLEID    int                                         
)                                        
AS                                        
                                        
BEGIN                                        
set quoted_identifier off                                        
  
DECLARE  @COMPREHENSIVEDEDUCTIBLE   nvarchar(100)                            
DECLARE  @COLLISIONDEDUCTIBLE   nvarchar(100)                                        
DECLARE  @COVGCOLLISIONTYPE   nvarchar(100)                                        
DECLARE  @COVGCOLLISIONDEDUCTIBLE   nvarchar(100)                                        
DECLARE  @ROADSERVICE   nvarchar(100)                                        
DECLARE  @RENTALREIMBURSEMENT   nvarchar(100)                                        
DECLARE  @RENTALREIMLIMITDAY   nvarchar(100)                                        
DECLARE  @RENTALREIMMAXCOVG   nvarchar(100)                                        
DECLARE  @MINITORTPDLIAB   nvarchar(100)                                     
DECLARE  @LOANLEASEGAP   nvarchar(100)                                        
DECLARE  @ISANTILOCKBRAKESDISCOUNTS   nvarchar(100)                                        
DECLARE  @AIRBAGDISCOUNT   nvarchar(100)                                        
DECLARE  @IS200SOUNDREPRODUCING   nvarchar(100)                                        
DECLARE  @SOUNDRECEIVINGTRANSMITTINGSYSTEM   nvarchar(100)                                        
DECLARE  @MULTICARDISCOUNT   nvarchar(100)                                        
DECLARE  @INSURANCEAMOUNT   nvarchar(100)                                        
DECLARE  @EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE   nvarchar(100)                                        
DECLARE  @COLLISIONTYPEDED   nvarchar(100)                                        
DECLARE  @EXTRAEQUIPCOLLISIONTYPE   nvarchar(100)                                        
DECLARE  @EXTRAEQUIPCOLLISIONDEDUCTIBLE   nvarchar(100)                                        
DECLARE  @BI   nvarchar(100)                                        
DECLARE  @BILIMIT1   nvarchar(100)                                        
DECLARE  @BILIMIT2   nvarchar(100)                                        
DECLARE  @PD   nvarchar(100)                                        
DECLARE  @CSL   nvarchar(100)                                        
DECLARE  @MEDPM   nvarchar(100)                                        
DECLARE  @TYPE   nvarchar(100)                                        
DECLARE  @ISUNDERINSUREDMOTORISTS   nvarchar(100)                                  
DECLARE  @UMSPLIT   nvarchar(100)         
DECLARE  @UMSPLITLIMIT1   nvarchar(100)                                        
DECLARE  @UMSPLITLIMIT2   nvarchar(100)                                        
DECLARE  @UMCSL   nvarchar(100)                                        
DECLARE  @PDLIMIT   nvarchar(100)                                        
DECLARE  @PDDEDUCTIBLE   nvarchar(100)                                        
DECLARE  @QUALIFIESTRAIBLAZERPROGRAM   nvarchar(100)                                        
  
 /***************************** START *************************************                                         
   
 ------------------------------------------------------------------------------*/                                        
 DECLARE @ReturnValue  varchar(8000)                                        
 DECLARE @STATEID varchar(5)                                        
 DECLARE @LOBID varchar(3)                                        
 SET @ReturnValue = ''                                        
                                         
 /* IF ELSE for coverages for different states as the codes may differ for different states */                                        
 SELECT @STATEID=STATE_ID ,@LOBID= APP_LOB from APP_LIST WITH (NOLOCK) where CUSTOMER_ID =@CUSTOMERID and APP_ID =@APPID and APP_VERSION_ID=@APPVERSIONID                                        
                   
 if @STATEID ='22'  -- MICHIGAN                                        
  /************************************************************** BEGINNING  OF MICHIGAN  **********************************************************************************************************************************/                                 




  
     
     
       
  begin                                        
   SELECT @ReturnValue =                                         
       (CASE COV_CODE                                          
 WHEN 'COMP' THEN  '<COMPREHENSIVEDEDUCTIBLE>' + CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR) + '</COMPREHENSIVEDEDUCTIBLE>'                                        
 WHEN 'COLL' THEN '<COLLISIONDEDUCTIBLE>' + CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR) + '  ' +  CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR)  +'</COLLISIONDEDUCTIBLE><COVGCOLLISIONTYPE>'                  



  
 +CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE1_AMOUNT_TEXT,'') AS VARCHAR)+'</COVGCOLLISIONTYPE><COVGCOLLISIONDEDUCTIBLE>'                                        
 + CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR) +'</COVGCOLLISIONDEDUCTIBLE>'                                        
                                    
 --WHEN 'ROAD' THEN '<ROADSERVICE>' + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR) + '</ROADSERVICE>'                                        
 WHEN 'ROAD' THEN '<ROADSERVICE>'                    + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))               



  
 + '</ROADSERVICE>'                                         
                          
       WHEN 'RREIM' THEN '<RENTALREIMBURSEMENT>' + CONVERT(varchar(30),isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)) +'/'                           
 + CONVERT(varchar(30),isnull(APP_VEHICLE_COVERAGES.LIMIT_2,0))+ '</RENTALREIMBURSEMENT>'                                      
 + '<RENTALREIMLIMITDAY>'+CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)                          
 + '</RENTALREIMLIMITDAY><RENTALREIMMAXCOVG>'                          
 + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_2,0)AS VARCHAR) +'</RENTALREIMMAXCOVG>'                                                           
                          
              WHEN 'LPD' THEN '<MINITORTPDLIAB>'+ CAST( (case isnull(APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0)when 0 then 'FALSE'              
      else 'TRUE' end) AS VARCHAR) +              
    '</MINITORTPDLIAB>'           
                                     
              WHEN 'LLGC' THEN '<LOANLEASEGAP>' + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR) + '</LOANLEASEGAP>'                                        
       WHEN 'SORPE' THEN '<IS200SOUNDREPRODUCING>' + CAST( (CASE isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0)                                           
             when  0   then  'FALSE'                                   
              else 'TRUE'                                        
  end)                                        
            AS VARCHAR) + '</IS200SOUNDREPRODUCING>'                                        
             WHEN 'SRTE' THEN '<SOUNDRECEIVINGTRANSMITTINGSYSTEM>'                           
  + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0)) + '</SOUNDRECEIVINGTRANSMITTINGSYSTEM>'                         




   
           
       WHEN 'EECOMP' THEN '<EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE>'+                        
 case CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)                        
 when 0 then ''                      
 else CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)                         
  end  +'</EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE>'                                        
           +'<INSURANCEAMOUNT>'+ convert(varchar(30),isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0))+'</INSURANCEAMOUNT>'                                  
                      
                            
         WHEN 'EECOLL' THEN '<COLLISIONTYPEDED>'+   CASE CAST(ISNULL(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)+' '+  CAST(ISNULL(APP_VEHICLE_COVERAGES.DEDUCTIBLE1_AMOUNT_TEXT, 0) AS VARCHAR)                      
                                                    WHEN '0 0' THEN ''                   
                                                    WHEN '0  ' THEN ''                      
                                                    ELSE                      
               CAST(ISNULL(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)+' '+  CAST(ISNULL(APP_VEHICLE_COVERAGES.DEDUCTIBLE1_AMOUNT_TEXT, 0) AS VARCHAR)                         
             END                      
    +'</COLLISIONTYPEDED>'                            
                      
                              
        +'<EXTRAEQUIPCOLLISIONDEDUCTIBLE>'+  
			CASE WHEN ISNULL(APP_VEHICLE_COVERAGES.DEDUCTIBLE1_AMOUNT_TEXT, 0)='Limited'
								THEN CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR) + ' '+  CAST(ISNULL(APP_VEHICLE_COVERAGES.DEDUCTIBLE1_AMOUNT_TEXT, 0) AS VARCHAR)
							 ELSE
									CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR) 
			END
		+ '</EXTRAEQUIPCOLLISIONDEDUCTIBLE>'                                        
        +'<EXTRAEQUIPCOLLISIONTYPE>'+  CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR) +'</EXTRAEQUIPCOLLISIONTYPE>'                                          
WHEN 'BISPL' THEN                              
  '<BI>'                              
         + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                          
   +'/'                              
         + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_2,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_2,0)),1),0))                              
         + '</BI><BILIMIT1>'                       
         + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)                              
         +'</BILIMIT1><BILIMIT2>'                                     
         + Substring(CAST((isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR),0,charindex('.',CAST((isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR))  )                     
         +'</BILIMIT2>'                                  
                              
      WHEN 'PD' THEN                           
 '<PD>'                           
-- + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)                        
 + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                          
 + '</PD>'                                        
                                                  
      WHEN 'SLL' THEN '<CSL>' + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))  + '</CSL>'                         




  
              
      WHEN 'PUMSP' THEN '<UMSPLIT>' + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                           
    +'/'                           
    -- + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0) AS VARCHAR)                           
    + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_2,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_2,0)),1),0))                   
    + '</UMSPLIT>'                                        
           +'<UMSPLITLIMIT1>'+ Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))            +'</UMSPLITLIMIT1>'        

          --  +'<UMSPLITLIMIT2>'+ CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0)/1000.00 AS VARCHAR)        
   +'<UMSPLITLIMIT2>'+ Substring(CAST((isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR),0,charindex('.',CAST((isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR)))                          
   +'</UMSPLITLIMIT2>'
-- UNDERINSURED MOTORIST LIMIT                               
	 WHEN 'UNDSP' THEN '<UIMSPLIT>'+Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))+'/' + Substring(convert(varchar(30),
convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_2,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_2,0)),1),0))+'</UIMSPLIT>'+
					   '<UIMSPLITLIMIT1>'+Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0)) +'</UIMSPLITLIMIT1>'+
					   '<UIMSPLITLIMIT2>'+Substring(CAST((isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR),0,charindex('.',CAST((isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR)))+'</UIMSPLITLIMIT2>'+	
					   case isnull(APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0) when 0  then '' else  '<isUnderInsuredMotorists>TRUE</isUnderInsuredMotorists>'  END
     WHEN 'UNCSL' THEN '<UIMCSL>'+CASE isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)                      
									WHEN 0 THEN ''                      
								 ELSE SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0)) END+'</UIMCSL>'+ 
						case isnull(APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0) when 0  then '' else  '<isUnderInsuredMotorists>TRUE</isUnderInsuredMotorists>'  end                                             
	WHEN 'PUNCS' THEN '<UMCSL>' +                        
 case isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)                      
 when 0 then ''                      
 else Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                            
  end +'</UMCSL><PDLIMIT></PDLIMIT><PDDEDUCTIBLE>0</PDDEDUCTIBLE>'                                        
                                        
     WHEN 'PIP' THEN '<PIP>'+ Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0)) +'</PIP>'                              



  
  
 +'<MEDPMDEDUCTIBLE>'+ cast (isnull( APP_VEHICLE_COVERAGES.DEDUCTIBLE_1,0) as varchar) +'</MEDPMDEDUCTIBLE>'       
           
   +'<MEDPM>'+ case isnull(APP_VEHICLE_COVERAGES.LIMIT_ID, 0) when 685  then 'FULL' WHEN 686 THEN 'EXCESSWAGE' WHEN 687 THEN 'EXCESSMEDICAL' WHEN 688 THEN 'EXCESSBOTH' when 1372 then 'FULLMEDICALFULLWAGELOSS' when 1373 then'FULLMEDICALEXCESSWAGEWORKCOMP' 
  
    
      
when 1374 then 'EXCESSMEDICALFULLWAGELOSS' when 1375 then 'EXCESSMEDICALEXCESSWAGELOSSWORKCOMP'  else '' END +'</MEDPM>'                                           
      
         
       
WHEN 'PPI' THEN '<PPI>'+ Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))  +'</PPI>'                           
         +'<QUALIFIESTRAIBLAZERPROGRAM>FALSE</QUALIFIESTRAIBLAZERPROGRAM>'                                      
          --+'<MEDPM>0</MEDPM>'                 
         +'<TYPE>UNINSURED MOTORISTS</TYPE>'  --CHANGE LATER                                        
        -- +'<ISUNDERINSUREDMOTORISTS>TRUE</ISUNDERINSUREDMOTORISTS>' --CHANGE LATER                                        
    -- WHEN 'UNDSP'  THEN case isnull(APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0) when 0  then '' else  '<isUnderInsuredMotorists>TRUE</isUnderInsuredMotorists>'  end                                            
        --  WHEN 'UNCSL'  THEN case isnull(APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0) when 0  then '' else  '<isUnderInsuredMotorists>TRUE</isUnderInsuredMotorists>'  end                                             
   
                                        
      ELSE ''                                        
                     END ) + @ReturnValue                                                  
    FROM         MNT_COVERAGE  WITH (NOLOCK)                                       
 LEFT OUTER JOIN  APP_VEHICLE_COVERAGES WITH (NOLOCK) ON APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                                        
 AND (APP_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMERID) AND (APP_VEHICLE_COVERAGES.APP_ID = @APPID) AND                                         
 (APP_VEHICLE_COVERAGES.APP_VERSION_ID = @APPVERSIONID) AND   (APP_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLEID)                                        
 WHERE     MNT_COVERAGE.STATE_ID =@STATEID  and MNT_COVERAGE.LOB_ID=@LOBID                                        
  end                                        
  /************************************************************** END  OF MICHIGAN  **********************************************************************************************************************************/                    
 else if @STATEID ='14'   --INDIANA                                        
                                        
 /************************************************************** BEGINNING  OF INDIANA **********************************************************************************************************************************/                            


 
 
  
  begin     
                               
   SELECT    @ReturnValue =                                         
       (CASE COV_CODE                                    
                WHEN 'OTC'  THEN  '<COMPREHENSIVEDEDUCTIBLE>'   
     + case  CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)                       
      when '0' then ''  
     else                 
     + CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)  
     end  
     + '</COMPREHENSIVEDEDUCTIBLE>'                                        
       
              WHEN 'COLL'  THEN '<COLLISIONDEDUCTIBLE>'  
     + case  CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)                       
      when '0' then ''  
     else        
     + CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR) + '  ' +  CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1_TYPE, '') AS VARCHAR)    
     end  
     +'</COLLISIONDEDUCTIBLE>'  
     +'<COVGCOLLISIONTYPE>'              
          +CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1_TYPE,'') AS VARCHAR)+'</COVGCOLLISIONTYPE><COVGCOLLISIONDEDUCTIBLE>'                                        
            + CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR) +'</COVGCOLLISIONDEDUCTIBLE>'                                             
              WHEN 'ROAD'  THEN '<ROADSERVICE>'   
     + case  CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)                       
      when '0' then ''  
     else   
     + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))  
     end  
     + '</ROADSERVICE>      
        '+'<MINITORTPDLIAB>FALSE</MINITORTPDLIAB>' -- we do not need it here                                        
         WHEN 'RREIM'  THEN '<RENTALREIMBURSEMENT>'   
     + case  CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)                       
      when '0' then ''  
     else                           
      +  CONVERT(varchar(30),isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)) +'/'                           
      + CONVERT(varchar(30),isnull(APP_VEHICLE_COVERAGES.LIMIT_2,0))  
     end  
     +'</RENTALREIMBURSEMENT>'                                        
            + '<RENTALREIMLIMITDAY>'                          
      + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                          
      + '</RENTALREIMLIMITDAY>'  
     + '<RENTALREIMMAXCOVG>'                          
      + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_2,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_2,0)),1),0))                          
      + '</RENTALREIMMAXCOVG>'                             
                          
         WHEN 'LLGC'  THEN  '<LOANLEASEGAP>'   
     + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR)   
     + '</LOANLEASEGAP>'                                        
         WHEN 'SORPE'  THEN  '<IS200SOUNDREPRODUCING>' + CAST( (CASE isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0)                                           
                       when  0 then  'FALSE'                                        
                        else   'TRUE'  
            end)AS VARCHAR)   
     + '</IS200SOUNDREPRODUCING>'                                        
          WHEN 'SRTE'  THEN  '<SOUNDRECEIVINGTRANSMITTINGSYSTEM>'  
     + CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)   
     + '</SOUNDRECEIVINGTRANSMITTINGSYSTEM>'                                        
         WHEN 'EECOMP'  THEN      '<EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE>'+                       
      case  CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)                       
       when '0' then ''                      
       else CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)                       
        end    
     +'</EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE>'                                        
            +'<INSURANCEAMOUNT>'+  convert(varchar(30),isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)) +'</INSURANCEAMOUNT>'                
                WHEN 'EECOLL'  THEN  '<EXTRAEQUIPCOLLISIONDEDUCTIBLE>'  
     +  CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)     
     +'</EXTRAEQUIPCOLLISIONDEDUCTIBLE>'                                  
            +'<COLLISIONTYPEDED></COLLISIONTYPEDED>'                                                
            +'<EXTRAEQUIPCOLLISIONTYPE></EXTRAEQUIPCOLLISIONTYPE>'                                          
     WHEN 'BISPL'  THEN  '<BI>'  
     + case  CAST(isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)                       
      when '0' then ''  
     else                       
     + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                            
      +'/'                              
      + replace(convert(varchar(20),convert(money,APP_VEHICLE_COVERAGES.LIMIT_2),1),'.00','')                              
      end  
     + '</BI>'  
     +'<BILIMIT1>'  
     + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                              
      +'</BILIMIT1>'  
     +'<BILIMIT2>'                              
     + Substring(CAST((isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR),0,charindex('.',CAST((isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR))  )                             
      +'</BILIMIT2>'                               
       
                              
             WHEN 'PD'  THEN  '<PD>'   
     + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))    
     + '</PD>'                       
         WHEN 'SLL'  THEN  '<CSL>'                       
      + case isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)                    
       when 0 then ''                       
       else Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                       
       end   
     + '</CSL>'                                      
   WHEN 'PUMSP'  THEN '<UMSPLIT>'                           
      + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                          
      + '/'                           
       +  Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_2,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_2,0)),1),0))                          
      + '</UMSPLIT>'                                        
     + '<UMSPLITLIMIT1>'                          
     + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                          
     + '</UMSPLITLIMIT1>'                                        
     + '<UMSPLITLIMIT2>'                          
     + Substring(CAST((isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR),0,charindex('.',CAST((isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR))  )                          
     + '</UMSPLITLIMIT2>'
-- UNDERINSURED MOTORIST UIMSPLIT                                        
       WHEN 'UNDSP' THEN '<UIMSPLIT>'+Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))+'/' + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_2,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_2,0)),1),0))+'</UIMSPLIT>'+
					   '<UIMSPLITLIMIT1>'+Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0)) +'</UIMSPLITLIMIT1>'+
					   '<UIMSPLITLIMIT2>'+Substring(CAST((isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR),0,charindex('.',CAST((isnull(APP_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR)))+'</UIMSPLITLIMIT2>'+
					case isnull(APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0)   
						when 0  then ''   
					else  '<isUnderInsuredMotorists>TRUE</isUnderInsuredMotorists>'  end
     WHEN 'UNCSL' THEN '<UIMCSL>'+CASE isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)                      
									WHEN 0 THEN ''                      
								 ELSE SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0)) END+'</UIMCSL>'+ 
	                   case isnull(APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0)   
							when 0  then ''   
						else  '<isUnderInsuredMotorists>TRUE</isUnderInsuredMotorists>'  end
       WHEN 'PUNCS'  THEN  '<UMCSL>'   
     + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))   
     +'</UMCSL>'                        
                   + '<PIP></PIP>'                                        
            + '<PPI></PPI>'                                        
  WHEN 'MP'  THEN    '<MEDPM>'  
     + case isnull(APP_VEHICLE_COVERAGES.LIMIT_ID, 0) when 685  then 'PRIMARY' WHEN 686 THEN 'EXCESSWAGE' WHEN 687 THEN 'EXCESSMEDICAL' WHEN 688 THEN 'EXCESSBOTH'  else '' END    
     +'</MEDPM>'      
     +'<MEDPMDEDUCTIBLE>'+ cast (isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1,0) as varchar)   
     +'</MEDPMDEDUCTIBLE>'    
      + '<MPLIMIT>'+ CONVERT(VARCHAR(30),isnull(APP_VEHICLE_COVERAGES.LIMIT_1, 0))    
     +'</MPLIMIT>'                                      
  WHEN 'UMPD'  THEN    '<PDLIMIT>'  
     + Substring(convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(APP_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))   
     +'</PDLIMIT>'                       
     +'<PDDEDUCTIBLE>'  
     + CAST(isnull(APP_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)   
     +'</PDDEDUCTIBLE>'                                        
--  WHEN 'UNDSP'   THEN    case isnull(APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0)   
--               when 0  then ''   
--        else  '<isUnderInsuredMotorists>TRUE</isUnderInsuredMotorists>'  end -- only in case of indiana                                            
--         WHEN 'UNCSL'   THEN    case isnull(APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0)   
--      when 0  then ''   
--      else  '<isUnderInsuredMotorists>TRUE</isUnderInsuredMotorists>'  end -- only in case of  indiana                                            
                                    
          /* - TO DO                                          
          1. RENTALREIMLIMITDAY (Rental Reimbursement Limit Day)                                        
          2. RENTALREIMMAXCOVG (Rental Reimbursement Max Coverage)  
        3. Extra Equipment Collision Type                                        
          4. Extra Equipment Collision Deductible                                        
          5. TYPE                        
          6. ISUNDERINSUREDMOTORIST                                        
          7. INSURANCE AMOUNT                                        
          */                                        
       ELSE ''                                         
            END )   
        + @ReturnValue                                                  
   FROM         MNT_COVERAGE   WITH (NOLOCK)                                      
    left OUTER JOIN  APP_VEHICLE_COVERAGES WITH (NOLOCK) ON APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                                        
      AND (APP_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMERID) AND (APP_VEHICLE_COVERAGES.APP_ID = @APPID) AND                                         
                 (APP_VEHICLE_COVERAGES.APP_VERSION_ID = @APPVERSIONID) AND   (APP_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLEID)                                        
   WHERE     MNT_COVERAGE.STATE_ID =@STATEID and MNT_COVERAGE.LOB_ID=@LOBID                                        
        
                                  
                                          
                                           
  end                            
                      
                         
                      
 /************************************************************** END OF INDIANA ****************************************************************************************************************************************/                                     




  
   
--select isnull(@ReturnValue,'')                      
                      
                      
if @STATEID ='22'  -- MICHIGAN                     
  /************************************************************** BEGINNING  OF MICHIGAN  **********************************************************************************************************************************/                                  



  
    
     
       
                                      
 DECLARE @RetVal varchar(8000)                                        
                          
-- SET @RetVal = @ReturnValue                            
                          
 select @RetVal =                                 
 case                                  
  when charindex('<isUnderInsuredMotorists>',@ReturnValue) > 0  then @ReturnValue                                 
  when charindex('<isUnderInsuredMotorists>',@ReturnValue) = 0                             
  then @ReturnValue + '<isUnderInsuredMotorists>False</isUnderInsuredMotorists>'                                          
  else @ReturnValue                                          
 end                          
 select @RetVal=                          
  case                          
  when charindex('<CSL>',@ReturnValue) > 0  then @ReturnValue                                 
  when charindex('<CSL>',@ReturnValue) = 0      
  then @ReturnValue + '<CSL></CSL>'                                          
  else @ReturnValue                                          
 end                       
                               
 select @RetVal=                          
  case                 
  when charindex('<MINITORTPDLIAB>',@ReturnValue) > 0  then @ReturnValue                                 
  when charindex('<MINITORTPDLIAB>',@ReturnValue) = 0                             
  then @ReturnValue + '<MINITORTPDLIAB>FALSE</MINITORTPDLIAB>'                                          
  else @ReturnValue                                          
 end                       
                      
                      
--SET @RetVal=REPLACE(@RetVal,'<COLLISIONTYPEDED>0 0</COLLISIONTYPEDED>','<COLLISIONTYPEDED></COLLISIONTYPEDED>')                      
                          
 /* Add 'type' node in case of INDIANA state/                                          
 1. If BI coverage is present then 'BI ONLY'                                        
 2. If BI and UMPD both do not exists then '' or 'NO COVERAGE'                                             
 3. IF UMPD exists then check the deductible and 'BI/PD with 0 deductible' or 'BI/PD with 300 deductible' */                                          
   IF @STATEID ='14'   --INDIANA                                             
     BEGIN                                          
      if exists                                          
   ( SELECT *                                          
     FROM                                                        
        MNT_COVERAGE WITH (NOLOCK) inner JOIN  APP_VEHICLE_COVERAGES WITH (NOLOCK) ON APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                                                
         AND (APP_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMERID)                                
       AND (APP_VEHICLE_COVERAGES.APP_ID = @APPID)                                                 
       AND (APP_VEHICLE_COVERAGES.APP_VERSION_ID = @APPVERSIONID)                                                 
       AND (APP_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLEID)                                                
      WHERE                                                     
       MNT_COVERAGE.STATE_ID =@STATEID and MNT_COVERAGE.LOB_ID=@LOBID and MNT_COVERAGE.COV_CODE='BI'                                          
   )                                          
    BEGIN                                          
    SET @RetVal = @RetVal + '<Type>BI ONLY</Type>'                                          
    END                                      
   else if exists                                          
   ( SELECT *                                
     FROM                                                        
        MNT_COVERAGE WITH (NOLOCK) inner JOIN  APP_VEHICLE_COVERAGES WITH (NOLOCK) ON APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                                                
         AND (APP_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMERID)                                         
      AND (APP_VEHICLE_COVERAGES.APP_ID = @APPID)                                                 
       AND (APP_VEHICLE_COVERAGES.APP_VERSION_ID = @APPVERSIONID)                                                 
       AND (APP_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLEID)                                                
      WHERE                                                     
       MNT_COVERAGE.STATE_ID =@STATEID and MNT_COVERAGE.LOB_ID=@LOBID and MNT_COVERAGE.COV_CODE in ('BIPD','UMPD')                                          
   )                                          
     BEGIN                                          
      SET @RetVal = @RetVal + '<Type>BIPD</Type>'                                          
     END                                          
   else                         
   BEGIN                                          
    SET @RetVal = @RetVal + '<Type></Type>'                                          
   END                                          
     END                      
                                         
                                          
    /*                  
-------------------------------------------                      
DECLARE @ENDORSEMENTS  nvarchar(4000)               
DECLARE @ENDORSEMENT_ID NVARCHAR(4)                      
SET @ENDORSEMENTS=''                      
                      
                      
IF @STATEID ='14'                         
 SET @ENDORSEMENT_ID ='6'--SOUND RECEIVING AND TRANSMITTING ENDORSEMENT CODE FOR INDIANA                      
ELSE                      
 SET @ENDORSEMENT_ID ='28'--SOUND RECEIVING AND TRANSMITTING ENDORSEMENT CODE FOR MICHIGAN                      
                      
                      
                                     
SELECT @ENDORSEMENTS =  '<SOUNDRECEIVINGTRANSMITTINGSYSTEM>'                   
   + Substring(convert(varchar(30),convert(money,isnull(REMARKS,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(REMARKS,0)),1),0)) + '</SOUNDRECEIVINGTRANSMITTINGSYSTEM>'                                          
FROM APP_VEHICLE_ENDORSEMENTS                        
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND                       
 VEHICLE_ID = @VEHICLEID AND ENDORSEMENT_ID =@ENDORSEMENT_ID                      
                      
                      
IF @STATEID ='14'                         
 SET @ENDORSEMENT_ID ='3'--extra equipment amount  ENDORSEMENT CODE FOR INDIANA                      
ELSE                      
 SET @ENDORSEMENT_ID ='26'--extra equipment amount  ENDORSEMENT CODE FOR michigan                      
                      
                      
SELECT @ENDORSEMENTS =  @ENDORSEMENTS + '<INSURANCEAMOUNT>'                           
   + Substring(convert(varchar(30),convert(money,isnull(REMARKS,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(REMARKS,0)),1),0)) + '</INSURANCEAMOUNT>'                                          
FROM APP_VEHICLE_ENDORSEMENTS                        
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND                       
 VEHICLE_ID = @VEHICLEID AND ENDORSEMENT_ID =@ENDORSEMENT_ID                      
                      
 SELECT                                      
   @RetVal  +@ENDORSEMENTS  as COVERAGES                        
-------------------------------------------    */                  
                      
 /* FINAL SELECT */                                        
 SELECT                                      
  @RetVal   as COVERAGES                         
                       
                                           
END                                        


GO

