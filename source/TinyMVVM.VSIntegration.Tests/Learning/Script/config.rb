flag = true

@result = {
	"views" => true,
	"controllers" => true,
	"partial ViewModels" => true,
	"unit tests" => true
}

def configure
	yield
end

def not
	yield
end

def generate(what)
	item = @result.find do |k,v| k == what end
	if item != nil
		@result[what] = false
	end
end

configure do
	not generate "views"
	not generate "controllers"
	generate "partial ViewModels"
	generate "unit tests"
end

@result