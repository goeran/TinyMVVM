
using System;
using NUnit.Framework;
using TestGUI.ViewModel;
using TinyMVVM.Framework.Testing;
using System;
using System.Collections.ObjectModel;
using TinyMVVM.Framework;

namespace TestGUI.Tests.ViewModel
{
	public abstract class SearchViewModelTestContext : TestContext
	{
		protected SearchViewModel viewModel;

		[SetUp]
		public void Setup()
		{
			ServiceLocator.SetLocator(ServiceLocatorForTesting.GetServiceLocator());
			
			Context();
		}

		public abstract void Context();

		public void Given_SearchViewModel_is_created()
		{
			viewModel = new SearchViewModel();
		}

		public void And_SearchViewModel_is_created()
		{
			viewModel = new SearchViewModel();
		}
		
		public void And_Query_is_set(string value)
		{
			viewModel.Query = value;
		}

	
		
	
		public void And_SearchViewModel_PropertyChangeRecording_is_Started()
		{
			viewModel.PropertyChangeRecorder.Start();
		}

		public void When_Query_is_set(string value)
		{
			viewModel.Query = value;
		}
		
		
		public void When_SearchViewModel_is_spawned()
		{
			viewModel = new SearchViewModel();
		} 
		
		public void And_Search_Command_is_executed()
		{
			viewModel.Search.Execute(null);
		}

		public void When_execute_Search_Command()
		{
			viewModel.Search.Execute(null);
		}
		public void And_Clear_Command_is_executed()
		{
			viewModel.Clear.Execute(null);
		}

		public void When_execute_Clear_Command()
		{
			viewModel.Clear.Execute(null);
		}
		public void And_Save_Command_is_executed()
		{
			viewModel.Save.Execute(null);
		}

		public void When_execute_Save_Command()
		{
			viewModel.Save.Execute(null);
		}
			
		public void And(string description, Action unitOfWork)
		{
			unitOfWork.Invoke();
		}

		public void When(string description, Action unitOfWork)
		{
			unitOfWork.Invoke();
		}
}

	public abstract class LoginViewModelTestContext : TestContext
	{
		protected LoginViewModel viewModel;

		[SetUp]
		public void Setup()
		{
			ServiceLocator.SetLocator(ServiceLocatorForTesting.GetServiceLocator());
			
			Context();
		}

		public abstract void Context();

		public void Given_LoginViewModel_is_created()
		{
			viewModel = new LoginViewModel();
		}

		public void And_LoginViewModel_is_created()
		{
			viewModel = new LoginViewModel();
		}
		
		public void And_Username_is_set(string value)
		{
			viewModel.Username = value;
		}

	
		public void And_Password_is_set(string value)
		{
			viewModel.Password = value;
		}

	
		public void And_IsVisible_is_set(bool value)
		{
			viewModel.IsVisible = value;
		}

	
		public void And_Status_is_set(string value)
		{
			viewModel.Status = value;
		}

	
		public void And_ReadOnly_is_set(bool value)
		{
			viewModel.ReadOnly = value;
		}

	
		
	
		public void And_LoginViewModel_PropertyChangeRecording_is_Started()
		{
			viewModel.PropertyChangeRecorder.Start();
		}

		public void When_Username_is_set(string value)
		{
			viewModel.Username = value;
		}
		
		public void When_Password_is_set(string value)
		{
			viewModel.Password = value;
		}
		
		public void When_IsVisible_is_set(bool value)
		{
			viewModel.IsVisible = value;
		}
		
		public void When_Status_is_set(string value)
		{
			viewModel.Status = value;
		}
		
		public void When_ReadOnly_is_set(bool value)
		{
			viewModel.ReadOnly = value;
		}
		
		
		public void When_LoginViewModel_is_spawned()
		{
			viewModel = new LoginViewModel();
		} 
		
		public void And_Login_Command_is_executed()
		{
			viewModel.Login.Execute(null);
		}

		public void When_execute_Login_Command()
		{
			viewModel.Login.Execute(null);
		}
		public void And_Cancel_Command_is_executed()
		{
			viewModel.Cancel.Execute(null);
		}

		public void When_execute_Cancel_Command()
		{
			viewModel.Cancel.Execute(null);
		}
			
		public void And(string description, Action unitOfWork)
		{
			unitOfWork.Invoke();
		}

		public void When(string description, Action unitOfWork)
		{
			unitOfWork.Invoke();
		}
}

