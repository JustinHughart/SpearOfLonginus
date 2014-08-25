using System.Collections.Generic;

namespace SpearOfLonginus.Maps
{
    public class SOLMap
    {
        protected List<SOLTile> TileSet;

        public SOLVector Size { get; protected set; }
        public SOLVector TileSize { get; protected set; }

        protected List<int> CollisionLayer;
        protected List<int> Background;
        protected List<int> Foreground;

        protected List<MapLogic> Logics;





        public void Update()
        {
            foreach (var tile in TileSet)
            {
                tile.Update();
            }

            foreach (var logic in Logics)
            {
                logic.Update();
            }
        }
    }
}
