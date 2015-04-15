using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240
{
    class AssetManager
    {
        private static AssetManager _instance;
        private Dictionary<string, Texture2D> _assets;
        private ContentManager _content;

        private AssetManager(Game game)
        {
            _content = new ContentManager(game.Services, "Content");
            _assets = new Dictionary<string, Texture2D>();
        }

        public static AssetManager CreateInstance(Game game)
        {
            _instance = new AssetManager(game);

            return _instance;
        }

        public static AssetManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    throw new Exception("Not instansiated, create instance with CreateInstance");
                }
                return _instance;
            }
        }

        public void AddAsset(string name)
        {
            _assets.Add(name, _content.Load<Texture2D>(name));
        }

        public Texture2D GetAsset(string name)
        {
            Texture2D asset = null; ;
            _assets.TryGetValue(name, out asset);

            return asset;
        }

    }
}
