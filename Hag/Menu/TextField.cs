using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hag.Menu
{
    class TextField : Entity
    {

      
        public ref string Value
        {
            get
            {
                return ref this.value;
            }
        }

        public TextField(string text, string description, ref string value)
        {
            base.Name = text;
            base.Description = description;
            this.value = value;
        }

        private string value;

    }

}
