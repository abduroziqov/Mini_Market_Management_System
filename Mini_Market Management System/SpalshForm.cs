﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mini_Market_Management_System
{
    public partial class SpalshForm : Form
    {
        public SpalshForm()
        {
            InitializeComponent();
        }

        private void SpalshForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void CircleProgressBar_ValueChanged(object sender, EventArgs e)
        {

        }
        int startPoint = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            startPoint += 2;
            myProgressBar.Value = startPoint;
            if(myProgressBar.Value == 100)
            {
                myProgressBar.Value = 0;
                timer1.Stop();
                LoginForm loginForm = new LoginForm();
                this.Hide();
                loginForm.Show();
            }
        }
    }
}
