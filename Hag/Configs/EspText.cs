using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Configs
{
    enum Alignment
    {
    Bottom,
    Top,
    Right,
    Left
    }
    class EspText
    {
        public EspText(bool enable, int alignment, int line)
        {
            Enable = enable;
            Alignment = alignment;
            Line = line;
        }
        public bool Enable;
        public int Alignment;
        public int Line;
    }
}
