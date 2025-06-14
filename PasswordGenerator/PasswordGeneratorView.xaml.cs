using System.Windows;

namespace PasswordGenerator
{
    public partial class PasswordGeneratorView : Window
    {
        public PasswordGeneratorView()
        {
            InitializeComponent();
            this.DataContext = new PasswordGeneratorViewModel();
        }
    }
}
