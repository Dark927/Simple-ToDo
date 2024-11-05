using Microsoft.VisualBasic.ApplicationServices;
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
using ToDoApp.Dada;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        private MainWindow _mainWindow;
        private int _maxAge = 120;
        private Person _person = new Person
        {
            Age = 19,
            Name = "Dark"
        };

        public LoginView(MainWindow window)
        {
            _mainWindow = window;
            _mainWindow.Title = $"{App.Name} - Login";
            InitializeComponent();
            DataContext = _person;

            Loaded += _mainWindow.CenterWindowOnScreen;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.SetContent(new ToDo(_mainWindow, _person));
        }

        private void AgeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilities.IsTextMatch(e.Text, @"^[0-9]+");
           
            if(!e.Handled)
            {
                int age = int.Parse(_ageTextBox.Text + e.Text);
                e.Handled = (age > _maxAge);
            }
        }

        
    }
}
