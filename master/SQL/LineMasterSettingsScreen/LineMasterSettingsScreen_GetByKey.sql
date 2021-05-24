select top 1 * from TB_M_LINE 
	where 1=1
		and PROCESS_CD = @PROCESS_CD
		and LINE_CD = @LINE_CD