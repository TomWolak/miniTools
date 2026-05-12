           //  ------------------------------ Method --------------------------------------
            // Die Bedienung eines Checkboxes unter Berücksichtigung seines aktuellen Zustands
            
      public void SetCheckbox(bool shouldBeChecked)
		{
		    var checkbox = repo. ... .Beleg_drucken_Checkbox_zustandsabhaengiger_Klick;
		
		    SetCheckboxByAriaChecked(checkbox, shouldBeChecked);
		}

		public void SetCheckboxByAriaChecked(Adapter checkbox, bool shouldBeChecked)
		{
		    if (checkbox == null)
		        throw new ArgumentNullException("Checkbox adapter is null");
		
		    string state = checkbox.GetAttributeValue<string>("aria-checked") ?? "false";
		    bool isChecked = state.Equals("true", StringComparison.OrdinalIgnoreCase);
		
		    if (isChecked != shouldBeChecked)
		        checkbox.Click();
		} 
     
			// --------------- Ende Method -------------------------- //

            // Beleg_drucken_Checkbox_zustandsabhaengiger_Klick:
            // xpath .//?[@innertext=' ... ']//div[@class~'hux-ui-checkbox']//div[@class~'hux-ui-checkitem hux-ui-check-item']