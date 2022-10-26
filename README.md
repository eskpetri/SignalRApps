# SignalRApps

Testing SignalR to gain good access to deliver data to same app family on multible devices. .MAUI xaml differs a lot from WPF xaml. 
Host: localhost:7181
Maybe I deliver this to heroku cloud service and add crypting of messages without saving them. Then you should have quite secure chat. :)

## Android real device

To use Android device on developement in Visual Studio you need to set up phone for development and enable USB debugging.
https://learn.microsoft.com/en-us/xamarin/android/get-started/installation/set-up-device-for-development

To access localhost via cable ie USB debugging you have to run adb command to gain access. <br/>
adb reverse tcp:7181 tcp:7181
https://developer.android.com/studio/command-line/adb <-- install Android Debug Bridge

## .MAUI

Still some certificate problem persists. java.security.cert.CertPathValidatorException:...
Chechking it out tomorrow. MAUI took so much time. Many basic stuff is done totally differently and I didn't found easier way to do it. 
