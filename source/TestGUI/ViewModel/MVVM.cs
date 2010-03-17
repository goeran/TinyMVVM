using System;
using TinyMVVM.Framework;

namespace TestGUI.ViewModel
{
	public partial class SearchViewModel : ViewModelBase
	{
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
		
			ApplyDefaultConventions();
		}
	}
		
	public partial class LoginViewModel : ViewModelBase
	{
		//State
		private string _Username;
		public string Username
		{
			get { return _Username; }
			set
			{
				if (value != _Username)
				{
					_Username = value;
					TriggerPropertyChanged("Username");
				}
			}
		}
		private string _Password;
		public string Password
		{
			get { return _Password; }
			set
			{
				if (value != _Password)
				{
					_Password = value;
					TriggerPropertyChanged("Password");
				}
			}
		}
		private bool _IsVisible;
		public bool IsVisible
		{
			get { return _IsVisible; }
			set
			{
				if (value != _IsVisible)
				{
					_IsVisible = value;
					TriggerPropertyChanged("IsVisible");
				}
			}
		}
		private string _Status;
		public string Status
		{
			get { return _Status; }
			set
			{
				if (value != _Status)
				{
					_Status = value;
					TriggerPropertyChanged("Status");
				}
			}
		}
		private bool _ReadOnly;
		public bool ReadOnly
		{
			get { return _ReadOnly; }
			set
			{
				if (value != _ReadOnly)
				{
					_ReadOnly = value;
					TriggerPropertyChanged("ReadOnly");
				}
			}
		}
	
		
		//Commands
		public DelegateCommand Login { get; set; }
		public DelegateCommand Cancel { get; set; }
		
		public LoginViewModel()
		{
			Login = new DelegateCommand();
			Cancel = new DelegateCommand();
				ServiceLocator.SetLocatorIfNotSet(() => ServiceLocator.GetServiceLocator());
		
			ApplyDefaultConventions();
		}
	}
		
	public partial class AddressBookViewModel : ViewModelBase
	{
		//State
		public string Contacts { get; set; }
		
		//Commands
		public DelegateCommand Add { get; set; }
		public DelegateCommand Delete { get; set; }
		
		public AddressBookViewModel()
		{
			Add = new DelegateCommand();
			Delete = new DelegateCommand();
				ServiceLocator.SetLocatorIfNotSet(() => ServiceLocator.GetServiceLocator());
		
			ApplyDefaultConventions();
		}
	}
		
}

