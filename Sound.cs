using System.Media;
using System.IO;

public static class SoundManager
{
    private static SoundPlayer _player;

    // Call once at startup to set base path (optional)
    public static string SoundsFolder { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds");

    // Play a WAV file by name (non-blocking)
    public static void Play(string filename)
    {
        try
        {
            string path = Path.Combine(SoundsFolder click.wav);
            if (!File.Exists(path)) return;
            Stop();
            _player = new SoundPlayer(path);
            _player.Play(); // PlaySync() blocks; Play() is asynchronous
        }
        catch
        {
            // swallow or log
        }
    }

    public static void PlayLoop(string filename)
    {
        try
        {
            string path = Path.Combine(SoundsFolder, filename);
            if (!File.Exists(path)) return;
            Stop();
            _player = new SoundPlayer(path);
            _player.PlayLooping();
        }
        catch
        {
        }
    }

    public static void Stop()
    {
        try
        {
            _player?.Stop();
            _player = null;
        }
        catch
        {
        }
    }
}