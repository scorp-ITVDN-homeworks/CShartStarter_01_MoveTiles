using System;
using System.Collections.Generic;
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

namespace MoveTiles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PreviewMouseMove += GridMouseMove;
            SizeChanged += OnWinSizeChanged;
        }

        private Point tilePoint;

        public void OnWinSizeChanged(object sender, SizeChangedEventArgs args)
        {
            gridForTile.Height = dp.ActualHeight - 30;
        }

        public void GridMouseMove(object sender, MouseEventArgs e)
        {            
            //base.OnPreviewMouseMove(e);
            Point p = e.GetPosition(gridForTile);

            int xPos = 0;
            int yPos = 0;
            double deltaX = 0;
            double deltaY = 0;

            foreach (ColumnDefinition colDef in gridForTile.ColumnDefinitions)
            {
                deltaX += colDef.ActualWidth;
                xPos++;
                if(deltaX > p.X)
                {
                    xPosition.Text = xPos.ToString();
                    break;
                }                
            }

            foreach(RowDefinition rowDef in gridForTile.RowDefinitions)
            {
                deltaY += rowDef.ActualHeight;
                yPos++;
                if (deltaY > p.Y)
                {
                    yPosition.Text = yPos.ToString();
                    break;
                }
            }

            if (Mouse.Captured == tileWithText && e.LeftButton == MouseButtonState.Pressed)
            {
                //Point setP = blackTile.TranslatePoint(p,blackTile);
                //UIElement element = Mouse.Captured as UIElement;
                TranslateTransform transform = new TranslateTransform();
                transform.X = p.X - tilePoint.X;
                transform.Y = p.Y - tilePoint.Y;       
                tileWithText.RenderTransform = transform;                
            }
        }

        private void tileWithText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {            
            Mouse.Capture(tileWithText);
            //MessageBox.Show(Mouse.Captured.ToString());
            //DragDrop.DoDragDrop(blackTile, blackTile, DragDropEffects.Copy);
            tilePoint = Mouse.GetPosition(gridForTile);
        }

        private void tileWithText_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Grid.SetColumn(Mouse.Captured as UIElement, Int32.Parse(xPosition.Text) - 1);
            Grid.SetRow(Mouse.Captured as UIElement, Int32.Parse(yPosition.Text) - 1);
            Mouse.Capture(null);
            tileWithText.RenderTransform = new TranslateTransform(0,0);
        }
    }
}
