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
https://developer.android.com/studio/command-line/adb <-- install Android Debug Bridge <br/> 
Still some certificate problem persists. <br/> 
java.security.cert.CertPathValidatorException: Trust anchor for certification path not found.<br/>
Chechking it out tomorrow. 
Google gived few solution to try 1.1 Self Signed Certificate to Blazor server (That might be cause also so... Maybe) <br/>
https://stackoverflow.com/questions/6825226/trust-anchor-not-found-for-android-ssl-connection <br/>
1.2 You can configure your application's Network Security Config(in .MAUI project->Android part), in this case to have your application trust your own self-signed certificates. <br/>
https://developer.android.com/training/articles/security-ssl <-- Read this at least twice! <br/>
https://developer.android.com/training/articles/security-config <br/>
Blaah easy only two steps 1.1 and 1.2 ... What I am going to do today??? <br/>
Actually Self Signed Certificate was already installed so spending time creating one went one kind of training. Problem was Android device and emulator not accepting Self Signed Certificate. There were plenty of work arounds but one that can be applied to SignalR was scarser. Little tweets told that .NET7 these kind of problems are solved. Progress is that emulator works but real device don't. That should be handled if digging deeper in code. At least knowledge on certificates and Android phones increased <br/>
https://github.com/dotnet/maui/discussions/8131<br/>
adevice branch has real phone and android emulator working but code looks horrible and real device needs command as next stated. <br/>
adb reverse tcp:7181 tcp:7181 or adb -s 63fa0277 reverse tcp:7181 tcp:7181 where strange string is your device (adb devices) <br/>
Noticed from the comments of above post that you can configure android device to trust self signed certificate. 

## OpenSSL to test Certificate and Create one
This requires many steps and lots of time. It's not just download and use. Need to add compilers etc... <br/>
https://github.com/openssl/openssl <br/>
but after that you can run "openssl s_client -debug -connect localhost:443" in terminal to get more information on certificates. <br/>
openssl s_client -connect 127.0.0.1:7181 or openssl s_client -debug -connect localhost:7181   <-- run openssl command towards your app port to check certificate in use and if there are any Verification error like self signed certificate.

I used Windows build - Notes for Windows platforms -> Native builds using Visual C++. Required installing via Visual Studio installer Visual C++ (Modify Visual Studio Community 2022 -> select - Desktop development with C++) , Strawberry perl and Nasm from their homepages. 

For noobs Google Environmental Variables adding. Type "echo %PATH%" in terminal to chech current Environment Variable of Path. Note that you need to restart terminal to get changes in effect (in case you made changes and terminal isn't "working" ie find perl or nasm commands. 

## Https in development environment
Going to Http is not a good option. Better to keep development and production as similar as possible to avoid suprices. 
https://auth0.com/blog/using-https-in-your-development-environment/

## .MAUI

MAUI took so much time. Many basic stuff is done totally differently and I didn't found easier way to do it.
OpenSSL and Android certificate problem took the time. More on this in next project.
Good option is to early test .MAUI Android part in production environment with real device (Small screen Android smallest the consumer of ap has) UI parts particularly. Functionality should be same as in windows app (Windows Machine) and UI layout can be tested by shrinking screen horizontaly. 

## Heroku deployment
Deployed to Heroku using https://github.com/jincod/dotnetcore-buildpack#preview buildpack. Needs still Heroku branch to point application away from localhost to Heroku url. Free account downside is no SSL certificate on the house. Have to test how it works (Android part will not work if adevice branch is not in use). <br/><br/>

https://pete-signalr.herokuapp.com/<br/><br/>

openssl s_client -debug -connect https://pete-signalr.herokuapp.com/ <br/>
7956:error:2008F002:BIO routines:BIO_lookup_ex:system lib:crypto/bio/b_addr.c:730:T<br/>
connect:errno=10109<br/>
