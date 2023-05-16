﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chip_8_Emulator
{
    internal class NonSelectableButton : Button
    {
        public NonSelectableButton() 
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}
