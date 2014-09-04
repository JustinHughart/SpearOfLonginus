using System.Xml.Linq;
using SpearOfLonginus;
using SpearOfLonginus.Maps;

namespace SOLXNA.Maps
{
    public class XnaBackdrop : Backdrop
    {
        public XnaBackdrop(string textureid, Vector position, Vector parallax, Vector autoparallax, bool loopx, bool loopy, int layer) : base(textureid, position, parallax, autoparallax, loopx, loopy, layer)
        {

        }

        public XnaBackdrop(XElement element) : base(element)
        {

        }
    }
}
