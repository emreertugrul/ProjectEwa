﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.VoiceCommands;

namespace Eva.IOT.CortanaListener
{

    public sealed class EvaCortanaListener : IBackgroundTask
    {
        VoiceCommandServiceConnection voiceServiceConnection;
        BackgroundTaskDeferral serviceDeferral;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            serviceDeferral = taskInstance.GetDeferral();
            taskInstance.Canceled += OnTaskCanceled;
            var triggerDetails = taskInstance.TriggerDetails as AppServiceTriggerDetails;

            if (triggerDetails != null && triggerDetails.Name == "EvaListener")
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
                        default:
                            VoiceCommandUserMessage userMessage = new VoiceCommandUserMessage();
                            userMessage.DisplayMessage = "shh! Eva is still sleeping...";
                            userMessage.SpokenMessage = "shh! Eva is still sleeping...";
                            VoiceCommandResponse response = VoiceCommandResponse.CreateResponse(userMessage, null);
                            await voiceServiceConnection.ReportSuccessAsync(response);
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