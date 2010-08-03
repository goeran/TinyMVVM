a = ""

@config = {
	"views" => false,
	"controllers" => false,
	"partial ViewModels" => false,
	"unit tests" => false
}

def configure
	yield
end

def generate(what)
	@config[what] = true
end

configure do
	generate "views"
	generate "controllers"
	generate "partial ViewModels"
	#generate "unit tests"
end

@config