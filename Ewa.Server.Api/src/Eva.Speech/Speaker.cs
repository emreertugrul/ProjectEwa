using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.Media.SpeechSynthesis;

namespace Eva.Speech
{
    public class Speaker
    {
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        SpeechSynthesisStream synthesisStream;

        public async Task Speak(string text)
        {
            try
            {

                foreach (VoiceInformation voice in SpeechSynthesizer.AllVoices)
                {
                    if (voice.DisplayName == "Microsoft Zira Mobile")
                        synthesizer.Voice = voice;
                }

                synthesisStream = await synthesizer.SynthesizeTextToStreamAsync(text);

                BackgroundMediaPlayer.Current.AutoPlay = true;
                BackgroundMediaPlayer.Current.SetStreamSource(synthesisStream);
                BackgroundMediaPlayer.Current.Play();
            }
            catch (Exception eException)
            {
                // Handle Error
            }
        }
    }
}
