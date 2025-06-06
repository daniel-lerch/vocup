﻿using System.Windows.Forms;
using System.ComponentModel;

namespace Vocup.Controls
{
    public partial class StatisticsPanel : UserControl
    {
        private int _unpracticed;
        private int _wronglyPracticed;
        private int _correctlyPracticed;
        private int _fullyPracticed;

        public StatisticsPanel()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Unpracticed
        {
            get => _unpracticed;
            set
            {
                _unpracticed = value;
                LbUnpracticed.Text = value.ToString();
                RenewSum();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int WronglyPracticed
        {
            get => _wronglyPracticed;
            set
            {
                _wronglyPracticed = value;
                LbWronglyPracticed.Text = value.ToString();
                RenewSum();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CorrectlyPracticed
        {
            get => _correctlyPracticed;
            set
            {
                _correctlyPracticed = value;
                LbCorrectlyPracticed.Text = value.ToString();
                RenewSum();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FullyPracticed
        {
            get => _fullyPracticed;
            set
            {
                _fullyPracticed = value;
                LbFullyPracticed.Text = value.ToString();
                RenewSum();
            }
        }

        public void Reset()
        {
            Unpracticed = 0;
            WronglyPracticed = 0;
            CorrectlyPracticed = 0;
            FullyPracticed = 0;
        }

        private void RenewSum()
        {
            LbTotalCount.Text = (Unpracticed + WronglyPracticed + CorrectlyPracticed + FullyPracticed).ToString();
        }
    }
}
