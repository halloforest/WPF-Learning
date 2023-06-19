using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace MeetingNotes_V1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WaveInEvent waveIn;
        private MemoryStream audioDataStream;
        private SpeechRecognizer speechRecognizer;
        private CancellationTokenSource cancellationTokenSource;
        private SpeechConfig speechConfig;
        private AudioConfig audioConfig;
        private static string outputFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "adesso Meeting Dictation.txt");

        public MainWindow()
        {
            InitializeComponent();

            cancellationTokenSource = new CancellationTokenSource();

            startButton.Click += StartButton_Click;
            stopButton.Click += StopButton_Click;           
        }

        private void configRecognizer(SpeechRecognizer speechRecognizer)
        {
            speechRecognizer.Recognized += (s, e) =>
            {
                if (e.Result.Reason == ResultReason.RecognizedSpeech)
                {
                    Dispatcher.Invoke(() => { dictatedTextBox.Text += e.Result.Text + "\n\n"; });
                    File.AppendAllText (outputFile, e.Result.Text + "\n\n");
                }
                else if (e.Result.Reason == ResultReason.NoMatch)
                {
                    Dispatcher.Invoke(() => { dictatedTextBox.Text += "NOMATCH: Speech could not be recognized."; });
                }
            };

            speechRecognizer.Recognizing += (s, e) =>
            {
                Dispatcher.Invoke(() => { infoTextBlock.Text = "*** Dictating... ***\n" + e.Result.Text; });
            };

            speechRecognizer.Canceled += (s, e) =>
            {
                Dispatcher.Invoke(() => { infoTextBlock.Text = "*** Dictating canceled ***\n"; });
            };

            speechRecognizer.SessionStopped += (s, e) =>
            {
                Dispatcher.Invoke(() => { infoTextBlock.Text = "*** Dictating stopped ***\n"; });
            };
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            cancellationTokenSource.Cancel();
            speechRecognizer.StopContinuousRecognitionAsync();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            startDictating();
        }

        private async void startDictating()
        {
            if(!speechConfiguration()) return;
            if(!audioSourceConfiguration()) return;

            speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);
            configRecognizer(speechRecognizer);

            try
            {
                infoTextBlock.Text += "*** Start dictating ***\n";
                dictatedTextBox.Text += "*** " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ***\n";
                File.AppendAllText(outputFile, "*** " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ***\n");
                await speechRecognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                infoTextBlock.Text = ($"Failed to start recognition: {ex.Message}");
            }
        }


        private bool speechConfiguration()
        {
            speechConfig = SpeechConfig.FromSubscription("5aeb461b7b2f4b9c844dc4fae6d02b21", "switzerlandnorth");
            if (null == chooseLanguage.SelectedItem)
            {
                infoTextBlock.Text = "*** Please select the language! ***\n";
                return false;
            }

            speechConfig.SpeechRecognitionLanguage = ((ComboBoxItem)chooseLanguage.SelectedItem).Tag as string;
            infoTextBlock.Text = "Language selected: " + ((ComboBoxItem)chooseLanguage.SelectedItem).Content as string + " (" + speechConfig.SpeechRecognitionLanguage + ")\n";
            return true;
        }

        private bool audioSourceConfiguration()
        {
            if (null == chooseSource.SelectedItem)
            {
                infoTextBlock.Text = "*** Please select the audio source! ***\n";
                return false;
            }

            var audioSource = ((ComboBoxItem)chooseSource.SelectedItem).Tag as string;

            if ("microfon" == audioSource)
            {
                audioConfig = AudioConfig.FromDefaultMicrophoneInput();
                infoTextBlock.Text += "Audio Source selected: Microfon\n";
                return true;
            }
            else if ("soundCard" == audioSource)
            {                
                infoTextBlock.Text += "Implementation is still open\n";
                return false;
            }
            else
            {
                infoTextBlock.Text += "*** Error: Audio Source ***\n";
                return false;
            }            
        }
    }
}