	public abstract class AddressBookViewModelTestContext : TestContext
	{
		protected AddressBookViewModel viewModel;

		[SetUp]
		public void Setup()
		{
			ServiceLocator.SetLocator(ServiceLocatorForTesting.GetServiceLocator());
			
			Context();
		}

		public abstract void Context();

		public void Given_AddressBookViewModel_is_created()
		{
			viewModel = new AddressBookViewModel();
		}

		public void And_AddressBookViewModel_is_created()
		{
			viewModel = new AddressBookViewModel();
		}
		
		public void And_Contacts_is_set(ObservableCollection<Contact> value)
		{
			viewModel.Contacts = value;
		}

		public void When_add_Contact(Action unitOfWork)
		{
			unitOfWork.Invoke();
		}
	
		
	
		public void And_AddressBookViewModel_PropertyChangeRecording_is_Started()
		{
			viewModel.PropertyChangeRecorder.Start();
		}

		public void When_Contacts_is_set(ObservableCollection<Contact> value)
		{
			viewModel.Contacts = value;
		}
		
		
		public void When_AddressBookViewModel_is_spawned()
		{
			viewModel = new AddressBookViewModel();
		} 
		
		public void And_Add_Command_is_executed()
		{
			viewModel.Add.Execute(null);
		}

		public void When_execute_Add_Command()
		{
			viewModel.Add.Execute(null);
		}
		public void And_Delete_Command_is_executed()
		{
			viewModel.Delete.Execute(null);
		}

		public void When_execute_Delete_Command()
		{
			viewModel.Delete.Execute(null);
		}
			
		public void And(string description, Action unitOfWork)
		{
			unitOfWork.Invoke();
		}

		public void When(string description, Action unitOfWork)
		{
			unitOfWork.Invoke();
		}
}

	public abstract class ContactTestContext : TestContext
	{
		protected Contact viewModel;

		[SetUp]
		public void Setup()
		{
			ServiceLocator.SetLocator(ServiceLocatorForTesting.GetServiceLocator());
			
			Context();
		}

		public abstract void Context();

		public void Given_Contact_is_created()
		{
			viewModel = new Contact();
		}

		public void And_Contact_is_created()
		{
			viewModel = new Contact();
		}
		
		public void And_Name_is_set(string value)
		{
			viewModel.Name = value;
		}

	
		public void And_Email_is_set(string value)
		{
			viewModel.Email = value;
		}

	
		public void And_Phone_is_set(string value)
		{
			viewModel.Phone = value;
		}

	
		public void And_Mobile_is_set(string value)
		{
			viewModel.Mobile = value;
		}

	
		
	
		public void And_Contact_PropertyChangeRecording_is_Started()
		{
			viewModel.PropertyChangeRecorder.Start();
		}

		public void When_Name_is_set(string value)
		{
			viewModel.Name = value;
		}
		
		public void When_Email_is_set(string value)
		{
			viewModel.Email = value;
		}
		
		public void When_Phone_is_set(string value)
		{
			viewModel.Phone = value;
		}
		
		public void When_Mobile_is_set(string value)
		{
			viewModel.Mobile = value;
		}
		
		
		public void When_Contact_is_spawned()
		{
			viewModel = new Contact();
		} 
		
			
		public void And(string description, Action unitOfWork)
		{
			unitOfWork.Invoke();
		}

		public void When(string description, Action unitOfWork)
		{
			unitOfWork.Invoke();
		}
}

	public abstract class FriendTestContext : TestContext
	{
		protected Friend viewModel;

		[SetUp]
		public void Setup()
		{
			ServiceLocator.SetLocator(ServiceLocatorForTesting.GetServiceLocator());
			
			Context();
		}

		public abstract void Context();

		public void Given_Friend_is_created()
		{
			viewModel = new Friend();
		}

		public void And_Friend_is_created()
		{
			viewModel = new Friend();
		}
		
		
	
		public void And_Friend_PropertyChangeRecording_is_Started()
		{
			viewModel.PropertyChangeRecorder.Start();
		}

		
		public void When_Friend_is_spawned()
		{
			viewModel = new Friend();
		} 
		
			
		public void And(string description, Action unitOfWork)
		{
			unitOfWork.Invoke();
		}

		public void When(string description, Action unitOfWork)
		{
			unitOfWork.Invoke();
		}
}

}

