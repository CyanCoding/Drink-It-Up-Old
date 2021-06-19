using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Drink_It_Up {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Application.Current.MainWindow.Width = 450;
            Application.Current.MainWindow.Height = 600;


            Startup();
        }

        readonly static string TEMP_DIR_ADDRESS = Path.Combine(Path.GetTempPath() + "Drink-It-Up\\");
        readonly static string TEMP_FILE_ADDRESS = TEMP_DIR_ADDRESS + "data.txt";

        private void Startup() {
            string fileData = "1";

            try {
                fileData = File.ReadAllText(TEMP_FILE_ADDRESS);
            }
            catch (DirectoryNotFoundException) {
                Directory.CreateDirectory(TEMP_DIR_ADDRESS);
            }
            catch (FormatException) { // Most likely happens when the file is blank
                fileData = "1";
            }

            frequency.SelectedIndex = Int32.Parse(fileData);
        }

        /// <summary>
        /// Takes the user to the GitHub page on click
        /// </summary>
        private void GitHub_Link(object sender, MouseButtonEventArgs e) {
            System.Diagnostics.Process.Start("https://github.com/CyanCoding/Drink-It-Up");
        }

        /// <summary>
        /// Takes the user to a help page on click
        /// </summary>
        private void HowTo_Link(object sender, MouseButtonEventArgs e) {
            System.Diagnostics.Process.Start("https://github.com/CyanCoding/Drink-It-Up/blob/master/HOWTO.md");
        }

        /// <summary>
        /// Triggers when the user changes the ComboBox.
        /// Saves the selected item in the temp file.
        /// 
        /// This also executes at the beginning of the program when it changes from the saved value
        /// </summary>
        private void FrequencyChanged(object sender, SelectionChangedEventArgs e) {
            if (!Directory.Exists(TEMP_DIR_ADDRESS)) {
                Directory.CreateDirectory(TEMP_DIR_ADDRESS);
            }

            string fileData = "" + frequency.SelectedIndex;

            try {
                File.WriteAllText(TEMP_FILE_ADDRESS, fileData);
            }
            catch (IOException) {
                // File is probably being used by another task
                Console.WriteLine("Unable to write to file!");
            }
        }
    }
}
