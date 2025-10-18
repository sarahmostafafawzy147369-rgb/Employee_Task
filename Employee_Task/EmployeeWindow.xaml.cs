using Employee_Task.Data;
using Employee_Task.Models;
using Microsoft.EntityFrameworkCore;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Employee_Task
{
    public partial class EmployeeWindow : Window
    {
        private Employee _employee; 
        public EmployeeWindow(Employee employee )
        {
            InitializeComponent();
            _employee = employee;  
            txt_name.Text =$"[ {employee.Name}]";
            LoadData();
        }
        public void LoadData()
        {
            using (var db = new MyDbcontext())
            {
               var Data=db.Task.Select(t=>t.Status).Distinct().ToList();                 
                     
                cmb_status.ItemsSource=Data;
                var result = db.Task.FirstOrDefault(t => t.UserId==_employee.UserID);
                if (result != null)
                {
                    var ShowData = db.Task.Where(t => t.UserId==_employee.UserID&&(t.Status=="Pending"||t.Status=="In Progress"))
                        .Select(t => new
                        {
                            t.TaskID,
                            t.Title,
                            t.Description,
                            t.Status    
                        }).ToList();                                            
                    Progress.ItemsSource=ShowData;
                    var ShowCompleted = db.Task.Where(t => t.UserId==_employee.UserID&&t.Status=="Completed")
                        .Select(t => new 
                        {
                          t.TaskID ,
                          t.Title,
                          t.Description,
                          t.Status
                        }).ToList();
                    completed.ItemsSource=ShowCompleted;    
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           if(cmb_status.SelectedItem==null||txt_TaskId.Text==null)
            {
                MessageBox.Show("Please Enter TaskId and The Status");
                return;
            }
           if(!txt_TaskId.Text.All(char.IsDigit))
            {
                MessageBox.Show("Please The Task Id Must Be Digit");
                return;
            }
            using (var db = new MyDbcontext())
            {
                var TaskEdit = db.Task.FirstOrDefault(t => t.TaskID==int.Parse(txt_TaskId.Text)&&t.UserId==_employee.UserID);
                {
                    if (TaskEdit==null)
                    {
                        MessageBox.Show("This Task Not found");
                        return;
                    }
                    if (TaskEdit!=null)
                    {
                        TaskEdit.Status=cmb_status.SelectedItem.ToString();
                        db.SaveChanges();

                    }
                    LoadData();
                }
            }
        }
    }
}
