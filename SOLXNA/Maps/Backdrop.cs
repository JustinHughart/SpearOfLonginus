using System.Xml.Linq;
using SpearOfLonginus;
using SpearOfLonginus.Maps;

namespace SOLXNA.Maps
{
    public class Backdrop : SOLBackdrop
    {
        public Backdrop(string textureid, SOLVector position, SOLVector parallax, SOLVector autoparallax, bool loopx, bool loopy, int layer) : base(textureid, position, parallax, autoparallax, loopx, loopy, layer)
        {

        }

        public Backdrop(XElement element) : base(element)
        {

        }
    }
}
