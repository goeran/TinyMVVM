viewmodel LoginViewModel:
	oproperty Username as string
	oproperty Password as string
	
	command Login
	command Cancel

viewmodel SearchViewModel:
	property Expression as ExpressionViewModel
	
	command Search
	command Clear

vm ExpressionViewModel:
	data Text as string
	

	