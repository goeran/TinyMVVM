vm LoginViewModel:
	data Username as string
	data Password as string
	
	command Login
	command Cancel

vm SearchViewModel:
	data Expression as ExpressionViewModel
	
	command Search
	command Clear

vm ExpressionViewModel:
	data Text as string
	

	