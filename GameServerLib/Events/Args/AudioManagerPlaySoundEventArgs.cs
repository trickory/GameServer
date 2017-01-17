using System;

namespace LeagueSandbox.GameServer.Events.Args
{
    class AudioManagerPlaySoundEventArgs : EventArgs
    {
        public bool Process { get; private set; }
        public string SoundFile { get; private set; }

        public AudioManagerPlaySoundEventArgs(string soundFile)
        {
            SoundFile = soundFile;
            Process = true;
        }
    }
}
