using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicsContracts.ViewModels;
using BusinessLogicsContracts.BusinessLogicsContracts;

namespace Laba5View
{
    public partial class FormFurnitureMaterial : Form
    {
        public int Id
        {
            get { return Convert.ToInt32(comboBox.SelectedValue); }
            set { comboBox.SelectedValue = value; }
        }
        public string MaterialName { get { return comboBox.Text; } }
        public int Count
        {
            get { return Convert.ToInt32(textBox.Text); }
            set
            {
                textBox.Text = value.ToString();
            }
        }
        public FormFurnitureMaterial(IMaterialLogic logic)
        {
            InitializeComponent();
            List<MaterialVM> list = logic.Read(null);
            if (list != null)
            {
                comboBox.DisplayMember = "MaterialName";
                comboBox.ValueMember = "Id";
                comboBox.DataSource = list;
                comboBox.SelectedItem = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите материал", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
