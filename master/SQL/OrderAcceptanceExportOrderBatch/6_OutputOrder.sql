------------------------------------------------------------------ 6 Ainsert into TB_R_ORDER_SPEC (
COMPANY_CD																																	
,RECEIVE_NO																																			
,DETAIL_NO																																			
,DATA_ID																																		
,VERSION																																		
,REVISION																																		
,IMPORTER_CD																																			
,ORD_TYPE																																		
,EXPORTER_CD																																			
,PACK_MONTH																																		
,CFC																																	
,IMPORTER_NAME																																			
,EXPORTER_NAME																																			
,SS_NO																																	
,LINE_CD																																		
,PART_NO																																		
,EXT_COLOR																																			
,INT_COLOR																																			
,CTRL_MODE_CD																																				
,DISP_MODE_CD																																				
,LOT_SIZE																																		
,TRANS_CD																																					
,ORDER_DT																																		
,SERIES																																	
,STATUS_CD																																
,RE_EXPORT_CD																																		
,AICO_FLAG																																		
,TOOL_CD																														
,CREATED_DT																															
,CHANGED_DT																																																					
,CREATED_BY																												
,CHANGED_BY		)																									

select
 @Param_CompanyCD						
,RCVNO			                
,DTLNO							
,DATAID                 			
,VERSION								
,REVISIONNO                    			
,IMPORTERDCD							
,ODRTYPE						
,EXPORTERCD                  	
,PCMN						
,CFC     							
,IMPORTERNAME  			
,EXPORTERNAME 			
,SSNO							
,[LINECD]						
,PARTNO							
,[Exterior_Color]			
,[Interior_Color]			
,[Control_Mode_Code]			
,[Display_Mode_Code]		
,[Lot_Code]					
,[Transportation_Code]		
,[Order_Date]							
,[Series]									
,'1'								
,[REEXPCD]						
,[AICO/CEPT]						
,3												
,convert(varchar(max),getdate(),111)								
,NULL																													
,'Username'														
,NULL															
from [dbo].[TB_T_EXPORT_ORDER_ATTACHMENT] where ODRTYPE != 'C'

----------------------------------   6.B

