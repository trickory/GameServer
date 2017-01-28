using System;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class AudioManagerPlaySoundEventArgs : EventArgs
    {
        public string SoundFile { get; private set; }

        public AudioManagerPlaySoundEventArgs(string soundFile)
        {
            SoundFile = soundFile;
        }
    }
}
