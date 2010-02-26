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
			Search = new DelegateCommand(OnSearch, CanSearch);
			Clear = new DelegateCommand(OnClear, CanClear);
			Save = new DelegateCommand(OnSave, CanSave);
			
			OnInitialize();
		}
	}
		
	public partial class LoginViewModel : ViewModelBase
	{
		//State
		public string Username { get; set; }
		public string Password { get; set; }
		public bool IsVisible { get; set; }
		public string Status { get; set; }
		public bool ReadOnly { get; set; }
			
		//Commands
		public DelegateCommand Login { get; set; }
		public DelegateCommand Cancel { get; set; }
		
		public LoginViewModel()
		{
			Login = new DelegateCommand(OnLogin, CanLogin);
			Cancel = new DelegateCommand(OnCancel, CanCancel);
			
			OnInitialize();
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
			Add = new DelegateCommand(OnAdd, CanAdd);
			Delete = new DelegateCommand(OnDelete, CanDelete);
			
			OnInitialize();
		}
	}
		
}

