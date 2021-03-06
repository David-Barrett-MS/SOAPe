# SOAPe
An application that allows testing of SOAP implementations, with extra features targeting Exchange Web Services.  It includes an HTTP listener which means that it can be used to test Exchange push subscriptions, and there are various EWS templates included for convenience.

OAuth is supported.  The default application registration supports delegated access only (so the user must log on for SOAPe to acquire a token).  Application access can be configured (using secret key or certificate) by registering an application in the tenant being accessed.

SOAPe also incorporates an EWS trace analyser that can import traces from other EWS applications.  SOAPe’s log viewer allows you to analyse EWS trace logs quickly and identify errors or pinpoint particular events.

![SOAPe Interface](https://github.com/David-Barrett-MS/SOAPe/blob/master/SOAPe/Docs/Images/SOAPe%20OAuth.png?raw=true)

The log viewer allows easy analysis of EWS traces, and includes error highlighting:

![SOAPe Log Viewer](https://github.com/David-Barrett-MS/SOAPe/blob/master/SOAPe/Docs/Images/SOAPe%20Log%20Viewer.png?raw=true)
