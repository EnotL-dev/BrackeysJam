using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model
{
    public class Vibe
    {
        public int vibe = 50;

        public void AddVibe(int count)
        {
            if (vibe + count >= 100)
                vibe = 100;
            else
                vibe += count;
        }

        public void ReduceVibe(int count)
        {
            if (vibe - count <= 0)
                vibe = 0;
            else
                vibe -= count;
        }
    }
}