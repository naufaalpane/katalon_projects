if @PROCESS_CD = 'K'
	begin
		Select
		  RECEIVE_NO,
		  IMPORTER_CD,
		  EXPORTER_CD,
		  PACK_MONTH,
		  ORD_TYPE,				
		  CFC,						
		  STATUS_CD,
		  CHECK_STAT,
		  DAY_BY_DAY_STAT,
		  K_PACK_CREATE_STAT,
		  M_PACK_CREATE_STAT,
		  ORD_CONST_K,
		  ORD_CONST_M,									
		  CASE							
	  		WHEN (							
	  			  LTRIM(RTRIM(CHECK_STAT)) LIKE 'OK%' AND													
	  			  LTRIM(RTRIM(DAY_BY_DAY_STAT)) LIKE 'OK%' AND										
	  			  (LTRIM(RTRIM(K_PACK_CREATE_STAT)) LIKE 'OK%' OR LTRIM(RTRIM(K_PACK_CREATE_STAT)) IN ('_ _ _ _ _ _', 'ERROR')) AND							
	  			  (LTRIM(RTRIM(M_PACK_CREATE_STAT)) LIKE 'OK%' OR LTRIM(RTRIM(M_PACK_CREATE_STAT)) IN ('_ _ _ _ _ _', 'ERROR')) AND							
	  			  ORD_CONST_K = '1' AND ORD_CONST_M = '1'														
	  		 )							
	  		THEN 'OK'						
	  		ELSE 'NG'						
		  END AS READY,
		  RECEIVE_NO
	From  TB_R_ORDER_CONTROL										
	WHERE 							
		  DISABLE_FLAG = 0 AND										
		(						
			PACK_MONTH = SUBSTRING(CONVERT(NVARCHAR(10), GETDATE(), 112), 1, 6)																				
			OR						
			PACK_MONTH = SUBSTRING(CONVERT(NVARCHAR(10), DATEADD(MONTH, 1, GETDATE()), 112), 1, 6)					
		) AND						
		(CHECK_STAT IS NOT NULL OR CHECK_STAT != '-' OR CHECK_STAT != '') AND																					
		(DAY_BY_DAY_STAT IS NOT NULL OR DAY_BY_DAY_STAT != '-' OR DAY_BY_DAY_STAT != '') AND							
		(K_PACK_CREATE_STAT IS NOT NULL OR K_PACK_CREATE_STAT != '-' OR K_PACK_CREATE_STAT != '') AND							
		(M_PACK_CREATE_STAT IS NOT NULL OR M_PACK_CREATE_STAT != '-' OR M_PACK_CREATE_STAT != '') 																																											
	Order By CHECK_TIME DESC
	end
else
	begin 
		Select		
		  NQC_EXE_DT= '',
		  COM.NQC_EXE_DT [NQC_EXE_DT],								
		  ORC.IMPORTER_CD,
		  ORC.EXPORTER_CD,
		  ORC.PACK_MONTH,					
		  ORC.ORD_TYPE,				
		  ORC.CFC,						
		  ORC.STATUS_CD,
		  ORC.CHECK_STAT,
		  ORC.DAY_BY_DAY_STAT,
		  ORC.K_PACK_CREATE_STAT,
		  ORC.M_PACK_CREATE_STAT,
		  ORC.ORD_CONST_K,
		  ORC.ORD_CONST_M,								
		  CASE							
	  		WHEN (							
	  			  LTRIM(RTRIM(ORC.CHECK_STAT)) LIKE 'OK%' AND 								
	  			  LTRIM(RTRIM(ORC.DAY_BY_DAY_STAT)) LIKE 'OK%' AND  							
	  			  (LTRIM(RTRIM(ORC.K_PACK_CREATE_STAT)) LIKE 'OK%' OR LTRIM(RTRIM(ORC.K_PACK_CREATE_STAT)) IN ('_ _ _ _ _ _', 'ERROR')) AND	
	  			  (LTRIM(RTRIM(ORC.M_PACK_CREATE_STAT)) LIKE 'OK%' OR LTRIM(RTRIM(ORC.M_PACK_CREATE_STAT)) IN ('_ _ _ _ _ _', 'ERROR')) AND	
	  			  ORD_CONST_K = '1' AND ORD_CONST_M = '1'										
	  		 )							
	  		THEN 'OK'							
	  		ELSE 'NG'							
		  END AS READY,
		  ORC.RECEIVE_NO
		From  TB_R_ORDER_CONTROL ORC												
		Join  TB_M_COMPANY COM ON									
				ORC.COMPANY_CD = COM.COMPANY_CD													
		WHERE 							
			  DISABLE_FLAG = 0 AND													
			(							
				PACK_MONTH = SUBSTRING(CONVERT(NVARCHAR(10), GETDATE(), 112), 1, 6)														
				OR							
				PACK_MONTH = SUBSTRING(CONVERT(NVARCHAR(10), DATEADD(MONTH, 1, GETDATE()), 112), 1, 6)
				) AND									
			(CHECK_STAT IS NOT NULL OR CHECK_STAT != '-' OR CHECK_STAT != '') AND								
			(DAY_BY_DAY_STAT IS NOT NULL OR DAY_BY_DAY_STAT != '-' OR DAY_BY_DAY_STAT != '') AND									
			(K_PACK_CREATE_STAT IS NOT NULL OR K_PACK_CREATE_STAT != '-' OR K_PACK_CREATE_STAT != '') AND								
			(M_PACK_CREATE_STAT IS NOT NULL OR M_PACK_CREATE_STAT != '-' OR M_PACK_CREATE_STAT != '') 
			Order By CHECK_TIME DESC
	end
