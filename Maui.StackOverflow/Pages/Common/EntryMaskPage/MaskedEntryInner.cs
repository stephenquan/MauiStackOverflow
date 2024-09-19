using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.StackOverflow;

public class MaskedEntryInner : Entry
{
	public static readonly BindableProperty MaskProperty = BindableProperty.Create(nameof(Mask), typeof(string), typeof(MaskedEntryInner), default(string));
	public string Mask
	{
		get => (string)GetValue(MaskProperty);
		set => SetValue(MaskProperty, value);
	}

	public MaskedEntryInner()
	{
		PropertyChanged += OnPropertyChanged;
	}

	private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		throw new NotImplementedException();
	}
}
