﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            
            
        }

        private void quanLySachToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            QuanLySach qls = new QuanLySach();
            qls.ShowDialog();
            this.Hide();
            
        }
    }
}
