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

namespace FifteenPuzzle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button Hole = new Button();
        public MainWindow()
        {
            InitializeComponent();

            int nRows = 4;
            int nColumns = 4;

            for(int i = 0; i < nRows; i++)
            {
                BoardGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < nColumns; i++)
            {
                BoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            Brush buttonColor = Brushes.Red;

            int buttonNumber = 1;
            for(int row = 0; row < nRows; row++)
            {
                for(int col = 0; col < nColumns; col++)
                {
                    Button button = new Button();
                    button.Background = buttonColor;
                    button.Click += TilePress;
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, col);
                    button.Content = buttonNumber.ToString();
                    buttonNumber++;
                    BoardGrid.Children.Add(button);

                    buttonColor = (buttonColor == Brushes.Red) ? Brushes.White : Brushes.Red;
                    Hole = button;
                }
                buttonColor = (buttonColor == Brushes.Red) ? Brushes.White : Brushes.Red;
            }
            Hole.Background = Brushes.Black;
            Hole.Content = "";
        }

        private void TilePress(object sender, RoutedEventArgs e)
        {
            // sender is the object that generated the event
            Button pressedButton = (Button)sender;

            int pressedRow = Grid.GetRow(pressedButton);
            int pressedColumn = Grid.GetColumn(pressedButton);

            int holeRow = Grid.GetRow(Hole);
            int holeColumn = Grid.GetColumn(Hole);

            //if the pressed tile is adjacent to
            //the hole then it is a legal move.
            if ((pressedRow == holeRow && (Math.Abs(holeColumn - pressedColumn) == 1))
                ||
                (pressedColumn == holeColumn && (Math.Abs(holeRow - pressedRow) == 1)))
            {
                //Move the pressed button to where the hole was
                Grid.SetRow(pressedButton, holeRow);
                Grid.SetColumn(pressedButton, holeColumn);

                //Move the hole where the button was
                Grid.SetRow(Hole, pressedRow);
                Grid.SetColumn(Hole, pressedColumn);
            }
        }
    }
}
