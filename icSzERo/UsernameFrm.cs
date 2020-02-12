using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace icSzERo
{
    public partial class UsernameFrm : Form
    {
        public UsernameFrm()
        {
            InitializeComponent();
        }

        private bool isOk(string s)
        {
            foreach(char c in s)
            {
                if(!((c >= 'a' && c <= 'z') ||(c >= 'A' && c <= 'Z')))
                {
                    return false;
                }
            }
            return s.Length > 0;
        }

        private void BtnUser_Click(object sender, EventArgs e)
        {
            if(isOk(TBUser.Text))
            {
                (Tag as MainFrm).user = TBUser.Text;
                (Tag as MainFrm).StartConnecting();
                Close();
            }
        }
    }
}
