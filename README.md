# SignalRApps

Decided to use these README.md files as a diary/notes for myself and they maybe usefull to others too making the same walk. Also trying to commend code as detailed as possible at least give idea or guidance.

Testing SignalR to gain good access to deliver data to same app family on multible devices. .MAUI xaml differs a lot from WPF xaml. <br/>
Host: localhost:7181 <br/>
Maybe I deliver this to heroku cloud service and add crypting of messages without saving them. Then you should have quite secure chat. :)

## Android real device and emulator

To use Android device on developement in Visual Studio you need to set up phone for development and enable USB debugging.
https://learn.microsoft.com/en-us/xamarin/android/get-started/installation/set-up-device-for-development

To access localhost via cable ie USB debugging you have to run adb command to gain access. <br/>
adb reverse tcp:7181 tcp:7181 <br/>
https://developer.android.com/studio/command-line/adb <-- install Android Debug Bridge
Still some certificate problem persists. <br/> java.security.cert.CertPathValidatorException: Trust anchor for certification path not found.<br/>Chechking it out tomorrow. 
Google gived few solution to try 1. Self Signed Certificate (That might be cause also so...)
https://stackoverflow.com/questions/6825226/trust-anchor-not-found-for-android-ssl-connection


## OpenSSL to test Certificate and Create one
This requires many steps and lots of time. It's not just download and use. Need to add compilers etc... <br/>
https://github.com/openssl/openssl <br/>
but after that you can run "openssl s_client -debug -connect localhost:443" in terminal to get more information on certificates. 

I used Windows build - Notes for Windows platforms -> Native builds using Visual C++. Required installing via Visual Studio installer Visual C++, Strawberry perl and Nasm from their homepages. 

For noobs Google Environmental Variables adding. Type "echo %PATH%" in terminal to chech current Environment Variable of Path. Note that you need to restart terminal to get changes in effect (in case you made changes and terminal isn't "working" ie find perl or nasm commands. 

## .MAUI

MAUI took so much time. Many basic stuff is done totally differently and I didn't found easier way to do it. 
