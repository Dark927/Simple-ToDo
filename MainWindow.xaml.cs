using System;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.InteropServices;
using NotifyIcon = System.Windows.Forms.NotifyIcon;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIcon? _notifyIcon;
        private string _icon = "todo.ico";
        private bool _isExit = false;


        public MainWindow()
        {
            InitializeComponent();
            InitializeTrayIcon();
            SetContent(new LoginView(this));

            Loaded += (s, e) =>
            {
                var handle = new WindowInteropHelper(this).Handle;
            };
        }

        public void SetContent(object content)
        {
            _currentContent.Content = content;
        }

        public void CenterWindowOnScreen(object? sender, EventArgs e)
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        public void BringToFront()
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
            }
            Activate();
            Topmost = true;
            Topmost = false;
            Focus();
        }

        private void InitializeTrayIcon()
        {
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = new Icon(_icon);
            _notifyIcon.Visible = true;
            _notifyIcon.Text = "Application - ToDo";

            _notifyIcon.MouseClick += NotifyIcon_Click;

            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Open", null, (s, e) => ShowMainWindow());
            _notifyIcon.ContextMenuStrip.Items.Add("Close", null, (s, e) => ApplicationQuit());
        }

        private void NotifyIcon_Click(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ShowMainWindow();
            }
        }

        private void ShowMainWindow()
        {
            Show();
            WindowState = WindowState.Normal;
            Activate();
        }

        private void ApplicationQuit()
        {
            _isExit = true;
            _notifyIcon?.Dispose();
            Application.Current.Shutdown();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!_isExit)
            {
                e.Cancel = true;
                Hide();
            }
            base.OnClosing(e);
        }

        private bool IsGridCellEmpty(Grid grid, int row, int col)
        {
            foreach (UIElement child in grid.Children)
            {
                if ((Grid.GetRow(child) == row) || (Grid.GetColumn(child) == col))
                {
                    return false;
                }
            }
            return true;
        }
    }
}