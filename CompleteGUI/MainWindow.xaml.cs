using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompleteGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource cancellationTokenSource;
        CancellationToken cancellationToken;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            btnProcess.IsEnabled = false;//首先置灰按钮
            
            //下面要处理ProgressBar的效果了
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;
            int completedPercent = 0;
            //每隔0.5秒 进度条多完成10%
            for (int i = 0; i < 10; i++)
            {
                if (cancellationToken.IsCancellationRequested) break;
                try
                {
                    //下面这段是异步执行的
                    await Task.Delay(500, cancellationToken);
                    completedPercent = (i + 1) * 10;
                }
                catch (TaskCanceledException ex)
                {
                    completedPercent = i * 10;
                }
                progressBar.Value = completedPercent;
            }

            //如果点击了Cancel按钮，上面的异步操作提前结束；或者异步操作正常结束
            string message = cancellationToken.IsCancellationRequested ? string.Format($"Process was cancelled at {completedPercent}%.") : "Process completed normally.";
            MessageBox.Show(message, "Completion Status");

            progressBar.Value = 0;
            btnProcess.IsEnabled = true;
            btnCancel.IsEnabled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if(!btnProcess.IsEnabled)
            {
                btnCancel.IsEnabled = false;
                cancellationTokenSource.Cancel();
            }
        }
    }
}