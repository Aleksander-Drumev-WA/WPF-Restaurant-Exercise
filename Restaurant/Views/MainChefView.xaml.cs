using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Restaurant.ViewModels;

namespace WPF_Restaurant.Views
{
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
}
