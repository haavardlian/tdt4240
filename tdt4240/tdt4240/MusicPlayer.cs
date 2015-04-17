using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace tdt4240
{

    class MusicPlayer
    {
        private static readonly MusicPlayer Instance = new MusicPlayer();

        private Dictionary<String, SoundEffect> _songs;
        private SoundEffectInstance _currentlyPlayingSongInstance = null;

        public static MusicPlayer GetInstance()
        {
            return Instance;
        }

        private MusicPlayer()
        {
            _songs = new Dictionary<string, SoundEffect>
            {
                {"1", null},
                {"2", null},
                {"3", null},
                {"4", null},
                {"5", null},
                {"6", null},
                {"7", null},
                {"8", null},
            };
        }

        public void StartLoopingSong(string name)
        {
            var song = _songs[name];
            StartSong(song, true);
        }

        public void StartSong(SoundEffect song, Boolean enableLooping)
        {
            var songInstance = song.CreateInstance();
            songInstance.IsLooped = enableLooping;
            songInstance.Play();

            if (_currentlyPlayingSongInstance != null)
            {
                _currentlyPlayingSongInstance.Stop();
                _currentlyPlayingSongInstance.Dispose();
                _currentlyPlayingSongInstance = null;
            }

            _currentlyPlayingSongInstance = songInstance;
        }

        public void LoadContent(ContentManager content)
        {
            _songs["1"] = content.Load<SoundEffect>("music/ide1");
            _songs["2"] = content.Load<SoundEffect>("music/ide2");
            _songs["3"] = content.Load<SoundEffect>("music/ide3");
            _songs["4"] = content.Load<SoundEffect>("music/ide4");
            _songs["5"] = content.Load<SoundEffect>("music/ide5");
            _songs["6"] = content.Load<SoundEffect>("music/ide6");
            _songs["7"] = content.Load<SoundEffect>("music/ide7");
        }
    }
}
