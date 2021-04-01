﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models
{
    public interface IDrawable
    {
        void Draw(GameCellViewModel[,] context);
    }
}
