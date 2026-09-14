using Acc_Trede_winForms.Cashier;
using Acc_Trede_winForms.Models.cSuggestTextBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acc_Trede_winForms
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void test_Load(object sender, EventArgs e)
        {
            var u = new ucCashier();
            u.Dock = DockStyle.Fill;
            this.Controls.Add(u);


        }
    }
}
