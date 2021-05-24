select count(1) FROM TB_M_LINE
where 1=1
	  and ((@PROCESS_CD is null OR @PROCESS_CD = '') OR (PROCESS_CD like '%' +@PROCESS_CD+'%' ))
	  and ((@LINE_CD is null OR @LINE_CD = '') OR (LINE_CD like '%' +@LINE_CD+'%' ))