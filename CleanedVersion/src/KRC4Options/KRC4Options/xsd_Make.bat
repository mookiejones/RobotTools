cd ..\..

"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\xsd.exe" /c  XMLDATA\\ConfigXML.xsd
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\xsd.exe" /c  XMLDATA\\Authentication.xsd

cd %1XMLDATA
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\xsd.exe" /c  ConfigXML.xsd
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\xsd.exe" /c  Authentication.xsd
pause