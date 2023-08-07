using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hag.Esp;
namespace Hag.Menu.EnumSliders
{
    class CrosshairTypeSlider : Entity
    {
        public unsafe CrosshairTypeSlider(string text, string description, ref int value)
        {
            base.Name = text;
            base.Description = description;
            fixed (int* ptr = &value)
            {
                int* @Value = ptr;
                this.Int = @Value;
            }
            MinValue = 0;
            MaxValue = 5;
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
                    str = "Circle";
                    return str;
                case 1:
                    str = "Cross";
                    return str;
                case 2:
                    str = "Dotted Cross";
                    return str;
                case 3:
                    str = "Dot";
                    return str;
                case 4:
                    str = "Filled Circle";
                    return str;
                case 5:
                    str = "Swastika";
                    return str;
            }
            return str;
        }
    }



}
