      // =========== DelayWithCountdown start ============ //

        [UserCodeMethod]
        public static void DelayWithCountdown(int seconds)
        {
            Ranorex.Report.Info("Start des Ranorex-Testschritts 'Delay'");
            Ranorex.Report.Info("Auf dem Bildschirm wird die verbleibende Zeit bis zum Ende des Schritts 'Delay' angezeigt");
     
            System.DateTime endTime = System.DateTime.Now.AddSeconds(seconds);

            CountdownForm form = null;
            ManualResetEventSlim ready = new ManualResetEventSlim(false);

            Thread uiThread = new Thread(() =>
            {
                WinForms.Application.EnableVisualStyles();

                form = new CountdownForm(endTime);
                ready.Set();

                WinForms.Application.Run(form);
            });

            uiThread.SetApartmentState(ApartmentState.STA);
            uiThread.IsBackground = true;
            uiThread.Start();

            ready.Wait();

            while (System.DateTime.Now < endTime)
            {
                Thread.Sleep(200);
            }

            if (form != null && !form.IsDisposed)
            {
                try
                {
                    form.BeginInvoke(new Action(() => form.Close()));
                }
                catch { }
            }
        }

        // --------------- form ------------------------ //

        private class CountdownForm : WinForms.Form
        {
            private readonly System.DateTime _endTime;
            private readonly WinForms.Label _label;
            private readonly WinForms.Timer _timer;

            public CountdownForm(System.DateTime endTime)
            {
                _endTime = endTime;

                Width = 360;
                Height = 80;
                // StartPosition = WinForms.FormStartPosition.CenterScreen;  // Central position
                StartPosition = WinForms.FormStartPosition.Manual;           // Screen Position
                var area = WinForms.Screen.PrimaryScreen.WorkingArea;
				Location = new Point(
				    area.Left + (area.Width / 4) - (Width / 2),
				    area.Top + (area.Height * 3 / 4) - (Height / 2)          
				);
                FormBorderStyle = WinForms.FormBorderStyle.FixedToolWindow;
                TopMost = true;
                Opacity = 0.75;
                ShowInTaskbar = true; 
                Text = "Ranorex-Testschritt 'Delay'";

                Shown += (s, e) => ForceToFront();
                Activated += (s, e) => ForceToFront();

                _label = new WinForms.Label
                {
                    Dock = WinForms.DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold)
                };

                Controls.Add(_label);

                _timer = new WinForms.Timer();
                _timer.Interval = 250;
                _timer.Tick += (s, e) => UpdateText();
                _timer.Start();

                UpdateText();
            }

            private void UpdateText()
            {
                int remaining = (int)Math.Ceiling((_endTime - System.DateTime.Now).TotalSeconds);

                if (remaining < 0)
                    remaining = 0;

                _label.Text = remaining == 1
                    ? "Delay läuft - noch 1 Sekunde"
                    : string.Format("Delay läuft - noch {0} Sekunden", remaining);

                ForceToFront();

                if (remaining <= 0)
                    Close();
            }

            private void ForceToFront()
            {
                try
                {
                    TopMost = false;
                    TopMost = true;
                    BringToFront();
                    Activate();
                }
                catch { }
            }
        }

        // =========== DelayWithCountdown Ende ============ //