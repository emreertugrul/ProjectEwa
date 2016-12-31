using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Windows.Media.SpeechRecognition;

namespace Eva.Speech
{
    public class Listener
    {
        SpeechRecognizer speechRecognizer = new SpeechRecognizer();
        SpeechRecognizer evaListener = new SpeechRecognizer();

        public delegate void StateChangedArgs(string state);
        public event StateChangedArgs OnStateChanged;
        public delegate void SpeechResultArgs(string resultText);
        public event SpeechResultArgs OnSpeechResult;

        public async Task ConfigureEvaListener()
        {
            await speechRecognizer.CompileConstraintsAsync();
            string[] responses = { "hey eva" };
            evaListener.Constraints.Add(new SpeechRecognitionListConstraint(responses, "evatag"));
            evaListener.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;
            evaListener.ContinuousRecognitionSession.Completed += ContinuousRecognitionSession_Completed;
            evaListener.StateChanged += EvaListener_StateChanged;
            evaListener.ContinuousRecognitionSession.AutoStopSilenceTimeout = TimeSpan.FromDays(1);
            await evaListener.CompileConstraintsAsync();
        }

        private void EvaListener_StateChanged(SpeechRecognizer sender, SpeechRecognizerStateChangedEventArgs args)
        {
            OnStateChanged?.Invoke(args.State.ToString());
        }

        private async void ContinuousRecognitionSession_Completed(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionCompletedEventArgs args)
        {
            if (args.Status != SpeechRecognitionResultStatus.Success)
            {
                await StartEva();
            }
        }

        public async Task StartEva(bool resume = false)
        {
            if (!resume)
            {
                await evaListener.ContinuousRecognitionSession.StartAsync();
            }
            else
            {
                await evaListener.ContinuousRecognitionSession.StartAsync();
            }


            OnStateChanged?.Invoke("Eva listening");
        }

        private async void ContinuousRecognitionSession_ResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            if (args.Result.Text == "hey eva")
            {
                OnStateChanged?.Invoke("Yes Sir?");
                await evaListener.ContinuousRecognitionSession.StopAsync();
                SpeechRecognitionResult result = await speechRecognizer.RecognizeAsync();
                OnSpeechResult?.Invoke(result.Text);
            }
        }
    }
}
