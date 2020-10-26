﻿using System.Collections.Generic;
using TermLib;

namespace FillGameLib
{
    public class FillGame
    {
        public List<SimpleTerm> List;
        public int Lvl;
        public bool FixedLength;
        public bool TrainingMode;

        public FillGame(List<SimpleTerm> list, int lvl, bool bool1, bool bool2)
        {
            List = list;
            Lvl = lvl;
            FixedLength = bool1;
            TrainingMode = bool2;
        }
    }
}