declare @@NOMOR               varchar(max)
declare @@DATAID 			 varchar(max)
declare @@Param_VERSION 			 varchar(max)
declare @@REVISIONNO 		 varchar(max)
declare @@IMPORTERDCD 		 varchar(max)
declare @@EXPORTERCD 		 varchar(max)
declare @@ODRTYPE 			 varchar(max)
declare @@PCMN 				 varchar(max)
declare @@CFC 				 varchar(max)
declare @@REEXPCD 			 varchar(max)
declare @@AICOMirCEPT 		 varchar(max)
declare @@PXPLAG 			 varchar(max)
declare @@IMPORTERNAME 		 varchar(max)
declare @@EXPORTERNAME 		 varchar(max)
declare @@SSNO 				 varchar(max)
declare @@LINECD 			 varchar(max)
declare @@PARTNO 			 varchar(max)
declare @@Lot_Code 			 varchar(max)
declare @@Exterior_Color 	 varchar(max)
declare @@Interior_Color 	 varchar(max)
declare @@Control_Mode_Code 	 varchar(max)
declare @@Display_Mode_Code 	 varchar(max)
declare @@ODRLOT 			 varchar(max)
declare @@Nmonth 			 varchar(max)
declare @@NmonthPLUS1 		 varchar(max)
declare @@NmonthPLUS2 		 varchar(max)
declare @@NmonthPLUS3 		 varchar(max)
declare @@1N 				 varchar(max)
declare @@2N 				 varchar(max)
declare @@3N 				 varchar(max)
declare @@4N 				 varchar(max)
declare @@5N 				 varchar(max)
declare @@6N 				 varchar(max)
declare @@7N 				 varchar(max)
declare @@8N 				 varchar(max)
declare @@9N 				 varchar(max)
declare @@10N 				 varchar(max)
declare @@11N 				 varchar(max)
declare @@12N 				 varchar(max)
declare @@13N 				 varchar(max)
declare @@14N 				 varchar(max)
declare @@15N 				 varchar(max)
declare @@16N 				 varchar(max)
declare @@17N 				 varchar(max)
declare @@18N 				 varchar(max)
declare @@19N 				 varchar(max)
declare @@20N 				 varchar(max)
declare @@21N 				 varchar(max)
declare @@22N 				 varchar(max)
declare @@23N 				 varchar(max)
declare @@24N 				 varchar(max)
declare @@25N 				 varchar(max)
declare @@26N 				 varchar(max)
declare @@27N 				 varchar(max)
declare @@28N 				 varchar(max)
declare @@29N 				 varchar(max)
declare @@30N 				 varchar(max)
declare @@31N 				 varchar(max)
declare @@1NPLUS1 			 varchar(max)
declare @@2NPLUS1 			 varchar(max)
declare @@3NPLUS1 			 varchar(max)
declare @@4NPLUS1 			 varchar(max)
declare @@5NPLUS1 			 varchar(max)
declare @@6NPLUS1 			 varchar(max)
declare @@7NPLUS1 			 varchar(max)
declare @@8NPLUS1 			 varchar(max)
declare @@9NPLUS1 			 varchar(max)
declare @@10NPLUS1 			 varchar(max)
declare @@11NPLUS1 			 varchar(max)
declare @@12NPLUS1 			 varchar(max)
declare @@13NPLUS1 			 varchar(max)
declare @@14NPLUS1 			 varchar(max)
declare @@15NPLUS1 			 varchar(max)
declare @@16NPLUS1 			 varchar(max)
declare @@17NPLUS1 			 varchar(max)
declare @@18NPLUS1 			 varchar(max)
declare @@19NPLUS1 			 varchar(max)
declare @@20NPLUS1 			 varchar(max)
declare @@21NPLUS1 			 varchar(max)
declare @@22NPLUS1 			 varchar(max)
declare @@23NPLUS1 			 varchar(max)
declare @@24NPLUS1 			 varchar(max)
declare @@25NPLUS1 			 varchar(max)
declare @@26NPLUS1 			 varchar(max)
declare @@27NPLUS1 			 varchar(max)
declare @@28NPLUS1 			 varchar(max)
declare @@29NPLUS1 			 varchar(max)
declare @@30NPLUS1 			 varchar(max)
declare @@31NPLUS1 			 varchar(max)
declare @@Transportation_Code varchar(max)
declare @@Order_Date 		 varchar(max)
declare @@Series 			 varchar(max)
declare @@Dummy 				 varchar(max)
declare @@Termination_Code 	 varchar(max)
declare @@RCVNO 				 varchar(max)
declare @@DTLNO 				 varchar(max)
declare @@ERRORFLAG 			 varchar(max)

declare cursor_1 CURSOR FAST_FORWARD
for 
select * from [dbo].[TB_T_EXPORT_ORDER_ATTACHMENT]
open cursor_1 
fetch next from cursor_1 INTO 
 @@NOMOR              
