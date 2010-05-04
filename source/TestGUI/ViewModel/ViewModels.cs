
using TinyMVVM.Framework.Services;
using System;
using System.Collections.ObjectModel;
using TinyMVVM.Framework;

namespace TestGUI.ViewModel
{
	public partial class SearchViewModel : TinyMVVM.Framework.ViewModelBase
	{
		protected IUIInvoker UIInvoker { get; set; }

		//State
		public string Query { get; set; } 
	
		
		//Commands
		public DelegateCommand Search { get; set; }
		public DelegateCommand Clear { get; set; }
		public DelegateCommand Save { get; set; }
		
		public SearchViewModel()
		{
			Search = new DelegateCommand();
			Clear = new DelegateCommand();
			Save = new DelegateCommand();
		
			ServiceLocator.SetLocatorIfNotSet(() => ServiceLocator.GetServiceLocator());
			UIInvoker = ServiceLocator.Instance.GetInstance<IUIInvoker>();
		
			ApplyDefaultConventions();
		}
	}
		
	public partial class LoginViewModel : TinyMVVM.Framework.ViewModelBase
	{
		protected IUIInvoker UIInvoker { get; set; }

		//State
		public string Username
		{
			get { return _Username; }
			set
			{
				if (value != _Username)
				{
					UIInvoker.Invoke(() =>
					{
						_Username = value;
						TriggerPropertyChanged("Username");
					});
				}
			}
		}
		private string _Username;

		public string Password
		{
			get { return _Password; }
			set
			{
				if (value != _Password)
				{
					UIInvoker.Invoke(() =>
					{
						_Password = value;
						TriggerPropertyChanged("Password");
					});
				}
			}
		}
		private string _Password;

		public bool IsVisible
		{
			get { return _IsVisible; }
			set
			{
				if (value != _IsVisible)
				{
					UIInvoker.Invoke(() =>
					{
						_IsVisible = value;
						TriggerPropertyChanged("IsVisible");
					});
				}
			}
		}
		private bool _IsVisible;

		public string Status
		{
			get { return _Status; }
			set
			{
				if (value != _Status)
				{
					UIInvoker.Invoke(() =>
					{
						_Status = value;
						TriggerPropertyChanged("Status");
					});
				}
			}
		}
		private string _Status;

		public bool ReadOnly
		{
			get { return _ReadOnly; }
			set
			{
				if (value != _ReadOnly)
				{
					UIInvoker.Invoke(() =>
					{
						_ReadOnly = value;
						TriggerPropertyChanged("ReadOnly");
					});
				}
			}
		}
		private bool _ReadOnly;

	
		
		//Commands
		public DelegateCommand Login { get; set; }
		public DelegateCommand Cancel { get; set; }
		
		public LoginViewModel()
		{
			Login = new DelegateCommand();
			Cancel = new DelegateCommand();
		
			ServiceLocator.SetLocatorIfNotSet(() => ServiceLocator.GetServiceLocator());
			UIInvoker = ServiceLocator.Instance.GetInstance<IUIInvoker>();
		
			ApplyDefaultConventions();
		}
	}
		
	public partial class AddressBookViewModel : TinyMVVM.Framework.ViewModelBase
	{
		protected IUIInvoker UIInvoker { get; set; }

		//State
		public ObservableCollection<Contact> Contacts { get; set; } 
	
		
		//Commands
		public DelegateCommand Add { get; set; }
		public DelegateCommand Delete { get; set; }
		
		public AddressBookViewModel()
		{
			Add = new DelegateCommand();
			Delete = new DelegateCommand();
		
			ServiceLocator.SetLocatorIfNotSet(() => ServiceLocator.GetServiceLocator());
			UIInvoker = ServiceLocator.Instance.GetInstance<IUIInvoker>();
		
			ApplyDefaultConventions();
		}
	}
		
	public partial class Contact : TinyMVVM.Framework.ViewModelBase
	{
		protected IUIInvoker UIInvoker { get; set; }

		//State
		public string Name { get; set; } 
		public string Email { get; set; } 
		public string Phone { get; set; } 
		public string Mobile { get; set; } 
	
		
		//Commands
		
		public Contact()
		{
		
			ServiceLocator.SetLocatorIfNotSet(() => ServiceLocator.GetServiceLocator());
			UIInvoker = ServiceLocator.Instance.GetInstance<IUIInvoker>();
		
			ApplyDefaultConventions();
		}
	}
		
	public partial class Friend : Contact
	{
		protected IUIInvoker UIInvoker { get; set; }

		//State
	
		
		//Commands
		
		public Friend()
		{
		
			ServiceLocator.SetLocatorIfNotSet(() => ServiceLocator.GetServiceLocator());
			UIInvoker = ServiceLocator.Instance.GetInstance<IUIInvoker>();
		
			ApplyDefaultConventions();
		}
	}
		
}