using System.Windows;
using System.Windows.Controls;

namespace Hapo31.IkaNama4NCV
{
	/// <summary>
	/// IkaNama.xaml の相互作用ロジック
	/// </summary>
	public partial class IkaNamaView : UserControl
	{
		public IkaNamaView()
		{
			InitializeComponent();
		}

		private void logTextBox1_TextChanged(object sender, TextChangedEventArgs e)
		{
			textBoxScrollViewer.ScrollToBottom();
		}
	}
}
