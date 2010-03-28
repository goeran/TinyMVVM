using System;
using System.Collections.ObjectModel;
using Moq;
using NUnit.Framework;
using TestGUI.ViewModel;
using TinyMVVM.Framework;
using TinyMVVM.Framework.Services;
using TinyMVVM.Framework.Testing;

namespace TestGUI.Tests.ViewModel
{
	public abstract class SearchViewModelContext
	{
		protected SearchViewModel viewModel;

		[SetUp]
		public void Setup()
		{
			ServiceLocator.SetLocator(ServiceLocatorForTesting.GetServiceLocator());
			
			Context();
		}
				
		protected abstract void Context();
		
		protected Mock<T> GetFakeFor<T>() where T: class
		{
			return ServiceLocator.Instance.GetInstance<Mock<T>>();
		}
	
		public void Given_SearchViewModel_is_created()
		{
			viewModel = new SearchViewModel();
		}
		
		public void And_data_is_entered()
		{
		}
		
		public void And_Query_is_entered(string value)
		{
			viewModel.Query = value;
		}
		
	
		public void And_SearchViewModel_PropertyChangeRecording_is_Started()
		{
			viewModel.PropertyChangeRecorder.Start();
		}

		public void When_Query_is_entered(string value)
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

	public abstract class LoginViewModelContext
	{
		protected LoginViewModel viewModel;

		[SetUp]
		public void Setup()
		{
			ServiceLocator.SetLocator(ServiceLocatorForTesting.GetServiceLocator());
			
			Context();
		}
				
		protected abstract void Context();
		
		protected Mock<T> GetFakeFor<T>() where T: class
		{
			return ServiceLocator.Instance.GetInstance<Mock<T>>();
		}
	
		public void Given_LoginViewModel_is_created()
		{
			viewModel = new LoginViewModel();
		}
		
		public void And_data_is_entered()
		{
		}
		
		public void And_Username_is_entered(string value)
		{
			viewModel.Username = value;
		}
		public void And_Password_is_entered(string value)
		{
			viewModel.Password = value;
		}
		public void And_IsVisible_is_entered(bool value)
		{
			viewModel.IsVisible = value;
		}
		public void And_Status_is_entered(string value)
		{
			viewModel.Status = value;
		}
		public void And_ReadOnly_is_entered(bool value)
		{
			viewModel.ReadOnly = value;
		}
		
	
		public void And_LoginViewModel_PropertyChangeRecording_is_Started()
		{
			viewModel.PropertyChangeRecorder.Start();
		}

		public void When_Username_is_entered(string value)
		{
			viewModel.Username = value;
		}
		
		public void When_Password_is_entered(string value)
		{
			viewModel.Password = value;
		}
		
		public void When_IsVisible_is_entered(bool value)
		{
			viewModel.IsVisible = value;
		}
		
		public void When_Status_is_entered(string value)
		{
			viewModel.Status = value;
		}
		
		public void When_ReadOnly_is_entered(bool value)
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

	public abstract class AddressBookViewModelContext
	{
		protected AddressBookViewModel viewModel;

		[SetUp]
		public void Setup()
		{
			ServiceLocator.SetLocator(ServiceLocatorForTesting.GetServiceLocator());
			
			Context();
		}
				
		protected abstract void Context();
		
		protected Mock<T> GetFakeFor<T>() where T: class
		{
			return ServiceLocator.Instance.GetInstance<Mock<T>>();
		}
	
		public void Given_AddressBookViewModel_is_created()
		{
			viewModel = new AddressBookViewModel();
		}
		
		public void And_data_is_entered()
		{
		}
		
		public void And_Contacts_is_entered(ObservableCollection<Contact> value)
		{
			viewModel.Contacts = value;
		}
		
	
		public void And_AddressBookViewModel_PropertyChangeRecording_is_Started()
		{
			viewModel.PropertyChangeRecorder.Start();
		}

		public void When_Contacts_is_entered(ObservableCollection<Contact> value)
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

	public abstract class ContactContext
	{
		protected Contact viewModel;

		[SetUp]
		public void Setup()
		{
			ServiceLocator.SetLocator(ServiceLocatorForTesting.GetServiceLocator());
			
			Context();
		}
				
		protected abstract void Context();
		
		protected Mock<T> GetFakeFor<T>() where T: class
		{
			return ServiceLocator.Instance.GetInstance<Mock<T>>();
		}
	
		public void Given_Contact_is_created()
		{
			viewModel = new Contact();
		}
		
		public void And_data_is_entered()
		{
		}
		
		public void And_Name_is_entered(string value)
		{
			viewModel.Name = value;
		}
		public void And_Email_is_entered(string value)
		{
			viewModel.Email = value;
		}
		public void And_Phone_is_entered(string value)
		{
			viewModel.Phone = value;
		}
		public void And_Mobile_is_entered(string value)
		{
			viewModel.Mobile = value;
		}
		
	
		public void And_Contact_PropertyChangeRecording_is_Started()
		{
			viewModel.PropertyChangeRecorder.Start();
		}

		public void When_Name_is_entered(string value)
		{
			viewModel.Name = value;
		}
		
		public void When_Email_is_entered(string value)
		{
			viewModel.Email = value;
		}
		
		public void When_Phone_is_entered(string value)
		{
			viewModel.Phone = value;
		}
		
		public void When_Mobile_is_entered(string value)
		{
			viewModel.Mobile = value;
		}
		
	
	
		public void When_Contact_is_spawned()
		{
			viewModel = new Contact();
		} 
		
	}

}

