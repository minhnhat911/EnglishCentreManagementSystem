namespace PresentationLayer.FormManagement
{
    partial class FrmAddClass
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.btnCancelClass = new System.Windows.Forms.Button();
            this.btnSaveClass = new System.Windows.Forms.Button();
            this.groupBoxAssignTeachers = new System.Windows.Forms.GroupBox();
            this.lstAssignedTeachers = new System.Windows.Forms.ListBox();
            this.btnRemoveTeacherFromClass = new System.Windows.Forms.Button();
            this.btnAddTeacherToClass = new System.Windows.Forms.Button();
            this.lstAvailableTeachers = new System.Windows.Forms.ListBox();
            this.nudMaxStudents = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRoom = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cboCourse = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClassId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2GradientPanel1.SuspendLayout();
            this.groupBoxAssignTeachers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.Controls.Add(this.btnCancelClass);
            this.guna2GradientPanel1.Controls.Add(this.btnSaveClass);
            this.guna2GradientPanel1.Controls.Add(this.groupBoxAssignTeachers);
            this.guna2GradientPanel1.Controls.Add(this.nudMaxStudents);
            this.guna2GradientPanel1.Controls.Add(this.label6);
            this.guna2GradientPanel1.Controls.Add(this.txtRoom);
            this.guna2GradientPanel1.Controls.Add(this.label5);
            this.guna2GradientPanel1.Controls.Add(this.dtpEndDate);
            this.guna2GradientPanel1.Controls.Add(this.label4);
            this.guna2GradientPanel1.Controls.Add(this.dtpStartDate);
            this.guna2GradientPanel1.Controls.Add(this.label3);
            this.guna2GradientPanel1.Controls.Add(this.cboCourse);
            this.guna2GradientPanel1.Controls.Add(this.label2);
            this.guna2GradientPanel1.Controls.Add(this.txtClassId);
            this.guna2GradientPanel1.Controls.Add(this.label1);
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.White;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(677, 450);
            this.guna2GradientPanel1.TabIndex = 0;
            // 
            // btnCancelClass
            // 
            this.btnCancelClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancelClass.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnCancelClass.Location = new System.Drawing.Point(551, 395);
            this.btnCancelClass.Name = "btnCancelClass";
            this.btnCancelClass.Size = new System.Drawing.Size(100, 35);
            this.btnCancelClass.TabIndex = 29;
            this.btnCancelClass.Text = "Đóng";
            this.btnCancelClass.UseVisualStyleBackColor = false;
            // 
            // btnSaveClass
            // 
            this.btnSaveClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSaveClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnSaveClass.Location = new System.Drawing.Point(441, 395);
            this.btnSaveClass.Name = "btnSaveClass";
            this.btnSaveClass.Size = new System.Drawing.Size(100, 35);
            this.btnSaveClass.TabIndex = 28;
            this.btnSaveClass.Text = "Lưu";
            this.btnSaveClass.UseVisualStyleBackColor = false;
            this.btnSaveClass.Click += new System.EventHandler(this.btnSaveClass_Click_1);
            // 
            // groupBoxAssignTeachers
            // 
            this.groupBoxAssignTeachers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAssignTeachers.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxAssignTeachers.Controls.Add(this.lstAssignedTeachers);
            this.groupBoxAssignTeachers.Controls.Add(this.btnRemoveTeacherFromClass);
            this.groupBoxAssignTeachers.Controls.Add(this.btnAddTeacherToClass);
            this.groupBoxAssignTeachers.Controls.Add(this.lstAvailableTeachers);
            this.groupBoxAssignTeachers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBoxAssignTeachers.Location = new System.Drawing.Point(25, 150);
            this.groupBoxAssignTeachers.Name = "groupBoxAssignTeachers";
            this.groupBoxAssignTeachers.Size = new System.Drawing.Size(626, 230);
            this.groupBoxAssignTeachers.TabIndex = 27;
            this.groupBoxAssignTeachers.TabStop = false;
            this.groupBoxAssignTeachers.Text = "Phân công giáo viên";
            // 
            // lstAssignedTeachers
            // 
            this.lstAssignedTeachers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAssignedTeachers.FormattingEnabled = true;
            this.lstAssignedTeachers.ItemHeight = 20;
            this.lstAssignedTeachers.Location = new System.Drawing.Point(360, 25);
            this.lstAssignedTeachers.Name = "lstAssignedTeachers";
            this.lstAssignedTeachers.Size = new System.Drawing.Size(250, 164);
            this.lstAssignedTeachers.TabIndex = 3;
            // 
            // btnRemoveTeacherFromClass
            // 
            this.btnRemoveTeacherFromClass.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnRemoveTeacherFromClass.Location = new System.Drawing.Point(280, 125);
            this.btnRemoveTeacherFromClass.Name = "btnRemoveTeacherFromClass";
            this.btnRemoveTeacherFromClass.Size = new System.Drawing.Size(60, 30);
            this.btnRemoveTeacherFromClass.TabIndex = 2;
            this.btnRemoveTeacherFromClass.Text = "<<";
            this.btnRemoveTeacherFromClass.UseVisualStyleBackColor = true;
            this.btnRemoveTeacherFromClass.Click += new System.EventHandler(this.btnRemoveTeacherFromClass_Click);
            // 
            // btnAddTeacherToClass
            // 
            this.btnAddTeacherToClass.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAddTeacherToClass.Location = new System.Drawing.Point(280, 75);
            this.btnAddTeacherToClass.Name = "btnAddTeacherToClass";
            this.btnAddTeacherToClass.Size = new System.Drawing.Size(60, 30);
            this.btnAddTeacherToClass.TabIndex = 1;
            this.btnAddTeacherToClass.Text = ">>";
            this.btnAddTeacherToClass.UseVisualStyleBackColor = true;
            this.btnAddTeacherToClass.Click += new System.EventHandler(this.btnAddTeacherToClass_Click);
            // 
            // lstAvailableTeachers
            // 
            this.lstAvailableTeachers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstAvailableTeachers.FormattingEnabled = true;
            this.lstAvailableTeachers.ItemHeight = 20;
            this.lstAvailableTeachers.Location = new System.Drawing.Point(15, 25);
            this.lstAvailableTeachers.Name = "lstAvailableTeachers";
            this.lstAvailableTeachers.Size = new System.Drawing.Size(250, 164);
            this.lstAvailableTeachers.TabIndex = 0;
            // 
            // nudMaxStudents
            // 
            this.nudMaxStudents.Location = new System.Drawing.Point(522, 115);
            this.nudMaxStudents.Name = "nudMaxStudents";
            this.nudMaxStudents.Size = new System.Drawing.Size(120, 22);
            this.nudMaxStudents.TabIndex = 26;
            this.nudMaxStudents.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(342, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 20);
            this.label6.TabIndex = 25;
            this.label6.Text = "Giới hạn học sinh";
            // 
            // txtRoom
            // 
            this.txtRoom.Location = new System.Drawing.Point(153, 115);
            this.txtRoom.Name = "txtRoom";
            this.txtRoom.Size = new System.Drawing.Size(150, 22);
            this.txtRoom.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(38, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 20);
            this.label5.TabIndex = 23;
            this.label5.Text = "Phòng";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(492, 81);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.ShowCheckBox = true;
            this.dtpEndDate.Size = new System.Drawing.Size(150, 22);
            this.dtpEndDate.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(342, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 20);
            this.label4.TabIndex = 21;
            this.label4.Text = "Ngày kết thúc";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Checked = false;
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(153, 82);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.ShowCheckBox = true;
            this.dtpStartDate.Size = new System.Drawing.Size(150, 22);
            this.dtpStartDate.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(33, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "Ngày bắt đầu";
            // 
            // cboCourse
            // 
            this.cboCourse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCourse.FormattingEnabled = true;
            this.cboCourse.Location = new System.Drawing.Point(153, 47);
            this.cboCourse.Name = "cboCourse";
            this.cboCourse.Size = new System.Drawing.Size(489, 24);
            this.cboCourse.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(36, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "Khóa: ";
            // 
            // txtClassId
            // 
            this.txtClassId.Location = new System.Drawing.Point(153, 20);
            this.txtClassId.Name = "txtClassId";
            this.txtClassId.Size = new System.Drawing.Size(150, 22);
            this.txtClassId.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(33, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "ID lớp học:";
            // 
            // FrmAddClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 450);
            this.Controls.Add(this.guna2GradientPanel1);
            this.Name = "FrmAddClass";
            this.Text = "FrmAddClass";
            this.Load += new System.EventHandler(this.FrmAddClass_Load);
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2GradientPanel1.PerformLayout();
            this.groupBoxAssignTeachers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxStudents)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private System.Windows.Forms.Button btnCancelClass;
        private System.Windows.Forms.Button btnSaveClass;
        private System.Windows.Forms.GroupBox groupBoxAssignTeachers;
        private System.Windows.Forms.ListBox lstAssignedTeachers;
        private System.Windows.Forms.Button btnRemoveTeacherFromClass;
        private System.Windows.Forms.Button btnAddTeacherToClass;
        private System.Windows.Forms.ListBox lstAvailableTeachers;
        private System.Windows.Forms.NumericUpDown nudMaxStudents;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRoom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboCourse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtClassId;
        private System.Windows.Forms.Label label1;
    }
}