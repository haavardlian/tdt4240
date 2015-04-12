using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DataTypes
{
    public class SerializableQuestion
    {
        public String Question;
        public String Alternative_1;
        public String Alternative_2;
        public String Alternative_3;
        public String Alternative_4;
        public String CorrectAlternative;
    }
}
