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
using Ewa.MessageObjects.Common.Settings;
using Newtonsoft.Json;
using Ewa.MessageObjects.Commands;
using Ewa.MessageObjects.Messaging;
using Ewa.IOT.Operator;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Ewa.MessageObjects.RPI
{
    /// <summary>
    /// Test ground for all stuff. This page will be replaced by real UI page when ready.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private CoreDispatcher dispatcher;
        private Eva.Speech.Listener listener = new Eva.Speech.Listener();

        public MainPage()
        {
            this.InitializeComponent();

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
            //await RegisterVoiceCommands();

            SettingsManager.IOTDeviceId = "testdevice1";
            SettingsManager.IOTHubDeviceKey = "ZCA3r9t44jPfRvnx2OvoOnWwr9b5nyjyurScnGsU5+Y=";
            Common.IOT.IotHubManager.InitializeDeviceClient();
            
            await StartListeningForCommands();
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

        private async void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            await Common.IOT.IotHubManager.SendDeviceToCloudMessagesAsync();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Common.IOT.IotHubManager.InitializeDeviceClient();
        }

        private async void btnReceive_Click(object sender, RoutedEventArgs e)
        {
            await StartListeningForCommands();
        }

        private async Task StartListeningForCommands()
        {
            await Common.IOT.IotHubManager.StartReceiveCloudToDeviceMessagesAsync((s) =>
            {
                listMessages.Items.Add(s);              
                dynamic message = JsonConvert.DeserializeObject(s);
                var msgType = (MessageTypes)message.CommandType;


                if (msgType == MessageTypes.GPIOOnOf)
                {
                    GPIOOnOffCommandMessage msg = JsonConvert.DeserializeObject<GPIOOnOffCommandMessage>(s);
                    OperatorCommandBroker.OperateCommand(msg.Command);
                }

                return true;
            });
        }
    }
}
