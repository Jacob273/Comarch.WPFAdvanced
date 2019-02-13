using System;
using System.Collections.Generic;
using System.Text;

namespace KM.Bosman.Models
{
    public class Speed : Base
    {
        private float _value;

        public float Value
        {
            get => _value;
            set
            {                
                _value = value;

                OnPropertyChanged();
            }
        }
        public SpeedUnit Unit { get; set; }


       
    }
}
