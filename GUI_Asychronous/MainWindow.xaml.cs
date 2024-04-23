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

namespace GUI_Asychronous
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //使用Lambda表达式创建一个事件
            startWorkButton.Click += async (sender, e) =>
            {
                SetGuiValues(false, "Work Started");//设置窗口表现
                await DoSomeWork();
                SetGuiValues(true, "Work Finished");
            };
        }

        private Task DoSomeWork()
        {
            return Task.Delay(2500);
        }

        private void SetGuiValues(bool v1, string v2)
        {
            startWorkButton.IsEnabled = v1;
            workStartedTextBlock.Text = v2;
        }

        /*private async void btnDoStuff_Click(object sender, RoutedEventArgs e)
        {
            btnDoStuff.IsEnabled = false;//置灰这个button
            lblStatus.Content = "Doing Stuff";//改变按钮上方的文字
            await Task.Delay(4000);//阻塞当前线程4秒钟，模拟处理别的任务
            lblStatus.Content = "Not Doing Anything";
            btnDoStuff.IsEnabled = true;
        }*/


    }
}