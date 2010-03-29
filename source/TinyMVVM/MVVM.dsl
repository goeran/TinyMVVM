using System.Linq
using System.ComponentModel.Composition

viewmodel LoginViewModel:
	oproperty Username as string
	oproperty Password as string
	
	command Login
	command Cancel

viewmodel SearchViewModel:
	property Expression as ExpressionViewModel
	
	command Search
	command Clear

viewmodel ExpressionViewModel:
	data Text as string

viewmodel Traybar:
	[ImportMany]
	property Widgets as ObservableCollection<Widget>
	
viewmodel Widget	