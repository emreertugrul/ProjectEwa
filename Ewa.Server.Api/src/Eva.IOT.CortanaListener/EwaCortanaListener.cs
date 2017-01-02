using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.VoiceCommands;

namespace Ewa.IOT.CortanaListener
{

    public sealed class EwaCortanaListener : IBackgroundTask
    {
        VoiceCommandServiceConnection voiceServiceConnection;
        BackgroundTaskDeferral serviceDeferral;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            serviceDeferral = taskInstance.GetDeferral();
            taskInstance.Canceled += OnTaskCanceled;
            var triggerDetails = taskInstance.TriggerDetails as AppServiceTriggerDetails;

            if (triggerDetails != null && triggerDetails.Name == "EwaListener")
            {
                try
                {
                    voiceServiceConnection = VoiceCommandServiceConnection.FromAppServiceTriggerDetails(triggerDetails);
                    voiceServiceConnection.VoiceCommandCompleted += OnVoiceCommandCompleted;

                    // GetVoiceCommandAsync establishes initial connection to Cortana, and must be called prior to any 
                    // messages sent to Cortana. Attempting to use ReportSuccessAsync, ReportProgressAsync, etc
                    // prior to calling this will produce undefined behavior.

                    VoiceCommand voiceCommand = await voiceServiceConnection.GetVoiceCommandAsync();

                    // Depending on the operation (defined in AdventureWorks:AdventureWorksCommands.xml)
                    // perform the appropriate command.

                    switch(voiceCommand.CommandName)
                    {
                        case "switchLight":
                            var room = voiceCommand.SpeechRecognitionResult.SemanticInterpretation.Properties["dictatedLocation"];                            
                            string message = $"Did you just say {room.FirstOrDefault()}?";
                            await SpeekBackUsingCortana(message, message);
                            break;
                        default:
                            await SpeekBackUsingCortana("Sh! Eva is sleeping...", "Sh! Eva is sleeping...");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Handling Voice Command failed " + ex.ToString());
                }        // Once the asynchronous method(s) are done, close the deferral
                serviceDeferral.Complete();
            }
        }

        private async Task SpeekBackUsingCortana(string spokenText, string displayText)
        {
            VoiceCommandUserMessage userMessage = new VoiceCommandUserMessage();
            userMessage.DisplayMessage = displayText;
            userMessage.SpokenMessage = spokenText;
            VoiceCommandResponse response = VoiceCommandResponse.CreateResponse(userMessage, null);
            await voiceServiceConnection.ReportSuccessAsync(response);

        }

        private void OnVoiceCommandCompleted(VoiceCommandServiceConnection sender, VoiceCommandCompletedEventArgs args)
        {
            if (this.serviceDeferral != null)
            {
                this.serviceDeferral.Complete();
            }
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            System.Diagnostics.Debug.WriteLine("Task cancelled, clean up");

            if (this.serviceDeferral != null)
            {
                //Complete the service deferral
                this.serviceDeferral.Complete();
            }
        }
    }
}
