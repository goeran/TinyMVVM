a = ""

@result = []

def configure(&block)
	@result.push "configure"
	yield
end

def nott
	@result.push "not"
	yield
end

def generate(what)
	@result.push "generate"
end

configure do
	generate "views"
	nott { generate "controllers" }
end

@result