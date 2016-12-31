using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Core;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.ApplicationModel.VoiceCommands;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Ewa.IOT.RPI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private CoreDispatcher dispatcher;
        private Eva.Speech.Listener listener = new Eva.Speech.Listener();

        public MainPage()
        {
            this.InitializeComponent();
            //Compile the dictation grammar by default.
            this.dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            listener.OnSpeechResult += Listener_OnSpeechResult;
            listener.OnStateChanged += Listener_OnStateChanged;
        }

        private async void Listener_OnStateChanged(string state)
        {
            await this.dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
             {
                 txtSpeechState.Text = state;
             });
        }

        private async void Listener_OnSpeechResult(string resultText)
        {
            await this.dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                txtSpeechResult.Text = resultText;
            });
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await RegisterVoiceCommands();
        }

        private async Task RegisterVoiceCommands()
        {
            try
            {
                var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///VoiceCommands/VoiceCommands.xml"));
                await VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(file);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
