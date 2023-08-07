using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hag.Esp;
namespace Hag.Menu.EnumSliders
{
    class BoneTypeSlider : Entity
    {
        public unsafe BoneTypeSlider(string text, string description, ref int value)
        {
            base.Name = text;
            base.Description = description;
            fixed (int* ptr = &value)
            {
                int* @Value = ptr;
                this.Int = @Value;
            }
            MinValue = 0;
            MaxValue = 9;
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
                    str = "Closest";
                    return str;
                case 1:
                    str = "Head";
                    return str;
                case 2:
                    str = "Neck";
                    return str;
                case 3:
                    str = "Chest";
                    return str;
                case 4:
                    str = "Stomach";
                    return str;
                case 5:
                    str = "Arms";
                    return str;
                case 6:
                    str = "Legs";
                    return str;
                case 7:
                    str = "Hands";
                    return str;
                case 8:
                    str = "Feet";
                    return str;
                case 9:
                    str = "Hitscan";
                    return str;
            }
            return str;
        }
    }



}
