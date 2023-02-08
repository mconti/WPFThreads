using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BigInteger _counter = new BigInteger( 0 );
        static readonly object _locker = new object();

        Thread thread1;
        Thread thread2;
        Thread thread3;
        Thread thread4;
        Thread thread5;
        Thread thread6;


        public MainWindow()
        {
            InitializeComponent();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _counter = new BigInteger(0);
        
            thread1 = new Thread(IncrementCounter1);
            thread1.Start();

            thread2 = new Thread(IncrementCounter2);
            thread2.Start();

            thread3 = new Thread(IncrementCounter3);
            thread3.Start();

            thread4 = new Thread(IncrementCounter1);
            thread4.Start();

            thread5 = new Thread(IncrementCounter2);
            thread5.Start();

            thread6 = new Thread(IncrementCounter3);
            thread6.Start();

            Thread thread7 = new Thread(Fine);
            thread7.Start();

        }

        private void Fine()
        {
            while( true )
            {
                if (thread1.ThreadState == ThreadState.Stopped &&
                    thread2.ThreadState == ThreadState.Stopped &&
                    thread3.ThreadState == ThreadState.Stopped &&
                    thread4.ThreadState == ThreadState.Stopped &&
                    thread5.ThreadState == ThreadState.Stopped &&
                    thread6.ThreadState == ThreadState.Stopped
                )
                    break;
            }
            Button_Click(null, null);
        }

        private void IncrementCounter1()
        {
            for (int i = 0; i < 1000; i++)
            {
                lock (_locker)
                {
                    // corsa critica
                    _counter++;

                    Dispatcher.Invoke(() =>
                    {
                        CounterLabel1.Foreground = Brushes.LightBlue;
                        CounterLabel1.Text = _counter.ToString();
                    });
                }
            }
        }
        private void IncrementCounter2()
        {
            for (int i = 0; i < 1000; i++)
            {
                lock (_locker)
                {
                    // corsa critica
                    _counter++;

                    Dispatcher.Invoke(() =>
                    {
                        CounterLabel2.Foreground = Brushes.OrangeRed;
                        CounterLabel2.Text = _counter.ToString();
                    });
                }
            }
        }
        private void IncrementCounter3()
        {
            for (int i = 0; i < 1000; i++)
            {
                lock (_locker)
                {
                    // corsa critica
                    _counter += _counter;

                    Dispatcher.Invoke(() =>
                    {
                        CounterLabel3.Foreground = Brushes.LimeGreen;
                        CounterLabel3.Text = _counter.ToString();
                    });
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                CounterLabel1.Text = _counter.ToString();
                CounterLabel2.Text = _counter.ToString();
                CounterLabel3.Text = _counter.ToString();
                MessageBox.Show(_counter.GetBitLength().ToString());
            });
            
        }
    }
}
