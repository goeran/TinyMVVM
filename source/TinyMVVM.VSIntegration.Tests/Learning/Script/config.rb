a = ""

@flag = true

@result = {
	"views" => @flag,
	"controllers" => @flag,
	"partial ViewModels" => @flag,
	"unit tests" => @flag
}

def configure
	yield
end

def not()
	yield("views", false)
end

def generate(*args)
	what = args[1]
	flag = args[2]
	item = @result.find do |k,v| k == what end
	if item != nil
		@result[what] = flag
	end
end

configure do
	not generate "views"
	not generate "controllers"
	generate "partial ViewModels", true
	generate "unit tests", true
end

@result