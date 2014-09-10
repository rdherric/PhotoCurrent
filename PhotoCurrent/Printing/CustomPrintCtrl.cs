using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PhotoCurrent.Printing
{
    public partial class CustomPrintCtrl : UserControl
    {
        public CustomPrintCtrl()
        {
            //Do the standard initialization
            InitializeComponent();

            //Set the index on the ComboBox
            if (cbPerPage.SelectedIndex == -1)
                this.cbPerPage.SelectedIndex = 2;
        }


        #region Public Properties
        /// <summary>
        /// PrintAll determines if all Windows should be printed or 
        /// just the current one.
        /// </summary>
        public Boolean PrintAll
        {
            get { return this.rbPrintAll.Checked; }
        }


        /// <summary>
        /// ChartsPerPage allows the user to determine how many
        /// plots to put on one page.
        /// </summary>
        public Int32 ChartsPerPage
        {
            get { return Int32.Parse(this.cbPerPage.SelectedItem.ToString()); }
            set { this.cbPerPage.SelectedItem = value.ToString(); }
        }
        #endregion
    }
}
