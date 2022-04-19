using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicsContracts.BindingModels;
using BusinessLogicsContracts.ViewModels;
using BusinessLogicsContracts.BusinessLogicsContracts;

namespace Laba5View
{
    public partial class FormCreateOrder : Form
    {
        private readonly IFurnitureLogic _logicP;
        private readonly IOrderLogic _logicO;
        public int Id { set { id = value; } }
        private int? id;
        public FormCreateOrder(IFurnitureLogic logicP, IOrderLogic logicO)
        {
            InitializeComponent();
            _logicP = logicP;
            _logicO = logicO;
        }

        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                List<FurnitureVM> list = _logicP.Read(null);
                if (list != null)
                {
                    comboBox.DisplayMember = "FurnitureName";
                    comboBox.ValueMember = "Id";
                    comboBox.DataSource = list;
                    comboBox.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void CalcSum()
        {
            if (comboBox.SelectedValue != null &&
           !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBox.SelectedValue);
                    FurnitureVM furniture = _logicP.Read(new FurnitureBM
                    {
                        Id = id
                    })?[0];
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * furniture?.Price ?? 0).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
        
            try
            {
                _logicO.CreateOrUpdate(new OrderBM
                {
                    Id = id,
                    FurnitureId = Convert.ToInt32(comboBox.SelectedValue),
                    FurnitureName = comboBox.Text,
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToDecimal(textBoxSum.Text),
                    DateCreate = dateTimePicker1.Value,
                    DateImplement = dateTimePicker.Value
                }) ;
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void textBoxPrice_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
    }
}
