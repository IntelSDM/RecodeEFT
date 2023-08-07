using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hag.Esp;
namespace Hag.Menu.EnumSliders
{
    class SkeletonTypeSlider : Entity
    {
        public unsafe SkeletonTypeSlider(string text, string description, ref int value)
        {
            base.Name = text;
            base.Description = description;
            fixed (int* ptr = &value)
            {
                int* @Value = ptr;
                this.Int = @Value;
            }
            MinValue = 1;
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
                case 1:
                    str = "Colour By Health";
                    return str;
                case 2:
                    str = "Colour By Visibility";
                    return str;
            }
            return str;
        }
    }



}