,@@DATAID 			
,@@Param_VERSION 			
,@@REVISIONNO 		
,@@IMPORTERDCD 		
,@@EXPORTERCD 		
,@@ODRTYPE 			
,@@PCMN 				
,@@CFC 				
,@@REEXPCD 			
,@@AICOMirCEPT 		
,@@PXPLAG 			
,@@IMPORTERNAME 		
,@@EXPORTERNAME 		
,@@SSNO 				
,@@LINECD 			
,@@PARTNO 			
,@@Lot_Code 			
,@@Exterior_Color 	
,@@Interior_Color 	
,@@Control_Mode_Code 	
,@@Display_Mode_Code 	
,@@ODRLOT 			
,@@Nmonth 			
,@@NmonthPLUS1 		
,@@NmonthPLUS2 		
,@@NmonthPLUS3 		
,@@1N 				
,@@2N 				
,@@3N 				
,@@4N 				
,@@5N 				
,@@6N 				
,@@7N 				
,@@8N 				
,@@9N 				
,@@10N 				
,@@11N 				
,@@12N 				
,@@13N 				
,@@14N 				
,@@15N 				
,@@16N 				
,@@17N 				
,@@18N 				
,@@19N 				
,@@20N 				
,@@21N 				
,@@22N 				
,@@23N 				
,@@24N 				
,@@25N 				
,@@26N 				
,@@27N 				
,@@28N 				
,@@29N 				
,@@30N 				
,@@31N 				
,@@1NPLUS1 			
,@@2NPLUS1 			
,@@3NPLUS1 			
,@@4NPLUS1 			
,@@5NPLUS1 			
,@@6NPLUS1 			
,@@7NPLUS1 			
,@@8NPLUS1 			
,@@9NPLUS1 			
,@@10NPLUS1 			
,@@11NPLUS1 			
,@@12NPLUS1 			
,@@13NPLUS1 			
,@@14NPLUS1 			
,@@15NPLUS1 			
,@@16NPLUS1 			
,@@17NPLUS1 			
,@@18NPLUS1 			
,@@19NPLUS1 			
,@@20NPLUS1 			
,@@21NPLUS1 			
,@@22NPLUS1 			
,@@23NPLUS1 			
,@@24NPLUS1 			
,@@25NPLUS1 			
,@@26NPLUS1 			
,@@27NPLUS1 			
,@@28NPLUS1 			
,@@29NPLUS1 			
,@@30NPLUS1 			
,@@31NPLUS1 			
,@@Transportation_Code
,@@Order_Date 		
,@@Series 			
,@@Dummy 				
,@@Termination_Code 	
,@@RCVNO 				
,@@DTLNO 				
,@@ERRORFLAG 

BEGIN
while @@@FETCH_STATUS = 0
BEGIN
declare @@RAM_N int = 0
while (@@RAM_N <= 3)
begin
declare @@MONTHORDER    int = 0
declare @@DAYORDERVOL1  int = 0
declare @@DAYORDERVOL2  int = 0
declare @@DAYORDERVOL3  int = 0
declare @@DAYORDERVOL4  int = 0
declare @@DAYORDERVOL5  int = 0
declare @@DAYORDERVOL6  int = 0
declare @@DAYORDERVOL7  int = 0
declare @@DAYORDERVOL8  int = 0
declare @@DAYORDERVOL9  int = 0
declare @@DAYORDERVOL10 int = 0
declare @@DAYORDERVOL11 int = 0
declare @@DAYORDERVOL12 int = 0
declare @@DAYORDERVOL13 int = 0
declare @@DAYORDERVOL14 int = 0
declare @@DAYORDERVOL15 int = 0
declare @@DAYORDERVOL16 int = 0
declare @@DAYORDERVOL17 int = 0
declare @@DAYORDERVOL18 int = 0
declare @@DAYORDERVOL19 int = 0
declare @@DAYORDERVOL20 int = 0
declare @@DAYORDERVOL21 int = 0
declare @@DAYORDERVOL22 int = 0
declare @@DAYORDERVOL23 int = 0
declare @@DAYORDERVOL24 int = 0
declare @@DAYORDERVOL25 int = 0
declare @@DAYORDERVOL26 int = 0
declare @@DAYORDERVOL27 int = 0
declare @@DAYORDERVOL28 int = 0
declare @@DAYORDERVOL29 int = 0
declare @@DAYORDERVOL30 int = 0
declare @@DAYORDERVOL31 int = 0


set @@MONTHORDER       =  case when @@RAM_N = 0 then @@Nmonth when @@RAM_N = 1 then @@NmonthPLUS1 when @@RAM_N = 2 then @@NmonthPLUS2 when @@RAM_N = 3 then @@NmonthPLUS3 end

