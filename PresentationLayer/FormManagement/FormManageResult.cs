using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TransferObject;

namespace PresentationLayer.FormManagement
{
    public partial class FormManageResult : Form
    {
        private PlacementTestBL placementTestBL;

        public FormManageResult()
        {
            InitializeComponent();
            placementTestBL = new PlacementTestBL();
        }
        private void FormManageResult_Load(object sender, EventArgs e) //event
        {
            LoadEntryTestData();         
        }

        private void LoadEntryTestData()
        {
            try
            {
                List<PlacementTest> list = placementTestBL.GetAllEntryTest();
                dgvEntryTest.DataSource = null; // reset
                dgvEntryTest.DataSource = list;
                dgvEntryTest.AutoGenerateColumns = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
            }
        }

        private void btnAddResult_Click(object sender, EventArgs e)
        {
            RemoveUpdateColumnIfExists();
            RemoveDeleteColumnIfExists();

            FrmAddResult addForm = new FrmAddResult();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadEntryTestData();
            }
        }

        private void btnUpdateResult_Click(object sender, EventArgs e)
        {
            RemoveDeleteColumnIfExists();
            RemoveUpdateColumnIfExists();

            DataGridViewButtonColumn btnUpdate = new DataGridViewButtonColumn
            {
                Name = "Update",
                HeaderText = "",
                Text = "Sửa",
                UseColumnTextForButtonValue = true,
                Width = 50
            };

            dgvEntryTest.Columns.Add(btnUpdate);
        }

        private void btnDeleteResult_Click(object sender, EventArgs e)
        {
            RemoveUpdateColumnIfExists();
            RemoveDeleteColumnIfExists();

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "",
                Text = "X",
                UseColumnTextForButtonValue = true,
                Width = 50
            };

            dgvEntryTest.Columns.Add(btnDelete);
        }

        private void dgvEntryTest_CellClick(object sender, DataGridViewCellEventArgs e) //Bat su kien click 
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                DataGridViewRow selectedRow = dgvEntryTest.Rows[e.RowIndex];
                if (selectedRow == null || selectedRow.DataBoundItem == null)
                    return;

                if (!(selectedRow.DataBoundItem is PlacementTest selectedTest))
                    return;

                string testId = selectedTest.TestID;

                // Xử lý nút Delete
                if (dgvEntryTest.Columns[e.ColumnIndex].Name == "Delete")
                {
                    var result = MessageBox.Show("Bạn có chắc chắn muốn xóa kết quả này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            placementTestBL.DeletePlacementTest(testId); // Gọi BL xóa
                            RemoveDeleteColumnIfExists();
                            LoadEntryTestData(); // Tải lại
                            MessageBox.Show("Xóa kết quả thành công!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                        }
                    }
                    return;
                }

                // Xử lý nút Update
                if (dgvEntryTest.Columns[e.ColumnIndex].Name == "Update")
                {
                    FrmAddResult frm = new FrmAddResult(selectedTest); // Form sửa
                    frm.ShowDialog();
                    RemoveUpdateColumnIfExists();
                    LoadEntryTestData();
                    return;
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += "\nChi tiết lỗi: " + ex.InnerException.Message;
                }

                MessageBox.Show(message, "Lỗiiiiiiiiiiiiiiiiii", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvEntryTest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RemoveUpdateColumnIfExists();
            RemoveDeleteColumnIfExists();
        }

        private void RemoveDeleteColumnIfExists()
        {
            if (dgvEntryTest.Columns.Contains("Delete"))
                dgvEntryTest.Columns.Remove("Delete");
        }

        private void RemoveUpdateColumnIfExists()
        {
            if (dgvEntryTest.Columns.Contains("Update"))
                dgvEntryTest.Columns.Remove("Update");
        }

        private void SetupChart()
        {
            chartScoreByYear.Series.Clear();
            chartScoreByYear.ChartAreas.Clear();

            var chartArea = new ChartArea("MainArea");
            chartScoreByYear.ChartAreas.Add(chartArea);

            // Thiết lập trục X từ 0 đến 100
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = 100;
            chartArea.AxisX.Interval = 10; // Mỗi 10 điểm

            chartArea.AxisY.Interval = 1;

            chartArea.AxisX.Title = "Điểm đầu vào";
            chartArea.AxisY.Title = "Số lượng học viên";

            var series = new Series("ScoreFrequency")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.Blue
            };
            chartScoreByYear.Series.Add(series);
        }

        private void btnDrawChart_Click(object sender, EventArgs e)
        {           
            int selectedYear = (int)nudYear.Value;
            SetupChart(); 

            var data = placementTestBL.GetScoreFrequencyByYear(selectedYear);
            var series = chartScoreByYear.Series["ScoreFrequency"];

            foreach (var item in data)
            {
                series.Points.AddXY(item.Score, item.Frequency);
            }

            if (data.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu cho năm " + selectedYear, "Thông báo");
            }
        }
    }
}
