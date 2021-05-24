

SELECT 
	ROW_NUMBER() OVER (ORDER BY CREATED_DT desc) as r, 
		LINE_CD,
		LINE_NAME,
		PROCESS_CD,
		COMPANY_CD
	 
FROM TB_M_LINE
	WHERE 1 =1

	  AND ((@processCd is null OR @processCd= '') OR (PROCESS_CD = @processCd))
	  AND ((@companyCd is null OR @companyCd= '') OR (COMPANY_CD = @companyCd))
