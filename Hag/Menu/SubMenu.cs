using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SharpDX.Direct2D1;
using Hag.Renderer;

namespace Hag.Menu
{
    class SubMenu : Entity
    {
        public SubMenu(string text, string description)
        {
            base.Name = text;
            base.Description = description;
        }

        public List<Entity> Items = new List<Entity>();
        public int Index = 0;
       
        
    }
}
