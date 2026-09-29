using System;
using System.Collections.Generic;
using System.Text;

namespace КТ__13_Задание_на_структуры_и_перечисления
{
    struct Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }

        public override string ToString() => $"{Rank} of {Suit}";
    }
}
