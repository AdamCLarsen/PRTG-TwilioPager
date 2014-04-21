Quick Start
-----------
Read up on PRTG execute a batch file as notification. [http://www.paessler.com/knowledgebase/en/topic/2543-how-can-i-execute-a-batch-file-as-notification]

Make sure .Net Framework 4.5 is install on your core server.

Enable "Execute Program" in your notification, select "Prtg.pager.twilio" from the program files, if it doesn't show up then you likely don't have it in the correct folder.
First parameter is the phone number to send the message to, the the rest is the message that's send

Example parameters: 5555551234 %group %device %shortname: %status %down (%message)

You will also need to edit the "Prtg.Pager.Twilio.exe.config" file, to enter your Twilio API keys and source phone number you want to use.

[Download] (https://github.com/AdamCLarsen/PRTG-TwilioPager/raw/master/bin/Prtg-TwilioPager.zip)