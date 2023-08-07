using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hag.Esp;
namespace Hag.Menu.EnumSliders
{
    class ChamTypeSlider : Entity
    {
        public unsafe ChamTypeSlider(string text, string description, ref int value)
        {
            base.Name = text;
            base.Description = description;
            fixed (int* ptr = &value)
            {
                int* @Value = ptr;
                this.Int = @Value;
            }
            MinValue = 0;
            MaxValue = 6;
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
                    str = "Flat";
                    return str;
                case 1:
                    str = "Textured";
                    return str;
                case 2:
                    str = "Pulse";
                    return str;
                case 3:
                    str = "Rainbow";
                    return str;
                case 4:
                    str = "Wireframe";
                    return str;
                case 5:
                    str = "Transparent";
                    return str;
                case 6:
                    str = "Galaxy";
                    return str;
              
            }
            return str;
        }
    }



}
