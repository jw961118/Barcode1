using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace Item
{
    /// <summary>
    /// Interaction logic for ItemMaster.xaml
    /// </summary>
    public partial class ItemMaster : Window
    {
        private bool _isDirty = false;

        private bool IsInputValid()
        {
            if (txtBox_Name.Text == "" || txtBox_Size.Text == "" || txtBox_Weight.Text == "" || txtBox_Location.Text == "")
            {
                MessageBox.Show("Please fill up all details.");
                return false;
            }

            else
            {
                return true;
            }
        }

        public ItemMaster()
        {
            InitializeComponent();
        }

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            txtBox_Name.Clear();
            txtBox_Size.Clear();
            txtBox_Weight.Clear();
            txtBox_Location.Clear();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (IsInputValid())
            {
                using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-Q1K44I8\\SA;Initial Catalog=Item;Integrated Security=True"))
                {
                    using (SqlCommand Itemcmd = new SqlCommand("dbo.InsertItem", conn))
                    {
                        Itemcmd.CommandType = CommandType.StoredProcedure;

                        Itemcmd.Parameters.AddWithValue("@ItemName", txtBox_Name.Text);
                        Itemcmd.Parameters.AddWithValue("@ItemBatchSize", txtBox_Size.Text);
                        Itemcmd.Parameters.AddWithValue("@ItemWeight", txtBox_Weight.Text);
                        Itemcmd.Parameters.AddWithValue("@ItemLocation", txtBox_Location.Text);

                        try
                        {
                            conn.Open();
                            Itemcmd.ExecuteNonQuery();
                            MessageBox.Show("Done");
                        }

                        catch
                        {
                            MessageBox.Show("Fail");
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        private void btn_Return_Click(object sender, RoutedEventArgs e)
        {
            if (_isDirty)
            {
                if (MessageBox.Show("Do you want to close this window?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.Close(); // Close the window  
                }
                else
                {
                    // Do not close the window  
                }
            }
            else
            {
                this.Close();
            }
        }

        private void txtBox_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isDirty = true;
        }

        private void txtBox_Size_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isDirty = true;
        }

        private void txtBox_Weight_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isDirty = true;
        }

        private void txtBox_Location_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isDirty = true;
        }

    }
}
