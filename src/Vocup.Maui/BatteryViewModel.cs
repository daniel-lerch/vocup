using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocup.Maui
{
    public class BatteryViewModel : ReactiveUI.ReactiveObject
    {
        private double level;

        public BatteryViewModel()
        {
            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;
            ChargeLevel = Battery.ChargeLevel;
        }

        private void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            ChargeLevel = Battery.ChargeLevel;
        }

        public double ChargeLevel { get => level; set => this.RaiseAndSetIfChanged(ref level, value); }
    }
}
