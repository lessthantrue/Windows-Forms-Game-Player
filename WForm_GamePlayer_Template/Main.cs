using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WForm_GamePlayer;


namespace $safeprojectname$
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            m_gameControl.Init(30, new Game1(m_gameControl));
            m_gameControl.Play();
        }
    }
}
