using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hag.Esp;
namespace Hag.Menu.EnumSliders
{
    class TextAlignmentSlider : Entity
    {
        public unsafe TextAlignmentSlider(string text, string description, ref int value)
        {
            base.Name = text;
            base.Description = description;
            fixed (int* ptr = &value)
            {
                int* @Value = ptr;
                this.Int = @Value;
            }
            MinValue = 1;
            MaxValue = 4;
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
                case 1:
                    str = "Bottom";
                    return str;
                case 2:
                    str = "Left";
                    return str;
                case 3:
                    str = "Right";
                    return str;
                case 4:
                    str = "Top";
                    return str;
            }
            return str;
        }
    }



}
