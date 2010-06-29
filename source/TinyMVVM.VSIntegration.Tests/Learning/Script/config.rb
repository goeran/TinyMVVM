a = ""

@result = {
	"views" => true,
	"controllers" => true,
	"partial ViewModels" => true,
	"unit tests" => true
}


def configure
	yield
end

def generate(what, flag)
  item = @result.find do |k,v| k == what end
  if item != nil
      @result[what] = flag
  end
end

configure do
	generate "views", false
	generate "controllers", false
	generate "partial ViewModels", true
	generate "unit tests", true
end

@result