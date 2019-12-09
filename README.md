Solution constins 4 projects:

a) FutureFinancingTriangle - project which solves task

b) FutureFinancingTriangle.Generator - help projekct which allow to generate different "triangle" files for FutureFinancingTriangle project (if someone wants other files)

c) FutureFinancingTriangle - test project for FutureFinancingTriangle project

d) FutureFinancingTriangle.Generator.Tests - test project for FutureFinancingTriangle.Generator project

FutureFinancingTriangle has config file (appsetting.json) which allow to change some basic rules:

a) "InputFilePath" -> 'triangle' input file path (default value 'Date/triangle.txt')

b) "MaximalValue": -> helps with displaying result on screen by counting length of max value (default value '999')

c) "SolverKind" -> there are two solvers implemented "Graph" and "Array"

	* Graph solver - was implemented first, use recursion (post-order depth search), works slower and has worst computational complexity (adding one level to input triangle doubles solving time)
	
	* Array solver - was implemented later, use iteration, works faster and has quadratic computional complexity
	