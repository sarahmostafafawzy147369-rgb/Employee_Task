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
using System.Windows.Shapes;
using Employee_Task.Data;
namespace Employee_Task
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            using (var db = new MyDbcontext())
            {
                var result = db.Task.Select(t => t.Status).Distinct().ToList();
                cmb_status.ItemsSource=result;
                data_grid.ItemsSource=db.Task.ToList();
            }
        }
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_taskId.Text)||string.IsNullOrEmpty(txt_title.Text)||string.IsNullOrEmpty(txt_Description.Text)||string.IsNullOrEmpty(txt_employee_name.Text)||cmb_status.SelectedItem==null)
            {
                MessageBox.Show("Please All Data Must Be Completed!");
                return;
            }
            if (!txt_taskId.Text.Trim().All(char.IsDigit))
            {
                MessageBox.Show("Please Task Id Must Be Digits");
                return;
            }
            using (var db = new MyDbcontext())
            {
                var result = db.User.FirstOrDefault(t => t.Name==txt_employee_name.Text);
                if (result==null)
                {
                    MessageBox.Show("This Employee Not Found!");
                    return;
                }
                var selectedName = txt_employee_name.Text;
                var selectedUser = db.User.FirstOrDefault(t => t.Name==selectedName);
                var check = db.Task.FirstOrDefault(t => t.UserId==selectedUser.UserID&&t.TaskID==int.Parse(txt_taskId.Text));
                if (check!=null)
                {
                    MessageBox.Show("Task Id Cannot Repeated Please Enter A new TaskId!");
                    return;
                }
                var newTask = new Models.Employee_task
                {
                    TaskID=int.Parse(txt_taskId.Text),
                    Title=txt_title.Text,
                    Description=txt_Description.Text,
                    Status=cmb_status.SelectedItem.ToString(),
                    DueDate=DateTime.Now,
                    UserId=selectedUser.UserID
                };
                db.Task.Add(newTask);
                db.SaveChanges();
                MessageBox.Show("Task Added Successfully Alhamdulillah❤️");
                LoadData();
            }
        }
        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_taskId.Text)||string.IsNullOrEmpty(txt_title.Text)||string.IsNullOrEmpty(txt_Description.Text)||string.IsNullOrEmpty(txt_employee_name.Text)||cmb_status.SelectedItem==null)
            {
                MessageBox.Show("Please All Data Must Be Completed!");
                return;
            }
            if (!txt_taskId.Text.Trim().All(char.IsDigit))
            {
                MessageBox.Show("Please Task Id Must Be Digits");
                return;
            }
            using (var db = new MyDbcontext())
            {
                var selectemp = db.User.FirstOrDefault(t => t.Name==txt_employee_name.Text);
                if (selectemp==null)
                {
                    MessageBox.Show("This Employee Not Found!");
                    return;
                }
                var SelectedEditTask = db.Task.FirstOrDefault(t => t.TaskID==int.Parse(txt_taskId.Text)&&t.UserId==selectemp.UserID);
                if (SelectedEditTask!=null)
                {
                    SelectedEditTask.Title = txt_title.Text;
                    SelectedEditTask.Status=cmb_status.SelectedItem.ToString();
                    SelectedEditTask.Description=txt_Description.Text;
                    SelectedEditTask.DueDate=DateTime.Now;
                    SelectedEditTask.UserId=selectemp.UserID;
                    db.SaveChanges();
                    MessageBox.Show("Task Edit Successfully Alhamdulillah 💕");
                    LoadData();
                }
            }
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Enter Just TaskId  and Employee Name ");
            if (string.IsNullOrEmpty(txt_taskId.Text)||string.IsNullOrEmpty(txt_employee_name.Text))
            {
                MessageBox.Show("Please All Data Must Be Completed!");
                return;
            }
            if (!txt_taskId.Text.Trim().All(char.IsDigit))
            {
                MessageBox.Show("Please Task Id Must Be Digits");
                return;
            }
            using (var db = new MyDbcontext())
            {
                var result = db.User.FirstOrDefault(t => t.Name==txt_employee_name.Text);
                if (result==null)
                {
                    MessageBox.Show("This Employee Not Found!");
                    return;
                }
                var DeltedEmployee = db.Task.FirstOrDefault(t => t.UserId==result.UserID);
                if(DeltedEmployee!=null)
                {
                    db.Task.Remove(DeltedEmployee);
                    db.SaveChanges();
                    MessageBox.Show("This Task Delete Yet  Alhamdulillah💕");
                    LoadData();
                }
            }
        }
    }
}

