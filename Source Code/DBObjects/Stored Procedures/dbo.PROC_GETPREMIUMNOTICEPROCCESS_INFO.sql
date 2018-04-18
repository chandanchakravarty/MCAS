IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETPREMIUMNOTICEPROCCESS_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETPREMIUMNOTICEPROCCESS_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE PROC_GETPREMIUMNOTICEPROCCESS_INFO 

             

            @CUSTOMER_ID varchar(10), 

            @POLICY_ID varchar(10), 

            @POLICY_VERSION_ID varchar(10), 

            @PROCCESS_INFORMATION text --for xml

AS                                          

BEGIN     

INSERT INTO ACT_PREMIUM_NOTICE_PROCCESS_LOG

 

            (

                        CUSTOMER_ID ,

                        POLICY_ID ,

                        POLICY_VERSION_ID,

                        PROCCESS_INFORMATION 

            )            

            VALUES            

            (       

                        @CUSTOMER_ID,

                        @POLICY_ID,

                        @POLICY_VERSION_ID,

                        @PROCCESS_INFORMATION

            )

END



 

 

 

 

 



GO

