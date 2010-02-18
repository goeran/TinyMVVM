using System;
using TinyMVVM.Framework;

namespace TestGUI.ViewModel
{
	public partial class SearchViewModel : ViewModelBase
	{
		//State
		public System.String Query { get; set; }
			
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
		public System.String Username { get; set; }
		public System.String Password { get; set; }
		public System.Boolean IsVisible { get; set; }
		public System.String Status { get; set; }
		public System.String Domain { get; set; }
		public System.Boolean ReadOnly { get; set; }
			
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
		public System.String Contacts { get; set; }
			
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

