SELECT * FROM (
	SELECT DISTINCT
		   ROW_NUMBER() OVER(ORDER BY CFC ASC ) row_no,
		   COMPANY_CD        
		  ,CFC       
		  ,CONCAT(SUBSTRING(PART_NO, 1, 5), '-') PART_NO_1
		  ,CONCAT(SUBSTRING(PART_NO, 6, 5), '-') PART_NO_2
		  ,SUBSTRING(PART_NO, 11, 2) PART_NO_3
		  ,CONCAT(CONCAT(SUBSTRING(PART_NO, 1, 5), '-'),CONCAT(SUBSTRING(PART_NO, 6, 5), '-'),SUBSTRING(PART_NO, 11, 2)) PART_NO
		  ,LINE_CD
		  ,STATUS_CD
		  ,PART_NAME
		  ,UNIT_SIGN
		  ,CONVERT(VARCHAR(MAX), TC_FROM, 111) TC_FROM
		  ,CONVERT(VARCHAR(MAX), TC_TO, 111) TC_TO
		  ,SEQ_NO
		  ,CREATED_BY
		  ,CONVERT(VARCHAR(MAX), CREATED_DT, 111) CREATED_DT_STR
	FROM TB_M_UNIT_PROD
	WHERE 1=1
		  AND ((@cfc is null OR @cfc = '') OR (CFC LIKE '%' + @cfc + '%' ))
		  AND ((@partsno is null OR @partsno = '') OR (PART_NO like '%' +@partsno+ '%' ))
		  AND ((@partsno2 is null OR @partsno2 = '') OR (SUBSTRING(PART_NO, 6, 5) like '%' +@partsno2+ '%' ))
		  AND ((@partsno3 is null OR @partsno3 = '') OR (SUBSTRING(PART_NO, 11, 2) like '%' +@partsno3+ '%' ))
		  AND ((@linecd is null OR @linecd = '') OR (LINE_CD LIKE '%' + @linecd + '%' ))
	)tb
where 1 = 1 and tb.row_no between @RowStart and @RowEnd