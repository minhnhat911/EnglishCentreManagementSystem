using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace PresentationLayer.FormChild
{
    public partial class FormMenuAdmin : Form
    {
        //public event Action<Form> OnMenuClick;
        private Action<Form, Panel> addForm;
        private Panel targetPanel;
        public FormMenuAdmin(Action<Form, Panel> addFormMethod, Panel panel)
        {
            InitializeComponent();
            this.targetPanel = panel;
            this.addForm = addFormMethod;
        }
        private void btnManageUsers_Click(object sender, EventArgs e)
        {
          //  OnMenuClick?.Invoke(new FormManagement.FormManageUsers());
          FormManagement.FormManageAccounts f = new FormManagement.FormManageAccounts();
            targetPanel.Controls.Clear();
           addForm(f, targetPanel);
        }

        private void btnManageStudents_Click(object sender, EventArgs e)
        {
            FormManagement.FormManageStudents f1 = new FormManagement.FormManageStudents();
            targetPanel.Controls.Clear();
            addForm(f1, targetPanel);
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            FormManagement.FormManageResult f2 = new FormManagement.FormManageResult();
            targetPanel.Controls.Clear();
            addForm(f2, targetPanel);
        }

        private void btnManageTeaches_Click(object sender, EventArgs e)
        {
            FormManagement.FormManageTeacher f3 = new FormManagement.FormManageTeacher();
            targetPanel.Controls.Clear();
            addForm(f3, targetPanel);
        }

        private void btnManageClasses_Click(object sender, EventArgs e)
        {
            FormManagement.FormManageClasses f4 = new FormManagement.FormManageClasses();
            targetPanel.Controls.Clear();
            addForm(f4, targetPanel);
        }

        private void btnManageCourses_Click(object sender, EventArgs e)
        {
            FormManagement.FormManageCourse f5 = new FormManagement.FormManageCourse();
            targetPanel.Controls.Clear();
            addForm(f5, targetPanel);
        }

        private void btnManageFee_Click(object sender, EventArgs e)
        {
            FormManagement.FormManageFees f6 = new FormManagement.FormManageFees();
            targetPanel.Controls.Clear();
            addForm(f6, targetPanel);
        }
    }
}
