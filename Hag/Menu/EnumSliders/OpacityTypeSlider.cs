﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hag.Esp;
namespace Hag.Menu.EnumSliders
{
    class OpacityTypeSlider : Entity
    {
        public unsafe OpacityTypeSlider(string text, string description, ref int value)
        {
            base.Name = text;
            base.Description = description;
            fixed (int* ptr = &value)
            {
                int* @Value = ptr;
                this.Int = @Value;
            }
            MinValue = 0;
            MaxValue = 2;
            IncrementValue = 1;
            GetValue();
        }
        public unsafe int Value
        {
            get
            {
                return *this.Int;
            }
            set
            {
                *this.Int = value;
            }
        }
        private unsafe int* Int;
        public int MaxValue;
        public int MinValue;
        public int IncrementValue;
        public string GetValue()
        {
            string str = "";

            switch (Value)
            {
                case 0:
                    str = "Disabled";
                    return str;
                case 1:
                    str = "Opacity By Distance";
                    return str;
                case 2:
                    str = "Opacity By Value";
                    return str;
            }
            return str;
        }
    }



}
