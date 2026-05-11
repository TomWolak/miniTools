  // -------------------- Methode zur Nutzung des Systemdatums ------------------ // 
       // --- Um sie zu verwenden, muss die Variable „Systemdatum“ erstellt werden --- //
        [UserCodeMethod]
        public void SetSystemdatum()
        {
            string offsetText = this.Systemdatum;
            int tageOffset = 0;

            if (!string.IsNullOrWhiteSpace(offsetText))
            {
                offsetText = offsetText.Trim();

         	   if (offsetText == "leeres_Datumsfeld")
		   		 {
		         this.Systemdatum = "TT.MM.JJJJ";
		         Report.Info("Systemdatum was set for an empty date field: " + this.Systemdatum);
		         Delay.Seconds(5);
		          return;
		     	}
                   
                   
             	if (offsetText.Contains("."))
        		{
            	this.Systemdatum = offsetText;
           		 Report.Info("Systemdatum was taken directly from the variable table: " + this.Systemdatum);
            	Delay.Seconds(5);
            	return;
        		}  
                                                           
                
                if (!int.TryParse(offsetText, out tageOffset))
                {
                    throw new ArgumentException(
  					  "Invalid value for Systemdatum. Use, for example, 0, +7, -1, or -365, or leeres_Datumsfeld.");
                }
            }

            this.Systemdatum = System.DateTime.Today
                .AddDays(tageOffset)
                .ToString("dd.MM.yyyy");

            Report.Info("Calculated Systemdatum (to be entered / validated): " + this.Systemdatum);
            Delay.Seconds(5);
        }
       
		// ------------ Ende der Methode zur Nutzung des Systemdatums -------------- //   



        // ------------ Unterschiede zwischen Key sequence und Validation:  -------------- //   
        Note: TS for input (key sequence) and for validation are different:

            1. For input they look like this:
            if (offsetText == "leeres_Datumsfeld")
            {
            this.Systemdatum = "TT.MM.JJJJ";
            2. For validation they look like this:
            if (offsetText == "leeres_Datumsfeld")
            {
            this.Systemdatum = "";

            plus a different adapter:
            .//label[@innertext='fällig am']/..//div[@class~'textfield']/.//input[@tagvalue=$Systemdatum]

            The name could be:
            "Systemdatum_faellig_am_validierung_TagValue_Equal"

        // ------------------------------------------------------------------------------ //