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
  if what == "views"
	config.GenerateViews = flag
  end
  if what == "controllers"
    config.GenerateControllers = flag
  end
  if what == "partial ViewModels" 
    config.GeneratePartialViewModels = flag
  end
  if what == "unit tests"
    config.GenerateUnitTests = flag
  end 
end

configure do
	generate "views", true
	generate "controllers", true
	generate "partial ViewModels", true
	generate "unit tests", true
end

config