using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Menu
{
    class Button : Entity
    {
        public Button(string text, string description, Action button)
        {
            base.Name = text;
            base.Description = description;
            Method = button;
        }
        public Action Method;
    }
}
