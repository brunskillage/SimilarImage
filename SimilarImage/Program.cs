using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using SimilarImage.Controllers;
using SimilarImage.Services;
using SimilarImage.Ui;
using SimpleInjector;

namespace SimilarImage
{
    internal static class Program
    {

        public static Container InjectionContainer;

        [STAThread]
        private static void Main()
        {
            InjectionContainer = new Container();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            // Add the event handler for handling UI thread exceptions to the event.
            // Application.ThreadException += new ThreadExceptionEventHandler(Form.Form1_UIThreadException);

            // Set the unhandled exception mode to force all Windows Forms errors to go through
            // our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException +=
                CurrentDomain_UnhandledException;

            ConfigureInjection(InjectionContainer);
            var form =  InjectionContainer.GetInstance<MetroMainForm>();
            Application.Run(form);
        }

        private static void ConfigureInjection(Container container)
        {
            Assembly appAssembly = typeof(MetroMainForm).Assembly;

            var registrations =
                from type in appAssembly.GetExportedTypes()
                where (type.Namespace == "SimilarImage.Services" || type.Namespace == "SimilarImage.Controllers")
                select type;

            foreach (var reg in registrations)
                container.Register(reg, reg, Lifestyle.Singleton);

            container.Register<MetroMainForm>(Lifestyle.Singleton);

            container.Verify();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception) e.ExceptionObject;
                string errorMsg = "An application error occurred. Please contact the adminstrator " +
                                  "with the following information:\n\n";

                // Since we can't prevent the app from terminating, log this to the event log.
                if (!EventLog.SourceExists("ThreadException"))
                {
                    EventLog.CreateEventSource("ThreadException", "Application");
                }

                // Create an EventLog instance and assign its source.
                EventLog myLog = new EventLog();
                myLog.Source = "ThreadException";
                myLog.WriteEntry(errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show("Fatal Non-UI Error",
                        "Fatal Non-UI Error. Could not write the error to the event log. Reason: "
                        + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }
    }
}