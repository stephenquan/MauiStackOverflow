using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Maui.StackOverflow;

public partial class BoolModel : ObservableObject
{
	public enum MyEnum
	{
		FalseCode,
		FalseUser,
		TrueCode,
		TrueUser
	};

	[ObservableProperty]
	MyEnum myValue = MyEnum.FalseCode;
}

public class BoolConverter : IValueConverter
{ 	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value is not null && value is BoolModel.MyEnum myEnum)
		{
			return myEnum switch
			{
				BoolModel.MyEnum.FalseCode  => false,
				BoolModel.MyEnum.FalseUser => false,
				BoolModel.MyEnum.TrueCode => true,
				BoolModel.MyEnum.TrueUser => true,
				_ => throw new NotImplementedException(),
			};
		}
		return false;
	}

	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value is not null && value is bool boolValue)
		{
			return boolValue ? BoolModel.MyEnum.TrueUser: BoolModel.MyEnum.FalseUser;
		}
		return BoolModel.MyEnum.FalseUser;
	}
}

public partial class UserBoolPage : ContentPage
{
	public BoolModel ViewModel { get; } = new BoolModel();

	public UserBoolPage()
	{
		InitializeComponent();

		BindingContext = ViewModel;
	}
}