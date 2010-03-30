
using NUnit.Framework;
using TestGUI.ViewModel;
using TinyMVVM.Framework.Testing;
using System;
using System.Collections.ObjectModel;
using TinyMVVM.Framework;

namespace TestGUI.Tests.ViewModel
{
	public abstract class SearchViewModelContext : TestContext
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
            viewModel.PropertyChangeRecorder.Start();
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
		
		public void When_execute_Search_Command()
		{
			viewModel.Search.Execute(null);
		}
		
		public void When_execute_Clear_Command()
		{
			viewModel.Clear.Execute(null);
		}
		
		public void When_execute_Save_Command()
		{
			viewModel.Save.Execute(null);
		}
		
	}

	public abstract class LoginViewModelContext : TestContext
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
            viewModel.PropertyChangeRecorder.Start();
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
		
		public void When_execute_Login_Command()
		{
			viewModel.Login.Execute(null);
		}
		
		public void When_execute_Cancel_Command()
		{
			viewModel.Cancel.Execute(null);
		}
		
	}

	public abstract class AddressBookViewModelContext : TestContext
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
            viewModel.PropertyChangeRecorder.Start();
		}
		
		public void And_Contacts_is_set(ObservableCollection<Contact> value)
		{
			viewModel.Contacts = value;
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
		
		public void When_execute_Add_Command()
		{
			viewModel.Add.Execute(null);
		}
		
		public void When_execute_Delete_Command()
		{
			viewModel.Delete.Execute(null);
		}
		
	}

	public abstract class ContactContext : TestContext
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
            viewModel.PropertyChangeRecorder.Start();
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
		
	}

}

