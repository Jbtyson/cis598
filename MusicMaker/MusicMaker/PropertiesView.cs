using System.Windows.Forms;

namespace MusicMaker
{
   public partial class PropertiesView : Form
   {
      public PropertiesView()
      {
         InitializeComponent();
         InitializeGridView();
      }

      public void InitializeGridView()
      {
         uxGridViewMotif.Columns.Add("0", "0");
         uxGridViewMotif.Columns.Add("1", "1");
         uxGridViewMotif.Columns.Add("2", "2");
         uxGridViewMotif.Columns.Add("3", "3");
         uxGridViewMotif.Columns.Add("4", "4");
         uxGridViewMotif.Columns.Add("5", "5");
         uxGridViewMotif.Columns.Add("6", "6");
         uxGridViewMotif.Columns.Add("7", "7");
         uxGridViewMotif.Columns.Add("8", "8");
         uxGridViewMotif.Columns.Add("9", "9");
         uxGridViewMotif.Columns.Add("10", "10");
         uxGridViewMotif.Columns.Add("11", "1");
         uxGridViewMotif.Columns.Add("12", "12");

         var temp = new object[] { "8.33", "8.33", "8.33", "8.33", "8.33", "8.33", "8.33", "8.33", "8.33", "8.33", "8.33", "8.33" };
         uxGridViewMotif.Rows.Add(temp);
         uxGridViewMotif.Rows.Add(temp);
         uxGridViewMotif.Rows.Add(temp);
         uxGridViewMotif.Rows.Add(temp);
         uxGridViewMotif.Rows.Add(temp);
         uxGridViewMotif.Rows.Add(temp);
         uxGridViewMotif.Rows.Add(temp);
         uxGridViewMotif.Rows.Add(temp);
         uxGridViewMotif.Rows.Add(temp);
         uxGridViewMotif.Rows.Add(temp);
         uxGridViewMotif.Rows.Add(temp);
         uxGridViewMotif.Rows.Add(temp);
         for (var i = 0; i < 12; i++)
         {
            uxGridViewMotif.Rows[i].HeaderCell.Value = i.ToString();
         }
      }

      private void uxButtonMotifSave_Click(object sender, System.EventArgs e)
      {

      }

      private void uxButtonMotifCancel_Click(object sender, System.EventArgs e)
      {

      }
   }
}
