using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ToDo.xaml
    /// </summary>
    public partial class ToDo : UserControl
    {
        private MainWindow _mainWindow;
        private Person _user;

        public ToDo(MainWindow window, Person user)
        {
            _user = user;
            _mainWindow = window;
            _mainWindow.Title = $"{App.Name} - {_user.Name}";

            InitializeComponent();
            Loaded += FocusOnTextBox;
            Loaded += _mainWindow.CenterWindowOnScreen;
        }

        private void FocusOnTextBox(object sender, EventArgs e)
        {
            if (!_todoTextBox.Focus())
            {
                Debug.WriteLine(" # Error : _todoTextBox can not be focused");
            }
        }

        private void AddToDoButton_click(object sender, RoutedEventArgs e)
        {
            string? inputText = _todoTextBox.Text;
            inputText = inputText?.Trim();

            if (!string.IsNullOrEmpty(inputText))
            {
                string formattedText = $" | Date : {DateTime.Now.ToString()}\n | Task : {inputText}";
                TextBlock todoItem = new TextBlock
                {
                    Text = formattedText,
                    FontSize = 16,
                    Foreground = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(10)
                };

                _todoList.Children.Add(todoItem);
                _todoTextBox.Clear();

                FocusOnTextBox(sender, e);
            }
        }

        private async void TodoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MethodInfo? btnClickMethod = typeof(Button)?.GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic);
                btnClickMethod?.Invoke(_createTodoBtn, new object[] { true });
                await Task.Delay(100);
                _createTodoBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                btnClickMethod?.Invoke(_createTodoBtn, new object[] { false });

            }
        }
    }
}
