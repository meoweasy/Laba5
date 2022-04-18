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
using Unity;

namespace Laba5View
{
    public partial class FormFurniture : Form
    {
        public int Id { set { id = value; } }
        private readonly IFurnitureLogic _logic;
        private int? id;
        private Dictionary<int, (string, int)> FurnitureMaterials;
        public FormFurniture(IFurnitureLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormFurnitureMaterial>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (FurnitureMaterials.ContainsKey(form.Id))
                {
                    FurnitureMaterials[form.Id] = (form.MaterialName, form.Count);
                }
                else
                {
                    FurnitureMaterials.Add(form.Id, (form.MaterialName, form.Count));
                }
                LoadData();
            }
        }

        private void FormFurniture_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    FurnitureVM view = _logic.Read(new FurnitureBM
                    {
                        Id =
                   id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.FurnitureName;
                        textBoxPrice.Text = view.Price.ToString();
                        FurnitureMaterials = view.FurnitureMaterials;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                FurnitureMaterials = new Dictionary<int, (string, int)>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (FurnitureMaterials != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in FurnitureMaterials)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1,
pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormFurnitureMaterial>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = FurnitureMaterials[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    FurnitureMaterials[form.Id] = (form.MaterialName, form.Count);
                    LoadData();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        FurnitureMaterials.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (FurnitureMaterials == null || FurnitureMaterials.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new FurnitureBM
                {
                    Id = id,
                    FurnitureName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    FurnitureMaterials = FurnitureMaterials
                });
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

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