set @@DAYORDERVOL1 	   =   case when @@RAM_N = 0 then @@1N  when @@RAM_N = 1 then @@1NPLUS1  when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end                            
set @@DAYORDERVOL2 	   =   case when @@RAM_N = 0 then @@2N  when @@RAM_N = 1 then @@2NPLUS1  when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end    
set @@DAYORDERVOL3 	   =   case when @@RAM_N = 0 then @@3N  when @@RAM_N = 1 then @@3NPLUS1  when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL4 	   =   case when @@RAM_N = 0 then @@4N  when @@RAM_N = 1 then @@4NPLUS1  when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL5 	   =   case when @@RAM_N = 0 then @@5N  when @@RAM_N = 1 then @@5NPLUS1  when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL6 	   =   case when @@RAM_N = 0 then @@6N  when @@RAM_N = 1 then @@6NPLUS1  when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL7 	   =   case when @@RAM_N = 0 then @@7N  when @@RAM_N = 1 then @@7NPLUS1  when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL8 	   =   case when @@RAM_N = 0 then @@8N  when @@RAM_N = 1 then @@8NPLUS1  when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL9 	   =   case when @@RAM_N = 0 then @@9N  when @@RAM_N = 1 then @@9NPLUS1  when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL10	   =   case when @@RAM_N = 0 then @@10N when @@RAM_N = 1 then @@10NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL11	   =   case when @@RAM_N = 0 then @@11N when @@RAM_N = 1 then @@11NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL12	   =   case when @@RAM_N = 0 then @@12N when @@RAM_N = 1 then @@12NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL13	   =   case when @@RAM_N = 0 then @@13N when @@RAM_N = 1 then @@13NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL14	   =   case when @@RAM_N = 0 then @@14N when @@RAM_N = 1 then @@14NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL15	   =   case when @@RAM_N = 0 then @@15N when @@RAM_N = 1 then @@15NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL16	   =   case when @@RAM_N = 0 then @@16N when @@RAM_N = 1 then @@16NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL17	   =   case when @@RAM_N = 0 then @@17N when @@RAM_N = 1 then @@17NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL18	   =   case when @@RAM_N = 0 then @@18N when @@RAM_N = 1 then @@18NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL19	   =   case when @@RAM_N = 0 then @@19N when @@RAM_N = 1 then @@19NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL20	   =   case when @@RAM_N = 0 then @@20N when @@RAM_N = 1 then @@20NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL21	   =   case when @@RAM_N = 0 then @@21N when @@RAM_N = 1 then @@21NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL22	   =   case when @@RAM_N = 0 then @@22N when @@RAM_N = 1 then @@22NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL23	   =   case when @@RAM_N = 0 then @@23N when @@RAM_N = 1 then @@23NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL24	   =   case when @@RAM_N = 0 then @@24N when @@RAM_N = 1 then @@24NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL25	   =   case when @@RAM_N = 0 then @@25N when @@RAM_N = 1 then @@25NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL26	   =   case when @@RAM_N = 0 then @@26N when @@RAM_N = 1 then @@26NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL27	   =   case when @@RAM_N = 0 then @@27N when @@RAM_N = 1 then @@27NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL28	   =   case when @@RAM_N = 0 then @@28N when @@RAM_N = 1 then @@28NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL29	   =   case when @@RAM_N = 0 then @@29N when @@RAM_N = 1 then @@29NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL30	   =   case when @@RAM_N = 0 then @@30N when @@RAM_N = 1 then @@30NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end
set @@DAYORDERVOL31	   =   case when @@RAM_N = 0 then @@31N when @@RAM_N = 1 then @@31NPLUS1 when @@RAM_N = 2 then 0 when @@RAM_N = 3 then 0 end


insert into [dbo].TB_R_ORDER_MONTHLY (
 COMPANY_CD																										
,RECEIVE_NO																						
,DETAIL_NO																				
,[YYMM]																							
,[MONTH_ORD_VOL]																																			 
,[DAY_ORD_VOL_01]                
,[DAY_ORD_VOL_02]				
,[DAY_ORD_VOL_03]				
,[DAY_ORD_VOL_04]				
,[DAY_ORD_VOL_05]				
,[DAY_ORD_VOL_06]				
,[DAY_ORD_VOL_07]				
,[DAY_ORD_VOL_08]				
,[DAY_ORD_VOL_09]				
,[DAY_ORD_VOL_10]				
,[DAY_ORD_VOL_11]				
,[DAY_ORD_VOL_12]				
,[DAY_ORD_VOL_13]				
,[DAY_ORD_VOL_14]				
,[DAY_ORD_VOL_15]				
,[DAY_ORD_VOL_16]				
,[DAY_ORD_VOL_17]				
,[DAY_ORD_VOL_18]				
,[DAY_ORD_VOL_19]				
,[DAY_ORD_VOL_20]				
,[DAY_ORD_VOL_21]				
,[DAY_ORD_VOL_22]				
,[DAY_ORD_VOL_23]				
,[DAY_ORD_VOL_24]				
,[DAY_ORD_VOL_25]				
,[DAY_ORD_VOL_26]				
,[DAY_ORD_VOL_27]				
,[DAY_ORD_VOL_28]				
,[DAY_ORD_VOL_29]				
,[DAY_ORD_VOL_30]				
,[DAY_ORD_VOL_31]				
,EXPLOSION_FLAG									
,CREATED_DT																			
,CHANGED_DT																																																	
,CREATED_BY																			
,CHANGED_BY			)		
select
 @Param_CompanyCD												            --COMPANY_CD	
