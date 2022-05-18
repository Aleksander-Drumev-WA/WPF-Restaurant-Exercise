using System.Windows.Controls;

namespace WPF_Restaurant.Views;

	/// <summary>
	/// Interaction logic for MainChefView.xaml
	/// </summary>
public partial class MainChefView : UserControl
{
	public MainChefView()
	{
		InitializeComponent();
	}

	private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (sender is TextBox textBox)
		{
			if (textBox.DataContext is MainChefViewModel viewModel)
			{
				viewModel.LoadOrdersCommand.Execute(viewModel);
			}
		}
	}
}
