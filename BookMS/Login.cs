﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookMS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                TryLogin();
            }
            else
            {
                MessageBox.Show("输入有空项，请重新输入");
            }
        }

        public void TryLogin()
        {
            string table = "";
            if (radioButtonUser.Checked) table = "t_user";
            else if (radioButtonAdmin.Checked) table = "t_admin";
            else MessageBox.Show("请先选择登录类型！");

            Dao dao = new Dao();
            string sql = $"Select * from {table} where ID='{textBox1.Text}' and Password='{textBox2.Text}'";
            IDataReader dc = dao.read(sql);
            if (dc.Read())
            {
                Data.UID = dc["ID"].ToString();
                if (radioButtonUser.Checked)
                {
                    UserWindow user = new UserWindow();
                    this.Hide();
                    user.ShowDialog();
                }
                else if (radioButtonAdmin.Checked)
                {
                    AdminWindow admin = new AdminWindow();
                    this.Hide();
                    admin.ShowDialog();
                }


                Data.UID = "";
                this.Show();
            }
            else
            {
                MessageBox.Show("用户名或者密码错误！");
            }
            dao.DaoClose();            
        }
    }
}
