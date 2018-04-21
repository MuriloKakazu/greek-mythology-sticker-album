using stickeralbum.Generics;
using System;
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
using System.Windows.Shapes;

namespace stickeralbum
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LinkedList<Int32> linkedList = new LinkedList<Int32>();
            linkedList.Add(2);
            linkedList.Add(5);
            linkedList.Add(10);
            linkedList.Add(20);
            linkedList.Add(50);

            Console.WriteLine(linkedList[0]);
            Console.WriteLine(linkedList[1]);
            Console.WriteLine(linkedList[2]);
            Console.WriteLine(linkedList[3]);
            Console.WriteLine(linkedList[4]);

            linkedList[0] = 100;
            linkedList[1] = 200;
            linkedList[2] = 300;
            linkedList[3] = 400;
            linkedList[4] = 500;

            linkedList.ToList().ForEach(x => Console.WriteLine(String.Format($"R${x},00")));
            /* Output:
             * R$2,00
             * R$5,00
             * R$10,00
             * [...]
             */

            var xyz = linkedList.ToList();

            InitializeComponent();
        }
    }
}
