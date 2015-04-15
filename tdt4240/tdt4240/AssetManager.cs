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
        private Dictionary<string, object> _assets;
        private ContentManager _content;

        private AssetManager(Game game)
        {
            _content = new ContentManager(game.Services, "Content");
            _assets = new Dictionary<string, object>();
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

        public T AddAsset<T>(string name)
        {
            if(!_assets.ContainsKey(name))
                _assets.Add(name, _content.Load<T>(name));
            return (T) _assets[name];
        }

        public T GetAsset<T>(string name)
        {
            object asset = null; ;
            _assets.TryGetValue(name, out asset);

            return (T)asset;
        }

        /// <summary>
        /// Get all assets of type T that starts with start
        /// </summary>
        public List<T> FindAssets<T>(string start)
        {
            var results = from result in _assets
                          where result.Key.StartsWith(start) && result is T                    
                          select (T)result.Value;

            return results.ToList();
        } 

    }
}