,@@RCVNO																		--RECEIVE_NO	
,@@DTLNO																		--DETAIL_NO		
,@@PCMN	+ @@RAM_N															--[YYMM]		
,@@MONTHORDER                                                    			--[MONTH_ORD_VOL
,@@DAYORDERVOL1 															--[DAY_ORD_VOL_0
,@@DAYORDERVOL2 															--[DAY_ORD_VOL_0
,@@DAYORDERVOL3 															--[DAY_ORD_VOL_0
,@@DAYORDERVOL4 															--[DAY_ORD_VOL_0
,@@DAYORDERVOL5 															--[DAY_ORD_VOL_0
,@@DAYORDERVOL6 															--[DAY_ORD_VOL_0
,@@DAYORDERVOL7 															--[DAY_ORD_VOL_0
,@@DAYORDERVOL8 															--[DAY_ORD_VOL_0
,@@DAYORDERVOL9 															--[DAY_ORD_VOL_0
,@@DAYORDERVOL10															--[DAY_ORD_VOL_1
,@@DAYORDERVOL11															--[DAY_ORD_VOL_1
,@@DAYORDERVOL12															--[DAY_ORD_VOL_1
,@@DAYORDERVOL13															--[DAY_ORD_VOL_1
,@@DAYORDERVOL14															--[DAY_ORD_VOL_1
,@@DAYORDERVOL15															--[DAY_ORD_VOL_1
,@@DAYORDERVOL16															--[DAY_ORD_VOL_1
,@@DAYORDERVOL17															--[DAY_ORD_VOL_1
,@@DAYORDERVOL18															--[DAY_ORD_VOL_1
,@@DAYORDERVOL19															--[DAY_ORD_VOL_1
,@@DAYORDERVOL20															--[DAY_ORD_VOL_2
,@@DAYORDERVOL21															--[DAY_ORD_VOL_2
,@@DAYORDERVOL22															--[DAY_ORD_VOL_2
,@@DAYORDERVOL23															--[DAY_ORD_VOL_2
,@@DAYORDERVOL24															--[DAY_ORD_VOL_2
,@@DAYORDERVOL25															--[DAY_ORD_VOL_2
,@@DAYORDERVOL26															--[DAY_ORD_VOL_2
,@@DAYORDERVOL27															--[DAY_ORD_VOL_2
,@@DAYORDERVOL28															--[DAY_ORD_VOL_2
,@@DAYORDERVOL29															--[DAY_ORD_VOL_2
,@@DAYORDERVOL30															--[DAY_ORD_VOL_3
,@@DAYORDERVOL31															--[DAY_ORD_VOL_3
,case when (@@MONTHORDER) != 0 		                                 	--EXPLOSION_FLAG
 and @@DAYORDERVOL1 	  = 0												--
 and @@DAYORDERVOL2 	  = 0												--
 and @@DAYORDERVOL3 	  = 0												--
 and @@DAYORDERVOL4 	  = 0												--
 and @@DAYORDERVOL5 	  = 0												--
 and @@DAYORDERVOL6 	  = 0												--
 and @@DAYORDERVOL7 	  = 0												--
 and @@DAYORDERVOL8 	  = 0												--
 and @@DAYORDERVOL9 	  = 0												--
 and @@DAYORDERVOL10	  = 0												--
 and @@DAYORDERVOL11	  = 0												--
 and @@DAYORDERVOL12	  = 0												--
 and @@DAYORDERVOL13	  = 0												--
 and @@DAYORDERVOL14	  = 0												--
 and @@DAYORDERVOL15	  = 0												--
 and @@DAYORDERVOL16	  = 0												--
 and @@DAYORDERVOL17	  = 0												--
 and @@DAYORDERVOL18	  = 0												--
 and @@DAYORDERVOL19	  = 0												--
 and @@DAYORDERVOL20	  = 0												--
 and @@DAYORDERVOL21	  = 0												--
 and @@DAYORDERVOL22	  = 0												--
 and @@DAYORDERVOL23	  = 0												--
 and @@DAYORDERVOL24	  = 0												--
 and @@DAYORDERVOL25	  = 0												--
 and @@DAYORDERVOL26	  = 0												--
 and @@DAYORDERVOL27	  = 0												--
 and @@DAYORDERVOL28	  = 0												--
 and @@DAYORDERVOL29	  = 0												--
 and @@DAYORDERVOL30	  = 0												--
 and @@DAYORDERVOL31	  = 0 												--
 then ''																	--
 else '0' end																--		
,getdate()																	--CREATED_DT	
,NULL																		--CHANGED_DT	
,@username														--CREATED_BY	
,NULL																		--CHANGED_BY	

set @@RAM_N = @@RAM_N + 1
end

fetch next from cursor_1 INTO 
 @@NOMOR              
,@@DATAID 			
,@@Param_VERSION 			
,@@REVISIONNO 		
,@@IMPORTERDCD 		
,@@EXPORTERCD 		
,@@ODRTYPE 			
,@@PCMN 				
,@@CFC 				
,@@REEXPCD 			
,@@AICOMirCEPT 		
,@@PXPLAG 			
,@@IMPORTERNAME 		
,@@EXPORTERNAME 		
,@@SSNO 				
,@@LINECD 			
,@@PARTNO 			
,@@Lot_Code 			
,@@Exterior_Color 	
,@@Interior_Color 	
,@@Control_Mode_Code 	
,@@Display_Mode_Code 	
,@@ODRLOT 			
,@@Nmonth 			
,@@NmonthPLUS1 		
,@@NmonthPLUS2 		
,@@NmonthPLUS3 		
,@@1N 				
,@@2N 				
,@@3N 				
,@@4N 				
,@@5N 				
,@@6N 				
,@@7N 				
,@@8N 				
,@@9N 				
,@@10N 				
,@@11N 				
,@@12N 				
,@@13N 				
,@@14N 				
,@@15N 				
,@@16N 				
,@@17N 				
,@@18N 				
,@@19N 				
,@@20N 				
,@@21N 				
,@@22N 				
,@@23N 				
,@@24N 				
,@@25N 				
,@@26N 				
,@@27N 				
,@@28N 				
,@@29N 				
,@@30N 				
,@@31N 				
,@@1NPLUS1 			
,@@2NPLUS1 			
,@@3NPLUS1 			
,@@4NPLUS1 			
,@@5NPLUS1 			
,@@6NPLUS1 			
,@@7NPLUS1 			
,@@8NPLUS1 			
,@@9NPLUS1 			
,@@10NPLUS1 			
,@@11NPLUS1 			
,@@12NPLUS1 			
,@@13NPLUS1 			
,@@14NPLUS1 			
,@@15NPLUS1 			
,@@16NPLUS1 			
,@@17NPLUS1 			
,@@18NPLUS1 			
,@@19NPLUS1 			
,@@20NPLUS1 			
,@@21NPLUS1 			
,@@22NPLUS1 			
,@@23NPLUS1 			
,@@24NPLUS1 			
,@@25NPLUS1 			
,@@26NPLUS1 			
,@@27NPLUS1 			
,@@28NPLUS1 			
,@@29NPLUS1 			
,@@30NPLUS1 			
,@@31NPLUS1 			
,@@Transportation_Code
,@@Order_Date 		
,@@Series 			
,@@Dummy 				
,@@Termination_Code 	
,@@RCVNO 				
,@@DTLNO 				
,@@ERRORFLAG 	
END
END
close cursor_1
deallocate cursor_1


	select 'SUCCESS' [RESULT]