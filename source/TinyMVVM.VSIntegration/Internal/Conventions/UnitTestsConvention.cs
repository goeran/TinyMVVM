using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.VSIntegration.Internal.Factories;
using TinyMVVM.VSIntegration.Internal.Model.Internal;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;
using TinyMVVM.VSIntegration.Internal.Services;
using TinyMVVM.VSIntegration.Internal.Templates;

namespace TinyMVVM.VSIntegration.Internal.Conventions
{
	public class UnitTestsConvention : IViewModelConvention
	{
		private readonly ITemplate testScenarioCodeGenTemplate = new TestScenarioTemplate();
		private readonly ITemplate unitTestCodeGenTemplate = new UnitTestTemplate();
		private IModelFactory factory;
		private ICodeGeneratorService codeGeneratorService;

		public UnitTestsConvention(ICodeGeneratorService codeGeneratorService, IModelFactory factory)
		{
			this.codeGeneratorService = codeGeneratorService;
			this.factory = factory;
		}

		public void Apply(ModelSpecification viewModelSpecification, File mvvmFile)
		{
			var project = mvvmFile.Project;
			var solution = project.Solution;

			var testProject = solution.Projects.Where(p => p.Name == string.Format("{0}.Tests", project.Name) ||
                p.Name == string.Format("{0}Tests", project.Name)).FirstOrDefault();

			if (testProject != null)
			{
				Folder folderInUnitTestProj = CreateFolderInUnitTestProject(mvvmFile, testProject);

				if (!folderInUnitTestProj.HasFolder("TestScenarios"))
					folderInUnitTestProj.AddFolder(factory.NewFolder("TestScenarios", folderInUnitTestProj));

				var testScenariosFolder = folderInUnitTestProj.GetSubFolder("TestScenarios");

				foreach (var viewModel in viewModelSpecification.ViewModels)
				{
					var testScenarioFileName = string.Format("{0}TestScenario.cs", viewModel.Name);
					var unitTestFileName = string.Format("{0}Tests.cs", viewModel.Name);

					if (!testScenariosFolder.HasFile(testScenarioFileName))
					{
						var testScenarioFile = factory.NewFile(testScenarioFileName, testScenariosFolder);
						testScenariosFolder.AddFile(testScenarioFile);
					}

					codeGeneratorService.Generate(mvvmFile, testScenariosFolder.GetFile(testScenarioFileName), 
						new CodeGeneratorArgs(viewModelSpecification, viewModel, testScenarioCodeGenTemplate));
				
					//UnitTests
					if (!folderInUnitTestProj.HasFile(unitTestFileName))
					{
						var unitTestFile = factory.NewFile(unitTestFileName, folderInUnitTestProj);
						folderInUnitTestProj.AddFile(unitTestFile);

						codeGeneratorService.Generate(mvvmFile, unitTestFile, 
							new CodeGeneratorArgs(viewModelSpecification, viewModel, unitTestCodeGenTemplate));
					}
				}

			}
		}

		private Folder CreateFolderInUnitTestProject(File mvvmFile, Project testProject)
		{
			var folderStack = TreeWalker.GetFolderStack(mvvmFile);

			var folderInUnitTestProj = testProject as Folder;
			while (folderStack.Count > 0)
			{
				var folderToCreate = folderStack.Pop();
	
				if (!folderInUnitTestProj.HasFolder(folderToCreate.Name))
					folderInUnitTestProj.AddFolder(factory.NewFolder(folderToCreate.Name, folderInUnitTestProj));
					
				folderInUnitTestProj = folderInUnitTestProj.GetSubFolder(folderToCreate.Name);
			}
			return folderInUnitTestProj;
		}
	}
}
