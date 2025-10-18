using Employee_Task.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Employee_Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_name.Text)||string.IsNullOrEmpty(txt_pass.Password))
            {
                MessageBox.Show("Please Enter User Password and Name");
                return;

            }
            using (var db = new MyDbcontext())
            {
                var result = db.User.FirstOrDefault(t => t.Password==txt_pass.Password&&t.Name==txt_name.Text);
                if (result == null)
                {
                    MessageBox.Show("This User Not Found");
                    return;
                }
                if (result.Role=="Manager")
                {
                    ManagerWindow managerWindow = new ManagerWindow();
                    managerWindow.Show();   
                }
                if(result.Role=="Employee")
                {

                    EmployeeWindow employeeWindow = new EmployeeWindow(result);
                    employeeWindow.Show();
                }
            }
        }
    }
}